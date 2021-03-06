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
public partial class IssueCenter_Edit_Procurement_SCSC : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmdP = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Transporter tobj = null;
    MoveChallan mobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj2 = null;
    Districts DObj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    public string time;
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string getdatef = "";
    public string challan = "";
    public string version = "";
    public string recdID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();
            challan = Session["challan"].ToString();
            recdID = Session["Recd_ID"].ToString();
            version = Session["hindi"].ToString();           

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtquant.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtbagno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtrecdbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecdqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtquant.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtquant.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbagno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbagno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecdbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdbags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecdqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txttrukcno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttrukcno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttrukcno.Attributes.Add("onchange", "return chksqltxt(this)");

            txttruckno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttruckno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttruckno.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

            txttrukcno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttruckno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtquant.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbagno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtaccptno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbookno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txttrukcno.Text);
            ctrllist.Add(txttruckno.Text);
            ctrllist.Add(txtquant.Text);
            ctrllist.Add(txtbagno.Text);
            ctrllist.Add(txtrecdbags.Text);
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
                DaintyDate2.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                GetScheme();

                GetTransport();
                GetCommodity();
                GetDist();
                GetName();
                Getdepo();
                //GetGodown();
                GetData();
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
                    btnsave.Text = Resources.LocalizedText.btnsave;
                    lblNameDepot.Text = Resources.LocalizedText.lblNameDepot;
                    lblDistrictName.Text = Resources.LocalizedText.lblDistrictName;
                    //lblKgs.Text = Resources.LocalizedText.lblKgs;
                }
                                
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
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId='23" + did + "'";
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

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        string gsch = "Select Scheme_Name,Scheme_Id from dbo.tbl_MetaData_SCHEME where Scheme_Name='DCP'";
        DataSet ds = schobj.selectAny(gsch);
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();

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

    public string getdateg(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    void GetData()
    {
        try
        {
            Getgon();
            mobj = new MoveChallan(ComObj);
            string query = "SELECT SCSC_Procurement.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName,districtsmp.district_name as district_name   FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transporter_Table on SCSC_Procurement.Transporter_ID =Transporter_Table.Transporter_ID left join pds.districtsmp on SCSC_Procurement.Sending_District=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on SCSC_Procurement.IssueCenter_ID=tbl_MetaData_DEPOT.DepotID  where SCSC_Procurement.Distt_ID='" + did + "' and  SCSC_Procurement.IssueCenter_ID='" + sid + "' and SCSC_Procurement.TC_Number='" + challan + "'";
            DataSet ds = mobj.selectAny(query);
            DataRow dr = ds.Tables[0].Rows[0];
            ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
            ddldistrict.SelectedValue = dr["Sending_District"].ToString();
            //ddlissuecenter.SelectedItem.Text = dr["DepotName"].ToString();
            ddlissuecenter.SelectedValue = dr["Purchase_Center"].ToString();
             ddlissuecenter.Enabled = false;
            DaintyDate1.Text = getdateg(dr["Dispatch_Date"].ToString());
            txttrukcno.Text = dr["TC_Number"].ToString();
            //txttrukcno.ReadOnly = true;
            //txttrukcno.BackColor = System.Drawing.Color.Wheat;
            txttruckno.Text = dr["Truck_Number"].ToString();
            ddltransporter.SelectedItem.Text = dr["Transporter_Name"].ToString();
            ddltransporter.SelectedValue = dr["Transporter_ID"].ToString();
            ddlcomdty.SelectedItem.Text = dr["Commodity_Name"].ToString();
            ddlcomdty.SelectedValue = dr["Commodity_Id"].ToString();
            ddlcropyear.SelectedItem.Text = dr["Crop_Year"].ToString();
            txtbagno.Text = dr["No_of_Bags"].ToString();
             txtbagno.ReadOnly = true;
            txtquant.Text = dr["Quantity"].ToString();
             txtquant.ReadOnly = true;
            txtrecdbags.Text = dr["Recd_Bags"].ToString();
            txtrecdqty.Text = dr["Recd_Qty"].ToString();
            lblrecbag.Text = dr["Recd_Bags"].ToString();
            lblrqty.Text = dr["Recd_Qty"].ToString();
            lblgid.Text = dr["Recd_Godown"].ToString();
            ddlgodown.SelectedValue = dr["Recd_Godown"].ToString();
           // ddlgodown.Enabled = false;
            DaintyDate3.Text = getdateg(dr["Recd_Date"].ToString());
            //ddlbranchwlc.SelectedItem.Text = dr["B"].ToString();
            ddlbranchwlc.SelectedValue = dr["Branch_Id"].ToString();
           // ddlbranchwlc.Enabled = false;

            //Getgon();
            getgonCap();
            //GetCapacity();
            //GetBalance();
            //txtbookno.Text = dr["Book_no"].ToString();
            //string mbook = dr["Book_no"].ToString();

            //string lenth = mbook.Length.ToString();
            //int n = 4;
            //int mlen = n+int.Parse(lenth);

            //string mascnote = dr["Acceptance_No"].ToString();
            //string asnlenth = mascnote.Length.ToString();
            //int index = int.Parse(asnlenth);
            //string masnoten = mascnote.Substring(mlen);
            ////txtaccptno.Text = masnoten;
            //txtaccptno.Text = mascnote;



            //DaintyDate2.SelectedDate  = Convert .ToDateTime (dr["Acceptance_Date"].ToString());
            GetDCName();

        }
        catch (Exception ex)
        { 
        
        }



    }
    //void GetCapacity()
    //{
    //    string gname = lblgid.Text;
    //    mobj = new MoveChallan(ComObj);
    //    string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did  + "' and DepotId='" + sid  + "' and Godown_ID='" + gname + "'";

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
    //}
    //void GetCapGodown()
    //{
    //    string gname = lblgid.Text;
    //    mobj = new MoveChallan(ComObj);
    //    string qrygdn = "SELECT Sum(Current_Balance) as Current_Balance  FROM dbo.issue_opening_balance where District_Id='" + did + "' and Depotid='" + sid + "' and Godown='" + gname + "'";

    //    DataSet ds = mobj.selectAny(qrygdn);
    //    if (ds == null)
    //    {
    //    }

    //    else
    //    {
    //        if (ds.Tables[0].Rows.Count == 0)
    //        {
    //            txtcurntcap.Text = "";

    //        }
    //        else
    //        {
    //            DataRow dr = ds.Tables[0].Rows[0];
    //            txtcurntcap.Text = (System.Math.Round(CheckNull(dr["Current_Balance"].ToString()), 5)).ToString();
    //            txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();
    //        }


    //    }

    //}
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
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        string qry = "Select * from tbl_MetaData_Storage_COmmodity where Commodity_Id in ('22','13','14','11','12','8','9')";
        DataSet ds = comdtobj.selectAny(qry);
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }
    void GetGodown()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + did + "' and DepotId='" + sid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }

    }
    void GetTransport()
    {
        tobj = new Transporter(ComObj);
        string qry = "SELECT Transporter_Name,Transporter_ID FROM dbo.Transporter_Table where Distt_ID='" + did + "' and IsActive='Y'";
        DataSet ds = tobj.selectAny(qry);
       
        ddltransporter.DataSource = ds.Tables[0];
        ddltransporter.DataTextField = "Transporter_Name";
        ddltransporter.DataValueField = "Transporter_ID";
        ddltransporter.DataBind();
        ddltransporter.Items.Insert(0, "--Select--");

    }
    void GetName()
    {
        mobj2 = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + did + "'";
        DataSet ds1dt = mobj2.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdist.Text = dr1dt["district_name"].ToString();

        txtdist.ReadOnly = true;
        txtdist.BackColor = System.Drawing.Color.Wheat;

        mobj2 = new MoveChallan(ComObj);
        string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + sid + "'";
        DataSet dsic = mobj2.selectAny(qryissue);
        DataRow dric = dsic.Tables[0].Rows[0];
        txtissue.Text = dric["DepotName"].ToString();

        txtissue.ReadOnly = true;
        txtissue.BackColor = System.Drawing.Color.Wheat;

        //ddldistrict.SelectedItem.Text = dr1dt["district_name"].ToString();
        //ddldistrict.SelectedValue = did;

        GetDCName();



    }

    void GetDCName()
    {
        string distcode = ddldistrict.SelectedValue;

        distobj = new DistributionCenters(ComObj);
        string ord = "DistrictId='23" + ddldistrict.SelectedValue.ToString() + "' order by PcId";
        DataSet ds = distobj.selectPC(ord);
        ddlissuecenter.DataSource = ds.Tables[0];
        ddlissuecenter.DataTextField = "PurchaseCenterName";
        ddlissuecenter.DataValueField = "PcId";

        ddlissuecenter.DataBind();
        ddlissuecenter.Items.Insert(0, "--Select--");
        
        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetICName()
    {

        distobj = new DistributionCenters(ComObj);
       
        string ord = "Districtid='23" + ddldistrict.SelectedValue + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissuecenter.DataSource = ds.Tables[0];
        ddlissuecenter.DataTextField = "DepotName";
        ddlissuecenter.DataValueField = "DepotId";
        ddlissuecenter.DataBind();
        ddlissuecenter.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();

    }
    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    void GetICDname()
    {
        tobj = new Transporter(ComObj);
        string qry = "Select * from dbo.tbl_MetaData_DEPOT where DistrictId='" + "23" + did + "'";

        DataSet ds = tobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];

        ddlissuecenter.SelectedItem.Text = dr["DepotName"].ToString();
        ddlissuecenter.SelectedValue = dr["DepotID"].ToString();

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        decimal ccap = CheckNull(txtcurntcap.Text);
        decimal rcap = CheckNull(txtrecdqty.Text);
        decimal chkcap = ccap + rcap;
        decimal maxcap= CheckNull (txtmaxcap.Text);

        if (chkcap > maxcap)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sorry Space is not available at Godown....'); </script> ");
        }
        else
        {

            if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlissuecenter.SelectedItem.Text == "--Select--" || ddltransporter.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Purchase Center /Transporter Name/Godown....'); </script> ");
            }
            else
            {

            # region prcwht
                if (ddlcomdty.SelectedValue.ToString() == "22")
                {
                    string selectc = "Select TC_Number  from SCSC_Procurement where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + txttrukcno.Text + "'";
                    mobj = new MoveChallan(ComObj);
                    DataSet dsgcc = mobj.selectAny(selectc);

                    //if (dsgcc.Tables[0].Rows.Count == 0)        // remove this to edit
                    //{

                        string smonth = "";
                        string syear = "";
                        string mcom = "";
                        string msch = ddlscheme.SelectedValue;
                        string mgodown = "";
                        string branch = "";
                        string mdchallan = "";
                        int month = int.Parse(DateTime.Today.Month.ToString());
                        int year = int.Parse(DateTime.Today.Year.ToString());
                        string qrygchallan = "Select * from dbo.SCSC_Procurement where Distt_ID='" + did + "' and TC_Number='" + challan + "' and IssueCenter_ID='" + sid + "'";
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
                            mcom = drgc["Commodity_Id"].ToString();
                            mgodown = drgc["Recd_Godown"].ToString();
                            mdchallan = drgc["TC_Number"].ToString();
                            branch = drgc["Branch_Id"].ToString();
                        }

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlTransaction trns;
                        cmd.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd.Transaction = trns;

                        if (con_WPMS.State == ConnectionState.Closed)
                        {
                            con_WPMS.Open();
                        }

                        SqlTransaction trns2;
                        cmdP.Connection = con_WPMS;
                        trns2 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdP.Transaction = trns2;

                        try
                        {

                            string uopen = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5), Current_Balance)-(" + CheckNull(lblrqty.Text) + "),Current_Bags=Current_Bags-(" + CheckNull(lblrecbag.Text) + ") where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcom + "'and Scheme_Id='" + msch + "' and Source='01' and Godown='" + mgodown + "' and Branch_Id='" + branch + "'";
                            cmd.CommandText = uopen;
                            //cmd.Connection = con;
                            cmd.ExecuteNonQuery();

                            decimal ruqty = CheckNull(txtrecdqty.Text);
                            decimal prqty = CheckNull(lblrqty.Text);
                            decimal uuqty = decimal.Parse((System.Math.Round(prqty, 5) - System.Math.Round(ruqty, 5)).ToString());

                            
                            string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Procure=Recieved_Procure-(" + prqty + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + smonth + "and Year=" + syear;
                            cmd.CommandText = qryinsU;
                            //con.Open();
                            cmd.ExecuteNonQuery();
                            // con.Close();


                            string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + smonth + "and Year=" + syear;
                            cmd.CommandText = qryinsopen;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet dsopen = new DataSet();
                            da.Fill(dsopen);

                            if (dsopen.Tables[0].Rows.Count == 0)
                            {
                                decimal mopening = 0;
                                string qryfind = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                cmd.CommandText = qryfind;
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                                DataSet dsf = new DataSet();
                                da1.Fill(dsf);
                                if (dsf.Tables[0].Rows.Count == 0)
                                {
                                    string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + ddlcomdty.SelectedValue + "' and Scheme_Id ='" + ddlscheme.SelectedValue + "'";
                                    cmd.CommandText = chkopenss;
                                    SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                                    DataSet dsqry = new DataSet();
                                    da2.Fill(dsqry);
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
                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "'," + mopening + "," + CheckNull(txtrecdqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + smonth + "," + syear + ",'')";
                                cmd.CommandText = qryinsr;
                                //con.Open();
                                cmd.ExecuteNonQuery();
                                // con.Close();
                            }
                            else
                            {
                                string qryustock = "update dbo.tbl_Stock_Registor set Recieved_Procure=Recieved_Procure+(" + CheckNull(txtrecdqty.Text) + ") where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + smonth + "and Year=" + syear;
                                cmd.CommandText = qryustock;
                                //con.Open();
                                cmd.ExecuteNonQuery();
                                //con.Close();


                            }


                            if (int.Parse(smonth.ToString()) == month)
                            {
                            }
                            else
                            {
                                string qrysstock = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                cmd.CommandText = qrysstock;
                                SqlDataAdapter da3 = new SqlDataAdapter(cmd);
                                DataSet dsstock = new DataSet();
                                da3.Fill(dsstock);

                                if (dsstock.Tables[0].Rows.Count == 0)
                                {


                                }
                                else
                                {
                                    //string qryupdate = "update dbo.tbl_Stock_Registor set Recieved_Procure=Recieved_Procure-(" + uuqty  + ") where Commodity_Id ='" + mcom + "'and  Scheme_ID='" + msch + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + smonth + "and Year=" + syear;
                                    //cmd.CommandText = qryupdate;
                                    //con.Open();
                                    //cmd.ExecuteNonQuery();
                                    //con.Close();

                                    string qrynew = "update dbo.tbl_Stock_Registor set Opening_Balance=Opening_Balance-(" + uuqty + ") where Commodity_Id ='" + ddlcomdty.SelectedValue + "'and  Scheme_ID='" + ddlscheme.SelectedValue + "' and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                    cmd.CommandText = qrynew;

                                    //con.Open();
                                    cmd.ExecuteNonQuery();
                                    //con.Close();
                                }

                            }


                            //# region prcwht
                            //if (ddlcomdty.SelectedValue.ToString() == "22")
                            //{
                            string mpcdist = ddldistrict.SelectedValue;
                            string mpcic = ddlissuecenter.SelectedValue;
                            string mdispdate = getDate_MDY(DaintyDate1.Text);
                            string mrecdate = getDate_MDY(DaintyDate3.Text);
                            string mchallan = txttrukcno.Text;
                            string mtruckno = txttruckno.Text;
                            string mtrans = ddltransporter.SelectedValue;
                            string mcomdty = ddlcomdty.SelectedValue;
                            string mcropy = ddlcropyear.SelectedItem.ToString();
                            int mbags = CheckNullInt(txtbagno.Text);
                            decimal mqty = CheckNull(txtquant.Text);
                            int mrecdbags = CheckNullInt(txtrecdbags.Text);
                            decimal mrecdqty = CheckNull(txtrecdqty.Text);
                            string macno = txtaccptno.Text;

                            string mfyear = DateTime.Today.Year.ToString();
                            string mbookno = txtbookno.Text;
                            string accpno = mfyear + mbookno + txtaccptno.Text;
                            string ipadd = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            //try
                            //{

                            //if (con.State == ConnectionState.Closed)
                            //{
                            //    con.Open();
                            //}
                            //SqlTransaction trns;
                            //cmd.Connection = con;
                            //trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                            //cmd.Transaction = trns;

                            //if (con_WPMS.State == ConnectionState.Closed)
                            //{
                            //    con_WPMS.Open();
                            //}
                            //SqlTransaction trns2;                  
                            //cmdP.Connection = con_WPMS;
                            //trns2 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                            //cmdP.Transaction = trns2;
                            //try
                            //{
                            string instr = "insert into SCSC_Procurement_dellog  select *  from SCSC_Procurement where  Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "' and Receipt_Id='" + recdID + "'";
                            cmd.CommandText = instr;
                            cmd.ExecuteNonQuery();

                            string qryUpdate = "update  dbo.SCSC_Procurement set Sending_District='" + mpcdist + "',Purchase_Center='" + mpcic + "',Dispatch_Date='" + mdispdate + "',TC_Number='" + mchallan + "',Truck_Number='" + mtruckno + "',Transporter_ID='" + mtrans + "',Commodity_Id='" + mcomdty + "',Crop_Year='" + mcropy + "',No_of_Bags=" + mbags + ",Quantity=" + mqty + ",Updates_Date=getdate(),Recd_Bags=" + mrecdbags + ",Recd_Qty=" + mrecdqty + ",Recd_Godown='" + ddlgodown.SelectedValue + "',Branch_Id='" + ddlbranchwlc.SelectedValue + "',Recd_Date='" + mrecdate + "',IP_Address='" + ipadd + "'  where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "' and Receipt_Id='" + recdID + "'";
                            cmd.CommandText = qryUpdate;


                            string insrtP = "INSERT INTO IssueCenterReceipt_Online_log select * from IssueCenterReceipt_Online where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "' and Receipt_Id='" + recdID + "'";
                            cmdP.CommandText = insrtP;
                            cmdP.ExecuteNonQuery();


                            string qryupdtp = "update dbo.IssueCenterReceipt_Online set Sending_District='23" + mpcdist + "',SocietyID='" + mpcic + "',DateOfIssue='" + mdispdate + "',TruckChalanNo='" + mchallan + "',TruckNo='" + mtruckno + "',TransporterId='" + mtrans + "',CommodityId='1',CropYear='" + mcropy + "',Bags='" + mbags + "',QtyTransffer='" + mqty + "',UpdatedDate=getdate(),Recv_Qty='" + mrecdqty + "',Recd_Godown='" + ddlgodown.SelectedValue + "',Branch_Id='" + ddlbranchwlc.SelectedValue + "',Recd_Date='" + mrecdate + "' where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "' and Receipt_Id='" + recdID + "'";
                            cmdP.CommandText = qryupdtp;


                            //con.Open();
                            cmd.ExecuteNonQuery();
                            cmdP.ExecuteNonQuery();
                            UpdateCBalance();
                            trns.Commit();
                            trns2.Commit();
                            con.Close();
                            con_WPMS.Close();
                            //UpdateStock();

                            Update_Trans_Log();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
                            btnsave.Enabled = false;


                        }
                        catch (Exception ex)
                        {
                            trns.Rollback();
                            trns2.Rollback();
                            Label9.Visible = true;
                            Label9.Text = ex.Message;

                        }
                        finally
                        {
                            ComObj.CloseConnection();
                            con.Close();
                            con_WPMS.Close();
                        }


                    //}
                    //else
                    //{ 
                    //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist....'); </script> "); 
                    //}
                }
                # endregion
               # region prPaddyCom
               //else if (ddlcomdty.SelectedValue.ToString() == "13")
               // {
               //     string mpcdist = ddldistrict.SelectedValue;
               //     string mpcic = ddlissuecenter.SelectedValue;
               //     string mdispdate = getDate_MDY(DaintyDate1.Text);
               //     string mrecdate = getDate_MDY(DaintyDate3.Text);
               //     string mchallan = txttrukcno.Text;
               //     string mtruckno = txttruckno.Text;
               //     string mtrans = ddltransporter.SelectedValue;
               //     string mcomdty = ddlcomdty.SelectedValue;
               //     string mcropy = ddlcropyear.SelectedItem.ToString();
               //     int mbags = CheckNullInt(txtbagno.Text);
               //     decimal mqty = CheckNull(txtquant.Text);
               //     int mrecdbags = CheckNullInt(txtrecdbags.Text);
               //     decimal mrecdqty = CheckNull(txtrecdqty.Text);
               //     string macno = txtaccptno.Text;

               //     string mfyear = DateTime.Today.Year.ToString();
               //     string mbookno = txtbookno.Text;
               //     string accpno = mfyear + mbookno + txtaccptno.Text;
                   
               //     SqlTransaction trns;
               //     con.Open();
               //     trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmd.Transaction = trns;
               //     SqlTransaction trns2;
               //     con_paddy.Open();
               //     trns2 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmdP.Transaction = trns2;

               //     string qryUpdate = "update  dbo.SCSC_Procurement set Sending_District='" + mpcdist + "',Purchase_Center='" + mpcic + "',Dispatch_Date='" + mdispdate + "',Truck_Number='" + mtruckno + "',Transporter_ID='" + mtrans + "',Commodity_Id='" + mcomdty + "',Crop_Year='" + mcropy + "',No_of_Bags=" + mbags + ",Quantity=" + mqty + ",Updates_Date=getdate(),Recd_Bags=" + mrecdbags + ",Recd_Qty=" + mrecdqty + ",Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "'  where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "'";
               //     cmd.CommandText = qryUpdate;
               //     cmd.Connection = con;

               //     string qryupdtp = "update dbo.IssueCenterReceipt_Online set Sending_District='23" + mpcdist + "',SocietyID='" + mpcic + "',DateOfIssue='" + mdispdate + "',TruckNo='" + mtruckno + "',TransporterId='" + mtrans + "',CommodityId='2',CropYear='" + mcropy + "',Bags='" + mbags + "',QtyTransffer='" + mqty + "',UpdatedDate=getdate(),Recv_Qty='" + mrecdqty + "',Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "' where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "'";
               //     cmdP.CommandText = qryupdtp;
               //     cmdP.Connection = con_paddy;
               //     try
               //     {
               //         //con.Open();
               //         cmd.ExecuteNonQuery();
               //         cmdP.ExecuteNonQuery();
               //         trns.Commit();
               //         trns2.Commit();
               //         con.Close();
               //         con_paddy.Close();
               //         //UpdateStock();
               //         UpdateCBalance();
               //         Update_Trans_Log();
               //         Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
               //         btnsave.Enabled = false;
               //     }
               //     catch (Exception ex)
               //     {
               //         trns.Rollback();
               //         trns2.Rollback();
               //         Label9.Visible = true;
               //         Label9.Text = ex.Message;

               //     }
               //     finally
               //     {
               //         ComObj.CloseConnection();
               //         con.Close();
               //         con_paddy.Close();
               //     }


               // }
                # endregion
                # region prPaddyGA
               // else if (ddlcomdty.SelectedValue.ToString() == "14")
               // {
               //     string mpcdist = ddldistrict.SelectedValue;
               //     string mpcic = ddlissuecenter.SelectedValue;
               //     string mdispdate = getDate_MDY(DaintyDate1.Text);
               //     string mrecdate = getDate_MDY(DaintyDate3.Text);
               //     string mchallan = txttrukcno.Text;
               //     string mtruckno = txttruckno.Text;
               //     string mtrans = ddltransporter.SelectedValue;
               //     string mcomdty = ddlcomdty.SelectedValue;
               //     string mcropy = ddlcropyear.SelectedItem.ToString();
               //     int mbags = CheckNullInt(txtbagno.Text);
               //     decimal mqty = CheckNull(txtquant.Text);
               //     int mrecdbags = CheckNullInt(txtrecdbags.Text);
               //     decimal mrecdqty = CheckNull(txtrecdqty.Text);
               //     string macno = txtaccptno.Text;

               //     string mfyear = DateTime.Today.Year.ToString();
               //     string mbookno = txtbookno.Text;
               //     string accpno = mfyear + mbookno + txtaccptno.Text;

               //     SqlTransaction trns;
               //     con.Open();
               //     trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmd.Transaction = trns;
               //     SqlTransaction trns2;
               //     con_paddy.Open();
               //     trns2 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmdP.Transaction = trns2;

               //     string qryUpdate = "update  dbo.SCSC_Procurement set Sending_District='" + mpcdist + "',Purchase_Center='" + mpcic + "',Dispatch_Date='" + mdispdate + "',Truck_Number='" + mtruckno + "',Transporter_ID='" + mtrans + "',Commodity_Id='" + mcomdty + "',Crop_Year='" + mcropy + "',No_of_Bags=" + mbags + ",Quantity=" + mqty + ",Updates_Date=getdate(),Recd_Bags=" + mrecdbags + ",Recd_Qty=" + mrecdqty + ",Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "'  where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "'";
               //     cmd.CommandText = qryUpdate;
               //     cmd.Connection = con;

               //     string qryupdtp = "update dbo.IssueCenterReceipt_Online set Sending_District='23" + mpcdist + "',SocietyID='" + mpcic + "',DateOfIssue='" + mdispdate + "',TruckNo='" + mtruckno + "',TransporterId='" + mtrans + "',CommodityId='3',CropYear='" + mcropy + "',Bags='" + mbags + "',QtyTransffer='" + mqty + "',UpdatedDate=getdate(),Recv_Qty='" + mrecdqty + "',Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "' where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "'";
               //     cmdP.CommandText = qryupdtp;
               //     cmdP.Connection = con_paddy;
               //     try
               //     {
               //         //con.Open();
               //         cmd.ExecuteNonQuery();
               //         cmdP.ExecuteNonQuery();
               //         trns.Commit();
               //         trns2.Commit();
               //         con.Close();
               //         con_paddy.Close();
               //         //UpdateStock();
               //         UpdateCBalance();
               //         Update_Trans_Log();
               //         Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
               //         btnsave.Enabled = false;
               //     }
               //     catch (Exception ex)
               //     {
               //         trns.Rollback();
               //         trns2.Rollback();
               //         Label9.Visible = true;
               //         Label9.Text = ex.Message;

               //     }
               //     finally
               //     {
               //         ComObj.CloseConnection();
               //         con.Close();
               //         con_paddy.Close();
               //     }


               // }
                # endregion
                # region prJwr
               // else if (ddlcomdty.SelectedValue.ToString() == "11")
               // {
               //     string mpcdist = ddldistrict.SelectedValue;
               //     string mpcic = ddlissuecenter.SelectedValue;
               //     string mdispdate = getDate_MDY(DaintyDate1.Text);
               //     string mrecdate = getDate_MDY(DaintyDate3.Text);
               //     string mchallan = txttrukcno.Text;
               //     string mtruckno = txttruckno.Text;
               //     string mtrans = ddltransporter.SelectedValue;
               //     string mcomdty = ddlcomdty.SelectedValue;
               //     string mcropy = ddlcropyear.SelectedItem.ToString();
               //     int mbags = CheckNullInt(txtbagno.Text);
               //     decimal mqty = CheckNull(txtquant.Text);
               //     int mrecdbags = CheckNullInt(txtrecdbags.Text);
               //     decimal mrecdqty = CheckNull(txtrecdqty.Text);
               //     string macno = txtaccptno.Text;

               //     string mfyear = DateTime.Today.Year.ToString();
               //     string mbookno = txtbookno.Text;
               //     string accpno = mfyear + mbookno + txtaccptno.Text;

               //     SqlTransaction trns;
               //     con.Open();
               //     trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmd.Transaction = trns;
               //     SqlTransaction trns2;
               //     con_Maze.Open();
               //     trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmdP.Transaction = trns2;

               //     string qryUpdate = "update  dbo.SCSC_Procurement set Sending_District='" + mpcdist + "',Purchase_Center='" + mpcic + "',Dispatch_Date='" + mdispdate + "',Truck_Number='" + mtruckno + "',Transporter_ID='" + mtrans + "',Commodity_Id='" + mcomdty + "',Crop_Year='" + mcropy + "',No_of_Bags=" + mbags + ",Quantity=" + mqty + ",Updates_Date=getdate(),Recd_Bags=" + mrecdbags + ",Recd_Qty=" + mrecdqty + ",Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "'  where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "'";
               //     cmd.CommandText = qryUpdate;
               //     cmd.Connection = con;

               //     string qryupdtp = "update dbo.IssueCenterReceipt_Online set Sending_District='23" + mpcdist + "',SocietyID='" + mpcic + "',DateOfIssue='" + mdispdate + "',TruckNo='" + mtruckno + "',TransporterId='" + mtrans + "',CommodityId='4',CropYear='" + mcropy + "',Bags='" + mbags + "',QtyTransffer='" + mqty + "',UpdatedDate=getdate(),Recv_Qty='" + mrecdqty + "',Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "' where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "'";
               //     cmdP.CommandText = qryupdtp;
               //     cmdP.Connection = con_Maze;
               //     try
               //     {
               //         //con.Open();
               //         cmd.ExecuteNonQuery();
               //         cmdP.ExecuteNonQuery();
               //         trns.Commit();
               //         trns2.Commit();
               //         con.Close();
               //         con_Maze.Close();
               //         //UpdateStock();
               //         UpdateCBalance();
               //         Update_Trans_Log();
               //         Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
               //         btnsave.Enabled = false;
               //     }
               //     catch (Exception ex)
               //     {
               //         trns.Rollback();
               //         trns2.Rollback();
               //         Label9.Visible = true;
               //         Label9.Text = ex.Message;

               //     }
               //     finally
               //     {
               //         ComObj.CloseConnection();
               //         con.Close();
               //         con_Maze.Close();
               //     }


               // }
                # endregion
                # region prMaze
               // else if (ddlcomdty.SelectedValue.ToString() == "12")
               // {
               //     string mpcdist = ddldistrict.SelectedValue;
               //     string mpcic = ddlissuecenter.SelectedValue;
               //     string mdispdate = getDate_MDY(DaintyDate1.Text);
               //     string mrecdate = getDate_MDY(DaintyDate3.Text);
               //     string mchallan = txttrukcno.Text;
               //     string mtruckno = txttruckno.Text;
               //     string mtrans = ddltransporter.SelectedValue;
               //     string mcomdty = ddlcomdty.SelectedValue;
               //     string mcropy = ddlcropyear.SelectedItem.ToString();
               //     int mbags = CheckNullInt(txtbagno.Text);
               //     decimal mqty = CheckNull(txtquant.Text);
               //     int mrecdbags = CheckNullInt(txtrecdbags.Text);
               //     decimal mrecdqty = CheckNull(txtrecdqty.Text);
               //     string macno = txtaccptno.Text;

               //     string mfyear = DateTime.Today.Year.ToString();
               //     string mbookno = txtbookno.Text;
               //     string accpno = mfyear + mbookno + txtaccptno.Text;

               //     SqlTransaction trns;
               //     con.Open();
               //     trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmd.Transaction = trns;
               //     SqlTransaction trns2;
               //     con_Maze.Open();
               //     trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmdP.Transaction = trns2;

               //     string qryUpdate = "update  dbo.SCSC_Procurement set Sending_District='" + mpcdist + "',Purchase_Center='" + mpcic + "',Dispatch_Date='" + mdispdate + "',Truck_Number='" + mtruckno + "',Transporter_ID='" + mtrans + "',Commodity_Id='" + mcomdty + "',Crop_Year='" + mcropy + "',No_of_Bags=" + mbags + ",Quantity=" + mqty + ",Updates_Date=getdate(),Recd_Bags=" + mrecdbags + ",Recd_Qty=" + mrecdqty + ",Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "'  where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "'";
               //     cmd.CommandText = qryUpdate;
               //     cmd.Connection = con;

               //     string qryupdtp = "update dbo.IssueCenterReceipt_Online set Sending_District='23" + mpcdist + "',SocietyID='" + mpcic + "',DateOfIssue='" + mdispdate + "',TruckNo='" + mtruckno + "',TransporterId='" + mtrans + "',CommodityId='5',CropYear='" + mcropy + "',Bags='" + mbags + "',QtyTransffer='" + mqty + "',UpdatedDate=getdate(),Recv_Qty='" + mrecdqty + "',Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "' where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "'";
               //     cmdP.CommandText = qryupdtp;
               //     cmdP.Connection = con_Maze;
               //     try
               //     {
               //         //con.Open();
               //         cmd.ExecuteNonQuery();
               //         cmdP.ExecuteNonQuery();
               //         trns.Commit();
               //         trns2.Commit();
               //         con.Close();
               //         con_Maze.Close();
               //         //UpdateStock();
               //         UpdateCBalance();
               //         Update_Trans_Log();
               //         Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
               //         btnsave.Enabled = false;
               //     }
               //     catch (Exception ex)
               //     {
               //         trns.Rollback();
               //         trns2.Rollback();
               //         Label9.Visible = true;
               //         Label9.Text = ex.Message;

               //     }
               //     finally
               //     {
               //         ComObj.CloseConnection();
               //         con.Close();
               //         con_Maze.Close();
               //     }


               // }
                # endregion
                # region prBajra
               // else if (ddlcomdty.SelectedValue.ToString() == "8")
               // {
               //     string mpcdist = ddldistrict.SelectedValue;
               //     string mpcic = ddlissuecenter.SelectedValue;
               //     string mdispdate = getDate_MDY(DaintyDate1.Text);
               //     string mrecdate = getDate_MDY(DaintyDate3.Text);
               //     string mchallan = txttrukcno.Text;
               //     string mtruckno = txttruckno.Text;
               //     string mtrans = ddltransporter.SelectedValue;
               //     string mcomdty = ddlcomdty.SelectedValue;
               //     string mcropy = ddlcropyear.SelectedItem.ToString();
               //     int mbags = CheckNullInt(txtbagno.Text);
               //     decimal mqty = CheckNull(txtquant.Text);
               //     int mrecdbags = CheckNullInt(txtrecdbags.Text);
               //     decimal mrecdqty = CheckNull(txtrecdqty.Text);
               //     string macno = txtaccptno.Text;

               //     string mfyear = DateTime.Today.Year.ToString();
               //     string mbookno = txtbookno.Text;
               //     string accpno = mfyear + mbookno + txtaccptno.Text;

               //     SqlTransaction trns;
               //     con.Open();
               //     trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmd.Transaction = trns;
               //     SqlTransaction trns2;
               //     con_Maze.Open();
               //     trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmdP.Transaction = trns2;

               //     string qryUpdate = "update  dbo.SCSC_Procurement set Sending_District='" + mpcdist + "',Purchase_Center='" + mpcic + "',Dispatch_Date='" + mdispdate + "',Truck_Number='" + mtruckno + "',Transporter_ID='" + mtrans + "',Commodity_Id='" + mcomdty + "',Crop_Year='" + mcropy + "',No_of_Bags=" + mbags + ",Quantity=" + mqty + ",Updates_Date=getdate(),Recd_Bags=" + mrecdbags + ",Recd_Qty=" + mrecdqty + ",Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "'  where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "'";
               //     cmd.CommandText = qryUpdate;
               //     cmd.Connection = con;

               //     string qryupdtp = "update dbo.IssueCenterReceipt_Online set Sending_District='23" + mpcdist + "',SocietyID='" + mpcic + "',DateOfIssue='" + mdispdate + "',TruckNo='" + mtruckno + "',TransporterId='" + mtrans + "',CommodityId='6',CropYear='" + mcropy + "',Bags='" + mbags + "',QtyTransffer='" + mqty + "',UpdatedDate=getdate(),Recv_Qty='" + mrecdqty + "',Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "' where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "'";
               //     cmdP.CommandText = qryupdtp;
               //     cmdP.Connection = con_Maze;
               //     try
               //     {
               //         //con.Open();
               //         cmd.ExecuteNonQuery();
               //         cmdP.ExecuteNonQuery();
               //         trns.Commit();
               //         trns2.Commit();
               //         con.Close();
               //         con_Maze.Close();
               //         //UpdateStock();
               //         UpdateCBalance();
               //         Update_Trans_Log();
               //         Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
               //         btnsave.Enabled = false;
               //     }
               //     catch (Exception ex)
               //     {
               //         trns.Rollback();
               //         trns2.Rollback();
               //         Label9.Visible = true;
               //         Label9.Text = ex.Message;

               //     }
               //     finally
               //     {
               //         ComObj.CloseConnection();
               //         con.Close();
               //         con_Maze.Close();
               //     }


               // }
                # endregion
                # region prBarly
               // else if (ddlcomdty.SelectedValue.ToString() == "9")
               // {
               //     string mpcdist = ddldistrict.SelectedValue;
               //     string mpcic = ddlissuecenter.SelectedValue;
               //     string mdispdate = getDate_MDY(DaintyDate1.Text);
               //     string mrecdate = getDate_MDY(DaintyDate3.Text);
               //     string mchallan = txttrukcno.Text;
               //     string mtruckno = txttruckno.Text;
               //     string mtrans = ddltransporter.SelectedValue;
               //     string mcomdty = ddlcomdty.SelectedValue;
               //     string mcropy = ddlcropyear.SelectedItem.ToString();
               //     int mbags = CheckNullInt(txtbagno.Text);
               //     decimal mqty = CheckNull(txtquant.Text);
               //     int mrecdbags = CheckNullInt(txtrecdbags.Text);
               //     decimal mrecdqty = CheckNull(txtrecdqty.Text);
               //     string macno = txtaccptno.Text;

               //     string mfyear = DateTime.Today.Year.ToString();
               //     string mbookno = txtbookno.Text;
               //     string accpno = mfyear + mbookno + txtaccptno.Text;

               //     SqlTransaction trns;
               //     con.Open();
               //     trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmd.Transaction = trns;
               //     SqlTransaction trns2;
               //     con_Maze.Open();
               //     trns2 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
               //     cmdP.Transaction = trns2;

               //     string qryUpdate = "update  dbo.SCSC_Procurement set Sending_District='" + mpcdist + "',Purchase_Center='" + mpcic + "',Dispatch_Date='" + mdispdate + "',Truck_Number='" + mtruckno + "',Transporter_ID='" + mtrans + "',Commodity_Id='" + mcomdty + "',Crop_Year='" + mcropy + "',No_of_Bags=" + mbags + ",Quantity=" + mqty + ",Updates_Date=getdate(),Recd_Bags=" + mrecdbags + ",Recd_Qty=" + mrecdqty + ",Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "'  where Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "' and TC_Number='" + challan + "'";
               //     cmd.CommandText = qryUpdate;
               //     cmd.Connection = con;

               //     string qryupdtp = "update dbo.IssueCenterReceipt_Online set Sending_District='23" + mpcdist + "',SocietyID='" + mpcic + "',DateOfIssue='" + mdispdate + "',TruckNo='" + mtruckno + "',TransporterId='" + mtrans + "',CommodityId='7',CropYear='" + mcropy + "',Bags='" + mbags + "',QtyTransffer='" + mqty + "',UpdatedDate=getdate(),Recv_Qty='" + mrecdqty + "',Recd_Godown='" + mgodown + "',Branch_Id='" + branch + "',Recd_Date='" + mrecdate + "' where DistrictId='23" + did + "' and IssueCenter_ID='" + sid + "' and TruckChalanNo='" + challan + "'";
               //     cmdP.CommandText = qryupdtp;
               //     cmdP.Connection = con_Maze;
               //     try
               //     {
               //         //con.Open();
               //         cmd.ExecuteNonQuery();
               //         cmdP.ExecuteNonQuery();
               //         trns.Commit();
               //         trns2.Commit();
               //         con.Close();
               //         con_Maze.Close();
               //         //UpdateStock();
               //         UpdateCBalance();
               //         Update_Trans_Log();
               //         Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
               //         btnsave.Enabled = false;
               //     }
               //     catch (Exception ex)
               //     {
               //         trns.Rollback();
               //         trns2.Rollback();
               //         Label9.Visible = true;
               //         Label9.Text = ex.Message;

               //     }
               //     finally
               //     {
               //         ComObj.CloseConnection();
               //         con.Close();
               //         con_Maze.Close();
               //     }


               // }
               # endregion




            }
        }
    }
    void UpdateStock()
    {

        string mcomdtyu = ddlcomdty.SelectedValue;


        string mfyear = DateTime.Today.Year.ToString();
        string mbookno = txtbookno.Text;

        int monthu = int.Parse(DateTime.Today.Month.ToString());
        int yearu = int.Parse(DateTime.Today.Year.ToString());
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string qrystock = "select Sum(Quantity) as Qty from dbo.SCSC_Procurement where Commodity_Id ='" + mcomdtyu + "'and Distt_ID='" + did + "'and IssueCenter_ID='" + sid + "'and Month=" + monthu + "and Year=" + yearu;
        mobj = new MoveChallan(ComObj);
        DataSet dspro = mobj.selectAny(qrystock);
        if (dspro.Tables[0].Rows.Count == 0)
        {

        }
        else
        {
            DataRow drop = dspro.Tables[0].Rows[0];
            decimal mobal = 0;
            decimal mrp = CheckNull(drop["Qty"].ToString());
            decimal mrod = 0;
            decimal msod = 0;
            decimal msdelo = 0;
            decimal mrfci = 0;
            decimal mros = 0;
            decimal msos = 0;
            string mremark = "";
            string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdtyu + "'and DistrictId ='" + did + "'and DepotID='" + sid + "'and Month=" + monthu + "and Year=" + yearu;
            mobj = new MoveChallan(ComObj);
            DataSet dsopen = mobj.selectAny(qryinsopen);

            if (dsopen.Tables[0].Rows.Count == 0)
            {
                string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + did + "','" + sid + "','" + mcomdtyu + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + mros + "," + msdelo + "," + msod + "," + msos + "," + monthu + "," + yearu + ",'" + mremark + "')";
                cmd.CommandText = qryins;
                cmd.Connection = con;
                try
                {

                    cmd.ExecuteNonQuery();
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
            else
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Procure=" + mrp + " where Commodity_Id ='" + mcomdtyu + "'and DistrictId='" + did + "'and DepotID='" + sid + "'and Month=" + monthu + "and Year=" + yearu;
                cmd.CommandText = qryinsU;
                cmd.Connection = con;
                try
                {

                    cmd.ExecuteNonQuery();
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
    void Update_Trans_Log()
    {
        string opid = Session["OperatorId"].ToString();
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //DateTime my1 = Convert.ToDateTime(DaintyDate1.Text);
        //DateTime my3 = Convert.ToDateTime(DaintyDate3.Text);
        int my3month = int.Parse(DateTime.Today.Month.ToString());
        int my3year = int.Parse(DateTime.Today.Year.ToString());

        string qryinsert = "insert into dbo.SCSC_Procurement_Trans_Log(Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Updates_Date,IP_Address,Operation,OperatorID)values('" + did + "','" + sid + "','" + ddldistrict.SelectedValue + "','" + ddlissuecenter.SelectedValue + "','" + getDate_MDY(DaintyDate1.Text) + "','" + txttrukcno.Text + "','" + txttruckno.Text + "','" + ddltransporter.SelectedValue + "','" + ddlcomdty.SelectedValue + "','" + ddlcropyear.SelectedValue + "'," + CheckNullInt(txtbagno.Text) + "," + CheckNull(txtquant.Text) + "," + CheckNullInt(txtrecdbags.Text) + "," + CheckNull(txtrecdqty.Text) + ",'" + getDate_MDY(DaintyDate3.Text) + "','" + ddlgodown.SelectedValue + "','" + recdID + "'," + my3month + "," + my3year + ",getdate(),'" + ip + "','U','" + opid + "')";

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
            
        }
        finally
        {
        }


    }
    void UpdateCBalance()
    {

        decimal ruqty =   CheckNull(txtrecdqty.Text)-CheckNull (lblrqty.Text);
        decimal buqty = CheckNull(txtbalqty.Text);
        decimal uuqty = (ruqty + buqty);

        int rubags=CheckNullInt (txtrecdbags.Text)-CheckNullInt (lblrecbag.Text);

        string comdtyid = ddlcomdty.SelectedValue;
        string godown = ddlgodown.SelectedValue;
        string source = "01";
        string schemeid = ddlscheme.SelectedValue;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string query = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5), Current_Balance)+(" + CheckNull(txtrecdqty.Text) + "),Current_Bags=Current_Bags+(" + CheckNullInt(txtrecdbags.Text) + ") where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Godown='" + godown + "' and Scheme_Id='" + schemeid + "' and Source='01'";
        cmd.CommandText = query;
        //cmd.Connection = con;

        //try
        //{
            //con.Open();
            cmd.ExecuteNonQuery();
            //con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Label1.Text = ex.Message;

        //}
        //finally
        //{
        //    //con.Close();

        //}
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

    }
    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acceptance_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT SCSC_Procurement.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name  FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id where IssueCenter_ID='" + sid + "'";
        DataSet ds = mobj.selectAny(qry);
        dgridchallan.DataSource = ds.Tables[0];
        dgridchallan.DataBind();


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
        fillgrid();
    }
   
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblgid.Text = ddlgodown.SelectedValue;
    //    GetCapacity();
    //    GetBalance();
    //}

    void getgonCap()
    {
        try
        {
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
                        txtcurntcap.Text = ds.Tables[0].Rows[0]["depositmsp"].ToString();
                        txtavalcap.Text = ds.Tables[0].Rows[0]["vacientcap"].ToString();

                    }
                }

            }
            else
            {
            }
            GetBalance();
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
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {

        getgonCap();
    }
    void GetBalance()
    {
        string mcomid = ddlcomdty.SelectedValue;
        //string mcatid = ddlcategory.SelectedValue;
        //string mscheme = "4";
        string godownid = ddlgodown.SelectedValue;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string source = "01";
        mobj1 = new MoveChallan(ComObj);

        string qry = "Select Sum(Current_Balance)as Current_Balance from dbo.issue_opening_balance where District_Id='" + did + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Source='" + source + "'and Godown='" + godownid + "'";
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
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlbranchwlc_SelectedIndexChanged(object sender, EventArgs e)
    {

        Getgon();
        txtmaxcap.Text = "";
        txtcurntcap.Text = "";
        txtavalcap.Text = "";
        txthhty.Text = "";
        txtbalqty.Text = "";
    }
}
