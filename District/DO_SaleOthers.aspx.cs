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
using System.Text;

public partial class District_DO_SaleOthers : System.Web.UI.Page
{
    Districts DObj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    chksql chk = null;
    string sid = "";
    protected Common ComObj = null, cmn = null;

    public string cat = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] == null)
        {
            Response.Redirect("~/Session_Expire_Dist.aspx");
        }

        else
        {
            distid = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                //get_comm();
                get_scheme();
                get_bankname();
                getparty();
                getsaletype();

                //tx_dd_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                //tx_do_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                //tx_do_validity.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
               

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

                hyp_printdo.Attributes.Add("onclick", "window.open('print_Delivaryorder_miller.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

                //tx_do_no.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                //tx_do_no.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                //tx_do_no.Attributes.Add("onchange", "return chksqltxt(this)");

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
               
                tx_do_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                tx_do_date.Attributes.Add("onchange", "return chksqltxt(this)");

                tx_do_validity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                tx_do_validity.Attributes.Add("onchange", "return chksqltxt(this)");

                tx_dd_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                tx_dd_date.Attributes.Add("onchange", "return chksqltxt(this)");
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
        lstitem1.Value = "0";
        ddl_commodity.Items.Insert(0, lstitem1);

        dr.Close();
        con.Close();

    }

    protected void get_scheme()
    {
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME where status='Y' order by Scheme_Id";
        cmd.Connection = con;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["scheme_name"].ToString();
            lstitem.Value = dr["scheme_id"].ToString();
            ddl_scheme.Items.Add(lstitem);
        }
        ddl_scheme.Items.Insert(0, "-Select-");
       
        dr.Close();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void get_bankname()
    {
        ddl_bank.Items.Clear();
        cmd.CommandText = "select Bank_ID,Bank_Name from dbo.Bank_Master where Bank_Name != '' and District_Code = '"+distid+"' and Bank_Name not like '%0%' order by Bank_Name ";
        cmd.Connection = con;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
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

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void getsaletype()
    {
        string qry = "SELECT SaleId ,SaleType FROM DO_SaleType";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdsale = new SqlCommand(qry, con);
        SqlDataAdapter dasale = new SqlDataAdapter(cmdsale);
        DataSet dsSale = new DataSet();

        dasale.Fill(dsSale);

        if (dsSale.Tables[0].Rows.Count == 0)
        {

        }

        else
        {
            ddl_issueto.DataSource = dsSale.Tables[0];
            ddl_issueto.DataTextField = "SaleType";
            ddl_issueto.DataValueField = "SaleId";

            ddl_issueto.DataBind();

            ddl_issueto.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    void Miller()
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string qry = "SELECT Miller_ID, Miller_Name+'('+ Miller_ID+')' as Miller_Name FROM dbo.Miller_Master where District_Code='" + distid + "' and crop_year='" + ddlfinancialyear.SelectedItem.ToString() + "'";

        SqlCommand cmdmiller = new SqlCommand(qry, con);
        SqlDataAdapter damiller = new SqlDataAdapter(cmdmiller);
        DataSet dsmiller = new DataSet();
        damiller.Fill(dsmiller);

        if (dsmiller.Tables[0].Rows.Count > 0)
        {
            ddl_party.DataSource = dsmiller.Tables[0];
            ddl_party.DataTextField = "Miller_Name";
            ddl_party.DataValueField = "Miller_ID";
            ddl_party.DataBind();
            ddl_party.Items.Insert(0, "--Select--");

            lbl_lead.Text = "Miller Name";

            ddl_commodity.Enabled = true; ;
            ddl_scheme.Enabled = true;
        }
        else
        {
            ddl_party.DataSource = "";
            ddl_party.DataBind();

            ddl_party.Items.Insert(0, "Miller Name Not Found");

            ddl_commodity.Enabled = false;
            ddl_scheme.Enabled = false;
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void getparty()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qry = "SELECT PartyId ,PartyName FROM OpenSaleParty where District = '"+distid+"' and SaleTypeID = '"+ddl_issueto.SelectedValue+"'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter daparty = new SqlDataAdapter(cmd);
        DataSet dsparty = new DataSet();
        daparty.Fill(dsparty);

        if (dsparty.Tables[0].Rows.Count > 0)
        {

            ddl_party.DataSource = dsparty.Tables[0];
            ddl_party.DataTextField = "PartyName";
            ddl_party.DataValueField = "PartyId";
            ddl_party.DataBind();
            ddl_party.Items.Insert(0, "--Select--");

            lbl_lead.Text = "Party Name";

            ddl_commodity.Enabled = true; ;
            ddl_scheme.Enabled = true;
        }
        else
        {
            ddl_party.DataSource = "";
            ddl_party.DataBind();

            ddl_party.Items.Insert(0, "Please Add Party Name");

            ddl_commodity.Enabled = false; ;
            ddl_scheme.Enabled = false;
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddl_issueto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_issueto.SelectedValue == "1")
        {
           

            cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where status='Y' and Commodity_Id in ('13','14') order by Commodity_Name ";
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            ddl_commodity.DataSource = "";
            ddl_commodity.DataBind();
            
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
            lstitem1.Value = "0";
            ddl_commodity.Items.Insert(0, lstitem1);

            dr.Close();

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }

        else
        {
            getparty();

            ddl_commodity.DataSource = "";
            ddl_commodity.DataBind();

            cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where status='Y' order by Commodity_Name ";
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


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
            lstitem1.Value = "0";
            ddl_commodity.Items.Insert(0, lstitem1);

            dr.Close();

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
    }

    protected void tx_rate_qt_TextChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (tx_rate_qt.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter Rate per Quintal...');</script>");
            return;
        }
        else
        {
          tx_tot_amt.Text = (CheckNull(tx_rate_qt.Text) * CheckNull(tx_qty.Text)).ToString();
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

    protected void ddl_pmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;
        ddl_bank.Enabled = true;
        if (ddl_pmode.SelectedItem.Value == "D")
        {
            tx_dd_no.Enabled = true;
                
            lbl_ddno.Visible = true;
            lbl_amt.Visible = true;
            tx_dd_date.ReadOnly = false;
            tx_dd_date.Text = "";

            tx_dd_date.Visible = true;

            lblddchekno.Visible = true;
            lblddchekno.Text = "DD/Chq.No.";
            tx_dd_no.Visible = true;
            lblddchekdate.Visible = true;
            tx_do_date.Visible = true;
            tx_dd_amount.Visible = true;
            lblamount.Visible = true;
        }
        else
        {
            tx_dd_no.Enabled = false;
           
            lbl_ddno.Visible = false;
            lbl_amt.Visible = true;

            if (ddl_pmode.SelectedItem.Value == "R")
            {     
                lbl_amt.Visible = false;
                tx_dd_amount.Text = "0";
                ddl_bank.Enabled = false;
                tx_dd_date.Text = "1/1/1";
                tx_dd_date.ReadOnly = true;
                tx_dd_date.Visible = false;
                lblddchekno.Visible = false;
                tx_dd_no.Visible = false;
                lblddchekdate.Visible = false;
                //tx_do_date.Visible = false;
                tx_dd_amount.Visible = true;
                lblamount.Visible = true;
            }
            if (ddl_pmode.SelectedItem.Value == "AD")
            {
                lbl_amt.Visible = false;
                tx_dd_amount.Text = "0";
                ddl_bank.Enabled = false;
                tx_dd_date.Text = "1/1/1";
                tx_dd_date.ReadOnly = true;
                tx_dd_date.Visible = false;
                lblddchekno.Visible = false;
                tx_dd_no.Visible = false;
                lblddchekdate.Visible = false;
                
                tx_dd_amount.Visible = false;
                lblamount.Visible = false;

            }
            if (ddl_pmode.SelectedItem.Value == "OP")
            {
              
                lbl_amt.Visible = false;
                tx_dd_amount.Text = "0";
                ddl_bank.Enabled = false;
                tx_dd_date.Text = "1/1/1";
                tx_dd_date.ReadOnly = true;
                tx_dd_date.Visible = false;
                lblddchekno.Visible = true;
                lblddchekno.Text = "Neft/RTGS Number";
                tx_dd_no.Visible = true;
                tx_dd_no.Enabled = true;
                tx_dd_no.Text = "0";
                lblddchekdate.Visible = false;
              
                tx_dd_amount.Visible = false;
                lblamount.Visible = true;
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/DO_SaleOthers.aspx");
    }
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        

    if (ddl_issueto.SelectedValue == "2") // Damage , calculate Damage
        {
            string qry = "select isnull (sum(Damage_Quantity),0)DamageQty from Tbl_Damage_Sweepage where Dist_Id= '" + distid + "' and Commodity = '" + ddl_commodity.SelectedValue + "' and Scheme = '" + ddl_scheme.SelectedValue + "'";
            SqlCommand cmdqty = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmdqty);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string damqty = ds.Tables[0].Rows[0]["DamageQty"].ToString();
           
            txtdamqty.Text = damqty;
           
        }

    

        else if (ddl_issueto.SelectedValue == "4") // Sweepage , calculate Sweepage
        {
            string qry = "select isnull (sum(SweepageQty),0)SweepageQty from Tbl_Damage_Sweepage where Dist_Id= '" + distid + "' and Commodity = '" + ddl_commodity.SelectedValue + "' and Scheme = '" + ddl_scheme.SelectedValue + "'";
            SqlCommand cmdqty = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmdqty);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            string sweQty = ds.Tables[0].Rows[0]["SweepageQty"].ToString();

            txtdamqty.Text = sweQty;     
        }

        else
        {
            //Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Sale Type');</script>");
            //return;
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        
    }

    protected void save_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddl_issueto.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Sale Type');</script>");
            return;
        }

        if (ddl_party.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Party/Miller Name');</script>");
            return;
        }
        if (ddl_party.SelectedValue == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter miller name');</script>");
            return;
        }

        if (ddl_commodity.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('select Commodity');</script>");
            return;
        }

        //if (tx_do_no.Text == "")
        //{
        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter D.O. Number');</script>");
        //    return;
        //}

        if (tx_do_date.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select D.O. Date from calender');</script>");
            return;
        }

        if (tx_qty.Text == "")
        {
            tx_rate_qt.Text = "";
            tx_tot_amt.Text = "";

            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter D.O. Quantity');</script>");
            return;
        }

        //

        if (ddl_party.SelectedValue == "0" || ddl_party.SelectedItem.Text == "--Select--")
        {
           Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Party Name');</script>");
            return;
        }
        if (tx_do_validity.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select D.O. Validity from Calender');</script>");
            return;
        }

        if (ddl_pmode.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Payment Mode');</script>");
            return;
        }

        if (tx_qty.Text == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('DO qty will not be 0');</script>");
            return;
        }
        //double stock = Convert.ToDouble(txtdamqty.Text);

        double DOQty = Convert.ToDouble(tx_qty.Text);      

        //if (DOQty > stock)
        //{
        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('DO qty is More than Stock');</script>");
        //    return;
        //}

        //else
        //{

            string year_do = System.DateTime.Now.Date.ToString("yy");    // For DO generation year wise (29/03/14)

            string selectmax = "select max(delivery_order_no) as Do_Num from OpenSale_DO where district_code='" + distid + "'";

            SqlCommand cmdmax = new SqlCommand(selectmax, con);
            SqlDataAdapter damax = new SqlDataAdapter(cmdmax);

            DataSet dsmax = new DataSet();

            damax.Fill(dsmax);

            string DO_ID = dsmax.Tables[0].Rows[0]["Do_Num"].ToString();

            if (DO_ID == "")
            {
                DO_ID = distid + year_do + "1000";
            }
            else
            {
                string fordo = DO_ID.Substring(DO_ID.Length - 4);

                Int64 DO_ID_new = Convert.ToInt64(fordo);

                DO_ID_new = DO_ID_new + 1;

                string combine = DO_ID_new.ToString();

                DO_ID = distid + year_do + combine;
            }

            string delivery_order_no = DO_ID;

            //string checkdo = "SELECT COUNT(delivery_order_no)FROM OpenSale_DO where delivery_order_no = '"+delivery_order_no+"'";
            //SqlCommand cmdche = new SqlCommand(checkdo, con);
            //Int16 che = Convert.ToInt16(cmdche.ExecuteScalar());
            //if (che != 0)
            //{
            //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('DO Number is Available');</script>");
            //    return;
            //}
            //else
            //{
                string Issueto_partyName = ddl_party.SelectedValue;

                string district_code = distid;

                string issue_type = ddl_issueto.SelectedItem.Value;

                string do_date = getDate_MDY(tx_do_date.Text);

                string payment_mode = ddl_pmode.SelectedItem.Value;

                string dd_no = tx_dd_no.Text;

                if (tx_dd_date.Text == "")
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select DD/Cheque Date from Calender');</script>");
                    return;
                }

                string dd_date = getDate_MDY(tx_dd_date.Text);
                string validdate = getDate_MDY(tx_do_validity.Text);

                string quantity = tx_qty.Text.Trim();

                string bank_id = ddl_bank.SelectedValue;

                string commodity_id = ddl_commodity.SelectedValue;

                string scheme_id = ddl_scheme.SelectedValue;

                string rate_per_qtls = tx_rate_qt.Text.Trim();

                string tot_amount = tx_tot_amt.Text.Trim();

                string amount = tx_dd_amount.Text.Trim();

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string opid = Session["OperatorIDDM"].ToString();

                string ins = "Insert into OpenSale_DO (crop_year,delivery_order_no,district_code,issue_type,do_date,payment_mode,dd_no,dd_date,quantity, rate_per_qtls,tot_amount,amount,bank_id,commodity_id,scheme_id,created_date,IP,OperatorID,Partyname,DO_ValidDate) Values ('" + ddlfinancialyear.SelectedItem.ToString() + "','" + delivery_order_no + "','" + district_code + "','" + issue_type + "','" + do_date + "','" + payment_mode + "','" + dd_no + "','" + dd_date + "','" + quantity + "','" + rate_per_qtls + "','" + tot_amount + "','" + amount + "','" + bank_id + "','" + commodity_id + "','" + scheme_id + "',getDate(),'" + ip + "','" + opid + "','" + Issueto_partyName + "','" + validdate + "')";

                SqlCommand cmd = new SqlCommand(ins, con);

                try
                {
                    int y = cmd.ExecuteNonQuery();

                    if (y > 0)
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved, DO Number is -- " + delivery_order_no + "');</script>");

                        LblShowDonum.Text = delivery_order_no;

                        save.Enabled = false;

                        LblShowDonum.Visible = true;
                        LbldisplayDo.Visible = true;


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
                    hyp_printdo.Visible = true;
                    hyp_printdo.Enabled = true;
                        Session["doforprint"] = delivery_order_no;

                
                    }

                }

                catch
                {
                    throw;
                }

            //}

         if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        
    }
    protected void ddlfinancialyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_issueto.SelectedValue == "1")
        {
            Miller();
        }
        
    }
}
