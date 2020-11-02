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
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class print_DeleveryOrder : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());

    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string dono = "";
    public string distid = "";
    public string comdty = "";
    public string sid = "";
    decimal grdTotal = 0;
    decimal grdTotalQty = 0;
    decimal grdTotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            dono = Session["doforprint"].ToString();
            distid = Session["dist_id"].ToString();
            sid = Session["issue_id"].ToString();
            //comdty = Session["comodty"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string getdotype = "select issue_type from delivery_order_mpscsc where delivery_order_no = '" + dono + "'";
                SqlCommand cmd = new SqlCommand(getdotype, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                string typeis = ds.Tables[0].Rows[0]["issue_type"].ToString();


                if (typeis == "MPSCSC" || typeis == "F")
                {

                    if (con_opdms.State == ConnectionState.Closed)
                    {
                        con_opdms.Open();
                    }

                    lblisname.Text = "MPSCSC";

                    GetDistName();
                    GetDatafps();
                }

                else
                {
                    GetDistName();
                    GetData();
                }

                if (con_opdms.State == ConnectionState.Open)
                {
                    con_opdms.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));
            ////decimal rowTotalPaidAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "amount"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

            //grdTotalAmount = grdTotalAmount + rowTotalPaidAmt;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Label lbl = (Label)e.Row.FindControl("lblTotal");
            //Label lblqty = (Label)e.Row.FindControl("lblTotalQty");

            //Label lblPtot = (Label)e.Row.FindControl("lblPaidTotal");

            //lbl.Text = grdTotal.ToString("");
            //lblqty.Text = grdTotalQty.ToString("");

            //lblPtot.Text = grdTotalAmount.ToString("");
        }

    }
    protected string changeDate(DateTime inDate, int inDays)
    {
        int noofdays = DateTime.DaysInMonth(inDate.Year, inDate.Month);
        int count = 1;
        int xday = inDate.Day;
        int xmonth = inDate.Month;
        int xyear = inDate.Year;
        while (count <= inDays)
        {
            xday = xday + 1;
            if (xday > noofdays)
            {
                xday = 1;
                xmonth = xmonth + 1;
                if (xmonth > 12)
                {
                    xyear = xyear + 1;
                    xmonth = 1;
                }
                noofdays = DateTime.DaysInMonth(xyear, xmonth);
            }
            count = count + 1;
        }
        return (xday + "/" + xmonth + "/" + xyear);
    }
    void GetData()
    {
        mobj = new MoveChallan(ComObj);
        string query = "select delivery_order_mpscsc.*,m_LeadSoc.LeadSoc_nameU  as LeadSoc_name, Bank_Master.Bank_Name as Bank_Name from dbo.delivery_order_mpscsc left join  opdms.dbo. m_LeadSoc ON delivery_order_mpscsc.issue_name = m_LeadSoc.LeadSoc_Code left join Bank_Master on Bank_Master.Bank_ID = delivery_order_mpscsc.bank_id  left join  do_fps ON do_fps.delivery_order_no = delivery_order_mpscsc.delivery_order_no left join  opdms.pds.fps_master ON do_fps.fps_code = opdms.pds.fps_master.fps_code AND do_fps.district_code =  opdms.pds.fps_master.district_code where delivery_order_mpscsc.delivery_order_no='" + dono + "' and delivery_order_mpscsc.district_code ='" + distid + "'and delivery_order_mpscsc.issueCentre_code='" + sid + "'";
        DataSet ds = mobj.selectAny(query);
        DataRow dr = ds.Tables[0].Rows[0];

        string daten = dr["do_date"].ToString();
        string gdaten = getdate(daten);
        lblissudt.Text = gdaten;

        DateTime dt = Convert.ToDateTime(daten);
        int validty = int.Parse(dr["do_validity"].ToString());
        string vvdt = changeDate(dt, validty);

        lblvaliddt.Text = vvdt;
        lblisname.Text = dr["LeadSoc_name"].ToString();
        lblbname.Text = dr["Bank_Name"].ToString();
        lblper.Text = dr["permit_no"].ToString();

        string permit = dr["permit_date"].ToString();
        string permitdt = getdate(permit);
        lblrodate.Text = permitdt;

        lbldraft.Text = dr["dd_no"].ToString();
        string month = dr["allotment_month"].ToString();
        lblyear.Text = dr["allotment_year"].ToString();
        int monthn = int.Parse(month);

        lblmonth.Text = GetMonthName(monthn, false);
        string draft = dr["dd_date"].ToString();
        string dddt = getdate(draft);
        lbldt.Text = dddt;
        do_no.Text = dono;

        lbldepositamt.Text = dr["amount"].ToString();

        lbltotamount.Text = dr["amount"].ToString();

        GridView1.Visible = false;
        GridView1.Height = 1;
       // Fillgrid();
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    private static string GetMonthName(int month, bool abbrev)
    {
        DateTime date = new DateTime(1900, month, 1);
        if (abbrev) return date.ToString("MMM");
        return date.ToString("MMMM");
    }
    void Fillgrid()
    {
        try
        {
            //string query1 = "select do_fps.fps_code,(opdms.pds.fps_master.fps_Uname + '('+do_fps.fps_code + ')')  as fps_name,commodity,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_name,do_fps.scheme_id,tbl_MetaData_SCHEME.Scheme_Name as Scheme_name,quantity,rate_per_qtls,quantity*rate_per_qtls as Totalamt from dbo.do_fps left join  opdms.pds.fps_master ON do_fps.fps_code = opdms.pds.fps_master.fps_code AND do_fps.district_code =  opdms.pds.fps_master.district_code  left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY . Commodity_Id=do_fps.Commodity left join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.scheme_id=do_fps.scheme_id where do_fps.delivery_order_no='" + dono + "' and do_fps.district_code ='" + distid + "' and do_fps.issueCentre_code='" + sid + "'";
            string query1 = "select do_fps.commodity,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_name,do_fps.scheme_id,tbl_MetaData_SCHEME.Scheme_Name as Scheme_name,(select fps_Uname from [opdms].[pds].[fps_master] where [opdms].[pds].[fps_master].fps_code = do_fps.fps_code)fps_name,do_fps.quantity,do_fps.rate_per_qtls,do_fps.quantity*do_fps.rate_per_qtls as Totalamt,delivery_order_mpscsc.tot_amount from do_fps inner join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id=do_fps.commodity inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.scheme_id=do_fps.scheme_id inner join delivery_order_mpscsc on delivery_order_mpscsc.delivery_order_no = '" + dono + "' and do_fps.delivery_order_no = '" + dono + "' and do_fps.district_code ='" + distid + "' and do_fps.issueCentre_code= '" + sid + "'";
            //string query1 = "select delivery_order_mpscsc.commodity_id,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_name,delivery_order_mpscsc.scheme_id,tbl_MetaData_SCHEME.Scheme_Name as Scheme_name,quantity,rate_per_qtls,quantity*rate_per_qtls as Totalamt,delivery_order_mpscsc.amount from delivery_order_mpscsc inner join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id=delivery_order_mpscsc.commodity_id inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.scheme_id=delivery_order_mpscsc.scheme_id where delivery_order_mpscsc.delivery_order_no='" + dono + "' and delivery_order_mpscsc.district_code ='" + distid + "' and delivery_order_mpscsc.issueCentre_code='" + sid + "'";
            DataSet ds = mobj.selectAny(query1);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                // GridView1.Rows[i].Cells[0].Font.Name = "DVBW-TTYogeshEn";
                GridView1.Rows[i].Cells[0].Font.Size = 10;
                GridView1.Rows[i].Cells[0].Width = 300;

                lbltotamount.Text = ds.Tables[0].Rows[0]["tot_amount"].ToString();             
            }
        }
        catch (Exception)
        {

        }
    }

    void GetDistName()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string query = "select district_name from pds.districtsmp where district_code='" + distid + "'";
            DataSet ds = mobj.selectAny(query);
            DataRow dr = ds.Tables[0].Rows[0];
            lbldist.Text = dr["district_name"].ToString();
            lbldd_district.Text = dr["district_name"].ToString();
        }
        catch (Exception)
        {
        }
    }

    void GetDatafps()
    {
        mobj = new MoveChallan(ComObj);
        string query = "select delivery_order_mpscsc.*,m_LeadSoc.LeadSoc_nameU  as LeadSoc_name, Bank_Master.Bank_Name as Bank_Name from dbo.delivery_order_mpscsc left join  opdms.dbo. m_LeadSoc ON delivery_order_mpscsc.issue_name = m_LeadSoc.LeadSoc_Code left join Bank_Master on Bank_Master.Bank_ID = delivery_order_mpscsc.bank_id  left join  do_fps ON do_fps.delivery_order_no = delivery_order_mpscsc.delivery_order_no left join  opdms.pds.fps_master ON do_fps.fps_code = opdms.pds.fps_master.fps_code AND do_fps.district_code =  opdms.pds.fps_master.district_code where delivery_order_mpscsc.delivery_order_no='" + dono + "' and delivery_order_mpscsc.district_code ='" + distid + "'and delivery_order_mpscsc.issueCentre_code='" + sid + "'";
        DataSet ds = mobj.selectAny(query);
        DataRow dr = ds.Tables[0].Rows[0];

        string daten = dr["do_date"].ToString();
        string gdaten = getdate(daten);
        lblissudt.Text = gdaten;

        DateTime dt = Convert.ToDateTime(daten);
        int validty = int.Parse(dr["do_validity"].ToString());
        string vvdt = changeDate(dt, validty);

        lblvaliddt.Text = vvdt;

        lblbname.Text = dr["Bank_Name"].ToString();
        lblper.Text = dr["permit_no"].ToString();

        string permit = dr["permit_date"].ToString();
        string permitdt = getdate(permit);
        lblrodate.Text = permitdt;

        lbldraft.Text = dr["dd_no"].ToString();
        string month = dr["allotment_month"].ToString();
        lblyear.Text = dr["allotment_year"].ToString();
        int monthn = int.Parse(month);

        lbltotamount.Text = dr["tot_amount"].ToString();
        
        lblmonth.Text = GetMonthName(monthn, false);
        string draft = dr["dd_date"].ToString();
        string dddt = getdate(draft);
        lbldt.Text = dddt;
        do_no.Text = dono;
        GridView1.Height = 180;
        GridView1.Visible = true;

        lbldepositamt.Text = dr["amount"].ToString();


        Fillgrid();
    }

}
