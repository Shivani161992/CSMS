using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    /// <summary>
    /// Summary description for DataAccess.
    /// </summary>
    ///

    public class _Column
    {

        public string ParamName;
        public object ParamValue;
        public int ParamType;
        public int ParamSize;

        public _Column()
        {

        }
    }

    public class _Parameters : System.Collections.CollectionBase
    {

        public void Add(string sParamName, string sParamValue)
        {
            _Column Col = new _Column();
            Col.ParamName = sParamName;
            Col.ParamValue = sParamValue;
            Col.ParamType = -1;
            Col.ParamSize = -1;
            List.Add(Col);

        }
        // Image file Adding thru byte array
        public void Add(string sParamName, byte[] bImg)
        {
            _Column Col = new _Column();
            Col.ParamName = sParamName;
            Col.ParamValue = bImg;
            Col.ParamType = (int)SqlDbType.Image;
            Col.ParamSize = bImg.Length;
            List.Add(Col);

        }
        public void Add(string sParamName, string sParamValue, int nParamType)
        {
            _Column Col = new _Column();
            Col.ParamName = sParamName;
            Col.ParamValue = sParamValue;
            Col.ParamType = nParamType;
            Col.ParamSize = -1;
            List.Add(Col);
        }
        public void Add(string sParamName, string sParamValue, int nParamType, int nParamSize)
        {
            _Column Col = new _Column();
            Col.ParamName = sParamName;
            Col.ParamValue = sParamValue;
            Col.ParamType = nParamType;
            Col.ParamSize = nParamSize;
            List.Add(Col);
        }

        public void Remove(int index)
        {

            if (index > Count - 1 || index < 0)
            {
                System.Diagnostics.Trace.WriteLine("Index not valid!");
            }
            else
            {
                List.RemoveAt(index);
            }
        }
        public _Column Item(int Index)
        {
            return (_Column)List[Index];
        }
    }


    /// <summary>
    /// Summary description for DbClass.
    /// </summary>
    public class DbClass
    {
        private   SqlConnection oConn = null;
        private _Parameters dbParameters = null;
        private SqlTransaction trans = null;
        private string sQry;

        public SqlConnection Connection
        {
            get { return oConn; }
            set { oConn = value; }
        }
        public SqlTransaction Transaction
        {
            get { return trans; }
            set { trans = value; }
        }
        public _Parameters DbParameters
        {
            get { return dbParameters; }
        }
        public string Query
        {
            get { return sQry; }
            set { sQry = value; }
        }
        ~DbClass()
        {
            this.Dispose();
        }

        public DbClass()
        {
            try
            {
                //				string connStr = System.Configuration.ConfigurationSettings.AppSettings["lrc"];
                //				oConn = new SqlConnection(connStr);
                //				oConn.Open();
                dbParameters = new _Parameters();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
        }
        public DbClass(string sQuery)
        {
            try
            {
                //				string connStr = System.Configuration.ConfigurationSettings.AppSettings["lrc"];
                //				oConn = new SqlConnection(connStr);
                //				oConn.Open();
                dbParameters = new _Parameters();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
            this.sQry = sQuery;
        }
        public DbClass(SqlConnection Connection)
        {
            try
            {
                dbParameters = new _Parameters();
                oConn = Connection;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
        }
        public DbClass(SqlConnection Connection, string sQuery)
        {
            try
            {
                dbParameters = new _Parameters();
                sQry = sQuery;
                oConn = Connection;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
        }
        public void Write_Variables()
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine(this.Query);
            System.Diagnostics.Debug.WriteLine("");
            for (int i = 0; i < this.dbParameters.Count; i++)
            {
                System.Diagnostics.Debug.Write(this.dbParameters.Item(i).ParamName + "=" + this.dbParameters.Item(i).ParamValue + "|");
            }
            System.Diagnostics.Debug.WriteLine("");
        }
        public SqlDataReader SelectinReader()
        {
            try
            {
                _Parameters ColParam = dbParameters;
                SqlCommand dComm = new SqlCommand(this.sQry, oConn);
                if (trans != null)
                    dComm.Transaction = trans;

                for (int i = 0; i < ColParam.Count; i++)
                {
                    if (ColParam.Item(i).ParamType == -1)
                        dComm.Parameters.Add(new SqlParameter(ColParam.Item(i).ParamName, ColParam.Item(i).ParamValue));
                    else if (ColParam.Item(i).ParamSize == -1)
                        dComm.Parameters.Add(new SqlParameter(ColParam.Item(i).ParamName, (SqlDbType)ColParam.Item(i).ParamType));
                    else
                        dComm.Parameters.Add(new SqlParameter(ColParam.Item(i).ParamName, (SqlDbType)ColParam.Item(i).ParamType, ColParam.Item(i).ParamSize));

                    dComm.Parameters[ColParam.Item(i).ParamName].Value = ColParam.Item(i).ParamValue;
                }
                SqlDataReader dr = dComm.ExecuteReader();
                return dr;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
                if (trans != null)
                    this.Rollback();
                return null;
            }
            finally
            {
                oConn.Close();                
            }
        }
        public DataSet SelectinDataSet()
        {
            string sTableName = null; 
            //string sTableName = "BankMaster";
            int nfrom = 0, nChars = 0;

            nfrom = this.sQry.ToLower().IndexOf("from");
            nfrom += 5;
            nChars = sQry.IndexOf(" ", nfrom);
            if (nChars >= 0)
                nChars = nChars - nfrom;
            else
                nChars = sQry.Length - nfrom;

            sTableName = this.sQry.Substring(nfrom, nChars);
            try
            {
                _Parameters ColParam = dbParameters;
                SqlCommand dComm = new SqlCommand(this.sQry, oConn);

                for (int i = 0; i < ColParam.Count; i++)
                {
                    if (ColParam.Item(i).ParamType == -1)
                        dComm.Parameters.Add(new SqlParameter(ColParam.Item(i).ParamName, ColParam.Item(i).ParamValue));
                    else if (ColParam.Item(i).ParamSize == -1)
                        dComm.Parameters.Add(new SqlParameter(ColParam.Item(i).ParamName, (SqlDbType)ColParam.Item(i).ParamType));
                    else
                        dComm.Parameters.Add(new SqlParameter(ColParam.Item(i).ParamName, (SqlDbType)ColParam.Item(i).ParamType, ColParam.Item(i).ParamSize));

                    dComm.Parameters[ColParam.Item(i).ParamName].Value = ColParam.Item(i).ParamValue;
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dComm;
                DataSet ds = new DataSet();
                da.Fill(ds, sTableName);
                return ds;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
                return null;
            }
            finally
            {
                oConn.Close();
                SqlConnection.ClearAllPools();
            }
        }
        public SqlTransaction BeginTransaction(string sTransName)
        {
            try
            {
                trans = oConn.BeginTransaction(IsolationLevel.ReadCommitted, sTransName);
                return trans;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
                return null;
            }
        }
        public void Commit()
        {
            try
            {
                if (trans != null)
                    trans.Commit();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
            if (trans != null)
                trans.Dispose();
            trans = null;
        }
        public void Rollback()
        {
            try
            {
                if (trans != null)
                    trans.Rollback();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
            if (trans != null)
                trans.Dispose();
            trans = null;
        }
        public int ExecuteNonQuery()
        {
            try
            {
                _Parameters ColParam = dbParameters;
                if (oConn.State == ConnectionState.Closed)
                {
                    oConn.Open();
                }
                SqlCommand dComm = new SqlCommand(this.sQry, oConn);
                if (trans != null)
                    dComm.Transaction = trans;

                for (int i = 0; i < ColParam.Count; i++)
                {
                    if (ColParam.Item(i).ParamType == -1)
                        dComm.Parameters.Add(ColParam.Item(i).ParamName, ColParam.Item(i).ParamValue);
                    else if (ColParam.Item(i).ParamSize == -1)
                        dComm.Parameters.Add(ColParam.Item(i).ParamName,(SqlDbType)ColParam.Item(i).ParamType);
                    else
                        dComm.Parameters.Add(ColParam.Item(i).ParamName, (SqlDbType)ColParam.Item(i).ParamType, ColParam.Item(i).ParamSize);

                    if (ColParam.Item(i).ParamValue == "" && ColParam.Item(i).ParamType == (int)SqlDbType.DateTime)
                        dComm.Parameters[ColParam.Item(i).ParamName].Value = System.Data.SqlTypes.SqlDateTime.Null;
                    else
                        dComm.Parameters[ColParam.Item(i).ParamName].Value = ColParam.Item(i).ParamValue;
                }
                int nRet = dComm.ExecuteNonQuery();
                return nRet;
            }
            catch (Exception e)
            {
                this.Write_Variables();
                System.Diagnostics.Trace.Write(e.Message);
                if (trans != null)
                    this.Rollback();
                return -1;
            }
            finally
            {
                oConn.Close();

            }
        }
        public long ExecuteScalar()
        {
            try
            {
                _Parameters ColParam = dbParameters;
                SqlCommand dComm = new SqlCommand(this.sQry, oConn);

                for (int i = 0; i < ColParam.Count; i++)
                {
                    if (ColParam.Item(i).ParamType == -1)
                        dComm.Parameters.Add(ColParam.Item(i).ParamName, ColParam.Item(i).ParamValue);
                    else if (ColParam.Item(i).ParamSize == -1)
                        dComm.Parameters.Add(ColParam.Item(i).ParamName, (SqlDbType)ColParam.Item(i).ParamType);
                    else
                        dComm.Parameters.Add(ColParam.Item(i).ParamName, (SqlDbType)ColParam.Item(i).ParamType, ColParam.Item(i).ParamSize);

                    dComm.Parameters[ColParam.Item(i).ParamName].Value = ColParam.Item(i).ParamValue;
                }
                object obj = dComm.ExecuteScalar();
                long nRet = Convert.ToInt64(obj);
                return nRet;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
                return -1;
            }
            finally
            {
                oConn.Close();

            }
        }
        public void Dispose()
        {
            try
            {
                oConn = null;
                dbParameters = null;
                trans.Dispose();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
        }
    }
}
