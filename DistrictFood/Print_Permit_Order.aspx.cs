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
public partial class District_Print_Permit_Order : System.Web.UI.Page
{
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string permit_no = "";
    public string distid = "";
    public string comdty = "";
    decimal grdTotal = 0;
    decimal grdTotalQty = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        if (Session["dist_id"] != null)
        {
            permit_no = Session["permit_print"].ToString();
            distid = Session["dist_id"].ToString();
            //comdty = Session["comodty"].ToString();
            if (!IsPostBack)
            {
                GetDistName();
                GetData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
           
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            grdTotal = grdTotal + rowTotal;
            grdTotalQty = grdTotalQty + rowTotalQty;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbl = (Label)e.Row.FindControl("lblTotal");
            Label lblqty = (Label)e.Row.FindControl("lblTotalQty");

            lbl.Text = grdTotal.ToString("");
            lblqty.Text = grdTotalQty.ToString("");

        } 

    }

    void GetData()
    {

        mobj = new MoveChallan(ComObj);
        string query = "select Dccb_Permit.*,m_LeadSoc.LeadSoc_nameU from dbo.Dccb_Permit left join dbo.m_LeadSoc on Dccb_Permit.issue_name=m_LeadSoc.LeadSoc_Code  where Dccb_Permit.Permit_order_no='" + permit_no + "' and Dccb_Permit.district_code ='" + distid + "'";
        DataSet ds = mobj.selectAny(query);
        DataRow dr = ds.Tables[0].Rows[0];
        string daten = dr["permit_date"].ToString();
        DateTime dt = Convert.ToDateTime(daten);
        int validty = int.Parse(dr["permit_validity"].ToString());
        string vvdt = changeDate(dt, validty);

        string gdaten = getdate(daten);
        lblissudt.Text = gdaten;
        //DateTime vadate =gdaten ;

        //int day = vadate.Day + int.Parse(dr["do_validity"].ToString());
        //string ym = vadate.Month.ToString () + "/" + vadate.Year.ToString();
        //string days = Convert.ToString (day);
        //string val_date =days +"/" + ym;
        lblvaliddt.Text = vvdt;
        lblisname.Text = dr["LeadSoc_nameU"].ToString();
        lblper.Text = dr["Permit_order_no"].ToString();
        //lblrodate.Text = dr["permit_date"].ToString();
        string permit = dr["permit_date"].ToString();
        string permitdt = getdate(permit);
        lblrodate.Text = permitdt;
        //lblcomodt.Text = dr["commodity"].ToString();
        //lblqtl.Text = dr["quantity"].ToString();
        //lblrate.Text = dr["rate_per_qtls"].ToString();
        //lbltotrs.Text = Convert.ToString((Convert.ToInt32(dr["quantity"].ToString()) * Convert.ToInt32(dr["rate_per_qtls"].ToString())));
        lbldraft.Text = dr["dd_no"].ToString();
        string month = dr["allotment_month"].ToString();
        //lblyear.Text = dr["allotment_year"].ToString();
        int monthn = int.Parse(month);



        lblmonth.Text = GetMonthName(monthn, false);

        string draft = dr["dd_date"].ToString();
        string dddt = getdate(draft);
        lbldt.Text = dddt;
        lblper_no.Text = permit_no ;

        Fillgrid();










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
        //string query = "select fps_code,commodity,scheme_id,quantity,rate_per_qtls,quantity*rate_per_qtls as Totalamt from dbo.Dccb_Permit_FPS_Detail where delivery_order_no='" + dono + "' and district_code ='" + distid + "'";
        string query1 = "select Dccb_Permit_FPS_Detail.fps_code,pds.fps_master.fps_name as fps_name,commodity,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_name,Dccb_Permit_FPS_Detail.scheme_id,tbl_MetaData_SCHEME.Scheme_Name as Scheme_name,quantity,rate_per_qtls,quantity*rate_per_qtls as Totalamt from dbo.Dccb_Permit_FPS_Detail left join pds.fps_master on Dccb_Permit_FPS_Detail.fps_code=fps_master.fps_code left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY . Commodity_Id=Dccb_Permit_FPS_Detail.Commodity left join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.scheme_id=Dccb_Permit_FPS_Detail.scheme_id where Dccb_Permit_FPS_Detail.Permit_order_no='" + permit_no + "' and Dccb_Permit_FPS_Detail.district_code ='" + distid + "'";
        DataSet ds = mobj.selectAny(query1);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridView1.Rows[i].Cells[0].Font.Name = "DVBW-TTYogeshEn";
            GridView1.Rows[i].Cells[0].Font.Size = 13;
            GridView1.Rows[i].Cells[0].Width = 300;

        }

    }
    void GetDistName()
    {
        mobj = new MoveChallan(ComObj);
        string query = "select district_name from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds = mobj.selectAny(query);
        DataRow dr = ds.Tables[0].Rows[0];
        lbldistrict.Text = dr["district_name"].ToString();
        lbltodist.Text = dr["district_name"].ToString();
        lblbranch_dist.Text = dr["district_name"].ToString();

    }
}
