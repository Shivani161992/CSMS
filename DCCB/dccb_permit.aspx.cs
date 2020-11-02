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
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class District_dccb_permit : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string sid = "";

    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
    DataTable dt = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {

        distid = Session["dist_id"].ToString();
        sid = null;
        save.Enabled = true;
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
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

            Session["dt"] = dt;

            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.SelectedIndex = 1;

            get_comm();
            get_lead();
            get_block();
            get_scheme();
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
        }
        //tx_do_no.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_permit_no.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        //tx_do_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_permit_validity.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_qty.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
        tx_rate_qt.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");

        hlinkpdo.Attributes.Add("onclick", "window.open('Print_Permit_Order.aspx',null,'left=100, top=50, height=700, width= 650, status=n o, resizable= no, scrollbars= yes, toolbar= no,location= no, menubar= no');");

        tx_permit_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_validity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_rate_qt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_amt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_tot_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_allot_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_no.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_dd_amount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

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
            //string do_no = tx_do_no.Text;
            string per_no = tx_permit_no.Text;
            string pmode = ddl_pmode.SelectedItem.Value;
            //string dd_no = tx_dd_no.Text;
            string temp = "yyy";
            //string do_date = getDate_MDY(tx_do_date.Text);
            string permit_date = getDate_MDY(tx_permit_date.Text);
            string dd_date = getDate_MDY(tx_dd_date.Text);
            string crdate = getDate_MDY(DateTime.Today.Date.ToString());
            string udate = "";
            //float rate_qt = new float();
            //if (tx_rate_qt.Text == "")
            //{
            //    rate_qt = 0;
            //}
            //else
            //{
            //    rate_qt = float.Parse(tx_rate_qt.Text);
            //}
            cmd.CommandText = "select issueCentre_code,district_code from dbo.Dccb_Permit where Permit_order_no='" + per_no + "'";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (issue_centre_code == dr["issueCentre_code"].ToString() && dist == dr["district_code"].ToString())
                {
                    temp = "nnn";
                    break;
                }
            }

            dr.Close();
            con.Close();

            if (temp == "yyy")
            {

                if (issue_type == "L")
                {
                    issue_name = ddl_lead.SelectedItem.Text;
                    //fps_code = ddl_fps_name.SelectedItem.Value;
                }
                else
                {
                    issue_name = null;

                }


                if (tx_permit_validity.Text == "")
                {
                    permit_valid = 0;
                }
                else
                {
                    permit_valid = Convert.ToInt32(tx_permit_validity.Text);
                }



                string str1 = "INSERT INTO dbo.Dccb_Permit(Permit_order_no,district_code,issueCentre_code,issue_type,issue_name,allotment_month,allotment_year,permit_validity,permit_date,created_date,updated_date,payment_mode,dd_no,dd_date,quantity,amount) VALUES('" + per_no + "','" + dist + "','" + issue_centre_code + "','" + issue_type + "','" + issue_name + "'," + month.ToString() + "," + crdate + "," + permit_valid + ",'" + permit_date + "','" + udate + "','','" + pmode + "'," + tx_dd_no.Text + ",'" + dd_date + "'," + tx_tot_qty.Text + "," + tx_dd_amount.Text + ")";
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

                        str1 = "INSERT INTO dbo.Dccb_Permit_FPS_Detail(Permit_order_no,district_code,block_code,fps_code,commodity,scheme_id,quantity,rate_per_qtls,status)VALUES('" + per_no + "','" + dist + "','" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][5] + "'," + dt.Rows[i][8] + "," + dt.Rows[i][7] + ",'Y')";
                        cmd.CommandText = str1;
                        cmd.ExecuteNonQuery();
                        i = i + 1;
                    }
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Inserted Successfully...');</script>");
                    Label1.Text = "Data Inserted Successfully ...";
                    Session["permit_print"] = per_no ;
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();
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
                Page.Form.Disabled = true;
                save.Enabled = false;
                Session["permit_print"] = per_no;
                hlinkpdo.Enabled = true;
                hlinkpdo.Visible = true;


            }
            else
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Record already exist');</script>");
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Record already exist ... Change DD No./DO No.";
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
        cmd.CommandText = "select Commodity_ID,Commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY ";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["Commodity_name"].ToString();
            lstitem.Value = dr["Commodity_ID"].ToString();
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
        string blk = ddl_block.SelectedItem.Value;
        cmd.CommandText = "SELECT Lead_soc_fps.*,pds.fps_master.fps_name, pds.fps_master.fps_code FROM dbo.Lead_soc_fps Left JOIN pds.fps_master ON Lead_soc_fps.District_code = pds.fps_master.district_code AND Lead_soc_fps.fps_code = pds.fps_master.fps_code where Lead_soc_fps.District_code='36' and Lead_soc_fps.LeadSoc_Code='360701'";
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
        dr.Close();
        con_opdms.Close();
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
    protected void get_scheme()
    {
        cmd.CommandText = "select Scheme_Id,Scheme_Name from dbo.tbl_MetaData_SCHEME where Scheme_Name in ('APL','BPL','AAY')";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["Scheme_Name"].ToString();
            lstitem.Value = dr["Scheme_Id"].ToString();
            ddl_scheme.Items.Add(lstitem);
        }
        ListItem lstitem1 = new ListItem();
        lstitem1.Text = "Non Scheme";
        lstitem1.Value = "0";
        ddl_scheme.Items.Insert(0, lstitem1);

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
            lbl_ld_v.Visible = true;
            //ddl_fps_name.Enabled = true;
            //tx_issue_name.Visible = false;

        }
        else
        {
            ddl_lead.Visible = false;
            lbl_lead.Visible = false;
            lbl_ld_v.Visible = false;
            //ddl_fps_name.Enabled = false;
            //tx_issue_name.Visible = true;
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
            lbl_ddno.Visible = true;


        }
        else
        {
            tx_dd_no.Enabled = false;
            RequiredFieldValidator3.Enabled = false;
            lbl_ddno.Visible = false;

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = System.Drawing.Color.Blue;

        if (ddl_commodity.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select commodity');</script>");
            Label1.Text = "Please select commodity ...";
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
            if (tx_allot_qty.Text == "")
            {
                tx_allot_qty.Text = "0";
            }
            if (float.Parse(tx_qty.Text) > float.Parse(tx_allot_qty.Text))
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to Issue can not be greater than Alloted...');</script>");
                tx_qty.Text = "";
                tx_qty.Focus();
            }
            else
            {
                tx_tot_qty.Text = (float.Parse(tx_tot_qty.Text) + float.Parse(tx_qty.Text)).ToString();
                tx_tot_amt.Text = (float.Parse(tx_tot_amt.Text) + (float.Parse(tx_qty.Text) * float.Parse(tx_rate_qt.Text))).ToString();
                dt = (DataTable)Session["dt"];
                dt.Rows.Add(ddl_block.SelectedItem.Value, ddl_fps_name.SelectedItem.Value, ddl_fps_name.SelectedItem.Text, ddl_commodity.SelectedItem.Value, ddl_commodity.SelectedItem.Text, ddl_scheme.SelectedItem.Value, ddl_scheme.SelectedItem.Text, tx_rate_qt.Text, tx_qty.Text, float.Parse(tx_qty.Text) * float.Parse(tx_rate_qt.Text));

                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["dt"] = dt;
                tx_qty.Text = "";
                //tx_rate_qt.Text = "";
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        float qty = float.Parse(GridView1.SelectedRow.Cells[5].Text);
        float amt = float.Parse(GridView1.SelectedRow.Cells[6].Text);
        tx_tot_qty.Text = (float.Parse(tx_tot_qty.Text) - qty).ToString();
        tx_tot_amt.Text = (float.Parse(tx_tot_amt.Text) - amt).ToString();
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        dt.Rows[idx].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;

    }
    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_fps();
    }
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_scheme.SelectedItem.Text == "Non Scheme" || ddl_commodity.SelectedItem.Text == "Select" || ddl_fps_name.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS/Commodity/Scheme...');</script>");
        }
        else
        {
            string comm = ddl_commodity.SelectedItem.Text;
            string scheme = ddl_scheme.SelectedItem.Text;
            int amonth = int.Parse(ddl_allot_month.SelectedItem.Value);
            int ayear = int.Parse(ddd_allot_year.SelectedItem.Text);
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
                cmd.CommandText = "select  " + commodity + "   from pds.fps_allot where fps_code='" + fps_code + "' and month='" + amonth + "' and Year='" + ayear + "'";
                cmd.Connection = con_opdms;
                con_opdms.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tx_allot_qty.Text = dr[commodity].ToString();
                }
                dr.Close();
                con_opdms.Close();
            }
        }
        GetRate();
    }
    protected void GetRate()
    {

        string qry = "Select Rate from dbo.SCSC_MSP_rate where Scheme_ID='" + ddl_scheme.SelectedValue + "'and Commodity_ID='" + ddl_commodity.SelectedValue + "'";
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is no Rate available for selected commodity....'); </script> ");

            tx_rate_qt.ReadOnly = false;
            tx_rate_qt.Focus();


        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            tx_rate_qt.Text = dr["Rate"].ToString();
            tx_rate_qt.ReadOnly = true;
            tx_rate_qt.BackColor = System.Drawing.Color.Wheat;
            tx_rate_qt.Focus();
        }


    }
    protected void tx_permit_no_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_lead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_fps_name_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}
