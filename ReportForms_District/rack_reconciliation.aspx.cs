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

public partial class District_food_rpt_rack_reconciliation : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    MoveChallan mobj = null;
    MoveChallan mobj12 = null;

    Commodity_MP comdtobj = null;
    DistributionCenters distobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string getdatef = "";
    public string hname = "";

    public string transid = "";
    public int railnum;
    DataTable dt = new DataTable();
    float disqty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


            if (!IsPostBack)
            {
                GetRack();

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }

    void GetRack()
    {
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());

        ddlrackno.Items.Insert(0, "--Select--");
        string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + distid + "' and Month =" + month + " and Year=" + year;
        cmd.Connection = con;
        cmd.CommandText = qreyrac;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrackno.Items.Add(dr["Rack_No"].ToString());

        }
        dr.Close();
        con.Close();

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DistrictFood/DO_Welcome.aspx");
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
   
   
    void GetData()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
           
        }
        else
        {
            string rackno = ddlrackno.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qrey = "select  Rack_Dispatch_Bulk.*, Commodity_Name,tbl_Rail_Head .RailHead_Name, Rack_Dispatch_Bulk .Rack_Qty,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name,districtsmp.district_name,RH .RailHead_Name as SRH,destdist.district_name as Destdist  from dbo.Rack_Dispatch_Bulk left join dbo.tbl_MetaData_STORAGE_COMMODITY on Rack_Dispatch_Bulk.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID left join pds.districtsmp on Rack_Dispatch_Bulk.district_code=  districtsmp.district_code left join pds.districtsmp as destdist  on Rack_Dispatch_Bulk.Dest_District=destdist.district_code left join dbo.tbl_Rail_Head on Rack_Dispatch_Bulk.Dest_RailHead=tbl_Rail_Head .RailHead_Code left join  dbo.tbl_Rail_Head as RH  on Rack_Dispatch_Bulk.Sending_RailHead=RH .RailHead_Code where Rack_Dispatch_Bulk.district_code='" + distid + "' and Rack_Dispatch_Bulk.Rack_No=" + rackno;
            DataSet ds = mobj.selectAny(qrey);
             if (ds.Tables[0].Rows.Count==0)
            {
                lblrackno.Text = ddlrackno.SelectedValue;
                lblsrh.Text = "";
                lblsdist.Text ="";
                lbldisdate.Text = "";
                lblrqty.Text = "";
                lblcmdty.Text ="";
                lblrrh.Text = "";
                lblrecdist.Text = "";
               
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                lblrackno.Text = ddlrackno.SelectedValue;
                lblsrh.Text = drs["SRH"].ToString();
                lblsdist.Text = drs["district_name"].ToString();
                lbldisdate.Text = getdate(drs["Rack_DispDate"].ToString());
                lblrqty.Text = drs["Rack_Qty"].ToString();
                lblcmdty.Text =drs["Commodity_Name"].ToString();
                lblrrh.Text = drs["RailHead_Name"].ToString();
                lblrecdist.Text = drs["Destdist"].ToString();
                lblrecdqty.Text = "";
                lblrrecddate.Text = ""; 
               
            }
        }
    }
    void GetBags()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {

        }
        else
        {
            string rackno = ddlrackno.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qreysd = "select Sum(Received_Bags) as SentBag,Sum(Disp_Qty) as Disp_Qty  from dbo.Rack_Dispatch_Details  where Rack_Dispatch_Details.district_code='" + distid + "' and Rack_Dispatch_Details.Rack_No=" + rackno;
            DataSet ds = mobj.selectAny(qreysd);
             if (ds.Tables[0].Rows.Count==0)
            {
                lblsntbags.Text = "";
                lblbags.Text ="";
                lblsntqty.Text = "";
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                lblsntbags.Text = drs["SentBag"].ToString();
                lblbags.Text = drs["SentBag"].ToString();
                lblsntqty.Visible = true;
                lblsntqty.Text = drs["Disp_Qty"].ToString();
                lblrecdqty.Text = "";
                lblrrecddate.Text = "";
                
               

            }
        }
    }
    void GetRecdDetails()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {

        }
        else
        {
            string rackno = ddlrackno.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qreysd = "select *  from dbo.Rack_Receipt_Bulk  where Rack_Receipt_Bulk.Sending_District='" + distid + "' or Rack_Receipt_Bulk.district_code='" + distid + "' and Rack_Receipt_Bulk.Rack_No=" + rackno;
            DataSet ds = mobj.selectAny(qreysd);
             if (ds.Tables[0].Rows.Count==0)
            {
                lblrecdqty.Text = "";
                lblrrecddate.Text ="";
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                lblrecdqty.Text= drs["Rack_RecdQty"].ToString();
                lblrrecddate .Text  = getdate(drs["Rack_RecdDate"].ToString());
                



            }
        }
    }
    void GetRect()

    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {

        }
        else
        {
            string rackno = ddlrackno.SelectedValue;
            mobj12 = new MoveChallan(ComObj);
            string qreyfetch = "select *  from  dbo.Rack_Receipt_Bulk where Rack_No=" + rackno;

            DataSet dsfetch = mobj12.selectAny(qreyfetch);




            if (dsfetch.Tables[0].Rows.Count==0)
            {
               
            }
            else
            {
                DataRow drs = dsfetch.Tables[0].Rows[0];
                lblrecdqty.Text = drs["Rack_RecdQty"].ToString();
                lblrrecddate.Text = drs["Rack_RecdDate"].ToString();
              

            }
        }
    }
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
        GetBags();
       
      
    }
    //void GetRackQty()
    //{
    //    if (ddlrackno.SelectedItem.Text == "--Select--")
    //    {
    //    }
    //    else
    //    {
    //        int month = int.Parse(DateTime.Today.Month.ToString());
    //        int year = int.Parse(DateTime.Today.Year.ToString());
    //        string rackno = ddlrackno.SelectedValue;

    //        mobj = new MoveChallan(ComObj);
    //        string qreyrq = "select Sum(Rack_Qty) as Rack_Qty  from dbo.Rack_Dispatch_Bulk where district_code='" + distid + "' and Rack_No=" + rackno + " and Month=" + month + " and Year=" + year;
    //        DataSet ds = mobj.selectAny(qreyrq);
    //         if (ds.Tables[0].Rows.Count==0)
    //        {
    //        }
    //        else
    //        {
    //            DataRow drs = ds.Tables[0].Rows[0];
    //            string flag = "";
    //            if (flag == drs["Rack_Qty"].ToString())
    //            {

    //                txtrackqty.Text = "0";
    //                txtrackqty.ReadOnly = true;

    //            }
    //            else
    //            {
    //                txtrackqty.Text = drs["Rack_Qty"].ToString();
    //                txtrackqty.ReadOnly = true;
    //            }

    //        }

    //    }
    //}
    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }

    protected void ddlrackno_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GetData();
        GetBags();
        GetRecdDetails();
        //GetRect();

    }
}
