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

public partial class IssueCenter_Reprint_DoorStepDO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
   
    string distid = "";
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();

        if (!IsPostBack)
        {
            hlinkpdo.Attributes.Add("onclick", "window.open('Print_DoorStep_DO.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "Select delivery_order_no from DoorStep_DO where district_code = '"+distid+"' and issueCentre_code = '"+sid+"'";

            SqlCommand cmd = new SqlCommand(qry,con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDOnum.DataSource = ds.Tables[0];
                ddlDOnum.DataTextField = "delivery_order_no";
                ddlDOnum.DataValueField = "delivery_order_no";
                ddlDOnum.DataBind();
                ddlDOnum.Items.Insert(0, "--Select--");

               
               // Session["doforprint"] = do_no;

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        
        }
    }

    protected void ddlDOnum_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDOnum.SelectedIndex == 0 || ddlDOnum.SelectedItem.Text == "Select")
        {
            hlinkpdo.Visible = false;
            hlinkpdo.Enabled = false;

        }

        else
        {
            Session["doforprint"] = ddlDOnum.SelectedItem.Text;

            hlinkpdo.Visible = true;
            hlinkpdo.Enabled = true;

        }
      // create do session for selected index change and enable print button
    }
}
