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
using System.Data.SqlClient;
using Data;
using DataAccess;

public partial class issueagainst_do_warehouse : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    public SqlConnection con_warehouse = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    //MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    string distid = "";
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] == null)
            {
                Response.Redirect("~/Session_Expire_Dist.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        save.Enabled = true;
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        if (Page.IsPostBack == false)
        {
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            get_do_no();
           // get_vehicle();
            get_transport();
        }
        

        tx_bags.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_qty_to_issue.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");      
        tx_bags .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balance_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_gatepass .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issue_balqty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issued_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue .Style["TEXT-ALIGN"] = TextAlign.Right.ToString(); 
        PostwhrData();
    }

    protected void get_do_no()
    {
        Label1.Text = "";
        tx_qty_to_issue.Text = "";
        tx_bags.Text = "";
        tx_gatepass.Text = "";
        tx_balance_qty.Text = "";
        tx_do_qty.Text = "";
        tx_do_validity.Text = "";
        tx_issue_balqty.Text = "";
        tx_issued_qty.Text = "";
        tx_issueto.Text = "";
        tx_issueto_name.Text = "";
        string temp = "YYY";
        string dist = distid;
        ddl_do_no.Items.Clear();
        //string issue_centre_code = sid;
        cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where district_code='" + dist + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " order by  created_date desc";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddl_do_no.Items.Add(dr["delivery_order_no"].ToString());
            temp = "NNN";
        }
        ddl_do_no.Items.Insert(0, "Select");
        dr.Close();
        con.Close();
        if (temp == "YYY")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Delivery Order No. Not Found please Select Allotment Month";
            ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Delivery Order No. Not Found please Select Allotment Month ...');</script>");
        }

    }
    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        showwhr.Enabled = false ;
        lbl_issuestock.Visible = false;
        lbl_stock.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        whrdata.Visible = false;
        tx_qty_to_issue.Text = "";
        tx_bags.Text = "";
        if (ddl_do_no.SelectedItem.Text == "Select")
        {
            Label1.Text = "";
            tx_qty_to_issue.Text = "";
            tx_bags.Text = "";
            tx_gatepass.Text = "";
            tx_balance_qty.Text = "";
            tx_do_qty.Text = "";
            tx_do_validity.Text = "";
            tx_issue_balqty.Text = "";
            tx_issued_qty.Text = "";
            tx_issueto.Text = "";
            tx_issueto_name.Text = "";
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Delivery Order No. ...";
            ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
        }
        else
        {
            get_do_data();
        }

    }
    protected void save_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;

        if (ddl_do_no.SelectedItem.Text == "Select")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            Label1.Text = "Please Select Delivery Order No. ...";
        }
        else
        {
            string dist = distid;
            string issue_centre_code = sid;
            string issue_date=getDate_MDY(tx_issued_date .Text );
            string do_no = ddl_do_no.SelectedItem.Text;
            string gate_pass = tx_gatepass.Text;
            decimal do_qty = decimal.Parse(tx_balance_qty.Text);
            decimal issue_qty = decimal.Parse(tx_qty_to_issue.Text);

            if (issue_qty > do_qty || issue_qty <=0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Quantity To Issue can not be greater  than DO Quantity');</script>");
                Label1.Text = "Error: Quantity To Issue can not be greater than DO Quantity";
                tx_qty_to_issue.Text = "";
                tx_qty_to_issue.Focus();
            }
            else
            {
                int rcount = 0;
                string docount = "";
                string strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_do  where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "'and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                cmd.CommandText = strqr;
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    docount = dr["rwcount"].ToString();
                }
                dr.Close();
                con.Close();
                if (docount != "")
                {
                    rcount = int.Parse(docount);
                }
                rcount = rcount + 1;
                string transid = dist.ToString() + do_no.ToString() + (rcount).ToString();
                decimal lift_qty = 0;              
                string str2 = "select lift_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "'";
                cmd.Connection = con;
                cmd.CommandText = str2;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lift_qty  =decimal .Parse( dr["lift_qty"].ToString());                    
                }
                dr.Close();
                con.Close();
                string str1 = "INSERT INTO dbo.issue_against_do(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,issue_to,qty_issue,bags,issue_date,gate_pass,created_date,updated_date) VALUES('"+ transid +"','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + Session["issueto"].ToString() + "'," + tx_qty_to_issue.Text + "," + tx_bags.Text + ",'" + issue_date + "','" + gate_pass + "',getdate(),'')";
                cmd.CommandText = str1;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();                    
                    
                    //GetSelected();
                    //get_do_data();
                    lift_qty = lift_qty + decimal.Parse(tx_qty_to_issue.Text);
                    str1 = "update dbo.sum_trans_do set lift_qty=" + lift_qty + " where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                    cmd.CommandText = str1;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    update_Issue_Balance();
                    ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                    Label1.Text = "Data Saved Successfully ...";
                    panel_do.Enabled = false;
                    save.Enabled = false;
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }                
                //tx_qty_to_issue.Text = "";
                //tx_bags.Text = "";
                //tx_gatepass.Text = "";
                //ddl_do_no.SelectedIndex = 0;
               
            }
        }

    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
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
        int noofdays = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
        int d = (m * noofdays) + d2;
        int day = d - d1;
        return day.ToString();
    }
    protected void get_do_data()
    {
        Label1.ForeColor = System.Drawing.Color.Blue;
        ddl_godown.Items.Clear();
        tx_qty_to_issue.Enabled = true;
        tx_bags.Enabled = true;
        tx_qty_to_issue.Text="";
        tx_bags.Text = "";
        Label1.Text = "";
        tx_qty_to_issue.Text = "";
        tx_bags.Text = "";
        tx_gatepass.Text = "";
        //tx_issued_date.Enabled = true ;
        tx_gatepass.Enabled = true;
        string do_no = ddl_do_no.SelectedItem.Text;
        string dist = distid;
        string issue_centre_code = sid;
        //string bal_qty = "";
        string issueqty = "";
        string issueto_name = "";
        string do_valid = "";
        DateTime do_date = new DateTime();
        string do_qty = "";
        string lead_code = "";
        lst_comm.Items.Clear();
        cmd.CommandText = "SELECT round(delivery_order_mpscsc.quantity,5) as quantity,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity,delivery_order_mpscsc.issue_type,round(SUM(issue_against_do.qty_issue),5) AS issueqty,tbl_MetaData_STORAGE_COMMODITY.commodity_id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name FROM dbo.delivery_order_mpscsc LEFT JOIN dbo.issue_against_do ON delivery_order_mpscsc.delivery_order_no = issue_against_do.delivery_order_no AND delivery_order_mpscsc.district_code = issue_against_do.district_code and delivery_order_mpscsc.issueCentre_code = issue_against_do.issueCentre_code Left Join dbo.tbl_MetaData_STORAGE_COMMODITY on delivery_order_mpscsc.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY.Commodity_Id GROUP BY delivery_order_mpscsc.delivery_order_no,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.issueCentre_code, delivery_order_mpscsc.district_code, delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity, delivery_order_mpscsc.issue_type,delivery_order_mpscsc.quantity,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,tbl_MetaData_STORAGE_COMMODITY.commodity_id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  having delivery_order_mpscsc.delivery_order_no='" + do_no + "' and delivery_order_mpscsc.district_code='" + dist + "' and delivery_order_mpscsc.issueCentre_code='" + issue_centre_code + "' and delivery_order_mpscsc.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and delivery_order_mpscsc.allotment_year=" + ddd_allot_year.SelectedItem.Text + " ";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            do_date = DateTime.Parse(dr["do_date"].ToString());
            do_valid = dr["do_validity"].ToString();
            issueto_name = dr["issue_type"].ToString();
            do_qty = dr["quantity"].ToString();
            issueqty = dr["issueqty"].ToString();
            lead_code= dr["issue_name"].ToString();
            ListItem lst = new ListItem();
            lst.Text = dr["Commodity_Name"].ToString();
            lst.Value = dr["Commodity_Id"].ToString();
            lst_comm.Items.Add(lst);
            lst_comm.SelectedIndex=0;
        }

        dr.Close();
        con.Close();
        Session["issueto"] = lead_code;
        if (issueto_name == "L")
        {
            tx_issueto.Text = "Lead Society";
            cmd.CommandText = "SELECT LeadSoc_nameU FROM dbo.m_LeadSoc where District_code='" + distid + "' and LeadSoc_Code='"+ lead_code +"'";
            cmd.Connection = con_opdms;
            con_opdms.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                tx_issueto_name.Text = dr["LeadSoc_nameU"].ToString();
            }

            dr.Close();
            con_opdms.Close();
             
        }

        if (issueto_name == "F")
        {
            tx_issueto.Text = "FPS";
            tx_issueto_name.Text ="";
        }
        if (issueto_name == "O")
        {
            tx_issueto.Text = "Others";
            tx_issueto_name.Text = lead_code ;
            
        }
        if (issueqty == "")
        {
            issueqty = "0";
        }

        SqlDataAdapter da = new SqlDataAdapter("select do_fps.*,opdms.pds.fps_master.fps_name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name,do_fps.quantity*rate_per_qtls as amt from dbo.do_fps LEFT JOIN opdms.pds.fps_master ON do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id where do_fps.delivery_order_no='" + do_no + "' and do_fps.district_code='" + dist + "' and do_fps.issueCentre_code='" + issue_centre_code + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.status='N'", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "do_fps");
        GridView1.DataSource = ds.Tables["do_fps"];
        GridView1.DataBind();
        GridView1.Columns[8].Visible = false;
        GridView1.Columns[9].Visible = false;
        //string ddate = ;
        tx_do_date.Text = getdate(do_date.ToString());
        tx_do_validity.Text = changeDate(do_date, int.Parse(do_valid));
        //tx_do_validity.Text = int.Parse(do_valid) + days + "/" + do_date.Month.ToString() + "/" + do_date.Year.ToString();
        string do_valid_days = do_valid;
        string chk_valid_days = get_days(do_date, DateTime.Today.Date);
        if (int.Parse(chk_valid_days) > int.Parse(do_valid_days))
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Validity  has Expired...');</script>");
            Label1.Text = "Validity  has Expired ...";
            tx_qty_to_issue.Text = "";
            tx_bags.Text = "";
            //tx_issued_date.Text = "";
            tx_gatepass.Text = "";
            tx_qty_to_issue.Enabled = false;
            tx_bags.Enabled = false;
            //tx_issued_date.Enabled = false;
            tx_gatepass.Enabled = false;
            if (do_qty != "" && issueqty != "")
            {
                decimal qty = decimal.Parse(do_qty) - decimal.Parse(issueqty);
                tx_balance_qty.Text =System .Math .Round (qty,5).ToString();
            }

            tx_issue_balqty.Text = "";

        }
        else
        {
            int rdays = int.Parse(do_valid_days) - int.Parse(chk_valid_days);
            Label1.Text = "Remaining valid days : " + rdays.ToString();
            decimal qty = decimal.Parse(do_qty) - decimal.Parse(issueqty);
            tx_balance_qty.Text = System .Math .Round (qty,5).ToString();
            tx_issue_balqty.Text = System .Math .Round (qty,5).ToString();
            // For Godown List
            cmd.CommandText = "SELECT * from dbo.tbl_MetaData_GODOWN where StateId='23' and DistrictId='" + distid + "' and DepotId='" + sid + "'";
            cmd.Connection = con_warehouse;
            con_warehouse.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lst = new ListItem();
                lst.Text = dr["Godown_Name"].ToString();
                lst.Value = dr["Godown_ID"].ToString();
                ddl_godown.Items.Add(lst);
                showwhr.Enabled = true;
            }

            dr.Close();
            con_warehouse.Close();
        }

        tx_do_qty.Text = do_qty;
        tx_issued_qty.Text = issueqty;
       
        
    }

    private void GetSelected()
    {

        foreach (GridViewRow di in GridView1.Rows)
        {
            HtmlInputCheckBox chkBx = (HtmlInputCheckBox)di.FindControl("chkBoxId");

            if (chkBx != null && chkBx.Checked)
            {
                string fpscode = di.Cells[1].Text;
                string do_no = ddl_do_no.SelectedItem.Text;
                cmd.CommandText = "update dbo.do_fps set status='Y' where fps_code='" + fpscode + "' and delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_do_no();
        showwhr.Enabled = false;
        lbl_issuestock.Visible = false;
        lbl_stock.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        whrdata.Visible = false;
        tx_qty_to_issue.Text = "";
        tx_bags.Text = "";
    }
    protected string  changeDate(DateTime inDate, int inDays)
    {
        int noofdays = DateTime.DaysInMonth(inDate.Year,inDate .Month);
        int count = 1;
        int xday = inDate.Day;
        int xmonth = inDate.Month;
        int xyear = inDate.Year;
        while (count <= inDays)
        {
            xday = xday + 1;
            if (xday > noofdays)
            {
                xday = 1;
                xmonth = xmonth + 1;
                if (xmonth > 12)
                {
                    xyear = xyear + 1;
                    xmonth = 1;
                }
                noofdays = DateTime.DaysInMonth(xyear, xmonth);
            }
            count = count + 1;
        }
        return (xday+"/"+xmonth+"/"+xyear);
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
    protected void update_Issue_Balance()
    {
        string dist = distid;
        string issue_centre_code = sid;       
        string do_no = ddl_do_no.SelectedItem.Text;        
        decimal issue_qty = decimal.Parse(tx_qty_to_issue.Text);
        int count=0;
        decimal totqty = 0;
        int rcount = 0;
        string docount = "";
        string strqr = "select count(delivery_order_no) as rwcount  from dbo.Issued_do_fps  where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "'and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
        cmd.CommandText = strqr;
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            docount = dr["rwcount"].ToString();
        }
        dr.Close();
        con.Close();
        if (docount != "")
        {
            rcount = int.Parse(docount);
        }
        while ( count< GridView1.Rows.Count )
            {
                string fpscode = GridView1.Rows[count].Cells[1].Text;
                string comm = GridView1.Rows[count].Cells[8].Text;
                string scheme = GridView1.Rows[count].Cells[9].Text; 
                decimal do_qty=decimal.Parse(GridView1.Rows[count].Cells[5].Text);
                string comm_name = GridView1.Rows[count].Cells[3].Text;
                string scheme_name = GridView1.Rows[count].Cells[4].Text; 
                decimal ariqty = 0;
                string alrqty = "";
                string str1 = "";
                str1 = "select sum(lift_qty) as lift_qty  from dbo.Issued_do_fps  where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "'and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and fps_code='" + fpscode + "' and commodity='" + comm + "' and scheme_id='" + scheme + "'";
                cmd.CommandText = str1;
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    alrqty = dr["lift_qty"].ToString();                    
                }
                dr.Close();
                con.Close();
                if (alrqty != "")
                {
                    ariqty = decimal.Parse(alrqty);
                }
                do_qty = do_qty - ariqty;
                totqty = totqty + do_qty;
                rcount = rcount + 1;
                string transid = dist.ToString() + do_no.ToString() + (rcount).ToString();
                decimal tot =decimal.Parse(System .Math .Round (totqty,5).ToString());

            // for FPS Allot 
                string ipadd = "";
                string qty_lift_old = "";
                decimal qty_lift_new = 0;
                string comm_str = "";
                if (scheme_name.ToLower() == "apl")
                {
                    if (comm_name.ToLower().Contains("wheat"))
                    {
                        comm_str = "wheat_apl_lift";
                    }
                    if (comm_name.ToLower().Contains("rice"))
                    {
                        comm_str = "rice_apl_lift";
                    } 
                }
                if (scheme_name.ToLower() == "bpl")
                {
                    if (comm_name.ToLower().Contains("wheat"))
                    {
                        comm_str = "wheat_bpl_lift";
                    }
                    if (comm_name.ToLower().Contains("rice"))
                    {
                        comm_str = "rice_bpl_lift";
                    }                   
                }
                if (scheme_name.ToLower() == "aay")
                {
                    if (comm_name.ToLower().Contains("wheat"))
                    {
                        comm_str = "wheat_aay_lift";
                    }
                    if (comm_name.ToLower().Contains("rice"))
                    {
                        comm_str = "rice_aay_lift";
                    }                    
                }
                if (comm_name.ToLower().Contains("sugar"))
                {
                    comm_str = "sugar_lift";
                }

                str1 = "select " + comm_str + " ,lft_insert  from pds.fps_allot  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                cmd.CommandText = str1;
                cmd.Connection=con_opdms ;
                con_opdms.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    qty_lift_old = dr[comm_str].ToString();
                    ipadd = dr["lft_insert"].ToString();
                }
                dr.Close();
                con_opdms.Close();

                if (tot <= issue_qty)
                {
                    if (ipadd == "")
                    {
                        if (qty_lift_old == "")
                        {
                            qty_lift_old = "0";
                        }
                        qty_lift_new = decimal.Parse(qty_lift_old) + do_qty;
                        str1 = "update pds.fps_allot set " + comm_str + "=" + qty_lift_new + "  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                        cmd.CommandText = str1;
                        cmd.Connection = con_opdms;
                        con_opdms.Open();
                        cmd.ExecuteNonQuery();
                        con_opdms.Close();
                    }
                    else
                    {
                        //if (qty_lift_old == "")
                        //{
                        //    qty_lift_old = "0";
                        //}
                        //qty_lift_new = decimal.Parse(qty_lift_old);
                        //if (qty_lift_new == 0)
                        //{
                        //    qty_lift_new = qty_lift_new + do_qty;
                        //    str1 = "update pds.fps_allot set " + comm_str + "=" + qty_lift_new + "  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                        //    cmd.CommandText = str1;
                        //    cmd.Connection = con_opdms;
                        //    con_opdms.Open();
                        //    cmd.ExecuteNonQuery();
                        //    con_opdms.Close();
                        //}                        
                    }

                    str1 = "update dbo.do_fps set status='Y' where fps_code='" + fpscode + "' and delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and commodity='" + comm + "' and scheme_id='" + scheme + "'";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date)VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "'," + do_qty + "," + do_qty + ",getdate())";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();                   
                    str1 = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + do_qty + " where District_Id='" + dist + "'and Depotid='" + issue_centre_code + "'and Commodity_Id='" + comm + "'and Scheme_Id='" + scheme + "'";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    string qrystock = "select Sum(lift_qty) as Qty from dbo.Issued_do_fps where Commodity ='" + comm + "'and district_code='" + dist + "'and issueCentre_code='" + issue_centre_code + "'and allotment_month=" + ddl_allot_month.SelectedItem.Value + "and allotment_year=" + ddd_allot_year.SelectedItem.Text;
                    mobj = new MoveChallan(ComObj);
                    DataSet dsstock = mobj.selectAny(qrystock);

                    if (dsstock.Tables[0].Rows.Count == 0)
                    {

                    }
                    else
                    {
                        DataRow drop = dsstock.Tables[0].Rows[0];
                        decimal mobal = 0;
                        decimal mrp = 0;
                        decimal mrod = 0;
                        decimal msod = 0;
                        decimal msdelo = CheckNull(drop["Qty"].ToString());
                        decimal mrfci = 0;
                        string mremark = "";


                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comm + "'and DistrictId ='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + ddl_allot_month.SelectedItem.Value + "and Year=" + ddd_allot_year.SelectedItem.Text;
                        mobj = new MoveChallan(ComObj);
                        DataSet dsopen = mobj.selectAny(qryinsopen);
                        if (dsopen.Tables[0].Rows.Count == 0)
                        {
                            string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Sale_Do,Sale_otherg,Month,Year,Remarks) Values('" + distid + "','" + issue_centre_code + "','" + comm + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + msdelo + "," + msod + "," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + mremark + "')";
                            cmd.CommandText = qryins;                            
                            cmd.ExecuteNonQuery();                           

                        }
                        else
                        {
                            string qryinsU = "update dbo.tbl_Stock_Registor set Sale_Do=" + msdelo + " where Commodity_Id ='" + comm + "'and DistrictId='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + ddl_allot_month.SelectedItem.Value + "and Year=" + ddd_allot_year.SelectedItem.Text;
                            cmd.CommandText = qryinsU;                           
                            cmd.ExecuteNonQuery();
                            
                        }

                    }

                    con.Close();
                }
                else
                {
                    decimal diff = totqty - issue_qty;
                    decimal final_qty = do_qty - diff;
                    if (ipadd == "")
                    {
                        if (qty_lift_old == "")
                        {
                            qty_lift_old = "0";
                        }
                        qty_lift_new = decimal.Parse(qty_lift_old) + final_qty;
                        str1 = "update pds.fps_allot set " + comm_str + "=" + qty_lift_new + "  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                        cmd.CommandText = str1;
                        cmd.Connection = con_opdms;
                        con_opdms.Open();
                        cmd.ExecuteNonQuery();
                        con_opdms.Close();
                    }

                    con.Open();
                    str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date)VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "'," + do_qty + "," + final_qty + ",getdate())";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    str1 = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + final_qty + " where District_Id='" + dist + "'and Depotid='" + issue_centre_code + "'and Commodity_Id='" + comm + "'and Scheme_Id='" + scheme + "'";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string qrystock = "select Sum(lift_qty) as Qty from dbo.Issued_do_fps where commodity ='" + comm + "'and district_code='" + dist + "'and issueCentre_code='" + issue_centre_code + "'and allotment_month=" + ddl_allot_month.SelectedItem.Value + "and allotment_year=" + ddd_allot_year.SelectedItem.Text;
                            mobj = new MoveChallan(ComObj);
                            DataSet dsstock = mobj.selectAny(qrystock);

                            if (dsstock.Tables[0].Rows.Count == 0)
                            {

                            }
                            else
                            {
                                DataRow drop = dsstock.Tables[0].Rows[0];
                                decimal mobal = 0;
                                decimal mrp = 0;
                                decimal mrod = 0;
                                decimal msod = 0;
                                decimal msdelo = CheckNull(drop["Qty"].ToString());
                                decimal mrfci = 0;
                                string mremark = "";


                                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + comm + "'and DistrictId ='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + ddl_allot_month.SelectedItem.Value + "and Year=" + ddd_allot_year.SelectedItem.Text;
                                mobj = new MoveChallan(ComObj);
                                DataSet dsopen = mobj.selectAny(qryinsopen);
                                if (dsopen.Tables[0].Rows.Count == 0)
                                {
                                    string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Sale_Do,Sale_otherg,Month,Year,Remarks) Values('" + distid + "','" + issue_centre_code + "','" + comm + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + msdelo + "," + msod + "," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + mremark + "')";
                                    cmd.CommandText = qryins;
                                    
                                    cmd.ExecuteNonQuery();
                                    

                                }
                                else
                                {
                                    string qryinsU = "update dbo.tbl_Stock_Registor set Sale_Do=" + msdelo + " where Commodity_Id ='" + comm + "'and DistrictId='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + ddl_allot_month.SelectedItem.Value + "and Year=" + ddd_allot_year.SelectedItem.Text;
                                    cmd.CommandText = qryinsU;
                                  
                                    cmd.ExecuteNonQuery();
                                  
                                }

                            }

                    con.Close();
                    break;
                }
                count = count + 1;
            }
        

    }
    protected void showwhr_Click(object sender, EventArgs e)
    {
        tx_qty_to_issue.Text = "";
        tx_bags.Text = "";        
        if (ddl_godown.Items.Count > 0)
        {
            string str1 = "SELECT tbl_MetaData_STACK.Stack_ID, tbl_MetaData_STACK.Stack_Name, tbl_storage_Stacking_Details.Bags, tbl_storage_Stacking_Details.Weight, tbl_storage_Stacking_Details.WHRId,tbl_storage_Depositor_WHR_Relation.Lot_No FROM   tbl_MetaData_STACK INNER JOIN  tbl_storage_Stacking_Details ON tbl_MetaData_STACK.State_Id = tbl_storage_Stacking_Details.State_Id AND tbl_MetaData_STACK.District_Id = tbl_storage_Stacking_Details.District_Id AND tbl_MetaData_STACK.DepotId = tbl_storage_Stacking_Details.Depotid AND  tbl_MetaData_STACK.Godown_ID = tbl_storage_Stacking_Details.Godown_ID AND  tbl_MetaData_STACK.Stack_ID = tbl_storage_Stacking_Details.Stack_ID  AND   tbl_storage_Stacking_Details.Status='Y'   INNER JOIN  tbl_storage_Depositor_WHR_Relation ON tbl_storage_Stacking_Details.State_Id = tbl_storage_Depositor_WHR_Relation.State_Id AND tbl_storage_Stacking_Details.District_Id = tbl_storage_Depositor_WHR_Relation.District_Id AND tbl_storage_Stacking_Details.Depotid = tbl_storage_Depositor_WHR_Relation.Depotid AND tbl_storage_Stacking_Details.WHRId = tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id AND tbl_storage_Depositor_WHR_Relation.Depositor_Name='MPSCSC' WHERE   tbl_MetaData_STACK.State_Id='23' and  tbl_MetaData_STACK.District_Id = '" + distid + "'  AND tbl_MetaData_STACK.DepotId ='" + sid + "'  AND  tbl_MetaData_STACK.Godown_ID = '" + ddl_godown.SelectedItem.Value + "' and tbl_MetaData_STACK.Commodity_Id=" + lst_comm.SelectedValue + "";
            SqlDataAdapter da = new SqlDataAdapter(str1 , con_warehouse);
            DataSet ds = new DataSet();
            da.Fill(ds, "whr");
            grid_whr.DataSource = ds.Tables["whr"];
            grid_whr.DataBind();
            if (grid_whr.Rows.Count > 0)
            {
                lbl_issuestock.Visible = false;
                lbl_stock.Visible = true ;
                Panel2.Visible = true;
                Panel3.Visible = false;
                whrdata.Visible = true ;
            }
            else 
            {
                lbl_issuestock.Visible = false;
                lbl_stock.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
                whrdata.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('WHR No. Not Found ...');</script>");
            }
       }
       
    }

    void PostwhrData()
    {
        tx_qty_to_issue.Text = "";
        tx_bags.Text = "";
        if (grid_stock_issue.Rows.Count == 0)
        {
            //message
        }
        else
        {
            int ibags = 0;
            decimal iwt = 0;
            string totbags = "";
            string totwt = "";
            decimal balqty = CheckNull(tx_balance_qty.Text);
            decimal issuedqty = 0;
            foreach (GridViewRow gr in grid_stock_issue.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("chkSelectAll");
                TextBox txbg = (TextBox)gr.Cells[3].FindControl("TextBox1");
                TextBox txwt = (TextBox)gr.Cells[4].FindControl("TextBox2");
                DropDownList lststack = (DropDownList)gr.Cells[7].FindControl("ddl_stack");
                txbg.ReadOnly = false ;
                txwt.ReadOnly = false ;
                lststack.Enabled =true ;                
                if (GchkBx.Checked == true)
                {
                    if (CheckNullInt(txbg.Text) > 0 && CheckNull(txwt.Text) > 0)
                    {
                        int abags = int.Parse(gr.Cells[5].Text);
                        decimal aweight = decimal.Parse(gr.Cells[6].Text);
                        issuedqty = issuedqty + decimal.Parse(txwt.Text);            
                        if (issuedqty > balqty)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Entered Weight can not be greater than Balance Quantity ...');</script>");
                            GchkBx.Checked = false;
                        }
                        else if (int.Parse(txbg.Text) > abags || decimal.Parse(txwt.Text) > aweight)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Entered Bags and Weight can not be greater than Available Bags and Weight ...');</script>");
                            GchkBx.Checked = false;
                        }
                        else if (int.Parse(txbg.Text) < abags && lststack.SelectedItem.Value =="Y")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Entered Bags and Weight must be equalto Available Bags and Weight for Stack Killing ...');</script>");
                            GchkBx.Checked = false;
                        }
                        else
                        {
                            ibags = ibags + int.Parse(txbg.Text);
                            iwt = iwt + decimal.Parse(txwt.Text);
                            totbags = ibags.ToString();
                            totwt = iwt.ToString();
                            txbg.ReadOnly = true;
                            txwt.ReadOnly = true;
                            lststack.Enabled = false ;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "asdsad", "<script language=javascript > alert('Please enter Bags and Weight...');</script>");
                        GchkBx.Checked = false;
                    }
                }                
                
            }
            tx_qty_to_issue.Text = totwt.ToString();
            tx_bags.Text  = totbags.ToString();
        }
    }
    private void GetwhrData()
    {
        if (grid_whr.Rows.Count > 0)
        {
            grid_stock_issue.Columns[5].Visible = true;
            grid_stock_issue.Columns[6].Visible = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("whr");
            dt.Columns.Add("stack");
            dt.Columns.Add("bags");
            dt.Columns.Add("weight");
            dt.Columns.Add("abags");
            dt.Columns.Add("aweight");
            string temp = "NNN";
            foreach (GridViewRow gr in grid_whr.Rows)
            {
                HtmlInputCheckBox chkBx = (HtmlInputCheckBox)gr.FindControl("chkBoxId");
                if (chkBx != null && chkBx.Checked)
                {
                    string whr = gr.Cells[1].Text;
                    string stackid = gr.Cells[2].Text;
                    string abags = gr.Cells[4].Text;
                    string aweight = gr.Cells[5].Text;
                    string bags = "";
                    string weight = "";
                    dt.Rows.Add(whr, stackid, bags, weight, abags, aweight);
                    lbl_issuestock.Visible = true;
                    temp = "YYY";
                }
            }
            Session["dt_whr"] = dt;           
            grid_stock_issue.DataSource = dt;            
            grid_stock_issue.DataBind();
            grid_stock_issue.Columns[5].Visible = false ;
            grid_stock_issue.Columns[6].Visible = false ;
            if (temp == "NNN")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(),"asdsad", "<script language=javascript > alert('Please select CheckBox ...');</script>");
            }
        }        
    }
    protected void whrdata_Click(object sender, EventArgs e)
    {
        lbl_issuestock.Visible = false;             
        GetwhrData();              
        Panel3.Visible = true;
        tx_qty_to_issue.Text = "";
        tx_bags.Text = "";
    }
    protected void grid_stock_issue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
          
       if(e.Row.RowType == DataControlRowType.DataRow)
        {
             TextBox txbg = (TextBox)e.Row.Cells[3].FindControl("TextBox1");
             TextBox txwt = (TextBox)e.Row.Cells[4].FindControl("TextBox2");
             txbg.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
             txwt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
             txbg.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
             txwt.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");            
        }
    }
    protected void get_vehicle()
    {
        ddl_vtype.Items.Clear();
        cmd.CommandText = "select * from dbo.tbl_MetaData_Vehicle_Type";
        cmd.Connection = con_warehouse;
        con_warehouse.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Vehicle_Type"].ToString();
            lst.Value = dr["Vehicle_Type_ID"].ToString();
            ddl_vtype.Items.Add(lst);           
        }
        dr.Close();
        con_warehouse.Close();        

    }
    protected void ddl_vtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_vtype.SelectedItem.Value == "V1")
        {
            tx_driver.Visible = false;
            lbl_driver.Visible = false;
            tx_gatepass.Visible = false;
            TextBox3.Visible = false;
            lbl_vno.Visible = false;
            lbl_valid.Visible = false;
            lbl_licence.Visible = false;
        }
        else
        {
            tx_driver.Visible = true ;
            lbl_driver.Visible = true;
            tx_gatepass.Visible = true;
            TextBox3.Visible = true;
            lbl_vno.Visible = true;
            lbl_valid.Visible = true;
            lbl_licence.Visible = true;
        }
    }
    protected void get_transport()
    {
        ddl_transport.Items.Clear();
        cmd.CommandText = "select * from dbo.Transporter_Table where Distt_ID='"+ distid +"'";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Transporter_Name"].ToString();
            lst.Value = dr["Transporter_ID"].ToString();
            ddl_transport.Items.Add(lst);
        }
        ddl_transport.Items.Insert(0,"Select");
        dr.Close();
        con.Close();

    }
    protected void ddd_allot_year_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
}
    

