using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class State_PM_Ratio_Master : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    //string Gdistance = "";
    //string Mdistance = "";

    string Branch;
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddlCropYear.Items.Insert(0, "--Select--");
            //ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
            //ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            //ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));

            string fromdate = Request.Form[txtFrmdate.UniqueID];
            txtFrmdate.Text = fromdate;
            string Todate = Request.Form[txtTOdate.UniqueID];
            txtTOdate.Text = Todate;
            GetCropYearValues();
            //GETLotNo();
            //GETCMRPercentNo();

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
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlCropYear.DataSource = ds.Tables[0];
                    ddlCropYear.DataTextField = "CropYear";
                    ddlCropYear.DataValueField = "CropYear";
                    ddlCropYear.DataBind();
                    ddlCropYear.Items.Insert(0, "--Select--");
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
    protected void txttotal_TextChanged(object sender, EventArgs e)
    {
        if (txttotal.Text != "")
        {
            txtfdr.Enabled = true;
            txtfdr.Text = "";
            txtcheck.Text = "";
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please fill Total field'); </script> ");
            return;

        }
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAll.Checked == true)
        {
            rdbparticularMiller.Checked = false;
            trmillname.Visible = false;
            trmillnamespace.Visible = false;
            trdist.Visible = false;
            trdistspace.Visible = false;
            ddlCropYear.ClearSelection();
            ddlMillName.Items.Clear();
            ddlmillDist.Items.Clear();
            
        }
        else
        {
            rdbparticularMiller.Checked = true;
            trmillname.Visible = true;
            trmillnamespace.Visible = true;
            trdist.Visible = true;
            trdistspace.Visible = true;
            ddlCropYear.ClearSelection();
            ddlMillName.Items.Clear();
            ddlmillDist.Items.Clear();
        }
    }
    protected void rdbparticularMiller_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbparticularMiller.Checked == true)
        {
            rdbAll.Checked = false;
            trmillname.Visible = true;
            trmillnamespace.Visible = true;
            trdist.Visible = true;
            trdistspace.Visible = true;
            ddlCropYear.ClearSelection();
            ddlMillName.Items.Clear();
            ddlmillDist.Items.Clear();
          
        }
        else
        {
            rdbAll.Checked = true;
            trmillname.Visible = false;
            trmillnamespace.Visible = false;
            trdist.Visible = false;
            trdistspace.Visible = false;
            ddlCropYear.ClearSelection();
            ddlMillName.Items.Clear();
            ddlmillDist.Items.Clear();
        }
    }
    protected void txtfdr_TextChanged(object sender, EventArgs e)
    {
        int total = Convert.ToInt16(txttotal.Text);
        int fdr = Convert.ToInt16(txtfdr.Text);
        if (txtfdr.Text != "" && fdr < total)
        {
            txtcheck.Enabled = true;
            txtcheck.Text = "";
            int check = total - fdr;
            txtcheck.Text = Convert.ToString(check);
            bttsubmit.Enabled = true;
            txtFrmdate.Enabled = true;
            txtTOdate.Enabled = true;
        }
        else if (fdr >= total && fdr == 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('FDR will always be less than Total'); </script> ");
            txtfdr.Text = "";
            txtcheck.Text = "";
            return;

        }


        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please fill FDR field'); </script> ");
            return;

        }
    }
    protected void txtcheck_TextChanged(object sender, EventArgs e)
    {
        int total = Convert.ToInt16(txttotal.Text);
        int fdr = Convert.ToInt16(txtfdr.Text);
        txtcheck.Enabled = true;
        int check = total - fdr;
        txtcheck.Text = Convert.ToString(check);
        bttsubmit.Enabled = true;
        txtFrmdate.Enabled = true;
        txtTOdate.Enabled = true;
    }


    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex > 0)
        {
            GetMillerDistrict();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year '); </script> ");
            return;


        }

    }
    public void GetMillerDistrict()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlmillDist.DataSource = ds.Tables[0];
                        ddlmillDist.DataTextField = "district_name";
                        ddlmillDist.DataValueField = "district_code";
                        ddlmillDist.DataBind();
                        ddlmillDist.Items.Insert(0, "--Select--");

                    }
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
    protected void ddlmillDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex > 0)
        {
            GetMillerName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller District '); </script> ");
            return;


        }
    }
    public void GetMillerName()
    {
        ddlMillName.Items.Clear();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select R.Mill_ID as MillID, MR.mill_name as MillName from PM_RatioMaster as R inner join Miller_Registration_2017 as MR on MR.Registration_ID=R.Mill_ID and MR.CropYear=R.CropYear where MillerDist='" + ddlmillDist.SelectedValue.ToString() + "' and  R.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' order by  MR.mill_name";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillID";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर का पंजीकरण स्वीकार नहीं किया गया है|'); </script> ");
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
    protected void bttsubmit_Click(object sender, EventArgs e)
    {

        if (rdbAll.Checked == true)
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    ClientIP objClientIP = new ClientIP();
                    string GetIp = objClientIP.GETIP();
                    ConvertServerDate ServerFromDate = new ConvertServerDate();
                    string IssuedFromDate = ServerFromDate.getDate_MDY(txtFrmdate.Text);
                    ConvertServerDate ServerToDate = new ConvertServerDate();
                    string IssuedToDate = ServerToDate.getDate_MDY(txtTOdate.Text);
                    //ConvertServerDate ServerDate = new ConvertServerDate();
                    //string strselect = "update PM_RatioMaster set Total='" + txttotal.Text + "', FDR='" + txtfdr.Text + "', Checks='" + txtcheck.Text + "', IP='" + GetIp + "', CreatedDate=getdate(), ParticularMiller='N', Lot_amount='" + txtlot.Text + "',Frm_Date='" + IssuedFromDate + "', To_Date='" + IssuedToDate + "'  where CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                    string instr = "";
                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                       "insert into PM_RatioMaster_LOG select * from PM_RatioMaster as RM  where RM.CropYear='" + ddlCropYear.SelectedValue.ToString() + "'  and (ParticularMiller='N' or  ParticularMiller is null or  ParticularMiller='')  ; ";

                    instr += "update PM_RatioMaster set Total='" + txttotal.Text + "', FDR='" + txtfdr.Text + "', Checks='" + txtcheck.Text + "', IP='" + GetIp + "', CreatedDate=getdate(), ParticularMiller='N', Lot_amount='" + txtlot.Text + "',Frm_Date='" + IssuedFromDate + "', To_Date='" + IssuedToDate + "'  where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and (ParticularMiller='N' or  ParticularMiller is null or  ParticularMiller='') ; ";

                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    cmd = new SqlCommand(instr, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully for Crop Year  " + ddlCropYear.SelectedValue.ToString() + "'); </script> ");
                        ddlCropYear.Enabled = false;
                        txtlot.Enabled = false;
                        txttotal.Enabled = false;
                        txtfdr.Enabled = false;
                        txtcheck.Enabled = false;
                        txtTOdate.Enabled = false;
                        txtFrmdate.Enabled = false;

                        bttsubmit.Enabled = false;
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
        else if (rdbparticularMiller.Checked == true)
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    ClientIP objClientIP = new ClientIP();
                    string GetIp = objClientIP.GETIP();

                    ConvertServerDate ServerFromDate = new ConvertServerDate();
                    string IssuedFromDate = ServerFromDate.getDate_MDY(txtFrmdate.Text);
                    ConvertServerDate ServerToDate = new ConvertServerDate();
                    string IssuedToDate = ServerToDate.getDate_MDY(txtTOdate.Text);
                    //ConvertServerDate ServerDate = new ConvertServerDate();
                    //string strselect = "update PM_RatioMaster set Total='" + txttotal.Text + "', FDR='" + txtfdr.Text + "', Checks='" + txtcheck.Text + "', ParticularMiller='Y',  CreatedDate=getdate(),  IP='" + GetIp + "',   Lot_amount='" + txtlot.Text + "',Frm_Date='" + IssuedFromDate + "', To_Date='" + IssuedToDate + "' where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MillerDist='" + ddlmillDist.SelectedValue.ToString() + "'";

                    string instr = "";
                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                       "insert into PM_RatioMaster_LOG select * from PM_RatioMaster as RM  where RM.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and MillerDist='" + ddlmillDist.SelectedValue.ToString() + "'  and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' ; ";

                    instr += "update PM_RatioMaster set Total='" + txttotal.Text + "', FDR='" + txtfdr.Text + "', Checks='" + txtcheck.Text + "', ParticularMiller='Y',  CreatedDate=getdate(),  IP='" + GetIp + "',   Lot_amount='" + txtlot.Text + "',Frm_Date='" + IssuedFromDate + "', To_Date='" + IssuedToDate + "' where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MillerDist='" + ddlmillDist.SelectedValue.ToString() + "'; ";

                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";


                    cmd = new SqlCommand(instr, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully for Crop Year  " + ddlCropYear.SelectedValue.ToString() + "'); </script> ");
                        ddlCropYear.Enabled = false;
                        ddlmillDist.Enabled = false;
                        ddlMillName.Enabled = false;
                        txtlot.Enabled = false;
                        txttotal.Enabled = false;
                        txtfdr.Enabled = false;
                        txtcheck.Enabled = false;
                        txtTOdate.Enabled = false;
                        txtFrmdate.Enabled = false;
                        bttsubmit.Enabled = false;
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
}