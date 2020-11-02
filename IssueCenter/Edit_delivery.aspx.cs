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
public partial class Edit_delivery : System.Web.UI.Page
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
    DataTable dt_del = new DataTable();
    protected Common ComObj = null, cmn = null;
    public string cat = "";
    MoveChallan mobj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] == null)
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        save.Enabled = true;
        if (Page.IsPostBack == false)
        {
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
            dt_del.Columns.Add("block_code");
            dt_del.Columns.Add("fps_code");
            dt_del.Columns.Add("fps_name");
            dt_del.Columns.Add("commodity_id");
            dt_del.Columns.Add("commodity_name");
            dt_del.Columns.Add("scheme_id");
            dt_del.Columns.Add("scheme_name");
            dt_del.Columns.Add("rate_qtls");
            dt_del.Columns.Add("qty");
            dt_del.Columns.Add("amt");
            dt_del.Columns.Add("allot_qty");
            Session["dt_del"] = dt_del;
            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            get_do_no();
            get_comm();
            get_block();
            get_scheme();
            get_bankname();
            get_lead();
            Session["del_all_fps"] = "N";
            //tx_do_date.Dispose();
            //tx_do_date.Enabled = false;
            //tx_do_date.SelectedDate = DateTime.Today.Date;
            //tx_permit_date.Dispose();
            //tx_permit_date.Enabled = false;
            //tx_permit_date.SelectedDate = DateTime.Today.Date;
            //tx_dd_date.Dispose();
            //tx_dd_date.Enabled = false;
            //tx_dd_date.SelectedDate = DateTime.Today.Date;
            //tx_do_validity.Dispose();
            //tx_do_validity.Enabled = false;
            //tx_do_validity.SelectedDate = DateTime.Today.Date;
            if (Session["dono"] != null)
            {
                get_do_data();
            }
        }
        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        //tx_permit_no.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");              
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

         
        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_amount.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_amount.Attributes.Add("onchange", "return chksqltxt(this)");


        tx_rate_qt.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_rate_qt.Attributes.Add("onchange", "return chksqltxt(this)");

       

        tx_do_validity.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_permit_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_permit_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_permit_no.Attributes.Add("onchange", "return chksqltxt(this)");


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

        tx_permit_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_permit_date.Attributes.Add("onchange", "return chksqltxt(this)");


        tx_do_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_date.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_date.Attributes.Add("onchange", "return chksqltxt(this)");
           




        hlinkpdo.Attributes.Add("onclick", "window.open('print_DeleveryOrder.aspx',null,'left=300, top=90, height=700, width= 500, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        tx_rate_qt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_permit_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_tot_amt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balQty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_already_iqty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        chk = new chksql();
        ArrayList ctrllist = new ArrayList();
      
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
    protected void save_Click(object sender, EventArgs e)
    {
        string state = Session["State_Id"].ToString();
        string opid = Session["OperatorId"].ToString();
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;
        if(CheckNull(txtcomdty_bal.Text )==0)
        {
            Label1.Visible = true;
            Label1.Text="Sorry You dont't have balance ....";
            Label1.ForeColor = System.Drawing.Color.Red;            
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry You dont't have balance ......');</script>");
        }
        else if (CheckNull(tx_tot_qty.Text) <= 0)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter quantity...');</script>");
        }
        else
        {
            delEdit_fps();
            if (Session["del_all_fps"].ToString() == "Y")
            {
                del_fps();
            }
            string issutype = Session["issuetype"].ToString();
            string issueto = ddl_lead.SelectedItem.Value;
            if (issutype == "LF" || issutype == "L")
            {
                issueto = ddl_lead.SelectedItem.Value;
            }
            if (issutype == "F")
            {
                issueto = "";
            }
            if (issutype == "O" || issutype == "FCI")
            {
                issueto = tx_lead.Text;
            }
            dt = (DataTable)Session["dt"];
            string dist = distid;
            string do_no = ddl_do_no.SelectedItem.Text;
            string dodate = getDate_MDY(tx_do_date.Text);
            string dovaldate = getDate_MDY(tx_do_validity.Text);
            //DateTime sss = DateTime.Parse(tx_do_date.Text);




            string do_validity = get_days(DateTime.Parse(getDate_MDY(tx_do_date.Text)), DateTime.Parse(getDate_MDY(tx_do_validity.Text)));
            int do_valid = CheckNullInt(do_validity);
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string str1 = "update dbo.delivery_order_mpscsc  set commodity_id='" + ddl_commodity.SelectedItem.Value + "',scheme_id='" + ddl_scheme.SelectedItem.Value + "',rate_per_qtls='" + tx_rate_qt .Text + "',issue_name='" + issueto + "',tot_amount='" + tx_tot_amt.Text + "',payment_mode='" + ddl_pmode.SelectedItem.Value + "',permit_no='" + tx_permit_no.Text + "',quantity='" + tx_tot_qty.Text + "',do_validity=" + do_valid + ",updated_date=getdate(),dd_no='" + tx_dd_no.Text + "',amount=" + tx_dd_amount.Text + ",do_date='" + getDate_MDY(tx_do_date.Text) + "',permit_date='" + getDate_MDY(tx_permit_date.Text) + "',dd_date='" + getDate_MDY(tx_dd_date.Text) + "',bank_id='" + ddl_bank.SelectedItem.Value + "' where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
            cmd.CommandText = str1;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                decimal allot_qty = 0;
                decimal issue_qty = CheckNull(tx_tot_qty.Text);
                //For FPS
                int count = dt.Rows.Count;
                if (count > 0)
                {
                    if (issutype == "LF" || issutype == "F")
                    {
                        int i = 0;
                        while (i < count)
                        {
                            string transid = "";
                            string temp1 = "YYY";
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
                            transid = ddd_allot_year.SelectedItem.Text + "-" + dist.ToString() + "-" + do_no.ToString() + "-" + (trnscnt).ToString(); 
                            string blkcode = dt.Rows[i][0].ToString();
                            string fpscode = dt.Rows[i][1].ToString();
                            string commcode = dt.Rows[i][3].ToString();
                            string schemecode = dt.Rows[i][5].ToString();
                            str1 = "select * from dbo.do_fps where delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and  allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and block_code='" + blkcode + "' and fps_code='" + fpscode + "' and commodity='" + commcode + "' and scheme_id='" + schemecode + "'";
                            cmd.CommandText = str1;
                            cmd.Connection = con;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                temp1 = "NNN";
                                break;
                            }
                            dr.Close();
                            if (temp1 == "YYY")
                            {
                                str1 = "INSERT INTO dbo.do_fps(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,block_code,fps_code,commodity,scheme_id,allot_qty,quantity,rate_per_qtls,status,ip_add)VALUES('" + state +"','"+  transid + "','" + do_no + "','" + distid + "','" + sid + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][10] + "'," + dt.Rows[i][8] + "," + dt.Rows[i][7] + ",'N','" + ip + "')";
                                cmd.CommandText = str1;
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                                allot_qty = allot_qty + CheckNull(dt.Rows[i][10].ToString());
                                ///////////////////////////
                                string comm = dt.Rows[i][4].ToString();
                                string scheme = dt.Rows[i][6].ToString();
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
                                }
                                if (comm.ToLower().Contains("sugar"))
                                {
                                    commodity = "sugar_alloc";
                                }
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
                                    strr = "update pds.fps_allot_mpsc set " + commodity + "='" + dt.Rows[i][10] + "',updated_on=getdate()  where district_code='" + distid + "' and block_code='" + dt.Rows[i][0] + "' and  depot_code='" + sid + "' and  fps_code='" + dt.Rows[i][1] + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
                                }
                                else
                                {
                                    strr = "INSERT INTO pds.fps_allot_mpsc(State_Id,district_code,block_code, depot_code , fps_code, month, Year," + commodity + ",initial_creation,IP,OperatorID) values ('" + state +"','"+  distid + "','" + dt.Rows[i][0] + "','" + sid + "','" + dt.Rows[i][1] + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + dt.Rows[i][10] + "',getdate(),'"+ ip +"','"+opid+"')";
                                }
                                cmd.CommandText = strr;
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                            }
                            i = i + 1;
                        }
                    }
                }
                str1 = "update dbo.sum_trans_do set allot_qty=round(convert(decimal(18,5),allot_qty)+" + allot_qty + ",5),issue_qty=round(convert(decimal(18,5),issue_qty)+" + issue_qty + ",5)  where district_code='" + distid + "' and issueCentre_code='" + sid + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                cmd.CommandText = str1;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Updated Successfully ...');</script>");
                Label1.Text = "Data Updated Successfully ...";
                save.Enabled = false;
                dt_del.Rows.Clear();
                dt_del.AcceptChanges();
                dt.Rows.Clear();
                dt.AcceptChanges();
                Session["del_all_fps"] = "N";
                ddl_do_no.SelectedIndex = 0;
                // panel_do.Enabled = false;                
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
        }
    }

    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void get_lead()
    {
        string dist = distid;
        ddl_lead.Items.Clear();
        cmd.CommandText = "select * from dbo.m_LeadSoc where District_code='" + distid + "' order by LeadSoc_nameU";
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
    protected void get_do_no()
    {
        Label1.Text = "";
        tx_permit_no.Text = "";
       // tx_issueto.Text = "";
        tx_tot_qty.Text = "";
        tx_dd_no.Text = "";
        tx_dd_amount.Text = "";
        string dist = distid;
        string issue_centre_code = sid;
        ddl_do_no.Items.Clear();
        cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where district_code='" + dist + "' and issueCentre_code='" + sid + "' and status='N' and delivery_order_no not like '%NoDO%' order by  created_date desc";
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
        Label1.ForeColor = System.Drawing.Color.Blue;
        //ddl_do_no.Enabled = false;
        if (ddl_do_no.SelectedItem.Text == "Select")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Delivery Order No. ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            Session["dono"] = null;
            Response.Redirect("~/IssueCenter/Edit_delivery.aspx");
        }
        else
        {
            tx_permit_no.Text = "";
            //tx_issueto.Text = "";
            tx_tot_qty.Text = "";
            // tx_tot_amt.Text = "";
            tx_dd_no.Text = "";
            tx_dd_amount.Text = "";
            Session["dono"] = ddl_do_no.SelectedItem.Text;
            Response.Redirect("~/IssueCenter/Edit_delivery.aspx");

        }
    }
    public string getdateg(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected void get_do_data()
    {
        save.Enabled = true;
        ddl_pmode.Enabled = true;
        Label1.ForeColor = System.Drawing.Color.Blue;
        Label1.Text = "";
        string do_no = Session["dono"].ToString();
        ddl_do_no.SelectedValue = do_no;
        string dist = distid;
        string issue_centre_code = sid;
        //string bal_qty = "";
        string issue_to = "";
        string lead_code = "";
        // string pmode = "";
        string do_date = "";
        string permit_date = "";
        string dd_date = "";
        string templift = "";
        string dovalidity = "";
        ddl_bank.SelectedIndex = 0;
        cmd.CommandText = "SELECT *,DATEADD(day,do_validity,do_date) as validdate,convert(decimal(18,5),quantity) as quantity1,convert(decimal(18,5),tot_amount) as tot_amount1,convert(decimal(18,5),amount) as amount1 FROM dbo.delivery_order_mpscsc where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issueCentre_code='" + sid + "'";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            dovalidity = dr["validdate"].ToString();
            tx_permit_no.Text = dr["permit_no"].ToString();
            issue_to = dr["issue_type"].ToString();
            tx_tot_qty.Text =CheckNull( dr["quantity1"].ToString()).ToString();
            tx_tot_amt.Text = CheckNull(dr["tot_amount1"].ToString()).ToString();
            lead_code = dr["issue_name"].ToString();
            tx_dd_no.Text = dr["dd_no"].ToString();
            tx_dd_amount.Text = CheckNull (dr["amount1"].ToString()).ToString();
            do_date = dr["do_date"].ToString();
            permit_date = dr["permit_date"].ToString();
            dd_date = dr["dd_date"].ToString();
            ddl_bank.SelectedValue = dr["bank_id"].ToString();
            ddl_allot_month.SelectedValue = dr["allotment_month"].ToString();
            ddd_allot_year.SelectedValue = dr["allotment_year"].ToString();
            ddl_commodity.SelectedValue = dr["commodity_id"].ToString();
            ddl_scheme.SelectedValue = dr["scheme_id"].ToString();
            templift = dr["status"].ToString();
            tx_rate_qt.Text =CheckNull ( dr["rate_per_qtls"].ToString()).ToString();
            ddl_pmode.SelectedValue = dr["payment_mode"].ToString();
        }

        dr.Close();
        string isuueqty = "";
        cmd.CommandText = "SELECT * FROM dbo.issue_against_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issueCentre_code='" + sid + "' and  allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and delivery_order_no not like '%NoDO%'";
        cmd.Connection = con;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            isuueqty = "YYY";
            break;
        }

        dr.Close();
        con.Close();

        if (tx_tot_qty.Text != "")
        {
            tx_tot_qty.Text = System.Math.Round(CheckNull(tx_tot_qty.Text), 5).ToString();
        }
        if (tx_tot_amt.Text != "")
        {
            tx_tot_amt.Text = System.Math.Round(CheckNull(tx_tot_amt.Text), 2).ToString();
        }
        if (tx_dd_amount.Text != "")
        {
            tx_dd_amount.Text = System.Math.Round(CheckNull(tx_dd_amount.Text), 2).ToString();
        }
        tx_do_date.Text = getdateg(do_date);
        tx_permit_date.Text = getdateg(permit_date);
        tx_do_validity.Text  = getdateg(dovalidity);
        tx_dd_date.Text  = getdateg(dd_date);
        tx_do_date.Enabled = true;
        tx_permit_date.Enabled = true;
        tx_dd_date.Enabled = true;
        tx_do_validity.Enabled = true;
        Session["issuetype"] = issue_to;
        Session["issueto"] = lead_code;
        if (issue_to == "L")
        {
            ddl_lead.Visible = true;
            tx_lead.Visible = false;
            ddl_lead.SelectedValue = lead_code;
        }
        tx_dd_no.Enabled = true;
        ddl_bank.Enabled = true;
        RequiredFieldValidator1.Enabled = true;
        RequiredFieldValidator3.Enabled = true;
        ddl_lead.SelectedIndex = 0;
        ddl_block.SelectedIndex = 0;
        ddl_issueto.SelectedValue = issue_to;
        if (issue_to == "L" || issue_to == "LF")
        {
            if (lead_code == "")
            {
                lead_code = "N";
            }
           // tx_issueto.Text = "Lead Society";
            ddl_lead.Visible = true;
            tx_lead.Visible = false;
            ddl_lead.SelectedValue = lead_code;

        }

        if (issue_to == "F")
        {
            //tx_issueto.Text = "FPS";
            ddl_lead.Visible = false;
            tx_lead.Visible = false;
            tx_lead.Text = "";
        }
        if (issue_to == "O")
        {
            ddl_lead.Visible = false;
            tx_lead.Visible = true;
            //tx_issueto.Text = "Others";
            tx_lead.Text = lead_code;

        }

        if (ddl_pmode.SelectedItem.Value == "D")
        {
            tx_dd_no.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator1.Enabled = true;
            lbl_ddno.Visible = true;
            lbl_amt.Visible = true;
        }
        else
        {
            tx_dd_no.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator3.Enabled = true;
            lbl_ddno.Visible = false;
            lbl_amt.Visible = true;
            if (ddl_pmode.SelectedItem.Value == "R")
            {
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator1.Enabled = false;
                lbl_amt.Visible = false;
                tx_dd_amount.Text = "0";
                ddl_bank.Enabled = false;
            }

        }
        if (isuueqty == "YYY")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('DO quantity has Lifted..User can Change only DO Validity ...');</script>");
            Label1.Text = "DO quantity has Lifted..User can Change only DO Validity ...";
            //save.Enabled = false;
            ddl_pmode.Enabled = false;
            tx_permit_no.ReadOnly = true;
           // tx_issueto.ReadOnly = true;
            tx_tot_qty.ReadOnly = true;
            tx_tot_amt.ReadOnly = true;
            tx_dd_no.ReadOnly = true;
            tx_dd_amount.ReadOnly = true;
            ddl_bank.Enabled = false;
            ddl_lead.Enabled = false;
           ddl_commodity.Enabled  = false;
           ddl_scheme.Enabled = false;
           ddl_rate_type.Enabled = false;

        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("select do_fps.block_code,opdms.pds.fps_master.fps_code,opdms.pds.fps_master.fps_name,do_fps.commodity as commodity_id,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name,do_fps.scheme_id,tbl_MetaData_SCHEME .Scheme_Name,convert(decimal(18,5),do_fps.rate_per_qtls) as rate_qtls,convert(decimal(18,5),do_fps.quantity) as qty,convert(decimal(18,2),convert(decimal(18,5),do_fps.quantity)*convert(decimal(18,5),do_fps.rate_per_qtls)) as amt,convert(decimal(18,5),do_fps.allot_qty) as allot_qty from dbo.do_fps LEFT JOIN opdms.pds.fps_master ON do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id where do_fps.delivery_order_no='" + do_no + "' and do_fps.district_code='" + dist + "' and do_fps.issueCentre_code='" + sid + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.status='N'", con);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
            if (issue_to == "LF" || issue_to == "F")
            {
                tx_tot_qty.ReadOnly = true;
                tx_tot_qty.AutoPostBack = false;
                tx_tot_qty.BackColor = System.Drawing.Color.Linen;
                get_fps();
                lbl_blk.Visible = true;
                lbl_fps.Visible = true;
                lbl_allotqty.Visible = true;
                lbl_issueqty.Visible = true;
                lbl_balqty.Visible = true;
                lbl_qtls.Visible = true;
                lbl_qty.Visible = true;
                Label3.Visible = true;
                lbl_qtls1.Visible = true;
                Button1.Visible = true;
                ddl_block.Visible = true;
                ddl_fps_name.Visible = true;
                tx_allot_qty2.Visible = true;
                tx_already_iqty2.Visible = true;
                tx_balQty2.Visible = true;
                tx_qty.Visible = true;
                lbl_dodtl.Visible = true;
                get_IssuedQty();
                get_balAtIC();
                get_allotQty();
            }
            else
            {
                tx_tot_qty.ReadOnly = false;
                tx_tot_qty.AutoPostBack = true;
                tx_tot_qty.BackColor = System.Drawing.Color.White;
                lbl_blk.Visible = false;
                lbl_fps.Visible = false;
                lbl_allotqty.Visible = false;
                lbl_issueqty.Visible = false;
                lbl_balqty.Visible = false;
                lbl_qtls.Visible = false;
                lbl_qty.Visible = false;
                Label3.Visible = false;
                lbl_qtls1.Visible = false;
                Button1.Visible = false;
                ddl_block.Visible = false;
                ddl_fps_name.Visible = false;
                tx_allot_qty2.Visible = false;
                tx_already_iqty2.Visible = false;
                tx_balQty2.Visible = false;
                tx_qty.Visible = false;
                lbl_dodtl.Visible = false;
            }
        }
        get_ComdtyBal();
        Session["dono"] = null;
    }
    protected void get_fps()
    {
        string dist = distid;
        ddl_fps_name.Items.Clear();
        cmd.CommandText = "SELECT pds.fps_master.block_code,pds.fps_master.fps_name, pds.fps_master.fps_code FROM dbo.Lead_soc_fps Left JOIN pds.fps_master ON Lead_soc_fps.District_code = pds.fps_master.district_code AND Lead_soc_fps.fps_code = pds.fps_master.fps_code where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "' order by pds.fps_master.fps_name";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["fps_name"].ToString();
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
    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        //get_do_no();
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected void get_bankname()
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
    protected void Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected string changeDate(DateTime inDate, int inDays)
    {
        int noofdays = DateTime.DaysInMonth(inDate.Year, inDate.Month);
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
        return (xday + "/" + xmonth + "/" + xyear);
    }
  protected  decimal CheckNull(string Val)
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (ddl_fps_name.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS ...');</script>");
            Label1.Text = "Please select FPS ...";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
        {

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
                if (CheckNull(tx_qty.Text) > bal_qty || CheckNull(tx_qty.Text) <= 0)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Alloted...');</script>");
                    tx_qty.Text = "";
                    tx_qty.Focus();
                }
                else
                {
                    //tx_tot_qty.Text = System.Math.Round((CheckNull(tx_tot_qty.Text) + CheckNull(tx_qty.Text)), 5).ToString();
                    //tx_tot_amt.Text = System.Math.Round((CheckNull(tx_tot_amt.Text) + (CheckNull(tx_qty.Text) * CheckNull(tx_rate_qt.Text))), 2).ToString();
                    dt = (DataTable)Session["dt"];
                    dt.Rows.Add(ddl_block.SelectedItem.Value, ddl_fps_name.SelectedItem.Value, ddl_fps_name.SelectedItem.Text, ddl_commodity.SelectedItem.Value, ddl_commodity.SelectedItem.Text, ddl_scheme.SelectedItem.Value, ddl_scheme.SelectedItem.Text, tx_rate_qt.Text, tx_qty.Text,System.Math.Round(CheckNull(tx_qty.Text) * CheckNull(tx_rate_qt.Text),2), tx_allot_qty2.Text);
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
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //decimal qty = CheckNull(GridView1.SelectedRow.Cells[5].Text);
        //decimal amt = CheckNull(GridView1.SelectedRow.Cells[6].Text);
        //tx_tot_qty.Text = System.Math.Round((CheckNull(tx_tot_qty.Text) - qty), 5).ToString();
        //tx_tot_amt.Text = System.Math.Round((CheckNull(tx_tot_amt.Text) - amt), 2).ToString();        
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        string do_no = ddl_do_no.SelectedItem.Text;
        string blkcode = dt.Rows[idx][0].ToString();
        string fpscode = dt.Rows[idx][1].ToString();
        string commcode = dt.Rows[idx][3].ToString();
        string schemecode = dt.Rows[idx][5].ToString();
        decimal iqty = CheckNull(dt.Rows[idx][8].ToString());
        decimal aqty = CheckNull(dt.Rows[idx][10].ToString());
        string temp = "NNN";
        cmd.CommandText = "select * from dbo.do_fps where delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and  allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and block_code='" + blkcode + "' and fps_code='" + fpscode + "' and commodity='" + commcode + "' and scheme_id='" + schemecode + "'";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            temp = "YYY";
        }
        dr.Close();
        con.Close();
        if (temp == "YYY")
        {
            dt_del = (DataTable)Session["dt_del"];
            dt_del.Rows.Add(blkcode, fpscode, dt.Rows[idx][2].ToString(), commcode, dt.Rows[idx][4].ToString(), schemecode, dt.Rows[idx][6].ToString(), dt.Rows[idx][7].ToString(), iqty, dt.Rows[idx][9].ToString(), aqty);
            Session["dt_del"] = dt_del;
        }
        dt.Rows[idx].Delete();
        dt.AcceptChanges();
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
    }
    protected void delEdit_fps()
    {
        DataTable dt1 = (DataTable)Session["dt_del"];
        int idx = 0;
        string do_no = ddl_do_no.SelectedItem.Text;
        while (idx < dt1.Rows.Count)
        {           
            string blkcode = dt1.Rows[idx][0].ToString();
            string fpscode = dt1.Rows[idx][1].ToString();
            string commcode = dt1.Rows[idx][3].ToString();
            string schemecode = dt1.Rows[idx][5].ToString();
            decimal iqty = CheckNull(dt1.Rows[idx][8].ToString());
            decimal aqty = CheckNull(dt1.Rows[idx][10].ToString());
            string str1 = "delete from dbo.do_fps where delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and  allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and block_code='" + blkcode + "' and fps_code='" + fpscode + "' and commodity='" + commcode + "' and scheme_id='" + schemecode + "'";
            cmd.CommandText = str1;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            str1 = "update dbo.sum_trans_do set allot_qty=convert(decimal(18,5),allot_qty)-" + aqty + ",issue_qty=convert(decimal(18,5),issue_qty)-" + iqty + "  where district_code='" + distid + "' and issueCentre_code='" + sid + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
            cmd.CommandText = str1;
            cmd.ExecuteNonQuery();
            ///////////////////////////
            string comm = dt1.Rows[idx][4].ToString();
            string scheme = dt1.Rows[idx][6].ToString();
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
            }
            if (comm.ToLower().Contains("sugar"))
            {
                commodity = "sugar_alloc";
            }
            string strr = "";
            string allotqty = "0";
            strr = "update pds.fps_allot_mpsc set " + commodity + "=" + allotqty + ",updated_on=getdate()  where district_code='" + distid + "' and block_code='" + dt1.Rows[idx][0] + "' and  depot_code='" + sid + "' and  fps_code='" + dt1.Rows[idx][1] + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
            cmd.CommandText = strr;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            idx = idx + 1;
        }
    }
    protected void get_block()
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
            lstitem.Text = dr["Block_name"].ToString();
            lstitem.Value = dr["block_code"].ToString();
            ddl_block.Items.Add(lstitem);
        }
        ddl_block.Items.Insert(0, "Select");
        dr.Close();
        con_opdms.Close();
    }
    protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_blkFPS();
    }
    protected void get_blkFPS()
    {
        string dist = distid;
        ddl_fps_name.Items.Clear();
        string blk = ddl_block.SelectedItem.Value;
        cmd.CommandText = "SELECT * FROM pds.fps_master where district_code='" + dist + "' and block_code='" + blk + "' and del_status='False' order by fps_name";
        cmd.Connection = con_opdms;
        con_opdms.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["fps_name"].ToString();
            lstitem.Value = dr["fps_code"].ToString();
            ddl_fps_name.Items.Add(lstitem);
        }
        ddl_fps_name.Items.Insert(0, "Select");
        dr.Close();
        con_opdms.Close();
    }
    protected void ddl_fps_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        tx_qty.Text = "";
        Button1.Enabled = true;
        Label1.Text = "";
        //tx_rate_qt.Text = ""; 
        tx_allot_qty2.Text = "";
        tx_already_iqty2.Text = "";
        tx_balQty2.Text = "";
        //tx_allot_qty.Text = "";
        if (ddl_fps_name.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS...');</script>");
        }
        else
        {
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
            int amonth = CheckNullInt(ddl_allot_month.SelectedItem.Value);
            int ayear = CheckNullInt(ddd_allot_year.SelectedItem.Text);
            string fps_code = ddl_fps_name.SelectedItem.Value;
            //get_IssuedQty();
            // get_balAtIC();
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


            }
            if (comm.ToLower().Contains("sugar"))
            {
                commodity = "sugar_alloc";
            }

            if (commodity != "")
            {
                cmd.CommandText = "select  convert(decimal(18,5)," + commodity + ") as commodity1   from pds.fps_allot where district_code='" + distid + "' and block_code='" + ddl_block.SelectedItem.Value + "' and fps_code='" + fps_code + "' and month=" + amonth + " and Year=" + ayear + "";
                cmd.Connection = con_opdms;
                con_opdms.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tx_allot_qty2.Text = System.Math.Round(CheckNull(dr["commodity1"].ToString()), 5).ToString();
                }
                dr.Close();
                con_opdms.Close();
            }
            if (tx_allot_qty2.Text == "")
            {
                tx_allot_qty2.Text = "0";
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotmennt for Selected Commodity...');</script>");
            }
            if (tx_already_iqty2.Text == "")
            {
                tx_already_iqty2.Text = "0";
            }
            tx_balQty2.Text = "";
            tx_balQty2.Text = System.Math.Round(CheckNull(tx_allot_qty2.Text) - CheckNull(tx_already_iqty2.Text), 5).ToString();
            tx_qty.Focus();
        }

    }
    protected void ddl_pmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        ddl_bank.Enabled = true;
        Label1.ForeColor = System.Drawing.Color.Blue;
        if (ddl_pmode.SelectedItem.Value == "D")
        {
            tx_dd_no.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator1.Enabled = true;
            lbl_ddno.Visible = true;
            lbl_amt.Visible = true;
        }
        else
        {
            tx_dd_no.Enabled = false;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator1.Enabled = false;
            lbl_ddno.Visible = false;
            lbl_amt.Visible = true;
            if (ddl_pmode.SelectedItem.Value == "R")
            {
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator1.Enabled = false;
                lbl_amt.Visible = false;
                tx_dd_amount.Text = "0";
                ddl_bank.Enabled = false;
            }
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
        lstitem1.Text = "Select";
        lstitem1.Value = "N";
        ddl_commodity.Items.Insert(0, lstitem1);

        dr.Close();
        con.Close();

    }
    protected void get_scheme()
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
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Session["dono"] = null;
        Response.Redirect("~/IssueCenter/Edit_delivery.aspx");
    }
    protected void tx_tot_qty_TextChanged(object sender, EventArgs e)
    {
        if (tx_tot_qty.Text == "" || tx_tot_qty.Text == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter Quantity ...');</script>");
        }
        else
        {
            tx_tot_amt.Text = System.Math.Round(CheckNull(tx_tot_qty.Text) * CheckNull(tx_rate_qt.Text), 2).ToString();
        }
    }
    protected void ddl_lead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_do_no.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Delivery Order No. ...');</script>");
        }
        else if (ddl_lead.SelectedItem.Text == "pq«Uk;¢")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Lead Society ...');</script>");
        } 
        else
        {
            get_fps();
            tx_dd_amount.Text = "";
            tx_tot_amt.Text = "";
            tx_tot_qty.Text = "";
            tx_qty.Text = "";
            ddl_commodity.SelectedIndex = 0;
            ddl_scheme.SelectedIndex = 0;
            dt = (DataTable )Session["dt"];
            dt.Rows.Clear();
            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
            Session["del_all_fps"] = "Y";
        }
    }
    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_do_no.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Delivery Order No. ...');</script>");
        }        
        else if (ddl_commodity.SelectedItem.Text == "Select" || ddl_lead.SelectedItem.Text == "pq«Uk;¢")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Lead Society/Commodity ...');</script>");
        } 
        else
        {
            tx_dd_amount.Text = "";
            tx_tot_amt.Text = "";
            tx_tot_qty.Text = "";
            tx_qty.Text = "";
            tx_balQty2.Text = "0";
            txtcomdty_bal.Text = "0";           
            get_ComdtyBal();
            DataTable dtnew = new DataTable();
            GridView1.DataSource = dtnew;
            GridView1.DataBind();
            Session["del_all_fps"] = "Y";
        }
    }
    protected void get_ComdtyBal()
    {
        txtcomdty_bal.Text = "0";
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
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        tx_tot_amt.Text = "";
        tx_tot_qty.Text = "";
        tx_qty.Text = "";
        tx_allot_qty.Text = "";
        tx_balQty2.Text = "";
        tx_dd_amount.Text = "";
        if (ddl_do_no.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Delivery Order No. ...');</script>");
        }
        else if (ddl_issueto.SelectedItem.Value == "F" && ddl_commodity.SelectedItem.Text != "Select" && ddl_scheme.SelectedItem.Text != "Select")
        {
            GetRate();
            dt = (DataTable)Session["dt"];
            dt.Rows.Clear();
            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
            Session["del_all_fps"] = "Y";
        }
        else if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select" || ddl_lead.SelectedItem.Text == "pq«Uk;¢")
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
            dt = (DataTable)Session["dt"];
            dt.Rows.Clear();
            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
            Session["del_all_fps"] = "Y";
        }
    }
    protected void GetRate()
    {
        DObj = new Districts(ComObj);
        if (ddl_commodity.SelectedItem.Text == "Select" || ddl_scheme.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
        }
        else if (ddl_rate_type.SelectedItem.Value == "U")
        {
            string qry = "Select convert(decimal(18,5),Uraban_rate) as Uraban_rate  from dbo.SCSC_IssueRate where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "' and District_code='" + distid + "'";
            DObj = new Districts(ComObj);
            DataSet ds = DObj.selectAny(qry);
            if (ds == null)
            {

            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Rate is not available for selected commodity....'); </script> ");
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
        }
        else
        {
            string qry = "Select convert(decimal(18,5),Rural_rate) as Rural_rate from dbo.SCSC_IssueRate  where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "' and District_code='" + distid + "'";
            DObj = new Districts(ComObj);
            DataSet ds = DObj.selectAny(qry);
            if (ds==null)
            {
                
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Rate is not available for selected commodity....'); </script> ");
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
    }
    protected void get_IssuedQty()
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
    protected void get_balAtIC()
    {
        tx_bal_ic.Text = "";
        string cmdstr = "";
        if (ddl_commodity.SelectedItem.Text.ToLower().Contains("sugar"))
        {
            cmdstr = "SELECT round(sum(convert(decimal(18,5),Current_Balance)),5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'";
        }
        else
        {
            cmdstr = "SELECT round(sum(convert(decimal(18,5),Current_Balance)),5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "'";
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
    protected void get_allotQty()
    {
        string comm = ddl_commodity.SelectedItem.Text;
        string scheme = ddl_scheme.SelectedItem.Text;
        int amonth = CheckNullInt(ddl_allot_month.SelectedItem.Value);
        int ayear = CheckNullInt(ddd_allot_year.SelectedItem.Text);
        string fps_code = ddl_fps_name.SelectedItem.Value;
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
        }
        if (comm.ToLower().Contains("sugar"))
        {
            commodity = "sugar_alloc";
        }

        if (commodity != "")
        {
            cmd.CommandText = "SELECT sum(convert(decimal(18,5)," + commodity + ")) as commodity FROM   pds.fps_allot INNER JOIN  dbo.Lead_soc_fps ON pds.fps_allot.district_code = Lead_soc_fps.District_code AND pds.fps_allot.fps_code = Lead_soc_fps.fps_code and pds.fps_allot.month=" + amonth + " and pds.fps_allot.year=" + ayear + "  where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "'";
            cmd.Connection = con_opdms;
            cmd.CommandTimeout = 2400;
            con_opdms.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tx_allot_qty.Text = System.Math.Round(CheckNull(dr["commodity"].ToString()), 5).ToString();
            }
            dr.Close();
            con_opdms.Close();
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
    protected void ddl_rate_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRate();
        tx_dd_amount.Text = "";
        tx_tot_amt.Text = "";
        tx_tot_qty.Text = "";
        tx_qty.Text = "";
        dt = (DataTable)Session["dt"];
        dt.Rows.Clear();
        dt.AcceptChanges();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;
        Session["del_all_fps"] = "Y";
    }
    protected void del_fps()
    {        
        DataTable dtfps = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("SELECT do_fps.trans_id, do_fps.delivery_order_no, do_fps.district_code, do_fps.issueCentre_code, do_fps.block_code, do_fps.allotment_month, do_fps.allotment_year, do_fps.fps_code, do_fps.commodity, do_fps.scheme_id, convert(decimal(18,5),do_fps.allot_qty) as allot_qty,convert(decimal(18,5),do_fps.quantity) as quantity , convert(decimal(18,5),do_fps.rate_per_qtls) as rate_per_qtls, do_fps.status, do_fps.ip_add, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, tbl_MetaData_SCHEME.Scheme_Name FROM dbo.do_fps LEFT JOIN  dbo.tbl_MetaData_STORAGE_COMMODITY ON  do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME ON do_fps.scheme_id = tbl_MetaData_SCHEME.Scheme_Id where do_fps.delivery_order_no ='" + ddl_do_no.SelectedItem.Text + "' and do_fps.district_code='" + distid + "' and do_fps.issueCentre_code='" + sid + "' and  do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year =" + ddd_allot_year.SelectedItem.Text + "", con);
        da.Fill(dtfps);
        int idx =0;
        int rcount = dtfps.Rows.Count;
        decimal taqty = 0;
        decimal tiqty = 0;
        con.Open();
        string temp = "NNN";
        while (idx < rcount)
        {   
            string do_no = ddl_do_no.SelectedItem.Text;
            string blkcode = dtfps.Rows[idx][4].ToString();
            string fpscode = dtfps.Rows[idx][7].ToString();
            string commcode = dtfps.Rows[idx][8].ToString();
            string schemecode = dtfps.Rows[idx][9].ToString();
            taqty =taqty + CheckNull(dtfps.Rows[idx][10].ToString());
            tiqty =tiqty + CheckNull(dtfps.Rows[idx][11].ToString());
            string comm = dtfps.Rows[idx][15].ToString();
            string scheme = dtfps.Rows[idx][16].ToString();
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
                }
                if (comm.ToLower().Contains("sugar"))
                {
                    commodity = "sugar_alloc";
                }
                string strr = "";
                string allotqty = "0";
                strr = "update pds.fps_allot_mpsc set " + commodity + "=" + allotqty + ",updated_on=getdate()  where district_code='" + distid + "' and block_code='" + blkcode + "' and  depot_code='" + sid + "' and  fps_code='" + fpscode + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
                cmd.CommandText = strr;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                temp = "YYY";
                idx = idx + 1;
           }
           string str1 = "";
           if (temp == "YYY")
           {
               str1 = "delete from dbo.do_fps where delivery_order_no='" + ddl_do_no.SelectedItem.Text + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and  allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
               cmd.CommandText = str1;
               cmd.Connection = con;
               cmd.ExecuteNonQuery();
           }
           str1 = "update dbo.sum_trans_do set allot_qty=convert(decimal(18,5),allot_qty)-" + taqty + ",issue_qty=convert(decimal(18,5),issue_qty)-" + tiqty + "  where district_code='" + distid + "' and issueCentre_code='" + sid + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
            cmd.CommandText = str1;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            //SqlDataAdapter da1 = new SqlDataAdapter("select do_fps.block_code,opdms.pds.fps_master.fps_code,opdms.pds.fps_master.fps_name,do_fps.commodity as commodity_id,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name,do_fps.scheme_id,tbl_MetaData_SCHEME .Scheme_Name,convert(decimal(18,5),do_fps.rate_per_qtls) as rate_qtls,convert(decimal(18,5),do_fps.quantity) as qty,convert(decimal(18,5),do_fps.quantity)*convert(decimal(18,5),do_fps.rate_per_qtls) as amt,convert(decimal(18,5),do_fps.allot_qty) as allot_qty from dbo.do_fps LEFT JOIN opdms.pds.fps_master ON do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id where do_fps.delivery_order_no='" + ddl_do_no.SelectedItem.Text + "' and do_fps.district_code='" + distid + "' and do_fps.issueCentre_code='" + sid + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.status='N'", con);
            //da1.Fill(dt);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            //Session["dt"] = dt;
    }
}