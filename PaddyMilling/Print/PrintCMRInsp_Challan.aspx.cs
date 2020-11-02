using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_PrintCMRInsp_Challan : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string QRGridDetails = "", districtid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                districtid = Session["dist_id"].ToString();
                lblChallanNo.Text = Session["RejCMR_Challan"].ToString();
                GetChallanData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetChallanData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select StackName, InspectorName, D1.district_name as MillDist, D.district_name, IC.DepotName, Godown_id, C.Commodity_Name, BT.BagType, CMRAcceptance, CMRDO, AgreementID, LotNumber, RejectedQuantity, RejectedBags, InspectorName, convert(varchar(10), DoFInsp, 103 )DoFInsp,    CMR_RejectionNo, IA.CropYear, Mill_ID , MR.Mill_Name, Issued_Bags, Issued_Qty, Truck_No, convert(varchar(10),Issue_date,103) as Issue_date   from StackRejection_DeliveryChallan as IA inner join Miller_Registration_2017 as MR on IA.CropYear=MR.CropYear and IA.Mill_ID=MR.Registration_ID inner join pds.districtsmp as D on IA.CMR_RecdDist=D.district_code inner join tbl_MetaData_DEPOT as IC on IC.DepotID=IA.IssueCenter inner Join pds.districtsmp as D1 On D1.district_code=MR.District_Code inner join tbl_MetaData_STORAGE_COMMODITY as C on C.Commodity_Id=IA.Commodity_Id inner join FIN_Bag_Type as BT on Bt.Bag_Type_ID=IA.Bags_Type where CMR_Rej_ChallanNo='" + lblChallanNo.Text + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblCMRType.Text = lblCMRType1.Text = lblCMRType2.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lblRejectionNo.Text = ds.Tables[0].Rows[0]["CMR_RejectionNo"].ToString();
                    lblRejectionDate.Text = ds.Tables[0].Rows[0]["DoFInsp"].ToString();
                   // lblRejQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();

                    lblCMRAccep.Text = ds.Tables[0].Rows[0]["CMRAcceptance"].ToString();
                    lblCMRDO.Text = ds.Tables[0].Rows[0]["CMRDO"].ToString();
                    lblRejQty.Text = ds.Tables[0].Rows[0]["RejectedQuantity"].ToString();
                    lblRejBags.Text = ds.Tables[0].Rows[0]["RejectedBags"].ToString();



                   
                   
                  
                   // lblMillerState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                    lblMillerDist.Text = ds.Tables[0].Rows[0]["MillDist"].ToString();
                    lblMillName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblAgree.Text = ds.Tables[0].Rows[0]["AgreementID"].ToString();
                    lblLot.Text = ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    lblChallanDate.Text = ds.Tables[0].Rows[0]["Issue_date"].ToString();
                    lblBagsType.Text = ds.Tables[0].Rows[0]["BagType"].ToString();
                    lblIssuedQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();
                    lblNoBags.Text = ds.Tables[0].Rows[0]["Issued_Bags"].ToString();




                    lblTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    lblIssueCentreName.Text = ds.Tables[0].Rows[0]["DepotName"].ToString(); ;
                    lblGodownNo.Text = ds.Tables[0].Rows[0]["Godown_id"].ToString();
                    lblStackNo.Text = ds.Tables[0].Rows[0]["StackName"].ToString();
                    lblInspName.Text = ds.Tables[0].Rows[0]["InspectorName"].ToString();

                    GetGodownName();

                    QRGridDetails = "Dist=" + lblDist.Text + ", CropYear=" + lblCropYear.Text + ", Challan No='" + lblChallanNo.Text + "', Challan Date='" + lblChallanDate.Text + "', Issued Qty='" + lblIssuedQty + "', Issued Bags='" + lblNoBags + "', Truck No='" + lblTruckNo.Text + "', Godown='" + lblGodownNo.Text + "'";
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