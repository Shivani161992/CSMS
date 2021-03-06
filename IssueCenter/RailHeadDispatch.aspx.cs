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

public partial class RailHeadDispatch : System.Web.UI.Page
    {
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    chksql chk = null;
    SqlDataReader dr;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Transporter tobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    DistributionCenters distobj = null;
    Districts DObj = null;
    string distid = "";
    string gatepass = "";
    string sid = "23";
    int getnum;
    string issueid = "";
    string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            version = Session["hindi"].ToString();

            txtbagno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtquant.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
        


            txtremark.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtremark.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtremark.Attributes.Add("onchange", "return chksqltxt(this)");

            txttruckno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttruckno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttruckno.Attributes.Add("onchange", "return chksqltxt(this)");

            txttrukcno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttrukcno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttrukcno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbagno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbagno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtquant.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtquant.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");


            txtbagno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtquant.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttrukcno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
                   
            
            
            if (!IsPostBack)
            {
                GetSource();
                GetScheme();
                GetCommodity();
                GetTransport();
                GetGodown();
                //GetDepot();
                GetDist();
                GetICname();
                GetName();
                GetTo();
                
                //GetRack();
                if (version == "H")
                {
                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblScheme.Text = Resources.LocalizedText.lblScheme;
                    lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                    lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                    //lbltransfer.Text = Resources.LocalizedText.lbltransfer;
                    lbltransferdepot.Text = Resources.LocalizedText.lbltransferdepot;
                    lbltransrailhead.Text = Resources.LocalizedText.lbltransrailhead;
                    //lbltono.Text = Resources.LocalizedText.lbltono;
                    //lblmono.Text = Resources.LocalizedText.lblmono;

                    lblChallanNumber.Text = Resources.LocalizedText.lblChallanNumber;
                    lblTrans.Text = Resources.LocalizedText.lblTrans;
                    lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                    lbldisdate.Text = Resources.LocalizedText.lbldisdate;
                    lblrecddist.Text = Resources.LocalizedText.lblrecddist;
                    lblrackno.Text = Resources.LocalizedText.lblrackno;
                    lblrailhead.Text = Resources.LocalizedText.lblrailhead;
                    lbldistime.Text = Resources.LocalizedText.lbldistime;
                    lblNameDepot.Text = Resources.LocalizedText.lblNameDepot;
                    lblDistrictName.Text = Resources.LocalizedText.lblDistrictName;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    lblRemark.Text = Resources.LocalizedText.lblRemark;
                }


            }
            HyperLink1.Attributes.Add("onclick", "window.open('Print_TruckChallan.aspx',null,'left=200, top=10, height=620, width=580, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }
    }
    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
    }
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }
    void GetGodown()
    {

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid  + "' and DepotId='" + issueid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);
        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");


    }
    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }
    void GetDepot()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + distid  + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "DepotName";
        ddlgodown.DataValueField = "DepotId";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");

       
    }
    void GetRailHead()
    {
        mobj = new MoveChallan(ComObj);
        string qrygrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + ddldistrict.SelectedValue + "'";
        DataSet dsd = mobj.selectAny(qrygrh);

        ddlsenrailhead.DataSource = dsd.Tables[0];
        ddlsenrailhead.DataTextField = "RailHead_Name";
        ddlsenrailhead.DataValueField = "RailHead_Code";
        ddlsenrailhead.DataBind();
        ddlsenrailhead.Items.Insert(0, "--Select--");



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
    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlsenrailhead.DataSource = ds.Tables[0];
        ddlsenrailhead.DataTextField = "DepotName";
        ddlsenrailhead.DataValueField = "DepotId";

        ddlsenrailhead.DataBind();
        ddlsenrailhead.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetTransport()
    {
        tobj = new Transporter(ComObj);
        string qry = "SELECT Transporter_Name,Transporter_ID FROM dbo.Transporter_Table where Distt_ID='" + distid + "'and IsActive='Y'";
        DataSet ds = tobj.selectAny(qry);
         
        ddltransporter.DataSource = ds.Tables[0];
        ddltransporter.DataTextField = "Transporter_Name";
        ddltransporter.DataValueField = "Transporter_ID";
        ddltransporter.DataBind();
        ddltransporter.Items.Insert(0, "--Select--");

    }
    void GetICname()
    {
        tobj = new Transporter(ComObj);
        string qry = "Select * from dbo.tbl_MetaData_DEPOT where DistrictId='" + "23"+distid + "' and DepotID='" + issueid + "'";

        DataSet ds = tobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];
        //ddlgodown.SelectedItem.Text  = dr["DepotName"].ToString ();
        //ddlgodown.SelectedValue = dr["DepotID"].ToString ();
        
    }

    void GetRack()
    {
        try
        {
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string railh = ddlsenrailhead.SelectedValue;
            string dist = ddldistrict.SelectedValue;
            string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where Dest_District='" + dist + "' and IsSend='N'";
            cmd = new SqlCommand(qreyrac, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlrackno.DataSource = ds.Tables[0];
                ddlrackno.DataTextField = "Rack_No";
                ddlrackno.DataValueField = "Rack_No";
                ddlrackno.DataBind();
                ddlrackno.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlrackno.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया पुनः प्रयास करें '); </script> ");
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
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
    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid  + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        Label3.Text = dr1dt["district_name"].ToString();
         

        mobj = new MoveChallan(ComObj);
        string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid  + "'";
        DataSet dsic = mobj.selectAny(qryissue);
        DataRow dric = dsic.Tables[0].Rows[0];
        Label4.Text = dric["DepotName"].ToString();

       

       



    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

            string opid = Session["OperatorId"].ToString();
            string state = Session["State_Id"].ToString();

            mobj1 = new MoveChallan(ComObj);
            string qrey = "select max(Dispatch_id) as Dispatch_id from dbo.SCSC_RailHead_TC where Depot_Id='" + issueid + "' and Dist_ID='" + distid + "'";
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

            string disdate = getDate_MDY(DaintyDate1.Text);
            string mdepo = ddlgodown.SelectedValue;
            string mcomid = ddlcomdty.SelectedValue;
            string mschid = ddlscheme.SelectedValue;
            string mtrid = ddltransporter.SelectedValue;
            string mdfrom = ddlgodown.SelectedValue;
            string mtodist = lbldist.Text ;
            string mtoissue = lbldepo.Text ;
            string mfci = "";
            int mbagno = CheckNullInt(txtbagno.Text);
            float mqtys = CheckNull(txtquant.Text);
            float mbqty = CheckNull(txtbqty.Text);
          
            string mudate = "";
            string mdstatus = "N";
            int month = int.Parse(DateTime.Today.Date.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string rackno = ddlrackno.SelectedValue;
            string remark = txtremark.Text;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string notrans = "N";
            string msource = ddlsarrival.SelectedValue;
            //if (mbqty < mqtys)
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Have Insufficieant Balance ....'); </script> ");
            //}
            //else
            //{
                if (mqtys == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be Zero ....'); </script> ");
                }
                else
                {

                    string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
                    string qry = "insert into dbo.SCSC_RailHead_TC(State_Id,Dist_ID,Depot_Id,Challan_Date,Dispatch_Godown,Sendto_District,RailHead,Commodity,Scheme,Rack_NO,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + issueid + "','" + disdate + "','" + mdepo + "','" + mtodist + "','" + mtoissue + "','" + mcomid + "','" + mschid + "','" + rackno + "'," + mbagno + "," + mqtys + ",'" + txttrukcno.Text + "','" + txttruckno.Text + "','" + mtrid + "','" + time + "','" + remark + "','" + gatepass + "','" + mdstatus + "'," + month + "," + year + ",getdate(),'" + mudate + "','" + ip + "','" + msource + "','" + opid + "','"+notrans+"')";
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--" || ddlrackno.SelectedItem.Text == "--Select--" || ddlsenrailhead.SelectedItem.Text == "--Select--")
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Scheme/Dispatch Godown/Rack Number/Rail Head ....'); </script> ");

                    }
                    else
                    {
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            Session["gatepass"] = txttrukcno.Text; ;
                            float ruqty = CheckNull(txtquant.Text);
                            float buqty = CheckNull(txtbqty.Text);
                            float uuqty = (buqty - ruqty);
                            string mcomdtyid = ddlcomdty.SelectedValue;
                            string mscheme = ddlscheme.SelectedValue;
                            string msaletype = "Other Depot";
                            string mstate = "23";
                            float msaleqty = CheckNull(txtquant.Text);
                            string godown = ddlgodown.SelectedValue;
                            string mssrc = ddlsarrival.SelectedValue;
                            string mddate = "";
                            string query = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + CheckNull (txtquant.Text) + ",Current_Bags=Current_Bags-" +CheckNullInt (txtbagno.Text)+ " where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and Godown='" + godown + "' and Source='" + mssrc + "'";
                            cmd.CommandText = query;
                            cmd.Connection = con;
                            string qreySale = "insert into dbo.SCSC_Sale_Details(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Sale_Type,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + distid + "','" + issueid + "','" + mcomid + "','" + mscheme + "','" + msaletype + "'," + msaleqty + "," + month + "," + year + ",getdate(),'" + mudate + "','" + mddate + "'" + ")";
                            try
                            {
                                string updateto = "Update dbo.Transport_Order set IsLifted='Y' where fromDistrict='" + distid + "'and fromIssueCenter='" + issueid + "'and TO_Number='" + ddltono.SelectedValue + "'";
                                cmd.ExecuteNonQuery();
                                con.Close();
                                cmd.CommandText = updateto;
                                                                                              
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                cmd.CommandText = qreySale;
                                con.Open();
                                int count = cmd.ExecuteNonQuery();
                                con.Close();
                                                       
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                            }
                            catch (Exception ex)
                            {
                                Label1.Text = ex.Message;

                            }
                            finally
                            {
                                con.Close();
                                ComObj.CloseConnection();
                            }

                            btnsave.Enabled = false;
                            //HyperLink1.Visible = true;
                            con.Close();

                        }
                        catch (Exception ex)
                        {

                            Label1.Text = ex.Message;




                        }
                        finally
                        {
                            con.Close();
                            ComObj.CloseConnection();
                        }



                    }
                }

            

        //}
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldist.Text = ddldistrict.SelectedValue;
        GetRailHead();
        GetRack(); 

    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Stock Issued From....'); </script> ");
        }
        else
        {
            GetBalance();
        }
    }
    void GetBalance()
    {

        string mcomdtyid = ddlcomdty.SelectedValue;
        //string mcatid = ddlcategory.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string mgdown = ddlgodown.SelectedValue;
        string msource = ddlsarrival.SelectedValue;

        mobj1 = new MoveChallan(ComObj);
        string qry = "Select Round(Current_Balance,5) as Current_Balance from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and Godown='" + mgdown + "' and Source='"+ msource+"'";
        DataSet ds = mobj1.selectAny(qry);

        if (ds.Tables[0].Rows.Count == 0)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            lblbqty.Visible = false;
            txtbqty.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbqty.Text = dr["Current_Balance"].ToString();
            lblbqty.Visible = true;
            txtbqty.Visible = true;
            txtbqty.BackColor = System.Drawing.Color.Wheat;
            txtbqty.ReadOnly = true;
        }
    }
    void GetTo()
     {
        mobj = new MoveChallan(ComObj);

        string qry = "SELECT TO_Number  FROM dbo.Transport_Order where fromDistrict='" + distid + "'and fromIssueCenter='" + issueid + "'and IsLifted='N'";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {

            ddltono.Items.Clear();
            ListItem lst = new ListItem();
            lst.Text = "Not Indicated";
            lst.Value = "0";

            ddltono.Items.Insert(0, "--Select--");
            ddltono.Items.Insert(1, lst);


        }
        else
        {
            ddltono.Items.Clear();
            ddltono.DataSource = ds.Tables[0];
            ddltono.DataTextField = "TO_Number";
            ddltono.DataValueField = "TO_Number";
            ddltono.DataBind();
            ddltono.Items.Insert(0, "--Select--");
            ddltono.Items.Insert(1, "Not Indicated");


        }
    }
    void GetData()
    {
        string tono = ddltono.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string getdata = "SELECT Transport_Order.toDistrict, Transport_Order.toIssueCenter, tbl_MetaData_DEPOT.DepotName, pds.districtsmp.district_name,Transport_Order.Transporter_ID, Transporter_Table.Transporter_Name, Transport_Order.Scheme_ID,tbl_MetaData_SCHEME.Scheme_Name, Transport_Order.Commodity_ID,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, Transport_Order.TO_Number, Transport_Order.TO_Date,Transport_Order.TO_Validity, Transport_Order.fromDistrict, Transport_Order.fromIssueCenter,Transport_Order.Quantity FROM  Transport_Order INNER JOIN pds.districtsmp ON Transport_Order.toDistrict = pds.districtsmp.district_code INNER JOIN tbl_MetaData_DEPOT ON Transport_Order.toIssueCenter = tbl_MetaData_DEPOT.DepotID INNER JOIN  Transporter_Table ON Transport_Order.Transporter_ID = Transporter_Table.Transporter_ID INNER JOIN tbl_MetaData_SCHEME ON Transport_Order.Scheme_ID = tbl_MetaData_SCHEME.Scheme_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON Transport_Order.Commodity_ID = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where Transport_Order.fromDistrict='" + distid + "' and Transport_Order.fromIssueCenter='" + issueid + "'and Transport_Order.TO_Number='"+tono+"'";
        DataSet ds = mobj.selectAny(getdata);
         if (ds.Tables[0].Rows.Count == 0)
         {
            


         }
         else
         {
             DataRow dr = ds.Tables[0].Rows[0];
             ddlcomdty.SelectedItem.Text = dr["Commodity_Name"].ToString();
             ddlcomdty.SelectedValue = dr["Commodity_ID"].ToString();
             ddlscheme.SelectedItem.Text = dr["Scheme_Name"].ToString();
             ddlscheme.SelectedValue = dr["Scheme_ID"].ToString();
             txtquant.Text = dr["Quantity"].ToString();
             ddltransporter.SelectedItem.Text = dr["Transporter_Name"].ToString();
             ddltransporter.SelectedValue = dr["Transporter_ID"].ToString();
             ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
             ddlsenrailhead.SelectedItem.Text = dr["DepotName"].ToString();
             lbldist.Text  = dr["toDistrict"].ToString();
             lbldepo.Text = dr["toIssueCenter"].ToString();
             txtbagno.Focus();
             GetBalance();


         }
    }
   
    protected void ddltono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
    }
    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldepo.Text = ddlsenrailhead.SelectedValue;
    }
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
