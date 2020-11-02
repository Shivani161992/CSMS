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
public partial class IssueCenter_DeliveryOrder_FCI : System.Web.UI.Page
{
    Districts DObj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
        chksql chk = null;

    string distid = "";
    string sid = "";
    LARO obj = null;
    protected Common ComObj = null, cmn = null;
    public string cat = "";
    string version = "";
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
        version = Session["hindi"].ToString();
        //sid = Session["issue_id"].ToString();
        save.Enabled = true;
        chk = new chksql();
        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(tx_do_no.Text);
        ctrllist.Add(TextBox1.Text);
        ctrllist.Add(tx_qty.Text);
        ctrllist.Add(tx_dd_no.Text);
        ctrllist.Add(tx_dd_amount.Text);

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



        if (Page.IsPostBack == false)
        {
            tx_dd_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            tx_do_validity.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            tx_do_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");


            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            get_comm();
            get_scheme();
            get_bankname();
            //GetRO();
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;

            if (version == "H")
            {
              
                lbl_lead.Text = Resources.LocalizedText.lbl_lead;
                lblallotyear.Text = Resources.LocalizedText.lblallotyear;
                lblallotmonth.Text = Resources.LocalizedText.lblallotmonth;
                lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                lblScheme.Text = Resources.LocalizedText.lblScheme;
             
                lblratetype.Text = Resources.LocalizedText.lblratetype;
                lbltoissue.Text = Resources.LocalizedText.lbltoissue;
                lbldono.Text = Resources.LocalizedText.lbldono;
                lbldodate.Text = Resources.LocalizedText.lbldodate;
                lbldovalidity.Text = Resources.LocalizedText.lbldovalidity;
               
                lbl_balqty.Text = Resources.LocalizedText.lbl_balqty;

                lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                lblqty.Text = Resources.LocalizedText.lblqty;

                lblRateQuintal.Text = Resources.LocalizedText.lblRateQuintal;
                lblPaymentMode.Text = Resources.LocalizedText.lblPaymentMode;
                lblddchekno.Text = Resources.LocalizedText.lblddchekno;
                lblddchekdate.Text = Resources.LocalizedText.lblddchekdate;
                lblBankName.Text = Resources.LocalizedText.lblBankName;
                lblamount.Text = Resources.LocalizedText.lblamount;
                lbltotamt.Text = Resources.LocalizedText.lbltotamt;
                btn_new.Text = Resources.LocalizedText.btn_new;
                save.Text = Resources.LocalizedText.btnsave;
                btnClose.Text = Resources.LocalizedText.btnclose;
               
            }


        }

        //tx_do_no.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");       
        //tx_do_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        //tx_permit_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_rate_qt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_do_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_do_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_no.Attributes.Add("onchange", "return chksqltxt(this)");

        TextBox1.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        TextBox1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        TextBox1.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_dd_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_no.Attributes.Add("onchange", "return chksqltxt(this)");


        tx_dd_amount.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_dd_amount.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_amount.Attributes.Add("onchange", "return chksqltxt(this)");





        //hlinkpdo.Attributes.Add("onclick", "window.open('print_DeleveryOrder.aspx',null,'left=300, top=90, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        txtcomdty_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        //tx_do_validity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_rate_qt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_amt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();        
        tx_dd_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

        tx_do_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_do_validity.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_dd_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");

      
        tx_do_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_date.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_date.Attributes.Add("onchange", "return chksqltxt(this)");


              
    }
    //void GetRO()
    //{
    //    ddlrono.Items.Insert(0, "--Select--");
    //    int month = int.Parse(DateTime.Today.Month.ToString());
    //    int year = int.Parse(DateTime.Today.Year.ToString());
    //    string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'";//and  Balance_Qty >0";
    //    cmd.Connection = con;
    //    cmd.CommandText = qry;
    //    con.Open();
    //    dr = cmd.ExecuteReader();
    //    while (dr.Read())
    //    {
    //        ddlrono.Items.Add(dr["RO_No"].ToString());



    //    }
    //    dr.Close();
    //    con.Close();

    //}
    protected void save_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;
        //if (CheckNull(txtcomdty_bal.Text) == 0)
        //{
        //    Label1.Visible = true;
        //    Label1.Text = "Sorry You dont't have balance ....";
        //    Label1.ForeColor = System.Drawing.Color.Red;
        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry You dont't have balance ......');</script>");
        //}
        //else 
        if (CheckNull(tx_qty.Text) > 0)
        {
                string dist = distid;
               // string issue_centre_code = sid;
                string issue_type = ddl_issueto.SelectedItem.Value;
                //string comm = ddl_commodity.SelectedItem.Value;
                //string scheme = ddl_scheme.SelectedItem.Value;
                string issue_name = "";
                //string fps_code = "";
                string month = ddl_allot_month.SelectedItem.Value;
                //int do_valid = Convert.ToInt32(tx_do_validity.Text);
                int permit_valid = 0;
                //Double qty = Convert.ToDouble(tx_qty.Text);
                string do_no = tx_do_no.Text;                
                string pmode = ddl_pmode.SelectedItem.Value;
                string dd_no = tx_dd_no.Text;
                string temp = "yyy";
                string do_date = getDate_MDY(tx_do_date.Text);               
                string dd_date = getDate_MDY(tx_dd_date.Text);

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string opid = Session["OperatorIDDM"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "N";
                string do_validity = get_days(DateTime.Parse(getDate_MDY(tx_do_date.Text)), DateTime.Parse(getDate_MDY(tx_do_validity.Text)));
                int do_valid = CheckNullInt(do_validity);
                cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where delivery_order_no='" + do_no + "'  and district_code='" + dist + "'";
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

                    if (issue_type == "O")
                    {
                        issue_name = TextBox1.Text.Trim();
                        //fps_code = ddl_fps_name.SelectedItem.Value;
                    }
                    else
                    {
                        issue_name = TextBox1.Text.Trim();

                    }


                    //if (tx_permit_validity.Text == "")
                    //{
                    //    permit_valid = 0;
                    //}
                    //else
                    //{
                    //    permit_valid = Convert.ToInt32(tx_permit_validity.Text);
                    //}
                    //decimal allot_qty = 0;
                    //decimal issue_qty = 0;
                    //string aqty = "";
                    //string iqty = "";
                    //decimal lift_qty = 0;
                    //string temp1 = "NNN";
                    //string str2 = "select allot_qty,issue_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + month.ToString() + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                    //cmd.Connection = con;
                    //cmd.CommandText = str2;
                    //con.Open();
                    //dr = cmd.ExecuteReader();
                    //while (dr.Read())
                    //{
                    //    aqty = dr["allot_qty"].ToString();
                    //    iqty = dr["issue_qty"].ToString();
                    //    temp1 = "YYY";
                    //}
                    //dr.Close();
                    //con.Close();
                    //if (aqty == "")
                    //{
                    //    aqty = "0";
                    //}
                    //if (iqty == "")
                    //{
                    //    iqty = "0";
                    //}
                    //allot_qty = decimal.Parse(aqty);
                    //issue_qty = decimal.Parse(iqty);
                    string ro_no = "";
                    string str1 = "INSERT INTO dbo.delivery_order_mpscsc(State_Id,delivery_order_no,district_code,issueCentre_code,permit_no,issue_type,issue_name,allotment_month,allotment_year,do_validity,permit_validity,do_date,permit_date,created_date,updated_date,payment_mode,dd_no,dd_date,quantity,amount,tot_amount,bank_id,commodity_id,scheme_id,rate_per_qtls,status,IP,OperatorID,NoTransaction) VALUES('" + state + "','" + do_no + "','" + dist + "','','','" + issue_type + "','" + issue_name + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + "," + do_valid.ToString() + "," + 0 + ",'" + do_date + "',''," + "getdate(),getdate(),'" + pmode + "','" + dd_no + "','" + dd_date + "'," + tx_qty.Text + "," + tx_dd_amount.Text + "," + tx_tot_amt.Text + ",'" + ddl_bank.SelectedItem.Value + "'," + ddl_commodity.SelectedValue + ",'" + ddl_scheme.SelectedValue + "'," + tx_rate_qt.Text + ",'N','" + ip + "','" + opid + "','" + notrans + "')";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //decimal doqty = CheckNull(tx_qty.Text);

                        //string mqryTOqty = "Update dbo.TO_Allot_Lift set Cumulative_Qty=Round(convert(decimal(18,5),Cumulative_Qty),5)+" + doqty + ",Pending_Qty=Round(convert(decimal(18,5),Pending_Qty),5)-" + doqty + " where RO_NO='" + ro_no + "' and Distt_Id='" + distid + "'";
                        //cmd.CommandText = mqryTOqty;
                        //con.Open ();
                        //cmd.ExecuteNonQuery();
                        //con.Close();



                        //issue_qty = issue_qty + decimal.Parse(tx_qty.Text);

                        //if (temp1 == "NNN")
                        //{
                        //    str1 = "insert into dbo.sum_trans_do (district_code,issueCentre_code,allot_qty,issue_qty,lift_qty,trans_month,trans_year,created_date) values('" + dist + "','" + issue_centre_code + "'," + allot_qty.ToString() + "," + issue_qty.ToString() + "," + lift_qty.ToString() + "," + month + "," + ddd_allot_year.SelectedItem.Text + ",'" + do_date + "')";
                        //    cmd.CommandText = str1;
                        //    cmd.ExecuteNonQuery();
                        //}
                        //else
                        //{
                        //    str1 = "update dbo.sum_trans_do set allot_qty=" + allot_qty.ToString() + ",issue_qty=" + issue_qty.ToString() + "  where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + month + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                        //    cmd.CommandText = str1;
                        //    cmd.ExecuteNonQuery();
                        //}
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                        Label1.Text = "Data Saved Successfully ...";
                        //panelDO.Enabled = false;
                        save.Enabled = false;
                        hlinkpdo.Visible = true;
                        hlinkpdo.Enabled = true;
                        Session["doforprint"] = do_no;
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

                    //Label1.ForeColor = System.Drawing.Color.Blue;

                    //tx_dd_amount.Text = "";
                    ////tx_dd_date.Text = "";
                    //tx_dd_no.Text = "";
                    ///// tx_do_date.Text = "";
                    //tx_do_no.Text = "";
                    //tx_do_validity.Text = "";
                    ////tx_issue_name.Text = "";
                    ////tx_permit_date.Text = "";
                    //tx_permit_no.Text = "";
                    //tx_permit_validity.Text = "";
                    //tx_qty.Text = "";
                    //Page.Form.Disabled = true;
                    //save.Enabled = false;
                    //hlinkpdo.Visible = true;

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
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('DO Quantity can not be greater than Balance Quantity ...');</script>");
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "DO Quantity should be greater than Zero ...";
        }

    }
    protected void get_comm()
    {
        cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where status='Y' order by Commodity_Name Desc";
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
    protected void get_scheme()
    {
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME where status='Y'  order by Scheme_Id ";
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
    protected void ddl_pmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;
        ddl_bank.Enabled = true ;
        if (ddl_pmode.SelectedItem.Value  == "D")
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
        DObj = new Districts(ComObj);
        //if (ddl_commodity.SelectedItem.Text == "Select" || ddl_scheme.SelectedItem.Text == "Select")
        //{
        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
        //}
        //else 
        string todaydate = DateTime.Today.ToString("MM/dd/yyyy");
        if (ddl_rate_type.SelectedItem.Value == "U")
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
        else if (ddl_rate_type.SelectedItem.Value == "R")
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
        else if (ddl_rate_type.SelectedItem.Value == "C")
        {
            string qry = "Select Consumar_Rate from dbo.SCSC_IssueRate  where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "' and District_code='" + distid + "' and Effective_From<='" + todaydate + "' order by Effective_From desc";
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
                tx_rate_qt.Text = dr["Consumar_Rate"].ToString();
                tx_rate_qt.ReadOnly = true;
            }
        }
    }
    void GetBalanceQty()
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
        con.Close();
        dr.Close();
        int ind;
        ind = comdty.LastIndexOf(",");
        comdty = comdty.Remove(ind, 1);

        string commid = ddl_commodity.SelectedValue; ;
        string schemeid = ddl_scheme.SelectedValue;
        int month = int.Parse(ddl_allot_month.SelectedValue);
        int year = int.Parse(ddd_allot_year.SelectedValue);


        string qryGB = "Select Round(convert(decimal,Sum(quantity)),5) as RO_qty from dbo.delivery_order_mpscsc group by district_code,commodity_id,allotment_month,allotment_year,scheme_id,issue_type having (commodity_id in(" + comdty + ")) and (district_code='" + distid + "') and (allotment_month=" + month + ")and (allotment_year=" + year + ") and (scheme_id='" + schemeid + "') and (issue_type='FCI')";
        DObj = new Districts(ComObj);
        DataSet dsGB = DObj.selectAny(qryGB);
        if (dsGB == null)
        {

        }
        else
        {
            if (dsGB.Tables[0].Rows.Count == 0)
            {
                decimal mbqty = CheckNull(txtcomdty_bal.Text);
                decimal mdoqty = CheckNull(txtrobalqty.Text);
               
                txtbalqty.Text =  (mbqty-mdoqty).ToString();
                txtbalqty.ReadOnly = true;
                txtbalqty.BackColor = System.Drawing.Color.Wheat;

            }
            else
            {
                DataRow drGB = dsGB.Tables[0].Rows[0];
                string mbalance = System.Math.Round(CheckNull(drGB["RO_qty"].ToString()), 5).ToString();
                if (mbalance == "0")
                {
                    txtbalqty.Text = txtcomdty_bal.Text;
                }
                else
                {
                    decimal mrobal = CheckNull(drGB["RO_qty"].ToString());
                    decimal mroalot = CheckNull(txtcomdty_bal.Text);
                    decimal mdoqty = CheckNull(txtrobalqty.Text);
                    decimal macbal = mroalot - mrobal-mdoqty ;

                    //txtbalqty.Text = System.Math.Round(CheckNull(drGB["RO_qty"].ToString()), 5).ToString();
                    txtbalqty.Text = macbal.ToString();
                    txtbalqty.ReadOnly = true;
                    txtbalqty.BackColor = System.Drawing.Color.Wheat;
                }


            }
        }

    }
    void GetRoBalance()
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
        con.Close();
        dr.Close();
        int ind;
        ind = comdty.LastIndexOf(",");
        comdty = comdty.Remove(ind, 1);

        string commid = ddl_commodity.SelectedValue; ;
        string schemeid = ddl_scheme.SelectedValue;
        int month = int.Parse(ddl_allot_month.SelectedValue);
        int year = int.Parse(ddd_allot_year.SelectedValue);


        string qryGB = "Select Round(convert(decimal,Sum(RO_qty)),5) as RO_qty from dbo.RO_of_FCI group by Distt_Id,Commodity,Allot_month,Allot_year,scheme having (Commodity in(" + comdty + ")) and (Distt_Id='" + distid + "') and (Allot_month=" + month + ")and (Allot_year=" + year + ") and (Scheme='" + schemeid + "')";
        DObj = new Districts(ComObj);
        DataSet dsGB = DObj.selectAny(qryGB);
        if (dsGB == null)
        {

        }
        else
        {
            if (dsGB.Tables[0].Rows.Count == 0)
            {
                decimal mbqty = CheckNull(txtcomdty_bal.Text);
                txtrobalqty.Text ="0";
                txtrobalqty.ReadOnly = true;
                txtrobalqty.BackColor = System.Drawing.Color.Wheat;

            }
            else
            {
                DataRow drGB = dsGB.Tables[0].Rows[0];
                               
                    decimal mrobal = CheckNull(drGB["RO_qty"].ToString());
                    txtrobalqty.Text = mrobal.ToString ();
                   

                  


            }
        }
    }
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select" )
        //{
        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
        //}
        //else
        //{
        //    tx_do_no.Focus();
        //    GetRate();
        //    get_ComdtyBal();
        //}
         
        string sctype = ddl_scheme.SelectedItem.Text;
        int malot = int.Parse(ddl_allot_month.SelectedValue.ToString());
        int malotyear = int.Parse(ddd_allot_year.SelectedValue.ToString());
        string commodty = ddl_commodity.SelectedValue;
        string mscheme = ddl_scheme.SelectedValue;

        if (sctype == "APL" || sctype == "BPL" || sctype == "AAY")
        {
            
            txtcomdty_bal.Text = "";

            if (ddl_scheme.SelectedItem.Text == "Non Scheme" || ddl_scheme.SelectedItem.Text == "--Select--" || ddl_commodity.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
            }
            else
            {
               
                string comm = ddl_commodity.SelectedItem.Text;
                string scheme = ddl_scheme.SelectedItem.Text;
                int amonth = int.Parse(ddl_allot_month.SelectedItem.Value);
                int ayear = int.Parse(ddd_allot_year.SelectedItem.Value);

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
                    cmd.CommandText = "select  " + commodity + "   from pds.state_alloc where district_code='" + distid + "' and month='" + malot + "' and Year='" + malotyear + "'";
                    cmd.Connection = con_opdms;
                    con_opdms.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[commodity].ToString() == "")
                        {
                            txtcomdty_bal.Text = "0";
                        }
                        else
                        {
                            txtcomdty_bal.Text = System.Math.Round(decimal.Parse(dr[commodity].ToString()), 2).ToString();
                        }
                    }
                    dr.Close();
                    con_opdms.Close();
                }
                if (txtcomdty_bal.Text == "")
                {
                    txtcomdty_bal.Text = "0";
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotment for Selected Commodity...');</script>");
                }
                GetRate();
                GetRoBalance();
                GetBalanceQty();
            }
            txtcomdty_bal.ReadOnly = true;
            txtcomdty_bal.BackColor = System.Drawing.Color.Wheat;

        }
        else
        {
           
            DObj = new Districts(ComObj);
            string malloc = "select alloc_qty from dbo.dist_mpscsc_alloc where district_code='" + distid  + "' and alloc_month=" + malot + " and alloc_year=" + malotyear + " and commodity_id='" + commodty + "' and scheme_id='" + mscheme + "'";
            DataSet dsGB = DObj.selectAny(malloc);
            if (dsGB == null)
            {
            }
            else
            {

                if (dsGB.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotment for Selected Commodity...');</script>");
                    decimal mbqty = CheckNull(txtcomdty_bal.Text) - CheckNull(tx_qty.Text);
                    txtbalqty.Text = mbqty.ToString();
                    txtcomdty_bal.Text = "0";
                    txtbalqty.ReadOnly = true;
                    txtbalqty.BackColor = System.Drawing.Color.Wheat;
                    txtcomdty_bal.ReadOnly = true;
                    txtcomdty_bal.BackColor = System.Drawing.Color.Wheat;

                }
                else
                {
                    DataRow drGB = dsGB.Tables[0].Rows[0];
                    txtcomdty_bal.Text = System.Math.Round(CheckNull(drGB["alloc_qty"].ToString()), 5).ToString();
                    txtcomdty_bal.ReadOnly = true;
                    txtcomdty_bal.BackColor = System.Drawing.Color.Wheat;

                }
            }
            GetBalanceQty();
            GetRate();
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
    protected void get_bankname()
    {
        ddl_bank.Items.Clear();
        cmd.CommandText = "select Bank_ID,Bank_Name from dbo.Bank_Master where District_Code='" + distid + "'";
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void tx_qty_TextChanged(object sender, EventArgs e)
    {
        if (tx_rate_qt.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter Rate per Quintal...');</script>");
        }
        else if (CheckNull(tx_qty.Text) > 0)
        {            
                tx_tot_amt.Text = (CheckNull(tx_rate_qt.Text) * CheckNull(tx_qty.Text)).ToString();            
        }
        else
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('DO Quantity can not be greater than Balance stock of commodity or Zero ...');</script>");
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "DO Quantity can not be greater than Balance stock of commodity or Zero ...";
            tx_qty.Text = "";
            tx_qty.Focus();
        }        
    }    
    protected void get_ComdtyBal()
    {
        txtcomdty_bal.Text = "";
        string cmdstr = "";
        if (ddl_commodity.SelectedItem.Text.ToLower().Contains("sugar"))
        {
            cmdstr = "select sum(quantity) as qty  from delivery_order_mpscsc  group by district_code,commodity_id,scheme_id,allotment_month,allotment_year,issue_type having (commodity_id in(5,6,7,22))  and (district_code='20') and (allotment_month=2)  and (allotment_year=2010) and (issue_type='FCI')";
        }
        else
        {
            cmdstr = "select sum(quantity) as qty  from delivery_order_mpscsc  group by district_code,commodity_id,scheme_id,allotment_month,allotment_year,issue_type having (commodity_id in(5,6,7,22)) and (scheme_id='3') and (district_code='20') and (allotment_month=2)  and (allotment_year=2010) and (issue_type='FCI')";
        }        
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
    protected void ddl_rate_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRate();
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/DeliveryOrder_FCI.aspx");
    }
    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtbalqty_TextChanged(object sender, EventArgs e)
    {

    }
   
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    //void GetData()
    //{
    //    if (ddlrono.SelectedItem.Text != "--Select--")
    //    {
    //        obj = new LARO(ComObj);
    //        string qryall = "SELECT RO_of_FCI.Commodity , RO_of_FCI.Distt_Id,RO_of_FCI.RO_Validity, RO_of_FCI.RO_No, RO_of_FCI.RO_date, round(convert(decimal(18,5),RO_of_FCI.RO_qty),5) as RO_qty ,round(convert(decimal(18,5),TO_Allot_Lift.Pending_Qty),5) as Quantity ,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date,round(convert(decimal(18,5),RO_of_FCI.Balance_Qty),5) as Balance_Qty,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,dbo.tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name  From dbo.RO_of_FCI Left JOIN dbo.tbl_MetaData_STORAGE_COMMODITY  ON RO_of_FCI.Commodity = dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=dbo.tbl_MetaData_SCHEME.Scheme_id left join TO_Allot_Lift on RO_of_FCI.RO_No=TO_Allot_Lift.RO_No  where RO_of_FCI.RO_No='" + ddlrono.SelectedItem + "' and RO_of_FCI.Distt_Id='" + distid + "'";
    //        DataSet ds = obj.selectAny(qryall);
    //        DataRow dr = ds.Tables[0].Rows[0];

    //        string rdate = dr["RO_date"].ToString();
    //        string rodate = getdate(rdate);
    //        txtrodate.Text = rodate;
    //        txtrodate.ReadOnly = true;
    //        txtrodate.BackColor = System.Drawing.Color.Wheat;

    //        //roqty = dr["RO_qty"].ToString();
    //        txtroqty.Text = System.Math.Round(CheckNull(dr["RO_qty"].ToString()), 5).ToString();

    //        txtroqty.ReadOnly = true;


    //        txtcomdty.Text = dr["Commodity_Name"].ToString();
    //        txtcomdty.ReadOnly = true;
    //        txtcomdty.BackColor = System.Drawing.Color.Wheat;

    //        txtscheme.Text = dr["Scheme_Name"].ToString();
    //        ddl_scheme.SelectedValue = dr["Scheme"].ToString();
    //        ddl_commodity.SelectedValue = dr["Commodity"].ToString();
    //        txtscheme.ReadOnly = true;
    //        txtscheme.BackColor = System.Drawing.Color.Wheat;
    //        decimal toqty = CheckNull(dr["Quantity"].ToString());
    //        //decimal mroqty = CheckNull(dr["RO_qty"].ToString());
    //        //decimal balance = toqty.ToString();
    //        txtbalance.Text = toqty.ToString();
    //        txtbalqty.ReadOnly = true;
    //        txtbalqty.BackColor = System.Drawing.Color.Wheat;
    //        lblmonth.Text = dr["Allot_month"].ToString();
    //        lblyear.Text = dr["Allot_year"].ToString();
    //        if (toqty == 0)
    //        {
    //            Label1.Visible = true;
    //            Label1.Text = "Message : You have Insufficient Balance To Issue DO,Please Update Transport Order Quantity to Issue DO";
    //            tx_do_no.ReadOnly = true;
    //        }
    //        else
    //        {
    //            Label1.Visible = false ;
    //            tx_do_no.ReadOnly = false;
    //        }

    //    }
    //    else
    //    {
    //        txtrodate.Text = "";
    //        txtroqty.Text = "";
    //        txtcomdty.Text = "";
    //        txtbalqty.Text = "";
    //    }

    //}
}