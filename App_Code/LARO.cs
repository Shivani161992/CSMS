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
    public class LARO
    {
        private Common cCommon;
        private DbClass db;
        public  String query;
        public  Boolean found;

        public String  Dist_Id;
        public  String RO_No;
        public  String RO_date;
        public  String RO_qty;
        public  String Commodity;
        public  String Balance_Qty;
        public  String Transporter;
        public  String Vehicle_No;
        public  String Challan_No;
        public  String Challan_Date;
        public  String Qty_send;
        public  String Category;
        public  String Crop_year;
        public  String Godown;
        public  String Gunny_type;
        public  String No_of_Bags;
        public  String Send_District;
        public  String Issue_center;
        public  String Updated_date;
        public String  Created_Date;
        public  String Deleted_date;
      


        public LARO(Common cCommon)
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

            String qryInsert = "insert into dbo.Lift_A_RO(Dist_Id,RO_No,RO_date,RO_qty,Commodity,Balance_Qty,Transporter,Vehicle_No,Challan_No,Challan_Date,Qty_send,Category,Crop_year,Godown,Gunny_type,No_of_Bags,Send_District,Issue_center,Created_Date,Updated_date,Deleted_date)values(@Dist_Id,@RO_No,@RO_date,@RO_qty,@Commodity,@Balance_Qty,@Transporter,@Vehicle_No,@Challan_No,@Challan_Date,@Qty_send,@Category,@Crop_year,@Godown,@Gunny_type,@No_of_Bags,@Send_District,@Issue_center,@Created_Date,@Updated_date,@Deleted_date)";
            
            db.DbParameters.Clear();
            db.DbParameters.Add("@Dist_Id",Dist_Id);
            db.DbParameters.Add("@RO_No",RO_No);
            
            db.DbParameters.Add("@RO_date",RO_date);
            db.DbParameters.Add("@RO_qty",RO_qty);
            db.DbParameters.Add("@Commodity",Commodity);
            
            db.DbParameters.Add("@Balance_Qty",Balance_Qty);
            db.DbParameters.Add("@Transporter",Transporter);
            db.DbParameters.Add("@Vehicle_No",Vehicle_No);
            
            db.DbParameters.Add("@Challan_No",Challan_No);
            db.DbParameters.Add("@Challan_Date",Challan_Date);
            db.DbParameters.Add("@Qty_send",Qty_send);
            
            db.DbParameters.Add("@Category",Category);
            db.DbParameters.Add("@Crop_year",Crop_year);
            db.DbParameters.Add("@Godown",Godown);
            
            db.DbParameters.Add("@Gunny_type",Gunny_type);
            db.DbParameters.Add("@No_of_Bags",No_of_Bags);
            db.DbParameters.Add("@Send_District",Send_District);
            db.DbParameters.Add("@Issue_center",Issue_center);

            db.DbParameters.Add("@Created_Date",Created_Date);
            db.DbParameters.Add("@Updated_date", Updated_date);
            db.DbParameters.Add("@Deleted_date", Deleted_date);
            db.Query = qryInsert;
            return db.ExecuteNonQuery();

        }
               
    }




}

