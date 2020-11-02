using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_Print_MillingAgreement : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                lblAgreeNo.Text = Session["Agreement_ID"].ToString();
                lblAgrmtNo.Text = Session["Agreement_ID"].ToString();
                lblAgmntNo.Text = Session["Agreement_ID"].ToString();
                GetCropYearValues();
                GetAgreementNOData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfCropYear.Value = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetAgreementNOData()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string AgreementID= Session["Agreement_ID"].ToString();
                string select = "select DMP.district_name as District, Dist_Manager_Name, DM.district_name as Miller_Dist, MR.Mill_Name as Miller_Name, PMA.CropYear,CONVERT(varchar(10), From_Date, 103) as From_Date, CONVERT(varchar(10),To_Date, 103) as To_Date, Common_Dhan, GradeA_Dhan, Total_Dhan, DhanAmountDetails,CONVERT(varchar(10),Agrmt_Date, 103) as Agrmt_Date, R_Arva, R_Ushna, Milling_Type, MobileNO, MR.milling_capacity_arwa as Arva, MR.milling_capacity_usna as Ushna, PMA.DhanLot as SecLot, PMA.DhanAmountType As SecDepType, PMA.DepositMoney as SecDeposit from PaddyMilling_Agreement_2017 as PMA inner join pds.districtsmp as DMP on DMP.district_code=PMA.District inner join pds.districtsmp as DM on DM.district_code=PMA.Mill_Addr_District inner join Miller_Registration_2017 as MR on MR.CropYear=PMA.CropYear and MR.Registration_ID=PMA.Mill_Name and MR.District_Code=PMA.Mill_Addr_District where Agreement_ID='" + AgreementID + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblDistManagerName.Text = ds.Tables[0].Rows[0]["District"].ToString();

                    lblDM.Text = ds.Tables[0].Rows[0]["Dist_Manager_Name"].ToString();
                    //lblAgrmtDist.Text = ds.Tables[0].Rows[0]["Paddy_AgrmtDist"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblAgrntDate.Text = ds.Tables[0].Rows[0]["Agrmt_Date"].ToString();
                    lblAgreeDate.Text = ds.Tables[0].Rows[0]["Agrmt_Date"].ToString();
                    lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString();
                    lblMobNo.Text = ds.Tables[0].Rows[0]["MobileNO"].ToString();
                    lblMillerPlace.Text = ds.Tables[0].Rows[0]["Miller_Dist"].ToString();
                    lblAgrmtDate.Text = ds.Tables[0].Rows[0]["Agrmt_Date"].ToString();
                   // DateTime CretedDateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    lblQty.Text = ds.Tables[0].Rows[0]["Total_Dhan"].ToString();
                    lblMillName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString();
                    lblLotNo.Text = ds.Tables[0].Rows[0]["DhanAmountDetails"].ToString();
                    lblquantity.Text = ds.Tables[0].Rows[0]["Total_Dhan"].ToString();
                    lblagrmtFrmDate.Text = ds.Tables[0].Rows[0]["From_Date"].ToString();
                    lblagrmtToDate.Text = ds.Tables[0].Rows[0]["To_Date"].ToString();

                    //DateTime AgrmtDate = DateTime.Parse(ds.Tables[0].Rows[0]["AgrmtDate"].ToString());
                    //lblAgrmtDate.Text = AgrmtDate.ToString("dd/MMM/yyyy");

                    lblarva.Text = ds.Tables[0].Rows[0]["Arva"].ToString();
                    lblushna.Text = ds.Tables[0].Rows[0]["Ushna"].ToString();
                    lblMillingType.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                    //lblSecLot.Text = ds.Tables[0].Rows[0]["SecLot"].ToString();
                    //lblSecAmt.Text = ds.Tables[0].Rows[0]["SecDeposit"].ToString();
                    //lblSecDepType.Text = ds.Tables[0].Rows[0]["SecDepType"].ToString();

                    lblCommPaddy.Text = ds.Tables[0].Rows[0]["Common_Dhan"].ToString();

                    lblGradePaddy.Text = ds.Tables[0].Rows[0]["GradeA_Dhan"].ToString();

                    lblTotalDhan.Text = ds.Tables[0].Rows[0]["Total_Dhan"].ToString();




                    string QRGridDetails = "";

                    QRGridDetails = "Miller_Name=" + lblMillerName.Text + ",Miller_District=" + lblMillerPlace.Text+ ", CropYear=" + lblCropYear.Text + ", Agrmt_No=" + lblAgrmtNo.Text + ", Lot_NO=" + lblLotNo.Text + ",Total_Amt='" + lblQty.Text + "',Paddy_Agrmt_Dist='" + lblDistManagerName.Text + "'  ";
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