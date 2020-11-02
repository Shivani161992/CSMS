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
    Districts DObj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string sid = "";
    chksql chk = null;

    DataTable dt = new DataTable();
    protected Common ComObj = null, cmn = null;
    public string cat = "";
    public string version = "";
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
        version = Session["hindi"].ToString();
        //sid = Session["issue_id"].ToString();
        save.Enabled = true;
        if (Page.IsPostBack == false)
        {

            tx_dd_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            tx_do_validity.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            tx_do_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
            tx_permit_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

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
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today .Year.ToString())-1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            get_do_no();
            get_comm();          
            get_block();
            get_scheme();
            get_bankname();
            get_lead();
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

            if (version == "H")
            {

                lbl_lead.Text = Resources.LocalizedText.lbl_lead;
                lblallotyear.Text = Resources.LocalizedText.lblallotyear;
                lblallotmonth.Text = Resources.LocalizedText.lblallotmonth;
                lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                lblScheme.Text = Resources.LocalizedText.lblScheme;

               
                lbltoissue.Text = Resources.LocalizedText.lbltoissue;
                lbldono.Text = Resources.LocalizedText.lbldono;
                lbldodate.Text = Resources.LocalizedText.lbldodate;
                lbldovalidity.Text = Resources.LocalizedText.lbldovalidity;

                lbl_balqty.Text = Resources.LocalizedText.lbl_balqty;

                lblQuantity.Text = Resources.LocalizedText.lblQuantity;
               
                lblRateQuintal.Text = Resources.LocalizedText.lblRateQuintal;
                lblPaymentMode.Text = Resources.LocalizedText.lblPaymentMode;
                lblddchekno.Text = Resources.LocalizedText.lblddchekno;
               
                lblBankName.Text = Resources.LocalizedText.lblBankName;
                lblamount.Text = Resources.LocalizedText.lblamount;
                lbltotamt.Text = Resources.LocalizedText.lbltotamt;
                btn_new.Text = Resources.LocalizedText.btn_new;
                save.Text = Resources.LocalizedText.btnsave;
                lbl_issueqty1.Text = Resources.LocalizedText.lbl_issueqty;
                lblddchekdate.Text = Resources.LocalizedText.lblddchekdate;
                Close.Text = Resources.LocalizedText.btnclose;
            }



        }        
        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(this);"); 
        //tx_permit_no.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");              
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");        
        hlinkpdo.Attributes.Add("onclick", "window.open('print_DeleveryOrder.aspx',null,'left=300, top=90, height=700, width= 500, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

        tx_tot_qty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_tot_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_tot_qty.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_issueto.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_issueto.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_issueto.Attributes.Add("onchange", "return chksqltxt(this)");
        tx_lead.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_lead.Attributes.Add("onchange", "return chksqltxt(this)");
        tx_dd_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_dd_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_no.Attributes.Add("onchange", "return chksqltxt(this)");
        tx_do_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_do_validity.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_dd_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");

        tx_dd_amount.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_dd_amount.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_amount.Attributes.Add("onchange", "return chksqltxt(this)");


        tx_do_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_date.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_dd_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_dd_date.Attributes.Add("onchange", "return chksqltxt(this)");



        tx_rate_qt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_permit_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();      
        tx_tot_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");  
        tx_tot_amt .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();       
        tx_dd_no .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balQty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_already_iqty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

        chk = new chksql();
        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(tx_tot_qty.Text);
        ctrllist.Add(tx_issueto.Text);
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


    }
    protected void save_Click(object sender, EventArgs e)
    {
        decimal iqty = CheckNull(tx_issue_balqty.Text);
        decimal dqty = CheckNull(tx_tot_qty.Text);
        if (dqty < iqty)
        {
            Label1.Visible = true;
            Label1.Text = "DO Quantity Should not be less than Issued Quantity....";
        }
        else
        {

            Label1.Text = "";
            Label1.ForeColor = System.Drawing.Color.Blue;
            if (CheckNull(tx_tot_qty.Text) <= 0)
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter quantity...');</script>");
            }
            else
            {
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
                if (issutype == "O")
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
                string str1 = "update dbo.delivery_order_mpscsc  set issue_name='" + tx_lead.Text + "',tot_amount='" + tx_tot_amt.Text + "',payment_mode='" + ddl_pmode.SelectedItem.Value + "',quantity='" + tx_tot_qty.Text + "',do_validity=" + do_valid + ",updated_date=getdate(),dd_no='" + tx_dd_no.Text + "',amount=" + tx_dd_amount.Text + ",do_date='" + getDate_MDY(tx_do_date.Text) + "',dd_date='" + getDate_MDY(tx_dd_date.Text) + "',bank_id='" + ddl_bank.SelectedItem.Value + "' where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                cmd.CommandText = str1;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //decimal udoqty = CheckNull(lbldoqty.Text) - CheckNull(tx_tot_qty.Text);
                    //string mqryTOqty = "Update dbo.TO_Allot_Lift set Cumulative_Qty=Round(convert(decimal(18,5),Cumulative_Qty),5)-(" + udoqty + "),Pending_Qty=Round(convert(decimal(18,5),Pending_Qty),5)+(" + udoqty + ") where RO_NO='" + txtrono.Text + "' and Distt_Id='" + distid + "'";
                    //cmd.CommandText = mqryTOqty;
                    //con.Open();
                    //cmd.ExecuteNonQuery();
                    //con.Close();


                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Updated Successfully ...');</script>");
                    Label1.Text = "Data Updated Successfully ...";
                    save.Enabled = false;

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
        }
       
       protected String  getDate_MDY(string inDate)
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
   
    protected void get_do_no()
    {
        Label1.Text = "";      
        tx_permit_no.Text = "";
        tx_issueto.Text = "";
        tx_tot_qty.Text = "";
       
        tx_dd_no.Text = "";
        tx_dd_amount.Text = "";        
        string dist = distid;
        string issue_centre_code = sid;
        ddl_do_no.Items.Clear();
        cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where district_code='" + dist + "'and status='N' and issue_type='FCI'  and delivery_order_no not like '%NoDO%' order by  created_date desc";
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
    void Balance()
    {
        string issueqty = "";      
        string do_qty = "";
        cmd.CommandText = "SELECT delivery_order_mpscsc.permit_no,delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,round(convert(decimal(18,5),delivery_order_mpscsc.quantity),5) as quantity,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity,delivery_order_mpscsc.issue_type,round(SUM(convert(decimal(18,5),issue_against_do.qty_issue)),5) AS issueqty FROM dbo.delivery_order_mpscsc LEFT JOIN dbo.issue_against_do ON delivery_order_mpscsc.delivery_order_no = issue_against_do.delivery_order_no AND delivery_order_mpscsc.district_code = issue_against_do.district_code   GROUP BY delivery_order_mpscsc.permit_no,delivery_order_mpscsc.delivery_order_no,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.district_code, delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity, delivery_order_mpscsc.issue_type,delivery_order_mpscsc.quantity,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id  having delivery_order_mpscsc.delivery_order_no='" + ddl_do_no.SelectedValue  + "' and delivery_order_mpscsc.district_code='" + distid  + "'";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
           
            do_qty = dr["quantity"].ToString();
            issueqty = dr["issueqty"].ToString();
            decimal qty =CheckNull(issueqty);
            tx_issue_balqty.Text = System.Math.Round(qty, 5).ToString();


        }

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
            Response.Redirect("~/District/Edit_delivery.aspx");
        }
        else
        {
            tx_permit_no.Text = "";
            tx_issueto.Text = "";
            tx_tot_qty.Text = "";
            // tx_tot_amt.Text = "";
           
           
            tx_dd_no.Text = "";
            tx_dd_amount.Text = "";
            Session["dono"] = ddl_do_no.SelectedItem.Text;
            Response.Redirect("~/District/Edit_delivery.aspx");
            
        }
    }

    public string getdateg(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd-MM-yyyy");
    }
    protected void get_do_data()
    {        
        save.Enabled = true;
        ddl_pmode.Enabled = true ;
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
        cmd.CommandText = "SELECT *,DATEADD(day,do_validity,do_date) as validdate FROM dbo.delivery_order_mpscsc where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issue_type='FCI'";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            dovalidity = dr["validdate"].ToString();
            //tx_permit_no.Text = dr["permit_no"].ToString();
            issue_to = dr["issue_type"].ToString();
            tx_issueto.Text = dr["issue_type"].ToString();
            tx_tot_qty .Text  = dr["quantity"].ToString();
            lbldoqty.Text = dr["quantity"].ToString();
            tx_tot_amt.Text = dr["tot_amount"].ToString();
            lead_code = dr["issue_name"].ToString();
            tx_lead.Text = dr["issue_name"].ToString();  
            tx_dd_no.Text = dr["dd_no"].ToString();
            tx_dd_amount.Text = dr["amount"].ToString();
            do_date = dr["do_date"].ToString();
            //permit_date =dr["permit_date"].ToString();
            dd_date = dr["dd_date"].ToString();
            ddl_bank.SelectedValue = dr["bank_id"].ToString();
            ddl_allot_month.SelectedValue = dr["allotment_month"].ToString();
            ddd_allot_year.SelectedValue   = dr["allotment_year"].ToString();
            ddl_commodity.SelectedValue = dr["commodity_id"].ToString();
            ddl_scheme.SelectedValue = dr["scheme_id"].ToString();
            templift = dr["status"].ToString();
            tx_rate_qt.Text = dr["rate_per_qtls"].ToString();
            txtrono.Text = dr["permit_no"].ToString();
            ddl_pmode.SelectedValue = dr["payment_mode"].ToString();
        }

        dr.Close();
        string isuueqty = "";
        cmd.CommandText = "SELECT * FROM dbo.issue_against_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and delivery_order_no not like '%NoDO%'";
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
            tx_tot_qty.Text = System.Math.Round(decimal.Parse(tx_tot_qty.Text), 5).ToString();
        }
        if (tx_tot_amt.Text != "")
        {
            tx_tot_amt.Text = System.Math.Round(decimal.Parse(tx_tot_amt.Text), 2).ToString();
        }
        if (tx_dd_amount.Text != "")
        {
            tx_dd_amount.Text = System.Math.Round(decimal.Parse(tx_dd_amount.Text), 2).ToString();
        }
        tx_do_date.Text = getdateg(do_date);
        //tx_permit_date.SelectedDate = Convert.ToDateTime(permit_date);       
        tx_do_validity.Text = getdateg(dovalidity);
        tx_dd_date.Text = getdateg(dd_date);
        tx_do_date.Enabled =true;
        //tx_permit_date.Enabled = true;
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
        tx_dd_no.Enabled = true ;
        ddl_bank.Enabled = true ;
        RequiredFieldValidator1.Enabled = true;
        RequiredFieldValidator3.Enabled = true;
        ddl_lead.SelectedIndex = 0;
        ddl_block.SelectedIndex = 0;
        if (issue_to == "L" || issue_to == "LF")
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

        if (issue_to == "F")
        {
            tx_issueto.Text = "FPS";
            ddl_lead.Visible = false;
            tx_lead.Visible = false;
            tx_lead.Text = "";
        }
        if (issue_to == "O")
        {
            ddl_lead.Visible = false;
            tx_lead.Visible = true;
            tx_issueto.Text = "Others";
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
            tx_issueto.ReadOnly = true;
            //tx_tot_qty.ReadOnly = true;
            tx_tot_amt.ReadOnly = true;
            tx_dd_no.ReadOnly = true;            
            tx_dd_amount.ReadOnly = true;
            ddl_bank.Enabled = false;
           
        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("select do_fps.block_code,fps_master.fps_code,fps_master.fps_name,do_fps.commodity as commodity_id,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name,do_fps.scheme_id,tbl_MetaData_SCHEME .Scheme_Name,do_fps.rate_per_qtls as rate_qtls,do_fps.quantity as qty,do_fps.quantity*do_fps.rate_per_qtls as amt,do_fps.allot_qty from dbo.do_fps LEFT JOIN pds.fps_master ON do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id where do_fps.delivery_order_no='" + do_no + "' and do_fps.district_code='" + dist + "' and do_fps.issueCentre_code='" + sid + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.status='N'", con);
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
            }
            else
            {
                tx_tot_qty.ReadOnly = false ;
                tx_tot_qty.AutoPostBack = true ;
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
        Session["dono"] = null;
        Balance();
    }
    protected void get_fps()
    {
        string dist = distid;
        ddl_fps_name.Items.Clear();
        cmd.CommandText = "SELECT pds.fps_master.block_code,pds.fps_master.fps_name, pds.fps_master.fps_code FROM dbo.Lead_soc_fps Left JOIN pds.fps_master ON Lead_soc_fps.District_code = pds.fps_master.district_code AND Lead_soc_fps.fps_code = pds.fps_master.fps_code where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "' order by Lead_soc_fps.fps_code";
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
        Response.Redirect("~/District/Dist_Welcome.aspx");
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
    protected void Button1_Click(object sender, EventArgs e)
    {
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
                decimal bal_qty = decimal.Parse(tx_balQty2.Text);
                if (decimal.Parse(tx_qty.Text) > bal_qty || decimal.Parse(tx_qty.Text) <= 0)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Alloted...');</script>");
                    tx_qty.Text = "";
                    tx_qty.Focus();
                }
                else
                {
                    tx_tot_qty.Text = System.Math.Round((decimal.Parse(tx_tot_qty.Text) + decimal.Parse(tx_qty.Text)), 5).ToString();
                    tx_tot_amt.Text = System.Math.Round((decimal.Parse(tx_tot_amt.Text) + (decimal.Parse(tx_qty.Text) * decimal.Parse(tx_rate_qt.Text))), 2).ToString();
                    dt = (DataTable)Session["dt"];
                    dt.Rows.Add(ddl_block.SelectedItem.Value, ddl_fps_name.SelectedItem.Value, ddl_fps_name.SelectedItem.Text, ddl_commodity.SelectedItem.Value, ddl_commodity.SelectedItem.Text, ddl_scheme.SelectedItem.Value, ddl_scheme.SelectedItem.Text, tx_rate_qt.Text, tx_qty.Text, decimal.Parse(tx_qty.Text) * decimal.Parse(tx_rate_qt.Text), tx_allot_qty2.Text);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    Session["dt"] = dt;
                    //tx_already_iqty.Text = System.Math.Round(decimal.Parse(tx_already_iqty.Text) + decimal.Parse(tx_qty.Text), 2).ToString();
                    //tx_balQty.Text = System.Math.Round(decimal.Parse(tx_balQty.Text) - decimal.Parse(tx_qty.Text), 2).ToString();
                    //tx_allot_qty.Text =System.Math .Round ( (decimal.Parse(tx_allot_qty.Text) - decimal.Parse(tx_qty.Text)),2).ToString();
                    //tx_already_iqty.Text = tx_qty.Text;
                    tx_qty.Text = "";
                    //tx_rate_qt.Text = "";
                }
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal qty = decimal.Parse(GridView1.SelectedRow.Cells[5].Text);
        decimal amt = decimal.Parse(GridView1.SelectedRow.Cells[7].Text);
        tx_tot_qty.Text = System.Math.Round((decimal.Parse(tx_tot_qty.Text) - qty), 5).ToString();
        tx_tot_amt.Text = System.Math.Round((decimal.Parse(tx_tot_amt.Text) - amt), 2).ToString();
        //tx_already_iqty.Text = (decimal.Parse(tx_already_iqty.Text) - qty).ToString();
        //tx_balQty.Text = (decimal.Parse(tx_balQty.Text) + qty).ToString();
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        //string do_no = ddl_do_no.SelectedItem.Text;
        //string blkcode = dt.Rows[idx][0].ToString();
        //string fpscode = dt.Rows[idx][1].ToString();
        //string commcode = dt.Rows[idx][3].ToString();
        //string schemecode = dt.Rows[idx][5].ToString();
        //string temp = "NNN";
        //cmd.CommandText = "select * from dbo.do_fps where delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and  allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and block_code='" + blkcode + "' and fps_code='" + fpscode + "' and commodity='" + commcode + "' and scheme_id='" + schemecode + "'";
        //cmd.Connection = con;
        //con.Open();
        //dr = cmd.ExecuteReader();
        //while (dr.Read())
        //{
        //    temp = "YYY";
        //}        
        //dr.Close();
        //con.Close();
        //if (temp == "YYY")
        //{
        //    string str1 = "delete from dbo.do_fps where delivery_order_no='" + do_no + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and  allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and block_code='" + blkcode + "' and fps_code='" + fpscode + "' and commodity='" + commcode + "' and scheme_id='" + schemecode + "'";
        //    cmd.CommandText = str1;
        //    cmd.Connection = con;
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
        dt.Rows[idx].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;

    }    
    protected void get_block()
    {
        string dist = distid;
        ddl_block.Items.Clear();
        cmd.CommandText = "select * from  pds.block_master where District_code=" + dist + "";
        cmd.Connection = con;
        con.Open();
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
        con.Close();
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
        cmd.CommandText = "SELECT * FROM pds.fps_master where district_code='" + dist + "' and block_code='" + blk + "' ";
        cmd.Connection = con;
        con.Open();
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
        con.Close();
    }
    protected void ddl_fps_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        tx_qty.Text = "";
        Button1.Enabled = true;
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
            string cmdstr = "SELECT round(quantity,5) as quantity FROM dbo.do_fps where district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + " and fps_code='" + ddl_fps_name.SelectedItem.Value + "' and commodity=" + ddl_commodity.SelectedItem.Value + " and scheme_id=" + ddl_scheme.SelectedItem.Value + "";
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            con.Open();
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
                    tx_allot_qty2.Text = System.Math.Round(decimal.Parse(dr[commodity].ToString()), 5).ToString();
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
            tx_balQty2.Text = System.Math.Round(decimal.Parse(tx_allot_qty2.Text) - decimal.Parse(tx_already_iqty2.Text), 5).ToString();
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
            RequiredFieldValidator3.Enabled = true ;
            RequiredFieldValidator1.Enabled = false ;
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
        ddl_scheme.Items.Insert(0, "Select");

        dr.Close();
        con.Close();
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Session["dono"] = null;
        Response.Redirect("~/District/Edit_delivery.aspx");
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
}
