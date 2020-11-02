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

public partial class District_Print_OpenSale_DO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] == null)
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        else
        {
            if (!IsPostBack)
            {

                string districtid = Session["dist_id"].ToString();

                getdata();

                GetDistName(districtid);

            }

            else
            {

            }
        }
    }


    private void getdata()
    {
        String DONumber = Session["doforprint"].ToString();

        string issuetype = Session["DoSaleType"].ToString();

        if (issuetype == "1")    // Paddy To Miller
        {
            string distcode = Session["dist_id"].ToString();


            string qry = "select OpenSale_DO.delivery_order_no ,CONVERT(nvarchar, OpenSale_DO.Do_Validdate,103)Do_Validdate ,OpenSale_DO.amount, CONVERT(nvarchar, OpenSale_DO.do_date,103)do_date , OpenSale_DO.dd_no ,  CONVERT(nvarchar, OpenSale_DO.dd_date,103)dd_date , OpenSale_DO.tot_amount , Miller_Master.Miller_Name , Bank_Master.Bank_Name from OpenSale_DO inner join Miller_Master on Miller_Master.Miller_ID = OpenSale_DO.Partyname inner join Bank_Master on Bank_Master.Bank_ID = OpenSale_DO.bank_id where delivery_order_no = '" + DONumber + "' and OpenSale_DO.district_code = '" + distcode + "'";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                do_no.Text = DONumber;

                DoValid.Text = ds.Tables[0].Rows[0]["Do_Validdate"].ToString();

                
                lblisname.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString();        // Miller Name

                lblrodate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();            // DO Date

                lbltotamount.Text = ds.Tables[0].Rows[0]["tot_amount"].ToString();      // Total Amt

                lbldraft.Text = ds.Tables[0].Rows[0]["dd_no"].ToString();             // DD Number

                DODate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();
                
                lbldt.Text = ds.Tables[0].Rows[0]["dd_date"].ToString();               // DD Date

                DD_Amt.Text = ds.Tables[0].Rows[0]["amount"].ToString();              // DD Amt

                lblbname.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();            // Bank Name

            }

        }

        else if (issuetype == "2")   // Sale of Damaged Commodity
        {

            string distcode = Session["dist_id"].ToString();


            string qry = "select OpenSale_DO.delivery_order_no ,CONVERT(nvarchar, OpenSale_DO.Do_Validdate,103)Do_Validdate ,OpenSale_DO.amount, CONVERT(nvarchar, OpenSale_DO.do_date,103)do_date , OpenSale_DO.dd_no ,  CONVERT(nvarchar, OpenSale_DO.dd_date,103)dd_date , OpenSale_DO.tot_amount , OpenSaleParty.PartyName , Bank_Master.Bank_Name from OpenSale_DO inner join OpenSaleParty on OpenSaleParty.PartyId = OpenSale_DO.Partyname inner join Bank_Master on Bank_Master.Bank_ID = OpenSale_DO.bank_id where delivery_order_no = '" + DONumber + "' and OpenSale_DO.district_code = '" + distcode + "'";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                do_no.Text = DONumber;

                DoValid.Text = ds.Tables[0].Rows[0]["Do_Validdate"].ToString();


                lblisname.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString();        // Miller Name

                lblrodate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();            // DO Date

                lbltotamount.Text = ds.Tables[0].Rows[0]["tot_amount"].ToString();      // Total Amt

                lbldraft.Text = ds.Tables[0].Rows[0]["dd_no"].ToString();             // DD Number

                DODate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();

                lbldt.Text = ds.Tables[0].Rows[0]["dd_date"].ToString();               // DD Date

                DD_Amt.Text = ds.Tables[0].Rows[0]["amount"].ToString();              // DD Amt

                lblbname.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();            // Bank Name

            }


        }

        else if (issuetype == "3")     // Sale of Sound Commodity
        {
            string distcode = Session["dist_id"].ToString();


            string qry = "select OpenSale_DO.delivery_order_no ,CONVERT(nvarchar, OpenSale_DO.Do_Validdate,103)Do_Validdate ,OpenSale_DO.amount, CONVERT(nvarchar, OpenSale_DO.do_date,103)do_date , OpenSale_DO.dd_no ,  CONVERT(nvarchar, OpenSale_DO.dd_date,103)dd_date , OpenSale_DO.tot_amount , OpenSaleParty.PartyName , Bank_Master.Bank_Name from OpenSale_DO inner join OpenSaleParty on OpenSaleParty.PartyId = OpenSale_DO.Partyname inner join Bank_Master on Bank_Master.Bank_ID = OpenSale_DO.bank_id where delivery_order_no = '" + DONumber + "' and OpenSale_DO.district_code = '" + distcode + "'";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                do_no.Text = DONumber;

                DoValid.Text = ds.Tables[0].Rows[0]["Do_Validdate"].ToString();


                lblisname.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString();        // Miller Name

                lblrodate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();            // DO Date

                lbltotamount.Text = ds.Tables[0].Rows[0]["tot_amount"].ToString();      // Total Amt

                lbldraft.Text = ds.Tables[0].Rows[0]["dd_no"].ToString();             // DD Number

                DODate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();

                lbldt.Text = ds.Tables[0].Rows[0]["dd_date"].ToString();               // DD Date

                DD_Amt.Text = ds.Tables[0].Rows[0]["amount"].ToString();              // DD Amt

                lblbname.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();            // Bank Name

            }
        }

        else   // 4    // Sale of Sweepage Commodity
        {
            string distcode = Session["dist_id"].ToString();


            string qry = "select OpenSale_DO.delivery_order_no ,CONVERT(nvarchar, OpenSale_DO.Do_Validdate,103)Do_Validdate ,OpenSale_DO.amount, CONVERT(nvarchar, OpenSale_DO.do_date,103)do_date , OpenSale_DO.dd_no ,  CONVERT(nvarchar, OpenSale_DO.dd_date,103)dd_date , OpenSale_DO.tot_amount , OpenSaleParty.PartyName , Bank_Master.Bank_Name from OpenSale_DO inner join OpenSaleParty on OpenSaleParty.PartyId = OpenSale_DO.Partyname inner join Bank_Master on Bank_Master.Bank_ID = OpenSale_DO.bank_id where delivery_order_no = '" + DONumber + "' and OpenSale_DO.district_code = '" + distcode + "'";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                do_no.Text = DONumber;

                DoValid.Text = ds.Tables[0].Rows[0]["Do_Validdate"].ToString();


                lblisname.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString();        // Miller Name

                lblrodate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();            // DO Date

                lbltotamount.Text = ds.Tables[0].Rows[0]["tot_amount"].ToString();      // Total Amt

                lbldraft.Text = ds.Tables[0].Rows[0]["dd_no"].ToString();             // DD Number

                DODate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();

                lbldt.Text = ds.Tables[0].Rows[0]["dd_date"].ToString();               // DD Date

                DD_Amt.Text = ds.Tables[0].Rows[0]["amount"].ToString();              // DD Amt

                lblbname.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();            // Bank Name

            }
        }
    }


    void GetDistName(string distid)
    {
        try
        {
           
            string query = "select district_name from pds.districtsmp where district_code='" + distid + "'";

            SqlCommand cmd = new SqlCommand(query, con);

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            lbldist.Text = dr["district_name"].ToString();
            lbldd_district.Text = dr["district_name"].ToString();
        }
        catch (Exception)
        {
        }
    }
}
