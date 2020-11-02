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
public partial class District_Truck_Movement_Print : System.Web.UI.Page
{
    Transporter tobj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string issueid = "";
    DataTable dt = new DataTable();
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string challan = "";
    public string truckno = "";
    public string gatepass = "";
    public int getnum;
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            challan = Session["challan"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
                GetData();
                GetRecipt();
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }



    }
    void GetData()
    {
        string date = getdateM(DateTime.Today.Date.ToString());
        lblgdtae.Text = date;
        mobj1 = new MoveChallan(ComObj);
        string qrey = "select SCSC_Truck_challan.*,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName ,IC.DepotName as ICName, districtsmp.district_Name as district_Name,distmp.district_Name as sendto_dist   from dbo.SCSC_Truck_challan left join pds.districtsmp on SCSC_Truck_challan.Dist_Id=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on SCSC_Truck_challan.Sendto_IC=tbl_MetaData_DEPOT.DepotID left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID left join pds.districtsmp as distmp on SCSC_Truck_challan.Sendto_District=distmp.district_code left join dbo.tbl_MetaData_DEPOT as IC on SCSC_Truck_challan.Depot_Id=IC.DepotID   where SCSC_Truck_challan.Challan_No='" + challan + "'and SCSC_Truck_challan.Dist_ID='" + distid + "'";
        DataSet ds = mobj1.selectAny(qrey);
        DataRow dr = ds.Tables[0].Rows[0];
        lbldistrict.Text = dr["district_Name"].ToString();
        lbldispdist.Text = dr["ICName"].ToString();
        lbldispdate.Text = getdateM(dr["Challan_Date"].ToString());
        lbldispqty .Text = dr["Qty_send"].ToString();
        lbldestination.Text = dr["sendto_dist"].ToString();
        lbldestdepo.Text = dr["DepotName"].ToString();
        lbldisptime .Text = dr["Dispatch_Time"].ToString();
        lbltruckno.Text = dr["Truck_no"].ToString();
        lbltransp.Text = dr["Transporter_Name"].ToString();
        lblchallanno.Text = challan;
              


    }
    void GetRecipt()
    {
        mobj1 = new MoveChallan(ComObj);
        string qrey = "select *  from dbo.tbl_Receipt_Details where challan_no='" + challan + "'and S_of_arrival='02'";
        DataSet ds = mobj1.selectAny(qrey);
         if (ds.Tables[0].Rows.Count==0)
        {
            Label1.Visible = true;
            Label1.Text = "Currently no Receipt Details Found";
        }
        else
        {
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            DataRow dr = ds.Tables[0].Rows[0];
            lblrecdate.Visible = true;
            lblrecdate.Text = getdateM(dr["arrival_date"].ToString());
            lblatime.Visible = true;
            lblatime.Text = dr["Arrival_time"].ToString();
            lblweight.Visible = true;
            lblweight.Text = dr["Recd_Qty"].ToString();
        }
        

    }


    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    public string getdateM(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}
