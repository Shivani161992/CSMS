using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;

public partial class State_PDSMovementOrder_Acpt : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    double QtyTotal = 0, ConvertQtlsToMT = 0, QtyTotalSubMO = 0;
    int ro = 0, RowSpan = 0;
    public string GenerateOTP = "", OTPSMS = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Session["Acpt/Rjct"] = "";
                GetMONumber();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMONumber()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("Select distinct MoveOrdernum From StateMovementOrder Where (IsAccepted='F' and ((DATEADD(DAY,2,CreatedDate))>=Getdate()))");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMvmtNo.DataSource = ds.Tables[0];
                        ddlMvmtNo.DataTextField = "MoveOrdernum";
                        ddlMvmtNo.DataValueField = "MoveOrdernum";
                        ddlMvmtNo.DataBind();
                        ddlMvmtNo.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Movement Order Number Is Not Available'); </script> ");
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

    protected void ddlMvmtNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMODate.Text = txtCommodity.Text = txtDispMode.Text = txtEmpName.Text = txtOTP.Text = "";
        ddlMobileNo.Items.Clear();
        hdfMobileNo.Value = hdfEmpID.Value = hdfOTP.Value = hdfCheckSubMO.Value = "";
        //btnAccept.Enabled = btnReject.Enabled = true;
        btnOTP.Disabled = true;
        btnAccept.Enabled = btnReject.Enabled = false;
        rowGunnyType.Visible = false;

        ViewState["hdfEndDate"] = ViewState["hdfFromDist"] = ViewState["hdfFromDistCode"] = ViewState["hdfCropYear"] = ViewState["hdfModeofDispatch"] = ViewState["hdfModeofDist"] = "";

        if (ddlMvmtNo.SelectedIndex > 0)
        {
            GetMODetails();

            if (GridView1.Rows.Count > 0 && hdfCheckSubMO.Value == "")
            {
                GetMobileNo();
            }
        }

        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
            GridView2.DataSource = "";
            GridView2.DataBind();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select MO Number'); </script> ");
            // btnAccept.Enabled = btnReject.Enabled = false;
        }
    }

    public void GetMODetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                select = "Select Abc.ComdtyName,ABC.Commodity,ABC.CreatedDate,ABC.CropYear,ABC.DispatchModeName,ABC.FrmDist,ABC.FromDistName,ABC.GunnyType,ABC.ModeofDispatch,ABC.ModeofDist,ABC.ReachDate,ABC.ReceiveDistName,ABC.SMS_frmDist,ABC.ToDist, (Case when ABC.Commodity='25' then (ABC.PDSMOQty) else (ABC.PDSMOQty/10) End) As PDSMOQty From (Select ModeofDist,SMS_frmDist,(PDSMO.Quantity) As PDSMOQty,(SELECT district_name FROM pds.districtsmp where district_code=PDSMO.ToDist) ReceiveDistName,ToDist,CropYear,(SELECT district_name FROM pds.districtsmp where district_code=PDSMO.FrmDist) FromDistName,FrmDist,ReachDate,(select Source_Name from  Source_Arrival_Type where Source_ID=PDSMO.ModeofDispatch) DispatchModeName,ModeofDispatch,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=PDSMO.Commodity) ComdtyName,Commodity,CreatedDate,GunnyType From StateMovementOrder PDSMO where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "') As ABC";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DateTime MOCreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        txtMODate.Text = MOCreatedDate.ToString("dd-MMM-yyyy");

                        txtCommodity.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();

                        string strGunnyType = ds.Tables[0].Rows[0]["GunnyType"].ToString();

                        if (txtCommodity.Text == "Gunny")
                        {
                            rowGunnyType.Visible = true;
                            if (strGunnyType == "JUTE")
                            {
                                txtGunnyType.Text = "Jute(SBT)";
                            }
                            else
                            {
                                txtGunnyType.Text = "PP";
                            }
                        }

                        txtDispMode.Text = ds.Tables[0].Rows[0]["DispatchModeName"].ToString();

                        DateTime MOEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReachDate"].ToString());
                        ViewState["hdfEndDate"] = MOEndDate.ToString("dd-MMM-yyyy");
                        ViewState["hdfFromDist"] = ds.Tables[0].Rows[0]["FromDistName"].ToString();
                        ViewState["hdfFromDistCode"] = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                        ViewState["hdfCropYear"] = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        ViewState["hdfModeofDispatch"] = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();
                        ViewState["hdfModeofDist"] = ds.Tables[0].Rows[0]["ModeofDist"].ToString();

                        string SMS_frmDist = ds.Tables[0].Rows[0]["SMS_frmDist"].ToString();

                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();

                        string ModeofDist = ds.Tables[0].Rows[0]["ModeofDist"].ToString();

                        if (ModeofDist == "Other" || ModeofDist == "Both")
                        {
                            if (SMS_frmDist == "Y")
                            {
                                hdfCheckSubMO.Value = "";
                                GetSubData();
                            }
                            else
                            {
                                hdfCheckSubMO.Value = "1";
                                GridView2.DataSource = "";
                                GridView2.DataBind();

                                btnAccept.Enabled = btnReject.Enabled = false;
                                btnOTP.Disabled = true;
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Create Your Sub Movement Order, After That You Will Accept/Reject Movement Order'); </script> ");
                            }

                        }
                        else
                        {
                            GridView2.DataSource = "";
                            GridView2.DataBind();
                        }
                    }
                    else
                    {
                        GridView1.DataSource = "";
                        GridView1.DataBind();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Not Available'); </script> ");
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

    private void GetMobileNo()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select Mobile_No From PDSMO_EMPContactDetails";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMobileNo.DataSource = ds.Tables[0];
                    ddlMobileNo.DataTextField = "Mobile_No";
                    ddlMobileNo.DataValueField = "Mobile_No";
                    ddlMobileNo.DataBind();
                    ddlMobileNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Mobile Number Is Not Available. Please Fill The Master Data of PDS Movement Department.'); </script> ");
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

    protected void ddlMobileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAccept.Enabled = btnReject.Enabled = false;
        btnOTP.Disabled = true;

        txtEmpName.Text = "";
        hdfMobileNo.Value = hdfEmpID.Value = hdfOTP.Value = "";

        if (ddlMobileNo.SelectedIndex > 0)
        {
            GetEmpName();
        }
    }

    private void GetEmpName()
    {
        hdfMobileNo.Value = hdfEmpID.Value = "";
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select Name,Employee_ID,Mobile_No From PDSMO_EMPContactDetails where Mobile_No='" + ddlMobileNo.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtEmpName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    hdfMobileNo.Value = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                    hdfEmpID.Value = ds.Tables[0].Rows[0]["Employee_ID"].ToString();

                    btnOTP.Disabled = false;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Mobile Number Is Not Available. Please Fill The Master Data of PDS Movement Department.'); </script> ");
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;

            if (txtCommodity.Text == "Gunny")
            {
                e.Row.Cells[5].Text = "परिवहन की मात्रा (Bales)";
            }
            else
            {
                e.Row.Cells[5].Text = "परिवहन की मात्रा (मै० टन)";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            e.Row.Cells[4].Text = ViewState["hdfCropYear"].ToString();
            e.Row.Cells[1].Text = ViewState["hdfFromDist"].ToString();
            e.Row.Cells[3].Text = ViewState["hdfEndDate"].ToString();

            QtyTotal += ((double.Parse(e.Row.Cells[5].Text)));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "कुल मात्रा";
            e.Row.Cells[5].Text = QtyTotal.ToString("0.00");

            RowSpan = 1;
            ro = 0;
            int j = 0;
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                GridViewRow currrow = GridView1.Rows[j];
                GridViewRow nextrow = GridView1.Rows[i + 1];
                if (currrow.Cells[1].Text == nextrow.Cells[1].Text && currrow.Cells[4].Text == nextrow.Cells[4].Text && currrow.Cells[3].Text == nextrow.Cells[3].Text)
                {
                    nextrow.Cells[1].Visible = false;
                    nextrow.Cells[4].Visible = false;
                    nextrow.Cells[3].Visible = false;
                    RowSpan += 1;
                    ro++;
                }
                else
                {
                    currrow.Cells[1].RowSpan = RowSpan;
                    currrow.Cells[4].RowSpan = RowSpan;
                    currrow.Cells[3].RowSpan = RowSpan;
                    RowSpan = 1;
                    j = i + 1;
                }
            }

            GridViewRow currrow1 = GridView1.Rows[j];
            currrow1.Cells[1].RowSpan = RowSpan;
            currrow1.Cells[4].RowSpan = RowSpan;
            currrow1.Cells[3].RowSpan = RowSpan;

        }
    }

    public void GetSubData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select1 = "";

                if (txtCommodity.Text == "Gunny")
                {
                    select1 = string.Format("Select (SubMO.QtyByDist) SubQtyByDist ,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToOtherDist) RecdDist,ToOtherDist,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToDist) RackRecdDist From StateSubMovementOrder SubMO where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "'");
                }
                else
                {
                    select1 = string.Format("Select (SubMO.QtyByDist/10) SubQtyByDist ,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToOtherDist) RecdDist,ToOtherDist,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToDist) RackRecdDist From StateSubMovementOrder SubMO where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "'");
                }

                da = new SqlDataAdapter(select1, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = "";
                    GridView2.DataBind();
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;

            if (txtCommodity.Text == "Gunny")
            {
                e.Row.Cells[3].Text = "परिवहन की मात्रा (Bales)";
            }
            else
            {
                e.Row.Cells[3].Text = "परिवहन की मात्रा (मै० टन)";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            ConvertQtlsToMT = 0;
            ConvertQtlsToMT = ((double.Parse(e.Row.Cells[3].Text)));
            QtyTotalSubMO += ConvertQtlsToMT;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "कुल मात्रा";
            e.Row.Cells[3].Text = QtyTotalSubMO.ToString("0.00");

            RowSpan = 1;
            ro = 0;
            int j = 0;
            for (int i = 0; i < GridView2.Rows.Count - 1; i++)
            {
                GridViewRow currrow = GridView2.Rows[j];
                GridViewRow nextrow = GridView2.Rows[i + 1];
                if (currrow.Cells[1].Text == nextrow.Cells[1].Text)
                {
                    nextrow.Cells[1].Visible = false;
                    RowSpan += 1;
                    ro++;
                }
                else
                {
                    currrow.Cells[1].RowSpan = RowSpan;
                    RowSpan = 1;
                    j = i + 1;
                }
            }

            GridViewRow currrow1 = GridView2.Rows[j];
            currrow1.Cells[1].RowSpan = RowSpan;
        }
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/MovementOrderHome.aspx");
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        if (ddlMobileNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mobile Number For OTP'); </script> ");
            return;
        }
        else
        {
            if (txtDispMode.Text != "" && txtCommodity.Text != "")
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string instr = "", smsToDist = "", smsToDistCode = "", smsToDist1 = "", smsToDistCode1 = "", strSMS1 = "", SMSToRackDist1 = "";

                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                smsToDist += ((smsToDist == "") ? "" : " , ") + "'" + GridView1.Rows[i].Cells[2].Text + "' = '" + GridView1.Rows[i].Cells[5].Text + "'";
                                smsToDistCode += ((smsToDistCode == "") ? "" : ",") + "'" + GridView1.Rows[i].Cells[6].Text + "'";
                            }

                            if (ViewState["hdfModeofDist"].ToString() == "Other")
                            {
                                for (int i = 0; i < GridView2.Rows.Count; i++)
                                {
                                    smsToDist1 += ((smsToDist1 == "") ? "" : ",") + "'" + GridView2.Rows[i].Cells[2].Text + "(" + GridView2.Rows[i].Cells[3].Text + ")'";
                                    smsToDistCode1 += ((smsToDistCode1 == "") ? "" : ",") + "'" + GridView2.Rows[i].Cells[4].Text + "'";
                                    SMSToRackDist1 += ((SMSToRackDist1 == "") ? "" : ",") + "'" + GridView2.Rows[i].Cells[1].Text + " To " + GridView2.Rows[i].Cells[2].Text + "-" + GridView2.Rows[i].Cells[3].Text + "'";
                                }

                                if (txtCommodity.Text == "Gunny")
                                {
                                    strSMS1 = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "'.Please Create TO By[" + SMSToRackDist1 + "]Bales. Please Create Movement Plan By[" + smsToDist1 + "]Bales";
                                }
                                else
                                {
                                    strSMS1 = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "'.Please Create TO By[" + SMSToRackDist1 + "]MT. Please Create Movement Plan By[" + smsToDist1 + "]MT";
                                }
                            }

                            if (ViewState["hdfModeofDist"].ToString() == "Both")
                            {
                                for (int i = 0; i < GridView2.Rows.Count; i++)
                                {
                                    if (GridView2.Rows[i].Cells[1].Text != GridView2.Rows[i].Cells[2].Text)
                                    {
                                        smsToDist1 += ((smsToDist1 == "") ? "" : ",") + "'" + GridView2.Rows[i].Cells[2].Text + "(" + GridView2.Rows[i].Cells[3].Text + ")'";
                                        smsToDistCode1 += ((smsToDistCode1 == "") ? "" : ",") + "'" + GridView2.Rows[i].Cells[4].Text + "'";
                                    }
                                    SMSToRackDist1 += ((SMSToRackDist1 == "") ? "" : ",") + "'" + GridView2.Rows[i].Cells[1].Text + " To " + GridView2.Rows[i].Cells[2].Text + "-" + GridView2.Rows[i].Cells[3].Text + "'";
                                }

                                if (txtCommodity.Text == "Gunny")
                                {
                                    strSMS1 = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "'.Please Create TO By[" + SMSToRackDist1 + "]Bales. Please Create Movement Plan By[" + smsToDist1 + "]Bales";
                                }
                                else
                                {
                                    strSMS1 = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "'.Please Create TO By[" + SMSToRackDist1 + "]MT. Please Create Movement Plan By[" + smsToDist1 + "]MT";
                                }
                            }

                            if (ViewState["hdfModeofDist"].ToString() == "Both" || ViewState["hdfModeofDist"].ToString() == "Other")
                            {

                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                        "Update StateMovementOrder Set IsAccepted='Y', AcceptedIP='" + GetIp + "',AcceptedDate=GETDATE(),Accepted_EmpID='" + hdfEmpID.Value + "',Accepted_MobileNo='" + hdfMobileNo.Value + "',Accepted_OTP='" + hdfOTP.Value + "' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "';";

                                instr += "Update StateSubMovementOrder Set IsAccepted='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "';";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                            else
                            {
                                instr = "Update StateMovementOrder Set IsAccepted='Y', AcceptedIP='" + GetIp + "',AcceptedDate=GETDATE(),Accepted_EmpID='" + hdfEmpID.Value + "',Accepted_MobileNo='" + hdfMobileNo.Value + "',Accepted_OTP='" + hdfOTP.Value + "' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "'";
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnAccept.Enabled = btnReject.Enabled = ddlMvmtNo.Enabled = btnPrint.Enabled = false;
                                ChkOTP.Disabled = true;
                                Session["MovmtOrderNo"] = ddlMvmtNo.SelectedItem.ToString();

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Movement Order Is Accepted'); </script> ");

                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                string strSMS = "";
                                //Code For SMS

                                if (txtCommodity.Text == "Gunny")
                                {
                                    if (ViewState["hdfModeofDist"].ToString() == "Other")
                                    {
                                        strSMS = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "' For '" + txtCommodity.Text + "', '" + txtGunnyType.Text + "', '" + txtDispMode.Text + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")Bales With End Date'" + ViewState["hdfEndDate"].ToString() + "'. Please Send All Commodity To Other District.";
                                    }
                                    else if (ViewState["hdfModeofDist"].ToString() == "Both")
                                    {
                                        strSMS = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "' For '" + txtCommodity.Text + "', '" + txtGunnyType.Text + "', '" + txtDispMode.Text + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")Bales With End Date'" + ViewState["hdfEndDate"].ToString() + "'. Please Send Commodity To Other District & Own District";
                                    }
                                    else
                                    {
                                        strSMS = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "' For '" + txtCommodity.Text + "', '" + txtGunnyType.Text + "', '" + txtDispMode.Text + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")Bales With End Date'" + ViewState["hdfEndDate"].ToString() + "'";
                                    }
                                }
                                else
                                {
                                    if (ViewState["hdfModeofDist"].ToString() == "Other")
                                    {
                                        strSMS = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "' For '" + txtCommodity.Text + "', '" + txtDispMode.Text + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")MT With End Date'" + ViewState["hdfEndDate"].ToString() + "'. Please Send All Commodity To Other District.";
                                    }
                                    else if (ViewState["hdfModeofDist"].ToString() == "Both")
                                    {
                                        strSMS = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "' For '" + txtCommodity.Text + "', '" + txtDispMode.Text + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")MT With End Date'" + ViewState["hdfEndDate"].ToString() + "'. Please Send Commodity To Other District & Own District";
                                    }
                                    else
                                    {
                                        strSMS = "Movement Order Issued On '" + txtMODate.Text + "' with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "' For '" + txtCommodity.Text + "', '" + txtDispMode.Text + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")MT With End Date'" + ViewState["hdfEndDate"].ToString() + "'";
                                    }
                                }

                                SMS Message = new SMS();

                                smsToDistCode += ",'" + ViewState["hdfFromDistCode"].ToString() + "'";
                                string FindDistContactNo = "select DM_Mobile,RM_Mobile,District_code From officers_list where District_code in (" + smsToDistCode + ")";
                                da = new SqlDataAdapter(FindDistContactNo, con);
                                ds = new DataSet();
                                da.Fill(ds);
                                string CheckDuplicate = "", GMPDS = hdfMobileNo.Value, SharmaSir = "9479374277";

                                if (ds != null)
                                {
                                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            string UpdateSMS = "";
                                            string checkLength = ds.Tables[0].Rows[i]["DM_Mobile"].ToString();
                                            string checkLengthRM = ds.Tables[0].Rows[i]["RM_Mobile"].ToString();
                                            string DistCode = ds.Tables[0].Rows[i]["District_code"].ToString();

                                            if (i == 0)
                                            {
                                                Message.SendSMS(GMPDS, strSMS);
                                                Thread.Sleep(1000);
                                                Message.SendSMS(SharmaSir, strSMS);
                                            }
                                            if (checkLength.Length == 10)
                                            {
                                                Message.SendSMS(checkLength, strSMS);
                                                if (ViewState["hdfFromDistCode"].ToString() == DistCode)
                                                {
                                                    UpdateSMS = "Update StateMovementOrder Set FrmDist_SMS='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and FrmDist='" + ViewState["hdfFromDistCode"].ToString() + "';";
                                                }
                                                else
                                                {
                                                    UpdateSMS += "Update StateMovementOrder Set ToDist_SMS='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and ToDist='" + DistCode + "' and FrmDist='" + ViewState["hdfFromDistCode"].ToString() + "';";
                                                }

                                                cmd = new SqlCommand(UpdateSMS, con);
                                                cmd.ExecuteNonQuery();

                                                //Thread.Sleep(1000);
                                            }
                                            if (checkLengthRM.Length == 10)
                                            {
                                                if (checkLength != checkLengthRM)
                                                {
                                                    if (checkLengthRM != CheckDuplicate)
                                                    {
                                                        Message.SendSMS(checkLengthRM, strSMS);
                                                        CheckDuplicate = checkLengthRM;
                                                        Thread.Sleep(1000);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (ViewState["hdfModeofDist"].ToString() == "Other" || ViewState["hdfModeofDist"].ToString() == "Both")
                                {
                                    SMS Message1 = new SMS();
                                    if (ds != null)
                                    {
                                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                            {
                                                string UpdateSMS = "", GMPDS1 = hdfMobileNo.Value, SharmaSir1 = "9479374277";
                                                string checkLength = ds.Tables[0].Rows[i]["DM_Mobile"].ToString();
                                                string DistCode = ds.Tables[0].Rows[i]["District_code"].ToString();

                                                if (i == 0)
                                                {
                                                    Message1.SendSMS(GMPDS1, strSMS1);
                                                    Thread.Sleep(1000);
                                                    Message1.SendSMS(SharmaSir1, strSMS1);
                                                }
                                                if (DistCode != ViewState["hdfFromDistCode"].ToString())
                                                {
                                                    if (checkLength.Length == 10)
                                                    {
                                                        Message1.SendSMS(checkLength, strSMS1);

                                                        UpdateSMS = "Update StateSubMovementOrder Set FrmDist_SMS='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and FrmDist='" + ViewState["hdfFromDistCode"].ToString() + "' and ToDist='" + DistCode + "';";
                                                        if (ViewState["hdfModeofDist"].ToString() == "Both")
                                                        {
                                                            UpdateSMS += "Update StateSubMovementOrder Set ToDist_SMS='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and FrmDist='" + ViewState["hdfFromDistCode"].ToString() + "' and ToDist='" + DistCode + "' and ToOtherDist='" + DistCode + "';";
                                                        }
                                                        cmd = new SqlCommand(UpdateSMS, con);
                                                        cmd.ExecuteNonQuery();

                                                        //Thread.Sleep(1000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    string FindDistContactNo1 = "select DM_Mobile,District_code From officers_list where District_code in (" + smsToDistCode1 + ")";
                                    da1 = new SqlDataAdapter(FindDistContactNo1, con);
                                    ds1 = new DataSet();
                                    da1.Fill(ds1);

                                    if (ds1 != null)
                                    {
                                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                                            {
                                                string checkLength = ds1.Tables[0].Rows[i]["DM_Mobile"].ToString();
                                                string DistCode = ds1.Tables[0].Rows[i]["District_code"].ToString();

                                                if (checkLength.Length == 10)
                                                {
                                                    string UpdateSMS = "";
                                                    Message1.SendSMS(checkLength, strSMS1);

                                                    UpdateSMS += "Update StateSubMovementOrder Set ToDist_SMS='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and FrmDist='" + ViewState["hdfFromDistCode"].ToString() + "' and ToOtherDist='" + DistCode + "';";
                                                    cmd = new SqlCommand(UpdateSMS, con);
                                                    cmd.ExecuteNonQuery();
                                                    Thread.Sleep(1000);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
                            txtDispMode.Text = txtCommodity.Text = "";
                        }
                    }
                }
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Commodity & Mode of Dispatch'); </script> ");
            }
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (ddlMobileNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mobile Number For OTP'); </script> ");
            return;
        }
        else
        {
            if (txtDispMode.Text != "" && txtCommodity.Text != "")
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string instr = "";

                            instr = "Update StateMovementOrder Set IsAccepted='N', AcceptedIP='" + GetIp + "',AcceptedDate=GETDATE(),Accepted_EmpID='" + hdfEmpID.Value + "',Accepted_MobileNo='" + hdfMobileNo.Value + "',Accepted_OTP='" + hdfOTP.Value + "' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "'";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnAccept.Enabled = btnReject.Enabled = ddlMvmtNo.Enabled = false;
                                ChkOTP.Disabled = true;
                                btnPrint.Enabled = true;
                                Session["MovmtOrderNo"] = ddlMvmtNo.SelectedItem.ToString();

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Movement Order Is Rejected'); </script> ");
                                txtDispMode.Text = txtCommodity.Text = "";
                                Session["Acpt/Rjct"] = "Reject";
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
                            txtDispMode.Text = txtCommodity.Text = "";
                        }
                    }
                }
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["Acpt/Rjct"] = "Reject".ToString();

        if (ViewState["hdfModeofDispatch"].ToString() == "12")
        {
            string url = "Print_MovementOrder.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            if (ViewState["hdfModeofDist"].ToString() == "Self")
            {
                string url = "Print_MovementOrder.aspx";
                string s = "window.open('" + url + "', 'popup_window');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else
            {
                string url = "Print_SubMovementOrder.aspx";
                string s = "window.open('" + url + "', 'popup_window');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
    }

    protected void btnOTP_Click(object sender, EventArgs e)
    {
        hdfOTP.Value = "";
        if (ddlMobileNo.SelectedIndex > 0)
        {
            // Call Jquery Function TimerFunc()
            txtOTP.Text = "";
            ddlMvmtNo.Enabled = ddlMobileNo.Enabled = false;

            GenerateUniqueOTP();

            btnOTP.Disabled = true;
            txtOTP.Enabled = true;
            ChkOTP.Disabled = false;
            txtOTP.Focus();
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:TimerFunc(); ", true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mobile Number For OTP'); </script> ");
            return;
        }

    }

    protected void GenerateUniqueOTP()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        string characters = numbers;

        characters += alphabets + small_alphabets + numbers;

        string MONumber = ddlMvmtNo.SelectedItem.ToString();
        int lastdigit = int.Parse(MONumber.Substring(9));
        int length = 0;
        if (lastdigit >= 6)
        {
            length = 8;
        }
        else if (lastdigit >= 3 && lastdigit <= 5)
        {
            length = 6;
        }
        else
        {
            length = 5;
        }

        //int length = int.Parse(ddlMvmtNo.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }

        GenerateOTP = otp;
        OTPSMS = "'" + txtCommodity.Text + "' Movement Order Number " + ddlMvmtNo.SelectedItem.ToString() + " OTP Is '" + otp + "'";
        hdfOTP.Value = "";
        hdfOTP.Value = otp;

        SMS Message = new SMS();

        string MobileNo = "";

        MobileNo = hdfMobileNo.Value;
        Message.SendSMS(MobileNo, OTPSMS);
    }
}