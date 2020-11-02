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

public partial class allocation_N_2_leadwise : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
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
           
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 2).ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            get_monthyear();
            get_distname();
            get_lead();            
        }
        
        rice_apl_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_bpl_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_aay_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_apl_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_bpl_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_aay_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_apl_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_bpl_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_aay_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_apl_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_bpl_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_aay_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_apl_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_bpl_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_aay_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_apl_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_bpl_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_aay_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        sugar_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        sugar_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        sugar_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        kerosene_st.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        kerosene_allot.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        kerosene_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

        rice_apl_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_bpl_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_aay_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_apl_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_bpl_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_aay_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_apl_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_bpl_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        rice_aay_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_apl_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_bpl_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_aay_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_apl_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_bpl_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_aay_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_apl_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_bpl_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        wheat_aay_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        sugar_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        sugar_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        sugar_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        kerosene_ope.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        kerosene_rec.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        kerosene_distr.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();




    }
   
    protected void get_lead()
    {
        string dist =Session["dist_id"].ToString();
        ddl_lead.Items.Clear();
        cmd.CommandText = "select * from dbo.m_LeadSoc where District_code='" + dist + "'";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["LeadSoc_nameU"].ToString();
            lstitem.Value = dr["LeadSoc_Code"].ToString();
            ddl_lead.Items.Add(lstitem);
        }
        ListItem lstitem1 = new ListItem();
        lstitem1.Text = "pq«Uk;¢";
        lstitem1.Value = "N";
        ddl_lead.Items.Insert(0, lstitem1);
        dr.Close();
        con_opdms.Close();
    }
    protected void get_distname()
    {
        string dist =Session["dist_id"].ToString();

        cmd.CommandText = "select * from pds.districtsmp where district_code='" + dist + "'";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            Label1.Text = dr["district_name"].ToString();   
        }
       
        dr.Close();
        con_opdms.Close();
    }
    protected void ddl_lead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_lead.SelectedItem.Text == "pq«Uk;¢")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Lead Society Name ...');</script>");
            Empty_taxtbox();
            LinkButton1.Enabled = false;
            LinkButton2.Enabled = false;
            LinkButton3.Enabled = false;
            LinkButton4.Enabled = false;
            LinkButton5.Enabled = false;
            LinkButton6.Enabled = false;
            LinkButton7.Enabled = false;
            LinkButton8.Enabled = false;
        }
        else
        {
            Empty_taxtbox();
            string lead = ddl_lead.SelectedItem.Value;
            int i = 0;
            float st_qty = 0, rec_qty = 0, distr_qty = 0;
            string str1 = "SELECT    pds.fps_data1.commodity_code,round(sum( pds.fps_data1.qty_recev),2) as rec_qty,round(sum(pds.fps_data1.qty_distr),2) as distr_qty,round(sum(pds.fps_data1.open_st),2) as st_qty FROM  dbo.Lead_soc_fps LEFT JOIN  pds.fps_data ON Lead_soc_fps.fps_code = pds.fps_data.fps_code  LEFT JOIN pds.fps_data1 ON pds.fps_data.sr_no = pds.fps_data1.sr_no where  month(pds.fps_data.date_of_reporting)=" + ddl_allot_month.SelectedItem.Value + " and year(pds.fps_data.date_of_reporting)=" + ddd_allot_year.SelectedItem.Text + "  and Lead_soc_fps.LeadSoc_Code='" + lead + "' and pds.fps_data1.commodity_code<>'8' group by pds.fps_data1.commodity_code";
            SqlDataAdapter da = new SqlDataAdapter(str1,con_opdms);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Session["dt"] = dt;
            for (i = 0; i <dt.Rows .Count ; i++)
            {
                int comm_id=int.Parse(dt.Rows[i][0].ToString());
                rec_qty = float.Parse(dt.Rows[i][1].ToString());
                distr_qty = float.Parse(dt.Rows[i][2].ToString());
                st_qty = float.Parse(dt.Rows[i][3].ToString());              
                
                if (comm_id == 1)
                {
                    rice_apl_ope.Text = st_qty.ToString();
                    rice_apl_rec.Text = rec_qty.ToString();
                    rice_apl_distr.Text = distr_qty.ToString();
                    rice_apl_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }
                if (comm_id == 2)
                {
                    rice_bpl_ope.Text = st_qty.ToString();
                    rice_bpl_rec.Text = rec_qty.ToString();
                    rice_bpl_distr.Text = distr_qty.ToString();
                    rice_bpl_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }
                if (comm_id == 3)
                {
                    rice_aay_ope.Text = st_qty.ToString();
                    rice_aay_rec.Text = rec_qty.ToString();
                    rice_aay_distr.Text = distr_qty.ToString();
                    rice_aay_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }
                if (comm_id == 4)
                {
                    wheat_apl_ope.Text = st_qty.ToString();
                    wheat_apl_rec.Text = rec_qty.ToString();
                    wheat_apl_distr.Text = distr_qty.ToString();
                    wheat_apl_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }
                if (comm_id == 5)
                {
                    wheat_bpl_ope.Text = st_qty.ToString();
                    wheat_bpl_rec.Text = rec_qty.ToString();
                    wheat_bpl_distr.Text = distr_qty.ToString();
                    wheat_bpl_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }
                if (comm_id == 6)
                {
                    wheat_aay_ope.Text = st_qty.ToString();
                    wheat_aay_rec.Text = rec_qty.ToString();
                    wheat_aay_distr.Text = distr_qty.ToString();
                    wheat_aay_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }
                if (comm_id == 7)
                {
                    sugar_ope.Text = st_qty.ToString();
                    sugar_rec.Text = rec_qty.ToString();
                    sugar_distr.Text = distr_qty.ToString();
                    sugar_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }
                if (comm_id == 9)
                {
                    kerosene_ope.Text = st_qty.ToString();
                    kerosene_rec.Text = rec_qty.ToString();
                    kerosene_distr.Text = distr_qty.ToString();
                    kerosene_st.Text = (st_qty + rec_qty - distr_qty).ToString();
                }

            }
            string str2 = "SELECT SUM(pds.fps_allot.rice_apl_alloc) AS rice_apl, SUM(pds.fps_allot.rice_bpl_alloc) AS rice_bpl, SUM(pds.fps_allot.rice_aay_alloc)AS rice_aay, SUM(pds.fps_allot.wheat_apl_alloc) AS wheat_apl, SUM(pds.fps_allot.wheat_bpl_alloc) AS wheat_bpl,SUM(pds.fps_allot.wheat_aay_alloc) AS wheat_aay, SUM(pds.fps_allot.sugar_alloc) AS sugar, SUM(pds.fps_allot.kerosene_alloc)AS kerosene FROM   dbo.Lead_soc_fps INNER JOIN pds.fps_allot ON Lead_soc_fps.fps_code = pds.fps_allot.fps_code where  pds.fps_allot.month=" + ddl_allot_month.SelectedItem.Value + " and pds.fps_allot.year=" + ddd_allot_year.SelectedItem.Text + "  and Lead_soc_fps.LeadSoc_Code='" + lead + "'";
            cmd.CommandText = str2;
            cmd.Connection = con_opdms;
            con_opdms.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                rice_apl_allot.Text =dr["rice_apl"].ToString();
                rice_bpl_allot.Text = dr["rice_bpl"].ToString();
                rice_aay_allot.Text = dr["rice_aay"].ToString();
                wheat_apl_allot.Text = dr["wheat_apl"].ToString();
                wheat_bpl_allot.Text = dr["wheat_bpl"].ToString();
                wheat_aay_allot.Text = dr["wheat_aay"].ToString();
                sugar_allot.Text = dr["sugar"].ToString();
                kerosene_allot.Text = dr["kerosene"].ToString();

            }

            dr.Close();
            con_opdms.Close();
            if(rice_apl_allot.Text=="")
            {
            rice_apl_allot.Text="0";
            }
            if(rice_bpl_allot.Text=="")
            {
                rice_bpl_allot.Text = "0";
            }
            if(rice_aay_allot.Text=="")
            {
                rice_aay_allot.Text = "0";
            }
            if(wheat_apl_allot.Text=="")
            {
                wheat_apl_allot.Text = "0";
            }
            if(wheat_bpl_allot.Text=="")
            {
                wheat_bpl_allot.Text = "0";
            }
            if(wheat_aay_allot.Text=="")
            {
                wheat_aay_allot.Text = "0";
            }
            if(sugar_allot.Text=="")
            {
                sugar_allot.Text = "0";
            }
            if(kerosene_allot.Text=="")
            {
                kerosene_allot.Text = "0";
            }
            if (rice_apl_st.Text == "")
            {
                rice_apl_st.Text = "0";
            }
            if (rice_bpl_st.Text == "")
            {
                rice_bpl_st.Text = "0";
            }
            if (rice_aay_st.Text == "")
            {
                rice_aay_st.Text = "0";
            }
            if (wheat_apl_st.Text == "")
            {
                wheat_apl_st.Text = "0";
            }
            if (wheat_bpl_st.Text == "")
            {
                wheat_bpl_st.Text = "0";
            }
            if (wheat_aay_st.Text == "")
            {
                wheat_aay_st.Text = "0";
            }
            if (sugar_st.Text == "")
            {
                sugar_st.Text = "0";
            }
            if (kerosene_st.Text == "")
            {
                kerosene_st.Text = "0";
            }

            rice_apl_bal.Text = (float.Parse(rice_apl_allot.Text) - float.Parse(rice_apl_st.Text)).ToString();
            rice_bpl_bal.Text = (float.Parse(rice_bpl_allot.Text) - float.Parse(rice_bpl_st.Text)).ToString();
            rice_aay_bal.Text = (float.Parse(rice_aay_allot.Text) - float.Parse(rice_aay_st.Text)).ToString();
            wheat_apl_bal.Text = (float.Parse(wheat_apl_allot.Text) - float.Parse(wheat_apl_st.Text)).ToString();
            wheat_bpl_bal.Text = (float.Parse(wheat_bpl_allot.Text) - float.Parse(wheat_bpl_st.Text)).ToString();
            wheat_aay_bal.Text = (float.Parse(wheat_aay_allot.Text) - float.Parse(wheat_aay_st.Text)).ToString();
            sugar_bal.Text = (float.Parse(sugar_allot.Text) - float.Parse(sugar_st.Text)).ToString();
            kerosene_bal.Text = (float.Parse(kerosene_allot.Text) - float.Parse(kerosene_st.Text)).ToString();
            LinkButton1.Enabled = true;
            LinkButton2.Enabled = true;
            LinkButton3.Enabled = true;
            LinkButton4.Enabled = true;
            LinkButton5.Enabled = true;
            LinkButton6.Enabled = true;
            LinkButton7.Enabled = true;
            LinkButton8.Enabled = true;
            LinkButton10.Enabled = true;
        }
    }
    protected void get_monthyear()
    {
        int month_id = DateTime.Today.Month;
        int year_id = DateTime.Today.Year;
        month_id = month_id - 2;
        
        if (month_id < 1)
        {
            year_id = year_id - 1;

            if (month_id ==0)
            {
               month_id =12;

            }
            if (month_id == -1)
            {
                month_id = 11;

            }
            
        }
        ddd_allot_year.SelectedValue   =year_id.ToString();
        ddl_allot_month.SelectedIndex = month_id - 1;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
        Session["comm"] = "1";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        Session["comm"] = "2";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        Session["comm"] = "3";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {

        Session["comm"] = "4";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {

        Session["comm"] = "5";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {

        Session["comm"] = "6";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {

        Session["comm"] = "7";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {

        Session["comm"] = "9";
        Session["ldcode"] = ddl_lead.SelectedItem.Value;
        Session["smonth"] = ddl_allot_month.SelectedItem.Value;
        Session["syear"] = ddd_allot_year.SelectedItem.Text;
        Response.Redirect("allocation_N-2_fpswise.aspx");
    }
    protected void Empty_taxtbox()
    {
        rice_apl_st.Text ="";
        rice_bpl_st.Text ="";
        rice_aay_st.Text ="";
        rice_apl_allot.Text ="";
        rice_bpl_allot.Text ="";
        rice_aay_allot.Text ="";
        rice_apl_bal.Text ="";
        rice_bpl_bal.Text ="";
        rice_aay_bal.Text ="";
        wheat_apl_st.Text ="";
        wheat_bpl_st.Text ="";
        wheat_aay_st.Text ="";
        wheat_apl_allot.Text ="";
        wheat_bpl_allot.Text ="";
        wheat_aay_allot.Text ="";
        wheat_apl_bal.Text ="";
        wheat_bpl_bal.Text ="";
        wheat_aay_bal.Text ="";
        sugar_st.Text ="";
        sugar_allot.Text ="";
        sugar_bal.Text ="";
        kerosene_st.Text ="";
        kerosene_allot.Text ="";
        kerosene_bal.Text ="";

        rice_apl_ope.Text ="";
        rice_bpl_ope.Text ="";
        rice_aay_ope.Text ="";
        rice_apl_rec.Text ="";
        rice_bpl_rec.Text ="";
        rice_aay_rec.Text ="";
        rice_apl_distr.Text ="";
        rice_bpl_distr.Text ="";
        rice_aay_distr.Text ="";
        wheat_apl_ope.Text ="";
        wheat_bpl_ope.Text ="";
        wheat_aay_ope.Text ="";
        wheat_apl_rec.Text ="";
        wheat_bpl_rec.Text ="";
        wheat_aay_rec.Text ="";
        wheat_apl_distr.Text ="";
        wheat_bpl_distr.Text ="";
        wheat_aay_distr.Text ="";
        sugar_ope.Text ="";
        sugar_rec.Text ="";
        sugar_distr.Text ="";
        kerosene_ope.Text ="";
        kerosene_rec.Text ="";
        kerosene_distr.Text ="";

    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/MainLogin.aspx");
    }

    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["dist"]= Session["dist_id"].ToString();
        Session["lead"]= ddl_lead.SelectedItem.Value; 
        Session["month"] = ddl_allot_month.SelectedItem.Value;
        Session["year"] = ddd_allot_year.SelectedItem.Text;
        LinkButton10.Attributes.Add("onclick", "window.open('printRpt_N-2_leadwise_alloc.aspx',null,'left=300, top=75, height=700, width= 600, status=n o, resizable= no, scrollbars=yes, toolbar= no,location= no, menubar= no');");
    }
}
