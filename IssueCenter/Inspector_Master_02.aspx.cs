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
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class IssueCenter_Inspector_Master : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label3.Text = "Add Inspector";

    }

   
    


    protected void Button1_Click(object sender, EventArgs e)
    {
        
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string strselect = "insert into Inspector_master(Inspector_Name,Inspector_Desig) values ('" + txt_name.Text + "',N '" + txt_desig.Text + "')";
            cmd1 = new SqlCommand(strselect, con);
            string check = (string)cmd1.ExecuteScalar();

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

        }
        catch
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
        }

        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}