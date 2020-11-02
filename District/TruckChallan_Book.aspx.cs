using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_TruckChallan_Book : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string Dist_Id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Dist_Id = Session["dist_id"].ToString();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        Dist_Id = Session["dist_id"].ToString();

        if (txtBookNo.Text.Trim() != "" && txtFrmPageNo.Text.Trim() != "" && txtUptoPageNo.Text.Trim() != "")
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        string select = "Select Book_No From TruckChallan_Book where Book_No='" + txtBookNo.Text.Trim() + "' and District='" + Dist_Id + "'";
                        da = new SqlDataAdapter(select, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Book Number Already Available'); </script> ");
                            return;
                        }
                        else
                        {
                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            string insert = "Insert Into TruckChallan_Book(Book_No,From_Page,Upto_Page,District,CreatedDate,IP,Rem_Page) values('" + txtBookNo.Text.Trim() + "','" + txtFrmPageNo.Text.Trim() + "','" + txtUptoPageNo.Text.Trim() + "','" + Dist_Id + "',GETDATE(),'" + GetIp + "','" + txtUptoPageNo.Text.Trim() + "')";

                            cmd = new SqlCommand(insert, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = false;

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully'); </script> ");
                                //txtBookNo.Text = txtFrmPageNo.Text = txtUptoPageNo.Text = "";

                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter All Field'); </script> ");
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }


}