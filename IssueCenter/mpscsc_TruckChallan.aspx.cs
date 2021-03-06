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


public partial class mpscsc_TruckChallan : System.Web.UI.Page
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

                GetScheme();
                GetCommodity();
                GetTransport();
                GetGodown();
                GetDist();
                GetICname();
                GetName();
                GetDCNameAll();


                DaintyDate1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
              


               
                ddlallot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddlallot_year.Items.Add(DateTime.Today.Year.ToString());
                ddlallot_year.SelectedIndex = 1;
                ddlalotmm.SelectedIndex = DateTime.Today.Month - 1;
                GetTo();
                GetChallan();
                GetSource();
                if (version == "H")
                {
                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblScheme.Text = Resources.LocalizedText.lblScheme;
                    lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                    lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                    lbltransfer.Text = Resources.LocalizedText.lbltransfer;
                    lbltransferdepot.Text = Resources.LocalizedText.lbltransferdepot;
                    lbltono.Text = Resources.LocalizedText.lbltono;
                    lblmono.Text = Resources.LocalizedText.lblmono;
                    lbldispsource.Text = Resources.LocalizedText.lbldispsource;
                    lblchallanNumber.Text = Resources.LocalizedText.lblChallanNumber;
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
                    btnsave.Text = Resources.LocalizedText.btnsave;
                    btnaddnew.Text = Resources.LocalizedText.btnaddnew;
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
    void GetGodown()
    {

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" +distid + "' and DepotId='" + issueid  + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");

       
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
    void GetDCNameAll()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Select * from tbl_MetaData_Depot order by DepotName";
        DataSet ds = distobj.selectAny(ord);

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
        DataRow dr = ds.Tables[0].Rows[0];
        //ddlgodown.SelectedItem.Text  = dr["DepotName"].ToString ();
        //ddlgodown.SelectedValue = dr["DepotID"].ToString ();
        
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
    void UpdateReceipt()
    {
        //string ro_no = ddlrono.SelectedValue;
        //string todist = ddldistrict.SelectedValue;
        //string toissue = ddlissue.SelectedValue;
        //string to_no = ddltono.SelectedValue;
        string challan = txttrukcno.Text;
        string upd = "Update dbo.tbl_Receipt_Details set Challan_Status='I' where A_dist='" + distid  + "'and A_Depo='" + issueid  + "'and challan_no='" + challan + "'";
        cmd.CommandText = upd;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();

            ddlchallan.Visible = false;

            txttrukcno.Text = "";
            txttrukcno.Visible = true;
        }
        catch (Exception ex)
        {
            Label1.Visible = true;
            Label1.Text = ex.Message;
        }
        finally
        {


        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string opid = Session["OperatorId"].ToString();

        string state = Session["State_Id"].ToString();
        DateTime my1 = Convert.ToDateTime(getDate_MDY(DaintyDate1.Text));

        string month = my1.Month.ToString();
        string year = my1.Year.ToString();

        string challan_n = txttrukcno.Text;
        mobj1 = new MoveChallan(ComObj);
        string qreychal = "select Challan_No from dbo.SCSC_Truck_challan where Depot_Id='" + issueid + "' and Dist_ID='" + distid + "' and Challan_No='" + challan_n + "'";
        DataSet dsc = mobj1.selectAny(qreychal);
        if (dsc.Tables[0].Rows.Count == 0)
        {
            mobj1 = new MoveChallan(ComObj);
            string qrey = "select max(Dispatch_id) as Dispatch_id from dbo.SCSC_Truck_challan where Depot_Id='" + issueid + "' and Dist_ID='" + distid + "'";
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
            string mtodist = lbldist.Text;
            string mtoissue = lbldepo.Text;
            string mfci = "";
            int mbagno = CheckNullInt(txtbagno.Text);
            float mqtys = CheckNull(txtquant.Text);
            float mbqty = CheckNull(txtbqty.Text);
           
            string mudate = "";
            string mdstatus = "N";
            //int month = int.Parse(DateTime.Today.Date.Month.ToString());
            //int year = int.Parse(DateTime.Today.Year.ToString());
            string remark = txtremark.Text;
            string mtono = txttono.Text;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            int bagsc = CheckNullInt(txtbagno.Text);
            string msource = ddlsarrival.SelectedValue;
            string notrans = "N";
            if (mbqty < mqtys)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Have Insufficieant Balance ....'); </script> ");
            }
            else
            {
                if (mqtys == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be Zero ....'); </script> ");
                }
                else
                {

                    string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
                    string qry = "insert into dbo.SCSC_Truck_challan(State_Id,Dist_ID,Depot_Id,TO_Number,Challan_Date,Dispatch_Godown,Sendto_District,Sendto_IC,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,OperatorID,NoTransaction) values('" + state + "','" + distid + "','" + issueid + "','" + mtono + "','" + disdate + "','" + mdepo + "','" + mtodist + "','" + mtoissue + "','" + mcomid + "','" + mschid + "'," + mbagno + "," + mqtys + ",'" + txttrukcno.Text + "','" + txttruckno.Text + "','" + mtrid + "','" + time + "','" + remark + "','" + gatepass + "','" + mdstatus + "'," + month + "," + year + ",getdate(),'" + mudate + "','" + ip + "','" + msource + "','" + opid + "','"+notrans+"')";
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--" || ddlsarrival.SelectedItem.Text == "--Select--" || ddltransporter.SelectedItem.Text == "--Select--" || ddldistrict.SelectedItem.Text == "--Select--" || ddlissuecenter.SelectedItem.Text == "--Select--")
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Scheme/Dispatch Godown/Source/Transporter/Issue Center....'); </script> ");

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
                            string source = ddlsarrival.SelectedValue;
                            string mddate = "";
                            string query = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + ruqty + ",Current_Bags=Current_Bags-" + mbagno + " where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and Godown='" + godown + "'and Source='" + source + "'";
                            cmd.CommandText = query;
                            cmd.Connection = con;
                            string qreySale = "insert into dbo.SCSC_Sale_Details(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Sale_Type,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + distid + "','" + issueid + "','" + mcomid + "','" + mscheme + "','" + msaletype + "'," + msaleqty + "," + month + "," + year + ",getdate(),'" + mudate + "','" + mddate + "'" + ")";
                            try
                            {

                                cmd.ExecuteNonQuery();
                                //string updateto = "Update dbo.Transport_Order set IsLifted='Y' where fromDistrict='" + distid + "'and fromIssueCenter='" + issueid + "'and TO_Number='" + ddltono.SelectedValue + "' and toDistrict='";
                                //con.Close();
                                //cmd.CommandText = updateto;

                                //con.Open();
                                //cmd.ExecuteNonQuery();
                                //con.Close();
                                cmd.CommandText = qreySale;
                                //con.Open();
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
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdtyid + "' and Scheme_Id='" + mscheme + "' and DistrictId ='" + distid + "'and DepotID='" + issueid + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);

                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {

                                            string qrysr = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomdtyid + "' and Scheme_Id='" + mscheme + "' and DistrictId ='" + distid + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dssr = mobj.selectAny(qrysr);

                                            if (dssr.Tables[0].Rows.Count == 0)
                                            {
                                                string chkopenss = "Select Sum(Current_Balance) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomdtyid + "' and Scheme_Id='" + mscheme + "'";
                                                mobj = new MoveChallan(ComObj);
                                                DataSet dsqry = mobj.selectAny(chkopenss);
                                                if (dsqry == null)
                                                {

                                                }

                                                else
                                                {
                                                    DataRow drss = dsqry.Tables[0].Rows[0];
                                                    float sropen = CheckNull(drss["Current_Balance"].ToString());
                                                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + mcomdtyid + "','" + mscheme + "'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtbqty.Text) + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                    cmd.CommandText = qryinsr;
                                                    con.Open();
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();

                                                }



                                            }

                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Sale_otherg=" + msod + " where Commodity_Id ='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and DistrictId='" + distid + "'and DepotID='" + issueid + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }

                                   
                                    //}



                                }
                                Update_Trans_Log( gatepass);
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                UpdateReceipt();

                            }
                            catch (Exception ex)
                            {
                                Label1.Text = ex.Message;

                            }
                            finally
                            {
                                con.Close();

                            }

                            btnsave.Enabled = false;
                            HyperLink1.Visible = true;
                            UpdateTo();
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




            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist....'); </script> ");

        }
    }

    void Update_Trans_Log(string disp_id)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string state = Session["State_Id"].ToString();
        DateTime dt = Convert.ToDateTime(DaintyDate1.Text);
        string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());
        string qry = "insert into dbo.SCSC_Truck_challan_Trans_Log(State_Id,Dist_ID,Depot_Id,TO_Number,Challan_Date,Dispatch_Godown,Sendto_District,Sendto_IC,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,Month,Year,Updated_Date,IP_Address,Source,Operation) values('" + state +"','"+ distid + "','" + issueid + "','" + txttono.Text + "','" + getDate_MDY(DaintyDate1.Text) + "','" + ddlgodown.SelectedValue + "','" + ddldistrict.SelectedValue + "','" + ddlissuecenter.SelectedValue + "','" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "'," + CheckNullInt(txtbagno.Text) + "," + CheckNull(txtquant.Text) + ",'" + txttrukcno.Text + "','" + txttruckno.Text + "','" + ddltransporter.SelectedValue + "','" + time + "','" + txtremark.Text + "','" + disp_id + "'," + dt.Month.ToString() + "," + dt.Year.ToString() + ",getdate(),'" + ip + "','" + ddlsarrival.SelectedValue + "','I')";

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
        lbldist.Text = ddldistrict.SelectedValue;
        GetDCName();

    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    void GetBalance()
    {

        string mcomdtyid = ddlcomdty.SelectedValue;
        //string mcatid = ddlcategory.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string mgdown = ddlgodown.SelectedValue;
        string msource = ddlsarrival.SelectedValue;
        mobj1 = new MoveChallan(ComObj);
        string qry = "Select Round(Current_Balance,5) as Current_Balance,Current_Bags from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and Godown='" + mgdown + "'and Source='" + msource + "'";
        DataSet ds = mobj1.selectAny(qry);

        if (ds.Tables[0].Rows.Count ==0)
        {
             
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
            txtbqty.Text = "0";
            lblbqty.Visible = true;
            txtbqty.Visible = true;
            txtbqty.BackColor = System.Drawing.Color.Wheat;
            txtcurbags.Text = "0";
            txtcurbags.ReadOnly = true;
            txtcurbags.Visible = true;
            txtcurbags.BackColor = System.Drawing.Color.Wheat;
            txtbqty.ReadOnly = true;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbqty.Text = dr["Current_Balance"].ToString();
            lblbqty.Visible = true;
            txtbqty.Visible = true;
            txtbqty.BackColor = System.Drawing.Color.Wheat;
            txtcurbags.Text = dr["Current_Bags"].ToString();
            txtcurbags.ReadOnly  =true;
            txtcurbags.Visible  =true;
            txtcurbags.BackColor = System.Drawing.Color.Wheat;
            txtbqty.ReadOnly = true;
        }
    }
    void GetTo()
     {
        mobj = new MoveChallan(ComObj);
        string month=ddlalotmm.SelectedValue;
        string year=ddlallot_year.SelectedValue;
        string qry = "SELECT distinct(TO_Number) as TO_Number   FROM dbo.Transport_Order where fromDistrict='" + distid + "'and fromIssueCenter='" + issueid + "'and IsLifted='N' and Source_ID=02 and Month="+month+" and Year="+year;
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
             
             //ddltransporter.SelectedItem.Text = dr["Transporter_Name"].ToString();
             //ddltransporter.SelectedValue = dr["Transporter_ID"].ToString();
             //ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
             //ddlissuecenter.SelectedItem.Text = dr["DepotName"].ToString();
             //lbldist.Text  = dr["toDistrict"].ToString();
             //lbldepo.Text = dr["toIssueCenter"].ToString();
             txtbagno.Focus();
             

         }
    }

    void getToDetails()
    {
        string tono = ddltono.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qryro = "SELECT Transport_Order.Trunsuction_Id,Transport_Order.toDistrict,Transport_Order.toIssueCenter,pds.districtsmp.district_name, tbl_MetaData_DEPOT.DepotName, Transport_Order.Quantity, Transport_Order.fromDistrict,Transport_Order.fromIssueCenter, Transport_Order.TO_Number,isnull(Sum(SCSC_Truck_challan.Qty_send),0) as Qty_send ,(isnull((Transport_Order.Quantity-isnull(Sum(SCSC_Truck_challan.Qty_send),0)),0) ) as Pending FROM Transport_Order LEFT JOIN  SCSC_Truck_challan ON Transport_Order.toIssueCenter = SCSC_Truck_challan.Sendto_IC AND Transport_Order.toDistrict = SCSC_Truck_challan.Sendto_District AND Transport_Order.fromDistrict = SCSC_Truck_challan.Dist_ID AND Transport_Order.fromIssueCenter = SCSC_Truck_challan.Depot_Id AND Transport_Order.TO_Number = SCSC_Truck_challan.TO_Number  left JOIN pds.districtsmp ON Transport_Order.toDistrict = pds.districtsmp.district_code  left JOIN tbl_MetaData_DEPOT ON Transport_Order.toIssueCenter = tbl_MetaData_DEPOT.DepotID where Transport_Order.TO_Number='" + tono + "' and Transport_Order.fromDistrict='" + distid + "' and Transport_Order.fromIssueCenter='" + issueid + "' group by Transport_Order.Trunsuction_Id,Transport_Order.toDistrict,Transport_Order.toIssueCenter,pds.districtsmp.district_name, tbl_MetaData_DEPOT.DepotName, Transport_Order.Quantity, Transport_Order.fromDistrict,Transport_Order.fromIssueCenter, Transport_Order.TO_Number";
        //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

        DataSet dstod = mobj.selectAny(qryro);
        if (dstod == null)
        {
            //lbltoqty.Visible = true ;
            //txttoqty.Visible = true;
            //lblpendingqty.Visible = true;
            //txttopending.Visible = true;
            btngetdtl.Visible = false ;
        }
        else
        {
            if (dstod.Tables[0].Rows.Count == 0)
            {
                //lbltoqty.Visible = true;
                //txttoqty.Visible = true;
                //lblpendingqty.Visible = true;
                //txttopending.Visible = true;
                btngetdtl.Visible = false;
                GridView2.Visible = false;
            }
            else
            {
                GridView2.Visible = true;
               
              
               
              
                btngetdtl.Visible = true;
                this.GridView2.DataSource = dstod;
                this.GridView2.DataBind();

            }
           
        }
        
    }
    protected void ddltono_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltono.SelectedItem.Text == "Not Indicated")
        {
            lblmono.Visible = true;
            txttono.Visible = true;
            txttono.Text = "";
            txttono.Focus();
            getToDetails();
        }
        else
        {
            lblmono.Visible = true;
            txttono.Visible = true;
            txttono.Text = ddltono.SelectedValue;
            txttono.ReadOnly = true;
            getToDetails();
            GetData();
            //GetLiftQty();
        }
    }
    //void GetLiftQty()
    //{
    //    string tono = txttono.Text;
    //    string qrystock = "select Sum(Qty_send) as Qty from dbo.SCSC_Truck_challan where Dist_ID='" + distid + "'and Depot_Id='" + issueid + "'and TO_Number='" + tono +"'";
    //    mobj = new MoveChallan(ComObj);
    //    DataSet dsstock = mobj.selectAny(qrystock);

    //    if (dsstock.Tables[0].Rows.Count == 0)
    //    {

    //    }
    //    else
    //    {
    //        DataRow drop = dsstock.Tables[0].Rows[0];
    //        float msod = CheckNull(drop["Qty"].ToString());
          
    //    }


    //}
    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldepo.Text = ddlissuecenter.SelectedValue;
    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        txttrukcno.Text = ddlchallan.SelectedValue;
        GetChallanData();

    }
    void GetChallan()
    {
        //string ro_no = ddlrono.SelectedValue;
        //string todist = ddldistrict.SelectedValue;
        //string toissue = ddlissuecenter.SelectedValue;
        string to_no = ddltono.SelectedValue;
        string gchalan = "Select challan_no from dbo.tbl_Receipt_Details where A_dist='" + distid  + "'and A_Depo='" + issueid   + "' and Challan_Status='N'";
        mobj = new MoveChallan(ComObj);
        DataSet dsch = mobj.selectAny(gchalan);
        if (dsch == null)
        {
            ddlchallan.Visible = false;
            txttrukcno.Visible = true;
            lblstatus.Text = "N";

        }
        else
        {
            if (dsch.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                lblstatus.Text = "Y";
                ddlchallan.Visible = true;
                txttrukcno.Visible = false;
                DataRow drch = dsch.Tables[0].Rows[0];
                ddlchallan.DataSource = dsch;
                ddlchallan.DataTextField = "challan_no";
                ddlchallan.DataValueField = "challan_no";
                ddlchallan.DataBind();
                ListItem lst = new ListItem();
                lst.Text = "Not Indicated";
                lst.Value = "NA";
                ddlchallan.Items.Insert(0, "--Select--");
                ddlchallan.Items.Insert(1,lst);
            }

        }
    }
    void GetChallanData()
    {     
       
        string to_no = ddltono.SelectedValue;
        string challan = txttrukcno.Text ;
        string qrychalan = "Select challan_no,Dispatch_Date,challan_date,Qty,Crop_year,Category,Vehile_no,No_of_Bags,Moisture from dbo.tbl_Receipt_Details where A_dist='" + distid  + "'and A_Depo='" + issueid  +"'and Challan_Status='N' and challan_no='" + challan + "'";
        mobj = new MoveChallan(ComObj);
        DataSet dschdt = mobj.selectAny(qrychalan);
        if (dschdt == null)
        {

        }
        else
        {
            if (dschdt.Tables[0].Rows.Count == 0)
            {
                txttrukcno.Visible = true;
                txttrukcno.Text = "";
                txttrukcno.Focus();

            }
            else
            {
                txttrukcno.Visible = false;
                DataRow drchdt = dschdt.Tables[0].Rows[0];
                  
                txttruckno.Text = drchdt["Vehile_no"].ToString();
                txtbagno.Text = drchdt["No_of_Bags"].ToString();
                txtquant.Text = drchdt["Qty"].ToString();
                
            }

        }


    }
    protected void ddldispdipo_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void txtrono_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/mpscsc_TruckChallan.aspx");
       
        
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    //Find the checkbox control in header and add an attribute
        //    ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
        //}
    }
    protected void btngetdtl_Click(object sender, EventArgs e)
    {
        
        
        //
        //GridView2.Columns[6].Visible =true ;
        //GridView2.Columns[8].Visible =true ;
        //GridView2.Columns[12].Visible = true;

        if (GridView2.Rows.Count == 0)
        {
        }
        else
        {

            foreach (GridViewRow gr in GridView2.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

                if (GchkBx.Checked == true)
                {
                   
                    string todist = gr.Cells[7].Text;
                    string tdepot = gr.Cells[8].Text;
                    lbldist.Text = gr.Cells[7].Text;
                    lbldepo.Text = gr.Cells[8].Text;
                    lblpendqty.Text = gr.Cells[6].Text;
                    lbltid.Text = gr.Cells[9].Text;
                    string mtono = ddltono.SelectedValue;
                    
                    mobj = new MoveChallan(ComObj);
                    string qryro = "SELECT Transport_Order.toDistrict,Transport_Order.toIssueCenter,Transport_Order.Trunsuction_Id,Transport_Order.toDistrict,Transport_Order.toIssueCenter,pds.districtsmp.district_name, tbl_MetaData_DEPOT.DepotName, Transport_Order.Quantity, Transport_Order.fromDistrict,Transport_Order.fromIssueCenter, Transport_Order.TO_Number,isnull(SCSC_Truck_challan.Qty_send,0) as Qty_send ,(isnull((Transport_Order.Quantity-SCSC_Truck_challan.Qty_send),0) ) as Pending FROM Transport_Order LEFT JOIN  SCSC_Truck_challan ON Transport_Order.toIssueCenter = SCSC_Truck_challan.Sendto_IC AND Transport_Order.toDistrict = SCSC_Truck_challan.Sendto_District AND Transport_Order.fromDistrict = SCSC_Truck_challan.Dist_ID AND Transport_Order.fromIssueCenter = SCSC_Truck_challan.Depot_Id AND Transport_Order.TO_Number = SCSC_Truck_challan.TO_Number  left JOIN pds.districtsmp ON Transport_Order.toDistrict = pds.districtsmp.district_code  left JOIN tbl_MetaData_DEPOT ON Transport_Order.toIssueCenter = tbl_MetaData_DEPOT.DepotID where Transport_Order.TO_Number='" + mtono + "' and Transport_Order.toDistrict='" + todist + "' and Transport_Order.toIssueCenter='" + tdepot + "'";
                    //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

                    DataSet dstod = mobj.selectAny(qryro);
                    
                    DataRow dr = dstod.Tables[0].Rows[0];
                  
                    //txtqtysend.Text = dr["Quantity"].ToString();
                    
                    ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
                    ddldistrict.SelectedValue = dr["toDistrict"].ToString();
                    ddlissuecenter.SelectedItem.Text = dr["DepotName"].ToString();
                    ddlissuecenter.SelectedValue = dr["toIssueCenter"].ToString();
                    //ddldistrict.BackColor = System.Drawing.Color.CadetBlue;

                    //ddlissue.BackColor = System.Drawing.Color.CadetBlue;
                    //ddlfcidist.BackColor = System.Drawing.Color.CadetBlue;
                    //ddlfcidepo.BackColor = System.Drawing.Color.CadetBlue;
                    //txtqtysend.ReadOnly = true;

                    //txttrans.BackColor = System.Drawing.Color.CadetBlue;
                    ddldistrict.Enabled = false;
                    ddlissuecenter.Enabled = false;
                    GetChallan();
                    //Label5.Visible = true;
                  
                    ddldistrict.Visible = true;
                  


                }
                else
                {
                    if (GchkBx.Checked == false)
                    {
                    }
                    else
                    {
                      
                        GetDist();
                      
                        GetDCName();
                        ddldistrict.BackColor = System.Drawing.Color.White;
                        ddldistrict.Enabled = true;
                      
                    }
                }


            }
        }
        //GridView2.Columns[6].Visible =false ;
        //GridView2.Columns[8].Visible =false;
        //GridView2.Columns[12].Visible = false;

    }
    void UpdateTo()
    {
        string tono = ddltono.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qryrotou = "SELECT Transport_Order.Trunsuction_Id,Transport_Order.toDistrict,Transport_Order.toIssueCenter,pds.districtsmp.district_name, tbl_MetaData_DEPOT.DepotName, Transport_Order.Quantity, Transport_Order.fromDistrict,Transport_Order.fromIssueCenter, Transport_Order.TO_Number,isnull(Sum(SCSC_Truck_challan.Qty_send),0) as Qty_send ,(isnull((Transport_Order.Quantity-Sum(SCSC_Truck_challan.Qty_send)),0) ) as Pending FROM Transport_Order LEFT JOIN  SCSC_Truck_challan ON Transport_Order.toIssueCenter = SCSC_Truck_challan.Sendto_IC AND Transport_Order.toDistrict = SCSC_Truck_challan.Sendto_District AND Transport_Order.fromDistrict = SCSC_Truck_challan.Dist_ID AND Transport_Order.fromIssueCenter = SCSC_Truck_challan.Depot_Id AND Transport_Order.TO_Number = SCSC_Truck_challan.TO_Number  left JOIN pds.districtsmp ON Transport_Order.toDistrict = pds.districtsmp.district_code  left JOIN tbl_MetaData_DEPOT ON Transport_Order.toIssueCenter = tbl_MetaData_DEPOT.DepotID where Transport_Order.TO_Number='" + tono + "' and Transport_Order.fromDistrict='" + distid + "' and Transport_Order.Trunsuction_Id='" + lbltid.Text + "' and Transport_Order.fromIssueCenter='" + issueid + "' group by Transport_Order.Trunsuction_Id,Transport_Order.toDistrict,Transport_Order.toIssueCenter,pds.districtsmp.district_name, tbl_MetaData_DEPOT.DepotName, Transport_Order.Quantity, Transport_Order.fromDistrict,Transport_Order.fromIssueCenter, Transport_Order.TO_Number";
        DataSet dsto = mobj.selectAny(qryrotou);
        if (dsto == null)
        {
        }
        else
        {
            if (dsto.Tables[0].Rows.Count == 0)
            {



            }
            else
            {
                DataRow drto = dsto.Tables[0].Rows[0];
                float tpending = CheckNull(drto["Pending"].ToString());
                if (tpending == 0)
                {

                    string updateto = "Update dbo.Transport_Order set IsLifted='Y' where fromDistrict='" + distid + "'and fromIssueCenter='" + issueid + "'and TO_Number='" + ddltono.SelectedValue + "' and Trunsuction_Id='" + lbltid.Text +"'";
                    cmd.CommandText = updateto;
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
                    }
                }
                else
                {


                }



            }
        }
    }
    protected void txttoqty_TextChanged(object sender, EventArgs e)
    {
        //string tono = txttono.Text;
        //mobj = new MoveChallan(ComObj);
        //string getdata = "SELECT sum(Qty_send) as Qty_Send  from dbo.SCSC_Truck_challan where SCSC_Truck_challan.Dist_ID='"+distid +"' and SCSC_Truck_challan.Depot_Id='"+issueid +"' and SCSC_Truck_challan.TO_Number='"+tono +"'";
        //DataSet ds = mobj.selectAny(getdata);
        //if (ds.Tables[0].Rows.Count == 0)
        //{



        //}
        //else
        //{
        //    DataRow dr=ds.Tables[0].Rows[0];
        //    txttopending.Text = dr["Qty_Send"].ToString();


        //}
    }
    protected void ddlalotmm_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTo();
    }
    protected void ddlallot_year_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
