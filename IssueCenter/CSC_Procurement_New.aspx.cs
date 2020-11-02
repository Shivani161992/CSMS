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
using System.Text.RegularExpressions;

public partial class IssueCenter_CSC_Procurement : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());
    // By A public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    // By A public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2016"].ToString());

    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    DistributionCenters distobj = null;
    Commodity_MP comdtobj = null;
    Transporter tobj = null;
    MoveChallan mobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj2 = null;
    Districts DObj = null;
    Scheme_MP schobj = null;
    protected Common ComObj = null, cmn = null;
    public string time;
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string getdatef = "";
    public string gatepass = "";
    public string version = "";
    public string distp = "";
    public static string CSMS_Comid;
    chksql chk = null;
    public Int64 getnum;

    DataTable Dt1 = new DataTable();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();
            version = Session["hindi"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtissueqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtissueqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtissueqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtissubag.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txt_recJutNew.Attributes.Add("onkeypress", "return IsNumericProcQtyBag(event,this)");
            txt_recJutNew.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_recJutNew.Attributes.Add("onchange", "return chksqltxt(this)");

            txtfaq_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtfaq_qty.Attributes.Add("onchange", "return ChkNotgrate(this);");
            txtfaq_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtfaq_qty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtUrs_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtUrs_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtUrs_qty.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_recPP.Attributes.Add("onkeypress", "return IsNumericProcQtyBag(event,this)");
            txt_recPP.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_recPP.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_recJutOld.Attributes.Add("onkeypress", "return IsNumericProcQtyBag(event,this)");
            txt_recJutOld.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_recJutOld.Attributes.Add("onchange", "return chksqltxt(this)");

            txtmoisture.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtmoisture.Attributes.Add("onchange", "return chksqltxt(this)");
            txtmoisture.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
           
            txtchlnno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtchlnno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtchlnno.Attributes.Add("onchange", "return chksqltxt(this)");

            //txttrucknopady.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");     /// commented because wrong number dispatch by Society and this check stop the entry.
            //txttrucknopady.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            //txttrucknopady.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1P.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1P.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1P.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

            txtchlnno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttrucknopady.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtissueqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txt_recJutNew.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtissubag.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           // txtbookno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txt_recJutNew.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txt_recPP.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txt_recJutOld.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            txtbadStiching.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            txtBadStelcile.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            txtmoisture.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtTaulNum.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            txtfaq_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtUrs_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txt_bags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();


            chk = new chksql();
            HyperLink1.Attributes.Add("onclick", "window.open('Print_Gatepass_Procurement.aspx',null,'left=50, top=10, height=570, width= 690, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            distobj = new DistributionCenters(ComObj);
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtchlnno.Text);
            //ctrllist.Add(txttrucknopady.Text);
            ctrllist.Add(txtissueqty.Text);
            ctrllist.Add(txtissubag.Text);
            ctrllist.Add(txt_recJutNew.Text);
            ctrllist.Add(DaintyDate1P.Text);
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
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया सेक्टर की खरीद केंद्र से मेपिंग ,जिला कार्यालय द्वारा अनिवार्य रूप से करा ले  !'); </script> ");

                txtisdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate1P.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                
                GetCommodity();
                Getdepo();
                getbranch();
                GetName();
               
                if (version == "H")
                {
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                    lblIssuedBags.Text = Resources.LocalizedText.lblIssuedBags;
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblRecFromDist.Text = Resources.LocalizedText.lblRecFromDist;
                    lblpcname.Text = Resources.LocalizedText.lblpcname;
                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lbltotalReceivedBags.Text = Resources.LocalizedText.lbltotalReceivedBags;
                    lblTotalQuantityReceived.Text = Resources.LocalizedText.lblTotalQuantityReceived;
                    lblMaxCap.Text = Resources.LocalizedText.lblCapacity;
                    lblChallanNumber.Text = Resources.LocalizedText.lblChallanNumber;
                    lblTrans.Text = Resources.LocalizedText.lblTrans;
                    lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                    lblReceiptDate.Text = Resources.LocalizedText.lblReceiptDate;
                    lblRecepDetail.Text = Resources.LocalizedText.lblRecepDetail;
                    lblDateOfChallan.Text = Resources.LocalizedText.lblDateOfChallan;
                    lblCurStackCap.Text = Resources.LocalizedText.lblCurStackCap;
                    lblAvailable.Text = Resources.LocalizedText.lblAvailable;
                    lblCropYear.Text = Resources.LocalizedText.lblCropYear;
                    lbldispprocure.Text = Resources.LocalizedText.lbldispprocure;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    //btnsave.Text = Resources.LocalizedText.btnsave;
                    lblNameDepot.Text = Resources.LocalizedText.lblNameDepot;
                    lblDistrictName.Text = Resources.LocalizedText.lblDistrictName;
                    lblKgs.Text = Resources.LocalizedText.lblKgs;
                    btnaddnew.Text = Resources.LocalizedText.btnaddnew;
                    lblissdat.Text = Resources.LocalizedText.lblissdat;
                    lblissid.Text = Resources.LocalizedText.lblissid;
                    btnsavePaddy.Text = Resources.LocalizedText.btnsavePaddy;
                    lblhirty.Text = Resources.LocalizedText.lblhirty;
                    lbldpo.Text = Resources.LocalizedText.lbldpo;
                }

                string monthh = DateTime.Now.Month.ToString();

                int myear = int.Parse(DateTime.Now.Year.ToString());
                
                ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());

                //ddlcropyear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                //ddlcropyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                //ddlcropyear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));

                //ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);
                                     
                //ddlcropyear.Items.Add("Crop Year");
               
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void Getdepo()
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }
                
                string qrysel = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='23" + did + "'";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "BranchId";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }

    }

    private void Getgon()
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchId='" + ddlbranchwlc.SelectedValue.ToString() + "' and Remarks = 'Y'";
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

                    else
                    {
                        ddlgodown.DataSource = "";
                        ddlgodown.DataBind();
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }

    }
  
    void GetGodown()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + sid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");

    }
    void GetCommodity()
    {

        try
        {
            if (con_paddy != null)
            {

                ////comdtobj = new Commodity_MP(ComObj);
                ////string qry = "Select * from tbl_MetaData_Storage_COmmodity where Commodity_Id IN ('24','22') order by Commodity_Name Desc";
                //con_paddy.Open();
                //string qrysel = "select cropsno,crop from cropmast";
                //SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                //da.Fill(ds);
                //ddlcomdty.DataSource = ds.Tables[0];
                //ddlcomdty.DataTextField = "cropsno";
                //ddlcomdty.DataValueField = "crop";
                //ddlcomdty.DataBind();
                //ddlcomdty.Items.Insert(0, "--Select--");

                if (con_WPMS.State == ConnectionState.Closed)
                {

                    con_WPMS.Open();   /// con_paddy karna hai
                }

                if (Session["Markfed"].ToString() == "Y")
                {
                    string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('1','4','5','6','7','8')";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);   /// con_paddy karna hai 
                    /// 
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlcomdty.DataSource = ds.Tables[0];
                            ddlcomdty.DataTextField = "crop";
                            ddlcomdty.DataValueField = "crpcode";
                            ddlcomdty.DataBind();
                            ddlcomdty.Items.Insert(0, "--Select--");

                        }
                    }
                }

                else
                {
                    string qrysel = "select crop,crpcode from Crop_Master where crpcode in ('1')";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);   /// con_paddy karna hai 
                    /// 
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlcomdty.DataSource = ds.Tables[0];
                            ddlcomdty.DataTextField = "crop";
                            ddlcomdty.DataValueField = "crpcode";
                            ddlcomdty.DataBind();
                            ddlcomdty.Items.Insert(0, "--Select--");

                        }
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

                con_WPMS.Close();   /// con_paddy karna hai
            }
        }
        finally
        {
            if (con_WPMS.State == ConnectionState.Open)
            {

                con_WPMS.Close();   /// con_paddy karna hai
            }
        }
    }
 
    void GetName()
    {
        mobj2 = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + did + "'";
        DataSet ds1dt = mobj2.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdist.Text = dr1dt["district_name"].ToString();


        mobj2 = new MoveChallan(ComObj);
        string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + sid + "'";
        DataSet dsic = mobj2.selectAny(qryissue);
        DataRow dric = dsic.Tables[0].Rows[0];
        txtissue.Text = dric["DepotName"].ToString();

        //ddldistrict.SelectedItem.Text = dr1dt["district_name"].ToString();
        //ddldistrict.SelectedValue = did;

    }

    //void GetICName()
    //{

    //    distobj = new DistributionCenters(ComObj);
    //    string ord = "Districtid='23" + ddldistpdy.SelectedValue + "' order by DepotName";
    //    DataSet ds = distobj.select(ord);

    //    ddlissuecenter.DataSource = ds.Tables[0];
    //    ddlissuecenter.DataTextField = "DepotName";
    //    ddlissuecenter.DataValueField = "DepotId";

    //    ddlissuecenter.DataBind();
    //    ddlissuecenter.Items.Insert(0, "--Select--");

    //    // ddDistId.Items.Insert(0, "--चुनिये--");
    //}

    void getpadyDist()
    {
        try
        {
            if (con_paddy != null)
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }
                string qrysel = "select District_Name,District_Code from Districts  order by District_Name";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        
                        ddldistpdy.DataSource = ds.Tables[0];
                        ddldistpdy.DataTextField = "District_Name";
                        ddldistpdy.DataValueField = "District_Code";
                        ddldistpdy.DataBind();
                        ddldistpdy.Items.Insert(0, "--चुनें--");
                        ddldistpdy.ClearSelection();
                        ddldistpdy.Items.FindByValue(23 + did).Selected = true;

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

    void getDistCorcgrn()
    {
        try
        {
            if (con_Maze != null)
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }
                string qrysel = "select District_Name,District_Code from Districts  order by District_Name";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddldistpdy.DataSource = ds.Tables[0];
                        ddldistpdy.DataTextField = "District_Name";
                        ddldistpdy.DataValueField = "District_Code";
                        ddldistpdy.DataBind();
                        ddldistpdy.Items.Insert(0, "--चुनें--");
                        ddldistpdy.ClearSelection();
                        ddldistpdy.Items.FindByValue(23 + did).Selected = true;
                                              
                        Label1.Text = "";

                    }

                    else
                    {
                        Label1.Text = "Maize District Not Found";
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {
            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }
        finally
        {
            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }
    }

    void getDistWht()
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

                        ddldistpdy.DataSource = ds.Tables[0];
                        ddldistpdy.DataTextField = "District_Name";
                        ddldistpdy.DataValueField = "District_Code";
                        ddldistpdy.DataBind();
                        ddldistpdy.Items.Insert(0, "--चुनें--");
                        ddldistpdy.ClearSelection();
                        ddldistpdy.Items.FindByValue(23 + did).Selected = true;

                        /// This district is bind because of new changes , need to display receiving godown district, as problem arise in Budhni and its godown is in Itarsi
                        /// changes on 22/05/2014 , anurag

                        //ddlRec_DistGodown.DataSource = ds.Tables[0];
                        //ddlRec_DistGodown.DataTextField = "District_Name";
                        //ddlRec_DistGodown.DataValueField = "District_Code";
                        //ddlRec_DistGodown.DataBind();

                        //ddlRec_DistGodown.ClearSelection();
                        //ddlRec_DistGodown.Items.FindByValue(23 + did).Selected = true;
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
   
    void getpadyUparjncntr()
    {
        try
        {
            if (con_paddy != null)
            {
              con_paddy.Open();
             
                //string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and Status='Y' order by Society_Name";
               // string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' order by ic.SocietyID";
                    string qrysel = "select   ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID) as varchar(50)) + ')') as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistpdy.SelectedValue.ToString() + "'  and ic.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online)  and ic.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  group by  ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID order by ic.SocietyID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddluparjan.DataSource = ds.Tables[0];
                        ddluparjan.DataTextField = "Society_Name";
                        ddluparjan.DataValueField = "Society_Id";
                        ddluparjan.DataBind();
                        ddluparjan.Items.Insert(0, "--Select--");

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

    void getcorgrnUparjncntr()
    {
        try
        {
            if (con_Maze != null)
            {
                con_Maze.Open();
                //string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and Status='Y' order by Society_Name";
                string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' order by ic.SocietyID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddluparjan.DataSource = ds.Tables[0];
                        ddluparjan.DataTextField = "Society_Name";
                        ddluparjan.DataValueField = "Society_Id";
                        ddluparjan.DataBind();
                        ddluparjan.Items.Insert(0, "--Select--");

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

    void getWhtUparjncntr()
    {
        try
        {
            if (con_WPMS != null)
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }
                // string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IsWheat='Y' order by Society_Name";
                string qrysel = "select   ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID) as varchar(50)) + ')') as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistpdy.SelectedValue.ToString() + "'  and ic.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online)  and ic.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  group by  ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID order by ic.SocietyID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddluparjan.DataSource = ds.Tables[0];
                        ddluparjan.DataTextField = "Society_Name";
                        ddluparjan.DataValueField = "Society_Id";
                        ddluparjan.DataBind();
                        ddluparjan.Items.Insert(0, "--Select--");
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

    void getpaddyTranspoter()
    {
        if (ddlcomdty.SelectedValue.ToString() == "1")
        {
            try
            {
                if (con_WPMS != null)
                {
                    con_WPMS.Open();
                    string qrysel = "select Transporter_ID,Transporter_Name from TransportMaster where District_ID='" + ddldistpdy.SelectedValue.ToString() + "' and SocietyCode='" + ddluparjan.SelectedValue.ToString() + "'";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlpdyTransporter.DataSource = ds.Tables[0];
                            ddlpdyTransporter.DataTextField = "Transporter_Name";
                            ddlpdyTransporter.DataValueField = "Transporter_ID";
                            ddlpdyTransporter.DataBind();
                            ddlpdyTransporter.Items.Insert(0, "--Select--");

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

        else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
        {
            try
            {
                if (con_paddy != null)
                {
                    con_paddy.Open();
                    string qrysel = "select Transporter_ID,Transporter_Name from TransportMaster where District_ID='" + ddldistpdy.SelectedValue.ToString() + "' and SocietyCode='" + ddluparjan.SelectedValue.ToString() + "'";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlpdyTransporter.DataSource = ds.Tables[0];
                            ddlpdyTransporter.DataTextField = "Transporter_Name";
                            ddlpdyTransporter.DataValueField = "Transporter_ID";
                            ddlpdyTransporter.DataBind();
                            ddlpdyTransporter.Items.Insert(0, "--चुनें--");

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
        else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
        {
            try
            {
                if (con_Maze != null)
                {
                    con_Maze.Open();
                    string qrysel = "select Transporter_ID,Transporter_Name from TransportMaster where District_ID='" + ddldistpdy.SelectedValue.ToString() + "' and SocietyCode='" + ddluparjan.SelectedValue.ToString() + "'";
                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlpdyTransporter.DataSource = ds.Tables[0];
                            ddlpdyTransporter.DataTextField = "Transporter_Name";
                            ddlpdyTransporter.DataValueField = "Transporter_ID";
                            ddlpdyTransporter.DataBind();
                            ddlpdyTransporter.Items.Insert(0, "--चुनें--");

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

    void getpaddyIssueid()
    {
        distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);

        # region Wheat
        if (ddlcomdty.SelectedValue.ToString() == "1")
        {
            try
            {
                if (con_WPMS != null)
                {
                    con_WPMS.Open();

                    //if (distp == did)
                    //{
                        // DateTime mydt = Convert.ToDateTime(getDate_MDY(txtisdate.Text));
                    string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,isnull(tm.Transporter_Name,'')Transporter_Name,ist.TransporterId , ist.GodownTypeId  from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.DistrictId = tm.District_ID and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    //}
                    //else
                    //{
                    //    // DateTime mydt = Convert.ToDateTime(getDate_MDY(txtisdate.Text));
                    //    string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and  SendingDistId ='23" + did + "' order by DateOfIssue desc ";
                    //    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                    //    DataSet ds = new DataSet();
                    //    da.Fill(ds);
                    //    if (ds != null)
                    //    {
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //        {

                    //            dgridchallan.DataSource = ds.Tables[0];
                    //            dgridchallan.DataBind();
                    //            lber.Visible = false;
                    //            lber.Text = "";
                    //        }
                    //        else
                    //        {
                    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                    //            dlissuClear();
                    //            lber.Visible = true;
                    //            pnlrcvdtl.Visible = false;
                    //            lber.Text = "Data not available for this Purchase Center";
                    //        }
                    //    }

                    //}
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

        # endregion

        # region Paddy

        else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
        {
            try
            {
                if (con_paddy != null)
                {
                    //if (distp == did)
                    //{
                        //string qrysel = "select (IssueToSangrahanaKendra.IssueID+'/'+IssueToSangrahanaKendra.TruckChalanNo)as Name,IssueToSangrahanaKendra.IssueID from IssueToSangrahanaKendra where IssueToSangrahanaKendra.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IssueToSangrahanaKendra.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and convert(varchar,DateOfIssue,101)='" + getDate_MDY(txtisdate.Text) + "' and IssueTo not in('OD') ";
                       // string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and IssueTo not in('OD') order by DateOfIssue desc ";
                    string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId , ist.GodownTypeId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    //}
                    //else
                    //{
                    //    // string qrysel = "select (IssueToSangrahanaKendra.IssueID+'/'+IssueToSangrahanaKendra.TruckChalanNo)as Name,IssueToSangrahanaKendra.IssueID from IssueToSangrahanaKendra where IssueToSangrahanaKendra.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IssueToSangrahanaKendra.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and convert(varchar,DateOfIssue,101)='" + getDate_MDY(txtisdate.Text) + "' and SendingDistId ='23" + did + "'";
                    //    string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and SendingDistId ='23" + did + "' order by DateOfIssue desc";
                    //    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                    //    DataSet ds = new DataSet();
                    //    da.Fill(ds);
                    //    if (ds != null)
                    //    {
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //        {

                    //            dgridchallan.DataSource = ds.Tables[0];
                    //            dgridchallan.DataBind();
                    //            lber.Visible = false;
                    //            lber.Text = "";
                    //        }
                    //        else
                    //        {
                    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                    //            dlissuClear();
                    //            lber.Visible = true;
                    //            pnlrcvdtl.Visible = false;
                    //            lber.Text = "Data not available for this Purchase Center";
                    //        }
                    //    }
                    //}
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

        # endregion

        # region Coarse

        else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
        {
            try
            {
                if (con_Maze != null)
                {
                    con_Maze.Open();
                    //if (distp == did)
                    //{
                    //   // string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and IssueTo not in('OD') order by DateOfIssue desc ";
                    string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId , ist.GodownTypeId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    //}
                    //else
                    //{
                    //    string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and SendingDistId ='23" + did + "' order by DateOfIssue desc";
                    //    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                    //    DataSet ds = new DataSet();
                    //    da.Fill(ds);
                    //    if (ds != null)
                    //    {
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //        {

                    //            dgridchallan.DataSource = ds.Tables[0];
                    //            dgridchallan.DataBind();
                    //            lber.Visible = false;
                    //            lber.Text = "";
                    //        }
                    //        else
                    //        {
                    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                    //            dlissuClear();
                    //            lber.Visible = true;
                    //            pnlrcvdtl.Visible = false;
                    //            lber.Text = "Data not available for this Purchase Center";
                    //        }
                    //    }
                    //}

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

        # endregion

    }

    protected void ddluparjan_SelectedIndexChanged(object sender, EventArgs e)
     {
        if (ddluparjan.SelectedIndex == 0)
        {
            txtissueId.Text = "";
            ddlpdyTransporter.Items.Clear();
            ddlpdyTransporter.DataSource = null;
            ddlpdyTransporter.DataBind();
            dgridchallan.DataSource = null;
            dgridchallan.DataBind();
            pnlgrd.Visible = false;
            dlissuClear();
        }
        else
        {
            dlissuClear();
            pnlgrd.Visible = true;
           
            getcsms_Commdty();

            //if(did == "51")
            //{
            //    getpaddyIssueid_Agarmalwa();

            //}

            //else
            //{
            //    getpaddyIssueid();
            //}

            getpaddyIssueid();
            
            getpaddyTranspoter();

            lblsector.Visible = true;

            ddlsector.Visible = true;

            string getsector = "Select SectorCode from Sectorto_PC_Mapping where PCCode = '" + ddluparjan.SelectedValue + "' and cropyear = '"+ddlcropyear.SelectedItem.Text+"'";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmdsector = new SqlCommand(getsector,con);

            SqlDataAdapter dasec = new SqlDataAdapter(cmdsector);

            DataSet dssector = new DataSet();

            dasec.Fill(dssector);

            if (dssector.Tables[0].Rows.Count > 0)
            {
                string sector = dssector.Tables[0].Rows[0]["SectorCode"].ToString();

                string getsectorName = "Select SectorId, SectorName from District_SectorMaster where SectorId = '" + sector + "'";

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmdsectorname = new SqlCommand(getsectorName, con);

                SqlDataAdapter dasecname = new SqlDataAdapter(cmdsectorname);

                DataSet dssectorname = new DataSet();

                dasecname.Fill(dssectorname);

                if (dssectorname.Tables[0].Rows.Count > 0)
                {
                    ddlsector.DataSource = dssectorname.Tables[0];

                    ddlsector.DataTextField = "SectorName";
                    ddlsector.DataValueField = "SectorId";
                    ddlsector.DataBind();
                   
                    ddlsector.Enabled = false;
                }

                else
                {
                    ddlsector.DataSource = "";
                   
                    ddlsector.DataBind();
                    ddlsector.Items.Insert(0, "--Select--");
                }

            }

            else
            {

            }
        }
    }

    protected void ddldistpdy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistpdy.SelectedIndex == 0)
        {
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();
            dgridchallan.DataSource = null;
            dgridchallan.DataBind();
            pnlgrd.Visible = false;
            pnlrcvdtl.Visible = false;
        }
        else
        {
            pnlrcvdtl.Visible = false;
            dgridchallan.DataSource = null;
            dgridchallan.DataBind();
            pnlgrd.Visible = false;
            ddlpdyTransporter.Items.Clear();
            ddlpdyTransporter.DataSource = null;
            ddlpdyTransporter.DataBind();
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();
            txtchlnno.Text = "";
            txttrucknopady.Text = "";
            DaintyDate1P.Text = "";
            txtissubag.Text = "";
            txtissueqty.Text = "";
            txtissueId.Text = "";
            if (ddlcomdty.SelectedValue.ToString() == "1")
            {

                getWhtUparjncntr();
            }
            else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {

                getpadyUparjncntr();

            }
            else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
            {

                getcorgrnUparjncntr();
            }
            else
            {
            }
        }
    }
    //protected void ddlissueid_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlcomdty.SelectedValue.ToString() == "1")
    //    {
    //        try
    //        {
    //            if (con_WPMS != null)
    //            {
    //                con_WPMS.Open();
    //                string qrysel = " Select *,tm.Transporter_Name from IssueToSangrahanaKendra left join cropmast on cropmast.cropsno = IssueToSangrahanaKendra.CommodityId left join TransportMaster tm on tm.Transporter_ID=IssueToSangrahanaKendra.TransporterId where IssueID='" + ddlissueid.SelectedValue.ToString() + "' and SocietyID='" + ddluparjan.SelectedValue.ToString() + "'";
    //                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
    //                DataSet ds = new DataSet();
    //                da.Fill(ds);
    //                if (ds != null)
    //                {
    //                    //txtcrptyp.Enabled = false;
    //                    //txtcrptyp.Text = ds.Tables[0].Rows[0]["crop"].ToString();
    //                    txtchlnno.Enabled = false;
    //                    txtchlnno.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
    //                    txttrucknopady.Enabled = false;
    //                    txttrucknopady.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
    //                    ddlpdyTransporter.Enabled = false;
    //                    ddlpdyTransporter.SelectedItem.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
    //                    ddlpdyTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
    //                    //ddltransporter.DataValueField = transporterid.ToString();

    //                    DaintyDate1P.Enabled = false;
    //                    string pdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
    //                    DaintyDate1P.Text = getdate(pdate);
    //                    txtissubag.Enabled = false;
    //                    txtissubag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
    //                    txtissueqty.Enabled = false;
    //                    txtissueqty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

    //                }

    //            }
    //            else
    //            {
    //            }

    //        }

    //        catch (Exception)
    //        {

    //            con_WPMS.Close();
    //        }
    //        finally
    //        {
    //            con_WPMS.Close();
    //        }
    //    }

    //    else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
    //    {
    //        try
    //        {
    //            if (con_paddy != null)
    //            {
    //                con_paddy.Open();
    //                string qrysel = "Select *,tm.Transporter_Name from IssueToSangrahanaKendra left join Crop_Master on Crop_Master.crpcode = IssueToSangrahanaKendra.CommodityId left join TransportMaster tm on tm.Transporter_ID=IssueToSangrahanaKendra.TransporterId where IssueID='" + ddlissueid.SelectedValue.ToString() + "' and SocietyID='" + ddluparjan.SelectedValue.ToString() + "'";
    //                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
    //                DataSet ds = new DataSet();
    //                da.Fill(ds);
    //                if (ds != null)
    //                {
    //                    //txtcrptyp.Enabled = false;
    //                    //txtcrptyp.Text = ds.Tables[0].Rows[0]["cropName"].ToString();
    //                    txtchlnno.Enabled = false;
    //                    txtchlnno.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
    //                    txttrucknopady.Enabled = false;
    //                    txttrucknopady.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
    //                    ddlpdyTransporter.Enabled = false;
    //                    ddlpdyTransporter.SelectedItem.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
    //                    ddlpdyTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
    //                    //ddltransporter.DataValueField = transporterid.ToString();

    //                    DaintyDate1P.Enabled = false;
    //                    string pdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
    //                    DaintyDate1P.Text = getdate(pdate);
    //                    txtissubag.Enabled = false;
    //                    txtissubag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
    //                    txtissueqty.Enabled = false;
    //                    txtissueqty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

    //                }

    //            }
    //            else
    //            {
    //            }

    //        }

    //        catch (Exception)
    //        {

    //            con_paddy.Close();
    //        }
    //        finally
    //        {
    //            con_paddy.Close();
    //        }
    //    }
    //    else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
    //    {
    //        try
    //        {
    //            if (con_Maze != null)
    //            {
    //                con_Maze.Open();
    //                string qrysel = "Select *,tm.Transporter_Name from IssueToSangrahanaKendra left join Crop_Master on Crop_Master.crpcode = IssueToSangrahanaKendra.CommodityId left join TransportMaster tm on tm.Transporter_ID=IssueToSangrahanaKendra.TransporterId where IssueID='" + ddlissueid.SelectedValue.ToString() + "' and SocietyID='" + ddluparjan.SelectedValue.ToString() + "'";
    //                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
    //                DataSet ds = new DataSet();
    //                da.Fill(ds);
    //                if (ds != null)
    //                {
    //                    //txtcrptyp.Enabled = false;
    //                    //txtcrptyp.Text = ds.Tables[0].Rows[0]["cropName"].ToString();
    //                    txtchlnno.Enabled = false;
    //                    txtchlnno.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
    //                    txttrucknopady.Enabled = false;
    //                    txttrucknopady.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
    //                    ddlpdyTransporter.Enabled = false;
    //                    ddlpdyTransporter.SelectedItem.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
    //                    ddlpdyTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
    //                    //ddltransporter.DataValueField = transporterid.ToString();

    //                    DaintyDate1P.Enabled = false;
    //                    string pdate = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
    //                    DaintyDate1P.Text = getdate(pdate);
    //                    txtissubag.Enabled = false;
    //                    txtissubag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
    //                    txtissueqty.Enabled = false;
    //                    txtissueqty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

    //                }

    //            }
    //            else
    //            {
    //            }

    //        }

    //        catch (Exception)
    //        {

    //            con_Maze.Close();
    //        }
    //        finally
    //        {
    //            con_Maze.Close();
    //        }
    //    }


    //}

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //void GetBalance()
    //{
    //    string mcomid = CSMS_Comid;
    //    //string mcatid = ddlcategory.SelectedValue;
    //    string godownid = ddlgodown.SelectedValue;
    //    string brnch = ddlbranchwlc.SelectedValue;
    //    int month = int.Parse(DateTime.Today.Month.ToString());
    //    int year = int.Parse(DateTime.Today.Year.ToString());
    //    string source = "01";
    //    mobj1 = new MoveChallan(ComObj);


    //    string recgdndist = ddlRec_DistGodown.SelectedValue.ToString().Substring(2, 2);


    //    string qry = "Select Sum(Current_Balance)as Current_Balance from dbo.issue_opening_balance where District_Id='" + recgdndist + "'and Depotid='" + ddlbranchwlc.SelectedValue + "'and Commodity_Id='" + mcomid + "'and Source='" + source + "'and Godown='" + godownid + "' and Branch_Id='" + brnch + "' ";
    //    DataSet ds = mobj1.selectAny(qry);

    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
    //        txtbalqty.Text = "0";
    //        lblbalanceqty.Visible = true;
    //        txtbalqty.Visible = true;
    //        txtbalqty.BackColor = System.Drawing.Color.Wheat;
    //        txtbalqty.ReadOnly = true;
    //    }
    //    else
    //    {
    //        DataRow dr = ds.Tables[0].Rows[0];
    //        txtbalqty.Text = dr["Current_Balance"].ToString();
    //        lblbalanceqty.Visible = true;
    //        txtbalqty.Visible = true;
    //        txtbalqty.BackColor = System.Drawing.Color.Wheat;
    //        txtbalqty.ReadOnly = true;
    //    }
    //}

    void UpdateCBalance()
    {
        getcsms_Commdty();
        string opid = Session["OperatorId"].ToString();
        string comdtyid = CSMS_Comid;
        string godown = ddlgodown.SelectedValue;
        string brnch = ddlbranchwlc.SelectedValue;
        string source = "01";
        string schemeid = ddlscheme.SelectedValue;
        int recdbags = CheckNullInt(txt_recJutNew.Text);
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        int openbag = 0;
        decimal openqty = 0;
        string udate = "";
        string ddate = "";
        string mstate = "23";
        string pdate = getDate_MDY("01/04/2011");
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string chkopen = "Select Quantity from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Scheme_Id='" + schemeid + "' and Godown='" + godown + "' and Source='01' and Branch_Id='" + brnch + "'";
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
                string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate,OperatorID,Branch_Id) values('" + mstate + "','" + did + "','" + sid + "','" + comdtyid + "','" + schemeid + "','','" + mggn + "','" + ddlcropyear.SelectedValue + "'," + openbag + "," + openqty + ",'01'," + CheckNull(txtfaq_qty.Text) + "," + recdbags + "," + month + "," + year + ",'" + ip + "',getdate(),getdate(),'" + udate + "','" + ddate + "','" + opid + "','" + brnch + "')";
                cmd.CommandText = qreyins;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Label9.Visible = true;
                    Label9.Text = "error:2" + ex.Message;

                }
                finally
                {
                    con.Close();

                }
            }
            else
            {
                string query = "Update dbo.issue_opening_balance set Current_Balance = convert(decimal(18,5), Current_Balance)+" + CheckNull(txtfaq_qty.Text) + ",Current_Bags=Current_Bags+" + recdbags + " where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Godown='" + godown + "' and Scheme_Id='" + schemeid + "' and Source='01' and Branch_Id='" + brnch + "' ";
                cmd.CommandText = query;
                cmd.Connection = con;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Label9.Visible = true;
                    Label9.Text = "error:3" + ex.Message;
                }
                finally
                {
                    con.Close();

                }
            }
        }
    }
    void Update_Trans_Log(string GPASS)
    {

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

       // DateTime my1 = Convert.ToDateTime(getDate_MDY(DaintyDate1.Text));
        //DateTime my3 = Convert.ToDateTime(DaintyDate3.Text);
        int my3month = int.Parse(DateTime.Today.Month.ToString());
        int my3year = int.Parse(DateTime.Today.Year.ToString());
        string mdispdate1 = getDate_MDY(DaintyDate1P.Text);
        string mrecddate1 = getDate_MDY(DaintyDate3.Text);
                              
        string qryinsert = "insert into dbo.SCSC_Procurement_Trans_Log(Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Updates_Date,IP_Address,Operation,Branch_Id)values('" + did + "','" + sid + "','" + distp + "','" + ddluparjan.SelectedValue + "','" + mdispdate1 + "','" + txtchlnno.Text + "','" + txttrucknopady.Text + "','" + ddlpdyTransporter.SelectedValue + "','" + CSMS_Comid + "','" + ddlcropyear.SelectedValue + "'," + CheckNullInt(txtissubag.Text) + "," + CheckNull(txtissueqty.Text) + "," + CheckNullInt(txt_recJutNew.Text) + "," + CheckNull(txtfaq_qty.Text) + ",'" + mrecddate1 + "','" + ddlgodown.SelectedValue + "','" + GPASS + "'," + my3month + "," + my3year + ",getdate(),'" + ip + "','I','" + ddlbranchwlc.SelectedValue + "')";

        cmd.CommandText = qryinsert;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            Label9.Visible = true;
            Label9.Text = "error:1" + ex.Message;
        }
        finally
        {
            con.Close();

        }
    }
   
    protected void btnsavePaddy_Click(object sender, EventArgs e)
    {
        try
        {
            string PurchaseCenter_Name = ddluparjan.SelectedItem.Text;

            if (Convert.ToDecimal(txtqty.Text) > 0 || Convert.ToDecimal(txt_bags.Text) > 0)
            {
                
                decimal chk_RquintyPr = CheckNull(txtissueqty.Text) * 40;
                decimal chk_Rquinty = chk_RquintyPr / 100;
               
                decimal mqty1 = CheckNull(txtissueqty.Text) + chk_Rquinty;
                decimal mrecdqty1 = CheckNull(txtqty.Text);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // check duplicate entries, anurag -- 22-04-2014 // adding issue id in filter on 20-05-2014

                string tcnum = txtchlnno.Text;
                string trucknumber = txttrucknopady.Text;
                decimal recdqty = CheckNull(txtfaq_qty.Text);
                string recdate = getDate_MDY(DaintyDate3.Text);
                string issueid = txtissueId.Text;

                string CheckduplicateRec = "Select * from SCSC_Procurement2016 where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + tcnum + "' and Truck_Number = '" + trucknumber + "' and Recd_Date = '" + recdate + "' and Recd_Qty = '" + recdqty + "' and Receipt_Id = '"+issueid+"' ";

                SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                SqlDataReader drduplicate;

                drduplicate = cmdduplirec.ExecuteReader();

                if (drduplicate.Read())
                {
                    return;
                }
                else
                {
               
                drduplicate.Close();

                string csmsTransporter;

                string mrecdgdn_check = ddlgodown.SelectedValue;    // For check Selected Silo Godown or Not

                if (mrecdgdn_check == "0" || mrecdgdn_check == "--select--")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('गोदाम का नाम चुने |'); </script> ");
                    return;
                }


                string checksilo = "select Godown_ID from tbl_MetaData_GODOWN where Storage_Type like 's%' and DistrictId='23" + Session["dist_id"].ToString() + "' and Godown_ID = '"+mrecdgdn_check+"'";

                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }

                SqlCommand cmdsilo = new SqlCommand(checksilo, cons);

                SqlDataReader drsilo;
                drsilo = cmdsilo.ExecuteReader();

                if (drsilo.Read())
                {
                    drsilo.Close();

                    csmsTransporter = ddlcsms_transp.SelectedValue;
                    if (csmsTransporter == "0" || csmsTransporter == "--Select--")
                    {
                        csmsTransporter = "";
                    }

                    if (lblgdntype.Text == "&nbsp;" || lblgdntype.Text == "")
                    {
                        lblgdntype.Text = "0";
                    }
                }

                else
                {
                    drsilo.Close();

                    if (ddlcsms_transp.SelectedValue == "0" || ddlcsms_transp.SelectedValue == "--Select--")
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('CSMS सॉफ्टवेयर का वास्तविक परिवहनकर्ता का नाम चुने |'); </script> ");
                        return;
                        btnsavePaddy.Enabled = true;
                    }

                    csmsTransporter = ddlcsms_transp.SelectedValue;

                    if (lblgdntype.Text == "&nbsp;" || lblgdntype.Text == "")
                    {
                        lblgdntype.Text = "0";
                    }
                }


                //if (lblgdntype.Text == "12" || lblgdntype.Text == "9" || lblgdntype.Text == "" || lblgdntype.Text == "&nbsp;" || lblgdntype.Text == "1")
                //{
                //    csmsTransporter = ddlcsms_transp.SelectedValue;
                //    if (csmsTransporter == "0" || csmsTransporter == "--Select--")
                //    {
                //        csmsTransporter = "";
                //    }

                //    if (lblgdntype.Text == "&nbsp;" || lblgdntype.Text == "")
                //    {
                //        lblgdntype.Text = "0";
                //    }
                    
                //}

                //else
                //{
                    //if (ddlcsms_transp.SelectedValue == "0" || ddlcsms_transp.SelectedValue == "--Select--")
                    //{
                    //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('CSMS सॉफ्टवेयर का वास्तविक परिवहनकर्ता का नाम चुने |'); </script> ");
                    //    return;
                    //    btnsavePaddy.Enabled = true;
                    //}

                    //csmsTransporter = ddlcsms_transp.SelectedValue;

                    //if (lblgdntype.Text == "&nbsp;" || lblgdntype.Text == "")
                    //{
                    //    lblgdntype.Text = "0";
                    //}
                //}
                
                    # region wheat
                    if (ddlcomdty.SelectedValue.ToString() == "1")
                    {

                        //decimal ccap = CheckNull(txtcurntcap.Text);
                        //decimal rcap = CheckNull(txtfaq_qty.Text);
                        //decimal chkcap = ccap + rcap;
                        //decimal maxcap = CheckNull(txtmaxcap.Text);

                        //if (chkcap > maxcap)
                        //{
                        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sorry Space is not available at Godown....'); </script> ");
                        //}
                        //else
                        //{
                        SqlDataAdapter daP;
                        DataSet dsP = new DataSet();

                        //string select = "Select TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + ddlcomdty.SelectedValue.ToString() + "' and YEAR=Year(GETDATE())";
                        string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement2016 where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' ";
                        cmd.CommandText = select;

                        cmd.Connection = con;


                        daP = new SqlDataAdapter(cmd);

                        daP.Fill(dsP);

                        if (dsP.Tables[0].Rows.Count == 0)
                        {

                            //for (int i = 0; GridView2.Rows.Count > i; i++)  // Multiple Godown Insert.
                            //{

                            if (con_WPMS.State == ConnectionState.Closed)
                            {
                                con_WPMS.Open();
                            }
                            SqlTransaction trns1;
                            cmd1.Connection = con_WPMS;
                            trns1 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                            cmd1.Transaction = trns1;

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            SqlTransaction trns;
                            cmd.Connection = con;
                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                            cmd.Transaction = trns;

                            gatepass = txtissueId.Text.Trim().ToString();
                            distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);
                            string mpcdist = distp;
                            string mpcic = ddluparjan.SelectedValue;
                            string mdispdate = getDate_MDY(DaintyDate1P.Text);
                            string mchallan = txtchlnno.Text;
                            string mtruckno_first = txttrucknopady.Text;
                            string mtrans = ddlpdyTransporter.SelectedValue;
                            getcsms_Commdty();
                            string mcomdty = CSMS_Comid;
                            string mcropy = ddlcropyear.SelectedItem.ToString();
                            int mbags = CheckNullInt(txtissubag.Text);
                            decimal mqty = CheckNull(txtissueqty.Text);

                            Regex re = new Regex("[;\\/:*?\"<>|&']");
                            string mtruckno = re.Replace(mtruckno_first, " ");
                            //string macno = txtaccptno.Text;
                            //string macdate = getDate_MDY(DaintyDate2.Text);
                            string mstatus = "N";

                            string mudate = "";
                            string mddate = "";
                            string mfyear = DateTime.Today.Year.ToString();
                            string mbookno = txtbookno.Text;
                            // string accpno = mfyear + mbookno + txtaccptno.Text;
                            int month = int.Parse(DateTime.Today.Month.ToString());
                            int year = int.Parse(DateTime.Today.Year.ToString());
                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string state = Session["State_Id"].ToString();
                            string anst = "N";
                            
                            decimal mrecdqty = CheckNull(txtqty.Text);     // Calculated Qunaity receiving

                            int mrecdbags = CheckNullInt(txt_bags.Text.Trim());

                            int mrecdbagsJute = CheckNullInt(txt_recJutNew.Text);
                            int mrecdbagsPP = CheckNullInt(txt_recPP.Text);
                            int mrecdbagsJuteOld = CheckNullInt(txt_recJutOld.Text);

                            mrecdbags = mrecdbagsJute + mrecdbagsPP + mrecdbagsJuteOld;

                            int badStiching = CheckNullInt(txtbadStiching.Text);

                            int BadStelcile = CheckNullInt(txtBadStelcile.Text);

                            decimal mrecdqtyFaq = CheckNull(txtfaq_qty.Text);
                            decimal mrecdqtyUrs = CheckNull(txtUrs_qty.Text);

                            mrecdqty = mrecdqtyFaq + mrecdqtyUrs;

                            decimal moisture = CheckNull(txtmoisture.Text.Trim());

                            string Taulparchi = txtTaulNum.Text.Trim();
                            
                            string recdGodownName = ddlgodown.SelectedItem.Text;
                            string Category = ddlcategory.SelectedItem.Value;
                            // string mrecdgdn = GridView2.Rows[i].Cells[0].Text.ToString();

                            string mrecddate = getDate_MDY(DaintyDate3.Text);
                            string mrecdgdn = ddlgodown.SelectedValue;

                            string branch = ddlbranchwlc.SelectedValue;
                            string opid = Session["OperatorId"].ToString();
                            string notrans = "N";

                            DateTime dispdate = Convert.ToDateTime(DaintyDate1P.Text);

                            // DateTime Recdate = Convert.ToDateTime((DaintyDate3.Text).ToString("MM/dd/yyyy"));
                            DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate3.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

                            string todaydate = DateTime.Now.ToString("dd/MM/yyyy");

                            DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

                            int result = DateTime.Compare(Recdate, dispdate);

                            //if (result == -1)
                            //{
                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not Less than Dispatch Date...'); </script> ");
                            //    return;
                            //}

                            int greaterdate = DateTime.Compare(currentdate, Recdate);

                            if (greaterdate == -1)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not greater than.To Day Date'); </script> ");
                                return;
                            }

                            if (ddlcomdty.SelectedItem.Text == "--Select--" || ddluparjan.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--select--")
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Purchase Center /Transporter Name/Godown....'); </script> ");
                                return;
                            }

                            //else
                            // {
                            //     string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement where Receipt_Id = '" + gatepass + "'and IssueCenter_ID = '" + sid + "'";
                            //     //cmd1 = new SqlCommand(checkpre, con_paddy);
                            //      cmd.CommandText = checkrcid;
                            //      //cmd.Connection = con;

                            //   string str1 = cmd.ExecuteScalar().ToString();

                            //if (Convert.ToInt16(str1) == 0)   // not Found, Insert start,(commented on 03-03-15 Anurag , because multiple entry required for multiple gdn.)
                            //   {

                            try
                            {
                                if (mtrans == "0" || mtrans == "--Select--")
                                {
                                    mtrans = "";
                                }

                                string qryinsert = "insert into dbo.SCSC_Procurement2016(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber,RecdQty_Faq ,RecdQty_Urs ,RecdBags_JuteNew ,RecdBags_PP,RecdBags_JuteOld,Stiching_bags ,Stencile_bags,Moisture,TaulParchi,GodownName,category,Transp_Pancard,GodownTypeId)values('" + state + "','" + did + "','" + sid + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'',getdate(),'" + mbookno + "'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + "," + badStiching + "," + BadStelcile + "," + moisture + ",'" + Taulparchi + "',N'" + recdGodownName + "','" + Category + "','" + csmsTransporter + "'," + lblgdntype.Text + ")";
                                cmd.CommandText = qryinsert;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();

                                }
                                //txtissueId.Text.Trim().ToString();
                                string issuid = txtissueId.Text.Trim().ToString();
                                string socity = ddluparjan.SelectedValue.ToString();
                                string str = " Select * from IssueToSangrahanaKendra where IssueID='" + issuid + "' and SocietyID='" + socity + "'";
                                cmd1.CommandText = str;

                                SqlDataAdapter daP1 = new SqlDataAdapter(cmd1);
                                DataSet dsP1 = new DataSet();
                                daP1.Fill(dsP1);
                                string Issuid = dsP1.Tables[0].Rows[0]["IssueID"].ToString();
                                string disid = dsP1.Tables[0].Rows[0]["DistrictId"].ToString();
                                string Socid = dsP1.Tables[0].Rows[0]["SocietyID"].ToString();
                                string Crpyr = dsP1.Tables[0].Rows[0]["CropYear"].ToString();
                                string mrktson = dsP1.Tables[0].Rows[0]["MarketingSeasonId"].ToString();
                                string issuedt2 = dsP1.Tables[0].Rows[0]["DateOfIssue"].ToString();

                                string comdty = dsP1.Tables[0].Rows[0]["CommodityId"].ToString();
                                string bags = dsP1.Tables[0].Rows[0]["Bags"].ToString();
                                string qty = dsP1.Tables[0].Rows[0]["QtyTransffer"].ToString();
                                string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();

                                string taulnumber = "0";   // added on 22/07 to remove wrong taulpatrak number

                                int SlashPos = taulptrk.IndexOf("'");

                                if (SlashPos > 0)
                                {
                                    taulnumber = taulptrk.Substring(0, taulptrk.IndexOf("'"));
                                }

                                else
                                {
                                    taulnumber = taulptrk;
                                }

                                string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();

                                string udate = "";
                                string status = "N";
                                // string recqty = txtrecqty.Text;
                                //float recqty = CheckNulFloat(txtrecqty.Text.ToString().Trim());
                                // string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "'";
                                //cmd1 = new SqlCommand(checkpre, con_paddy);
                                // cmd1.CommandText = checkpre;
                                //cmd1.Connection = con_WPMS;
                                //  string str12 = cmd1.ExecuteScalar().ToString();

                                //if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                //{
                                string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld,GodownTypeId)  VALUES('" + Issuid + "','23" + did + "','" + sid + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulnumber + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + ","+lblgdntype.Text+")";

                                cmd1.CommandText = inserttotest;

                                if (con_WPMS.State == ConnectionState.Closed)
                                {
                                    con_WPMS.Open();
                                }
                                cmd1.Connection = con_WPMS;

                                //cmd1.CommandTimeout = 4600;
                                int x = cmd1.ExecuteNonQuery();

                                //}
                                //paddy end
                                                                
                                int count = cmd.ExecuteNonQuery();

                                if (count >= 1)
                                {
                                    trns1.Commit();
                                    
                                    if (con_WPMS.State == ConnectionState.Open)
                                    {
                                        con_WPMS.Close();
                                    }

                                    trns.Commit();
                                    
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();

                                    }

                                    if (cons.State == ConnectionState.Open)
                                    {
                                        cons.Close();
                                    }

                                    //UpdateStock();
                                    //UpdateCBalance();

                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                btnsavePaddy.Enabled = false;

                                Session["dt1"] = null;

                                Update_Trans_Log(gatepass);
                                Session["Receipt_ID"] = gatepass;
                                Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();
                                HyperLink1.Visible = true;
                                
                                //txtissubag.Text = "";
                                //txtissueqty.Text = "";
                                // txtaccptno.Text = "";
                                txtbookno.Text = "";
                                txtchlnno.Text = "";
                                txttrucknopady.Text = "";
                                ddldistpdy.Focus();

                                // Response.Write("<script Language=javascript>alert('सफलतापूर्वक प्राप्ति हो चुकी हें..... ');self.location='IssueCenter_PaddyReceiptTest.aspx';</script>");
                            }
                            catch (Exception ex)
                            {

                                trns1.Rollback();
                                Label9.Visible = true;
                                Label9.Text = "error:6" + ex.Message;

                            }
                            finally
                            {
                                if (con_WPMS.State == ConnectionState.Open)
                                {
                                    con_WPMS.Close();
                                }

                                //trns.Commit();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();

                                }

                                if (cons.State == ConnectionState.Open)
                                {
                                    cons.Close();
                                }
                            }

                            //}
                            //else
                            //{
                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again....'); </script> ");
                            //}
                            // }

                            //}

                            //drdup.Close();
                            //}
                        }

                        else
                        {
                            string tcc = dsP.Tables[0].Rows[0]["TC_Number"].ToString();
                            string pdat = dsP.Tables[0].Rows[0]["Recd_Date"].ToString();
                            string pdat1 = getdate(pdat);
                            string Tr_Nu = dsP.Tables[0].Rows[0]["Truck_Number"].ToString();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist...." + tcc + "..,Truck No..." + Tr_Nu + "... Recd_Date..." + pdat1 + "'); </script> ");

                        }

                        //}
                    }

                    # endregion

                    # region paddy

                    else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
                    {
                        if (con_paddy.State == ConnectionState.Closed)
                        {
                            con_paddy.Open();
                        }
                        SqlTransaction trns1;
                        cmd1.Connection = con_paddy;
                        trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd1.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlTransaction trns;
                        cmd.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd.Transaction = trns;
                        
                        decimal ccap = CheckNull(txtcurntcap.Text);
                        decimal rcap = CheckNull(txtfaq_qty.Text);
                        decimal chkcap = ccap + rcap;
                        decimal maxcap = CheckNull(txtmaxcap.Text);

                        //if (chkcap > maxcap)
                        //{
                        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sorry Space is not available at Godown....'); </script> ");
                        //}
                        //else
                        //{


                        SqlDataAdapter daP;
                        DataSet dsP = new DataSet();
                        getcsms_Commdty();
                        string cmtid = CSMS_Comid;
                        //string select = "Select TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "' and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + cmtid + "' and YEAR=Year(GETDATE())";
                        string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' ";
                        cmd.CommandText = select;
                        daP = new SqlDataAdapter(cmd);

                        daP.Fill(dsP);
                        
                        if (dsP.Tables[0].Rows.Count == 0)
                        {

                            gatepass = txtissueId.Text.Trim().ToString();
                            distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);
                            string mpcdist = distp;
                            string mpcic = ddluparjan.SelectedValue;
                            string mdispdate = getDate_MDY(DaintyDate1P.Text);
                            string mchallan = txtchlnno.Text;
                            string mtruckno = txttrucknopady.Text;
                            string mtrans = ddlpdyTransporter.SelectedValue;

                            getcsms_Commdty();
                            string mcomdty = CSMS_Comid;
                            string mcropy = ddlcropyear.SelectedItem.ToString();
                            int mbags = CheckNullInt(txtissubag.Text);
                            decimal mqty = CheckNull(txtissueqty.Text);
                            //  string macno = txtaccptno.Text;
                            //string macdate = getDate_MDY(DaintyDate2.Text);
                            string mstatus = "N";

                            string mudate = "";
                            string mddate = "";
                            string mfyear = DateTime.Today.Year.ToString();
                            string mbookno = txtbookno.Text;
                            // string accpno = mfyear + mbookno + txtaccptno.Text;
                            int month = int.Parse(DateTime.Today.Month.ToString());
                            int year = int.Parse(DateTime.Today.Year.ToString());
                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string state = Session["State_Id"].ToString();
                            string anst = "N";
                            int mrecdbags = CheckNullInt(txt_bags.Text);   ////  Calculated Bags Quantity
                            decimal mrecdqty = CheckNull(txtqty.Text);     // Calculated Qunaity receiving

                            string mrecddate = getDate_MDY(DaintyDate3.Text);
                            string mrecdgdn = ddlgodown.SelectedValue;
                            string branch = ddlbranchwlc.SelectedValue;
                            string opid = Session["OperatorId"].ToString();
                            string notrans = "N";
                            if (ddlcomdty.SelectedItem.Text == "--Select--" || ddluparjan.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--select--")
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Purchase Center /Transporter Name/Godown....'); </script> ");
                            }
                            else
                            {
                                
                                string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement where Receipt_Id = '" + gatepass + "'and IssueCenter_ID = '" + sid + "'";

                                cmd.CommandText = checkrcid;
                                //cmd.Connection = con;
                                string str1 = cmd.ExecuteScalar().ToString();

                                if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                {

                                    try
                                    {
                                        int mrecdbagsJute = CheckNullInt(txt_recJutNew.Text);
                                        int mrecdbagsPP = CheckNullInt(txt_recPP.Text);
                                        int mrecdbagsJuteOld = CheckNullInt(txt_recJutOld.Text);

                                        mrecdbags = mrecdbagsJute + mrecdbagsPP + mrecdbagsJuteOld;

                                        int badStiching = CheckNullInt(txtbadStiching.Text);

                                        int BadStelcile = CheckNullInt(txtBadStelcile.Text);

                                        decimal mrecdqtyFaq = CheckNull(txtfaq_qty.Text);
                                        decimal mrecdqtyUrs = CheckNull(txtUrs_qty.Text);

                                        mrecdqty = mrecdqtyFaq + mrecdqtyUrs;

                                        txt_bags.Text = Convert.ToString(mrecdbags);
                                        txtqty.Text = Convert.ToString(mrecdqty);

                                        decimal moisture = CheckNull(txtmoisture.Text.Trim());

                                        string Taulparchi = txtTaulNum.Text.Trim();
                                        
                                        string recdGodownName = ddlgodown.SelectedItem.Text;
                                        string Category = ddlcategory.SelectedItem.Value;

                                        string qryinsert = "insert into dbo.SCSC_Procurement(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber,RecdQty_Faq ,RecdQty_Urs ,RecdBags_JuteNew ,RecdBags_PP,RecdBags_JuteOld,Stiching_bags ,Stencile_bags,Moisture,TaulParchi,GodownName,category,GodownTypeId)values('" + state + "','" + did + "','" + sid + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'',getdate(),'" + mbookno + "'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + "," + badStiching + "," + BadStelcile + "," + moisture + ",'" + Taulparchi + "',N'" + recdGodownName + "','" + Category + "',"+lblgdntype.Text+")";
                                        cmd.CommandText = qryinsert;
                                        
                                        string issuid = txtissueId.Text.Trim().ToString();
                                        string socity = ddluparjan.SelectedValue.ToString();
                                        string str = " Select * from IssueToSangrahanaKendra where IssueID='" + issuid + "' and SocietyID='" + socity + "'";
                                        cmd1.CommandText = str;
                                        // cmd1.Connection = con_WPMS;
                                        SqlDataAdapter daP1 = new SqlDataAdapter(cmd1);
                                        DataSet dsP1 = new DataSet();
                                        daP1.Fill(dsP1);
                                        string Issuid = dsP1.Tables[0].Rows[0]["IssueID"].ToString();
                                        string disid = dsP1.Tables[0].Rows[0]["DistrictId"].ToString();
                                        string Socid = dsP1.Tables[0].Rows[0]["SocietyID"].ToString();
                                        string Crpyr = dsP1.Tables[0].Rows[0]["CropYear"].ToString();
                                        string mrktson = dsP1.Tables[0].Rows[0]["MarketingSeasonId"].ToString();
                                        string issuedt2 = dsP1.Tables[0].Rows[0]["DateOfIssue"].ToString();

                                        string comdty = dsP1.Tables[0].Rows[0]["CommodityId"].ToString();
                                        string bags = dsP1.Tables[0].Rows[0]["Bags"].ToString();
                                        string qty = dsP1.Tables[0].Rows[0]["QtyTransffer"].ToString();
                                        string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();
                                        string taulnumber = "0";   // added on 22/07 to remove wrong taulpatrak number

                                        int SlashPos = taulptrk.IndexOf("'");

                                        if (SlashPos > 0)
                                        {
                                            taulnumber = taulptrk.Substring(0, taulptrk.IndexOf("'"));
                                        }

                                        else
                                        {
                                            taulnumber = taulptrk;
                                        }

                                        string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                        string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                        string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();

                                        // string udate = dsP.Tables[0].Rows[0]["UpdatedDate"].ToString();
                                        string udate = "";
                                        string status = "N";
                                        // string recqty = txtrecqty.Text;
                                        //float recqty = CheckNulFloat(txtrecqty.Text.ToString().Trim());
                                        string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "'";
                                        //cmd1 = new SqlCommand(checkpre, con_paddy);
                                        cmd1.CommandText = checkpre;
                                        //cmd1.Connection = con_WPMS;
                                        string str12 = cmd1.ExecuteScalar().ToString();

                                        if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                        {
                                            string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld,GodownTypeId)  VALUES('" + Issuid + "','23" + did + "','" + sid + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulnumber + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + ","+lblgdntype.Text+")";

                                            cmd1.CommandText = inserttotest;
                                            //cmd1.Connection = con_WPMS;
                                            //cmd1.CommandTimeout = 4600;
                                            int x = cmd1.ExecuteNonQuery();

                                        }
                                        //paddy end
                                        
                                        int count = cmd.ExecuteNonQuery();

                                        if (count >= 1)
                                        {
                                            trns1.Commit();

                                            trns.Commit();

                                            //UpdateStock();
                                            //UpdateCBalance();

                                            if (con_paddy.State == ConnectionState.Open)
                                            {
                                                con_paddy.Close();
                                            }

                                            if (con.State == ConnectionState.Open)
                                            {
                                                con.Close();
                                            }

                                        }

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                        btnsavePaddy.Enabled = false;
                                        // Update_Trans_Log(gatepass);
                                        Session["Receipt_ID"] = gatepass;
                                        Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();
                                        HyperLink1.Visible = true;


                                        txtissubag.Text = "";
                                        txtissueqty.Text = "";
                                        //  txtaccptno.Text = "";
                                        txtbookno.Text = "";
                                        txtchlnno.Text = "";
                                        txttrucknopady.Text = "";
                                        ddldistpdy.Focus();

                                        // Response.Write("<script Language=javascript>alert('सफलतापूर्वक प्राप्ति हो चुकी हें..... ');self.location='IssueCenter_PaddyReceiptTest.aspx';</script>");
                                    }
                                    catch (Exception ex)
                                    {
                                        // trns1.Rollback();
                                        Label9.Visible = true;
                                        Label9.Text = "error:6" + ex.Message;

                                    }
                                    finally
                                    {
                                        if (con_paddy.State == ConnectionState.Open)
                                        {
                                            con_paddy.Close();
                                        }

                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }
                                   }

                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again....'); </script> ");
                                }

                            }
                        }

                        else
                        {
                            string tcc = dsP.Tables[0].Rows[0]["TC_Number"].ToString();
                            string pdat = dsP.Tables[0].Rows[0]["Recd_Date"].ToString();
                            string pdat1 = getdate(pdat);
                            string Tr_Nu = dsP.Tables[0].Rows[0]["Truck_Number"].ToString();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist...." + tcc + "..,Truck No..." + Tr_Nu + "... Recd_Date..." + pdat1 + "'); </script> ");

                        }

                        //}
                    }

                    # endregion

                    # region Maize
                    else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
                    {
                        if (con_Maze.State == ConnectionState.Closed)
                        {
                            con_Maze.Open();
                        }
                        SqlTransaction trns1;
                        cmd1.Connection = con_Maze;
                        trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd1.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlTransaction trns;
                        cmd.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd.Transaction = trns;
                        
                        decimal ccap = CheckNull(txtcurntcap.Text);
                        decimal rcap = CheckNull(txtfaq_qty.Text);
                        decimal chkcap = ccap + rcap;
                        decimal maxcap = CheckNull(txtmaxcap.Text);

                        //if (chkcap > maxcap)
                        //{
                        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sorry Space is not available at Godown....'); </script> ");
                        //}
                        //else
                        //{
                        SqlDataAdapter daP;
                        DataSet dsP = new DataSet();

                        //string select = "Select TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "' and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + ddlcomdty.SelectedValue.ToString() + "' and YEAR=Year(GETDATE())";
                        string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' ";
                        cmd.CommandText = select;
                        daP = new SqlDataAdapter(cmd);

                        daP.Fill(dsP);

                        if (dsP.Tables[0].Rows.Count == 0)
                        {

                            gatepass = txtissueId.Text.Trim().ToString();
                            distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);
                            string mpcdist = distp;
                            string mpcic = ddluparjan.SelectedValue;
                            string mdispdate = getDate_MDY(DaintyDate1P.Text);
                            string mchallan = txtchlnno.Text;
                            string mtruckno = txttrucknopady.Text;
                            string mtrans = ddlpdyTransporter.SelectedValue;

                            getcsms_Commdty();
                            string mcomdty = CSMS_Comid;
                            string mcropy = ddlcropyear.SelectedItem.ToString();
                            int mbags = CheckNullInt(txtissubag.Text);
                            decimal mqty = CheckNull(txtissueqty.Text);
                            // string macno = txtaccptno.Text;

                            string mstatus = "N";

                            string mudate = "";
                            string mddate = "";
                            string mfyear = DateTime.Today.Year.ToString();
                            string mbookno = txtbookno.Text;
                            //  string accpno = mfyear + mbookno + txtaccptno.Text;
                            int month = int.Parse(DateTime.Today.Month.ToString());
                            int year = int.Parse(DateTime.Today.Year.ToString());
                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string state = Session["State_Id"].ToString();
                            string anst = "N";
                            int mrecdbags = CheckNullInt(txt_recJutNew.Text);
                            decimal mrecdqty = CheckNull(txtfaq_qty.Text);
                            string mrecddate = getDate_MDY(DaintyDate3.Text);
                            string mrecdgdn = ddlgodown.SelectedValue;
                            string branch = ddlbranchwlc.SelectedValue;
                            string opid = Session["OperatorId"].ToString();
                            string notrans = "N";

                            if (ddlcomdty.SelectedItem.Text == "--Select--" || ddluparjan.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--select--")
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Purchase Center /Transporter Name/Godown....'); </script> ");
                            }
                            else
                            {

                                string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement where Receipt_Id = '" + gatepass + "'and IssueCenter_ID = '" + sid + "'";

                                cmd.CommandText = checkrcid;

                                string str1 = cmd.ExecuteScalar().ToString();

                                if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                {

                                    try
                                    {
                                        int mrecdbagsJute = CheckNullInt(txt_recJutNew.Text);
                                        int mrecdbagsPP = CheckNullInt(txt_recPP.Text);
                                        int mrecdbagsJuteOld = CheckNullInt(txt_recJutOld.Text);

                                        mrecdbags = mrecdbagsJute + mrecdbagsPP + mrecdbagsJuteOld;

                                        int badStiching = CheckNullInt(txtbadStiching.Text);

                                        int BadStelcile = CheckNullInt(txtBadStelcile.Text);

                                        decimal mrecdqtyFaq = CheckNull(txtfaq_qty.Text);
                                        decimal mrecdqtyUrs = CheckNull(txtUrs_qty.Text);

                                        mrecdqty = mrecdqtyFaq + mrecdqtyUrs;

                                        decimal moisture = CheckNull(txtmoisture.Text.Trim());

                                        string Taulparchi = txtTaulNum.Text.Trim();
                                        
                                        string recdGodownName = ddlgodown.SelectedItem.Text;
                                        string Category = ddlcategory.SelectedItem.Value;

                                        string qryinsert = "insert into dbo.SCSC_Procurement(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber,RecdQty_Faq ,RecdQty_Urs ,RecdBags_JuteNew ,RecdBags_PP,RecdBags_JuteOld,Stiching_bags ,Stencile_bags,Moisture,TaulParchi,GodownName,category,GodownTypeId)values('" + state + "','" + did + "','" + sid + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'',getdate(),'" + mbookno + "'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + "," + badStiching + "," + BadStelcile + "," + moisture + ",'" + Taulparchi + "',N'" + recdGodownName + "','" + Category + "',"+lblgdntype.Text+")";
                                        cmd.CommandText = qryinsert;


                                        string issuid = txtissueId.Text.Trim().ToString();
                                        string socity = ddluparjan.SelectedValue.ToString();
                                        string str = " Select * from IssueToSangrahanaKendra where IssueID='" + issuid + "' and SocietyID='" + socity + "'";
                                        cmd1.CommandText = str;

                                        SqlDataAdapter daP1 = new SqlDataAdapter(cmd1);
                                        DataSet dsP1 = new DataSet();
                                        daP1.Fill(dsP1);
                                        string Issuid = dsP1.Tables[0].Rows[0]["IssueID"].ToString();
                                        string disid = dsP1.Tables[0].Rows[0]["DistrictId"].ToString();
                                        string Socid = dsP1.Tables[0].Rows[0]["SocietyID"].ToString();
                                        string Crpyr = dsP1.Tables[0].Rows[0]["CropYear"].ToString();
                                        string mrktson = dsP1.Tables[0].Rows[0]["MarketingSeasonId"].ToString();
                                        string issuedt2 = dsP1.Tables[0].Rows[0]["DateOfIssue"].ToString();

                                        string comdty = dsP1.Tables[0].Rows[0]["CommodityId"].ToString();
                                        string bags = dsP1.Tables[0].Rows[0]["Bags"].ToString();
                                        string qty = dsP1.Tables[0].Rows[0]["QtyTransffer"].ToString();
                                        //string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();
                                        string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                        string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                        string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();
                                        string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();
                                        string taulnumber = "0";   // added on 22/07 to remove wrong taulpatrak number

                                        int SlashPos = taulptrk.IndexOf("'");

                                        if (SlashPos > 0)
                                        {
                                            taulnumber = taulptrk.Substring(0, taulptrk.IndexOf("'"));
                                        }

                                        else
                                        {
                                            taulnumber = taulptrk;
                                        }

                                        string udate = "";
                                        string status = "N";

                                        string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "'";

                                        cmd1.CommandText = checkpre;

                                        string str12 = cmd1.ExecuteScalar().ToString();

                                        if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                        {
                                            string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld,GodownTypeId)  VALUES('" + Issuid + "','23" + did + "','" + sid + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulnumber + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + ","+lblgdntype.Text+")";

                                            cmd1.CommandText = inserttotest;
                                            //cmd1.Connection = con_WPMS;
                                            //cmd1.CommandTimeout = 4600;
                                            int x = cmd1.ExecuteNonQuery();

                                        }
                                        //paddy end


                                        int count = cmd.ExecuteNonQuery();

                                        if (count >= 1)
                                        {
                                            trns1.Commit();

                                            trns.Commit();

                                            //UpdateStock();
                                            //UpdateCBalance();

                                            if (con_Maze.State == ConnectionState.Open)
                                            {
                                                con_Maze.Close();
                                            }

                                            if (con.State == ConnectionState.Open)
                                            {
                                                con.Close();
                                            }

                                        }

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                        btnsavePaddy.Enabled = false;
                                        Update_Trans_Log(gatepass);
                                        Session["Receipt_ID"] = gatepass;
                                        Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();
                                        HyperLink1.Visible = true;


                                        txtissubag.Text = "";
                                        txtissueqty.Text = "";
                                        //  txtaccptno.Text = "";
                                        txtbookno.Text = "";
                                        txtchlnno.Text = "";
                                        txttrucknopady.Text = "";
                                        ddldistpdy.Focus();

                                        // Response.Write("<script Language=javascript>alert('सफलतापूर्वक प्राप्ति हो चुकी हें..... ');self.location='IssueCenter_PaddyReceiptTest.aspx';</script>");
                                    }
                                    catch (Exception ex)
                                    {

                                        trns1.Rollback();
                                        Label9.Visible = true;
                                        Label9.Text = "error:6" + ex.Message;

                                    }
                                    finally
                                    {
                                        if (con_Maze.State == ConnectionState.Open)
                                        {
                                            con_Maze.Close();
                                        }

                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }

                                    }

                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again....'); </script> ");
                                }

                            }

                        }

                        else
                        {
                            string tcc = dsP.Tables[0].Rows[0]["TC_Number"].ToString();
                            string pdat = dsP.Tables[0].Rows[0]["Recd_Date"].ToString();
                            string pdat1 = getdate(pdat);
                            string Tr_Nu = dsP.Tables[0].Rows[0]["Truck_Number"].ToString();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist...." + tcc + "..,Truck No..." + Tr_Nu + "... Recd_Date..." + pdat1 + "'); </script> ");


                        }

                        // }
                    }

                    # endregion               

                }
            }
        //}
           
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity & Bags  should be greater than 0....'); </script> ");
            }
        }
        catch (Exception ex)
        {
            Label9.Visible = true;
            Label9.Text = "error:01" + ex.Message;
        }
        finally
        {
           
        }
    }
  
    void UpdateStock()
    {
        getcsms_Commdty();
        string mcomdtyu = CSMS_Comid;
        string mscheme = ddlscheme.SelectedValue;

        string mfyear = DateTime.Today.Year.ToString();
        string mbookno = txtbookno.Text;

        int monthu = int.Parse(DateTime.Today.Month.ToString());
        int yearu = int.Parse(DateTime.Today.Year.ToString());
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string qrystock = "select  Sum(convert(decimal(18,5),Recd_Qty))  as Qty from dbo.SCSC_Procurement where Commodity_Id ='" + mcomdtyu + "'and Distt_ID='" + did + "'and IssueCenter_ID='" + sid + "'and Month=" + monthu + "and Year=" + yearu;
        mobj = new MoveChallan(ComObj);
        DataSet dspro = mobj.selectAny(qrystock);
        if (dspro.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            DataRow drop = dspro.Tables[0].Rows[0];
            decimal mrfci = CheckNull(drop["Qty"].ToString());
            string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdtyu + "'and Scheme_ID='" + mscheme + "' and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + monthu + "and Year=" + mfyear;
            mobj = new MoveChallan(ComObj);
            DataSet dsopen = mobj.selectAny(qryinsopen);
            if (dsopen.Tables[0].Rows.Count == 0)
            {
                string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdtyu + "' and Scheme_Id ='" + mscheme + "'";
                mobj = new MoveChallan(ComObj);
                DataSet dsqry = mobj.selectAny(chkopenss);
                if (dsqry == null)
                {

                }

                else
                {
                    DataRow drss = dsqry.Tables[0].Rows[0];
                    decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdtyu + "','" + mscheme + "'," + sropen + "," + CheckNull(txtfaq_qty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + monthu + "," + yearu + ",'')";
                    cmd.CommandText = qryinsr;
                    cmd.Connection = con;
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        Label9.Visible = true;
                        Label9.Text = "error:4" + ex.Message;

                    }
                    finally
                    {
                        con.Close();

                    }
                }
            }
            else
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Procure=" + mrfci + " where Commodity_Id ='" + mcomdtyu + "' and Scheme_ID='" + mscheme + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + monthu + "and Year=" + yearu;
                cmd.CommandText = qryinsU;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    Label9.Visible = true;
                    Label9.Text = "error:5" + ex.Message;

                }
                finally
                {
                    con.Close();

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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
      
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Session["dt1"] = null;

        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["dt1"] = null;

        //GridView2.DataSource = (DataTable)Session["dt1"];
        //GridView2.DataBind();

        txt_recJutNew.Text = "";
        txtfaq_qty.Text = "";
        pnlrcvdtl.Visible = true;
        txtissueId.Enabled = false;
        txtissueId.Text = dgridchallan.SelectedRow.Cells[2].Text;
        txtchlnno.Enabled = false;
        txtchlnno.Text = dgridchallan.SelectedRow.Cells[3].Text;
        txttrucknopady.Enabled = false;
        txttrucknopady.Text = dgridchallan.SelectedRow.Cells[4].Text;
        txtissubag.Enabled = false;
        txtissueqty.Enabled = false;
        txtissubag.Text = dgridchallan.SelectedRow.Cells[5].Text;
        txtissueqty.Text = dgridchallan.SelectedRow.Cells[6].Text;
        ddlpdyTransporter.Enabled = false;
       // ddlpdyTransporter.SelectedItem.Text = dgridchallan.SelectedRow.Cells[7].Text;

        string transpid = dgridchallan.SelectedRow.Cells[8].Text.Trim();

        if (transpid == "" || transpid == "&nbsp;")
        {
                        
        }

        else
        {
            ddlpdyTransporter.SelectedValue = dgridchallan.SelectedRow.Cells[8].Text; 
        }

                  
        DaintyDate1P.Enabled = false;
        DaintyDate1P.Text = dgridchallan.SelectedRow.Cells[1].Text;

        txtrec_tcnumber.Text = dgridchallan.SelectedRow.Cells[3].Text;
        txtRec_TruckNumber.Text = dgridchallan.SelectedRow.Cells[4].Text;

        if (txtRec_TruckNumber.Text == "&nbsp;")
        {
            txtRec_TruckNumber.Text = "";
        }

        txtqty.Text = dgridchallan.SelectedRow.Cells[6].Text;

        txt_bags.Text = dgridchallan.SelectedRow.Cells[5].Text;


        txtfaq_qty.Text = dgridchallan.SelectedRow.Cells[6].Text;

        txt_recJutNew.Text = dgridchallan.SelectedRow.Cells[9].Text;

        txt_recJutOld.Text = dgridchallan.SelectedRow.Cells[10].Text;

        txt_recPP.Text = dgridchallan.SelectedRow.Cells[11].Text;

        lblgdntype.Text = dgridchallan.SelectedRow.Cells[12].Text;


        string mystring = ddldistpdy.SelectedValue;

        string disttid = mystring.Substring(mystring.Length - 2);

        string Transport_CSMS = "select distinct Pancard_no,Transporter_Name from Transporter_Table where Transport_ID='8' and Distt_ID='" + disttid + "' and Pancard_no <> '0' and Pancard_no is not null and LRT_proc_secter= '" + ddlsector.SelectedValue + "'";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdtrans = new SqlCommand(Transport_CSMS, con);

        SqlDataAdapter datrans = new SqlDataAdapter(cmdtrans);

        DataSet dstrans = new DataSet();

        datrans.Fill(dstrans);

        if (dstrans.Tables[0].Rows.Count > 0)
        {
            ddlcsms_transp.DataSource = dstrans.Tables[0];

            ddlcsms_transp.DataTextField = "Transporter_Name";
            ddlcsms_transp.DataValueField = "Pancard_no";
            ddlcsms_transp.DataBind();
            ddlcsms_transp.Items.Insert(0, "--Select--");
        }

        else
        {
            ddlcsms_transp.DataSource = "";

            ddlcsms_transp.DataBind();
            ddlcsms_transp.Items.Insert(0, "--Select--");

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया सेक्टर की खरीद केंद्र से मेपिंग एवं परिवहनकर्ता का पेनकार्ड,जिला कार्यालय द्वारा अनिवार्य रूप से करा ले ,इस के बाद ही एंट्री हो पायेगी   !'); </script> ");

        }

     }

    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        string arg;
        arg = e.CommandArgument.ToString();

        switch (arg)
        {
            case "next":
                //The next Button was Clicked
                if ((dgridchallan.PageIndex < (dgridchallan.PageCount - 1)))
                {
                    dgridchallan.PageIndex += 1;
                }

                break;

            case "prev":
                //The prev button was clicked
                if ((dgridchallan.PageIndex > 0))
                {
                    dgridchallan.PageIndex -= 1;
                }

                break;

            case "last":
                //The Last Page button was clicked
                dgridchallan.PageIndex = (dgridchallan.PageCount - 1);
                break;

            default:
                //The First Page button was clicked
                dgridchallan.PageIndex = Convert.ToInt32(arg);
                break;
        }
        //fillgrid();
    }

    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    } 
  
    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    void getcsms_Commdty() // Wheat / Paddy
    {
        mobj = new MoveChallan(ComObj);
        string qry = " SELECT Commodity_Id FROM Procurement_COMMODITY WHERE Proc_Commodity_Id='" + ddlcomdty.SelectedValue.ToString() + "'";
        DataSet ds = mobj.selectAny(qry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            CSMS_Comid = dr["Commodity_Id"].ToString();
        }
        else
        {
            CSMS_Comid = "";
        }

    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlcomdty.SelectedIndex == 0)
        {
            ddldistpdy.Items.Clear();
            ddldistpdy.DataSource = null;
            ddldistpdy.DataBind();
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();
            dlissuClear();
        }
        else
        {
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();

            if (ddlgodown.SelectedIndex != 0)
            {
                ddlgodown.SelectedIndex = 0;
            }

            DaintyDate1P.Text = "";
            txtmaxcap.Text = "";
            txtcurntcap.Text = "";
            txtavalcap.Text = "";
            txthhty.Text = "";
            txt_recJutNew.Text = "";
            txtfaq_qty.Text = "";
            txtchlnno.Text = "";
            txttrucknopady.Text = "";
            txtissubag.Text = "";
            txtissueqty.Text = "";
            ddlpdyTransporter.DataSource = null;
            ddlpdyTransporter.DataBind();
            ddlpdyTransporter.Items.Clear();
            //lblbalanceqty.Visible = false;
            //txtbalqty.Text = "";
            //txtbalqty.Visible = false;
            HyperLink1.Visible = false;
            dlissuClear();
            if (ddlcomdty.SelectedValue.ToString() == "1")
            {

                lblNameDepot.Text = "प्रदाय केन्द";
                getDistWht();
                ddldistpdy_SelectedIndexChanged(sender, e);
                btnsavePaddy.Visible = true;

                getcsms_Commdty();
            }
            else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {

                // No need to Get Paddy / Coarse Grain Data in Wheat Season, already taken Opening Balance in 01/April/2014, Mail by RM Sharma, 5 April 2014

                lblNameDepot.Text = "प्रदाय केन्द";
                getpadyDist();

                ddldistpdy_SelectedIndexChanged(sender, e);

                btnsavePaddy.Visible = true;

                getcsms_Commdty();
            }
            else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
            {



                lblNameDepot.Text = "प्रदाय केन्द";
                getDistCorcgrn();

                ddldistpdy_SelectedIndexChanged(sender, e);

                btnsavePaddy.Visible = true;

                getcsms_Commdty();
            }
        }
    }
    //protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string gname = ddlgodown.SelectedValue;
    //    mobj = new MoveChallan(ComObj);
    //    string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + sid + "' and Godown_ID='" + gname + "'";

    //    DataSet ds = mobj.selectAny(qrygdn);
    //    if (ds == null)
    //    {
    //    }

    //    else
    //    {
    //        if (ds.Tables[0].Rows.Count == 0)
    //        {
    //            txtmaxcap.Text = "";

    //        }
    //        else
    //        {
    //            DataRow dr = ds.Tables[0].Rows[0];
    //            txtmaxcap.Text = dr["Godown_Capacity"].ToString();

    //        }


    //    }
    //    GetCapGodown();
    //    GetBalance();

    //}
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        try
        {
            # region commentedcode_frmware
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }

                //string qrysel = "select tbl_MetaData_GODOWN.Godown_ID,Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,isnull(SUM(ReceiptWts),0)as depositmsp,ISNULL(Godown_Capacity - SUM(ReceiptWts),0)as vacientcap from tbl_MetaData_STACK left join DailyStacking_TransactionStatus on DailyStacking_TransactionStatus.Stackid = tbl_MetaData_STACK.Stack_ID   inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = tbl_MetaData_STACK.Godown_ID where tbl_MetaData_GODOWN.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'  group by Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,tbl_MetaData_GODOWN.Godown_ID order by tbl_MetaData_GODOWN.Godown_ID";
                string qrysel = "select tbl_MetaData_GODOWN.Godown_ID,Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,isnull(SUM(ReceiptWts),0)as depositmsp,ISNULL(Godown_Capacity - SUM(ReceiptWts),0)as vacientcap from tbl_MetaData_GODOWN  left join tbl_MetaData_STACK on tbl_MetaData_GODOWN.Godown_ID = tbl_MetaData_STACK.Godown_ID  left join DailyStacking_TransactionStatus on DailyStacking_TransactionStatus.Stackid = tbl_MetaData_STACK.Stack_ID  where tbl_MetaData_GODOWN.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'   group by Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,tbl_MetaData_GODOWN.Godown_ID order by tbl_MetaData_GODOWN.Godown_ID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txthhty.Text = ds.Tables[0].Rows[0]["Hired_Type"].ToString().Trim() + "/" + ds.Tables[0].Rows[0]["Storage_Type"].ToString().Trim();
                        txtmaxcap.Text = ds.Tables[0].Rows[0]["Godown_Capacity"].ToString();
                       
                    }
                }

            }
            else
            {
            }

            # endregion

            string pqry = "available_space_godown";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmdpqty = new SqlCommand(pqry, con);
            cmdpqty.CommandType = CommandType.StoredProcedure;


            cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = did;
            cmdpqty.Parameters.Add("@Depotid", SqlDbType.Int).Value = sid;
            cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = ddlgodown.SelectedValue.ToString();

            DataSet ds1 = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

            dr.Fill(ds1);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                double stock = Math.Round(Convert.ToDouble(ds1.Tables[0].Rows[0]["Total"].ToString()), 5);

                txtcurntcap.Text = Convert.ToString(stock);

                double Max_Cap =  Math.Round(Convert.ToDouble(CheckNull(txtmaxcap.Text)), 5);

                double availble = Max_Cap - stock;

                txtavalcap.Text = Convert.ToString(availble);


            }

           // GetBalance();
        }

        catch (Exception)
        {

            if (cons.State == ConnectionState.Closed)
            {
                cons.Open();
            }

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (cons.State == ConnectionState.Closed)
            {
                cons.Open();
            }
        }
       
    }

    void GetCapGodown()
    {
        string gname = ddlgodown.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT Sum(Current_Balance) as Current_Balance  FROM dbo.issue_opening_balance where District_Id='" + did + "' and Depotid='" + sid + "' and Godown='" + gname + "'";

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
                txtcurntcap.Text = (System.Math.Round(CheckNull(dr["Current_Balance"].ToString()), 5)).ToString();
                txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();
            }


        }

    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Session["dt1"] = null;

        //GridView2.DataSource = "";

        //GridView2.DataBind();

        if (ddluparjan.SelectedIndex > 0)
        {
            btnsavePaddy.Enabled = true;
            //ddluparjan.SelectedIndex = 0;
            
            if (ddlgodown.SelectedIndex != 0)
            {
                ddlgodown.SelectedIndex = 0;
            }
            DaintyDate1P.Text = "";
            txtmaxcap.Text = "";
            txtcurntcap.Text = "";
            txtavalcap.Text = "";
            txthhty.Text = "";
            txt_recJutNew.Text = "";
            txtfaq_qty.Text = "";
            txtchlnno.Text = "";
            txtissueId.Text = "";
          txttrucknopady.Text = "";
          txtissubag.Text = "";
          txtissueqty.Text = "";
          getpaddyIssueid();
         // ddlpdyTransporter.DataSource = null;
          //ddlpdyTransporter.DataBind();
         // ddlpdyTransporter.Items.Clear();
            //ddlgodown.SelectedIndex = 0;
            //lblbalanceqty.Visible = false;
            //txtbalqty.Text = "";
            //txtbalqty.Visible = false;
            HyperLink1.Visible = false;
        }
        else
        {
            Response.Redirect("~/IssueCenter/CSC_Procurement_New.aspx");
        }
        //Response.Redirect("~/IssueCenter/CSC_Procurement.aspx");
    }
    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void txtrecdqty_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        decimal mqty = CheckNull(txtissueqty.Text) * 10;
    //        decimal mrecdqty = CheckNull(txtrecdqty.Text);
    //        if (mrecdqty >= mqty)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Recd_Qty it should not be greater than ten times issue_Qty....'); </script> ");

    //            txtmaxcap.Text = "";
    //            txtcurntcap.Text = "";
    //            txtavalcap.Text = "";
    //            txtbalqty.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Label9.Visible = true;
    //        Label9.Text = "error:02" + ex.Message;
    //    }
    //    finally
    //    {
           

    //    }
    //}

    protected void ddlbranchwlc_SelectedIndexChanged(object sender, EventArgs e)
    {

        Getgon();
        txtmaxcap.Text = "";
        txtcurntcap.Text = "";
        txtavalcap.Text = "";
        txthhty.Text = "";
        //txtbalqty.Text = "";
    }

    private void dlissuClear()
    {
        txt_recJutNew.Text = "";
        txtfaq_qty.Text = "";
        txtissueId.Text = "";
        txtchlnno.Text = "";
        txttrucknopady.Text = "";
        DaintyDate1P.Text = "";
        txtissubag.Text = "";
        txtissueqty.Text = "";
        dgridchallan.DataSource = null;
        dgridchallan.DataBind();
        pnlgrd.Visible = false;
        pnlrcvdtl.Visible = false;
    }

    protected void getbranch()
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }


                string qrysel = "select DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + did + "'";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "DepotID";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }
    }
    
    void getpaddyIssueid_Agarmalwa()  // This function only for Agar Malwa District, because they need to enter the data of Sajapur also, dispatch frm Procurement, but no need to use this function in coming paddy season. 
    {
        //distp = "51";    //  Anurag . 30/05/2014, mail on 29/05/2014

        distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);

        # region Wheat
        if (ddlcomdty.SelectedValue.ToString() == "1")
        {
            try
            {
                if (con_WPMS != null)
                {
                    con_WPMS.Open();



                    if (distp == did)
                    {
                        // DateTime mydt = Convert.ToDateTime(getDate_MDY(txtisdate.Text));
                        string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    }
                    else
                    {
                        // DateTime mydt = Convert.ToDateTime(getDate_MDY(txtisdate.Text));
                        string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and  SendingDistId ='23" + did + "' order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
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

        # endregion


        # region Paddy

        else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
        {

            try
            {
                if (con_paddy != null)
                {
                    if (distp == did)
                    {
                        //string qrysel = "select (IssueToSangrahanaKendra.IssueID+'/'+IssueToSangrahanaKendra.TruckChalanNo)as Name,IssueToSangrahanaKendra.IssueID from IssueToSangrahanaKendra where IssueToSangrahanaKendra.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IssueToSangrahanaKendra.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and convert(varchar,DateOfIssue,101)='" + getDate_MDY(txtisdate.Text) + "' and IssueTo not in('OD') ";
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and IssueTo not in('OD') order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    }
                    else
                    {
                        // string qrysel = "select (IssueToSangrahanaKendra.IssueID+'/'+IssueToSangrahanaKendra.TruckChalanNo)as Name,IssueToSangrahanaKendra.IssueID from IssueToSangrahanaKendra where IssueToSangrahanaKendra.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IssueToSangrahanaKendra.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and convert(varchar,DateOfIssue,101)='" + getDate_MDY(txtisdate.Text) + "' and SendingDistId ='23" + did + "'";
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and SendingDistId ='23" + did + "' order by DateOfIssue desc";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
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

        # endregion

        # region Coarse

        else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
        {
            try
            {
                if (con_Maze != null)
                {
                    con_Maze.Open();
                    if (distp == did)
                    {
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and IssueTo not in('OD') order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    }
                    else
                    {
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and SendingDistId ='23" + did + "' order by DateOfIssue desc";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                                pnlrcvdtl.Visible = false;
                                lber.Text = "Data not available for this Purchase Center";
                            }
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

        # endregion

    }
        
    //protected void btnaddGodown_Click(object sender, EventArgs e)
    //{

    //    Panel2.Visible = true;
    //    bool checkstatus = false;

    //    if (txtfaq_qty.Text == "")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('प्राप्त एफ ऐ क्यू की मात्रा भरे !'); </script> ");
    //        return;
    //    }

    //    if (ddlcomdty.SelectedValue == "0")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('फसल का प्रकार चुने !'); </script> ");
    //        return;
    //    }
        
    //    if (dgridchallan.Visible == false)
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('जिले एवं उपार्जन केंद्र का नाम चुने या प्राप्ति के लिए Click to Receive करें !'); </script> ");
    //        return;
    //    }

    //    if (txtUrs_qty.Text == "")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('यु आर एस की मात्रा खाली नहीं होगी कृपया शुन्य भरे !'); </script> ");
    //        return;
    //    }

    //    if (txt_recJutNew.Text == "")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('नए जुट बैग की संख्या भरे !'); </script> ");
    //        return;
    //    }

    //    if (txt_recPP.Text == "")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पी पी बेग्स की संख्या भरे!'); </script> ");
    //        return;
    //    }

    //    if (txt_recJutOld.Text == "")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पुराने जुट बैग की संख्या भरे !'); </script> ");
    //        return;
    //    }
          

    //    else if (ddlgodown.SelectedIndex == 0)
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('गोदाम चुने !'); </script> ");
    //        return;
    //    }

    //    else if (txtmoisture.Text == "" || txtTaulNum.Text == "")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('नमी प्रतिशत अथवा तौल नंबर भरें!'); </script> ");
    //        return;
    //    }

    //    else if ((txtavalcap.Text != "") && (Convert.ToDecimal(txtavalcap.Text) < Convert.ToDecimal(txtfaq_qty.Text) || (Convert.ToDecimal(txtavalcap.Text) < Convert.ToDecimal(txtUrs_qty.Text))))
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('गोदाम में जगह उपलब्ध नहीं है !'); </script> ");
    //        return;
    //    }

    //    else if (ddlcategory.SelectedItem.Text == "--Select--" || ddlcategory.SelectedItem.Value == "0")
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('URS की Category चुने |'); </script> ");
    //    }
    //    else if ((ddlcategory.SelectedItem.Text == "Y" || ddlcategory.SelectedItem.Text == "Z") && txtUrs_qty.Text == "0" )
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Y या Z Category होने पर URS की मात्रा भरें |'); </script> ");
              
    //    } 

    //    else
    //    {

    //        Int32 recjn = Convert.ToInt32(txt_recJutNew.Text);

    //        Int32 recpp = Convert.ToInt32(txt_recPP.Text);


    //        Int32 recjo = Convert.ToInt32(txt_recJutOld.Text);

    //        int sumbags = recjn + recpp + recjo;

    //        txt_bags.Text = Convert.ToString(sumbags);
    //        // all sum


    //        double recfaq = Convert.ToDouble(txtfaq_qty.Text);


    //        double recurs = Convert.ToDouble(txtUrs_qty.Text);

    //        double sumqty = recfaq + recurs;

    //        txtqty.Text = Convert.ToString(sumqty);
            


    //        if (ddlgodown.Items.Count > 0)
    //        {
    //            btnsavePaddy.Enabled = true;
    //            if (Session["dt1"] == null)
    //            {
    //                Dt1 = CreateTable();
    //                Session["dt1"] = Dt1;
    //            }

    //            // adding rows to the datatable
    //            DataRow dr = ((DataTable)Session["dt1"]).NewRow();

    //            ((DataTable)Session["dt1"]).AcceptChanges();

    //            dr["Godown"] = ddlgodown.SelectedItem;

    //            dr["qty_RecF"] = txtfaq_qty.Text.Trim();

    //            dr["qty_RecU"] = txtUrs_qty.Text.Trim();

    //            dr["bags_JuteN"] = txt_recJutNew.Text.Trim();

    //            dr["bags_PP"] = txt_recPP.Text.Trim();

    //            dr["bags_JuteO"] = txt_recJutOld.Text.Trim();

    //            dr["commodity"] = ddlcomdty.SelectedItem;

    //            dr["commodityid"] = ddlcomdty.SelectedValue;

    //            dr["Godownid"] = ddlgodown.SelectedValue;

    //            dr["Category"] = ddlcategory.SelectedItem.Text;

            
    //            if (GridView2.Rows.Count > 0)
    //            {
    //                int i;

    //                // checking whether or not the godown is already added to the grid view
    //                for (i = 0; i <= GridView2.Rows.Count - 1; i++)
    //                {
    //                    string commodityid = GridView2.DataKeys[i].Values[0].ToString();
    //                    string selectcomm = ddlcomdty.SelectedValue.ToString();

    //                    string godownid = GridView2.DataKeys[i].Values[1].ToString();

    //                    string selectgodown = ddlgodown.SelectedValue.ToString();

    //                    if (godownid == selectgodown && commodityid == selectcomm)
    //                    {
    //                        checkstatus = true;
    //                    }
    //                }
    //                if (checkstatus == false)
    //                {
    //                    ((DataTable)Session["dt1"]).Rows.Add(dr);
    //                    ((DataTable)Session["dt1"]).AcceptChanges();
    //                    GridView2.DataSource = (DataTable)Session["dt1"];
    //                    GridView2.DataBind();

    //                    txt_recJutNew.Text = "0";

    //                    txt_recPP.Text = "0";

    //                    txt_recJutOld.Text = "0";

    //                    txtfaq_qty.Text = "0";

    //                    txtUrs_qty.Text = "0";
    //                }
    //                else
    //                {
    //                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('इस गोदाम के लिए प्रविष्टि हो चुकी है |'); </script> ");
    //                }
    //            }
    //            else
    //            {
    //                ((DataTable)Session["dt1"]).Rows.Add(dr);
    //                ((DataTable)Session["dt1"]).AcceptChanges();
    //                GridView2.DataSource = (DataTable)Session["dt1"];
    //                GridView2.DataBind();

    //                txt_recJutNew.Text = "0";

    //                txt_recPP.Text = "0";

    //                txt_recJutOld.Text = "0";

    //                txtfaq_qty.Text = "0";

    //                txtUrs_qty.Text = "0";

    //            }

    //        }


    //    }

    //}

    private DataTable CreateTable()
    {
       
        DataTable dt = new DataTable();//DataTable is created
        DataColumn Godown = new DataColumn("Godown", Type.GetType("System.String"));
        DataColumn qty_RecFaq = new DataColumn("qty_RecF", Type.GetType("System.Decimal"));

        DataColumn qty_RecUrs = new DataColumn("qty_RecU", Type.GetType("System.Decimal"));

        DataColumn bagsJuteN = new DataColumn("bags_JuteN", Type.GetType("System.Int32"));
        DataColumn bagsPP = new DataColumn("bags_PP", Type.GetType("System.Int32"));
        DataColumn bagsJuteO = new DataColumn("bags_JuteO", Type.GetType("System.Int32"));
     

        //DataColumn truck = new DataColumn("gate_pass", Type.GetType("System.String"));

        DataColumn commodity = new DataColumn("commodity", Type.GetType("System.String"));

        DataColumn commodityid = new DataColumn("commodityid", Type.GetType("System.Int32"));

        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.Int64"));


        DataColumn Category = new DataColumn("Category", Type.GetType("System.String"));

       
        dt.Columns.Add(Godown);//Column is added to the DataTable
        dt.Columns.Add(qty_RecFaq);//Column is added to the DataTable

        dt.Columns.Add(qty_RecUrs);//Column is added to the DataTable

        dt.Columns.Add(bagsJuteN);//Column is added to the DataTable

        dt.Columns.Add(bagsPP);//Column is added to the DataTable


        dt.Columns.Add(bagsJuteO);//Column is added to the DataTable

        dt.Columns.Add(commodity);//Column is added to the DataTable

        dt.Columns.Add(commodityid);//Column is added to the DataTable

        dt.Columns.Add(Godownid);

        dt.Columns.Add(Category);

        
        dt.AcceptChanges();
        return dt;
    }

    protected void btnbags_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
           

            if (txtfaq_qty.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('प्राप्त एफ ऐ क्यू की मात्रा भरे !'); </script> ");
                return;
            }

            if (ddlcomdty.SelectedValue == "0")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('फसल का प्रकार चुने !'); </script> ");
                return;
            }

            if (dgridchallan.Visible == false)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('जिले एवं उपार्जन केंद्र का नाम चुने या प्राप्ति के लिए Click to Receive करें !'); </script> ");
                return;
            }

            if (txtUrs_qty.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('यु आर एस की मात्रा खाली नहीं होगी कृपया शुन्य भरे !'); </script> ");
                return;
            }

            if (txt_recJutNew.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('नए जुट बैग की संख्या भरे !'); </script> ");
                return;
            }

            if (txt_recPP.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पी पी बेग्स की संख्या भरे!'); </script> ");
                return;
            }

            if (txt_recJutOld.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पुराने जुट बैग की संख्या भरे !'); </script> ");
                return;
            }

            else
            {

                Int32 recjn = Convert.ToInt32(txt_recJutNew.Text);

                Int32 recpp = Convert.ToInt32(txt_recPP.Text);


                Int32 recjo = Convert.ToInt32(txt_recJutOld.Text);

                int sumbags = recjn + recpp + recjo;

                txt_bags.Text = Convert.ToString(sumbags);
                // all sum

            }
           
    }

    protected void btnqty_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        bool checkstatus = false;

        if (txtfaq_qty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('प्राप्त एफ ऐ क्यू की मात्रा भरे !'); </script> ");
            return;
        }

        if (ddlcomdty.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('फसल का प्रकार चुने !'); </script> ");
            return;
        }

        if (dgridchallan.Visible == false)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('जिले एवं उपार्जन केंद्र का नाम चुने या प्राप्ति के लिए Click to Receive करें !'); </script> ");
            return;
        }

        if (txtUrs_qty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('यु आर एस की मात्रा खाली नहीं होगी कृपया शुन्य भरे !'); </script> ");
            return;
        }

        if (txt_recJutNew.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('नए जुट बैग की संख्या भरे !'); </script> ");
            return;
        }

        if (txt_recPP.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पी पी बेग्स की संख्या भरे!'); </script> ");
            return;
        }

        if (txt_recJutOld.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पुराने जुट बैग की संख्या भरे !'); </script> ");
            return;
        }


        else if (ddlgodown.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('गोदाम चुने !'); </script> ");
            return;
        }


        else
        {
            double recfaq = Convert.ToDouble(txtfaq_qty.Text);
            
            double recurs = Convert.ToDouble(txtUrs_qty.Text);

            double sumqty = recfaq + recurs;

            txtqty.Text = Convert.ToString(sumqty);

        }
    }
}
