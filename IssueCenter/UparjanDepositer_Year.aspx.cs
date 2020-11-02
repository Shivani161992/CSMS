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

public partial class IssueCenter_UparjanDepositer_Year : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                GetCommodity();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Name,Commodity_Id From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN ('24','28','8','11','12','40') Order By Commodity_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlcomdty.DataSource = ds.Tables[0];
                    ddlcomdty.DataTextField = "Commodity_Name";
                    ddlcomdty.DataValueField = "Commodity_Id";
                    ddlcomdty.DataBind();
                    ddlcomdty.SelectedIndex = 4;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ddl_Year.SelectedItem.Value == "2015")
        {
            Response.Redirect("~/IssueCenter/DepositerForm_procurement2015.aspx");
        }
        else if (ddl_Year.SelectedItem.Value == "2016" && ddlcomdty.SelectedValue.ToString() == "28") //For Wheat
        {
            Response.Redirect("~/IssueCenter/DepositerForm_Procurement.aspx");
        }
        else if (ddl_Year.SelectedItem.Value == "2016" && ddlcomdty.SelectedValue.ToString() != "28") //Kharif 2016
        {
            Response.Redirect("~/IssueCenter/DepositerForm_ProcPaddy2016.aspx");
        }
        //else if (ddl_Year.SelectedItem.Value == "2016" && ddlcomdty.SelectedValue.ToString() == "24") //For Paddy
        //{
        //    Response.Redirect("~/IssueCenter/DepositerForm_ProcPaddy2016.aspx");
        //}
    }
}
