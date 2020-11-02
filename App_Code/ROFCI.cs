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
    public class ROFCI
    {
        private Common cCommon;
        private DbClass db;
        public  String query;
        public  Boolean found;
        public String Distt_Id;
        public  String RO_No;
        public  String RO_date;
        public String RO_qty;
        public  String RO_district;
        public  String Commodity;
        public  String Scheme;
        public String Rate;
        public String Amount;
        public  String Allot_month;
        public String Allot_year;
        public String DD_chk_no;
        public  String DD_chk_date;
        public String DD_chk_Amount;
        public  String Remarks;
        public  String Created_date;
        public  String updated_date;
        public  String deleted_date;
        public String Balance_Qty;
        public String IsLifted;
        public String RO_Validity;
        public ROFCI(Common cCommon)
        {
            db = new DbClass(cCommon.Connection);
        }

        //public int insert()
        //{
        //    String qryInsert = "insert into dbo.RO_of_FCI(Distt_Id,RO_No,RO_date,RO_qty,RO_Validity,RO_district,Commodity,Scheme,Rate,Amount,Allot_month,Allot_year,DD_chk_no,DD_chk_date,DD_chk_Amount,Remarks,IsLifted,Created_date,updated_date,deleted_date,Balance_Qty)values(@Distt_Id,@RO_No,@RO_date,@RO_qty,@RO_Validity,@RO_district,@Commodity,@Scheme,@Rate,@Amount,@Allot_month,@Allot_year,@DD_chk_no,@DD_chk_date,@DD_chk_Amount,@Remarks,@IsLifted,@Created_date,@updated_date,@deleted_date,@Balance_Qty)";
        //    db.DbParameters.Clear();
        //    db.DbParameters.Add("@Distt_Id", Distt_Id );
        //    db.DbParameters.Add("@RO_No",RO_No.ToString ());
        //    db.DbParameters.Add("@RO_date", RO_date );
        //    db.DbParameters.Add("@RO_qty", float.Parse ( RO_qty) );
        //    db.DbParameters.Add("@RO_Validity",RO_Validity);
           

        //    db.DbParameters.Add("@RO_district", RO_district );
        //    db.DbParameters.Add("@Commodity", Commodity );
        //    db.DbParameters.Add("@Scheme", Scheme );
        //    db.DbParameters.Add("@Rate", float.Parse (Rate) );
        //    db.DbParameters.Add("@Amount",float.Parse ( Amount) );
        //    db.DbParameters.Add("@Allot_month", Allot_month );
        //    db.DbParameters.Add("@Allot_year", Allot_year );
        //    db.DbParameters.Add("@DD_chk_no",int.Parse (DD_chk_no) );
        //    db.DbParameters.Add("@DD_chk_date", DD_chk_date );
        //    db.DbParameters.Add("@DD_chk_Amount",float.Parse (DD_chk_Amount));
        //    db.DbParameters.Add("@Remarks", Remarks );
        //    db.DbParameters.Add("@IsLifted",IsLifted);
        //    db.DbParameters.Add("@Created_date", Created_date );
        //    db.DbParameters.Add("@updated_date", updated_date );
        //    db.DbParameters.Add("@deleted_date", deleted_date );
        //    db.DbParameters.Add("@Balance_Qty", float.Parse (Balance_Qty));
        //    db.Query = qryInsert;
        //    return db.ExecuteNonQuery();

        //}

        public int update()
        {

            //string dis = Depot_ID;
            //string sid = Dist_ID;
            //string c = challan_no;

            String qryUpdate = "update dbo.RO_of_FCI set RO_date=@RO_date,RO_qty=@RO_qty,RO_Validity=@RO_Validity,RO_district=@RO_district,Commodity=@Commodity,Scheme=@Scheme,Rate=@Rate,Amount=@Amount,Allot_month=@Allot_month,Allot_year=@Allot_year,DD_chk_no=@DD_chk_no,DD_chk_date=@DD_chk_date,DD_chk_Amount=@DD_chk_Amount,Remarks=@Remarks,updated_date=@updated_date,Balance_Qty=@Balance_Qty where Distt_Id=@Distt_Id And RO_No=@RO_No";
            db.DbParameters.Clear();

            db.DbParameters.Add("@Distt_Id", Distt_Id);
            db.DbParameters.Add("@RO_No", RO_No);
            db.DbParameters.Add("@RO_date", RO_date);
            db.DbParameters.Add("@RO_qty", RO_qty);
            db.DbParameters.Add("@RO_Validity", RO_Validity);

            db.DbParameters.Add("@RO_district", RO_district);
            db.DbParameters.Add("@Commodity", Commodity);
            db.DbParameters.Add("@Scheme", Scheme);
            db.DbParameters.Add("@Rate", Rate);
            db.DbParameters.Add("@Amount", Amount);
            db.DbParameters.Add("@Allot_month", Allot_month);
            db.DbParameters.Add("@Allot_year", Allot_year);

            db.DbParameters.Add("@DD_chk_no", DD_chk_no);
            db.DbParameters.Add("@DD_chk_date", DD_chk_date);
            db.DbParameters.Add("@DD_chk_Amount", DD_chk_Amount);
            db.DbParameters.Add("@Remarks", Remarks);
            db.DbParameters.Add("@Balance_Qty", Balance_Qty);
            db.DbParameters.Add("@updated_date", updated_date);

            db.Query = qryUpdate;
            return db.ExecuteNonQuery();
        }


    }




}

