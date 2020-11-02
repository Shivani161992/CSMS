using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_PrintRcptEntry_RejCMR : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public string ICID, DistId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                lblAcptNo.Text = Session["RejCMRRecd_ID"].ToString();
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();

                GetDataCMRTesting();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetDataCMRTesting()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select GETDATE() As ServerDate,Rej.CMR_RejectionNo,Rej.CropYear,RecdDist.district_name As RecdDist,(Case When CMR.Miller_State='23' Then (Select district_name From pds.districtsmp Where district_code=CMR.Miller_Dist) Else (Select district_name From OtherState_DistrictCode where district_code=CMR.Miller_Dist) End) As MillerDist,ST.State_Name,MR.Mill_Name,Depot.DepotName,Rej.Godown_id,Rej.Recd_Date,Rej.Recd_Qty,Rej.Bags,Rej.BagType,Rej.Tags,Rej.TagNo,Rej.TruckNo,Rej.TruckNo1,Rej.ToulReceiptNo,Comdty.Commodity_Name,CMR.Inspection_Date From Receipt_RejCMR As Rej Left Join pds.districtsmp As RecdDist ON(Rej.CMR_RecdDist=RecdDist.district_code) Left Join CMR_Inspection_Rejected As CMR ON(CMR.CMR_RejectionNo=Rej.CMR_RejectionNo) Left Join State_Master As ST ON(CMR.Miller_State=ST.State_Code) Left Join Miller_Registration_2017 As MR ON(MR.Registration_ID=CMR.Mill_ID and MR.State_Code=CMR.Miller_State) Left Join tbl_MetaData_DEPOT As Depot ON(Depot.DepotID=Rej.IssueCenter and Depot.DistrictId='23'+Rej.CMR_RecdDist) Left Join tbl_MetaData_STORAGE_COMMODITY As Comdty ON(Comdty.Commodity_Id=Rej.Commodity_Id) Where Rej.CMR_AcptNo='" + lblAcptNo.Text + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblmsg1.Text = "1. उपरोक्तानुसार प्राप्त चावल केन्द्रीय निर्धारित मापदंड के अंतर्गत पायी गयी है| अत: स्वीकृत किया जाता हैं|";
                    lblmsg2.Text = "2. जमा कराये गये चावल की केन्द्रीय/ राज्य शासन/ म.प्र. स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड द्वारा कालांतर में आकस्मिक जाँच करायी जाती है तथा जाँच के दोरान निर्धारित मापदंड का चावल नहीं पाये जाने पर केन्द्रीय/ राज्य शासन/ म.प्र. स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड द्वारा अधिरोपित दंड/ आदेश मान्य होगा|";

                    lblDistName.Text = ds.Tables[0].Rows[0]["RecdDist"].ToString();
                    lblCMRType.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblMillerState.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                    lblMillerDistrict.Text = ds.Tables[0].Rows[0]["MillerDist"].ToString();
                    lblMillerName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblRejectionNo.Text = ds.Tables[0].Rows[0]["CMR_RejectionNo"].ToString();

                    DateTime frmDate = DateTime.Parse(ds.Tables[0].Rows[0]["Recd_Date"].ToString());
                    lblAcptDate.Text = frmDate.ToString("dd/MMM/yyyy");

                    DateTime RejDate = DateTime.Parse(ds.Tables[0].Rows[0]["Inspection_Date"].ToString());
                    lblRejectionDate.Text = RejDate.ToString("dd/MMM/yyyy");

                    lblTagNo.Text = ds.Tables[0].Rows[0]["TagNo"].ToString();
                    lblToulNo.Text = ds.Tables[0].Rows[0]["ToulReceiptNo"].ToString();

                    string BagType = ds.Tables[0].Rows[0]["BagType"].ToString();
                    if (BagType == "9")
                    {
                        lblBagType.Text = "New Jute(SBT)";
                    }
                    else if (BagType == "10")
                    {
                        lblBagType.Text = "Old Jute(SBT)";
                    }
                    else if (BagType == "11")
                    {
                        lblBagType.Text = "Once Used Jute(SBT)";
                    }
                    else if (BagType == "4")
                    {
                        lblBagType.Text = "New PP(HDPE)";
                    }
                    else if (BagType == "2")
                    {
                        lblBagType.Text = "Once Used PP(HDPE)";
                    }
                    else
                    {
                        lblBagType.Text = "Old PP(HDPE)";
                    }

                    string Tag = ds.Tables[0].Rows[0]["Tags"].ToString();
                    if (Tag == "Y")
                    {
                        lblTag.Text = "Yes";
                    }
                    else
                    {
                        lblTag.Text = "No";
                    }

                    string Qty = "", Bags = "", Truck = "", LotNo = "";
                    Qty = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    Bags = ds.Tables[0].Rows[0]["Bags"].ToString();
                    Truck = ds.Tables[0].Rows[0]["TruckNo"].ToString();

                    lblServerDateTime.Text = ds.Tables[0].Rows[0]["ServerDate"].ToString();

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    string QRGridDetails = "Dist=" + lblDistName.Text + ", CropYear=" + lblYear.Text + ", Acpt No=" + lblAcptNo.Text + ", Qty=" + Qty + "(Qtls), Bags=" + Bags + ", Truck_No=" + Truck + "";
                    ImgQRCode.ImageUrl = ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;
                }
                else
                {
                    GridView1.DataSource = "";
                    GridView1.DataBind();
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

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        string GodownName = e.Row.Cells[1].Text;

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    e.Row.Cells[1].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }
}