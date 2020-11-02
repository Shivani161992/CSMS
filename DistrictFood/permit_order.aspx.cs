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
public partial class permit_order : System.Web.UI.Page
{   
    DistributionCenters distobj = null;
    Districts DObj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
     string distid = "";    
    DataTable dt = new DataTable();
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
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today .Year.ToString())-1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.SelectedIndex = 1;
            GetDepot();
            get_comm();
            get_lead();
            get_block();
            get_scheme();            
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
        }
        tx_permit_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_dd_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_permit_no.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_permit_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");       
        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_rate_qt.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        hlinkpdo.Attributes.Add("onclick", "window.open('Print_Permit_Order.aspx',null,'left=300, top=90, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        tx_permit_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();        
        tx_permit_validity .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_rate_qt .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_amt .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_qty .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_no .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount .Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_already_iqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_already_iqty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balQty2.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balQty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_bal_ic.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
    }
    protected void save_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;
        dt = (DataTable)Session["dt"];
        int count = dt.Rows.Count;
        if (count > 0)
        {
            string dist = distid;
            string issue_centre_code =ddlissue.SelectedItem.Value ;
            string issue_type = ddl_issueto.SelectedItem.Value;            
            string issue_name = "";
            //string fps_code = "";
            string month = ddl_allot_month.SelectedItem.Value;
            int permit_valid = Convert.ToInt32(tx_permit_validity.Text);         
            //Double qty = Convert.ToDouble(tx_qty.Text);           
            string per_no = tx_permit_no.Text;
            string pmode = ddl_pmode.SelectedItem.Value;
            string dd_no = tx_dd_no.Text;
            string temp = "YYY";          
            string permit_date = getDate_MDY(tx_permit_date.Text);
            string dd_date = getDate_MDY(tx_dd_date.Text);
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            cmd.CommandText = "select permit_order_no  from dbo.Dccb_Permit where permit_order_no='" + per_no + "' and issueCentre_code='" + issue_centre_code + "' and district_code='" + dist + "' and commodity_id='" + ddl_commodity.SelectedValue + "' and scheme_id='" + ddl_scheme.SelectedValue + "'";
            cmd.Connection = con;
            con.Open();
            dr=cmd.ExecuteReader();
            while (dr.Read())
            {
                temp = "NNN";
                break;
            }
            dr.Close();
            con.Close();

            if (temp == "YYY")
            {

                if (issue_type == "LF")
                {
                    issue_name = ddl_lead.SelectedItem.Value ;
                    //fps_code = ddl_fps_name.SelectedItem.Value;
                }
                else
                {
                    issue_name = null;
                    
                }                
               
                string str1 = "INSERT INTO dbo.Dccb_Permit(Permit_order_no,district_code,issueCentre_code,issue_type,issue_name,allotment_month,allotment_year,permit_validity,permit_date,created_date,updated_date,payment_mode,dd_no,dd_date,quantity,amount,tot_amount,bank_id,commodity_id,scheme_id,rate_per_qtls,status) VALUES('" + per_no + "','" + dist + "','" + issue_centre_code + "','" + issue_type + "','" + issue_name + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + "," + permit_valid + ",'" + permit_date + "',getdate(),'','" + pmode + "','" + dd_no + "','" + dd_date + "'," + tx_tot_qty.Text + "," + tx_dd_amount.Text + "," + tx_tot_amt.Text + ",'" + ddl_bank.SelectedItem.Value + "'," + ddl_commodity.SelectedItem.Value + ",'"+ ddl_scheme.SelectedItem.Value +"',"+ tx_rate_qt.Text +",'N')";
                cmd.CommandText = str1;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //For Dccb_Permit_FPS_Detail Table

                    int i = 0;
                    while (i < count)
                    {
                        int trnscnt = 0;
                        string docount = "";
                        string strqr = "select count(Permit_order_no) as rwcount  from dbo.Dccb_Permit_FPS_Detail  where Permit_order_no='" + per_no + "' and district_code='" + dist + "'";
                        cmd.CommandText = strqr;
                        cmd.Connection = con;
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
                        string transid = dist.ToString() + per_no.ToString() + (trnscnt).ToString();                         
                        str1 = "INSERT INTO dbo.Dccb_Permit_FPS_Detail(trans_id,Permit_order_no,district_code,issueCentre_code,allotment_month,allotment_year,block_code,fps_code,commodity,scheme_id,allot_qty,quantity,rate_per_qtls,status,ip_add)VALUES('" + transid + "','" + per_no + "','" + dist + "','" + issue_centre_code + "'," + month.ToString() + "," + ddd_allot_year.SelectedItem.Text + ",'" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][10] + "'," + dt.Rows[i][8] + "," + dt.Rows[i][7] + ",'N','" + ip + "')";
                        cmd.CommandText = str1;
                        cmd.ExecuteNonQuery();                        
                        i = i + 1;                        
                    }                   
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                    Label1.Text = "Data Saved Successfully ...";
                    panelDO.Enabled = false;
                    save.Enabled = false;
                    hlinkpdo.Visible = true;
                    hlinkpdo.Enabled = true;
                    Session["permit_print"] = per_no;                   
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
            else
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Record already exist');</script>");
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Record already exist ... Change Permit No.";
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter FPS details');</script>");
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please enter FPS details ...";
        }

    }
    protected void get_comm()
    {
        cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY";
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
        lstitem1.Text = "pq«Uk;¢";
        lstitem1.Value = "N";
        ddl_lead.Items.Insert(0, lstitem1);
        dr.Close();
        con_opdms.Close();
    }
    protected void get_fps()
    {
        string dist = distid;
        ddl_fps_name.Items.Clear();
        if (ddl_block.SelectedItem.Text != "Select")
        {
            string blk = ddl_block.SelectedItem.Value;
            cmd.CommandText = "SELECT pds.fps_master.block_code,pds.fps_master.fps_name, pds.fps_master.fps_code FROM dbo.Lead_soc_fps Left JOIN pds.fps_master ON Lead_soc_fps.District_code = pds.fps_master.district_code AND Lead_soc_fps.fps_code = pds.fps_master.fps_code where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "' order by Lead_soc_fps.fps_code";
            cmd.Connection = con_opdms;
            con_opdms.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["fps_name"].ToString();
                lstitem.Value = dr["fps_code"].ToString();
                ddl_block.SelectedValue = dr["block_code"].ToString();
                ddl_fps_name.Items.Add(lstitem);
            }
            ddl_fps_name.Items.Insert(0, "Select");
            ddl_block.SelectedIndex = 0;
            dr.Close();
            con_opdms.Close();
        }
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
        ddl_block.SelectedIndex = 0;
        dr.Close();
        con.Close();
    }
    protected void get_scheme()
    {
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME where lower(tbl_MetaData_SCHEME.Scheme_Name) in(Select lower(tbl_MetaData_PDS_S.Scheme_Name) from dbo.tbl_MetaData_PDS_S )   order by Scheme_Id";
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
        ListItem lstitem1 = new ListItem();
        lstitem1.Text = "Non Scheme";
        lstitem1.Value = "0";
        ddl_scheme.Items.Insert(ddl_scheme.Items.Count, lstitem1);

        dr.Close();
        con.Close();
    }

    protected void ddl_issueto_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;

        if (ddl_issueto.SelectedItem.Text == "Lead Society")
        {
            ddl_lead.Visible = true;
            lbl_lead.Visible = true;
            ddl_scheme.AutoPostBack = true;
            Label4.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            tx_allot_qty.Visible = true;
            tx_already_iqty.Visible = true;
            tx_balQty.Visible = true;
            tx_bal_ic.Visible = true;
            Label9.Visible = true;
            Label10.Visible = true;
            Label11.Visible = true;
            Label12.Visible = true;
        }
        else
        {
            ddl_lead.Visible = false;
            lbl_lead.Visible = false;
            ddl_scheme.AutoPostBack = false;
            Label4.Visible = false;
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            tx_allot_qty.Visible = false;
            tx_already_iqty.Visible = false;
            tx_balQty.Visible = false;
            tx_bal_ic.Visible = false;
            Label9.Visible = false;
            Label10.Visible = false;
            Label11.Visible = false;
            Label12.Visible = false;
        }
    }
    protected void ddl_pmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;
        if (ddl_pmode.SelectedItem.Text == "DD")
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
            if (ddl_pmode.SelectedItem.Text == "Credit")
            {
                RequiredFieldValidator4.Enabled = false;               
                lbl_amt.Visible =false;
                tx_dd_amount.Text  = "0";
            }

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;

        if (ddl_commodity.SelectedItem.Text == "Select" || ddl_scheme.SelectedItem.Text == "Select" || ddl_fps_name.SelectedItem.Text == "Select" || ddl_block.SelectedItem.Text=="Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme/FPSBlock/FPS');</script>");
            Label1.Text = "Please select Commodity/SchemeFPSBlock/FPS ...";
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
            if (tx_bal_ic.Text == "")
            {
                tx_bal_ic.Text = "0";
            }
            
            string temp = "NNN";
            dt = (DataTable) Session["dt"];
            int row =0;
            if (dt.Rows.Count > 0)
            {
                while (row < dt.Rows.Count)
                {
                    if (dt.Rows[row][1].ToString() == ddl_fps_name.SelectedItem.Value && dt.Rows[row][3].ToString() == ddl_commodity.SelectedItem .Value  && dt.Rows[row][5].ToString() ==ddl_scheme .SelectedItem.Value)
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
                float bal_qty = float.Parse(tx_balQty2.Text);
                //if (float.Parse(tx_tot_qty.Text) + float.Parse(tx_qty.Text) > float.Parse(tx_bal_ic.Text))
                //{
                //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Balance Quantity at IssueCentre...');</script>");
                //    tx_qty.Text = "";
                //    tx_qty.Focus();
                //}
                //else 
                if (float.Parse(tx_qty.Text) > bal_qty || float.Parse(tx_qty.Text) <= 0 )
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Alloted...');</script>");
                    tx_qty.Text = "";
                    tx_qty.Focus();
                }
                else
                {
                    tx_tot_qty.Text =System.Math.Round((float.Parse(tx_tot_qty.Text) + float.Parse(tx_qty.Text)),5).ToString();
                    tx_tot_amt.Text = System.Math.Round((float.Parse(tx_tot_amt.Text) + (float.Parse(tx_qty.Text) * float.Parse(tx_rate_qt.Text))),2).ToString();
                    dt = (DataTable)Session["dt"];
                    dt.Rows.Add(ddl_block.SelectedItem.Value, ddl_fps_name.SelectedItem.Value, ddl_fps_name.SelectedItem.Text, ddl_commodity.SelectedItem.Value, ddl_commodity.SelectedItem.Text, ddl_scheme.SelectedItem.Value, ddl_scheme.SelectedItem.Text, tx_rate_qt.Text, tx_qty.Text, float.Parse(tx_qty.Text) * float.Parse(tx_rate_qt.Text), tx_allot_qty2.Text);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();                   
                    tx_qty.Text = "";                    
                    ddl_block.SelectedIndex = 0;
                    ddl_fps_name.SelectedIndex = 0;
                   
                    
                }
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        float qty = float.Parse(GridView1.SelectedRow.Cells[5].Text);
        float amt = float.Parse(GridView1.SelectedRow.Cells[7].Text);
        tx_tot_qty.Text =System.Math.Round( (float.Parse(tx_tot_qty.Text) - qty),5).ToString();
        tx_tot_amt.Text = System.Math.Round((float.Parse(tx_tot_amt.Text) - amt),2).ToString();       
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        dt.Rows[idx].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;

    }
    protected String  getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_blkFPS();
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

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                tx_rate_qt.Text = dr["Rural_rate"].ToString();
                tx_rate_qt.ReadOnly = true;
            }
        }
            
          
        
    }
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        tx_allot_qty.Text = "";
        if (ddl_issueto.SelectedItem.Text == "Lead Society")
        {
            if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select" || ddl_lead.SelectedItem.Text == "pq«Uk;¢")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Lead Society/Commodity/Scheme...');</script>");
            }
            else
            {
                GetRate();
                string comm = ddl_commodity.SelectedItem.Text;
                string scheme = ddl_scheme.SelectedItem.Text;
                int amonth = int.Parse(ddl_allot_month.SelectedItem.Value);
                int ayear = int.Parse(ddd_allot_year.SelectedItem.Text);                
               // string fps_code = ddl_fps_name.SelectedItem.Value;
                get_IssuedQty();
                get_balAtIC();
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
                    cmd.CommandText = "SELECT sum(" + commodity + ") as commodity FROM   pds.fps_allot INNER JOIN  dbo.Lead_soc_fps ON pds.fps_allot.district_code = Lead_soc_fps.District_code AND pds.fps_allot.fps_code = Lead_soc_fps.fps_code and pds.fps_allot.month=" + amonth + " and pds.fps_allot.year=" + ayear + "  where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='" + ddl_lead.SelectedItem.Value + "'";
                    cmd.Connection = con_opdms;
                    con_opdms.Open();
                    cmd.CommandTimeout = 2400;
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
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('There is no Allotmennt for Selected Commodity...');</script>");
                }
                if (tx_already_iqty.Text == "")
                {
                    tx_already_iqty.Text = "0";
                }
                tx_balQty.Text = "";
                ddl_block.SelectedIndex = 0;
                tx_balQty.Text = System.Math.Round(CheckNull(tx_allot_qty.Text) - CheckNull(tx_already_iqty.Text), 5).ToString();
                tx_qty.Focus();
            }
        }
    }
    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }

    protected void ddl_lead_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_fps();
    }
    protected void get_blkFPS()
    {
        string dist = distid;
        ddl_fps_name.Items.Clear();
        string blk = ddl_block.SelectedItem.Value;
        cmd.CommandText = "SELECT * FROM pds.fps_master where district_code='"+ dist +"' and block_code='"+ blk +"' ";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text =dr["fps_code"].ToString() +"-"+ dr["fps_name"].ToString() ;
            lstitem.Value = dr["fps_code"].ToString();
            ddl_fps_name.Items.Add(lstitem);
        }
        ddl_fps_name.Items.Insert(0, "Select");
        dr.Close();
        con.Close();
    }
    protected void get_IssuedQty()
    {
        tx_already_iqty.Text = "";
        string sid = ddlissue.SelectedItem.Value;
        string cmdstr = "SELECT round(sum(do_fps.quantity),5) as quantity FROM dbo.do_fps INNER JOIN  dbo.Lead_soc_fps ON do_fps.district_code = Lead_soc_fps.District_code  AND do_fps.fps_code = Lead_soc_fps.fps_code and do_fps.district_code='" + distid + "' and do_fps.issueCentre_code='" + sid + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.commodity=" + ddl_commodity.SelectedItem.Value + " and do_fps.scheme_id=" + ddl_scheme.SelectedItem.Value + " where Lead_soc_fps.District_code='" + distid + "' and Lead_soc_fps.LeadSoc_Code='"+ ddl_lead .SelectedItem.Value +"'";
        cmd.CommandText = cmdstr; 
        cmd.Connection = con;
        con.Open();
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
        string sid = ddlissue.SelectedItem.Value;
        string cmdstr = "SELECT round(Current_Balance,5) as bal_ic FROM dbo.issue_opening_balance where District_Id='" + distid + "' and Depotid='" + sid + "' and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "'";
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
    protected void ddl_fps_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_issueto.SelectedItem.Text != "Lead Society")
        {
            GetRate();
        }
        //ddl_scheme.SelectedIndex = 0;
        string sid = ddlissue.SelectedItem.Value;
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
            get_IssuedQty();
            get_balAtIC();
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
                    tx_allot_qty2.Text = System.Math.Round(float.Parse(dr[commodity].ToString()), 5).ToString();
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
            tx_balQty2.Text = System.Math.Round(float.Parse(tx_allot_qty2.Text) - float.Parse(tx_already_iqty2.Text), 5).ToString();
            tx_qty.Focus();
        }  
        
    }
    protected void get_bankname()
    {
        string sid = ddlissue.SelectedItem.Value;
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
    void GetDepot()
    {
        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + distid + "' order by DepotName";
        DataSet ds = distobj.select(ord);
        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";
        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "Select");
    }
    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_bankname();
    }
   
}
