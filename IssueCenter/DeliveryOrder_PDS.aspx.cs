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

public partial class IssueCenter_DeliveryOrder_PDS : System.Web.UI.Page
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
        if ((string)Session["dist_id"] != null)
        {
            ddl_issueto.Items.Clear();
            ddl_issueto.Items.Add(new ListItem("Only FPS", "F"));
        }

        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        version = Session["hindi"].ToString();
        if (!IsPostBack)
        {

            tx_dd_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            tx_permit_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            tx_do_date.Text = DateTime.Today.Date.Date.ToString("dd/MM/yyyy");
            tx_do_validity.Text = DateTime.Today.Date.Date.ToString("dd/MM/yyyy");
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            GET_Table_Rows();
            Session["dt"] = dt;
            get_comm();
            get_block();
            GetScheme();
            get_bankname();
            GetName();
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            Lstbx_fps_name.Enabled = false;
            GetPermit();

            if (version == "H")
            {
                save.Text = Resources.Hindi_New.btnsave;
                btn_Add_FPS.Text = Resources.Hindi_New.btnAdd_FPS;
                btn_new.Text = Resources.Hindi_New.btnaddnew;
                btnClose.Text = Resources.Hindi_New.btnclose;
                lblpayment.Text = Resources.Hindi_New.lblpayment;
                lbl_Avail_Head.Text = Resources.Hindi_New.lbl_Avail_Head;
                lblpermitno.Text = Resources.Hindi_New.lblpermitno;
                lblpermitdate.Text = Resources.Hindi_New.lblpermitdate;
                lblallotyear.Text = Resources.Hindi_New.lblallotyear;
                lblallotmonth.Text = Resources.Hindi_New.lblallotmonth;
                lblCommodity.Text = Resources.Hindi_New.lblCommodity;
                lblScheme.Text = Resources.Hindi_New.lblScheme;
                lblbalcomdty.Text = Resources.Hindi_New.lblbalcomdty;
                lblratetype.Text = Resources.Hindi_New.lblratetype;

                lbltoissue.Text = Resources.Hindi_New.lbltoissue;

                lblpermitno1.Text = Resources.Hindi_New.lblpermitno1;

                lbldono.Text = Resources.Hindi_New.lbldono;
                lbldodate.Text = Resources.Hindi_New.lbldodate;
                lbldovalidity.Text = Resources.Hindi_New.lbldovalidity;
                lbl_blk.Text = Resources.Hindi_New.lbl_blk;

                lbl_fps.Text = Resources.Hindi_New.lbl_fps;
                lbl_allotqty.Text = Resources.Hindi_New.lbl_allotqty;
                lbl_issueqty.Text = Resources.Hindi_New.lbl_issueqty;
                lbl_balqty.Text = Resources.Hindi_New.lbl_balqty;

                lblQuantity.Text = Resources.Hindi_New.lblQuantity;
                totaldoqty.Text = Resources.Hindi_New.totaldoqty;
                lbldo.Text = Resources.Hindi_New.lbldo;
                lbldodetails.Text = Resources.Hindi_New.lbldodetails;
                lbldodetailsfps.Text = Resources.Hindi_New.lbldodetailsfps;

                lblRateQuintal.Text = Resources.Hindi_New.lblRateQuintal;
                lblPaymentMode.Text = Resources.Hindi_New.lblPaymentMode;
                lblddchekno.Text = Resources.Hindi_New.lblddchekno;
                lblddchekdate.Text = Resources.Hindi_New.lblddchekdate;
                lblBankName.Text = Resources.Hindi_New.lblBankName;
                lblamount.Text = Resources.Hindi_New.lblamount;
                lbltotamt.Text = Resources.Hindi_New.lbltotamt;
            }
        }

        tx_do_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_rate_qt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_amount.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_amount.Attributes.Add("onchange", "return chksqltxt(this)");
        tx_dd_amount.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_rate_qt.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_rate_qt.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_permit_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_permit_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_permit_no.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_do_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_no.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_dd_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_no.Attributes.Add("onchange", "return chksqltxt(this)");

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

        tx_permit_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_validity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_rate_qt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_amt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txtcomdty_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balQty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_already_iqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        chk = new chksql();
        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(tx_permit_no.Text);
        ctrllist.Add(tx_do_no.Text);
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

        if (ddl_block.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Block ......');</script>");
        }
        else
        {
            get_blkFPS();
        }

    }
    protected void save_Click(object sender, EventArgs e)
    {
        try
        {
           
            string opid = Session["OperatorId"].ToString();
            string state = Session["State_Id"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (CheckNull(txtcomdty_bal.Text) == 0)
            {
                lbl_Msg.Visible = true;
                lbl_Msg.Text = "Sorry You dont't have balance ....";
                lbl_Msg.ForeColor = System.Drawing.Color.Red;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry You dont't have balance ......');</script>");
            }
            else
            {
                lbl_Msg.Text = "";
                lbl_Msg.ForeColor = System.Drawing.Color.Blue;
                dt = (DataTable)Session["dt"];
                int count = dt.Rows.Count;
                if (count > 0)
                {
                    string dist = distid;
                    string issue_centre_code = sid;
                    string issue_type = ddl_issueto.SelectedItem.Value;
                    string issue_name = "";
                    string month = ddl_allot_month.SelectedItem.Value;
                    string year = ddd_allot_year.SelectedItem.Value;
                    int    permit_valid = 0;
                    string do_no = tx_do_no.Text.Trim().ToString();
                    string per_no = tx_permit_no.Text.Trim();
                    string pmode = ddl_pmode.SelectedItem.Value;
                    string dd_no = tx_dd_no.Text.Trim();
                    string temp = "yyy";
                    string do_date = getDate_MDY(tx_do_date.Text);
                    string permit_date = getDate_MDY(tx_permit_date.Text);
                    string dd_date = "";

                    if (tx_dd_date.Text != "")
                    {
                       dd_date = getDate_MDY(tx_dd_date.Text);
                    }
                  
                    string notrans = "N";
                    string do_validity = "";
                    int do_valid = 0;
                    
                    try
                    {

                        ///////////////////////////////////// Calculating Number Of days for Delivery Order //////////////////////////
                        do_validity = get_days(DateTime.Parse(getDate_MDY(tx_do_date.Text)), DateTime.Parse(getDate_MDY(tx_do_validity.Text)));
                        do_valid = CheckNullInt(do_validity);

                    }
                    catch (Exception)
                    { 

                    }
                        
                        ///////////////////////////////////////Cheking Delivery Order Number exits or not ///////////////////////
                    cmd.CommandText = "select delivery_order_no from dbo.do_fps where delivery_order_no='" + do_no + "' and issueCentre_code='" + issue_centre_code + "' and district_code='" + dist + "'";
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
                        /////////////////////////////////////////// Get Present Commodity Quantity in Table ///////////////
                        
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
                 
                        
                        /////////////////////////////////////// Inserting into Delivery Order mpscsc Table ///////////////
                        
                        string str1 = "INSERT INTO dbo.delivery_order_mpscsc(State_Id,delivery_order_no,district_code,issueCentre_code,permit_no,issue_type,issue_name,allotment_month,allotment_year,do_validity,permit_validity,do_date,permit_date,created_date,updated_date,payment_mode,dd_no,dd_date,quantity,amount,tot_amount,bank_id,commodity_id,scheme_id,rate_per_qtls,status,IP,OperatorID,NoTransaction) VALUES('" + state + "','" + do_no + "','" + dist + "','" + issue_centre_code + "','" + per_no + "','" + issue_type + "','" + issue_name + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + "," + do_valid + "," + permit_valid + ",'" + do_date + "','" + permit_date + "',getdate(),'','" + pmode + "','" + dd_no + "','" + dd_date + "'," + tx_tot_qty.Text + "," + tx_dd_amount.Text + "," + tx_tot_amt.Text + ",'" + ddl_bank.SelectedItem.Value + "'," + ddl_commodity.SelectedItem.Value + ",'" + ddl_scheme.SelectedItem.Value + "'," + tx_rate_qt.Text + ",'N','" + ip + "','" + opid + "','" + notrans + "')";
                        cmd.CommandText = str1;
                        cmd.Connection = con;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();

                        ///////////////////////////////////////////   Insert into DO_FPS Table   /////////////////////////
                            
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

                                ////////////////////////////////////////// Insert into D.o fps table //////////////////////////
                                
                                string transid = ddd_allot_year.SelectedItem.Text + "-" + dist.ToString() + "-" + do_no.ToString() + "-" + (trnscnt).ToString();
                                str1 = "INSERT INTO dbo.do_fps(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,block_code,fps_code,commodity,scheme_id,allot_qty,quantity,rate_per_qtls,status,ip_add)VALUES('" + state + "','" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + ",'" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][10] + "'," + dt.Rows[i][8] + "," + dt.Rows[i][7] + ",'N','" + ip + "')";
                                cmd.CommandText = str1;
                                cmd.ExecuteNonQuery();
                                allot_qty = allot_qty + CheckNull(dt.Rows[i][10].ToString());
                                issue_qty = issue_qty + CheckNull(dt.Rows[i][8].ToString());

                                ////////////////////////////////// For pds.fps_allot_mpsc table /////////////////

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
                                    strr = "INSERT INTO pds.fps_allot_mpsc(State_Id,district_code,block_code,depot_code,fps_code,month,Year," + commodity + ",initial_creation,IP,OperatorID) values ('" + state + "','" + distid + "','" + dt.Rows[i][0] + "','" + sid + "','" + dt.Rows[i][1] + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + "," + dt.Rows[i][10] + ",getdate(),'" + ip + "','" + opid + "')";
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

                            //----------------------

                            //----------------------

                            save.Enabled = false;

                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                            String s = Request.QueryString["field1"];
                            StringBuilder sbScript = new StringBuilder();

                            sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
                            sbScript.Append("<!--\n");
                            sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
                            sbScript.Append("// -->\n");
                            sbScript.Append("</script>\n");

                            this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
                            lbl_Msg.Text = "Data Saved Successfully ...";

                            hlinkpdo.Visible = true;
                            hlinkpdo.Enabled = true;
                            Session["doforprint"] = do_no;

                            txtcomdty_bal.Text = "";
                            lblFPSNAme.Text = "";
                        }
                        catch (Exception ex)
                        {
                            lbl_Msg.Text = ex.Message;
                            lbl_Msg.ForeColor = System.Drawing.Color.Red;
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
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Delivery Order Number already exist');</script>");
                        lbl_Msg.ForeColor = System.Drawing.Color.Red;
                        lbl_Msg.Text = "Record already exist ... Change DO No.";
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter FPS details');</script>");
                    lbl_Msg.ForeColor = System.Drawing.Color.Red;
                    lbl_Msg.Text = "Please enter FPS details ...";
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Try Again ...');</script>");
        }
    }
    protected void get_comm()
    {
        try
        {
            cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (1,2,3,4,5,6,7,17,20,22,23,61,62) and status='Y' order by commodity_name";
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
            lstitem1.Text = "--Select--";
            lstitem1.Value = "N";
            ddl_commodity.Items.Insert(0, lstitem1);
            dr.Close();
            con.Close();
        }
        catch (Exception)
        {

        }


    }
    protected void get_blkFPS()
    {
        try
        {

            string dist = distid;
            Lstbx_fps_name.Items.Clear();
            string blk = ddl_block.SelectedItem.Value;
            cmd.CommandText = "SELECT fps_Uname,fps_code FROM pds.fps_master_New where district_code='" + dist + "' and block_code='" + blk + "' and del_status='False' order by fps_Uname ";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["fps_Uname"].ToString() + "  (" + dr["fps_code"].ToString() + ")";
                lstitem.Value = dr["fps_code"].ToString();
                Lstbx_fps_name.Items.Add(lstitem);
                Lstbx_fps_name.Enabled = true;

            }
            Lstbx_fps_name.Items.Insert(0, "--Select--");
            dr.Close();
            con.Close();
        }
        catch (Exception)
        {

        }
    }
    protected void get_block()
    {
        try
        {
            string dist = distid;
            ddl_block.Items.Clear();
            cmd.CommandText = "select block_code,Block_Uname from  pds.block_master where District_code=" + dist + " order by Block_Uname";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["Block_Uname"].ToString();
                lstitem.Value = dr["block_code"].ToString();
                ddl_block.Items.Add(lstitem);
            }
            ddl_block.Items.Insert(0, "--Select--");
            dr.Close();
            con.Close();
        }
        catch (Exception)
        {

        }

    }
    public void GetScheme()
    {
        try
        {
            string query = "select Scheme_Id,Scheme_Name from dbo.tbl_MetaData_SCHEME  where Scheme_Id in (0,1,2,3,4) and status='Y' order by Scheme_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddl_scheme.DataSource = ds.Tables[0];
            ddl_scheme.DataTextField = "scheme_name";
            ddl_scheme.DataValueField = "scheme_id";
            ddl_scheme.DataBind();
            ddl_scheme.Items.Insert(0, "--Select--");
            con.Close();
        }
        catch (Exception)
        {

        }
    }
    protected void ddl_pmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lbl_Msg.Text = "";
            ddl_bank.Enabled = true;
            lbl_Msg.ForeColor = System.Drawing.Color.Blue;
            if (ddl_pmode.SelectedItem.Value == "D")
            {
                tx_dd_amount.Text = "";
                tx_dd_no.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                tx_dd_date.Enabled = true;
                tx_dd_amount.Enabled = true;
            }
            else if (ddl_pmode.SelectedItem.Value == "R")
            {
                RequiredFieldValidator4.Enabled = false;
                tx_dd_amount.Text = "0";
                tx_dd_amount.Enabled = false;
                ddl_bank.Enabled = false;
                tx_dd_date.Enabled = false;
                tx_dd_no.Enabled = false;
                tx_dd_date.Text = "";
                RequiredFieldValidator3.Enabled = false;
            }
            else
            {
                ddl_bank.Enabled = false;
                tx_dd_date.Text = "";
                tx_dd_date.Enabled = false;
                tx_dd_amount.Enabled = true;
                tx_dd_no.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator4.Enabled = true;
            }
        }
        catch (Exception EX)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Try Again...');</script>");
        }
    }
    protected void btn_Add_FPS_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_commodity.SelectedItem.Text == "--Select--" || ddl_scheme.SelectedItem.Text == "--Select--" || Lstbx_fps_name.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme');</script>");
                lbl_Msg.Text = "Please select Commodity/Scheme/FPS ...";
                lbl_Msg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lbl_Msg.Text = "";
                tr_head.Visible = true;
                Pnl_Fps_View.Visible = true;
                gv_FPS_Details.Columns[1].Visible = false;
                string temp = "NNN";
                dt = (DataTable)Session["dt"];
                int row = 0;
                if (dt.Rows.Count > 0)
                {
                    while (row < dt.Rows.Count)
                    {
                        if (dt.Rows[row][1].ToString() == Lstbx_fps_name.SelectedItem.Value && dt.Rows[row][3].ToString() == ddl_commodity.SelectedItem.Value && dt.Rows[row][5].ToString() == ddl_scheme.SelectedItem.Value)
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
                    lbl_Msg.Text = "Quantity to be selected FPS/Commodity/Scheme Already Issued";
                    lbl_Msg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    decimal bal_qty = CheckNull(tx_balQty2.Text);
                    if (CheckNull(tx_tot_qty.Text) + CheckNull(tx_qty.Text) > CheckNull(txtcomdty_bal.Text))
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Balance Stock of Commodity...');</script>");
                        tx_qty.Text = "";
                        tx_qty.Focus();
                    }

                    else
                    {

                        dt = (DataTable)Session["dt"];
                        dt.Rows.Add(ddl_block.SelectedItem.Value, Lstbx_fps_name.SelectedItem.Value, Lstbx_fps_name.SelectedItem.Text, ddl_commodity.SelectedItem.Value, ddl_commodity.SelectedItem.Text, ddl_scheme.SelectedItem.Value, ddl_scheme.SelectedItem.Text, tx_rate_qt.Text, tx_qty.Text, CheckNull(tx_qty.Text) * CheckNull(tx_rate_qt.Text), tx_allot_qty2.Text);
                        gv_FPS_Details.DataSource = dt;
                        gv_FPS_Details.DataBind();
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
        catch (Exception)
        {
        }
    }
    protected void gv_FPS_Details_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int idx = gv_FPS_Details.SelectedIndex;
            dt = (DataTable)Session["dt"];
            dt.Rows[idx].Delete();
            gv_FPS_Details.DataSource = dt;
            gv_FPS_Details.DataBind();
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
            if (gv_FPS_Details.Rows.Count > 0)
            {
                Pnl_Fps_View.Visible = true;
            }
            else
            {
                Pnl_Fps_View.Visible = false;
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
            if (ddl_commodity.SelectedItem.Text == "--Select--" || ddl_scheme.SelectedItem.Text == "--Select--")
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
            else
            {
                string qry = "Select Rural_rate  from dbo.SCSC_IssueRate  where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "' and District_code='" + distid + "' and Effective_From<='" + todaydate + "' order by Effective_From desc";
                DObj = new Districts(ComObj);
                DataSet ds = DObj.selectAny(qry);
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
        catch (Exception)
        {
        }

    }
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tx_balQty2.Text = "";
            if (ddl_commodity.SelectedItem.Text != "--Select--" && ddl_scheme.SelectedItem.Text != "--Select--")
            {
                GetRate();
            }
            else if (ddl_scheme.SelectedItem.Text == "--Select--" || ddl_commodity.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
            }
            else
            {
                GetRate();
                get_IssuedQty();
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
    protected void get_IssuedQty()
    {
        try
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
            con.Close();
        }
        catch (Exception)
        {

        }
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
        {

        }
    }
    protected void Lstbx_fps_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddl_scheme.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Scheme...');</script>");
            }
            else if (ddl_commodity.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity...');</script>");
            }
            else if (Lstbx_fps_name.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS...');</script>");
            }
            else
            {
                Lstbx_fps_name.SelectedItem.Attributes.Add("style", "background-color:Green");

                if (Lstbx_fps_name.SelectedIndex == 0)
                {
                    lblFPSNAme.Text = "";
                }
                else
                {
                    lblFPSNAme.Text = Convert.ToString(Lstbx_fps_name.SelectedItem.Text);
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
                    cmdstr = "SELECT Round(sum(convert(decimal(18,5),quantity)),5) as quantity FROM dbo.do_fps where district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and fps_code='" + Lstbx_fps_name.SelectedItem.Value + "' and commodity in(" + comdty + ")";
                }
                else
                {
                    cmdstr = "SELECT Round(sum(convert(decimal(18,5),quantity)),5) as quantity FROM dbo.do_fps where district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and fps_code='" + Lstbx_fps_name.SelectedItem.Value + "' and commodity in(" + comdty + ") and scheme_id=" + ddl_scheme.SelectedItem.Value + "";
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
                string comm = ddl_commodity.SelectedItem.Text;
                string scheme = ddl_scheme.SelectedItem.Text;
                int amonth = int.Parse(ddl_allot_month.SelectedItem.Value);
                int ayear = int.Parse(ddd_allot_year.SelectedItem.Text);
                string fps_code = Lstbx_fps_name.SelectedItem.Value;
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
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotment for Selected Commodity...');</script>");
                }
                if (tx_already_iqty.Text == "")
                {
                    tx_already_iqty.Text = "0";
                }
                if (tx_allot_qty2.Text == "0")
                {
                    tx_balQty2.Text = "0";
                }
                else
                {
                    tx_balQty2.Text = System.Math.Round(CheckNull(tx_allot_qty2.Text) - CheckNull(tx_already_iqty.Text), 5).ToString();
                }

                tx_qty.Focus();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Try Again...');</script>");
        }
    }
    protected void get_bankname()
    {
        try
        {
            string query = "select Bank_ID,Bank_Name from dbo.Bank_Master_New order by Bank_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddl_bank.DataSource = ds.Tables[0];
            ddl_bank.DataTextField = "Bank_Name";
            ddl_bank.DataValueField = "Bank_ID";
            ddl_bank.DataBind();
            ddl_bank.Items.Insert(0, "--Select--");
            con.Close();
        }
        catch (Exception)
        {

        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
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
        Response.Redirect("~/IssueCenter/DeliveryOrder_PDS.aspx");
    }
    protected void ddd_allot_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tx_balQty2.Text = "";
            DataTable dtt = new DataTable();
            gv_FPS_Details.DataSource = dtt;
            gv_FPS_Details.DataBind();
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
            gv_FPS_Details.DataSource = dtt;
            gv_FPS_Details.DataBind();
        }
        catch (Exception)
        { }
    }
    public void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        lblDist.Text = dr1dt["district_name"].ToString();

    }
    public void GET_Table_Rows()
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
    }
    protected void ddlpermit_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_Add_FPS.Enabled = true;
    }
    void GetPermit()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT Permit_order_no  FROM dbo.Dccb_Permit where district_code='" + distid + "'and issueCentre_code='" + sid + "'";
            DataSet ds = mobj.selectAny(qry);
            if (ds.Tables[0].Rows.Count == 0)
            {
                ddlpermit.Items.Clear();
                ListItem lst = new ListItem();
                lst.Text = "Not Indicated";
                lst.Value = "0";
                ddlpermit.Items.Insert(0, "--Select--");
                ddlpermit.Items.Insert(1, lst);
            }
            else
            {
                ddlpermit.Items.Clear();
                ddlpermit.DataSource = ds.Tables[0];
                ddlpermit.DataTextField = "Permit_order_no";
                ddlpermit.DataValueField = "Permit_order_no";
                ddlpermit.DataBind();
                ddlpermit.Items.Insert(0, "--Select--");
                ddlpermit.Items.Insert(1, "Not Indicated");
            }
        }
        catch (Exception)
        {

        }
    }
}
