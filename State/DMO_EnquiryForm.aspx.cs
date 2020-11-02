using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class State_DMO_EnquiryForm : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string Update;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                GetAllDistName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetAllDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed = 'Y' order by district_name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDist.DataSource = ds.Tables[0];
                        ddlDist.DataTextField = "district_name";
                        ddlDist.DataValueField = "district_code";
                        ddlDist.DataBind();
                        ddlDist.Items.Insert(0, "--Select--");
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

    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDMName.Text = txtDMMail.Text = txtDMMobile.Text = "";
        if (ddlDist.SelectedIndex > 0)
        {
            GetDistName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Dist Name'); </script> ");
        }
    }

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string check = "select District_code From officers_list where District_code= '" + ddlDist.SelectedValue.ToString() + "'";
                con.Open();
                cmd = new SqlCommand(check, con);
                ViewState["Present"] = cmd.ExecuteScalar();
                if (ViewState["Present"] != null)
                {
                    ViewState["Present"] = "1";
                    string select = "";

                    select = "select DMO_Name,DMO_MailId,DMO_Mobile From officers_list where District_code='" + ddlDist.SelectedValue.ToString() + "'";
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds, "officers_list");

                    txtDMName.Text = ds.Tables[0].Rows[0]["DMO_Name"].ToString();
                    txtDMMail.Text = ds.Tables[0].Rows[0]["DMO_MailId"].ToString();
                    txtDMMobile.Text = ds.Tables[0].Rows[0]["DMO_Mobile"].ToString();
                }
                else
                {
                    ViewState["Present"] = "0";
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
        using (con = new SqlConnection(strcon))
        {
            try
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                if (ViewState["Present"].ToString() == 1.ToString())
                {
                    Update = string.Format("Update officers_list set DMO_Name=N'{0}',DMO_MailId='{1}',DMO_Mobile={2},DMO_IP='{3}',DMO_CreatedDate={4} where District_code='" + ddlDist.SelectedValue.ToString() + "' ", txtDMName.Text, txtDMMail.Text, "0" + txtDMMobile.Text, GetIp.ToString(), "GETDATE()");
                }
                else
                {
                    Update = string.Format("Insert Into officers_list (DMO_Name,DMO_MailId,DMO_Mobile,DMO_IP,DMO_CreatedDate,District_code,District) values(N'{0}','{1}',{2},'{3}',{4},'{5}','{6}',N'{7}')", txtDMName.Text, txtDMMail.Text, "0" + txtDMMobile.Text, GetIp.ToString(), "GETDATE()", ddlDist.SelectedValue.ToString(), ddlDist.SelectedItem.ToString());
                }

                con.Open();
                cmd = new SqlCommand(Update, con);
                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Save Successfully...'); </script> ");
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/Markfed_Welcome.aspx");
    }
}