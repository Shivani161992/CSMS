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
using System.Data.Sql;

public partial class IssueCenter_Delete_DeliveryOrder : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd_opdms = new SqlCommand();
    SqlDataReader dr;
   
    protected Common ComObj = null, cmn = null;
    string distid = "";
    string sid = "";
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

        Label1.Visible = false;

        btndelete.Enabled = true;
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();

        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Page.IsPostBack == false)
        {
            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            get_do_no();
            //GetGodown();
            get_comm();
            get_scheme();
            //GetSource();
            get_lead();
           
        }
    }

    protected void get_do_no()
    {
        try
        {
            Label1.Text = "";
          
            tx_balance_qty.Text = "";
            tx_do_qty.Text = "";
            tx_do_validity.Text = "";
            tx_issue_balqty.Text = "";
            tx_issued_qty.Text = "";
            tx_issueto.Text = "";
            tx_lead.Text = "";
            string dist = distid;
            ddl_do_no.Items.Clear();

            cmd.CommandText = "select delivery_order_no from dbo.delivery_order_mpscsc where district_code='" + dist + "' and issueCentre_code='" + sid + "' and issue_type <> 'FCI' and delivery_order_no not like '%NoDO%' order by  delivery_order_no";
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
        {
        
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
            lstitem1.Text = " ";
            lstitem1.Value = "N";
            ddl_commodity.Items.Insert(0, lstitem1);

            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }

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
        ddl_scheme.Items.Insert(0, " ");


        dr.Close();
        con.Close();
    }

    

    protected void get_lead()
    {
        try
        {
            string dist = distid;
            ddl_lead.Items.Clear();
            cmd.CommandText = "select * from dbo.m_LeadSoc where District_code='" + distid + "'";
            cmd.Connection = con_opdms;
            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }
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
            if (con_opdms.State == ConnectionState.Open)
            {
                con_opdms.Close();
            }
        }
        catch (Exception)
        { }
    }

    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_do_no.SelectedItem.Text == "Select")
            {
                Label1.Text = "";
              
                tx_balance_qty.Text = "";
                tx_do_qty.Text = "";
                tx_do_validity.Text = "";
                tx_issue_balqty.Text = "";
                tx_issued_qty.Text = "";
                tx_issueto.Text = "";
                tx_lead.Text = "";
               
                tx_do_date.Text = "";
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Please Select Delivery Order No. ...";
               
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            }
            else
            {
                
                get_do_data();
            }
        }
        catch (Exception)
        { }

    }

    protected void get_do_data()
    {
        try
        {
            btndelete.Enabled = true;
            Label1.ForeColor = System.Drawing.Color.Blue;
          
            Label1.Text = "";
                 
            string do_no = ddl_do_no.SelectedItem.Text;
            string dist = distid;
            string issue_centre_code = sid;
          
            string issueqty = "";
            string issueto_name = "";
            string do_valid = "";
            DateTime do_date = new DateTime();
            string do_qty = "";
            string lead_code = "";
            cmd.CommandText = "SELECT delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,round(convert(decimal(18,5),delivery_order_mpscsc.quantity),5) as quantity,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity,delivery_order_mpscsc.issue_type,round(SUM(convert(decimal(18,5),issue_against_do.qty_issue)),5) AS issueqty FROM dbo.delivery_order_mpscsc LEFT JOIN dbo.issue_against_do ON delivery_order_mpscsc.delivery_order_no = issue_against_do.delivery_order_no AND delivery_order_mpscsc.district_code = issue_against_do.district_code and delivery_order_mpscsc.issueCentre_code = issue_against_do.issueCentre_code GROUP BY delivery_order_mpscsc.delivery_order_no,delivery_order_mpscsc.issue_name,delivery_order_mpscsc.issueCentre_code, delivery_order_mpscsc.district_code, delivery_order_mpscsc.do_date, delivery_order_mpscsc.do_validity, delivery_order_mpscsc.issue_type,delivery_order_mpscsc.quantity,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year,delivery_order_mpscsc.commodity_id,delivery_order_mpscsc.scheme_id  having delivery_order_mpscsc.delivery_order_no='" + do_no + "' and delivery_order_mpscsc.district_code='" + dist + "' and delivery_order_mpscsc.issueCentre_code='" + issue_centre_code + "'";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                do_date = DateTime.Parse(dr["do_date"].ToString());
                do_valid = dr["do_validity"].ToString();
                issueto_name = dr["issue_type"].ToString();
                do_qty = dr["quantity"].ToString();
                issueqty = dr["issueqty"].ToString();
                lead_code = dr["issue_name"].ToString();
                ddl_allot_month.SelectedValue = dr["allotment_month"].ToString();
                try
                {
                    ddd_allot_year.SelectedValue = dr["allotment_year"].ToString();
                }
                catch (Exception)
                { }
                ddl_commodity.SelectedValue = dr["commodity_id"].ToString();
                ddl_scheme.SelectedValue = dr["scheme_id"].ToString();
            }

            dr.Close();
            con.Close();
            Session["issueto"] = lead_code;
            if (issueto_name == "L" || issueto_name == "LF")
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

            if (issueto_name == "F")
            {
                tx_issueto.Text = "FPS";
                ddl_lead.Visible = false;
                tx_lead.Visible = false;
                tx_lead.Text = "";
            }
            if (issueto_name == "O")
            {
                ddl_lead.Visible = false;
                tx_lead.Visible = true;
                tx_issueto.Text = "Others";
                tx_lead.Text = lead_code;

            }
            if (issueqty == "")
            {
                issueqty = "0";
            }

            SqlDataAdapter da = new SqlDataAdapter("select do_fps.*,opdms.pds.fps_master.fps_Uname,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME .Scheme_Name,round(convert(decimal(18,5),do_fps.quantity),5) as qty,round(convert(decimal(18,5),rate_per_qtls),2) as rateqtls,round(convert(decimal(18,5),do_fps.quantity)*convert(decimal(18,5),rate_per_qtls),2) as amt from dbo.do_fps LEFT JOIN opdms.pds.fps_master ON do_fps.fps_code=fps_master.fps_code  LEFT JOIN dbo.tbl_MetaData_STORAGE_COMMODITY ON do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN dbo.tbl_MetaData_SCHEME  ON do_fps.scheme_id = tbl_MetaData_SCHEME .Scheme_Id where do_fps.delivery_order_no='" + do_no + "' and do_fps.district_code='" + dist + "' and do_fps.issueCentre_code='" + issue_centre_code + "' and do_fps.allotment_month=" + ddl_allot_month.SelectedItem.Value + " and do_fps.allotment_year=" + ddd_allot_year.SelectedItem.Text + " and do_fps.status='N'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "do_fps");
           
            tx_do_date.Text = getdate(do_date.ToString());
            tx_do_validity.Text = changeDate(do_date, CheckNullInt(do_valid));
            Session["do_valid"] = do_valid;
            
           
            decimal qty = CheckNull(do_qty) - CheckNull(issueqty);
            tx_balance_qty.Text = System.Math.Round(qty, 5).ToString();
            tx_issue_balqty.Text = System.Math.Round(qty, 5).ToString();
           

            tx_do_qty.Text = do_qty;
            tx_issued_qty.Text = issueqty;
        }
        catch (Exception)
        { }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Delete_DeliveryOrder.aspx");
    }

    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_do_no();
    }


    protected void btndelete_Click(object sender, EventArgs e)
    {
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

        else
        {
            string dist = distid;
            string issue_centre_code = sid;

            string do_no = ddl_do_no.SelectedItem.Text;
           
            decimal do_qty = CheckNull(tx_balance_qty.Text);
           
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            try
            {
                Int32 bags = 0;
                string Godownnum = "";
                string Source = "";            

                # region sum_trans_do

                string chek = "select count(*) from sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                SqlCommand cmdpre = new SqlCommand(chek, con);
                string y = cmdpre.ExecuteScalar().ToString();

                int a = Convert.ToInt16(y);

                if (a > 0)
                {
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

                    lift_qty = lift_qty - CheckNull(tx_issued_qty.Text);

                    string inslog = "Insert into sum_trans_do_Log select * from sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                    cmd.CommandText = inslog;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string str1 = "update dbo.sum_trans_do set lift_qty=round(" + lift_qty + ",5) where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                }


                # endregion

                # region Current_Godown_Position


                string getbags = "select Sum(bags)bags,Godown,Source from issue_against_do where district_code = '" + dist + "' and delivery_order_no = '" + do_no + "'group by Godown,Source";
                SqlCommand cmdbags = new SqlCommand(getbags, con);

                SqlDataAdapter da = new SqlDataAdapter(cmdbags);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {

                }

                else
                {
                    bags = Convert.ToInt32(ds.Tables[0].Rows[0]["bags"].ToString());
                    Godownnum = ds.Tables[0].Rows[0]["Godown"].ToString();
                    Source = ds.Tables[0].Rows[0]["Source"].ToString();

                    string GodownLog = "Insert into Current_Godown_Position_Log select * from Current_Godown_Position where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + Godownnum + "'";
                    cmd.CommandText = GodownLog;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    string str11 = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags + " + bags + ",Current_Balance=round(convert(decimal(18,5),Current_Balance) +" + CheckNull(tx_issued_qty.Text) + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)-" + CheckNull(tx_issued_qty.Text) + ",5) where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + Godownnum + "'";
                    cmd.CommandText = str11;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }

                # endregion             

                # region Update_tblStockRegister

                string tempos = "NNN";
                string strope = "";
                strope = "select *  from dbo.tbl_Stock_Registor  where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                cmd.CommandText = strope;
                cmd.Connection = con;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tempos = "YYY";
                }
                dr.Close();
                if (tempos == "YYY")
                {
                    string StockLog = "Insert into tbl_Stock_Registor_Log (DistrictId ,DepotID  ,Commodity_ID  ,Scheme_ID  ,Opening_Balance  ,Recieved_Procure  ,Recieved_Otherg   ,Recieved_FCI  ,Received_OtherSch   ,Recieved_Other_Src   ,Received_RailHead  ,Received_CMR   ,Received_Levy  ,Sale_Do  ,Sale_otherg  ,Transfer_OtherSch  ,Month   ,Year  ,Remarks ,DispatchType ,Crop_Year ,DispatchID)  Select DistrictId ,DepotID ,Commodity_ID ,Scheme_ID ,Opening_Balance ,Recieved_Procure ,Recieved_Otherg ,Recieved_FCI ,Received_OtherSch ,Recieved_Other_Src  ,Received_RailHead ,Received_CMR ,Received_Levy ,Sale_Do ,Sale_otherg  ,Transfer_OtherSch ,Month ,Year  ,Remarks  ,DispatchType ,Crop_Year, DispatchID from tbl_Stock_Registor where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist + "'and DepotID='" + issue_centre_code + "'and Month= '" + DateTime.Today.Month + "'and Year= '" + DateTime.Today.Year + "'"; 
                    cmd.CommandText = StockLog;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    strope = "update dbo.tbl_Stock_Registor set Sale_Do=round(convert(decimal(18,5),Sale_Do)-" + CheckNull(tx_issued_qty.Text) + ",5) where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                    cmd.CommandText = strope;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }




                # endregion

                # region update_OpeningBalance

                tempos = "NNN";
                strope = "select *  from dbo.issue_opening_balance  where District_Id='" + dist + "'and Depotid='" + issue_centre_code + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + Godownnum + "' and Source='" + Source + "'";
                cmd.CommandText = strope;
                cmd.Connection = con;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tempos = "YYY";
                }
                dr.Close();
                if (tempos == "YYY")
                {
                    strope = "Insert into issue_opening_balance_Log select * from issue_opening_balance  where District_Id='" + dist + "'and Depotid='" + issue_centre_code + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + Godownnum + "' and Source='" + Source + "'";
                    cmd.CommandText = strope;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)+" + CheckNull(tx_issued_qty.Text) + ",5),Current_Bags=Current_Bags+" + bags + " where District_Id='" + dist + "'and Depotid='" + issue_centre_code + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + Godownnum + "' and Source='" + Source + "'";
                    cmd.CommandText = strope;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }


                # endregion

                # region issue_against_do

                string cheprelog = "select count(*) from issue_against_do where district_code = '" + dist + "' and delivery_order_no = '" + do_no + "'";
                SqlCommand cmdprelog = new SqlCommand(cheprelog, con);
                string x = cmdprelog.ExecuteScalar().ToString();

                int chlog = Convert.ToInt16(x);
               
                if (chlog > 0)
                {
                    string Insert_issue_against_do_Log = "Insert into issue_against_do_log select * from issue_against_do where district_code = '" + dist + "' and delivery_order_no = '" + do_no + "'";
                    SqlCommand cmdLog = new SqlCommand(Insert_issue_against_do_Log, con);

                    int logdetail = cmdLog.ExecuteNonQuery();


                    string str1 = "Delete from issue_against_do where district_code = '" + dist + "' and delivery_order_no = '" + do_no + "'";
                    cmd.CommandText = str1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                }

                # endregion

                # region Delete_DeliveryOrder_table

                string LogDO = "Insert into delivery_order_mpscsc_log select * from delivery_order_mpscsc where delivery_order_no='" + ddl_do_no.SelectedItem.Text + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "'";
                cmd.CommandText = LogDO;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string strdo = "delete FROM dbo.delivery_order_mpscsc where delivery_order_no='" + ddl_do_no.SelectedItem.Text + "' and district_code='" + distid + "' and issueCentre_code='" + sid + "'";
                cmd.CommandText = strdo;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                # endregion

                btndelete.Enabled = false;
                Label1.Text = "Data Delete Successfully ...";
                Label1.Visible = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Delete Successfully ...');</script>");
            }

            catch
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Error on Deletion ...');</script>");
            }

            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

        }
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
}
