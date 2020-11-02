using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_PDhan_Challan : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string QRGridDetails = "", districtid = "", MillCode = "";

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
                }
                else
                {
                    Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
                    lblMFD.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
                }

                districtid = Session["dist_id"].ToString();
                lblChallanNo.Text = Session["Paddy_Challan"].ToString();
                lblAgrmtNo.Text = Session["Agreement_ID"].ToString();
                MillCode = Session["MillCode"].ToString();
                GetDataTO();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetDataTO()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select GETDATE() As CurrentDateTime,MP.district_name,PC.CropYear,PC.issue_date,PC.delivery_order_no As DONO,PC.LotNumber As LotNO,DO.From_Date As DOFrmDate, DO.To_Date As DOToDate,MR.Mill_Name,PC.Issued_CommonDhan,PC.Issued_GradeADhan,PC.bags,PC.NoTransaction As BagsType, BT.BagType  as BagsTypeNew, PC.gate_pass As TruckNO,Depot.DepotName,PC.Godown, StackNumber From PaddyMilling_IssueAgainst_DO As PC inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=PC. NoTransaction left Join pds.districtsmp As MP ON(MP.district_code=PC.district_code) Left Join PaddyMilling_DO_2017 AS DO ON(DO.Mill_Code=PC.Partyname and DO.Agreement_ID=PC.Agreement_ID and DO.DO_Number=PC.remark) Left Join Miller_Registration_2017 As MR ON(MR.Registration_ID=PC.Partyname and MR.CropYear=PC.CropYear) Left Join tbl_MetaData_DEPOT As Depot ON(Depot.DepotID=PC.issueCentre_code)where PC.trans_id = '" + lblChallanNo.Text + "' and PC.Agreement_ID='" + lblAgrmtNo.Text + "' and PC.Partyname='" + MillCode + "' and PC.district_code='" + districtid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    
                    lblDistManagerName.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblTotalCDhan.Text = "403";
                    lblTotalGDhan.Text = "0";
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDateTime"].ToString();

                    DateTime FrmDate = DateTime.Parse(ds.Tables[0].Rows[0]["issue_date"].ToString());
                    lblChallanDate.Text = FrmDate.ToString("dd/MMM/yyyy");

                    lblDoNumber.Text = ds.Tables[0].Rows[0]["DONO"].ToString();
                    lblLotNumber.Text = ds.Tables[0].Rows[0]["LotNO"].ToString();
                    lblMillName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();

                    DateTime DODate = DateTime.Parse(ds.Tables[0].Rows[0]["DOFrmDate"].ToString());
                    lblDoDate.Text = DODate.ToString("dd/MMM/yyyy");

                    DateTime DOLastDate = DateTime.Parse(ds.Tables[0].Rows[0]["DOToDate"].ToString());
                    lblDOLastDate.Text = DOLastDate.ToString("dd/MMM/yyyy");

                    lblChalanCDhan.Text = ds.Tables[0].Rows[0]["Issued_CommonDhan"].ToString();
                    lblChalanGDhan.Text = ds.Tables[0].Rows[0]["Issued_GradeADhan"].ToString();

                    lblNoBags.Text = ds.Tables[0].Rows[0]["bags"].ToString();

                    //string bagsCode = ds.Tables[0].Rows[0]["BagsType"].ToString();
                    //if (bagsCode == "9")
                    //{
                    lblBagsType.Text = ds.Tables[0].Rows[0]["BagsTypeNew"].ToString();
                    //}
                    //else if (bagsCode == "10")
                    //{
                    //    lblBagsType.Text = "Old Jute(SBT)";
                    //}
                    //else
                    //{
                    //    lblBagsType.Text = "PP(HDPE)";
                    //}
                    lblTruckNo.Text = ds.Tables[0].Rows[0]["TruckNO"].ToString();
                    lblIssueCentreName.Text = ds.Tables[0].Rows[0]["DepotName"].ToString(); ;
                    lblGodownNo.Text = ds.Tables[0].Rows[0]["Godown"].ToString();
                    lblStackNo.Text = ds.Tables[0].Rows[0]["StackNumber"].ToString();

                    GetGodownName();
                   
                    GetStackNumber();

                    QRGridDetails = "Dist=" + lblDistManagerName.Text + ", DO_No=" + lblDoNumber.Text + ", CropYear=" + lblCropYear.Text + ", Agrmt_No=" + lblAgrmtNo.Text + ", Challan_No=" + lblChallanNo.Text + ", Lot_NO=" + lblLotNumber.Text + ", Challan_Qty=" + lblChalanCDhan.Text + "(Qtls), Bags=" + lblNoBags.Text + ", Truck_No=" + lblTruckNo.Text + "";
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

    public void GetGodownName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblGodownNo.Text + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

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
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }
    public void GetStackNumber()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Stack_ID, Stack_Name from tbl_MetaData_STACK where Stack_ID='"+lblStackNo.Text+"' ");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblStackNo.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();
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
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }
}