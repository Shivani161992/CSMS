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

public partial class IssueCenter_Issue_RejectionNote : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

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
    public static string CSMS_Comid;
    int getnum;
    string issueid = "";
    string version = "";
    string Accpt_NO = "";

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
            hyprlnkprint.Attributes.Add("onclick", "window.open('Print_RejectNote.aspx',null,'left=100, top=10, height=570, width= 750, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

            txtAccDate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            txtAccDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtAccDate.Attributes.Add("onchange", "return chksqltxt(this)");

            
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
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                txtAccDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");


                gettype();
                GetDepot();

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


            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    void getcsms_Commdty()
    {
        mobj = new MoveChallan(ComObj);
        string qry = " SELECT Commodity_Id FROM Procurement_COMMODITY WHERE Proc_Commodity_Id='" + ddlmarksesn.SelectedValue.ToString() + "'";
        DataSet ds = mobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];
        CSMS_Comid = dr["Commodity_Id"].ToString();
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

    //protected void ddlpurchcenter_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    void GetGodown()
    {
        string pdate = getDate_MDY(DaintyDate3P.Text);

        //string ord = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlbranch.SelectedValue.ToString() + "'";
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

    void gettype()
    {
        try
        {
            if (con != null)
            {
                con.Open();
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

            con.Close();
        }
        finally
        {
            con.Close();
        }
    }

    void getsociety(string distid)
    {
        if (ddlmarksesn.SelectedValue.ToString() == "1")
        {
            try
            {
                if (con_WPMS != null)
                {
                    con_WPMS.Open();
                    // string qrysel = "select (Society.Society_Name+','+Society.SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + distid + "' and IsWheat='Y' order by Society_Name";
                    string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + distid + "' order by SocietyID";
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

                con_WPMS.Close();
            }
            finally
            {
                con_WPMS.Close();

            }
        }
        else if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
        {
            try
            {
                if (con_WPMS != null)
                {
                    con_WPMS.Open();
                    //string qrysel = "select (Society.Society_Name+','+Society.SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + distid + "' and Status='Y' order by Society_Name";
                    string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + distid + "' order by SocietyID";
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

                con_WPMS.Close();
            }
            finally
            {
                con_WPMS.Close();
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
                    con_WPMS.Open();
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

                con_WPMS.Close();
            }
            finally
            {
                con_WPMS.Close();

            }
        }
        else if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
        {
            try
            {
                if (con_WPMS != null)
                {
                    con_WPMS.Open();
                    string qrysel = "select District_Name,District_Code from Districts  order by District_Name";
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

                con_WPMS.Close();
            }
            finally
            {
                con_WPMS.Close();
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
                string qrysel = "select crop,crpcode from Crop_Master where crpcode in ('1')";
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
            }
        }

        catch (Exception)
        {

            con_WPMS.Close();
        }
        finally
        {
            con_WPMS.Close();
        }


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


    }

    protected void btnnw_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/IssueCenter/IssueACNo.aspx");

        bindgrid();
    }
    
    private void bindgrid()
    {
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
            if (ddlmarksesn.SelectedValue.ToString() == "1")
            {
                try
                {
                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;


                    //string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.IssueID,Crop_Master.crop as Commodity_Name  from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "'";

                    string qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and QtyTransffer <> Recv_Qty ";
                    
                    
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
                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";
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
                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;
                    string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.IssueID,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = 'Rejected'";
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
                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";
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


            # region maze_viewdetil
            else if (ddlmarksesn.SelectedValue.ToString() == "4" || ddlmarksesn.SelectedValue.ToString() == "5" || ddlmarksesn.SelectedValue.ToString() == "6" || ddlmarksesn.SelectedValue.ToString() == "7" || ddlmarksesn.SelectedValue.ToString() == "8")
            {
                try
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }
                    string pdate = getDate_MDY(DaintyDate3P.Text);
                    string mpc = ddlpurchcenterP.SelectedValue;
                    string qrydata = "select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.IssueID,Crop_Master.crop as Commodity_Name from IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId where IssueCenterReceipt_Online.DistrictId='23" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = 'Rejected'";
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
                            string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + distid + "' and IssueCenter_ID='" + issueid + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            SqlCommand cmdacno = new SqlCommand();
            // SqlTransaction tracn;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string AcceptDate = getDate_MDY(txtAccDate.Text);

            //tracn = con.BeginTransaction();
            //cmdacno.Transaction = tracn;
            cmdacno.Parameters.Clear();
            cmdacno.Parameters.AddWithValue("@District_ID", distid);
            cmdacno.Parameters.AddWithValue("@IssueCenter_ID", issueid);
            cmdacno.Connection = con;
            cmdacno.CommandType = CommandType.StoredProcedure;
            cmdacno.CommandText = "prc_getMaxAcceptanceNo";

            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

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
                                        string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,CommodityId,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + CSMS_Comid + "','','','','')";
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
            else if (ddltype.SelectedValue.ToString() == "01")
            {
                # region wheat_submit
                if (ddlmarksesn.SelectedValue.ToString() == "1")
                {
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
                                       CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                        //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                        //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                        //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");

                                        if (GchkBx.Checked == true)
                                        {
                                            getcsms_Commdty();

                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            //decimal R_Bag = 0;
                                            //decimal mqty = 0;
                                            //decimal rejcqty = 0;
                                            //string rvcqty = gr.Cells[6].Text;
                                            //decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            //decimal Totl = mqty + rejcqty;


                                         string rvcqtyFaq = gr.Cells[4].Text;
                                                    decimal rcv_QTYfaq = Convert.ToDecimal(rvcqtyFaq);

                                                    string rvcqtyurs = gr.Cells[5].Text;
                                                    decimal rcv_QTYurs = Convert.ToDecimal(rvcqtyurs);

                                                  //  string rvcqty = gr.Cells[3].Text;

                                                    decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;       // total of Faq and Urs is accepted qty (Anurag)


                                                    string jutenew = gr.Cells[6].Text;
                                                    decimal rcv_juteNew = Convert.ToDecimal(jutenew);

                                                    string recpp = gr.Cells[7].Text;
                                                    decimal rcv_ppbags = Convert.ToDecimal(recpp);

                                                    string juteold = gr.Cells[8].Text;
                                                    decimal rcv_juteold = Convert.ToDecimal(juteold);

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
                                          //string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','','','','')";
                                               
                                        string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','"+ddlgodown.SelectedValue+"')";
                                                           
                                               try
                                                {
                                                    cmd.Connection = con;
                                                    cmd.CommandText = inst;

                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();

                                                    string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '"+ddlgodown.SelectedValue+"'";
                                                    cmd.CommandText = updt;
                                                    //con.Open();
                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();


                                                    //procment
                                                    //string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','','','','')";

                                                   string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "')";


                                                    cmdP.Connection = con_WPMS;
                                                    cmdP.CommandText = inst1;
                                                    cmdP.ExecuteNonQuery();


                                                   string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '"+ddlgodown.SelectedValue+"'";
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
                                                    Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                    hyprlnkprint.Visible = true;
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

                                          

                                       }

                                        else
                                        {
                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
                                        }
                                    }
                                }

                            }


                        }

                        else
                        {
                            string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "'";
                            mobj = new MoveChallan(ComObj);
                            DataSet dspacno = mobj.selectAny(qryacno);

                            if (dspacno.Tables[0].Rows.Count == 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                            }
                            else
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
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
                                            CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            //TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            //TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                            if (GchkBx.Checked == true)
                                            {
                                                getcsms_Commdty();
                                                string iss_ID = gr.Cells[1].Text;
                                                string challan = gr.Cells[2].Text;
                                                string truckno = gr.Cells[3].Text;
                                                decimal R_Bag = 0;
                                                decimal mqty = 0;
                                                decimal rejcqty = 0;
                                                //string rvcqty = gr.Cells[6].Text;
                                                //decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                                //decimal Totl = mqty + rejcqty;
                                                
                                                SqlTransaction trns;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                                cmd.Transaction = trns;
                                                SqlTransaction trns2;
                                                con_WPMS.Open();
                                                trns2 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                                cmdP.Transaction = trns2;
                                                string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','','','','')";
                                                try
                                                {
                                                    cmd.Connection = con;
                                                    cmd.CommandText = inst;

                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();

                                                    string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Purchase_Center ='" + mpc + "'  ";
                                                    cmd.CommandText = updt;
                                                    //con.Open();
                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();


                                                    //procment
                                                    string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','','','','')";

                                                    cmdP.Connection = con_WPMS;
                                                    cmdP.CommandText = inst1;
                                                    cmdP.ExecuteNonQuery();


                                                    string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and IssueID = '" + iss_ID + "' and IssueCenter_ID ='" + issueid + "' ";
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
                                                    Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                    hyprlnkprint.Visible = true;
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


                                            }
                                            //--
                                            else
                                            {
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick the Checkbox...'); </script> ");
                                            }
                                        }
                                    }

                                }


                            }
                        }
                    }
                }
                # endregion

                # region Paddy_submit
                else if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
                {
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
                                        CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                        TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                        TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                        TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                        if (GchkBx.Checked == true)
                                        {
                                            txtrcq.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
                                            getcsms_Commdty();
                                            string iss_ID = gr.Cells[1].Text;
                                            string challan = gr.Cells[2].Text;
                                            string truckno = gr.Cells[3].Text;
                                            decimal R_Bag = CheckNull(txtrcBags.Text);
                                            decimal mqty = CheckNull(txtrcq.Text);
                                            decimal rejcqty = CheckNull(txtrjctqt.Text);
                                            string rvcqty = gr.Cells[6].Text;
                                            decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            decimal Totl = mqty + rejcqty;
                                            if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                            {
                                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                                return;
                                            }
                                            SqlTransaction trns;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;

                                            if (con_paddy.State == ConnectionState.Closed)
                                            {

                                                con_paddy.Open();
                                            }
                                            trns2 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','','','','')";
                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;

                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();
                                                cmd.ExecuteNonQuery();
                                                // con.Close();


                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','','','','')";

                                                cmdP.Connection = con_paddy;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

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
                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;
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



                                        }
                                        //----
                                        else
                                        {
                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick The Checkbox...'); </script> ");
                                        }
                                        //----
                                    }
                                }

                            }


                        }

                        else
                        {
                            string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "'";
                            mobj = new MoveChallan(ComObj);
                            DataSet dspacno = mobj.selectAny(qryacno);

                            if (dspacno.Tables[0].Rows.Count == 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ACNO already Exists...'); </script> ");
                            }
                            else
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
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
                                            CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            TextBox txtrcq = (TextBox)gr.FindControl("txtrcq");
                                            TextBox txtrjctqt = (TextBox)gr.FindControl("txtrjctqt");
                                            TextBox txtrcBags = (TextBox)gr.FindControl("txtrcBags");
                                            if (GchkBx.Checked == true)
                                            {
                                                getcsms_Commdty();
                                                string iss_ID = gr.Cells[1].Text;
                                                string challan = gr.Cells[2].Text;
                                                string truckno = gr.Cells[3].Text;
                                                decimal R_Bag = CheckNull(txtrcBags.Text);
                                                decimal mqty = CheckNull(txtrcq.Text);
                                                decimal rejcqty = CheckNull(txtrjctqt.Text);
                                                string rvcqty = gr.Cells[6].Text;
                                                decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                                decimal Totl = mqty + rejcqty;
                                                if (rejcqty >= rcv_QTYck || Totl > rcv_QTYck)
                                                {
                                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Reject_Qty it should not be Recd_Qty....'); </script> ");
                                                    return;
                                                }
                                                SqlTransaction trns;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                                cmd.Transaction = trns;
                                                SqlTransaction trns2;
                                                con_WPMS.Open();
                                                trns2 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                                cmdP.Transaction = trns2;
                                                string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','','','','')";
                                                try
                                                {
                                                    cmd.Connection = con;
                                                    cmd.CommandText = inst;

                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();

                                                    string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                                    cmd.CommandText = updt;
                                                    //con.Open();
                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();


                                                    //procment
                                                    string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','','','','')";

                                                    cmdP.Connection = con_WPMS;
                                                    cmdP.CommandText = inst1;
                                                    cmdP.ExecuteNonQuery();


                                                    string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
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
                                                    Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                    hyprlnkprint.Visible = true;
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
                # region Maze_submit
                else if (ddlmarksesn.SelectedValue.ToString() == "4" || ddlmarksesn.SelectedValue.ToString() == "5" || ddlmarksesn.SelectedValue.ToString() == "6" || ddlmarksesn.SelectedValue.ToString() == "7" || ddlmarksesn.SelectedValue.ToString() == "8")
                {
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
                                        CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                        
                                        if (GchkBx.Checked == true)
                                        {
                                            getcsms_Commdty();
                                            string iss_ID = gr.Cells[1].Text;
                                            string challan = gr.Cells[2].Text;
                                            string truckno = gr.Cells[3].Text;
                                            decimal R_Bag = 0;
                                            decimal mqty = 0;
                                            decimal rejcqty = 0;
                                            //string rvcqty = gr.Cells[6].Text;
                                            //decimal rcv_QTYck = Convert.ToDecimal(rvcqty);
                                            //decimal Totl = mqty + rejcqty;
                                            
                                            SqlTransaction trns;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;
                                            con_Maze.Open();
                                            trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmdP.Transaction = trns2;
                                            string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "','" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','','','','')";
                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;

                                                cmd.ExecuteNonQuery();
                                                // con.Close();

                                                string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                                cmd.CommandText = updt;
                                                //con.Open();
                                                cmd.ExecuteNonQuery();
                                                // con.Close();


                                                //procment
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "','" + rejcqty + "','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','','','','')";

                                                cmdP.Connection = con_Maze;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();


                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
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
                                                Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                hyprlnkprint.Visible = true;
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

                        else
                        {
                            string qryacno = "Select Acceptance_No from Acceptance_Note_Detail  where  Distt_ID='" + distid + "'and IssueCenter_ID='" + issueid + "'and Acceptance_No='" + cacno + "' and Purchase_Center='" + ddlpurchcenterP.SelectedValue + "'";
                            mobj = new MoveChallan(ComObj);
                            DataSet dspacno = mobj.selectAny(qryacno);

                            if (dspacno.Tables[0].Rows.Count == 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('RJNO already Exists...'); </script> ");
                            }
                            else
                            {
                                if (ddlpurchcenterP.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name...'); </script> ");
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
                                            CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
                                            //TextBox txtrcq = "";
                                            //TextBox txtrjctqt = "";
                                            //TextBox txtrcBags = "";

                                            if (GchkBx.Checked == true)
                                            {
                                                getcsms_Commdty();
                                                string iss_ID = gr.Cells[1].Text;
                                                string challan = gr.Cells[2].Text;
                                                string truckno = gr.Cells[3].Text;
                                                decimal R_Bag = 0;
                                                decimal mqty = 0;
                                                decimal rejcqty = 0;
                                                                                               
                                                SqlTransaction trns;
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }

                                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                                cmd.Transaction = trns;
                                                SqlTransaction trns2;
                                                con_Maze.Open();
                                                trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                                cmdP.Transaction = trns2;
                                                string inst = "insert into dbo.Acceptance_Note_Detail(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('" + state + "','" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + mqty + "','" + rejcqty + "' ,'" + CSMS_Comid + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + R_Bag + "','','','','')";
                                                try
                                                {
                                                    cmd.Connection = con;
                                                    cmd.CommandText = inst;

                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();

                                                    string updt = "Update dbo.SCSC_Procurement set AN_Status='Y',Acceptance_No='" + acptno + "' where Distt_ID='" + distid + "' and IssueCenter_ID='" + issueid + "' and TC_Number='" + challan + "'";
                                                    cmd.CommandText = updt;
                                                    //con.Open();
                                                    cmd.ExecuteNonQuery();
                                                    // con.Close();


                                                    //procment
                                                    string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad)values('23" + distid + "','" + issueid + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "',getdate()," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + mqty + "' ,'" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + R_Bag + "','','','','')";

                                                    cmdP.Connection = con_Maze;
                                                    cmdP.CommandText = inst1;
                                                    cmdP.ExecuteNonQuery();


                                                    string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + distid + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "'";
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
                                                    Session["Commodity_ID"] = ddlmarksesn.SelectedValue.ToString();
                                                    hyprlnkprint.Visible = true;
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
            }

        }
        catch (Exception ex)
        {
            Label3.Visible = true;
            Label3.Text = ex.Message;
        }
    }
}
