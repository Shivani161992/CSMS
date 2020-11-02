using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class IssueCenter_Receipt_OtherDepot : System.Web.UI.Page
{
    SqlConnection con_MPStorage;
    SqlDataAdapter da_MPStorage;
    DataSet ds_MPStorage;

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Transporter tobj = null;
    MoveChallan mobj = null;
    MoveChallan mobj1 = null;
    DateTime dt1;
    DateTime dt2;
    MoveChallan mobj2 = null;
    chksql chk = null;
    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
    public string time;
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string gatepass = "";
    public long getnum = 0;
    public string source_from = "";
    public string source_name = "";
    public string status = "";
    public string district = "";
    public static string depoto = "";
    public string type = "";
    public string Challan_no = "";
    public string Challan_Date = "";
    public string st = "";
    public string Acceptane_No = "";
    public string Acceptane_Date = "";
    public string Dispatch_Date = "";
    public string qryforchallan = "";
    public string qryforgetdata = "";
    public int count = 0;
    public string distt = "";
    public string depot = "";
    public string comdtyid = "";
    public string schemeid = "";
    public string RO_NO = "";
    public string tono = "";
    string version = "";
    static string prosource = "";
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();


            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtwcmno.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtrqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtmoisture.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtrecbags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
        
            txtvehleno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtvehleno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtvehleno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtchallan.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtchallan.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtchallan.Attributes.Add("onchange", "return chksqltxt(this)");

           
            txtqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtqty.Attributes.Add("onchange", "return chksqltxt(this)");
            txtnobags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtnobags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtwcmno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtwcmno.Attributes.Add("onchange", "return chksqltxt(this)");
            txtrqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtmoisture.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtmoisture.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecbags.Attributes.Add("onchange", "return chksqltxt(this)");


            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");
            
            txtchallan.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtnobags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtwcmno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtmoisture.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrecbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            if (!IsPostBack)
            {
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                            
                GetScheme();
             
                GetCommodity();
               
                GetDist();
                Getdepot();

                GetBranch();

                Transport();

                getselection_other_depo();

                ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);

            }
        }

        else
        {

            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void getselection_other_depo()
    {
        Label6.Visible = true;
        ddlcropyear.Visible = true;

        lblRecFromDist.Visible = true;
        ddldistrict.Visible = true;

        lblNameDepot.Visible = true;
        ddlissue.Visible = true;

        GetCommodity();

       string qryforchallan = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and IsDeposit='N'";
        GetChallan();
        //qryforgetdata = "SELECT SCSC_Truck_challan.* ,Transporter_Table.Transporter_Name as Transporter_Name   FROM dbo.SCSC_Truck_challan left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + ddlchallan.SelectedValue + "'";
        Session["OD"] = "OD";
        st = "OD";

    }

    void GetCommodity()
    {
        string qry = "SELECT [Commodity_Id]  ,[Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] where Status = 'Y' order by Commodity_Name desc";
        SqlCommand cmd = new SqlCommand(qry, con);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void GetScheme()
    {
        string qry = "SELECT  [Scheme_Id] ,[Scheme_Name] FROM [tbl_MetaData_SCHEME]  where Status = 'Y' order by displayorder";
        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
       
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }

    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    public void GetBranch()
    {
        ddlBranch.Items.Clear();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", sid);
                da_MPStorage = new SqlDataAdapter(select, con_MPStorage);

                ds_MPStorage = new DataSet();
                da_MPStorage.Fill(ds_MPStorage);
                if (ds_MPStorage != null)
                {
                    if (ds_MPStorage.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds_MPStorage.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + did + "' order by DepotName");
                    da_MPStorage = new SqlDataAdapter(select1, con_MPStorage);

                    ds_MPStorage = new DataSet();
                    da_MPStorage.Fill(ds_MPStorage, "tbl_MetaData_DEPOT");
                    if (ds_MPStorage.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds_MPStorage.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    public void GetGodown()
    {

        cons.Open();

        string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + sid + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_Name";
                ddlgodown.DataValueField = "Godown_ID";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--select--");
            }
        }
        cons.Close();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txtbalqty.Text = "";
        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodownMPStorage();
        }
        else
        {
            ddlgodown.Items.Clear();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
        }
    }

    public void GetGodownMPStorage()
    {
        ddlgodown.Items.Clear();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks = 'Y' and BranchID='{0}'", ddlBranch.SelectedValue.ToString());
                da_MPStorage = new SqlDataAdapter(select, con_MPStorage);

                ds_MPStorage = new DataSet();
                da_MPStorage.Fill(ds_MPStorage);
                if (ds_MPStorage != null)
                {
                    if (ds_MPStorage.Tables[0].Rows.Count > 0)
                    {
                        ddlgodown.DataSource = ds_MPStorage.Tables[0];
                        ddlgodown.DataTextField = "Godown_Name";
                        ddlgodown.DataValueField = "Godown_ID";
                        ddlgodown.DataBind();
                        ddlgodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच का नाम चुनें |'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
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
        int noofdays = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
        int d = (m * noofdays) + d2;
        int day = d - d1;
        return day.ToString();
    }

    void GetDist()
    {
        string qry = "SELECT [district_code] ,[district_name] FROM [pds].[districtsmp] order by district_name";
        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddldistrict.DataSource = ds.Tables[0];
        ddldistrict.DataTextField = "district_name";
        ddldistrict.DataValueField = "District_Code";

        ddldistrict.DataBind();
        ddldistrict.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void GetChallan()
    {
        string qry = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and IsDeposit='N' and Challan_No NOT LIKE 'MORD%' ";
        //string qry = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and IsDeposit='N' ";
        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ddlchallan.Items.Clear();
            ListItem lst = new ListItem();
            lst.Text = "Not Indicated";
            lst.Value = "0";
            ddlchallan.Items.Insert(0, "--Select--");
            ddlchallan.Items.Insert(1, lst);


        }
        else
        {

            ddlchallan.Items.Clear();
            ddlchallan.DataSource = ds.Tables[0];
            ddlchallan.DataTextField = "Challan_No";
            ddlchallan.DataValueField = "Challan_No";
            ddlchallan.DataBind();
            ddlchallan.Items.Insert(0, "--Select--");
            ddlchallan.Items.Insert(1, "Not Indicated");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }

    void GetDCName()
    {

        string qry = "SELECT  [DepotID],[DepotName] FROM [tbl_MetaData_DEPOT] where DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

      
        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotID";
        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlchallan.SelectedValue == "1" || ddlchallan.SelectedItem.Text == "Not Indicated")
        {
            lblchallanno.Visible = true;
            txtchallan.Visible = true;

            lblchallandate.Visible = true;

            DaintyDate3.Visible = true;

        }

        else
        {
            lblchallanno.Visible = true;
            txtchallan.Visible = true;
            txtchallan.Enabled = false;

            lblchallandate.Visible = true;

            DaintyDate3.Visible = true;


            lblRecFromDist.Visible = true;
            ddldistrict.Visible = true;

            lblNameDepot.Visible = true;
            ddlissue.Visible = true;
            qryforgetdata = "SELECT SCSC_Truck_challan.* , CONVERT(nvarchar,challan_Date,103)as DateChallan,Transporter_Table.Transporter_Name as Transporter_Name   FROM dbo.SCSC_Truck_challan left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where Sendto_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";


            SqlCommand cmd = new SqlCommand(qryforgetdata, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            
            txtnobags.Text = dr["Bags"].ToString();

            txtrecbags.Text = dr["Bags"].ToString();

            txtnobags.BackColor = System.Drawing.Color.Wheat;
            txtnobags.ReadOnly = true;

            txtqty.Text = dr["Qty_send"].ToString();

            txtrqty.Text = dr["Qty_send"].ToString();

            txtqty.BackColor = System.Drawing.Color.Wheat;
            txtqty.ReadOnly = true;

            string comid = dr["Commodity"].ToString();

            comdtyid = dr["Commodity"].ToString();

            ddltransport.SelectedItem.Text = dr["Transporter_Name"].ToString();
            lbltid.Text = dr["Transporter"].ToString();


            ddltransport.BackColor = System.Drawing.Color.Wheat;
            ddltransport.Enabled = false;
            txtvehleno.Text = dr["Truck_no"].ToString();
            txtvehleno.BackColor = System.Drawing.Color.Wheat;
            txtvehleno.ReadOnly = true;

            string scheme = dr["Scheme"].ToString();
            schemeid = dr["Scheme"].ToString();

            distt = dr["Dist_ID"].ToString();
            depot = dr["Depot_Id"].ToString();

            string qry1 = "select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + comid + "'";

            SqlCommand cmd1 = new SqlCommand(qry1, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);

            DataRow dr1 = ds1.Tables[0].Rows[0];

            string challanDate = dr["DateChallan"].ToString();

            string challanNum = dr["Challan_No"].ToString();

            ddlcomdty.SelectedValue = comid;

            ddlcomdty.Enabled = false;

            ddlscheme.SelectedValue = scheme;

            ddlscheme.Enabled = false;


            ddldistrict.SelectedValue = distt;

            ddldistrict.Enabled = false;


            ddlissue.SelectedValue = depot;

            ddlissue.Enabled = false;

            txtchallan.Text = challanNum;

            DaintyDate3.Text = challanDate;
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }


    void Getdepot()
    {
         if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qry = "SELECT  [DepotID],[DepotName] FROM [tbl_MetaData_DEPOT] order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);


        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotID";
        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
  

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCapacity();
        txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txtbalqty.Text = "";

        if (ddlgodown.SelectedIndex > 0)
        {
            string gname = ddlgodown.SelectedValue;
           
            string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + sid + "' and Godown_ID='" + gname + "'";

            SqlCommand cmd = new SqlCommand(qrygdn, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
           
            if (ds == null)
            {

            }

            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    txtmaxcap.Text = "";

                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    txtmaxcap.Text = (System.Math.Round(CheckNull(dr["Godown_Capacity"].ToString()), 5)).ToString();

                }
            }
            GetBalQty();
            GetCapGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown Number'); </script> ");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void GetBalQty()
    {
        string mcomid = ddlcomdty.SelectedValue;
      
        
        string mgodown = ddlgodown.SelectedValue;
       
      

        string qry = "Select Sum(convert(decimal(18,5),Current_Balance))as Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Godown='" + mgodown + "'";

        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds == null)
        {
            //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            lblbalanceqty.Visible = false;
            txtbalqty.Visible = false;
        }
        else
        {

            if (ds.Tables[0].Rows.Count == 0)
            {

                txtbalqty.Text = "0";
                lblbalanceqty.Visible = true;
                txtbalqty.Visible = true;
                //txtbalqty.BackColor = System.Drawing.Color.Wheat;
                txtbalqty.ReadOnly = true;
            }

            else
            {
                DataRow dr = ds.Tables[0].Rows[0];

                string qqty = ds.Tables[0].Rows[0]["Current_Balance"].ToString();

                if (qqty == "")
                {
                    qqty = "0";
                }

                // decimal qty = CheckNull("qqttyy");

                decimal qty = decimal.Parse(qqty);

                //  decimal qty = CheckNull(dr["qqttyy"].ToString());

                if (qty == 0)
                {
                    txtbalqty.Text = "0";
                    lblbalanceqty.Visible = true;
                    txtbalqty.Visible = true;
                    //txtbalqty.BackColor = System.Drawing.Color.Wheat;
                    txtbalqty.ReadOnly = true;
                }
                else
                {
                    txtbalqty.Text = qty.ToString();

                    // txtbalqty.Text = (System.Math.Round (CheckNull(qty.ToString()),5)).ToString();

                    lblbalanceqty.Visible = true;
                    txtbalqty.Visible = true;
                    //txtbalqty.BackColor = System.Drawing.Color.Wheat;
                    txtbalqty.ReadOnly = true;
                }
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    void GetCapGodown()
    {
        string Godown = ddlgodown.SelectedItem.Value;

        Int64 comid = Convert.ToInt64(Godown);

        string pqry = "available_space_godown";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdpqty = new SqlCommand(pqry, con);
        cmdpqty.CommandType = CommandType.StoredProcedure;

        cmdpqty.CommandTimeout = 0;
        cmdpqty.Parameters.Add("@district_code", SqlDbType.VarChar).Value = did;
        cmdpqty.Parameters.Add("@Depotid", SqlDbType.VarChar).Value = ddlBranch.SelectedValue.ToString();
        cmdpqty.Parameters.Add("@GodownId", SqlDbType.VarChar).Value = Godown;

        DataSet ds = new DataSet();
        SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);
        
        dr.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

            txtavalcap.Text = Convert.ToString(stock);

            txtcurntcap.Text = Convert.ToString(stock);
            txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/IssueCenter/Receipt_OtherDepot.aspx");
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtchallan.Text == "" || DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Enter Challan Number or Challan Date'); </script> ");
            return;
        }

        if (ddldistrict.SelectedValue == "0" || ddlissue.SelectedValue == "0" || ddlissue.SelectedItem.Text == "--Select--" || ddldistrict.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sending District or Issue Center'); </script> ");
            return;
        }

        if (ddlcomdty.SelectedValue == "0" || ddlcomdty.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Commodity / Scheme'); </script> ");
            return;
        }

        if (txtnobags.Text == "" || txtqty.Text == "" || txtrecbags.Text == "" || txtrqty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Bags and Quantity Details'); </script> ");
            return;
        }

        
        if (txtvehleno.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Enter Truck Number'); </script> ");
            return;
        }

        if (ddlBranch.SelectedValue == "0" || ddlgodown.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Branch / Godown'); </script> ");
            return;
        }

        string source_from = "02";

         string opid = Session["OperatorId"].ToString();

        string state = Session["State_Id"].ToString();

        if (ddlchallan.SelectedItem.Text == "Not Indicated" || ddlchallan.SelectedItem.Text == "--Select--")
        {
            lbltid.Text = ddltransport.SelectedValue;
        }        

        decimal ccap = CheckNull(txtcurntcap.Text);
        decimal rcap = CheckNull(txtrqty.Text);
        decimal chkcap = ccap + rcap;
        decimal maxcap = CheckNull(txtmaxcap.Text);

        schemeid = ddlscheme.SelectedValue;


        DateTime dispdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate1.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

        string today_date = System.DateTime.Now.ToString("MM/dd/yyyy");

        dt1 = Convert.ToDateTime(today_date);


        DateTime date1 = new DateTime(dt1.Year, dt1.Month, dt1.Day);

        DateTime date2 = new DateTime(dispdate.Year, dispdate.Month, dispdate.Day);

        int result1 = DateTime.Compare(date1, date2);


        string relationship = string.Empty;

        if (result1 >= 0)
        {
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());

            decimal cqty = CheckNull(txtqty.Text);
            decimal rqty = CheckNull(txtrqty.Text);
            decimal vqty = (cqty - rqty);

            string qrey = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + sid + "' and Dist_Id='" + did + "'";

            SqlCommand cmd = new SqlCommand(qrey, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds == null)
            {

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                gatepass = dr["Receipt_id"].ToString();
                if (gatepass == "")
                {
                    string issue = sid.Substring(2, 5);
                    gatepass = issue + month.ToString() + "001";

                }
                else
                {
                    getnum = Convert.ToInt64(gatepass);
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();


                }

                time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());

                string mchallan = txtchallan.Text;
                string chdate = getDate_MDY(DaintyDate3.Text);
                string ardate = getDate_MDY(DaintyDate1.Text);
                string mcomdty = ddlcomdty.SelectedValue;

                if (mcomdty == "--Select--")
                {
                    return;
                }

                string mscheme = ddlscheme.SelectedValue;
                string mcropy = ddlcropyear.SelectedItem.ToString();
                int mrecbags = CheckNullInt(txtrecbags.Text);


                string mtransporter = lbltid.Text;

                string mvehicleno = txtvehleno.Text;
                string mtime = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
                int mbagno = CheckNullInt(txtnobags.Text);
                decimal mrqty = CheckNull(txtrqty.Text);
                decimal mmoisture = CheckNull(txtmoisture.Text);

                string mwcm = txtwcmno.Text;

                string gdwn = ddlgodown.SelectedValue;
            
                decimal mqty = CheckNull(txtqty.Text);
                int recdbags = CheckNullInt(txtrecbags.Text);

                string adate = getDate_MDY(DaintyDate1.Text);
                
                district = ddldistrict.SelectedValue;

                depoto = ddlissue.SelectedValue;

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string checkchallan = "Select challan_no from dbo.tbl_Receipt_Details where Dist_Id='" + did + "' and  Depot_ID='" + sid + "' and challan_no='" + mchallan + "'";

                 SqlCommand cmd2 = new SqlCommand(qrey, con);
                 if (con.State == ConnectionState.Closed)
                 {
                     con.Open();
                 }
                 SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                 DataSet dschallan = new DataSet();
                 da2.Fill(dschallan);

                        if (dschallan.Tables[0].Rows.Count == 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan No Already Exist...'); </script> ");

                        }
                        else
                        {
                            string qryInsert = "insert into dbo.tbl_Receipt_Details( State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch)values('" + state + "','" + did + "','" + sid + "','" + gatepass + "','" + source_from + "','','" + district + "','" + depoto + "','" + RO_NO + "','" + tono + "','" + chdate + "','" + adate + "','" + mchallan + "','" + chdate + "'," + mqty + ",'" + ddlcomdty.SelectedValue + "','" + schemeid + "','" + mcropy + "','','" + mtransporter + "','" + mvehicleno + "','" + mtime + "',''," + mbagno + "," + mrqty + "," + mrecbags + "," + mmoisture + ",'" + mwcm + "'," + vqty + "," + month + "," + year + ",'Y','" + ip + "',getdate(),'','Y','" + gdwn + "','" + opid + "','N','','" + ddlBranch.SelectedValue.ToString() + "')";

                            try
                            
                            {
                            
                                cmd.CommandText = qryInsert;
                                cmd.Connection = con;

                               int x =  cmd.ExecuteNonQuery();

                               if (x > 0)
                               {
                                   Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted...'); </script> ");

                                   HyperLink1.Attributes.Add("onclick", "window.open('print_Gatepass.aspx',null,'left=250, top=0, height=550, width=600, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

                                   Session["Receipt_ID"] = gatepass;
                                   HyperLink1.Visible = true;
                               
                               }

                                
                            }
                             catch (Exception ex)
                            {
                                Label1.Visible = true;
                                Label1.Text = ex.Message;

                            }
                            finally
                            {
                                con.Close();

                            }

                        }
            }
        }
    }

    void Transport()
    {
      
        string qry = "Select distinct Lead,Transporter_ID,Transporter_Name+'/'+'('+Lead_Distance.Lead_Name +')'as Transporter_Name  from dbo.Transporter_Table left join Lead_Distance on Transporter_Table.Lead=Lead_Distance.Lead_ID where Distt_ID='" + did + "' and IsActive='Y' and (Transporter_Name is not NULL and Lead is not null)";// and Lead='"+ddllead.SelectedValue+"'";
       
        SqlCommand cmd = new SqlCommand(qry, con);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
      
        SqlDataAdapter da = new SqlDataAdapter(cmd);
       
        DataSet ds = new DataSet();
       
        da.Fill(ds);


        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
        ddltransport.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


    }
}
