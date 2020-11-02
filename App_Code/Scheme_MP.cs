using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DataAccess;
using System.Data.SqlClient;
namespace Data 

{

    public class Scheme_MP
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String Commodity_ID;
        public String Commodity_name;
        public String Variety_name;

        public Scheme_MP(Common cCommon)
        {
             db = new DbClass(cCommon.Connection);
        }

        public void select()
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.m_scheme_pds where Commodity_ID =@Commodity_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Commodity_ID", Commodity_ID);
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    found = true;
                    DataRow dr = ds.Tables[0].Rows[0];
                    Commodity_ID = dr["Commodity_ID"].ToString();
                    Commodity_name = dr["Commodity_name"].ToString();
                    Variety_name = dr["Variety_name"].ToString();
                }
            }
            ds.Dispose();
        }

        public DataSet select(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.tbl_MetaData_SCHEME  Where Status='Y' " + where;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        //public DataSet selectAll()
        //{

        //    found = false;
        //    String qrySelect = "select * from dbo.tbl_MetaData_SCHEME where Scheme_Name in ('APL','BPL','AAY')";
        //    db.DbParameters.Clear();
        //    db.Query = qrySelect;
        //    DataSet ds = db.SelectinDataSet();
        //    return ds;
        //}

        public DataSet selectAll(String where)
        {

            found = false;
            String qrySelect = "select * from dbo.tbl_MetaData_SCHEME Where Status='Y' " + where;
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

       

       

       
    


    }



}
