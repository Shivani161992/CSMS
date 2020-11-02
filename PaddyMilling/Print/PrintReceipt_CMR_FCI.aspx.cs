using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_PrintReceipt_CMR_FCI : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS

    string districtid = "", ReceiptID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["Markfed"].ToString() == "Y")
                {
                    Image1.ImageUrl = "~/Images/markfedPDY.jpg";
                    lblMFD.Text = "मध्य प्रदेश राज्य सहकारी विपणन संघ लिमिटेड";
                    lblDM.Text = "जिला विपणन अधिकारी";
                }
                else
                {
                    Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
                    lblMFD.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
                    lblDM.Text = "जिला प्रबंधक";
                }

                districtid = Session["dist_id"].ToString();
                ReceiptID = Session["CMRDO_ID"].ToString();

                //ReceiptID = "CMRAF168517716690";
                GetCMRFCIData();
            }
            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
    }

    public void GetCMRFCIData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select GETDATE() As ServerDate, MP.district_name As MPDist,SM.State_Name,OD.district_name As OtherDist,CMR.Receipt_ID As RecptNo,CMR.Receipt_Date As AcptDate,CMR.CommonRice,CMR.Bags,CMR.Truck_No,CMR.CropYear,MR.Mill_Name,(Select State_Name From State_Master where State_Code=MR.State_Code) As MillerState,(Case when MR.State_Code='23' Then (Select district_name From pds.districtsmp where district_code=MR.District_Code) Else (Select district_name From OtherState_DistrictCode where district_code=MR.District_Code and State_Id=MR.State_Code) End) As MillerDist,Milling_Type,Agreement_ID,('Lot'+Lot_No) As Lot_No,Acceptance_No As AcptNo,WeightCheck_No,WeightCheck_Date,(Case BagType When '9' Then 'New Jute(SBT)' When '10' Then 'Old Jute(SBT)' When '11' Then 'Once Used Jute(SBT)' When '4' Then 'New PP(HDPE)' When '2' Then 'Once Used PP(HDPE)' Else 'Old PP(HDPE)' End) As  BagType From Receipt_CMR_FCI As CMR Left Join pds.districtsmp As MP ON(MP.district_code=CMR.Paddy_AgrmtDist) Left Join State_Master As SM ON(SM.State_Code=CMR.CMR_RecdState) Left Join OtherState_DistrictCode As OD ON(OD.district_code=CMR.CMR_RecdDist and OD.State_Id=CMR.CMR_RecdState) Left join Miller_Registration_2017 MR ON(MR.Registration_ID=CMR.Mill_ID and MR.CropYear=CMR.CropYear) Where CMR.Receipt_ID='" + ReceiptID + "' and CMR.IsAccepted='Y' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblDistManagerName.Text = ds.Tables[0].Rows[0]["MPDist"].ToString();
                    lblMillerName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblAgrmtNo.Text = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();
                    lblMillerState.Text = ds.Tables[0].Rows[0]["MillerState"].ToString();
                    lblMillerDistrict.Text = ds.Tables[0].Rows[0]["MillerDist"].ToString();
                    lblAcptNo.Text = ds.Tables[0].Rows[0]["AcptNo"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblWeightMemoNo.Text = ds.Tables[0].Rows[0]["WeightCheck_No"].ToString();
                    lblFCiState.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                    lblFCiDist.Text = ds.Tables[0].Rows[0]["OtherDist"].ToString();

                    DateTime frmDate = DateTime.Parse(ds.Tables[0].Rows[0]["AcptDate"].ToString());
                    lblAcptDate.Text = frmDate.ToString("dd/MMM/yyyy");

                    String WeightDate = ds.Tables[0].Rows[0]["WeightCheck_Date"].ToString();
                    if (WeightDate != "")
                    {
                        DateTime WeightCheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["WeightCheck_Date"].ToString());
                        lblWeightMemoDate.Text = WeightCheckDate.ToString("dd/MMM/yyyy");
                    }

                    string Qty = "", Bags = "", Truck = "", LotNo = "",RecptNo= "";
                    Qty = ds.Tables[0].Rows[0]["CommonRice"].ToString();
                    Bags = ds.Tables[0].Rows[0]["Bags"].ToString();
                    Truck = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    LotNo = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    RecptNo = ds.Tables[0].Rows[0]["RecptNo"].ToString();

                    lblServerDateTime.Text = ds.Tables[0].Rows[0]["ServerDate"].ToString();

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    string QRGridDetails = "Dist=" + lblDistManagerName.Text + ", CropYear=" + lblCropYear.Text + ", Agrmt_No=" + lblAgrmtNo.Text + ", Receipt No=" + RecptNo + ", Lot_No=" + LotNo + ", Qty=" + Qty + "(Qtls), Bags=" + Bags + ", Truck_No=" + Truck + "";
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
}