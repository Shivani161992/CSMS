using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class State_DM_EnquiryForm : System.Web.UI.Page
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
                txtPanNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtPanNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtPanNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTinNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTinNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTinNo.Attributes.Add("onchange", "return chksqltxt(this)");

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
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp Order By district_name");
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
        txtDMName.Text = txtDMMail.Text = txtDMMobile.Text = txtDMMobile2.Text = txtPanNo.Text = txtTinNo.Text =  "";
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

                    select = "select DM_Name,DM_MailId,DM_Mobile,RM_Mobile,PanNo,TinNo From officers_list where District_code='" + ddlDist.SelectedValue.ToString() + "'";
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds, "officers_list");

                    txtDMName.Text = ds.Tables[0].Rows[0]["DM_Name"].ToString();
                    txtDMMail.Text = ds.Tables[0].Rows[0]["DM_MailId"].ToString();
                    txtDMMobile.Text = ds.Tables[0].Rows[0]["DM_Mobile"].ToString();
                    txtDMMobile2.Text = ds.Tables[0].Rows[0]["RM_Mobile"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PanNo"].ToString();
                    txtTinNo.Text = ds.Tables[0].Rows[0]["TinNo"].ToString();
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

                string browser = Request.Browser.Browser.ToString();
                string version = Request.Browser.Version.ToString();
                string useragent = browser + version;

                if (ViewState["Present"].ToString() == 1.ToString())
                {
                    Update = "Update officers_list set DM_Name=N'" + txtDMName.Text + "',DM_MailId='" + txtDMMail.Text + "',DM_Mobile='0" + txtDMMobile.Text + "',DM_IP_Address='" + GetIp.ToString() + "',DM_Current_DateTime=GETDATE(),DM_User_Agent='" + useragent + "',RM_Mobile='" + txtDMMobile2.Text + "',PanNo='"+txtPanNo.Text+"',TinNo='"+txtTinNo.Text+"' where District_code='" + ddlDist.SelectedValue.ToString() + "'";
                }
                else
                {
                    Update = "Insert Into officers_list (DM_Name,DM_MailId,DM_Mobile,DM_IP_Address,DM_Current_DateTime,DM_User_Agent,District_code,District,RM_Mobile,PanNo,TinNo) values(N'" + txtDMName.Text + "','" + txtDMMail.Text + "','0" + txtDMMobile.Text + "','" + GetIp.ToString() + "',GETDATE(),'" + useragent + "',N'" + ddlDist.SelectedValue.ToString() + "','" + ddlDist.SelectedItem.ToString() + "','" + txtDMMobile2.Text + "','" + txtPanNo.Text + "','" + txtTinNo.Text + "')";
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
        Response.Redirect("~/State/State_Welcome.aspx");
    }
}