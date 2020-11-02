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

public partial class PaddyMilling_RejectedCMR_DepositOrder : System.Web.UI.Page
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
                Session["CMRDeposit_ID"] = null;

                txtFromDate.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtFromDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtFromDate.Attributes.Add("onchange", "return chksqltxt(this)");

                txtDist.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtDist.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtDist.Attributes.Add("onchange", "return chksqltxt(this)");

                txtInspDate.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtInspDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtInspDate.Attributes.Add("onchange", "return chksqltxt(this)");

                txtStackNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtStackNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtStackNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRejQty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRejQty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRejQty.Attributes.Add("onchange", "return chksqltxt(this)");

                txtIssuedQty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtIssuedQty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtIssuedQty.Attributes.Add("onchange", "return chksqltxt(this)");

                ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
                ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlCropYear.Items.Insert(0, "--Select--");

                txtDist.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCommodity();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
    }

    public void GetMPIssueCentre()
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

    private void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN('3','4') order by Commodity_Id";
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

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
        txtInspDate.Text = txtIssuedQty.Text = txtRejQty.Text = txtStackNo.Text = "";
        ddlMillName.Items.Clear();
        ddlRejectionNo.Items.Clear();
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        GetMillName();
    }

    private void GetMillName()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select CMR.Mill_ID,MR.Mill_Name From CMR_Inspection_Rejected As CMR Left Join Miller_Registration As MR ON(CMR.Miller_State=MR.State_Code and CMR.Miller_Dist=MR.District_Code and CMR.Mill_ID=MR.Registration_ID) Where CMR.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and CMR.CMR_RecdDist='" + districtid + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "Mill_Name";
                    ddlMillName.DataValueField = "Mill_ID";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Name Is Not Available'); </script> ");
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

    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtInspDate.Text = txtIssuedQty.Text = txtRejQty.Text = txtStackNo.Text = "";
        btnRecptSubmit.Enabled = false;
        ddlRejectionNo.Items.Clear();
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlMillName.SelectedIndex > 0)
        {
            GetRejectionNo();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
            return;
        }
    }

    private void GetRejectionNo()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select CMR_RejectionNo From CMR_Inspection_Rejected Where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and CMR_RecdDist='" + districtid + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRejectionNo.DataSource = ds.Tables[0];
                    ddlRejectionNo.DataTextField = "CMR_RejectionNo";
                    ddlRejectionNo.DataValueField = "CMR_RejectionNo";
                    ddlRejectionNo.DataBind();
                    ddlRejectionNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rejection No. Is Not Available'); </script> ");
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
    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtInspDate.Text = txtIssuedQty.Text = txtRejQty.Text = txtStackNo.Text = "";
        btnRecptSubmit.Enabled = false;
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlRejectionNo.SelectedIndex > 0)
        {
            GetRejectionData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Rejection No.'); </script> ");
            return;
        }
    }

    private void GetRejectionData()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Inspection_Date,Stack_No,Rejected_Qty,(Rejected_Qty-Rem_Qty) As IssuedQty From CMR_Inspection_Rejected Where CMR_RejectionNo='" + ddlRejectionNo.SelectedValue.ToString() + "' and CMR_RecdDist='" + districtid + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DateTime InspectionDate = DateTime.Parse(ds.Tables[0].Rows[0]["Inspection_Date"].ToString());
                    txtInspDate.Text = InspectionDate.ToString("dd/MMM/yyyy");

                    txtStackNo.Text = ds.Tables[0].Rows[0]["Stack_No"].ToString();
                    txtRejQty.Text = ds.Tables[0].Rows[0]["Rejected_Qty"].ToString();
                    txtIssuedQty.Text = ds.Tables[0].Rows[0]["IssuedQty"].ToString();

                    GetMPIssueCentre();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rejection No. Details Is Not Available'); </script> ");
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

    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
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
        btnRecptSubmit.Enabled = false;
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

    protected void ddlGodam_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;

        if (ddlGodam.SelectedIndex > 0)
        {
            btnRecptSubmit.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
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
        else if (ddlRejectionNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Rejection No.'); </script> ");
            return;
        }
        else if (txtFromDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Deposit Date'); </script> ");
            return;
        }
        else if (txtStackNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Stack Number Is Not Available'); </script> ");
            return;
        }
        else if (txtRejQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rejected Qty Is Not Available'); </script> ");
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
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

                            ConvertServerDate ServerDate = new ConvertServerDate();
                            string ConvertFromDate = ServerDate.getDate_MDY(txtFromDate.Text);

                            //double RejectedQty = double.Parse(txtQty.Text);

                            SubDCDate = "CRD" + SubDCDate;
                            //instr = "Insert Into CMR_Inspection_Rejected(CMR_RejectionNo,CropYear,CMR_RecdDist,Miller_State,Miller_Dist,Mill_ID,IssueCenter,Branch,Godown_id,Inspection_Date,Stack_No,Rejected_Qty,Rem_Qty,Inspected_By,CreatedDate,IP_Address) Values('" + SubDCDate + "','" + ddlCropYear.SelectedItem.ToString() + "','" + districtid + "','" + MillerState + "','" + ddlMacersAddDist.SelectedValue.ToString() + "','" + ddlMacersName.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodam.SelectedValue.ToString() + "','" + ConvertFromDate + "','" + txtStackNo.Text + "','" + RejectedQty + "','" + RejectedQty + "','" + txtInspectedName.Text + "',GETDATE(),'" + GetIp + "')";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = false;
                            btnPrint.Enabled = true;

                            Session["CMRDeposit_ID"] = SubDCDate.ToString();

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rejected CMR Deposit Order Is Created Successfully And Your Deposit Order Number Is " + SubDCDate.ToString() + "'); </script> ");

                            Label2.Visible = true;
                            Label2.Text = "Rejected CMR Deposit Order Is Created Successfully And Your Deposit Order Number Is " + SubDCDate.ToString();
                            txtDist.Text = "";
                            ddlCropYear.Enabled = ddlMillName.Enabled = ddlRejectionNo.Enabled = ddlIC.Enabled = ddlBranch.Enabled = ddlGodam.Enabled = ddlCommodity.Enabled = false;
                            ddlCropYear.Items.Clear();

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PrintRejCMR_Deposit.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }


}