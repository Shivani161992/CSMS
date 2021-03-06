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
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class IssueCenter_Godown_Master_New : System.Web.UI.Page
{
   // public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["hindi"].ToString() == "H")
            {
                lbl_Branch.Text = Resources.hindi.lbl_Branch;
                lblGodownMaster.Text = Resources.hindi.lblGodownMaster;
               
            }
            if (!IsPostBack)
            {
                Get_Brance_Name();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();

        }
    }

    void Get_Brance_Name()
    {
        try
        {
            string distid = "23" + Session["dist_id"].ToString();
            string query = "Select DepotId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + distid + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddl_Branch_Name.DataSource = ds.Tables[0];
                ddl_Branch_Name.DataTextField = "DepotName";
                ddl_Branch_Name.DataValueField = "DepotId";
                ddl_Branch_Name.DataBind();
                ddl_Branch_Name.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();

        }
       
    }
    protected void ddl_Branch_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Depot_DepotID"] = ddl_Branch_Name.SelectedValue;
        Response.Redirect("~/IssueCenter/Godown_Add.aspx");
    }
}
