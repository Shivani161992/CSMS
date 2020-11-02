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

public partial class PaddyMilling_PM_Agrmt_Delete : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
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
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                //if (Session["st_id"].ToString() == "10")
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed='Y' Order By district_name";
                //}
                //else
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed IS NULL Order By district_name";
                //}

                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistCode + "'";

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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlAgtmtNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
            return;
        }
        else if (hdfAgrmtData.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Agreement No. के विरुद्ध धान जारी किया जा चूका हैं, इसलिए इसे Delete नहीं किया जा सकता|'); </script> ");
            return;
        }
        else
        {
            if (txtYear.Text != "")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            con.Open();

                            string instr = "";

                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnDelete.Enabled = false;

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Agreement Is Deleted Successfully'); </script> ");
                                txtYear.Text = "";
                                ddlFrmDist.Enabled = ddlMillName.Enabled = ddlAgtmtNumber.Enabled = false;
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Deletion Not Allow'); </script> ");
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        btnDelete.Enabled = false;
        txtMobileNo.Text = txtFrmDate.Text = txtToDate.Text = txtAgrmtLot.Text = txtAgrmtQty.Text = "";
        hdfAgrmtData.Value = "";

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetMillName();
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

                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement As PM Left Join Miller_Registration MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + ddlFrmDist.SelectedValue.ToString() + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

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
        btnDelete.Enabled = false;
        txtMobileNo.Text = txtFrmDate.Text = txtToDate.Text = txtAgrmtLot.Text = txtAgrmtQty.Text = "";
        hdfAgrmtData.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    select = "Select Agreement_ID From PaddyMilling_Agreement where District='" + ddlFrmDist.SelectedValue.ToString() + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
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
        btnDelete.Enabled = false;
        txtMobileNo.Text = txtFrmDate.Text = txtToDate.Text = txtAgrmtLot.Text = txtAgrmtQty.Text = "";
        hdfAgrmtData.Value = "";

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select From_Date,To_Date,Total_Dhan,DhanAmountDetails As AgrmtLot,MobileNO,Rem_Total_Dhan,Return_TotalRice From PaddyMilling_Agreement where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and IsAccepted='Y' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Decimal ReturnRice = 0,RemPaddy= 0, AgrmtPaddy=0;

                    txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNO"].ToString();

                    DateTime FrmDate = DateTime.Parse(ds.Tables[0].Rows[0]["From_Date"].ToString());
                    txtFrmDate.Text = FrmDate.ToString("dd-MMM-yyyy");

                    DateTime ToDate = DateTime.Parse(ds.Tables[0].Rows[0]["To_Date"].ToString());
                    txtToDate.Text = ToDate.ToString("dd-MMM-yyyy");

                    txtAgrmtLot.Text = ds.Tables[0].Rows[0]["AgrmtLot"].ToString();
                    txtAgrmtQty.Text = ds.Tables[0].Rows[0]["Total_Dhan"].ToString();

                    ReturnRice = decimal.Parse(ds.Tables[0].Rows[0]["Return_TotalRice"].ToString());
                    RemPaddy = decimal.Parse(ds.Tables[0].Rows[0]["Rem_Total_Dhan"].ToString());
                    AgrmtPaddy = decimal.Parse(ds.Tables[0].Rows[0]["Total_Dhan"].ToString());

                    if (ReturnRice > 0 || (RemPaddy != AgrmtPaddy))
                    {
                        hdfAgrmtData.Value = "1";
                        btnDelete.Enabled = false;
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Agreement No. के विरुद्ध धान जारी किया जा चूका हैं, इसलिए इसे Delete नहीं किया जा सकता|'); </script> ");
                        return;
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Agreement Data Is Not Available'); </script> ");
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