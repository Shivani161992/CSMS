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
    public class Permit
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String Dist_Id;
        public String Permit_No;
        public String Permit_Date;
        public String Validity;
        public String Commodity;
        public String Scheme;
        public String LeadSociety;
        public String Quantity;
        public String Allot_month;
        public String Allot_Year;
        public String Created_date;
        public String Updated_date="";
        public String fps_code;
        public String fps_name;
        public String fps_quantity;


       

        public Permit(Common cCommon)
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
            String qryInsert = "insert into dbo.Mpscsc_Permit(Dist_Id,Permit_No,Permit_Date,Validity,Commodity,Scheme,LeadSociety,Quantity,Allot_month,Allot_Year,Created_date,Updated_date)values(@Dist_Id,@Permit_No,@Permit_Date,@Validity,@Commodity,@Scheme,@LeadSociety,@Quantity,@Allot_month,@Allot_Year,@Created_date,@Updated_date)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Dist_Id", Dist_Id.ToString());
            db.DbParameters.Add("@Permit_No", Permit_No.ToString());
          
            db.DbParameters.Add("@Permit_Date", Permit_Date.ToString());
            db.DbParameters.Add("@Validity", Validity.ToString());
            db.DbParameters.Add("@Commodity", Commodity.ToString());

            db.DbParameters.Add("@Scheme", Scheme.ToString());
            db.DbParameters.Add("@LeadSociety", LeadSociety.ToString());
          
            db.DbParameters.Add("@Quantity", Quantity.ToString());
            db.DbParameters.Add("@Allot_month", Allot_month.ToString());
            db.DbParameters.Add("@Allot_Year", Allot_Year.ToString());
          

            db.DbParameters.Add("@Created_date", Created_date.ToString());
            db.DbParameters.Add("@Updated_date", Updated_date.ToString());
            db.Query = qryInsert;
            return db.ExecuteNonQuery();

        }
        public int insertFPS()
        {
            String qryInsert = "insert into dbo.Mpscsc_Permit_Fps(Dist_Id,Permit_No,LeadSociety,fps_code,fps_name,fps_quantity)values(@Dist_Id,@Permit_No,@LeadSociety,@fps_code,@fps_name,@fps_quantity)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Dist_Id", Dist_Id.ToString());
            db.DbParameters.Add("@Permit_No", Permit_No.ToString());
                               
            db.DbParameters.Add("@LeadSociety", LeadSociety.ToString());

            db.DbParameters.Add("@fps_code", fps_code.ToString());
            db.DbParameters.Add("@fps_name", fps_name.ToString());
            db.DbParameters.Add("@fps_quantity", fps_quantity.ToString());

            
            db.Query = qryInsert;
            return db.ExecuteNonQuery();

        }

    }
}
