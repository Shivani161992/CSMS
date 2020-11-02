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
public partial class DistrictFood_Rack_master : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    Commodity_MP comdtobj =null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string stid= "";
    public string getdatef = "";
    public string hname = "";
    chksql chk = null;
    public string transid = "";
    public int railnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            stid = Session["st_id"].ToString();

            //txtrackno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtrackno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtrackno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrackno.Attributes.Add("onchange", "return chksqltxt(this)");
            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrackno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           


            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtrackno.Text);
            ctrllist.Add(DaintyDate3.Text);
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

            if (!IsPostBack)
            {
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                GetState();
                //GetName();
                GetSourceDist();
                fillgrid();
                GetCommodity();
                GetDist();
               
                GetRackAllert();
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
    //void GetName()
    //{
    //    mobj = new MoveChallan(ComObj);
    //    string qryd = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
    //    DataSet dsd = mobj.selectAny(qryd);
    //    if (dsd.Tables[0].Rows.Count==0)
    //    {
    //    }
    //    else
    //    {
    //        DataRow drd = dsd.Tables[0].Rows[0];

    //        txtdistrict.Text = drd["district_name"].ToString();
    //        txtdistrict.ReadOnly = true;

    //    }

    //}
    void GetRailHead()
    {
        mobj = new MoveChallan(ComObj);
        string qrygrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + ddlsourcedist.SelectedValue + "'";
        DataSet dsd = mobj.selectAny(qrygrh);
        ddlsengrailhead.DataSource = dsd.Tables[0];
        ddlsengrailhead.DataTextField ="RailHead_Name";
        ddlsengrailhead.DataValueField ="RailHead_Code";
        ddlsengrailhead.DataBind();
        ddlsengrailhead.Items.Insert(0, "--Select--");
        
      

    }
    void GetState()
    {
        mobj = new MoveChallan(ComObj);
        string qryms = "select *  from dbo.State_Master order by State_Name ";
        DataSet dsms = mobj.selectAny(qryms);
        if (dsms == null)
        {
        }
        else
        {

            ddlstate.DataSource = dsms.Tables[0];
            ddlstate.DataTextField = "State_Name";
            ddlstate.DataValueField = "State_Code";
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, "--Select--");
        }

    }
    void GetPlace()
    {
        string st = ddlstate.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qryms = "select distinct(Place_code) as Place_code,Place_Name  from dbo.Sugar_Places where State_code=" + st + " order by Place_name";
        DataSet dsms = mobj.selectAny(qryms);
        if (dsms == null)
        {
        }
        else
        {

            ddlplace.DataSource = dsms.Tables[0];
            ddlplace.DataTextField = "Place_Name";
            ddlplace.DataValueField = "Place_Code";
            ddlplace.DataBind();
            ddlplace.Items.Insert(0, "--Select--");
           
        }

    }
    void GetFactory()
    {
        string st = ddlstate.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qryms = "select distinct(Factory_Code) as Factory_Code,Factory_Name  from dbo.Sugar_Places where State_code=" + st + " and Place_code='" + ddlplace.SelectedValue + "' order by Factory_Name";
        DataSet dsms = mobj.selectAny(qryms);
        if (dsms == null)
        {
        }
        else
        {

            ddlfactory.DataSource = dsms.Tables[0];
            ddlfactory.DataTextField = "Factory_Name";
            ddlfactory.DataValueField = "Factory_Code";
            ddlfactory.DataBind();
            ddlfactory.Items.Insert(0, "--Select--");
          
        }

    }
    void GetDestRH()
    {

        mobj = new MoveChallan(ComObj);
        string qrydrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + ddldesdistt .SelectedValue  + "'";
        DataSet dsdh = mobj.selectAny(qrydrh);

        ddldesrailhead.DataSource = dsdh.Tables[0];
        ddldesrailhead.DataTextField = "RailHead_Name";
        ddldesrailhead.DataValueField = "RailHead_Code";
        ddldesrailhead.DataBind();
        ddldesrailhead.Items.Insert(0, "--Select--");
    }
   
    void GetDist()
    {
        mobj = new MoveChallan(ComObj);
        string qrydist = "select district_name,district_code  from pds.districtsmp  order by district_name ";
        DataSet dsd = mobj.selectAny(qrydist);

        ddldesdistt.DataSource = dsd.Tables[0];
        ddldesdistt.DataTextField = "district_name";
        ddldesdistt.DataValueField = "district_code";
        ddldesdistt.DataBind();
        ddldesdistt.Items.Insert(0, "--Select--");

    }
    void GetSourceDist()
    {
         
        mobj = new MoveChallan(ComObj);
        string qrydist = "select district_name,district_code  from pds.districtsmp  order by district_name ";
        DataSet dsd = mobj.selectAny(qrydist);

        ddlsourcedist.DataSource = dsd.Tables[0];
        ddlsourcedist.DataTextField = "district_name";
        ddlsourcedist.DataValueField = "district_code";
        ddlsourcedist.DataBind();
        ddlsourcedist.Items.Insert(0, "--Select--");

    }
    void GetCommodity()
    {

        ddlcommodity.Items.Clear();
        comdtobj = new Commodity_MP(ComObj);
        string selcom = "Select * from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (23,17) and Status='Y' order by Commodity_Id";
        DataSet ds = comdtobj.selectAny(selcom);

        ddlcommodity.DataSource = ds.Tables[0];
        ddlcommodity.DataTextField = "Commodity_Name";
        ddlcommodity.DataValueField = "Commodity_Id";
        ddlcommodity.DataBind();
        ddlcommodity.Items.Insert(0, "--Select--");


    }
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select  tbl_RackMaster.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name ,districtsmp.district_Name,tbl_Rail_Head.RailHead_Name ,RailHead_Dest.RailHead_Name as DestRName from dbo.tbl_RackMaster left join pds.districtsmp on tbl_RackMaster.Dest_District=districtsmp.district_code left join dbo.tbl_MetaData_STORAGE_COMMODITY  on tbl_RackMaster.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID  left join dbo.tbl_Rail_Head on tbl_RackMaster.Dest_RailHead=tbl_Rail_Head.RailHead_Code left join  dbo.tbl_Rail_Head as RailHead_Dest on tbl_RackMaster.RailHead=RailHead_Dest.RailHead_Code   where tbl_RackMaster.district_code='" + distid + "'";
        DataSet ds = mobj.selectAny(qrey);
         if (ds.Tables[0].Rows.Count==0)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Rack_DispDate"));
           

            getdatef = getdate(griddate);



            Label lbl = (Label)e.Row.FindControl("lbldisdate");
            lbl.Text = getdatef;
                   


        }



    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        if (GridView1.PageCount == 0)
        {
        }
        else
        {
            //Used by external paging
            string arg;
            arg = e.CommandArgument.ToString();

            switch (arg)
            {
                case "next":
                    //The next Button was Clicked
                    if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
                    {
                        GridView1.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((GridView1.PageIndex > 0))
                    {
                        GridView1.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    GridView1.PageIndex = (GridView1.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    GridView1.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            fillgrid();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridView1.SelectedRow.BackColor = System.Drawing.Color.Wheat;
        //txtrailhead.BackColor = System.Drawing.Color.Wheat;
        //hname = GridView1.SelectedRow.Cells[1].Text.Trim();
        //txtrailhead.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
        //txtrailhead.Focus();
        //btnadd.Visible = false;
        //btnupdate.Visible = true;





    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/State/State_Welcome.aspx");
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }

    void GetRackAllert()
    {
        //string qryr = "select Rack_Receipt_Bulk.Rack_No,Rack_Receipt_Bulk.district_code,districtsmp.district_name,Recd_RailHead,tbl_Rail_Head.RailHead_Name from Rack_Receipt_Bulk left join pds.districtsmp on districtsmp.district_code=Rack_Receipt_Bulk.district_code left join dbo.tbl_Rail_Head on Rack_Receipt_Bulk.Recd_RailHead=tbl_Rail_Head.RailHead_Code  where Sending_District='" + distid + "' and  Rack_No not in(select Rack_No from tbl_RackMaster where district_code='" + distid + "')";
        //mobj = new MoveChallan(ComObj);
        //DataSet dsrac = mobj.selectAny(qryr);
        //if (dsrac == null)
        //{
        //    ddlrackno.Visible = false; ;
        //    lblrackno.Visible =false ;
        //    lbltctrackno.Visible = true;
        //    txtrackno.Visible = true;
        //}
        //else
        //{
        //    if (dsrac.Tables[0].Rows.Count == 0)
        //    {
        //        //Label2.Visible = false;

        //    }
        //    else
        //    {
               
        //        ddlrackno.Visible = true;
        //        lblrackno.Visible = true;
        //        lbltctrackno.Visible = false;
        //        txtrackno.Visible = false;
        //        ddlrackno.DataSource = dsrac;
        //        ddlrackno.DataTextField = "Rack_No";
        //        ddlrackno.DataValueField = "Rack_No";
        //        ddlrackno.DataBind();
        //        ddlrackno.Items.Insert(0, "--Select--");
        //        ddlrackno.Items.Insert(1, "Not Indicated");

        //    }
        //}
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string state = Session["State_Id"].ToString();
        string opid = "H";

        string distid = ddlsourcedist.SelectedValue;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());

        mobj = new MoveChallan(ComObj);
        string qrey = "select max(Trans_ID) as Trans_ID  from dbo.Rake_Master_Sugar where district_code='" + distid + "' and Month =" + month;
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

        mobj = new MoveChallan(ComObj);
        string rackchk = txtrackno.Text;
        string qreyrac = "select Rack_No  from dbo.Rake_Master_Sugar where district_code='" + distid + "' and Rack_No='" + rackchk + "'";
        DataSet dsrac = mobj.selectAny(qreyrac);
        if (dsrac.Tables[0].Rows.Count == 0)
        {
            string mracno = txtrackno.Text;
            string msendrh = ddlsengrailhead.SelectedValue;
            string mdesrh = ddldesrailhead.SelectedValue;
            string mddistt = ddldesdistt.SelectedValue;
            string crdate = DateTime.Today.Date.ToString();
            string udate = "";
            string mdisdate = getDate_MDY(DaintyDate3.Text);
            string mcommdty = ddlcommodity.SelectedValue;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string st = "N";
            string qryinsert = "Insert into  dbo.Rake_Master_Sugar(State_Id,district_code,recd_state,recd_place,recd_factory,Rack_No,RailHead,Rack_DispDate,Commodity_ID,Trans_ID,Month,Year,Created_Date,Updated_Date,IP_Address,IsSend,ModeOperatorID,)values('" + state +"','"+  distid + "','" + ddlstate.SelectedValue + "','" + ddlplace.SelectedValue + "','" + ddlfactory.SelectedValue + "','" + mracno + "','" + msendrh + "','" + mdisdate + "','" + mcommdty + "','" + transid + "'," + month + "," + year + ",getdate(),'" + udate + "','" + ip + "','" + st + "','S','"+ opid +"')";
            cmd.Connection = con;
            cmd.CommandText = qryinsert;
            SqlTransaction trns;
            con.Open();
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;
            try
            {

                cmd.ExecuteNonQuery();
                trns.Commit();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved  Successfully.....'); </script> ");
                btnSubmit.Enabled = false;

            }
            catch (Exception ex)
            {
                trns.Rollback();
                Label1.Text = ex.Message;
            }
            finally
            {
                con.Close();
                ComObj.CloseConnection();
            }

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rake Number Already Exist.....'); </script> ");

        }
     


      


    }
    void GetRackDetails()

    {
       
        string qryr = "select Rack_Receipt_Bulk.Rack_No,Rack_Receipt_Bulk.district_code,districtsmp.district_name,Recd_RailHead,tbl_Rail_Head.RailHead_Name from Rack_Receipt_Bulk left join pds.districtsmp on districtsmp.district_code=Rack_Receipt_Bulk.district_code left join dbo.tbl_Rail_Head on Rack_Receipt_Bulk.Recd_RailHead=tbl_Rail_Head.RailHead_Code  where Sending_District='" + distid + "' and Rack_No='" + txtrackno.Text + "'";
        mobj = new MoveChallan(ComObj);
        DataSet dsrac = mobj.selectAny(qryr);
        if (dsrac == null)
        {
        }
        else
        {
            if (dsrac.Tables[0].Rows.Count == 0)
            {
                lbltctrackno.Visible = true;
                txtrackno.Visible = true;
                ddldesrailhead.Enabled = true;
                ddldesdistt.Enabled = true;
                GetDist();
                GetDestRH();

            }
            else
            {
                lbltctrackno.Visible = false ;
                txtrackno.Visible =false ;
                DataRow drrack = dsrac.Tables[0].Rows[0];
                ddldesdistt.SelectedValue = drrack["district_code"].ToString();
                ddldesdistt.Enabled = false;

                mobj = new MoveChallan(ComObj);
                string qrydrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + drrack["district_code"].ToString() + "'";
                DataSet dsdh = mobj.selectAny(qrydrh);

                ddldesrailhead.DataSource = dsdh.Tables[0];
                ddldesrailhead.DataTextField = "RailHead_Name";
                ddldesrailhead.DataValueField = "RailHead_Code";
                ddldesrailhead.DataBind();
                ddldesrailhead.Items.Insert(0, "--Select--");

                ddldesrailhead.SelectedValue = drrack["Recd_RailHead"].ToString();
                ddldesrailhead.Enabled = false;


            }
        }
    }
    protected void ddldesdistt_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDestRH();
    }
    protected void ddlsengrailhead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    
    protected void txtrackno_TextChanged(object sender, EventArgs e)
    {
        GetRackDetails();
    }
    protected void ddlsourcedist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRailHead();
    }
    protected void ddlplace_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFactory();
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPlace();
    }
    protected void ddlfactory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
