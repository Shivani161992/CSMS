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
public partial class mpscsc_RO_New : System.Web.UI.Page
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
    public string islifted = "N";
    string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            version = Session["hindi"].ToString();
            
            txtroqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtrate.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtamount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtchqamt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");

           
           
            txtrodate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            txtrovalidity.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            txtddate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

            txtroqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtroqty.Attributes.Add("onchange", "return chksqltxt(this),calcAmount();");

            txtrodate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrodate.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrovalidity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrovalidity.Attributes.Add("onchange", "return chksqltxt(this)");

            txtddate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtddate.Attributes.Add("onchange", "return chksqltxt(this)");

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
            txtchqamt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");

            txtremark.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtremark.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtremark.Attributes.Add("onchange", "return chksqltxt(this)");


            txtrono.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtamount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtckeckno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtchqamt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtalotqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            //string dbname = "Warehouse";

           



            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        

            //if (Session["dc_id"] != null)
            //{

            //}

            distobj = new DistributionCenters(ComObj);
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
                if (version == "H")
                {

                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lblScheme.Text = Resources.LocalizedText.lblScheme;
                    lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                    lblReleaseOrder.Text = Resources.LocalizedText.lblDetailsofRO;
                    btnsave.Text = Resources.LocalizedText.btnsave;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    lblmonth.Text = Resources.LocalizedText.lblmonth;
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
        comdtobj  = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");

        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");
    
    
    }
    public string get_days(DateTime  fromDate, DateTime  toDate)
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
        int d = (m * 30) + d2;
        int day = d - d1;
        return day.ToString();
    }
    void GetScheme()
    {
       

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll(" order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");
    
    }
    void GetName()
    {
        mobj2 = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" +distid + "'";
        DataSet ds1dt = mobj2.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        lbldistrict.Text  = dr1dt["district_name"].ToString();
        ddldistrict.SelectedItem.Text = dr1dt["district_name"].ToString(); ;
        ddldistrict.SelectedValue = distid;


   } 
    void GetBank()
    {
        mobj2 = new MoveChallan(ComObj);
        string qrybank = "select Bank_ID,Bank_Name  from dbo.Bank_Master where district_code='" + distid + "' and issueCenter_code='"+distid +"'";
        DataSet dsb = mobj2.selectAny(qrybank);
        ddlbankname.DataSource = dsb.Tables[0];

        ddlbankname.DataTextField = "Bank_Name";
        ddlbankname.DataValueField ="Bank_ID";
        ddlbankname.DataBind();
        ListItem lst = new ListItem();
        lst.Text = "--Select--";
        lst.Value = "NA";
        ddlbankname.Items.Insert(0,lst);

    }


    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCommodity();
        
    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {       

    }
    protected string  getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }

    
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtchqamt.Text == "" || txtckeckno.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Payment Details....'); </script> ");

        }
        else
        {

            string mronoc = txtrono.Text;
            mobj2 = new MoveChallan(ComObj);
            string qryrochk = "select *  from dbo.RO_of_FCI where Distt_Id='" + distid + "'and RO_No='" + mronoc + "'";

            DataSet dsrochk = mobj2.selectAny(qryrochk);

            if (dsrochk.Tables[0].Rows.Count == 0)
            {

                //DataRow drro = dsro.Tables[0].Rows[0];

                if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select The Commodity /Scheme/BankName....'); </script> ");
                }
                else
                {
                    DateTime fdate = new DateTime();
                    DateTime tdate = new DateTime();

                    string fromdate =getDate_MDY(txtrodate.Text);
                    string todate = getDate_MDY(txtrovalidity.Text);

                    fdate = Convert.ToDateTime(fromdate);
                    tdate = Convert.ToDateTime(todate);

                    string validity = get_days(fdate, tdate);
                    if (int.Parse(validity) > 25)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Validity Should not be more than 25 days'); </script> ");

                    }
                    else
                    {


                        if (Page.IsValid)
                        {
                            decimal mroqty = CheckNull(txtroqty.Text);
                            if (mroqty == 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Quantity....'); </script> ");
                                txtroqty.Focus();

                            }

                            else
                            {
                                string mdistid = distid;
                                string mrono = txtrono.Text;
                                string mrodate = getDate_MDY(txtrodate.Text);
                                string mvdate = getDate_MDY(txtrovalidity.Text);

                                string mrodist = ddldistrict.SelectedValue;
                                string mcomdty = ddlcomdty.SelectedValue;
                                string mscheme = ddlscheme.SelectedValue;
                                decimal mrate = CheckNull(txtrate.Text);
                                decimal mamt = CheckNull(txtamount.Text);
                                string mallotm = ddlalotmm.SelectedValue;
                                string myear = DateTime.Today.Year.ToString();
                                string mddno = txtckeckno.Text;
                                string mdddate = getDate_MDY(txtddate.Text);
                                decimal mddamt = CheckNull(txtchqamt.Text);
                                string mremark = txtremark.Text;
                                string mislift = "N";
                                //string mcrdate = DateTime.Today.Date.ToString();

                                //string mcrtdate = getDate_MDY(mcrdate);
                                string mudate = "";
                                string mddate = "";
                                decimal mbqty = decimal.Parse(txtroqty.Text);

                                decimal mallotqty = CheckNull(txtalotqty.Text);
                                decimal mbalqty = CheckNull(txtbalance.Text);
                                decimal mrobalance = mbalqty - CheckNull(txtroqty.Text);
                                int maltmonth = int.Parse(ddlalotmm.SelectedValue);
                                int maltyear = int.Parse(ddlallot_year.SelectedValue);
                                string mbank = ddlbankname.SelectedValue;
                                string iex = "N";
                                string pmode = ddlpaymenttype.SelectedValue;


                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string opid = Session["OperatorIDDM"].ToString();
                                string state = Session["State_Id"].ToString();
                                string notrans = "N";


                                string qryInsert = "insert into dbo.RO_of_FCI(State_Id,Distt_Id,RO_No,RO_date,RO_qty,RO_Validity,RO_district,Commodity,Scheme,Rate,Amount,Allot_month,Allot_year,DD_chk_no,DD_chk_date,DD_chk_Amount,Bank_ID,Remarks,IsLifted,Created_date,updated_date,deleted_date,Balance_Qty,IsExpire,IP_Address,Payment_Mode,OperatorID,NoTransaction)values('" + state + "','" + mdistid + "','" + mrono + "','" + mrodate + "'," + mroqty + ",'" + mvdate + "','" + mrodist + "','" + mcomdty + "','" + mscheme + "'," + mrate + "," + mamt + ",'" + mallotm + "','" + myear + "','" + mddno + "','" + mdddate + "'," + mddamt + ",'" + mbank + "','" + mremark + "','" + mislift + "',getdate(),'" + mudate + "','" + mddate + "'," + mbqty + ",'" + iex + "','" + ip + "','" + pmode + "','" + opid + "','" + notrans + "')";
                              
                                cmd.CommandText = qryInsert;
                                cmd.Connection = con;
                                con.Open();
                                SqlTransaction trns;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd.Transaction = trns;
                                try
                                {
                                    if (mbqty > mbalqty)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('RO Quantity Should not be greater than Balance Qty....'); </script> ");
                                        txtroqty.Focus();
                                    }
                                    else
                                    {
                                        int count = cmd.ExecuteNonQuery();
                                        if (count == 1)
                                        {
                                            string trans = "Insert";
                                            DateTime date = DateTime.Now;
                                            string qryTrans = "insert into dbo.tbl_ROFCI_Transuction(State_Id,Distt_Id,RO_No,RO_date,RO_qty,RO_Validity,RO_district,Commodity,Scheme,Rate,Amount,Allot_month,Allot_year,DD_chk_no,DD_chk_date,DD_chk_Amount,Bank_ID,Remarks,Operation,Trans_Date,IP_Address,User_ID,OperatorID)values('" + state +"','"+  mdistid + "','" + mrono + "','" + mrodate + "'," + mroqty + ",'" + mvdate + "','" + mrodist + "','" + mcomdty + "','" + mscheme + "'," + mrate + "," + mamt + ",'" + mallotm + "','" + myear + "','" + mddno + "','" + mdddate + "'," + mddamt + ",'" + mbank + "','" + mremark + "','" + trans + "',getdate(),'" + ip + "','" + distid + "','"+ opid +"')";
                                            cmd.CommandText = qryTrans;
                                            cmd.Transaction = trns;
                                            cmd.ExecuteNonQuery();

                                            int month = int.Parse(ddlalotmm.SelectedValue);
                                            int year = int.Parse(ddlallot_year.SelectedValue);
                                            decimal mlift = 0;
                                            string qryGD = "Select Balance_Qty,Allotment_Qty from dbo.District_Alloc where Scheme_ID='" + mscheme + "'and Commodity_ID='" + mcomdty + "' and district_code='" + distid + "' and Month=" + month + "and Year=" + year;

                                            DObj = new Districts(ComObj);
                                            DataSet dsGD = DObj.selectAny(qryGD);
                                            if (dsGD.Tables[0].Rows.Count == 0)
                                            {

                                                string qrydalloc = "insert into dbo.District_Alloc(State_Id,district_code,Month,Year,Commodity_ID,Scheme_ID,Allotment_Qty,Balance_Qty,Lifted_Qty,OperatorID)values('" +state +"','"+  distid + "'," + mallotm + "," + maltyear + ",'" + mcomdty + "','" + mscheme + "'," + mallotqty + "," + mrobalance + "," + mlift + ",'"+ opid +"')";
                                                cmd.CommandText = qrydalloc;
                                                cmd.Transaction = trns;
                                                cmd.ExecuteNonQuery();



                                            }
                                            else
                                            {
                                                string qrydallocU = "Update dbo.District_Alloc set Balance_Qty =" + mrobalance + " where Scheme_ID='" + mscheme + "'and Commodity_ID='" + mcomdty + "' and district_code='" + distid + "'and Month=" + month + "and Year=" + year;
                                                cmd.Connection = con;
                                                cmd.CommandText = qrydallocU;
                                                cmd.Transaction = trns;
                                                cmd.ExecuteNonQuery();


                                            }



                                        }
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");

                                        btnsave.Enabled = false;
                                    }

                                    trns.Commit();
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




                            }
                        }
                    }
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Release Order Number Already Exist....'); </script> ");

            }
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtrono.Text = "";
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
       ComObj.CloseConnection();
       Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void txtrate_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtroqty_TextChanged(object sender, EventArgs e)
    {
        decimal mbalance = CheckNull(txtbalance.Text);
        decimal msqty = CheckNull(txtroqty.Text);
        decimal mchkqty = mbalance - msqty;
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
             decimal  amt = System.Math.Round (CheckNull(txtroqty.Text) * CheckNull(txtrate.Text),2);
            txtamount.Text = amt.ToString();
            txtamount.ReadOnly = true;
            //txtamount.BackColor = System.Drawing.Color.Wheat;
            txtckeckno.Focus();
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

    void GetRate()
    {
        string mcomdty = ddlcomdty.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string todaydate =DateTime.Today.ToString("MM/dd/yyyy");
        string qry = "Select Rate from dbo.SCSC_MSP_rate where Commodity_ID='" + mcomdty + "'and Purchase_From='03' and Scheme_ID='" + mscheme + "' and District_code='" + ddldistrict.SelectedValue + "' and Effective_From<='" + todaydate + "' order by Effective_From desc";
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAny(qry);
         if (ds==null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is no Rate available for selected commodity....'); </script> ");
            txtroqty.ReadOnly = false;
            txtrate.Text = "0";
            ddlscheme.SelectedIndex = 0;
            ddlcomdty.SelectedIndex = 0;
            txtrate.Focus();

        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is no Rate available for selected commodity....'); </script> ");
                txtroqty.ReadOnly = false;
                txtrate.Text = "0";
                txtrate.Focus();


            }
            else
            {
                
                DataRow dr = ds.Tables[0].Rows[0];

                txtrate.Text = CheckNull(dr["Rate"].ToString()).ToString();
                txtrate.ReadOnly = true;
               // txtrate.BackColor = System.Drawing.Color.Wheat;
                txtroqty.Focus();
            }
        }
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {

        string sctype = ddlscheme.SelectedItem.Text;
        int  malot=int.Parse (ddlalotmm.SelectedValue.ToString ());
        int  malotyear=int.Parse (ddlallot_year.SelectedValue.ToString ());
        string commodty = ddlcomdty.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        if (sctype == "APL" || sctype == "BPL" || sctype == "AAY")
        {
            ddlpaymenttype.Enabled = false;
            txtalotqty.Text = "";

            if (ddlscheme.SelectedItem.Text == "Non Scheme" || ddlscheme.SelectedItem.Text == "--Select--" || ddlcomdty.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
                ddlscheme.SelectedIndex = 0;
            }
            else
            {
                GetRate();
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
                    cmd.CommandText = "select  " + commodity + "   from pds.state_alloc where district_code='" + ddldistrict.SelectedValue  + "' and month='" + malot + "' and Year='" + malotyear + "'";
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
                            txtalotqty.Text = System.Math.Round(decimal.Parse(dr[commodity].ToString()), 2).ToString();
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
            //txtalotqty.BackColor = System.Drawing.Color. ;

        }
        else
        {
            ddlpaymenttype.Enabled = true ;
            DObj = new Districts(ComObj);
            string malloc = "select alloc_qty from dbo.dist_mpscsc_alloc where district_code='" + ddldistrict.SelectedValue + "' and alloc_month=" + malot + " and alloc_year=" + malotyear + " and commodity_id='" + commodty + "' and scheme_id='" + mscheme + "'";
            DataSet dsGB = DObj.selectAny(malloc);
            if (dsGB == null)
            {
            }
            else
            {

                if (dsGB.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotment for Selected Commodity...');</script>");
                   
                    txtalotqty.Text = "0";
                    txtbalance.ReadOnly = true;
                   // txtbalance.BackColor = System.Drawing.Color.Blue;
                    txtalotqty.ReadOnly = true;
                    //txtalotqty.BackColor = System.Drawing.Color.Blue;

                }
                else
                {
                    DataRow drGB = dsGB.Tables[0].Rows[0];
                    txtalotqty.Text = System.Math.Round(CheckNull(drGB["alloc_qty"].ToString()), 5).ToString();
                    txtalotqty.ReadOnly = true;
                    //txtalotqty.BackColor = System.Drawing.Color.Blue;
                    //decimal mbqty = CheckNull(txtalotqty.Text) - CheckNull(txtroqty.Text);
                    //txtbalance.Text = mbqty.ToString();
                    
                }
            }
            GetBalanceQty();
            GetRate();
        }
    }
    void GetBalanceQty()
    {
        string comodity = "";
        if (ddlcomdty.SelectedItem.Text.ToLower().Contains("wheat"))
        {
            comodity="wheat";

        }

        if (ddlcomdty.SelectedItem.Text.ToLower().Contains("rice"))
        {
            comodity = "rice";
        }

        if (ddlcomdty.SelectedItem.Text.ToLower().Contains("sugar"))
        {
            comodity  = "sugar";
        }

        string comdty = "";
        string qrycom = "Select Commodity_ID from dbo.tbl_MetaData_STORAGE_COMMODITY where lower(Commodity_Name) like'%" + comodity + "%'";

        cmd.CommandText = qrycom;
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
       
        while (dr.Read())
        {
            comdty = comdty+dr["Commodity_ID"].ToString();
            comdty = comdty + ",";

        }

        int ind ;
        ind = comdty.LastIndexOf(",");
        comdty = comdty.Remove(ind, 1);
      
        string commid = ddlcomdty.SelectedValue; ;
        string schemeid = ddlscheme.SelectedValue;
        int month=int.Parse (ddlalotmm.SelectedValue );
        int year=int.Parse (ddlallot_year .SelectedValue);

        //decimal doqty = 0;
        //string cmdstr = "";
        //if (ddlcomdty.SelectedItem.Text.ToLower().Contains("sugar"))
        //{
        //    cmdstr = "select Round(sum(convert(decimal(18,5),quantity)),5) as qty  from delivery_order_mpscsc  group by district_code,commodity_id,scheme_id,allotment_month,allotment_year,issue_type having (commodity_id in(" + comdty +"))  and (district_code='"+distid +"') and (allotment_month="+month +")  and (allotment_year="+year +") and (issue_type='FCI')";
        //}
        //else
        //{
        //    cmdstr = "select Round(sum(convert(decimal(18,5),quantity)),5) as qty from delivery_order_mpscsc  group by district_code,commodity_id,scheme_id,allotment_month,allotment_year,issue_type having (commodity_id in(" + comdty + ")) and (scheme_id='" + schemeid + "') and (district_code='" + distid + "') and (allotment_month=" + month + ")  and (allotment_year=" + year + ") and (issue_type='FCI')";
        //}
        //DObj = new Districts(ComObj);
        //DataSet dsDO = DObj.selectAny(cmdstr);
        //if (dsDO == null)
        //{
        //}
        //else
        //{
        //    if (dsDO.Tables[0].Rows.Count == 0)
        //    {
               
        //    }
        //    else
        //    {
        //        DataRow drDO = dsDO.Tables[0].Rows[0];

        //        doqty = CheckNull(drDO["qty"].ToString());                   

        //    }


        //}

        string qryGB = "Select Round(Sum(convert(decimal(18,5),RO_qty)),5) as RO_qty from dbo.RO_of_FCI group by Distt_Id,Commodity,Allot_month,Allot_year,scheme having (Commodity in(" + comdty + ")) and (Distt_Id='" + ddldistrict.SelectedValue + "') and (Allot_month=" + month + ")and (Allot_year=" + year + ") and (Scheme='" + schemeid + "')";
        DObj = new Districts(ComObj);
        DataSet dsGB = DObj.selectAny(qryGB);
        if (dsGB == null)
        {
        }
        else
        {
            if (dsGB.Tables[0].Rows.Count == 0)
            {
                decimal mbqty = CheckNull(txtalotqty.Text);
                txtbalance.Text = mbqty.ToString();
                txtbalance.ReadOnly = true;
               // txtbalance.BackColor = System.Drawing.Color.Wheat;

            }
            else
            {
                DataRow drGB = dsGB.Tables[0].Rows[0];
                string  mbalance = System.Math.Round(CheckNull(drGB["RO_qty"].ToString()), 5).ToString();
                if (mbalance =="0")
                {
                    txtbalance.Text = txtalotqty.Text;
                }
                else
                {
                    decimal mrobal=CheckNull(drGB["RO_qty"].ToString());
                    decimal mroalot = CheckNull(txtalotqty.Text);
                    decimal macbal = mroalot - mrobal;

                    //txtbalance.Text = System.Math.Round(CheckNull(drGB["RO_qty"].ToString()), 5).ToString();
                    txtbalance.Text = macbal.ToString();
                    txtbalance.ReadOnly = true;
                    //txtbalance.BackColor = System.Drawing.Color.Wheat;
                }
                

            }
        }

    }
    protected void txtremark_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtchqamt_TextChanged(object sender, EventArgs e)
    {
        //decimal mamt = CheckNull(txtamount.Text);
        //decimal mddamt = CheckNull(txtchqamt.Text);
        //if (mddamt > mamt)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('DD Amount Should not be Greater than Actual Amount....'); </script> ");
        //    txtchqamt.Text= "0";
        //}
        //else
        //{

        //}


    }
    protected void txtrono_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlalotmm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlallot_year_SelectedIndexChanged(object sender, EventArgs e)
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
            txtddate.Visible  = false;
            lblamountdd.Visible = false;
            txtchqamt.Visible = false;
            lblchqrs.Visible = false;
            txtchqamt.Text = "0";
            txtckeckno.Text = "F";
            ddlbankname.SelectedValue = "NA";
            txtddate.Text = "1/1/1";
            txtddate.ReadOnly = true;

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
            lblchqrs.Visible = true;
            txtddate.ReadOnly = false;



        }
    }
}
