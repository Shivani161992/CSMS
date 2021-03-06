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
public partial class District_Transport_Order_Other : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Districts DObj = null;
    MoveChallan mobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
   
    public string distid = "";
    public string islifted = "N";
    public string source = "";
    public string source_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            source = Session["Source"].ToString();
            source_id = Session["Sorce_ID"].ToString();

            txtsendqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
           

            txttorderno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtsendqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            



            //txtchallan.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtqtysend.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");



            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                DaintyDate1.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                DaintyDate2.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

                Fillgrid();
                GetTransport();
                GetName();
                GetDist();
                GetCommodity();
                GetScheme();
                GetSrcDist();
                Label3.Text = source;
                ddlsrcdist.SelectedValue  = distid;
                ddldistrict.SelectedValue = distid;
                ddlallotyear.Items.Add(DateTime.Today.Year.ToString());
                ddlallotyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());               
                ddlallotyear.SelectedIndex = 1;
                ddlalotmm.SelectedIndex = DateTime.Today.Month - 1;
                
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }

    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdistrict.Text = dr1dt["district_name"].ToString();
        txtdistrict.ReadOnly = true;
        txtdistrict.BackColor = System.Drawing.Color.Wheat;


    }
    void GetScheme()
    {


        schobj = new Scheme_MP(ComObj);
                DataSet ds = schobj.selectAll(" order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }
    void GetCommodity()
    {
        ddlcomdty.Items.Clear();
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");

        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }
        
    void GetTransport()
    {
        tobj = new Transporter(ComObj);
        string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid + "'and IsActive='Y'";

        DataSet ds = tobj.selectAny(qry);

        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
        ddltransport.Items.Insert(0, "--Select--");

    }

    void GetDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        ddldistrict.DataSource = ds.Tables[0];
        ddldistrict.DataTextField = "district_name";
        ddldistrict.DataValueField = "District_Code";

        ddldistrict.DataBind();
        ddldistrict.Items.Insert(0, "--Select--");
    }
    void GetSrcDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddlsrcdist.DataSource = ds.Tables[0];
        ddlsrcdist.DataTextField = "district_name";
        ddlsrcdist.DataValueField = "District_Code";

        ddlsrcdist.DataBind();
        ddlsrcdist.Items.Insert(0, "--Select--");
    }
    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetSrcDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddlsrcdist.SelectedValue  + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlsrcdepot.DataSource = ds.Tables[0];
        ddlsrcdepot.DataTextField = "DepotName";
        ddlsrcdepot.DataValueField = "DepotId";

        ddlsrcdepot.DataBind();
        ddlsrcdepot.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
   
   
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }
    public string get_days(DateTime fromDate, DateTime toDate)
    {

        int y1 = 0, m1 = 0, d1 = 0, y2 = 0, m2 = 0, d2 = 0;
        y1 = fromDate.Year;
        m1 = fromDate.Month;
        d1 = fromDate.Day;
        y2 = toDate.Year;
        m2 = toDate.Month;
        d2 = toDate.Day;

        int y = (y2 - y1) * 12;
        int m = (y + m2) - m1;
        int d = (m * 30) + d2;
        int day = d - d1;
        return day.ToString();
    }
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
    
    protected void btnsave_Click(object sender, EventArgs e)
    {

        
        if (ddltransport.SelectedItem.Text == "--Select--" || ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity/Scheme/Transporter..'); </script> ");
        }
        else
        {
            
            string mtono = txttorderno.Text;
            string mtodate = getDate_MDY(DaintyDate1.Text);
            string mtaname = ddltransport.SelectedValue;
            float miqty = CheckNull(txtsendqty.Text);
          
            //string mcdate = getDate_MDY(DateTime.Today.Date.ToString());
            int month = int.Parse(ddlalotmm.SelectedValue.ToString ());
            int year = int.Parse(ddlallotyear.SelectedValue .ToString ());
            string notrans = "N";

            string udate = "";
            string ddate = "";
            string tid = ddlissue.SelectedValue + txttorderno.Text;
                        

            string mtid = ddltransport.SelectedValue;
            string todist = ddldistrict.SelectedValue;
            string toissuecenter = ddlissue.SelectedValue;
            string frmdist = ddlsrcdist.SelectedValue;
            string frmdepot = ddlsrcdepot.SelectedValue;
            string lift = "N";
            string mcomdty = ddlcomdty.SelectedValue;
            string mscheme = ddlscheme.SelectedValue;
            string mtovalidity = getDate_MDY(DaintyDate2.Text);
            string msource = source_id;

            string state = Session["State_Id"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string opid = Session["OperatorIDDM"].ToString();


        string to_no =txttorderno.Text ;
        mobj = new MoveChallan(ComObj);
        string qreychal = "select TO_Number from dbo.Transport_Order where Distt_Id='" + distid + "' and TO_Number='" + to_no + "' and Source_ID='02'";
        DataSet dsc = mobj.selectAny(qreychal);
        if (dsc.Tables[0].Rows.Count == 0)
        {
            string qry = "insert into dbo.Transport_Order(State_Id,Distt_Id,Source_ID,TO_Number,TO_Date,Commodity_ID,Scheme_ID,TO_Validity,Transporter_ID,fromDistrict,fromIssueCenter,toDistrict,toIssueCenter,Quantity,Month,Year,Trunsuction_Id,IsLifted,Created_date,updated_date,deleted_date,IP,OperatorID,NoTransaction)values('" + state + "','" + distid + "','" + msource + "','" + mtono + "','" + mtodate + "','" + mcomdty + "','" + mscheme + "','" + mtovalidity + "','" + mtid + "','" + frmdist + "','" + frmdepot + "','" + todist + "','" + toissuecenter + "'," + miqty + "," + month + "," + year + ",'" + tid + "','" + lift + "',getdate(),'" + udate + "','" + ddate + "','" + ip + "','" + opid + "','" + notrans + "')";
            cmd.CommandText = qry;
            cmd.Connection = con;
            con.Open();
            SqlTransaction trns;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;
            try
            {
                if (miqty == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Quantity...'); </script> ");
                }
                else
                {

                    int count = cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                    btnsave.Enabled = false;

                }

                trns.Commit();





            }
            catch (Exception ex)
            {
                trns.Rollback();
                Label1.Visible = true;
                Label1.Text = ex.Message;


            }
            finally
            {
                con.Close();
                //ComObj.CloseConnection();


            }





        }
        else
        {

        }
                   
        }
       


    }
    
   
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
   
    protected void ddltransport_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/TransportOrder_Type.aspx");
    }
    protected void ddlsrcdist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSrcDCName();
    }
    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    void Fillgrid()
    {


        string qrey = "SELECT tbl_MetaData_DEPOT.DepotName,TO_Number ,Sendto_District,Sendto_IC,Commodity,Scheme,Sum(Bags) as Bags ,Sum(Qty_send) as Qty_send,FDepot.DepotName as SDepot,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name    FROM  SCSC_Truck_challan left join tbl_MetaData_DEPOT on SCSC_Truck_challan.Sendto_IC=tbl_MetaData_DEPOT.DepotID left join tbl_MetaData_DEPOT as FDepot on  SCSC_Truck_challan.Depot_Id=FDepot.DepotID left join tbl_MetaData_STORAGE_COMMODITY on SCSC_Truck_challan.Commodity=tbl_MetaData_STORAGE_COMMODITY.Commodity_ID left join tbl_MetaData_SCHEME on SCSC_Truck_challan.Scheme =tbl_MetaData_SCHEME.Scheme_ID where  SCSC_Truck_challan.Dist_ID='" + distid + "' and SCSC_Truck_challan.TO_Number  not in (Select Distinct(TO_Number)as TO_Number from  Transport_Order where Distt_ID='" + distid + "' and Source_ID='02' ) and SCSC_Truck_challan.TO_Number!=''  group by TO_Number ,Sendto_District,Sendto_IC,Commodity,Scheme,tbl_MetaData_DEPOT.DepotName,FDepot.DepotName,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name";
        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds, "Tables[0]");
        dgridchallan.DataSource = ds.Tables[0];
        dgridchallan.DataBind();
       

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void ddlalotmm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
