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

public partial class District_SuspendedFPS : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

   // public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());

    string distid = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] == null)
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }


        distid = Session["dist_id"].ToString();

        if (!IsPostBack)
        {


            tx_order_date.Text = DateTime.Today.Date.Date.ToString("dd/MM/yyyy");

            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;


            get_fps();

            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
           
            get_Sfps();
        }
    }

    protected void get_fps()
    {
        try
        {
            string dist = distid;
            ddlcoctFPS.Items.Clear();

            string fpsget = "select fps_code , tbl_rootchart_master.fps_name + '(' + tbl_rootchart_master.fps_code + ')'  as fps_name from tbl_rootchart_master where DistrictId = '"+dist+"' order by fps_code";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmdfps = new SqlCommand(fpsget, con);

            SqlDataAdapter dafps = new SqlDataAdapter(cmdfps);

            DataSet dsfps = new DataSet();

            dafps.Fill(dsfps);

            if (dsfps.Tables[0].Rows.Count > 0)
            {
                ddlcoctFPS.DataSource = dsfps.Tables[0];

                ddlcoctFPS.DataTextField = "fps_name";
                ddlcoctFPS.DataValueField = "fps_code";
                ddlcoctFPS.DataBind();
                ddlcoctFPS.Items.Insert(0, "--Select--");

            }

        }

        catch (Exception)
        {

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_Sfps();
    }

    protected void get_Sfps()
    {
        try
        {
            string dist = distid;
            ddlsusFPS.Items.Clear();

            string fpsget = "select fps_code , tbl_rootchart_master.fps_name + '(' + tbl_rootchart_master.fps_code + ')'  as fps_name from tbl_rootchart_master where DistrictId = '" + dist + "' and  fps_code not in (Select SuspendFPS from SuspendedFPS where Month = '" + ddl_allot_month.SelectedItem.Value + "' and Year = '" + ddd_allot_year.SelectedItem.Text + "') order by fps_code";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmdfps = new SqlCommand(fpsget, con);

            SqlDataAdapter dafps = new SqlDataAdapter(cmdfps);

            DataSet dsfps = new DataSet();

            dafps.Fill(dsfps);

            if (dsfps.Tables[0].Rows.Count > 0)
            {
                ddlsusFPS.DataSource = dsfps.Tables[0];

                ddlsusFPS.DataTextField = "fps_name";
                ddlsusFPS.DataValueField = "fps_code";
                ddlsusFPS.DataBind();
                ddlsusFPS.Items.Insert(0, "--Select--");

            }

        }

        catch (Exception)
        {

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        
        try
        {
            string opid = Session["OperatorIDDM"].ToString();

            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            string dist = distid;

            if (txt_orderNum.Text == "" || tx_order_date.Text == "" )
            {
               
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('आदेश क्रमांक भारे अथवा आदेश दिनांक चुने ');</script>");
                return;
            }

            if (ddlsusFPS.SelectedValue == "0" || ddlcoctFPS.SelectedValue == "0")
            {
                
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS Name.');</script>");
                return;
            }

            string month = ddl_allot_month.SelectedItem.Value;

            string year = ddd_allot_year.SelectedItem.Text;

            string ordernum = txt_orderNum.Text.Trim();

            string orderDate = getDate_MDY(tx_order_date.Text);

            string susFPS = ddlsusFPS.SelectedValue;

            string conctfps = ddlcoctFPS.SelectedValue;

            if (susFPS == conctfps)
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('निलंबित दूकान एवं सम्बंधित जुड़ने वाली दूकान एक सी नहीं हो सकती');</script>");
                return;
            }

            string qry = "Insert into SuspendedFPS (DistrictId ,Month ,Year ,DSO_Order ,OrderDate ,SuspendFPS ,ConnectFPS ,IP ,CreatedDate ,UserId) Values  ('"+distid+"','"+month+"','"+year+"','"+ordernum+"' ,'"+orderDate+"' ,'"+susFPS+"' ,'"+conctfps+"', '"+ip+"',getdate(),'"+opid+"')";

            SqlCommand cmd = new SqlCommand(qry, con);

            int y = cmd.ExecuteNonQuery();

            if (y > 0)
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Record Saved.');</script>");

                btnsave.Enabled = false;
            }
        }

        catch
        {

        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        Response.Redirect("~/District/SuspendedFPS.aspx");
    }
}
