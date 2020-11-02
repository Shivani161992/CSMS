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
using Data;
using DataAccess;

public partial class IssueCenter_Print_DoorStep_DO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
   // public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());

    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string dono = "";
    public string distid = "";
    public string comdty = "";
    public string sid = "";
    //decimal grdTotal = 0;
    //decimal grdTotalQty = 0;
    //decimal grdTotalAmount = 0;


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

               
                    GetDistName();
                    GetDatafps();
                    getissue();

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


    void Fillgrid()
    {
        try
        {
            //string query1 = "select do_fps.fps_code,(opdms.pds.fps_master.fps_Uname + '('+do_fps.fps_code + ')')  as fps_name,commodity,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_name,do_fps.scheme_id,tbl_MetaData_SCHEME.Scheme_Name as Scheme_name,quantity,rate_per_qtls,quantity*rate_per_qtls as Totalamt from dbo.do_fps left join  opdms.pds.fps_master ON do_fps.fps_code = opdms.pds.fps_master.fps_code AND do_fps.district_code =  opdms.pds.fps_master.district_code  left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY . Commodity_Id=do_fps.Commodity left join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.scheme_id=do_fps.scheme_id where do_fps.delivery_order_no='" + dono + "' and do_fps.district_code ='" + distid + "' and do_fps.issueCentre_code='" + sid + "'";
            string query1 = "select DoorStep_do_fps.commodity,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_name,DoorStep_do_fps.scheme_id,tbl_MetaData_SCHEME.Scheme_Name as Scheme_name,DoorStep_do_fps.fps_name,DoorStep_do_fps.quantity,DoorStep_do_fps.rate_per_qtls,DoorStep_do_fps.quantity*DoorStep_do_fps.rate_per_qtls as Totalamt,DoorStep_DO.tot_amount from DoorStep_do_fps inner join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id=DoorStep_do_fps.commodity inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.scheme_id=DoorStep_do_fps.scheme_id inner join DoorStep_DO on DoorStep_DO.delivery_order_no = '" + dono + "' and DoorStep_do_fps.delivery_order_no = '" + dono + "' and DoorStep_do_fps.district_code ='" + distid + "' and DoorStep_do_fps.issueCentre_code= '" + sid + "'";
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
            lbldist.Text = dr["district_name"].ToString();
        }
        catch (Exception)
        {
        }
    }

    void GetDatafps()
    {

        string query = "SELECT permit_no,convert(nvarchar,permit_date,103)permit_date,DoorStep_DO.DDNum ,isnull(convert(nvarchar,DoorStep_DO.DDDate,103),'')DDDate ,issue_toname,isnull(Bank_Master_New.Bank_Name,'')Bank_Name , DoorStep_DO.allotment_month , DoorStep_DO.allotment_year ,convert(nvarchar,do_date,103)do_date , convert(nvarchar,do_validdate,103)do_validdate , payment_mode ,DDAmount, DoorStep_DO.quantity , tot_amount , DoorStep_do_fps.fps_name , Transporter_Table.Transporter_Name  FROM DoorStep_DO inner join DoorStep_do_fps on DoorStep_do_fps.delivery_order_no = DoorStep_DO.delivery_order_no and DoorStep_do_fps.issueCentre_code = DoorStep_DO.issueCentre_code inner join Transporter_Table on Transporter_Table.Transporter_ID = DoorStep_DO.Transporter and Transporter_Table.Distt_ID = DoorStep_DO.district_code  left join Bank_Master_New on Bank_Master_New.Bank_ID = DoorStep_DO.BankName  where DoorStep_DO.delivery_order_no='" + dono + "' and DoorStep_DO.district_code ='" + distid + "'and DoorStep_DO.issueCentre_code='" + sid + "'";

        SqlCommand cmd = new SqlCommand(query, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            string month = dr["allotment_month"].ToString();
          
            int monthn = int.Parse(month);

            lblmonth.Text = GetMonthName(monthn, false);

           // lblmonth.Text = "";

            lblyear.Text = dr["allotment_year"].ToString();

            do_no.Text = dono; 

            //lbldist.Text = "";   // Dist Name

            lblissudt.Text = dr["do_date"].ToString();    //  DO Date

            if (lblissudt.Text == "01/01/1900")
            {
                lblissudt.Text = "";
            }

           // lbldt.Text = dr["do_date"].ToString();

            lblvaliddt.Text = dr["do_validdate"].ToString();      // Validity Date

            lblisname.Text = dr["issue_toname"].ToString();   // Issue Name

            if (lblisname.Text =="")
            {
                lblisname.Text = dr["fps_name"].ToString();   // FPS Name but will be wrong in case of Multiple FPS 
            }

            transName.Text = dr["Transporter_Name"].ToString();   // Transporter Name

            lblper.Text = dr["permit_no"].ToString();  // Permit Num

            lblrodate.Text = dr["permit_date"].ToString();   // permit_date

            lbltotamount.Text = dr["tot_amount"].ToString();    // Total Amt

            lblpayoption.Text = dr["payment_mode"].ToString();   // Payoption

            lblbname.Text = dr["Bank_Name"].ToString();    // Bank Name

            ddNum.Text = dr["DDNum"].ToString();    // DD Number

            ddamt.Text = dr["DDAmount"].ToString();

            lbldt.Text = dr["DDDate"].ToString();

            

            if (lbldt.Text == "01/01/1900")
            {
                lbldt.Text = "";
            }

            //
            if (lblpayoption.Text == "Cr")
            {
                lblpayoption.Text = "Credit";
            }

            if (lblpayoption.Text == "C")
            {
                lblpayoption.Text = "DD/Pay Order/Cheque";
            }

            if (lblbname.Text == "")
            {
                lblpayoption.Text = "Free Scheme";
            }

            //lbldd_district.Text = "";    // Dist Name
      

        }
            

       Fillgrid();
    }


    private static string GetMonthName(int month, bool abbrev)
    {
        DateTime date = new DateTime(1900, month, 1);
        if (abbrev) return date.ToString("MMM");
        return date.ToString("MMMM");
    }


    public void getissue()
    {

        string issname = "Select DepotName from tbl_MetaData_DEPOT where DepotID = '"+sid+"'";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdissue = new SqlCommand(issname, con);

        SqlDataAdapter daissue = new SqlDataAdapter(cmdissue);

        DataSet dsissue = new DataSet();

        daissue.Fill(dsissue);

        if (dsissue.Tables[0].Rows.Count > 0)
        {
            string issuename = dsissue.Tables[0].Rows[0]["DepotName"].ToString();

            ICName.Text = issuename;

        }




    }




}
