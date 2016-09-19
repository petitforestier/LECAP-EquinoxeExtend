using DriveWorks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveWorks.Helper.Object
{
    public class TableValue : ITableValue
    {
        #region Public PROPERTIES

        public object[,] Data { get; set; }

        int ITableValue.Columns
        {
            get
            {
                return Data.GetLength(1);
            }
        }

        int ITableValue.Rows
        {
            get
            {
                return Data.GetLength(0);
            }
        }

        #endregion

        #region Public METHODS

        object ITableValue.GetElement(int rowIndex, int columnIndex)
        {
            return Data[rowIndex, columnIndex];
        }

        bool? ITableValue.GetElementAsBoolean(CultureInfo ci, int rowIndex, int columnIndex)
        {
            bool value;
            Titan.Rules.Execution.ValueConvertHelper.TryConvert(ci, Data[rowIndex, columnIndex], out value);
            return value;
        }

        DateTime? ITableValue.GetElementAsDateTime(CultureInfo ci, int rowIndex, int columnIndex)
        {
            DateTime value;
            Titan.Rules.Execution.ValueConvertHelper.TryConvert(ci, Data[rowIndex, columnIndex], out value);
            return value;
        }

        double? ITableValue.GetElementAsDouble(CultureInfo ci, int rowIndex, int columnIndex)
        {
            double value;
            Titan.Rules.Execution.ValueConvertHelper.TryConvert(ci, Data[rowIndex, columnIndex], out value);
            return value;
        }

        string ITableValue.GetElementAsString(CultureInfo ci, int rowIndex, int columnIndex)
        {
            string value = null;
            Titan.Rules.Execution.ValueConvertHelper.TryConvert(ci, Data[rowIndex, columnIndex], out value);
            return value;
        }

        object[,] ITableValue.ToArray()
        {
            return Data;
        }

        #endregion
    }
}