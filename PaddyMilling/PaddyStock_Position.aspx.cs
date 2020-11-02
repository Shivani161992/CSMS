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

public partial class PaddyMilling_PaddyStock_Position : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                GetCommodity();
                GetDist();
                lblCommodity.Text = lblCommodity1.Text = ddlCommodity.SelectedItem.ToString();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN('13','14') order by Commodity_Id";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCommodity.DataSource = ds.Tables[0];
                    ddlCommodity.DataTextField = "Commodity_Name";
                    ddlCommodity.DataValueField = "Commodity_Id";
                    ddlCommodity.DataBind();
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

    private void GetDist()
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
                        ddlDistrict.DataSource = ds.Tables[0];
                        ddlDistrict.DataTextField = "district_name";
                        ddlDistrict.DataValueField = "district_code";
                        ddlDistrict.DataBind();
                        ddlDistrict.Items.Insert(0, "--Select--");
                        ddlDistrict.SelectedValue = Session["dist_id"].ToString();
                        GetMPIssueCentre();
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

    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCommodity.Text = ddlCommodity.SelectedItem.ToString();
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
                    txtYear.Text = lblYear.Text = lblYear1.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        txtOpeningBal.Text = txtBags.Text = "";
        hdfCount.Value = "";
        txtOpeningBal.Enabled = txtBags.Enabled = btnRecptSubmit.Enabled = btnRecptSave.Enabled = btnRecptUpdate.Enabled = false;

        if (ddlDistrict.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    private void GetMPIssueCentre()
    {
        string districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIC.DataSource = ds.Tables[0];
                    ddlIC.DataTextField = "DepotName";
                    ddlIC.DataValueField = "DepotID";
                    ddlIC.DataBind();
                    ddlIC.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();
        txtOpeningBal.Text = txtBags.Text = "";
        hdfCount.Value = "";
        txtOpeningBal.Enabled = txtBags.Enabled = btnRecptSubmit.Enabled = btnRecptSave.Enabled = btnRecptUpdate.Enabled = false;

        if (ddlIC.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया प्रदाय केंद्र चुनें|'); </script> ");
        }
    }

    public void GetBranch()
    {
        string districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", ddlIC.SelectedValue.ToString());
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOpeningBal.Text = "";
        ddlGodam.Items.Clear();
        hdfCount.Value = txtBags.Text = "";
        txtOpeningBal.Enabled = txtBags.Enabled = btnRecptSubmit.Enabled = btnRecptSave.Enabled = btnRecptUpdate.Enabled = false;

        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें|'); </script> ");
        }
    }

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodam.DataSource = ds.Tables[0];
                        ddlGodam.DataTextField = "Godown_Name";
                        ddlGodam.DataValueField = "Godown_ID";
                        ddlGodam.DataBind();
                        ddlGodam.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
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

    protected void ddlGodam_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOpeningBal.Text = txtBags.Text = "";
        hdfCount.Value = "";
        txtOpeningBal.Enabled = txtBags.Enabled = btnRecptSubmit.Enabled = btnRecptSave.Enabled = btnRecptUpdate.Enabled = false;
        Label2.Visible = false;

        if (ddlGodam.SelectedIndex > 0)
        {
            GetGodownQty();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें|'); </script> ");
        }
        
    }
    
    public void GetGodownQty()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string districtid = Session["dist_id"].ToString();
                con.Open();

                string select = "Select Opening_Qty,Bags From PaddyStock_Position Where Login_Dist='" + districtid + "' and Paddy_Dist='" + ddlDistrict.SelectedValue.ToString() + "' and Godown='" + ddlGodam.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfCount.Value = "1";   
                    txtOpeningBal.Text = ds.Tables[0].Rows[0]["Opening_Qty"].ToString();
                    txtBags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने इस गोदाम के लिए Balance डाल रखा है, इसलिए आप इसमें कोई भी बदलाव नहीं कर सकते|'); </script> ");
                    btnRecptUpdate.Enabled = true;
                    return;
                    
                }
                else
                {
                    txtOpeningBal.Enabled = txtBags.Enabled = btnRecptSubmit.Enabled  = true;
                    btnRecptSave.Enabled = false;
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

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        Decimal OpeningBal = 0;
        OpeningBal = decimal.Parse(txtOpeningBal.Text);

        if (ddlGodam.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
        else if (txtOpeningBal.Text.Trim() == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Qty'); </script> ");
            return;
        }
        else if (txtBags.Text.Trim() == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Bags'); </script> ");
            return;
        }

        else if (hdfCount.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने इस गोदाम के लिए Balance डाल रखा है, इसलिए आप इसमें कोई भी बदलाव नहीं कर सकते|'); </script> ");
            return;
        }
        else
        {
            string districtid = Session["dist_id"].ToString();

            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        int CheckBag = int.Parse(txtBags.Text);

                        string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                        da = new SqlDataAdapter(selectmax, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        string DCDate = "", SubDCDate = "", instr = "";

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                            SubDCDate = DCDate.Substring(2);
                        }

                        if (SubDCDate != "")
                        {
                            instr += "Insert Into PaddyStock_Position(PaddyStock_No,Login_Dist,Paddy_Dist,Commodity_Id,IssueCentre,Branch,Godown,Opening_Qty,Rem_Qty,Bags,CropYear,CreatedDate,IP) Values('" + SubDCDate + "','" + districtid + "','" + ddlDistrict.SelectedValue.ToString() + "','" + ddlCommodity.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodam.SelectedValue.ToString() + "','" + OpeningBal + "','" + OpeningBal + "','" + CheckBag + "','" + txtYear.Text + "',GETDATE(),'" + GetIp + "')";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = txtOpeningBal.Enabled = txtBags.Enabled = false;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Saved Successfully'); </script> ");

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                            Label2.Visible = true;
                            Label2.Text = "Data Is Saved Successfully";
                            btnRecptUpdate.Enabled = true;

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
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        if (ddlGodam.SelectedIndex > 0)
        {
            ddlGodam.SelectedIndex = 0;
        }
        txtOpeningBal.Text = txtBags.Text = "";
        hdfCount.Value = "";
        txtOpeningBal.Enabled = txtBags.Enabled = btnRecptSubmit.Enabled = btnRecptSave.Enabled = btnRecptUpdate.Enabled = false;
        Label2.Visible = false;
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }


    protected void btnRecptUpdate_Click(object sender, EventArgs e)
    {
        txtOpeningBal.Enabled = txtBags.Enabled = btnRecptSave.Enabled = true;
            btnRecptSubmit.Enabled = false;
    }
    protected void btnRecptSave_Click(object sender, EventArgs e)
    {
        Decimal OpeningBal = 0;
        OpeningBal = decimal.Parse(txtOpeningBal.Text);
        int CheckBag = int.Parse(txtBags.Text);
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string districtid = Session["dist_id"].ToString();
                con.Open();

                string select = "";
                select = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                select += "insert into PaddyStock_Position_log_2017 select * from PaddyStock_Position where Login_Dist='" + districtid + "' and Paddy_Dist='" + ddlDistrict.SelectedValue.ToString() + "' and Godown='" + ddlGodam.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "'";
                select += "update PaddyStock_Position set  Opening_Qty='" + OpeningBal + "',Bags='" + CheckBag + "',Rem_Qty='" + OpeningBal + "' Where Login_Dist='" + districtid + "' and Paddy_Dist='" + ddlDistrict.SelectedValue.ToString() + "' and Godown='" + ddlGodam.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "'";
                select += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                cmd = new SqlCommand(select, con);
                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    btnRecptSubmit.Enabled = txtOpeningBal.Enabled = txtBags.Enabled = false;

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Saved Successfully'); </script> ");

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    Label2.Visible = true;
                    Label2.Text = "Data Is Saved Successfully";
                    btnRecptUpdate.Enabled = true;
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

