using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

namespace whtmpData
{
    public class loginWhtMP
    {
        
        private DbClass db;
        public String query;
        public Boolean found;
        public String UID;
        public String District_ID;
        public String Password;

        public loginWhtMP(Common cCommon)
        {
            
            db = new DbClass(cCommon.Connection);
        }

        public void select()
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.login where District_ID =@District_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@District_ID", District_ID);
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    found = true;
                    DataRow dr = ds.Tables[0].Rows[0];
                    UID = dr["UID"].ToString();
                    District_ID = dr["District_ID"].ToString();
                    Password = dr["Password"].ToString();                    
                }
            }
            ds.Dispose();
        }

        public DataSet select(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM login where " + where;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll()
        {

            found = false;
            String qrySelect = "SELECT * FROM login";
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM login " + where;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAny(String selectstring)
        {

            found = false;
            String qrySelect = selectstring;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }
        public int UpdatePassword(String qryUpdate)
        {
            db.Query = qryUpdate;
            return db.ExecuteNonQuery();
        }
        public int InsertPasswordLog(String qryInsert)
        {
            db.Query = qryInsert;
            return db.ExecuteNonQuery();
        }


  public int InsertInfo(String qry)
        {
            found = false;
            String qrySelect = qry;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            int ret = db.ExecuteNonQuery();
            return ret;
        }

        public string RetLoginCountInfo(String qry)
        {
            found = false;
            String qrySelect = qry;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            string res = Convert.ToString(db.ExecuteScalar());
            return res;

        }

    }
}
