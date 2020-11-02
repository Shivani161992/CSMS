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

public partial class IssueCenter_Reprint_AcceptanceNote : System.Web.UI.Page
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
            hlinkpdo.Attributes.Add("onclick", "window.open('Print_AcceptanceNo_New.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            get_Acceptanceno();



        }
    }
    protected void get_Acceptanceno()
    {
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        try
        {


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "select distinct Acceptance_No from dbo.Acceptance_Note_Detail where Acceptance_Note_Detail.Distt_ID='" + distid + "' and Acceptance_Note_Detail.IssueCenter_ID='" + sid + "' and Year='2015'  order by  Acceptance_No desc";

            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);
            DataRow dr = ds.Tables[0].Rows[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_Acceptanceno.DataSource = ds.Tables[0];
                ddl_Acceptanceno.DataTextField = "Acceptance_No";
                ddl_Acceptanceno.DataValueField = "Acceptance_No";
                ddl_Acceptanceno.DataBind();
                ddl_Acceptanceno.Items.Insert(0, "--Select--");


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
 
    protected void ddl_Acceptanceno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string qrydate = "select godown from dbo.Acceptance_Note_Detail where Acceptance_Note_Detail.Distt_ID='" + distid + "' and Acceptance_Note_Detail.IssueCenter_ID='" + sid + "' and Acceptance_Note_Detail.Acceptance_No='" + ddl_Acceptanceno.SelectedValue.ToString() + "'  ";

            SqlCommand cmddate = new SqlCommand(qrydate, con);

            SqlDataAdapter dadate = new SqlDataAdapter(cmddate);

            DataSet dsdate = new DataSet();

            dadate.Fill(dsdate);
            DataRow dr = dsdate.Tables[0].Rows[0];

         hd_godown.Value = dr["godown"].ToString();
        }


          
        
        catch
        {
        }
        hlinkpdo.Visible = true;
        Session["Acceptance_NO"] = ddl_Acceptanceno.SelectedValue.ToString();
        Session["issue_id"] = sid;
        Session["dist_id"] = distid;
        Session["Godown"] = hd_godown.Value;


     
    }

}
