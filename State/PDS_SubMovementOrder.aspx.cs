using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class State_PDS_SubMovementOrder : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    double QtyTotal = 0;
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                Session["GridFill"] = null;
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetMONumber();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
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
                        ddlToSendDist.DataSource = ds.Tables[0];
                        ddlToSendDist.DataTextField = "district_name";
                        ddlToSendDist.DataValueField = "district_code";
                        ddlToSendDist.DataBind();
                        ddlToSendDist.Items.Insert(0, "--Select--");
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

    public void GetMONumber()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("Select distinct MoveOrdernum From StateMovementOrder Where ModeofDispatch='13' and (SMS_frmDist='N' and ((DATEADD(DAY,2,CreatedDate))>=Getdate())) and ModeofDist In('Other','Both') order by MoveOrdernum");
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
        lblMT.Visible = lblMT1.Visible = lblMT2.Visible = false;
        ddlToDist.Items.Clear();
        ddlToSendDist.Items.Clear();

        txtMODate.Text = txtDispDist.Text = txtCropYear.Text = txtComdty.Text = txtFrmDist.Text = txtToDist.Text = txtTotalQty.Text = txtRemQty.Text = txtAddQty.Text = LblChkDist.Text = "";
        hdfHideFrmDist.Value = "";

        if (ddlMvmtNo.SelectedIndex > 0)
        {
            GetMODetails();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select MO Number'); </script> ");
        }
    }

    public void GetMODetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("Select FrmDist,(SELECT district_name FROM pds.districtsmp where district_code=SMO.ToDist) ToDistName,ToDist From StateMovementOrder SMO Where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and ModeofDispatch='13' and SMS_frmDist='N' and ModeofDist In('Other','Both') Order By ToDistName");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlToDist.DataSource = ds.Tables[0];
                        ddlToDist.DataTextField = "ToDistName";
                        ddlToDist.DataValueField = "ToDist";
                        ddlToDist.DataBind();
                        ddlToDist.Items.Insert(0, "--Select--");

                        hdfHideFrmDist.Value = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('District Is Not Available'); </script> ");
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

    protected void ddlToDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMODate.Text = txtDispDist.Text = txtCropYear.Text = txtComdty.Text = txtFrmDist.Text = txtToDist.Text = txtTotalQty.Text = txtRemQty.Text = txtAddQty.Text = LblChkDist.Text = "";
        ddlToSendDist.Items.Clear();
        ddlToSendDist.Enabled = true;

        if (ddlToDist.SelectedIndex > 0)
        {
            lblMT.Visible = lblMT1.Visible = lblMT2.Visible = true;
            txtAddQty.Enabled = btnAdd.Enabled = true;
            GetDistName();
            GetMODistDetails();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select To District'); </script> ");
        }
    }

    public void GetMODistDetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("Select ReachDate,SMO,CreatedDate,Quantity,(Select Sum(Quantity) From StateMovementOrder where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and ModeofDispatch='13' and SMS_frmDist='N' and ModeofDist In('Other','Both')) TotalMOQty,ModeofDist,ModeofDispatch,ReachDate,CropYear,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=SMO.Commodity) ComdtyName,Commodity,(SELECT district_name FROM pds.districtsmp where district_code=SMO.ToDist) ToDistName,ToDist,(SELECT district_name FROM pds.districtsmp where district_code=SMO.FrmDist) FrmDistName,FrmDist From StateMovementOrder SMO Where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and ModeofDispatch='13' and SMS_frmDist='N' and ModeofDist In('Other','Both') and ToDist='" + ddlToDist.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlToSendDist.Items.FindByValue(hdfHideFrmDist.Value).Enabled = false;
                        hdfToDist.Value = ds.Tables[0].Rows[0]["ToDist"].ToString();

                        string strHideToDist = ds.Tables[0].Rows[0]["ToDist"].ToString();

                        ddlToSendDist.SelectedIndex = 0;

                        txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                        txtToDist.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                        txtComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();

                        DateTime MODate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        txtMODate.Text = MODate.ToString("dd/MMM/yyyy");

                        txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        hdfDistMode.Value = ds.Tables[0].Rows[0]["ModeofDist"].ToString();

                        if (hdfDistMode.Value == "Other")
                        {
                            txtDispDist.Text = LblChkDist.Text = "अन्य जिलों में";
                            ddlToSendDist.Items.FindByValue(strHideToDist).Enabled = false;
                        }
                        else if (hdfDistMode.Value == "Both")
                        {
                            txtDispDist.Text = LblChkDist.Text = "स्वयं एवं अन्य जिलों में";
                            ddlToSendDist.SelectedValue = hdfToDist.Value;
                            ddlToSendDist.Enabled = false;
                        }
                        else
                        {
                            txtDispDist.Text = "";
                        }

                        hdfFrmDist.Value = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                        hdfComdty.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();
                        hdfSMO.Value = ds.Tables[0].Rows[0]["SMO"].ToString();
                        hdfTotalQuantity.Value = ds.Tables[0].Rows[0]["Quantity"].ToString();
                        hdfReachDate.Value = ds.Tables[0].Rows[0]["ReachDate"].ToString();
                        hdfMOCreatedDate.Value = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

                        double TotalMOQty, TotalQty = 0;

                        if (hdfComdty.Value == "25")
                        {
                            TotalMOQty = ((double.Parse(ds.Tables[0].Rows[0]["TotalMOQty"].ToString())));
                            txtTotalQty.Text = TotalMOQty.ToString();

                            TotalQty = ((double.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString())));
                            txtRemQty.Text = txtAddQty.Text = TotalQty.ToString();

                            lblMT.Text = "(In Bales)";
                            lblMT1.Text = "(In Bales)";
                            lblMT2.Text = "(In Bales)";
                        }
                        else
                        {
                            TotalMOQty = ((double.Parse(ds.Tables[0].Rows[0]["TotalMOQty"].ToString())) / 10);
                            txtTotalQty.Text = TotalMOQty.ToString();

                            TotalQty = ((double.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString())) / 10);
                            txtRemQty.Text = txtAddQty.Text = TotalQty.ToString();


                            lblMT.Text = "(In MT)";
                            lblMT1.Text = "(In MT)";
                            lblMT2.Text = "(In MT)";
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Not Found'); </script> ");
                    }
                }
                txtAddQty.Focus();
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
        double TotalMOQty = double.Parse(txtTotalQty.Text);
        double RemQty = double.Parse(txtRemQty.Text);
        double AddQty = double.Parse(txtAddQty.Text);


        if (ddlToSendDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select To District'); </script> ");
            return;
        }
        else if (txtAddQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Quantity'); </script> ");
            return;
        }
        else if (RemQty < AddQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Allow Because Alloted Quantity Is Greater Than Remaining Dist Quantity'); </script> ");
            return;
        }
        else if (ddlToSendDist.Enabled == false && AddQty == RemQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपको स्कंध का वितरण स्वयं तथा अन्य जिलों में करना है, इसलिए आप स्कंध की पूरी मात्रा अपने जिले में नहीं रख सकते |'); </script> ");
            ddlToSendDist.Enabled = false;
            return;
        }
        else
        {
            ddlToSendDist.Enabled = true;
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("fromdisttext");
                dt.Columns.Add("fromdistval");
                dt.Columns.Add("todisttext");
                dt.Columns.Add("todistval");
                dt.Columns.Add("quantity");
                dt.Columns.Add("toOtherdisttext");
                dt.Columns.Add("toOtherdistval");
                dt.Columns.Add("SMONumber");
                dt.Columns.Add("TotalQty");
            }
            DataRow dr = dt.NewRow();
            dr["fromdisttext"] = txtFrmDist.Text;
            dr["fromdistval"] = hdfHideFrmDist.Value;
            dr["todisttext"] = ddlToDist.SelectedItem.ToString();
            dr["todistval"] = ddlToDist.SelectedValue.ToString();
            dr["toOtherdisttext"] = ddlToSendDist.SelectedItem.ToString();
            dr["toOtherdistval"] = ddlToSendDist.SelectedValue.ToString();
            dr["SMONumber"] = hdfSMO.Value;
            dr["TotalQty"] = hdfTotalQuantity.Value;

            ddlToSendDist.Items.FindByValue(ddlToSendDist.SelectedValue.ToString()).Enabled = false;

            dr["quantity"] = (float.Parse(txtAddQty.Text)).ToString("0.00");
            dt.Rows.Add(dr);
            Session["GridFill"] = dt;
            ddlMvmtNo.Enabled = ddlToDist.Enabled = false;
            fillgrid();

            txtRemQty.Text = txtAddQty.Text = (RemQty - AddQty).ToString();

            ddlToSendDist.SelectedIndex = 0;

            if (txtAddQty.Text == "0" || txtRemQty.Text == "0")
            {
                btnAdd.Enabled = false;
                txtAddQty.Enabled = false;

                ddlToDist.Items.FindByValue(ddlToDist.SelectedValue.ToString()).Enabled = false;
                ddlToSendDist.Items.Clear();
                ddlToDist.SelectedIndex = 0;
                ddlToDist.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = true;
                txtAddQty.Enabled = true;
            }
            txtAddQty.Focus();
        }
    }

    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["GridFill"];
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
            GridView1.Columns[3].HeaderText = txtDispDist.Text;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[4].Text));
            hdfFooterQtyTotal.Value = QtyTotal.ToString("0.00");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (hdfComdty.Value == "25")
            {
                e.Row.Cells[4].Text = "Total Qty (In Bales) = " + QtyTotal.ToString("0.00");
            }
            else
            {
                e.Row.Cells[4].Text = "Total Qty (In MT) = " + QtyTotal.ToString("0.00");
            }



            double TotalMOQty = double.Parse(txtTotalQty.Text);
            string strTotalMOQty = TotalMOQty.ToString("0.00");

            if ((strTotalMOQty == QtyTotal.ToString("0.00")))
            {
                btnRecptSubmit.Enabled = true;
            }
            else
            {
                btnRecptSubmit.Enabled = false;
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("fromdisttext");
            dt.Columns.Add("fromdistval");
            dt.Columns.Add("todisttext");
            dt.Columns.Add("todistval");
            dt.Columns.Add("quantity");
            dt.Columns.Add("toOtherdisttext");
            dt.Columns.Add("toOtherdistval");
            dt.Columns.Add("SMONumber");
            dt.Columns.Add("TotalQty");
        }
        else
        {
            string strTodistval = dt.Rows[e.RowIndex]["todistval"].ToString();
            if (strTodistval == ddlToDist.SelectedValue.ToString())
            {
                if (hdfDistMode.Value != "Both")
                {
                    string DistRowValue = dt.Rows[e.RowIndex]["toOtherdistval"].ToString();
                    ddlToSendDist.Items.FindByValue(DistRowValue).Enabled = true;

                    string QtyRowValue = dt.Rows[e.RowIndex]["quantity"].ToString();

                    double RemQty = double.Parse(txtRemQty.Text);
                    double AddQty = double.Parse(QtyRowValue);

                    txtRemQty.Text = txtAddQty.Text = (RemQty + AddQty).ToString();

                    dt.Rows.RemoveAt(e.RowIndex);

                    if (txtAddQty.Text == "0" || txtRemQty.Text == "0")
                    {
                        btnAdd.Enabled = false;
                        txtAddQty.Enabled = false;
                        // btnRecptSubmit.Enabled = true;
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                        txtAddQty.Enabled = true;
                        // btnRecptSubmit.Enabled = false;
                    }
                }
                else
                {
                    if (dt.Rows[e.RowIndex]["todistval"].ToString() != dt.Rows[e.RowIndex]["toOtherdistval"].ToString())
                    {
                        string DistRowValue = dt.Rows[e.RowIndex]["toOtherdistval"].ToString();
                        ddlToSendDist.Items.FindByValue(DistRowValue).Enabled = true;

                        string QtyRowValue = dt.Rows[e.RowIndex]["quantity"].ToString();

                        double RemQty = double.Parse(txtRemQty.Text);
                        double AddQty = double.Parse(QtyRowValue);

                        txtRemQty.Text = txtAddQty.Text = (RemQty + AddQty).ToString();

                        dt.Rows.RemoveAt(e.RowIndex);

                        if (txtAddQty.Text == "0" || txtRemQty.Text == "0")
                        {
                            btnAdd.Enabled = false;
                            txtAddQty.Enabled = false;
                        }
                        else
                        {
                            btnAdd.Enabled = true;
                            txtAddQty.Enabled = true;
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('SORRY, Do Not Allow To Remove'); </script> ");
                    }
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('SORRY, Do Not Allow To Remove'); </script> ");
            }
        }
        Session["GridFill"] = dt;
        fillgrid();
        txtAddQty.Focus();
    }

    protected void ddlToSendDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAddQty.Focus();
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/MovementOrderHome.aspx");
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["Acpt/Rjct"] = "Pending".ToString();
        string url = "Print_SubMovementOrder.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (txtCropYear.Text != "" || txtComdty.Text != "")
        {
            double TotalMOQty = double.Parse(txtTotalQty.Text);
            string strTotalMOQty = TotalMOQty.ToString("0.00");

            if (hdfFooterQtyTotal.Value == strTotalMOQty)
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

                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                    "Update StateMovementOrder Set SMS_frmDist='Y' where MoveOrdernum='" + ddlMvmtNo.SelectedItem.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' and Commodity='" + hdfComdty.Value + "' and CropYear='" + txtCropYear.Text + "' and ModeofDispatch='13' and SMS_frmDist='N';";

                            DataTable dt = adddetails();
                            if (dt != null)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string SubSMO = ((dt.Rows[i]["SMONumber"].ToString()) + ((60 + i) + 1));
                                    double ConvertMtToQtls = 0;

                                    if (hdfComdty.Value == "25")
                                    {
                                        ConvertMtToQtls = ((double.Parse(dt.Rows[i]["quantity"].ToString())));
                                    }
                                    else
                                    {
                                        ConvertMtToQtls = ((double.Parse(dt.Rows[i]["quantity"].ToString())) * 10);
                                    }

                                    instr += "Insert into StateSubMovementOrder(MoveOrdernum,SMO,SubSMO,FrmDist,ToDist,ModeofDist,ToOtherDist,Commodity,Quantity,CropYear,ReachDate,MOCreatedDate,ModeofDispatch,IsIssued,CreatedDate,IP,IsMvmtPlan,QtyByDist,RemQty,SubmitedQty,IsAccepted) values('" + ddlMvmtNo.SelectedItem.ToString() + "','" + dt.Rows[i]["SMONumber"] + "','" + SubSMO + "','" + hdfHideFrmDist.Value + "','" + dt.Rows[i]["todistval"] + "','" + hdfDistMode.Value + "','" + dt.Rows[i]["toOtherdistval"] + "','" + hdfComdty.Value + "','" + dt.Rows[i]["TotalQty"] + "','" + txtCropYear.Text + "','" + hdfReachDate.Value + "','" + hdfMOCreatedDate.Value + "','13','N',GETDATE(),'" + GetIp + "','N','" + ConvertMtToQtls + "','" + ConvertMtToQtls + "',0,'F');";
                                }
                            }

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = btnAdd.Enabled = false;
                                btnPrint.Enabled = true;
                                Session["GridFill"] = null;
                                Session["MovmtOrderNo"] = ddlMvmtNo.SelectedItem.ToString();

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully'); </script> ");
                                txtCropYear.Text = txtComdty.Text = "";
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
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Total MO Quantiy Is Not Equal To Total Qty'); </script> ");
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

}