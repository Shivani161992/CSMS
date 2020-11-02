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
public partial class Dist_Welcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string issueid = "";
    public string gatepass = "";
    public int getnum;
    private DataReader DObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            string DistMgr = "";
            if (Session["DistrictManager"] != null)
            {
                DistMgr = Session["DistrictManager"].ToString();
            }
            if (Session["Collector/DIO"] != null)
            {
                if (Session["Collector/DIO"].ToString() == "Collector" || Session["Collector/DIO"].ToString() == "DIO")
                {
                    Panel1.Visible = false;
                }
            }
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                GetRackAllert();
                Fillgrid();
                 string httpf = Application["sPath"].ToString();
                 string shtt = httpf + "/MainLogin.aspx";
                 string st = Request.UrlReferrer.ToString ();;
                if (shtt==st)
                {
                    insertLog();
                }
                else
                {
                   
                }
                if (DistMgr == "DM")
                {
                    btnLink_AddOperator.Visible = true;
                }
                else
                {
                    btnLink_AddOperator.Visible = false;

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
    void GetRackAllert()
    {
        string qryr = "select Rack_No,districtsmp.district_name,Recd_RailHead,tbl_Rail_Head.RailHead_Name from Rack_Receipt_Bulk left join pds.districtsmp on districtsmp.district_code=Rack_Receipt_Bulk.district_code left join dbo.tbl_Rail_Head on Rack_Receipt_Bulk.Recd_RailHead=tbl_Rail_Head.RailHead_Code  where Sending_District='" + distid + "' and  Rack_No not in(select Rack_No from tbl_RackMaster where district_code='" + distid + "')";
        mobj = new MoveChallan(ComObj);
        DataSet dsrac = mobj.selectAny(qryr);
        if (dsrac == null)
        {
            
        }
        else
        {
            if (dsrac.Tables[0].Rows.Count == 0)
            {
                lblrack.Visible = false;
                Label1.Visible = false;
                Label1.Text = "Currently no record found....";
            }
            else
            {
                lblrack.Visible = true;
                Label1.Visible = false;
                GridView1.DataSource = dsrac;
                GridView1.DataBind();
            }

          
        }

        
    }
    public string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    } 

    void insertLog()
    {
        string of = "False";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
       string hh = DateTime.Now.Hour.ToString();
        string mm = DateTime.Now.Minute.ToString();
        string ss = DateTime.Now.Second.ToString();
        string time = hh + ":" + mm + ":" + ss;
        string logtype = "2";

        string qry = "insert into dbo.User_LogTime(Login_Type,user_id,IP_Address,Login_Date,Login_Time,offline)values('" + logtype + "','" + distid + "','" + ip + "',getdate(),'" + time + "','" + of + "')";
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
    void Fillgrid()
    {


        string qrey = "SELECT tbl_MetaData_DEPOT.DepotName,TO_Number ,Sendto_District,Sendto_IC,Commodity,Scheme,Sum(Bags) as Bags ,Sum(Qty_send) as Qty_send,FDepot.DepotName as SDepot,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name    FROM  SCSC_Truck_challan left join tbl_MetaData_DEPOT on SCSC_Truck_challan.Sendto_IC=tbl_MetaData_DEPOT.DepotID left join tbl_MetaData_DEPOT as FDepot on  SCSC_Truck_challan.Depot_Id=FDepot.DepotID left join tbl_MetaData_STORAGE_COMMODITY on SCSC_Truck_challan.Commodity=tbl_MetaData_STORAGE_COMMODITY.Commodity_ID left join tbl_MetaData_SCHEME on SCSC_Truck_challan.Scheme =tbl_MetaData_SCHEME.Scheme_ID where  SCSC_Truck_challan.Dist_ID='" + distid + "' and SCSC_Truck_challan.TO_Number  not in (Select Distinct(TO_Number)as TO_Number from  Transport_Order where Distt_ID='" + distid + "' and Source_ID='02' ) and SCSC_Truck_challan.TO_Number!=''  group by TO_Number ,Sendto_District,Sendto_IC,Commodity,Scheme,tbl_MetaData_DEPOT.DepotName,FDepot.DepotName,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name";
        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds, "Tables[0]");
        if (ds.Tables[0].Rows.Count == 0)
        {
            lblto.Visible = false;
        }
        else
        {
            lblto.Visible = true;
            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();
        }


    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Sorce_ID"] = "02";
        Session["Source"] = "Other Source";
        Response.Redirect("~/District/Transport_Order_Other.aspx");
    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnLink_AddOperator_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/OperatorRegistration.aspx");
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (txt_Date.Text != "")
        {
            //for Dispatch Check
            string date = getDate_MDY(txt_Date.Text.ToString().Trim());
            int record = CheckDispatchROofFCI(date);
            int recgenTO = CheckDispatchGenTO(date);
            int recgenTOFCI = CheckDispatchGenTOFCI(date);
            int recliftRO = ChechkDispatchLiftinAgaRO(date);
            int recRackDisp = CheckRackDispatch(date);
            //End Dispatch
            if (record == 0 && recgenTO == 0 && recgenTOFCI == 0 && recliftRO == 0 && recRackDisp == 0)
            {
                chkDispatchDetails.Enabled = true;
            }
            //for Distribution Check
            int recDO = CheckDistributionDO(date);
            int reissueAgainDO = CheckDistributionIssueAgaDO(date);
            //End Distribution
            if (recDO == 0 && reissueAgainDO == 0)
            {
                chkDistributionDetails.Enabled = true;
            }
            //for Reciept Check
            int recRackReciept = CheckRackRecieptSugarandOwnDistrict(date);
            if (recRackReciept == 0)
            {
                chkRecieptDetails.Enabled = true;
            }
        }
    }

    private int CheckRackRecieptSugarandOwnDistrict(string date)
    {
        string distid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }

        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[Rack_Receipt_Bulk] where district_code='" + distid + "' and Rack_RecdDate='" + date + "'";
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
    

    private int CheckDistributionIssueAgaDO(string date)
    {
        string distid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }

        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[issue_against_do] where district_code='" + distid + "' and issue_date='" + date + "'";
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

    private int CheckDistributionDO(string date)
    {
        string distid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }

        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[delivery_order_mpscsc] where district_code='" + distid + "' and issue_type='FCI' and do_date='" + date + "'";
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

    private int CheckDispatchGenTOFCI(string date)
    {
        string distid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }

        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[Transport_Order_againstRo] where Distt_Id='" + distid + "' and TO_Date='" + date + "'";
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

    private int ChechkDispatchLiftinAgaRO(string date)
    {
        string distid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }

        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[Lift_A_RO] where Dist_Id='" + distid + "' and Challan_Date='" + date + "'";
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

    private int CheckDispatchGenTO(string date)
    {
        string distid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }

        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[Transport_Order] where Distt_Id='" + distid + "' and TO_Date='" + date + "'";
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

    private int CheckDispatchROofFCI(string date)
    {
        string distid = "";        
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        
        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[RO_of_FCI] where Distt_Id='" + distid + "' and RO_date='" + date + "'";
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

    private int CheckRackDispatch(string date)
    {
        string distid = "";
        int record = 0;
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }

        DObj = new DataReader(ComObj);
        string qry = "Select count(*) as record from [dbo].[Rack_Dispatch_Details] where district_code='" + distid + "' and Challan_date='" + date + "'";
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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    
    
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        if (txt_Date.Text != "")
        {
            if (chkDispatchDetails.Checked)
            {
                bool resDispatchGenTOFCI = InsertDispatchGenTOFCI();
                bool resDispatchGenTO = InsertDispatchGenTO();
                bool resDispatchROofFCI = InsertDispatchROofFCI();
                bool resDispatchLiftinAgaRO = InsertDispatchLiftinAgaRO();
                bool resRackDispatch = InsertRackDispatch();
                chkDispatchDetails.Enabled = false;
            }
            if (chkDistributionDetails.Checked)
            {
                bool resDistributionDO = InsertDistributionDO();
                bool resDistributionIssueAgaDO = InsertDistributionIssueAgaDO();
                chkDistributionDetails.Enabled = false;
            }
            if (chkRecieptDetails.Checked)
            {
                bool resRackRecieptSugarandOwnDistrict = InsertRackRecieptSugarandOwnDistrict();
                chkRecieptDetails.Enabled = false;
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

    private bool InsertRackRecieptSugarandOwnDistrict()
    {
        bool res = false;
        string distid = "";
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj = new MoveChallan(ComObj);
            string qreychal = "select Rack_RecdDate from dbo.Rack_Receipt_Bulk where district_code='" + distid + "' and Rack_RecdDate='" + date + "'";
            DataSet dsc = mobj.selectAny(qreychal);
            if (dsc.Tables[0].Rows.Count == 0)
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string opid = Session["OperatorIDDM"].ToString();
                string qryinsert = "insert into dbo.Rack_Receipt_Details(State_Id,district_code,Rack_No,Challan_No,Challan_date,Created_date,OperatorID,NoTransaction) values('" + state + "','" + distid + "','NoRack','NoChallan','" + date + "',getDate(),'" + opid + "','" + notrans + "')";
                string qryins = "insert into dbo.Rack_Receipt_Bulk(State_Id,district_code,Rack_RecdDate,Created_Date,IP_Address,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + date + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";

                cmd.CommandText = qryinsert;
                cmd.Connection = con;
                cmd1.CommandText = qryins;
                cmd1.Connection = con;
                try
                {
                    con.Open();
                    int count = cmd.ExecuteNonQuery();
                    int count1 = cmd1.ExecuteNonQuery();
                    if (count > 0 && count1 > 0)
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

    private bool InsertDistributionIssueAgaDO()
    {
        string distid = "";
        
        bool res = false;
        string gatepass = "";

        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            gatepass = "NoTrans" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Hour.ToString() +distid;
            string divno = "NoDO" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Hour.ToString()+distid;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string state = Session["State_Id"].ToString();
            string notrans = "Y";
            string opid = Session["OperatorIDDM"].ToString();
            string qryinsert = "insert into dbo.issue_against_do(State_Id,trans_id,delivery_order_no,district_code,allotment_month,allotment_year,qty_issue,bags,issue_date,created_date,ip_add,OperatorID,NoTransaction) values('" + state + "','" + gatepass + "','" + divno + "','" + distid + "'," + int.Parse(DateTime.Now.Month.ToString()) + "," + int.Parse(DateTime.Now.Year.ToString()) + ",0.0,0,'" + date + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";
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

    private bool InsertDistributionDO()
    {
        string distid = "";
       
        bool res = false;
        
        string gatepass = "";

        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            //mobj = new MoveChallan(ComObj);
            //string qrey = "select delivery_order_no from dbo.delivery_order_mpscsc where do_date='" + date + "' and district_code='" + distid + "' and issue_type='FCI'";
            //DataSet ds = mobj.selectAny(qrey);
            //DataRow dr = ds.Tables[0].Rows[0];
            //gatepass = dr["delivery_order_no"].ToString();
            if (gatepass == "")
            {
                gatepass = "NoDO" + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + distid;
            }
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string state = Session["State_Id"].ToString();
            string notrans = "Y";
            string opid = Session["OperatorIDDM"].ToString();
            string issuecode = "Nocode";
            int month = int.Parse(date.Substring(0, 2).ToString());
            int year = int.Parse(date.Substring(6, 4).ToString());
            string qryinsert = "insert into dbo.delivery_order_mpscsc(State_Id,district_code,issueCentre_code,delivery_order_no,issue_type,allotment_month,allotment_year,do_validity,permit_validity,do_date,created_date,dd_no,amount,IP,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + issuecode + "','" + gatepass + "','FCI',"+month+","+year+","+0+","+0+",'" + date + "',getDate(),'" + gatepass + "',0,'" + ip + "','" + opid + "','" + notrans + "')";
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

    private bool InsertRackDispatch()
    {
        bool res = false;
        string distid = "";
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj = new MoveChallan(ComObj);
            string qreychal = "select Challan_date from dbo.Rack_Dispatch_Details where district_code='" + distid + "' and Challan_date='" + date + "'";
            DataSet dsc = mobj.selectAny(qreychal);
            if (dsc.Tables[0].Rows.Count == 0)
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string rnum = "NoRack";
                string cnum = "NoChallan";
                string opid = Session["OperatorIDDM"].ToString();
                string qryinsert = "insert into dbo.Rack_Dispatch_Details(State_Id,district_code,Rack_No,Challan_No,Challan_date,Created_date,OperatorID,IP,NoTransaction) values('" + state + "','" + distid + "','" + rnum + "','" + cnum + "','" + date + "',getDate(),'" + opid + "','" + ip + "','" + notrans + "')";
                string qryins = "insert into dbo.Rack_Dispatch_Bulk(State_Id,district_code,Rack_No,Created_Date,IP_Address,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + rnum + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";

                cmd.CommandText = qryinsert;
                cmd.Connection = con;
                cmd1.CommandText = qryins;
                cmd1.Connection = con;
                try
                {
                    con.Open();
                    int count = cmd.ExecuteNonQuery();
                    int count1=cmd1.ExecuteNonQuery();
                    if (count > 0 && count1>0)
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

    private bool InsertDispatchLiftinAgaRO()
    {
        bool res = false;
        string distid = "";
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj = new MoveChallan(ComObj);
            string qreychal = "select Challan_Date from dbo.Lift_A_RO where Dist_Id='" + distid + "' and Challan_Date='" + date + "'";
            DataSet dsc = mobj.selectAny(qreychal);
            if (dsc.Tables[0].Rows.Count == 0)
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string ronum = "NoRO";
                string opid = Session["OperatorIDDM"].ToString();
                string qryinsert = "insert into dbo.Lift_A_RO(State_Id,Dist_Id,RO_No,Challan_Date,Created_Date,IP_Address,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + ronum + "','" + date + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";
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

    private bool InsertDispatchROofFCI()
    {
        bool res = false;
        string distid = "";
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj = new MoveChallan(ComObj);
            string qreychal = "select RO_date from dbo.RO_of_FCI where Distt_Id='" + distid + "' and RO_date='" + date + "'";
            DataSet dsc = mobj.selectAny(qreychal);
            if (dsc.Tables[0].Rows.Count == 0)
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string ronum = "NoRO";
                string opid = Session["OperatorIDDM"].ToString();
                string qryinsert = "insert into dbo.RO_of_FCI(State_Id,Distt_Id,RO_No,RO_date,Created_date,IP_Address,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + ronum + "','" + date + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";
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

    private bool InsertDispatchGenTO()
    {
        bool res = false;
        string distid = "";
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj = new MoveChallan(ComObj);
            string qreychal = "select TO_Date from dbo.Transport_Order where Distt_Id='" + distid + "' and TO_Date='" + date + "'";
            DataSet dsc = mobj.selectAny(qreychal);
            if (dsc.Tables[0].Rows.Count == 0)
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string transid = "NoTrans" + distid + DateTime.Now.Minute.ToString();
                string tonum = "NoTO";
                string opid = Session["OperatorIDDM"].ToString();
                string qryinsert = "insert into dbo.Transport_Order(State_Id,Distt_Id,TO_Number,TO_Date,Trunsuction_Id,Created_date,IP,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + tonum + "','" + date + "','" + transid + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";
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

    private bool InsertDispatchGenTOFCI()
    {
        bool res = false;
        string transuct = "";
        string distid = "";
        long transnum = 0;
        int month = int.Parse(DateTime.Today.Month.ToString());
        string date = getDate_MDY(txt_Date.Text.ToString().Trim());
        if (Session["dist_id"].ToString() != "")
        {
            distid = Session["dist_id"].ToString();
        }
        if (txt_Date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the date for No transaction'); </script> ");
        }
        else
        {
            mobj = new MoveChallan(ComObj);
            string qrey = "select max(Trunsuction_Id) as Trunsuction_Id from dbo.Transport_Order_againstRo where Distt_Id='" + distid + "' and Month=" + month + "";
            DataSet ds = mobj.selectAny(qrey);
            if (ds == null)
            {

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                transuct = dr["Trunsuction_Id"].ToString();
                if (transuct == "")
                {

                    transuct = distid + month.ToString() + "0001";

                }
                else
                {
                    transnum = Convert.ToInt64(transuct);
                    transnum = transnum + 1;
                    transuct = transnum.ToString();
                }
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string state = Session["State_Id"].ToString();
                string notrans = "Y";
                string ronum = "NoRO";
                string tonum = "NoTO";
                string opid = Session["OperatorIDDM"].ToString();
                string qryinsert = "insert into dbo.Transport_Order_againstRo(State_Id,Distt_Id,RO_No,TO_Number,TO_Date,Trunsuction_Id,Created_date,IP_Address,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + ronum + "','" + tonum + "','" + date + "','" + transuct + "',getDate(),'" + ip + "','" + opid + "','" + notrans + "')";
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
}
