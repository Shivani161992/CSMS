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
public partial class District_RO_Edit : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Districts DObj = null;
    MoveChallan mobj2 = null;
    chksql chk = null;

    protected Common ComObj = null, cmn = null;
    ROFCI roobj = null;
    public string distid = "";
    public string Ro_No = "";
    public string Lift_st = "";
    public string version = "";
    //public string islifted = "N";
    public string rodist = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["dist_id"] != null)
        {
            Ro_No = Session["Ro_No"].ToString();
            distid = Session["dist_id"].ToString();
            Lift_st = Session["Lifts"].ToString();
            version = Session["hindi"].ToString();
            rodist = Session["ROdist"].ToString();
           ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        

          txtroqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
         txtrate.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
         txtamount.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
         txtchqamt.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

         txtrono.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
         txtrono.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtrono.Attributes.Add("onchange", "return chksqltxt(this)");

         txtrate.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
         txtrate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtrate.Attributes.Add("onchange", "return chksqltxt(this)");

         txtckeckno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
         txtckeckno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtckeckno.Attributes.Add("onchange", "return chksqltxt(this)");

         txtchqamt.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
         txtchqamt.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtchqamt.Attributes.Add("onchange", "return chksqltxt(this)");


         txtremark.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
         txtremark.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtremark.Attributes.Add("onchange", "return chksqltxt(this)");

         txtrodate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
         txtrovalidity.Attributes.Add("onkeypress", "return CheckCalDate(this)");
         txtddate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
         txtrodate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtrodate.Attributes.Add("onchange", "return chksqltxt(this)");

         txtrovalidity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtrovalidity.Attributes.Add("onchange", "return chksqltxt(this)");

         txtddate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
         txtddate.Attributes.Add("onchange", "return chksqltxt(this)");




         txtrono.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         txtrate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         txtamount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         txtckeckno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         txtchqamt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         chk = new chksql();
         ArrayList ctrllist = new ArrayList();
         ctrllist.Add(txtrono.Text);
         ctrllist.Add(txtrate.Text);
         ctrllist.Add(txtckeckno.Text);
         ctrllist.Add(txtchqamt.Text);
         ctrllist.Add(txtremark.Text);

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
            ddldistrict.SelectedValue = rodist;
            txtddate.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            txtrovalidity.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            txtrodate.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

            GetDist();
            GetCommodity();
            GetScheme();
            GetName();
            
            GetBank();
            ddlallot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddlallot_year.Items.Add(DateTime.Today.Year.ToString());
            ddlallot_year.SelectedIndex = 1;
            ddlalotmm.SelectedIndex = DateTime.Today.Month - 1;
            GetData();


            if (version == "H")
            {

                lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                lblScheme.Text = Resources.LocalizedText.lblScheme;
                lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                lblReleaseOrder.Text = Resources.LocalizedText.lblDetailsofRO;
                btnsave.Text = Resources.LocalizedText.btnsave;
                btnclose.Text = Resources.LocalizedText.btnclose;
                lblallotm.Text = Resources.LocalizedText.lblallotm;
                lblyear.Text = Resources.LocalizedText.lblyear;
                lblRateQuintal.Text = Resources.LocalizedText.lblRateQuintal;
                lblPaymentMode.Text = Resources.LocalizedText.lblPaymentMode;
                lblddchekno.Text = Resources.LocalizedText.lblddchekno;
                lblddchekdate.Text = Resources.LocalizedText.lblddchekdate;
                lblBankName.Text = Resources.LocalizedText.lblBankName;
                lblamount.Text = Resources.LocalizedText.lblamount;
                lblamountdd.Text = Resources.LocalizedText.lblamountdd;
                lblforDist.Text = Resources.LocalizedText.lblforDist;
                lblReleaseOrderDate.Text = Resources.LocalizedText.lblReleaseOrderDate;
                lbldovalidity.Text = Resources.LocalizedText.lbldovalidity;
                lblRemark.Text = Resources.LocalizedText.lblRemark;
                lblReleaseOrderDate.Text = Resources.LocalizedText.lblReleaseOrderDate;
                lblqty.Text = Resources.LocalizedText.lblqty;
                lbl_balqty.Text = Resources.LocalizedText.lbl_balqty;
                btnupdateqty.Text = Resources.LocalizedText.btnupdateqty;

            }



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
    void GetCommodity()
    {
        ddlcomdty.Items.Clear();
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");

        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


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
    void GetBank()
    {
        mobj2 = new MoveChallan(ComObj);
        string qrybank = "select Bank_ID,Bank_Name  from dbo.Bank_Master where district_code='" + distid + "' and issueCenter_code='" + distid + "'";
        DataSet dsb = mobj2.selectAny(qrybank);
        ddlbankname.DataSource = dsb.Tables[0];

        ddlbankname.DataTextField = "Bank_Name";
        ddlbankname.DataValueField = "Bank_ID";
        ddlbankname.DataBind();
        ListItem lst = new ListItem();
        lst.Text = "--Select--";
        lst.Value = "NA";
        ddlbankname.Items.Insert(0, lst);

    }
    void GetName()
    {
        mobj2 = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj2.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        lbldistrict.Text = dr1dt["district_name"].ToString();




    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd-MM-yyyy");
    }
    void GetData()
    {
        string qryro = "Select RO_of_FCI.* ,Bank_Master.Bank_Name as Bank_Name  from dbo.RO_of_FCI left join dbo.Bank_Master on RO_of_FCI.Bank_ID =Bank_Master.Bank_ID  where RO_of_FCI.RO_No='" + Ro_No + "'and RO_of_FCI.Distt_Id='" + distid + "' and RO_district='"+ddldistrict.SelectedValue+"'";
        DataSet dsro = mobj2.selectAny(qryro);
        DataRow drro = dsro.Tables[0].Rows[0];
        txtrono.Text = Ro_No;
        txtrono.Enabled = false;
        //txtrono.BackColor = System.Drawing.Color.Wheat;
        ddlcomdty.SelectedItem.Text = drro["Commodity"].ToString();
        string cmdty = drro["Commodity"].ToString();
        lblcmdty.Text = drro["Commodity"].ToString();
        ddlscheme.SelectedItem.Text = drro["Scheme"].ToString();
        string sche = drro["Scheme"].ToString();
        lblsch.Text = drro["Scheme"].ToString();
        //ddlbankname.SelectedValue = drro["Bank_ID"].ToString();
        //ddlbankname.SelectedItem.Text = drro["Bank_Name"].ToString();
        txtroqty.Text = System.Math.Round(CheckNull(drro["RO_qty"].ToString()), 5).ToString();
        lblroqty.Text = System.Math.Round(CheckNull(drro["RO_qty"].ToString()), 5).ToString();
        //ddldistrict.SelectedItem.Text = drro["RO_district"].ToString();
        lbldist.Text = rodist ;
        txtrodate.Text = getdate(drro["RO_date"].ToString());
        txtckeckno.Text = drro["DD_chk_no"].ToString();
        txtddate.Text = getdate(drro["DD_chk_date"].ToString());
        ddlalotmm.SelectedItem.Text = drro["Allot_month"].ToString();
        ddlbankname.SelectedItem.Text = drro["Bank_Name"].ToString();
        ddlbankname.SelectedValue = drro["Bank_ID"].ToString();
        txtchqamt.Text = System.Math.Round(CheckNull(drro["DD_chk_Amount"].ToString()), 2).ToString();
        //txtrovalidty.Text = drro["RO_Validity"].ToString();
        txtrovalidity.Text = getdate(drro["RO_Validity"].ToString());
        lblmonth.Text = drro["Allot_month"].ToString();
        int month = int.Parse(drro["Allot_month"].ToString());
        mobj2 = new MoveChallan(ComObj);
        string qryd = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet dsd = mobj2.selectAny(qryd);
        DataRow drd = dsd.Tables[0].Rows[0];
        //ddldistrict.SelectedItem.Text = drd["district_name"].ToString();
        //ddldistrict.SelectedValue = drro["RO_district"].ToString();

        
        mobj2 = new MoveChallan(ComObj);
        string qryc = "select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + cmdty + "'";
        DataSet dsc = mobj2.selectAny(qryc);
        DataRow drc = dsc.Tables[0].Rows[0];
        ddlcomdty.SelectedItem.Text = drc["Commodity_Name"].ToString();
        ddlcomdty.SelectedValue = drro["Commodity"].ToString();

        mobj2 = new MoveChallan(ComObj);
        string qrys = "select Scheme_Name from dbo.tbl_MetaData_SCHEME where Scheme_Id='" + sche+ "'";
        DataSet dss = mobj2.selectAny(qrys);
        DataRow drs = dss.Tables[0].Rows[0];
        
        ddlscheme.SelectedItem.Text = drs["Scheme_Name"].ToString();
        ddlscheme.SelectedValue = drro["Scheme"].ToString();

        ddlalotmm.SelectedItem.Text = GetMonthName(month,false);
        ddlalotmm.SelectedValue = drro["Allot_month"].ToString();
        //GetRate();
        txtrate.Text = drro["Rate"].ToString();
        float amt = (CheckNull(txtrate.Text) * CheckNull(txtroqty.Text));

         txtamount.Text = amt.ToString();
         GetAllotment();
         GetBalanceQty();

         string paytype = drro["Payment_Mode"].ToString();
         if (paytype.Trim () == "F")
         {
             lblBankName.Visible = false;
             ddlbankname.Visible = false;
             lblddchekno.Visible = false;
             txtckeckno.Visible = false;
             lblddchekdate.Visible = false;
             txtddate.Visible = false;
             lblamountdd.Visible = false;
             txtchqamt.Visible = false;
            
             txtchqamt.Text = "0";
             txtckeckno.Text = "F";
             ddlbankname.SelectedValue = "NA";
             lblcrs.Visible = false;
             ddlpaymenttype.SelectedValue = paytype.Trim();
         }
         else
         {
             //ddlpaymenttype.SelectedValue = drro["Payment_Mode"].ToString();
             lblBankName.Visible = true;
             lblBankName.Visible = true;
             lblddchekno.Visible = true;
             txtckeckno.Visible = true;
             lblddchekdate.Visible = true;
             lblddchekdate.Visible = true;
             lblamountdd.Visible = true;
             txtchqamt.Visible = true;
             lblcrs.Visible = true;
             ddlpaymenttype.SelectedValue = drro["Payment_Mode"].ToString().Trim ();


         }
         if (Lift_st == "Y")
         {
             btnsave.Visible = false;
             btnupdateqty.Visible = true;
             //ddlallot_year.Enabled = false;
             //ddlalotmm.Enabled = false;
             ddlbankname.Enabled = false;
             ddlcomdty.Enabled = false;
             ddlscheme.Enabled = false;
             ddldistrict.Enabled = false;
             txtckeckno.ReadOnly = false;
             txtrate.ReadOnly = true;
             txtrono.ReadOnly = true;
             ddlpaymenttype.Enabled = false;



         }
         else
         {
             btnsave.Visible = true ;
             btnupdateqty.Visible = false;

         }



    }
    void GetBalanceQty()
    {
        string commid = lblcmdty.Text;
        string schemeid = lblsch.Text ;
        int month = int.Parse(ddlalotmm.SelectedValue);
        int year = int.Parse(ddlallot_year.SelectedValue);
        string qryGB = "Select Sum(RO_qty) as RO_qty from dbo.RO_of_FCI where Scheme='" + schemeid + "'and Commodity='" + commid + "' and Distt_Id='" + distid + "' and Allot_month=" + month + "and Allot_year=" + year;
        DObj = new Districts(ComObj);
        DataSet dsGB = DObj.selectAny(qryGB);
        if (dsGB == null)
        {
        }
        else
        {
            if (dsGB.Tables[0].Rows.Count == 0)
            {
                float mbqty = CheckNull(txtalotqty.Text) - CheckNull(txtroqty.Text);
                txtbalance.Text = "0";
                txtbalance.ReadOnly = true;
                //txtbalance.BackColor = System.Drawing.Color.Wheat;

            }
            else
            {
                DataRow drGB = dsGB.Tables[0].Rows[0];
                string mbalance = System.Math.Round(CheckNull(drGB["RO_qty"].ToString()), 5).ToString();
                if (mbalance == "0")
                {
                    txtbalance.Text = txtalotqty.Text;
                }
                else
                {
                    float mrobal = CheckNull(drGB["RO_qty"].ToString());
                    float mroalot = CheckNull(txtalotqty.Text);
                    float macbal = mroalot - mrobal;

                    //txtbalance.Text = System.Math.Round(CheckNull(drGB["RO_qty"].ToString()), 5).ToString();
                    txtbalance.Text = macbal.ToString();
                    txtbalance.ReadOnly = true;
                   // txtbalance.BackColor = System.Drawing.Color.Wheat;
                }


            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            string mdistid = distid;
            string mrono = txtrono.Text;
            string mrodate = getDate_MDY(txtrodate.Text);
            string mvdate = getDate_MDY(txtrovalidity.Text);
            float mroqty = float.Parse(txtroqty.Text);
            string mrodist = ddldistrict.SelectedValue;
            string mcomdty = ddlcomdty.SelectedValue;
            string mscheme = ddlscheme.SelectedValue;
            float mrate = float.Parse(txtrate.Text);
            float mamt = float.Parse(txtamount.Text);
            string mallotm = ddlalotmm.SelectedValue;
            string myear = DateTime.Today.Year.ToString();
            string  mddno = txtckeckno.Text;
            string mdddate = getDate_MDY(txtddate.Text);
            float mddamt = float.Parse(txtchqamt.Text);
            string mremark = txtremark.Text;
           
            string mpaymode=ddlpaymenttype.SelectedValue;
           float mrobalance=(CheckNull (txtbalance.Text)+CheckNull (lblroqty .Text))-(CheckNull (txtroqty.Text));
            float mbqty = float.Parse(txtroqty.Text);

            String qryUpdate = "Update  dbo.RO_of_FCI set RO_date='" + mrodate + "',RO_qty=" + mroqty + ",RO_Validity='" + mvdate + "',RO_district='" + ddldistrict.SelectedValue + "',Commodity='" + mcomdty + "',Scheme='" + mscheme + "',Rate=" + mrate + ",Amount=" + mamt + ",Allot_month='" + mallotm + "',Allot_year='" + myear + "',DD_chk_no='" + mddno + "',DD_chk_Amount=" + mddamt + ",Remarks='" + mremark + "',DD_chk_date='" + mdddate+ "',updated_date=getdate(),Balance_Qty=" + mbqty + ",Payment_Mode='" + mpaymode + "' where Distt_Id='" + distid + "'and RO_No='" + mrono + "'";
            cmd.CommandText = qryUpdate;
            cmd.Connection = con;
            con.Open();
            SqlTransaction trns;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;
            try
            {
                if (ddlbankname.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bank Name..'); </script> ");
                }
                else
                {
                    int count =cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        string month = ddlalotmm.SelectedValue;
                        string year = DateTime.Today.Year.ToString();

                        string qrydallocU = "Update dbo.District_Alloc set Balance_Qty =" + mrobalance + " where Scheme_ID='" + mscheme + "'and Commodity_ID='" + mcomdty + "' and district_code='" + distid + "'and Month=" + month + "and Year=" + year;
                        cmd.Connection = con;
                        cmd.CommandText = qrydallocU;
                        cmd.Transaction = trns;
                        cmd.ExecuteNonQuery();
                        string mbank = ddlbankname.SelectedValue;

                        string trans = "Update";
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string qryTrans = "insert into dbo.tbl_ROFCI_Transuction(Distt_Id,RO_No,RO_date,RO_qty,RO_Validity,RO_district,Commodity,Scheme,Rate,Amount,Allot_month,Allot_year,DD_chk_no,DD_chk_Amount,Bank_ID,Remarks,Operation,Trans_Date,IP_Address,User_ID)values('" + mdistid + "','" + mrono + "','" + mrodate + "'," + mroqty + ",'" + mvdate + "','" + mrodist + "','" + mcomdty + "','" + mscheme + "'," + mrate + "," + mamt + ",'" + mallotm + "','" + myear + "','" + mddno + "'," + mddamt + ",'" + mbank + "','" + mremark + "','" + trans + "',getdate(),'" + ip +"','"+ distid + "')";
                        cmd.CommandText = qryTrans;
                        cmd.Transaction = trns;
                        cmd.ExecuteNonQuery();
                    }
                }
                trns.Commit();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully..'); </script> ");
            }
            catch (Exception ex)

            {
                trns.Rollback();
                Label1.Visible = true;
                Label1.Text = ex.Message;
               
            }
            finally 
            {
                con.Close();
                ComObj.CloseConnection();
             
            }
            btnsave.Enabled = false;
          

        }
    }
    private static string GetMonthName(int month, bool abbrev)
    {
        DateTime date = new DateTime(1900, month, 1);
        if (abbrev) return date.ToString("MMM");
        return date.ToString("MMMM");
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsch.Text = ddlscheme.SelectedValue;
        GetRate();
        GetAllotment();
        GetBalanceQty();
        
    }
    void GetAllotment()
    {
        string sctype = lblsch.Text;
        int malot = int.Parse(ddlalotmm.SelectedValue.ToString());
        int malotyear = int.Parse(ddlallot_year.SelectedValue.ToString());
        string commodty = ddlcomdty.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        if (sctype == "1" || sctype == "2" || sctype == "3")
        {
            txtalotqty.Text = "";

            if (ddlscheme.SelectedItem.Text == "Non Scheme" || ddlscheme.SelectedItem.Text == "--Select--" || ddlcomdty.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
            }
            else
            {
                //GetRate();
                string comm = ddlcomdty.SelectedItem.Text;
                string scheme = ddlscheme.SelectedItem.Text;
                int amonth = int.Parse(ddlalotmm.SelectedItem.Value);
                int ayear = int.Parse(DateTime.Today.Year.ToString());

                string commodity = "";
                if (scheme.ToLower() == "apl")
                {
                    if (comm.ToLower().Contains("wheat"))
                    {
                        commodity = "wheat_apl_alloc";
                    }
                    if (comm.ToLower().Contains("rice"))
                    {
                        commodity = "rice_apl_alloc";
                    }
                    if (comm.ToLower().Contains("sugar"))
                    {
                        commodity = "sugar_alloc";
                    }

                }
                if (scheme.ToLower() == "bpl")
                {
                    if (comm.ToLower().Contains("wheat"))
                    {
                        commodity = "wheat_bpl_alloc";
                    }
                    if (comm.ToLower().Contains("rice"))
                    {
                        commodity = "rice_bpl_alloc";
                    }
                    if (comm.ToLower().Contains("sugar"))
                    {
                        commodity = "sugar_alloc";
                    }

                }
                if (scheme.ToLower() == "aay")
                {
                    if (comm.ToLower().Contains("wheat"))
                    {
                        commodity = "wheat_aay_alloc";
                    }
                    if (comm.ToLower().Contains("rice"))
                    {
                        commodity = "rice_aay_alloc";
                    }
                    if (comm.ToLower().Contains("sugar"))
                    {
                        commodity = "sugar_alloc";
                    }

                }
                if (commodity != "")
                {
                    cmd.CommandText = "select  " + commodity + "   from pds.state_alloc where district_code='" + rodist + "' and month='" + amonth + "' and Year='" + ayear + "'";
                    cmd.Connection = con_opdms;
                    con_opdms.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[commodity].ToString() == "")
                        {
                            txtalotqty.Text = "0";
                        }
                        else
                        {
                            txtalotqty.Text = System.Math.Round(float.Parse(dr[commodity].ToString()), 2).ToString();
                        }
                    }
                    dr.Close();
                    con_opdms.Close();
                }
                if (txtalotqty.Text == "")
                {
                    txtalotqty.Text = "0";
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotment for Selected Commodity...');</script>");
                }

                GetBalanceQty();
            }
            txtalotqty.ReadOnly = true;
           // txtalotqty.BackColor = System.Drawing.Color.Wheat;

        }
        else  
        {
            DObj = new Districts(ComObj);
            string malloc = "select alloc_qty from dbo.dist_mpscsc_alloc where district_code='" + rodist + "' and alloc_month=" + malot + " and alloc_year=" + malotyear + " and commodity_id='" + commodty + "' and scheme_id='" + mscheme + "'";
            DataSet dsGB = DObj.selectAny(malloc);
            if (dsGB == null)
            {
            }
            else
            {

                if (dsGB.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotment for Selected Commodity...');</script>");
                    float mbqty = CheckNull(txtalotqty.Text) - CheckNull(txtroqty.Text);
                    txtbalance.Text = mbqty.ToString();
                    txtalotqty.Text = "0";
                    txtbalance.ReadOnly = true;
                   // txtbalance.BackColor = System.Drawing.Color.Wheat;
                    txtalotqty.ReadOnly = true;
                   // txtalotqty.BackColor = System.Drawing.Color.Wheat;

                }
                else
                {
                    DataRow drGB = dsGB.Tables[0].Rows[0];
                    txtalotqty.Text = System.Math.Round(CheckNull(drGB["alloc_qty"].ToString()), 5).ToString();
                    txtalotqty.ReadOnly = true;
                   // txtalotqty.BackColor = System.Drawing.Color.Wheat;

                }
            }
            GetBalanceQty();
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
    void GetRate()
    {
        string qry = "Select Rate from dbo.SCSC_MSP_rate where Commodity_ID='" + ddlcomdty.SelectedValue + "'and Purchase_From='03'and District_code='" +rodist + "'";
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is no Rate available for selected commodity....'); </script> ");
            txtroqty.ReadOnly = false;
           

        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtrate.Text = CheckNull(dr["Rate"].ToString()).ToString();
            txtrate.ReadOnly = true;
           // txtrate.BackColor = System.Drawing.Color.Wheat;
           
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected string getDate_MDYCal(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("dd-MM-yyyy"));
    }
    protected void txtroqty_TextChanged(object sender, EventArgs e)
    {
        float mbalance = CheckNull(txtbalance.Text);
        float msqty = CheckNull(txtroqty.Text);
        float mchkqty = mbalance - msqty +CheckNull (lblroqty.Text);
        if (mchkqty < 0)
        {
            lblmssg.Visible = true;
            lblmssg.Text = "RO Quantity Should not be greater than Balance Qty.";
            txtroqty.Text = "0";

            txtroqty.Focus();
        }
        else
        {
            lblmssg.Visible = false;
            float amt = CheckNull(txtroqty.Text) * CheckNull(txtrate.Text);
            txtamount.Text = amt.ToString();
            txtamount.ReadOnly = true;
           // txtamount.BackColor = System.Drawing.Color.Wheat;
            txtckeckno.Focus();
        }

    }
    protected void txtamount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtrate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcmdty.Text = ddlcomdty.SelectedValue;

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldist.Text = ddldistrict.SelectedValue;

    }
    protected void btnreset_Click(object sender, EventArgs e)
    {

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void ddlalotmm_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmonth.Text = ddlalotmm.SelectedValue;
    }
    protected void txtchqamt_TextChanged(object sender, EventArgs e)
    {
        //float mamt = CheckNull(txtamount.Text);
        //float mddamt = CheckNull(txtchqamt.Text);
        //if (mddamt > mamt)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('DD Amount Should not be Greater than Actual Amount....'); </script> ");
        //    txtchqamt.Text = "0";
        //}
        //else
        //{

        //}
    }
    protected void txtremark_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpaymenttype.SelectedItem.Text == "Free Scheme")
        {
            lblBankName.Visible = false;
            ddlbankname.Visible = false;
            lblddchekno.Visible = false;
            txtckeckno.Visible = false;
            lblddchekdate.Visible = false;
            txtddate.Visible = false;
            lblamountdd.Visible = false;
            txtchqamt.Visible = false;
            txtchqamt.Visible = false;
            lblcrs.Visible  = false;
            txtchqamt.Text = "0";
            txtckeckno.Text = "F";
            ddlbankname.SelectedValue = "NA";

        }
        else
        {
            lblBankName.Visible = true;
            lblBankName.Visible = true;
            lblddchekno.Visible = true;
            txtckeckno.Visible = true;
            lblddchekdate.Visible = true;
            lblddchekdate.Visible = true;
            lblamountdd.Visible = true;
            txtchqamt.Visible = true;
            lblcrs.Visible = true;

            txtchqamt.Text = "";
            txtckeckno.Text = "";

        }
    }
    protected void btnupdateqty_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string mdistid = distid;
            string mrono = txtrono.Text;
            string mrodate = getDate_MDY(txtrodate.Text);
            string mvdate = getDate_MDY(txtrovalidity.Text);
            float mroqty = float.Parse(txtroqty.Text);
            string mrodist = ddldistrict.SelectedValue;
            string mcomdty = ddlcomdty.SelectedValue;
            string mscheme = ddlscheme.SelectedValue;
            float mrate = float.Parse(txtrate.Text);
            float mamt = float.Parse(txtamount.Text);
            string mallotm = ddlalotmm.SelectedValue;
            string myear = DateTime.Today.Year.ToString();
            string mddno = txtckeckno.Text;
            string mdddate = getDate_MDY(txtddate.Text);
            float mddamt = float.Parse(txtchqamt.Text);
            string mremark = txtremark.Text;
            //string mcrdate = DateTime.Today.Date.ToString();
            //string mcrtdate = getDate_MDY(mcrdate);
           // string mudate = getDate_MDY(mcrdate);
            string mpaymode = ddlpaymenttype.SelectedValue;
            float mrobalance = (CheckNull(txtbalance.Text) + CheckNull(lblroqty.Text)) - (CheckNull(txtroqty.Text));
            float mbqty = float.Parse(txtroqty.Text);

            String qryUpdate = "Update  dbo.RO_of_FCI set RO_qty=" + mroqty + ",Rate=" + mrate + ",Amount=" + mamt + ",Allot_month='" + mallotm + "',Allot_year='" + myear + "',DD_chk_no='" + mddno + "',DD_chk_Amount=" + mddamt + ",Remarks='" + mremark + "',updated_date=getdate(),Balance_Qty=" + mbqty + ",Payment_Mode='" + mpaymode + "' where Distt_Id='" + distid + "'and RO_No='" + mrono + "' and RO_district='"+rodist +"'";
            cmd.CommandText = qryUpdate;
            cmd.Connection = con;
            con.Open();
            SqlTransaction trns;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;
            try
            {
                if (ddlbankname.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bank Name..'); </script> ");
                }
                else
                {
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        string month = ddlalotmm.SelectedValue;
                        string year = DateTime.Today.Year.ToString();

                        string qrydallocU = "Update dbo.District_Alloc set Balance_Qty =" + mrobalance + " where Scheme_ID='" + mscheme + "'and Commodity_ID='" + mcomdty + "' and district_code='" + distid + "'and Month=" + month + "and Year=" + year;
                        cmd.Connection = con;
                        cmd.CommandText = qrydallocU;
                        cmd.Transaction = trns;
                        cmd.ExecuteNonQuery();
                        string mbank = ddlbankname.SelectedValue;

                        string trans = "Update";
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string qryTrans = "insert into dbo.tbl_ROFCI_Transuction(Distt_Id,RO_No,RO_date,RO_qty,RO_Validity,RO_district,Commodity,Scheme,Rate,Amount,Allot_month,Allot_year,DD_chk_no,DD_chk_Amount,Bank_ID,Remarks,Operation,Trans_Date,IP_Address,User_ID)values('" + mdistid + "','" + mrono + "','" + mrodate + "'," + mroqty + ",'" + mvdate + "','" + mrodist + "','" + mcomdty + "','" + mscheme + "'," + mrate + "," + mamt + ",'" + mallotm + "','" + myear + "','" + mddno + "'," + mddamt + ",'" + mbank + "','" + mremark + "','" + trans + "',getdate(),'" + ip + "','" + distid + "')";
                        cmd.CommandText = qryTrans;
                        cmd.Transaction = trns;
                        cmd.ExecuteNonQuery();
                    }
                }
                trns.Commit();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully..'); </script> ");
            }
            catch (Exception ex)
            {
                trns.Rollback();
                Label1.Visible = true;
                Label1.Text = ex.Message;

            }
            finally
            {
                con.Close();
                ComObj.CloseConnection();

            }
            btnupdateqty.Enabled = false;


        }
    }
}
