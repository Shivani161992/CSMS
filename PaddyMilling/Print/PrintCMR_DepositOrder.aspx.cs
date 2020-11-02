using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_PrintCMR_DepositOrder : System.Web.UI.Page
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
                lblDepositNo.Text = Session["CMRDO_ID"].ToString();
                GetCropYearValues();
                GetDepositNOData();
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

    public void GetDepositNOData()
    {
   
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select GETDATE() As CurrentDateTime,CMRDist.district_name As CMRRecdDist,CMRAgrmtDist.district_name As Paddy_AgrmtDist,Do.CropYear,DO.CMR_DODate, MR.Mill_Name, MillerDist.district_name As MillerDistName,DO.Agreement_ID,MA.From_Date As AgrmtDate, MA.Total_Dhan,DO.Lot_No,Do.DO_No,DO.CreatedDate,Depot.DepotName,DO.Branch,DO.Godown_id, IsAgainst_MovementOrder, MoveOrdernum From CMR_DepositOrder As DO Left Join pds.districtsmp As CMRDist ON(DO.CMR_RecdDist=CMRDist.district_code) Left Join pds.districtsmp As CMRAgrmtDist ON(DO.Paddy_AgrmtDist=CMRAgrmtDist.district_code) Left Join Miller_Registration_2017 As MR ON (MR.Registration_ID=DO.Mill_ID and MR.CropYear='" + hdfCropYear.Value + "') Left Join pds.districtsmp As MillerDist ON(MR.Registration_ID=DO.Mill_ID and MR.CropYear='" + hdfCropYear.Value + "' and MillerDist.district_code=MR.District_Code) Left Join PaddyMilling_Agreement_2017 As MA ON(MA.Agreement_ID=DO.Agreement_ID) Left Join tbl_MetaData_DEPOT As Depot ON(Depot.DepotID=DO.IssueCenter and Depot.DistrictId='23'+DO.CMR_RecdDist) where CMR_DO='" + lblDepositNo.Text + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblDistManagerName.Text = ds.Tables[0].Rows[0]["CMRRecdDist"].ToString();
                    lblAgrmtDist.Text = ds.Tables[0].Rows[0]["Paddy_AgrmtDist"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    DateTime CretedDateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    lblChallanDate.Text = CretedDateDate.ToString("dd/MMM/yyyy");

                    lblMillerName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblMillerPlace.Text = ds.Tables[0].Rows[0]["MillerDistName"].ToString();
                    lblAgrmtNo.Text = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();

                    DateTime AgrmtDate = DateTime.Parse(ds.Tables[0].Rows[0]["AgrmtDate"].ToString());
                    lblAgrmtDate.Text = AgrmtDate.ToString("dd/MMM/yyyy");

                    lblQty.Text = ds.Tables[0].Rows[0]["Total_Dhan"].ToString();
                    lblLotNo.Text = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    lblDoNO.Text = ds.Tables[0].Rows[0]["DO_No"].ToString();

                    DateTime FrmDate = DateTime.Parse(ds.Tables[0].Rows[0]["CMR_DODate"].ToString());
                    lblCMRdeliverDate.Text = FrmDate.ToString("dd/MMM/yyyy");

                    lbldepositdist.Text = ds.Tables[0].Rows[0]["CMRRecdDist"].ToString();
                    lblIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblBranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                    lblGodam.Text = ds.Tables[0].Rows[0]["Godown_id"].ToString();
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDateTime"].ToString();

                   string IsAgaMovementOrder= ds.Tables[0].Rows[0]["IsAgainst_MovementOrder"].ToString();
                   if (IsAgaMovementOrder == "Yes")
                   {
                       lblAgMove.Visible = true;
                       lblMovementOrder.Visible = true;
                       lblAgMove.Text = "Movement Order Number के विरुद्ध....";
                       lblMovementOrder.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();

                   }
                   else if (IsAgaMovementOrder == "No")
                   {
                       lblAgMove.Visible = false;
                       lblMovementOrder.Visible = false;
                   }



                    Getdistname();
                    GetGodownName();
                    

                    string QRGridDetails = "";

                    QRGridDetails = "CMR_Rec_Dist=" + lblDistManagerName.Text + ", Deposit_No=" + lblDepositNo.Text + ", CropYear=" + lblCropYear.Text + ", Agrmt_No=" + lblAgrmtNo.Text + ", Lot_NO=" + lblLotNo.Text + ",DO_NO='" + lblDoNO.Text + "',Paddy_Agrmt_Dist='" + lblAgrmtDist.Text + "'  ";
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
    public void Getdistname()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select (select district_name from districtsmp where district_code='" + lbldepositdist.Text + "') As district_name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lbldepositdist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        //lblBranch.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                        //lblGodam.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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

    public void GetGodownName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + lblBranch.Text + "') As BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblGodam.Text + "') As Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblBranch.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                        lblGodam.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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