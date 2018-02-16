﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GCDCore.UserInterface.UtilityForms
{
    public class ChartContextMenu
    {
        public readonly ContextMenuStrip CMS;

        public ChartContextMenu()
        {
            CMS = new ContextMenuStrip();

            CMS.Items.Add(new ToolStripMenuItem("Copy Chart Image To The ClipBoard", Properties.Resources.Copy, CopyChartToClipBoard_Click));
            CMS.Items.Add(new ToolStripMenuItem("Save Chart Image To File", Properties.Resources.Save, SaveChartToFile_Click));
        }

        private void CopyChartToClipBoard_Click(object sender, EventArgs e)
        {
            try
            {
                Chart cht = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl as Chart;
                using (MemoryStream ms = new MemoryStream())
                {
                    cht.SaveImage(ms, ChartImageFormat.Bmp);
                    Bitmap bm = new Bitmap(ms);
                    Clipboard.SetImage(bm);
                }
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex, "An error occurred copying the chart image to the clipboard");
            }
        }

        private void SaveChartToFile_Click(object sender, EventArgs e)
        {
            try
            {
                Chart cht = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl as Chart;

                List<string> lFormats = new List<string>();
                lFormats.Add("Bitmap (*.bmp)|*.bmp");
                lFormats.Add("GIF (*.gif)|*.gif");
                lFormats.Add("JPEG (*.jpg)|*.jpg");
                lFormats.Add("PNG (*.png)|*.png");
                lFormats.Add("TIFF (*.tiff)|*.tif");
                lFormats.Add("WMF (*.wmf)|*.wmf");
                SaveFileDialog dlgSave = new SaveFileDialog();
                dlgSave.Title = "Save Chart Image";
                dlgSave.AddExtension = true;
                foreach (string sFormat in lFormats)
                {
                    if (string.IsNullOrEmpty(dlgSave.Filter))
                    {
                        dlgSave.Filter = sFormat;
                    }
                    else
                    {
                        dlgSave.Filter = dlgSave.Filter + "|" + sFormat;
                    }
                }

                dlgSave.FilterIndex = 3;
                if (dlgSave.ShowDialog() == DialogResult.OK)
                {
                    string sFilePath = dlgSave.FileName;
                    System.Drawing.Imaging.ImageFormat imgFormat;
                    switch (dlgSave.FilterIndex)
                    {
                        case 1: imgFormat = System.Drawing.Imaging.ImageFormat.Bmp; break;
                        case 2: imgFormat = System.Drawing.Imaging.ImageFormat.Gif; break;
                        case 3: imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg; break;
                        case 4: imgFormat = System.Drawing.Imaging.ImageFormat.Png; break;
                        case 5: imgFormat = System.Drawing.Imaging.ImageFormat.Tiff; break;
                        case 6: imgFormat = System.Drawing.Imaging.ImageFormat.Wmf; break;
                        default:
                            Exception ex = new Exception("Unhandled image format.");
                            ex.Data["Filter Index"] = dlgSave.FilterIndex;
                            ex.Data["Filter"] = dlgSave.Filter[dlgSave.FilterIndex];
                            ex.Data["File Path"] = sFilePath;
                            throw ex;
                    }

                    cht.SaveImage(sFilePath, imgFormat);

                    if (File.Exists(sFilePath))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(sFilePath);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("The image file was created at {0}, but an error occurred attempting to open the image file.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }        
                    }
                }
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex, "An error occurred saving the chart image to file");
            }
        }
    }
}