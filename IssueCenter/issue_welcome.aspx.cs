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
public partial class movementchallan : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string issueid = "";
    
    
    private DataReader DObj = null;
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
             string branchMgr="";
             if (Session["BranchManager"] != null)
             {
                 branchMgr = Session["BranchManager"].ToString();
             }
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                string httpf = Application["sPath"].ToString();
                string shtt = httpf + "/MainLogin.aspx";
                string st = Request.UrlReferrer.ToString();
                if (Session["BranchManager"] != null)
                {
                    grd_operater.Visible = true;
                }
                fillgrid();
                if (shtt == st)
                {
                    insertLog();

                }
                else
                {


                }
                if (branchMgr == "BM")
                {
                    btnLink_AddOperator.Visible = true;
                }
                chkDispatchDetails.Checked = false;
                chkDistributionDetails.Checked = false;
                chkRecieptDetails.Checked = false;
            }
            txt_Date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
       
    }
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select distinct OperatorName,Mobile from dbo.Operator_Registration where District_ID='" + distid + "' and DepotID='" + issueid + "'";
        DataSet ds = mobj.selectAny(qrey);
        if (ds == null)
        {
        }
        else
        {
           grd_operater.DataSource = ds.Tables[0];
           grd_operater.DataBind();
        }


    }
    
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    void insertLog()
    {
        string of = "False";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString ();
        string hh = DateTime.Now.Hour.ToString();
        string mm = DateTime.Now.Minute.ToString();
        string ss = DateTime.Now.Second.ToString();
        string time = hh + ":" + mm + ":" + ss;
        string logtype = "1";

        string qry = "insert into dbo.User_LogTime(Login_Type,user_id,IP_Address,Login_Date,Login_Time,offline)values('" + logtype + "','" + issueid + "','" + ip + "',getdate(),'" + time + "','" + of + "')";
        cmd.Connection = con;
        cmd.CommandText = qry;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();         
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();
        }


    }

    void UpdateBalance()
    {
       // int month = int.Parse(DateTime.Today.Month.ToString());
       // int year = int.Parse(DateTime.Today.Year.ToString());

       //string gmonth="Select Month,Year from dbo.issue_opening_balance"
    }
    protected void btnLink_AddOperator_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/OperatorRegistration.aspx");
    }
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        if (txt_Date.Text != "")
        {
            if (chkRecieptDetails.Checked)
            {
               // bool resRecieptProc = InsertRecieptProcurementNoTrans();
                bool resRecieptAll = InsertRecieptAllNoTrans();
                bool resRecieptRailSugar = InsertRecieptRailSugarNoTrans();
                bool resRecieptLosGain = InsertRecieptLossGain();
                chkRecieptDetails.Enabled = false;
            }
            if (chkDispatchDetails.Checked)
            {
                bool resDispatchTruck = InsertDispatchTruckChallan();
                bool resDispatchRail = InsertDispatchRailHead();
                chkDispatchDetails.Enabled = false;
            }
            if (chkDistributionDetails.Checked)
            {
                bool resDistributeDeliveryOrder = InsertDistributeDeliveryOrder();
                bool resDistributeIssueAgainstDO = InsertDistributeIssueAgainstDO();
                chkDistributionDetails.Enabled = false;
            }
            txt_Date.Text = "";
            chkDispatchDetails.Checked = false;
            chkDistributionDetails.Checked = false;
            chkRecieptDetails.Checked = false;
            chkRecieptDetails.Enabled = false;
            chkDispatchDetails.Enabled = false;
            chkDistributionDetails.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No transaction entered Sucessfully for the date'); </script> ");
            
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
    }

    private bool InsertDistributeIssueAgainstDO()
    {
        string distid = "";
        string depotid = "";
        bool res = false;
        string gatepass = "";

        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            gatepass = "NoTrans" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + depotid;
            string divno = "NoDO" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + depotid;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string state = Session["State_Id"].ToString();
            string notrans = "Y";
            string opid = Session["OperatorId"].ToString();
            string qryinsert = "insert into dbo.issue_against_do(State_Id,trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,qty_issue,bags,issue_date,created_date,ip_add,OperatorID,NoTransaction) values('" + state + "','" + gatepass + "','" + divno + "','" + distid + "','" + depotid + "'," + int.Parse(DateTime.Now.Month.ToString()) + "," + int.Parse(DateTime.Now.Year.ToString()) + ",0.0,0,'" + date + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";
            cmd.CommandText = qryinsert;
            cmd.Connection = con;
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
        return res;
    }

    private bool InsertDistributeDeliveryOrder()
    {
        string distid = "";
        string depotid = "";
        bool res = false;
        MoveChallan mobj1 = null;
        string gatepass = "";
        
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            //mobj1 = new MoveChallan(ComObj);
            //string qrey = "select delivery_order_no from dbo.delivery_order_mpscsc where do_date='" + date + "' and issueCentre_code='" + depotid + "' and district_code='" + distid + "'";
            //DataSet ds = mobj1.selectAny(qrey);
            //DataRow dr = ds.Tables[0].Rows[0];
            //gatepass = dr["delivery_order_no"].ToString();
            if (gatepass == "")
            {
                gatepass = "NoDO" + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + depotid;
            }            
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string state = Session["State_Id"].ToString();
            string notrans = "Y";
            string opid = Session["OperatorId"].ToString();
            int month = int.Parse(date.Substring(0, 2).ToString());
            int year = int.Parse(date.Substring(6, 4).ToString());
            string qryinsert = "insert into dbo.delivery_order_mpscsc(State_Id,district_code,issueCentre_code,delivery_order_no,allotment_month,allotment_year,do_validity,permit_validity,do_date,created_date,dd_no,amount,IP,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + depotid + "','" + gatepass + "'," + month + "," + year + "," + 0 + "," + 0 + ",'" + date + "',getDate(),'" + gatepass + "',0,'" + ip + "','" + opid + "','" + notrans + "')";
            cmd.CommandText = qryinsert;
            cmd.Connection = con;
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
        return res;
    }

    private bool InsertDispatchRailHead()
    {
        string distid = "";
        string depotid = "";
        bool res = false;
        MoveChallan mobj1 = null;
        mobj1 = new MoveChallan(ComObj);
        string gatepass = "";
        Int64 getnum;
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj1 = new MoveChallan(ComObj);
            string qrey = "select max(Dispatch_id) as Dispatch_id from dbo.SCSC_RailHead_TC where Depot_Id='" + depotid + "' and Dist_ID='" + distid + "'";
            DataSet ds = mobj1.selectAny(qrey);
            DataRow dr = ds.Tables[0].Rows[0];
            gatepass = dr["Dispatch_id"].ToString();
            if (gatepass == "")
            {
                gatepass = issueid + "01";
            }
            else
            {
                getnum = Convert.ToInt32(gatepass);
                getnum = getnum + 1;
                gatepass = getnum.ToString();
            }
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string state = Session["State_Id"].ToString();
            string notrans = "Y";
            string opid = Session["OperatorId"].ToString();
            string challanno = "NoChallan" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString();
            string qryinsert = "insert into dbo.SCSC_RailHead_TC(State_Id,Dist_ID,Depot_Id,Challan_Date,Challan_No,Dispatch_id,Created_date,IP_Address,Source,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + depotid + "','" + date + "','" + challanno + "','" + gatepass + "',getDate(),'"+ip+"','NoSource','" + opid + "','" + notrans + "')";
            cmd.CommandText = qryinsert;
            cmd.Connection = con;
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
        return res;
    }

    private bool InsertDispatchTruckChallan()
    {
        string distid = "";
        string depotid = "";
        string gatepass = "";
        Int64 getnum;
        bool res=false;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        MoveChallan mobj1 = null;
         mobj1 = new MoveChallan(ComObj);
         string date = getDate_MDY(txt_Date.Text.ToString().Trim());
         if (txt_Date.Text == "")
         {
             Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
         }
         else
         {
             string qreychal = "select Challan_Date from dbo.SCSC_Truck_challan where Depot_Id='" + depotid + "' and Dist_ID='" + distid + "' and Challan_Date='" + date + "'";
             DataSet dsc = mobj1.selectAny(qreychal);
             if (dsc.Tables[0].Rows.Count == 0)
             {
                 mobj1 = new MoveChallan(ComObj);
                 string qrey = "select max(Dispatch_id) as Dispatch_id from dbo.SCSC_Truck_challan where Depot_Id='" + depotid + "' and Dist_ID='" + distid + "'";
                 DataSet ds = mobj1.selectAny(qrey);
                 DataRow dr = ds.Tables[0].Rows[0];
                 gatepass = dr["Dispatch_id"].ToString();
                 if (gatepass == "")
                 {
                     gatepass = depotid + "01";

                 }
                 else
                 {
                     getnum = Convert.ToInt32(gatepass);
                     getnum = getnum + 1;
                     gatepass = getnum.ToString();

                 }
                 string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                 string state = Session["State_Id"].ToString();
                 string notrans = "Y";
                 string opid = Session["OperatorId"].ToString();
                 string challanno = "NoChallan" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()+DateTime.Now.Minute.ToString();
                 string qryinsert = "insert into dbo.SCSC_Truck_challan(State_Id,Dist_ID,Depot_Id,Challan_Date,Challan_No,Created_date,IP_Address,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + depotid + "','" + date + "','"+challanno+"',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";
                 cmd.CommandText = qryinsert;
                 cmd.Connection = con;
                 try
                 {
                     con.Open();
                     int count = cmd.ExecuteNonQuery();
                     if (count > 0)
                     {
                         res = true;
                     }
                 }
                 catch (Exception ex)
                 {
                 }
                 finally
                 {
                     con.Close();
                 }
             }
         }
        return res;
    }

    private bool InsertRecieptLossGain()
    {
        string distid = "";
        string depotid = "";
        SqlDataReader dr;
        bool res = false;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            int trnscnt = 0;
            string rowcount = "";
            string strqr = "select count(Depotid) as rwcount  from dbo.Loss_gain  where Depotid='" + depotid + "' and District_Id='" + distid + "'";
            cmd.CommandText = strqr;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rowcount = dr["rwcount"].ToString();
            }
            dr.Close();
            con.Close();
            if (rowcount != "")
            {
                trnscnt = CheckNullInt(rowcount);
            }
            trnscnt = trnscnt + 1;
            string transid = depotid.ToString() + (trnscnt).ToString();
            string state = Session["State_Id"].ToString();
            string notrans = "Y";
            string opid = Session["OperatorId"].ToString();            
            string qryinsert = "INSERT INTO Loss_gain(State_Id,trans_id,District_Id,Depotid,Commodity_Id,Scheme_Id,Godown,Source,stock_type,IP_Address,CreatedDate,OperatorID,NoTransaction) values('" + state + "','" + transid + "','" + distid + "','" + depotid + "','NoC','NoScheme','NoGodown','NoSource','N','" + ip + "',getDate(),'" + opid + "','" + notrans + "')";
            cmd.CommandText = qryinsert;
            cmd.Connection = con;
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
        return res;
    }

    private bool InsertRecieptRailSugarNoTrans()
    {
        bool res = false;
        string distid = "";
        string depotid = "";
        MoveChallan mobj1 = null;
        string transid = "";
        Int64 railnum;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            string date = getDate_MDY(txt_Date.Text.ToString().Trim());
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string qry = "SELECT TC_Number from dbo.RR_receipt_Depot where district_code='" + distid + "'and DepotID='" + depotid + "' and TC_date='" + date + "'and Month=" + month + " and Year=" + year;
            DataSet dschk = mobj.selectAny(qry);
            if (dschk.Tables[0].Rows.Count == 0)
            {
                mobj = new MoveChallan(ComObj);
                string qrey = "select max(Trans_ID) as Trans_ID  from dbo.RR_receipt_Depot where district_code='" + distid + "' and Month =" + month + "and Year=" + year;
                DataSet ds = mobj.selectAny(qrey);
                DataRow dr = ds.Tables[0].Rows[0];
                transid = dr["Trans_ID"].ToString();
                string mmonth = month.ToString();


                if (transid == "")
                {
                    transid = distid + mmonth + "001";

                }
                else
                {
                    railnum = Convert.ToInt32(transid);
                    railnum = railnum + 1;
                    transid = railnum.ToString();
                }

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string opid = Session["OperatorId"].ToString();
                string Challan_st = "N";
                string Tcnumbr = "NoTrans";
                string qryinsert = "Insert into  dbo.RR_receipt_Depot(State_Id,district_code,DepotID,TC_Number,TC_date,Created_date,Ip_Address,Challan_st,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + depotid + "','" + Tcnumbr + "','" + date + "',getDate(),'" + ip + "','" + Challan_st + "','" + opid + "','" + notrans + "')";
                cmd.Connection = con;
                cmd.CommandText = qryinsert;
                try
                {
                    con.Open();
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        res = true;
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    con.Close();
                }
            }
        }
        return res;
    }

    private bool InsertRecieptAllNoTrans()
    {
        bool res = false;
        string distid = "";
        string depotid = "";
        MoveChallan mobj1 = null;
        string gatepass = "";
        Int64 getnum;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj1 = new MoveChallan(ComObj);
            string date = getDate_MDY(txt_Date.Text.ToString().Trim());
            string checkchallan = "Select arrival_date from dbo.tbl_Receipt_Details where Dist_Id='" + depotid + "' and  Depot_ID='" + distid + "' and arrival_date='" + date + "'";
            DataSet dschallan = mobj1.selectAny(checkchallan);
            if (dschallan.Tables[0].Rows.Count == 0)
            {
                mobj1 = new MoveChallan(ComObj);
                string month = date.Substring(0, 2);
                string qrey = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + depotid + "' and Dist_Id='" + distid + "'";
                DataSet ds = mobj1.selectAny(qrey);
                if (ds == null)
                {
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    gatepass = dr["Receipt_id"].ToString();
                    if (gatepass == "")
                    {
                        string issue = depotid.Substring(2, 5);
                        gatepass = issue + month.ToString() + "001";

                    }
                    else
                    {
                        getnum = Convert.ToInt64(gatepass);
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string state = Session["State_Id"].ToString();
                    string notrans = "Y";
                    string opid = Session["OperatorId"].ToString();
                    string isdeposit = "N";
                    string sourceofarrival = "No Arrival";
                    string qryInsert = "insert into dbo.tbl_Receipt_Details(State_Id,Dist_Id,Depot_ID,Receipt_id,S_of_arrival,arrival_date,challan_no,IsDeposit,IP_Address,Created_date,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + depotid + "','" + gatepass + "','" + sourceofarrival + "','" + date + "','No Challan','" + isdeposit + "','" + ip + "',getDate(),'" + opid + "','" + notrans + "')";

                    cmd.CommandText = qryInsert;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            res = true;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
        return res;
    }

    private bool InsertRecieptProcurementNoTrans()
    {
        bool res = false;
        MoveChallan mobj1 = null;
        string depotid="";
        string distid="";
        string gatepass="";
        Int64 getnum;
         if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            string date=getDate_MDY(txt_Date.Text.ToString().Trim());
            string select = "Select Recd_Date  from SCSC_Procurement where Distt_ID='" + distid + "' and IssueCenter_ID='" + depotid + "' and Recd_Date='" + date + "'";
            mobj = new MoveChallan(ComObj);
            DataSet dsgdn = mobj.selectAny(select);
            if (dsgdn.Tables[0].Rows.Count == 0)
            {
                mobj1 = new MoveChallan(ComObj);
                string qrey = "select max(convert(bigint,Receipt_Id)) as Receipt_Id from dbo.SCSC_Procurement where IssueCenter_ID='" + depotid + "' and Distt_ID='" + distid + "'";
                DataSet ds = mobj1.selectAny(qrey);
                DataRow dr = ds.Tables[0].Rows[0];
                gatepass = dr["Receipt_Id"].ToString();
                if (gatepass == "")
                {
                    gatepass = depotid + "001";

                }
                else
                {
                    getnum = Convert.ToInt64(gatepass);
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
                }
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string opid = Session["OperatorId"].ToString();
                string anst = "N";
                string mstatus = "N";
                string qryinsert = "insert into dbo.SCSC_Procurement(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,Acceptance_No,Recd_Date,Receipt_Id,Status_Deposit,Created_Date,IP_Address,AN_Status,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + depotid + "',"+000+",'" + date + "','NoAcceptanceNo','" + date + "','" + gatepass + "','" + mstatus + "',getDate(),'" + ip + "','" + anst + "','" + opid + "','" + notrans + "')";
                cmd.CommandText = qryinsert;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        res = true;
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    con.Close();
                }
            }
          }
        return res;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (txt_Date.Text != "")
        {
            //for Reciept Check
            string date = getDate_MDY(txt_Date.Text.ToString().Trim());
            //int record = CheckRecieptProcurement(date);
            int recOtherFCIAll = CheckRecieptAll(date);
            int recRailSugar = CheckRecieptRailSugar(date);
            int recLossGain = CheckLossGain(date);
            if (recOtherFCIAll == 0 && recRailSugar == 0 && recLossGain == 0)
            {
                chkRecieptDetails.Enabled = true;
            }
            //Reciept End 

            //for Dispatch Check
            int recordDispatch = CheckDispatchTruckChallan(date);
            int recDispatchRail = CheckDispatchRailHead(date);
            if (recordDispatch == 0 && recDispatchRail == 0) 
            {
                chkDispatchDetails.Enabled = true;
            }
            //End Dispatch

            //for Distrtribution Check
            int recordDistrbuteissuDO = CheckDistributeIssueAgainstDO(date);
            int recDistributeDO = CheckDistributeDeliveryOrder(date);
            if (recordDistrbuteissuDO == 0 && recDistributeDO == 0)
            {
                chkDistributionDetails.Enabled = true;
            }
            //End Distrtribution
        }
    }

    private int CheckRecieptProcurement(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[SCSC_Procurement] where Distt_ID='" + distid + "' and IssueCenter_ID='" + depotid + "' and Recd_Date='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
    }
   

    private int CheckLossGain(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[Loss_gain] where District_Id='" + distid + "' and Depotid='" + depotid + "' and CreatedDate='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
    }

    private int CheckRecieptRailSugar(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[RR_receipt_Depot] where district_code='" + distid + "' and DepotID='" + depotid + "' and TC_date='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
    }

    private int CheckRecieptAll(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[tbl_Receipt_Details] where Dist_Id='" + distid + "' and Depot_ID='" + depotid + "' and arrival_date='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
    }
    private int CheckDistributeDeliveryOrder(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[delivery_order_mpscsc] where district_code='" + distid + "' and issueCentre_code='" + depotid + "' and do_date='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
    }

    private int CheckDistributeIssueAgainstDO(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[issue_against_do] where district_code='" + distid + "' and issueCentre_code='" + depotid + "' and issue_date='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
    }

    private int CheckDispatchTruckChallan(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[SCSC_Truck_challan] where Dist_ID='" + distid + "' and Depot_Id='" + depotid + "' and Challan_Date='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
    }

    private int CheckDispatchRailHead(string date)
    {
        string distid = "";
        string depotid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (Session["issue_id"].ToString() != "")
        {
            depotid = Session["issue_id"].ToString();
        }
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[SCSC_RailHead_TC] where Dist_ID='" + distid + "' and Depot_Id='" + depotid + "' and Challan_Date='" + date + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                record = Convert.ToInt32(ds.Tables[0].Rows[0]["record"].ToString());
            }
        }
        return record;
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
}
