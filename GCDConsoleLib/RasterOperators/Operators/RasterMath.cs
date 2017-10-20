﻿using System;
using GCDConsoleLib;
using System.Collections.Generic;
using GCDConsoleLib.Internal;

namespace GCDConsoleLib.Internal.Operators
{
    public class RasterMath<T> : CellByCellOperator<T>
    {
        public enum MathOpType : byte { Addition, Subtraction, Division, Multipication };
        private bool _scalar;
        private MathOpType _type;
        private T _operand;

        /// <summary>
        /// We protect the constructors because we don't really want anyone using them.
        /// </summary>
        /// <param name="otType"></param>
        /// <param name="rInput"></param>
        /// <param name="dOperand"></param>
        /// <param name="sOutputRaster"></param>
        protected RasterMath(MathOpType otType, ref Raster rInput, T dOperand, ref Raster rOutputRaster) :
            base(new List<Raster> { rInput }, ref rOutputRaster)
        {
            _type = otType;
            _scalar = true;
            _operand = dOperand;
        }

        protected RasterMath(MathOpType otType, ref Raster rInputA, ref Raster rInputB, ref Raster rOutputRaster) :
            base(new List<Raster> { rInputA, rInputB }, ref rOutputRaster)
        {
            _type = otType;
            _scalar = false;
        }

        public static dynamic Add(dynamic a, dynamic b) { return a + b; }
        public static dynamic Subtract(dynamic a, dynamic b) { return a - b; }
        public static dynamic Multiply(dynamic a, dynamic b) { return a * b; }
        public static dynamic Divide(dynamic a, dynamic b) { return a / b; }

        protected override T CellOp(ref List<T[]> data, int id)
        {
            T val = OpNodataVal;
            if (_scalar)
            {
                if (!data[0][id].Equals(OpNodataVal))
                {
                    switch (_type)
                    {
                        case MathOpType.Addition:
                            val = Add(data[0][id],_operand);
                            break;
                        case MathOpType.Subtraction:
                            val = Subtract(data[0][id], _operand);
                            break;
                        case MathOpType.Multipication:
                            val = Multiply(data[0][id], _operand);
                            break;
                        case MathOpType.Division:
                            val = Divide(data[0][id], _operand);
                            break;
                    }
                }
            }
            else
            {
                if (!data[0][id].Equals(OpNodataVal) && !data[1][id].Equals(OpNodataVal))
                {
                    switch (_type)
                    {
                        case MathOpType.Addition:
                            val = Add(data[0][id], data[1][id]);
                            break;
                        case MathOpType.Subtraction:
                            val = Subtract(data[0][id], data[1][id]);
                            break;
                        case MathOpType.Multipication:
                            val = Multiply(data[0][id], data[1][id]);
                            break;
                        case MathOpType.Division:
                            val = Divide(data[0][id], data[1][id]);
                            break;
                    }
                }
            }
            return val;
        }
    }
}
