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

public partial class IssueCenter_Movement_Challan_WLC : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Transporter tobj = null;
    MoveChallan mobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj2 = null;
    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
    protected Common ComObjWlC = null, cmnWLC = null;
    public string time;
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string gatepass = "";
    public int getnum = 0;
    public string source_from = "";
    public string source_name = "";
    public string status = "";
    public string district = "";
    public string depoto = "";
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();

            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtwcmno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtmoisture.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");


            txtmoisture.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtchallan.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtnobags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtwcmno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();



            btnadd.Attributes.Add("onclick", "window.open('GunnyBagsDetails.aspx',null,'left=300, top=10, height=400, width=580, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            ComObjWlC = new Common(ConfigurationManager.AppSettings["ConnectionWlc"].ToString());
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
          
          
            distobj = new DistributionCenters(ComObj);

            if (!IsPostBack)
            {

                Transport();
                GetScheme();
                GetGunny();
                GetCommodity();
                GetCategory();
                GetDist();
                GetDCName();
                GetRecdCategory();
                GetSource();
                //GetFCIdist();
                GetDepositorType();
                GetVehicleType();

            }


        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");




        }

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
    //void GetDistFCI()
    //{
    //    DObj = new Districts(ComObj);
    //    DataSet ds = DObj.selectAll(" order by district_name");

    //    ddlfcidepo.DataSource = ds.Tables[0];
    //    ddlfcidepo.DataTextField = "district_name";
    //    ddlfcidepo.DataValueField = "District_Code";

    //    ddlfcidepo.DataBind();
    //    ddlfcidepo.Items.Insert(0, "--Select--");
    //}
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
    //void GetVariety()
    //{


    //    comdtobj = new Commodity_MP(ComObjWlC);
    //    string var = "Select Commodity_Id,Commodity_Name,Variety from dbo.tbl_MetaData_Variety where Commodity_Name='" + ddlcomdty.SelectedItem.Text + "'";
    //    DataSet ds = comdtobj.selectAny(var);
    //    ddlVariety.DataSource = ds.Tables[0];
    //    ddlVariety.DataTextField = "Variety";
    //    ddlVariety.DataValueField = "Commodity_Id";
    //    ddlVariety.DataBind();


    //    // ddDistId.Items.Insert(0, "--चुनिये--");
    //}
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObjWlC);
        string  qrySelect = "SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_ID";
        DataSet ds = comdtobj.selectAny(qrySelect);
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }
    void GetDepositorType()

    {
        comdtobj = new Commodity_MP(ComObjWlC);
        string qrey = "select * from dbo.tbl_MetaData_Depositor_Type order by Depositor_Type_Id";
        DataSet ds = comdtobj.selectAny(qrey);
        ddldepositortype.DataSource = ds.Tables[0];
        ddldepositortype.DataTextField = "Depositor_Type";
        ddldepositortype.DataValueField = "Depositor_Type_Id";
        ddldepositortype.DataBind();
        ddldepositortype.Items.Insert(0, "--Select--");


    }
    void GetVehicleType()

    {
        comdtobj = new Commodity_MP(ComObjWlC);
        string qreyvt = "select * from dbo.tbl_MetaData_Vehicle_Type order by Vehicle_Type_Id";
        DataSet ds = comdtobj.selectAny(qreyvt);

        ddlvrhicletype.DataSource = ds.Tables[0];
        ddlvrhicletype.DataTextField = "Vehicle_Type";
        ddlvrhicletype.DataValueField = "Vehicle_Type_ID";
        ddlvrhicletype.DataBind();
        ddlvrhicletype.Items.Insert(0, "--Select--");


    }
    void GetDepositor()
    {
        comdtobj = new Commodity_MP(ComObjWlC);
        string qrey = "select * from dbo.tbl_MetaData_DEPOSITOR where Depositor_Type='" + ddldepositortype.SelectedItem.Text + "'";
        DataSet ds = comdtobj.selectAny(qrey);

        ddldepositor.DataSource = ds.Tables[0];
        ddldepositor.DataTextField = "Depositor_Name";
        ddldepositor.DataBind();
        ddldepositor.Items.Insert(0, "--Select--");

    }

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        string qrysch = "Select * from dbo.tbl_MetaData_SCHEME order by Scheme_Id ";
        DataSet ds = schobj.selectAny(qrysch);
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

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
        string qry = "SELECT * FROM MPSCSC.dbo.Source_Arrival_Type order by Source_ID";
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
    void GetRecdCategory()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
        DataSet ds = mobj.selectAny(qry);

        ddlacptcat.DataSource = ds.Tables[0];
        ddlacptcat.DataTextField = "Category_Name";
        ddlacptcat.DataValueField = "Category_Id";
        ddlacptcat.DataBind();


    }
    void Transport()
    {

        tobj = new Transporter(ComObj);
        string qry = "SELECT Transporter_Name,Transporter_ID FROM dbo.Transporter_Table where Distt_ID='" + did + "'";
        DataSet ds = tobj.selectAny(qry);
        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
        ddltransport.Items.Insert(0, "--Select--");

    }
    //void GetFCIdist()
    //{
    //    tobj = new Transporter(ComObj);
    //    string qry = "select districtsmp.district_name as dist_name,DepoCode.district_code as dist_code from DepoCode left join pds.districtsmp   on upper(DepoCode.district)=upper( districtsmp.district_name) group by districtsmp.district_name, DepoCode.district_code";
    //    DataSet ds = tobj.selectAny(qry);

    //    ddlfcidist.DataSource = ds.Tables[0];
    //    ddlfcidist.DataTextField = "dist_name";
    //    ddlfcidist.DataValueField = "dist_code";
    //    ddlfcidist.DataBind();
    //    ddlfcidist.Items.Insert(0, "--Select--");

    //}
    //void GetFCIdepot()
    //{
    //    //string dtype = ddldepottype.SelectedItem.ToString();
    //    string dcode = ddlfcidist.SelectedValue;
    //    tobj = new Transporter(ComObj);
    //    string qry = "select distinct(DepoName) as depo_name  ,DepoCode as depo_code,type from DepoCode where district_code='" + dcode + "'";//and type='" + dtype + "'";
    //    DataSet ds = tobj.selectAny(qry);

    //    ddlfcidepo.DataSource = ds.Tables[0];
    //    ddlfcidepo.DataTextField = "depo_name";
    //    ddlfcidepo.DataValueField = "depo_code";
    //    ddlfcidepo.DataBind();
    //    ddlfcidepo.Items.Insert(0, "--Select--");

    //}
    void GetChallan()
    {
        mobj = new MoveChallan(ComObj);

        //string qry = "SELECT * FROM dbo.Lift_A_RO where Send_District='"+did +"'and Issue_center='"+ sid +"'";
        DataSet ds = mobj.selectAny(qryforchallan);
         if (ds.Tables[0].Rows.Count==0)
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

        }
    }
    void GetData()
    {
        mobj = new MoveChallan(ComObj);
        if (ddlsarrival.SelectedItem.Text == "Other Depot")
        {          
            qryforgetdata = "SELECT SCSC_Truck_challan.* ,Transporter_Table.Transporter_Name as Transporter_Name   FROM dbo.SCSC_Truck_challan left join dbo.Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + ddlchallan.SelectedValue + "'";

        }
        else if (ddlsarrival.SelectedItem.Text == "From FCI")
        {
            qryforgetdata = "SELECT Lift_A_RO.*,DepoCode.DepoName as DepoName,DepoCode.District as FCIDname,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name as Scheme_Name ,Transporter_Table.Transporter_Name as Transporter_Name  FROM dbo.Lift_A_RO left join dbo.tbl_MetaData_STORAGE_COMMODITY on Lift_A_RO.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on Lift_A_RO.Scheme=tbl_MetaData_SCHEME .Scheme_Id left join dbo.Transporter_Table on Lift_A_RO.Transporter=Transporter_Table.Transporter_ID left join DepoCode on Lift_A_RO.FCIdepo=DepoCode.DepoCode   where Send_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";


        }
        DataSet ds = mobj.selectAny(qryforgetdata);
         if (ds.Tables[0].Rows.Count==0)
        {
            if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
            {
                Session["st"] = "F";
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lbldist.Visible = true;
                ddldistrict.Visible = true;

                lbldepo.Visible = true;
                ddlissue.Visible = true;
                txtqty.ReadOnly = false;
               

            }
            else if (ddlsarrival.SelectedItem.ToString() == "From FCI")
            {
                Session["st"] = "F";
                lblchallandt.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lbldist.Visible = false;
                ddldistrict.Visible = false;
                //lblfcdist.Visible = true;
                //ddlfcidist.Visible = true;
                lbldepo.Visible = false;
                ddlissue.Visible = false;

                lbldist.Visible = false;
                ddldistrict.Visible = false;

                lbldepo.Visible = false;
                ddlissue.Visible = false;
                //lblbalanceqty.Visible = false;
                //txtbalqty.Visible = false;

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
                ddlscheme.SelectedItem.Text = "--Select--";
                ddlcomdty.SelectedItem.Text = "--Select--";
                ddlcropyear.SelectedItem.Text = "--Select--";

                ddltransport.Enabled = true;
                ddlcropyear.Enabled = true;
                ddlcategory.Enabled = true;
                ddlgtype.Enabled = true;
                ddlcomdty.Enabled = true;

                txtqty.Text = "";
                txtvehleno.Text = "";
                txtnobags.Text = "";
                txtchallan.BackColor = System.Drawing.Color.Snow;
                txtchallan.Focus();
            }
            else if (ddlsarrival.SelectedItem.ToString() == "CMR")
            {
                Session["st"] = "TT";
                lblchallandt.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lbldist.Visible = false;
                ddldistrict.Visible = false;
                lbldepo.Visible = false;
                ddlissue.Visible = false;

                lbldist.Visible = false;
                ddldistrict.Visible = false;

                lbldepo.Visible = false;
                ddlissue.Visible = false;
               
            }
            else if (ddlsarrival.SelectedItem.ToString() == "Levy Rice")
            {
                Session["st"] = "TT";
                lblchallandt.Visible = false;
                txtchallandt.Visible = false;
                lblchallanno.Visible = true;
                txtchallan.Visible = true;
                lblchallandate.Visible = true;
                DaintyDate3.Visible = true;
                lbldist.Visible = false;
                ddldistrict.Visible = false;
                //lblfcdist.Visible = false;
                //ddlfcidist.Visible = false;
                lbldepo.Visible = false;
                ddlissue.Visible = false;

                lbldist.Visible = false;
                ddldistrict.Visible = false;

                lbldepo.Visible = false;
                ddlissue.Visible = false;
                //lblbalanceqty.Visible = false;
                //txtbalqty.Visible = false;
               
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
                lblchallandt.Visible = true;
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


                ddlcropyear.SelectedItem.Text = "Not Indicated";

                ddlcropyear.BackColor = System.Drawing.Color.Wheat;
                ddlcropyear.Enabled = false;

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

                GetBalance();


            }
            else
            {
                Session["st"] = "T";

                lblchallanno.Visible = false;
                txtchallan.Visible = false;
                lblchallandate.Visible = false;
                DaintyDate3.Visible = false;

                DataRow dr = ds.Tables[0].Rows[0];
                lblchallandt.Visible = true;
                txtchallandt.Visible = true;

                fcidist.Text = dr["Dist_Id"].ToString();
            
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


                ddlcropyear.SelectedItem.Text = dr["Crop_year"].ToString();

                ddlcropyear.BackColor = System.Drawing.Color.Wheat;
                ddlcropyear.Enabled = false;

                txtnobags.Text = dr["No_of_Bags"].ToString();
                txtnobags.BackColor = System.Drawing.Color.Wheat;
                txtnobags.ReadOnly = true;
                string comid = dr["Commodity"].ToString();

                lblcomdty.Text = dr["Commodity"].ToString();

                //txttono.Text = dr["TO_Number"].ToString();
                //txtrono.Text = dr["RO_NO"].ToString();
                comdtyid = dr["Commodity"].ToString();
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
                ddltransport.SelectedValue = dr["Transporter"].ToString();
                //ddltransport.SelectedValue  = dr["Transporter"].ToString();
                ddltransport.BackColor = System.Drawing.Color.Wheat;
                ddltransport.Enabled = false;
                txtvehleno.Text = dr["Vehicle_No"].ToString();
                txtvehleno.BackColor = System.Drawing.Color.Wheat;
                txtvehleno.ReadOnly = true;

                ddlgtype.SelectedItem.Text = dr["Gunny_type"].ToString();
                ddlgtype.BackColor = System.Drawing.Color.Wheat;
                ddlgtype.Enabled = false;
             
                GetBalance();


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
   

    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        type = ddlsarrival.SelectedItem.ToString();
       if (ddlsarrival.SelectedItem.ToString() == "From FCI")
        {
            status = "F";
            source_from = ddlsarrival.SelectedItem.ToString();
            lblchallandt.Visible = false;
            txtchallandt.Visible = false;

            qryforchallan = "SELECT * FROM dbo.Lift_A_RO where Send_District='" + did + "'and Issue_center='" + sid + "' and IsRecieved='N'";

            GetChallan();           
           
        }
        else if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
        {
            status = "O";
            source_from = ddlsarrival.SelectedItem.ToString();
           
            lblchallandt.Visible = false;
            txtchallandt.Visible = false;

            qryforchallan = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and IsDeposit='N'";
            GetChallan();
        }
        else if (ddlsarrival.SelectedItem.ToString() == "Levy Rice")
        {
            status = "LR";
            source_from = ddlsarrival.SelectedItem.ToString();
            lblchallandt.Visible = false;
            txtchallandt.Visible = false;

            qryforchallan = "SELECT * FROM dbo.Lift_A_RO where Send_District=''and Issue_center=''";
            GetChallan();
          
        }
        else if (ddlsarrival.SelectedItem.ToString() == "CMR")
        {
            status = "CMR";
            source_from = ddlsarrival.SelectedItem.ToString();

            lblchallandt.Visible = false;
            txtchallandt.Visible = false;

            qryforchallan = "SELECT * FROM dbo.Lift_A_RO where Send_District=''and Issue_center=''";
            GetChallan();
          

        }
        else if (ddlsarrival.SelectedItem.ToString() == "Procurement")
        {
            status = "P";
            source_from = ddlsarrival.SelectedItem.ToString();

            lblchallandt.Visible = false;
            txtchallandt.Visible = false;

            qryforchallan = "SELECT TC_Number as Challan_No FROM dbo.SCSC_Procurement where Distt_ID='" + did + "'and IssueCenter_ID='" + sid + "'";
            GetChallan();


        }
       

    }

    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetGunny();
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mcomid = ddlcomdty.SelectedValue;
        string mcatid = ddlcategory.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;

        mobj1 = new MoveChallan(ComObj);
        string qry = "Select Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'";
        DataSet ds = mobj1.selectAny(qry);

         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            //lblbalanceqty.Visible = false;
            //txtbalqty.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            //txtbalqty.Text = dr["Current_Balance"].ToString();
            //lblbalanceqty.Visible = true;
            //txtbalqty.Visible = true;
            //txtbalqty.BackColor = System.Drawing.Color.Wheat;
            //txtbalqty.ReadOnly = true;
        }
    }
    void GetBalance()
    {
        string mcomid = comdtyid;
        //string mcatid = ddlcategory.SelectedValue;
        string mscheme = schemeid;
        mobj1 = new MoveChallan(ComObj);
        string qry = "Select Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'";
        DataSet ds = mobj1.selectAny(qry);

         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            //lblbalanceqty.Visible = false;
            //txtbalqty.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            //txtbalqty.Text = dr["Current_Balance"].ToString();
            //lblbalanceqty.Visible = true;
            //txtbalqty.Visible = true;
            //txtbalqty.BackColor = System.Drawing.Color.Wheat;
            //txtbalqty.ReadOnly = true;
        }
    }
    protected void ddlgtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Source Of Arrival...'); </script> ");
        }
        else
        {

            float cqty = CheckNull(txtqty.Text);
            //float rqty = CheckNull(txtrqty.Text);
            //float vqty = (cqty - rqty);

            if (ddlsarrival.SelectedItem.Text == "From FCI")
            {
                status = "F";
                source_from = ddlsarrival.SelectedValue;
                //source_name = lblfdepo.Text;
                //district = lblfdist.Text;
                //depoto = lblfdepo.Text;
                //RO_NO = txtrono.Text;
                //Dispatch_Date = "";
                //tono = txttono.Text;
            }
            else if (ddlsarrival.SelectedItem.Text == "Other Depot")
            {
                status = "O";
                source_from = ddlsarrival.SelectedValue;
                source_name = "";
                district = distt;
                depoto = depot;
                Dispatch_Date = "";
                tono = "";
                RO_NO = "";

            }
            else if (ddlsarrival.SelectedItem.ToString() == "CMR")
            {
                status = "C";
                source_from = ddlsarrival.SelectedValue;
                source_name = "CMR";
                district = "";
                depoto = "";
                Dispatch_Date = "";
                tono = "";
                RO_NO = "";

            }
            else if (ddlsarrival.SelectedItem.ToString() == "Levy Rice")
            {
                status = "L";
                source_from = ddlsarrival.SelectedValue;
                source_name = "Levy Rice";
                district = "";
                depoto = "";
                Dispatch_Date = "";
                tono = "";
                RO_NO = "";

            }
            mobj1 = new MoveChallan(ComObj);
            string qrey = "select max(GatePass_id) as GatePass_id from dbo.tbl_Receipt_Details where Depot_id='" + sid + "' and Dist_Id='" + did + "'";
            DataSet ds = mobj1.selectAny(qrey);
            DataRow dr = ds.Tables[0].Rows[0];
            gatepass = dr["GatePass_id"].ToString();
            if (gatepass == "")
            {
                gatepass = sid + "1";

            }
            else
            {
                getnum = Convert.ToInt32(gatepass);
                getnum = getnum + 1;
                gatepass = getnum.ToString();


            }
            try
            {

                if (Session["st"].ToString() == "T")
                {
                    Challan_no = ddlchallan.SelectedItem.ToString();
                    string chdt = getmmddyy(txtchallandt.Text);
                    Challan_Date = chdt;
                    comdtyid = lblcomdty.Text;
                    schemeid = lblsch.Text;
                    Dispatch_Date = chdt;

                }
                else
                {
                    Challan_no = txtchallan.Text;
                    string chdt1 = getDate_MDY(DaintyDate3.Text);
                    Challan_Date = chdt1;
                    comdtyid = ddlcomdty.SelectedValue;
                    schemeid = ddlscheme.SelectedValue;
                    Dispatch_Date = getDate_MDY(DaintyDate3.Text);
                }

              
                string chadate = getDate_MDY(DaintyDate3.Text);

                time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());


                string mchallan = txtchallan.Text;
                string chdate = getDate_MDY(DaintyDate3.Text);
                //string ardate = getDate_MDY(DaintyDate1.Text);
                string mcomdty = ddlcomdty.SelectedValue;
                string mscheme = ddlscheme.SelectedValue;
                string mcropy = ddlcropyear.SelectedItem.ToString();
                //int mrecbags = CheckNullInt(txtrecbags.Text);
                string mcategry = ddlcategory.SelectedValue;
                string mtransporter = ddltransport.SelectedValue;
                string mvehicleno = txtvehleno.Text;
                string mtime = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
                int mbagno = CheckNullInt(txtnobags.Text);
                //float mrqty = CheckNull(txtrqty.Text);
                float mmoisture = CheckNull(txtmoisture.Text);
                string mwcm = txtwcmno.Text;

                float mqty = CheckNull(txtqty.Text);
                //string msarival = ddlsarrival.SelectedValue;
                string Created_date = getDate_MDY(DateTime.Today.Date.ToString());//.ToString();
                string updated_date = "";
                string deleted_date = "";
                int month = int.Parse(DateTime.Today.Date.Month.ToString());
                int year = int.Parse(DateTime.Today.Date.Year.ToString());


                //string qryInsert = "insert into dbo.tbl_Receipt_Details(Dist_ID,Depot_ID,GatePass_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,Created_date,updated_date,deleted_date)values('" + did + "','" + sid + "','" + gatepass + "','" + source_from + "','" + source_name + "','" + district + "','" + depoto + "','" + RO_NO + "','" + tono + "','" + Dispatch_Date + "','" +  + "','" + Challan_no + "','" + Challan_Date + "'," + mqty + ",'" + comdtyid + "','" + schemeid + "','" + mcropy + "','" + mcategry + "','" + mtransporter + "','" + mvehicleno + "','" + mtime + "','" + ddlgtype.SelectedItem.ToString() + "'," + mbagno + "," + 0 + "," + 0 + "," + mmoisture + ",'" + mwcm + "'," + 0 + "," + month + "," + year + ",'" + Created_date + "','" + updated_date + "','" + deleted_date + "')";

                //cmd.CommandText = qryInsert;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    count = cmd.ExecuteNonQuery();
                    con.Close();
                    if (ddlsarrival.SelectedItem.Text == "From FCI")
                    {
                        string qrylaro = "Update dbo.Lift_A_RO set IsRecieved='Y' Where Send_District='" + did + "'and Issue_center='" + sid + "' and Challan_No='" + Challan_no + "' and Dist_Id= '" + fcidist.Text + "'";
                        cmd.CommandText = qrylaro;
                        con.Open();
                        int countm = cmd.ExecuteNonQuery();
                        con.Close();

                        if (countm >= 1)
                        {

                            string qrystock = "select Sum(Recd_Qty) as Qty from dbo.tbl_Receipt_Details where Commodity='" + comdtyid + "'and Dist_Id='" + did + "'and Depot_ID='" + sid + "' and S_of_arrival='" + ddlsarrival.SelectedValue + "' and Month=" + month + "and Year=" + year;
                            mobj = new MoveChallan(ComObj);
                            DataSet dspro = mobj.selectAny(qrystock);
                           if (dspro.Tables[0].Rows.Count==0)
                            {

                            }
                            else
                            {
                                DataRow drop = dspro.Tables[0].Rows[0];
                                float mobal = 0;
                                float mrp = 0;
                                float mrod = 0;
                                float msod = 0;
                                float msdelo = 0;
                                float mrfci = CheckNull(drop["Qty"].ToString());
                                string mremark = "";

                                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comdtyid + "'and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                mobj = new MoveChallan(ComObj);
                                DataSet dsopen = mobj.selectAny(qryinsopen);
                                 if (dsopen.Tables[0].Rows.Count==0)
                                {
                                    string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Sale_Do,Sale_otherg,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + comdtyid + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + msdelo + "," + msod + "," + month + "," + year + ",'" + mremark + "')";
                                    cmd.CommandText = qryins;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                }
                                else
                                {
                                    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_FCI=" + mrfci + " where Commodity_Id ='" + comdtyid + "'and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                    cmd.CommandText = qryinsU;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }


                            }

                        }




                    }
                    else
                    {
                        string qrylaro = "Update dbo.SCSC_Truck_challan set IsDeposit='Y' Where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + Challan_no + "'";
                        cmd.CommandText = qrylaro;
                        con.Open();
                        int countmm = cmd.ExecuteNonQuery();
                        con.Close();

                        if (countmm >= 1)
                        {
                            string qrystock = "select Sum(Recd_Qty) as Qty from dbo.tbl_Receipt_Details where Commodity='" + comdtyid + "'and Dist_Id='" + did + "'and Depot_ID='" + sid + "'and S_of_arrival='" + ddlsarrival.SelectedValue + "'";
                            mobj = new MoveChallan(ComObj);
                            DataSet dspro = mobj.selectAny(qrystock);
                           if (dspro.Tables[0].Rows.Count==0)
                            {

                            }
                            else
                            {
                                DataRow drop = dspro.Tables[0].Rows[0];
                                float mobal = 0;
                                float mrp = 0;
                                float mrod = CheckNull(drop["Qty"].ToString());
                                float msod = 0;
                                float msdelo = 0;
                                float mrfci = 0;
                                string mremark = "";
                                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comdtyid + "'and DistrictId ='" + did + "'and DepotID='" + sid + "'";
                                mobj = new MoveChallan(ComObj);
                                DataSet dsopen = mobj.selectAny(qryinsopen);
                                 if (dsopen.Tables[0].Rows.Count==0)
                                {
                                    string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Sale_Do,Sale_otherg,Remarks) Values('" + did + "','" + sid + "','" + comdtyid + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + msdelo + "," + msod + ",'" + mremark + "')";
                                    cmd.CommandText = qryins;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                }
                                else
                                {
                                    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=" + mrod + " where Commodity_Id ='" + comdtyid + "'and DistrictId='" + did + "'and DepotID='" + sid + "'";
                                    cmd.CommandText = qryinsU;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }


                            }

                        }
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
                    ComObj.CloseConnection();
                }


                if (count == 1)
                {

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully...'); </script> ");
                    btnsubmit.Enabled = false;
                    //float ruqty = CheckNull(txtrqty.Text);
                    //float buqty = CheckNull(txtbalqty.Text);
                    //float uuqty = (ruqty + buqty);
                    string mcomid = ddlcomdty.SelectedValue;
                    string mcatid = ddlcategory.SelectedValue;


                    string query = "Update dbo.issue_opening_balance set Current_Balance='" + 0 + "' where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Scheme_Id='" + schemeid + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
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
                        ComObj.CloseConnection();
                    }

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Valid Information....'); </script> ");

                }


            }
            catch (SqlException ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");


            }




        }


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
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
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
    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
        {
            qryforgetdata = "SELECT * FROM dbo.SCSC_Truck_challan where Sendto_District='" + did + "'and Sendto_IC='" + sid + "' and Challan_No='" + ddlchallan.SelectedValue + "'";

        }
        else if (ddlsarrival.SelectedItem.ToString() == "From FCI")
        {
            qryforgetdata = "SELECT * FROM dbo.Lift_A_RO where Send_District='" + did + "'and Challan_No='" + ddlchallan.SelectedValue + "'";

        }
        else if (ddlsarrival.SelectedItem.ToString() == "Procurement")
        {
            qryforgetdata = "SELECT * FROM dbo.SCSC_Procurement where Distt_ID='" + did + "'and TC_Number='" + ddlchallan.SelectedValue + "'and IssueCenter_ID='"+sid +"'";

        }

        GetData();
    }
    protected void ddlfcidepo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblfdepo.Text = ddlfcidepo.SelectedValue;
    }
    protected void btnclose_Click1(object sender, EventArgs e)
    {

        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void ddlfcidist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetFCIdepot();
        //lblfdist.Text = ddlfcidist.SelectedValue;

    }
    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositor();
    }
    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlvrhicletype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

    }
}
