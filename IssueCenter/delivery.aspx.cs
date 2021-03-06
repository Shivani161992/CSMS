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
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
public partial class delivery : System.Web.UI.Page
{
    chksql chk = null;
    Districts DObj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string sid = "";
    DataTable dt = new DataTable();
    protected Common ComObj = null, cmn = null;
    public string cat = "";
    MoveChallan mobj = null;
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
       
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
      
        version = Session["hindi"].ToString();
        if (Page.IsPostBack == false)
        {
            save.Enabled = true;
            Session["issubmited"] = "N";

            if ((string)Session["dist_id"] == "21" || (string)Session["dist_id"] == "49" || (string)Session["dist_id"] == "31")
            {
                ddl_issueto.Items.Clear();
                ddl_issueto.Items.Add(new ListItem("Only FPS", "F"));
            }
            else
            {
                ddl_issueto.Items.Clear();
                ddl_issueto.Items.Add(new ListItem("Lead Society with FPS", "LF"));
                //ddl_issueto.Items.Add(new ListItem("Only Lead Society", "L"));
                ddl_issueto.Items.Add(new ListItem("Only FPS", "F"));
                //ddl_issueto.Items.Add(new ListItem("Others", "O"));
            }
            try
            {
                if (Session["Society"].ToString() == "F")
                {
                    ddl_issueto.Items.Clear();
                    
                    ddl_issueto.Items.Add(new ListItem("Only FPS", "F"));
                    ddl_issueto.Items.Add(new ListItem("Lead Society with FPS", "LF"));
                    ddl_lead.Visible = false;
                    lbl_lead.Visible = false;
                    lbl_alloc.Visible = false;
                    lbl_issue.Visible = false;
                    lbl_curbal.Visible = false;
                    lbl_stock.Visible = false;
                    lbl_qtalloc.Visible = false;
                    lbl_qtcur.Visible = false;
                    lbl_qtissue.Visible = false;
                    lbl_qtstk.Visible = false;
                    tx_allot_qty.Visible = false;
                    tx_already_iqty.Visible = false;
                    tx_bal_ic.Visible = false;
                    tx_balQty.Visible = false;
                }
            }
            catch (Exception)
            {
            
            }
            tx_dd_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            tx_permit_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            tx_do_date.Text = DateTime.Today.Date.Date.ToString("dd/MM/yyyy");
            tx_do_validity.Text = DateTime.Today.Date.Date.ToString("dd/MM/yyyy");


            dt.Columns.Add("block_code");
            dt.Columns.Add("fps_code");
            dt.Columns.Add("fps_name");
            dt.Columns.Add("commodity_id");
            dt.Columns.Add("commodity_name");
            dt.Columns.Add("scheme_id");
            dt.Columns.Add("scheme_name");
            dt.Columns.Add("rate_qtls");
            dt.Columns.Add("qty");
            dt.Columns.Add("amt");
            dt.Columns.Add("allot_qty");
            Session["dt"] = dt;

            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            get_comm();
            get_lead();
            get_block();
            get_scheme();
            get_bankname();
            GetName();
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            GetPermit();

            if (version == "H")
            {
                lblpermitno.Text = Resources.LocalizedText.lblpermitno;
                lblpermitdate.Text = Resources.LocalizedText.lblpermitdate;
                lbl_lead.Text = Resources.LocalizedText.lbl_lead;
                lblallotyear.Text = Resources.LocalizedText.lblallotyear;
                lblallotmonth.Text = Resources.LocalizedText.lblallotmonth;
                lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                lblScheme.Text = Resources.LocalizedText.lblScheme;
                lblbalcomdty.Text = Resources.LocalizedText.lblbalcomdty;
                lblratetype.Text = Resources.LocalizedText.lblratetype;
                lbl_alloc.Text = Resources.LocalizedText.lbl_alloc;
                lbl_issue.Text = Resources.LocalizedText.lbl_issue;
                lbltoissue.Text = Resources.LocalizedText.lbltoissue;
                lbl_curbal.Text = Resources.LocalizedText.lbl_curbal;
                lbl_stock.Text = Resources.LocalizedText.lbl_stock;
                lblpermitno1.Text = Resources.LocalizedText.lblpermitno1;

                //lbldono.Text = Resources.LocalizedText.lbldono;
                lbldodate.Text = Resources.LocalizedText.lbldodate;
                lbldovalidity.Text = Resources.LocalizedText.lbldovalidity;
                lbl_blk.Text = Resources.LocalizedText.lbl_blk;

                lbl_fps.Text = Resources.LocalizedText.lbl_fps;
                lbl_allotqty.Text = Resources.LocalizedText.lbl_allotqty;
                lbl_issueqty.Text = Resources.LocalizedText.lbl_issueqty;
                lbl_balqty.Text = Resources.LocalizedText.lbl_balqty;

                lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                totaldoqty.Text = Resources.LocalizedText.totaldoqty;
                lbldo.Text = Resources.LocalizedText.lbldo;
                lbldodetails.Text = Resources.LocalizedText.lbldodetails;
                lbldodetailsfps.Text = Resources.LocalizedText.lbldodetailsfps;

                lblRateQuintal.Text = Resources.LocalizedText.lblRateQuintal;
                lblPaymentMode.Text = Resources.LocalizedText.lblPaymentMode;
                lblddchekno.Text = Resources.LocalizedText.lblddchekno;
                lblddchekdate.Text = Resources.LocalizedText.lblddchekdate;
                lblBankName.Text = Resources.LocalizedText.lblBankName;
                lblamount.Text = Resources.LocalizedText.lblamount;
                lbltotamt.Text = Resources.LocalizedText.lbltotamt;
            }

        }

        //tx_do_no.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        //tx_permit_no.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_do_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        //tx_permit_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_rate_qt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_amount.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_amount.Attributes.Add("onchange", "return chksqltxt(this)");


        tx_rate_qt.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_rate_qt.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_permit_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_permit_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_permit_no.Attributes.Add("onchange", "return chksqltxt(this)");

        //tx_do_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        //tx_do_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        //tx_do_no.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_dd_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_no.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_amount.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_dd_amount.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_amount.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_do_validity.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_dd_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_permit_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");


        tx_do_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_date.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_date.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_permit_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_permit_date.Attributes.Add("onchange", "return chksqltxt(this)");



        hlinkpdo.Attributes.Add("onclick", "window.open('print_DeleveryOrder.aspx',null,'left=300, top=90, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        //BTNReview.Attributes.Add("onclick", "window.open('FRMViewAddedFPS_Delievry.aspx',null,'left=300, top=90, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

        tx_permit_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        //tx_do_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_validity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_rate_qt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_amt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_already_iqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txtcomdty_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balQty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_bal_ic.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balQty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_already_iqty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        chk = new chksql();
        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(tx_permit_no.Text);
        //ctrllist.Add(tx_do_no.Text);
        ctrllist.Add(tx_do_validity.Text);
        ctrllist.Add(tx_qty.Text);
        ctrllist.Add(tx_rate_qt.Text);

        ctrllist.Add(tx_do_date.Text);
        ctrllist.Add(tx_dd_date.Text);
        ctrllist.Add(tx_do_date.Text);
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
    }

    protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_blkFPS();
        txt_fps.Enabled = true;
    }
    protected void save_Click(object sender, EventArgs e)
    {
        if (Session["issubmited"].ToString() == "Yes")
        {

        }
        else
        {


            try
            {
                if (tx_tot_amt.Text == "")
                {
                    tx_tot_amt.Text = "0";
                }
                string totalamount = tx_tot_amt.Text;

                double amt = Convert.ToDouble(totalamount);

                string enteredamount = tx_dd_amount.Text;

                double ddamt = Convert.ToDouble(enteredamount);

                if (ddl_pmode.SelectedItem.Text == "Advance Payment" || ddl_pmode.SelectedItem.Text == "Free Scheme / Credit" || ddl_pmode.SelectedItem.Text == "Online Payment")
                {

                }

                else
                {
                    if (ddamt < amt)
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry DD/Check Amount Less than Actual amount is Not Permitted ......');</script>");
                        return;
                    }
                }


                // check here amount enter is not less than actual amount

                // tx_tot_amt     tx_dd_amount

                string opid = Session["OperatorId"].ToString();

                string state = Session["State_Id"].ToString();

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                //if (CheckNull(txtcomdty_bal.Text) == 0)
                //{
                //    Label1.Visible = true;
                //    Label1.Text = "Sorry You dont't have balance ....";
                //    Label1.ForeColor = System.Drawing.Color.Red;
                //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry You dont't have balance ......');</script>");
                //}
                
                    BTNReview.Enabled = false;
                    Label1.Text = "";
                    Label1.ForeColor = System.Drawing.Color.Blue;
                    dt = (DataTable)Session["dt"];
                    int count = dt.Rows.Count;

                    if (count > 0)
                    {
                        string dist = distid;
                        string issue_centre_code = sid;
                        string issue_type = ddl_issueto.SelectedItem.Value;
                        //string comm = ddl_commodity.SelectedItem.Value;
                        //string scheme = ddl_scheme.SelectedItem.Value;
                        string issue_name = "";
                        //string fps_code = "";
                        string month = ddl_allot_month.SelectedItem.Value;
                        int permit_valid = 0;

                        //Double qty = Convert.ToDouble(tx_qty.Text);


                        string cetId = issue_centre_code;

                        //string cetId = issue_centre_code.Substring(issue_centre_code.Length - 5);    // Left 5 digit from center for Do generation

                        string year_do = System.DateTime.Now.Date.ToString("yy");    // For DO generation year wise (29/03/14)

                        string selectmax = "select max(cast(delivery_order_no as bigint)) as Do_Num from delivery_order_mpscsc where district_code='" + dist + "' and issueCentre_code = '" + issue_centre_code + "' ";

                        SqlCommand cmdmax = new SqlCommand(selectmax, con);
                        SqlDataAdapter damax = new SqlDataAdapter(cmdmax);

                        DataSet dsmax = new DataSet();

                        damax.Fill(dsmax);


                        string DO_ID = dsmax.Tables[0].Rows[0]["Do_Num"].ToString();

                        if (DO_ID == "")
                        {
                            DO_ID = cetId + year_do + "1000";
                        }
                        else
                        {
                            string fordo = DO_ID.Substring(DO_ID.Length - 5);

                            Int64 DO_ID_new = Convert.ToInt64(fordo);

                            DO_ID_new = DO_ID_new + 1;

                            string combine = DO_ID_new.ToString();

                            DO_ID = cetId + year_do + combine;
                        }

                        string do_no = DO_ID;


                        //string do_no = tx_do_no.Text;
                        string per_no = tx_permit_no.Text;
                        string pmode = ddl_pmode.SelectedItem.Value;
                        string dd_no = tx_dd_no.Text;
                        string temp = "yyy";
                        string do_date = getDate_MDY(tx_do_date.Text);
                        string permit_date = "";
                        string dd_date = "";
                        string chkpermit = "";
                        string notrans = "N";

                        if (ddlpermit.SelectedItem.Text == "Not Indicated")
                        {
                            permit_date = getDate_MDY(tx_permit_date.Text);
                            dd_date = getDate_MDY(tx_dd_date.Text);
                        }
                        else
                        {
                            permit_date = getDate_MDY(tx_permit_date1.Text);
                            dd_date = getDate_MDY(tx_dd_date1.Text);
                            chkpermit = "Y";
                        }
                        // string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string do_validity = "";

                        int do_valid = 0;


                        string do_validityDate = getDate_MDY(tx_do_validity.Text);

                        try
                        {
                            do_validity = get_days(Convert.ToDateTime(do_date), Convert.ToDateTime(do_validityDate));

                            //do_validity = get_days(Convert.ToDateTime(tx_do_date.Text), Convert.ToDateTime(tx_do_validity.Text));

                            do_valid = CheckNullInt(do_validity);

                        }
                        catch (Exception)
                        {

                        }

                        //decimal rate_qt = new decimal();
                        //if (tx_rate_qt.Text == "")
                        //{
                        //    rate_qt = 0;
                        //}
                        //else
                        //{
                        //    rate_qt = CheckNull(tx_rate_qt.Text);
                        //}

                        cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where delivery_order_no='" + do_no + "' and issueCentre_code='" + issue_centre_code + "' and district_code='" + dist + "'";
                        cmd.Connection = con;
                        con.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            temp = "nnn";
                            break;
                        }
                        dr.Close();
                        con.Close();

                        if (temp == "yyy")
                        {

                            if (issue_type == "LF")
                            {
                                issue_name = ddl_lead.SelectedItem.Value;
                                //fps_code = ddl_fps_name.SelectedItem.Value;
                            }
                            else
                            {
                                issue_name = null;

                            }

                            //if (tx_permit_validity.Text == "")
                            //{
                            //    permit_valid = 0;
                            //}
                            //else
                            //{
                            //    permit_valid = Convert.ToInt32(tx_permit_validity.Text);
                            //}
                            decimal allot_qty = 0;
                            decimal issue_qty = 0;
                            string aqty = "";
                            string iqty = "";
                            decimal lift_qty = 0;
                            string temp1 = "NNN";
                            string str2 = "select round(convert(decimal(18,5),allot_qty),5) as allot_qty,round(convert(decimal(18,5),issue_qty),5) as issue_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + month.ToString() + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                            cmd.Connection = con;
                            cmd.CommandText = str2;
                            con.Open();
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                aqty = dr["allot_qty"].ToString();
                                iqty = dr["issue_qty"].ToString();
                                temp1 = "YYY";
                            }
                            dr.Close();
                            con.Close();
                            if (aqty == "")
                            {
                                aqty = "0";
                            }
                            if (iqty == "")
                            {
                                iqty = "0";
                            }
                            allot_qty = CheckNull(aqty);
                            issue_qty = CheckNull(iqty);

                            if (tx_rate_qt.Text == "")
                            {
                                tx_rate_qt.Text = "0";
                            }

                            if (tx_tot_amt.Text == "")
                            {
                                tx_tot_amt.Text = "0";
                            }

                            if (tx_tot_qty.Text == "")
                            {
                                tx_tot_qty.Text = "0";
                            }
                            save.Enabled = false;
                            string str1 = "INSERT INTO dbo.delivery_order_mpscsc(State_Id,delivery_order_no,district_code,issueCentre_code,permit_no,issue_type,issue_name,allotment_month,allotment_year,do_validity,permit_validity,do_date,permit_date,created_date,updated_date,payment_mode,dd_no,dd_date,quantity,amount,tot_amount,bank_id,commodity_id,scheme_id,rate_per_qtls,status,IP,OperatorID,NoTransaction) VALUES('" + state + "','" + do_no + "','" + dist + "','" + issue_centre_code + "','" + per_no + "','" + issue_type + "','" + issue_name + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + "," + do_valid + "," + permit_valid + ",'" + do_date + "','" + permit_date + "',getdate(),'','" + pmode + "','" + dd_no + "','" + dd_date + "'," + tx_tot_qty.Text + "," + tx_dd_amount.Text + "," + tx_tot_amt.Text + ",'" + ddl_bank.SelectedItem.Value + "'," + ddl_commodity.SelectedItem.Value + ",'" + ddl_scheme.SelectedItem.Value + "'," + tx_rate_qt.Text + ",'N','" + ip + "','" + opid + "','" + notrans + "')";
                            cmd.CommandText = str1;
                            cmd.Connection = con;
                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                //For DO_FPS Table
                                int i = 0;
                                while (i < count)
                                {
                                    int trnscnt = 0;
                                    string docount = "";
                                    string strqr = "select max(convert(int,right(trans_id,len(trans_id)-len(allotment_year)-len(district_code)-len(delivery_order_no)-3))) as rwcount  from dbo.do_fps where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
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

                                    string transid = ddd_allot_year.SelectedItem.Text + "-" + dist.ToString() + "-" + do_no.ToString() + "-" + (trnscnt).ToString();
                                    str1 = "INSERT INTO dbo.do_fps(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,block_code,fps_code,commodity,scheme_id,allot_qty,quantity,rate_per_qtls,status,ip_add)VALUES('" + state + "','" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + ",'" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][10] + "'," + dt.Rows[i][8] + "," + dt.Rows[i][7] + ",'N','" + ip + "')";
                                    cmd.CommandText = str1;

                                    cmd.ExecuteNonQuery();

                                    allot_qty = allot_qty + CheckNull(dt.Rows[i][10].ToString());
                                    issue_qty = issue_qty + CheckNull(dt.Rows[i][8].ToString());

                                    string comm = dt.Rows[i][4].ToString();
                                    string scheme = dt.Rows[i][6].ToString();
                                    string commodity = ddl_commodity.SelectedValue;

                                    //if (scheme.ToLower() == "apl")
                                    //{
                                    //    if (comm.ToLower().Contains("wheat"))
                                    //    {
                                    //        commodity = "wheat_apl_alloc";
                                    //    }
                                    //    if (comm.ToLower().Contains("rice"))
                                    //    {
                                    //        commodity = "rice_apl_alloc";
                                    //    }
                                    //}
                                    //if (scheme.ToLower() == "bpl")
                                    //{
                                    //    if (comm.ToLower().Contains("wheat"))
                                    //    {
                                    //        commodity = "wheat_bpl_alloc";
                                    //    }
                                    //    if (comm.ToLower().Contains("rice"))
                                    //    {
                                    //        commodity = "rice_bpl_alloc";
                                    //    }
                                    //}
                                    //if (scheme.ToLower() == "aay")
                                    //{
                                    //    if (comm.ToLower().Contains("wheat"))
                                    //    {
                                    //        commodity = "wheat_aay_alloc";
                                    //    }
                                    //    if (comm.ToLower().Contains("rice"))
                                    //    {
                                    //        commodity = "rice_aay_alloc";
                                    //    }
                                    //}

                                    //if (comm.ToLower().Contains("sugar"))
                                    //{
                                    //    commodity = "sugar_alloc";
                                    //}

                                    string tempr = "NNN";
                                    
                                    string strr = "";
                                    strr = "select * from pds.fps_allot_mpsc where district_code='" + distid + "' and block_code='" + dt.Rows[i][0] + "' and  depot_code='" + sid + "' and  fps_code='" + dt.Rows[i][1] + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
                                    cmd.Connection = con;
                                    cmd.CommandText = strr;
                                    dr = cmd.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        tempr = "YYY";
                                    }

                                    dr.Close();
                                    if (tempr == "YYY")
                                    {
                                        strr = "update pds.fps_allot_mpsc set commodity = '" + commodity + "',updated_on=getdate()  where district_code='" + distid + "' and block_code='" + dt.Rows[i][0] + "' and  depot_code='" + sid + "' and  fps_code='" + dt.Rows[i][1] + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
                                    }
                                    else
                                    {
                                        strr = "INSERT INTO pds.fps_allot_mpsc(State_Id,district_code,block_code,depot_code,fps_code,month,Year,commodity,initial_creation,IP,OperatorID) values ('" + state + "','" + distid + "','" + dt.Rows[i][0] + "','" + sid + "','" + dt.Rows[i][1] + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + "," + commodity + ",getdate(),'" + ip + "','" + opid + "')";
                                    }
                                    cmd.CommandText = strr;
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    i = i + 1;
                                }

                                if (temp1 == "NNN")
                                {
                                    str1 = "insert into dbo.sum_trans_do (State_Id,district_code,issueCentre_code,allot_qty,issue_qty,lift_qty,trans_month,trans_year,created_date) values('" + state + "','" + dist + "','" + issue_centre_code + "'," + allot_qty + "," + issue_qty + "," + lift_qty + "," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + ",'" + do_date + "')";
                                    cmd.CommandText = str1;
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    str1 = "update dbo.sum_trans_do set allot_qty=" + allot_qty + ",issue_qty=" + issue_qty + "  where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + month.ToString() + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                                    cmd.CommandText = str1;
                                    cmd.ExecuteNonQuery();
                                }
                                str1 = "update dbo.Dccb_Permit set status='Y' where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and Permit_order_no='" + per_no + "'";
                                cmd.CommandText = str1;
                                cmd.ExecuteNonQuery();

                                LblShowDonum.Text = do_no;

                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved, DO Number is -- " + do_no + "');</script>");

                                LblShowDonum.Visible = true;
                                LbldisplayDo.Visible = true;

                                String s = Request.QueryString["field1"];
                                //Response.Write("<script language='javascript'>window.open('frmViewAddedFPS.aspx?distName=" + Session["dist_name"].ToString() + "', null, 'left = 300,top=90, height=800, width= 650,modal=yes ,status=no, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no'); </script>");

                                StringBuilder sbScript = new StringBuilder();

                                sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
                                sbScript.Append("<!--\n");
                                sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
                                sbScript.Append("// -->\n");
                                sbScript.Append("</script>\n");

                                this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
                                Label1.Text = "Data Saved Successfully ...";
                                //panelDO.Enabled = false;
                                save.Enabled = false;
                                hlinkpdo.Visible = true;
                                hlinkpdo.Enabled = true;
                                Session["doforprint"] = do_no;

                                txtcomdty_bal.Text = "";
                                lblFPSNAme.Text = "";

                                Session["issubmited"] = "Yes";
                            }
                            catch (Exception ex)
                            {
                                Label1.Text = ex.Message;
                                Label1.ForeColor = System.Drawing.Color.Red;
                            }
                            finally
                            {
                                con.Close();
                            }

                            dt.Rows.Clear();
                            dt.AcceptChanges();
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Record already exist');</script>");
                            Label1.ForeColor = System.Drawing.Color.Red;
                            Label1.Text = "Record already exist ... Change DO No.";
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter FPS details');</script>");
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Please enter FPS details ...";
                    }
               
            }
            catch (Exception)
            { }
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
            lstitem1.Text = "Select";
            lstitem1.Value = "N";
            ddl_commodity.Items.Insert(0, lstitem1);
            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }
    }

    protected void get_lead()
    {
        try
        {
            string dist = distid;
            ddl_lead.Items.Clear();
            cmd.CommandText = "select LeadSoc_nameU,LeadSoc_Code from dbo.m_LeadSoc where District_code='" + distid + "' order by LeadSoc_nameU";
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
            lstitem1.Text = "Select";
            lstitem1.Value = "N";
            ddl_lead.Items.Insert(0, lstitem1);
            dr.Close();
            con_opdms.Close();
        }
        catch (Exception)
        { }
    }

    protected void get_fps()
    {
        try
        {
            string dist = distid;
            ddl_fps_name.Items.Clear();
            lblFPSNAme.Text = "";
            cmd.CommandText = "SELECT pds.fps_master.block_code,pds.fps_master.fps_Uname, pds.fps_master.fps_code FROM dbo.Lead_soc_fps Left JOIN pds.fps_master ON Lead_soc_fps.District_code = pds.fps_master.district_code AND Lead_soc_fps.fps_code = pds.fps_master.fps_code where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "' order by pds.fps_master.fps_Uname";
            cmd.Connection = con_opdms;
            con_opdms.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["fps_Uname"].ToString() + "  (" + dr["fps_code"].ToString() + ")";
                lstitem.Value = dr["fps_code"].ToString();
                //ddl_block.SelectedValue = dr["block_code"].ToString();
                ddl_fps_name.Items.Add(lstitem);
            }
            ddl_fps_name.Items.Insert(0, "Select");
            dr.Close();
            //
            string blk = "";
            cmd.CommandText = "SELECT block_code FROM dbo.m_LeadSoc where District_code='" + distid + "' and LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "'";
            cmd.Connection = con_opdms;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                blk = dr["block_code"].ToString();
            }
            dr.Close();
            con_opdms.Close();
            ddl_block.SelectedIndex = 0;
            if (blk != "")
            {
                ddl_block.SelectedValue = blk;
            }
        }
        catch (Exception)
        { }
    }

    protected void get_block()
    {
        try
        {
            string dist = distid;
            ddl_block.Items.Clear();
            cmd.CommandText = "select * from  pds.block_master where District_code=" + dist + " order by Block_name";
            cmd.Connection = con_opdms;
            con_opdms.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["Block_Uname"].ToString();
                lstitem.Value = dr["block_code"].ToString();
                ddl_block.Items.Add(lstitem);
            }
            ddl_block.Items.Insert(0, "Select");
            dr.Close();
            con_opdms.Close();
        }
        catch (Exception)
        { }
    }

    protected void get_scheme()
    {
        try
        {
            cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME   where status='Y' order by displayorder";
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
            ddl_scheme.Items.Insert(0, "Select");

            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }
    }

    protected void ddl_issueto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = "";
            Label1.ForeColor = System.Drawing.Color.Blue;

            if (ddl_issueto.SelectedItem.Value == "LF")
            {
                ddl_lead.Visible = true;
                lbl_lead.Visible = true;
                lbl_alloc.Visible = true;
                lbl_issue.Visible = true;
                lbl_curbal.Visible = true;
                lbl_stock.Visible = true;
                lbl_qtalloc.Visible = true;
                lbl_qtcur.Visible = true;
                lbl_qtissue.Visible = true;
                lbl_qtstk.Visible = true;
                tx_allot_qty.Visible = true;
                tx_already_iqty.Visible = true;
               // tx_bal_ic.Visible = true;
                //tx_balQty.Visible = true;
            }
            else
            {
                ddl_lead.Visible = false;
                lbl_lead.Visible = false;
                lbl_alloc.Visible = false;
                lbl_issue.Visible = false;
                lbl_curbal.Visible = false;
                lbl_stock.Visible = false;
                lbl_qtalloc.Visible = false;
                lbl_qtcur.Visible = false;
                lbl_qtissue.Visible = false;
                lbl_qtstk.Visible = false;
                tx_allot_qty.Visible = false;
                tx_already_iqty.Visible = false;
                tx_bal_ic.Visible = false;
                tx_balQty.Visible = false;
            }
        }
        catch (Exception)
        { }
    }

    protected void ddl_pmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        //tx_tot_amt.Text = (CheckNull(tx_rate_qt.Text) * CheckNull(tx_qty.Text)).ToString();

        try
        {
            Label1.Text = "";
            ddl_bank.Enabled = true;
            Label1.ForeColor = System.Drawing.Color.Blue;

            if (ddl_pmode.SelectedItem.Value == "D")
            {
                tx_dd_no.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lbl_ddno.Visible = true;
                lbl_amt.Visible = true;
            }
            else
            {
                tx_dd_no.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator4.Enabled = true;
                lbl_ddno.Visible = false;
                lbl_amt.Visible = true;
                if (ddl_pmode.SelectedItem.Value == "R")
                {
                    RequiredFieldValidator4.Enabled = false;
                    lbl_amt.Visible = false;
                    tx_dd_amount.Text = "0";
                    ddl_bank.Enabled = false;
                }

                if (ddl_pmode.SelectedItem.Value == "AD")
                {
                    RequiredFieldValidator4.Enabled = false;
                    lbl_amt.Visible = false;
                    tx_dd_amount.Text = "0";
                    ddl_bank.Enabled = false;
                    //tx_dd_date.Text = "1/1/1";
                    tx_dd_date.ReadOnly = true;
                    lblddchekno.Visible = false;
                    tx_dd_no.Visible = false;
                    lblddchekdate.Visible = false;
                    tx_do_date.Visible = false;
                    tx_dd_amount.Visible = false;
                    lblamount.Visible = false;

                }

                if (ddl_pmode.SelectedItem.Value == "OP")
                {
                    RequiredFieldValidator4.Enabled = false;
                    lbl_amt.Visible = false;
                    tx_dd_amount.Text = "0";
                    ddl_bank.Enabled = false;
                    //tx_dd_date.Text = "1/1/1";
                    tx_dd_date.ReadOnly = true;
                    lblddchekno.Visible = true;
                    lblddchekno.Text = "Neft/RTGS Number";
                    tx_dd_no.Visible = true;
                    tx_dd_no.Enabled = true;
                    tx_dd_no.Text = "0";
                    lblddchekdate.Visible = false;
                    tx_do_date.Visible = false;
                    tx_dd_amount.Visible = false;
                    lblamount.Visible = true;
                }

            }
        }
        catch (Exception)
        { 
        
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (tx_rate_qt.Text == "" )
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Enter Rate/Quintal');</script>");
                tx_rate_qt.Focus();
                return;
            }

            Label1.Text = "";
            Label1.ForeColor = System.Drawing.Color.Blue;

            if (ddl_commodity.SelectedItem.Text == "Select" || ddl_scheme.SelectedItem.Text == "Select" || ddl_fps_name.SelectedItem.Text == "Select")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme');</script>");
                Label1.Text = "Please select Commodity/Scheme/FPS ...";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                Panel1.Visible = true;
                Panel1.Height = 70;

                if (tx_rate_qt.Text == "")
                {
                    tx_rate_qt.Text = "0";
                }
                if (tx_tot_amt.Text == "")
                {
                    tx_tot_amt.Text = "0";
                }
                if (tx_qty.Text == "")
                {
                    tx_qty.Text = "0";
                }
                if (tx_tot_qty.Text == "")
                {
                    tx_tot_qty.Text = "0";
                }
                if (tx_allot_qty2.Text == "")
                {
                    tx_allot_qty2.Text = "0";
                }
                if (tx_balQty2.Text == "")
                {
                    tx_balQty2.Text = "0";
                }
                if (tx_bal_ic.Text == "")
                {
                    tx_bal_ic.Text = "0";
                }
                GridView1.Columns[1].Visible = false;
                string temp = "NNN";
                dt = (DataTable)Session["dt"];
                int row = 0;

                if (dt.Rows.Count > 0)
                {
                    while (row < dt.Rows.Count)
                    {
                        if (dt.Rows[row][1].ToString() == ddl_fps_name.SelectedItem.Value && dt.Rows[row][3].ToString() == ddl_commodity.SelectedItem.Value && dt.Rows[row][5].ToString() == ddl_scheme.SelectedItem.Value)
                        {
                            temp = "YYY";
                            break;
                        }
                        row = row + 1;
                    }
                }
                if (temp == "YYY")
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to be selected FPS/Commodity/Scheme Already Issued ...');</script>");
                    Label1.Text = "Quantity to be selected FPS/Commodity/Scheme Already Issued";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }

                else
                {
                    decimal bal_qty = CheckNull(tx_balQty2.Text);
                    //if (CheckNull(tx_tot_qty.Text) + CheckNull(tx_qty.Text) > CheckNull(txtcomdty_bal.Text))
                    //{
                    //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Balance Stock of Commodity...');</script>");
                    //    tx_qty.Text = "";
                    //    tx_qty.Focus();
                    //}
                    //else if (CheckNull(tx_qty.Text) > bal_qty || CheckNull(tx_qty.Text) <= 0)
                    //{
                    //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Alloted...');</script>");
                    //    tx_qty.Text = "";
                    //    tx_qty.Focus();
                    //}
                   
                        ////tx_tot_qty.Text =System.Math.Round((CheckNull(tx_tot_qty.Text) + CheckNull(tx_qty.Text)),5).ToString();
                        ////tx_tot_amt.Text = System.Math.Round((CheckNull(tx_tot_amt.Text) + (CheckNull(tx_qty.Text) * CheckNull(tx_rate_qt.Text))),2).ToString();
                        dt = (DataTable)Session["dt"];
                        dt.Rows.Add(ddl_block.SelectedItem.Value, ddl_fps_name.SelectedItem.Value, ddl_fps_name.SelectedItem.Text, ddl_commodity.SelectedItem.Value, ddl_commodity.SelectedItem.Text, ddl_scheme.SelectedItem.Value, ddl_scheme.SelectedItem.Text, tx_rate_qt.Text, tx_qty.Text, CheckNull(tx_qty.Text) * CheckNull(tx_rate_qt.Text), tx_allot_qty2.Text);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        Session["dt"] = dt;
                        tx_qty.Text = "";
                        int rcount = 0;
                        decimal tqty = 0;
                        decimal tamt = 0;
                        while (rcount < dt.Rows.Count)
                        {
                            tqty = tqty + CheckNull(dt.Rows[rcount][8].ToString());
                            tamt = tamt + CheckNull(dt.Rows[rcount][9].ToString());
                            rcount = rcount + 1;
                        }

                        tx_tot_qty.Text = System.Math.Round(tqty, 5).ToString();
                        tx_tot_amt.Text = System.Math.Round(tamt, 2).ToString();
                   
                }
            }
            //tx_allot_qty2.Text = "3";
            //tx_already_iqty2.Text = "2";
            if (GridView1.Rows.Count > 0)
            {
                BTNReview.Enabled = true;
            }
            else
            {
                BTNReview.Enabled = false;
            }
        }
        catch (Exception)
        {

        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int idx = GridView1.SelectedIndex;
            dt = (DataTable)Session["dt"];
            dt.Rows[idx].Delete();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
            int rcount = 0;
            decimal tqty = 0;
            decimal tamt = 0;
            while (rcount < dt.Rows.Count)
            {
                tqty = tqty + CheckNull(dt.Rows[rcount][8].ToString());
                tamt = tamt + CheckNull(dt.Rows[rcount][9].ToString());
                rcount = rcount + 1;
            }
            tx_tot_qty.Text = System.Math.Round(tqty, 5).ToString();
            tx_tot_amt.Text = System.Math.Round(tamt, 2).ToString();

            if (GridView1.Rows.Count > 0)
            {
                Panel1.Visible = true;
                BTNReview.Enabled = true;
            }
            else
            {
                Panel1.Visible = false;
                BTNReview.Enabled = false;
            }
            lblFPSNAme.Text = "";
        }

        catch (Exception)
        {
        }
    }

    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void GetRate()
    {
        try
        {
            string todaydate = DateTime.Today.ToString("MM/dd/yyyy");
            DObj = new Districts(ComObj);
            if (ddl_commodity.SelectedItem.Text == "Select" || ddl_scheme.SelectedItem.Text == "Select")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
            }
            else if (ddl_rate_type.SelectedItem.Value == "U")
            {
                string qry = "Select Uraban_rate  from dbo.SCSC_IssueRate where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "' and District_code='" + distid + "' and Effective_From<='" + todaydate + "' order by Effective_From desc";
                DObj = new Districts(ComObj);
                DataSet ds = DObj.selectAny(qry);

                if (ds.Tables[0].Rows.Count == 0)
                {
                   // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Rate is not available for selected commodity....'); </script> ");
                    tx_rate_qt.ReadOnly = false;
                    tx_rate_qt.Focus();
                    tx_rate_qt.BackColor = System.Drawing.Color.White;
                    tx_rate_qt.Text = "0";
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tx_rate_qt.Text = dr["Uraban_rate"].ToString();
                    tx_rate_qt.ReadOnly = true;
                }
            }
            else
            {
                string qry = "Select Rural_rate  from dbo.SCSC_IssueRate  where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "' and District_code='" + distid + "' and Effective_From<='" + todaydate + "' order by Effective_From desc";
                DObj = new Districts(ComObj);
                DataSet ds = DObj.selectAny(qry);
                if (ds.Tables[0].Rows.Count == 0)
                {
                   // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Rate is not available for selected commodity....'); </script> ");
                    tx_rate_qt.ReadOnly = false;
                    tx_rate_qt.Focus();
                    tx_rate_qt.BackColor = System.Drawing.Color.White;
                    tx_rate_qt.Text = "0";
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tx_rate_qt.Text = dr["Rural_rate"].ToString();
                    tx_rate_qt.ReadOnly = true;
                }
            }
        }
        catch (Exception)
        { }
    }

    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tx_allot_qty.Text = "";
            tx_balQty2.Text = "";
            if (ddl_issueto.SelectedItem.Value == "F" && ddl_commodity.SelectedItem.Text != "Select" && ddl_scheme.SelectedItem.Text != "Select")
            {
                GetRate();
            }
            else if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select" || ddl_lead.SelectedItem.Text == "pqÇUk;¢")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Lead Society/Commodity/Scheme...');</script>");
            }
            else
            {
                GetRate();
                get_IssuedQty();
                get_balAtIC();
                get_allotQty();
                tx_qty.Focus();
            }
        }
        catch (Exception)
        { }
    }

    protected decimal CheckNull(string Val)
    {
        decimal rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
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
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = int.Parse(Val);
        }
        return rval;
    }

    protected void ddl_lead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            get_fps();
            ddl_commodity.SelectedIndex = 0;
            ddl_scheme.SelectedIndex = 0;
        }
        catch (Exception)
        { }
    }

    protected void get_blkFPS()
    {
        try
        {

            string dist = distid;
            ddl_fps_name.Items.Clear();
            lblFPSNAme.Text = "";
            string blk = ddl_block.SelectedItem.Value;
            cmd.CommandText = "SELECT [fps_name],[fps_code],[block_code],[del_status],[district_code] ,[rc_ant],[rc_bpl],[rc_apl],[rc_others] ,(fps_Uname +','+ fps_code +'') as fps_Uname FROM pds.fps_master where district_code='" + dist + "' and block_code='" + blk + "' and del_status='False' order by fps_code "; ;
            cmd.Connection = con_opdms;
            con_opdms.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["fps_Uname"].ToString();
                lstitem.Value = dr["fps_code"].ToString();
                ddl_fps_name.Items.Add(lstitem);
            }
            ddl_fps_name.Items.Insert(0, "Select");
            dr.Close();
            con_opdms.Close();
        }
        catch (Exception)
        { }
    }

    protected void get_IssuedQty()
    {
        try
        {
            tx_already_iqty.Text = "";
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
            string cmdstr = "";
            if (ddl_commodity.SelectedItem.Text.ToLower().Contains("sugar"))
            {
                cmdstr = "SELECT Round(sum(convert(decimal(18,5),quantity)),5) as quantity FROM dbo.do_fps INNER JOIN  opdms.dbo.Lead_soc_fps ON do_fps.district_code = Lead_soc_fps.District_code  AND do_fps.fps_code = Lead_soc_fps.fps_code and do_fps.district_code='" + distid + "' and do_fps.issueCentre_code='" + sid + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.commodity in(" + comdty + ")  where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "'";
            }
            else
            {
                cmdstr = "SELECT Round(sum(convert(decimal(18,5),quantity)),5) as quantity FROM dbo.do_fps INNER JOIN  opdms.dbo.Lead_soc_fps ON do_fps.district_code = Lead_soc_fps.District_code  AND do_fps.fps_code = Lead_soc_fps.fps_code and do_fps.district_code='" + distid + "' and do_fps.issueCentre_code='" + sid + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.commodity in(" + comdty + ") and do_fps.scheme_id=" + ddl_scheme.SelectedItem.Value + " where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "'";
            }
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tx_already_iqty.Text = dr["quantity"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }
    }

    protected void get_balAtIC()
    {
        try
        {
            tx_bal_ic.Text = "";
            string cmdstr = "";
            if (ddl_commodity.SelectedItem.Text.ToLower().Contains("sugar"))
            {
                cmdstr = "SELECT round(sum(Current_Balance),5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'";
            }
            else
            {
                cmdstr = "SELECT round(sum(Current_Balance),5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "'";
            }
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tx_bal_ic.Text = dr["bal_ic"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }
    }

    protected void get_ComdtyBal()
    {
        try
        {
            txtcomdty_bal.Text = "";
            string cmdstr = "SELECT round(Sum(Current_Balance),5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'";
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtcomdty_bal.Text = dr["bal_ic"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }
    }

    protected void ddl_fps_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tx_tot_qty.Text = "";

            txt_fps.Enabled = false;
            //ddl_scheme.SelectedIndex = 0;
            tx_qty.Text = "";
            //tx_rate_qt.Text = ""; 
            tx_allot_qty2.Text = "";
            tx_already_iqty2.Text = "";
            tx_balQty2.Text = "";
            //tx_allot_qty.Text = "";
            if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select" || ddl_fps_name.SelectedItem.Text == "Select")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS/Commodity/Scheme...');</script>");
            }
            else
            {
                //ddl_fps_name.SelectedItem.Attributes.Add("style","background-color:Green");
                Button1.Enabled = true;
                if (ddl_fps_name.SelectedIndex == 0)
                {
                    lblFPSNAme.Text = "";
                }
                else
                {
                    lblFPSNAme.Text = Convert.ToString(ddl_fps_name.SelectedItem.Text);
                }
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
                string cmdstr = "";
                if (ddl_commodity.SelectedItem.Text.ToLower().Contains("sugar"))
                {
                    cmdstr = "SELECT Round(sum(convert(decimal(18,5),quantity)),5) as quantity FROM dbo.do_fps where district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and fps_code='" + ddl_fps_name.SelectedItem.Value + "' and commodity in(" + comdty + ")";
                }
                else
                {
                    cmdstr = "SELECT Round(sum(convert(decimal(18,5),quantity)),5) as quantity FROM dbo.do_fps where district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and fps_code='" + ddl_fps_name.SelectedItem.Value + "' and commodity in(" + comdty + ") and scheme_id=" + ddl_scheme.SelectedItem.Value + "";
                }
                cmd.CommandText = cmdstr;
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tx_already_iqty2.Text = dr["quantity"].ToString();
                }

                dr.Close();
                con.Close();
                string comm = ddl_commodity.SelectedItem.Text;
                string scheme = ddl_scheme.SelectedItem.Text;
                int amonth = int.Parse(ddl_allot_month.SelectedItem.Value);
                int ayear = int.Parse(ddd_allot_year.SelectedItem.Text);
                string fps_code = ddl_fps_name.SelectedItem.Value;
                //get_IssuedQty();
                // get_balAtIC();
                string commodity = "";
                if (scheme.ToLower() == "apl")                {
                    if (comm.ToLower().Contains("wheat"))
                    {
                        commodity = "wheat_apl_alloc";
                    }
                    if (comm.ToLower().Contains("rice"))
                    {
                        commodity = "rice_apl_alloc";
                    }
                    //if (comm.ToLower().Contains("sugar"))
                    //{
                    //    commodity = "sugar_alloc";
                    //}

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
                    //if (comm.ToLower().Contains("sugar"))
                    //{
                    //    commodity = "sugar_alloc";
                    //}

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
                    //if (comm.ToLower().Contains("sugar"))
                    //{
                    //    commodity = "sugar_alloc";
                    //}

                }

                if (scheme.ToLower() == "priority households")             /// Added on 10-06-2014 for phh
                {
                    if (comm.ToLower().Contains("wheat"))
                    {
                        commodity = "wheat_phh_alloc";
                    }
                    if (comm.ToLower().Contains("rice"))
                    {
                        commodity = "rice_phh_alloc";
                    }

                }

                if (comm.ToLower().Contains("sugar"))
                {
                    commodity = "sugar_alloc";
                }

                if (commodity != "")
                {
                    cmd.CommandText = "select  " + commodity + "   from pds.fps_allot where district_code='" + distid + "' and block_code='" + ddl_block.SelectedItem.Value + "' and fps_code='" + fps_code + "' and month=" + amonth + " and Year=" + ayear + "";
                    cmd.Connection = con_opdms;
                    con_opdms.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tx_allot_qty2.Text = System.Math.Round(CheckNull(dr[commodity].ToString()), 5).ToString();
                    }
                    dr.Close();
                    con_opdms.Close();
                }
                if (tx_allot_qty2.Text == "")
                {
                    tx_allot_qty2.Text = "0";
                    // Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotmennt for Selected Commodity...');</script>");
                    //Button1.Enabled = true;
                }
                if (tx_already_iqty2.Text == "")
                {
                    tx_already_iqty2.Text = "0";
                }
                tx_balQty2.Text = "";
                tx_balQty2.Text = System.Math.Round(CheckNull(tx_allot_qty2.Text) - CheckNull(tx_already_iqty2.Text), 5).ToString();
                tx_qty.Focus();

                //tx_allot_qty2.Text = "3";
                //tx_already_iqty2.Text = "2";

            }

        }
        catch (Exception)
        { }
    }


    protected void get_bankname()
    {
        try
        {
            ddl_bank.Items.Clear();
            cmd.CommandText = "select Bank_ID,Bank_Name from dbo.Bank_Master where District_Code='" + distid + "' and issueCenter_code='" + sid + "' order by Bank_Name";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["Bank_Name"].ToString();
                lstitem.Value = dr["Bank_ID"].ToString();
                ddl_bank.Items.Add(lstitem);
            }
            ListItem lstitem1 = new ListItem();
            lstitem1.Text = "Select";
            lstitem1.Value = "N";
            ddl_bank.Items.Insert(0, lstitem1);
            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }
    }

    void GetPermit()
    {
        try
        {
            string society = "";
            if (Session["Society"].ToString() != "")
            {
                society = Session["Society"].ToString();
            }
            mobj = new MoveChallan(ComObj);

            string qry = "SELECT Permit_order_no  FROM dbo.Dccb_Permit where district_code='" + distid + "'and issueCentre_code='" + sid + "' and status='N' and issue_type='" + society + "'";
            DataSet ds = mobj.selectAny(qry);
            if (ds.Tables[0].Rows.Count == 0)
            {
                ddlpermit.Items.Clear();
                ListItem lst = new ListItem();
                lst.Text = "Not Indicated";
                lst.Value = "0";
                //ddlpermit.Items.Insert(0, "--Select--");
                ddlpermit.Items.Insert(0, lst);


            }
            else
            {
                ddlpermit.Items.Clear();
                ddlpermit.DataSource = ds.Tables[0];
                ddlpermit.DataTextField = "Permit_order_no";
                ddlpermit.DataValueField = "Permit_order_no";
                ddlpermit.DataBind();
                //ddlpermit.Items.Insert(0, "--Select--");
                ddlpermit.Items.Insert(0, "Not Indicated");
            }
        }
        catch (Exception)
        { }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void tx_permit_no_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlpermit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tx_permit_no.ReadOnly = false;
            tx_permit_date.Enabled = true;
            ddl_issueto.Enabled = true;
            ddl_lead.Enabled = true;
            ddd_allot_year.Enabled = true;
            ddl_allot_month.Enabled = true;
            ddl_commodity.Enabled = true;
            ddl_scheme.Enabled = true;
            Button1.Enabled = true;
            save.Enabled = true;
            ddl_block.Enabled = true;
            ddl_fps_name.Enabled = true;
            tx_qty.ReadOnly = false;
            ddl_pmode.Enabled = true;
            tx_dd_no.ReadOnly = false;
            tx_dd_date.Enabled = true;
            ddl_bank.Enabled = true;
            tx_dd_amount.ReadOnly = false;
            //GridView1.Dispose();
            DataTable xyz = new DataTable();
            GridView1.DataSource = xyz;
            GridView1.DataBind();
            tx_permit_no.Text = "";
            tx_qty.Text = "";
            tx_dd_no.Text = "";
            tx_dd_amount.Text = "";
            tx_allot_qty.Text = "";
            txtcomdty_bal.Text = "";
            tx_balQty.Text = "";
            tx_already_iqty.Text = "";
            tx_bal_ic.Text = "";
            string dist = distid;
            string issue_centre_code = sid;
            GridView1.Columns[1].Visible = true;
            tx_permit_date1.Text = "";
            tx_dd_date1.Text = "";
            tx_permit_date1.Visible = true;
            tx_dd_date1.Visible = true;
            tx_permit_date.Visible = false;
            tx_dd_date.Visible = false;
            lbl_blk.Visible = false;
            ddl_block.Visible = false;
            lbl_fps.Visible = false;
            ddl_fps_name.Visible = false;
            lbl_allotqty.Visible = false;
            tx_allot_qty2.Visible = false;
            lbl_balqty.Visible = false;
            tx_balQty2.Visible = false;
            lbl_issueqty.Visible = false;
            tx_already_iqty2.Visible = false;
            lblQuantity.Visible = false;
            tx_qty.Visible = false;
            lbl_qtls1.Visible = false;
            lbl_qtls.Visible = false;
            Label3.Visible = false;
            Button1.Visible = false;

            if (ddlpermit.SelectedItem.Text == "--Select--")
            {
                save.Enabled = false;
                Button1.Enabled = false;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Permit No. or Not Indicated ...');</script>");
            }
            else if (ddlpermit.SelectedItem.Text == "Not Indicated")
            {
                ddl_commodity.SelectedIndex = 0;
                ddl_lead.SelectedIndex = 0;
                ddl_scheme.SelectedIndex = 0;
                tx_permit_date1.Visible = false;
                tx_dd_date1.Visible = false;
                tx_permit_date.Visible = true;
                tx_dd_date.Visible = true;
                lbl_blk.Visible = true;
                ddl_block.Visible = true;
                lbl_fps.Visible = true;
                ddl_fps_name.Visible = true;
                lbl_allotqty.Visible = true;
                tx_allot_qty2.Visible = true;
                lbl_balqty.Visible = true;
                //tx_balQty2.Visible = true;
                lbl_issueqty.Visible = true;
                tx_already_iqty2.Visible = true;
                lblQuantity.Visible = true;
                tx_qty.Visible = true;
                lbl_qtls1.Visible = true;
                lbl_qtls.Visible = true;
                Label3.Visible = true;
                Button1.Visible = true;
                GridView1.Columns[0].Visible = true;
            }
            else
            {

                mobj = new MoveChallan(ComObj);
                string permit = ddlpermit.SelectedValue;
                string qryper = "SELECT  Dccb_Permit.*,m_LeadSoc.LeadSoc_nameU  FROM dbo.Dccb_Permit left join opdms.dbo.m_LeadSoc on Dccb_Permit.issue_name=m_LeadSoc.LeadSoc_Code  where Dccb_Permit.district_code='" + distid + "'and Dccb_Permit.issueCentre_code='" + sid + "' and Dccb_Permit.Permit_order_no='" + permit + "'";
                string permitdate = "";
                string dddate = "";
                DataSet ds = mobj.selectAny(qryper);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Permit details not found ...');</script>");
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tx_permit_no.Text = dr["Permit_order_no"].ToString();
                    permitdate = dr["permit_date"].ToString();
                    ddl_issueto.SelectedValue = dr["issue_type"].ToString();
                    if (ddl_issueto.SelectedValue == "LF")
                    {
                        ddl_lead.SelectedValue = dr["issue_name"].ToString();
                    }
                    ddd_allot_year.SelectedValue = dr["allotment_year"].ToString();
                    ddl_allot_month.SelectedValue = dr["allotment_month"].ToString();
                    ddl_commodity.SelectedValue = dr["commodity_id"].ToString();
                    ddl_scheme.SelectedValue = dr["scheme_id"].ToString();
                    Button1.Enabled = false;
                    tx_tot_qty.Text = dr["quantity"].ToString();
                    tx_rate_qt.Text = dr["rate_per_qtls"].ToString();
                    tx_tot_amt.Text = dr["tot_amount"].ToString();
                    ddl_pmode.SelectedValue = dr["payment_mode"].ToString();
                    tx_dd_no.Text = dr["dd_no"].ToString();
                    dddate = dr["dd_date"].ToString();
                    ddl_bank.SelectedValue = dr["bank_id"].ToString();
                    tx_dd_amount.Text = dr["amount"].ToString();
                    dt = (DataTable)Session["dt"];
                    dt.Clear();
                    SqlDataAdapter da = new SqlDataAdapter("select Dccb_Permit_FPS_Detail.block_code,Dccb_Permit_FPS_Detail.fps_code,opdms.pds.fps_master.fps_name,Dccb_Permit_FPS_Detail.commodity as commodity_id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as commodity_name,Dccb_Permit_FPS_Detail.scheme_id,tbl_MetaData_SCHEME .Scheme_Name as scheme_name,Dccb_Permit_FPS_Detail.rate_per_qtls as rate_qtls,Dccb_Permit_FPS_Detail.quantity as qty,Dccb_Permit_FPS_Detail.quantity*rate_per_qtls as amt,Dccb_Permit_FPS_Detail.allot_qty from dbo.Dccb_Permit_FPS_Detail LEFT JOIN opdms.pds.fps_master ON Dccb_Permit_FPS_Detail.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON Dccb_Permit_FPS_Detail.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON Dccb_Permit_FPS_Detail.scheme_id = tbl_MetaData_SCHEME .Scheme_Id where Dccb_Permit_FPS_Detail.permit_order_no='" + tx_permit_no.Text + "' and Dccb_Permit_FPS_Detail.district_code='" + dist + "' and Dccb_Permit_FPS_Detail.issueCentre_code='" + issue_centre_code + "' and Dccb_Permit_FPS_Detail.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and Dccb_Permit_FPS_Detail.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and Dccb_Permit_FPS_Detail.status='N'", con);
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //Session["dt"] = dt;
                    GridView1.Columns[0].Visible = false;
                    get_ComdtyBal();
                    get_IssuedQty();
                    get_balAtIC();
                    get_allotQty();
                    tx_permit_date1.Text = getdate(permitdate);
                    tx_dd_date1.Text = getdate(dddate);
                    tx_permit_no.ReadOnly = true;
                    //tx_permit_date.Enabled = false;
                    ddl_issueto.Enabled = false;
                    ddl_lead.Enabled = false;
                    ddd_allot_year.Enabled = false;
                    ddl_allot_month.Enabled = false;
                    ddl_commodity.Enabled = false;
                    ddl_scheme.Enabled = false;
                    Button1.Enabled = false;
                    ddl_block.Enabled = false;
                    ddl_fps_name.Enabled = false;
                    tx_qty.ReadOnly = true;
                    ddl_pmode.Enabled = false;
                    tx_dd_no.ReadOnly = true;
                    //tx_dd_date.Enabled = false;
                    ddl_bank.Enabled = false;
                    tx_dd_amount.ReadOnly = true;
                }
            }
        }
        catch (Exception)
        { }
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }

    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        tx_balQty2.Text = "";
        get_ComdtyBal();
    }

    protected void get_allotQty()
    {
        try
        {
            string comm = ddl_commodity.SelectedItem.Text;
            string scheme = ddl_scheme.SelectedItem.Text;
            int amonth = int.Parse(ddl_allot_month.SelectedItem.Value);
            int ayear = int.Parse(ddd_allot_year.SelectedItem.Text);
            //string fps_code = ddl_fps_name.SelectedItem.Value;
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
                //if (comm.ToLower().Contains("sugar"))
                //{
                //    commodity = "sugar_alloc";
                //}

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
                //if (comm.ToLower().Contains("sugar"))
                //{
                //    commodity = "sugar_alloc";
                //}

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
                //if (comm.ToLower().Contains("sugar"))
                //{
                //    commodity = "sugar_alloc";
                //}

            }

            if (scheme.ToLower() == "priority households")             /// Added on 10-06-2014 for phh
            {
                if (comm.ToLower().Contains("wheat"))
                {
                    commodity = "wheat_phh_alloc";
                }
                if (comm.ToLower().Contains("rice"))
                {
                    commodity = "rice_phh_alloc";
                }

            }

            if (comm.ToLower().Contains("sugar"))
            {
                commodity = "sugar_alloc";
            }

            if (commodity != "")
            {
                cmd.CommandText = "SELECT sum(" + commodity + ") as commodity FROM   pds.fps_allot INNER JOIN  dbo.Lead_soc_fps ON pds.fps_allot.district_code = Lead_soc_fps.District_code AND pds.fps_allot.fps_code = Lead_soc_fps.fps_code and  pds.fps_allot.month=" + amonth + " and  pds.fps_allot.year=" + ayear + "  where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "'";
                cmd.Connection = con_opdms;
                cmd.CommandTimeout = 2400;

                if (con_opdms.State == ConnectionState.Closed)
                {
                    con_opdms.Open();
                }

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tx_allot_qty.Text = System.Math.Round(CheckNull(dr["commodity"].ToString()), 5).ToString();
                }
                dr.Close();

                if (con_opdms.State == ConnectionState.Open)
                {
                    con_opdms.Close();
                }
            }
            if (tx_allot_qty.Text == "")
            {
                tx_allot_qty.Text = "0";
                // Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotmennt for Selected Commodity...');</script>");
            }
            if (tx_already_iqty.Text == "")
            {
                tx_already_iqty.Text = "0";
            }
            tx_balQty.Text = "";
            tx_balQty.Text = System.Math.Round(CheckNull(tx_allot_qty.Text) - CheckNull(tx_already_iqty.Text), 5).ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
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

    protected void ddl_rate_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRate();
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        LblShowDonum.Visible = false;
        LbldisplayDo.Visible = false;

        Response.Redirect("~/IssueCenter/delivery.aspx");
    }

    protected void ddd_allot_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tx_balQty2.Text = "";
            DataTable dtt = new DataTable();
            GridView1.DataSource = dtt;
            GridView1.DataBind();
        }
        catch (Exception)
        { }
    }

    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tx_balQty2.Text = "";
            DataTable dtt = new DataTable();
            GridView1.DataSource = dtt;
            GridView1.DataBind();
        }
        catch (Exception)
        { }
    }

    public void SendSMS()
    {
        

    }
    protected void BTNReview_Click(object sender, EventArgs e)
    {
        try
        {
            dt = (DataTable)Session["dt"];
            DataTable dtGridValue = new DataTable();
            dtGridValue.Columns.Add("FPSName");
            dtGridValue.Columns.Add("Commodity");
            dtGridValue.Columns.Add("Scheme");
            dtGridValue.Columns.Add("AgainstAllot");
            dtGridValue.Columns.Add("Quantity");
            dtGridValue.Columns.Add("Rate/Qtls");
            dtGridValue.Columns.Add("Amount");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string fpsname = dt.Rows[i]["fps_name"].ToString();
                string commodity = dt.Rows[i]["commodity_name"].ToString();
                string scheme = dt.Rows[i]["scheme_name"].ToString();
                string allotQTY = dt.Rows[i]["allot_qty"].ToString();
                string qty = dt.Rows[i]["qty"].ToString();
                string rateQTL = dt.Rows[i]["rate_qtls"].ToString();
                string amt = dt.Rows[i]["amt"].ToString();
                dtGridValue.Rows.Add(fpsname, commodity, scheme, allotQTY, qty, rateQTL, amt);


            }
            Session["dtGridValue"] = dtGridValue;
            String s = Request.QueryString["field1"];
            Response.Write("<script language='javascript'>window.open('FRMViewAddedFPS_Delievry.aspx?distName=" + lblDist.Text + "', null, 'left = 300,top=90, height=800, width= 650, status=no, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');</script>");

            StringBuilder sbScript = new StringBuilder();

            sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
            sbScript.Append("<!--\n");
            sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
            sbScript.Append("// -->\n");
            sbScript.Append("</script>\n");

            this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
        }
        catch (Exception)
        { }
    }

    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        lblDist.Text = dr1dt["district_name"].ToString();

    }


    protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            DateTime d;
            //args.IsValid =
            if (!DateTime.TryParseExact(args.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Enter Permit Date in dd/MM/yyyy Format'); </script> ");
                tx_permit_date.Text = "";
                return;
            }

        }
        catch (Exception)
        { }
    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            DateTime d;
            //args.IsValid =
            if (!DateTime.TryParseExact(args.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Enter DO Date in dd/MM/yyyy Format'); </script> ");
                tx_do_date.Text = "";
                return;
            }

        }
        catch (Exception)
        { }
    }

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            DateTime d;
            //args.IsValid =
            if (!DateTime.TryParseExact(args.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Enter Validity Date in dd/MM/yyyy Format'); </script> ");
                tx_do_validity.Text = "";
                return;
            }

        }
        catch (Exception)
        { }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            DateTime d;
            //args.IsValid =
            if (!DateTime.TryParseExact(args.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Enter DD Date in dd/MM/yyyy Format'); </script> ");
                tx_dd_date.Text = "";
                return;
            }

        }
        catch (Exception)
        { }
    }
    protected void txt_fps_TextChanged(object sender, EventArgs e)
    {
        string dist = distid;
        try
        {
            if (txt_fps.Text != null)
            {

                ddl_fps_name.Items.FindByValue(dist + ddl_block.SelectedValue.ToString() + txt_fps.Text).Selected = true;
                ddl_fps_name_SelectedIndexChanged(sender, e);
            }
            else
            {

                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('please enter last 3 digits of fpscode'); </script> ");

            }



        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('please enter fpscode for search fpsName in listbox'); </script> ");

        }

        //txt_fps.Enabled = false;
    }
}
