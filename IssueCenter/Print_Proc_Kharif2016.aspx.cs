using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_Print_Proc_Kharif2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string Con_Paddy = ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString(); //PPMS 2016
    string Con_Maze = ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString(); //MPMS 2016

    string sid = "", issueid = "", dist = "", cdate = "", QRGridDetails="";

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

                lblgno.Text = sid = Session["Receipt_ID"].ToString();
                issueid = Session["issue_id"].ToString();
                dist = Session["dist_id"].ToString();

                if (Session["Commodity_ID"].ToString() == "2" || Session["Commodity_ID"].ToString() == "3")
                {
                    GetPaddyData();
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

    public void GetPaddyData()
    {
        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = " SELECT GETDATE() As CurrentDatetime,IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,Districts.District_Eng,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='23" + dist + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    
                    lblchallanno.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblsenddist.Text = ds.Tables[0].Rows[0]["District_Eng"].ToString();
                    lblpccenter.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    cdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);
                    lblchallandt.Text = gdate;
                    lblCropYear.Text =lblcrop.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblcomdty.Text = lblcomdty0.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltransp.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lblvicln.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                    lblbagno.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    lblweight.Text = ds.Tables[0].Rows[0]["Recv_Qty"].ToString();
                    lbldepon.Text = ds.Tables[0].Rows[0]["IssueCenter_ID"].ToString();
                    lblGodownNo.Text = ds.Tables[0].Rows[0]["Recd_Godown"].ToString();

                    lblsend_bagsNum.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lblSend_Qtydisplay.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

                    lblmoisture.Text = getdateM(ds.Tables[0].Rows[0]["Recd_Date"].ToString());
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();

                    GetICName();
                    GetGodownName();

                    QRGridDetails = "Dist=" + lblDistManagerName.Text + ", Receipt ID=" + lblgno.Text + ", CropYear=" + lblCropYear.Text + ", Challan_No=" + lblchallanno.Text + ", Sending Bags=" + lblsend_bagsNum.Text + ", Sending Qty=" + lblSend_Qtydisplay.Text + "(Qtls), Recd. Bags=" + lblbagno.Text + ", Recd. Qty=" + lblweight.Text + "(Qtls)";
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

    public void GetMotaAnaajData()
    {
        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = " SELECT GETDATE() As CurrentDatetime,IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,Districts.District_Eng,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='23" + dist + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    lblchallanno.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblsenddist.Text = ds.Tables[0].Rows[0]["District_Eng"].ToString();
                    lblpccenter.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    cdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);
                    lblchallandt.Text = gdate;
                    lblCropYear.Text = lblcrop.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblcomdty.Text = lblcomdty0.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltransp.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lblvicln.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                    lblbagno.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    lblweight.Text = ds.Tables[0].Rows[0]["Recv_Qty"].ToString();
                    lbldepon.Text = ds.Tables[0].Rows[0]["IssueCenter_ID"].ToString();
                    lblGodownNo.Text = ds.Tables[0].Rows[0]["Recd_Godown"].ToString();

                    lblsend_bagsNum.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lblSend_Qtydisplay.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

                    lblmoisture.Text = getdateM(ds.Tables[0].Rows[0]["Recd_Date"].ToString());
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();

                    GetICName();
                    GetGodownName();

                    QRGridDetails = "Dist=" + lblDistManagerName.Text + ", Receipt ID=" + lblgno.Text + ", CropYear=" + lblCropYear.Text + ", Challan_No=" + lblchallanno.Text + ", Sending Bags=" + lblsend_bagsNum.Text + ", Sending Qty=" + lblSend_Qtydisplay.Text + "(Qtls), Recd. Bags=" + lblbagno.Text + ", Recd. Qty=" + lblweight.Text + "(Qtls)";
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

    private void GetICName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "Select (select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + lbldepon.Text + "') As DepotName, district_name from pds.districtsmp where district_code='" + dist + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbldepon.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblDistManagerName.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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
                string select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblGodownNo.Text + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblGodownNo.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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