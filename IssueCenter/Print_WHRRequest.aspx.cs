using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class IssueCenter_Print_WHRRequest : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    string distid = "";
    string issuecenid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["WHR_Request"] != null)
        {
          distid = Session["dist_id"].ToString();

          issuecenid = Session["issue_id"].ToString();

          string whrreq = Session["WHR_Request"].ToString();

          string Commodity = Session["Commodity"].ToString();

          string qry = "SELECT  *  FROM [pds].[districtsmp] where (paddy_markfed = 'Y' or Wheat_Markfed = 'Y') and district_code = '" + distid + "'";
          SqlCommand cmdmrk = new SqlCommand(qry, con);

          if (con.State == ConnectionState.Closed)
          {
              con.Open();
          }

          SqlDataReader dr = cmdmrk.ExecuteReader();

          if (dr.Read())
          {
              lblmarkfed.Visible = true;

              lblscsc.Visible = false;
          }

          else
          {
              lblscsc.Visible = true;

              lblmarkfed.Visible = false;
          }

          lblscsc.Visible = true;

          lblmarkfed.Visible = false;

          dr.Close();

         

          if (Commodity == "22")
          {
              string getdata = "  SELECT pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName , tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,convert(nvarchar,SCSC_Procurement2016.Recd_Date,103)Recd_Date , tbl_MetaData_GODOWN.Godown_Name , sum(Accept_Qty)Quantity , sum(Bags)Bags , COUNT(truck_no)truck_no FROM Acceptance_Note_Detail2016 inner join pds.districtsmp on pds.districtsmp.district_code = Acceptance_Note_Detail2016.Distt_ID inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = Acceptance_Note_Detail2016.IssueCenter_ID inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Acceptance_Note_Detail2016.godown  inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = Acceptance_Note_Detail2016.CommodityId inner join SCSC_Procurement2016 on SCSC_Procurement2016.Receipt_Id = Acceptance_Note_Detail2016.IssueID and  SCSC_Procurement2016.IssueCenter_ID = Acceptance_Note_Detail2016.IssueCenter_ID and SCSC_Procurement2016.TC_Number = Acceptance_Note_Detail2016.TC_Number and  SCSC_Procurement2016.Acceptance_No = Acceptance_Note_Detail2016.Acceptance_No  where WHR_Request = '" + whrreq + "' and Acceptance_Note_Detail2016.Distt_ID = '" + distid + "' and Acceptance_Note_Detail2016.IssueCenter_ID = '" + issuecenid + "' group by pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName , tbl_MetaData_GODOWN.Godown_Name ,  tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , SCSC_Procurement2016.Recd_Date";

              SqlCommand cmd = new SqlCommand(getdata, con);

              if (con.State == ConnectionState.Closed)
              {
                  con.Open();
              }

              SqlDataAdapter da = new SqlDataAdapter(cmd);

              DataSet ds = new DataSet();

              da.Fill(ds);

              if (ds.Tables[0].Rows.Count > 0)
              {
                  lbldistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();

                  lblissuecenter.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

                  lblgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                  lblpavtigdn.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                  lblcommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();

                  lbltruckcount.Text = ds.Tables[0].Rows[0]["truck_no"].ToString();

                  lblweight.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();

                  lblbagscount.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                  lbldepositdate.Text = ds.Tables[0].Rows[0]["Recd_Date"].ToString();

                  lblpavtidate.Text = ds.Tables[0].Rows[0]["Recd_Date"].ToString();

                  lblwhrReq.Text = Session["WHR_Request"].ToString();

                  lbl_whrrequest.Text = Session["WHR_Request"].ToString();

                  if (con.State == ConnectionState.Open)
                  {
                      con.Close();

                  }

                  lblscsc.Visible = true;

                  lblmarkfed.Visible = false;

              }
          }

          else
          {
              string getdata = "SELECT pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName , tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,convert(nvarchar,SCSC_Procurement.Recd_Date,103)Recd_Date , tbl_MetaData_GODOWN.Godown_Name , sum(Accept_Qty)Quantity , sum(Bags)Bags , COUNT(truck_no)truck_no FROM Acceptance_Note_Detail inner join pds.districtsmp on pds.districtsmp.district_code = Acceptance_Note_Detail.Distt_ID inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = Acceptance_Note_Detail.IssueCenter_ID inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Acceptance_Note_Detail.godown  inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = Acceptance_Note_Detail.CommodityId inner join SCSC_Procurement on SCSC_Procurement.Receipt_Id = Acceptance_Note_Detail.IssueID and  SCSC_Procurement.IssueCenter_ID = Acceptance_Note_Detail.IssueCenter_ID and SCSC_Procurement.TC_Number = Acceptance_Note_Detail.TC_Number and  SCSC_Procurement.Acceptance_No = Acceptance_Note_Detail.Acceptance_No  where WHR_Request = '" + whrreq + "' and Acceptance_Note_Detail.Distt_ID = '" + distid + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + issuecenid + "' group by pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName , tbl_MetaData_GODOWN.Godown_Name ,  tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , SCSC_Procurement.Recd_Date";

              SqlCommand cmd = new SqlCommand(getdata, con);

              if (con.State == ConnectionState.Closed)
              {
                  con.Open();
              }

              SqlDataAdapter da = new SqlDataAdapter(cmd);

              DataSet ds = new DataSet();

              da.Fill(ds);

              if (ds.Tables[0].Rows.Count > 0)
              {
                  lbldistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();

                  lblissuecenter.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

                  lblgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                  lblpavtigdn.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                  lblcommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();

                  lbltruckcount.Text = ds.Tables[0].Rows[0]["truck_no"].ToString();

                  lblweight.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();

                  lblbagscount.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                  lbldepositdate.Text = ds.Tables[0].Rows[0]["Recd_Date"].ToString();

                  lblpavtidate.Text = ds.Tables[0].Rows[0]["Recd_Date"].ToString();

                  lblwhrReq.Text = Session["WHR_Request"].ToString();

                  lbl_whrrequest.Text = Session["WHR_Request"].ToString();

                  if (con.State == ConnectionState.Open)
                  {
                      con.Close();

                  }

              }
          }
        }
    }
}
