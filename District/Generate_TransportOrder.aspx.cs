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
public partial class District_Generate_TransportOrder : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    LARO obj = null;
    chksql chk = null;

    LARO objo = null;
    public string distid = "";
    public string sid = "";
    string roqty = null;
    MoveChallan mobj = null;
    public string getdatef = "";
    DataTable dt = new DataTable();
    decimal disqty = 0;
    string transuct = "";
    long  transnum = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            //txttorderno.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtroqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this),checksqlkey_gen(event,this)");
            txtsendqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtcumlqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtbalqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");



            txttorderno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttorderno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttorderno.Attributes.Add("onchange", "return chksqltxt_psw(this)");

            //txtsendqty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtsendqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtsendqty.Attributes.Add("onchange", "return chksqltxt_psw(this)");
                      


            txttorderno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtsendqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcumlqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbalqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcommodity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtscheme.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrodate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtliftqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            chk = new chksql();

            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txttorderno.Text);
            ctrllist.Add(txtsendqty.Text);
         
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
           
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                DaintyDate1 .Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                //GetTransportAll();
                GetTransport();
                GetDistance();
                //GetRO();
                GetName();
                GetDist();
                GetFCIdist();
                dt.Columns.Add("RO_No");
                dt.Columns.Add("TO_No");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("DisName");
                dt.Columns.Add("IssueName");
                dt.Columns.Add("District");
                dt.Columns.Add("IssueCenter");
                ddldistrict.SelectedValue = distid;
                GetDCName();
                Session["dt"] = dt;
                ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
                ddd_allot_year.SelectedIndex = 1;
                ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
                GetRO();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }

    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdistrict.Text = dr1dt["district_name"].ToString();
        txtdistrict.ReadOnly = true;
        //txtdistrict.BackColor = System.Drawing.Color.Wheat;


    }

    void GetRO()
    {
        ddlrono.Items.Clear();
        int year = int.Parse(ddd_allot_year.SelectedValue.ToString ());
        int month = int.Parse(ddl_allot_month .SelectedValue.ToString ());
              
        //string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'and Allot_month=" + month + " and Allot_year=" + year + " and  Balance_Qty >0";
        string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'and Balance_Qty >0";
        cmd.Connection = con;
        cmd.CommandText = qry;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrono.Items.Add(dr["RO_No"].ToString());



        }
        ddlrono.Items.Insert(0, "--Select--");
        dr.Close();
        con.Close();

    }

    void GetTransport()
    {
        tobj = new Transporter(ComObj);
        string qry = "Select Lead,Transporter_ID,Transporter_Name+'/'+'('+Lead_Distance.Lead_Name +')'as Transporter_Name  from dbo.Transporter_Table left join Lead_Distance on Transporter_Table.Lead=Lead_Distance.Lead_ID where Distt_ID='" + distid + "' and IsActive='Y'";// and Lead='"+ddllead.SelectedValue+"'";

        DataSet ds = tobj.selectAny(qry);

        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
        ddltransport.Items.Insert(0, "--Select--");

    }
    void GetTransportAll()
    {
        tobj = new Transporter(ComObj);
        string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid + "' and IsActive='Y'";

        DataSet ds = tobj.selectAny(qry);

        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
        ddltransport.Items.Insert(0, "--Select--");

    }
    void GetDistance()
    {


        mobj = new MoveChallan(ComObj);
        string lead = "Select * from dbo.Lead_Distance";
        DataSet ds = mobj.selectAny(lead);

        ddllead.DataSource = ds.Tables[0];
        ddllead.DataTextField = "Lead_Name";
        ddllead.DataValueField = "Lead_ID";
        ddllead.DataBind();
        ddllead.Items.Insert(0, "--Select--");

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
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetFCIdist()
    {
        obj = new LARO(ComObj);
        string qry = "select districtsmp.district_name as dist_name,DepoCode.district_code as dist_code From dbo.DepoCode left join pds.districtsmp   on upper(DepoCode.district)=upper( districtsmp.district_name) group by districtsmp.district_name, DepoCode.district_code";
        DataSet ds = obj.selectAny(qry);

        ddlfcidist.DataSource = ds.Tables[0];
        ddlfcidist.DataTextField = "dist_name";
        ddlfcidist.DataValueField = "dist_code";
        ddlfcidist.DataBind();
        ddlfcidist.Items.Insert(0, "--Select--");

    }
    void GetFCIdepot()
    {
        string dtype = ddldepottype.SelectedItem.ToString();
        string dcode = ddlfcidist.SelectedValue;
        obj = new LARO(ComObj);
        string qry = "select distinct(DepoName) as depo_name  ,DepoCode as depo_code,type From dbo.DepoCode where district_code='" + dcode + "'";
        DataSet ds = obj.selectAny(qry);

        ddlfcidepo.DataSource = ds.Tables[0];
        ddlfcidepo.DataTextField = "depo_name";
        ddlfcidepo.DataValueField = "depo_code";
        ddlfcidepo.DataBind();
        ddlfcidepo.Items.Insert(0, "--Select--");

    }
    void GetData()
    {
        if (ddlrono.SelectedItem.Text != "--Select--")
        {
            obj = new LARO(ComObj);
            string qryall = "SELECT TO_Allot_Lift.Lifted_Qty,RO_of_FCI.Commodity AS Expr1,Transport_Order_againstRo.Cumulative_Qty as Cumulative_Qty , RO_of_FCI.Distt_Id, RO_of_FCI.RO_No, RO_of_FCI.RO_Validity, RO_of_FCI.RO_date, RO_of_FCI.RO_qty,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date, RO_of_FCI.Balance_Qty,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name  From dbo.RO_of_FCI Left JOIN tbl_MetaData_STORAGE_COMMODITY  ON RO_of_FCI.Commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join Transport_Order_againstRo on RO_of_FCI.RO_No=Transport_Order_againstRo.RO_No left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=tbl_MetaData_SCHEME.Scheme_id left join dbo.TO_Allot_Lift on RO_of_FCI.RO_No=TO_Allot_Lift.RO_No  where RO_of_FCI.RO_No='" + ddlrono.SelectedItem + "' and RO_of_FCI.Distt_Id='" + distid + "'";
            DataSet ds = obj.selectAny(qryall);
         
                DataRow dr = ds.Tables[0].Rows[0];

                string rdate = dr["RO_Validity"].ToString();
                string rodate = getdate(rdate);
                txtrodate.Text = rodate;
                txtrodate.ReadOnly = true;
                //txtrodate.BackColor = System.Drawing.Color.Wheat;
                lblcomdty.Text = dr["Expr1"].ToString();
                lblsch.Text = dr["Scheme"].ToString();
                roqty = dr["RO_qty"].ToString();
                txtroqty.Text = dr["RO_qty"].ToString();
                txtroqty.ReadOnly = true;
               // txtroqty.BackColor = System.Drawing.Color.Wheat;



                txtbalqty.Text = dr["Balance_Qty"].ToString();
               
                txtbalqty.ReadOnly = true;
               // txtbalqty.BackColor = System.Drawing.Color.Wheat;
                txtcumlqty.Text = dr["Cumulative_Qty"].ToString();
                txtcommodity.Text = dr["Commodity_Name"].ToString();
                txtcommodity.ReadOnly = true;
                //txtcommodity.BackColor = System.Drawing.Color.Wheat;
                txtscheme.Text = dr["Scheme_Name"].ToString();
                txtscheme.ReadOnly = true;
                //txtscheme.BackColor = System.Drawing.Color.Wheat;
                txtliftqty.Text = dr["Lifted_Qty"].ToString();
                txtliftqty.ReadOnly = true;
                //txtliftqty.BackColor = System.Drawing.Color.Wheat;
            
           

           


            //txtbalqty.Text = dr["Pending_Qty"].ToString();
            string cumqtyqry = "Select Cumulative_Qty ,Pending_Qty from dbo.TO_Allot_Lift where RO_No='" + ddlrono.SelectedItem + "' and Distt_Id='" + distid + "'";
            DataSet dscq = obj.selectAny(cumqtyqry);
            if (dscq.Tables[0].Rows.Count==0)
            {
                txtcumlqty.Text = "0";
                txtcumlqty.ReadOnly = true;
               // txtcumlqty.BackColor = System.Drawing.Color.Wheat;
                lbltoqty.Text = txtbalqty.Text;
            }
            else
            {

                DataRow drcq = dscq.Tables[0].Rows[0];
                txtcumlqty.Text = drcq["Cumulative_Qty"].ToString();
                txtbalqty.Text = drcq["Pending_Qty"].ToString();
                txtcumlqty.ReadOnly = true;
                lbltoqty.Text = drcq["Pending_Qty"].ToString();
                decimal chkp = CheckNull(drcq["Pending_Qty"].ToString());
                if (chkp == 0)
                {
                    lbltoqty.Text=txtbalqty.Text;

                }
                else
                {
                    lbltoqty.Text=drcq["Pending_Qty"].ToString();
                }
                //txtcumlqty.BackColor = System.Drawing.Color.Wheat;
            }


        }
        else
        {
            txtrodate.Text = "";
            txtroqty.Text = "";

            txtbalqty.Text = "";
            txtcumlqty.Text = "";
            txtroqty.Text = "";
            txtrodate.Text = "";
            txtbalqty.Text = "";
            txtcommodity.Text = "";
            txtscheme.Text = "";
            ddlrono.Focus();

        }
        
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
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
        int d = (m * 30) + d2;
        int day = d - d1;
        return day.ToString();
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        
        string mroto = ddlrono.SelectedValue;
        string mtonoRO = txttorderno.Text;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        //string qryTODR = "Select * from dbo.Transport_Order_againstRo  where RO_No='" + mroto + "' and Distt_Id='" + distid + "' and TO_Number='" + mtonoRO + "'";
        //obj = new LARO(ComObj);

        //DataSet dstoDR = obj.selectAny(qryTODR);
        //if (dstoDR.Tables[0].Rows.Count == 0)
        //{
        if (ddlrono.SelectedItem.Text == "--Select--" || ddltransport.SelectedItem.Text == "--Select--" || ddlfcidepo.SelectedItem.Text=="--Select--")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select The RO Number  And Transporter Name /FCI Depot'); </script> ");
            }
            else
            {
                DateTime fdate = new DateTime();
                DateTime tdate = new DateTime();

                string fromdate = getmmddyy(txtrodate.Text);
                string  todate =  getDate_MDY(DaintyDate1.Text);

                fdate = DateTime.Parse(fromdate.ToString());
                //string todate = getDate_MDY(DaintyDate1.Text);
                tdate = Convert.ToDateTime(todate);


                string validity = get_days(tdate, fdate);
                if (int.Parse(validity) < 0)
                {
                    string RO_NO = ddlrono.SelectedValue;

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Validity has been expired'); </script> ");

                    //string qrytoupdate = "Update dbo.TO_Allot_Lift set Locked='N' where RO_No='" + RO_NO + "' and Distt_Id='" + distid + "'";

                    string qrytoupdate = "Update dbo.RO_of_FCI set IsExpire='Y' where RO_No='" + RO_NO + "'and Distt_Id='" + distid + "'";
                    cmd.CommandText = qrytoupdate;
                    cmd.Connection = con;
                    try
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                       
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        con.Close();

                    }


                }
                else
                {
                    //int bqty = CheckNullInt(txtbalqty.Text);
                    //int sqty = CheckNullInt(txtsendqty.Text);

                    //if (sqty > bqty || sqty == 0)
                    //{
                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sending Qty Should Not Be Greater Than Pending Qty or 0'); </script> ");
                    //}
                    //else
                    //{
                   

                        string mrono = ddlrono.SelectedItem.ToString();
                        decimal mroqty = CheckNull(txtroqty.Text);
                        string mrdate = DateTime.Today.Date.ToString();
                        string mtono = txttorderno.Text;
                        string mtodate = getDate_MDY(DaintyDate1.Text);
                        string mtaname = ddltransport.SelectedValue;
                        decimal miqty = CheckNull(txtsendqty.Text);
                        decimal mcumqty = CheckNull(txtcumlqty.Text);
                        decimal mpqty = CheckNull(txtbalqty.Text);
                       // string mcdate = getDate_MDY(DateTime.Today.Date.ToString());
                       
                        string fcidist = ddlfcidist.SelectedValue;
                        string fcidepo = ddlfcidepo.SelectedValue;

                        string udate = "";
                        string ddate = "";
                        

                        //int balamt = int.Parse((txtbalqty.Text)) - int.Parse((txtsendqty.Text));

                        //string Balance_Qty = balamt.ToString();
                        //decimal balqty = CheckNull(Balance_Qty);

                        decimal cumqty = CheckNull(txtcumlqty.Text);
                        decimal sendqty = CheckNull(txtsendqty.Text);
                        decimal tcumqty = cumqty + sendqty;
                        string mtid = ddltransport.SelectedValue;
                        string todist = ddldistrict.SelectedValue;
                        string toissuecenter = ddlissue.SelectedValue;
                        string lift = "N";
                        decimal mliftqty = 0;
                        decimal mpendqty = CheckNull(txtsendqty.Text);
                        string notrans = "N";
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string opid = Session["OperatorIDDM"].ToString();
                        string state = Session["State_Id"].ToString();


                        string tid = "";
                        if (ddltransport.SelectedItem.Text == "--Select--")
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select The Transporter..'); </script> ");
                        }
                        else
                        {
                            
                            //string uquery = "update  dbo.RO_of_FCI set Balance_Qty=" + balqty + "where RO_No='" + mrono + "' and Distt_Id='" + distid + "'";



                            //DataRow drto = dsto.Tables[0].Rows[0];

                            con.Open();
                            SqlTransaction trns;
                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                            cmd.Transaction = trns;
                            try
                            {
                                dt = (DataTable)Session["dt"];
                                cmd.Connection = con;
                                int count = dt.Rows.Count;
                                if (count > 0)
                                {
                                  string crdate = DateTime.Today.Date.ToString();
                                   int i = 0;
                                  while (i < count)
                                    {
                                      if(i==0)
                                      {
                                        mobj = new MoveChallan(ComObj);
                                        string qrey = "select max(Trunsuction_Id) as Trunsuction_Id from dbo.Transport_Order_againstRo where Distt_Id='" + distid + "' and Month=" + month + "";
                                        DataSet ds = mobj.selectAny(qrey);
                                        if (ds == null)
                                        {

                                        }
                                        else
                                        {
                                            DataRow dr = ds.Tables[0].Rows[0];
                                            transuct = dr["Trunsuction_Id"].ToString();
                                            if (transuct == "")
                                            {

                                                transuct = distid + month.ToString() + "0001";

                                            }
                                            else
                                            {
                                                transnum = Convert.ToInt64(transuct);
                                                transnum = transnum + 1;
                                                transuct = transnum.ToString();


                                            }
                                        }
                                  }
                                    else{
                                        transnum = int.Parse(tid) + 1;
                                        transuct = transnum.ToString();


                                    }
                                        tid = transuct;

                                        disqty = disqty + CheckNull(dt.Rows[i][2].ToString());
                                        decimal  balamt = CheckNull(txtbalqty.Text) - CheckNull(disqty.ToString ());
                                        string qry = "insert into dbo.Transport_Order_againstRo(State_Id,Distt_Id,RO_No,RO_qty,RO_Validity,TO_Number,TO_Date,Transporter_Name,Commodity_ID,Scheme_ID,FCI_district,FCI_depot,toDistrict,toIssueCenter,Quantity,Cumulative_Qty,Pending_Qty,Month,Year,Trunsuction_Id,IsLifted,Created_date,updated_date,deleted_date,IP_Address,OperatorID,NoTransaction)values('" + state + "','" + distid + "','" + mrono + "'," + mroqty + ",'" + mrdate + "','" + mtono + "','" + mtodate + "','" + mtaname + "','" + lblcomdty.Text + "','" + lblsch.Text + "','" + fcidist + "','" + fcidepo + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "'," + dt.Rows[i][2] + "," + mliftqty + "," + dt.Rows[i][2] + "," + month + "," + year + ",'" + tid + "','" + lift + "',getdate(),'" + udate + "','" + ddate + "','" + ip + "','" + opid + "','" + notrans + "')";
                                        cmd.CommandText = qry;
                                        cmd.Transaction = trns;
                                        cmd.ExecuteNonQuery();                                    
                                       
                                        i = i + 1;
                                                                               
                                    }
                                                                   
                                }
                                else
                                {

                                }

                                    string mmto=txttorderno  .Text;

                                    string qryTO = "Select * from dbo.TO_Allot_Lift where RO_No='" + mrono + "' and Distt_Id='" + distid + "'";
                                    obj = new LARO(ComObj);

                                    DataSet dsto = obj.selectAny(qryTO);
                                    if (dsto.Tables[0].Rows.Count==0 )
                                    {
                                        string locked = "Y";
                                        decimal liftqty = 0;
                                        decimal pqty = CheckNull(txtbalqty.Text) - CheckNull(disqty.ToString());
                                        decimal ttcum = CheckNull(txtcumlqty.Text) + disqty;

                                        string qrytoinsert = "insert into dbo.TO_Allot_Lift(State_Id,Distt_Id,RO_No,RO_qty,RO_Validity,Transporter_ID,Cumulative_Qty,Pending_Qty,Lifted_Qty,Month,Year,Created_date,Locked,IP_Address,OperatorID)values('" +state + "','"+  distid + "','" + mrono + "'," + mroqty + ",'" + mrdate + "','" + mtid + "'," + ttcum + "," + pqty + "," + liftqty + "," + month + "," + year + ",getdate(),'" + locked + "','"+ ip+"','"+ opid +"')";
                                        cmd.CommandText = qrytoinsert;
                                        cmd.Transaction = trns;
                                        cmd.ExecuteNonQuery();

                                    }
                                    else
                                    {
                                        decimal pqty = CheckNull(txtbalqty.Text) - CheckNull(disqty.ToString ());
                                        decimal ttcum = CheckNull(txtcumlqty.Text) + disqty;
                                        string qrytoupdate = "Update dbo.TO_Allot_Lift set RO_qty=" + mroqty + ",Cumulative_Qty=" + ttcum + ",Pending_Qty=" + pqty + ",Month=" + month + ",Year=" + year + ",Created_date=getdate() where RO_No='" + mrono + "' and Distt_Id='" + distid + "'";
                                        cmd.CommandText = qrytoupdate;
                                        cmd.Transaction = trns;
                                        cmd.ExecuteNonQuery();


                                    }


                                    trns.Commit();
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                btnsave.Enabled = false;

                                fillgrid();
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

                        con.Open();
                    //}


                }




            }
        //}



        //else
        //{


        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Number Already Exist ..... '); </script> ");
        //}


    }
    protected void ddlrono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
        //GetTOTransport();
        fillgrid();
        txttorderno.Focus();
        Label3.Visible = false;
    }


    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ro_no = dgridchallan.SelectedRow.Cells[1].Text;
        string to_no = dgridchallan.SelectedRow.Cells[2].Text;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string qty = dgridchallan.SelectedRow.Cells[4].Text;
        string id = dgridchallan.SelectedRow.Cells[8].Text;
        decimal mqtys = CheckNull(qty);

        string qrychk = "Select * from dbo.Lift_A_RO where RO_No='" + ro_no + "' and Dist_Id='" + distid + "' and TO_Number='" +to_no  + "' and Month="+ month +" and Year="+year ;
        obj = new LARO(ComObj);
        DataSet dschk = obj.selectAny(qrychk);
        if (dschk == null)
        {
           
        }
        else
        {
            if (dschk.Tables[0].Rows.Count == 0)
            {
                string qrydlt = "delete from dbo.Transport_Order_againstRo where RO_No='" + ro_no + "' and Distt_Id='" + distid + "' and TO_Number='" + to_no + "' and Month=" + month + " and Year=" + year+"and Trunsuction_Id='"+id+"'";
                cmd.CommandText = qrydlt;

                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string qrygetb = "Select Cumulative_Qty,Pending_Qty from dbo.TO_Allot_Lift where RO_No='" + ro_no + "' and Distt_Id='" + distid + "' and Month=" + month + " and Year=" + year;
                    obj = new LARO(ComObj);
                    DataSet dsbal = obj.selectAny(qrygetb);
                    if (dsbal == null)
                    {

                    }
                    else
                    {
                        if (dsbal.Tables[0].Rows.Count == 0)
                        {

                        }
                        else
                        {
                            DataRow drbal = dsbal.Tables[0].Rows[0];
                            string bal = drbal["Cumulative_Qty"].ToString();
                            string pbal = drbal["Pending_Qty"].ToString();
                            decimal uqty = CheckNull(bal) - mqtys;
                            decimal upqty = CheckNull(pbal) +mqtys;

                            string updatebal = "Update dbo.TO_Allot_Lift set Cumulative_Qty=" + uqty + ",Pending_Qty="+upqty +" where RO_No='" + ro_no + "' and Distt_Id='" + distid + "'and Month=" + month + " and Year=" + year;
                            cmd.CommandText = updatebal;
                            cmd.ExecuteNonQuery();
                            Label3.Visible = true;
                            Label3.Text = "Record Deleted Successfully........";
                            Label3.ForeColor = System.Drawing.Color.OrangeRed;
                            fillgrid();
                            GetData();

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
                }




            }
            else
            {
                Label3.Visible = true;
                Label3.Text = "Sorry You Can't Delete This Transport Order, It has been lifted !";
            }

           

        }


    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        if (dgridchallan.PageCount == 0)
        {
        }
        else
        {
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
    }
    void fillgrid()
    {
        string mrono = ddlrono.SelectedItem.ToString();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT tbl_MetaData_DEPOT.DepotName,Transport_Order_againstRo.RO_No,Transport_Order_againstRo.Quantity,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.TO_Date,Transport_Order_againstRo.Transporter_Name,Transporter_Table.Transporter_Name as Tname,Transport_Order_againstRo.Trunsuction_Id FROM dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join dbo.tbl_MetaData_DEPOT  on Transport_Order_againstRo.toIssueCenter= tbl_MetaData_DEPOT.DepotID where Transport_Order_againstRo.Distt_Id='" + distid + "'and Transport_Order_againstRo.RO_No ='" + mrono + "'";
        DataSet ds = mobj.selectAny(qry);
        dgridchallan.DataSource = ds.Tables[0];
        dgridchallan.DataBind();


    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TO_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    void GetTOTransport()
    {
        string mrono = ddlrono.SelectedItem.ToString();
        string ttid = ddltransport.SelectedValue;
        string qryTO = "Select TO_Allot_Lift.Locked,TO_Allot_Lift.RO_NO,TO_Allot_Lift.Transporter_ID,Transporter_Table.Transporter_Name as Transporter_Name  from dbo.TO_Allot_Lift left join dbo.Transporter_Table on TO_Allot_Lift.Transporter_ID=Transporter_Table.Transporter_ID  where TO_Allot_Lift.Distt_Id='" + distid + "'and TO_Allot_Lift.RO_NO='" + mrono + "'";
        obj = new LARO(ComObj);

        DataSet dsto = obj.selectAny(qryTO);
         if (dsto.Tables[0].Rows.Count==0)
        {
            ddltransport.Enabled = true;
            ddltransport.BackColor = System.Drawing.Color.White;
            GetTransport();



        }
        else
        {

            DataRow drchk = dsto.Tables[0].Rows[0];
            string Status = drchk["Locked"].ToString();
            string mdro = drchk["RO_NO"].ToString();

            //ddltransport.SelectedValue = drchk["Transporter_ID"].ToString();
            ddltransport.SelectedItem.Text = drchk["Transporter_Name"].ToString();
            //ddltransport.Enabled = false;
            //ddltransport.BackColor = System.Drawing.Color.Wheat;

        }

    }
    protected void ddltransport_SelectedIndexChanged(object sender, EventArgs e)
    {
        // string mrono = ddlrono.SelectedItem.ToString();
        // string ttid = ddltransport.SelectedValue;
        // string qryTO = "Select Locked,RO_NO from dbo.TO_Allot_Lift where Distt_Id='" + distid + "'and Transporter_ID='" + ttid + "'";
        // obj = new LARO(ComObj);

        // DataSet dsto = obj.selectAny(qryTO);
        //if (dsto.Tables[0].Rows.Count == 0)
        // {


        // }
        // else
        // {
        //     DataRow drchk = dsto.Tables[0].Rows[0];
        //     string Status = drchk["Locked"].ToString();
        //     string mdro=drchk["RO_NO"].ToString();
        //     if (Status.Trim ()=="Y" && mrono == mdro)
        //     {
        //         ddldistrict.Focus();



        //     }
        //     else
        //     {
        //         if (Status.Trim() == "N")
        //         {

        //         }
        //         else
        //         {
        //             Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('This Transporter is Locked Please Select another one..'); </script> ");
        //             ddltransport.Focus();
        //             GetTransport();
        //         }


        //     }




        // }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/TransportOrder_Type.aspx");
    }
    protected void addmore_Click(object sender, EventArgs e)
    {
        string temp = "N";
        string to = txttorderno.Text;
        if (ddldistrict.SelectedItem.Text == "--Select--" || ddlissue.SelectedItem.Text == "--Select--" || ddlrono.SelectedItem.Text=="--Select--" || to=="" )
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select RO Number,TO Number,District and IssueCenter..'); </script> ");
        }
        else
        {
            DateTime  mtodate =Convert.ToDateTime(getDate_MDY(DaintyDate1.Text))  ;
            DateTime  today = DateTime.Today.Date;
            //if (mtodate < today)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry Back Date Entry is not allow here..'); </script> ");
            //}
            //else
            //{
               
                dt = (DataTable)Session["dt"];
                int rocount= dt.Rows.Count;
                if (rocount > 0)
                {
                    int i = 0;
                    while (i < rocount)
                    {
                        string missuename = ddlissue.SelectedItem.Text;
                        string chkissue = dt.Rows[i][4].ToString();
                        if (missuename == chkissue)
                        {
                            temp = "Y";
                            break;

                        }
                        i = i + 1;
                    }
                    if (temp == "Y")
                    {
                    }
                    else
                    {
                        string mrono = ddlrono.SelectedItem.ToString();
                        string mtono = txttorderno.Text;
                        string mtrans = ddltransport.SelectedValue;
                        decimal sqty = CheckNull(txtsendqty.Text);
                        string ddist = ddldistrict.SelectedValue;
                        string dissue = ddlissue.SelectedValue;
                        string distname = ddldistrict.SelectedItem.Text;
                        string issuename = ddlissue.SelectedItem.Text;


                        decimal topqty = CheckNull(lbltoqty.Text) - sqty;
                        lbltoqty.Text = topqty.ToString();

                        if (CheckNull(lbltoqty.Text) < 0)
                        {
                            Label4.Visible = true;
                            Label4.Text = "Quantity should not be Greater then Pending Qty.";
                            decimal tolift = CheckNull(lbltoqty.Text) + sqty;
                            lbltoqty.Text = tolift.ToString();
                            txtsendqty.Text = "";
                            txtsendqty.Focus();
                        }
                        else
                        {
                            Label4.Visible = false;
                            dt.Rows.Add(mrono, mtono, sqty, distname, issuename, ddist, dissue);

                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            txtsendqty.Text = "";
                            txtsendqty.Focus();
                            //decimal bqty=CheckNull (txtbalqty.Text)-CheckNull (txtsendqty.Text);
                            //txtbalqty.Text = bqty.ToString();
                        }
                    }

                }



                else
                {
                    string mmrono = ddlrono.SelectedItem.ToString();
                    string mmtono = txttorderno.Text;
                    string mmtrans = ddltransport.SelectedValue;
                    decimal msqty = CheckNull(txtsendqty.Text);
                    string mddist = ddldistrict.SelectedValue;
                    string mdissue = ddlissue.SelectedValue;
                    string mdistname = ddldistrict.SelectedItem.Text;
                    string mmissuename = ddlissue.SelectedItem.Text;


                    decimal mtopqty = CheckNull(lbltoqty.Text) - msqty;
                    lbltoqty.Text = mtopqty.ToString();

                    if (CheckNull(lbltoqty.Text) < 0)
                    {
                        Label4.Visible = true;
                        Label4.Text = "Quantity should not be Greater then Pending Qty.";
                        decimal tolift = CheckNull(lbltoqty.Text) + msqty;
                        lbltoqty.Text = tolift.ToString();
                        txtsendqty.Text = "";
                        txtsendqty.Focus();
                    }
                    else
                    {
                        Label4.Visible = false;
                        dt.Rows.Add(mmrono, mmtono, msqty, mdistname, mmissuename, mddist, mdissue);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        txtsendqty.Text = "";
                        txtsendqty.Focus();
                        //decimal bqty=CheckNull (txtbalqty.Text)-CheckNull (txtsendqty.Text);
                        //txtbalqty.Text = bqty.ToString();
                    }
                }
                GetDist();
               
            //}
        }

        Label3.Visible = false;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = GridView1.SelectedIndex;
        decimal qty = CheckNull ( GridView1.SelectedRow.Cells[3].Text);
        decimal uqty = CheckNull(lbltoqty.Text) + qty;
        lbltoqty.Text = uqty.ToString();
        dt = (DataTable)Session["dt"];
        dt.Rows[idx].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;
    }
   
    protected void ddlfcidist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFCIdepot();
       
    }
    protected void ddlfcidepo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void ddd_allot_year_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRO();
    }
    protected void ddllead_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTransport();
    }
}
