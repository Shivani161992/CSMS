using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_PrintCMRInsp_Rejection : System.Web.UI.Page
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
                lblRejectionNo.Text = Session["CMRReject_ID"].ToString();
                GetRejectionData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetRejectionData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select GETDATE() As CurrentDateTime,CMR.CMR_RejectionNo,CMR.CropYear,MP.district_name,ST.State_Name,(Case When CMR.Miller_State='23' Then (Select district_name From pds.districtsmp Where district_code=CMR.Miller_Dist) Else (Select district_name From OtherState_DistrictCode where district_code=CMR.Miller_Dist) End) As MillerDist,MR.Mill_Name,Depot.DepotName,Branch,Godown_id,Inspection_Date,Stack_No,Rejected_Qty,Inspected_By From CMR_Inspection_Rejected As CMR Left Join pds.districtsmp As MP ON(CMR.CMR_RecdDist=MP.district_code) Left Join State_Master As ST ON(CMR.Miller_State=ST.State_Code) Left Join Miller_Registration As MR ON(MR.Registration_ID=CMR.Mill_ID and MR.State_Code=CMR.Miller_State) Left Join tbl_MetaData_DEPOT As Depot ON(Depot.DepotID=CMR.IssueCenter and Depot.DistrictId='23'+CMR.CMR_RecdDist) Where CMR.CMR_RejectionNo='" + lblRejectionNo.Text + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblMillerState.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                    lblMillerDist.Text = ds.Tables[0].Rows[0]["MillerDist"].ToString();
                    lblMillerName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblQty.Text = ds.Tables[0].Rows[0]["Rejected_Qty"].ToString();
                    lblStackNo.Text = ds.Tables[0].Rows[0]["Stack_No"].ToString();
                    lblInspectedName.Text = lblInspectedName1.Text = ds.Tables[0].Rows[0]["Inspected_By"].ToString();

                    lblIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblBranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                    lblGodown.Text = ds.Tables[0].Rows[0]["Godown_id"].ToString();
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDateTime"].ToString();

                    DateTime RejectionDate = DateTime.Parse(ds.Tables[0].Rows[0]["Inspection_Date"].ToString());
                    lblRejectionDate.Text = RejectionDate.ToString("dd/MMM/yyyy");

                    GetGodownName();

                    string QRGridDetails = "";

                    QRGridDetails = "RejectionNo.=" + lblRejectionNo.Text + ", CropYear=" + lblCropYear.Text + ", CMR_RecdDist=" + lblDist.Text + ", MillerDist=" + lblMillerDist.Text + ", MillerName=" + lblMillerName.Text + ", IssueCenter='" + lblIC.Text + "', Godown='" + lblGodown.Text + "', RejectedQty='" + lblQty.Text + "'";
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
                string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + lblBranch.Text + "') As BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblGodown.Text + "') As Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblBranch.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
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
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }
}