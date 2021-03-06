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


public partial class mpsc_move_challan : System.Web.UI.Page
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
            version = Session["hindi"].ToString();

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

            //txtparty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            //txtparty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            //txtparty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrono.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtrono.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrono.Attributes.Add("onchange", "return chksqltxt(this)");


            txttono.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttono.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttono.Attributes.Add("onchange", "return chksqltxt(this)");

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


            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            //type = ddlsarrival.SelectedItem.ToString();
            if (Session["dc_id"] != null)
            {

            }

            distobj = new DistributionCenters(ComObj);
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtvehleno.Text);
            ctrllist.Add(txtqty.Text);
            ctrllist.Add(txtnobags.Text);
            ctrllist.Add(txtwcmno.Text);
            ctrllist.Add(txtrqty.Text);
            ctrllist.Add(txtmoisture.Text);
            ctrllist.Add(txtrecbags.Text);
            ctrllist.Add(txttono.Text);
            ctrllist.Add(txtrono.Text);
            //ctrllist.Add(txtparty.Text);
            ctrllist.Add(txtchallan.Text);
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


            if (!IsPostBack)
            {
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                txtchallandt.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                Transport();
                GetScheme();
                GetGunny();
                GetCommodity();
                GetCategory();
                GetDist();
                GetDCName();
                GetDistFCI();
                GetSource();
                GetFCIdist();
                Getsupplier();

                //GetGodown();
                GetBranch();

                ddlallot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddlallot_year.Items.Add(DateTime.Today.Year.ToString());
                ddlallot_year.SelectedIndex = 1;
                ddlalotmm.SelectedIndex = DateTime.Today.Month - 1;
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
                    lblRecFromDist.Text = Resources.LocalizedText.lblRecFromDist;
                    lblNameDepot.Text = Resources.LocalizedText.lblNameDepot;
                    lblReleaseOrder.Text = Resources.LocalizedText.lblDetailsofRO;
                    lbltono.Text = Resources.LocalizedText.lbltono;
                    lbltotalReceivedBags.Text = Resources.LocalizedText.lbltotalReceivedBags;
                    lblTotalQuantityReceived.Text = Resources.LocalizedText.lblTotalQuantityReceived;
                    lblMaxCap.Text = Resources.LocalizedText.lblCapacity;
                    lblChallanNumber.Text = Resources.LocalizedText.lblChallanNumber;
                    lblTrans.Text = Resources.LocalizedText.lblTrans;
                    lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                    lblArrivalTime.Text = Resources.LocalizedText.lblArrivalTime;
                    lblReceiptDate.Text = Resources.LocalizedText.lblReceiptDate;
                    lblRecepDetail.Text = Resources.LocalizedText.lblRecepDetail;
                    lblDateOfChallan.Text = Resources.LocalizedText.lblDateOfChallan;
                    lblchallandate.Text = Resources.LocalizedText.lblchallandate;
                    lblparty.Text = Resources.LocalizedText.lblparty;
                    lblCurStackCap.Text = Resources.LocalizedText.lblCurStackCap;
                    lblAvailable.Text = Resources.LocalizedText.lblAvailable;
                    lblKgs.Text = Resources.LocalizedText.lblKgs;
                    btnsubmit.Text = Resources.LocalizedText.btnsubmit;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    lbldepositstock.Text = Resources.LocalizedText.lbldepositstock;
                    btnnew.Text = Resources.LocalizedText.btnnew;
                    lblfcdist.Text = Resources.LocalizedText.lblfcdist;
                    lbldepofci.Text = Resources.LocalizedText.lbldepofci;
                    lblmonth.Text = Resources.LocalizedText.lblmonth;
                    lblyear.Text = Resources.LocalizedText.lblyear;
                }

            }


        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");




        }

    }

    private void Getsupplier()
    {
        SqlCommand cmd = new SqlCommand("select distinct S_name from tbl_SupplyOrder_Master where district_code='" + did + "' and Depot_ID='" + sid + "' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "S_name";

        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
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
    void GetDistFCI()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddlfcidepo.DataSource = ds.Tables[0];
        ddlfcidepo.DataTextField = "district_name";
        ddlfcidepo.DataValueField = "District_Code";

        ddlfcidepo.DataBind();
        ddlfcidepo.Items.Insert(0, "--Select--");
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
    void GetCommodityMarfed()
    {
        comdtobj = new Commodity_MP(ComObj);
        string qry = "Select * from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id IN ('3','4')order by Commodity_Id ASC ";
        DataSet ds = comdtobj.selectAny(qry);
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");



    }
    void GetCommodityCMR()
    {
        comdtobj = new Commodity_MP(ComObj);
        //string comd="SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY  where Commodity_Id in(0,2,) Status='Y' "
        DataSet ds = comdtobj.selectAll();
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }

    void GetCommoditytenderPurchase()
    {
        comdtobj = new Commodity_MP(ComObj);
        string qry = "Select * from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id IN ('23','19','17')order by Commodity_Id ASC ";
        DataSet ds = comdtobj.selectAny(qry);
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();



    }

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("order by displayorder");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();


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
    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID!='01' order by Source_id";
        DataSet ds = mobj.selectAny(qry);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
    }
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

    void Transport()
    {

        tobj = new Transporter(ComObj);
        string qry = "Select distinct Lead,Transporter_ID,Transporter_Name+'/'+'('+Lead_Distance.Lead_Name +')'as Transporter_Name  from dbo.Transporter_Table left join Lead_Distance on Transporter_Table.Lead=Lead_Distance.Lead_ID where Distt_ID='" + did + "' and IsActive='Y'";// and Lead='"+ddllead.SelectedValue+"'";
        DataSet ds = tobj.selectAny(qry);
        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
        ddltransport.Items.Insert(0, "--Select--");

    }
    void Miller()
    {

        tobj = new Transporter(ComObj);
        string qry = "SELECT Miller_ID,Miller_Name+'('+Miller_ID+')' as Miller_Name FROM dbo.Miller_Master where District_Code='" + ddldistrict.SelectedValue + "'";
        DataSet ds = tobj.selectAny(qry);

        ddlmiller.DataSource = ds.Tables[0];
        ddlmiller.DataTextField = "Miller_Name";
        ddlmiller.DataValueField = "Miller_ID";
        ddlmiller.DataBind();
        ddlmiller.Items.Insert(0, "--Select--");

    }
    void GetFCIdist()
    {
        tobj = new Transporter(ComObj);
        string qry = "select districtsmp.district_name as dist_name,DepoCode.district_code as dist_code from DepoCode left join pds.districtsmp   on upper(DepoCode.district)=upper( districtsmp.district_name) group by districtsmp.district_name, DepoCode.district_code";
        DataSet ds = tobj.selectAny(qry);

        ddlfcidist.DataSource = ds.Tables[0];
        ddlfcidist.DataTextField = "dist_name";
        ddlfcidist.DataValueField = "dist_code";
        ddlfcidist.DataBind();
        ddlfcidist.Items.Insert(0, "--Select--");

    }
    void GetFCIdepot()
    {
        //string dtype = ddldepottype.SelectedItem.ToString();
        string dcode = ddlfcidist.SelectedValue;
        tobj = new Transporter(ComObj);
        string qry = "select distinct(DepoName) as depo_name,DepoCode as depo_code,type from DepoCode where district_code='" + dcode + "'";//and type='" + dtype + "'";
        DataSet ds = tobj.selectAny(qry);

        ddlfcidepo.DataSource = ds.Tables[0];
        ddlfcidepo.DataTextField = "depo_name";
        ddlfcidepo.DataValueField = "depo_code";
        ddlfcidepo.DataBind();
        ddlfcidepo.Items.Insert(0, "--Select--");

    }
    void GetChallan()
    {
        mobj = new MoveChallan(ComObj);

        //string qry = "SELECT * FROM dbo.Lift_A_RO where Send_District='"+did +"'and Issue_center='"+ sid +"'";
        DataSet ds = mobj.selectAny(qryforchallan);
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
    }

    void GetChallanmarfed()
    {
        mobj = new MoveChallan(ComObj);

        //string qry = "SELECT * FROM dbo.Lift_A_RO where Send_District='"+did +"'and Issue_center='"+ sid +"'";
        DataSet ds = mobj.selectAny(qryforchallan);

        ddlchallan.Items.Clear();
        ListItem lst = new ListItem();
        lst.Text = "Not Indicated";
        lst.Value = "0";
        ddlchallan.Items.Insert(0, "--Select--");
        ddlchallan.Items.Insert(1, lst);





    }
    void GetData()
    {
        mobj = new MoveChallan(ComObj);
        //string qry = "SELECT * FROM dbo.Lift_A_RO where Send_District='" + did + "'and Issue_center='" + sid + "' and Challan_No='"+ddlchallan .SelectedValue +"'";

        if (ddlsarrival.SelectedItem.Text == "Other Depot")
        {
            lblRecFromDist.Visible = true;
            ddldistrict.Visible = true;

            lblNameDepot.Visible = true;
            ddlissue.Visible = true;
            qryforgetdata = "SELECT SCSC_Truck_challan.* ,Transporter_Table.Transporter_Name as Transporter_Name   FROM dbo.SCSC_Truck_challan left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where Sendto_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";
            ddl_order.Visible = false;


            icode.Text = ddlissue.SelectedValue;
            depoto = icode.Text;

        }

        else if (ddlsarrival.SelectedItem.Text == "From Marketing Federation")
        {
            lblRecFromDist.Visible = true;
            ddldistrict.Visible = true;

            //lblNameDepot.Visible = true;
            //ddlissue.Visible = true;
            qryforgetdata = "SELECT SCSC_Truck_challan.* ,Transporter_Table.Transporter_Name as Transporter_Name   FROM dbo.SCSC_Truck_challan left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where Sendto_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";
            ddl_order.Visible = false;
            ddldistrict.Enabled = false;
            //ddlissue.Enabled = false;

            //icode.Text = ddlissue.SelectedValue;
            //depoto = icode.Text;

        }

        else if (ddlsarrival.SelectedItem.Text == "From FCI")
        {


            lblRecFromDist.Visible = false;
            ddldistrict.Visible = false;

            lblNameDepot.Visible = false;
            ddlissue.Visible = false;
            qryforgetdata = "SELECT Lift_A_RO.*,DepoCode.DepoName as DepoName,DepoCode.District as FCIDname,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name as Scheme_Name ,Transporter_Table.Transporter_Name as Transporter_Name  FROM dbo.Lift_A_RO left join dbo.tbl_MetaData_STORAGE_COMMODITY on Lift_A_RO.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on Lift_A_RO.Scheme=tbl_MetaData_SCHEME .Scheme_Id left join dbo.Transporter_Table on Lift_A_RO.Transporter=Transporter_Table.Transporter_ID left join DepoCode on Lift_A_RO.FCIdepo=DepoCode.DepoCode   where Send_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";


        }
        DataSet ds = mobj.selectAny(qryforgetdata);
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
            {
                Session["st"] = "F";
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lblRecFromDist.Visible = true;
                ddldistrict.Visible = true;

                icode.Text = ddlissue.SelectedValue;
                depoto = icode.Text;

                lblNameDepot.Visible = true;
                ddlissue.Visible = true;
                txtqty.ReadOnly = false;
                Clear();

            }
            else if (ddlsarrival.SelectedItem.ToString() == "From Marketing Federation")
            {
                Session["st"] = "T";
                lblDateOfChallan.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lblRecFromDist.Visible = true;
                ddldistrict.Visible = true;
                lblfcdist.Visible = false;
                ddlfcidist.Visible = false;
                lblNameDepot.Visible = false;
                ddlissue.Visible = false;



                ddltransport.SelectedItem.Text = "Not Indicated";
                ddltransport.BackColor = System.Drawing.Color.Wheat;
                ddltransport.Enabled = false;


                lblRecFromDist.Visible = true;
                ddldistrict.Visible = true;

                lblNameDepot.Visible = false;
                ddlissue.Visible = false;
                lblbalanceqty.Visible = false;
                txtbalqty.Visible = false;
                Clear();

            }
            else if (ddlsarrival.SelectedItem.ToString() == "From FCI")
            {
                Session["st"] = "F";
                lblDateOfChallan.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lblRecFromDist.Visible = false;
                ddldistrict.Visible = false;
                lblfcdist.Visible = true;
                ddlfcidist.Visible = true;
                lblNameDepot.Visible = false;
                ddlissue.Visible = false;

                lblRecFromDist.Visible = false;
                ddldistrict.Visible = false;

                lblNameDepot.Visible = false;
                ddlissue.Visible = false;
                lblbalanceqty.Visible = false;
                txtbalqty.Visible = false;

                txtqty.ReadOnly = false;
                ddlcategory.BackColor = System.Drawing.Color.White;
                ddlcropyear.BackColor = System.Drawing.Color.White;
                ddltransport.BackColor = System.Drawing.Color.White;
                ddlgtype.BackColor = System.Drawing.Color.White;

                txtnobags.ReadOnly = false;
                txtnobags.BackColor = System.Drawing.Color.White;
                txtvehleno.ReadOnly = false;
                txtvehleno.BackColor = System.Drawing.Color.White;
                txtqty.BackColor = System.Drawing.Color.White;
                txtnobags.BackColor = System.Drawing.Color.White;

                ddltransport.SelectedItem.Text = "--Select--";
                ddlcategory.SelectedItem.Text = "NA";
                ddlgtype.SelectedItem.Text = "NA";
                //ddlscheme.SelectedItem.Text = "--Select--";
                ddlcomdty.SelectedItem.Text = "--Select--";

                ddlcropyear.SelectedItem.Text = "NA";

                //ddlcropyear.Enabled = true;

                ddltransport.Enabled = true;
                ddlcategory.Enabled = true;
                ddlgtype.Enabled = true;
                ddlcomdty.Enabled = true;
                txtrono.Text = "";
                txttono.Text = "";
                txtqty.Text = "";
                txtvehleno.Text = "";
                txtnobags.Text = "";
                txtrono.ReadOnly = false;
                txttono.ReadOnly = false;
                txtchallan.BackColor = System.Drawing.Color.Snow;
                txtrono.BackColor = System.Drawing.Color.Snow;
                txttono.BackColor = System.Drawing.Color.Snow;
                txtrono.Focus();
            }
            else if (ddlsarrival.SelectedItem.ToString() == "CMR")
            {
                Session["st"] = "TT";
                lblDateOfChallan.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lblRecFromDist.Visible = true;
                ddldistrict.Visible = true;
                lblfcdist.Visible = false;
                ddlfcidist.Visible = false;
                lblNameDepot.Visible = false;
                ddlissue.Visible = false;
                //ddlcropyear.Enabled = false;



                ddltransport.SelectedItem.Text = "Not Indicated";
                ddltransport.BackColor = System.Drawing.Color.Wheat;
                ddltransport.Enabled = false;


                lblRecFromDist.Visible = true;
                ddldistrict.Visible = true;

                lblNameDepot.Visible = false;
                ddlissue.Visible = false;
                lblbalanceqty.Visible = false;
                txtbalqty.Visible = false;
                Clear();
            }
            else if (ddlsarrival.SelectedItem.ToString() == "Levy Rice")
            {
                Session["st"] = "TT";
                lblDateOfChallan.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lblRecFromDist.Visible = true;
                ddldistrict.Visible = true;
                lblfcdist.Visible = false;
                ddlfcidist.Visible = false;
                lblNameDepot.Visible = false;
                ddlissue.Visible = false;

                lblRecFromDist.Visible = false;
                ddldistrict.Visible = false;

                lblNameDepot.Visible = false;
                ddlissue.Visible = false;
                lblbalanceqty.Visible = false;
                txtbalqty.Visible = false;
                Clear();
            }
            else if (ddlsarrival.SelectedItem.ToString() == "Tender Purchase(by Road)-Sugar/Salt")
            {
                Session["st"] = "TT";
                lblDateOfChallan.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lblRecFromDist.Visible = false;
                ddldistrict.Visible = false;
                lblfcdist.Visible = false;
                ddlfcidist.Visible = false;
                lblNameDepot.Visible = false;
                ddlissue.Visible = false;

                lblRecFromDist.Visible = false;
                ddldistrict.Visible = false;

                lblNameDepot.Visible = false;
                ddlissue.Visible = false;
                lblbalanceqty.Visible = false;
                txtbalqty.Visible = false;
                Clear();
            }


        }
        else
        {
            Session["st"] = "T";
            if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
            {
                lblchallanno.Visible = false;
                txtchallan.Visible = false;
                lblchallandate.Visible = false;
                DaintyDate3.Visible = false;


                DataRow dr = ds.Tables[0].Rows[0];
                lblDateOfChallan.Visible = true;
                txtchallandt.Visible = true;

                string chdate = dr["Challan_Date"].ToString();
                string challan = getdate(chdate);

                txtchallandt.BackColor = System.Drawing.Color.Wheat;
                txtchallandt.ReadOnly = true;

                txtchallandt.Text = challan;
                txtqty.Text = dr["Qty_send"].ToString();

                txtqty.BackColor = System.Drawing.Color.Wheat;
                txtqty.ReadOnly = true;

                ddlcategory.SelectedItem.Text = "Not Indicated";

                ddlcategory.BackColor = System.Drawing.Color.Wheat;
                ddlcategory.Enabled = false;

                if (ddlsarrival.SelectedItem.ToString() != "CMR")
                {

                    ddlcropyear.SelectedItem.Text = "Not Indicated";

                    ddlcropyear.BackColor = System.Drawing.Color.Wheat;
                    // ddlcropyear.Enabled = false;
                }

                txtnobags.Text = dr["Bags"].ToString();
                txtnobags.BackColor = System.Drawing.Color.Wheat;
                txtnobags.ReadOnly = true;
                string comid = dr["Commodity"].ToString();
                lblcomdty.Text = dr["Commodity"].ToString();
                comdtyid = dr["Commodity"].ToString();

                //ddlcomdty.SelectedItem.Text = dr["Commodity"].ToString();


                ddltransport.SelectedItem.Text = dr["Transporter_Name"].ToString();
                lbltid.Text = dr["Transporter"].ToString();
                //ddltransport.SelectedValue = dr["Transporter"].ToString();
                ddltransport.BackColor = System.Drawing.Color.Wheat;
                ddltransport.Enabled = false;
                txtvehleno.Text = dr["Truck_no"].ToString();
                txtvehleno.BackColor = System.Drawing.Color.Wheat;
                txtvehleno.ReadOnly = true;

                ddlgtype.SelectedItem.Text = "Not Indicated";
                ddlgtype.BackColor = System.Drawing.Color.Wheat;
                ddlgtype.Enabled = false;
                string scheme = dr["Scheme"].ToString();
                schemeid = dr["Scheme"].ToString();
                lblsch.Text = dr["Scheme"].ToString();

                //string distt = dr["Dist_ID"].ToString();
                //string depot = dr["Depot_Id"].ToString();
                distt = dr["Dist_ID"].ToString();
                depot = dr["Depot_Id"].ToString();
                dcode.Text = dr["Dist_ID"].ToString();
                icode.Text = dr["Depot_Id"].ToString();
                //st = "T";


                mobj2 = new MoveChallan(ComObj);
                string qry1 = "select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + comid + "'";
                DataSet ds1 = mobj2.selectAny(qry1);
                DataRow dr1 = ds1.Tables[0].Rows[0];
                ddlcomdty.SelectedItem.Text = dr1["Commodity_Name"].ToString();

                mobj2 = new MoveChallan(ComObj);
                string qry1sc = "select Scheme_Name from dbo.tbl_MetaData_SCHEME where Scheme_Id='" + scheme + "'";
                DataSet ds1sc = mobj2.selectAny(qry1sc);
                DataRow dr1sc = ds1sc.Tables[0].Rows[0];
                ddlscheme.SelectedItem.Text = dr1sc["Scheme_Name"].ToString();


                mobj2 = new MoveChallan(ComObj);
                string qry1d = "select district_name from pds.districtsmp where district_code='" + distt + "'";
                DataSet ds1d = mobj2.selectAny(qry1d);
                DataRow dr1d = ds1d.Tables[0].Rows[0];
                ddldistrict.SelectedItem.Text = dr1d["district_name"].ToString();



                mobj2 = new MoveChallan(ComObj);
                string qry1dt = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + depot + "'";
                DataSet ds1dt = mobj2.selectAny(qry1dt);
                DataRow dr1dt = ds1dt.Tables[0].Rows[0];
                ddlissue.SelectedItem.Text = dr1dt["DepotName"].ToString();

                //GetBalance();


            }
            else
            {
                Session["st"] = "T";

                lblchallanno.Visible = false;
                txtchallan.Visible = false;
                lblchallandate.Visible = false;
                DaintyDate3.Visible = false;

                DataRow dr = ds.Tables[0].Rows[0];
                lblDateOfChallan.Visible = true;
                txtchallandt.Visible = true;

                fcidist.Text = dr["Dist_Id"].ToString();

                ddlfcidepo.SelectedItem.Text = dr["DepoName"].ToString();
                lblfdepo.Text = dr["FCIdepo"].ToString();


                lblfdist.Text = dr["FCIdistrict"].ToString();
                ddlfcidist.SelectedItem.Text = dr["FCIDname"].ToString();
                ddlfcidist.SelectedValue = dr["FCIdistrict"].ToString();
                string chdate = dr["Challan_Date"].ToString();
                string challan = getdate(chdate);
                txtchallandt.BackColor = System.Drawing.Color.Wheat;
                txtchallandt.ReadOnly = true;

                txtchallandt.Text = challan;
                txtqty.Text = dr["Qty_send"].ToString();

                txtqty.BackColor = System.Drawing.Color.Wheat;
                txtqty.ReadOnly = true;

                ddlcategory.SelectedItem.Text = dr["Category"].ToString();

                ddlcategory.BackColor = System.Drawing.Color.Wheat;
                ddlcategory.Enabled = false;

                if (ddlsarrival.SelectedItem.ToString() != "CMR")
                {
                    ddlcropyear.SelectedItem.Text = dr["Crop_year"].ToString();

                    ddlcropyear.BackColor = System.Drawing.Color.Wheat;
                    // ddlcropyear.Enabled = false;
                }
                txtnobags.Text = dr["No_of_Bags"].ToString();
                txtnobags.BackColor = System.Drawing.Color.Wheat;
                txtnobags.ReadOnly = true;
                string comid = dr["Commodity"].ToString();

                lblcomdty.Text = dr["Commodity"].ToString();

                txttono.Text = dr["TO_Number"].ToString();
                txtrono.Text = dr["RO_NO"].ToString();
                comdtyid = dr["Commodity"].ToString();
                lblcomdty.Text = dr["Commodity"].ToString();
                schemeid = dr["Scheme"].ToString();
                lblsch.Text = dr["Scheme"].ToString();

                //ddlscheme.SelectedItem.Text = schemeid;
                ddlscheme.SelectedItem.Text = dr["Scheme_Name"].ToString();
                ddlscheme.SelectedValue = dr["Scheme"].ToString();
                ddlcomdty.SelectedItem.Text = dr["Commodity_Name"].ToString();
                ddlcomdty.SelectedValue = dr["Commodity"].ToString();


                //ddlscheme . SelectedItem.Text = dr["Commodity"].ToString();


                ddltransport.SelectedItem.Text = dr["Transporter_Name"].ToString();
                lbltid.Text = dr["Transporter"].ToString();
                ddlalotmm.SelectedValue = dr["Month"].ToString();
                ddlallot_year.SelectedValue = dr["Year"].ToString();
                //ddltransport.SelectedValue = dr["Transporter"].ToString();
                //ddltransport.SelectedValue  = dr["Transporter"].ToString();
                ddltransport.BackColor = System.Drawing.Color.Wheat;
                ddltransport.Enabled = false;
                txtvehleno.Text = dr["Vehicle_No"].ToString();
                txtvehleno.BackColor = System.Drawing.Color.Wheat;
                txtvehleno.ReadOnly = true;
                txtrono.BackColor = System.Drawing.Color.Wheat;
                txtrono.ReadOnly = true;
                txttono.BackColor = System.Drawing.Color.Wheat;
                txttono.ReadOnly = true;
                ddlfcidepo.BackColor = System.Drawing.Color.Wheat;
                ddlfcidepo.Enabled = false;
                ddlfcidist.BackColor = System.Drawing.Color.Wheat;
                ddlfcidist.Enabled = false;

                ddlgtype.SelectedItem.Text = dr["Gunny_type"].ToString();
                ddlgtype.BackColor = System.Drawing.Color.Wheat;
                ddlgtype.Enabled = false;
                //st = "T";


                //mobj2 = new MoveChallan(ComObj);
                //string qry1 = "select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + comid + "'";
                //DataSet ds1 = mobj2.selectAny(qry1);
                //DataRow dr1 = ds1.Tables[0].Rows[0];
                //ddlcomdty.SelectedItem.Text = dr1["Commodity_Name"].ToString();

                //GetBalance();


            }
        }

    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }
    void Clear()
    {
        txtqty.ReadOnly = false;
        ddlcategory.BackColor = System.Drawing.Color.White;
        ddlcropyear.BackColor = System.Drawing.Color.White;
        ddltransport.BackColor = System.Drawing.Color.White;
        ddlgtype.BackColor = System.Drawing.Color.White;

        txtnobags.ReadOnly = false;
        txtnobags.BackColor = System.Drawing.Color.White;
        txtvehleno.ReadOnly = false;
        txtvehleno.BackColor = System.Drawing.Color.White;
        txtqty.BackColor = System.Drawing.Color.White;
        txtnobags.BackColor = System.Drawing.Color.White;



        ddltransport.SelectedItem.Text = "--Select--";
        ddlcategory.SelectedItem.Text = "--Select--";
        ddlgtype.SelectedItem.Text = "--Select--";
        // ddlscheme.SelectedItem.Text = "--Select--";
        ddlcomdty.SelectedItem.Text = "--Select--";

        ddlcategory.BackColor = System.Drawing.Color.Wheat;
        ddlcategory.Enabled = false;
        if (ddlsarrival.SelectedItem.ToString() != "CMR")
        {

            ddlcropyear.SelectedItem.Text = "--Select--";
            ddlcropyear.BackColor = System.Drawing.Color.White;
            // ddlcropyear.Enabled = true;
        }
        txtnobags.BackColor = System.Drawing.Color.White;
        txtnobags.ReadOnly = false;
        ddltransport.BackColor = System.Drawing.Color.White;
        ddltransport.Enabled = true;
        txtvehleno.BackColor = System.Drawing.Color.Wheat;
        txtvehleno.ReadOnly = false;
        ddlgtype.BackColor = System.Drawing.Color.White;
        ddlgtype.Enabled = true;
        ddlcategory.BackColor = System.Drawing.Color.White;
        ddlcategory.Enabled = true;
        txtvehleno.ReadOnly = false;
        txtvehleno.BackColor = System.Drawing.Color.White;
        txtqty.Text = "";
        txtvehleno.Text = "";
        txtnobags.Text = "";
    }

    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mcom = ddlcomdty.SelectedItem.Text;
        lblcomdty.Text = ddlcomdty.SelectedValue;

        if (mcom.Contains("Sugar"))
        {
            lblsch.Text = "0";
            ddlscheme.Visible = false;
            lblScheme.Visible = false;
            string coms = ddlcomdty.SelectedValue;
            mobj1 = new MoveChallan(ComObj);
            string qrysugar = "Select Sum(Current_Balance)as Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + coms + "'and Scheme_Id='" + lblsch.Text + "'";//and Source='" + source + "'";
            DataSet ds = mobj1.selectAny(qrysugar);

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
                decimal sqty = CheckNull(dr["Current_Balance"].ToString());

                if (sqty == 0)
                {
                    txtbalqty.Text = "0";
                    lblbalanceqty.Visible = true;
                    txtbalqty.Visible = true;
                    //txtbalqty.BackColor = System.Drawing.Color.Wheat;
                    txtbalqty.ReadOnly = true;
                }
                else
                {
                    txtbalqty.Text = sqty.ToString();
                    lblbalanceqty.Visible = true;
                    txtbalqty.Visible = true;
                    //txtbalqty.BackColor = System.Drawing.Color.Wheat;
                    txtbalqty.ReadOnly = true;
                }
            }






        }
        else
        {
            ddlscheme.Visible = true;
            lblScheme.Visible = true;

        }
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        RFV_RO.Enabled = false;
        RFV_TO.Enabled = false;
        lbl_pass.Visible = false;
        txt_pass.Visible = false;
        btnsubmit.Enabled = true;
        type = ddlsarrival.SelectedItem.ToString();
        if (ddlsarrival.SelectedItem.ToString() == "Procurement")
        {
            // Response.Redirect("CSC_Procurement.aspx"); 
            // Response.Redirect("Paddy_Procurement.aspx");
            Response.Redirect("CSC_Procurement.aspx");
        }
        else if (ddlsarrival.SelectedItem.ToString() == "From RailHead")
        {
            Response.Redirect("RR_Entry.aspx");

        }
        else if (ddlsarrival.SelectedValue.ToString() == "15")
        {
            Response.Redirect("Gunnybags_ReciptEntry.aspx");

        }
        else if (ddlsarrival.SelectedItem.ToString() == "Tender Purchase(by Rack)-Sugar/Salt")
        {
            Response.Redirect("~/IssueCenter/RR_Entry_Sugar.aspx");

        }
        else if (ddlsarrival.SelectedItem.Value == "08")
        {
            Response.Redirect("~/IssueCenter/Loss_Gain.aspx");

        }
        else if (ddlsarrival.SelectedItem.Text == "From Marketing Federation")
        {

            GetCommodityMarfed();

            ddlscheme.SelectedValue = "0";
            lblmonth.Text = "Month";
            lblyear.Text = "Year";
            status = "O";
            source_from = type;

            ddl_order.Visible = false;
            lblmonth.Visible = false;
            ddlalotmm.Visible = false;

            lblyear.Visible = false;
            ddlallot_year.Visible = false;

            Label6.Visible = true;
            ddlcropyear.Visible = true;

            lblTrans.Visible = false;
            ddltransport.Visible = false;

            lblfcdist.Visible = false;
            ddlfcidist.Visible = false;
            lbldepofci.Visible = false;
            ddlfcidepo.Visible = false;

            lblRecFromDist.Visible = true;
            ddldistrict.Visible = true;

            lblNameDepot.Visible = false;
            ddlissue.Visible = false;

            lblReleaseOrder.Visible = false;
            txtrono.Visible = false;
            lbltono.Visible = false;
            txttono.Visible = false;
            lblMillersName.Visible = false;
            ddlmiller.Visible = false;
            lblparty.Visible = false;
            DropDownList1.Visible = false;
            lblDateOfChallan.Visible = false;
            txtchallandt.Visible = false;
            //GetCommodity();

            lblsupplierorderno.Visible = false;

            qryforchallan = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and IsDeposit='N'";
            GetChallanmarfed();
            //qryforgetdata = "SELECT SCSC_Truck_challan.* ,Transporter_Table.Transporter_Name as Transporter_Name   FROM dbo.SCSC_Truck_challan left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + ddlchallan.SelectedValue + "'";
            Session["OD"] = "OD";
            st = "OD";

            Clear();

        }
        else if (ddlsarrival.SelectedItem.ToString() == "From FCI")
        {
            lblmonth.Text = "Allotment Month";
            lblyear.Text = "Allotment Year";
            RFV_RO.Enabled = true;
            RFV_TO.Enabled = true;
            status = "F";
            source_from = type;
            source_name = ddlfcidepo.SelectedItem.ToString();

            lblfcdist.Visible = true;
            ddlfcidist.Visible = true;


            lblmonth.Visible = false;
            ddlalotmm.Visible = false;

            lblyear.Visible = false;
            ddlallot_year.Visible = false;

            lblTrans.Visible = true;
            ddltransport.Visible = true;


            Label6.Visible = true;
            ddlcropyear.Visible = true;

            lbldepofci.Visible = true;
            ddlfcidepo.Visible = true;
            lblRecFromDist.Visible = false;
            ddldistrict.Visible = false;
            lblNameDepot.Visible = false;
            ddlissue.Visible = false;
            lblfcdist.Visible = true;
            ddlfcidist.Visible = true;
            lblReleaseOrder.Visible = true;
            txtrono.Visible = true;
            lbltono.Visible = true;
            txttono.Visible = true;
            lblparty.Visible = false;
            DropDownList1.Visible = false;
            lblMillersName.Visible = false;
            ddlmiller.Visible = false;
            lblDateOfChallan.Visible = false;
            txtchallandt.Visible = false;

            lblsupplierorderno.Visible = false;



            GetCommodity();
            qryforchallan = "SELECT * FROM dbo.Lift_A_RO where Send_District='" + did + "'and Issue_center='" + sid + "' and IsRecieved='N'";// and Month="+ddlalotmm.SelectedValue +" and Year="+ddlallot_year.SelectedValue ;

            GetChallan();

            //qryforgetdata = "SELECT Lift_A_RO.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name as Scheme_Name ,Transporter_Table.Transporter_Name as Transporter_Name  FROM dbo.Lift_A_RO left join dbo.tbl_MetaData_STORAGE_COMMODITY on Lift_A_RO.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on Lift_A_RO.Scheme=tbl_MetaData_SCHEME .Scheme_Id left join dbo.Transporter_Table on Lift_A_RO.Transporter=Transporter_Table.Transporter_ID   where Send_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";
            Session["FCI"] = "FCI";

            Clear();

        }
        else if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
        {
       //     lbl_pass.Visible = true;
       //   txt_pass.Visible = true;
       //   //ddlchallan.Visible = false;
       //   //lblchallanno.Visible = false;
       ////lblDateOfChallan.Visible = false;
       ////   txtchallandt.Visible = false;
       //   btnsubmit.Enabled = false;
       //   lblsupplierorderno.Visible = false;
       //   ddl_order.Visible = false;
          Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select pds movement order option for dispatch and recieving entries, Enter password for this entry....'); </script> ");

            txt_pass_TextChanged( sender,  e);

        }
        else if (ddlsarrival.SelectedItem.ToString() == "Levy Rice")
        {
            lblmonth.Text = "Month";
            lblyear.Text = "Year";
            status = "LR";
            source_from = type;

            lblmonth.Visible = false;
            ddlalotmm.Visible = false;

            lblyear.Visible = false;
            ddlallot_year.Visible = false;

            Label6.Visible = true;
            ddlcropyear.Visible = true;

            lblfcdist.Visible = false;
            ddlfcidist.Visible = false;
            lbldepofci.Visible = false;
            ddlfcidepo.Visible = false;
            lblRecFromDist.Visible = true;
            ddldistrict.Visible = true;
            lblNameDepot.Visible = false;
            ddlissue.Visible = false;
            lblMillersName.Visible = true;
            ddlmiller.Visible = true;

            lblReleaseOrder.Visible = false;
            txtrono.Visible = false;
            lbltono.Visible = false;
            txttono.Visible = false;
            lblparty.Visible = false;
            DropDownList1.Visible = false;
            lblDateOfChallan.Visible = false;
            txtchallandt.Visible = false;
            GetCommodityCMR();

            lblsupplierorderno.Visible = false;


            qryforchallan = "SELECT * FROM dbo.Lift_A_RO where Send_District=''and Issue_center=''";
            GetChallan();
            Clear();

        }
        else if (ddlsarrival.SelectedItem.ToString() == "CMR")
        {
            lblmonth.Text = "Month";
            lblyear.Text = "Year";
            status = "CMR";
            source_from = type;

            lblmonth.Visible = false;
            ddlalotmm.Visible = false;

            lblyear.Visible = false;
            ddlallot_year.Visible = false;

            lblTrans.Visible = false;
            ddltransport.Visible = false;

            Label6.Visible = true;
            ddlcropyear.Visible = true;

            lblfcdist.Visible = false;
            ddlfcidist.Visible = false;
            lbldepofci.Visible = false;
            ddlfcidepo.Visible = false;
            lblRecFromDist.Visible = true;
            ddldistrict.Visible = true;
            lblNameDepot.Visible = false;
            ddlissue.Visible = false;
            lblMillersName.Visible = true;
            ddlmiller.Visible = true;

            lblReleaseOrder.Visible = false;
            txtrono.Visible = false;
            lbltono.Visible = false;
            txttono.Visible = false;

            lblsupplierorderno.Visible = false;



            lblDateOfChallan.Visible = false;
            txtchallandt.Visible = false;
            lblparty.Visible = false;
            DropDownList1.Visible = false;
            GetCommodityCMR();
            qryforchallan = "SELECT * FROM dbo.Lift_A_RO where Send_District=''and Issue_center=''";
            GetChallan();
            Clear();

        }
        else if (ddlsarrival.SelectedItem.ToString() == "Tender Purchase(by Road)-Sugar/Salt")
        {
            lblmonth.Text = "Month";
            lblyear.Text = "Year";
            status = "Other Source";
            source_from = type;

            lblmonth.Visible = false;
            ddlalotmm.Visible = false;

            lblyear.Visible = false;
            ddlallot_year.Visible = false;

            lblTrans.Visible = false;
            ddltransport.Visible = false;

            Label6.Visible = true;
            ddlcropyear.Visible = true;

            lblTrans.Visible = false;
            ddltransport.Visible = false;

            lblfcdist.Visible = false;
            ddlfcidist.Visible = false;
            lbldepofci.Visible = false;
            ddlfcidepo.Visible = false;
            lblRecFromDist.Visible = false;
            ddldistrict.Visible = false;
            lblNameDepot.Visible = false;
            ddlissue.Visible = false;
            lblMillersName.Visible = false;
            ddlmiller.Visible = false;
            lblparty.Visible = true;
            DropDownList1.Visible = true;
            lblReleaseOrder.Visible = false;
            txtrono.Visible = false;
            lbltono.Visible = false;
            txttono.Visible = false;

            lblDateOfChallan.Visible = false;
            txtchallandt.Visible = false;

            lblsupplierorderno.Visible = true;


            qryforchallan = "SELECT * FROM dbo.Lift_A_RO where Send_District=''and Issue_center=''";
            GetChallan();
            GetCommoditytenderPurchase();
            Clear();

        }

        else if (ddlsarrival.SelectedItem.ToString() == "Other Source")
        {

        }

        lblbalanceqty.Visible = false;
        txtbalqty.Visible = false;

    }
    protected void getselection_other_depo()
    {
        lblmonth.Text = "Month";
        lblyear.Text = "Year";
        status = "O";
        source_from = type;

        ddl_order.Visible = false;
        lblmonth.Visible = false;
        ddlalotmm.Visible = false;

        lblyear.Visible = false;
        ddlallot_year.Visible = false;

        Label6.Visible = true;
        ddlcropyear.Visible = true;


        lblfcdist.Visible = false;
        ddlfcidist.Visible = false;
        lbldepofci.Visible = false;
        ddlfcidepo.Visible = false;

        lblRecFromDist.Visible = true;
        ddldistrict.Visible = true;

        lblNameDepot.Visible = true;
        ddlissue.Visible = true;

        lblReleaseOrder.Visible = false;
        txtrono.Visible = false;
        lbltono.Visible = false;
        txttono.Visible = false;
        lblMillersName.Visible = false;
        ddlmiller.Visible = false;
        lblparty.Visible = false;
        DropDownList1.Visible = false;
        lblDateOfChallan.Visible = false;
        txtchallandt.Visible = false;
        GetCommodity();

        lblsupplierorderno.Visible = false;

        qryforchallan = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and IsDeposit='N'";
        GetChallan();
        //qryforgetdata = "SELECT SCSC_Truck_challan.* ,Transporter_Table.Transporter_Name as Transporter_Name   FROM dbo.SCSC_Truck_challan left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + ddlchallan.SelectedValue + "'";
        Session["OD"] = "OD";
        st = "OD";

        Clear();
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetGunny();
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsch.Text = ddlscheme.SelectedValue;
        string mcomid = ddlcomdty.SelectedValue;
        string mcatid = ddlcategory.SelectedValue;
        string mscheme = lblsch.Text;

        string source = ddlsarrival.SelectedValue;
        mobj1 = new MoveChallan(ComObj);

        string qry = "Select  Sum(convert(decimal(18,5),Current_Balance))as Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'and Source='" + source + "'";
        DataSet ds = mobj1.selectAny(qry);
        if (ds == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
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
                decimal qty = CheckNull(dr["Current_Balance"].ToString());
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
                    lblbalanceqty.Visible = true;
                    txtbalqty.Visible = true;
                    //txtbalqty.BackColor = System.Drawing.Color.Wheat;
                    txtbalqty.ReadOnly = true;
                }
            }
        }
    }

    void GetBalance()
    {
        string mcomid = comdtyid;

        //string mcatid = ddlcategory.SelectedValue;
        string mscheme = schemeid;
        int month = int.Parse(ddlalotmm.SelectedValue.ToString());
        int year = int.Parse(ddlallot_year.SelectedValue.ToString());
        string source = ddlsarrival.SelectedValue;
        mobj1 = new MoveChallan(ComObj);
        string godown = ddlgodown.SelectedValue;
        string qry = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'and Source='" + source + "' and Godown='" + godown + "'";
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
            txtbalqty.Text = (System.Math.Round(CheckNull(dr["Current_Balance"].ToString()), 5)).ToString();
            lblbalanceqty.Visible = true;
            txtbalqty.Visible = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.ReadOnly = true;
        }
    }
    protected void ddlgtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {


        string opid = Session["OperatorId"].ToString();

        string state = Session["State_Id"].ToString();

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
            //if (chkcap > maxcap)
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sorry Space is not available at Godown....'); </script> ");
            //}
            //else
            //{

                string chalanst = "";
                if (ddlsarrival.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--" || ddlcomdty.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Source Of Arrival/Receipt Godown /Commodity/Scheme/Transporter...'); </script> ");
                }
                else
                {
                    int month = int.Parse(ddlalotmm.SelectedValue.ToString());
                    int year = int.Parse(ddlallot_year.SelectedValue.ToString());
                    decimal cqty = CheckNull(txtqty.Text);
                    decimal rqty = CheckNull(txtrqty.Text);
                    decimal vqty = (cqty - rqty);

                    if (ddlsarrival.SelectedItem.Text == "From FCI")
                    {
                        status = "F";
                        source_from = ddlsarrival.SelectedValue;
                        source_name = lblfdepo.Text;
                        district = lblfdist.Text;
                        depoto = lblfdepo.Text;
                        RO_NO = txtrono.Text;
                        Dispatch_Date = "";
                        tono = txttono.Text;
                    }
                    else if (ddlsarrival.SelectedItem.Text == "Other Depot")
                    {
                        if (ddldistrict.SelectedItem.Text == "-Select-" || ddlissue.SelectedItem.Text == "-Select-" || ddlcropyear.SelectedItem.Text == "-Select-" || ddldistrict.SelectedItem.Value == "01" || ddlissue.SelectedItem.Value == "01" || ddlcropyear.SelectedItem.Value == "01")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Sending District or Depot or Crop Year'); </script> ");
                            return;
                        }

                        status = "O";
                        source_from = ddlsarrival.SelectedValue;
                        source_name = "";
                        district = dcode.Text; ;
                        depoto = icode.Text;
                        Dispatch_Date = "";
                        tono = "";
                        RO_NO = "";

                    }
                    else if (ddlsarrival.SelectedItem.Text == "From Marketing Federation")
                    {
                        status = "O";
                        source_from = ddlsarrival.SelectedValue;
                        source_name = "";
                        district = dcode.Text; ;
                        depoto = icode.Text;
                        Dispatch_Date = "";
                        tono = "";
                        RO_NO = "";

                    }
                    else if (ddlsarrival.SelectedItem.ToString() == "CMR")
                    {
                        status = "C";
                        source_from = ddlsarrival.SelectedValue;
                        if (ddlmiller.SelectedValue == "0" || ddlmiller.SelectedItem.Text == "--Select--" || ddlmiller.SelectedValue == "--Select--")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Miller Name...'); </script> ");
                            return;
                        }
                        else
                        {
                            source_name = ddlmiller.SelectedValue;
                            district = dcode.Text;
                            depoto = "";
                            Dispatch_Date = "";
                            tono = "";
                            RO_NO = "";
                        }

                    }
                    else if (ddlsarrival.SelectedItem.ToString() == "Tender Purchase(by Road)-Sugar/Salt")
                    {
                        status = "OC";
                        source_from = ddlsarrival.SelectedValue;
                        source_name = DropDownList1.SelectedValue;
                        if (DropDownList1.SelectedItem.Text == "--Select--")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Supplier Name...'); </script> ");
                            return;

                        }
                        else
                        {
                            district = "";
                            depoto = "";
                            Dispatch_Date = "";
                            tono = "";
                            RO_NO = "";
                        }
                    }
                    else if (ddlsarrival.SelectedItem.ToString() == "Levy Rice")
                    {
                        status = "L";
                        source_from = ddlsarrival.SelectedValue;
                        source_name = ddlmiller.SelectedValue;

                        district = "";
                        depoto = "";
                        Dispatch_Date = "";
                        tono = "";
                        RO_NO = "";

                    }
                    mobj1 = new MoveChallan(ComObj);
                    string qrey = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + sid + "' and Dist_Id='" + did + "'";
                    DataSet ds = mobj1.selectAny(qrey);
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
                    }

                    try
                    {
                        if (ddlsarrival.SelectedItem.Text != "From Marketing Federation")
                        {

                            if (Session["st"].ToString() == "T")
                            {
                                Challan_no = ddlchallan.SelectedItem.ToString();
                                string chdt = getmmddyy(txtchallandt.Text);
                                Challan_Date = chdt;
                                comdtyid = lblcomdty.Text;
                                schemeid = lblsch.Text;
                                Dispatch_Date = chdt;
                                chalanst = "I";
                            }
                            else
                            {
                                Challan_no = txtchallan.Text;
                                string chdt1 = getDate_MDY(DaintyDate3.Text);
                                Challan_Date = chdt1;
                                comdtyid = ddlcomdty.SelectedValue;
                                schemeid = ddlscheme.SelectedValue.ToString();
                                Dispatch_Date = getDate_MDY(DaintyDate3.Text);
                                chalanst = "N";
                            }
                        }
                        else
                        {
                            Challan_no = txtchallan.Text;
                            string chdt1 = getDate_MDY(DaintyDate3.Text);
                            Challan_Date = chdt1;
                            comdtyid = ddlcomdty.SelectedValue;
                            schemeid = lblsch.Text;
                            Dispatch_Date = getDate_MDY(DaintyDate3.Text);
                            chalanst = "N";

                        }

                        string adate = getDate_MDY(DaintyDate1.Text);

                        string chadate = getDate_MDY(DaintyDate3.Text);

                        time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());


                        string mchallan = txtchallan.Text;
                        string chdate = getDate_MDY(DaintyDate3.Text);
                        string ardate = getDate_MDY(DaintyDate1.Text);
                        string mcomdty = lblcomdty.Text;
                        string mscheme = lblsch.Text;
                        string mcropy = ddlcropyear.SelectedItem.ToString();
                        int mrecbags = CheckNullInt(txtrecbags.Text);
                        string mcategry = ddlcategory.SelectedValue;
                        //string mtransporter = ddltransport.SelectedValue;
                        string mtransporter = lbltid.Text;
                        string mvehicleno = txtvehleno.Text;
                        string mtime = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
                        int mbagno = CheckNullInt(txtnobags.Text);
                        decimal mrqty = CheckNull(txtrqty.Text);
                        decimal mmoisture = CheckNull(txtmoisture.Text);
                        string mwcm = txtwcmno.Text;
                        string gdwn = ddlgodown.SelectedValue;
                        string Orderno = ddl_order.SelectedValue.ToString();
                        decimal mqty = CheckNull(txtqty.Text);
                        int recdbags = CheckNullInt(txtrecbags.Text);
                        // string Created_date = getDate_MDY(DateTime.Today.Date.ToString());//.ToString();
                        string updated_date = "";
                        //string deleted_date = "";
                        string notrans = "N";
                        string isd = "N";
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        mobj1 = new MoveChallan(ComObj);
                        string checkchallan = "Select challan_no from dbo.tbl_Receipt_Details where Dist_Id='" + did + "' and  Depot_ID='" + sid + "' and challan_no='" + Challan_no + "'";
                        DataSet dschallan = mobj1.selectAny(checkchallan);
                        if (dschallan.Tables[0].Rows.Count != 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan No Already Exist...'); </script> ");

                        }
                        else
                        {
                            string S_Detailsname;
                            if (ddlsarrival.SelectedItem.ToString() == "CMR")
                            {
                                source_name = ddlmiller.SelectedValue;
                            }
                            else
                            {
                                source_name = "";
                            }

                            string qryInsert = "insert into dbo.tbl_Receipt_Details( State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch)values('" + state + "','" + did + "','" + sid + "','" + gatepass + "','" + source_from + "','" + source_name + "','" + district + "','" + depoto + "','" + RO_NO + "','" + tono + "','" + Dispatch_Date + "','" + adate + "','" + Challan_no + "','" + Challan_Date + "'," + mqty + ",'" + mcomdty + "','" + schemeid + "','" + mcropy + "','" + mcategry + "','" + mtransporter + "','" + mvehicleno + "','" + mtime + "','" + ddlgtype.SelectedItem.ToString() + "'," + mbagno + "," + mrqty + "," + mrecbags + "," + mmoisture + ",'" + mwcm + "'," + vqty + "," + month + "," + year + ",'" + isd + "','" + ip + "',getdate(),'" + updated_date + "','" + chalanst + "','" + gdwn + "','" + opid + "','" + notrans + "','" + Orderno + "','" + ddlBranch .SelectedValue.ToString()+ "')";

                            cmd.CommandText = qryInsert;
                            cmd.Connection = con;
                            try
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                count = cmd.ExecuteNonQuery();
                                con.Close();

                                string qryTranscut = "insert into dbo.tbl_Receipt_Details_Trans_Log(State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Transporter,Vehile_no,Arrival_time,No_of_Bags,Recd_Qty,Recieved_Bags,Month,Year,IP_Address,updated_date,Godown,Operation,OperatorID,Branch)values('" + state + "','" + did + "','" + sid + "','" + gatepass + "','" + source_from + "','" + source_name + "','" + district + "','" + depoto + "','" + RO_NO + "','" + tono + "','" + Dispatch_Date + "','" + adate + "','" + Challan_no + "','" + Challan_Date + "'," + mqty + ",'" + mcomdty + "','" + schemeid + "','" + mtransporter + "','" + mvehicleno + "','" + mtime + "'," + CheckNullInt(txtnobags.Text) + "," + CheckNull(txtrqty.Text) + "," + CheckNullInt(txtrecbags.Text) + "," + ddlalotmm.SelectedValue + "," + ddlallot_year.SelectedValue + ",'" + ip + "',getdate(),'" + ddlgodown.SelectedValue + "','I','" + opid + "','" + ddlBranch.SelectedValue.ToString() + "')";
                                cmd.CommandText = qryTranscut;
                                cmd.Connection = con;
                                try
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                                catch (Exception ex)
                                {
                                }
                                finally
                                {
                                }

                                if (ddlsarrival.SelectedItem.Text == "From FCI")
                                {
                                    string qrystock = "select  Sum(convert(decimal(18,5),Recd_Qty))  as Qty from dbo.tbl_Receipt_Details where Commodity='" + mcomdty + "' and Scheme ='" + mscheme + "' and Dist_Id='" + did + "'and Depot_ID='" + sid + "' and S_of_arrival='" + ddlsarrival.SelectedValue + "' and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrfci = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdty + "'and Scheme_ID='" + mscheme + "' and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "' and Scheme_Id ='" + mscheme + "'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }

                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdty + "','" + mscheme + "'," + sropen + "," + 0 + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd.CommandText = qryinsr;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_FCI=" + mrfci + " where Commodity_Id ='" + mcomdty + "' and Scheme_ID='" + mscheme + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }

                                    string qrylaro = "Update dbo.Lift_A_RO set IsRecieved='Y' Where Send_District='" + did + "'and Issue_center='" + sid + "' and Challan_No='" + Challan_no + "' and Dist_Id= '" + fcidist.Text + "'";
                                    cmd.CommandText = qrylaro;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int countm = cmd.ExecuteNonQuery();
                                    con.Close();

                                }
                                else if (ddlsarrival.SelectedItem.Text == "Other Depot")
                                {
                                    //if (countmm >= 1)
                                    //{
                                    string qrystock = "select Sum(convert(decimal(18,5),Recd_Qty)) as Qty from dbo.tbl_Receipt_Details where Commodity='" + mcomdty + "'and Scheme ='" + mscheme + "' and Dist_Id='" + did + "'and Depot_ID='" + sid + "'and S_of_arrival='" + ddlsarrival.SelectedValue + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdty + "' and Scheme_ID='" + mscheme + "' and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id ='" + mscheme + "'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }

                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdty + "','" + mscheme + "'," + sropen + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd.CommandText = qryinsr;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }

                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=" + mrod + " where Commodity_Id ='" + mcomdty + "' and Scheme_ID='" + mscheme + "'and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }


                                    }
                                    string qrylaro = "Update dbo.SCSC_Truck_challan set IsDeposit='Y' Where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + Challan_no + "'";
                                    cmd.CommandText = qrylaro;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int countmm = cmd.ExecuteNonQuery();
                                    con.Close();

                                    //}
                                }

                                else if (ddlsarrival.SelectedItem.Text == "From Marketing Federation")
                                {
                                    //if (countmm >= 1)
                                    //{
                                    string qrystock = "select Sum(convert(decimal(18,5),Recd_Qty)) as Qty from dbo.tbl_Receipt_Details where Commodity='" + mcomdty + "'and Scheme ='" + mscheme + "' and Dist_Id='" + did + "'and Depot_ID='" + sid + "'and S_of_arrival='" + ddlsarrival.SelectedValue + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdty + "' and Scheme_ID='" + mscheme + "' and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id ='" + mscheme + "'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }

                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdty + "','" + mscheme + "'," + sropen + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd.CommandText = qryinsr;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }

                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=" + mrod + " where Commodity_Id ='" + mcomdty + "' and Scheme_ID='" + mscheme + "'and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }


                                    }
                                    string qrylaro = "Update dbo.SCSC_Truck_challan set IsDeposit='Y' Where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + Challan_no + "'";
                                    cmd.CommandText = qrylaro;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int countmm = cmd.ExecuteNonQuery();
                                    con.Close();

                                    //}
                                }
                                else if (ddlsarrival.SelectedItem.Text == "Tender Purchase(by Road)")
                                {
                                    //if (countmm >= 1)
                                    //{
                                    string qrystock = "select Sum(convert(decimal(18,5),Recd_Qty)) as Qty from dbo.tbl_Receipt_Details where Commodity='" + mcomdty + "' and Scheme ='" + mscheme + "' and Dist_Id='" + did + "'and Depot_ID='" + sid + "'and S_of_arrival='" + ddlsarrival.SelectedValue + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdty + "'   and Scheme_ID='" + mscheme + "' and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id ='" + mscheme + "'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }

                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdty + "','" + mscheme + "'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd.CommandText = qryinsr;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }

                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Other_Src=" + mrod + " where Commodity_Id ='" + mcomdty + "' and Scheme_ID='" + mscheme + "'and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }


                                    }
                                }
                                else if (ddlsarrival.SelectedItem.Text == "CMR")
                                {
                                    //if (countmm >= 1)
                                    //{
                                    string qrystock = "select Sum(convert(decimal(18,5),Recd_Qty)) as Qty from dbo.tbl_Receipt_Details where Commodity='" + mcomdty + "' and Scheme ='" + mscheme + "' and Dist_Id='" + did + "'and Depot_ID='" + sid + "'and S_of_arrival='" + ddlsarrival.SelectedValue + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdty + "'   and Scheme_ID='" + mscheme + "' and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id ='" + mscheme + "'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }

                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdty + "','" + mscheme + "'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd.CommandText = qryinsr;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }

                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Received_CMR=" + mrod + " where Commodity_Id ='" + mcomdty + "'  and Scheme_ID='" + mscheme + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                                else if (ddlsarrival.SelectedItem.Text == "Levy Rice")
                                {
                                    //if (countmm >= 1)
                                    //{
                                    string qrystock = "select Sum(convert(decimal(18,5),Recd_Qty)) as Qty from dbo.tbl_Receipt_Details where Commodity='" + mcomdty + "' and Scheme ='" + mscheme + "' and Dist_Id='" + did + "'and Depot_ID='" + sid + "'and S_of_arrival='" + ddlsarrival.SelectedValue + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdty + "'  and Scheme_ID='" + mscheme + "' and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id ='" + mscheme + "'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }

                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdty + "','" + mscheme + "'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtrqty.Text) + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd.CommandText = qryinsr;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }

                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Received_Levy=" + mrod + " where Commodity_Id ='" + mcomdty + "'  and Scheme_ID='" + mscheme + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                                Session["Receipt_ID"] = gatepass;
                                HyperLink1.Visible = true;

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


                            //if (count == 1)
                            //{

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully...'); </script> ");
                            btnsubmit.Enabled = false;
                            decimal ruqty = CheckNull(txtrqty.Text);
                            decimal buqty = CheckNull(txtbalqty.Text);
                            decimal uuqty = (ruqty + buqty);
                            string mcomid = ddlcomdty.SelectedValue;
                            string mcatid = ddlcategory.SelectedValue;
                            string source = ddlsarrival.SelectedValue;
                            string mstate = "23";
                            decimal bags = CheckNullInt(txtrecbags.Text);
                            decimal quanty = CheckNull(txtrqty.Text);
                            decimal openqty = 0;
                            decimal openbag = 0;
                            string pdate = getDate_MDY("01/04/2011");
                            int curbag = CheckNullInt(txtrecbags.Text);
                            string mmmgn = ddlgodown.SelectedValue;
                            //int month = int.Parse(ddlalotmm.SelectedValue.ToString ());
                            //int year = int.Parse(ddlallot_year.SelectedValue.ToString ());
                            //string source = ddlsarrival.SelectedValue;
                            //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string udate = "";
                            string ddate = "";
                            decimal mmcbags = CheckNullInt(txtrecbags.Text);
                            //string schemeid = ddlscheme.SelectedValue;
                            if (ddlcomdty.SelectedItem.Text.Contains("Sugar") || ddlcomdty.SelectedItem.Text.Contains("Salt"))
                            {
                                string chkopen = "Select Quantity from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id='0' and Godown='" + gdwn + "' and Source='" + source + "'";
                                mobj = new MoveChallan(ComObj);
                                DataSet dsqry = mobj.selectAny(chkopen);
                                if (dsqry == null)
                                {

                                }

                                else
                                {
                                    if (dsqry.Tables[0].Rows.Count == 0)
                                    {
                                        string mggn = ddlgodown.SelectedValue;
                                        string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate,OperatorID) values('" + mstate + "','" + did + "','" + sid + "','" + mcomdty + "','0','','" + mggn + "',''," + openbag + "," + openqty + ",'" + source + "'," + quanty + "," + mmcbags + "," + month + "," + year + ",'" + ip + "','" + pdate + "',getdate(),'" + udate + "','" + ddate + "','" + opid + "')";
                                        cmd.CommandText = qreyins;
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        decimal mmrecdq = CheckNull(txtrqty.Text);
                                        string query = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5),Current_Balance)+" + mmrecdq + ",Current_Bags=Current_Bags+" + curbag + " where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id='0' and Source='" + source + "'and Godown='" + mmmgn + "'";
                                        cmd.CommandText = query;
                                        cmd.Connection = con;

                                        try
                                        {
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            Label1.Text = ex.Message;

                                        }
                                        finally
                                        {
                                            con.Close();

                                        }

                                    }

                                }
                            }
                            else
                            {
                                string mcomcode = lblcomdty.Text;
                                //if (mcomcode == "22")
                                //{
                                //    prosource = "01";
                                //}
                                //else
                                //{
                                //    prosource = ddlsarrival.SelectedValue;
                                //}
                                string mggn = ddlgodown.SelectedValue;
                                int recdbagq = CheckNullInt(txtrecbags.Text);
                                string chkopenbal = "Select * from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id='" + schemeid + "' and Godown='" + gdwn + "' and Source='" + ddlsarrival.SelectedValue + "'";
                                //mobj = new MoveChallan(ComObj);

                                distobj = new DistributionCenters(ComObj);
                                DataSet dsbal = distobj.selectAny(chkopenbal);
                                if (dsbal == null)
                                {

                                }

                                else
                                {
                                    if (dsbal.Tables[0].Rows.Count == 0)
                                    {
                                        string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + did + "','" + sid + "','" + mcomdty + "','" + mscheme + "','','" + mggn + "',''," + openbag + "," + openqty + ",'" + ddlsarrival.SelectedValue + "'," + quanty + "," + recdbagq + "," + month + "," + year + ",'" + ip + "',getdate(),getdate(),'" + udate + "','" + ddate + "'" + ")";
                                        cmd.CommandText = qreyins;
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                        con.Close();

                                    }
                                    else
                                    {
                                        string mugdn = ddlgodown.SelectedValue;
                                        decimal mmrecdq = CheckNull(txtrqty.Text);
                                        string query = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5),Current_Balance)+" + mmrecdq + ",Current_Bags=Current_Bags+" + curbag + " where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdty + "'and Scheme_Id='" + lblsch.Text + "'and Source='" + ddlsarrival.SelectedValue + "' and Godown='" + mugdn + "'";
                                        cmd.CommandText = query;
                                        cmd.Connection = con;

                                        try
                                        {
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            Label1.Text = ex.Message;

                                        }
                                        finally
                                        {
                                            con.Close();

                                        }

                                    }

                                }

                                decimal gcapacity = CheckNull(txtmaxcap.Text);
                                decimal gcurrcap = gcapacity - openqty;
                                string mgodown = ddlgodown.SelectedValue;
                                string select = "Select * from Current_Godown_Position where District_Id='" + did + "' and Depotid='" + sid + "' and Godown='" + mgodown + "'";
                                mobj = new MoveChallan(ComObj);
                                DataSet dsgdn = mobj.selectAny(select);
                                if (dsgdn.Tables[0].Rows.Count == 0)
                                {
                                    string qreygdn = "insert into dbo.Current_Godown_Position(District_Id,Depotid,Godown,Current_Balance,Current_Bags,Month,Year,IP_Address,CreatedDate,UpdatedDate,DeletedDate,Godown_Capacity,Current_Capacity) values('" + did + "','" + sid + "','" + mgodown + "'," + openqty + "," + openbag + "," + month + "," + year + ",'" + ip + "',getdate(),'" + udate + "','" + ddate + "'," + gcapacity + "," + gcurrcap + ")";
                                    cmd.CommandText = qreygdn;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                }
                                else
                                {
                                    DataRow drgdn = dsgdn.Tables[0].Rows[0];
                                    decimal recdqty = CheckNull(txtrqty.Text);
                                    int mrecdbags = CheckNullInt(txtrecbags.Text);
                                    string capact = drgdn["Current_Capacity"].ToString();
                                    string cbags = drgdn["Current_Bags"].ToString();
                                    string ccqty = drgdn["Current_Balance"].ToString();
                                    decimal ucap = CheckNull(capact) - recdqty;
                                    decimal ucqty = CheckNull(ccqty) + recdqty;
                                    int ucbags = CheckNullInt(cbags) + mrecdbags;
                                    string qreygdnU = "update dbo.Current_Godown_Position set Current_Capacity=convert(decimal(18,5),Current_Capacity)-" + CheckNull(txtrqty.Text) + ",Current_Bags=Current_Bags+" + CheckNullInt(txtrecbags.Text) + ",Current_Balance=Current_Balance+" + CheckNull(txtrqty.Text) + " where District_Id='" + did + "' and Depotid='" + sid + "' and Godown='" + mgodown + "'";
                                    cmd.CommandText = qreygdnU;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                if (ddlsarrival.SelectedItem.ToString() == "From FCI")
                {

                    HyperLink1.Attributes.Add("onclick", "window.open('print_Gatepass_FCI.aspx',null,'left=250, top=0, height=550, width= 600, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
                }
                else if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
                {
                    HyperLink1.Attributes.Add("onclick", "window.open('print_Gatepass.aspx',null,'left=250, top=0, height=550, width=600, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
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
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void txtrqty_TextChanged(object sender, EventArgs e)
    {
        //GetCat();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("issue_welcome.aspx");
    }
    protected void btnaddmore_Click(object sender, EventArgs e)
    {


    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
        dcode.Text = ddldistrict.SelectedValue;


        ddl_order.Visible = false;


        ddlmiller.Enabled = true;
        Miller();





    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
        {
            qryforgetdata = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and  Challan_No='" + ddlchallan.SelectedValue + "'";
            GetData();
        }
        else if (ddlsarrival.SelectedItem.ToString() == "From Marketing Federation")
        {
            qryforgetdata = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and  Challan_No='" + ddlchallan.SelectedValue + "'";
            txtchallan.Visible = true;
            lblchallanno.Visible = true;

        }
        else
        {
            qryforgetdata = "SELECT * FROM dbo.Lift_A_RO where Send_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";
            GetData();
        }


    }
    protected void ddlfcidepo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblfdepo.Text = ddlfcidepo.SelectedValue;
    }
    protected void btnclose_Click1(object sender, EventArgs e)
    {

        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void ddlfcidist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFCIdepot();
        lblfdist.Text = ddlfcidist.SelectedValue;

    }
    protected void ddltransport_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbltid.Text = ddltransport.SelectedValue;
    }
    protected void ddlmiller_SelectedIndexChanged(object sender, EventArgs e)
    {
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT * FROM dbo.Miller_Master where Miller_ID='" + ddlmiller.SelectedValue.ToString() + "' and District_Code='" + ddldistrict.SelectedValue.ToString() + "' ";

        DataSet ds = mobj.selectAny(qrygdn);
        DataRow dr = ds.Tables[0].Rows[0];
        string cropyear = dr["crop_year"].ToString();
        if (cropyear == "")
        {
            ddlcropyear.Enabled = true;

        }

        else
        {

            ddlcropyear.SelectedItem.Text = cropyear;
            //  ddlcropyear.Enabled = false;

        }
    }
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    void GetBalQty()
    {
        string mcomid = ddlcomdty.SelectedValue;
        string mcatid = ddlcategory.SelectedValue;
        string mscheme = lblsch.Text;
        string mgodown = ddlgodown.SelectedValue;
        string source = ddlsarrival.SelectedValue;
        mobj1 = new MoveChallan(ComObj);

        string qry = "Select Sum(convert(decimal(18,5),Current_Balance))as Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Godown='" + mgodown + "'and Source='" + source + "'";
        DataSet ds = mobj1.selectAny(qry);
        if (ds == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
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

    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCapacity();
        txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txtbalqty.Text = "";

        if (ddlgodown.SelectedIndex > 0)
        {
            string gname = ddlgodown.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + sid + "' and Godown_ID='" + gname + "'";

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


    }
    void GetCapGodown()
    {
        //string gname = ddlgodown.SelectedValue;
        //mobj = new MoveChallan(ComObj);
        //string qrygdn = "SELECT Sum(Current_Balance) as Current_Balance  FROM dbo.issue_opening_balance where District_Id='" + did + "' and Depotid='" + sid + "' and Godown='" + gname + "'";

        //DataSet ds = mobj.selectAny(qrygdn);
        //if (ds == null)
        //{
        //}

        //else
        //{
        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        txtcurntcap.Text = "";

        //    }
        //    else
        //    {
        //        DataRow dr = ds.Tables[0].Rows[0];
        //        txtcurntcap.Text = (System.Math.Round (CheckNull(dr["Current_Balance"].ToString()),5)).ToString();
        //        txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text),5)).ToString();
        //    }


        //}


        string Godown = ddlgodown.SelectedItem.Value;

        Int64 comid = Convert.ToInt64(Godown);

        string pqry = "available_space_godown";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdpqty = new SqlCommand(pqry, con);
        cmdpqty.CommandType = CommandType.StoredProcedure;


        cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = did;
        cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = ddlBranch.SelectedValue.ToString();
        cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = Godown;

        DataSet ds = new DataSet();
        SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);
        //dr.SelectCommand.CommandTimeout = 600;

        dr.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

            txtavalcap.Text = Convert.ToString(stock);

            txtcurntcap.Text = Convert.ToString(stock);
            txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

        }

    }
    protected void ddlrackno_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void ddlgtype_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        icode.Text = ddlissue.SelectedValue;
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/mpsc_move_challan.aspx");
    }
    protected void ddlalotmm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlallot_year_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtchallandt_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlsarrival_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        if (ddl != null)
        {
            string qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID!='01' order by Source_id";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int d = 0; d < ds.Tables[0].Rows.Count; d++)
            {
                ddl.Items[d].Attributes.Add("title", ds.Tables[0].Rows[d]["Hindi"].ToString());
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            string query = "select distinct tbl_SupplyOrder_Master.Orderno from tbl_SupplyOrder_Master where tbl_SupplyOrder_Master.S_name ='" + DropDownList1.SelectedItem.Text.ToString() + "' and district_code='" + did + "' and  Depot_ID='" + sid + "'";
            SqlCommand cmdnew = new SqlCommand(query, con);
            SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
            DataSet dsnew = new DataSet();
            danew.Fill(dsnew);
            if (dsnew == null)
            {

            }
            else
            {
                ddl_order.Items.Clear();
                ddl_order.DataSource = dsnew.Tables[0];
                ddl_order.DataTextField = "Orderno";
                ddl_order.DataValueField = "Orderno";
                ddl_order.DataBind();

            }
        }
        catch (Exception ex)
        {

            Label1.Visible = true;
            Label1.Text = ex.Message;

        }
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
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='{0}'", ddlBranch.SelectedValue.ToString());
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
    protected void txt_pass_TextChanged(object sender, EventArgs e)
    {
        //if (txt_pass.Text == "mpscsc123")
        //{
            getselection_other_depo();
            btnsubmit.Enabled = true;
            lbl_pass.Visible = false;
        // txt_pass.Visible = false;
        //}
        //else
        //{
        //    //Response.Redirect("~/IssueCenter/issue_welcome.aspx");
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Correct password....'); </script> ");


        //}
          
    }
}
