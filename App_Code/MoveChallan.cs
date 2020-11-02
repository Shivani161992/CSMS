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
    public class MoveChallan
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;
        public String Dist_ID;
        public String Depot_ID;
        public String S_of_arrival;
        public String arrival_date;
        public String challan_no;
        public String challan_date;
        public String Qty;
        public String Commodity;
        public String Scheme;
        public String Crop_year;
        public String Category;
        public String Transporter;
        public String Vehile_no;
        public String Arrival_time;
        public String Gunny_type;
        public String No_of_Bags;
        public String Recd_Qty;
        public String Category_recd;
        public String Moisture;
        public String WCM_no;
        public String Created_date;
        public String updated_date;
        public String deleted_date;
        public String GatePass_id;

        public String S_name;
        public String A_dist;
        public String A_Depo;
        public String Variation_qty;
        public String Acceptance_No;
        public String Acceptance_Date;
        public String Dispatch_Date;

       
        


        public MoveChallan(Common cCommon)
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

       
      
    }

   


}

