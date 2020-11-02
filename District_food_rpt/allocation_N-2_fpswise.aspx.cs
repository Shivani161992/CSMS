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

public partial class allocation_N_2_fpswise : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
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
            LinkButton1.Attributes.Add("onclick", "window.open('printRpt_N-2_fpswise_alloc.aspx',null,'left=300, top=50, height=900, width= 700, status=n o, resizable= no, scrollbars=yes, toolbar= no,location= no, menubar= no');");
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 2).ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.SelectedValue = Session["syear"].ToString();
            ddl_allot_month.SelectedValue = Session["smonth"].ToString();
            get_lead();
            get_data();
            fillgrid();
            grand_total();
            GridView1.AllowPaging = true;
            fillgrid();
        }
    }
    protected void get_lead()
    {
        string dist =Session["dist_id"].ToString();
        string lead_code = Session["ldcode"].ToString();
        string comm = Session["comm"].ToString();
        string str1 = "select * from dbo.m_LeadSoc where LeadSoc_Code='" + lead_code + "'and District_code='" + dist + "'";
        cmd.CommandText = str1;
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            tx_lead.Text = dr["LeadSoc_nameU"].ToString();

        }

        dr.Close();
        con_opdms.Close();
        
        
        
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
    protected void get_data()
    {
        string lead_code = Session["ldcode"].ToString();
        string comm = Session["comm"].ToString();
        string str1 = "SELECT Round(pds.fps_data1.qty_recev,2) as qty_recev, Round(pds.fps_data1.qty_distr,2) as qty_distr, Round(pds.fps_data1.open_st,2) as open_st, pds.fps_master.fps_code, pds.fps_master.fps_name,Round(pds.fps_data1.open_st+pds.fps_data1.qty_recev- pds.fps_data1.qty_distr,2) as Balance FROM   dbo.Lead_soc_fps LEFT JOIN  pds.fps_data ON Lead_soc_fps.fps_code = pds.fps_data.fps_code  LEFT JOIN pds.fps_data1 ON pds.fps_data.sr_no = pds.fps_data1.sr_no INNER JOIN pds.fps_master ON pds.fps_data.fps_code = pds.fps_master.fps_code where  month(pds.fps_data.date_of_reporting)=" + ddl_allot_month.SelectedItem.Value + "and year(pds.fps_data.date_of_reporting)=" + ddd_allot_year.SelectedItem.Text + "  and Lead_soc_fps.LeadSoc_Code='" + lead_code + "'and pds.fps_data1.commodity_code=" + comm + "  order by Lead_soc_fps.fps_code";
        SqlDataAdapter da = new SqlDataAdapter(str1, con_opdms);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds"] = ds;
    }
  
    public void goto_page(object sender, CommandEventArgs e)
    {
        //Used by external paging
        string arg;
        arg = e.CommandArgument.ToString();

        switch (arg)
        {
            case "next":
                //The next Button was Clicked
                if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
                {
                    GridView1.PageIndex += 1;
                }
              
                break;

            case "prev":
                //The prev button was clicked
                if ((GridView1.PageIndex > 0))
                {
                    GridView1.PageIndex -= 1;
                }
                
                break;

            case "last":
                //The Last Page button was clicked
                GridView1.PageIndex = (GridView1.PageCount - 1);
               
                
                break;

            default:
                //The First Page button was clicked
                GridView1.PageIndex = Convert.ToInt32(arg);
               
                break;

                
        }
        fillgrid();
        if ((GridView1.PageIndex ==(GridView1.PageCount - 1)))
        {

            show_total();
        }
       

    }
    protected void fillgrid()
    {
        Panel2.Visible = false ;
        Label1.Text = "DETAILS NOT FOUND !";
        Label1.Visible = true ;
        DataSet ds = (DataSet)Session["ds"];
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
          GridView1.FooterRow.Visible = false;
          Panel2.Visible = true;
          Label1.Visible = false;
          LinkButton1.Enabled = true;
        }

    }
    protected void grand_total()
    {       
        int rowcount = GridView1.Rows.Count;
        int i = 0;
        float tot_ope = 0, tot_rece = 0, tot_distr = 0, tot_bal = 0;
        for (i = 0; i <= rowcount - 1; i++)
        {
            tot_ope = tot_ope + float.Parse(GridView1.Rows[i].Cells[2].Text.ToString());
            tot_rece = tot_rece + float.Parse(GridView1.Rows[i].Cells[3].Text);
            tot_distr = tot_distr + float.Parse(GridView1.Rows[i].Cells[4].Text);
            tot_bal = tot_bal + float.Parse(GridView1.Rows[i].Cells[5].Text);

        }

        Session ["tot_ope"] =System.Math .Round (tot_ope,2).ToString();
        Session["tot_rece"] = System.Math.Round(tot_rece,2).ToString();
        Session["tot_distr"] = System.Math.Round(tot_distr,2).ToString();
        Session["tot_bal"] = System.Math.Round(tot_bal,2).ToString();
        Session["ds_print"] =(DataSet)Session["ds"];

    }
    protected void show_total()
    {
        GridView1.FooterRow.Visible = true;
        GridView1.FooterRow.Cells[2].Text = Session ["tot_ope"].ToString();
        GridView1.FooterRow.Cells[3].Text = Session["tot_rece"].ToString();
        GridView1.FooterRow.Cells[4].Text = Session["tot_distr"].ToString();
        GridView1.FooterRow.Cells[5].Text = Session["tot_bal"].ToString();
    }
   
}