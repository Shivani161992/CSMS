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

public partial class IssueCenter_Print_Truckchalan_FPS : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string chalan_no = "";
    public string distid = "";
    public string Dis_Godown = "";
    decimal grdTotal = 0;
    decimal grdTotalQty = 0;
    string issueid = "";
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            issueid = Session["issue_id"].ToString();
            chalan_no  = Session["doforprint"].ToString();
            distid = Session["dist_id"].ToString();
            Dis_Godown = Session["Dispatch_godown"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            if (!IsPostBack)
            {
                GetDistName();
                Get_Data();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }
    }
    public void FillGrid()
    {
        try
        {
            if (chalan_no != null)
            {
                string query = "select tbl_TruckChalan_FPS.sendto_FPS,(opdms.pds.fps_master.fps_name + '('+tbl_TruckChalan_FPS.sendto_FPS + ')' ) as fps_name,commodity,tbl_Commodity_Hindi .Commodity_Name_Hindi as Commodity_name,tbl_TruckChalan_FPS.Scheme,tbl_MetaData_SCHEME.Scheme_Name as Scheme_name,tbl_TruckChalan_FPS.Qty_send,tbl_TruckChalan_FPS.Bags from dbo.tbl_TruckChalan_FPS left join opdms.pds.fps_master ON tbl_TruckChalan_FPS.sendto_FPS = opdms.pds.fps_master.fps_code AND tbl_TruckChalan_FPS.Sendto_District =  opdms.pds.fps_master.district_code  left join dbo.tbl_Commodity_Hindi on tbl_Commodity_Hindi . Commodity_Id=tbl_TruckChalan_FPS.Commodity left join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.scheme_id=tbl_TruckChalan_FPS.Scheme where tbl_TruckChalan_FPS.Challan_No= '" + chalan_no + "' and tbl_TruckChalan_FPS.Dist_ID ='" + distid + "' and tbl_TruckChalan_FPS.Dispatch_Godown='" + Dis_Godown + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tbl_TruckChalan_FPS");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_fps_Details.DataSource = ds.Tables[0];
                    gv_fps_Details.DataBind();
                    for (int i = 0; i < gv_fps_Details.Rows.Count; i++)
                    {
                        gv_fps_Details.Rows[i].Cells[0].Font.Name = "DVBW-TTYogeshEn";
                        gv_fps_Details.Rows[i].Cells[0].Font.Size = 10;
                        gv_fps_Details.Rows[i].Cells[0].Width = 300;
                    }
                }
            }
            else
            {
                Response.Redirect("~/MainLogin.aspx");

            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again'); </script> ");
        }
    }
    protected void gv_fps_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bags"));
            decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Qty_send"));

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
    void GetDistName()
    {
        mobj = new MoveChallan(ComObj);
        string query = "select district_name from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds = mobj.selectAny(query);
        DataRow dr = ds.Tables[0].Rows[0];
        lbldist.Text = dr["district_name"].ToString();

        mobj = new MoveChallan(ComObj);
        string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
        DataSet dsic = mobj.selectAny(qryissue);
        DataRow dric = dsic.Tables[0].Rows[0];
        lbl_Depot.Text = dric["DepotName"].ToString();
    }
    public void Get_Data()
    {
        try
        {
            if (chalan_no != null)
            {
                string query = "SELECT TOP 1 TFPS.Dispatch_id,TFPS.Depot_Id,TFPS.Book_No,TFPS.Challan_No,TFPS.Challan_Date,TFPS.Builty_No,TFPS.Builty_Date,TFPS.Dispatch_Godown,TFPS.Sendto_District,TFPS.Sendto_Depot,TFPS.sendto_Block,TFPS.sendto_FPS ,TFPS.Commodity,TFPS.Scheme,Bags,TFPS.Qty_send,TFPS.Truck_no,TFPS.Transporter,TFPS.Driver_Name ,TFPS.Dispatch_Time,TFPS.Created_date,Transporter_Table.Transporter_Name FROM tbl_TruckChalan_FPS TFPS inner join Transporter_Table on TFPS.Transporter = Transporter_Table.Transporter_ID  where TFPS.Challan_No= '" + chalan_no + "' and TFPS.Dist_ID ='" + distid + "' and TFPS.Dispatch_Godown='" + Dis_Godown + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tbl_TruckChalan_FPS");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbl_Book_no.Text = ds.Tables[0].Rows[0]["Book_No"].ToString();
                    lbl_Transporter.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lbl_date2.Text = getdate(ds.Tables[0].Rows[0]["Created_date"].ToString());
                    lbl_order_no.Text = ds.Tables[0].Rows[0]["Challan_No"].ToString();
                    lbl_builtyno.Text = ds.Tables[0].Rows[0]["Builty_No"].ToString();
                    lbl_builtydate.Text = getdate(ds.Tables[0].Rows[0]["Builty_Date"].ToString());
                    lbl_Truckno.Text = ds.Tables[0].Rows[0]["Truck_no"].ToString();
                    lbl_LoadDate.Text = getdate(ds.Tables[0].Rows[0]["Challan_Date"].ToString());
                    lbl_LoadTime.Text = ds.Tables[0].Rows[0]["Dispatch_Time"].ToString();
                    lbl_DriverName.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                    FillGrid();
                    Image1.ImageUrl = "~/Images/main1_03.jpg";
                }
            }

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again'); </script> ");
        }
        finally
        {
            //Session.Remove("Complaint_ID");
            //Session.Clear();
            //Session.Abandon();
        }
    }
}
