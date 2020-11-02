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
using System.Globalization;

public partial class IssueCenter_IssueAC2015 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());
    //public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    //public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    SqlCommand cmd = new SqlCommand();
    SqlCommand cmdP = new SqlCommand();
    chksql chk = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Transporter tobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    DistributionCenters distobj = null;
    Districts DObj = null;
    string distid = "";
    string gatepass = "";
    string sid = "23";
    DateTime dt1;
    DateTime dt2;
    public string CSMS_Comid;
    int getnum;
    string issueid = "";
    string version = "";
    string Accpt_NO = "";

    decimal total1 = 0;
    decimal qty = 0;

    int jutnew = 0;

    int ppbag = 0;

    int juteold = 0;

    public static string GridRecTotal;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            issueid = Session["issue_id"].ToString();
            version = Session["hindi"].ToString();

            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

            txtaccptno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtaccptno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtaccptno.Attributes.Add("onchange", "return chksqltxt(this)");
            hyprlnkprint.Attributes.Add("onclick", "window.open('Print_AcceptanceNo2015.aspx',null,'left=100, top=10, height=500, width= 750, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

           
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(DaintyDate3.Text);
            ctrllist.Add(txtaccptno.Text);

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

            if (Page.IsPostBack == false)
            {
                // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्वीकृति पत्रक बन जाने के बाद डाटा निरस्त नहीं किया जायेगा ,चेक लिस्ट से डाटा जांच कर ही स्वीकृति पत्रक बनाये '); </script> ");

                DaintyDate3.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                txtAccDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                //Getbranch();

                gettype();
                GetDepot();

                Session["issubmited"] = "No";   // for stop repeat of Acceptance Note.

                if (version == "H")
                {
                    lblDateOfDeposit.Text = Resources.LocalizedText.lblDateOfDeposit;
                    lblissueacno.Text = Resources.LocalizedText.lblissueacno;
                    lblpcname.Text = Resources.LocalizedText.lblpcname;
                    btnsubmit.Text = Resources.LocalizedText.btnsubmit;
                    lblacno.Text = Resources.LocalizedText.lblacno;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    btnviewdetails.Text = Resources.LocalizedText.btnviewdetails;
                    lbltypacc.Text = Resources.LocalizedText.lbltypacc;
                    lblsesn.Text = Resources.LocalizedText.lblsesn;
                    lblDateOfDepositP.Text = Resources.LocalizedText.lblDateOfDepositP;
                    lblpcnameP.Text = Resources.LocalizedText.lblpcnameP;
                    Label2.Text = Resources.LocalizedText.Label2;
                }


               // ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);

            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    void getcsms_Commdty()
    {
        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }


        mobj = new MoveChallan(ComObj);
        string qry = " SELECT Commodity_Id FROM Procurement_COMMODITY WHERE Proc_Commodity_Id='" + ddlmarksesn.SelectedValue.ToString() + "'";


        DataSet ds = mobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];
        CSMS_Comid = dr["Commodity_Id"].ToString();

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue.ToString() == "01")
        {
            DaintyDate3P.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            pnlaccother.Visible = false;
            pnlaccprocment.Visible = true;
            //string distid = Session["dist_id"].ToString();
            //getsociety(distid);
            lblsesn.Visible = true;
            ddlmarksesn.Visible = true;
            getsesson();
        }
        else
        {
            pnlaccprocment.Visible = false;
            pnlaccother.Visible = true;
            lblsesn.Visible = false;
            ddlmarksesn.Visible = false;
        }

    }

    protected void ddlpurchcenterP_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

        errorline.Text = "Date AC";

    }

    void GetDepot()
    {//string distcode = ddldistrict.SelectedValue;

        distobj = new DistributionCenters(ComObj);
        string ord = "DistrictId='23" + distid + "' order by PurchaseCenterName";
        DataSet ds = distobj.selectPC(ord);
        ddlpurchcenter.DataSource = ds.Tables[0];
        ddlpurchcenter.DataTextField = "PurchaseCenterName";
        ddlpurchcenter.DataValueField = "PcId";
        ddlpurchcenter.DataBind();
        ddlpurchcenter.Items.Insert(0, "--Select--");
    }

    void GetGodown()
    {
        string pdate = getDate_MDY(DaintyDate3P.Text);

        //string ord = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlbranch.SelectedValue.ToString() + "'";

        if (ddlmarksesn.SelectedValue.ToString() == "1")
        {
            string ord = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Purchase_Center = '" + ddlpurchcenterP.SelectedValue.ToString() + "' and Recd_Date = '" + pdate + "' and AN_Status = 'N'";

            SqlCommand cmdgdn = new SqlCommand(ord, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter dagdn = new SqlDataAdapter(cmdgdn);

            DataSet dsgdn = new DataSet();

            dagdn.Fill(dsgdn);

            if (dsgdn.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = dsgdn.Tables[0];
                ddlgodown.DataTextField = "GodownName";
                ddlgodown.DataValueField = "Recd_Godown";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--Select--");
            }

            else
            {
                ddlgodown.DataSource = "";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--Select--");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        else
        {
            string ord = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Purchase_Center = '" + ddlpurchcenterP.SelectedValue.ToString() + "' and Recd_Date = '" + pdate + "' and AN_Status = 'N'";

            SqlCommand cmdgdn = new SqlCommand(ord, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter dagdn = new SqlDataAdapter(cmdgdn);

            DataSet dsgdn = new DataSet();

            dagdn.Fill(dsgdn);

            if (dsgdn.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = dsgdn.Tables[0];
                ddlgodown.DataTextField = "GodownName";
                ddlgodown.DataValueField = "Recd_Godown";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--Select--");
            }

            else
            {
                ddlgodown.DataSource = "";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--Select--");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


    }

    private void Getbranch()
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }


                string qrysel = "select DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='23" + distid + "'";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //ddlbranch.DataSource = ds.Tables[0];
                        //ddlbranch.DataTextField = "DepotName";
                        //ddlbranch.DataValueField = "DepotID";
                        //ddlbranch.DataBind();
                        //ddlbranch.Items.Insert(0, "--select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            if (cons.State == ConnectionState.Open)
            {
                cons.Close();
            }
        }
        finally
        {
            if (cons.State == ConnectionState.Open)
            {
                cons.Close();
            }
        }

    }

    void gettype()
    {
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string qrysel = "select Source_Name,Source_ID from Source_Arrival_Type where Source_ID in ('01','06') order by Source_ID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddltype.DataSource = ds.Tables[0];
                        ddltype.DataTextField = "Source_Name";
                        ddltype.DataValueField = "Source_ID";
                        ddltype.DataBind();
                        ddltype.Items.Insert(0, "--चुनें--");

                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
    }

    void getsociety(string distid)
    {
        string pdate = getDate_MDY(DaintyDate3P.Text);
        if (ddlmarksesn.SelectedValue.ToString() == "1")
        {
            try
            {
                if (con_WPMS != null)
                {
                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }


                    // string qrysel = "select (Society.Society_Name+','+Society.SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + distid + "' and IsWheat='Y' order by Society_Name";
                    string qrysel = "select ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID)as varchar(50)) + ')') as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.Sending_District='" + ddldistproment.SelectedValue.ToString() + "' and ic.IssueCenter_ID='" + issueid + "' and ic.Recd_Date='" + pdate + "' and ic.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and ic.AN_Status='N'  group by ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID ";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlpurchcenterP.DataSource = ds.Tables[0];
                            ddlpurchcenterP.DataTextField = "Society_Name";
                            ddlpurchcenterP.DataValueField = "Society_Id";
                            ddlpurchcenterP.DataBind();
                            ddlpurchcenterP.Items.Insert(0, "--select--");

                        }
                    }

                }
                else
                {
                }
            }

            catch (Exception)
            {
                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }
            }
            finally
            {
                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

            }
        }
        else if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
        {
            try
            {
                if (con_paddy != null)
                {
                    con_paddy.Open();

                    //string qrysel = "select (Society.Society_Name+','+Society.SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + distid + "' and Status='Y' order by Society_Name";
                    //  string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + distid + "' order by SocietyID";

                    string qrysel = "select ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID)as varchar(50)) + ')') as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.Sending_District='" + ddldistproment.SelectedValue.ToString() + "' and ic.IssueCenter_ID='" + issueid + "' and ic.Recd_Date='" + pdate + "' and ic.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and ic.AN_Status='N'  group by ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID ";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlpurchcenterP.DataSource = ds.Tables[0];
                            ddlpurchcenterP.DataTextField = "Society_Name";
                            ddlpurchcenterP.DataValueField = "Society_Id";
                            ddlpurchcenterP.DataBind();
                            ddlpurchcenterP.Items.Insert(0, "--select--");

                        }
                    }

                }
                else
                {
                }
            }

            catch (Exception)
            {

                con_paddy.Close();
            }
            finally
            {
                con_paddy.Close();
            }
        }
        else if (ddlmarksesn.SelectedValue.ToString() == "4" || ddlmarksesn.SelectedValue.ToString() == "5" || ddlmarksesn.SelectedValue.ToString() == "6" || ddlmarksesn.SelectedValue.ToString() == "7" || ddlmarksesn.SelectedValue.ToString() == "8")
        {
            try
            {
                if (con_Maze != null)
                {
                    con_Maze.Open();
                    string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + distid + "' order by SocietyID";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlpurchcenterP.DataSource = ds.Tables[0];
                            ddlpurchcenterP.DataTextField = "Society_Name";
                            ddlpurchcenterP.DataValueField = "Society_Id";
                            ddlpurchcenterP.DataBind();
                            ddlpurchcenterP.Items.Insert(0, "--select--");

                        }
                    }

                }
                else
                {
                }
            }

            catch (Exception)
            {

                con_Maze.Close();
            }
            finally
            {
                con_Maze.Close();

            }

        }
    }

    void getDistprocment()
    {
        if (ddlmarksesn.SelectedValue.ToString() == "1")
        {
            try
            {
                if (con_WPMS != null)
                {

                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }

                    string qrysel = "select District_Name,District_Code from Districts order by District_Name";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddldistproment.DataSource = ds.Tables[0];
                            ddldistproment.DataTextField = "District_Name";
                            ddldistproment.DataValueField = "District_Code";
                            ddldistproment.DataBind();
                            ddldistproment.Items.Insert(0, "--select--");

                        }
                    }

                }
                else
                {

                }
            }

            catch (Exception)
            {

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }
            }
            finally
            {
                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

            }
        }
        else if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
        {
            try
            {
                if (con_paddy != null)
                {
                    con_paddy.Open();
                    string qrysel = "select District_Name,District_Code from Districts  order by District_Name";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddldistproment.DataSource = ds.Tables[0];
                            ddldistproment.DataTextField = "District_Name";
                            ddldistproment.DataValueField = "District_Code";
                            ddldistproment.DataBind();
                            ddldistproment.Items.Insert(0, "--select--");

                        }
                    }

                }
                else
                {
                }
            }

            catch (Exception)
            {

                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }
            }
            finally
            {
                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }
            }
        }
        else if (ddlmarksesn.SelectedValue.ToString() == "4" || ddlmarksesn.SelectedValue.ToString() == "5" || ddlmarksesn.SelectedValue.ToString() == "6" || ddlmarksesn.SelectedValue.ToString() == "7" || ddlmarksesn.SelectedValue.ToString() == "8")
        {
            try
            {
                if (con_Maze != null)
                {
                    con_Maze.Open();
                    string qrysel = "select District_Name,District_Code from Districts  order by District_Name";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddldistproment.DataSource = ds.Tables[0];
                            ddldistproment.DataTextField = "District_Name";
                            ddldistproment.DataValueField = "District_Code";
                            ddldistproment.DataBind();
                            ddldistproment.Items.Insert(0, "--select--");

                        }
                    }

                }
                else
                {
                }
            }

            catch (Exception)
            {

                con_Maze.Close();
            }
            finally
            {
                con_Maze.Close();

            }

        }
    }

    void getsesson()
    {
        try
        {
            if (con_WPMS != null)
            {
                con_WPMS.Open();

                if (Session["Markfed"].ToString() == "Y")
                {
                    string qrysel = "select crop,crpcode from Crop_Master where crpcode in ('2','3')";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlmarksesn.DataSource = ds.Tables[0];
                            ddlmarksesn.DataTextField = "crop";
                            ddlmarksesn.DataValueField = "crpcode";
                            ddlmarksesn.DataBind();
                            ddlmarksesn.Items.Insert(0, "--Select--");

                        }
                    }
                }

                else
                {
                    string qrysel = "select crop,crpcode from Crop_Master where crpcode in ('1','2','3','4','5','6','7')";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlmarksesn.DataSource = ds.Tables[0];
                            ddlmarksesn.DataTextField = "crop";
                            ddlmarksesn.DataValueField = "crpcode";
                            ddlmarksesn.DataBind();
                            ddlmarksesn.Items.Insert(0, "--Select--");

                        }
                    }
                }



                // string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";


            }
            else
            {
            }
        }

        catch (Exception)
        {

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }
        }
        finally
        {
            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }
        }


    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }

    protected void ddlmarksesn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmarksesn.SelectedIndex == 0)
        {
            ddlpurchcenterP.Items.Clear();
            ddlpurchcenterP.DataSource = null;
            ddlpurchcenterP.DataBind();
            ddldistproment.Items.Clear();
            ddldistproment.DataSource = null;
            ddldistproment.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        else
        {
            ddlpurchcenterP.Items.Clear();
            ddlpurchcenterP.DataSource = null;
            ddlpurchcenterP.DataBind();
            ddldistproment.Items.Clear();
            ddldistproment.DataSource = null;
            ddldistproment.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            getDistprocment();
            getcsms_Commdty();
        }

    }

    protected void ddldistproment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistproment.SelectedIndex == 0)
        {
            ddlpurchcenterP.Items.Clear();
            ddlpurchcenterP.DataSource = null;
            ddlpurchcenterP.DataBind();
        }
        else
        {
            ddlpurchcenterP.Items.Clear();
            ddlpurchcenterP.DataSource = null;
            ddlpurchcenterP.DataBind();
            string distid = ddldistproment.SelectedValue.ToString();
            getsociety(distid);

        }
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcsms_Commdty();

        bindgrid();
        txtAccDate.Text = DaintyDate3P.Text;
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        errorline.Text = "";

        if (Session["issubmited"].ToString() == "Yes")
        {

        }

        else
        {
            //string test = getDate_MDY(txtAccDate.Text.Trim());

            //DateTime Acccdate11 = Convert.ToDateTime(test.ToString());


            //  DateTime Acccdate = Convert.ToDateTime(DateTime.ParseExact(txtAccDate.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

            //  DateTime dispdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate3.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

            //    //string today_date = System.DateTime.Now.ToString("MM/dd/yyyy");

            //    //dt1 = Convert.ToDateTime(today_date);


            //DateTime date1 = new DateTime(dt1.Year, dt1.Month, dt1.Day);
            //DateTime date2 = new DateTime(Acccdate.Year, Acccdate.Month, Acccdate.Day);
            //DateTime date3 = new DateTime(dispdate.Year, dispdate.Month, dispdate.Day);

            //int result1 = DateTime.Compare(date1, date2);

            //int result2 = DateTime.Compare(date3, date2);

            // //string relationship = string.Empty;

            //if (result1 >= 0)
            //{

            //if (txtBstencile.Text == "")
            //{
            //    txtBstencile.Text = "0";
            //}

            //if (txtBStiching.Text == "")
            //{
            //    txtBStiching.Text = "0";
            //}

            //if (txtGstencile.Text == "")
            //{
            //    txtBstencile.Text = "0";
            //}

            //if (txtGStiching.Text == "")
            //{
            //    txtBstencile.Text = "0";
            //}


            try
            {

                SqlCommand cmdacno = new SqlCommand();
                // SqlTransaction tracn;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string AcceptDate = getDate_MDY(txtAccDate.Text);

                // errorline.Text = "Paramet Passing";

                //tracn = con.BeginTransaction();

                //cmdacno.Transaction = tracn;
                int Insyear = int.Parse(DateTime.Today.Year.ToString());

                cmdacno.Parameters.Clear();
                cmdacno.Parameters.AddWithValue("@District_ID", distid);
                cmdacno.Parameters.AddWithValue("@IssueCenter_ID", issueid);
                cmdacno.Parameters.AddWithValue("@Year", Insyear);

                cmdacno.Connection = con;

                //  errorline.Text = "Stored Proc genert Ac";



                //  errorline.Text = "AC Generated";

                # region otherSourse
                if (ddltype.SelectedValue.ToString() == "06")
                {
                    string opid = Session["OperatorId"].ToString();

                    string state = Session["State_Id"].ToString();

                    string cacno = Accpt_NO;
                    //string cacno = txtaccptno.Text;
                    string qrydata = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "'";
                    mobj = new MoveChallan(ComObj);
                    DataSet dschdt = mobj.selectAny(qrydata);
                    if (dschdt == null)
                    {


                    }
                    else
                    {
                        if (dschdt.Tables[0].Rows.Count == 0)
                        {
                            if (ddlpurchcenter.SelectedItem.Text == "--Select--")
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                            }
                            else
                            {
                                string acptno = Accpt_NO;
                                //string acptno = txtaccptno.Text;
                                string mpc = ddlpurchcenter.SelectedValue;
                                string pdatw = getDate_MDY(DaintyDate3.Text);
                                int month = int.Parse(DateTime.Today.Month.ToString());
                                int year = int.Parse(DateTime.Today.Year.ToString());
                                string udate = "";
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                if (GridView2.Rows.Count == 0)
                                {
                                }
                                else
                                {

                                    foreach (GridViewRow gr in GridView2.Rows)
                                    {
                                        CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                        TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");

                                        if (GchkBx.Checked == true)
                                        {
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;
                                            decimal mqty = CheckNull(txtrcq.Text);
                                            //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,CommodityId,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + CSMS_Comid + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,CommodityId)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + CSMS_Comid + "')";


                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                                cmd.CommandText = updt;

                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }

                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                btnsubmit.Enabled = false;
                                                //--
                                                lblacno.Visible = true;
                                                txtaccptno.Visible = true;
                                                txtaccptno.Text = Accpt_NO;
                                                txtaccptno.Enabled = false;
                                                Session["Acceptance_NO"] = Accpt_NO;
                                                Session["Commodity_ID"] = "";
                                                hyprlnkprint.Visible = true;
                                            }
                                            catch (Exception ex)
                                            {
                                                Label3.Visible = true;
                                                Label3.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                con.Close();
                                            }



                                        }
                                        //--
                                        else
                                        {
                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbox...'); </script> ");
                                        }
                                    }
                                }

                            }


                        }

                        else
                        {
                            string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenter.SelectedValue + "'";
                            mobj = new MoveChallan(ComObj);
                            DataSet dspacno = mobj.selectAny(qryacno);

                            if (dspacno.Tables[0].Rows.Count == 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                            }
                            else
                            {
                                if (ddlpurchcenter.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                }
                                else
                                {
                                    string acptno = Accpt_NO;
                                    //string acptno = txtaccptno.Text;
                                    string mpc = ddlpurchcenter.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {

                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            if (GchkBx.Checked == true)
                                            {
                                                string challan = gr.Cells[1].Text;
                                                string truckno = gr.Cells[2].Text;
                                                decimal mqty = CheckNull(txtrcq.Text);
                                                string inst = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,CommodityId)values('" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + CSMS_Comid + "')";
                                                try
                                                {
                                                    cmd.Connection = con;
                                                    cmd.CommandText = inst;
                                                    if (con.State == ConnectionState.Closed)
                                                    {
                                                        con.Open();
                                                    }
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();

                                                    string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                                    cmd.CommandText = updt;
                                                    if (con.State == ConnectionState.Closed)
                                                    {
                                                        con.Open();
                                                    }
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();
                                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                    btnsubmit.Enabled = false;
                                                    //--
                                                    lblacno.Visible = true;
                                                    txtaccptno.Visible = true;
                                                    txtaccptno.Text = Accpt_NO;
                                                    txtaccptno.Enabled = false;
                                                    Session["Acceptance_NO"] = Accpt_NO;
                                                    Session["Commodity_ID"] = "";
                                                    hyprlnkprint.Visible = true;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Label3.Visible = true;
                                                    Label3.Text = ex.Message;
                                                }
                                                finally
                                                {
                                                    con.Close();
                                                }



                                            }
                                            //--
                                            else
                                            {
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                            }
                                        }
                                    }

                                }


                            }
                        }
                    }
                }
                # endregion
                // 310 
                else if (ddltype.SelectedValue.ToString() == "01")
                {
                    # region wheat_submit
                    if (ddlmarksesn.SelectedValue.ToString() == "1")
                    {

                        cmdacno.CommandType = CommandType.StoredProcedure;
                        cmdacno.CommandText = "prc_getMaxAcceptanceNo";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                        string opid = Session["OperatorId"].ToString();

                        string state = Session["State_Id"].ToString();
                        //string cacno = txtaccptno.Text;
                        string cacno = Accpt_NO;
                        string qrydata = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and godown = '" + ddlgodown.SelectedValue + "'";

                        mobj = new MoveChallan(ComObj);
                        DataSet dschdt = mobj.selectAny(qrydata);

                        if (dschdt == null)
                        {


                        }
                        else
                        {
                            if (dschdt.Tables[0].Rows.Count == 0)
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                    return;
                                }
                                else if (ddlgodown.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
                                    return;
                                }

                                else
                                {
                                    string acptno = Accpt_NO;
                                    // string acptno = txtaccptno.Text;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                    if (GridView2.Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {

                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            //CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                            //if (GchkBx.Checked == true)
                                            //{
                                            getcsms_Commdty();
                                            //  txtrcq.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            string CropYear = gr.Cells[9].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);

                                                Int32 fir4 = Convert.ToInt32(firstfour);

                                                string lastfour = (fir4 + 1).ToString();

                                                CropYear = firstfour + "-" + lastfour;

                                            }
                                            else
                                            {

                                            }

                                            // decimal R_Bag = CheckNull(txtrcBags.Text);
                                            // decimal mqty = CheckNull(txtrcq.Text);
                                            // decimal rejcqty = CheckNull(txtrjctqt.Text);

                                            //string rvcqtyFaq = gr.Cells[4].Text;

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");


                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //string rvcqtyurs = gr.Cells[5].Text;

                                            Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");

                                            decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));

                                            //  string rvcqty = gr.Cells[3].Text;

                                            decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;       // total of Faq and Urs is accepted qty (Anurag)

                                            // decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            // decimal Totl = mqty + rejcqty;

                                            //if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");

                                            //}
                                            //else
                                            //{

                                            //string jutenew = gr.Cells[6].Text;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");


                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));

                                            // string recpp = gr.Cells[7].Text;

                                            Label recpp = (Label)gr.FindControl("lblrcpp");

                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));

                                            // string juteold = gr.Cells[8].Text;


                                            Label juteold = (Label)gr.FindControl("lblrcjutold");

                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));

                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;       // total of all type bags is accepted bags (Anurag)


                                            SqlTransaction trns;

                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Detail where Distt_ID='" + distid + "'  and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";

                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                                            SqlDataReader drduplicate;

                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();

                                                return;
                                            }

                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;
                                            con_WPMS.Open();
                                            trns2 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";


                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;

                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();
                                                cmd.ExecuteNonQuery();
                                                // con.Close();


                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "')";

                                                cmdP.Connection = con_WPMS;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

                                                trns.Commit();
                                                trns2.Commit();
                                                con.Close();
                                                con_WPMS.Close();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                btnsubmit.Enabled = false;
                                                //--
                                                lblacno.Visible = true;
                                                txtaccptno.Visible = true;
                                                txtaccptno.Text = Accpt_NO;
                                                txtaccptno.Enabled = false;
                                                Session["Acceptance_NO"] = Accpt_NO;

                                                Session["Godown"] = ddlgodown.SelectedValue;


                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;

                                                Session["issubmited"] = "Yes";

                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                con.Close();
                                                con_WPMS.Close();
                                                Label3.Visible = true;
                                                Label3.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                con.Close();
                                                con_WPMS.Close();
                                            }

                                            // }

                                            // } 12-03-15 Anurag , no need to select , just insert all grid data and generte accep note
                                            //--
                                            //else
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                            //}
                                        }
                                    }

                                }


                            }

                            else
                            {
                                # region unused

                                //string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "' and godown = '"+ddlgodown.SelectedValue+"'";
                                //mobj = new MoveChallan(ComObj);
                                //DataSet dspacno = mobj.selectAny(qryacno);

                                //if (dspacno.Tables[0].Rows.Count == 0)
                                //{
                                //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                                //}
                                //else
                                //{
                                //    if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                //    {
                                //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                //    }
                                //    else
                                //    {

                                //        string acptno = Accpt_NO;
                                //        string mpc = ddlpurchcenterP.SelectedValue;
                                //        string pdatw = getDate_MDY(DaintyDate3P.Text);
                                //        int month = int.Parse(DateTime.Today.Month.ToString());
                                //        int year = int.Parse(DateTime.Today.Year.ToString());
                                //        string udate = "";
                                //        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                //        if (GridView2.Rows.Count == 0)
                                //        {
                                //        }
                                //        else
                                //        {

                                //            foreach (GridViewRow gr in GridView2.Rows)
                                //            {
                                //                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                //                TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                //                TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                //                TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                //                if (GchkBx.Checked == true)
                                //                {
                                //                    getcsms_Commdty();
                                //                    string iss_ID = gr.Cells[1].Text;
                                //                    string challan = gr.Cells[2].Text;
                                //                    string truckno = gr.Cells[3].Text;
                                //                    decimal R_Bag = CheckNull(txtrcBags.Text);
                                //                    decimal mqty = CheckNull(txtrcq.Text);
                                //                    decimal rejcqty = CheckNull(txtrjctqt.Text);
                                //                    string rvcqty = gr.Cells[6].Text;
                                //                    decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                //                    decimal Totl = mqty + rejcqty;
                                //                    if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                //                    {
                                //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                //                        return;
                                //                    }
                                //                    SqlTransaction trns;

                                //                    if (con.State == ConnectionState.Closed)
                                //                    {
                                //                        con.Open();
                                //                    }
                                //                    trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmd.Transaction = trns;
                                //                    SqlTransaction trns2;
                                //                    con_WPMS.Open();
                                //                    trns2 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmdP.Transaction = trns2;
                                //                    //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                    string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "')";

                                //                    try
                                //                    {
                                //                        cmd.Connection = con;
                                //                        cmd.CommandText = inst;

                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();

                                //                        string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Purchase_Center ='" + mpc + "' and Recd_Godown '" + ddlgodown.SelectedValue + "' ";
                                //                        cmd.CommandText = updt;
                                //                        //con.Open();
                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();


                                //                        //procment
                                //                        //string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                        string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "')";


                                //                        cmdP.Connection = con_WPMS;
                                //                        cmdP.CommandText = inst1;
                                //                        cmdP.ExecuteNonQuery();


                                //                        string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and IssueID = '" + iss_ID + "' and IssueCenter_ID ='" + issueid + "' and Recd_Godown = '"+ddlgodown.SelectedValue+"' ";
                                //                        cmdP.CommandText = updt1;
                                //                        cmdP.ExecuteNonQuery();

                                //                        trns.Commit();
                                //                        trns2.Commit();
                                //                        con.Close();
                                //                        con_WPMS.Close();
                                //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                //                        btnsubmit.Enabled = false;
                                //                        //--
                                //                        lblacno.Visible = true;
                                //                        txtaccptno.Visible = true;
                                //                        txtaccptno.Text = Accpt_NO;
                                //                        txtaccptno.Enabled = false;
                                //                        Session["Acceptance_NO"] = Accpt_NO;
                                //                        Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                //                        hyprlnkprint.Visible = true;

                                //                        Session["issubmited"] = "Yes";

                                //                    }
                                //                    catch (Exception ex)
                                //                    {
                                //                        trns.Rollback();
                                //                        trns2.Rollback();
                                //                        con.Close();
                                //                        con_WPMS.Close();
                                //                        Label3.Visible = true;
                                //                        Label3.Text = ex.Message;
                                //                    }
                                //                    finally
                                //                    {
                                //                        con.Close();
                                //                        con_WPMS.Close();
                                //                    }


                                //                }
                                //                //--
                                //                else
                                //                {
                                //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick the Checkbox...'); </script> ");
                                //                }
                                //            }
                                //        }

                                //    }


                                //}

                                # endregion

                            }
                        }
                    }
                    # endregion

                    # region Paddy_submit
                    else if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
                    {

                        cmdacno.CommandType = CommandType.StoredProcedure;
                        cmdacno.CommandText = "prc_getMaxAcceptanceNo";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                        errorline.Text = "";
                        string opid = Session["OperatorId"].ToString();

                        string state = Session["State_Id"].ToString();

                        string cacno = Accpt_NO;

                        string qrydata = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "'";

                        errorline.Text = "Check Acc Comobj";

                        mobj = new MoveChallan(ComObj);
                        errorline.Text = "Select Duplicate csms";
                        DataSet dschdt = mobj.selectAny(qrydata);
                        if (dschdt == null)
                        {


                        }
                        else
                        {
                            if (dschdt.Tables[0].Rows.Count == 0)
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                }

                                else if (ddlgodown.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
                                    return;
                                }

                                else
                                {

                                    string acptno = Accpt_NO;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {

                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            //CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                            //if (GchkBx.Checked == true)

                                            //{



                                            getcsms_Commdty();
                                            //  txtrcq.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");

                                            errorline.Text = "Grid2 Reading";
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            string CropYear = gr.Cells[9].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);

                                                Int32 fir4 = Convert.ToInt32(firstfour);

                                                string lastfour = (fir4 + 1).ToString();

                                                CropYear = firstfour + "-" + lastfour;

                                            }
                                            else
                                            {

                                            }




                                            // decimal R_Bag = CheckNull(txtrcBags.Text);
                                            // decimal mqty = CheckNull(txtrcq.Text);
                                            // decimal rejcqty = CheckNull(txtrjctqt.Text);

                                            //string rvcqtyFaq = gr.Cells[4].Text;

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");


                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //string rvcqtyurs = gr.Cells[5].Text;

                                            Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");

                                            decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));

                                            //  string rvcqty = gr.Cells[3].Text;

                                            decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;       // total of Faq and Urs is accepted qty (Anurag)

                                            // decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            // decimal Totl = mqty + rejcqty;

                                            //if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");

                                            //}
                                            //else
                                            //{

                                            //string jutenew = gr.Cells[6].Text;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");


                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));

                                            // string recpp = gr.Cells[7].Text;

                                            Label recpp = (Label)gr.FindControl("lblrcpp");

                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));

                                            // string juteold = gr.Cells[8].Text;


                                            Label juteold = (Label)gr.FindControl("lblrcjutold");

                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));

                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;       // total of all type bags is accepted bags (Anurag)


                                            SqlTransaction trns;

                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Detail where Distt_ID='" + distid + "'  and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";

                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                                            SqlDataReader drduplicate;

                                            errorline.Text = "Reader";
                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();

                                                return;
                                            }

                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;


                                            if (con_paddy.State == ConnectionState.Closed)
                                            {
                                                con_paddy.Open();
                                            }


                                            trns2 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";


                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;
                                                cmd.CommandTimeout = 600;
                                                errorline.Text = "CSMS Accept Insert";
                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();

                                                cmd.CommandTimeout = 600;

                                                errorline.Text = "Update SCSC Accept tbl";
                                                cmd.ExecuteNonQuery();
                                                // con.Close();
                                                //if (con_Maze.State == ConnectionState.Closed)
                                                //{
                                                //    con_Maze.Open();
                                                //}

                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "')";

                                                cmdP.Connection = con_paddy;
                                                cmdP.CommandText = inst1;
                                                cmdP.CommandTimeout = 600;

                                                errorline.Text = "Insert PPMS Accet tbl";

                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.CommandTimeout = 600;

                                                errorline.Text = "Update PPMS Issue center Receipt";

                                                cmdP.ExecuteNonQuery();

                                                errorline.Text = "";

                                                trns.Commit();
                                                trns2.Commit();
                                                con.Close();
                                                con_paddy.Close();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                btnsubmit.Enabled = false;
                                                //--
                                                lblacno.Visible = true;
                                                txtaccptno.Visible = true;
                                                txtaccptno.Text = Accpt_NO;
                                                txtaccptno.Enabled = false;
                                                Session["Acceptance_NO"] = Accpt_NO;

                                                Session["Godown"] = ddlgodown.SelectedValue;


                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;

                                                Session["issubmited"] = "Yes";

                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                con.Close();
                                                con_paddy.Close();
                                                Label3.Visible = true;
                                                Label3.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                con.Close();
                                                con_paddy.Close();
                                            }

                                            // }

                                            // } 12-10-15 Anurag , no need to select , just insert all grid data and generte accep note
                                            //--
                                            //else
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                            //}
                                        }
                                    }

                                }


                            }

                            else
                            {
                                # region unused
                                //string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "'";
                                //mobj = new MoveChallan(ComObj);
                                //DataSet dspacno = mobj.selectAny(qryacno);

                                //if (dspacno.Tables[0].Rows.Count == 0)
                                //{
                                //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                                //}
                                //else
                                //{
                                //    if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                //    {
                                //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                //    }
                                //    else
                                //    {

                                //        string acptno = Accpt_NO;
                                //        string mpc = ddlpurchcenterP.SelectedValue;
                                //        string pdatw = getDate_MDY(DaintyDate3P.Text);
                                //        int month = int.Parse(DateTime.Today.Month.ToString());
                                //        int year = int.Parse(DateTime.Today.Year.ToString());
                                //        string udate = "";
                                //        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                //        if (GridView2.Rows.Count == 0)
                                //        {
                                //        }
                                //        else
                                //        {

                                //            foreach (GridViewRow gr in GridView2.Rows)
                                //            {
                                //                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                //                TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                //                TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                //                TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                //                if (GchkBx.Checked == true)
                                //                {
                                //                    getcsms_Commdty();
                                //                    string iss_ID = gr.Cells[1].Text;
                                //                    string challan = gr.Cells[2].Text;
                                //                    string truckno = gr.Cells[3].Text;
                                //                    decimal R_Bag = CheckNull(txtrcBags.Text);
                                //                    decimal mqty = CheckNull(txtrcq.Text);
                                //                    decimal rejcqty = CheckNull(txtrjctqt.Text);
                                //                    string rvcqty = gr.Cells[6].Text;
                                //                    decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                //                    decimal Totl = mqty + rejcqty;
                                //                    if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                //                    {
                                //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                //                        return;
                                //                    }
                                //                    SqlTransaction trns;
                                //                    if (con.State == ConnectionState.Closed)
                                //                    {
                                //                        con.Open();
                                //                    }
                                //                    trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmd.Transaction = trns;
                                //                    SqlTransaction trns2;
                                //                    con_paddy.Open();
                                //                    trns2 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmdP.Transaction = trns2;
                                //                    //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                    string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "')";

                                //                    try
                                //                    {
                                //                        cmd.Connection = con;
                                //                        cmd.CommandText = inst;

                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();

                                //                        string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                //                        cmd.CommandText = updt;
                                //                        //con.Open();
                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();


                                //                        //procment
                                //                        //string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                        string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "')";


                                //                        cmdP.Connection = con_paddy;
                                //                        cmdP.CommandText = inst1;
                                //                        cmdP.ExecuteNonQuery();


                                //                        string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
                                //                        cmdP.CommandText = updt1;
                                //                        cmdP.ExecuteNonQuery();

                                //                        trns.Commit();
                                //                        trns2.Commit();

                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }




                                //                        if (con_paddy.State == ConnectionState.Open)
                                //                        {
                                //                            con_paddy.Close();
                                //                        }



                                //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                //                        btnsubmit.Enabled = false;
                                //                        //--
                                //                        lblacno.Visible = true;
                                //                        txtaccptno.Visible = true;
                                //                        txtaccptno.Text = Accpt_NO;
                                //                        txtaccptno.Enabled = false;
                                //                        Session["Acceptance_NO"] = Accpt_NO;
                                //                        Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                //                        hyprlnkprint.Visible = true;

                                //                        Session["issubmited"] = "Yes";

                                //                    }
                                //                    catch (Exception ex)
                                //                    {
                                //                        trns.Rollback();
                                //                        trns2.Rollback();

                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }




                                //                        if (con_paddy.State == ConnectionState.Open)
                                //                        {
                                //                            con_paddy.Close();
                                //                        }



                                //                        Label3.Visible = true;
                                //                        Label3.Text = ex.Message;
                                //                    }
                                //                    finally
                                //                    {
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }




                                //                        if (con_paddy.State == ConnectionState.Open)
                                //                        {
                                //                            con_paddy.Close();
                                //                        }

                                //                    }


                                //                }
                                //                //--
                                //                else
                                //                {
                                //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                //                }
                                //            }

                                //        }

                                //    }


                                //}

                                # endregion
                            }
                        }
                    }
                    # endregion

                    # region Jowar_submit
                    else if (ddlmarksesn.SelectedValue.ToString() == "4")
                    {

                        cmdacno.CommandType = CommandType.StoredProcedure;
                        cmdacno.CommandText = "prc_getMaxAcceptanceNo";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                        string opid = Session["OperatorId"].ToString();

                        string state = Session["State_Id"].ToString();

                        string cacno = Accpt_NO;
                        string qrydata = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "'";
                        mobj = new MoveChallan(ComObj);
                        DataSet dschdt = mobj.selectAny(qrydata);
                        if (dschdt == null)
                        {


                        }
                        else
                        {
                            if (dschdt.Tables[0].Rows.Count == 0)
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                }

                                else if (ddlgodown.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
                                    return;
                                }
                                else
                                {

                                    string acptno = Accpt_NO;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {

                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            //CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                            //if (GchkBx.Checked == true)
                                            //{
                                            getcsms_Commdty();
                                            //  txtrcq.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            string CropYear = gr.Cells[9].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);

                                                Int32 fir4 = Convert.ToInt32(firstfour);

                                                string lastfour = (fir4 + 1).ToString();

                                                CropYear = firstfour + "-" + lastfour;

                                            }
                                            else
                                            {

                                            }

                                            // decimal R_Bag = CheckNull(txtrcBags.Text);
                                            // decimal mqty = CheckNull(txtrcq.Text);
                                            // decimal rejcqty = CheckNull(txtrjctqt.Text);

                                            //string rvcqtyFaq = gr.Cells[4].Text;

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");

                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //string rvcqtyurs = gr.Cells[5].Text;

                                            Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");

                                            decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));

                                            //  string rvcqty = gr.Cells[3].Text;

                                            decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;       // total of Faq and Urs is accepted qty (Anurag)

                                            // decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            // decimal Totl = mqty + rejcqty;

                                            //if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");

                                            //}
                                            //else
                                            //{

                                            //string jutenew = gr.Cells[6].Text;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");


                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));

                                            // string recpp = gr.Cells[7].Text;

                                            Label recpp = (Label)gr.FindControl("lblrcpp");

                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));

                                            // string juteold = gr.Cells[8].Text;


                                            Label juteold = (Label)gr.FindControl("lblrcjutold");

                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));

                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;       // total of all type bags is accepted bags (Anurag)


                                            SqlTransaction trns;

                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Detail where Distt_ID='" + distid + "'  and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";

                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                                            SqlDataReader drduplicate;

                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();

                                                return;
                                            }

                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;


                                            if (con_Maze.State == ConnectionState.Closed)
                                            {
                                                con_Maze.Open();
                                            }


                                            trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";


                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;

                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();
                                                cmd.ExecuteNonQuery();
                                                // con.Close();
                                                //if (con_Maze.State == ConnectionState.Closed)
                                                //{
                                                //    con_Maze.Open();
                                                //}

                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "')";

                                                cmdP.Connection = con_Maze;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

                                                trns.Commit();
                                                trns2.Commit();
                                                con.Close();
                                                con_Maze.Close();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                btnsubmit.Enabled = false;
                                                //--
                                                lblacno.Visible = true;
                                                txtaccptno.Visible = true;
                                                txtaccptno.Text = Accpt_NO;
                                                txtaccptno.Enabled = false;
                                                Session["Acceptance_NO"] = Accpt_NO;

                                                Session["Godown"] = ddlgodown.SelectedValue;


                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;

                                                Session["issubmited"] = "Yes";

                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                con.Close();
                                                con_Maze.Close();
                                                Label3.Visible = true;
                                                Label3.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                con.Close();
                                                con_Maze.Close();
                                            }

                                            // }

                                            // } 12-10-15 Anurag , no need to select , just insert all grid data and generte accep note
                                            //--
                                            //else
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                            //}
                                        }
                                    }

                                }


                            }

                            else
                            {
                                # region unused
                                //string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "'";
                                //mobj = new MoveChallan(ComObj);
                                //DataSet dspacno = mobj.selectAny(qryacno);

                                //if (dspacno.Tables[0].Rows.Count == 0)
                                //{
                                //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                                //}
                                //else
                                //{
                                //    if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                //    {
                                //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                //    }
                                //    else
                                //    {

                                //        string acptno = Accpt_NO;
                                //        string mpc = ddlpurchcenterP.SelectedValue;
                                //        string pdatw = getDate_MDY(DaintyDate3P.Text);
                                //        int month = int.Parse(DateTime.Today.Month.ToString());
                                //        int year = int.Parse(DateTime.Today.Year.ToString());
                                //        string udate = "";
                                //        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                //        if (GridView2.Rows.Count == 0)
                                //        {
                                //        }
                                //        else
                                //        {

                                //            foreach (GridViewRow gr in GridView2.Rows)
                                //            {
                                //                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                //                TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                //                TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                //                TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                //                if (GchkBx.Checked == true)
                                //                {
                                //                    getcsms_Commdty();
                                //                    string iss_ID = gr.Cells[1].Text;
                                //                    string challan = gr.Cells[2].Text;
                                //                    string truckno = gr.Cells[3].Text;
                                //                    decimal R_Bag = CheckNull(txtrcBags.Text);
                                //                    decimal mqty = CheckNull(txtrcq.Text);
                                //                    decimal rejcqty = CheckNull(txtrjctqt.Text);
                                //                    string rvcqty = gr.Cells[6].Text;
                                //                    decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                //                    decimal Totl = mqty + rejcqty;
                                //                    if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                //                    {
                                //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                //                        return;
                                //                    }
                                //                    SqlTransaction trns;
                                //                    if (con.State == ConnectionState.Closed)
                                //                    {
                                //                        con.Open();
                                //                    }
                                //                    trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmd.Transaction = trns;
                                //                    SqlTransaction trns2;

                                //                    if (con_Maze.State == ConnectionState.Closed)
                                //                    {

                                //                        con_Maze.Open();
                                //                    }
                                //                    trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmdP.Transaction = trns2;
                                //                    //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                    string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "')";

                                //                    try
                                //                    {
                                //                        cmd.Connection = con;
                                //                        cmd.CommandText = inst;

                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();

                                //                        string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                //                        cmd.CommandText = updt;
                                //                        //con.Open();
                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();


                                //                        //procment
                                //                        //string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";
                                //                        string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "')";


                                //                        cmdP.Connection = con_Maze;
                                //                        cmdP.CommandText = inst1;
                                //                        cmdP.ExecuteNonQuery();


                                //                        string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
                                //                        cmdP.CommandText = updt1;
                                //                        cmdP.ExecuteNonQuery();

                                //                        trns.Commit();
                                //                        trns2.Commit();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                //                        btnsubmit.Enabled = false;
                                //                        //--
                                //                        lblacno.Visible = true;
                                //                        txtaccptno.Visible = true;
                                //                        txtaccptno.Text = Accpt_NO;
                                //                        txtaccptno.Enabled = false;
                                //                        Session["Acceptance_NO"] = Accpt_NO;
                                //                        Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                //                        hyprlnkprint.Visible = true;

                                //                        Session["issubmited"] = "Yes";

                                //                    }
                                //                    catch (Exception ex)
                                //                    {
                                //                        trns.Rollback();
                                //                        trns2.Rollback();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Label3.Visible = true;
                                //                        Label3.Text = ex.Message;
                                //                    }
                                //                    finally
                                //                    {
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                    }
                                //                }
                                //                //--
                                //                else
                                //                {
                                //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                //                }
                                //            }

                                //        }

                                //    }


                                //}

                                # endregion

                            }
                        }
                    }
                    # endregion

                    # region Maze_submit
                    else if (ddlmarksesn.SelectedValue.ToString() == "5")
                    {

                        cmdacno.CommandType = CommandType.StoredProcedure;
                        cmdacno.CommandText = "prc_getMaxAcceptanceNo";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                        string opid = Session["OperatorId"].ToString();

                        string state = Session["State_Id"].ToString();

                        string cacno = Accpt_NO;
                        string qrydata = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "'";
                        mobj = new MoveChallan(ComObj);
                        DataSet dschdt = mobj.selectAny(qrydata);
                        if (dschdt == null)
                        {


                        }
                        else
                        {
                            if (dschdt.Tables[0].Rows.Count == 0)
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                }

                                else if (ddlgodown.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
                                    return;
                                }
                                else
                                {

                                    string acptno = Accpt_NO;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {

                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            //CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                            //if (GchkBx.Checked == true)
                                            //{
                                            getcsms_Commdty();
                                            //  txtrcq.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            string CropYear = gr.Cells[9].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);

                                                Int32 fir4 = Convert.ToInt32(firstfour);

                                                string lastfour = (fir4 + 1).ToString();

                                                CropYear = firstfour + "-" + lastfour;

                                            }
                                            else
                                            {

                                            }

                                            // decimal R_Bag = CheckNull(txtrcBags.Text);
                                            // decimal mqty = CheckNull(txtrcq.Text);
                                            // decimal rejcqty = CheckNull(txtrjctqt.Text);

                                            //string rvcqtyFaq = gr.Cells[4].Text;

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");


                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //string rvcqtyurs = gr.Cells[5].Text;

                                            Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");

                                            decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));

                                            //  string rvcqty = gr.Cells[3].Text;

                                            decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;       // total of Faq and Urs is accepted qty (Anurag)

                                            // decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            // decimal Totl = mqty + rejcqty;

                                            //if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");

                                            //}
                                            //else
                                            //{

                                            //string jutenew = gr.Cells[6].Text;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");


                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));

                                            // string recpp = gr.Cells[7].Text;

                                            Label recpp = (Label)gr.FindControl("lblrcpp");

                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));

                                            // string juteold = gr.Cells[8].Text;


                                            Label juteold = (Label)gr.FindControl("lblrcjutold");

                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));

                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;       // total of all type bags is accepted bags (Anurag)


                                            SqlTransaction trns;

                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Detail where Distt_ID='" + distid + "'  and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";

                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                                            SqlDataReader drduplicate;

                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();

                                                return;
                                            }

                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;


                                            if (con_Maze.State == ConnectionState.Closed)
                                            {
                                                con_Maze.Open();
                                            }


                                            trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";


                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;

                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();
                                                cmd.ExecuteNonQuery();
                                                // con.Close();
                                                //if (con_Maze.State == ConnectionState.Closed)
                                                //{
                                                //    con_Maze.Open();
                                                //}

                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "')";

                                                cmdP.Connection = con_Maze;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

                                                trns.Commit();
                                                trns2.Commit();
                                                con.Close();
                                                con_Maze.Close();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                btnsubmit.Enabled = false;
                                                //--
                                                lblacno.Visible = true;
                                                txtaccptno.Visible = true;
                                                txtaccptno.Text = Accpt_NO;
                                                txtaccptno.Enabled = false;
                                                Session["Acceptance_NO"] = Accpt_NO;

                                                Session["Godown"] = ddlgodown.SelectedValue;


                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;

                                                Session["issubmited"] = "Yes";

                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                con.Close();
                                                con_Maze.Close();
                                                Label3.Visible = true;
                                                Label3.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                con.Close();
                                                con_Maze.Close();
                                            }

                                            // }

                                            // } 12-10-15 Anurag , no need to select , just insert all grid data and generte accep note
                                            //--
                                            //else
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                            //}
                                        }
                                    }

                                }


                            }

                            else
                            {
                                # region unused
                                //string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "'";
                                //mobj = new MoveChallan(ComObj);
                                //DataSet dspacno = mobj.selectAny(qryacno);

                                //if (dspacno.Tables[0].Rows.Count == 0)
                                //{
                                //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                                //}
                                //else
                                //{
                                //    if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                //    {
                                //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                //    }
                                //    else
                                //    {

                                //        string acptno = Accpt_NO;
                                //        string mpc = ddlpurchcenterP.SelectedValue;
                                //        string pdatw = getDate_MDY(DaintyDate3P.Text);
                                //        int month = int.Parse(DateTime.Today.Month.ToString());
                                //        int year = int.Parse(DateTime.Today.Year.ToString());
                                //        string udate = "";
                                //        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                //        if (GridView2.Rows.Count == 0)
                                //        {
                                //        }
                                //        else
                                //        {

                                //            foreach (GridViewRow gr in GridView2.Rows)
                                //            {
                                //                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                //                TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                //                TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                //                TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                //                if (GchkBx.Checked == true)
                                //                {
                                //                    getcsms_Commdty();
                                //                    string iss_ID = gr.Cells[1].Text;
                                //                    string challan = gr.Cells[2].Text;
                                //                    string truckno = gr.Cells[3].Text;
                                //                    decimal R_Bag = CheckNull(txtrcBags.Text);
                                //                    decimal mqty = CheckNull(txtrcq.Text);
                                //                    decimal rejcqty = CheckNull(txtrjctqt.Text);
                                //                    string rvcqty = gr.Cells[6].Text;
                                //                    decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                //                    decimal Totl = mqty + rejcqty;
                                //                    if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                //                    {
                                //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                //                        return;
                                //                    }
                                //                    SqlTransaction trns;
                                //                    if (con.State == ConnectionState.Closed)
                                //                    {
                                //                        con.Open();
                                //                    }
                                //                    trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmd.Transaction = trns;
                                //                    SqlTransaction trns2;

                                //                    if (con_Maze.State == ConnectionState.Closed)
                                //                    {

                                //                        con_Maze.Open();
                                //                    }
                                //                    trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmdP.Transaction = trns2;
                                //                    //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                    string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "')";

                                //                    try
                                //                    {
                                //                        cmd.Connection = con;
                                //                        cmd.CommandText = inst;

                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();

                                //                        string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                //                        cmd.CommandText = updt;
                                //                        //con.Open();
                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();


                                //                        //procment
                                //                        //string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";
                                //                        string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "')";


                                //                        cmdP.Connection = con_Maze;
                                //                        cmdP.CommandText = inst1;
                                //                        cmdP.ExecuteNonQuery();


                                //                        string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
                                //                        cmdP.CommandText = updt1;
                                //                        cmdP.ExecuteNonQuery();

                                //                        trns.Commit();
                                //                        trns2.Commit();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                //                        btnsubmit.Enabled = false;
                                //                        //--
                                //                        lblacno.Visible = true;
                                //                        txtaccptno.Visible = true;
                                //                        txtaccptno.Text = Accpt_NO;
                                //                        txtaccptno.Enabled = false;
                                //                        Session["Acceptance_NO"] = Accpt_NO;
                                //                        Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                //                        hyprlnkprint.Visible = true;

                                //                        Session["issubmited"] = "Yes";

                                //                    }
                                //                    catch (Exception ex)
                                //                    {
                                //                        trns.Rollback();
                                //                        trns2.Rollback();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Label3.Visible = true;
                                //                        Label3.Text = ex.Message;
                                //                    }
                                //                    finally
                                //                    {
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                    }
                                //                }
                                //                //--
                                //                else
                                //                {
                                //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                //                }
                                //            }

                                //        }

                                //    }


                                //}

                                # endregion

                            }
                        }
                    }
                    # endregion

                    # region Bajra_submit
                    else if (ddlmarksesn.SelectedValue.ToString() == "6")
                    {

                        cmdacno.CommandType = CommandType.StoredProcedure;
                        cmdacno.CommandText = "prc_getMaxAcceptanceNo";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                        string opid = Session["OperatorId"].ToString();

                        string state = Session["State_Id"].ToString();

                        string cacno = Accpt_NO;
                        string qrydata = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "'";
                        mobj = new MoveChallan(ComObj);
                        DataSet dschdt = mobj.selectAny(qrydata);
                        if (dschdt == null)
                        {


                        }
                        else
                        {
                            if (dschdt.Tables[0].Rows.Count == 0)
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                }

                                else if (ddlgodown.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
                                    return;
                                }
                                else
                                {

                                    string acptno = Accpt_NO;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {

                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            //CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                            //if (GchkBx.Checked == true)
                                            //{
                                            getcsms_Commdty();
                                            //  txtrcq.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            string CropYear = gr.Cells[9].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);

                                                Int32 fir4 = Convert.ToInt32(firstfour);

                                                string lastfour = (fir4 + 1).ToString();

                                                CropYear = firstfour + "-" + lastfour;

                                            }
                                            else
                                            {

                                            }

                                            // decimal R_Bag = CheckNull(txtrcBags.Text);
                                            // decimal mqty = CheckNull(txtrcq.Text);
                                            // decimal rejcqty = CheckNull(txtrjctqt.Text);

                                            //string rvcqtyFaq = gr.Cells[4].Text;

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");


                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //string rvcqtyurs = gr.Cells[5].Text;

                                            Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");

                                            decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));

                                            //  string rvcqty = gr.Cells[3].Text;

                                            decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;       // total of Faq and Urs is accepted qty (Anurag)

                                            // decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            // decimal Totl = mqty + rejcqty;

                                            //if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");

                                            //}
                                            //else
                                            //{

                                            //string jutenew = gr.Cells[6].Text;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");


                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));

                                            // string recpp = gr.Cells[7].Text;

                                            Label recpp = (Label)gr.FindControl("lblrcpp");

                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));

                                            // string juteold = gr.Cells[8].Text;


                                            Label juteold = (Label)gr.FindControl("lblrcjutold");

                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));

                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;       // total of all type bags is accepted bags (Anurag)


                                            SqlTransaction trns;

                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Detail where Distt_ID='" + distid + "'  and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";

                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                                            SqlDataReader drduplicate;

                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();

                                                return;
                                            }

                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;


                                            if (con_Maze.State == ConnectionState.Closed)
                                            {
                                                con_Maze.Open();
                                            }


                                            trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";


                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;

                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();
                                                cmd.ExecuteNonQuery();
                                                // con.Close();
                                                //if (con_Maze.State == ConnectionState.Closed)
                                                //{
                                                //    con_Maze.Open();
                                                //}

                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "')";

                                                cmdP.Connection = con_Maze;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

                                                trns.Commit();
                                                trns2.Commit();
                                                con.Close();
                                                con_Maze.Close();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                btnsubmit.Enabled = false;
                                                //--
                                                lblacno.Visible = true;
                                                txtaccptno.Visible = true;
                                                txtaccptno.Text = Accpt_NO;
                                                txtaccptno.Enabled = false;
                                                Session["Acceptance_NO"] = Accpt_NO;

                                                Session["Godown"] = ddlgodown.SelectedValue;


                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;

                                                Session["issubmited"] = "Yes";

                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                con.Close();
                                                con_Maze.Close();
                                                Label3.Visible = true;
                                                Label3.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                con.Close();
                                                con_Maze.Close();
                                            }

                                            // }

                                            // } 12-10-15 Anurag , no need to select , just insert all grid data and generte accep note
                                            //--
                                            //else
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                            //}
                                        }
                                    }

                                }


                            }

                            else
                            {
                                # region unused
                                //string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "'";
                                //mobj = new MoveChallan(ComObj);
                                //DataSet dspacno = mobj.selectAny(qryacno);

                                //if (dspacno.Tables[0].Rows.Count == 0)
                                //{
                                //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                                //}
                                //else
                                //{
                                //    if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                //    {
                                //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                //    }
                                //    else
                                //    {

                                //        string acptno = Accpt_NO;
                                //        string mpc = ddlpurchcenterP.SelectedValue;
                                //        string pdatw = getDate_MDY(DaintyDate3P.Text);
                                //        int month = int.Parse(DateTime.Today.Month.ToString());
                                //        int year = int.Parse(DateTime.Today.Year.ToString());
                                //        string udate = "";
                                //        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                //        if (GridView2.Rows.Count == 0)
                                //        {
                                //        }
                                //        else
                                //        {

                                //            foreach (GridViewRow gr in GridView2.Rows)
                                //            {
                                //                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                //                TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                //                TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                //                TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                //                if (GchkBx.Checked == true)
                                //                {
                                //                    getcsms_Commdty();
                                //                    string iss_ID = gr.Cells[1].Text;
                                //                    string challan = gr.Cells[2].Text;
                                //                    string truckno = gr.Cells[3].Text;
                                //                    decimal R_Bag = CheckNull(txtrcBags.Text);
                                //                    decimal mqty = CheckNull(txtrcq.Text);
                                //                    decimal rejcqty = CheckNull(txtrjctqt.Text);
                                //                    string rvcqty = gr.Cells[6].Text;
                                //                    decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                //                    decimal Totl = mqty + rejcqty;
                                //                    if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                //                    {
                                //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                //                        return;
                                //                    }
                                //                    SqlTransaction trns;
                                //                    if (con.State == ConnectionState.Closed)
                                //                    {
                                //                        con.Open();
                                //                    }
                                //                    trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmd.Transaction = trns;
                                //                    SqlTransaction trns2;

                                //                    if (con_Maze.State == ConnectionState.Closed)
                                //                    {

                                //                        con_Maze.Open();
                                //                    }
                                //                    trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmdP.Transaction = trns2;
                                //                    //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                    string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "')";

                                //                    try
                                //                    {
                                //                        cmd.Connection = con;
                                //                        cmd.CommandText = inst;

                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();

                                //                        string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                //                        cmd.CommandText = updt;
                                //                        //con.Open();
                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();


                                //                        //procment
                                //                        //string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";
                                //                        string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "')";


                                //                        cmdP.Connection = con_Maze;
                                //                        cmdP.CommandText = inst1;
                                //                        cmdP.ExecuteNonQuery();


                                //                        string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
                                //                        cmdP.CommandText = updt1;
                                //                        cmdP.ExecuteNonQuery();

                                //                        trns.Commit();
                                //                        trns2.Commit();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                //                        btnsubmit.Enabled = false;
                                //                        //--
                                //                        lblacno.Visible = true;
                                //                        txtaccptno.Visible = true;
                                //                        txtaccptno.Text = Accpt_NO;
                                //                        txtaccptno.Enabled = false;
                                //                        Session["Acceptance_NO"] = Accpt_NO;
                                //                        Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                //                        hyprlnkprint.Visible = true;

                                //                        Session["issubmited"] = "Yes";

                                //                    }
                                //                    catch (Exception ex)
                                //                    {
                                //                        trns.Rollback();
                                //                        trns2.Rollback();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Label3.Visible = true;
                                //                        Label3.Text = ex.Message;
                                //                    }
                                //                    finally
                                //                    {
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                    }
                                //                }
                                //                //--
                                //                else
                                //                {
                                //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                //                }
                                //            }

                                //        }

                                //    }


                                //}

                                # endregion

                            }
                        }
                    }
                    # endregion

                    # region JO_submit
                    else if (ddlmarksesn.SelectedValue.ToString() == "7")
                    {

                        cmdacno.CommandType = CommandType.StoredProcedure;
                        cmdacno.CommandText = "prc_getMaxAcceptanceNo";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                        string opid = Session["OperatorId"].ToString();

                        string state = Session["State_Id"].ToString();

                        string cacno = Accpt_NO;
                        string qrydata = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "'";
                        mobj = new MoveChallan(ComObj);
                        DataSet dschdt = mobj.selectAny(qrydata);
                        if (dschdt == null)
                        {


                        }
                        else
                        {
                            if (dschdt.Tables[0].Rows.Count == 0)
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                }
                                else if (ddlgodown.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
                                    return;
                                }

                                else
                                {

                                    string acptno = Accpt_NO;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {

                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            //CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                            //if (GchkBx.Checked == true)
                                            //{
                                            getcsms_Commdty();
                                            //  txtrcq.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            string CropYear = gr.Cells[9].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);

                                                Int32 fir4 = Convert.ToInt32(firstfour);

                                                string lastfour = (fir4 + 1).ToString();

                                                CropYear = firstfour + "-" + lastfour;

                                            }
                                            else
                                            {

                                            }


                                            // decimal R_Bag = CheckNull(txtrcBags.Text);
                                            // decimal mqty = CheckNull(txtrcq.Text);
                                            // decimal rejcqty = CheckNull(txtrjctqt.Text);

                                            //string rvcqtyFaq = gr.Cells[4].Text;

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");


                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //string rvcqtyurs = gr.Cells[5].Text;

                                            Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");

                                            decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));

                                            //  string rvcqty = gr.Cells[3].Text;

                                            decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;       // total of Faq and Urs is accepted qty (Anurag)

                                            // decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            // decimal Totl = mqty + rejcqty;

                                            //if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");

                                            //}
                                            //else
                                            //{

                                            //string jutenew = gr.Cells[6].Text;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");


                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));

                                            // string recpp = gr.Cells[7].Text;

                                            Label recpp = (Label)gr.FindControl("lblrcpp");

                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));

                                            // string juteold = gr.Cells[8].Text;


                                            Label juteold = (Label)gr.FindControl("lblrcjutold");

                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));

                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;       // total of all type bags is accepted bags (Anurag)


                                            SqlTransaction trns;

                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Detail where Distt_ID='" + distid + "'  and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";

                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                                            SqlDataReader drduplicate;

                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();

                                                return;
                                            }

                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;


                                            if (con_Maze.State == ConnectionState.Closed)
                                            {
                                                con_Maze.Open();
                                            }


                                            trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";


                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;

                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();
                                                cmd.ExecuteNonQuery();
                                                // con.Close();
                                                //if (con_Maze.State == ConnectionState.Closed)
                                                //{
                                                //    con_Maze.Open();
                                                //}

                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "')";

                                                cmdP.Connection = con_Maze;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

                                                trns.Commit();
                                                trns2.Commit();
                                                con.Close();
                                                con_Maze.Close();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                                btnsubmit.Enabled = false;
                                                //--
                                                lblacno.Visible = true;
                                                txtaccptno.Visible = true;
                                                txtaccptno.Text = Accpt_NO;
                                                txtaccptno.Enabled = false;
                                                Session["Acceptance_NO"] = Accpt_NO;

                                                Session["Godown"] = ddlgodown.SelectedValue;


                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;

                                                Session["issubmited"] = "Yes";

                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                con.Close();
                                                con_Maze.Close();
                                                Label3.Visible = true;
                                                Label3.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                con.Close();
                                                con_Maze.Close();
                                            }

                                            // }

                                            // } 12-10-15 Anurag , no need to select , just insert all grid data and generte accep note
                                            //--
                                            //else
                                            //{
                                            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                            //}
                                        }
                                    }

                                }


                            }

                            else
                            {
                                # region unused
                                //string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "'";
                                //mobj = new MoveChallan(ComObj);
                                //DataSet dspacno = mobj.selectAny(qryacno);

                                //if (dspacno.Tables[0].Rows.Count == 0)
                                //{
                                //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                                //}
                                //else
                                //{
                                //    if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                //    {
                                //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
                                //    }
                                //    else
                                //    {

                                //        string acptno = Accpt_NO;
                                //        string mpc = ddlpurchcenterP.SelectedValue;
                                //        string pdatw = getDate_MDY(DaintyDate3P.Text);
                                //        int month = int.Parse(DateTime.Today.Month.ToString());
                                //        int year = int.Parse(DateTime.Today.Year.ToString());
                                //        string udate = "";
                                //        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                //        if (GridView2.Rows.Count == 0)
                                //        {
                                //        }
                                //        else
                                //        {

                                //            foreach (GridViewRow gr in GridView2.Rows)
                                //            {
                                //                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                //                TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                //                TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                //                TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                //                if (GchkBx.Checked == true)
                                //                {
                                //                    getcsms_Commdty();
                                //                    string iss_ID = gr.Cells[1].Text;
                                //                    string challan = gr.Cells[2].Text;
                                //                    string truckno = gr.Cells[3].Text;
                                //                    decimal R_Bag = CheckNull(txtrcBags.Text);
                                //                    decimal mqty = CheckNull(txtrcq.Text);
                                //                    decimal rejcqty = CheckNull(txtrjctqt.Text);
                                //                    string rvcqty = gr.Cells[6].Text;
                                //                    decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                //                    decimal Totl = mqty + rejcqty;
                                //                    if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                //                    {
                                //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                //                        return;
                                //                    }
                                //                    SqlTransaction trns;
                                //                    if (con.State == ConnectionState.Closed)
                                //                    {
                                //                        con.Open();
                                //                    }
                                //                    trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmd.Transaction = trns;
                                //                    SqlTransaction trns2;

                                //                    if (con_Maze.State == ConnectionState.Closed)
                                //                    {

                                //                        con_Maze.Open();
                                //                    }
                                //                    trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                //                    cmdP.Transaction = trns2;
                                //                    //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";

                                //                    string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "')";

                                //                    try
                                //                    {
                                //                        cmd.Connection = con;
                                //                        cmd.CommandText = inst;

                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();

                                //                        string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                //                        cmd.CommandText = updt;
                                //                        //con.Open();
                                //                        cmd.ExecuteNonQuery();
                                //                        // con.Close();


                                //                        //procment
                                //                        //string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','" + txtGStiching.Text.Trim() + "','" + txtBStiching.Text.Trim() + "','" + txtGstencile.Text.Trim() + "','" + txtBstencile.Text.Trim() + "')";
                                //                        string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "')";


                                //                        cmdP.Connection = con_Maze;
                                //                        cmdP.CommandText = inst1;
                                //                        cmdP.ExecuteNonQuery();


                                //                        string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
                                //                        cmdP.CommandText = updt1;
                                //                        cmdP.ExecuteNonQuery();

                                //                        trns.Commit();
                                //                        trns2.Commit();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                //                        btnsubmit.Enabled = false;
                                //                        //--
                                //                        lblacno.Visible = true;
                                //                        txtaccptno.Visible = true;
                                //                        txtaccptno.Text = Accpt_NO;
                                //                        txtaccptno.Enabled = false;
                                //                        Session["Acceptance_NO"] = Accpt_NO;
                                //                        Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                //                        hyprlnkprint.Visible = true;

                                //                        Session["issubmited"] = "Yes";

                                //                    }
                                //                    catch (Exception ex)
                                //                    {
                                //                        trns.Rollback();
                                //                        trns2.Rollback();
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                        Label3.Visible = true;
                                //                        Label3.Text = ex.Message;
                                //                    }
                                //                    finally
                                //                    {
                                //                        if (con.State == ConnectionState.Open)
                                //                        {
                                //                            con.Close();
                                //                        }

                                //                        if (con_Maze.State == ConnectionState.Open)
                                //                        {
                                //                            con_Maze.Close();
                                //                        }
                                //                    }
                                //                }
                                //                //--
                                //                else
                                //                {
                                //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                //                }
                                //            }

                                //        }

                                //    }


                                //}

                                # endregion

                            }
                        }
                    }
                    # endregion

                }

            }
            catch (Exception ex)
            {
                Label3.Visible = true;
                Label3.Text = ex.Message;
            }
            //}
            //else
            //{

            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select valid date...'); </script> ");


            //}
        }

    }

    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("issue_welcome.aspx");
    }

    protected void btnviewdetails_Click(object sender, EventArgs e)
    {

        getcsms_Commdty();

        bindgrid();
        txtAccDate.Text = DaintyDate3P.Text;


    }

    protected void btnnw_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/IssueCenter/IssueACNo.aspx");

        Session["issubmited"] = "No";

        getsociety(distid);

        GetGodown();

        bindgrid();
    }

    protected void txtrcq_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow currentRow = (GridViewRow)(sender as TextBox).Parent.Parent;
            CheckBox GchkBx = (CheckBox)currentRow.FindControl("cbSelectAll");
            TextBox txtrcq = (TextBox)currentRow.FindControl("txtrcq");
            TextBox txtrjctqt = (TextBox)currentRow.FindControl("txtrjctqt");


            if (GchkBx.Checked == true)
            {

                string truckno = currentRow.Cells[6].Text;
                decimal dtruckno = Convert.ToDecimal(truckno);
                decimal mqty = CheckNull(txtrcq.Text);
                decimal chkqt = dtruckno - mqty;
                decimal percnt = (dtruckno * 5) / 100;
                decimal lesqty = dtruckno + percnt;

                if (mqty > lesqty)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Acepted_Qty it should not be greater than ten times Recd_Qty....'); </script> ");
                    txtrjctqt.Text = "0";
                    txtrcq.Text = currentRow.Cells[6].Text;
                    return;
                }

                if (chkqt <= 0)
                {
                    txtrjctqt.Text = "0";
                }
                else
                {
                    txtrjctqt.Text = Convert.ToString(chkqt);
                }
            }

            // from here

            Int32 sum = 0;
            for (int i = 0; i < GridView2.Rows.Count; ++i)
            {

                TextBox txtrcQ = (TextBox)GridView2.Rows[i].FindControl("txtrcq");

                Int32 mqtyq = Convert.ToInt32(CheckNull(txtrcQ.Text));

                sum += mqtyq;

                GridRecTotal = sum.ToString();

            }

            GridViewRow row = GridView2.FooterRow;
            ((Label)row.FindControl("lbl_Qty")).Text = sum.ToString();

            // to here
        }
        catch (Exception ex)
        {
            Label3.Visible = true;
            Label3.ForeColor = System.Drawing.Color.Red;
            Label3.Text = ex.Message;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecdQty_Faq"));

            qty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecdQty_Urs"));

            jutnew += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecdBags_JuteNew"));

            ppbag += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecdBags_PP"));

            juteold += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecdBags_JuteOld"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount1 = (Label)e.Row.FindControl("lbl_totfaq");

            lblAmount1.Text = total1.ToString();

            Label lblqty = (Label)e.Row.FindControl("lbl_toturs");

            lblqty.Text = qty.ToString();


            Label lbljutnewbag = (Label)e.Row.FindControl("lbl_totjutnew");

            lbljutnewbag.Text = jutnew.ToString();

            Label lblppbags = (Label)e.Row.FindControl("lbl_totpp");

            lblppbags.Text = ppbag.ToString();


            Label lbljutold = (Label)e.Row.FindControl("lbl_totjutold");

            lbljutold.Text = juteold.ToString();



        }
    }

    //protected void txtrcBags_TextChanged(object sender, EventArgs e)
    //{
    //    Int32 sum = 0;
    //    for (int i = 0; i < GridView2.Rows.Count; ++i)
    //    {      

    //        GridViewRow currentRow = (GridViewRow)(sender as TextBox).Parent.Parent;

    //        TextBox txtrcBags = (TextBox)GridView2.Rows[i].FindControl("txtrcBags");

    //        Int32 mqty11 = Convert.ToInt32(CheckNull(txtrcBags.Text));

    //        sum += mqty11;

    //        GridRecTotal = sum.ToString();

    //        //txtBstencile.Text = "0";

    //        //txtBStiching.Text = "0";
    //    }

    //    GridViewRow row = GridView2.FooterRow;
    //    ((Label)row.FindControl("lbl_CurrentDarj")).Text = sum.ToString();

    //    txtGstencile.Text = sum.ToString();

    //    txtGStiching.Text = sum.ToString();

    //    txtBstencile.Text = "0";

    //    txtBStiching.Text = "0";
    //}

    //protected void txtGstencile_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtGstencile.Text == "")
    //    {


    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Must Enter Some Value....'); </script> ");

    //       return;
    //    }

    //    cal_Stencil();
    //}

    //protected void txtGStiching_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtGStiching.Text == "")
    //    {

    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Must Enter Some Value....'); </script> ");

    //        return;
    //    }
    //    cal_Stiching();

    //}

    //private void cal_Stencil()
    //{
    //    Int64 orignalsum = Convert.ToInt64(GridRecTotal);

    //    Int64 changed = Convert.ToInt64(txtGstencile.Text.Trim());

    //    if (changed > orignalsum)
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Recd Not be More than Actual Accepted Bags.'); </script> ");
    //        return;
    //    }

    //    Int64 cal = orignalsum - changed;

    //    string BadValue = Convert.ToString(cal);

    //    if (BadValue == "")
    //    {
    //        txtBstencile.Text = "0";
    //    }

    //    else
    //    {
    //        txtBstencile.Text = Convert.ToString(cal);
    //    }       
    //}

    //private void cal_Stiching()
    //{
    //Int64 orignalsum = Convert.ToInt64(GridRecTotal);

    //Int64 changed = Convert.ToInt64(txtGStiching.Text.Trim());

    //if (changed > orignalsum)
    //{
    //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Recd Not be More than Actual Accepted Bags.'); </script> ");
    //    return;
    //}

    //Int64 diff = orignalsum - changed;

    //string BadValue = Convert.ToString(diff);


    //if (BadValue == "")
    //{
    //    txtBStiching.Text = "0";
    //}

    //else
    //{
    //    txtBStiching.Text = Convert.ToString(diff);
    //}


    //}  //  Coomented for new changes 12-03-15 Anurag

    protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        // double sum = 0;

        //foreach (GridViewRow row in GridView2.Rows)

        //{

        //    CheckBox cb = (CheckBox)row.FindControl("cbSelectAll");

        //     if (cb.Checked)
        //     {

        //         TextBox tb = (TextBox)(row.FindControl("txtrcBags"));

        //         double amount = Convert.ToDouble(CheckNull(tb.Text));


        //         sum += amount;

        //         txtBstencile.Text = "0";
        //         txtBStiching.Text = "0";

        //     }

        // }
        // GridRecTotal = sum.ToString();

        //txtGstencile.Text = sum.ToString();

        //txtGStiching.Text = sum.ToString();

    }

    private void bindgrid()
    {
        if (ddlgodown.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Godown Name...'); </script> ");

        }

        # region other_viewdetil
        if (ddltype.SelectedValue.ToString() == "06")
        {

            string pdate = getDate_MDY(DaintyDate3.Text);
            string mpc = ddlpurchcenter.SelectedValue;
            string qrydata = "Select SCSC_Procurement.TC_Number,SCSC_Procurement.Truck_Number,SCSC_Procurement.Commodity_Id,SCSC_Procurement.Quantity,SCSC_Procurement.Recd_Qty,SCSC_Procurement.Receipt_Id as IssueID,SCSC_Procurement.Recd_Bags as Recd_Bags,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name from dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY.Commodity_ID where SCSC_Procurement.Distt_ID='" + distid + "'and SCSC_Procurement.IssueCenter_ID='" + issueid + "'and SCSC_Procurement.Purchase_Center='" + mpc + "'and SCSC_Procurement.Recd_Date='" + pdate + "'and SCSC_Procurement.AN_Status='N'";
            mobj = new MoveChallan(ComObj);
            DataSet dschdt = mobj.selectAny(qrydata);
            if (dschdt == null)
            {


            }
            else
            {
                if (dschdt.Tables[0].Rows.Count == 0)
                {
                    string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "'";
                    mobj = new MoveChallan(ComObj);
                    DataSet dsac = mobj.selectAny(getacdata);
                    if (dsac == null)
                    {
                    }
                    else
                    {
                        if (dsac.Tables[0].Rows.Count == 0)
                        {
                            Label4.Visible = true;
                            Label4.Text = "Currently no record found";
                            GridView2.Visible = false;

                        }
                        else
                        {
                            GridView2.Visible = false;
                            Label4.Visible = true;
                            DataRow drac = dsac.Tables[0].Rows[0];
                            string acno = drac["Acceptance_No"].ToString();
                            Label4.Text = "Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date";

                        }


                    }

                }
                else
                {

                    Label4.Visible = false;
                    GridView2.Visible = true;
                    GridView2.DataSource = dschdt;
                    GridView2.DataBind();
                    txtaccptno.Text = "";
                    btnsubmit.Enabled = true;
                    lblacno.Visible = false;
                    txtaccptno.Visible = false;
                    hyprlnkprint.Visible = false;
                }

            }

        }
        # endregion
        //procurement
        else if (ddltype.SelectedValue.ToString() == "01")
        {

            # region wheat_viewdetil
            if (ddlmarksesn.SelectedValue.ToString() == "1")  // Wheat
            {
                try
                {
                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }

                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;
                    //string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.QtyTransffer,IssueCenterReceipt_Online.Recv_Qty as Recd_Qty,IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.Recd_Bags,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '"+ddlgodown.SelectedValue+"'";

                    string qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";

                    SqlDataAdapter da = new SqlDataAdapter(qrydata, con_WPMS);
                    DataSet dschdt = new DataSet();
                    da.Fill(dschdt);
                    if (dschdt == null)
                    {


                    }
                    else
                    {
                        if (dschdt.Tables[0].Rows.Count == 0)
                        {
                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";

                            SqlDataAdapter da1 = new SqlDataAdapter(getacdata, con_WPMS);
                            DataSet dsac = new DataSet();
                            da1.Fill(dsac);

                            if (dsac == null)
                            {

                            }
                            else
                            {
                                if (dsac.Tables[0].Rows.Count == 0)
                                {
                                    Label4.Visible = true;
                                    Label4.Text = "Currently no record found";
                                    GridView2.Visible = false;

                                }
                                else
                                {
                                    GridView2.Visible = false;
                                    Label4.Visible = true;
                                    DataRow drac = dsac.Tables[0].Rows[0];
                                    string acno = drac["Acceptance_No"].ToString();
                                    Label4.Text = "Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date";

                                }


                            }


                        }
                        else
                        {
                            Label4.Visible = false;
                            GridView2.Visible = true;
                            GridView2.DataSource = dschdt;
                            GridView2.DataBind();
                            txtaccptno.Text = "";
                            txtaccptno.Visible = false;
                            btnsubmit.Enabled = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Label3.Visible = true;
                    Label3.Text = ex.Message;
                }
                finally
                {
                    con_WPMS.Close();

                }
            }
            # endregion

            # region paddy_viewdetil
            else if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
            {
                try
                {
                    if (con_paddy.State == ConnectionState.Closed)
                    {
                        con_paddy.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;

                    // string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.QtyTransffer,IssueCenterReceipt_Online.Recv_Qty as Recd_Qty,IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.Recd_Bags,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N'";

                    string qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";

                    SqlDataAdapter da = new SqlDataAdapter(qrydata, con_paddy);
                    DataSet dschdt = new DataSet();
                    da.Fill(dschdt);

                    if (dschdt == null)
                    {


                    }
                    else
                    {
                        if (dschdt.Tables[0].Rows.Count == 0)
                        {
                            //string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";
                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";


                            SqlDataAdapter da1 = new SqlDataAdapter(getacdata, con_paddy);
                            DataSet dsac = new DataSet();
                            da1.Fill(dsac);
                            if (dsac == null)
                            {
                            }
                            else
                            {
                                if (dsac.Tables[0].Rows.Count == 0)
                                {
                                    Label4.Visible = true;
                                    Label4.Text = "Currently no record found";
                                    GridView2.Visible = false;

                                }
                                else
                                {
                                    GridView2.Visible = false;
                                    Label4.Visible = true;
                                    DataRow drac = dsac.Tables[0].Rows[0];
                                    string acno = drac["Acceptance_No"].ToString();
                                    Label4.Text = "Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date";

                                }


                            }
                        }
                        else
                        {
                            Label4.Visible = false;
                            GridView2.Visible = true;
                            GridView2.DataSource = dschdt;
                            GridView2.DataBind();
                            txtaccptno.Text = "";
                            txtaccptno.Visible = false;
                            btnsubmit.Enabled = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Label3.Visible = true;
                    Label3.Text = ex.Message;
                }
                finally
                {
                    con_paddy.Close();
                }
            }
            # endregion

            # region Jowar_viewdetil
            else if (ddlmarksesn.SelectedValue.ToString() == "4") // Jowar
            {
                try
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;
                    // string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.QtyTransffer,IssueCenterReceipt_Online.Recv_Qty as Recd_Qty,IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.Recd_Bags,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N'";

                    string qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";

                    SqlDataAdapter da = new SqlDataAdapter(qrydata, con_Maze);
                    DataSet dschdt = new DataSet();
                    da.Fill(dschdt);
                    if (dschdt == null)
                    {


                    }
                    else
                    {
                        if (dschdt.Tables[0].Rows.Count == 0)
                        {
                            // string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";

                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";

                            SqlDataAdapter da1 = new SqlDataAdapter(getacdata, con_Maze);
                            DataSet dsac = new DataSet();
                            da1.Fill(dsac);
                            if (dsac == null)
                            {
                            }
                            else
                            {
                                if (dsac.Tables[0].Rows.Count == 0)
                                {
                                    Label4.Visible = true;
                                    Label4.Text = "Currently no record found";
                                    GridView2.Visible = false;

                                }
                                else
                                {
                                    GridView2.Visible = false;
                                    Label4.Visible = true;
                                    DataRow drac = dsac.Tables[0].Rows[0];
                                    string acno = drac["Acceptance_No"].ToString();
                                    Label4.Text = "Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date";

                                }

                            }

                        }
                        else
                        {
                            Label4.Visible = false;
                            GridView2.Visible = true;
                            GridView2.DataSource = dschdt;
                            GridView2.DataBind();
                            txtaccptno.Text = "";
                            txtaccptno.Visible = false;
                            btnsubmit.Enabled = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Label3.Visible = true;
                    Label3.Text = ex.Message;
                }
                finally
                {
                    con_Maze.Close();
                }
            }
            # endregion

            # region maze_viewdetil
            else if (ddlmarksesn.SelectedValue.ToString() == "5")  // Maize
            {
                try
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;
                    // string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.QtyTransffer,IssueCenterReceipt_Online.Recv_Qty as Recd_Qty,IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.Recd_Bags,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N'";

                    string qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";

                    SqlDataAdapter da = new SqlDataAdapter(qrydata, con_Maze);
                    DataSet dschdt = new DataSet();
                    da.Fill(dschdt);
                    if (dschdt == null)
                    {


                    }
                    else
                    {
                        if (dschdt.Tables[0].Rows.Count == 0)
                        {
                            // string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";

                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";

                            SqlDataAdapter da1 = new SqlDataAdapter(getacdata, con_Maze);
                            DataSet dsac = new DataSet();
                            da1.Fill(dsac);
                            if (dsac == null)
                            {
                            }
                            else
                            {
                                if (dsac.Tables[0].Rows.Count == 0)
                                {
                                    Label4.Visible = true;
                                    Label4.Text = "Currently no record found";
                                    GridView2.Visible = false;

                                }
                                else
                                {
                                    GridView2.Visible = false;
                                    Label4.Visible = true;
                                    DataRow drac = dsac.Tables[0].Rows[0];
                                    string acno = drac["Acceptance_No"].ToString();
                                    Label4.Text = "Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date";

                                }


                            }



                        }
                        else
                        {
                            Label4.Visible = false;
                            GridView2.Visible = true;
                            GridView2.DataSource = dschdt;
                            GridView2.DataBind();
                            txtaccptno.Text = "";
                            txtaccptno.Visible = false;
                            btnsubmit.Enabled = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Label3.Visible = true;
                    Label3.Text = ex.Message;
                }
                finally
                {
                    con_Maze.Close();
                }
            }
            # endregion

            # region Bajra_viewdetil
            else if (ddlmarksesn.SelectedValue.ToString() == "6") // Bajra
            {
                try
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;
                    // string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.QtyTransffer,IssueCenterReceipt_Online.Recv_Qty as Recd_Qty,IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.Recd_Bags,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N'";

                    string qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";

                    SqlDataAdapter da = new SqlDataAdapter(qrydata, con_Maze);
                    DataSet dschdt = new DataSet();
                    da.Fill(dschdt);
                    if (dschdt == null)
                    {


                    }
                    else
                    {
                        if (dschdt.Tables[0].Rows.Count == 0)
                        {
                            // string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";

                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";

                            SqlDataAdapter da1 = new SqlDataAdapter(getacdata, con_Maze);
                            DataSet dsac = new DataSet();
                            da1.Fill(dsac);
                            if (dsac == null)
                            {
                            }
                            else
                            {
                                if (dsac.Tables[0].Rows.Count == 0)
                                {
                                    Label4.Visible = true;
                                    Label4.Text = "Currently no record found";
                                    GridView2.Visible = false;

                                }
                                else
                                {
                                    GridView2.Visible = false;
                                    Label4.Visible = true;
                                    DataRow drac = dsac.Tables[0].Rows[0];
                                    string acno = drac["Acceptance_No"].ToString();
                                    Label4.Text = "Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date";

                                }

                            }

                        }
                        else
                        {
                            Label4.Visible = false;
                            GridView2.Visible = true;
                            GridView2.DataSource = dschdt;
                            GridView2.DataBind();
                            txtaccptno.Text = "";
                            txtaccptno.Visible = false;
                            btnsubmit.Enabled = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Label3.Visible = true;
                    Label3.Text = ex.Message;
                }
                finally
                {
                    con_Maze.Close();
                }
            }
            # endregion

            # region Jau_viewdetil
            else if (ddlmarksesn.SelectedValue.ToString() == "7")  // Jau
            {
                try
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;
                    // string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.QtyTransffer,IssueCenterReceipt_Online.Recv_Qty as Recd_Qty,IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.Recd_Bags,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N'";

                    string qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";

                    SqlDataAdapter da = new SqlDataAdapter(qrydata, con_Maze);
                    DataSet dschdt = new DataSet();
                    da.Fill(dschdt);
                    if (dschdt == null)
                    {


                    }
                    else
                    {
                        if (dschdt.Tables[0].Rows.Count == 0)
                        {
                            // string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";

                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";

                            SqlDataAdapter da1 = new SqlDataAdapter(getacdata, con_Maze);
                            DataSet dsac = new DataSet();
                            da1.Fill(dsac);
                            if (dsac == null)
                            {
                            }
                            else
                            {
                                if (dsac.Tables[0].Rows.Count == 0)
                                {
                                    Label4.Visible = true;
                                    Label4.Text = "Currently no record found";
                                    GridView2.Visible = false;

                                }
                                else
                                {
                                    GridView2.Visible = false;
                                    Label4.Visible = true;
                                    DataRow drac = dsac.Tables[0].Rows[0];
                                    string acno = drac["Acceptance_No"].ToString();
                                    Label4.Text = "Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date";

                                }
                            }
                        }
                        else
                        {
                            Label4.Visible = false;
                            GridView2.Visible = true;
                            GridView2.DataSource = dschdt;
                            GridView2.DataBind();
                            txtaccptno.Text = "";
                            txtaccptno.Visible = false;
                            btnsubmit.Enabled = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Label3.Visible = true;
                    Label3.Text = ex.Message;
                }
                finally
                {
                    con_Maze.Close();
                }
            }
            # endregion
        }


    }
}
