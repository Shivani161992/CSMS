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

public partial class IssueCenter_Reprint_PartialRejection : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    string distid = "";
    string sid = "";
    DateTime do_date = new DateTime();


    protected void Page_Load(object sender, EventArgs e)
    {
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        if (!IsPostBack)
        {
            hlinkpdo.Attributes.Add("onclick", "window.open('Print_RejectNote_New.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            get_no();

        }
    }


    protected void get_no()
    {
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        try
        {


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "select distinct IssueID , godown from dbo.Acceptance_Note_Detail where Acceptance_Note_Detail.Distt_ID='" + distid + "' and Acceptance_Note_Detail.IssueCenter_ID='" + sid + "' and Year='2015' and Reject_Qty <> 0  order by  IssueID desc";

            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_Prej.DataSource = ds.Tables[0];
                ddl_Prej.DataTextField = "IssueID";
                ddl_Prej.DataValueField = "godown";
                ddl_Prej.DataBind();
                ddl_Prej.Items.Insert(0, "--Select--");

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception)
        {
        
        }
    }

   
    protected void ddl_Prej_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Prej.SelectedItem.Text == "--Select--" || ddl_Prej.SelectedValue == "0")
            {
                hlinkpdo.Visible = false;
            }

            else
            {
                Session["ReceiptID"] = ddl_Prej.SelectedItem.Text;
                Session["issue_id"] = sid;
                Session["dist_id"] = distid;
                Session["Godown"] = ddl_Prej.SelectedValue;
                Session["Commodity_Id"] = 1;

                hlinkpdo.Visible = true;

            }
        }

        catch
        {

        }
    }
}
