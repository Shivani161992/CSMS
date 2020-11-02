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
    public class RateMaster
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String Scheme_ID;
        public String Scheme_name;

        public String Commodity_ID;
        public String Commodity_name;
        public String Qty;
       
        public String NANUrbanRate;
        public String NANRuralRate;
        public String LeadUrbanRate;
        public String LeadRuralRate;
        public String Unit;
        public String User_ID;
        public String DateTimeStamp;
        public String Operation;

        public RateMaster(Common cCommon)
        {
            db = new DbClass(cCommon.Connection);
        }
        public int insert()
        {
            String qryInsert = "insert into dbo.Schemes_rates(Scheme_ID,Scheme_name,Commodity_ID,Commodity_name,Qty,NANUrbanRate,NANRuralRate,LeadUrbanRate,LeadRuralRate,Unit,User_ID,DateTimeStamp,Operation)values(@Scheme_ID,@Scheme_name,@Commodity_ID,@Commodity_name,@Qty,@NANUrbanRate,@NANRuralRate,@LeadUrbanRate,@LeadRuralRate,@Unit,@User_ID,@DateTimeStamp,@Operation)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Scheme_ID", Scheme_ID.ToString());
            db.DbParameters.Add("@Scheme_name", Scheme_name.ToString());
            db.DbParameters.Add("@Commodity_ID", Commodity_ID.ToString());
            db.DbParameters.Add("@Commodity_name", Commodity_name.ToString());
            db.DbParameters.Add("@Qty", Qty.ToString());
            db.DbParameters.Add("@NANUrbanRate", NANUrbanRate.ToString());
            db.DbParameters.Add("@NANRuralRate", NANRuralRate.ToString());
            db.DbParameters.Add("@LeadUrbanRate", LeadUrbanRate.ToString());
            db.DbParameters.Add("@LeadRuralRate", LeadRuralRate.ToString());
            db.DbParameters.Add("@Unit", Unit.ToString());
            db.DbParameters.Add("@User_ID", User_ID.ToString());
            db.DbParameters.Add("@DateTimeStamp", DateTimeStamp.ToString());
            db.DbParameters.Add("@Operation", Operation.ToString());
            
            db.Query = qryInsert;
            return db.ExecuteNonQuery();

        }
    }
}