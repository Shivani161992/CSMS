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
    string distid = "";
    string sid = "";
    protected Common ComObj = null, cmn = null;
    public string cat = "";
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
        save.Enabled = true;
        if (Page.IsPostBack == false)
        {

            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            get_comm();
            get_scheme();
            get_bankname();
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
        }

        //tx_do_no.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");       
        //tx_do_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        //tx_permit_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_rate_qt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        hlinkpdo.Attributes.Add("onclick", "window.open('print_DeleveryOrder.aspx',null,'left=300, top=90, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        txtcomdty_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        //tx_do_validity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_rate_qt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_amt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();        
        tx_dd_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();    
        
    }
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
                string issue_centre_code = sid;
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
                string do_validity = get_days(DateTime.Parse(tx_do_date.Text), DateTime.Parse(tx_do_validity.Text));
                int do_valid = CheckNullInt(do_validity);
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
                    string str1 = "INSERT INTO dbo.delivery_order_mpscsc(delivery_order_no,district_code,issueCentre_code,permit_no,issue_type,issue_name,allotment_month,allotment_year,do_validity,permit_validity,do_date,permit_date,created_date,updated_date,payment_mode,dd_no,dd_date,quantity,amount,tot_amount,bank_id,commodity_id,scheme_id,rate_per_qtls,status) VALUES('" + do_no + "','" + dist + "','" + issue_centre_code + "','NA','" + issue_type + "','" + issue_name + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + "," + do_valid.ToString() + "," + permit_valid.ToString() + ",'" + do_date + "',getdate(),getdate(),'','" + pmode + "','" + dd_no + "','" + dd_date + "'," + tx_qty.Text + "," + tx_dd_amount.Text + "," + tx_tot_amt.Text + ",'" + ddl_bank.SelectedItem.Value + "'," + ddl_commodity.SelectedItem.Value + ",'" + ddl_scheme.SelectedItem.Value + "'," + tx_rate_qt.Text + ",'N')";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
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
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void GetRate()
    {
        DObj = new Districts(ComObj);
        //string getcat = "select Block_cat from pds.block_master where District_code ='" + distid + "' and block_code='" + ddl_block.SelectedValue +"'";
        //DataSet dscat = DObj.selectAny(getcat);
        //if (dscat.Tables[0].Rows.Count == 0)
        //{

        //}
        //else
        //{
        //    DataRow drcat = dscat.Tables[0].Rows[0];
        //    cat = drcat["Block_cat"].ToString();

        //}
        if (ddl_rate_type.SelectedItem.Value == "U")
        {
            string qry = "Select Uraban_rate  from dbo.SCSC_IssueRate where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "'and District_code='" + distid + "'";
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
            string qry = "Select Rural_rate  from dbo.SCSC_IssueRate  where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "'";
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

        if (ddl_rate_type.SelectedItem.Value == "C")
        {
            tx_rate_qt.ReadOnly = false;
            tx_rate_qt.Focus();
            tx_rate_qt.BackColor = System.Drawing.Color.White;
            tx_rate_qt.Text = "0";
        }

    }
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select" )
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
        }
        else
        {
            tx_do_no.Focus();
            GetRate();
            get_ComdtyBal();
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
        cmd.CommandText = "select Bank_ID,Bank_Name from dbo.Bank_Master where District_Code='" + distid + "' and issueCenter_code='" + sid + "'";
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
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
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
            cmdstr = "SELECT round(Sum(Current_Balance),5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'";
        }
        else
        {
            cmdstr = "SELECT round(Sum(Current_Balance),5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "'";
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
        Response.Redirect("~/IssueCenter/DeliveryOrder_FCI.aspx");
    }
    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}