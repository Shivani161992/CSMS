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
public partial class DistrictFood_Rack_Dispatch_Detail : System.Web.UI.Page
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
    chksql chk = null;
    public string transid = "";
    public int railnum;
    DataTable dt = new DataTable();
    float disqty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            txtdisbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtdisqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");


            txtdisbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdisbags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtdisqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdisqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrackqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrackqty.Attributes.Add("onchange", "return chksqltxt(this)");

           

            tctchallanno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            tctchallanno.Attributes.Add("onchange", "return chksqltxt(this)");

            txttruckno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttruckno.Attributes.Add("onchange", "return chksqltxt(this)");


            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

            txtdisbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrecbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrecqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtrecqty.Text);
            ctrllist.Add(txtdisbags.Text);
            ctrllist.Add(txtdisqty.Text);
            ctrllist.Add(txttruckno.Text);
            ctrllist.Add(tctchallanno.Text);
            ctrllist.Add(txttruckno.Text);
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


            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


            if (!IsPostBack)
            {
               
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                ddldistrict.SelectedValue = distid;
                GetTransport();
                GetRack();
                             
                dt.Columns.Add("district_code");
                dt.Columns.Add("Rack_No");
                dt.Columns.Add("PC_ID");
                dt.Columns.Add("IC_ID");
                dt.Columns.Add("IC_Name");
                dt.Columns.Add("Challan_No");
                dt.Columns.Add("Challan_date");
                dt.Columns.Add("Transporter_ID");
                dt.Columns.Add("Transporter_Name");
                dt.Columns.Add("Truck_No");
                dt.Columns.Add("Disp_Bags");
                dt.Columns.Add("Disp_Qty");
                dt.Columns.Add("Received_Bags");
                dt.Columns.Add("Received_Qty");
               
                Session["dt"] = dt;

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
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
        ddldistrict.SelectedValue = distid;

    }
    void GetRack()
    {
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());

        ddlrackno.Items.Insert(0, "--Select--");
        string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + distid + "' and IsSend ='N'";
        cmd.Connection = con;
        cmd.CommandText = qreyrac;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrackno.Items.Add(dr["Rack_No"].ToString());

        }
        dr.Close();
        con.Close();

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
        string ord = "Districtid='23" + ddldistrict.SelectedValue + "' order by DepotName";
        DataSet ds = distobj.select(ord);
        ddlissuename.DataSource = ds.Tables[0];
        ddlissuename.DataTextField = "DepotName";
        ddlissuename.DataValueField = "DepotId";
        ddlissuename.DataBind();
        ddlissuename.Items.Insert(0, "--Select--");
    }
    void GetPCName()
    {

        string distcode = ddldistrict.SelectedValue;
        distobj = new DistributionCenters(ComObj);
        string ord = "DistrictId='23" + ddldistrict.SelectedValue.ToString() + "' order by PcId";
        DataSet ds = distobj.selectPC(ord);
        ddlprocname.DataSource = ds.Tables[0];
        ddlprocname.DataTextField = "PurchaseCenterName";
        ddlprocname.DataValueField = "PcId";
        ddlprocname.DataBind();
        ddlprocname.Items.Insert(0, "--Select--");
              
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
    public string getdateMM(string DDDate)

    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorIDDM"].ToString();

        string state = Session["State_Id"].ToString();
        string notrans = "N";

        GridView1.Columns[4].Visible = true;
        GridView1.Columns[8].Visible = true;
        if (ddlrackno.SelectedItem.Text == "--Select--" || ddltransporter.SelectedItem.Text == "--Select--" ||ddlissuename.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number  And Receipt Details...');</script>");
        }
        else
        {
            dt = (DataTable)Session["dt"];
                   
            int count = dt.Rows.Count;
            if (count > 0)
            {
                int month = int.Parse(DateTime.Today.Month.ToString());
                int year = int.Parse(DateTime.Today.Year.ToString());
                string crdate = DateTime.Today.Date.ToString();
                string udate = "";
                int i = 0;
                SqlTransaction trns;
                con.Open();
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns;
                cmd.Connection = con;
                try{

                while (i < count)
                {
                    string qry = "insert into dbo.Rack_Dispatch_Details(State_Id,district_code,Rack_No,PC_ID,IC_ID,Challan_No,Challan_date,Transporter_ID,Truck_No,Disp_Bags,Disp_Qty,Received_Bags,Received_Qty,Month,Year,Created_date,Updated_date,OperatorID,IP,NoTransaction)values('" + state + "','" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][7] + "','" + dt.Rows[i][9] + "'," + dt.Rows[i][10] + "," + dt.Rows[i][11] + "," + dt.Rows[i][12] + "," + dt.Rows[i][13] + "," + month + "," + year + ",getdate(),'" + udate + "','" + opid + "','" + ip + "','" + notrans + "')";
                    cmd.CommandText = qry;
                    cmd.ExecuteNonQuery();
                    string icid = dt.Rows[i][3].ToString();
                    string chalan = dt.Rows[i][5].ToString();
                    string qryupd = "Update dbo.SCSC_RailHead_TC set IsDeposit='Y' where Depot_Id='"+icid +"' and Challan_No='"+chalan +"'";
                    cmd.CommandText = qryupd;
                    cmd.ExecuteNonQuery();
                    disqty = disqty + CheckNull(dt.Rows[i][13].ToString ());
                    i = i + 1;
                }
                string mrackno=ddlrackno.SelectedValue ;
                //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string qreyrq = "select Rack_Qty  from dbo.Rack_Dispatch_Bulk where district_code='" + distid + "' and Rack_No='" + mrackno + "' and Month=" + month + " and Year=" + year;
                mobj = new MoveChallan(ComObj);
                DataSet dsrq = mobj.selectAny(qreyrq);
                 if (dsrq.Tables[0].Rows.Count==0)
                {
                    string insertq = "insert into dbo.Rack_Dispatch_Bulk(State_Id,district_code,Sending_RailHead,Dest_District,Dest_RailHead,Commodity_ID,Rack_DispDate,Rack_No,Rack_Qty,Month,Year,Created_Date,Updated_Date,IP_Address,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + lblsrh.Text + "','" + lbldesdist.Text + "','" + lbldrh.Text + "','" + lblcomdty.Text + "','" + lbldisdate.Text + "','" + mrackno + "'," + disqty + "," + month + "," + year + ",getdate(),'" + udate + "','" + ip + "','" + opid + "','" + notrans + "')";
                    cmd.CommandText = insertq;
                    cmd.Transaction = trns;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    DataRow drs = dsrq.Tables[0].Rows[0];
                    float fqty = CheckNull(drs["Rack_Qty"].ToString());
                    float aqty = fqty + disqty;
                    string udates = DateTime.Now.Date.ToString();
                    string updateq = " Update dbo.Rack_Dispatch_Bulk set Rack_Qty=" + aqty + ",Updated_Date='" + udates +"' where district_code='" + distid + "' and Rack_No='" + mrackno + "' and Month=" + month + " and Year=" + year;
                    cmd.CommandText = updateq;
                    cmd.Transaction = trns;
                    cmd.ExecuteNonQuery ();

                    string updaterack = " Update dbo.tbl_RackMaster set IsSend='Y' where district_code='" + distid + "'and Rack_No='" + ddlrackno.SelectedValue+"'";
                    cmd.CommandText = updaterack;
                    cmd.Transaction = trns;
                    cmd.ExecuteNonQuery();

                       


                }

                trns.Commit();
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                btnSubmit.Enabled = false;
                }
            catch (Exception ex)
            {
                trns.Rollback();
                Label1.Visible = true;
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

            }
                 
           


        }
        GridView1.Columns[4].Visible = false;
        GridView1.Columns[8].Visible = false;

    }

    void GetChallan()
    {
        mobj = new MoveChallan(ComObj);

        string qry = "SELECT * FROM dbo.SCSC_RailHead_TC where Sendto_District='" + distid  + "'and RailHead='" + lblsrh.Text  +"'and IsDeposit='N'";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
          
            ddlchallan.Items.Clear();
            ListItem lst = new ListItem();
            lst.Text = "Not Indicated";
            lst.Value = "0";
            ddlchallan.Items.Insert(0, "--Select--");
            ddlchallan.Items.Insert(1, lst);


        }
        else
        {
            ddlchallan.Items.Clear();
            ddlchallan.DataSource = ds.Tables[0];
            ddlchallan.DataTextField = "Challan_No";
            ddlchallan.DataValueField = "Challan_No";
            ddlchallan.DataBind();
            ddlchallan.Items.Insert(0, "--Select--");
            ddlchallan.Items.Insert(1, "Not Indicated");


        }
    }
    void GetData()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
            txtsrailh.Text = "";
            txtrecrailh.Text = "";
            ddlrackno.Focus();
        }
        else
        {
            string rackno = ddlrackno.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qrey = "select dbo.tbl_RackMaster.*,tbl_Rail_Head.RailHead_Name ,RailHead_Dest.RailHead_Name as DestRName from dbo.tbl_RackMaster left join dbo.tbl_Rail_Head on tbl_RackMaster.RailHead=tbl_Rail_Head.RailHead_Code left join  dbo.tbl_Rail_Head as RailHead_Dest on tbl_RackMaster.Dest_RailHead=RailHead_Dest.RailHead_Code   where tbl_RackMaster.district_code='" + distid + "' and tbl_RackMaster.Rack_No='" + rackno+"'";
            DataSet ds = mobj.selectAny(qrey);
             if (ds.Tables[0].Rows.Count==0)
            {
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                txtsrailh.Text = drs["RailHead_Name"].ToString();
                txtrecrailh.Text = drs["DestRName"].ToString();
                lblcomdty.Text = drs["Commodity_ID"].ToString();
                lbldesdist.Text = drs["Dest_District"].ToString();
                lblsrh.Text = drs["RailHead"].ToString();
                lbldrh.Text = drs["Dest_RailHead"].ToString();
                lbldisdate.Text = drs["Rack_DispDate"].ToString();
                txtsrailh.ReadOnly = true;
                txtrecrailh.ReadOnly = true;
            }
        }
    }
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
        GetRackQty();
        ddlprocname.Focus();
            }
    void GetRackQty()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
        }
        else 

        {
        int month=int.Parse (DateTime.Today.Month .ToString ());
        int year=int.Parse (DateTime.Today.Year.ToString ());
        string rackno = ddlrackno.SelectedValue;
        
        mobj = new MoveChallan(ComObj);
        string qreyrq = "select Sum(Rack_Qty) as Rack_Qty  from dbo.Rack_Dispatch_Bulk where district_code='" + distid + "' and Rack_No='" + rackno + "' and Month=" + month + " and Year=" + year;
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
        if (chk_receive.SelectedValue == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select  Source of Arrival of Stock   ...');</script>");

        }
        
        else
        {
            if (chk_receive.SelectedValue == "01")
            {
                string pcid = "";
                string icid = "";
                string icname = "";
                if (ddlrackno.SelectedItem.Text == "--Select--" || ddltransporter.SelectedItem.Text == "--Select--" || ddlprocname.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number/Prucurement Center/Transporter Name  ...');</script>");
                    ddlrackno.Focus();
                }

                else
                {
                    dt = (DataTable)Session["dt"];
                    if (chk_receive.SelectedValue == "01")
                    {
                        pcid = ddlprocname.SelectedValue;
                        icid = "";
                        icname = "";
                    }
                    else
                    {
                        pcid = "";
                        icid = ddlissuename.SelectedValue;
                        icname = ddlissuename.SelectedItem.Text.ToString();
                    }

                    //string icname = ddlissuename.SelectedItem.Text;
                    string challan = tctchallanno.Text;
                    string tcdate = getDate_MDY(DaintyDate3.Text);
                    string mtransid = lbltrp.Text;
                    string mtransnm = ddltransporter.SelectedItem.Text;
                    string truckno = txttruckno.Text;
                    int dispbags = CheckNullInt(txtdisbags.Text);
                    float dispqty = CheckNull(txtdisqty.Text);
                    int recbag = CheckNullInt(txtrecbags.Text);
                    float recqty = CheckNull(txtrecqty.Text);
                    float rackqty = CheckNull(txtrackqty.Text);

                    dt.Rows.Add(distid, ddlrackno.SelectedValue, pcid, icid, icname, challan, tcdate, mtransid, mtransnm, truckno, dispbags, dispqty, recbag, recqty);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    tctchallanno.Text = "";
                    txtdisbags.Text = "";
                    txtdisqty.Text = "";
                    txtrecbags.Text = "";
                    txtrecqty.Text = "";
                    txttruckno.Text = "";
                    Session["dt"] = dt;
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[8].Visible = false;
                }
               


            }
            else
            {
                string pcid = "";
                string icid = "";
                string icname = "";
                if (ddlrackno.SelectedItem.Text == "--Select--" || ddltransporter.SelectedItem.Text == "--Select--" || ddlissuename.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number/IssueCenter/Transporter Name  ...');</script>");
                    ddlrackno.Focus();
                }

                else
                {
                    dt = (DataTable)Session["dt"];
                    if (chk_receive.SelectedValue == "01")
                    {
                        pcid = ddlprocname.SelectedValue;
                        icid = "";
                        icname = "";
                    }
                    else
                    {
                        pcid = "";
                        icid = ddlissuename.SelectedValue;
                        icname = ddlissuename.SelectedItem.Text.ToString();
                    }

                    //string icname = ddlissuename.SelectedItem.Text;
                    string challan = tctchallanno.Text;
                    string tcdate = getDate_MDY(DaintyDate3.Text);
                    string mtransid = lbltrp.Text;
                    string mtransnm = ddltransporter.SelectedItem.Text;
                    string truckno = txttruckno.Text;
                    int dispbags = CheckNullInt(txtdisbags.Text);
                    float dispqty = CheckNull(txtdisqty.Text);
                    int recbag = CheckNullInt(txtrecbags.Text);
                    float recqty = CheckNull(txtrecqty.Text);
                    float rackqty = CheckNull(txtrackqty.Text);

                    dt.Rows.Add(distid, ddlrackno.SelectedValue, pcid, icid, icname, challan, tcdate, mtransid, mtransnm, truckno, dispbags, dispqty, recbag, recqty);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    tctchallanno.Text = "";
                    txtdisbags.Text = "";
                    txtdisqty.Text = "";
                    txtrecbags.Text = "";
                    txtrecqty.Text = "";
                    txttruckno.Text = "";
                    Session["dt"] = dt;
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[8].Visible = false;
                }

            }
       
        }
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
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        GetChallanData();

    }
    void GetChallanData()
    {
        string challan=ddlchallan.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qry = " SELECT SCSC_RailHead_TC.Challan_Date,SCSC_RailHead_TC.Depot_Id, tbl_MetaData_DEPOT.DepotName, SCSC_RailHead_TC.Transporter,Transporter_Table.Transporter_Name, SCSC_RailHead_TC.Challan_No, SCSC_RailHead_TC.Truck_no,SCSC_RailHead_TC.Qty_send, SCSC_RailHead_TC.Bags, SCSC_RailHead_TC.Rack_NO, SCSC_RailHead_TC.RailHead,SCSC_RailHead_TC.Sendto_District, SCSC_RailHead_TC.Dispatch_id FROM SCSC_RailHead_TC INNER JOIN tbl_MetaData_DEPOT ON SCSC_RailHead_TC.Depot_Id = tbl_MetaData_DEPOT.DepotID INNER JOIN Transporter_Table ON SCSC_RailHead_TC.Transporter = Transporter_Table.Transporter_ID where SCSC_RailHead_TC.Sendto_District='" + distid + "' and SCSC_RailHead_TC.RailHead='" + lblsrh.Text + "' and SCSC_RailHead_TC.Challan_No='" + challan + "'";

        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DaintyDate3.Visible = true ;
            txtchallandt.Visible = false;
            //Label3.Visible = true;
            //ddlprocname.Visible = true;

            txttruckno.Text = "";
            txtdisbags.Text = "";
            txtdisqty.Text = "";
            tctchallanno.Text = "";
            txtchallandt.Text = "";
            txttruckno.ReadOnly =false ;
            txttruckno.BackColor = System.Drawing.Color.White;
            txtdisbags.ReadOnly = false;
            txtdisbags.BackColor = System.Drawing.Color.White;
            txtdisqty.ReadOnly = false;
            tctchallanno.ReadOnly = false;
            txtchallandt.ReadOnly = false;
            txtchallandt.BackColor = System.Drawing.Color.White;
            tctchallanno.BackColor = System.Drawing.Color.White;
            txtdisqty.BackColor = System.Drawing.Color.White;
            ddltransporter.BackColor = System.Drawing.Color.White;
            ddlissuename.BackColor = System.Drawing.Color.White;
            txtrecbags.Focus();
        }
        else
        {

            DaintyDate3.Visible = false;
            txtchallandt.Visible = true;
            Label3.Visible = false;
            ddlprocname.Visible = false;
            DataRow drs = ds.Tables[0].Rows[0];
            ddlissuename.SelectedItem.Text = drs["DepotName"].ToString();
            ddlissuename.SelectedValue = drs["Depot_Id"].ToString();
            tctchallanno.Text = drs["Challan_No"].ToString();
            txtchallandt.Text = getdateMM ( drs["Challan_Date"].ToString());
            ddltransporter.SelectedItem.Text = drs["Transporter_Name"].ToString();
            lbltrp.Text  = drs["Transporter"].ToString();
            txttruckno.Text = drs["Truck_no"].ToString();
            txtdisbags.Text = drs["Bags"].ToString();
            txtdisqty.Text = drs["Qty_send"].ToString();

            txttruckno.ReadOnly = true;
            txttruckno.BackColor = System.Drawing.Color.Wheat;
            txtdisbags.ReadOnly = true;
            txtdisbags.BackColor = System.Drawing.Color.Wheat;
            txtdisqty.ReadOnly = true;
            tctchallanno.ReadOnly = true;
            txtchallandt.ReadOnly = true;
            txtchallandt.BackColor = System.Drawing.Color.Wheat;
            tctchallanno.BackColor = System.Drawing.Color.Wheat;
            txtdisqty.BackColor = System.Drawing.Color.Wheat;
            ddltransporter.BackColor = System.Drawing.Color.Wheat;
            ddlissuename.BackColor = System.Drawing.Color.Wheat;
            txtrecbags.Focus();




        }

    }
    protected void ddltransporter_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbltrp.Text = ddltransporter.SelectedValue;
    }
    protected void chk_receive_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDist();
        ddldistrict.SelectedValue = distid;
        if (chk_receive.SelectedValue == "01")
        {
            lbldist.Visible = true;
            ddldistrict.Visible = true;
            lblpc.Visible = true;
            ddlprocname.Visible = true;
            lblic.Visible = false;
            ddlissuename.Visible = false;
            GetPCName();
            lblchallan.Visible = false;
            ddlchallan.Visible = false;
        }
        else
        {
            lbldist.Visible = true;
            ddldistrict.Visible = true;
            lblpc.Visible = false;
            ddlprocname.Visible = false;
            lblic.Visible = true;
            ddlissuename.Visible = true;
            GetDCName();
            GetChallan();
            lblchallan.Visible = true;
            ddlchallan.Visible = true;
        }

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chk_receive.SelectedValue == "01")
        {
           
            GetPCName();
        }
        else
        {
            GetDCName();

        }


    }
}
