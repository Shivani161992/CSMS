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

public partial class PaddyMilling_PMilling_SecurityLot : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    public string districtid = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                txtDistManager.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                GetMillName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                {
                    select = "Select distinct PM.Mill_Name As MillCode, MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' and PM.User_Agent!='DDMO' order by MillName Asc";
                }
                else if (Session["DistrictManager"].ToString() == "DDMO")
                {
                    select = "Select distinct PM.Mill_Name As MillCode, MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y'  and PM.User_Agent='DDMO' order by MillName Asc";
                }

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
        btnRecptSubmit.Enabled = false;
        string DistCode = Session["dist_id"].ToString();
        ddlAgtmtNumber.Items.Clear();
        ddlSecurityLot.Items.Clear();
        txtAgrmtFrmDate.Text = txtAgrmtToDate.Text = txtAgrmtLot.Text = txtAgrmtSecurityLot.Text = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                    {
                        select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' and User_Agent!='DDMO' order by Agreement_ID";
                    }
                    else if (Session["DistrictManager"].ToString() == "DDMO")
                    {
                        select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' and User_Agent='DDMO' order by Agreement_ID";
                    }

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
        ddlSecurityLot.Items.Clear();
        btnRecptSubmit.Enabled = false;

        txtAgrmtFrmDate.Text = txtAgrmtToDate.Text = txtAgrmtLot.Text = txtAgrmtSecurityLot.Text = "";

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("Select From_Date,To_Date,DhanLot As SecurityLot,DhanAmountDetails As AgrmtLot From PaddyMilling_Agreement_2017 where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and IsAccepted='Y' ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DateTime DOFrmDate = DateTime.Parse(ds.Tables[0].Rows[0]["From_Date"].ToString());
                    txtAgrmtFrmDate.Text = DOFrmDate.ToString("dd/MMM/yyyy");

                    DateTime DOTODate = DateTime.Parse(ds.Tables[0].Rows[0]["To_Date"].ToString());
                    txtAgrmtToDate.Text = DOTODate.ToString("dd/MMM/yyyy");

                    txtAgrmtLot.Text = ds.Tables[0].Rows[0]["AgrmtLot"].ToString();
                    txtAgrmtSecurityLot.Text = ds.Tables[0].Rows[0]["SecurityLot"].ToString();

                    int securitylot = int.Parse(txtAgrmtSecurityLot.Text);
                    int count = 0;

                    for (int i = securitylot; i < int.Parse(hdfSecurityLot.Value); i++)
                    {
                        count = count + 1;
                        ddlSecurityLot.Items.Add(new ListItem(count.ToString(), count.ToString()));
                    }
                    ddlSecurityLot.Items.Insert(0, "--Select--");
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

    public void GetCropYearValues()
    {
        hdfSecurityLot.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear,Paddy_SecurityLot FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    hdfSecurityLot.Value = ds.Tables[0].Rows[0]["Paddy_SecurityLot"].ToString();
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

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        int total = (int.Parse(ddlSecurityLot.SelectedItem.ToString()) + int.Parse(txtAgrmtSecurityLot.Text));

        int securityLot = int.Parse(ddlSecurityLot.SelectedItem.ToString());

        if (ddlSecurityLot.SelectedIndex > 0 && total <= int.Parse(hdfSecurityLot.Value))
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        districtid = Session["dist_id"].ToString();

                        ClientIP objClientIP = new ClientIP();
                        string GetIp = objClientIP.GETIP();

                        string instr = "";

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                            "Update PaddyMilling_Agreement_2017 Set DhanLot=(DhanLot+" + securityLot + ") , Rem_DhanLot=(Rem_DhanLot+" + securityLot + " ) where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'; ";

                        instr += "Insert Into PMilling_SecurityLotAmt(District,CropYear,Mill_ID,Agreement_ID,Agrmt_Lot,Additional_DhanLot,CreatedDate,IP_Address) values('" + districtid + "','" + txtYear.Text + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + txtAgrmtLot.Text + "','" + securityLot + "',GETDATE(),'" + GetIp + "'); ";

                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Agreement Security Lot Is Updated Successfully...'); </script> ");
                            btnRecptSubmit.Enabled = false;
                            ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlSecurityLot.Enabled = false;

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    protected void ddlSecurityLot_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;

        if (ddlSecurityLot.SelectedIndex > 0)
        {
            btnRecptSubmit.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया लॉट नंबर चुनें|'); </script> ");
        }
    }
}