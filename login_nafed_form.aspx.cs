using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class login_naged_form : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    DataSet ds;
    SqlDataAdapter Adp;
    protected void Page_Load(object sender, EventArgs e)
    {
        psw.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
        username.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
        psw.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        username.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");
       

        if (!IsPostBack)
        {

        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtVerificationCode.Text.ToLower() == Session["CaptchaVerify"].ToString())
            {
                //Response.Redirect("Default.aspx");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_ValidateUser";
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username.Text.Trim();
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = psw.Text.Trim();
                cmd.Connection = con;
                Adp = new SqlDataAdapter(cmd);
                ds = new DataSet();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                cmd.ExecuteNonQuery();
                Adp.Fill(ds);
                try
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string usernameID = ds.Tables[0].Rows[0]["Column1"].ToString();
                        if (usernameID == "2")
                        {
                            Session["NAFED"] = base64Encode(usernameID);
                            Response.Redirect("NAFED/nafed_Dashboard.aspx");
                        }
                        else
                        {

                            txtVerificationCode.Text = "";
                            username.Text = "";
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Check Username or Password');</script>");
                            return;

                        }
                    }
                    else
                    {
                        txtVerificationCode.Text = "";
                        username.Text = "";
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Check Username or Password');</script>");
                        return;

                    }

                }

                catch (Exception ex)
                {

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Dispose();

                }
            }
            else
            {
                lblCaptchaMessage.Text = "Please enter correct captcha !";
                lblCaptchaMessage.ForeColor = System.Drawing.Color.Red;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter correct captcha !');</script>");
                return;
            }

        }
        catch (Exception ex)
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        finally
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
    }
    private string base64Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }


}