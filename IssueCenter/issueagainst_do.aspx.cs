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
using System.Data.Sql;
public partial class issueagainst_do : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());


    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd_opdms = new SqlCommand();
    SqlDataReader dr;
    SqlDataReader dr_opdms;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    string distid = "";
    string sid = "";
    public string version = "";

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

        btnsave.Enabled = true;
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        version = Session["hindi"].ToString();
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Page.IsPostBack == false)
        {
            Session["issubmited"] = "No";

            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            get_do_no();
            GetGodown();
            get_comm();            
            get_scheme();
            GetSource();
            get_lead();

            get_Transporter();

            tx_issued_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            if (version == "H")
            {
                lbl_lead.Text = Resources.LocalizedText.lbl_lead;
                lblallotyear.Text = Resources.LocalizedText.lblallotyear;
                lblallotmonth.Text = Resources.LocalizedText.lblallotmonth;
                lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                lblScheme.Text = Resources.LocalizedText.lblScheme;
                lblbalcomdty.Text = Resources.LocalizedText.lblbalcomdty;
                lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                lblNoofBags.Text = Resources.LocalizedText.lblNoofBags;
                lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                lbl_issueqty.Text = Resources.LocalizedText.lbl_issueqty;
                lblbalqty.Text = Resources.LocalizedText.lblbalqty;
                lbldispsource.Text = Resources.LocalizedText.lbldispsource;
                lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                lblDispatchQty.Text = Resources.LocalizedText.lblDispatchQty;
                lblissuedate.Text = Resources.LocalizedText.lblissuedate;
                lbltoissue.Text = Resources.LocalizedText.lbltoissue;
                lbldono.Text = Resources.LocalizedText.lbldono;
                lbldodate.Text = Resources.LocalizedText.lbldodate;
                lbldovalidity.Text = Resources.LocalizedText.lbldovalidity;
                lbl_issueqty.Text = Resources.LocalizedText.lbl_issueqty;
                lbl_balqty.Text = Resources.LocalizedText.lbl_balqty;
                lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                btn_new.Text = Resources.LocalizedText.btn_new;
                btnsave.Text = Resources.LocalizedText.btnsave;
                btnclose.Text = Resources.LocalizedText.btnclose;

            }



        }       

        tx_bags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_qty_to_issue.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

        tx_bags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_bags.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty_to_issue.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty_to_issue.Attributes.Add("onchange", "return chksqltxt(this)");


        tx_gatepass.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_gatepass.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_gatepass.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_issued_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_issued_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_issued_date.Attributes.Add("onchange", "return chksqltxt(this)");

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
    }

    protected void get_do_no()
    {
        try
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
            cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where district_code='" + dist + "' and issueCentre_code='" + sid + "' and status='N' and issue_type <> 'FCI' and delivery_order_no not like '%NoDO%' order by  created_date desc";
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
        catch (Exception)
        { }
    }
    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
                tx_cur_bal.Text = "";
                tx_cur_bags.Text = "";
                tx_do_date.Text = "";
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Please Select Delivery Order No. ...";
                GridView1.DataSource = null;
                GridView1.DataBind();
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            }
            else
            {
                string dist = distid;

                string checkdo = "Select issue_type,Transporter  from delivery_order_mpscsc where district_code='" + dist + "' and issueCentre_code='" + sid + "' and delivery_order_no = '" + ddl_do_no.SelectedItem.Text + "' ";

                SqlCommand cmd_dotype = new SqlCommand(checkdo, con);
                SqlDataAdapter da_dotype = new SqlDataAdapter(cmd_dotype);
                DataSet ds_dotype = new DataSet();

                da_dotype.Fill(ds_dotype);

                string typeofDO_Issue = ds_dotype.Tables[0].Rows[0]["issue_type"].ToString();

                if (typeofDO_Issue == "MPSCSC")
                {
                    ddl_lead.Visible = false;

                    ddltransporter.Visible = true;

                    lbl_lead.Visible = true;

                    string transprter = ds_dotype.Tables[0].Rows[0]["Transporter"].ToString();

                    ddltransporter.SelectedValue = transprter;

                    ddl_godown.SelectedIndex = 0;
                    ddlsarrival.SelectedIndex = 0;
                    get_do_data();
                }

                else
                    if (typeofDO_Issue == "F")
                    {
                        ddl_lead.Visible = false;

                        ddltransporter.Visible = false;


                        ddl_commodity.Visible = false;

                        lblCommodity.Visible = false;

                        lblScheme.Visible = false;

                        ddl_scheme.Visible = false;
                        lbl_lead.Visible = false;


                        ddl_godown.SelectedIndex = 0;
                        ddlsarrival.SelectedIndex = 0;
                        get_do_data();
                    }

                else
                {
                    ddl_commodity.Visible = true;

                    lblCommodity.Visible = true;

                    lblScheme.Visible = true;

                    ddl_scheme.Visible = true;

                    ddl_lead.Visible = true;

                    ddltransporter.Visible = false;
                    lbl_lead.Visible = true;

                    ddl_godown.SelectedIndex = 0;
                    ddlsarrival.SelectedIndex = 0;
                    get_do_data();
                }

                
            }
        }
        catch (Exception)
        { 
        
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
        try
        {
            btnsave.Enabled = true;
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
            cmd.CommandText = "SELECT delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,round(convert(decimal(18,5),delivery_order_mpscsc.quantity),5) as quantity,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity,delivery_order_mpscsc.issue_type,round(SUM(convert(decimal(18,5),issue_against_do.qty_issue)),5) AS issueqty FROM dbo.delivery_order_mpscsc LEFT JOIN dbo.issue_against_do ON delivery_order_mpscsc.delivery_order_no = issue_against_do.delivery_order_no AND delivery_order_mpscsc.district_code = issue_against_do.district_code and delivery_order_mpscsc.issueCentre_code = issue_against_do.issueCentre_code GROUP BY delivery_order_mpscsc.delivery_order_no,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.issueCentre_code, delivery_order_mpscsc.district_code, delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity, delivery_order_mpscsc.issue_type,delivery_order_mpscsc.quantity,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id  having delivery_order_mpscsc.delivery_order_no='" + do_no + "' and delivery_order_mpscsc.district_code='" + dist + "' and delivery_order_mpscsc.issueCentre_code='" + issue_centre_code + "'";
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
                lead_code = dr["issue_name"].ToString();
                ddl_allot_month.SelectedValue = dr["allotment_month"].ToString();
                try
                {
                    ddd_allot_year.SelectedValue = dr["allotment_year"].ToString();
                }
                catch (Exception)
                { }
                ddl_commodity.SelectedValue = dr["commodity_id"].ToString();
                ddl_scheme.SelectedValue = dr["scheme_id"].ToString();
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

                Panel1.Height = 1;
                GridView1.Height = 1;

                GridView1.Visible = false;

                Panel1.Visible = false;

            }

            if (issueto_name == "F")
            {
                tx_issueto.Text = "FPS";
                ddl_lead.Visible = false;
                tx_lead.Visible = false;
                tx_lead.Text = "FPS";

                Panel1.Height = 140;
                GridView1.Height = 140;

                GridView1.Visible = true;

                Panel1.Visible = true;
            }
            if (issueto_name == "O")
            {
                ddl_lead.Visible = false;
                tx_lead.Visible = true;
                tx_issueto.Text = "Others";
                tx_lead.Text = lead_code;

                Panel1.Height = 1;
                GridView1.Height = 1;

            }
            if (issueqty == "")
            {
                issueqty = "0";
            }

            SqlDataAdapter da = new SqlDataAdapter("select do_fps.*,opdms.pds.fps_master.fps_Uname,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name,round(convert(decimal(18,5),do_fps.quantity),5) as qty,round(convert(decimal(18,5),rate_per_qtls),2) as rateqtls,round(convert(decimal(18,5),do_fps.quantity)*convert(decimal(18,5),rate_per_qtls),2) as amt from dbo.do_fps LEFT JOIN opdms.pds.fps_master ON do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id where do_fps.delivery_order_no='" + do_no + "' and do_fps.district_code='" + dist + "' and do_fps.issueCentre_code='" + issue_centre_code + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.status='N'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "do_fps");
            GridView1.DataSource = ds.Tables["do_fps"];
            GridView1.DataBind();
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = false;
            //string ddate = ;
            tx_do_date.Text = getdate(do_date.ToString());
            tx_do_validity.Text = changeDate(do_date, CheckNullInt(do_valid));
            Session["do_valid"] = do_valid;
            //tx_do_validity.Text = CheckNullInt(do_valid) + days + "/" + do_date.Month.ToString() + "/" + do_date.Year.ToString();
            //string do_valid_days = do_valid;
            //string chk_valid_days = get_days(do_date, DateTime.Today.Date);
            //if (CheckNullInt(chk_valid_days) > CheckNullInt(do_valid_days))
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
            //        decimal qty = CheckNull(do_qty) - CheckNull(issueqty);
            //        tx_balance_qty.Text =System .Math .Round (qty,5).ToString();
            //    }

            //    tx_issue_balqty.Text = "";

            //}
            //else
            //{
            //int rdays = CheckNullInt(do_valid_days) - CheckNullInt(chk_valid_days);
            //Label1.Text = "Remaining valid days : " + rdays.ToString();
            decimal qty = CheckNull(do_qty) - CheckNull(issueqty);
            tx_balance_qty.Text = System.Math.Round(qty, 5).ToString();
            tx_issue_balqty.Text = System.Math.Round(qty, 5).ToString();
            //}

            tx_do_qty.Text = do_qty;
            tx_issued_qty.Text = issueqty;
        }
        catch (Exception)
        { }
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
    protected decimal CheckNull(string Val)
    {
        decimal rval = 0;
        if (Val.Trim() == "" || Val.ToLower().Contains("n") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = Convert.ToDecimal(Val);
        }
        return rval;
    }
    protected int CheckNullInt(string Val)
    {
        int rval = 0;
        if (Val.Trim() == "" || Val.ToLower().Contains("n") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = int.Parse(Val);
        }
        return rval;
    }
    protected void update_Issue_Balance()
    {
        try
        {
            string dist = distid;
            string issue_centre_code = sid;
            string do_no = ddl_do_no.SelectedItem.Text;
            decimal issue_qty = CheckNull(tx_qty_to_issue.Text);
            int count = 0;
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
                rcount = CheckNullInt(docount);
            }
            while (count < GridView1.Rows.Count)
            {
                string fpscode = GridView1.Rows[count].Cells[1].Text;
                string comm = GridView1.Rows[count].Cells[8].Text;
                string scheme = GridView1.Rows[count].Cells[9].Text;
                decimal do_qty = CheckNull(GridView1.Rows[count].Cells[5].Text);
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
                    ariqty = CheckNull(alrqty);
                }
                do_qty = do_qty - ariqty;
                totqty = totqty + do_qty;
                rcount = rcount + 1;
                string transid = dist.ToString() + do_no.ToString() + (rcount).ToString();
                decimal tot = CheckNull(System.Math.Round(totqty, 5).ToString());
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

                if (tot <= issue_qty)
                {
                    if (ipadd == "")
                    {
                        if (qty_lift_old == "")
                        {
                            qty_lift_old = "0";
                        }
                        qty_lift_new = CheckNull(qty_lift_old) + do_qty;
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
                        //qty_lift_new = CheckNull(qty_lift_old);
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
                    str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add)VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "'," + do_qty + "," + do_qty + ",getdate(),'" + ip + "')";
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
                        qty_lift_new = CheckNull(qty_lift_old) + final_qty;
                        str1 = "update pds.fps_allot set " + comm_str + "=" + qty_lift_new + "  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                        cmd.CommandText = str1;
                        cmd.Connection = con_opdms;
                        con_opdms.Open();
                        cmd.ExecuteNonQuery();
                        con_opdms.Close();
                    }

                    con.Open();
                    str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add)VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "'," + do_qty + "," + final_qty + ",getdate(),'" + ip + "')";
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
        catch (Exception)
        { }

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";

        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void GetGodown()
    {
        ddl_godown.Items.Clear();
        //mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DepotId='" + sid + "' order by Godown_ID";

        SqlCommand cmdgdn = new SqlCommand(qry, cons);

        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }

        SqlDataAdapter dagdn = new SqlDataAdapter(cmdgdn);

        DataSet dsgdn = new DataSet();

        dagdn.Fill(dsgdn);
      
        ddl_godown.DataSource = dsgdn.Tables[0];
        ddl_godown.DataTextField = "Godown_Name";
        ddl_godown.DataValueField = "Godown_ID";
        ddl_godown.DataBind();
        ddl_godown.Items.Insert(0, "Select");

        if (cons.State == ConnectionState.Open)
        {
            cons.Close();
        }
    }
    void GetBalQty()
    {
        try
        {
            string mcomid = ddl_commodity.SelectedItem.Value;
            string mscheme = ddl_scheme.SelectedItem.Value;
            string mgodown = ddl_godown.SelectedItem.Value;
            tx_cur_bags.Text = "0";
            tx_cur_bal.Text = "0";
            mobj1 = new MoveChallan(ComObj);

            string comodity = "";
            if (ddl_commodity.SelectedItem.Text.ToLower().Contains("wheat"))
            {
                comodity = "wheat";
            }
            if (ddl_commodity.SelectedItem.Text.ToLower().Contains("rice"))
            {
                comodity = "rice";
            }
            if (ddl_commodity.SelectedItem.Text.ToLower().Contains("sugar"))
            {
                comodity = "sugar";
            }

            if (ddl_commodity.SelectedItem.Text.ToLower().Contains("A.Salt"))
            {
                comodity = "A.Salt";
            }

            string comdty = "";

            string qrycom = "Select Commodity_ID from dbo.tbl_MetaData_STORAGE_COMMODITY where lower(Commodity_Name) like'%" + comodity + "%'";
            cmd.CommandText = qrycom;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comdty = comdty + dr["Commodity_ID"].ToString();
                comdty = comdty + ",";
            }
            dr.Close();
            int ind;
            ind = comdty.LastIndexOf(",");
            comdty = comdty.Remove(ind, 1);


            string qry = "";
           // qry = "Select Sum(convert(decimal(18,5),Current_Balance))as Current_Balance,Sum(Current_Bags)as Current_Bags from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id in ("+comdty+") and Godown='" + mgodown + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";

            qry = "Select Sum(convert(decimal(18,5),Current_Balance))as Current_Balance,Sum(Current_Bags)as Current_Bags from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id in (" + comdty + ") and Godown='" + mgodown + "'";

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
                    DataRow dr1 = ds.Tables[0].Rows[0];
                    tx_cur_bags.Text = dr1["Current_Bags"].ToString();
                    tx_cur_bal.Text = dr1["Current_Balance"].ToString();

                }
            }
        }
        catch (Exception)
        { }

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
        try
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
        catch (Exception)
        { }

    }
    protected void get_scheme()
    {
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME   where status='Y' order by Scheme_Id";
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
    void GetSource()
    {
        try
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
        catch (Exception)
        { }
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void get_lead()
    {
        try
        {
            string dist = distid;
            ddl_lead.Items.Clear();
            cmd.CommandText = "select * from dbo.m_LeadSoc where District_code='" + distid + "'";
            cmd.Connection = con_opdms;
            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }
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
            if (con_opdms.State == ConnectionState.Open)
            {
                con_opdms.Close();
            }
        }
        catch (Exception)
        { }
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";

        Response.Redirect("~/IssueCenter/issueagainst_do.aspx");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Session["issubmited"].ToString() == "Yes")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Double Click Attempt, pls try again');</script>");
        }
        else
        {

            string opid = Session["OperatorId"].ToString();
            string state = Session["State_Id"].ToString();
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
            //else if (CheckNull(tx_cur_bal.Text) < CheckNull(tx_qty_to_issue.Text) || CheckNullInt(tx_cur_bags.Text) < CheckNullInt(tx_bags.Text))
            //{
            //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no balance for Selected Source and Godown...');</script>");
            //    Label1.Text = "There is no balance for Selected Source and Godown...";
            //}
            else
            {
                string do_valid_days = Session["do_valid"].ToString();
                string issueddate = "";
                if (tx_issued_date.Text == "")
                {
                    issueddate = DateTime.Today.Date.ToString();
                }
                else
                {
                    issueddate = tx_issued_date.Text;

                }

                DateTime frmDate = DateTime.Parse(getDate_MDY(tx_do_date.Text.Trim()));
                DateTime toDate = DateTime.Parse(getDate_MDY(issueddate));
                string chk_valid_days = get_days(frmDate, toDate);

                if (CheckNullInt(chk_valid_days) > CheckNullInt(do_valid_days))
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Validity  has Expired please check Issued Date ...');</script>");
                    Label1.Text = "Validity  has Expired please check Issued Date ...";
                }

                else if (CheckNullInt(chk_valid_days) < 0)
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
                    decimal do_qty = CheckNull(tx_balance_qty.Text);
                    decimal issue_qty = CheckNull(tx_qty_to_issue.Text);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string notrans = "N";
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
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        int trnscnt = 0;
                        string docount = "";
                        string transid = "";
                        string strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                        cmd.CommandText = strqr;
                        cmd.Connection = con;
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            docount = dr["rwcount"].ToString();
                        }
                        dr.Close();
                        strqr = "select trans_id from dbo.issue_against_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                        cmd.CommandText = strqr;
                        cmd.Connection = con;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            transid = dr["trans_id"].ToString();
                        }
                        dr.Close();
                        int indx1 = 0;
                        int indx2 = 0;
                        if (transid != "")
                        {
                            indx1 = transid.IndexOf("-");
                            indx2 = transid.LastIndexOf("-");
                        }
                        int indx = indx2 - indx1;
                        if (CheckNullInt(docount) > 0 && indx <= 0)
                        {
                            trnscnt = CheckNullInt(docount) + 1;
                            transid = dist.ToString() + do_no.ToString() + (trnscnt).ToString();
                        }
                        else
                        {
                            strqr = "select max(convert(int,right(trans_id,len(trans_id)-len(allotment_year)-len(district_code)-len(delivery_order_no)-3))) as rwcount  from dbo.issue_against_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                            cmd.CommandText = strqr;
                            cmd.Connection = con;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                docount = dr["rwcount"].ToString();
                            }
                            dr.Close();
                            if (CheckNullInt(docount) < 0)
                            {
                                docount = "0";
                            }
                            if (docount != "")
                            {
                                trnscnt = CheckNullInt(docount);
                            }
                            trnscnt = trnscnt + 1;
                            transid = ddd_allot_year.SelectedItem.Text + "-" + dist.ToString() + "-" + do_no.ToString() + "-" + (trnscnt).ToString();
                        }
                        decimal lift_qty = 0;
                        string str2 = "select round(convert(decimal(18,5),lift_qty),5) as lift_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                        cmd.Connection = con;
                        cmd.CommandText = str2;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lift_qty = CheckNull(dr["lift_qty"].ToString());
                        }
                        dr.Close();
                        if (con_opdms.State == ConnectionState.Closed)
                        {
                            con_opdms.Open();
                        }
                        SqlTransaction Trans_csms;
                        Trans_csms = con.BeginTransaction();
                        SqlTransaction Trans_opdms;
                        Trans_opdms = con_opdms.BeginTransaction();
                        cmd.Connection = con;
                        cmd_opdms.Connection = con_opdms;
                        cmd.Transaction = Trans_csms;
                        cmd_opdms.Transaction = Trans_opdms;

                        try
                        {
                            string IssuetoName = "";
                            if (tx_issueto.Text == "Lead Society")
                            {
                                IssuetoName = ddl_lead.SelectedItem.Text;
                            }

                            else
                            {
                                IssuetoName = "FPS";
                            }


                            if (txtgatepass.Text == "")
                            {
                                txtgatepass.Text = "0";
                            }

                            string str1 = "INSERT INTO dbo.issue_against_do(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Issue_to_LS_name,Gatepass) VALUES('" + state + "','" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + Session["issueto"].ToString() + "'," + tx_qty_to_issue.Text + "," + tx_bags.Text + ",'" + ddlsarrival.SelectedItem.Value + "','" + ddl_godown.SelectedItem.Value + "','" + issue_date + "','" + gate_pass + "',getdate(),'','N','" + ip + "','" + opid + "','" + notrans + "',N'" + IssuetoName + "','" + txtgatepass.Text.Trim() + "')";
                            cmd.CommandText = str1;
                            cmd.ExecuteNonQuery();

                            //GetSelected();
                            //get_do_data();
                            lift_qty = lift_qty + CheckNull(tx_qty_to_issue.Text);
                            str1 = "update dbo.sum_trans_do set lift_qty=round(" + lift_qty + ",5) where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                            cmd.CommandText = str1;
                            cmd.ExecuteNonQuery();
                            str1 = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + ",Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + CheckNull(tx_qty_to_issue.Text) + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)+" + CheckNull(tx_qty_to_issue.Text) + ",5) where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + ddl_godown.SelectedItem.Value + "'";
                            cmd.CommandText = str1;
                            cmd.ExecuteNonQuery();
                            //update_Issue_Balance();  

                            {
                                int rcount = 0;
                                string dist1 = distid;
                                string issue_centre_code1 = sid;
                                string ip1 = Request.ServerVariables["REMOTE_ADDR"].ToString();


                                foreach (GridViewRow di in GridView1.Rows)
                                {
                                    HtmlInputCheckBox chkBx = (HtmlInputCheckBox)di.FindControl("chkBoxId");
                                    if (chkBx != null && chkBx.Checked)
                                    {
                                        rcount = rcount + 1;
                                        string fpscode = di.Cells[1].Text;
                                        string do_no1 = ddl_do_no.SelectedItem.Text;
                                        string comm = di.Cells[8].Text;
                                        string scheme = di.Cells[9].Text;
                                        string blkcode = di.Cells[10].Text;
                                        decimal do_qty1 = CheckNull(di.Cells[5].Text);
                                        string comm_name = di.Cells[3].Text;
                                        string scheme_name = di.Cells[4].Text;
                                        string ipadd = "";
                                        decimal qty_lift_old = 0;
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



                                        if (scheme_name.ToLower() == "priority households")
                                        {
                                            if (comm_name.ToLower().Contains("wheat"))
                                            {
                                                comm_str = "wheat_phh_lift";
                                            }
                                            if (comm_name.ToLower().Contains("rice"))
                                            {
                                                comm_str = "rice_phh_lift";
                                            }
                                        }

                                        if (comm_str == "")
                                        {

                                        }

                                        else
                                        {
                                            str1 = "select " + comm_str + " ,lft_insert  from pds.fps_allot  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                                            cmd_opdms.CommandText = str1;
                                            dr_opdms = cmd_opdms.ExecuteReader();

                                            while (dr_opdms.Read())
                                            {
                                                qty_lift_old = CheckNull(dr_opdms[comm_str].ToString());
                                                ipadd = dr_opdms["lft_insert"].ToString();
                                            }

                                            dr_opdms.Close();

                                            if (qty_lift_old == 0)
                                            {
                                                qty_lift_new = qty_lift_old + do_qty1;
                                                str1 = "update pds.fps_allot set " + comm_str + "=round(" + qty_lift_new + ",5)  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                                                cmd_opdms.CommandText = str1;
                                                cmd_opdms.ExecuteNonQuery();
                                                string lftdate = comm_str + "_date";
                                                str1 = "update pds.fps_lift_date set " + lftdate + "=getdate()  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                                                cmd_opdms.CommandText = str1;
                                                cmd_opdms.ExecuteNonQuery();
                                            }
                                            //////////////////////////////////////////////////

                                            string strr = "";
                                            strr = "update pds.fps_allot_mpsc set " + comm_str + "=isnull(convert(decimal(18,5)," + comm_str + "),0)+round(" + do_qty1 + ",5),lft_updated_on=getdate()  where district_code='" + distid + "' and block_code='" + blkcode + "' and  depot_code='" + sid + "' and  fps_code='" + fpscode + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
                                            cmd.CommandText = strr;
                                            cmd.ExecuteNonQuery();
                                            int trnscnt1 = 0;
                                            string docount1 = "";



                                            string strqr1 = "select count(delivery_order_no) as rwcount  from dbo.Issued_do_fps where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                                            cmd.CommandText = strqr1;
                                            cmd.Connection = con;
                                            dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                docount1 = dr["rwcount"].ToString();
                                            }
                                            dr.Close();
                                            strqr1 = "select trans_id from dbo.issue_against_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                                            cmd.CommandText = strqr1;
                                            cmd.Connection = con;
                                            dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                transid = dr["trans_id"].ToString();
                                            }
                                            dr.Close();
                                            indx1 = 0;
                                            indx2 = 0;
                                            if (transid != "")
                                            {
                                                indx1 = transid.IndexOf("-");
                                                indx2 = transid.LastIndexOf("-");
                                            }
                                            indx = indx2 - indx1;
                                            if (CheckNullInt(docount1) > 0 && indx <= 0)
                                            {
                                                trnscnt1 = CheckNullInt(docount1) + 1;
                                                transid = dist1.ToString() + do_no1.ToString() + (trnscnt1).ToString();
                                            }
                                            else
                                            {
                                                strqr1 = "select max(convert(int,right(trans_id,len(trans_id)-len(allotment_year)-len(district_code)-len(delivery_order_no)-3))) as rwcount  from dbo.Issued_do_fps where delivery_order_no='" + do_no1 + "' and district_code='" + dist1 + "'";
                                                cmd.CommandText = strqr1;
                                                dr = cmd.ExecuteReader();
                                                while (dr.Read())
                                                {
                                                    docount1 = dr["rwcount"].ToString();
                                                }
                                                dr.Close();
                                                if (CheckNullInt(docount1) < 0)
                                                {
                                                    docount1 = "0";
                                                }
                                                if (docount1 != "")
                                                {
                                                    trnscnt1 = CheckNullInt(docount1);
                                                }
                                                trnscnt1 = trnscnt1 + 1;
                                                transid = ddd_allot_year.SelectedItem.Text + "-" + dist1.ToString() + "-" + do_no1.ToString() + "-" + (trnscnt1).ToString();
                                            }

                                        }

                                        str1 = "INSERT INTO dbo.Issued_do_fps(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add,OperatorID)VALUES('" + state + "','" + transid + "','" + do_no1 + "','" + dist1 + "','" + issue_centre_code1 + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "',round(" + do_qty1 + ",5),round(" + do_qty1 + ",5),getdate(),'" + ip1 + "','" + opid + "')";
                                        cmd.CommandText = str1;
                                        cmd.ExecuteNonQuery();
                                        str1 = "update dbo.do_fps set status='Y' where fps_code='" + fpscode + "' and delivery_order_no='" + do_no1 + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                                        cmd.CommandText = str1;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (CheckNull(tx_qty_to_issue.Text) >= CheckNull(tx_balance_qty.Text))
                                {
                                    string strdo = "update dbo.delivery_order_mpscsc  set status='Y' where delivery_order_no='" + ddl_do_no.SelectedItem.Text + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                                    cmd.CommandText = strdo;
                                    cmd.ExecuteNonQuery();
                                }
                                string tempos = "NNN";
                                string strope = "";
                                strope = "select *  from dbo.tbl_Stock_Registor  where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                cmd.CommandText = strope;
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    tempos = "YYY";
                                }
                                dr.Close();
                                if (tempos == "YYY")
                                {
                                    strope = "update dbo.tbl_Stock_Registor set Sale_Do=round(convert(decimal(18,5),Sale_Do)+" + CheckNull(tx_qty_to_issue.Text) + ",5) where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                    cmd.CommandText = strope;
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    decimal opebal = 0;
                                    strope = "select sum(convert(decimal(18,5),Current_Balance)) as opebal  from dbo.issue_opening_balance  where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'";
                                    cmd.CommandText = strope;
                                    dr = cmd.ExecuteReader();

                                    while (dr.Read())
                                    {
                                        opebal = CheckNull(dr["opebal"].ToString());
                                    }
                                    dr.Close();
                                    strope = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + ddl_commodity.SelectedItem.Value + "','" + ddl_scheme.SelectedItem.Value + "'," + opebal + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + tx_qty_to_issue.Text + "," + 0 + "," + 0 + "," + 0 + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'')";
                                    cmd.CommandText = strope;
                                    cmd.ExecuteNonQuery();
                                }
                                //
                                tempos = "NNN";
                                strope = "select *  from dbo.issue_opening_balance  where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + ddl_godown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
                                cmd.CommandText = strope;
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    tempos = "YYY";
                                }
                                dr.Close();
                                if (tempos == "YYY")
                                {
                                    strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + CheckNull(tx_qty_to_issue.Text) + ",5),Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + " where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + ddl_godown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
                                    cmd.CommandText = strope;
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    strope = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('23','" + distid + "','" + sid + "','" + ddl_commodity.SelectedItem.Value + "','" + ddl_scheme.SelectedItem.Value + "','','" + ddl_godown.SelectedItem.Value + "','',0,0,'" + ddlsarrival.SelectedItem.Value + "'," + -CheckNull(tx_qty_to_issue.Text) + "," + -CheckNullInt(tx_bags.Text) + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'" + ip1 + "',getdate(),getdate(),'','')";
                                    cmd.CommandText = strope;
                                    cmd.ExecuteNonQuery();
                                }

                            }
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                            Label1.Text = "Data Saved Successfully ...";
                            ddl_do_no.SelectedIndex = 0;
                            //panel_do.Enabled = false;
                            btnsave.Enabled = false;
                            Trans_csms.Commit();
                            Trans_opdms.Commit();

                            Session["issubmited"] = "Yes";

                        }
                        catch (Exception ex)
                        {
                            dr.Close();
                            // dr_opdms.Close();                  //// Object ref not set
                            Trans_csms.Rollback();
                            Trans_opdms.Rollback();
                            Label1.Text = ex.Message;
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            if (con_opdms.State == ConnectionState.Open)
                            {
                                con_opdms.Close();
                            }
                        }
                        //tx_qty_to_issue.Text = "";
                        //tx_bags.Text = "";
                        //tx_gatepass.Text = "";
                        //ddl_do_no.SelectedIndex = 0;

                    }
                }
            }
        }
    }

    protected void get_Transporter()
    {
        try
        {
            string dist = distid;
            ddltransporter.Items.Clear();
            cmd.CommandText = "SELECT distinct Transport_ID,Transporter_Name from Transporter_Table where Distt_ID ='" + distid + "' order by Transporter_Name";
            cmd.Connection = con;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["Transporter_Name"].ToString();
                lstitem.Value = dr["Transport_ID"].ToString();
                ddltransporter.Items.Add(lstitem);
            }
            ListItem lstitem1 = new ListItem();
            lstitem1.Text = "Select";
            lstitem1.Value = "N";
            ddltransporter.Items.Insert(0, lstitem1);
            dr.Close();

            if (con.State == ConnectionState.Open)
            {

                con.Close();
            }
        }
        catch (Exception)
        {
        }
    }
}
    

