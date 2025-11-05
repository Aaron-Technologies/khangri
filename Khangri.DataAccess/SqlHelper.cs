using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess
{
    public class SqlHelper : IDisposable, ISqlHelper
    {
        private SqlCommand oCmd = null;
        private SqlDataAdapter da = null;

        private readonly int vConTimeOut;

        private readonly string conStr = null;

        DataTable dt = null;

        // Get parameter from Sqlcommand by id
        public SqlParameter this[int id]
        {
            get
            {
                return oCmd.Parameters[id];
            }
        }

        // Get parameter from Sqlcommand by name
        public SqlParameter this[string name]
        {
            get
            {
                return oCmd.Parameters[name];
            }
        }

        public SqlHelper(IOptions<Khangri.Entities.ConnectionInfo> connectionInfo)
        {
            vConTimeOut = connectionInfo.Value.ConnectionTimeout;
            conStr = connectionInfo.Value.ConnectionString;
            oCmd = new SqlCommand();
            oCmd.CommandTimeout = vConTimeOut;
        }

        public void SetCommand(string pSqlString, CommandType pCmdType)
        {
            oCmd = new SqlCommand();
            oCmd.CommandText = pSqlString;
            oCmd.CommandType = pCmdType;
            oCmd.CommandTimeout = Convert.ToInt32(vConTimeOut);
        }


        public object GetParameterValue(short pId, ref string pMsg)
        {
            try
            {
                return this[pId].Value;
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                return null;
            }
        }

        public object GetParameterValue(string pName, ref string pMsg)
        {
            try
            {
                return this[pName].Value;
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                return null;
            }
        }


        public DataTable GetDataTable(string pAccYr, short? pCompany_key, ref string pMsg)
        {
            try
            {
                dt = new DataTable();
                da = new SqlDataAdapter();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    oCmd.Connection = con;
                    da.SelectCommand = oCmd;
                    con.Open();
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                return null;
            }
        }

        public DataTable GetDataTable(string pAccYr, short? pCompany_key, SqlParameter[] pParamList, ref string pMsg)
        {
            try
            {
                dt = new DataTable();
                da = new SqlDataAdapter();
                foreach (SqlParameter vParam in pParamList)
                {
                    if (vParam != null)
                        oCmd.Parameters.Add(vParam);
                    else
                    {
                        pMsg = "Parameter list must not contain null.";
                        return null;
                    }
                }
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    oCmd.Connection = con;
                    da.SelectCommand = oCmd;
                    con.Open();
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                return null;
            }
        }

        public DataSet GetDataSet(string pAccYr, short? pCompany_key, string pSqlString, CommandType pCmdType, ref string pMsg)
        {
            DataSet ds = null;
            try
            {
                SetCommand(pSqlString, pCmdType);
                ds = new DataSet();
                da = new SqlDataAdapter();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    oCmd.Connection = con;
                    da.SelectCommand = oCmd;
                    con.Open();
                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose(); ds = null;
                }
            }
        }

        public DataSet GetDataSet(string pAccYr, short? pCompany_key, string pSqlString, CommandType pCmdType, SqlParameter[] pParamList, ref string pMsg)
        {
            DataSet ds = null;
            try
            {
                SetCommand(pSqlString, pCmdType);
                ds = new DataSet();
                da = new SqlDataAdapter();
                foreach (SqlParameter vParam in pParamList)
                {
                    if (vParam != null)
                        oCmd.Parameters.Add(vParam);
                    else
                    {
                        pMsg = "Parameter list must not contain null.";
                        return null;
                    }
                }
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    oCmd.Connection = con;
                    da.SelectCommand = oCmd;
                    con.Open();
                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose(); ds = null;
                }
            }
        }


        public object ExecuteScaler(string pAccYr, short? pCompany_key, SqlParameter[] pParamList, ref string pMsg)
        {
            object oRet = null;
            try
            {
                foreach (SqlParameter vParam in pParamList)
                {
                    if (vParam != null)
                        oCmd.Parameters.Add(vParam);
                    else
                    {
                        pMsg = "Parameter list must not contain null.";
                        return oRet;
                    }
                }
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    oCmd.Connection = con;
                    con.Open();
                    oRet = oCmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return oRet;
        }

        public void ExecuteNonQuery(string pAccYr, short? pCompany_key, SqlParameter[] pParamList, ref string pMsg)
        {
            try
            {
                foreach (SqlParameter vParam in pParamList)
                {
                    if (vParam != null)
                        oCmd.Parameters.Add(vParam);
                    else
                    {
                        pMsg = "Parameter list must not contain null.";
                        return;
                    }
                }
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    oCmd.Connection = con;
                    con.Open();
                    oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
        }

        public void ExecuteNonQuery(string pAccYr, short? pCompany_key, string SQL, SqlParameter[] pParamList, ref string pMsg)
        {
            try
            {
                foreach (SqlParameter vParam in pParamList)
                {
                    if (vParam != null)
                        oCmd.Parameters.Add(vParam);
                    else
                    {
                        pMsg = "Parameter list must not contain null.";
                        return;
                    }
                }
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    oCmd.Connection = con;
                    oCmd.CommandText = SQL;
                    oCmd.CommandType = CommandType.Text;
                    con.Open();
                    oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
        }

        /// <summary>
        /// Resource Management
        /// </summary>
        public void Dispose()
        {
            //Dispose(true);
            if (dt != null)
            {
                dt.Dispose(); dt = null;
            }
            if (da != null)
            {
                da.Dispose(); da = null;
            }
            if (oCmd != null)
            {
                oCmd.Dispose(); oCmd = null;
            }
            //GC.SuppressFinalize(this);
        }

    }
}
