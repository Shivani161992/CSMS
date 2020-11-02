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

public partial class printRpt_N_2_fpswise_alloc : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    public string dist_code = "", lead_code = "", smonth = "", syear = "";
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
        if (Page.IsPostBack == false)
        {
            
        }
        dist_code = Session["dist_id"].ToString();
        lead_code = Session["ldcode"].ToString();
        smonth = Session["smonth"].ToString();
        syear = Session["syear"].ToString();
        get_distname();
        get_leadname();
        get_commodityname();
        int month = int.Parse(smonth.ToString());
        lbl_month.Text = get_Monthname(month, false);
        lbl_year.Text = syear;
        lbl_cmonth.Text = get_Monthname(DateTime.Today .Month,false);
        lbl_cyear.Text = DateTime.Today.Year.ToString() ;
        get_Data();
    }
    private static string get_Monthname(int month, bool xyz)
    {
        DateTime date = new DateTime(1900, month, 1);
        if (xyz) return date.ToString("MMM");
        return date.ToString("MMMM");

    }
    protected void get_leadname()
    {
        cmd.CommandText = "select * from dbo.m_LeadSoc where District_code='" + dist_code + "' and LeadSoc_Code='" + lead_code + "' ";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lbl_lead.Text = dr["LeadSoc_nameU"].ToString();
        }
        dr.Close();
        con_opdms.Close();
    }
    protected void get_distname()
    {
        cmd.CommandText = "select * from pds.districtsmp where district_code='" + dist_code + "'";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lbl_dist.Text = dr["district_name"].ToString();
        }
        dr.Close();
        con_opdms.Close();
    }
    protected void get_commodityname()
    {
        string comm = Session["comm"].ToString();
        if (comm == "1")
        {
            tx_comm.Text = "Rice APL";
        }
        if (comm == "2")
        {
            tx_comm.Text = "Rice BPL";
        }
        if (comm == "3")
        {
            tx_comm.Text = "Rice AAY";
        }
        if (comm == "4")
        {
            tx_comm.Text = "Wheat APL";
        }
        if (comm == "5")
        {
            tx_comm.Text = "Wheat BPL";
        }
        if (comm == "6")
        {
            tx_comm.Text = "Wheat AAY";
        }
        if (comm == "7")
        {
            tx_comm.Text = "Sugar";
        }
        if (comm == "9")
        {
            tx_comm.Text = "Kerosene";
        }
    }
    protected void get_Data()
    {
        DataSet ds=(DataSet) Session["ds_print"];
        GridView1.DataSource = ds;
        GridView1.DataBind();
        GridView1.FooterRow.Visible = true;
        GridView1.FooterRow.Cells[1].Text = Session["tot_ope"].ToString();
        GridView1.FooterRow.Cells[2].Text = Session["tot_rece"].ToString();
        GridView1.FooterRow.Cells[3].Text = Session["tot_distr"].ToString();
        GridView1.FooterRow.Cells[4].Text = Session["tot_bal"].ToString();
    }
}
