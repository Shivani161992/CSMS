using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DataAccess;

namespace Data
{
    public class Rigion
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String ID;
        public String District_Name;
        public String District_ID;
        public String DBStart_Name_En;


        public Rigion(Common cCommon)
        {
            db = new DbClass(cCommon.Connection);
        }
        public void select()
        {

            found = false;
            String qrySelect = "SELECT * FROM pds.division where Division_code=@Division_code";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Division_code", District_ID);
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    found = true;
                    DataRow dr = ds.Tables[0].Rows[0];
                    ID = dr["ID"].ToString();
                    District_Name = dr["Dist_Name"].ToString();
                    District_ID = dr["Division_code"].ToString();
                    DBStart_Name_En = dr["DBStart_Name_En"].ToString();
                }
            }
            ds.Dispose();
        }

        public DataSet select(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM pds.division  where " + where;
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll()
        {

            found = false;
            String qrySelect = "SELECT * FROM pds.division ";
            db.DbParameters.Clear();
            db.Query = qrySelect;
            DataSet ds = db.SelectinDataSet();
            return ds;
        }

        public DataSet selectAll(String where)
        {

            found = false;
            String qrySelect = "SELECT * FROM pds.division " + where;
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

            String qryInsert = "insert into  pds.division (ID,District_Name,District_ID,DBStart_Name_En) values(@ID,@District_Name,@District_ID,@DBStart_Name_En)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@ID", ID);
            db.DbParameters.Add("@District_Name", District_Name);
            db.DbParameters.Add("@District_ID", District_ID);
            db.DbParameters.Add("@DBStart_Name_En", DBStart_Name_En);
            db.Query = qryInsert;
            return db.ExecuteNonQuery();
        }

        public int update()
        {

            String qryUpdate = "update pds.division  set ID=@ID,District_Name=@District_Name,District_ID=@District_ID,DBStart_Name_En=@DBStart_Name_En where District_ID =@District_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@ID", ID);
            db.DbParameters.Add("@District_Name", District_Name);
            db.DbParameters.Add("@District_ID", District_ID);
            db.DbParameters.Add("@DBStart_Name_En", DBStart_Name_En);
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
                if (compareString[j].Equals("ID"))
                    query = query + compareString[j] + " = " + "@ID,";
                if (compareString[j].Equals("District_Name"))
                    query = query + compareString[j] + " = " + "@District_Name,";
                if (compareString[j].Equals("District_ID"))
                    query = query + compareString[j] + " = " + "@District_ID,";
                if (compareString[j].Equals("DBStart_Name_En"))
                    query = query + compareString[j] + " = " + "@DBStart_Name_En,";
            }

            query = query.Substring(0, query.Length - 1);
            String sqlStr = "update pds.division  set " + query + " where District_ID =@District_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@ID", ID);
            db.DbParameters.Add("@District_Name", District_Name);
            db.DbParameters.Add("@District_ID", District_ID);
            db.DbParameters.Add("@DBStart_Name_En", DBStart_Name_En);
            db.Query = sqlStr;
            return db.ExecuteNonQuery();
        }

        public int delete()
        {

            String qryDelete = " delete from pds.division  where District_ID =@District_ID";
            db.DbParameters.Clear();
            db.DbParameters.Add("@District_ID", District_ID);
            db.Query = qryDelete;
            return db.ExecuteNonQuery();
        }

    }

}



