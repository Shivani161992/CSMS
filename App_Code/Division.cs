using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
namespace Data 
{

    public class Division
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String Division_Code;
        public String DC_ID;
        public String DC_Name;
        public String Division_Name;
        public String Password;
        public String Tname;

        public Division(Common cCommon)
        {
            db = new DbClass(cCommon.Connection); 
        }


        public void select()
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.RegionLogin where Division_Code=@DC_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@DC_ID", DC_ID);
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    found = true;
                    DataRow dr = ds.Tables[0].Rows[0];
                    Division_Code = dr["Division_Code"].ToString();
                    //DC_ID = dr["DC_ID"].ToString();
                    Division_Name = dr["Division"].ToString();
                    Password = dr["Password"].ToString();
                }
            }
            ds.Dispose();
        }

        public DataSet select(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.RegionLogin where " + where;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll()
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.RegionLogin";
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM dbo.RegionLogin " + where;
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

            String qryInsert = "insert into  dbo.RegionLogin (Division_Code,Division,Password) values(@Division_Code,@Division,@Password)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Division_Code", Division_Code);
            //db.DbParameters.Add("@DC_ID", DC_ID);
            db.DbParameters.Add("@Division", Division_Name);
            db.DbParameters.Add("@Password", Password);
            db.Query = qryInsert;
            return db.ExecuteNonQuery();
        }

        public int update()
        {

            String qryUpdate = "update dbo.RegionLogin set District_ID=@District_ID,DC_ID=@DC_ID,DC_name=@DC_name,Password=@Password where DC_ID =@DC_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Division_Code", Division_Code);
            db.DbParameters.Add("@Division", Division_Name);
            db.DbParameters.Add("@Password", Password);
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
                if (compareString[j].Equals("District_ID"))
                    query = query + compareString[j] + " = " + "@District_ID,";
                if (compareString[j].Equals("DC_ID"))
                    query = query + compareString[j] + " = " + "@DC_ID,";
                if (compareString[j].Equals("DC_name"))
                    query = query + compareString[j] + " = " + "@DC_name,";
                if (compareString[j].Equals("Password"))
                    query = query + compareString[j] + " = " + "@Password,";
            }

            query = query.Substring(0, query.Length - 1);
            String sqlStr = "update dbo.RegionLogin set " + query + " where DC_ID =@DC_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Division_Code", Division_Code);
            db.DbParameters.Add("@Division", Division_Name);
            db.DbParameters.Add("@Password", Password);
            db.Query = sqlStr;
            return db.ExecuteNonQuery();
        }

        public int delete()
        {

            String qryDelete = " delete from dbo.RegionLogin where DC_ID =@DC_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@DC_ID", DC_ID);
            db.Query = qryDelete;
            return db.ExecuteNonQuery();
        }
    }

}



