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
    public class Transporter
    {
        private Common cCommon;
        private DbClass db;
        public String query;
        public Boolean found;

        public String Distt_ID;
        public String Depot_ID;
        public String Transporter_ID;
        public String Transporter_Name;


        public String DepotID;
      
        public Transporter(Common cCommon)
            
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

            String qryInsert = "insert into  dbo.Transporter_Table(Distt_ID,Depot_ID,Transporter_ID,Transporter_Name) values(@Distt_ID,@Depot_ID,@Transporter_ID,@Transporter_Name)";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Distt_ID", Distt_ID);
            db.DbParameters.Add("@Depot_ID", Depot_ID);
            db.DbParameters.Add("@Transporter_ID", Transporter_ID);
            db.DbParameters.Add("@Transporter_Name", Transporter_Name);
           
           
            db.Query = qryInsert;
            return db.ExecuteNonQuery();
        }
        public int update()
        {
            string tnam = Transporter_Name;
            //string tid = Transpoter_Id;
            string sid = DepotID;

            String qry = "update dbo.tbl_metadata_transport set Transpoter_Name=@Transpoter_Name where DepotID=@DepotID And Transporter_Id=@Transporter_Id";
            db.DbParameters.Clear();
            db.DbParameters.Add("@Transpoter_Name", Transporter_Name);
            db.DbParameters.Add("@DepotID", DepotID);
            db.DbParameters.Add("@Transporter_Id", Transporter_ID);
           
            db.Query = qry;
            return db.ExecuteNonQuery();
        }
    }
}