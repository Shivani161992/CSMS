using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class State_SearchQry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {


        }

        else
        {

            Response.Redirect("~/MainLogin.aspx");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string challan = "SELECT *  FROM "+TextBox1.Text.Trim()+" ";
        SqlCommand cmd = new SqlCommand(challan, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];

                GridView1.DataBind();
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Found'); </script> ");

                GridView1.DataSource = "";

                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            GridView1.DataSource = "";

            GridView1.DataBind();

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Wrong Query'); </script> ");

            Label2.Text = ex.Message.ToString();
                 
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
       



       
    }
}
