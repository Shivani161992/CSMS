using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class State_PDSMO_ContactDetails : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string Update;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_FA = ConfigurationManager.ConnectionStrings["constr_FA"].ConnectionString;
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_FA"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                txtDMName.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtDMName.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtDMName.Attributes.Add("onchange", "return chksqltxt(this)");

                GetEmpCode();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetEmpCode()
    {
        using (con = new SqlConnection(strcon_FA))
        {
            try
            {
                con.Open();
                string select = "Select Employee_ID From Pyl_Employee_Master where Office_Code='80' order by Employee_ID";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmpCode.DataSource = ds.Tables[0];
                    ddlEmpCode.DataTextField = "Employee_ID";
                    ddlEmpCode.DataValueField = "Employee_ID";
                    ddlEmpCode.DataBind();
                    ddlEmpCode.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Employee Code Is Not Available'); </script> ");
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

    protected void ddlEmpCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDMName.Text = "";
        GetEmpData();
    }

    private void GetEmpData()
    {
        using (con = new SqlConnection(strcon_FA))
        {
            try
            {
                con.Open();
                string select = "Select Employee_Name From Pyl_Employee_Master where Employee_ID='" + ddlEmpCode.SelectedItem.ToString() + "' and Office_Code='80' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtDMName.Text = ds.Tables[0].Rows[0]["Employee_Name"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Employee Name Is Not Available'); </script> ");
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    if (ddlEmpCode.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Code of Employee'); </script> ");
                        return;
                    }
                    else
                    {
                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        string select = "";

                        if (hdfData.Value == "1") //For Update
                        {
                            select = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";
                            select += "Insert Into PDSMO_EMPContactDetails_Log(Mobile_No,Email,Name,Employee_ID,IP_Address,CreatedDate,UpdatedDate,Updated_IP) Select Mobile_No,Email,Name,Employee_ID,IP_Address,CreatedDate,GETDATE(),'" + GetIp + "' From PDSMO_EMPContactDetails Where Mobile_No='" + hdfMobileNo.Value + "' ;";
                            select += "Update PDSMO_EMPContactDetails Set Email='" + txtDMMail.Text + "',Name='" + txtDMName.Text + "',Employee_ID='" + ddlEmpCode.SelectedItem.ToString() + "',UpdatedDate=GETDATE(),Updated_IP='" + GetIp.ToString() + "' where Mobile_No='" + hdfMobileNo.Value + "' ;";
                            select += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }
                        else if (hdfData.Value == "0") //For Insert
                        {
                            select = "Insert Into PDSMO_EMPContactDetails(Mobile_No,Email,Name,Employee_ID,IP_Address,CreatedDate) Values('" + hdfMobileNo.Value + "','" + txtDMMail.Text + "','" + txtDMName.Text + "','" + ddlEmpCode.SelectedItem.ToString() + "','" + GetIp.ToString() + "',GETDATE())";
                        }

                        con.Open();
                        cmd = new SqlCommand(select, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Save Successfully...'); </script> ");
                            txtDMMail.Enabled = txtDMName.Enabled = false;
                            ddlEmpCode.Enabled = false;
                            btnSave.Enabled = false;
                            hdfData.Value = hdfMobileNo.Value = "";

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
        else
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/MovementOrderHome.aspx");
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtDMMobile.Text.Trim().Length == 10)
        {
            txtDMMail.Text = txtDMName.Text = "";
            hdfData.Value = hdfMobileNo.Value = "";
            btnSave.Enabled = false;
            ddlEmpCode.Enabled = false;
            ddlEmpCode.SelectedIndex = 0;
            GetMobileData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Valid Mobile Number'); </script> ");
            return;
        }
    }

    private void GetMobileData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select * From PDSMO_EMPContactDetails where Mobile_No='" + txtDMMobile.Text.Trim() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtDMMail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtDMName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    string EmpID = ds.Tables[0].Rows[0]["Employee_ID"].ToString();
                    ddlEmpCode.SelectedValue = EmpID;

                    txtDMMail.Enabled = true;
                    btnSave.Text = "Update";
                    txtDMMobile.Enabled = false;
                    btnSearch.Enabled = false;
                    btnSave.Enabled = true;
                    ddlEmpCode.Enabled = true;

                    hdfData.Value = "1";
                    hdfMobileNo.Value = txtDMMobile.Text;
                }
                else
                {
                    txtDMMail.Enabled = true;
                    btnSave.Text = "Save";
                    txtDMMobile.Enabled = false;
                    btnSearch.Enabled = false;
                    btnSave.Enabled = true;
                    ddlEmpCode.Enabled = true;

                    hdfData.Value = "0";
                    hdfMobileNo.Value = txtDMMobile.Text;
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