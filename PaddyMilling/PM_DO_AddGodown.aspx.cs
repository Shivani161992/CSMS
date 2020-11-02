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
using System.Drawing;

public partial class PaddyMilling_PM_DO_AddGodown : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                GridView1.DataSource = "";
                GridView1.DataBind();

                GetCropYearValues();
                GetDistName();


                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (Session["st_id"].ToString() == "10")
            {
                this.MasterPageFile = "~/MasterPage/Markfed_PDY.master";
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
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                if (Session["st_id"].ToString() == "10")
                {
                    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed='Y' Order By district_name";
                }
                else
                {
                    select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist.DataSource = ds.Tables[0];
                        ddlFrmDist.DataTextField = "district_name";
                        ddlFrmDist.DataValueField = "district_code";
                        ddlFrmDist.DataBind();
                        ddlFrmDist.Items.Insert(0, "--Select--");
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

    public void GetOnlyDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code!='" + ddlFrmDist.SelectedValue.ToString() + "' Order By district_name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist1.DataSource = ds.Tables[0];
                        ddlFrmDist1.DataTextField = "district_name";
                        ddlFrmDist1.DataValueField = "district_code";
                        ddlFrmDist1.DataBind();
                        ddlFrmDist1.Items.Insert(0, "--Select--");
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        ClientIP objClientIP = new ClientIP();
        string GetIp = (objClientIP.GETIP());

        if (txtYear.Text != "")
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        string instr = "";

                        string ChechMax = "", DCDate = "", MaxMappingNo = "";
                        //decimal MaxMappingNo = 0;
                        //ChechMax = "Select Max(Mapping_No) As MaxMap From PaddyMapping_Godown Where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ";
                        //da = new SqlDataAdapter(ChechMax, con);
                        //ds = new DataSet();
                        //da.Fill(ds);

                        //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        //{
                        //    MaxMappingNo = decimal.Parse(ds.Tables[0].Rows[0]["MaxMap"].ToString());
                        //}

                        ChechMax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                        da = new SqlDataAdapter(ChechMax, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                            MaxMappingNo = DCDate.Substring(2);
                        }

                        if (MaxMappingNo != "")
                        {
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            if (chkGodam.Checked == true)
                            {
                                for (int i = 0; i < GridView2.Rows.Count; i++)
                                {
                                    string RemPaddy = GridView2.Rows[i].Cells[4].Text;
                                    string RemAllotedPaddy = GridView2.Rows[i].Cells[3].Text;
                                    string MappingNo = GridView2.Rows[i].Cells[6].Text;
                                    instr += "Update PaddyMapping_Godown Set Alloted_CommonPaddy='" + RemAllotedPaddy + "', Rem_CommonPaddy='" + RemPaddy + "' where Mapping_No='" + MappingNo + "' ;";
                                }
                            }
                            else
                            {
                                for (int i = 0; i < GridView2.Rows.Count; i++)
                                {
                                    string RemPaddy = GridView2.Rows[i].Cells[4].Text;
                                    string RemAllotedPaddy = GridView2.Rows[i].Cells[3].Text;
                                    string MappingNo = GridView2.Rows[i].Cells[6].Text;
                                    instr += "Update PaddyMapping_Godown Set Alloted_CommonPaddy='" + RemAllotedPaddy + "', Rem_CommonPaddy='" + RemPaddy + "' where Mapping_No='" + MappingNo + "' ;";
                                }

                                DataTable dt = adddetails();
                                if (dt != null)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        string Mapping_No = MaxMappingNo + (i + 1);
                                        instr += "Insert into PaddyMapping_Godown(District,CropYear,Mill_ID,Agreement_ID,Mapping_No,IssueCenter,Godown_id,Alloted_CommonPaddy,Alloted_GradePaddy,Rem_CommonPaddy,Rem_GradePaddy,CreatedDate,IP_Address) values('" + dt.Rows[i]["DistCode"] + "','" + txtYear.Text + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedValue.ToString() + "','" + Mapping_No + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','0','" + dt.Rows[i]["QuantityQtls"] + "','0',GETDATE(),'" + GetIp + "');";
                                    }
                                }
                            }

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = btnAdd.Enabled = false;
                                Session["ICGBQ"] = null;
                                ddlIssueCentre.Items.Clear();
                                ddlBranch.Items.Clear();
                                ddlGodown.Items.Clear();

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully'); </script> ");
                                txtYear.Text = "";
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
                            }
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Updation Not Allow'); </script> ");
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

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (Session["st_id"].ToString() == "10")
            {
                Response.Redirect("~/State/PaddyMillingHome_MFD.aspx");
            }
            else
            {
                Response.Redirect("~/State/PaddyMillingHome.aspx");
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        chkGodam.Checked = chkChange.Checked = false;
        chkGodam.Visible = chkChange.Visible = false;
        trMsg.Visible = false;

        txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = txtAgrmtQty.Text = "";
        hdfIndex.Value = hdfSelectedGodam.Value = "";

        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetMillName();
            //GetMPIssueCentre();
            GetOnlyDistName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + ddlFrmDist.SelectedValue.ToString() + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAgtmtNumber.Items.Clear();
        //ddlIssueCentre.SelectedIndex = 0;
        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();

        chkGodam.Checked = chkChange.Checked = false;
        chkGodam.Visible = chkChange.Visible = false;
        trMsg.Visible = false;

        txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = txtAgrmtQty.Text = "";
        hdfIndex.Value = hdfSelectedGodam.Value = "";
        hdfGodown.Value = "";

        GridView2.DataSource = "";
        GridView2.DataBind();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + ddlFrmDist.SelectedValue.ToString() + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgtmtNumber.DataSource = ds.Tables[0];
                        ddlAgtmtNumber.DataTextField = "Agreement_ID";
                        ddlAgtmtNumber.DataValueField = "Agreement_ID";
                        ddlAgtmtNumber.DataBind();
                        ddlAgtmtNumber.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
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

    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlIssueCentre.SelectedIndex = 0;
        ddlIssueCentre.Items.Clear();
        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();
        txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = txtAgrmtQty.Text = "";
        hdfIndex.Value = hdfSelectedGodam.Value = "";
        hdfGodown.Value = "";
        chkGodam.Checked = chkChange.Checked = false;
        chkGodam.Visible = chkChange.Visible = false;
        trMsg.Visible = false;

        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetMPIssueCentre();
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select PMG.IssueCenter,Mapping_No,Godown_id,Alloted_CommonPaddy,Rem_CommonPaddy ,Agrmt.Common_Dhan From PaddyMapping_Godown As PMG Left Join PaddyMilling_Agreement_2017 As Agrmt ON(Agrmt.Agreement_ID=PMG.Agreement_ID and Agrmt.CropYear=PMG.CropYear) where PMG.CropYear='" + txtYear.Text + "' and PMG.Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and PMG.Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtAgrmtQty.Text = ds.Tables[0].Rows[0]["Common_Dhan"].ToString();
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();

                    chkGodam.Visible = chkChange.Visible = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string GodownName = "", ICName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            GodownName = e.Row.Cells[2].Text;
            ICName = e.Row.Cells[1].Text;

            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = "";
                    select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where DepotId='" + ICName + "') As ICName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') As Godown_Name");
                    da = new SqlDataAdapter(select, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                            e.Row.Cells[1].Text = ds.Tables[0].Rows[0]["ICName"].ToString();
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

    protected void GridView2_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfIndex.Value = hdfSelectedGodam.Value = "";
        txtDORemQty.Text = "";
        txtDORemQty.Text = decimal.Parse(GridView2.Rows[GridView2.SelectedIndex].Cells[4].Text).ToString();
        hdfIndex.Value = int.Parse(GridView2.Rows[GridView2.SelectedIndex].Cells[0].Text).ToString();
        hdfSelectedGodam.Value = decimal.Parse(GridView2.Rows[GridView2.SelectedIndex].Cells[5].Text).ToString();

        if (ddlIssueCentre.Items.Count > 0)
        {
            ddlIssueCentre.SelectedIndex = 0;
        }

        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();
        txtGodownRemQty.Text = "";

        foreach (GridViewRow row in GridView2.Rows)
        {
            if (row.RowIndex == GridView2.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFBD6");
            }
        }
    }

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist.SelectedValue.ToString() + "' order by DepotName";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueCentre.DataSource = ds.Tables[0];
                    ddlIssueCentre.DataTextField = "DepotName";
                    ddlIssueCentre.DataValueField = "DepotID";
                    ddlIssueCentre.DataBind();
                    ddlIssueCentre.Items.Insert(0, "--Select--");
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

    protected void ddlIssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();
        txtGodownRemQty.Text = txtQty.Text = "";

        if (ddlIssueCentre.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
        }
    }

    public void GetBranch()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ddlIssueCentre.SelectedValue.ToString() + "'");
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
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist.SelectedValue.ToString() + "' order by DepotName");
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
        ddlGodown.Items.Clear();

        txtGodownRemQty.Text = txtQty.Text = "";
        hdfGodown.Value = "";

        if (ddlBranch.SelectedIndex > 0)
        {
            if (chkChange.Checked)
            {
                GETMappingGodown(); //For Other Dist Paddy Godown
                if (hdfGodown.Value != "")
                {
                    OnlyMappedGodown();
                }
            }
            else
            {
                GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
        }
    }

    public void GETMappingGodown()
    {
        hdfGodown.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "Select Godown_id From PM_DistToGodown Where FrmDistrict='" + ddlFrmDist.SelectedValue.ToString() + "' and ToDistrict='" + ddlFrmDist1.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfGodown.Value += ((hdfGodown.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["Godown_id"].ToString() + "'";
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मुख्यालय से गोदाम की मैपिंग कीजिये|'); </script> ");
                    return;
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

    public void OnlyMappedGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID IN(" + hdfGodown.Value + ") order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मुख्यालय से गोदाम की मैपिंग कीजिये|'); </script> ");
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

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
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

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGodownRemQty.Text = txtQty.Text = "";
        txtQty.Enabled = false;

        if (ddlGodown.SelectedIndex > 0)
        {
            GetGodownBalance();
            txtQty.Text = txtDORemQty.Text;
            txtQty.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
        }
    }

    public void GetGodownBalance()
    {
        string pqry = "space_forcommodity_ingodown";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                SqlCommand cmdpqty = new SqlCommand(pqry, con);
                cmdpqty.CommandType = CommandType.StoredProcedure;

                cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = ddlFrmDist.SelectedValue.ToString();
                cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = ddlIssueCentre.SelectedValue.ToString();
                cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = ddlGodown.SelectedValue.ToString();
                cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = "13";  //Paddy-Common
                cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = "01/01/2015";

                DataSet ds = new DataSet();
                SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);
                dr.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

                    txtGodownRemQty.Text = Convert.ToString(stock);
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtDORemQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mapping की गयी मात्रा में से किसी एक मात्रा का चुनाव करे|'); </script> ");
            return;
        }

        double DORemQty = double.Parse(txtDORemQty.Text);
        double AllotedQty = double.Parse(txtQty.Text);

        if (ddlAgtmtNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया अनुबंध नंबर चुने|'); </script> ");
            return;
        }
        if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया गोदाम चुने|'); </script> ");
            return;
        }
        else if (AllotedQty == 0.0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Do Not Allow 0'); </script> ");
            return;
        }
        else if (txtQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया Quantity इंटर करे|'); </script> ");
            return;
        }
        else if (DORemQty < AllotedQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mapping के आधार पर बची हुई मात्रा के अनुसार ही धान का आबंटन करे|'); </script> ");
            return;
        }
        else
        {
            chkGodam.Enabled = false;

            if (chkGodam.Checked == true)
            {
                int checkDupGodown = 0;

                if (hdfSelectedGodam.Value == ddlGodown.SelectedValue.ToString())
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप उस गोदाम का चुनाव नहीं कर सकते, जिस गोदाम को आपने Select किया हैं|'); </script> ");
                    return;
                }

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    string GodownCode = GridView2.Rows[i].Cells[5].Text;
                    if (GodownCode == ddlGodown.SelectedValue.ToString())
                    {
                        checkDupGodown = 1;
                        break;
                    }
                }

                if (checkDupGodown != 0)
                {
                    ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlFrmDist.Enabled = false;

                    int index = (int.Parse(hdfIndex.Value)) - 1;
                    txtDORemQty.Text = txtQty.Text = GridView2.Rows[index].Cells[4].Text = (DORemQty - AllotedQty).ToString();

                    double MappedQty = 0;
                    MappedQty = double.Parse(GridView2.Rows[index].Cells[3].Text);
                    GridView2.Rows[index].Cells[3].Text = (MappedQty - AllotedQty).ToString();

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        string GodownCode1 = GridView2.Rows[i].Cells[5].Text;
                        if (GodownCode1 == ddlGodown.SelectedValue.ToString())
                        {
                            double LastQty = 0, LastMappedQty = 0;
                            LastQty = double.Parse(GridView2.Rows[i].Cells[4].Text);
                            LastMappedQty = double.Parse(GridView2.Rows[i].Cells[3].Text);

                            GridView2.Rows[i].Cells[4].Text = (LastQty + AllotedQty).ToString();
                            GridView2.Rows[i].Cells[3].Text = (LastMappedQty + AllotedQty).ToString();
                            break;
                        }
                    }

                    btnRecptSubmit.Enabled = true;
                    txtQty.Focus();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप केवल उसी गोदाम का चुनाव कर सकते, जिस गोदाम की आपने पहले से Mapping की हैं|'); </script> ");
                    return;
                }
            }
            else
            {
                DataTable dt = adddetails();
                if (dt == null)
                {
                    dt = new DataTable("aadqty");
                    dt.Columns.Add("ICName");
                    dt.Columns.Add("ICVal");
                    dt.Columns.Add("GodownName");
                    dt.Columns.Add("GodownVal");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("QuantityQtls");
                    dt.Columns.Add("DistCode");
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (ddlGodown.SelectedValue.ToString() == dt.Rows[i]["GodownVal"].ToString())
                    {
                        count = 1;
                        break;
                    }
                }

                if (count == 0)
                {
                    int checkDupGodown = 0;

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        string GodownCode = GridView2.Rows[i].Cells[5].Text;
                        if (GodownCode == ddlGodown.SelectedValue.ToString())
                        {
                            checkDupGodown = 1;
                            break;
                        }
                    }

                    if (checkDupGodown == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ICName"] = ddlIssueCentre.SelectedItem.Text;
                        dr["ICVal"] = ddlIssueCentre.SelectedValue;
                        dr["GodownName"] = ddlGodown.SelectedItem.Text;
                        dr["GodownVal"] = ddlGodown.SelectedValue;
                        dr["Quantity"] = dr["QuantityQtls"] = txtQty.Text;

                        if (chkChange.Checked && ddlFrmDist1.SelectedIndex > 0)
                        {
                            dr["DistCode"] = ddlFrmDist1.SelectedValue.ToString();
                        }
                        else
                        {
                            dr["DistCode"] = ddlFrmDist.SelectedValue.ToString();
                        }

                        dr["quantity"] = (AllotedQty).ToString("0.00");
                        dt.Rows.Add(dr);
                        Session["ICGBQ"] = dt;
                        ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlFrmDist.Enabled = false;
                        fillgrid();

                        int index = (int.Parse(hdfIndex.Value)) - 1;
                        txtDORemQty.Text = txtQty.Text = GridView2.Rows[index].Cells[4].Text = (DORemQty - AllotedQty).ToString();

                        double MappedQty = 0;
                        MappedQty = double.Parse(GridView2.Rows[index].Cells[3].Text);
                        GridView2.Rows[index].Cells[3].Text = (MappedQty - AllotedQty).ToString();

                        btnRecptSubmit.Enabled = true;
                        txtQty.Focus();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप उस गोदाम का चुनाव नहीं कर सकते, जिस गोदाम की आपने पहले से Mapping की हैं|'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Duplicate Godown Is Not Allowed'); </script> ");
                    return;
                }
            }
        }
    }

    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["ICGBQ"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void chkGodam_CheckedChanged(object sender, EventArgs e)
    {
        if (chkGodam.Checked == true)
        {
            btnAdd.Text = "Update";
        }
        else
        {
            btnAdd.Text = "Add";
        }
    }

    protected void chkChange_CheckedChanged(object sender, EventArgs e)
    {
        ddlFrmDist1.SelectedIndex = 0;

        if (chkChange.Checked)
        {
            if (ddlAgtmtNumber.SelectedIndex <= 0)
            {
                trMsg.Visible = false;
                chkChange.Checked = false;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
                return;
            }
            else
            {
                ddlIssueCentre.Items.Clear();
                ddlBranch.Items.Clear();
                ddlGodown.Items.Clear();
                trMsg.Visible = true;
            }
        }
        else
        {
            ddlIssueCentre.Items.Clear();
            ddlBranch.Items.Clear();
            ddlGodown.Items.Clear();
            GetMPIssueCentre();
            trMsg.Visible = false;
        }
    }

    protected void ddlFrmDist1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (ddlFrmDist1.SelectedIndex > 0)
        {
            GetMPIssueCentre1();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetMPIssueCentre1()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist1.SelectedValue.ToString() + "' order by DepotName";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueCentre.DataSource = ds.Tables[0];
                    ddlIssueCentre.DataTextField = "DepotName";
                    ddlIssueCentre.DataValueField = "DepotID";
                    ddlIssueCentre.DataBind();
                    ddlIssueCentre.Items.Insert(0, "--Select--");
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

}