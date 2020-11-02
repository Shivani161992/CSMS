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

public partial class IssueCenter_Reprint_DeliveryChallan : System.Web.UI.Page
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
            hlinkpdo.Attributes.Add("onclick", "window.open('Print_DeliveryChallan.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

            ddlyear.Items.Add(DateTime.Today.Year.ToString());

            ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());

            ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());

            ddlyear.Items.Insert(0, "--Select--");

            ddlyear.SelectedIndex = 1;

            ddlmonth.SelectedValue = (Convert.ToInt64(DateTime.Today.Month.ToString()) + 1).ToString();

            ddlyear_SelectedIndexChanged(sender, e);
            
            //get_to_no();
           
            

        }
    }
    protected void get_to_no()
    {
        distid = Session["dist_id"].ToString();
        //sid = Session["issue_id"].ToString();
        try
        {


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "select  distinct TransportOrder from dbo.DPY_TranportOrder where DPY_TranportOrder.DistrictId='" + distid + "' and  AllotMonth='" + ddlmonth.SelectedValue.ToString() + "' and AllotYear='" + ddlyear.SelectedValue.ToString() + "'   order by  TransportOrder desc";

            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_TO_no.DataSource = ds.Tables[0];
                ddl_TO_no.DataTextField = "TransportOrder";
                ddl_TO_no.DataValueField = "TransportOrder";
                ddl_TO_no.DataBind();
                ddl_TO_no.Items.Insert(0, "--Select--");


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
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected void ddlDOnum_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlDChallan.SelectedIndex == 0 || ddlDChallan.SelectedItem.Text == "Select")
            {
                hlinkpdo.Visible = false;
                hlinkpdo.Enabled = false;

            }

            else
            {
                string fps = "";
                string query = "select fps_code from Issued_Doorstep_do_fps where delivery_order_no='" + ddlDChallan.SelectedValue.ToString() + "' ";

        SqlCommand cmd = new SqlCommand(query, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            fps = dr["fps_code"].ToString();
        
           

            hlinkpdo.Visible = true;
            hlinkpdo.Enabled = true;
        }
        Session["fpscode"] = fps;
        Session["DeliveryChallanTO"] = ddlDChallan.SelectedValue.ToString();
        Session["TO"] = ddl_TO_no.SelectedItem.Text;
        Session["TOdate"] = hd_date.Value;
            }
        }
        catch
        {
        }
        // create do session for selected index change and enable print button
    }
    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string qrydate = "select TransportDate from dbo.DPY_TranportOrder where DPY_TranportOrder.DistrictId='" + distid + "' and DPY_TranportOrder.TransportOrder='" + ddl_TO_no.SelectedValue.ToString() + "'  ";

            SqlCommand cmddate = new SqlCommand(qrydate, con);

            SqlDataAdapter dadate = new SqlDataAdapter(cmddate);

            DataSet dsdate = new DataSet();

            dadate.Fill(dsdate);
            DataRow dr = dsdate.Tables[0].Rows[0];

            hd_date.Value = dr["TransportDate"].ToString();
            do_date = DateTime.Parse(dr["TransportDate"].ToString());
            hd_date.Value = getdate(do_date.ToString());


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "Select distinct delivery_order_no+'('+fps_code+')' as delivery_challan_no,delivery_order_no from Issued_Doorstep_do_fps where district_code = '" + distid + "' and  Transport_order='" + ddl_TO_no.SelectedValue.ToString() + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);



            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDChallan.DataSource = ds.Tables[0];
                ddlDChallan.DataTextField = "delivery_challan_no";
                ddlDChallan.DataValueField = "delivery_order_no";
                ddlDChallan.DataBind();
                ddlDChallan.Items.Insert(0, "--Select--");


                // Session["doforprint"] = do_no;

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch
        {
        }
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_to_no();
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_to_no();
    }
}