using System.Data;

namespace Khangri.UI.Mapper
{
    public static class ConverterHelpers
    {
        #region Converter Helpers
        public static T? GetNullValueIfNoRow<T>(DataTable dt, string columnName) where T : struct
        {
            if (dt.Rows.Count == 0) { return null; }
            if (!dt.Columns.Contains(columnName)) { return null; }
            if (dt.Rows[0][columnName] == DBNull.Value) { return null; }
            return (T)Convert.ChangeType(dt.Rows[0][columnName], typeof(T));
        }

        public static object GetNullValueIfNoRow(DataTable dt, string columnName)
        {
            if (dt.Rows.Count == 0) { return null; }
            if (!dt.Columns.Contains(columnName)) { return null; }
            if (dt.Rows[0][columnName] == DBNull.Value) { return null; }
            return dt.Rows[0][columnName];
        }

        public static T? GetNullValueIfNoRow<T>(DataRow row, string columnName) where T : struct
        {
            if (row == null) { return null; }
            try
            {
                if (row[columnName] == DBNull.Value) { return null; }
                return (T)Convert.ChangeType(row[columnName], typeof(T));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static object GetNullValueIfNoRow(DataRow row, string columnName)
        {
            if (row == null) { return null; }
            try
            {
                if (row[columnName] == DBNull.Value) { return null; }
                return row[columnName];
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
