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

public partial class issueagainst_do1 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    MoveChallan mobj1 = null;
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
            GetGodown();
            get_comm();            
            get_scheme();
            get_lead();
        }
        

        tx_bags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_qty_to_issue.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");      
        tx_bags .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balance_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_gatepass .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issue_balqty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issued_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_cur_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_cur_bags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
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
        tx_lead.Text = "";        
        string dist = distid;
        ddl_do_no.Items.Clear();
        //string issue_centre_code = sid;
        cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where district_code='" + dist + "' and issueCentre_code='" + sid + "' and status='N'  and issue_type <> 'FCI' order by  created_date desc";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddl_do_no.Items.Add(dr["delivery_order_no"].ToString());            
        }
        ddl_do_no.Items.Insert(0, "Select");
        dr.Close();
        con.Close();      

    }
    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
    {
      
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
            tx_lead.Text = "";
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Delivery Order No. ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
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
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            Label1.Text = "Please Select Delivery Order No. ...";
        }
        else if (ddl_godown.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Godown ...');</script>");
            Label1.Text = "Please Select Godown ...";
        }
        else
        {
            string do_valid_days = Session["do_valid"].ToString();
            string chk_valid_days = get_days(DateTime.Parse(getDate_MDY(tx_do_date.Text.Trim())), DateTime.Parse(getDate_MDY(tx_issued_date.Text)));
            if (int.Parse(chk_valid_days) > int.Parse(do_valid_days))
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Validity  has Expired please check Issued Date ...');</script>");
                Label1.Text = "Validity  has Expired please check Issued Date ...";
            }
            else if (int.Parse(chk_valid_days) < 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Issued Date can not be less than DO Date please check Issued Date ...');</script>");
                Label1.Text = "Issued Date can not be less than DO Date please check Issued Date ...";
            }
            else
            {
                string dist = distid;
                string issue_centre_code = sid;
                string issue_date = getDate_MDY(tx_issued_date.Text);
                string do_no = ddl_do_no.SelectedItem.Text;
                string gate_pass = tx_gatepass.Text;
                decimal do_qty = decimal.Parse(tx_balance_qty.Text);
                decimal issue_qty = decimal.Parse(tx_qty_to_issue.Text);
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (issue_qty > do_qty || issue_qty <= 0)
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity To Issue can not be greater  than DO Quantity');</script>");
                    Label1.Text = "Error: Quantity To Issue can not be greater than DO Quantity";
                    tx_qty_to_issue.Text = "";
                    tx_qty_to_issue.Focus();
                }
                else
                {
                    int rcount = 0;
                    string docount = "";
                    string strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_do  where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
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
                        lift_qty = decimal.Parse(dr["lift_qty"].ToString());
                    }
                    dr.Close();
                    con.Close();
                    string str1 = "INSERT INTO dbo.issue_against_do(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add) VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + Session["issueto"].ToString() + "'," + tx_qty_to_issue.Text + "," + tx_bags.Text + ",'" + ddlsarrival.SelectedItem.Value + "','" + ddl_godown.SelectedItem.Value + "','" + issue_date + "','" + gate_pass + "',getdate(),'','N','" + ip + "')";
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
                        str1 = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + ",Current_Balance=Current_Balance-" + CheckNull(tx_qty_to_issue.Text) + ",Current_Capacity=Current_Capacity+" + CheckNull(tx_qty_to_issue.Text) + " where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + ddl_godown.SelectedItem.Value + "'";
                        cmd.CommandText = str1;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //update_Issue_Balance();
                        GetSelected();
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                        Label1.Text = "Data Saved Successfully ...";
                        //panel_do.Enabled = false;
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

    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
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
        tx_qty_to_issue.Enabled = true;
        tx_bags.Enabled = true;
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
        cmd.CommandText = "SELECT delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,round(delivery_order_mpscsc.quantity,5) as quantity,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity,delivery_order_mpscsc.issue_type,round(SUM(issue_against_do.qty_issue),5) AS issueqty FROM dbo.delivery_order_mpscsc LEFT JOIN dbo.issue_against_do ON delivery_order_mpscsc.delivery_order_no = issue_against_do.delivery_order_no AND delivery_order_mpscsc.district_code = issue_against_do.district_code and delivery_order_mpscsc.issueCentre_code = issue_against_do.issueCentre_code GROUP BY delivery_order_mpscsc.delivery_order_no,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.issueCentre_code, delivery_order_mpscsc.district_code, delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity, delivery_order_mpscsc.issue_type,delivery_order_mpscsc.quantity,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id  having delivery_order_mpscsc.delivery_order_no='" + do_no + "' and delivery_order_mpscsc.district_code='" + dist + "' and delivery_order_mpscsc.issueCentre_code='" + issue_centre_code + "'";
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
            ddl_allot_month.SelectedValue = dr["allotment_month"].ToString();
            ddd_allot_year.SelectedValue  = dr["allotment_year"].ToString();
            ddl_commodity.SelectedValue = dr["commodity_id"].ToString();
            ddl_scheme.SelectedValue  = dr["scheme_id"].ToString();
        }

        dr.Close();
        con.Close();
        Session["issueto"] = lead_code;
        if (issueto_name == "L" || issueto_name == "LF")
        {
            if (lead_code == "")
            {
                lead_code = "N";
            }
            tx_issueto.Text = "Lead Society";
            ddl_lead.Visible = true;
            tx_lead.Visible = false;
            ddl_lead.SelectedValue = lead_code;

        }

        if (issueto_name == "F")
        {
            tx_issueto.Text = "FPS";
            ddl_lead.Visible = false;
            tx_lead.Visible = false;
            tx_lead.Text = "";
        }
        if (issueto_name == "O")
        {
            ddl_lead.Visible = false;
            tx_lead.Visible = true;
            tx_issueto.Text = "Others";
            tx_lead.Text = lead_code;

        }
        if (issueqty == "")
        {
            issueqty = "0";
        }

        SqlDataAdapter da = new SqlDataAdapter("select do_fps.fps_code,do_fps.quantity,do_fps.commodity,do_fps.scheme_id,do_fps.rate_per_qtls,isnull(sum(Issued_do_fps.lift_qty),0) as liftqty,do_fps.quantity-isnull(sum(Issued_do_fps.lift_qty),0) as balqty,opdms.pds.fps_master.fps_name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name,do_fps.quantity*do_fps.rate_per_qtls as amt from dbo.do_fps LEFT JOIN opdms.pds.fps_master ON do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id LEFT JOIN dbo.Issued_do_fps ON do_fps.delivery_order_no=Issued_do_fps.delivery_order_no and do_fps.district_code=Issued_do_fps.district_code and do_fps.issueCentre_code=Issued_do_fps.issueCentre_code and do_fps.allotment_year=Issued_do_fps.allotment_year and do_fps.allotment_month=Issued_do_fps.allotment_month and do_fps.fps_code=Issued_do_fps.fps_code and Issued_do_fps.delivery_order_no='" + do_no + "' and Issued_do_fps.district_code='" + dist + "' and Issued_do_fps.issueCentre_code='" + issue_centre_code + "' and Issued_do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and Issued_do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " where do_fps.delivery_order_no='" + do_no + "' and do_fps.district_code='" + dist + "' and do_fps.issueCentre_code='" + issue_centre_code + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.status='N' group by do_fps.fps_code,do_fps.quantity,do_fps.commodity,do_fps.scheme_id,do_fps.rate_per_qtls,opdms.pds.fps_master.fps_name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "do_fps");
        GridView1.DataSource = ds.Tables["do_fps"];
        GridView1.DataBind();
        //GridView1.Columns[8].Visible = false;
        //GridView1.Columns[9].Visible = false;
        //string ddate = ;
        tx_do_date.Text = getdate(do_date.ToString());
        tx_do_validity.Text = changeDate(do_date, int.Parse(do_valid));
        Session["do_valid"] = do_valid;
        //tx_do_validity.Text = int.Parse(do_valid) + days + "/" + do_date.Month.ToString() + "/" + do_date.Year.ToString();
        //string do_valid_days = do_valid;
        //string chk_valid_days = get_days(do_date, DateTime.Today.Date);
        //if (int.Parse(chk_valid_days) > int.Parse(do_valid))
        //{
        //    Label1.ForeColor = System.Drawing.Color.Red;
        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Validity  has Expired...');</script>");
        //    Label1.Text = "Validity  has Expired ...";
        //    tx_qty_to_issue.Text = "";
        //    tx_bags.Text = "";
        //    //tx_issued_date.Text = "";
        //    tx_gatepass.Text = "";
        //    tx_qty_to_issue.Enabled = false;
        //    tx_bags.Enabled = false;
        //    save.Enabled = false;
        //    ddl_godown.Enabled = false;
        //    ddlsarrival.Enabled = false;
        //    tx_gatepass.Enabled = false;
        //    if (do_qty != "" && issueqty != "")
        //    {
        //        decimal qty = decimal.Parse(do_qty) - decimal.Parse(issueqty);
        //        tx_balance_qty.Text =System .Math .Round (qty,5).ToString();
        //    }

        //    tx_issue_balqty.Text = "";

        //}
        //else
        //{
            //int rdays = int.Parse(do_valid_days) - int.Parse(chk_valid_days);
            //Label1.Text = "Remaining valid days : " + rdays.ToString();
            decimal qty = decimal.Parse(do_qty) - decimal.Parse(issueqty);
            tx_balance_qty.Text = System .Math .Round (qty,5).ToString();
            tx_issue_balqty.Text = System .Math .Round (qty,5).ToString();
        //}

        tx_do_qty.Text = do_qty;
        tx_issued_qty.Text = issueqty;
    }

    private void GetSelected()
    {
        int rcount = 0;
        string dist = distid;
        string issue_centre_code = sid; 
        foreach (GridViewRow di in GridView1.Rows)
        {
            CheckBox chkBx = (CheckBox)di.FindControl("chkSelectAll");
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (chkBx != null && chkBx.Checked)
            {                
                TextBox txwt = (TextBox)di.Cells[8].FindControl("TextBox1");
                string str1 = "";                
                string fpscode = di.Cells[1].Text;
                string do_no = ddl_do_no.SelectedItem.Text;                
                string comm = di.Cells[11].Text;
                string scheme = di.Cells[12].Text;
                decimal do_qty = CheckNull (di.Cells[7].Text);
                decimal issue_qty = CheckNull(txwt.Text);
                string comm_name = di.Cells[3].Text;
                string scheme_name = di.Cells[4].Text;
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
                cmd.Connection = con_opdms;
                con_opdms.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    qty_lift_old = dr[comm_str].ToString();
                    ipadd = dr["lft_insert"].ToString();
                }
                dr.Close();
                con_opdms.Close();
                if (ipadd == "" && qty_lift_old == "")
                {
                    if (qty_lift_old == "")
                    {
                        qty_lift_old = "0";
                    }
                    qty_lift_new = CheckNull(qty_lift_old) + issue_qty;
                    str1 = "update pds.fps_allot set " + comm_str + "=" + qty_lift_new + "  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                    cmd.CommandText = str1;
                    cmd.Connection = con_opdms;
                    con_opdms.Open();
                    cmd.ExecuteNonQuery();
                    con_opdms.Close();
                }
                    //  
                int trnscnt = 0;
                string docount = "";
                string strqr = "select count(delivery_order_no) as rwcount  from dbo.Issued_do_fps  where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                cmd.CommandText = strqr;
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    docount = dr["rwcount"].ToString();
                }
                dr.Close();                
                if (docount != "")
                {
                    trnscnt = int.Parse(docount);
                }
                trnscnt = trnscnt + 1;
                string transid = dist.ToString() + do_no.ToString() + (trnscnt).ToString();                                     
                    str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add)VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "'," + do_qty + "," + issue_qty + ",getdate(),'" + ip + "')";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    if (do_qty == issue_qty)
                    {
                        str1 = "update dbo.do_fps set status='Y' where fps_code='" + fpscode + "' and delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                        cmd.CommandText = str1;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        rcount = rcount + 1;
                    }                                                
                con.Close();
            }
        }
        if (CheckNull(tx_qty_to_issue.Text) == CheckNull(tx_balance_qty.Text))
        {
            string strdo = "update dbo.delivery_order_mpscsc  set status='Y' where delivery_order_no='" + ddl_do_no.SelectedItem.Text + "' and district_code='" + distid  + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
            cmd.CommandText = strdo; 
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();            
            con.Close();
        }
        string qryinsU = "update dbo.tbl_Stock_Registor set Sale_Do=Sale_Do+" + CheckNull(tx_qty_to_issue.Text) + " where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
        cmd.CommandText = qryinsU;
        cmd.Connection = con;
        con.Open();
        cmd.ExecuteNonQuery();
        //
        string strope = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + CheckNull(tx_qty_to_issue.Text) + ",Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + " where District_Id='" + dist + "'and Depotid='" + issue_centre_code + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + ddl_godown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
        cmd.CommandText = strope;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_do_no();  
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
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
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
                    str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add)VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "'," + do_qty + "," + do_qty + ",getdate(),'"+ ip +"')";
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
                    str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add)VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "'," + do_qty + "," + final_qty + ",getdate(),'"+ ip +"')";
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void GetGodown()
    {
        ddl_godown.Items.Clear();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + sid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);
        ddl_godown.DataSource = ds.Tables[0];
        ddl_godown.DataTextField = "Godown_Name";
        ddl_godown.DataValueField = "Godown_ID";
        ddl_godown.DataBind();
        ddl_godown.Items.Insert(0, "Select");
    }
    void GetBalQty()
    {
        string mcomid = ddl_commodity.SelectedItem.Value;
        string mscheme = ddl_scheme.SelectedItem.Value;
        string mgodown = ddl_godown.SelectedItem.Value;
        tx_cur_bags.Text = "0";
        tx_cur_bal.Text = "0";
        mobj1 = new MoveChallan(ComObj);
        string qry = "Select Sum(Current_Balance)as Current_Balance,Sum(Current_Bags)as Current_Bags from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Godown='" + mgodown + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
        DataSet ds = mobj1.selectAny(qry);
        if (ds == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
        }
        else
        {

            if (ds.Tables[0].Rows.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                tx_cur_bags.Text = dr["Current_Bags"].ToString();
                tx_cur_bal.Text = dr["Current_Balance"].ToString();

            }
        }

    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_do_no.SelectedItem.Text == "Select")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Delivery Order No. ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            ddl_godown.SelectedIndex = 0;
        }
        else if (ddlsarrival.SelectedItem.Text == "--Select--")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Stock Issued From ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Stock Issued From ...');</script>");
            ddl_godown.SelectedIndex = 0;
        }
        else if (ddl_godown.SelectedItem.Text == "Select")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Godown. ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Godown ...');</script>");
        }
        else
        {
            GetBalQty();
        }
    }
    protected void get_comm()
    {
        cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where status='Y'";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["commodity_name"].ToString();
            lstitem.Value = dr["commodity_id"].ToString();
            ddl_commodity.Items.Add(lstitem);
        }
        ListItem lstitem1 = new ListItem();
        lstitem1.Text = " ";
        lstitem1.Value = "N";
        ddl_commodity.Items.Insert(0, lstitem1);

        dr.Close();
        con.Close();

    }
    protected void get_scheme()
    {
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME  where status='Y' order by Scheme_Id ";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["scheme_name"].ToString();
            lstitem.Value = dr["scheme_id"].ToString();
            ddl_scheme.Items.Add(lstitem);
        }
        ddl_scheme.Items.Insert(0, " ");
        

        dr.Close();
        con.Close();
    }
    void PostwhrData()
    {
        tx_qty_to_issue.Text = "";
        //tx_bags.Text = "";
        if (GridView1.Rows.Count == 0)
        {
            //message
        }
        else
        {
            decimal iwt = 0;          
            string totwt = "";           
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("chkSelectAll");               
                TextBox txwt = (TextBox)gr.Cells[8].FindControl("TextBox1");
                decimal balqty = CheckNull(gr.Cells[7].Text);
                txwt.ReadOnly = false;
                if (GchkBx.Checked == true)
                {
                    if (CheckNull(txwt.Text) > 0)
                    {
                        decimal issueqty = CheckNull(txwt.Text);
                        if (issueqty > balqty)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "asdsad", "<script language=javascript > alert('Entered Quantity can not be greater than Balance Quantity ...');</script>");
                            GchkBx.Checked = false;
                        }                        
                        else
                        {
                            iwt = iwt + issueqty;                            
                            totwt = iwt.ToString();
                            txwt.ReadOnly = true;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "asdsad", "<script language=javascript > alert('Please enter Quantity ...');</script>");
                        GchkBx.Checked = false;
                    }
                }

            }
            tx_qty_to_issue.Text = totwt.ToString();           
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            TextBox txwt = (TextBox)e.Row.Cells[4].FindControl("TextBox1");           
            txwt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();           
            txwt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        }
    }
    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID <> '08' order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void get_lead()
    {
        string dist = distid;
        ddl_lead.Items.Clear();
        cmd.CommandText = "select * from dbo.m_LeadSoc where District_code='" + distid + "'";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["LeadSoc_nameU"].ToString();
            lstitem.Value = dr["LeadSoc_Code"].ToString();
            ddl_lead.Items.Add(lstitem);
        }
        ListItem lstitem1 = new ListItem();
        lstitem1.Text = "";
        lstitem1.Value = "N";
        ddl_lead.Items.Insert(0, lstitem1);
        dr.Close();
        con_opdms.Close();
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issueagainst_do1.aspx");
    }
}
    

