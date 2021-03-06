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

public partial class mpscsc_edit_challan : System.Web.UI.Page
{
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
            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            challan = Session["challan"].ToString();
            gid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();
            source = Session["Source"].ToString();
            version = Session["hindi"].ToString();
            ReceiptID=Session["Receipt_Id"].ToString();

            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtwcmno.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtrqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtmoisture.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtrecbags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");

            txtvehleno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtvehleno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtvehleno.Attributes.Add("onchange", "return chksqltxt(this)");

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
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtvehleno.Text);
            ctrllist.Add(txtqty.Text);
            ctrllist.Add(txtnobags.Text);
            ctrllist.Add(txtwcmno.Text);
            ctrllist.Add(txtrqty.Text);
            ctrllist.Add(txtmoisture.Text);
            ctrllist.Add(txtrecbags.Text);
            ctrllist.Add(DaintyDate1.Text);
            ctrllist.Add(DaintyDate3.Text);
            if (chk == null)
            {
            }
            else
            {
                bool chkstr = chk.chksql_server(ctrllist);
                if (chkstr == true)
                {
                    Page.Server.Transfer(HttpContext.Current.Request.Path);
                }
            }
            if (source == "CMR")
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
            else if (source == "Other Source")
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
            else if (source == "From FCI")
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
                ddlscheme.Visible =true ;
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

            if (Page.IsPostBack == false)
            {
               
                //GetCat();
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
               GetData();
               if (version == "H")
               {
                   lblSorcePfArrival.Text = Resources.LocalizedText.lblSorcePfArrival;
                   lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                   lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                   lblScheme.Text = Resources.LocalizedText.lblScheme;
                   lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                   lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                   lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                   lblMillersName.Text = Resources.LocalizedText.lblMillersName;
                   lbldispdetails.Text = Resources.LocalizedText.lbldispdetails;

                   lbltotalReceivedBags.Text = Resources.LocalizedText.lbltotalReceivedBags;
                   lblTotalQuantityReceived.Text = Resources.LocalizedText.lblTotalQuantityReceived;
                   lblMaxCap.Text = Resources.LocalizedText.lblCapacity;
                   lblChallanNumber.Text = Resources.LocalizedText.lblChallanNumber;
                   lblTrans.Text = Resources.LocalizedText.lblTrans;
                   lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                   lblArrivalTime.Text = Resources.LocalizedText.lblArrivalTime;
                   lblReceiptDate.Text = Resources.LocalizedText.lblReceiptDate;

                   lblDateOfChallan.Text = Resources.LocalizedText.lblDateOfChallan;

                   lblparty.Text = Resources.LocalizedText.lblparty;
                   lblCurStackCap.Text = Resources.LocalizedText.lblCurStackCap;
                   lblAvailable.Text = Resources.LocalizedText.lblAvailable;
                   lblreceipt.Text = Resources.LocalizedText.lblreceipt;
                   lblRecepDetail.Text = Resources.LocalizedText.lblRecepDetail;
                   btnclose.Text = Resources.LocalizedText.btnclose;
                   btnsave.Text = Resources.LocalizedText.btnsave;

               }

            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }



    }
    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
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
        ddlmiller.Items.Insert(0, "--Select--");

    }
    public string getdateg(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    void GetData()
    {
        mobj = new MoveChallan(ComObj);
        string query = "Select tbl_Receipt_Details.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME.scheme_Name as Scheme_Name,Transporter_Table.Transporter_Name as Transporter_Name,Source_Arrival_Type.Source_Name as  Source_Name  from dbo.tbl_Receipt_Details left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_Receipt_Details.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on tbl_Receipt_Details.scheme=tbl_MetaData_SCHEME.scheme_Id  left join dbo.Transporter_Table on tbl_Receipt_Details.Transporter = Transporter_Table.Transporter_ID left join dbo.Source_Arrival_Type on tbl_Receipt_Details.S_of_arrival= Source_Arrival_Type.Source_ID where   tbl_Receipt_Details.Receipt_id='" + challan + "'and   tbl_Receipt_Details.Depot_Id='" + gid + "' and   tbl_Receipt_Details.Dist_id='" + did + "'";
        DataSet ds = mobj.selectAny(query);

        if (ds == null)
        {
        }
        else
        {

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
                ddldistrict.SelectedValue = adist;
                dcode.Text = adist;
            }
            string adepot = dr["A_Depo"].ToString();

            if (adepot == null)
            {
            }
            else
            {
                ddlissue.SelectedValue = adepot;
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
            ddlscheme.SelectedValue = dr["Scheme"].ToString();
            lbl_ssch.Text = dr["Scheme"].ToString();
            arscheme = dr["Scheme"].ToString();
            lblsc.Text = dr["Scheme"].ToString();
            ddlcropyear.SelectedItem.Text = dr["Crop_year"].ToString();
            ddlcategory.SelectedItem.Text = dr["Category"].ToString();
            ddltransport.SelectedItem.Text = dr["Transporter_Name"].ToString();
            ddltransport.SelectedValue = dr["Transporter"].ToString();


            txtvehleno.Text = dr["Vehile_no"].ToString();
            txtrono.Text =dr["RO_NO"].ToString();
            txttono.Text = dr["TO_Number"].ToString();
            //txtchallan.Text = dr["Arrival_time"];
            ddlgtype.SelectedItem.Text = dr["Gunny_type"].ToString();
            txtnobags.Text = dr["No_of_Bags"].ToString();
            txtrqty.Text = dr["Recd_Qty"].ToString();
            lblqty.Text = dr["Recd_Qty"].ToString();
            txtrecbags.Text = dr["Recieved_Bags"].ToString();
            lblrecbag.Text = dr["Recieved_Bags"].ToString();
            //ddlcat.SelectedItem.Text = dr["Category_recd"].ToString();
            txtmoisture.Text = dr["Moisture"].ToString();
            txtwcmno.Text = dr["WCM_no"].ToString();
            ddlgodown.SelectedValue = dr["Godown"].ToString();
            argodown = dr["Godown"].ToString();
            lblgid.Text = dr["Godown"].ToString();
            lblGodown.Text = dr["Godown"].ToString();
            ddlalotmm.SelectedValue = dr["Month"].ToString();
            ddlallot_year.SelectedValue = dr["Year"].ToString();
            DaintyDate3.Enabled = true;
            ddlsarrival.Enabled = false;
            ddlsarrival.ForeColor = System.Drawing.Color.Blue;
            if (source == "CMR")
            {
                string querym = "select Miller_Name from dbo.Miller_Master where District_Code='" + did + "' and Miller_ID='" + miller + "'";
                DataSet dsm = mobj.selectAny(querym);
                DataRow drm = dsm.Tables[0].Rows[0];
                ddlmiller.SelectedItem.Text = drm["Miller_Name"].ToString();
                ddlmiller.SelectedValue = miller;

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
        ddltransport.Items.Insert(0, "--Select--");
    }
    //void GetCat()
    //{
    //    mobj = new MoveChallan(ComObj);
    //    string qry = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
    //    DataSet ds = mobj.selectAny(qry);
    //    ddlcat.DataSource = ds.Tables[0];
    //    ddlcat.DataTextField = "Category_Name";
    //    ddlcat.DataValueField = "Category_Id";
    //    ddlcat.DataBind();
    //    ddlcat.Items.Insert(0, "--Select--");


    //}
    void GetCategory()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
        DataSet ds = mobj.selectAny(qry);

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
        DataSet ds = schobj.selectAll("  order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }
    void GetGodown()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + gid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");


    }
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


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
        string gname = ddlgodown.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + gid + "' and Godown_ID='" + gname + "'";

        DataSet ds = mobj.selectAny(qrygdn);
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
            mcom  = drgc["Commodity"].ToString();
            msch = drgc["Scheme"].ToString();
        }

        if (lbl_scomdty.Text == ddlcomdty.SelectedValue & lbl_ssch.Text  == ddlscheme.SelectedValue)
        {
            if (ddlsarrival.SelectedItem.Text == "From FCI")
            {   
                string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI+(" + uuqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='"+msch +"' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
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
                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comdtyid  + "'and  Scheme_ID='" + schemeid  + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
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
    protected string   getDate_MDYDD (string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);

        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBalance();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();


        Response.Redirect("~/IssueCenter/issue_welcome.aspx");

    }
    protected void btnaddmore_Click(object sender, EventArgs e)
    {

    }
    protected void ddlgtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtbalqty_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlmiller_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmcode.Text  = ddlmiller.SelectedValue;

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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        decimal ccap = CheckNull(txtcurntcap.Text);
            decimal rcap = CheckNull(txtrqty.Text);
        decimal chkcap = ccap + rcap;
        decimal maxcap= CheckNull (txtmaxcap.Text);

        if (chkcap > maxcap)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sorry Space is not available at Godown....'); </script> ");
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
            }



            string uopen = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5), Current_Balance)-(" + CheckNull(lblqty.Text) + "),Current_Bags=Current_Bags-(" + CheckNull(lblrecbag.Text) + ") where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcom + "'and Scheme_Id='" + msch + "' and Source='" + ddlsarrival.SelectedValue + "' and Godown='" + mgodown + "'";
            cmd.CommandText = uopen;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            decimal ruqty = CheckNull(txtrqty.Text);
            decimal buqty = CheckNull(txtbalqty.Text);
            decimal prqty = CheckNull(lblqty.Text);
            decimal uuqty = decimal.Parse((System.Math.Round(prqty, 5) - System.Math.Round(ruqty, 5)).ToString());


            if (ddlsarrival.SelectedItem.Text == "From FCI")
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI-(" + prqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                cmd.CommandText = qryinsU;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                mobj = new MoveChallan(ComObj);
                DataSet dsopen = mobj.selectAny(qryinsopen);

                if (dsopen.Tables[0].Rows.Count == 0)
                {
                    decimal mopening = 0;
                    string qryfind = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                    mobj = new MoveChallan(ComObj);
                    DataSet dsf = mobj.selectAny(qryfind);
                    if (dsf.Tables[0].Rows.Count == 0)
                    {
                        string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + ddlcomdty.SelectedValue + "' and Scheme_Id ='" + ddlscheme.SelectedValue + "'";
                        mobj = new MoveChallan(ComObj);
                        DataSet dsqry = mobj.selectAny(chkopenss);
                        if (dsqry == null)
                        {

                        }
                        else
                        {
                            DataRow drss = dsqry.Tables[0].Rows[0];
                            mopening = CheckNull(drss["Current_Balance"].ToString());

                        }

                    }
                    else
                    {
                        DataRow drf = dsf.Tables[0].Rows[0];
                        mopening = CheckNull(drf["Opening_Balance"].ToString());


                    }
                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + gid + "','" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "'," + mopening + "," + 0 + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + smonth + "," + syear + ",'')";
                    cmd.CommandText = qryinsr;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string qryustock = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI+(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    cmd.CommandText = qryustock;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }



                string qrysstock = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                mobj = new MoveChallan(ComObj);
                DataSet dsstock = mobj.selectAny(qrysstock);

                if (dsstock.Tables[0].Rows.Count == 0)
                {


                }
                else
                {
                    string qryupdate = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI-(" + CheckNull(lblqty.Text) + "),Opening_Balance=Opening_Balance-(" + CheckNull(lblqty.Text) + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    cmd.CommandText = qryupdate;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string qrynew = "update dbo.tbl_Stock_Registor set Recieved_FCI=Recieved_FCI+(" + CheckNull(txtrqty.Text) + "),Opening_Balance=Opening_Balance+(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                    cmd.CommandText = qrynew;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }

            }
            else if (ddlsarrival.SelectedItem.Text == "Other Depot")
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg-(" + prqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                cmd.CommandText = qryinsU;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                mobj = new MoveChallan(ComObj);
                DataSet dsopen = mobj.selectAny(qryinsopen);

                if (dsopen.Tables[0].Rows.Count == 0)
                {
                    decimal mopening = 0;
                    string qryfind = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                    mobj = new MoveChallan(ComObj);
                    DataSet dsf = mobj.selectAny(qryfind);
                    if (dsf.Tables[0].Rows.Count == 0)
                    {
                        string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + ddlcomdty.SelectedValue + "' and Scheme_Id ='" + ddlscheme.SelectedValue + "'";
                        mobj = new MoveChallan(ComObj);
                        DataSet dsqry = mobj.selectAny(chkopenss);
                        if (dsqry == null)
                        {

                        }
                        else
                        {
                            DataRow drss = dsqry.Tables[0].Rows[0];
                            mopening = CheckNull(drss["Current_Balance"].ToString());

                        }

                    }
                    else
                    {
                        DataRow drf = dsf.Tables[0].Rows[0];
                        mopening = CheckNull(drf["Opening_Balance"].ToString());


                    }
                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + gid + "','" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "'," + mopening + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + smonth + "," + syear + ",'')";
                    cmd.CommandText = qryinsr;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string qryustock = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg+(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    cmd.CommandText = qryustock;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }



                string qrysstock = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                mobj = new MoveChallan(ComObj);
                DataSet dsstock = mobj.selectAny(qrysstock);

                if (dsstock.Tables[0].Rows.Count == 0)
                {


                }
                else
                {
                    string qryupdate = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg-(" + CheckNull(lblqty.Text) + "),Opening_Balance=Opening_Balance-(" + CheckNull(lblqty.Text) + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                    cmd.CommandText = qryupdate;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string qrynew = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg+(" + CheckNull(txtrqty.Text) + "),Opening_Balance=Opening_Balance+(" + CheckNull(txtrqty.Text) + ") where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                    cmd.CommandText = qrynew;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }




                //string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg-(" + prqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + smonth + "and Year=" + syear;
                //cmd.CommandText = qryinsU;
                //con.Open();
                //cmd.ExecuteNonQuery();
                //con.Close();

                //string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                //mobj = new MoveChallan(ComObj);
                //DataSet dsopen = mobj.selectAny(qryinsopen);

                //if (dsopen.Tables[0].Rows.Count == 0)
                //{


                //}
                //else
                //{
                //    string qryupdate1 = "update dbo.tbl_Stock_Registor set Recieved_Otherg=Recieved_Otherg-(" + uuqty + "),Opening_Balance=Opening_Balance-(" + uuqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + gid + "'and Month=" + month + "and Year=" + year;
                //    cmd.CommandText = qryupdate1;
                //    con.Open();
                //    cmd.ExecuteNonQuery();
                //    con.Close();

                //}



            }




            decimal cqty = CheckNull(txtqty.Text);
            decimal rqty = CheckNull(txtrqty.Text);
            decimal vqty = (cqty - rqty);
            string mchallan = txtchallan.Text;
            string mchdate = getDate_MDY(DaintyDate3.Text);
            string mardate = getDate_MDY(DaintyDate1.Text);
            string mcomdty = ddlcomdty.SelectedValue;
            string mscheme = lblsc.Text;
            string mcropy = ddlcropyear.SelectedItem.ToString();
            string mcategry = ddlcategory.SelectedValue;
            string mtransporter = ddltransport.SelectedValue;
            string mvehicleno = txtvehleno.Text;
            string mtime = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
            int mbagno = CheckNullInt(txtnobags.Text);
            decimal mrqty = CheckNull(txtrqty.Text);
            //string mcatrecv = ddlcat.SelectedValue;
            decimal mmoisture = CheckNull(txtmoisture.Text);
            string mwcm = txtwcmno.Text;
            //string mudate = getDate_MDY(DateTime.Today.Date.ToString());
            decimal mqty = CheckNull(txtqty.Text);
            string msarival = ddlsarrival.SelectedValue;
            int mrecbags = CheckNullInt(txtrecbags.Text);
            string sourcem = Session["Source"].ToString();
            string godownid = ddlgodown.SelectedValue;
            string arrivedist = dcode.Text;
            string arrivedepot = icode.Text;

            if (sourcem == "Other Source")
            {
                lblmcode.Text = txtparty.Text;

            }
            if (sourcem == "From FCI")
            {
                lblmcode.Text = txtparty.Text;

            }
            string qryUpdate = "Update dbo.tbl_Receipt_Details set  S_of_arrival='" + msarival + "',S_Name='" + lblmcode.Text + "',challan_date='" + mchdate + "',arrival_date='" + mardate + "',Qty=" + mqty + ",Commodity='" + mcomdty + "',Scheme='" + mscheme + "',Crop_year='" + mcropy + "',Category='" + mcategry + "',Transporter='" + mtransporter + "',Vehile_no='" + mvehicleno + "',Arrival_time='" + mtime + "',No_of_Bags=" + mbagno + ",Recd_Qty=" + mrqty + ",Recieved_Bags=" + mrecbags + ",Moisture=" + mmoisture + ",WCM_no='" + mwcm + "',Godown='" + godownid + "',Variation_qty=" + vqty + ",updated_date=getdate(),A_dist='" + arrivedist + "',A_Depo='" + arrivedepot + "',RO_NO='" + txtrono.Text + "',TO_Number='" + txttono.Text + "',Month=" + ddlalotmm.SelectedValue + ",Year=" + ddlallot_year.SelectedValue + " where Dist_Id='" + did + "'and challan_no='" + mchallan + "'";
            cmd.CommandText = qryUpdate;
            cmd.Connection = con;
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                con.Close();
                if (count >= 1)
                {



                    //decimal ruqty = CheckNull(txtrqty.Text);
                    //decimal buqty = CheckNull(txtbalqty.Text);
                    //decimal prqty = CheckNull(lblqty.Text);
                    //decimal uuqty = decimal.Parse ( (System.Math.Round(prqty, 2) - System.Math.Round(ruqty, 2)).ToString());

                    string mcomid = ddlcomdty.SelectedValue;
                    string source = ddlsarrival.SelectedValue;

                    string godown = ddlgodown.SelectedValue;
                    string mstate = "23";
                    string udate = "";
                    string ddate = "";
                    int openbag = 0;
                    decimal openqty = 0;
                    int mmcbags = CheckNullInt(txtrecbags.Text);
                    decimal mmcqty = CheckNull(txtrqty.Text);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    //if (lblGodown.Text == ddlgodown.SelectedValue)
                    //{
                    //    int mmrecbags = CheckNullInt(txtrecbags.Text);
                    //    int mrrbags = CheckNullInt(lblrecbag.Text) - mmrecbags;

                    //    string query = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5), Current_Balance)-(" + uuqty + "),Current_Bags=Current_Bags-(" + mrrbags + ") where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Source='" + source + "' and Godown='" + ddlgodown.SelectedValue + "'";
                    //    cmd.CommandText = query;
                    //    cmd.Connection = con;
                    //    con.Open ();
                    //    cmd.ExecuteNonQuery();
                    //    con.Close();


                    //}
                    //else
                    //{
                    string chkopen = "Select * from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Godown='" + ddlgodown.SelectedValue + "' and Source='" + source + "'";
                    mobj = new MoveChallan(ComObj);
                    DataSet dsqry = mobj.selectAny(chkopen);
                    if (dsqry == null)
                    {

                    }

                    else
                    {

                        if (dsqry.Tables[0].Rows.Count == 0)
                        {
                            int mmrecbags = CheckNullInt(txtrecbags.Text);
                            int mrrbags = CheckNullInt(lblrecbag.Text) - mmrecbags;
                            string mggn = ddlgodown.SelectedValue;
                            string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + did + "','" + gid + "','" + mcomdty + "','" + mscheme + "','','" + ddlgodown.SelectedValue + "',''," + openbag + "," + openqty + ",'" + source + "'," + mmcqty + "," + mmcbags + "," + month + "," + year + ",'" + ip + "',getdate(),getdate(),'" + udate + "','" + ddate + "'" + ")";
                            cmd.CommandText = qreyins;
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();

                            con.Close();

                            //string query = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-(" + CheckNull (lblqty.Text) + "),Current_Bags=Current_Bags-(" +CheckNullInt (lblrecbag.Text)+ ") where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Source='" + source + "' and Godown='" + lblGodown.Text+ "'";
                            //cmd.CommandText = query;
                            //cmd.Connection = con;
                            //con.Open();
                            //cmd.ExecuteNonQuery();
                            //con.Close();



                        }
                        else
                        {
                            int mmrecbags = CheckNullInt(txtrecbags.Text);
                            int mrrbags = CheckNullInt(lblrecbag.Text) - mmrecbags;

                            string query = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance+(" + CheckNull(txtrqty.Text) + "),Current_Bags=Current_Bags+(" + CheckNullInt(txtrecbags.Text) + ") where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Source='" + source + "' and Godown='" + ddlgodown.SelectedValue + "'";
                            cmd.CommandText = query;
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            //string queryold = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-(" + CheckNull(lblqty.Text) + "),Current_Bags=Current_Bags-(" + CheckNullInt(lblrecbag.Text) + ") where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Source='" + source + "' and Godown='" + lblGodown.Text + "'";
                            //cmd.CommandText = queryold;
                            //cmd.Connection = con;
                            //con.Open();
                            //cmd.ExecuteNonQuery();
                            //con.Close();


                            //string queryold = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + prqty + ",Current_Bags=Current_Bags-" + lblrecbag.Text + " where District_Id='" + did + "'and Depotid='" + gid + "'and Commodity_Id='" + arcomdty + "'and Scheme_Id='" + arscheme + "' and Source='" + arsource + "'and Godown='" + argodown + "'";
                            //cmd.CommandText = queryold;
                            //cmd.Connection = con;
                            //cmd.ExecuteNonQuery();




                            //}


                        }


                    }


                    //try
                    //{


                    //    UpdateStock();

                    //}
                    //catch (Exception ex)
                    //{
                    //    Label1.Text = ex.Message;

                    //}
                    //finally
                    //{
                    //    con.Close();
                    //    //ComObj.CloseConnection();
                    //}
                }





                Update_Trans_Log();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + count + " Record Updated  " + "'); </script> ");

            }
            catch (Exception ex)
            {
                Label1.Visible = true;
                Label1.Text = ex.Message;

            }
            finally
            {
                con.Close();
                //ComObj.CloseConnection();
            }
            btnsave.Enabled = false;
        }
       
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
            district = ddldistrict.SelectedValue ;
            dipot = ddlissue.SelectedValue;
        }
        string mtime = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string qryInsert = "insert into dbo.tbl_Receipt_Details_Trans_Log(Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Transporter,Vehile_no,Arrival_time,No_of_Bags,Recd_Qty,Recieved_Bags,Month,Year,IP_Address,updated_date,Godown,Operation,OperatorID)values('" + did + "','" + gid + "','" + ReceiptID + "','" + ddlsarrival.SelectedValue + "','" + source + "','" + district + "','" + dipot + "','" + txtrono.Text + "','" + txttono.Text + "','" + getDate_MDY(DaintyDate3.Text) + "','" + getDate_MDY(DaintyDate1.Text) + "','" + txtchallan.Text + "','" + getDate_MDY(DaintyDate3.Text) + "'," + CheckNull(txtqty.Text) + ",'" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "','" + ddltransport.SelectedValue + "','" + txtvehleno.Text + "','" + mtime + "'," + CheckNullInt(txtnobags.Text) + "," + CheckNull(txtrqty.Text) + "," + CheckNullInt(txtrecbags.Text) + "," + ddlalotmm.SelectedValue + "," + ddlallot_year.SelectedValue + ",'" + ip + "',getdate(),'" + ddlgodown.SelectedValue + "','U','"+opid+"')";
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
}
