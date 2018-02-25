﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace GCDCore.Engines
{
    class ExcelXMLDocument
    {
        private string _template;
        private XmlDocument xmlDoc;
        private XmlNamespaceManager nsmgr;
        private Dictionary<string, NamedRange> dicNamedRanges;

        public ExcelXMLDocument(string template)
       {
            _template = template;

            //read template into XML document
            xmlDoc = new XmlDocument();
            xmlDoc.Load(template);

            //xpaths needs to be specified with namespace, see e.g. https://stackoverflow.com/questions/36504656/how-to-select-xml-nodes-by-position-linq-or-xpath
            nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

            //get named ranges
            ParseNamedRanges();
        }

        private void ParseNamedRanges()
        {
            dicNamedRanges = new Dictionary<string, NamedRange>();

            XmlNodeList NamedRangeNodes = xmlDoc.SelectNodes(".//ss:Names/ss:NamedRange", nsmgr); // gets the cell with the named cell name

            foreach (XmlNode NamedRange in NamedRangeNodes)
            {
                string name = NamedRange.Attributes["ss:Name"].Value;

                string refersto = NamedRange.Attributes["ss:RefersTo"].Value;
                //match R*C*
                Regex r = new Regex(@".*!R(.+)C(.+)", RegexOptions.IgnoreCase);
                Match m = r.Match(refersto);
                string sRow = m.Groups[1].Value;
                int iRow = int.Parse(sRow);

                string sCol = m.Groups[2].Value;
                int iCol = int.Parse(sCol);

                NamedRange oNamedRange = new Engines.NamedRange();
                oNamedRange.name = name;
                oNamedRange.col = iCol;
                oNamedRange.row = iRow;

                dicNamedRanges.Add(name, oNamedRange);
            }

        }

        public void UpdateRow(string NamedRange, Dictionary<string, string> NamedRangeValues)
        {
            //get row
            NamedRangeValues[NamedRange] = NamedRangeValues["TemplateRowName"];

            XmlNode TemplateRow = xmlDoc.SelectSingleNode(".//ss:Row[ss:Cell[ss:NamedCell[@ss:Name='" + NamedRange + "']]]", nsmgr); // gets the cell with the named cell name
            SetNamedCellValue(TemplateRow, NamedRangeValues);
        }

        public void CloneRow(string NamedRange, int offset, Dictionary<string, string> NamedRangeValues)
        {
            NamedRangeValues[NamedRange] = NamedRangeValues["TemplateRowName"];

            //find areal row
            XmlNode TemplateRow = xmlDoc.SelectSingleNode(".//ss:Row[ss:Cell[ss:NamedCell[@ss:Name='" + NamedRange + "']]]", nsmgr); // gets the cell with the named cell name
            XmlNode TemplateRowClone = TemplateRow.Clone();

            SetNamedCellValue(TemplateRowClone, NamedRangeValues);

            //Find reference row
            //Example: Our template row is row 4, offset is one, we will insert at row 5, and use row for as the node before
            NamedRange oNamedRange = dicNamedRanges[NamedRange];
            int NamedRangeRow = oNamedRange.row;
            int ReferenceRowBeforeInsert = NamedRangeRow + offset - 1;
            int InsertRowNumber = NamedRangeRow + offset; //we are inserting the row just below the reference row

            XmlNode ReferenceRowNode = xmlDoc.SelectSingleNode(".//ss:Row[position() >= " + ReferenceRowBeforeInsert + "]", nsmgr);

            XmlNode parent = TemplateRow.ParentNode;
            parent.InsertAfter(TemplateRowClone, TemplateRow);


            UpdateReference(InsertRowNumber);

        }

        /// <summary>
        /// Updates named cells in the node with the values in StatValues
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="nsmgr"></param>
        /// <param name="StatValues"></param>
        private void SetNamedCellValue(XmlNode xmlNode, Dictionary<string, string> StatValues)
        {
            foreach (KeyValuePair<string, string> kvp in StatValues)
            {
                String NamedCell = kvp.Key;
                String Value = kvp.Value;

                //find named cells
                XmlNodeList NamedCells = xmlNode.SelectNodes(".//ss:Cell[ss:NamedCell[@ss:Name='" + NamedCell + "']]", nsmgr); // gets the cell with the named cell name
                if (NamedCells.Count == 1)
                {
                    //if there is exactly one match, update data
                    XmlNode CellData = NamedCells[0].SelectSingleNode("ss:Data", nsmgr);
                    CellData.InnerText = Value;
                }

            }
        }

        public void Save(string path)
        {
            xmlDoc.Save(path);
        }

        private void UpdateReference(int rownumber)
        {
            Dictionary<string, NamedRange> dicUpdatedNamedRanges = new Dictionary<string, NamedRange>();
            foreach (NamedRange oNamedRange in dicNamedRanges.Values)
            {
                if (oNamedRange.row > rownumber)
                {
                    oNamedRange.row += 1;
                }
                dicUpdatedNamedRanges.Add(oNamedRange.name, oNamedRange);
            }

            XmlNode TableNode = xmlDoc.SelectSingleNode(".//ss:Table", nsmgr); // gets the cell with the named cell name
            string OrigExpandedRowCount = TableNode.Attributes["ss:ExpandedRowCount"].Value;
            int iOrigExpandedRowCount = int.Parse(OrigExpandedRowCount);
            int iNewExpandedRowCount = iOrigExpandedRowCount + 1; //add new row twice (once for area, once for volume)
            TableNode.Attributes["ss:ExpandedRowCount"].Value = iNewExpandedRowCount.ToString();

            //update formulas
            UpdateFormulaReferences(rownumber);

            UpdateSumFormulaRange(rownumber);

            dicNamedRanges =  dicUpdatedNamedRanges;

            UpdateNamedRangesXML();

        }

        private void UpdateFormulaReferences(int rownumber)
        {
            //first, find all rows, the loop through rows that are > rownumber
            XmlNodeList AllRows;
            AllRows = xmlDoc.SelectNodes(".//ss:Row", nsmgr); // gets the cell with the named cell name

            for (int RowIndex = 0; RowIndex < AllRows.Count; RowIndex++)
            {
                //get row
                XmlNode CurrentRow = AllRows[RowIndex];

                //select cells with formulas

                XmlNodeList CellsWithFormulas = CurrentRow.SelectNodes(".//ss:Cell[@ss:Formula]", nsmgr);

                //for each formula, check if it contains a relative reference, pattern "=R[-5]C[-4]/R[-10]C10", e.g. R[-5]C[-4]
                foreach (XmlNode currentCell in CellsWithFormulas)
                {
                    //get formula
                    string formula = currentCell.Attributes["ss:Formula"].Value;

                    //match for R
                    //var pattern = @"R\[-(\d+)\]C"; //matches only one in =SUM(R[-1]C:R[-1]C)
                    var pattern = @"(R\[-)(\d+)(\]C)";

                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                    MatchCollection mc = r.Matches(formula);

                    if (mc.Count > 0)
                    {

                        string NewFormula = "";
                        int textindex = 0;
                        for (int matchindex = 0; matchindex < mc.Count; matchindex++)
                        {
                            Match m = mc[matchindex];
                            NewFormula = NewFormula + formula.Substring(textindex, m.Index - textindex);

                            string sRow = m.Groups[2].Value;
                            int iRow = int.Parse(sRow);
                            int correctionrow = 0;
                            int referencerow = RowIndex - iRow;
                            if (referencerow <= rownumber && RowIndex > rownumber)
                            {
                                correctionrow = 1;
                            }

                            string replace = m.Groups[1].Value + (iRow + correctionrow) + m.Groups[3].Value;

                            NewFormula = NewFormula + replace;

                            textindex = m.Index + m.Length;

                        }

                        NewFormula = NewFormula + formula.Substring(textindex);

                        currentCell.Attributes["ss:Formula"].Value = NewFormula;
                    }


                }

            }

        }

        private void UpdateSumFormulaRange(int InsertRowNumber)
        {
            //first, find all rows, the loop through all rows to find formulas
            //we need to do it this way to know what the row number is

            XmlNodeList AllRows;
            AllRows = xmlDoc.SelectNodes(".//ss:Row", nsmgr); // gets the cell with the named cell name

            for (int RowIndex = 0; RowIndex < AllRows.Count; RowIndex++)
            {
                //get row
                XmlNode CurrentRow = AllRows[RowIndex];

                //select cells with formulas
                XmlNodeList CellsWithFormulas = CurrentRow.SelectNodes(".//ss:Cell[@ss:Formula]", nsmgr);

                //for each formula, check if it contains a relative reference, pattern "=R[-5]C[-4]/R[-10]C10", e.g. R[-5]C[-4]
                foreach (XmlNode currentCell in CellsWithFormulas)
                {
                    //get formula
                    string formula = currentCell.Attributes["ss:Formula"].Value;
                    int FormulaRow = RowIndex + 1; //RowIndex is zero based, Excel Rows are one-based
                    ExcelSumFormula oExcelFormula = ParseSumFormula(formula, FormulaRow);

                    if(oExcelFormula != null)
                    {
                        ExcelSumFormula oUpdatedExcelFormula = UpdateSumFormula(oExcelFormula, InsertRowNumber);
                        currentCell.Attributes["ss:Formula"].Value = oUpdatedExcelFormula.GetFormula();
                    }

                }

            }

        }

        /// <summary>
        /// Updates the sum formula range if a new row is added just below the range or inside the range
        /// </summary>
        /// <param name="oExcelSumFormula"></param>
        /// <param name="InsertRow"></param>
        /// <returns></returns>
        private ExcelSumFormula UpdateSumFormula(ExcelSumFormula oExcelSumFormula, int InsertRow)
        {
            Boolean ExpandRange = false;

            //if new row is inserted just below range, expand range
            //Example a. Range is from 5:5, new row inserted at row 5, range exanded to 4:5
            //Example a. Range is from 2:3, new row inserted at row 4, range exanded to 2:4
            if (InsertRow == oExcelSumFormula.ToAbsoluteRow) 
            {
                ExpandRange = true;
            }

            if(ExpandRange)
            {
                oExcelSumFormula.FromRelativeRow = oExcelSumFormula.FromRelativeRow + 1;
            }

            return (oExcelSumFormula);
        }

        /// <summary>
        /// Parse an XML sum formula and returns a SumFormulaObject
        /// </summary>
        /// <remarks>
        /// Only sums in the format =SUM(R[-1]C:R[-1]C) is currently match to lock down the system
        /// </remarks>
        private ExcelSumFormula ParseSumFormula(string formula, int FormulaRow)
        {
            //this patter will match e.g. =SUM(R[-1]C:R[-1]C) and return 2 groups, one for the first row reference and one for the second, without sign
            string pattern = @"=SUM\(R\[-(\d+)\]C:R\[-(\d+)\]C\)"; 

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection mc = r.Matches(formula);

            //return null if formula doesnt match
            if (mc.Count != 1)
            {
                return (null);
            }

            //parse FromRow and ToRow
            Match m = mc[0]; //We've alread checked that the MatchCollection only contains one match, so this is safe
            string sFromRow = m.Groups[1].Value;
            int iFromRow = int.Parse(sFromRow);
            string sToRow = m.Groups[2].Value;
            int iToRow = int.Parse(sToRow);

            ExcelSumFormula oExcelSumFormula = new ExcelSumFormula();
            oExcelSumFormula.FormulaRow = FormulaRow;
            oExcelSumFormula.FromRelativeRow = iFromRow;
            oExcelSumFormula.ToRelativeRow = iToRow;

            return (oExcelSumFormula);

        }

        private void UpdateNamedRangesXML()
        {
            foreach (NamedRange oNamedRange in dicNamedRanges.Values)
            {

                XmlNode NamedRangeNode = xmlDoc.SelectSingleNode(".//ss:Names/ss:NamedRange[@ss:Name='" + oNamedRange.name + "']", nsmgr); // gets the cell with the named cell name
                string refersto = NamedRangeNode.Attributes["ss:RefersTo"].Value;

                var pattern = @"(.*!)R(.+)C(.+)";
                var replaced = Regex.Replace(refersto, pattern, "$1R" + oNamedRange.row + "C" + oNamedRange.col);

                NamedRangeNode.Attributes["ss:RefersTo"].Value = replaced;
            }
        }
    }

    #region "Support Classess"
    class ExcelSumFormula
    {
        public int FormulaRow { get; set; }
        public int FromRelativeRow { get; set; }
        public int ToRelativeRow { get; set; }

        public int FromAbsoluteRow
        {
            get { return (FormulaRow - FromRelativeRow); }
        }

        public int ToAbsoluteRow
        {
            get { return (FormulaRow - ToRelativeRow); }
        }

        public string GetFormula()
        {
            string formula = "=SUM(R[-" + this.FromRelativeRow + "]C:R[-" + this.ToRelativeRow + "]C)";
            return (formula);
        }
    }

    #endregion
}
