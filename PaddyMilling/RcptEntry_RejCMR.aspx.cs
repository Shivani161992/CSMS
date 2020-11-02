using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_RcptEntry_RejCMR : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string districtid, IC_Id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["RejCMRRecd_ID"] = null;

                rdbNewJute.Checked = rdbTagYes.Checked = true;

                txtTruckNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTruckNo0.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo0.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo0.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRecdQty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRecdQty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRecdQty.Attributes.Add("onchange", "return chksqltxt(this)");

                txtBags.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtBags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtBags.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTagNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTagNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTagNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtDate.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtDate.Attributes.Add("onchange", "return chksqltxt(this)");

                txtToulReceiptNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtToulReceiptNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtToulReceiptNo.Attributes.Add("onchange", "return chksqltxt(this)");

                ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
                ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlCropYear.Items.Insert(0, "--Select--");

                txtIC.Text = Session["issue_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCommodity();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtDate.Text = Request.Form[txtDate.UniqueID];
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
        btnAccept.Enabled = false;
        txtIssuedQty.Text = txtRejQty.Text = txtStackNo.Text = txtGodown.Text = txtRecdQty.Text = txtBags.Text = txtTagNo.Text = txtTruckNo.Text = txtTruckNo0.Text = txtDate.Text = txtToulReceiptNo.Text = txtInspectionDate.Text = txtRecdTotalCMRQty.Text = "";
        ddlMillName.Items.Clear();
        ddlRejectionNo.Items.Clear();

        GetMillName();
    }

    private void GetMillName()
    {
        districtid = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select CMR.Mill_ID,MR.Mill_Name From CMR_Inspection_Rejected As CMR Left Join Miller_Registration_2017 As MR ON(CMR.Miller_State=MR.State_Code and CMR.Miller_Dist=MR.District_Code and CMR.Mill_ID=MR.Registration_ID) Where CMR.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and CMR.CMR_RecdDist='" + districtid + "' and CMR.IssueCenter='" + IC_Id + "'";
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Name Is Not Available On This Issue Centre'); </script> ");
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
        hdfGodown.Value = hdfBranch.Value = "";
        btnAccept.Enabled = false;
        txtIssuedQty.Text = txtRejQty.Text = txtStackNo.Text = txtGodown.Text = txtRecdQty.Text = txtBags.Text = txtTagNo.Text = txtTruckNo.Text = txtTruckNo0.Text = txtDate.Text = txtToulReceiptNo.Text = txtInspectionDate.Text = txtRecdTotalCMRQty.Text =  "";
        ddlRejectionNo.Items.Clear();

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
        districtid = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select CMR_RejectionNo From CMR_Inspection_Rejected Where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and CMR_RecdDist='" + districtid + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and IssueCenter='" + IC_Id + "' ";
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

    protected void ddlRejectionNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAccept.Enabled = false;
        hdfGodown.Value = hdfBranch.Value = "";
        txtIssuedQty.Text = txtRejQty.Text = txtStackNo.Text = txtGodown.Text = txtRecdQty.Text = txtBags.Text = txtTagNo.Text = txtTruckNo.Text = txtTruckNo0.Text = txtDate.Text = txtToulReceiptNo.Text = txtInspectionDate.Text = txtRecdTotalCMRQty.Text = "";

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
        hdfGodown.Value = "";

        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select MAX(CMR.Stack_No) As Stack_No,MAX(CMR.Rejected_Qty) As Rejected_Qty,MAX(CMR.Rejected_Qty-CMR.Rem_Qty) As IssuedQty,MAX(CMR.Godown_id) As Godown_id,MAX(CMR.Branch) As Branch ,MAX(CMR.Inspection_Date) As Inspection_Date ,SUM(ISNULL(Reject.Recd_Qty,0)) As RecdQty From CMR_Inspection_Rejected As CMR Left Join Receipt_RejCMR As Reject ON(CMR.CMR_RejectionNo=Reject.CMR_RejectionNo and CMR.CMR_RecdDist = Reject.CMR_RecdDist) Where CMR.CMR_RejectionNo='" + ddlRejectionNo.SelectedValue.ToString() + "' and CMR.IssueCenter='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtStackNo.Text = ds.Tables[0].Rows[0]["Stack_No"].ToString();
                    txtRejQty.Text = ds.Tables[0].Rows[0]["Rejected_Qty"].ToString();
                    txtIssuedQty.Text = ds.Tables[0].Rows[0]["IssuedQty"].ToString();
                    hdfGodown.Value = ds.Tables[0].Rows[0]["Godown_id"].ToString();
                    hdfBranch.Value = ds.Tables[0].Rows[0]["Branch"].ToString();
                    txtRecdTotalCMRQty.Text = ds.Tables[0].Rows[0]["RecdQty"].ToString();

                    DateTime RejDate = DateTime.Parse(ds.Tables[0].Rows[0]["Inspection_Date"].ToString());
                    txtInspectionDate.Text = RejDate.ToString("dd/MMM/yyyy");

                    GetGodownName();

                    btnAccept.Enabled = true;
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

    public void GetGodownName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + hdfGodown.Value + "'";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        decimal CheckRecdQty = decimal.Parse(txtRecdQty.Text);
        int CheckBag = int.Parse(txtBags.Text);

        if (ddlRejectionNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Rejection No.'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Recd. CMR Date'); </script> ");
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
        else if (txtRecdQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Recd. Qty'); </script> ");
            return;
        }
        else if (txtBags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Recd. Bags'); </script> ");
            return;
        }
        else if (txtToulReceiptNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Toul Parchi No.'); </script> ");
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
                string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                int greaterdate = DateTime.Compare(currentdate, Recdate);

                if (greaterdate == -1)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not greater Than Today Date'); </script> ");
                    return;
                }


                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        string Dist_Id = Session["dist_id"].ToString();
                        IC_Id = Session["issue_id"].ToString();
                        string opid = Session["OperatorId"].ToString();

                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        ConvertServerDate ServerDate = new ConvertServerDate();
                        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

                        string tags = "", BagType = "", gatepass = "";

                        if (rdbTagYes.Checked)
                        {
                            tags = "Y";
                        }
                        else
                        {
                            tags = "N";
                        }

                        if (rdbNewJute.Checked)
                        {
                            BagType = "9";
                        }
                        else if (rdbOldJute.Checked)
                        {
                            BagType = "10";
                        }
                        else if (rdbOnceJute.Checked)
                        {
                            BagType = "11";
                        }
                        else if (rdbNewPP.Checked)
                        {
                            BagType = "4";
                        }
                        else if (rdbOncePP.Checked)
                        {
                            BagType = "2";
                        }
                        else
                        {
                            BagType = "12";
                        }

                        con.Open();

                        int month = int.Parse(DateTime.Today.Date.Month.ToString());
                        int year = int.Parse(DateTime.Today.Year.ToString());
                        long getnum = 0;

                        string qrey = "";
                        qrey = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + IC_Id + "' and Dist_Id='" + Dist_Id + "'";
                        da = new SqlDataAdapter(qrey, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            gatepass = ds.Tables[0].Rows[0]["Receipt_id"].ToString();

                            if (gatepass == "")
                            {
                                string issue = IC_Id.Substring(2, 5);
                                gatepass = issue + month.ToString() + "001";

                            }
                            else
                            {
                                getnum = Convert.ToInt64(gatepass);
                                getnum = getnum + 1;
                                gatepass = getnum.ToString();
                            }
                        }

                        string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                        da = new SqlDataAdapter(selectmax, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        string DCDate = "", SubDCDate = "", RejNo = "", instr = "";

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                            SubDCDate = DCDate.Substring(2);
                        }

                        decimal RecdCMR = decimal.Parse(txtRecdQty.Text);

                        if (SubDCDate != "" && gatepass != "")
                        {
                            RejNo = "CRR" + SubDCDate;
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                        "Insert Into Receipt_RejCMR(CMR_AcptNo,CMR_RejectionNo,CropYear,CMR_RecdDist,Mill_ID,IssueCenter,Branch,Godown_id,Recd_Date,Recd_Qty,Bags,BagType,Tags,TagNo,TruckNo,TruckNo1,ToulReceiptNo,Commodity_Id,CreatedDate,IP_Address,OperatorID) Values ('" + RejNo + "','" + ddlRejectionNo.SelectedItem.ToString() + "','" + ddlCropYear.SelectedValue.ToString() + "','" + Dist_Id + "','" + ddlMillName.SelectedValue.ToString() + "','" + IC_Id + "','" + hdfBranch.Value + "','" + hdfGodown.Value + "','" + IssuedDate + "','" + RecdCMR + "','" + txtBags.Text + "','" + BagType + "','" + tags + "','" + txtTagNo.Text + "','" + txtTruckNo.Text + "','" + txtTruckNo0.Text + "','" + txtToulReceiptNo.Text + "','" + ddlCommodity.SelectedValue.ToString() + "',GETDATE(),'" + GetIp + "','" + opid + "');";

                            instr += "insert into dbo.tbl_Receipt_Details(State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch) values('23','" + Dist_Id + "','" + IC_Id + "','" + gatepass + "','17','" + ddlMillName.SelectedValue.ToString() + "','" + Dist_Id + "','','','','" + IssuedDate + "','" + IssuedDate + "','" + RejNo + "','" + IssuedDate + "','" + RecdCMR + "','" + ddlCommodity.SelectedValue.ToString() + "','0','" + ddlCropYear.SelectedValue.ToString() + "','1','','" + txtTruckNo.Text + "','','" + BagType + "'," + txtBags.Text + "," + RecdCMR + "," + txtBags.Text + ",'0','',''," + month + "," + year + ",'N','" + GetIp + "',getdate(),'','N','" + hdfGodown.Value + "','" + opid + "','N','','" + hdfBranch.Value + "') ; ";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            Session["RejCMRRecd_ID"] = RejNo.ToString();

                            btnAccept.Enabled = false;
                            btnPrint.Enabled = true;

                            Label2.Visible = true;
                            Label2.Text = "CMR Is Successfully Accepted And Your Acceptance Number Is " + RejNo.ToString();

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Is Successfully Accepted And Your Acceptance Number Is " + RejNo + "'); </script> ");
                            txtIC.Text = "";
                            ddlCropYear.Enabled = ddlMillName.Enabled = ddlRejectionNo.Enabled = ddlCommodity.Enabled = false;
                            txtRecdQty.Enabled = txtBags.Enabled = txtTagNo.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = txtToulReceiptNo.Enabled = false;
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
        string url = "Print/PrintRcptEntry_RejCMR.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/PaddyMillingHome.aspx");
    }

    protected void rdbTagYes_CheckedChanged(object sender, EventArgs e)
    {
        txtTagNo.Text = "";
        txtTagNo.Enabled = true;
    }
    protected void rdbTagNo_CheckedChanged(object sender, EventArgs e)
    {
        txtTagNo.Text = "";
        txtTagNo.Enabled = false;
    }


}