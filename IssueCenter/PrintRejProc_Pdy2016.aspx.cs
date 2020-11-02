using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_PrintRejProc_Pdy2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
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

                sid = Session["Receipt_ID"].ToString();
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
                    lbldepon.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
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
                hdfreceiptid.Value = "";
                con.Open();

                select = "SELECT GETDATE() As CurrentDatetime, IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='23" + dist + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string recdate = ds.Tables[0].Rows[0]["Recd_Date"].ToString();
                    string rdate = getdateM(recdate);
                    lblgdtae.Text = rdate;

                    lblchallan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblsenddist.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    lblpccenter.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    cdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);
                    lblchallanDate.Text = gdate;
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    commodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltransname.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lbltruckno.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                    lblsend_bagsNum.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lblSend_Qtydisplay.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();
                    Label2.Text = getdateM(ds.Tables[0].Rows[0]["Recd_Date"].ToString());

                    hdfreceiptid.Value = ds.Tables[0].Rows[0]["IssueID"].ToString();
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();

                    GetRejectedTruck();

                    QRGridDetails = "District=" + lbldist.Text + ", Issue Centre='" + lbldepon.Text + "', Sr.No.=" + lblgno.Text + ", Challan No.='" + lblchallan.Text + "', CropYear='" + lblCropYear.Text + "', Commodity='" + commodity.Text + "',Bags='" + lblsend_bagsNum.Text + "', Qty(Qtls)='" + lblSend_Qtydisplay.Text + "' ";
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

                select = "SELECT GETDATE() As CurrentDatetime, IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='23" + dist + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string recdate = ds.Tables[0].Rows[0]["Recd_Date"].ToString();
                    string rdate = getdateM(recdate);
                    lblgdtae.Text = rdate;

                    lblchallan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblsenddist.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    lblpccenter.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    cdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);
                    lblchallanDate.Text = gdate;
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    commodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltransname.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lbltruckno.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                    lblsend_bagsNum.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lblSend_Qtydisplay.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();
                    Label2.Text = getdateM(ds.Tables[0].Rows[0]["Recd_Date"].ToString());

                    hdfreceiptid.Value = ds.Tables[0].Rows[0]["IssueID"].ToString();
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();

                    GetRejectedTruck();

                    QRGridDetails = "District=" + lbldist.Text + ", Issue Centre='" + lbldepon.Text + "', Sr.No.=" + lblgno.Text + ", Challan No.='" + lblchallan.Text + "', CropYear='" + lblCropYear.Text + "', Commodity='" + commodity.Text + "',Bags='" + lblsend_bagsNum.Text + "', Qty(Qtls)='" + lblSend_Qtydisplay.Text + "' ";
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
                select = "SELECT * from Rejected_Truck_Details  where Rejected_Truck_Details.Distt_Id='" + dist + "' and Rejected_Truck_Details.Depot_Id='" + issueid + "' and Rejected_Truck_Details.IssueId= '" + hdfreceiptid.Value + "' order by Created_Date desc";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblFAQ.Text = ds.Tables[0].Rows[0]["Faq_Percent"].ToString();
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

    public string getdateM(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
}