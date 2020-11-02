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


namespace Data 

{
    public class Gunny_Insert
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String Issue_Center_Id;
        public String District_Id;
        public String Gunny_Master_ID;
        public String Gunny_Type;
        public String Gunny_Category;
        public String Bags;
        public String Created_Date;
       
        public Gunny_Insert(Common cCommon)
        {
            db = new DbClass(cCommon.Connection);
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
            String qryInsert = "insert into dbo.Gunny_Bags_Reciept(Issue_Center_Id,District_Id,Gunny_Master_ID,Gunny_Type,Gunny_Category,Bags,Created_Date)values(@Issue_Center_Id,@District_Id,@Gunny_Master_ID,@Gunny_Type,@Gunny_Category,@Bags,@Created_Date)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Issue_Center_Id", Issue_Center_Id.ToString());
            db.DbParameters.Add("@District_Id", District_Id.ToString());
            db.DbParameters.Add("@Gunny_Master_ID", Gunny_Master_ID.ToString());

            db.DbParameters.Add("@Gunny_Type", Gunny_Type.ToString());
            db.DbParameters.Add("@Gunny_Category", Gunny_Category.ToString());
            db.DbParameters.Add("@Bags", Bags.ToString());
            db.DbParameters.Add("@Created_Date", Created_Date.ToString());
            
            db.Query = qryInsert;
            return db.ExecuteNonQuery();

        }
    }


}

