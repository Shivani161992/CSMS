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

public partial class IssueCenter_CSC_Procurement : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();
            version = Session["hindi"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtissueqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtissubag.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecdbags.Attributes.Add("onkeypress", "return IsNumericProcQtyBag(event,this)");
            txtrecdqty.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");

            txtrecdqty.Attributes.Add("onchange", "return ChkNotgrate(this);");

            txtissueqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtissueqty.Attributes.Add("onchange", "return chksqltxt(this)");

            
            txtrecdbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdbags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecdqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdqty.Attributes.Add("onchange", "return chksqltxt(this)");

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
            txtrecdbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtissubag.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           // txtbookno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrecdbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrecdqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            chk = new chksql();
            HyperLink1.Attributes.Add("onclick", "window.open('Print_Gatepass_Procurement.aspx',null,'left=50, top=10, height=570, width= 690, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            distobj = new DistributionCenters(ComObj);
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtchlnno.Text);
            //ctrllist.Add(txttrucknopady.Text);
            ctrllist.Add(txtissueqty.Text);
            ctrllist.Add(txtissubag.Text);
            ctrllist.Add(txtrecdbags.Text);
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
                txtisdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate1P.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                
                GetCommodity();
                Getdepo();
                //GetGodown();
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
                int myear = int.Parse(DateTime.Now.Year.ToString());
                //ddlcropyear.Items.Add("Crop Year");
               
                ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 2).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 3).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 2).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 4).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 3).ToString());
                

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


                string qrysel = "select DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='23" + did + "'";
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
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlbranchwlc.SelectedValue.ToString() + "'";
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

                string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('1','8')";

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
                        //ddldistpdy.SelectedItem.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                        //ddldistpdy.SelectedValue = did;


                        ddlRec_DistGodown.DataSource = ds.Tables[0];
                        ddlRec_DistGodown.DataTextField = "District_Name";
                        ddlRec_DistGodown.DataValueField = "District_Code";
                        ddlRec_DistGodown.DataBind();




                        ddlRec_DistGodown.ClearSelection();
                        ddlRec_DistGodown.Items.FindByValue(23 + did).Selected = true;



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


                        ddlRec_DistGodown.DataSource = ds.Tables[0];
                        ddlRec_DistGodown.DataTextField = "District_Name";
                        ddlRec_DistGodown.DataValueField = "District_Code";
                        ddlRec_DistGodown.DataBind();




                        ddlRec_DistGodown.ClearSelection();
                        ddlRec_DistGodown.Items.FindByValue(23 + did).Selected = true;

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

                        ddlRec_DistGodown.DataSource = ds.Tables[0];
                        ddlRec_DistGodown.DataTextField = "District_Name";
                        ddlRec_DistGodown.DataValueField = "District_Code";
                        ddlRec_DistGodown.DataBind();




                        ddlRec_DistGodown.ClearSelection();
                        ddlRec_DistGodown.Items.FindByValue(23 + did).Selected = true;


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
                string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' order by ic.SocietyID";
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
                        string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";
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

            if(did == "51")
            {
                getpaddyIssueid_Agarmalwa();

            }

            else
            {
                getpaddyIssueid();
            }

           
            
            getpaddyTranspoter();
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
    void GetBalance()
    {
        string mcomid = CSMS_Comid;
        //string mcatid = ddlcategory.SelectedValue;
        string godownid = ddlgodown.SelectedValue;
        string brnch = ddlbranchwlc.SelectedValue;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string source = "01";
        mobj1 = new MoveChallan(ComObj);


        string recgdndist = ddlRec_DistGodown.SelectedValue.ToString().Substring(2, 2);


        string qry = "Select Sum(Current_Balance)as Current_Balance from dbo.issue_opening_balance where District_Id='" + recgdndist + "'and Depotid='" + ddlbranchwlc.SelectedValue + "'and Commodity_Id='" + mcomid + "'and Source='" + source + "'and Godown='" + godownid + "' and Branch_Id='" + brnch + "' ";
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
            lblbalanceqty.Visible = true;
            txtbalqty.Visible = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.ReadOnly = true;
        }
    }

    void UpdateCBalance()
    {
        getcsms_Commdty();
        string opid = Session["OperatorId"].ToString();
        string comdtyid = CSMS_Comid;
        string godown = ddlgodown.SelectedValue;
        string brnch = ddlbranchwlc.SelectedValue;
        string source = "01";
        string schemeid = ddlscheme.SelectedValue;
        int recdbags = CheckNullInt(txtrecdbags.Text);
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
                string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate,OperatorID,Branch_Id) values('" + mstate + "','" + did + "','" + sid + "','" + comdtyid + "','" + schemeid + "','','" + mggn + "','" + ddlcropyear.SelectedValue + "'," + openbag + "," + openqty + ",'01'," + CheckNull(txtrecdqty.Text) + "," + recdbags + "," + month + "," + year + ",'" + ip + "',getdate(),getdate(),'" + udate + "','" + ddate + "','" + opid + "','" + brnch + "')";
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
                string query = "Update dbo.issue_opening_balance set Current_Balance = convert(decimal(18,5), Current_Balance)+" + CheckNull(txtrecdqty.Text) + ",Current_Bags=Current_Bags+" + recdbags + " where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Godown='" + godown + "' and Scheme_Id='" + schemeid + "' and Source='01' and Branch_Id='" + brnch + "' ";
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
                              
        string qryinsert = "insert into dbo.SCSC_Procurement_Trans_Log(Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Updates_Date,IP_Address,Operation,Branch_Id)values('" + did + "','" + sid + "','" + distp + "','" + ddluparjan.SelectedValue + "','" + mdispdate1 + "','" + txtchlnno.Text + "','" + txttrucknopady.Text + "','" + ddlpdyTransporter.SelectedValue + "','" + CSMS_Comid + "','" + ddlcropyear.SelectedValue + "'," + CheckNullInt(txtissubag.Text) + "," + CheckNull(txtissueqty.Text) + "," + CheckNullInt(txtrecdbags.Text) + "," + CheckNull(txtrecdqty.Text) + ",'" + mrecddate1 + "','" + ddlgodown.SelectedValue + "','" + GPASS + "'," + my3month + "," + my3year + ",getdate(),'" + ip + "','I','" + ddlbranchwlc.SelectedValue + "')";

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

            if (Convert.ToDecimal(txtrecdqty.Text) > 0)
            {
                //decimal chk_Rquinty = CheckNull(((txtissueqty.Text) * 15) / 100);
                decimal chk_RquintyPr = CheckNull(txtissueqty.Text) * 40;
                decimal chk_Rquinty = chk_RquintyPr / 100;
                //decimal mqty1 = CheckNull(txtissueqty.Text) * 10;
                decimal mqty1 = CheckNull(txtissueqty.Text) + chk_Rquinty;
                decimal mrecdqty1 = CheckNull(txtrecdqty.Text);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // check duplicate entries, anurag -- 22-04-2014 // adding issue id in filter on 20-05-2014

                string tcnum = txtchlnno.Text;
                string trucknumber = txttrucknopady.Text;
                decimal recdqty = CheckNull(txtrecdqty.Text);
                string recdate = getDate_MDY(DaintyDate3.Text);
                string issueid = txtissueId.Text;

                string CheckduplicateRec = "Select * from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + tcnum + "' and Truck_Number = '" + trucknumber + "' and Recd_Date = '" + recdate + "' and Recd_Qty = '" + recdqty + "' and Receipt_Id = '"+issueid+"' ";

                SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

                SqlDataReader drduplicate;

                drduplicate = cmdduplirec.ExecuteReader();

                if (drduplicate.Read())
                {
                    return;
                }
                else
                {

                //if (mrecdqty1 >= mqty1)
                //{
                //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Recd_Qty it should not be greater than issue_Qty....'); </script> ");
                //}
                //else
                //{
                    drduplicate.Close();

                    # region wheat
                    if (ddlcomdty.SelectedValue.ToString() == "1")
                    {

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


                        decimal ccap = CheckNull(txtcurntcap.Text);
                        decimal rcap = CheckNull(txtrecdqty.Text);
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

                            //string select = "Select TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + ddlcomdty.SelectedValue.ToString() + "' and YEAR=Year(GETDATE())";
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
                                string macno = txtaccptno.Text;
                                //string macdate = getDate_MDY(DaintyDate2.Text);
                                string mstatus = "N";

                                string mudate = "";
                                string mddate = "";
                                string mfyear = DateTime.Today.Year.ToString();
                                string mbookno = txtbookno.Text;
                                string accpno = mfyear + mbookno + txtaccptno.Text;
                                int month = int.Parse(DateTime.Today.Month.ToString());
                                int year = int.Parse(DateTime.Today.Year.ToString());
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string state = Session["State_Id"].ToString();
                                string anst = "N";
                                int mrecdbags = CheckNullInt(txtrecdbags.Text);
                                decimal mrecdqty = CheckNull(txtrecdqty.Text);
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




                                if (ddlcomdty.SelectedItem.Text == "--Select--" || ddluparjan.SelectedItem.Text == "--Select--" || ddlpdyTransporter.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Purchase Center /Transporter Name/Godown....'); </script> ");
                                }

                       else
                        {
                            string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement where Receipt_Id = '" + gatepass + "'and IssueCenter_ID = '" + sid + "'";
                            //cmd1 = new SqlCommand(checkpre, con_paddy);
                             cmd.CommandText = checkrcid;
                             //cmd.Connection = con;
                            
                          string str1 = cmd.ExecuteScalar().ToString();

                            if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                               {
    
                                 try
                                 {

                                     string qryinsert = "insert into dbo.SCSC_Procurement(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber)values('" + state + "','" + did + "','" + sid + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'" + macno + "',getdate(),'" + mbookno + "'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "','')";
                                     cmd.CommandText = qryinsert;

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
                                            string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "'";
                                            //cmd1 = new SqlCommand(checkpre, con_paddy);
                                            cmd1.CommandText = checkpre;
                                            //cmd1.Connection = con_WPMS;
                                            string str12 = cmd1.ExecuteScalar().ToString();

                                            if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                            {
                                                string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber)  VALUES('" + Issuid + "','23" + did + "','" + sid + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulnumber + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "','')";

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
                                                con_WPMS.Close();
                                                trns.Commit();
                                                con.Close();
                                                UpdateStock();
                                                UpdateCBalance();

                                            }

                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                            btnsavePaddy.Enabled = false;
                                            Update_Trans_Log(gatepass);
                                            Session["Receipt_ID"] = gatepass;
                                            Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();
                                            HyperLink1.Visible = true;


                                            txtissubag.Text = "";
                                            txtissueqty.Text = "";
                                            txtaccptno.Text = "";
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
                                            con.Close();
                                            con_WPMS.Close();
                                        }



                                    }
                                    else
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again....'); </script> ");
                                    }
                                }

                            //}

                            //drdup.Close();

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
                        decimal rcap = CheckNull(txtrecdqty.Text);
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
                                string macno = txtaccptno.Text;
                                //string macdate = getDate_MDY(DaintyDate2.Text);
                                string mstatus = "N";

                                string mudate = "";
                                string mddate = "";
                                string mfyear = DateTime.Today.Year.ToString();
                                string mbookno = txtbookno.Text;
                                string accpno = mfyear + mbookno + txtaccptno.Text;
                                int month = int.Parse(DateTime.Today.Month.ToString());
                                int year = int.Parse(DateTime.Today.Year.ToString());
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string state = Session["State_Id"].ToString();
                                string anst = "N";
                                int mrecdbags = CheckNullInt(txtrecdbags.Text);
                                decimal mrecdqty = CheckNull(txtrecdqty.Text);
                                string mrecddate = getDate_MDY(DaintyDate3.Text);
                                string mrecdgdn = ddlgodown.SelectedValue;
                                string branch = ddlbranchwlc.SelectedValue;
                                string opid = Session["OperatorId"].ToString();
                                string notrans = "N";
                                if (ddlcomdty.SelectedItem.Text == "--Select--" || ddluparjan.SelectedItem.Text == "--Select--" || ddlpdyTransporter.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--")
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

                             string qryinsert = "insert into dbo.SCSC_Procurement(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber)values('" + state + "','" + did + "','" + sid + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + mchallan + "','" + mtruckno + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'" + macno + "',getdate(),'" + mbookno + "'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "','')";
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
                                                string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber)  VALUES('" + Issuid + "','23" + did + "','" + sid + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulptrk + "','" + trnsid + "','" + tcno + "','" + truckno + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "' ,'')";

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
                                                
                                                UpdateStock();
                                                UpdateCBalance();

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
                                            Update_Trans_Log(gatepass);
                                            Session["Receipt_ID"] = gatepass;
                                            Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();
                                            HyperLink1.Visible = true;


                                            txtissubag.Text = "";
                                            txtissueqty.Text = "";
                                            txtaccptno.Text = "";
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
                        decimal rcap = CheckNull(txtrecdqty.Text);
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
                                string macno = txtaccptno.Text;
                              
                                string mstatus = "N";

                                string mudate = "";
                                string mddate = "";
                                string mfyear = DateTime.Today.Year.ToString();
                                string mbookno = txtbookno.Text;
                                string accpno = mfyear + mbookno + txtaccptno.Text;
                                int month = int.Parse(DateTime.Today.Month.ToString());
                                int year = int.Parse(DateTime.Today.Year.ToString());
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string state = Session["State_Id"].ToString();
                                string anst = "N";
                                int mrecdbags = CheckNullInt(txtrecdbags.Text);
                                decimal mrecdqty = CheckNull(txtrecdqty.Text);
                                string mrecddate = getDate_MDY(DaintyDate3.Text);
                                string mrecdgdn = ddlgodown.SelectedValue;
                                string branch = ddlbranchwlc.SelectedValue;
                                string opid = Session["OperatorId"].ToString();
                                string notrans = "N";

                                if (ddlcomdty.SelectedItem.Text == "--Select--" || ddluparjan.SelectedItem.Text == "--Select--" || ddlpdyTransporter.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--")
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

                          string qryinsert = "insert into dbo.SCSC_Procurement(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id ,RackNumber)values('" + state + "','" + did + "','" + sid + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + mchallan + "','" + mtruckno + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'" + macno + "',getdate(),'" + mbookno + "'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "','')";
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
                                            string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();
                                            string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                            string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                            string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();

                                          
                                            string udate = "";
                                            string status = "N";
                                          
                                            string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "'";
                                            
                                            cmd1.CommandText = checkpre;
                                           
                                            string str12 = cmd1.ExecuteScalar().ToString();

                                            if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                            {
                                                string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber)  VALUES('" + Issuid + "','23" + did + "','" + sid + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulptrk + "','" + trnsid + "','" + tcno + "','" + truckno + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "','')";

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

                                                UpdateStock();
                                                UpdateCBalance();

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
                                            txtaccptno.Text = "";
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
                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdtyu + "','" + mscheme + "'," + sropen + "," + CheckNull(txtrecdqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + monthu + "," + yearu + ",'')";
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
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrecdbags.Text = "";
        txtrecdqty.Text = "";
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
        ddlpdyTransporter.SelectedItem.Text = dgridchallan.SelectedRow.Cells[7].Text;
        ddlpdyTransporter.SelectedValue = dgridchallan.SelectedRow.Cells[8].Text;            
        DaintyDate1P.Enabled = false;
        DaintyDate1P.Text = dgridchallan.SelectedRow.Cells[1].Text;

        txtrec_tcnumber.Text = dgridchallan.SelectedRow.Cells[3].Text;
        txtRec_TruckNumber.Text = dgridchallan.SelectedRow.Cells[4].Text;
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
            txtrecdbags.Text = "";
            txtrecdqty.Text = "";
            txtchlnno.Text = "";
            txttrucknopady.Text = "";
            txtissubag.Text = "";
            txtissueqty.Text = "";
            ddlpdyTransporter.DataSource = null;
            ddlpdyTransporter.DataBind();
            ddlpdyTransporter.Items.Clear();
            lblbalanceqty.Visible = false;
            txtbalqty.Text = "";
            txtbalqty.Visible = false; HyperLink1.Visible = false;
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
                btnsavePaddy.Visible = true;

                getcsms_Commdty();
            }
            else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
            {



                lblNameDepot.Text = "प्रदाय केन्द";
                getDistCorcgrn();
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


            cmdpqty.Parameters.Add("@district_code", SqlDbType.Int).Value = did;
            cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = sid;
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

            GetBalance();
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
            txtrecdbags.Text = "";
            txtrecdqty.Text = "";
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
            lblbalanceqty.Visible = false;
            txtbalqty.Text = "";
            txtbalqty.Visible = false;
            HyperLink1.Visible = false;
        }
        else
        {
            Response.Redirect("~/IssueCenter/CSC_Procurement.aspx");
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
        txtbalqty.Text = "";
    }

    private void dlissuClear()
    {
        txtrecdbags.Text = "";
        txtrecdqty.Text = "";
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

    protected void ddlRec_DistGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }


                string qrysel = "select DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddlRec_DistGodown.SelectedValue + "'";
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



}
