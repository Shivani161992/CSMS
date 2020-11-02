using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_Print_PartialRej_Pdy2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string Con_Paddy = ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString(); //PPMS 2016
    string Con_Maze = ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString(); //MPMS 2016

    string sid = "", issueid = "", dist = "", QRGridDetails = "";

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
                }
                else
                {
                    Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
                    lblMFD.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
                }

                sid = Session["ReceiptID"].ToString();
                issueid = Session["issue_id"].ToString();
                dist = Session["dist_id"].ToString();

                //CheckDist();
                GetDist();

                if (Session["Commodity_Id"].ToString() == "2" || Session["Commodity_Id"].ToString() == "3")
                {
                    GetData();
                }
                else
                {
                    GetMotaAnaajData();
                }
                
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
    //                lblMFD.Text = "मध्य प्रदेश राज्य सहकारी विपणन संघ लिमिटेड";
    //                Image1.ImageUrl = "~/Images/markfedPDY.jpg";
    //            }
    //            else
    //            {
    //                lblMFD.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
    //                Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
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

    private void GetDist()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select DepotName,(select district_name from pds.districtsmp where district_code='" + dist + "') As district_name from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbldepon.Text = lblissuecenter.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lbldist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();

                    lblgno.Text = sid;
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

    private void GetData()
    {
        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                dist = Session["dist_id"].ToString();
                string select = "", cdate = "";
                hdfreceiptid.Value = "" ;
                con.Open();
                select = "SELECT GETDATE() As CurrentDatetime, IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date, Acceptance_Note_Detail.Reject_Qty , isnull(Acceptance_Note_Detail.Reject_bags,0)Reject_bags ,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  where IssueCenterReceipt_Online.DistrictId='23" + dist + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.IssueID='" + sid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grd_viewDepot.DataSource = ds.Tables[0];
                    grd_viewDepot.DataBind();

                    lblsenddist.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    lblpccenter.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();

                    cdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);

                    string recdate = ds.Tables[0].Rows[0]["Recd_Date"].ToString();
                    string rdate = getdateM(recdate);

                    lblgdtae.Text = rdate;

                    lblCropYear.Text = lblcrop.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblchallan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();   // Challan
                    lblchallanDate.Text = ds.Tables[0].Rows[0]["DateOfIssue1"].ToString();  // Chalan Date
                    lbltruckno.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString(); // Truck Num
                    lbltransname.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString(); // Transp Name
                    lbldepon3.Text = lblmoisture.Text = getdateM(ds.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                    Label2.Text = getdateM(ds.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                    commodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lblwcmno.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();
                    lblrejqty.Text = ds.Tables[0].Rows[0]["Reject_Qty"].ToString();
                    lblrejbags.Text = ds.Tables[0].Rows[0]["Reject_bags"].ToString();

                    hdfreceiptid.Value = ds.Tables[0].Rows[0]["IssueID"].ToString();
                    lblGodown.Text = ds.Tables[0].Rows[0]["Recd_Godown"].ToString();

                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();

                    GetRejectedTruck();
                    GetGodownName();

                    QRGridDetails = "District=" + lbldist.Text + ", Sr.No.=" + lblgno.Text + ", Godown='" + lblGodown.Text + "', CropYear='" + lblCropYear.Text + "', Partial Rejection No.='" + lblwcmno.Text + "', Rejection Date='" + lblmoisture.Text + "', Commodity='" + commodity.Text + "'";
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

    private void GetMotaAnaajData()
    {
        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                dist = Session["dist_id"].ToString();
                string select = "", cdate = "";
                hdfreceiptid.Value = "";
                con.Open();
                select = "SELECT GETDATE() As CurrentDatetime, IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date, Acceptance_Note_Detail.Reject_Qty , isnull(Acceptance_Note_Detail.Reject_bags,0)Reject_bags ,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  where IssueCenterReceipt_Online.DistrictId='23" + dist + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.IssueID='" + sid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grd_viewDepot.DataSource = ds.Tables[0];
                    grd_viewDepot.DataBind();

                    lblsenddist.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    lblpccenter.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();

                    cdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);

                    string recdate = ds.Tables[0].Rows[0]["Recd_Date"].ToString();
                    string rdate = getdateM(recdate);

                    lblgdtae.Text = rdate;

                    lblCropYear.Text = lblcrop.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblchallan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();   // Challan
                    lblchallanDate.Text = ds.Tables[0].Rows[0]["DateOfIssue1"].ToString();  // Chalan Date
                    lbltruckno.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString(); // Truck Num
                    lbltransname.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString(); // Transp Name
                    lbldepon3.Text = lblmoisture.Text = getdateM(ds.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                    Label2.Text = getdateM(ds.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                    commodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lblwcmno.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();
                    lblrejqty.Text = ds.Tables[0].Rows[0]["Reject_Qty"].ToString();
                    lblrejbags.Text = ds.Tables[0].Rows[0]["Reject_bags"].ToString();

                    hdfreceiptid.Value = ds.Tables[0].Rows[0]["IssueID"].ToString();
                    lblGodown.Text = ds.Tables[0].Rows[0]["Recd_Godown"].ToString();

                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();

                    GetRejectedTruck();
                    GetGodownName();

                    QRGridDetails = "District=" + lbldist.Text + ", Sr.No.=" + lblgno.Text + ", Godown='" + lblGodown.Text + "', CropYear='" + lblCropYear.Text + "', Partial Rejection No.='" + lblwcmno.Text + "', Rejection Date='" + lblmoisture.Text + "', Commodity='" + commodity.Text + "'";
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

    private void GetRejectedTruck()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "";
                select = "SELECT * from Rejected_Truck_Details  where Rejected_Truck_Details.Distt_Id='" + dist + "' and Rejected_Truck_Details.Depot_Id='" + issueid + "' and Rejected_Truck_Details.IssueId= '" + hdfreceiptid.Value + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblextra.Text = ds.Tables[0].Rows[0]["Extra_Percent"].ToString();
                    lblaffect.Text = ds.Tables[0].Rows[0]["Damage_Percent"].ToString();
                    lblbright.Text = ds.Tables[0].Rows[0]["Bright_Percent"].ToString();
                    lblmoist.Text = ds.Tables[0].Rows[0]["Moisture_percent"].ToString();
                    lblsplit.Text = ds.Tables[0].Rows[0]["Split_Percent"].ToString();
                    lblPartial.Text = ds.Tables[0].Rows[0]["Partial_Percent"].ToString();
                    lblother.Text = ds.Tables[0].Rows[0]["Others"].ToString();
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

    public void GetGodownName()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = "select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblGodown.Text + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    }
                }
                else
                {

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

    public string getdateM(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
}