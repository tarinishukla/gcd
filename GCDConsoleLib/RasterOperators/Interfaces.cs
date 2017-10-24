﻿using System;
using System.Collections.Generic;
using System.Reflection;
using GCDConsoleLib.Internal.Operators;
namespace GCDConsoleLib
{
    public class RasterOperators
    {
        public enum MathOpType : byte { Addition, Subtraction, Division, Multipication };

        /// <summary>
        /// EXTENDED COPY
        /// </summary>
        public static Raster ExtendedCopy(ref Raster rInput, string sOutputRaster)
        {
            return (Raster)GenericRunner(typeof(RasterCopy<>), rInput.Datatype.CSType, new object[] {
                rInput, new Raster(ref rInput, sOutputRaster), rInput.Extent
            });
        }

        public static Raster ExtendedCopy(ref Raster rInput, string sOutputRaster, ExtentRectangle newRect)
        {
            return (Raster)GenericRunner(typeof(RasterCopy<>), rInput.Datatype.CSType, new object[] {
                rInput, new Raster(ref rInput, sOutputRaster), newRect
            });
        }

        public static Raster ExtendedCopy(ref Raster rInput, ref Raster rOutputRaster, ExtentRectangle newRect)
        {
            return (Raster)GenericRunner(typeof(RasterCopy<>), rInput.Datatype.CSType, new object[] {
                rInput, rOutputRaster, newRect
            });
        }

        /// <summary>
        /// Raster Math
        /// </summary>
        public static Raster Add<T>(ref Raster rInput, T dOperand, string sOutputRaster)
        {
            return (Raster)GenericRunner(typeof(RasterCopy<>), rInput.Datatype.CSType, new object[] {
                MathOpType.Addition, rInput, dOperand, new Raster(ref rInput, sOutputRaster)
            });
        }
        public static Raster Add(ref Raster rInputA, ref Raster rInputB, string sOutputRaster)
        {
            return (Raster)GenericRunner(typeof(RasterCopy<>), rInputA.Datatype.CSType, new object[] {
                MathOpType.Addition, rInputA, rInputB, new Raster(ref rInputA, sOutputRaster)
            });
        }









        ////////////////////////////////////    EVERYTHING BELOW HERE IS PRIVATE


        /// <summary>
        /// Generic function to get a generic type
        /// </summary>
        /// <param name="generic"></param>
        /// <param name="innerType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static object MakeGenericType(Type generic, Type innerType,
            params object[] args)
        {
            Type specificType = generic.MakeGenericType(new Type[] { innerType });
            return Activator.CreateInstance(specificType, args);
        }

        /// <summary>
        /// This method just calls the previous two in succession. Basically we're instantiating a generic 
        /// operator and then we're returning its "Run" method.
        /// </summary>
        /// <param name="generic"></param>
        /// <param name="innerType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static object GenericRunner(Type generic, Type innerType, params object[] args)
        {
            object myGenericClass = MakeGenericType(generic, innerType, args);
            MethodInfo method = myGenericClass.GetType().GetMethod("Run",
                BindingFlags.Public | BindingFlags.Instance);
            return method.Invoke(myGenericClass, null);
        }


    }

}





