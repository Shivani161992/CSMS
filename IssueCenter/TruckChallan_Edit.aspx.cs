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

public partial class TruckChallan_Edit : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    chksql chk = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Transporter tobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    DistributionCenters distobj = null;
    Districts DObj = null;
    string distid = "";
    string disp_id= "";
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
            disp_id = Session["Dispatch"].ToString();
            version = Session["hindi"].ToString();
            txtbagno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtquant.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txttono.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttono.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttono.Attributes.Add("onchange", "return chksqltxt(this)");


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


            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txttrukcno.Text);
            ctrllist.Add(txtbagno.Text);
            ctrllist.Add(txtquant.Text);
            ctrllist.Add(DaintyDate1.Text);
            ctrllist.Add(txttruckno.Text);
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

                DaintyDate1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                GetSource();

                GetScheme();
                GetCommodity();
                GetTransport();
                //GetDepot();
                GetGodown();
                GetDist();
                //GetICname();
                GetData();
                GetAllDepot();
                GetName();


                if (version == "H")
                {
                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblScheme.Text = Resources.LocalizedText.lblScheme;
                    lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                    lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                    lbltransfer.Text = Resources.LocalizedText.lbltransfer;
                    lbltono.Text = Resources.LocalizedText.lbltono;
                    lblChallanNumber.Text = Resources.LocalizedText.lblChallanNumber;
                    lblTrans.Text = Resources.LocalizedText.lblTrans;
                    lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                    lbldisdate.Text = Resources.LocalizedText.lbldisdate;
                    lblrecddist.Text = Resources.LocalizedText.lblrecddist;
                    lblrecdepo.Text = Resources.LocalizedText.lblrecdepo;
                    lbldistime.Text = Resources.LocalizedText.lbldistime;
                    lblNameDepot.Text = Resources.LocalizedText.lblNameDepot;
                    lblDistrictName.Text = Resources.LocalizedText.lblDistrictName;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    lblRemark.Text = Resources.LocalizedText.lblRemark;
                    lbldispsource.Text = Resources.LocalizedText.lbldispsource;
                    btnsave.Text = Resources.LocalizedText.btnsave;
                }



            }
            HyperLink1.Attributes.Add("onclick", "window.open('Print_TruckChallan.aspx',null,'left=200, top=10, height=620, width=580, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }
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
    void GetGodown()
    {

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + issueid + "' order by Godown_ID";
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
    //void GetDepot()
    //{

    //    distobj = new DistributionCenters(ComObj);
    //    string ord = "Districtid='23" + distid  + "' order by DepotName";
    //    DataSet ds = distobj.select(ord);

    //    ddlgodown.DataSource = ds.Tables[0];
    //    ddlgodown.DataTextField = "DepotName";
    //    ddlgodown.DataValueField = "DepotId";
    //    ddlgodown.DataBind();
    //    ddlgodown.Items.Insert(0, "--Select--");

       
    //}
    void GetAllDepot()
    {

        distobj = new DistributionCenters(ComObj);
        string ordall = "Select DepotName,DepotId from dbo.tbl_MetaData_DEPOT Order by DepotName";
        DataSet ds = distobj.selectAny(ordall);

        ddlissuecenter.DataSource = ds.Tables[0];
        ddlissuecenter.DataTextField = "DepotName";
        ddlissuecenter.DataValueField = "DepotId";
        ddlissuecenter.DataBind();
        ddlissuecenter.Items.Insert(0, "--Select--");


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

        ddlissuecenter.DataSource = ds.Tables[0];
        ddlissuecenter.DataTextField = "DepotName";
        ddlissuecenter.DataValueField = "DepotId";

        ddlissuecenter.DataBind();
        ddlissuecenter.Items.Insert(0, "--Select--");

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
        if (ds == null)
        {
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            ddlgodown.SelectedItem.Text = dr["DepotName"].ToString();
            ddlgodown.SelectedValue = dr["DepotID"].ToString();
            ddlgodown.Enabled = false;
            ddlgodown.ForeColor = System.Drawing.Color.Blue;
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
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        Label4.Text = dr1dt["district_name"].ToString();


        mobj = new MoveChallan(ComObj);
        string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
        DataSet dsic = mobj.selectAny(qryissue);
        DataRow dric = dsic.Tables[0].Rows[0];
        Label5.Text = dric["DepotName"].ToString();







    }
    protected void btnsave_Click(object sender, EventArgs e)
    {       
            
        
        
        string disdate = getDate_MDY(DaintyDate1.Text);
            string mdepo = ddlgodown.SelectedValue;
            string mcomid = ddlcomdty.SelectedValue;
            string mschid = ddlscheme.SelectedValue;
            string mtrid = ddltransporter.SelectedValue;
            string mdfrom = ddlgodown.SelectedValue;
            string mtodist = ddldistrict.SelectedValue;
            string mtoissue = ddlissuecenter.SelectedValue;
            string mchallan=txttrukcno.Text;
            string truckno=txttruckno.Text;
            string remarks=txtremark.Text;
            string mgodown=ddlgodown.SelectedValue;
            string mfci = "";
            int mbagno = CheckNullInt(txtbagno.Text);
            float mqtys = CheckNull(txtquant.Text);
            float mbqty = CheckNull(txtbqty.Text);
          
            string mudate = "";
            string mdstatus = "N";
            int month = int.Parse(DateTime.Today.Date.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string msource=ddlsarrival.SelectedValue;
            if (mbqty < mqtys)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Have Insufficieant Balance ....'); </script> ");
            }
            else
            {
                if (mqtys == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be Zero....'); </script> ");

                }
                else
                {
                    string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
                    string qry = "Update dbo.SCSC_Truck_challan Set Challan_Date='" + disdate + "',Sendto_District='" + mtodist + "',Sendto_IC='" + mtoissue + "',Commodity='" + mcomid + "',Scheme='" + mschid + "',Bags=" + mbagno + ",Qty_send=" + mqtys + ",Challan_No='" + mchallan + "',Truck_no='" + truckno + "',Transporter='" + mtrid + "',Dispatch_Time='" + time + "',Remarks='" + remarks + "',Source='"+msource +"',Updated_Date=getdate() where Dispatch_id='" + disp_id + "'";
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--")
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Scheme....'); </script> ");

                    }
                    else
                    {
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();

                            float ruqty = CheckNull(lblqtyb.Text);
                            int uubags = CheckNullInt(lblbagsc.Text) - CheckNullInt(txtbagno.Text);
                            int mucbags = CheckNullInt(txtcurbags.Text) + uubags;
                            float buqty = CheckNull(txtbqty.Text);
                            float sdqty = CheckNull(txtquant.Text);
                            float uuqty = (buqty + ruqty) - sdqty;
                            string mcomdtyid = ddlcomdty.SelectedValue;
                            string mscheme = ddlscheme.SelectedValue;
                            string msaletype = "Other Depot";
                            string mstate = "23";
                            float msaleqty = CheckNull(txtquant.Text);


                            string mddate = "";
                            string query = "Update dbo.issue_opening_balance set Current_Balance=" + uuqty + ",Current_Bags=" + mucbags + " where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and Godown='" + mgodown + "'and Source='" + msource + "'";
                            cmd.CommandText = query;
                            cmd.Connection = con;
                            string qreySale = "insert into dbo.SCSC_Sale_Details(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Sale_Type,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + distid + "','" + issueid + "','" + mcomid + "','" + mscheme + "','" + msaletype + "'," + msaleqty + "," + month + "," + year + ",getdate(),'" + mudate + "','" + mddate + "'" + ")";
                            try
                            {
                                cmd.ExecuteNonQuery();
                                con.Close();
                                cmd.CommandText = qreySale;
                                con.Open();
                                int count = cmd.ExecuteNonQuery();
                                con.Close();

                                if (count >= 1)
                                {
                                    string qrystock = "select Sum(Qty_send) as Qty from dbo.SCSC_Truck_challan where Commodity ='" + mcomdtyid + "' and Scheme='" + mscheme + "' and Dist_ID='" + distid + "'and Depot_Id='" + issueid + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dsstock = mobj.selectAny(qrystock);

                                    if (dsstock.Tables[0].Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {
                                        DataRow drop = dsstock.Tables[0].Rows[0];
                                       
                                        float msod = CheckNull(drop["Qty"].ToString());
                                        //string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdtyid + "'and DistrictId ='" + distid + "'and DepotID='" + issueid + "'and Month=" + month + "and Year=" + year;
                                        //mobj = new MoveChallan(ComObj);
                                        //DataSet dsopen = mobj.selectAny(qryinsopen);

                                        //if (dsopen.Tables[0].Rows.Count == 0)
                                        //{
                                        //    string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + issueid + "','" + mcomdtyid + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + mros + "," + msdelo + "," + msod + "," + msos + "," + month + "," + year + ",'" + mremark + "')";
                                        //    cmd.CommandText = qryins;
                                        //    con.Open();
                                        //    cmd.ExecuteNonQuery();
                                        //    con.Close();

                                        //}
                                        //else
                                        //{
                                           string qryinsU = "update dbo.tbl_Stock_Registor set Sale_otherg=" + msod + " where Commodity_Id ='" + mcomdtyid + "' and Scheme_Id='" + mscheme + "' and DistrictId='" + distid + "'and DepotID='" + issueid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        //}
                                    }
                                }
                                btnsave.Enabled = false;
                                Update_Trans_Log();
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully....'); </script> ");
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

        }
    }

    void Update_Trans_Log()
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        DateTime my1 = Convert.ToDateTime(getDate_MDY(DaintyDate1.Text));
        string opid = Session["OperatorId"].ToString();
        string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
        string qry = "insert into dbo.SCSC_Truck_challan_Trans_Log(Dist_ID,Depot_Id,TO_Number,Challan_Date,Dispatch_Godown,Sendto_District,Sendto_IC,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,Month,Year,Updated_Date,IP_Address,Source,Operation,OperatorID) values('" + distid + "','" + issueid + "','" + txttono.Text + "','" + getDate_MDY(DaintyDate1.Text) + "','" + ddlgodown.SelectedValue + "','" + ddldistrict.SelectedValue + "','" + ddlissuecenter.SelectedValue + "','" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "'," + CheckNullInt(txtbagno.Text) + "," + CheckNull(txtquant.Text) + ",'" + txttrukcno.Text + "','" + txttruckno.Text + "','" + ddltransporter.SelectedValue + "','" + time + "','" + txtremark.Text + "','" + disp_id + "'," + my1.Month.ToString() + "," + my1.Year.ToString() + ",getdate(),'" + ip + "','" + ddlsarrival.SelectedValue + "','U','"+opid +"')";

        cmd.CommandText = qry;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {

        }
        finally
        {
        }


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
        GetDCName();

    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBalance();
    }
    public string getdateg(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd-MM-yyyy");
    }
    void GetData()
    {
        mobj1 = new MoveChallan(ComObj);
        string getdata = "SELECT SCSC_Truck_challan.Bags,SCSC_Truck_challan.TO_Number,SCSC_Truck_challan.Source,SCSC_Truck_challan.Commodity,tbl_MetaData_SCHEME.Scheme_Name, tbl_MetaData_STORAGE_COMMODITY .Commodity_Name, SCSC_Truck_challan.Sendto_District,pds.districtsmp.district_name, SCSC_Truck_challan.Sendto_IC, tbl_MetaData_DEPOT.DepotName,SCSC_Truck_challan.Dist_ID, SCSC_Truck_challan.Depot_Id, SCSC_Truck_challan.Challan_Date,SCSC_Truck_challan.Dispatch_Godown, SCSC_Truck_challan.Scheme, SCSC_Truck_challan.Bags,SCSC_Truck_challan.Qty_send, SCSC_Truck_challan.Challan_No,SCSC_Truck_challan.Remarks, SCSC_Truck_challan.Truck_no,SCSC_Truck_challan.Transporter, Transporter_Table.Transporter_Name, SCSC_Truck_challan.Dispatch_Time,SCSC_Truck_challan.Dispatch_id FROM SCSC_Truck_challan Left JOIN tbl_MetaData_STORAGE_COMMODITY ON SCSC_Truck_challan.Commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id Left JOIN pds.districtsmp ON SCSC_Truck_challan.Sendto_District = pds.districtsmp.district_code Left JOIN tbl_MetaData_DEPOT ON SCSC_Truck_challan.Sendto_IC = tbl_MetaData_DEPOT.DepotID Left JOIN Transporter_Table ON SCSC_Truck_challan.Transporter = Transporter_Table.Transporter_ID left join dbo.tbl_MetaData_SCHEME on SCSC_Truck_challan.Scheme=tbl_MetaData_SCHEME.Scheme_ID where SCSC_Truck_challan.Dispatch_id='" + disp_id + "'";
        DataSet dsgd = mobj1.selectAny(getdata);
        if (dsgd==null)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            lblbqty.Visible = false;
            txtbqty.Visible = false;
        }
        else
        {
            txttono.Visible = true;
            DataRow drgd = dsgd.Tables[0].Rows[0];
            DaintyDate1.Text = getdateg(drgd["challan_date"].ToString());
            ddlcomdty.SelectedItem.Text = drgd["Commodity_Name"].ToString();
            ddlcomdty.SelectedValue = drgd["Commodity"].ToString();
            ddlscheme.SelectedItem.Text = drgd["Scheme_Name"].ToString();
            ddlscheme.SelectedValue = drgd["Scheme"].ToString();
            txtbagno.Text = drgd["Bags"].ToString();
            txtquant.Text = drgd["Qty_send"].ToString();
            lblqtyb.Text   = drgd["Qty_send"].ToString();
            lblbagsc.Text = drgd["Bags"].ToString();
            txttrukcno.Text = drgd["Challan_No"].ToString();
            txttruckno.Text = drgd["Truck_no"].ToString();
            ddltransporter.SelectedItem.Text = drgd["Transporter_Name"].ToString();
            ddltransporter.SelectedValue = drgd["Transporter"].ToString();
            ddldistrict.SelectedItem.Text = drgd["district_name"].ToString();
            ddldistrict.SelectedValue = drgd["Sendto_District"].ToString();
            ddlissuecenter.SelectedItem.Text = drgd["DepotName"].ToString();
            ddlissuecenter.SelectedValue = drgd["Sendto_IC"].ToString();
            ddlsarrival.SelectedValue = drgd["Source"].ToString();
            lblsource.Text = drgd["Source"].ToString();
            txttono.Text = drgd["TO_Number"].ToString();
            string time = drgd["Dispatch_Time"].ToString();
            string hh = time.Substring(0, 2);
            string mm = time.Substring(3, 2);
            string ampm = time.Substring(6, 2);
            ddlhour.SelectedItem.Text = hh;
            ddlminute.SelectedItem.Text = mm;
            ddlampm.SelectedItem.Text = ampm;
            txtremark.Text = drgd["Remarks"].ToString();
            ddlgodown.SelectedValue = drgd["Dispatch_Godown"].ToString();
            GetBalance();
            
           
        }
    }

    void GetBalance()
    {
       string mcomdtyid = ddlcomdty.SelectedValue;
        //string mcatid = ddlcategory.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string godown = ddlgodown.SelectedValue;
        string msource = lblsource.Text;
        mobj1 = new MoveChallan(ComObj);
        string qry = "Select Round(Current_Balance,2) as Current_Balance,Current_Bags from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and Godown='"+godown +"'and Source='"+msource +"'";
        DataSet ds = mobj1.selectAny(qry);

        if (ds==null)
        {
             
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            lblbqty.Visible = false;
            txtbqty.Visible = false;
        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                txtbqty.Text ="0";
                txtcurbags.Text ="0";
                lblbqty.Visible = true;
                txtbqty.Visible = true;
                txtbqty.BackColor = System.Drawing.Color.Wheat;
                txtcurbags.Visible = true;
                txtcurbags.BackColor = System.Drawing.Color.Wheat;
                txtbqty.ReadOnly = true;
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtbqty.Text = dr["Current_Balance"].ToString();
                txtcurbags.Text = dr["Current_Bags"].ToString();
                lblbqty.Visible = true;
                txtbqty.Visible = true;
                txtbqty.BackColor = System.Drawing.Color.Wheat;
                txtcurbags.Visible = true;
                txtcurbags.BackColor = System.Drawing.Color.Wheat;
                txtbqty.ReadOnly = true;
            }
        }
    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddldispdipo_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsource.Text = ddlsarrival.SelectedValue;
    }
}
