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

public partial class IssueCenter_RR_Entry_Sugar : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    MoveChallan mobj = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    DistributionCenters distobj = null;
    chksql chk =null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string getdatef = "";
    public string hname = "";
    public string sid = "";
    public string transid = "";
    public int railnum;
    public string chst = "";
    string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            distid = Session["dist_id"].ToString();
            version = Session["hindi"].ToString();

           
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

               
            txtrecdbaf.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecdqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtdisbag.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtdisqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtrecdbaf.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdbaf.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecdqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtdisbag.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdisbag.Attributes.Add("onchange", "return chksqltxt(this)");

            txtdisqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdisqty.Attributes.Add("onchange", "return chksqltxt(this)");

            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtrecdbaf.Text);
            ctrllist.Add(txtrecdqty.Text);
            ctrllist.Add(txtdisbag.Text);
            ctrllist.Add(txtdisqty.Text);          
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

            txtrackno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtchallan.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrecdbaf.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrecdqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisbag.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            DaintyDate3.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisbag.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtchalldt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttruckno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtmaxcap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcurntcap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtavalcap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            if (!IsPostBack)
            {
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                GetRailHead();
                GetChallan();
                GetRack();
                GetTransport();
                GetGodown();
                GetCommodity();
                GetScheme();
                if (version == "H")
                {
                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblScheme.Text = Resources.LocalizedText.lblScheme;
                    
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lbltotalReceivedBags.Text = Resources.LocalizedText.lbltotalReceivedBags;
                    lblTotalQuantityReceived.Text = Resources.LocalizedText.lblTotalQuantityReceived;
                    lblMaxCap.Text = Resources.LocalizedText.lblCapacity;
                    lblChallanNumber.Text = Resources.LocalizedText.lblChallanNumber;
                    lblTrans.Text = Resources.LocalizedText.lblTrans;
                    lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                    lblrailhead.Text = Resources.LocalizedText.lblrailhead;
                    lblchallandate.Text = Resources.LocalizedText.lblchallandate;
                   
                    lblCurStackCap.Text = Resources.LocalizedText.lblCurStackCap;
                    lblAvailable.Text = Resources.LocalizedText.lblAvailable;
                    lblIssuedBags.Text = Resources.LocalizedText.lblIssuedBags;
                    lblDispatchQty.Text = Resources.LocalizedText.lblDispatchQty;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    btnsubmit.Text = Resources.LocalizedText.btnsubmit;
                    lbldepositstock.Text = Resources.LocalizedText.lbldepositstock;
                    lblrackno1.Text = Resources.LocalizedText.lblrackno1;
                    lblrackno.Text = Resources.LocalizedText.lblrackno;
                    lblChallanNumber1.Text = Resources.LocalizedText.lblChallanNumber1;
                    
                }


            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }
    void GetRailHead()
    {
        mobj = new MoveChallan(ComObj);
        string qrygrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + distid + "'";
        DataSet dsd = mobj.selectAny(qrygrh);

        ddlsenrailhead.DataSource = dsd.Tables[0];
        ddlsenrailhead.DataTextField = "RailHead_Name";
        ddlsenrailhead.DataValueField = "RailHead_Code";
        ddlsenrailhead.DataBind();
        ddlsenrailhead.Items.Insert(0, "--Select--");



    }
    void GetTransport()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid + "'and IsActive='Y'";

        DataSet ds = mobj.selectAny(qry);

        ddltransporter.DataSource = ds.Tables[0];
        ddltransporter.DataTextField = "Transporter_Name";
        ddltransporter.DataValueField = "Transporter_ID";
        ddltransporter.DataBind();
        ddltransporter.Items.Insert(0, "--Select--");

    }
    void GetCommodity()
    {
      
        ddlcomdty.Items.Clear();
        comdtobj = new Commodity_MP(ComObj);
        string selcom = "Select * from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (23,17) and Status='Y' order by Commodity_Id";
        DataSet ds = comdtobj.selectAny(selcom);

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
        ddlscheme.SelectedValue = "0";
        ddlscheme.Enabled = false;
    }
    protected void ddlsenrailhead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txttruckno_TextChanged(object sender, EventArgs e)
    {

    }
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }
   

    void UpdateBalance()
    {
        string comdtyid = lblcomdty.Text;
        string schemeid = lblsch.Text;
        string source = "07";
        string gdwn = ddlgodown.SelectedValue;
        string mstate = "23";
        int openbag = 0;
        int openqty = 0;
        int recdbags = CheckNullInt(txtrecdbaf.Text);
        float recdqty = CheckNull(txtrecdqty.Text);
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        int month = int.Parse(DateTime.Today.Month.ToString ());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string udate = "";
        string ddate = "";


        string qrystock = "select Sum(Recd_Qty) as Qty from dbo.RR_receipt_Depot where Commodity='" + comdtyid + "'and Scheme ='" + schemeid + "' and district_code='" + distid + "'and DepotID='" + sid + "' and Month=" + month + "and Year=" + year;
        mobj = new MoveChallan(ComObj);
        DataSet dspro = mobj.selectAny(qrystock);
        if (dspro.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            DataRow drop = dspro.Tables[0].Rows[0];
            float mrod = CheckNull(drop["Qty"].ToString());
            string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comdtyid + "' and Scheme_ID='" + schemeid + "' and DistrictId ='" + distid + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
            mobj = new MoveChallan(ComObj);
            DataSet dsopen = mobj.selectAny(qryinsopen);
            if (dsopen.Tables[0].Rows.Count == 0)
            {
                string chkopenss = "Select Round(convert(decimal,Sum(Current_Balance)),5) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Scheme_Id ='" + schemeid + "'";
                mobj = new MoveChallan(ComObj);
                DataSet dsqry = mobj.selectAny(chkopenss);
                if (dsqry == null)
                {

                }

                else
                {
                    DataRow drss = dsqry.Tables[0].Rows[0];
                    float sropen = CheckNull(drss["Current_Balance"].ToString());
                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + comdtyid + "','" + schemeid + "'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtrecdqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                    cmd.CommandText = qryinsr;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            else
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Received_RailHead=" + mrod + " where Commodity_Id ='" + comdtyid + "' and Scheme_ID='" + schemeid + "'and DistrictId='" + distid + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                cmd.CommandText = qryinsU;
                cmd.ExecuteNonQuery();
                con.Close();
            }


        }








        string chkopenbal = "Select * from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Scheme_Id='" + schemeid + "' and Godown='" + gdwn + "' and Source='" + source + "'";
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
                string pdate = getDate_MDY("01/04/2011");
                string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + distid + "','" + sid + "','" + comdtyid + "','" + schemeid + "','','" + gdwn + "',''," + openbag + "," + openqty + ",'" + source + "'," + recdqty + "," + recdbags + "," + month + "," + year + ",'" + ip + "','"+pdate+"',getdate(),'" + udate + "','" + ddate + "'" + ")";
                cmd.CommandText = qreyins;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string qreygdnU = "update dbo.Current_Godown_Position set Current_Capacity=Current_Capacity-" + recdqty + ",Current_Bags=Current_Bags+" + recdbags + ",Current_Balance=Current_Balance+" + recdqty + " where District_Id='" + distid + "' and Depotid='" + sid + "' and Godown='" + gdwn + "'";
                cmd.CommandText = qreygdnU;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                string mugdn = ddlgodown.SelectedValue;
                string query = "Update dbo.issue_opening_balance set Current_Balance=Round(convert(decimal,Current_Balance),5)+" + recdqty + ",Current_Bags=Current_Bags+" + recdbags + " where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + comdtyid + "'and Scheme_Id='" + schemeid + "'and Source='" + source + "' and Godown='" + gdwn + "'";
                cmd.CommandText = query;
                cmd.Connection = con;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string qreygdnU = "update dbo.Current_Godown_Position set Current_Capacity=Current_Capacity-" + recdqty + ",Current_Bags=Current_Bags+" + recdbags + ",Current_Balance=Current_Balance+" + recdqty  + " where District_Id='" + distid  + "' and Depotid='" + sid  + "' and Godown='" + gdwn  + "'";
                    cmd.CommandText = qreygdnU;
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

                }

            }
        }

       
       



    }
    
   
    void GetGodown()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid  + "' and DepotId='" + sid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");


    }
    void GetRack()
    {
        //int month = int.Parse(DateTime.Today.Month.ToString());
        //int year = int.Parse(DateTime.Today.Year.ToString());

        //ddlrackno.Items.Insert(0, "--Select--");
        //string qreyrac = "select Rack_No  from dbo.Rake_Master_Sugar where Dest_District='" + distid + "'";
        //cmd.Connection = con;
        //cmd.CommandText = qreyrac;
        //con.Open();
        //dr = cmd.ExecuteReader();
        //while (dr.Read())
        //{
        //    ddlrackno.Items.Add(dr["Rack_No"].ToString());

        //}
        //dr.Close();
        //con.Close();
        mobj = new MoveChallan(ComObj);

        string qreyrac = "select Rack_No  from dbo.Rake_Master_Sugar where district_code='" + distid + "' and Mode='S'";
        DataSet ds = mobj.selectAny(qreyrac);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ddlrackno.Items.Clear();
            ListItem lst = new ListItem();
            lst.Text = "Not Indicated";
            lst.Value = "0";
            ddlrackno.Items.Insert(0, "--Select--");
            ddlrackno.Items.Insert(1, lst);


        }
        else
        {
            ddlrackno.Items.Clear();
            ddlrackno.DataSource = ds.Tables[0];
            ddlrackno.DataTextField = "Rack_No";
            ddlrackno.DataValueField = "Rack_No";
            ddlrackno.DataBind();
            ddlrackno.Items.Insert(0, "--Select--");
            ddlrackno.Items.Insert(1, "Not Indicated");

        }

    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
    }
    void GetChallan()
    {
        mobj = new MoveChallan(ComObj);

        string qry = "SELECT * FROM dbo.Rack_Receipt_Details where district_code='" + distid + "'and IssueCenter='" + sid + "' and IsReceived='N'";
        DataSet ds = mobj.selectAny(qry);
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
            ddlchallan.Items.Insert(1, "Not Indicated");

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
    void GetData()
    {
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string challan = ddlchallan.SelectedValue;
        mobj = new MoveChallan(ComObj);

        string qry = "SELECT Rack_Receipt_Details.*,Rake_Master_Sugar.Commodity_ID,tbl_Rail_Head.RailHead_Name as RailHead_Name  FROM dbo.Rack_Receipt_Details left join dbo.tbl_Rail_Head on Rack_Receipt_Details.Recd_RailHead=tbl_Rail_Head.RailHead_Code left join dbo.Rake_Master_Sugar on Rack_Receipt_Details.Rack_No=Rake_Master_Sugar.Rack_No where Rack_Receipt_Details.district_code='" + distid + "'and Rack_Receipt_Details.IssueCenter='" + sid + "' and Rack_Receipt_Details.Challan_No='" + challan + "'and Rack_Receipt_Details.Month=" + month + " and Rack_Receipt_Details.Year=" + year +" and Rack_Receipt_Details.RR_Status='S'";
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {

            Panel1.Visible = true ;
            txtchalldt.Visible = false ;
            txtchallan.Text = "";
            txttruckno.Text = "";
            txtdisbag.Text = "";
            txtdisqty.Text = "";
            ddlsenrailhead.Focus();
            txtchallan.ReadOnly = false;
            txtchalldt.ReadOnly = false;
            txttruckno.ReadOnly = false;
            ddlsenrailhead.Enabled = true;
            txtdisbag.ReadOnly = false ;
            txtdisqty.ReadOnly = false ;
            txtchallan.Focus();
            //lblrackno.Visible = true;
            //txtrackno.Visible = true ;
            //txtrackno.Text = "";
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];

            ddlsenrailhead.SelectedItem.Text = dr["RailHead_Name"].ToString();
            lblcomdty.Text = dr["Commodity_ID"].ToString();
            ddlsenrailhead.SelectedValue = dr["Recd_RailHead"].ToString();
            txtchallan.Text = dr["Challan_No"].ToString();
            txtchalldt.Text = getdate(dr["Challan_date"].ToString());
            txttruckno.Text = dr["Truck_No"].ToString();
            txtdisbag.Text = dr["Disp_Bags"].ToString();
            txtdisqty.Text = dr["Disp_Qty"].ToString();
            ddltransporter.SelectedValue = dr["Transporter_ID"].ToString();
            ddltransporter.Enabled = false;
             
            //ddlrackno.SelectedValue = dr["Rack_No"].ToString();
            //ddlcomdty.SelectedValue = dr["Commodity_ID"].ToString();
            ddlscheme.SelectedIndex = 1;
            Panel1.Visible = false;
            txtchalldt.Visible = true ;
            //lblrackno.Visible = false;
            //txtrackno.Visible = false;
            txtrackno.Text = dr["Rack_No"].ToString();
            txtchalldt.Font.Italic = true;
            txtchalldt.ForeColor = System.Drawing.Color.Blue;
            txtchallan.Font.Italic =   true;
            txtchallan.ForeColor = System.Drawing.Color.Blue;
            txttruckno.Font.Italic =   true;
            txttruckno.ForeColor = System.Drawing.Color.Blue;

            txtdisbag.Font.Italic = true;
            txtdisbag.ForeColor = System.Drawing.Color.Blue;
            txtdisqty.Font.Italic = true;
            txtdisqty.ForeColor = System.Drawing.Color.Blue;

            ddlsenrailhead.Font.Italic = true;
            ddlsenrailhead.ForeColor = System.Drawing.Color.Blue;
            txtchallan.ReadOnly = true;
            txtchalldt.ReadOnly = true;
            txttruckno.ReadOnly = true;
            txtdisbag.ReadOnly = true;
            txtdisqty.ReadOnly = true;
            ddlsenrailhead.Enabled = false;
            

          

        }

    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrackno.SelectedItem.Text == "Not Indicated")
        {
            lblrackno.Visible = true;
            txtrackno.Visible = true;
            txtrackno.Text = "";
            txtrackno.ReadOnly =false ;
            txtrackno.Focus();
            txtchallan.Text = "";
            
            txtchalldt.Text = "";
            txttruckno.Text = "";
            txtdisbag.Text = "";
            txtdisqty.Text = "";
            GetTransport();
            GetCommodity();
            GetRailHead();
            ddlsenrailhead.Enabled = true;
            ddltransporter.Enabled = true;
            GetGodown();
            txtmaxcap.Text = "";
            txtcurntcap.Text = "";
            txtavalcap.Text = "";

        }
        else
        {
            lblrackno.Visible = true;
            txtrackno.Visible = true;
            txtrackno.Text = ddlrackno.SelectedValue;
            txtrackno.ReadOnly = true;
        }

    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gname = ddlgodown.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" +distid  + "' and DepotId='" + sid + "' and Godown_ID='" + gname + "'";

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
                txtmaxcap.Text = dr["Godown_Capacity"].ToString();

            }


        }
        GetCapGodown();
    }
    void GetCapGodown()
    {
        string gname = ddlgodown.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT isnull(Sum(convert(decimal(18,5),Current_Balance)),0) as Current_Balance  FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Godown='" + gname + "'";

        DataSet ds = mobj.selectAny(qrygdn);
        if (ds == null)
        {
            txtcurntcap.Text = "00.00000";
        }

        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                txtcurntcap.Text = "00.00000";

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtcurntcap.Text = (System.Math.Round(decimal.Parse(dr["Current_Balance"].ToString()), 5)).ToString();
                txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();
            }


        }

    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsch.Text = ddlscheme.SelectedValue;
    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcomdty.Text = ddlcomdty.SelectedValue;
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");

    }
    protected void btnsubmit_Click(object sender, EventArgs e)

    {
        string state = Session["State_Id"].ToString();

        if (ddlsenrailhead.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Railhead.....'); </script> ");
        }
        else
        {
            mobj = new MoveChallan(ComObj);
            string challan = ddlchallan.SelectedValue;
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string qry = "SELECT TC_Number from dbo.RR_receipt_Depot where district_code='" + distid + "'and DepotID='" + sid + "' and TC_Number='" + challan + "'and Month=" + month + " and Year=" + year;
            DataSet dschk = mobj.selectAny(qry);

            if (dschk.Tables[0].Rows.Count == 0)
            {

                mobj = new MoveChallan(ComObj);
                string qrey = "select max(Trans_ID) as Trans_ID  from dbo.RR_receipt_Depot where district_code='" + distid + "' and Month =" + month + "and Year=" + year;
                DataSet ds = mobj.selectAny(qrey);
                DataRow dr = ds.Tables[0].Rows[0];
                transid = dr["Trans_ID"].ToString();
                string mmonth = month.ToString();


                if (transid == "")
                {
                    transid = distid + mmonth + "001";

                }
                else
                {
                    railnum = Convert.ToInt32(transid);
                    railnum = railnum + 1;
                    transid = railnum.ToString();

                }
                string msendrh = ddlsenrailhead.SelectedValue;
                string tcno = txtchallan.Text;
                string truckno = txttruckno.Text;
                int disbags = CheckNullInt(txtdisbag.Text);
                float disqty = CheckNull(txtdisqty.Text);
                int bags = CheckNullInt(txtrecdbaf.Text);
                float recdqty = CheckNull(txtrecdqty.Text);
                string tcdate = "";
                string crdate = DateTime.Today.Date.ToString();
                string udate = "";
                string mrack = txtrackno.Text;
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string trpid = ddltransporter.SelectedValue;
                string godown = ddlgodown.SelectedValue;
                string comdty = lblcomdty.Text;
                string scheme = lblsch.Text;
                string opid = Session["OperatorId"].ToString();
                string notrans = "N";
                if (ddlchallan.SelectedItem.Text == "Not Indicated")
                {
                    tcdate = getDate_MDY(DaintyDate3.Text);
                    chst = "N";

                }
                else
                {
                    tcdate = getmmddyy(txtchalldt.Text);
                    chst = "I";

                }

                string qryinsert = "Insert into  dbo.RR_receipt_Depot(State_Id,district_code,DepotID,Rack_No,S_RailHead,TC_Number,TC_date,Transporter_ID,Truck_No,Trans_ID,Disp_Bags,Disp_Qty,Recd_Bags,Recd_Qty,Month,Year,Created_Date,Updated_Date,Ip_Address,Challan_st,Godown,Commodity,Scheme,OperatorID,NoTransaction)values('" + state + "','" + distid + "','" + sid + "','" + mrack + "','" + msendrh + "','" + tcno + "','" + tcdate + "','" + trpid + "','" + truckno + "','" + transid + "'," + disbags + "," + disqty + "," + bags + "," + recdqty + "," + month + "," + year + ",getdate(),'" + udate + "','" + ip + "','" + chst + "','" + godown + "','" + comdty + "','" + scheme + "','" + opid + "','" + notrans + "')";
                cmd.Connection = con;
                cmd.CommandText = qryinsert;
                con.Open();
                SqlTransaction trns;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns;
                try
                {
                    if (recdqty == 0 || bags == 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receipt Bags and Weight Should not be 0 .....'); </script> ");
                    }
                    else
                    {

                        cmd.ExecuteNonQuery();

                        string upd = "Update dbo.Rack_Receipt_Details set IsReceived='Y' where IssueCenter='" + sid + "'and Challan_No='" + challan + "'";
                        cmd.CommandText = upd;
                        cmd.Transaction = trns;

                        cmd.ExecuteNonQuery();
                        trns.Commit();
                        UpdateBalance();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved  Successfully.....'); </script> ");
                        btnsubmit.Enabled = false;
                    }

                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                    //ComObj.CloseConnection();
                }



            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Number Already Exist.....'); </script> ");
            }




        }
    }
    protected void txtrackno_TextChanged(object sender, EventArgs e)
    {

    }
}
