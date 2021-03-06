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

public partial class IssueCenter_DeleteReceipt : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    MoveChallan mobj = null;
    MoveChallan mobj1 = null;
    Transporter tobj = null;
    Scheme_MP schobj = null;
    Commodity_MP comdtobj = null;
    Districts DObj = null;
    DistributionCenters distobj = null;
    protected Common ComObj = null, cmn = null;
    public string challan = "";
    public string gid = "";
    public string did = "";
    public string time = "";
    public string source = "";
    public string miller = "";
    public string version = "";
    static string arsource = "";
    static string arcomdty = "";
    static string arscheme = "";
    static string argodown = "";
    static string ReceiptID = "";
    chksql chk = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            gid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();

            if (!Page.IsPostBack)
            {
                GetfirstSource();  // for 

                GetSource();
                GetCategory();
                GetCommodity();
                GetGunny();
                Transport();
                GetScheme();
                Miller();
                GetGodown();
                GetDist();
                GetDCNameAll();
                ddlallot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddlallot_year.Items.Add(DateTime.Today.Year.ToString());
                ddlallot_year.SelectedIndex = 1;
                ddlalotmm.SelectedIndex = DateTime.Today.Month - 1;
            }

            if (ddlsource.SelectedItem.Text == "CMR")
            {
                lblMillersName.Visible = true;
                ddlmiller.Visible = true;
                lblReleaseOrder.Visible = false;
                lbltono.Visible = false;
                txtrono.Visible = false;
                txttono.Visible = false;
                lblmonth.Text = "Month";
                lblyear.Text = "Year";
            }

            else if (ddlsource.SelectedItem.Text == "Other Source")
            {
                lblMillersName.Visible = false;
                ddlmiller.Visible = false;
                ddlscheme.Visible = false;
                lblScheme.Visible = false;
                lblparty.Visible = true;
                txtparty.Visible = true;

                lblReleaseOrder.Visible = false;
                lbltono.Visible = false;
                txtrono.Visible = false;
                txttono.Visible = false;
                lblmonth.Text = "Month";
                lblyear.Text = "Year";
            }
            else if (ddlsource.SelectedItem.Text == "From FCI")
            {
                lblmonth.Text = "Allot. Month";
                lblyear.Text = "Allot.Year";
                lblReleaseOrder.Visible = true;
                lbltono.Visible = true;
                txtrono.Visible = true;
                txttono.Visible = true;
            }

            else
            {
                ddlscheme.Visible = true;
                lblScheme.Visible = true;
                lblparty.Visible = false;
                txtparty.Visible = false;
                lblmonth.Text = "Month";
                lblyear.Text = "Year";
                lblReleaseOrder.Visible = false;
                lbltono.Visible = false;
                txtrono.Visible = false;
                txttono.Visible = false;
            }

            BtnDelete.Visible = true;
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        
    }

    void GetfirstSource()
    {    
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            BtnDelete.Enabled = false;
        }

        else
        {
            ddlsource.DataSource = ds.Tables[0];
            ddlsource.DataTextField = "Source_Name";
            ddlsource.DataValueField = "Source_ID";
            ddlsource.DataBind();
            ddlsource.Items.Insert(0, "--Select--");

            BtnDelete.Enabled = true;
        }       
    }

    protected void ddlsource_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlsource.SelectedItem.Text == "Procurement")
            {
                Response.Redirect("~/IssueCenter/Deletefrm_Procurement.aspx");
            }
            
            else  if (ddlsource.SelectedItem.Text == "CMR")
                {
                    lblMillersName.Visible = true;
                    ddlmiller.Visible = true;
                }
                else
                {
                    lblMillersName.Visible = false;
                    ddlmiller.Visible = false;
                }
          

             if (ddlsource.SelectedItem.Text == "From RailHead" || ddlsource.SelectedItem.Text == "Tender Purchase(by Rack)")
            {
                string qryrail = "SELECT TC_Number,Rack_No FROM dbo.RR_receipt_Depot where  district_code = '" + did + "' and TC_Number Not Like 'MOR%' ";
                SqlCommand cmdr = new SqlCommand(qryrail, con);
                SqlDataAdapter dar = new SqlDataAdapter(cmdr);
                DataSet dsr = new DataSet();
                dar.Fill(dsr);

                if (dsr.Tables[0].Rows.Count == 0)
                {
                    ddlchallanNumber.DataSource = "";
                    ddlchallanNumber.DataBind();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Number Not Found'); </script> ");

                    BtnDelete.Enabled = false;
                }

                else
                {
                    BtnDelete.Enabled = true;

                    ddlchallanNumber.DataSource = dsr.Tables[0];
                    ddlchallanNumber.DataTextField = "TC_Number";
                    ddlchallanNumber.DataValueField = "Rack_No";
                    ddlchallanNumber.DataBind();
                    ddlchallanNumber.Items.Insert(0, "--Select--");

                    BtnDelete.Enabled = true;
                    
                }
            }

            else
            {
                //string qry = "SELECT challan_no,Receipt_id FROM dbo.tbl_Receipt_Details where  Dist_Id = '" + did + "' and S_of_arrival = '" + ddlsource.SelectedValue + "' and tbl_Receipt_Details.Depot_Id='" + gid + "' and tbl_Receipt_Details.challan_no != '' and challan_no Not Like 'MOR%' ";

                string qry = "SELECT challan_no,Receipt_id FROM dbo.tbl_Receipt_Details where  Dist_Id = '" + did + "' and S_of_arrival = '" + ddlsource.SelectedValue + "' and tbl_Receipt_Details.Depot_Id='" + gid + "' and tbl_Receipt_Details.challan_no != ''";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    ddlchallanNumber.DataSource = "";
                    ddlchallanNumber.DataBind();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record Not Found'); </script> ");
                }

                else
                {
                    ddlchallanNumber.DataSource = ds.Tables[0];
                    ddlchallanNumber.DataTextField = "challan_no";
                    ddlchallanNumber.DataValueField = "Receipt_id";
                    ddlchallanNumber.DataBind();
                    ddlchallanNumber.Items.Insert(0, "--Select--");
                }
            }
        }

        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Some Problem Exist'); </script> ");
        }
            
    }

    void GetSource()
    {
       
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);

        ddlsarrival.DataSource = ds1.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        //ddlsarrival.Items.Insert(0, "--Select--");
    }

    void GetDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddldistrict.DataSource = ds.Tables[0];
        ddldistrict.DataTextField = "district_name";
        ddldistrict.DataValueField = "District_Code";
        ddldistrict.DataBind();
        //ddldistrict.Items.Insert(0, "--Select--");
    }

    void GetDCName()
    {


        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);
        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotID";
        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");
        // ddDistId.Items.Insert(0, "--चुनिये--");
    }

    void GetDCNameAll()
    {


        distobj = new DistributionCenters(ComObj);
        string ord = "Select DepotName,DepotID from dbo.tbl_MetaData_DEPOT order by DepotName";
        DataSet ds = distobj.selectAny(ord);
        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotID";
        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");
        // ddDistId.Items.Insert(0, "--चुनिये--");
    }

    void Miller()
    {

        tobj = new Transporter(ComObj);
        string qry = "SELECT Miller_ID,Miller_Name FROM dbo.Miller_Master where District_Code='" + did + "'";
        DataSet ds = tobj.selectAny(qry);

        ddlmiller.DataSource = ds.Tables[0];
        ddlmiller.DataTextField = "Miller_Name";
        ddlmiller.DataValueField = "Miller_ID";
        ddlmiller.DataBind();
        //ddlmiller.Items.Insert(0, "--Select--");

    }

    public string getdateg(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }

    void GetData()
    {
        mobj = new MoveChallan(ComObj);
        string query = "Select tbl_Receipt_Details.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME.scheme_Name as Scheme_Name,Transporter_Table.Transporter_Name as Transporter_Name,Source_Arrival_Type.Source_Name as  Source_Name  from dbo.tbl_Receipt_Details left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_Receipt_Details.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on tbl_Receipt_Details.scheme=tbl_MetaData_SCHEME.scheme_Id  left join dbo.Transporter_Table on tbl_Receipt_Details.Transporter = Transporter_Table.Transporter_ID left join dbo.Source_Arrival_Type on tbl_Receipt_Details.S_of_arrival= Source_Arrival_Type.Source_ID where   tbl_Receipt_Details.Receipt_id='" + ddlchallanNumber.SelectedValue + "'and   tbl_Receipt_Details.Depot_Id='" + gid + "' and   tbl_Receipt_Details.Dist_id='" + did + "' ";
        DataSet ds = mobj.selectAny(query);

        if (ds.Tables[0].Rows.Count == 0)
        {
            BtnDelete.Enabled = false;
        }
        else
        {
            BtnDelete.Enabled = true;

            DataRow dr = ds.Tables[0].Rows[0];

            txtchallan.ReadOnly = true;
            txtchallan.ForeColor = System.Drawing.Color.Navy;
            txtchallan.Text = dr["challan_no"].ToString();
            DaintyDate3.Text = getdateg(dr["challan_date"].ToString());
            ddlsarrival.SelectedItem.Text = dr["Source_Name"].ToString();
            miller = dr["S_Name"].ToString();
            lblmcode.Text = dr["S_Name"].ToString();
            txtparty.Text = dr["S_Name"].ToString();
            arsource = dr["S_of_arrival"].ToString();
            string adist = dr["A_Dist"].ToString();
            if (adist == null)
            {

            }
            else
            {
                dcode.Text = adist;
            }
            string adepot = dr["A_Depo"].ToString();

            if (adepot == null)
            {
            }
            else
            {
                icode.Text = adepot;
            }
            ddlsarrival.SelectedValue = dr["S_of_arrival"].ToString();
            string stype = dr["S_of_arrival"].ToString();
            DaintyDate1.Text  = getdateg(dr["arrival_date"].ToString());

            string time = dr["arrival_time"].ToString();
            string hh = time.Substring(0, 2);
            string mm = time.Substring(3, 2);
            string ampm = time.Substring(6, 2);
            ddlhour.SelectedItem.Text = hh;
            ddlminute.SelectedItem.Text = mm;
            ddlampm.SelectedItem.Text = ampm;

            txtqty.Text = dr["Qty"].ToString();

            ddlcomdty.SelectedItem.Text = dr["Commodity_Name"].ToString();
            ddlcomdty.SelectedValue = dr["Commodity"].ToString();
            arcomdty = dr["Commodity"].ToString();
            lblcomdty.Text = dr["Commodity"].ToString();
            lbl_scomdty.Text =dr["Commodity"].ToString();
            ddlscheme.SelectedItem.Text = dr["Scheme_Name"].ToString();
            if(Convert.ToInt16(ddlsource.SelectedValue)!=11)
               
            {
            ddlscheme.SelectedValue = dr["Scheme"].ToString();
            lbl_ssch.Text = dr["Scheme"].ToString();
            arscheme = dr["Scheme"].ToString();
            lblsc.Text = dr["Scheme"].ToString();
            }
            ddlcropyear.SelectedItem.Text = dr["Crop_year"].ToString();
            ddlcategory.SelectedItem.Text = dr["Category"].ToString();
            ddltransport.SelectedItem.Text = dr["Transporter_Name"].ToString();
         
            txtvehleno.Text = dr["Vehile_no"].ToString();
            txtrono.Text =dr["RO_NO"].ToString();
            txttono.Text = dr["TO_Number"].ToString();
           
            ddlgtype.SelectedItem.Text = dr["Gunny_type"].ToString();
            txtnobags.Text = dr["No_of_Bags"].ToString();
            txtrqty.Text = dr["Recd_Qty"].ToString();
            lblqty.Text = dr["Recd_Qty"].ToString();
            txtrecbags.Text = dr["Recieved_Bags"].ToString();
            lblrecbag.Text = dr["Recieved_Bags"].ToString();
           
            txtmoisture.Text = dr["Moisture"].ToString();
            txtwcmno.Text = dr["WCM_no"].ToString();
            ddlgodown.SelectedValue = dr["Godown"].ToString();
            argodown = dr["Godown"].ToString();
            lblgid.Text = dr["Godown"].ToString();
            lblGodown.Text = dr["Godown"].ToString();
            //ddlalotmm.SelectedValue = dr["Month"].ToString();
            //ddlallot_year.SelectedValue = dr["Year"].ToString();
            DaintyDate3.Enabled = true;
            ddlsarrival.Enabled = false;
            ddlsarrival.ForeColor = System.Drawing.Color.Blue;

            if (ddlsource.SelectedItem.Text == "CMR")
            {
                string querym = "select Miller_Name from dbo.Miller_Master where District_Code='" + did + "' and Miller_ID='" + miller + "'";
                DataSet dsm = mobj.selectAny(querym);
                if (dsm.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record Not Found '); </script> ");
                }
                else
                {
                    DataRow drm = dsm.Tables[0].Rows[0];
                    ddlmiller.SelectedItem.Text = drm["Miller_Name"].ToString();
                    ddlmiller.SelectedValue = miller;
                    ddlmiller.Visible = true;
                    lblMillersName.Visible = true;
                }
               
            }
            else if (stype == "02")
            {
                
                lblRecFromDist.Visible = true;
                lblNameDepot.Visible = true;
                ddldistrict.Visible = true;
                ddlissue.Visible = true;
            }

            GetBalance();
            GetCapacity();
        }
    }

    void Transport()
    {

        tobj = new Transporter(ComObj);
        string qry = "Select Lead,Transporter_ID,Transporter_Name+'/'+'('+Lead_Distance.Lead_Name +')'as Transporter_Name  from dbo.Transporter_Table left join Lead_Distance on Transporter_Table.Lead=Lead_Distance.Lead_ID where Distt_ID='" + did  + "' and IsActive='Y'";// and Lead='"+ddllead.SelectedValue+"'";
        DataSet ds = tobj.selectAny(qry);
        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
       // ddltransport.Items.Insert(0, "--Select--");
    }

    void GetCategory()
    {     
        string qry = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlcategory.DataSource = ds.Tables[0];
        ddlcategory.DataTextField = "Category_Name";
        ddlcategory.DataValueField = "Category_Id";
        ddlcategory.DataBind();    
    }

    void GetGunny()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GunnyBags_Type";
        DataSet ds = mobj.selectAny(qry);
        ddlgtype.DataSource = ds.Tables[0];
        ddlgtype.DataTextField = "Gunny_Bags_Type";
        ddlgtype.DataValueField = "Gunny_Bags_Type_Id";
        ddlgtype.DataBind();
        
    }

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("order by displayorder");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
       // ddlscheme.Items.Insert(0, "--Select--");

    }

    void GetGodown()
    {
        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + gid + "' order by Godown_ID";

        SqlCommand cmdgod = new SqlCommand(qry,cons);
        SqlDataAdapter dagdn = new SqlDataAdapter(cmdgod);

        DataSet ds = new DataSet();

        dagdn.Fill(ds);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
       // ddlgodown.Items.Insert(0, "--Select--");

        // 5002141132


        if (cons.State == ConnectionState.Open)
        {
            cons.Close();
        }
    }

    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
       // ddlcomdty.Items.Insert(0, "--Select--");


    }

    void GetBalance()
    {
        string mcomid = lblcomdty.Text;        //string mcatid = ddlcategory.SelectedValue;
        string mscheme = lblsc.Text;
        string godown = ddlgodown.SelectedValue;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string source = ddlsarrival.SelectedValue;
        mobj1 = new MoveChallan(ComObj);

        string qry = "Select Sum(Current_Balance)as Current_Balance,Sum(Current_Bags) as Current_Bags  from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Godown='" + godown + "'and Source='" + source + "'";
        DataSet ds = mobj1.selectAny(qry);

        if (ds.Tables[0].Rows.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            txtbalqty.Text = "0";
            lblbalanceqty.Visible = true;
            txtbalqty.Visible = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.ReadOnly = true;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbalqty.Text = dr["Current_Balance"].ToString();
            txtcurbags.Text = dr["Current_Bags"].ToString();
            lblbalanceqty.Visible = true;
            txtbalqty.Visible = true;
            txtbalqty.BackColor = System.Drawing.Color.PaleGoldenrod;
            txtbalqty.ReadOnly = true;

            txtcurbags.Visible = true;
            txtcurbags.BackColor = System.Drawing.Color.PaleGoldenrod;
            txtcurbags.ReadOnly = true;

        }
    }

    protected string  getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    void GetCapacity()
    {
        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }
        string gname = ddlgodown.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + gid + "' and Godown_ID='" + gname + "'";

        SqlCommand cmdgod = new SqlCommand(qrygdn, cons);
        SqlDataAdapter dagdn = new SqlDataAdapter(cmdgod);

        DataSet ds = new DataSet();

        dagdn.Fill(ds);


        //DataSet ds = mobj.selectAny(qrygdn);
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
                txtmaxcap.Text = dr["Godown_Capacity"].ToString();

            }


        }
        GetCapGodown();

        if (cons.State == ConnectionState.Open)
        {
            cons.Close();
        }
    }

    void GetCapGodown()
    {
        string gname = ddlgodown.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT Round(Sum(convert(decimal(18,5),Current_Balance)),5) as Current_Balance  FROM dbo.issue_opening_balance where District_Id='" + did + "' and Depotid='" +gid + "' and Godown='" + gname + "'";

        DataSet ds = mobj.selectAny(qrygdn);
        if (ds == null)
        {
        }

        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                txtcurntcap.Text = "";

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtcurntcap.Text = (CheckNull(dr["Current_Balance"].ToString())).ToString ();
                txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();
            }


        }

    }

    void UpdateStock()
    {
        decimal ruqty = CheckNull(txtrqty.Text);
        decimal buqty = CheckNull(txtbalqty.Text);
        decimal prqty = CheckNull(lblqty.Text);
        decimal uuqty = decimal.Parse((System.Math.Round(prqty, 2) - System.Math.Round(ruqty, 2)).ToString());
        int mmrecbags = CheckNullInt(txtrecbags.Text);
        int mrrbags = CheckNullInt(lblrecbag.Text) - mmrecbags;
        int monthu = int.Parse(DateTime.Today.Month.ToString());
        int yearu = int.Parse(DateTime.Today.Year.ToString());
        string comdtyid = ddlcomdty.SelectedValue;
        string schemeid = ddlscheme.SelectedValue;
        string smonth = "";
        string syear = "";
        string mcom = "";
        string msch = "";
        string qrygchallan = "Select * from dbo.tbl_Receipt_Details where Dist_Id='" + did + "'and challan_no='" + txtchallan.Text + "'";
        mobj = new MoveChallan(ComObj);
        DataSet dsgc = mobj.selectAny(qrygchallan);
        if (dsgc.Tables[0].Rows.Count == 0)
        {

        }
        else
        {
            DataRow drgc = dsgc.Tables[0].Rows[0];
            smonth = drgc["Month"].ToString();
            syear = drgc["Year"].ToString();
            mcom = drgc["Commodity"].ToString();
            msch = drgc["Scheme"].ToString();
        }

        if (lbl_scomdty.Text == ddlcomdty.SelectedValue & lbl_ssch.Text == ddlscheme.SelectedValue)
        {
            if (ddlsarrival.SelectedItem.Text == "From FCI")
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI+(" + uuqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                cmd.CommandText = qryinsU;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


            }
            else if (ddlsarrival.SelectedItem.Text == "Other Depot")
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg+(" + uuqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                cmd.CommandText = qryinsU;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }



        }
        else
        {
            if (ddlsarrival.SelectedItem.Text == "From FCI")
            {
                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comdtyid + "'and  Scheme_ID='" + schemeid + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                mobj = new MoveChallan(ComObj);
                DataSet dsopen = mobj.selectAny(qryinsopen);

                if (dsopen.Tables[0].Rows.Count == 0)
                {
                    ////string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + gid + "','" + comdtyid + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + mros + "," + msdelo + "," + msod + "," + msos + "," + monthu + "," + yearu + ",'" + mremark + "')";
                    //cmd.CommandText = qryins;

                    //con.Open();
                    //cmd.ExecuteNonQuery();
                    //con.Close();


                }
                else
                {
                    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI+(" + uuqty + ") where Commodity_Id ='" + comdtyid + "'and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    cmd.CommandText = qryinsU;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            else
            {
                string qrystock = "select Sum(Recd_Qty) as Qty from dbo.tbl_Receipt_Details where Commodity='" + comdtyid + "'and Dist_Id='" + did + "'and Depot_ID='" + gid + "'and S_of_arrival='" + ddlsarrival.SelectedValue + "'and Month=" + monthu + "and Year=" + yearu;
                mobj = new MoveChallan(ComObj);
                DataSet dspro = mobj.selectAny(qrystock);
                if (dspro.Tables[0].Rows.Count == 0)
                {

                }
                else
                {
                    DataRow drop = dspro.Tables[0].Rows[0];
                    decimal mobal = 0;
                    decimal mrp = 0;
                    decimal mrod = CheckNull(drop["Qty"].ToString());
                    decimal msod = 0;
                    decimal msdelo = 0;
                    decimal mrfci = 0;
                    string mremark = "";
                    string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comdtyid + "'and DistrictId ='" + did + "'and DepotID='" + gid + "'";
                    mobj = new MoveChallan(ComObj);
                    DataSet dsopen = mobj.selectAny(qryinsopen);
                    if (dsopen.Tables[0].Rows.Count == 0)
                    {
                        string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Sale_Do,Sale_otherg,Remarks) Values('" + did + "','" + gid + "','" + comdtyid + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + msdelo + "," + msod + ",'" + mremark + "')";
                        cmd.CommandText = qryins;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                    }
                    else
                    {
                        string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=" + mrod + " where Commodity_Id ='" + comdtyid + "'and DistrictId='" + did + "'and DepotID='" + gid + "'";
                        cmd.CommandText = qryinsU;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }


                }

            }


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

    protected string getDate_MDYDD(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);

        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddlchallanNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsource.SelectedItem.Text == "From RailHead" || ddlsource.SelectedItem.Text == "Tender Purchase(by Rack)")
        {
            GetRailData();
        }
        else
        {
            GetData();
        }
       

        BtnDelete.Visible = true;
    }

    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        GetBalance();
    }

    protected void ddlgtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtbalqty_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlmiller_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmcode.Text = ddlmiller.SelectedValue;
    }
    protected void txtparty_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblgid.Text = ddlgodown.SelectedValue;
        GetCapacity();
        GetBalance();

    }

    void Update_Trans_Log()
    {
        string opid = Session["OperatorId"].ToString();
        string Source = "";
        string district = "";
        string dipot = "";
        if (ddlsarrival.SelectedValue == "06")
        {
            Source = txtparty.Text;
        }
        else
        {
            source = "";
        }

        if (ddlsarrival.SelectedValue == "02")
        {
            district = ddldistrict.SelectedValue;
            dipot = ddlissue.SelectedValue;
        }
        string mtime = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string qryInsert = "insert into dbo.tbl_Receipt_Details_Trans_Log(Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Transporter,Vehile_no,Arrival_time,No_of_Bags,Recd_Qty,Recieved_Bags,Month,Year,IP_Address,updated_date,Godown,Operation,OperatorID)values('" + did + "','" + gid + "','" + ReceiptID + "','" + ddlsarrival.SelectedValue + "','" + source + "','" + district + "','" + dipot + "','" + txtrono.Text + "','" + txttono.Text + "','" + getDate_MDY(DaintyDate3.Text) + "','" + getDate_MDY(DaintyDate1.Text) + "','" + txtchallan.Text + "','" + getDate_MDY(DaintyDate3.Text) + "'," + CheckNull(txtqty.Text) + ",'" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "','" + ddltransport.SelectedValue + "','" + txtvehleno.Text + "','" + mtime + "'," + CheckNullInt(txtnobags.Text) + "," + CheckNull(txtrqty.Text) + "," + CheckNullInt(txtrecbags.Text) + "," + ddlalotmm.SelectedValue + "," + ddlallot_year.SelectedValue + ",'" + ip + "',getdate(),'" + ddlgodown.SelectedValue + "','U','" + opid + "')";
        cmd.CommandText = qryInsert;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {

        }
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsc.Text = ddlscheme.SelectedValue;
        GetBalance();
        GetCapacity();
    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcomdty.Text = ddlcomdty.SelectedValue;
        GetBalance();
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        dcode.Text = ddldistrict.SelectedValue;
        GetDCName();
    }
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        icode.Text = ddlissue.SelectedValue;
    }
    protected void ddlalotmm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlallot_year_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();

        Response.Redirect("~/IssueCenter/issue_welcome.aspx");

    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if(ddlchallanNumber.SelectedValue == "0" || ddlchallanNumber.SelectedValue == "" )
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Challan/Rack Number'); </script> ");
            return;
        }

        if (ddlsource.SelectedItem.Text == "From RailHead" || ddlsource.SelectedItem.Text == "Tender Purchase(by Rack)")
        {
            string smonth = "";
            string syear = "";
            string mcom = "";
            string msch = "";
            string depotid = "";
            
            string qrygchallan = "Select * from dbo.RR_receipt_Depot where district_code='" + did + "'and TC_Number='" + ddlchallanNumber.SelectedItem.Text + "'";
            mobj = new MoveChallan(ComObj);
            DataSet dsgc = mobj.selectAny(qrygchallan);
            if (dsgc.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                DataRow drgc = dsgc.Tables[0].Rows[0];
                smonth = drgc["Month"].ToString();
                syear = drgc["Year"].ToString();
                mcom = drgc["Commodity"].ToString();
                msch = drgc["Scheme"].ToString();
                depotid = drgc["DepotID"].ToString();

                string uopen = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5), Current_Balance)-(" + CheckNull(txtrqty.Text) + "),Current_Bags=Current_Bags-(" + CheckNull(txtrecbags.Text) + ") where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcom + "'and Scheme_Id='" + msch + "' and Source='" + ddlsource.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(uopen, con);

                cmd1.Connection = con;

                cmd1.ExecuteNonQuery();

               
                string qryinsU = "update dbo.tbl_Stock_Registor set Received_RailHead=Received_RailHead-(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                SqlCommand cmd2 = new SqlCommand(qryinsU, con);

                cmd2.ExecuteNonQuery();

                string qrydel = " delete from RR_receipt_Depot where district_code = '"+did+"' and DepotID = '"+gid+"' and TC_Number = '"+ddlchallanNumber.SelectedItem.Text+"' ";
                SqlCommand cmddel = new SqlCommand(qrydel, con);

                cmddel.ExecuteNonQuery();       

            }          
        }

        else
        {
            string smonth = "";
            string syear = "";
            string mcom = "";
            string msch = "";
            string mgodown = "";
            string mdchallan = "";
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string qrygchallan = "Select * from dbo.tbl_Receipt_Details where Dist_Id='" + did + "'and challan_no='" + txtchallan.Text + "'";
            mobj = new MoveChallan(ComObj);
            DataSet dsgc = mobj.selectAny(qrygchallan);
            if (dsgc.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                DataRow drgc = dsgc.Tables[0].Rows[0];
                smonth = drgc["Month"].ToString();
                syear = drgc["Year"].ToString();
                mcom = drgc["Commodity"].ToString();
                msch = drgc["Scheme"].ToString();
                mgodown = drgc["Godown"].ToString();
                mdchallan = drgc["challan_no"].ToString();

                string uopen = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5), Current_Balance)-(" + CheckNull(lblqty.Text) + "),Current_Bags=Current_Bags-(" + CheckNull(lblrecbag.Text) + ") where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcom + "'and Scheme_Id='" + msch + "' and Source='" + ddlsarrival.SelectedValue + "' and Godown='" + mgodown + "'";
                SqlCommand cmd1 = new SqlCommand(uopen, con);

                cmd1.Connection = con;

                cmd1.ExecuteNonQuery();

                decimal ruqty = CheckNull(txtrqty.Text);
                decimal buqty = CheckNull(txtbalqty.Text);
                decimal prqty = CheckNull(lblqty.Text);
                decimal uuqty = decimal.Parse((System.Math.Round(prqty, 5) - System.Math.Round(ruqty, 5)).ToString());

                # region FCI
                if (ddlsarrival.SelectedItem.Text == "From FCI")
                {
                    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI-(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    SqlCommand cmd2 = new SqlCommand(qryinsU, con);

                    cmd2.ExecuteNonQuery();


                }
                # endregion

                # region Otherdepot
                else if (ddlsarrival.SelectedItem.Text == "Other Depot")
                {
                    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg-(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    SqlCommand cmd3 = new SqlCommand(qryinsU, con);

                    cmd3.ExecuteNonQuery();

                }
                # endregion

                # region CMR
                else if (ddlsarrival.SelectedItem.Text == "CMR")
                {
                    string qryinsU = "update dbo.tbl_Stock_Registor set Received_CMR=Received_CMR-(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    SqlCommand cmd3 = new SqlCommand(qryinsU, con);

                    cmd3.ExecuteNonQuery();

                }
                # endregion

                # region TransPurchase Road
                else if (ddlsarrival.SelectedItem.Text == "Tender Purchase(by Road)")
                {
                    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Other_Src =Recieved_Other_Src-(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    SqlCommand cmd3 = new SqlCommand(qryinsU, con);

                    cmd3.ExecuteNonQuery();

                }
                # endregion

                string inslog = "Insert into tbl_Receipt_Details_log select * from tbl_Receipt_Details where challan_no = '" + txtchallan.Text + "' and Receipt_id = '" + ddlchallanNumber.SelectedValue + "' and  Dist_Id = '" + did + "' and Depot_ID = '" + gid + "'";

                SqlCommand cmd4 = new SqlCommand(inslog, con);

                cmd4.ExecuteNonQuery();
                
                string qryUpdate = "delete from dbo.tbl_Receipt_Details where challan_no = '" + txtchallan.Text + "' and Receipt_id = '" + ddlchallanNumber.SelectedValue + "' and  Dist_Id = '" + did + "' and Depot_ID = '" + gid + "'";
                SqlCommand cmd5 = new SqlCommand(qryUpdate, con);
                cmd5.ExecuteNonQuery();
     
            }        

        }

        # region Current_Godown_Position

        string str11 = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags - " + txtrecbags.Text + ",Current_Balance=round(convert(decimal(18,5),Current_Balance) -" + CheckNull(txtrqty.Text) + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)+" + CheckNull(txtrqty.Text) + ",5) where District_Id='" + did + "' and Depotid='" + gid + "' ";
        cmd.CommandText = str11;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();

        
       

        # endregion             

        BtnDelete.Visible = false;

        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record Deleted Sucessfully'); </script> ");



        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    void GetRailData()
    {
        mobj = new MoveChallan(ComObj);
        string query = "Select RR_receipt_Depot.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME.scheme_Name as Scheme_Name,Transporter_Table.Transporter_Name as Transporter_Name  from dbo.RR_receipt_Depot left join dbo.tbl_MetaData_STORAGE_COMMODITY on RR_receipt_Depot.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on RR_receipt_Depot.scheme=tbl_MetaData_SCHEME.scheme_Id left join dbo.Transporter_Table on RR_receipt_Depot.Transporter_ID = Transporter_Table.Transporter_ID where RR_receipt_Depot.TC_Number='"+ddlchallanNumber.SelectedItem.Text+"' and  RR_receipt_Depot.DepotID='"+gid+"' and   RR_receipt_Depot.district_code='"+did+"'";
        DataSet ds = mobj.selectAny(query);

        if (ds.Tables[0].Rows.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record Not Found'); </script> ");
            BtnDelete.Enabled = false;
        }
        else
        {
            BtnDelete.Enabled = true; ;

            DataRow dr = ds.Tables[0].Rows[0];

            txtchallan.ReadOnly = true;
            txtchallan.ForeColor = System.Drawing.Color.Navy;
            txtchallan.Text = dr["Rack_No"].ToString();
            DaintyDate3.Text = getdateg(dr["TC_date"].ToString());
           

            DaintyDate1.Text = getdateg(dr["TC_date"].ToString());


            txtqty.Text = dr["Disp_Qty"].ToString();

            ddlcomdty.SelectedItem.Text = dr["Commodity_Name"].ToString();
            ddlcomdty.SelectedValue = dr["Commodity"].ToString();
           
            ddlscheme.SelectedItem.Text = dr["Scheme_Name"].ToString();
            ddlscheme.SelectedValue = dr["Scheme"].ToString();
            lbl_ssch.Text = dr["Scheme"].ToString();

                      
           
            ddltransport.SelectedItem.Text = dr["Transporter_Name"].ToString();

            txtvehleno.Text = dr["Truck_No"].ToString();
            
            txtnobags.Text = dr["Disp_Bags"].ToString();
            txtrqty.Text = dr["Disp_Qty"].ToString();
            lblqty.Text = dr["Recd_Qty"].ToString();
            txtrecbags.Text = dr["Recd_Bags"].ToString();
            lblrecbag.Text = dr["Recd_Bags"].ToString();
          
           
            ddlalotmm.SelectedValue = dr["Month"].ToString();
            ddlallot_year.SelectedValue = dr["Year"].ToString();
            DaintyDate3.Enabled = true;
            ddlsarrival.Enabled = false;
            ddlsarrival.ForeColor = System.Drawing.Color.Blue;

            
        }
    }



}
