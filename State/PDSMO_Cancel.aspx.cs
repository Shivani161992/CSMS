using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;

public partial class State_PDSMO_Cancel : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    double QtyTotal = 0, ConvertQtlsToMT = 0, QtyTotalSubMO = 0;
    int ro = 0, RowSpan = 0;
    public string  GenerateOTP = "", OTPSMS = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetCropYear();
                GetCommodity();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp Order By district_name");
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

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMODate.Text = txtGunnyType.Text = txtEmpName.Text = "";
        hdfMobileNo.Value = hdfEmpID.Value = hdfOTP.Value = "";
        GridView1.DataSource = "";
        GridView1.DataBind();
        GridView2.DataSource = "";
        GridView2.DataBind();

        btnCancel.Enabled = false;
        btnOTP.Disabled = true;
        rowGunnyType.Visible = false;

        ddlComdtyMode.Items.Clear();
        ddlFrmDist.Items.Clear();
        ddlMvmtNo.Items.Clear();
        ddlMobileNo.Items.Clear();

        ddlCommodity.SelectedIndex = 0;
    }

    public void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Commodity_Id, Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (3,13,22,12,11) order by Commodity_Name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCommodity.DataSource = ds.Tables[0];
                        ddlCommodity.DataTextField = "Commodity_Name";
                        ddlCommodity.DataValueField = "Commodity_Id";
                        ddlCommodity.DataBind();
                        ddlCommodity.Items.Insert(0, "--Select--");
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
        txtMODate.Text = txtGunnyType.Text = txtEmpName.Text = "";
        hdfMobileNo.Value = hdfEmpID.Value = hdfOTP.Value = "";
        GridView1.DataSource = "";
        GridView1.DataBind();
        GridView2.DataSource = "";
        GridView2.DataBind();

        btnCancel.Enabled = false;
        btnOTP.Disabled = true;
        rowGunnyType.Visible = false;

        ddlComdtyMode.Items.Clear();
        ddlFrmDist.Items.Clear();
        ddlMvmtNo.Items.Clear();
        ddlMobileNo.Items.Clear();

        if (ddlCommodity.SelectedIndex > 0)
        {
            GetSource();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    protected void ddlComdtyMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMODate.Text = txtGunnyType.Text = txtEmpName.Text = "";
        hdfMobileNo.Value = hdfEmpID.Value = hdfOTP.Value = "";
        GridView1.DataSource = "";
        GridView1.DataBind();
        GridView2.DataSource = "";
        GridView2.DataBind();

        btnCancel.Enabled = false;
        btnOTP.Disabled = true;
        rowGunnyType.Visible = false;

        ddlFrmDist.Items.Clear();
        ddlMvmtNo.Items.Clear();
        ddlMobileNo.Items.Clear();

        if (ddlComdtyMode.SelectedIndex > 0)
        {
            GetDistName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mode of Dispatch'); </script> ");
            return;
        }
    }

    public void GetSource()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Source_ID , Source_Name from  Source_Arrival_Type where Source_ID in (12,13) order by Source_Name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlComdtyMode.DataSource = ds.Tables[0];
                        ddlComdtyMode.DataTextField = "Source_Name";
                        ddlComdtyMode.DataValueField = "Source_ID";
                        ddlComdtyMode.DataBind();
                        ddlComdtyMode.Items.Insert(0, "--Select--");
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

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMODate.Text = txtGunnyType.Text = txtEmpName.Text = "";
        hdfMobileNo.Value = hdfEmpID.Value = hdfOTP.Value = "";
        GridView1.DataSource = "";
        GridView1.DataBind();
        GridView2.DataSource = "";
        GridView2.DataBind();

        btnCancel.Enabled = false;
        btnOTP.Disabled = true;
        rowGunnyType.Visible = false;

        ddlMvmtNo.Items.Clear();
        ddlMobileNo.Items.Clear();

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetMONumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sending District'); </script> ");
        }
    }

    public void GetMONumber()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("Select distinct MoveOrdernum From StateMovementOrder Where (IsAccepted='Y' and CropYear='" + ddlCropYear.SelectedItem.ToString() + "' and Commodity='" + ddlCommodity.SelectedValue.ToString() + "' and ModeofDispatch='" + ddlComdtyMode.SelectedValue.ToString() + "' and FrmDist='" + ddlFrmDist.SelectedValue.ToString() + "' and IsCancelled IS NULL and ((DATEADD(DAY,300,CreatedDate))>=Getdate()))");
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
        txtMODate.Text = txtGunnyType.Text = txtEmpName.Text = "";
        hdfMobileNo.Value = hdfEmpID.Value = hdfOTP.Value = "";
        GridView1.DataSource = "";
        GridView1.DataBind();
        GridView2.DataSource = "";
        GridView2.DataBind();
        ddlMobileNo.Items.Clear();

        //btnAccept.Enabled = btnReject.Enabled = true;
        btnCancel.Enabled = false;
        btnOTP.Disabled = true;
        rowGunnyType.Visible = false;

        ViewState["hdfEndDate"] = ViewState["hdfFromDist"] = ViewState["hdfFromDistCode"] = ViewState["hdfCropYear"] = ViewState["hdfModeofDispatch"] = ViewState["hdfModeofDist"] = "";

        if (ddlMvmtNo.SelectedIndex > 0)
        {
            GetMODetails();

            if (GridView1.Rows.Count > 0)
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

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DateTime MOCreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtMODate.Text = MOCreatedDate.ToString("dd-MMM-yyyy");

                    string strGunnyType = ds.Tables[0].Rows[0]["GunnyType"].ToString();

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
                        GetSubData();
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
        btnCancel.Enabled = false;
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

            e.Row.Cells[5].Text = "परिवहन की मात्रा (मै० टन)";
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

                select1 = string.Format("Select (SubMO.QtyByDist/10) SubQtyByDist ,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToOtherDist) RecdDist,ToOtherDist,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToDist) RackRecdDist From StateSubMovementOrder SubMO where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "'");

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

            e.Row.Cells[3].Text = "परिवहन की मात्रा (मै० टन)";
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (ddlMobileNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mobile Number For OTP'); </script> ");
            return;
        }
        else
        {
            if (txtMODate.Text != "")
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

                                strSMS1 = "Movement Order Cancelled By Head Office Bhopal with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "'.Please Stop All Transportation Against This Movement Order";
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

                                strSMS1 = "Movement Order Cancelled By Head Office Bhopal with MO Number='" + ddlMvmtNo.SelectedItem.ToString() + "'.Please Stop All Transportation Against This Movement Order";
                            }

                            if (ViewState["hdfModeofDist"].ToString() == "Both" || ViewState["hdfModeofDist"].ToString() == "Other")
                            {

                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                        "Update StateMovementOrder Set IsCancelled='Y', CancelledIP='" + GetIp + "',CancelledDate=GETDATE(),Cancelled_EmpID='" + hdfEmpID.Value + "',Cancelled_MobileNo='" + hdfMobileNo.Value + "',Cancelled_OTP='"+hdfOTP.Value+"' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "';";

                                instr += "Update StateSubMovementOrder Set IsCancelled='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "';";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                            else
                            {
                                instr = "Update StateMovementOrder Set IsCancelled='Y', CancelledIP='" + GetIp + "',CancelledDate=GETDATE(),Cancelled_EmpID='" + hdfEmpID.Value + "',Cancelled_MobileNo='" + hdfMobileNo.Value + "',Cancelled_OTP='" + hdfOTP.Value + "' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "'";
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnCancel.Enabled = ddlMvmtNo.Enabled = ddlCropYear.Enabled = ddlCommodity.Enabled = ddlComdtyMode.Enabled = ddlFrmDist.Enabled = ddlMobileNo.Enabled = false;
                                btnPrint.Enabled = true;
                                Session["MovmtOrderNo"] = ddlMvmtNo.SelectedItem.ToString();

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Movement Order Is Cancelled Successfully'); </script> ");

                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                string strSMS = "";
                                //Code For SMS

                                if (ViewState["hdfModeofDist"].ToString() == "Other")
                                {
                                    strSMS = "'" + ddlMvmtNo.SelectedItem.ToString() + "' Movement Order Is Cancelled By Head Office Bhopal. Movement Order Issued Date On '" + txtMODate.Text + "' For '" + ddlCommodity.SelectedItem.ToString() + "', '" + ddlComdtyMode.SelectedItem.ToString() + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")MT With End Date'" + ViewState["hdfEndDate"].ToString() + "'. Please Stop All Transportation Against This Movement Order";
                                }
                                else if (ViewState["hdfModeofDist"].ToString() == "Both")
                                {
                                    strSMS = "'" + ddlMvmtNo.SelectedItem.ToString() + "' Movement Order Is Cancelled By Head Office Bhopal. Movement Order Issued Date On '" + txtMODate.Text + "' For '" + ddlCommodity.SelectedItem.ToString() + "', '" + ddlComdtyMode.SelectedItem.ToString() + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")MT With End Date'" + ViewState["hdfEndDate"].ToString() + "'. Please Stop All Transportation Against This Movement Order";
                                }
                                else
                                {
                                    strSMS = "'" + ddlMvmtNo.SelectedItem.ToString() + "' Movement Order Is Cancelled By Head Office Bhopal. Movement Order Issued Date On '" + txtMODate.Text + "' For '" + ddlCommodity.SelectedItem.ToString() + "', '" + ddlComdtyMode.SelectedItem.ToString() + "' From '" + ViewState["hdfFromDist"].ToString() + "' To (" + smsToDist + ")MT With End Date'" + ViewState["hdfEndDate"].ToString() + "'. Please Stop All Transportation Against This Movement Order";
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
                                                string GMPDS1 = hdfMobileNo.Value, SharmaSir1 = "9479374277";
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
                                                    Message1.SendSMS(checkLength, strSMS1);
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
                            txtMODate.Text = "";
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
                return;
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ViewState["hdfModeofDispatch"].ToString() == "12")
        {
            string url = "PCancelPDSMO_Road.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            if (ViewState["hdfModeofDist"].ToString() == "Self")
            {
                string url = "PCancelPDSMO_Road.aspx";
                string s = "window.open('" + url + "', 'popup_window');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else
            {
                string url = "PCancelPDSMO_Rack.aspx";
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
            ddlMvmtNo.Enabled = ddlCropYear.Enabled = ddlCommodity.Enabled = ddlComdtyMode.Enabled = ddlFrmDist.Enabled = ddlMobileNo.Enabled = false;

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
        OTPSMS = "'" + ddlCommodity.SelectedItem.ToString() + "' Movement Order Number " + ddlMvmtNo.SelectedItem.ToString() + " Cancelled OTP Is '" + otp + "'";
        hdfOTP.Value = "";
        hdfOTP.Value = otp;

        SMS Message = new SMS();

        string MobileNo = "";

        MobileNo = hdfMobileNo.Value;
        Message.SendSMS(MobileNo, OTPSMS);
    }

}