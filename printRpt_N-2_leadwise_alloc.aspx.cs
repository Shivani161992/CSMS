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

public partial class printRpt_N_2_leadwise_alloc : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    public  string dist_code = "", lead_code = "", smonth="", syear = "";
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
        dist_code = Session["dist"].ToString();
        lead_code = Session["lead"].ToString();
        smonth = Session["month"].ToString();
        syear = Session["year"].ToString();
        get_distname();
        get_leadname();
        int month = int.Parse(smonth.ToString());
        lbl_month.Text= get_Monthname(month,false);
        lbl_year.Text = syear;
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
        get_Data();
    }
    private static string  get_Monthname(int month,bool xyz)
    {        
        DateTime date = new DateTime(1900,month,1);
        if (xyz) return date.ToString("MMM");
        return date.ToString("MMMM");

    }
    protected void get_leadname()
    {
        cmd.CommandText = "select * from dbo.m_LeadSoc where District_code='" + dist_code + "' and LeadSoc_Code='"+ lead_code +"' ";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {            
           lbl_lead .Text = dr["LeadSoc_nameU"].ToString();
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
            lbl_dist .Text = dr["district_name"].ToString();
        }
        dr.Close();
        con_opdms.Close();
    }
    protected void get_Data()
    {
        Empty_textbox();
        int i = 0;
        float st_qty = 0, rec_qty = 0, distr_qty = 0;
        DataTable dt=(DataTable) Session["dt"];
        for (i = 0; i < dt.Rows.Count; i++)
        {
            int comm_id = int.Parse(dt.Rows[i][0].ToString());
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
        string str2 = "SELECT SUM(pds.fps_allot.rice_apl_alloc) AS rice_apl, SUM(pds.fps_allot.rice_bpl_alloc) AS rice_bpl, SUM(pds.fps_allot.rice_aay_alloc)AS rice_aay, SUM(pds.fps_allot.wheat_apl_alloc) AS wheat_apl, SUM(pds.fps_allot.wheat_bpl_alloc) AS wheat_bpl,SUM(pds.fps_allot.wheat_aay_alloc) AS wheat_aay, SUM(pds.fps_allot.sugar_alloc) AS sugar, SUM(pds.fps_allot.kerosene_alloc)AS kerosene FROM   dbo.Lead_soc_fps INNER JOIN pds.fps_allot ON Lead_soc_fps.fps_code = pds.fps_allot.fps_code where  pds.fps_allot.month=" + smonth + " and pds.fps_allot.year=" + syear + "  and Lead_soc_fps.LeadSoc_Code='" + lead_code + "'";
        cmd.CommandText = str2;
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {

            rice_apl_allot.Text = dr["rice_apl"].ToString();
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
        if (rice_apl_allot.Text == "")
        {
            rice_apl_allot.Text = "0";
        }
        if (rice_bpl_allot.Text == "")
        {
            rice_bpl_allot.Text = "0";
        }
        if (rice_aay_allot.Text == "")
        {
            rice_aay_allot.Text = "0";
        }
        if (wheat_apl_allot.Text == "")
        {
            wheat_apl_allot.Text = "0";
        }
        if (wheat_bpl_allot.Text == "")
        {
            wheat_bpl_allot.Text = "0";
        }
        if (wheat_aay_allot.Text == "")
        {
            wheat_aay_allot.Text = "0";
        }
        if (sugar_allot.Text == "")
        {
            sugar_allot.Text = "0";
        }
        if (kerosene_allot.Text == "")
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
    }
    protected void Empty_textbox()
    {
        rice_apl_st.Text = "";
        rice_bpl_st.Text = "";
        rice_aay_st.Text = "";
        rice_apl_allot.Text = "";
        rice_bpl_allot.Text = "";
        rice_aay_allot.Text = "";
        rice_apl_bal.Text = "";
        rice_bpl_bal.Text = "";
        rice_aay_bal.Text = "";
        wheat_apl_st.Text = "";
        wheat_bpl_st.Text = "";
        wheat_aay_st.Text = "";
        wheat_apl_allot.Text = "";
        wheat_bpl_allot.Text = "";
        wheat_aay_allot.Text = "";
        wheat_apl_bal.Text = "";
        wheat_bpl_bal.Text = "";
        wheat_aay_bal.Text = "";
        sugar_st.Text = "";
        sugar_allot.Text = "";
        sugar_bal.Text = "";
        kerosene_st.Text = "";
        kerosene_allot.Text = "";
        kerosene_bal.Text = "";

        rice_apl_ope.Text = "";
        rice_bpl_ope.Text = "";
        rice_aay_ope.Text = "";
        rice_apl_rec.Text = "";
        rice_bpl_rec.Text = "";
        rice_aay_rec.Text = "";
        rice_apl_distr.Text = "";
        rice_bpl_distr.Text = "";
        rice_aay_distr.Text = "";
        wheat_apl_ope.Text = "";
        wheat_bpl_ope.Text = "";
        wheat_aay_ope.Text = "";
        wheat_apl_rec.Text = "";
        wheat_bpl_rec.Text = "";
        wheat_aay_rec.Text = "";
        wheat_apl_distr.Text = "";
        wheat_bpl_distr.Text = "";
        wheat_aay_distr.Text = "";
        sugar_ope.Text = "";
        sugar_rec.Text = "";
        sugar_distr.Text = "";
        kerosene_ope.Text = "";
        kerosene_rec.Text = "";
        kerosene_distr.Text = "";

    }
}
