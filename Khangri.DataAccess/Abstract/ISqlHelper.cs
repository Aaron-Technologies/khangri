using System.Data;
using Microsoft.Data.SqlClient;

namespace Khangri.DataAccess.Abstract
{
    public interface ISqlHelper
    {
        SqlParameter this[int id] { get; }
        SqlParameter this[string name] { get; }

        void Dispose();
        void ExecuteNonQuery(string pAccYr, short? pCompany_key, SqlParameter[] pParamList, ref string pMsg);
        void ExecuteNonQuery(string pAccYr, short? pCompany_key, string SQL, SqlParameter[] pParamList, ref string pMsg);
        object ExecuteScaler(string pAccYr, short? pCompany_key, SqlParameter[] pParamList, ref string pMsg);
        DataSet GetDataSet(string pAccYr, short? pCompany_key, string pSqlString, CommandType pCmdType, ref string pMsg);
        DataSet GetDataSet(string pAccYr, short? pCompany_key, string pSqlString, CommandType pCmdType, SqlParameter[] pParamList, ref string pMsg);
        DataTable GetDataTable(string pAccYr, short? pCompany_key, ref string pMsg);
        DataTable GetDataTable(string pAccYr, short? pCompany_key, SqlParameter[] pParamList, ref string pMsg);
        object GetParameterValue(short pId, ref string pMsg);
        object GetParameterValue(string pName, ref string pMsg);
        void SetCommand(string pSqlString, CommandType pCmdType);
    }
}