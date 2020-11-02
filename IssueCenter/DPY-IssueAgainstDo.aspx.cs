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

public partial class IssueCenter_DPY_IssueAgainstDo : System.Web.UI.Page
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
          DateTime dtt;
   
    DataTable Dt1 = new DataTable();
    public string version = "";
    string qunt = "0";
    decimal totalissuequnt;
    decimal do_qunat;
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
            Getbranch();
            //GetGodown();
          
            get_scheme();
            GetSource();
     

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

        tx_bags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balance_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_gatepass.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issue_balqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issued_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
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
            cmd.CommandText = "select delivery_order_no from dbo.DoorStep_DO where district_code='" + dist + "' and issueCentre_code='" + sid + "' and status='N' and delivery_order_no not like '%NoDO%' order by  created_date desc";
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
       
        Session["dt1"] = null;
        Panel2.Visible = false;
        lblCommodity.Visible = true;
        ddl_commodity.Visible = true;
        get_comm();
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

                string checkdo = "Select Transporter  from DoorStep_DO where district_code='" + dist + "' and issueCentre_code='" + sid + "' and delivery_order_no = '" + ddl_do_no.SelectedItem.Text + "' ";

                SqlCommand cmd_dotype = new SqlCommand(checkdo, con);
                SqlDataAdapter da_dotype = new SqlDataAdapter(cmd_dotype);
                DataSet ds_dotype = new DataSet();

                da_dotype.Fill(ds_dotype);

            

              
                    ddl_lead.Visible = false;

                    ddltransporter.Visible = true;

                    string transprter = ds_dotype.Tables[0].Rows[0]["Transporter"].ToString();
                    hd_transpoeter.Value = transprter;
                    if (Convert.ToInt32(transprter) == 7 || Convert.ToInt32(transprter) == 10 || Convert.ToInt32(transprter) == 3)
                {
                    
                    ddltransporter.SelectedIndex = 1;
                    //ddl_godown.SelectedIndex = 0;
                    ddlsarrival.SelectedIndex = 0;
                    get_do_data();
                
                }
                else
                {
                   

                    ddltransporter.SelectedValue = transprter;
                    //ddl_godown.SelectedIndex = 0;
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
            cmd.CommandText = "SELECT DoorStep_do_fps.commodity,DoorStep_do_fps.scheme_id,DoorStep_DO.allotment_month,DoorStep_DO.allotment_year,round(convert(decimal(18,5),DoorStep_DO.quantity),5) as quantity,DoorStep_DO.do_date,DoorStep_DO.issue_to, DoorStep_DO.do_validity,round(SUM(convert(decimal(18,5),issue_against_Doorstep_do.qty_issue)),5) AS issueqty FROM dbo.DoorStep_DO LEFT JOIN dbo.issue_against_Doorstep_do on DoorStep_DO.delivery_order_no=issue_against_Doorstep_do.delivery_order_no and DoorStep_DO.district_code=issue_against_Doorstep_do.district_code and issue_against_Doorstep_do.Transport_order is  null join dbo.DoorStep_do_fps ON DoorStep_DO.delivery_order_no = DoorStep_do_fps.delivery_order_no AND DoorStep_DO.district_code = DoorStep_do_fps.district_code and DoorStep_DO.issueCentre_code = DoorStep_do_fps.issueCentre_code and Transport_order is null  GROUP BY DoorStep_DO.issue_to,DoorStep_DO.delivery_order_no,DoorStep_do_fps.issueCentre_code,DoorStep_DO.issueCentre_code, DoorStep_DO.district_code, DoorStep_DO.do_date, DoorStep_DO.do_validity, DoorStep_do_fps.fps_name,DoorStep_DO.quantity,DoorStep_DO.allotment_month,DoorStep_DO.allotment_year,DoorStep_do_fps.commodity,DoorStep_do_fps.scheme_id having DoorStep_DO.delivery_order_no='" + do_no + "' and DoorStep_DO.district_code='" + dist + "' and DoorStep_DO.issueCentre_code='" + issue_centre_code + "' ";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                do_date = DateTime.Parse(dr["do_date"].ToString());
                do_valid = dr["do_validity"].ToString();
             
                do_qty = dr["quantity"].ToString();
                issueqty = dr["issueqty"].ToString();
                lead_code = dr["issue_to"].ToString();
                ddl_allot_month.SelectedValue = dr["allotment_month"].ToString();
                try
                {
                    ddd_allot_year.SelectedValue = dr["allotment_year"].ToString();
                }
                catch (Exception)
                { }
                //ddl_commodity.SelectedValue = dr["commodity"].ToString();
                ddl_scheme.SelectedValue = dr["scheme_id"].ToString();
            }

            dr.Close();
            con.Close();


            Session["issueto"] = lead_code;
            if (issueqty == "")
            {
                issueqty = "0";
            }

            SqlDataAdapter da = new SqlDataAdapter("select DoorStep_do_fps.fps_code,DoorStep_do_fps.*,opdms.pds.fps_master.fps_Uname,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name,round(convert(decimal(18,5),DoorStep_do_fps.quantity),5) as qty,round(convert(decimal(18,5),rate_per_qtls),2) as rateqtls,round(convert(decimal(18,5),DoorStep_do_fps.quantity)*convert(decimal(18,5),rate_per_qtls),2) as amt from dbo.DoorStep_do_fps LEFT JOIN opdms.pds.fps_master ON DoorStep_do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON DoorStep_do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON DoorStep_do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id  where DoorStep_do_fps.delivery_order_no='" + do_no + "' and DoorStep_do_fps.district_code='" + dist + "' and DoorStep_do_fps.issueCentre_code='" + issue_centre_code + "' and DoorStep_do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and DoorStep_do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and DoorStep_do_fps.status='N' ", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "DoorStep_do_fps");
            GridView1.DataSource = ds.Tables["DoorStep_do_fps"];
            GridView1.DataBind();
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = false;
            //string ddate = ;
            tx_do_date.Text = getdate(do_date.ToString());
            tx_do_validity.Text = changeDate(do_date, CheckNullInt(do_valid));
            Session["do_valid"] = do_valid;

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

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
   
    //void GetBalQty()
    //{
    //    try
    //    {
    //        string mcomid = ddl_commodity.SelectedItem.Value;
    //        string mscheme = ddl_scheme.SelectedItem.Value;
    //        string mgodown = ddl_godown.SelectedItem.Value;
    //        tx_cur_bags.Text = "0";
    //        tx_cur_bal.Text = "0";
    //        mobj1 = new MoveChallan(ComObj);
    //        string qry = "";
          
    //        if (ds == null)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
    //        }
    //        else
    //        {

    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
    //            }
    //            else
    //            {
    //                DataRow dr = ds.Tables[0].Rows[0];
    //                tx_cur_bags.Text = dr["Current_Bags"].ToString();
    //                tx_cur_bal.Text = dr["Current_Balance"].ToString();

    //            }
    //        }
    //    }
    //    catch (Exception)
    //    { }

    //}
 
    protected void get_comm()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "select distinct commodity,Commodity_Name from DoorStep_do_fps join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id=DoorStep_do_fps.commodity where delivery_order_no='" + ddl_do_no.SelectedValue.ToString() + "' and issueCentre_code='" + sid + "'";
            DataSet ds = mobj.selectAny(qry);

            ddl_commodity.DataSource = ds.Tables[0];
            ddl_commodity.DataTextField = "Commodity_Name";
            ddl_commodity.DataValueField = "commodity";
            ddl_commodity.DataBind();
            ddl_commodity.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        { }

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


    protected void btn_new_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";
        Response.Redirect("~/IssueCenter/DPY-IssueAgainstDo.aspx");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Session["issubmited"].ToString() == "Yes")
        {

        }
        else
        {

            DateTime dispdate = Convert.ToDateTime(DateTime.ParseExact(tx_issued_date.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

            string today_date = System.DateTime.Now.ToString("MM/dd/yyyy");

            dtt = Convert.ToDateTime(today_date);


            DateTime date1 = new DateTime(dtt.Year, dtt.Month, dtt.Day);

            DateTime date2 = new DateTime(dispdate.Year, dispdate.Month, dispdate.Day);

            int result1 = DateTime.Compare(date1, date2);


            string relationship = string.Empty;

            if (result1 >= 0)
            {


                decimal tot = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    if (((CheckBox)GridView1.Rows[i].FindControl("chkBoxId")).Checked == true)
                    {
                        {

                            tot = tot + decimal.Parse(GridView1.Rows[i].Cells[5].Text.ToString());
                            do_qunat = tot;


                        }

                    }
                }
                decimal issuedqty = 0;

                for (int i = 0; GridView2.Rows.Count > i; i++)
                {

                    issuedqty = issuedqty + decimal.Parse(GridView2.Rows[i].Cells[2].Text.ToString());
                    totalissuequnt = issuedqty;
                }

                if (totalissuequnt == do_qunat)
                {
                    for (int i = 0; GridView2.Rows.Count > i; i++)
                    {


                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        string strqr = "select sum(DoorStep_do_fps.quantity) as qunt from DoorStep_DO join DoorStep_do_fps on DoorStep_do_fps.delivery_order_no=DoorStep_DO.delivery_order_no and DoorStep_do_fps.district_code=DoorStep_DO.district_code where DoorStep_DO.delivery_order_no='" + ddl_do_no.SelectedValue.ToString() + "' and DoorStep_DO.district_code='" + distid + "' and commodity='" + GridView2.DataKeys[i].Values[0].ToString() + "' ";
                        cmd.CommandText = strqr;
                        cmd.Connection = con;
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        string doquntity = dr["qunt"].ToString();
                        decimal issuequnt = Convert.ToDecimal(GridView2.Rows[i].Cells[2].Text.ToString());
                        if (issuequnt > Convert.ToDecimal(doquntity) || issuequnt <= 0)
                        {

                            Label1.ForeColor = System.Drawing.Color.Red;
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity To Issue can not be greater than DO Quantity for commodities');</script>");
                            Label1.Text = "Error: Quantity To Issue can not be greater than DO Quantity for commodities";
                            return;
                        }
                        else
                        {


                        }


                        dr.Close();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity To Issue can not be lesser than DO Quantity for commodities');</script>");

                    Label1.Text = "Error: Quantity To Issue can not be lesser than DO Quantity";
                    return;
                }
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

                    //if (CheckNullInt(chk_valid_days) > CheckNullInt(do_valid_days))
                    //{
                    //    Label1.ForeColor = System.Drawing.Color.Red;
                    //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Validity  has Expired please check Issued Date ...');</script>");
                    //    Label1.Text = "Validity  has Expired please check Issued Date ...";
                    //}
                    if (CheckNullInt(chk_valid_days) < 0)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Issued Date can not be less than DO Date please check Issued Date ...');</script>");
                        Label1.Text = "Issued Date can not be less than DO Date please check Issued Date ...";
                        //return;
                    }
                    else
                    {

                        if (Convert.ToInt32(hd_transpoeter.Value) == 7 || Convert.ToInt32(hd_transpoeter.Value) == 10 || Convert.ToInt32(hd_transpoeter.Value) == 3)
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            string str1 = "update dbo.DoorStep_DO set Transporter='" + ddltransporter.SelectedValue.ToString() + "' where  issueCentre_code='" + sid + "' and delivery_order_no='" + ddl_do_no.SelectedValue.ToString() + "'";
                            cmd.CommandText = str1;
                            cmd.ExecuteNonQuery();
                            con.Close();

                        }
                        try
                        {

                            for (int i = 0; GridView2.Rows.Count > i; i++)
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
                                if (qunt == "1")
                                {

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
                                    string strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_Doorstep_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and issue_against_Doorstep_do.Transport_order is  null";
                                    cmd.CommandText = strqr;
                                    cmd.Connection = con;
                                    dr = cmd.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        docount = dr["rwcount"].ToString();
                                    }
                                    dr.Close();
                                    strqr = "select trans_id from dbo.issue_against_Doorstep_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and Transport_order is null";
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
                                        strqr = "select max(convert(int,right(trans_id,len(trans_id)-len(allotment_year)-len(district_code)-len(delivery_order_no)-3))) as rwcount  from dbo.issue_against_Doorstep_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and Transport_order is null ";
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
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }


                                        string mystr = "select* from issue_against_Doorstep_do Where delivery_order_no='" + do_no + "' AND district_code='" + dist + "' AND issueCentre_code='" + issue_centre_code + "' AND allotment_month='" + ddl_allot_month.SelectedItem.Value + "' AND allotment_year='" + ddd_allot_year.SelectedValue.ToString() + "' AND issue_to='" + Session["issueto"].ToString() + "' AND cmd='" + GridView2.DataKeys[i].Values[0].ToString() + "' AND qty_issue='" + GridView2.Rows[i].Cells[2].Text.ToString() + "' AND bags='" + GridView2.Rows[i].Cells[3].Text.ToString() + "' AND Source='" + GridView2.DataKeys[i].Values[1].ToString() + "' AND Godown='" + GridView2.DataKeys[i].Values[1].ToString() + "' AND gate_pass='" + GridView2.Rows[i].Cells[4].Text.ToString() + "'";
                                        cmd.Connection = con;
                                        cmd.CommandText = mystr;
                                        dr = cmd.ExecuteReader();


                                        if (dr.HasRows)
                                        {

                                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity Already Issued .....');</script>");

                                            Label1.Text = "Quantity Already Issued";
                                            return;


                                        }
                                        dr.Close();




                                        string IssuetoName = "FPS";
                                        string issue_to = "FPS";

                                        string str1 = "INSERT INTO dbo.issue_against_Doorstep_do(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,cmd,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Issue_to_LS_name,crop_year,Branch_id) VALUES('" + state + "','" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + GridView2.DataKeys[i].Values[0].ToString() + "','" + issue_to + "'," + GridView2.Rows[i].Cells[2].Text.ToString() + "," + GridView2.Rows[i].Cells[3].Text.ToString() + ",'" + GridView2.DataKeys[i].Values[2].ToString() + "','" + GridView2.DataKeys[i].Values[1].ToString() + "','" + issue_date + "','" + GridView2.Rows[i].Cells[4].Text.ToString() + "',getdate(),'','N','" + ip + "','" + opid + "','" + notrans + "',N'" + IssuetoName + "','" + ddlcropyear.SelectedValue.ToString() + "','" + ddl_branch.SelectedValue.ToString() + "')";
                                        cmd.CommandText = str1;
                                        cmd.ExecuteNonQuery();

                                        string strIssue = "INSERT INTO dbo.issue_against_do(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Issue_to_LS_name,commodity,DOType,Transporter_id,truck_no) VALUES('" + state + "','" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + issue_to + "'," + GridView2.Rows[i].Cells[2].Text.ToString() + "," + GridView2.Rows[i].Cells[3].Text.ToString() + ",'" + GridView2.DataKeys[i].Values[2].ToString() + "','" + GridView2.DataKeys[i].Values[1].ToString() + "','" + issue_date + "','" + GridView2.Rows[i].Cells[4].Text.ToString() + "',getdate(),'','N','" + ip + "','" + opid + "','" + notrans + "',N'" + IssuetoName + "','" + GridView2.DataKeys[i].Values[0].ToString() + "','DS','" + ddltransporter.SelectedValue.ToString() + "','" + GridView2.Rows[i].Cells[4].Text.ToString() + "')";
                                        cmd.CommandText = strIssue;
                                        cmd.ExecuteNonQuery();


                                        //GetSelected();
                                        //get_do_data();
                                        lift_qty = lift_qty + Convert.ToDecimal(GridView2.Rows[i].Cells[2].Text.ToString());
                                        str1 = "update dbo.sum_trans_do set lift_qty=round(" + lift_qty + ",5) where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                                        cmd.CommandText = str1;
                                        cmd.ExecuteNonQuery();
                                        str1 = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags-" + CheckNullInt(GridView2.Rows[i].Cells[3].Text.ToString()) + ",Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + Convert.ToDecimal(GridView2.Rows[i].Cells[2].Text.ToString()) + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)+" + Convert.ToDecimal(GridView2.Rows[i].Cells[2].Text.ToString()) + ",5) where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + GridView2.DataKeys[i].Values[1].ToString() + "'";
                                        cmd.CommandText = str1;
                                        cmd.ExecuteNonQuery();
                                        //update_Issue_Balance();  

                                        {
                                            int rcount = 0;
                                            string dist1 = distid;
                                            string issue_centre_code1 = sid;
                                            string ip1 = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                            for (int j = 0; j < GridView1.Rows.Count; j++)
                                            {

                                                if (((CheckBox)GridView1.Rows[j].FindControl("chkBoxId")).Checked == true)
                                                {


                                                    rcount = rcount + 1;
                                                    string fpscode = GridView1.Rows[j].Cells[1].Text.ToString();
                                                    string do_no1 = ddl_do_no.SelectedItem.Text;
                                                    string comm = GridView1.DataKeys[j].Values[0].ToString();
                                                    string scheme = GridView1.DataKeys[j].Values[1].ToString();
                                                    string blkcode = GridView1.Rows[j].Cells[10].Text.ToString();
                                                    decimal do_qty1 = CheckNull(GridView1.Rows[j].Cells[5].Text.ToString());
                                                    string comm_name = GridView1.Rows[j].Cells[3].Text.ToString(); ;
                                                    string scheme_name = GridView1.Rows[j].Cells[4].Text.ToString(); ;
                                                    //string ipadd = "";
                                                    //decimal qty_lift_old = 0;
                                                    //decimal qty_lift_new = 0;



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
                                                        if (comm_name.ToLower().Contains("Maize"))
                                                        {
                                                            comm_str = "Maize_AAY_lift";
                                                        }
                                                    }
                                                    if (comm_name.ToLower().Contains("sugar"))
                                                    {
                                                        comm_str = "sugar_lift";
                                                    }
                                                    if (comm_name.ToLower().Contains("salt"))
                                                    {
                                                        comm_str = "salt_lift";
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
                                                        if (comm_name.ToLower().Contains("maize"))
                                                        {
                                                            comm_str = "Maize_PHH_lift";
                                                        }
                                                    }



                                                    if (comm_str == "")
                                                    {

                                                    }


                                                    //else
                                                    //{
                                                    //string docount1 = "";
                                                    //int trnscnt1 = 0;
                                                    //if (comm_str != "")
                                                    //{
                                                    string ipadd = "";
                                                    decimal qty_lift_old = 0;
                                                    decimal qty_lift_new = 0;
                                                    string docount1 = "";
                                                    int trnscnt1 = 0;
                                                    if (comm_str != "")
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
                                                            qty_lift_new = qty_lift_old + 100 * do_qty1;
                                                            str1 = "update pds.fps_allot set " + comm_str + "=round(" + qty_lift_new + ",5)  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                                                            cmd_opdms.CommandText = str1;
                                                            cmd_opdms.ExecuteNonQuery();

                                                        }

                                                        string strr = "";
                                                        strr = "update pds.fps_allot_mpsc set " + comm_str + "=isnull(convert(decimal(18,5)," + comm_str + "),0)+round(" + 100 * do_qty1 + ",5),lft_updated_on=getdate()  where district_code='" + distid + "'  and  depot_code='" + sid + "' and  fps_code='" + fpscode + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
                                                        cmd.CommandText = strr;

                                                        cmd.ExecuteNonQuery();
                                                    }
                                                    trnscnt1 = 0;
                                                    docount1 = "";

                                                    string strqr1 = "select count(delivery_order_no) as rwcount  from dbo.Issued_Doorstep_do_fps where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and Transport_order is null";
                                                    cmd.CommandText = strqr1;
                                                    cmd.Connection = con;
                                                    dr = cmd.ExecuteReader();
                                                    while (dr.Read())
                                                    {
                                                        docount1 = dr["rwcount"].ToString();
                                                    }
                                                    dr.Close();
                                                    strqr1 = "select trans_id from dbo.issue_against_Doorstep_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and Transport_order is null";
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
                                                        strqr1 = "select max(convert(int,right(trans_id,len(trans_id)-len(allotment_year)-len(district_code)-len(delivery_order_no)-3))) as rwcount  from dbo.Issued_Doorstep_do_fps where delivery_order_no='" + do_no1 + "' and district_code='" + dist1 + "' and Transport_order is null";
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



                                                    if (i == 0)
                                                    {

                                                        str1 = "INSERT INTO dbo.Issued_DoorStep_do_fps(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add,issue_date,DeliverQuantity,DeliverDate,UpdatedDate)VALUES('" + state + "','" + transid + "','" + do_no1 + "','" + dist1 + "','" + issue_centre_code1 + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "',round(" + do_qty1 + ",5),round(" + do_qty1 + ",5),getdate(),'" + ip1 + "','" + issue_date + "','','','')";
                                                        cmd.CommandText = str1;
                                                        cmd.ExecuteNonQuery();
                                                        str1 = "INSERT INTO dbo.Issued_do_fps(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add,DOType,issue_date)VALUES('" + state + "','" + transid + "','" + do_no1 + "','" + dist1 + "','" + issue_centre_code1 + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "',round(" + do_qty1 + ",5),round(" + do_qty1 + ",5),getdate(),'" + ip1 + "','DS','" + issue_date + "')";
                                                        cmd.CommandText = str1;
                                                        cmd.ExecuteNonQuery();
                                                        str1 = "update dbo.DoorStep_do_fps set status='Y' where fps_code='" + fpscode + "' and delivery_order_no='" + do_no1 + "' and district_code='" + distid + "' and commodity='" + comm + "' and scheme_id='" + scheme + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                                                        cmd.CommandText = str1;
                                                        cmd.ExecuteNonQuery();


                                                    }
                                                }
                                            }

                                            if (totalissuequnt >= CheckNull(tx_balance_qty.Text))
                                            {
                                                string strdo = "update dbo.DoorStep_DO  set status='Y' where delivery_order_no='" + ddl_do_no.SelectedItem.Text + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "' and allotment_month=" + ddl_allot_month.SelectedItem.Value + " and allotment_year=" + ddd_allot_year.SelectedItem.Text + "";
                                                cmd.CommandText = strdo;
                                                cmd.ExecuteNonQuery();
                                            }
                                            string tempos = "NNN";
                                            string strope = "";
                                            strope = "select *  from dbo.tbl_Stock_Registor  where Commodity_Id ='" + GridView2.DataKeys[i].Values[0].ToString() + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                            cmd.CommandText = strope;
                                            dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                tempos = "YYY";
                                            }
                                            dr.Close();
                                            if (tempos == "YYY")
                                            {
                                                strope = "update dbo.tbl_Stock_Registor set Sale_Do=round(convert(decimal(18,5),Sale_Do)+" + GridView2.Rows[i].Cells[2].Text.ToString() + ",5) where Commodity_Id ='" + GridView2.DataKeys[i].Values[0].ToString() + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                                cmd.CommandText = strope;
                                                cmd.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                decimal opebal = 0;
                                                strope = "select sum(convert(decimal(18,5),Current_Balance)) as opebal  from dbo.issue_opening_balance  where Commodity_Id ='" + GridView2.DataKeys[i].Values[0].ToString() + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'";
                                                cmd.CommandText = strope;
                                                dr = cmd.ExecuteReader();
                                                while (dr.Read())
                                                {
                                                    opebal = CheckNull(dr["opebal"].ToString());
                                                }
                                                dr.Close();
                                                strope = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + GridView2.DataKeys[i].Values[0].ToString() + "','" + ddl_scheme.SelectedItem.Value + "'," + opebal + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + GridView2.Rows[i].Cells[2].Text.ToString() + "," + 0 + "," + 0 + "," + 0 + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'')";
                                                cmd.CommandText = strope;
                                                cmd.ExecuteNonQuery();
                                            }
                                            //
                                            tempos = "NNN";
                                            strope = "select *  from dbo.issue_opening_balance  where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + GridView2.DataKeys[i].Values[0].ToString() + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + GridView2.DataKeys[i].Values[1].ToString() + "' and Source='" + GridView2.DataKeys[i].Values[2].ToString() + "'";
                                            cmd.CommandText = strope;
                                            dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                tempos = "YYY";
                                            }
                                            dr.Close();
                                            if (tempos == "YYY")
                                            {
                                                strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + GridView2.Rows[i].Cells[2].Text.ToString() + ",5),Current_Bags=Current_Bags-" + GridView2.Rows[i].Cells[3].Text.ToString() + " where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + GridView2.DataKeys[i].Values[0].ToString() + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + GridView2.DataKeys[i].Values[1].ToString() + "' and Source='" + GridView2.DataKeys[i].Values[2].ToString() + "'";
                                                cmd.CommandText = strope;
                                                cmd.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                strope = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('23','" + distid + "','" + sid + "','" + GridView2.DataKeys[i].Values[0].ToString() + "','" + ddl_scheme.SelectedItem.Value + "','','" + GridView2.DataKeys[i].Values[1].ToString() + "','',0,0,'" + GridView2.DataKeys[i].Values[2].ToString() + "'," + -Convert.ToDecimal(GridView2.Rows[i].Cells[2].Text.ToString()) + "," + -CheckNullInt(GridView2.Rows[i].Cells[3].Text.ToString()) + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'" + ip1 + "',getdate(),getdate(),'','')";
                                                cmd.CommandText = strope;
                                                cmd.ExecuteNonQuery();
                                            }

                                        }

                                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                                        Label1.Text = "Data Saved Successfully ...";
                                        //ddl_do_no.SelectedIndex = 0;
                                        //panel_do.Enabled = false;
                                        btnsave.Enabled = false;
                                        Trans_csms.Commit();
                                        Trans_opdms.Commit();

                                    }
                                    catch (Exception ex)
                                    {
                                        dr.Close();
                                        dr_opdms.Close();
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
                                }

                                //tx_bags.Text = "";
                                //tx_gatepass.Text = "";
                                //ddl_do_no.SelectedIndex = 0;

                            }
                        }
                        catch (Exception ex)
                        {
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('TO already created for this month!...');</script>");


                        }
                    }
                }
            }

            else
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select correct date of issue');</script>");


            }
        }
    }
    
   
    protected void get_Transporter()
    {
        try
        {
            string dist = distid;
            ddltransporter.Items.Clear();
            cmd.CommandText = "SELECT distinct Transporter_ID,(Transporter_Name+',('+Transporter_ID+')') as Transporter_Name from Transporter_Table where transport_ID='7' and Distt_ID ='" + distid + "' order by Transporter_Name";
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
                lstitem.Value = dr["Transporter_ID"].ToString();
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
    void GetCapGodown()
    {
          //DateTime do_date = new DateTime();
        try
        {

            DateTime dodate = Convert.ToDateTime(DateTime.ParseExact(tx_do_date.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
            string Godown = ddl_godown.SelectedItem.Value;
            string openingdate;
            Int64 comid = Convert.ToInt64(Godown);
            if (dodate >= Convert.ToDateTime(DateTime.ParseExact("01/01/2015", "dd/MM/yyyy", null).ToString("MM/dd/yyyy")))
            {
                openingdate = "01/01/2015";
            }
            else
            {
                openingdate = "01/04/2014";
            }
            string pqry = "space_forcommodity_ingodown";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmdpqty = new SqlCommand(pqry, con);
            cmdpqty.CommandType = CommandType.StoredProcedure;


            cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = distid;
            cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = sid;
            cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = Godown;
            cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = ddl_commodity.SelectedValue.ToString();
            //cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
            cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

            dr.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

                double bags = Convert.ToDouble(ds.Tables[0].Rows[0]["Totalbags"].ToString());

                tx_cur_bal.Text = Convert.ToString(stock);
                tx_cur_bags.Text = Convert.ToString(bags);

                //txtcurntcap.Text = Convert.ToString(stock);
                //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

            }
        }
        catch
        {
        }

    }
    protected void Getbranch()
    {
        cons.Open();

        string qrysel = "select IssueCenterName, tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID=MetaDataBranchWithIssueCenter.DepotId where IssueCenterId='" + sid + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_branch.DataSource = ds.Tables[0];
                ddl_branch.DataTextField = "DepotName";
                ddl_branch.DataValueField = "BranchID";
                ddl_branch.DataBind();
                ddl_branch.Items.Insert(0, "--select--");
            }
        }
        else
        {
            string qrysel2 = " select BranchId,DepotName from  tbl_MetaData_DEPOT where DistrictId='23" + distid + "'";
            ddl_branch.DataSource = ds.Tables[0];
            ddl_branch.DataTextField = "DepotName";
            ddl_branch.DataValueField = "BranchId";
            ddl_branch.DataBind();
            ddl_branch.Items.Insert(0, "--select--");

        }
        cons.Close();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        
        Panel2.Visible = true;
        bool checkstatus = false;
        try
        {
            if (tx_qty_to_issue.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Issued Quantity to be added in Godown is required!'); </script> ");
                return;
            }
            else if (tx_bags.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Bags to be added in Godown is required!'); </script> ");
                return;
            }
            else if (ddl_godown.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown!'); </script> ");
                return;
            }
            else if ((tx_cur_bal.Text != "") && (Convert.ToDecimal(tx_cur_bal.Text) < Convert.ToDecimal(tx_qty_to_issue.Text)))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Insuffient Capacity in the Selected Godown!'); </script> ");
                return;
            }
            else if (ddlsarrival.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Source of arrival!'); </script> ");
                return;
            }
            else
            {
               
                    if (ddl_godown.Items.Count > 0)
                    {

                        btnsave.Enabled = true;
                        if (Session["dt1"] == null)
                        {
                            Dt1 = CreateTable();
                            Session["dt1"] = Dt1;
                        }
                        // adding rows to the datatable
                        DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                        ((DataTable)Session["dt1"]).AcceptChanges();
                        dr["Godown"] = ddl_godown.SelectedItem;
                        dr["qty_issue"] = tx_qty_to_issue.Text.Trim();
                        dr["bags"] =tx_bags.Text.Trim();
                        dr["gate_pass"] = tx_gatepass.Text.Trim();
                        dr["commodity"] = ddl_commodity.SelectedItem;

                        dr["commodityid"] = ddl_commodity.SelectedValue;
                        dr["Godownid"] = ddl_godown.SelectedValue;
                        dr["Source_ID"] = ddlsarrival.SelectedValue;
                       
                        if (GridView2.Rows.Count > 0)
                        {
                            int i;

                            // checking whether or not the godown is already added to the grid view
                            for (i = 0; i <= GridView2.Rows.Count - 1; i++)
                            {
                                string commodityid = GridView2.DataKeys[i].Values[0].ToString();
                                string selectcomm = ddl_commodity.SelectedValue.ToString();

                                string godownid = GridView2.DataKeys[i].Values[1].ToString();
                                string selectgodown = ddl_godown.SelectedValue.ToString();
                                if (godownid == selectgodown && commodityid == selectcomm)
                                {
                                    checkstatus = true;
                                }
                                else if (commodityid != selectcomm)
                                {
                                    checkstatus = true;
                                    lbl_messga.Visible = true;
                                }
                            }
                            if (checkstatus == false)
                            {
                                ((DataTable)Session["dt1"]).Rows.Add(dr);
                                ((DataTable)Session["dt1"]).AcceptChanges();
                                GridView2.DataSource = (DataTable)Session["dt1"];
                                GridView2.DataBind();
                                lbl_messga.Visible = false;
                         

                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select one commodity entry for one time or godown is already added for this commodity'); </script> ");
                            }
                        }
                        else
                        {
                            ((DataTable)Session["dt1"]).Rows.Add(dr);
                            ((DataTable)Session["dt1"]).AcceptChanges();
                            GridView2.DataSource = (DataTable)Session["dt1"];
                            GridView2.DataBind();
                      

                     
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Godown under the selected Commodity  Number'); </script> ");
                    }
              
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please enter valid quantity, try again'); </script> ");
        }

    }
  
    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn Godown = new DataColumn("Godown", Type.GetType("System.String"));
        DataColumn qty_issue = new DataColumn("qty_issue", Type.GetType("System.Decimal"));
        DataColumn bags = new DataColumn("bags", Type.GetType("System.Int32"));
        DataColumn truck = new DataColumn("gate_pass", Type.GetType("System.String"));
        DataColumn commodity = new DataColumn("commodity", Type.GetType("System.String"));
        DataColumn commodityid = new DataColumn("commodityid", Type.GetType("System.Int32"));

        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.Int64"));
        DataColumn Source_ID = new DataColumn("Source_ID", Type.GetType("System.String"));
        dt.Columns.Add(Godown);//Column is added to the DataTable
        dt.Columns.Add(qty_issue);//Column is added to the DataTable
        dt.Columns.Add(bags);//Column is added to the DataTable
        dt.Columns.Add(truck);//Column is added to the DataTable
      

        dt.Columns.Add(commodity);//Column is added to the DataTable
        dt.Columns.Add(commodityid);//Column is added to the DataTable
        dt.Columns.Add(Godownid);

        dt.Columns.Add(Source_ID);
        dt.AcceptChanges();
        return dt;
    }







    //protected void chkBoxId_CheckedChanged(object sender, EventArgs e)
    //{
         
       
    //    foreach (GridViewRow row in GridView1.Rows)
    //    {

    //        if (((CheckBox)row.FindControl("chk_del")).Checked == true)
    //        {
    //            ((CheckBox)row.FindControl("chk_receipt")).Enabled = true;
    //        }
    //        else {


    //            ((CheckBox)row.FindControl("chk_receipt")).Enabled = false;
    //        }

    //    }
         
    ////    }

    //}
    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
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
            GetCapGodown();
        }
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_comm();
    }
    protected void ddl_lead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    protected void GetGodown()
    {
        cons.Open();

        string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddl_branch.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_godown.DataSource = ds.Tables[0];
                ddl_godown.DataTextField = "Godown_Name";
                ddl_godown.DataValueField = "Godown_ID";
                ddl_godown.DataBind();
                ddl_godown.Items.Insert(0, "--select--");
            }
        }
        cons.Close();
    }
}