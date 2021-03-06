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
public partial class District_Edit_Rack_Receipt_Dtl : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    MoveChallan mobj = null;
    Commodity_MP comdtobj = null;
    DistributionCenters distobj = null;
    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string getdatef = "";
    public string hname = "";

    public string transid = "";
    public int railnum;
    DataTable dt = new DataTable();
    float disqty = 0;
    float chkqty = 0;
    public string rhcode = "";
    public string challan = "";
    public string rackno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            challan = Session["challan"].ToString();
            rackno = Session["rackno"].ToString();
            txtdisbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtdisqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecdqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtrecdqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");



            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


            if (!IsPostBack)
            {
                GetDist();
                GetTransport();
                
                GetDestDist();
                //GetDCName();
                GetRecddist();
                
                ddlrecddistrict.SelectedValue = distid;
                GetData();
                GetBalRQty();
                GetRackQty();
                GetRackRecdQty();
                GetDCNameAll();
                //dt.Columns.Add("district_code");
                //dt.Columns.Add("Rack_No");
                //dt.Columns.Add("IC_ID");
                //dt.Columns.Add("IC_Name");
                //dt.Columns.Add("Challan_No");
                //dt.Columns.Add("Challan_date");
                //dt.Columns.Add("Transporter_ID");
                //dt.Columns.Add("Transporter_Name");
                //dt.Columns.Add("Truck_No");
                //dt.Columns.Add("Disp_Bags");
                //dt.Columns.Add("Disp_Qty");
             

                Session["dt"] = dt;
             
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }

    void GetRack()
    {
        ddlrackno.Items.Clear();
        string sdist = ddldistrict.SelectedValue;

        //int month = int.Parse(DateTime.Today.Month.ToString());
        //int year = int.Parse(DateTime.Today.Year.ToString());

        //ddlrackno.Items.Insert(0, "--Select--");
        //string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + sdist + "' and Dest_District='" + distid + "'";
        //cmd.Connection = con;
        //cmd.CommandText = qreyrac;
        //con.Open();
        //dr = cmd.ExecuteReader();
        //while (dr.Read())
        //{
        //    ddlrackno.Items.Add(dr["Rack_No"].ToString());

        //}
        //dr.Close();
        //con.Close();

        string mrack =  txtrackno.Text;
        mobj = new MoveChallan(ComObj);

        string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + sdist + "' and Dest_District='" + distid + "'";
        DataSet ds = mobj.selectAny(qreyrac);
        if (ds.Tables[0].Rows.Count == 0)
        {
         
            ListItem lst = new ListItem();
            lst.Text = "Not Indicated";
            lst.Value = "0";
            ddlrackno.Items.Insert(0, "--Select--");
            ddlrackno.Items.Insert(1, lst);


        }
        else
        {
            ddlrackno.Items.Clear();
            ddlrackno.DataSource = ds.Tables[0];
            ddlrackno.DataTextField = "Rack_No";
            ddlrackno.DataValueField = "Rack_No";
            ddlrackno.DataBind();
            ddlrackno.Items.Insert(0, "--Select--");
            ddlrackno.Items.Insert(1, "Not Indicated");

        }


    }
    void GetDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        ddldistrict.DataSource = ds.Tables[0];
        ddldistrict.DataTextField = "district_name";
        ddldistrict.DataValueField = "District_Code";

        ddldistrict.DataBind();
        ddldistrict.Items.Insert(0, "--Select--");
       }
    void GetDestDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddldestdist.DataSource = ds.Tables[0];
        ddldestdist.DataTextField = "district_name";
        ddldestdist.DataValueField = "District_Code";

        ddldestdist.DataBind();
        ddldestdist.Items.Insert(0, "--Select--");
    }
    void GetRecddist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddlrecddistrict.DataSource = ds.Tables[0];
        ddlrecddistrict.DataTextField = "district_name";
        ddlrecddistrict.DataValueField = "District_Code";

        ddlrecddistrict.DataBind();
        ddlrecddistrict.Items.Insert(0, "--Select--");
    }
   
    void GetTransport()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid + "'and IsActive='Y'";

        DataSet ds = mobj.selectAny(qry);

        ddltransporter.DataSource = ds.Tables[0];
        ddltransporter.DataTextField = "Transporter_Name";
        ddltransporter.DataValueField = "Transporter_ID";
        ddltransporter.DataBind();
        ddltransporter.Items.Insert(0, "--Select--");

    }

    void GetDCName()
    {
        
        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" +  ddldestdist.SelectedValue  + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissuename.DataSource = ds.Tables[0];
        ddlissuename.DataTextField = "DepotName";
        ddlissuename.DataValueField = "DepotId";

        ddlissuename.DataBind();
        ddlissuename.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }

    void GetDCNameAll()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "select * from tbl_MetaData_DEPOT order by DepotName";
        DataSet ds = distobj.selectAny(ord);

        ddlissuename.DataSource = ds.Tables[0];
        ddlissuename.DataTextField = "DepotName";
        ddlissuename.DataValueField = "DepotId";

        ddlissuename.DataBind();
        ddlissuename.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string mtruckno=txttruckno.Text;
        float  disqty=CheckNull(txtdisqty.Text);
        int disbags=CheckNullInt (txtdisbags.Text);
        string challandate = getDate_MDY(DaintyDate3.Text);
        string Updateqry = "Update dbo.Rack_Receipt_Details set IssueCenter='" + ddlissuename.SelectedValue + "',Challan_date='"+challandate+"',Transporter_ID='" + ddltransporter.SelectedValue + "',Truck_No='" + mtruckno + "',Disp_Bags=" + disbags + ",Disp_Qty=" + disqty + ",Updated_date=getdate() where district_code='" + distid + "' and Challan_No='" + challan + "' and Rack_No='" + rackno + "'";

        try
        {
            cmd.CommandText = Updateqry;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Updated Successfully ...');</script>");
            btnSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            Label1.Visible = true;
            Label1.Text = ex.Message;
        }
        finally
        {
            con.Close();

        }

      

    }
        

    


    void GetData()
    {
       
            
            
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT Rack_Receipt_Details.Transporter_ID,Rack_Receipt_Details.Sending_District,Rack_Receipt_Details.Recd_RailHead, tbl_Rail_Head.RailHead_Name, Rack_Receipt_Details.IssueCenter,tbl_MetaData_DEPOT.DepotName, Rack_Receipt_Details.Rack_No, Rack_Receipt_Details.Challan_No,Rack_Receipt_Details.Challan_date, Rack_Receipt_Details.Truck_No, Rack_Receipt_Details.Disp_Bags,Rack_Receipt_Details.Disp_Qty, Rack_Receipt_Details.IsReceived, Rack_Receipt_Details.district_code FROM Rack_Receipt_Details Left JOIN tbl_Rail_Head ON Rack_Receipt_Details.Recd_RailHead = tbl_Rail_Head.RailHead_Code Left JOIN tbl_MetaData_DEPOT ON Rack_Receipt_Details.IssueCenter = tbl_MetaData_DEPOT.DepotID where Rack_Receipt_Details.district_code='" + distid + "' and Rack_Receipt_Details.Challan_No='" + challan + "' and Rack_Receipt_Details.Rack_No='" + rackno + "'";
            DataSet ds = mobj.selectAny(qry) ;
             if (ds.Tables[0].Rows.Count==0)
            {
                
      
            }
            else
            {
                             
                    DataRow drs = ds.Tables[0].Rows[0];
                    txtrecrailh.Text = drs["RailHead_Name"].ToString();
                    tctchallanno.Text = drs["Challan_No"].ToString();
                    txttruckno.Text = drs["Truck_No"].ToString();
                    txtdisbags.Text = drs["Disp_Bags"].ToString();
                    txtdisqty.Text = drs["Disp_Qty"].ToString();
                    txtrackno.Text = drs["Rack_No"].ToString();
                    ddldistrict.SelectedValue = drs["Sending_District"].ToString();
                    ddltransporter.SelectedValue = drs["Transporter_ID"].ToString();
                    ddlissuename.SelectedValue = drs["IssueCenter"].ToString();
                    ddldistrict.SelectedValue = distid;
                }

               
            
        }
    
   
  
   
   
    void GetRackQty()
    { 
            
            mobj = new MoveChallan(ComObj);
            string qreyrq = "select Sum(Rack_Qty) as Rack_Qty  from dbo.Rack_Dispatch_Bulk where district_code='" + distid  + "' and Rack_No='" + rackno + "'";
            DataSet ds = mobj.selectAny(qreyrq);
             if (ds.Tables[0].Rows.Count==0)
            {
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                string flag = "";
                if (flag == drs["Rack_Qty"].ToString())
                {

                    txtrackqty.Text = "0";
                    txtrackqty.ReadOnly = true;

                }
                else
                {
                    txtrackqty.Text = drs["Rack_Qty"].ToString();
                    txtrackqty.ReadOnly = true;
                }

            }

        
    }
     void GetBalRQty()
    {
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string rackno =  txtrackno.Text;
            string ddidt = ddldistrict.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qreyrq = "select Sum(Disp_Qty) as Disp_Qty from dbo.Rack_Receipt_Details where district_code='" + distid  + "' and Rack_No='" + rackno + "'";
            DataSet ds = mobj.selectAny(qreyrq);
             if (ds.Tables[0].Rows.Count==0)
            {
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                string flag = "";
                if (flag == drs["Disp_Qty"].ToString())
                {

                    txtissqty.Text = "0";
                    txtissqty.ReadOnly = true;

                }
                else
                {
                    txtissqty.Text = drs["Disp_Qty"].ToString();
                    txtissqty.ReadOnly = true;
                }

            }
        
        }
    void GetRackRecdQty()
    {      
        mobj = new MoveChallan(ComObj);
        string qreyrq = "select Rack_RecdQty,Rack_Qty,Rack_RecdDate from dbo.Rack_Receipt_Bulk where district_code='" + distid + "' and Rack_No='" + rackno + "'";
        DataSet ds = mobj.selectAny(qreyrq);
        if (ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            DataRow drs = ds.Tables[0].Rows[0];
               txtrackqty.Text = drs["Rack_Qty"].ToString();
               txtrecdqty.Text = drs["Rack_RecdQty"].ToString();
               DaintyDate1.Text = getdate(drs["Rack_RecdDate"].ToString());
                txtissqty.ReadOnly = true;
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
    protected void btnaddmore_Click(object sender, EventArgs e)
    {
        //Label2.Visible = false;
        //if (ddltransporter.SelectedItem.Text == "--Select--" || ddlissuename.SelectedItem.Text == "--Select--" || ddldistrict.SelectedItem.Text=="--Select--" || ddlrackno.SelectedItem.Text=="--Select--")
        //{
        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number/Prucurement Center/ IssueCenter/Transporter Name  ...');</script>");
        //    ddlrackno.Focus();
        //}

        //else
        //{
        //    dt = (DataTable)Session["dt"];
        //    int count = dt.Rows.Count;
        //    if (count > 0)
        //    {
        //        int i = 0;
        //        while (i < count)
        //        {
        //            chkqty = chkqty + CheckNull(dt.Rows[i][10].ToString());
        //            i = i + 1;
        //        }
        //        float recdqt =CheckNull(txtrecdqty.Text);
        //        float cksq = CheckNull(txtissqty.Text) + chkqty;
        //        if (cksq > recdqt)
        //        {
        //            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry You Can't Issue More Then Received Quantity  ...');</script>");
        //        }
        //        else
        //        {
        //            string icid = ddlissuename.SelectedValue;
        //            string icname = ddlissuename.SelectedItem.Text;
        //            string challan = tctchallanno.Text;
        //            string tcdate = getDate_MDY(DaintyDate3.Text);
        //            string mtransid = ddltransporter.SelectedValue;
        //            string mtrannm = ddlissuename.SelectedItem.Text;
        //            string truckno = txttruckno.Text;
        //            int dispbags = CheckNullInt(txtdisbags.Text);
        //            float dispqty = CheckNull(txtdisqty.Text);
        //            string rackno = txtrackno.Text;
        //            dt.Rows.Add(distid,rackno, icid, icname, challan, tcdate, mtransid, mtrannm, truckno, dispbags, dispqty);

        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();


        //            Session["dt"] = dt;


        //        }
        //    }

        //    else
        //    {

        //        string icid = ddlissuename.SelectedValue;
        //        string icname = ddlissuename.SelectedItem.Text;
        //        string mtrannm = ddltransporter.SelectedItem.Text;
        //        string challan = tctchallanno.Text;
        //        string tcdate = getDate_MDY(DaintyDate3.Text);
        //        string mtransid = ddltransporter.SelectedValue;
        //        string truckno = txttruckno.Text;
        //        int dispbags = CheckNullInt(txtdisbags.Text);
        //        float dispqty = CheckNull(txtdisqty.Text);
        //        string rackno = txtrackno.Text;
        //        dt.Rows.Add(distid, rackno, icid, icname, challan, tcdate, mtransid, mtrannm, truckno, dispbags, dispqty);

        //        GridView1.DataSource = dt;
        //        GridView1.DataBind();


        //        Session["dt"] = dt;


        //    }
           
           
        //}
        //txttruckno.Text = "";
        //tctchallanno.Text = "";
        //txtdisbags.Text = "";
        //txtdisqty.Text = "";
        //tctchallanno.Focus();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        dt.Rows[idx].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRack();
        

    }
    protected void txtrackddate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetChallanData();
        
        
    }


    //void GetChallanData()
    //{
    //    string mrack =  txtrackno.Text;
        
    //    mobj = new MoveChallan(ComObj);

    //    string qrydata = "SELECT * FROM dbo.RR_receipt_Depot where district_code='" + distid + "'and Rack_No='" + mrack + "' and TC_Number='"+ +"'";
    //    DataSet ds = mobj.selectAny(qrydata);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
            


    //    }
    //    else
    //    {
    //        DataRow dr = ds.Tables[0].Rows[0];
    //        txttruckno.Text = dr["Truck_No"].ToString();
    //        ddltransporter.SelectedValue  = dr["Transporter_ID"].ToString();
    //        txtdisbags.Text  = dr["Disp_Bags"].ToString();
    //       txtdisqty.Text  = dr["Disp_Qty"].ToString();
    //        ddlissuename.SelectedValue  = dr["DepotID"].ToString();
    //       //DaintyDate3.SelectedDate.Date  = Convert.ToDateTime ( dr["TC_date"].ToString());


    //    }

    //}
    //void GetChallan()
    //{
    //    string mrack= txtrackno.Text;
    //    mobj = new MoveChallan(ComObj);

    //    string qry = "SELECT * FROM dbo.RR_receipt_Depot where district_code='" + distid + "'and Rack_No='" + mrack + "' and Challan_st='N'";
    //    DataSet ds = mobj.selectAny(qry);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {

    //        ListItem lst = new ListItem();
    //        lst.Text = "Not Indicated";
    //        lst.Value = "0";


    //    }
    //    else
    //    {
    //    }
    //}
    protected void ddldesrailhead_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlrackno.SelectedItem.Text == "Not Indicated")
        {
           
        }
    }
    protected void txtrackno_TextChanged(object sender, EventArgs e)
    {
        GetBalRQty();
      
    }
    protected void ddldestdist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName(); 
    }
    protected void txtissqty_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlissuename_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
