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

public partial class PaddyMilling_CMRInspection_Rejection : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["CMRReject_ID"] = null;

                txtStackNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtStackNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtStackNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtQty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtQty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtQty.Attributes.Add("onchange", "return chksqltxt(this)");

                txtFromDate.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtFromDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtFromDate.Attributes.Add("onchange", "return chksqltxt(this)");

                txtDist.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtDist.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtDist.Attributes.Add("onchange", "return chksqltxt(this)");

                txtInspectedName.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtInspectedName.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtInspectedName.Attributes.Add("onchange", "return chksqltxt(this)");

                ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
                ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlCropYear.Items.Insert(0, "--Select--");

                txtDist.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
    }

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        rbMPState.Checked = rbOtherState.Checked = false;
        txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = false;
        txtStackNo.Text = txtQty.Text = txtInspectedName.Text = "";
        ddlOtherStates.Items.Clear();
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlCropYear.SelectedIndex > 0)
        {
            rbMPState.Checked = true;
            GetMPDistrict();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
        }
    }

    private void GetMPDistrict()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        ddlOtherStates.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_code ,district_name FROM pds.districtsmp order by district_name ");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "pds.districtsmp");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDist.DataSource = ds.Tables[0];
                    ddlMacersAddDist.DataTextField = "district_name";
                    ddlMacersAddDist.DataValueField = "district_code";
                    ddlMacersAddDist.DataBind();
                    ddlMacersAddDist.Items.Insert(0, "--Select--");
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

    private void GetOtherStates()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT State_Code ,State_Name FROM State_Master where Status = 'Y' and State_Code!=23 order by State_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "State_Master");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOtherStates.DataSource = ds.Tables[0];
                    ddlOtherStates.DataTextField = "State_Name";
                    ddlOtherStates.DataValueField = "State_Code";
                    ddlOtherStates.DataBind();
                    ddlOtherStates.Items.Insert(0, "--Select--");
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

    protected void ddlOtherStates_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = false;
        txtStackNo.Text = txtQty.Text = txtInspectedName.Text = "";

        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlOtherStates.SelectedIndex > 0)
        {
            GetOtherDistrict();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select State Name'); </script> ");
            return;
        }
    }

    private void GetOtherDistrict()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();

        string dist = ddlOtherStates.SelectedValue.ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "SELECT district_code ,district_name FROM OtherState_DistrictCode where State_Id = '" + dist + "'  order by district_name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "OtherState_DistrictCode");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDist.DataSource = ds.Tables[0];
                    ddlMacersAddDist.DataTextField = "district_name";
                    ddlMacersAddDist.DataValueField = "district_code";
                    ddlMacersAddDist.DataBind();
                    ddlMacersAddDist.Items.Insert(0, "--Select--");
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

    protected void ddlMacersAddDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = false;
        txtStackNo.Text = txtQty.Text = txtInspectedName.Text = "";

        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();
        ddlMacersName.Items.Clear();

        if (rbMPState.Checked)
        {
            getMillName();
        }
        else if (rbOtherState.Checked)
        {
            getMillName();
        }
    }

    protected void getMillName()
    {
        string district = ddlMacersAddDist.SelectedValue.ToString();
        ddlMacersName.Items.Clear();

        //DataTable dt = new DataTable(); DataTable dt1 = new DataTable(); string name = string.Empty; string id = string.Empty;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                if (rbMPState.Checked)
                {
                    select = "Select distinct Registration_ID,Mill_Name from Miller_Registration_2017 where District_Code='" + district + "' and Status = 1 and CropYear = '" + ddlCropYear.Text + "' and State_Code='23' and State='MP' order by Mill_Name";
                }
                else if (rbOtherState.Checked)
                {
                    select = "Select distinct Registration_ID,Mill_Name from Miller_Registration_2017 where District_Code='" + district + "' and Status = 1 and CropYear = '" + ddlCropYear.Text + "' and State_Code='" + ddlOtherStates.SelectedValue.ToString() + "' and State='Other' order by Mill_Name";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersName.DataSource = ds.Tables[0];
                    ddlMacersName.DataTextField = "Mill_Name";
                    ddlMacersName.DataValueField = "Registration_ID";
                    ddlMacersName.DataBind();
                    ddlMacersName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिल का नाम उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlMacersName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = false;
        txtStackNo.Text = txtQty.Text = txtInspectedName.Text = "";

        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlMacersName.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
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

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
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
        txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = false;
        txtStackNo.Text = txtQty.Text = txtInspectedName.Text = "";

        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlIC.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Centre'); </script> ");
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
        txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = false;
        txtStackNo.Text = txtQty.Text = txtInspectedName.Text = "";

        ddlGodam.Items.Clear();

        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
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

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        string DistCode = Session["dist_id"].ToString();

        if (ddlGodam.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
        else if (ddlMacersName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
            return;
        }
        else if (txtFromDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Inspection Date'); </script> ");
            return;
        }
        else if (txtStackNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Stack Number'); </script> ");
            return;
        }
        else if (txtQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Rejected CMR Qty '); </script> ");
            return;
        }
        else if (txtInspectedName.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Inspected By Name'); </script> ");
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(txtFromDate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
                string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                int greaterdate = DateTime.Compare(currentdate, Recdate);

                if (greaterdate == -1)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not greater than Today Date'); </script> ");
                    return;
                }

                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        con.Open();

                        string SubDCDate = "", DCDate = "", instr = "";
                        string selectmax = "Select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                        da = new SqlDataAdapter(selectmax, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                            SubDCDate = DCDate.Substring(2);
                        }

                        if (SubDCDate != "")
                        {
                            string districtid = Session["dist_id"].ToString();
                            string MillerState = "";

                            if (rbOtherState.Checked)
                            {
                                MillerState = ddlOtherStates.SelectedValue.ToString();
                            }
                            else
                            {
                                MillerState = "23";
                            }

                            ConvertServerDate ServerDate = new ConvertServerDate();
                            string ConvertFromDate = ServerDate.getDate_MDY(txtFromDate.Text);

                            double RejectedQty = double.Parse(txtQty.Text);

                            SubDCDate = "CR" + SubDCDate;
                            instr = "Insert Into CMR_Inspection_Rejected(CMR_RejectionNo,CropYear,CMR_RecdDist,Miller_State,Miller_Dist,Mill_ID,IssueCenter,Branch,Godown_id,Inspection_Date,Stack_No,Rejected_Qty,Rem_Qty,Inspected_By,CreatedDate,IP_Address) Values('" + SubDCDate + "','" + ddlCropYear.SelectedItem.ToString() + "','" + districtid + "','" + MillerState + "','" + ddlMacersAddDist.SelectedValue.ToString() + "','" + ddlMacersName.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodam.SelectedValue.ToString() + "','" + ConvertFromDate + "','" + txtStackNo.Text + "','" + RejectedQty + "','" + RejectedQty + "','" + txtInspectedName.Text + "',GETDATE(),'" + GetIp + "')";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = false;
                            btnPrint.Enabled = true;

                            Session["CMRReject_ID"] = SubDCDate.ToString();

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspection of CMR Rejection Entry Is Created Successfully And Your Rejection Number Is " + SubDCDate.ToString() + "'); </script> ");

                            lblmsg.Visible = true;
                            lblmsg.Text = "Inspection of CMR Rejection Entry Is Created Successfully And Your Rejection Number Is " + SubDCDate.ToString();
                            txtDist.Text = "";
                            txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = false;
                            ddlCropYear.Enabled = ddlOtherStates.Enabled = ddlMacersAddDist.Enabled = ddlMacersName.Enabled = ddlIC.Enabled = ddlBranch.Enabled = ddlGodam.Enabled = false;
                            ddlCropYear.Items.Clear();

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        btnRecptSubmit.Enabled = false;
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void rbOtherState_CheckedChanged(object sender, EventArgs e)
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        ddlOtherStates.Items.Clear();
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        rbOtherState.Checked = rbMPState.Checked = false;
        if (ddlCropYear.SelectedIndex > 0)
        {
            ddlOtherStates.Enabled = true;
            rbOtherState.Checked = true;
            GetOtherStates();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
    }
    protected void rbMPState_CheckedChanged(object sender, EventArgs e)
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        ddlOtherStates.Items.Clear();
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        rbOtherState.Checked = rbMPState.Checked = false;
        if (ddlCropYear.SelectedIndex > 0)
        {
            ddlOtherStates.Enabled = false;
            rbMPState.Checked = true;
            GetMPDistrict();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
    }

    protected void ddlGodam_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = false;
        txtStackNo.Text = txtQty.Text = txtInspectedName.Text = "";

        if (ddlGodam.SelectedIndex > 0)
        {
            txtStackNo.Enabled = txtQty.Enabled = txtInspectedName.Enabled = btnRecptSubmit.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PrintCMRInsp_Rejection.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}