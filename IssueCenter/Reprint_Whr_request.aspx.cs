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

public partial class IssueCenter_Reprint_Whr_request : System.Web.UI.Page
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
            hlinkpdo.Attributes.Add("onclick", "window.open('Print_WHRRequest.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            get_whrRequest();



        }
    }
    protected void get_whrRequest()
    {
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        try
        {


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "select distinct WHR_Request from Acceptance_Note_Detail where WHR_Request is not null and IssueCenter_ID='"+sid+"'  order by WHR_Request desc";

            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);
            DataRow dr = ds.Tables[0].Rows[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_WHR.DataSource = ds.Tables[0];
                ddl_WHR.DataTextField = "WHR_Request";
                ddl_WHR.DataValueField = "WHR_Request";
                ddl_WHR.DataBind();
                ddl_WHR.Items.Insert(0, "--Select--");


                // Session["doforprint"] = do_no;

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception)
        { }
    }

 
    protected void ddl_WHR_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        hlinkpdo.Visible = true;
        Session["WHR_Request"] = ddl_WHR.SelectedValue.ToString();
        //Session["issue_id"] = sid;
        //Session["dist_id"] = distid;
        //Session["Godown"] = hd_godown.Value;

    }
}