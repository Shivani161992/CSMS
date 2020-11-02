using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_PrintWHRRequest_Paddy2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string sid = "", issueid = "", dist = "", QRGridDetails = "", whrreq = "", Commodity = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["Markfed"].ToString() == "Y")
                {
                    Image1.ImageUrl = "~/Images/markfedPDY.jpg";
                    lblMFD.Text = "मध्य प्रदेश राज्य सहकारी विपणन संघ लिमिटेड";
                    lblmarkfed.Visible = true;
                    lblscsc.Visible = false;
                }
                else
                {
                    Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
                    lblMFD.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
                    lblscsc.Visible = true;
                    lblmarkfed.Visible = false;
                }

                whrreq = Session["WHR_Request"].ToString();
                Commodity = Session["Commodity"].ToString();

                issueid = Session["issue_id"].ToString();
                dist = Session["dist_id"].ToString();

                //CheckDist();

                GetData();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    //private void CheckDist()
    //{
    //    using (con = new SqlConnection(Con_CSMS))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string select = "";
    //            select = "SELECT  *  FROM pds.districtsmp where paddy_markfed = 'Y' and district_code = '" + dist + "'";
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                lblmarkfed.Visible = true;
    //                lblMFD1.Text = "मध्य प्रदेश राज्य सहकारी विपणन संघ लिमिटेड";
    //                Image1.ImageUrl = "~/Images/markfedPDY.jpg";
    //                lblscsc.Visible = false;
    //            }
    //            else
    //            {
    //                lblscsc.Visible = true;
    //                lblMFD1.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
    //                Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
    //                lblmarkfed.Visible = false;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}

    private void GetData()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "";
                select = "SELECT GETDATE() As CurrentDatetime,MAX(Acceptance_Note_Kharif2016.CropYear) As CropYear,pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName ,  tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,convert(nvarchar,SCSC_Procurement_Kharif2016.Recd_Date,103)Recd_Date ,  tbl_MetaData_GODOWN.Godown_Name , sum(Accept_Qty)Quantity , sum(Bags)Bags , COUNT(truck_no)truck_no  FROM Acceptance_Note_Kharif2016  inner join pds.districtsmp on (pds.districtsmp.district_code = Acceptance_Note_Kharif2016.Distt_ID) inner join tbl_MetaData_DEPOT on (tbl_MetaData_DEPOT.DepotID = Acceptance_Note_Kharif2016.IssueCenter_ID) inner join tbl_MetaData_GODOWN on (tbl_MetaData_GODOWN.Godown_ID = Acceptance_Note_Kharif2016.godown) inner join tbl_MetaData_STORAGE_COMMODITY on (tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = Acceptance_Note_Kharif2016.CommodityId) inner join SCSC_Procurement_Kharif2016 on (SCSC_Procurement_Kharif2016.Receipt_Id = Acceptance_Note_Kharif2016.IssueID and  SCSC_Procurement_Kharif2016.IssueCenter_ID = Acceptance_Note_Kharif2016.IssueCenter_ID and SCSC_Procurement_Kharif2016.TC_Number = Acceptance_Note_Kharif2016.TC_Number and  SCSC_Procurement_Kharif2016.Acceptance_No = Acceptance_Note_Kharif2016.Acceptance_No )  where WHR_Request = '" + whrreq + "' and Acceptance_Note_Kharif2016.Distt_ID = '" + dist + "' and Acceptance_Note_Kharif2016.IssueCenter_ID = '" + issueid + "' group by pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName , tbl_MetaData_GODOWN.Godown_Name ,  tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , SCSC_Procurement_Kharif2016.Recd_Date";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbldistrict.Text = lbldist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblissuecenter.Text = lblissCen.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    lblpavtigdn.Text = lblggdn.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    lblcommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltruckcount.Text = ds.Tables[0].Rows[0]["truck_no"].ToString();
                    lblweight.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    lblbagscount.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lbldepositdate.Text = ds.Tables[0].Rows[0]["Recd_Date"].ToString();
                    lblpavtidate.Text = ds.Tables[0].Rows[0]["Recd_Date"].ToString();
                    lblwhrReq.Text = lbl_whrrequest.Text = whrreq;
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    QRGridDetails = "District=" + lbldist.Text + ", Deposit No.=" + lblwhrReq.Text + ", Godown='" + lblgodown.Text + "', CropYear='" + lblCropYear.Text + "', Total Truck='" + lbltruckcount.Text + "', Total Qty='" + lblweight.Text + "', Total Bags='" + lblbagscount.Text + "', Commodity='" + lblcommodity.Text + "'";
                    ImgQRCode.ImageUrl = ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
}