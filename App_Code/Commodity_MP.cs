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

    public class Commodity_MP
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String Commodity_ID;
        public String Commodity_name;
        public String Variety_name;

        public Commodity_MP(Common cCommon)
        {
            db = new DbClass(cCommon.Connection);
        }

        public void select()
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY  where Commodity_ID =@Commodity_ID";
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
            String qrySelect = "SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY  where Status='Y' " + where;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll()
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id in(0,2,3,4,20) and  Status='Y'  order by Commodity_Name  desc";
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY  where Status='Y' " + where;
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

        public int insert()
        {

            String qryInsert = "insert into  dbo.tbl_MetaData_STORAGE_COMMODITY (Commodity_ID,Commodity_name,Variety_name) values(@Commodity_ID,@Commodity_name,@Variety_name)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Commodity_ID", Commodity_ID);
            db.DbParameters.Add("@Commodity_name", Commodity_name);
            db.DbParameters.Add("@Variety_name", Variety_name);
            db.Query = qryInsert;
            return db.ExecuteNonQuery();
        }

        public int update()
        {

            String qryUpdate = "update dbo.tbl_MetaData_STORAGE_COMMODITY  set Commodity_ID=@Commodity_ID,Commodity_name=@Commodity_name,Variety_name=@Variety_name where Commodity_ID =@Commodity_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Commodity_ID", Commodity_ID);
            db.DbParameters.Add("@Commodity_name", Commodity_name);
            db.DbParameters.Add("@Variety_name", Variety_name);
            db.Query = qryUpdate;
            return db.ExecuteNonQuery();
        }

        public int updateOnly(String str)
        {

            String stringTemp;
            String stringCal;
            String stringDuplicate;
            int count = 0;
            int pos;
            int start = 0;
            int i = 0;
            str = str + String.Concat(",");
            stringDuplicate = str;
            int length = str.Length;

            while (length != 0)
            {
                pos = str.IndexOf(",");
                count = count + 1;
                string stringCal1 = str.Substring(pos + 1);
                stringCal = stringCal1;
                length = length - (pos + 1);
                str = stringCal;
            }
            str = stringDuplicate;
            length = str.Length;
            string[] compareString = new string[count];
            while (length != 0)
            {
                pos = str.IndexOf(",");
                stringTemp = str.Substring(start, pos);
                compareString[i] = stringTemp;
                stringCal = str.Substring(pos + 1);
                length = length - (pos + 1);
                str = stringCal;
                i++;
            }

            for (int j = 0; j < compareString.Length; j++)
            {
                if (compareString[j].Equals("Commodity_ID"))
                    query = query + compareString[j] + " = " + "@Commodity_ID,";
                if (compareString[j].Equals("Commodity_name"))
                    query = query + compareString[j] + " = " + "@Commodity_name,";
                if (compareString[j].Equals("Variety_name"))
                    query = query + compareString[j] + " = " + "@Variety_name,";
            }

            query = query.Substring(0, query.Length - 1);
            String sqlStr = "update dbo.tbl_MetaData_STORAGE_COMMODITY  set " + query + " where Commodity_ID =@Commodity_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Commodity_ID", Commodity_ID);
            db.DbParameters.Add("@Commodity_name", Commodity_name);
            db.DbParameters.Add("@Variety_name", Variety_name);
            db.Query = sqlStr;
            return db.ExecuteNonQuery();
        }

        public int delete()
        {

            String qryDelete = " delete from dbo.tbl_MetaData_STORAGE_COMMODITY  where Commodity_ID =@Commodity_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Commodity_ID", Commodity_ID);
            db.Query = qryDelete;
            return db.ExecuteNonQuery();
        }
    
    
    
    }

}


