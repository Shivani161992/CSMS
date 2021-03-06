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
public partial class Admin_Edit_Transport_Order_Page : System.Web.UI.Page


{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    LARO obj = null;
    LARO objo = null;
    public string distid = "";
    public string adminid = "";
    string roqty = null;
    MoveChallan mobj = null;
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();

            txttorderno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtroqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtsendqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtcumlqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtbalqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txttorderno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtsendqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcumlqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbalqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcommodity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtscheme.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrodate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
              
           


            //txtchallan.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtqtysend.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");



            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                //GetTransport();
               
                //GetName();
                GetDist();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }

    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdistrict.Text = dr1dt["district_name"].ToString();
        txtdistrict.ReadOnly = true;
        txtdistrict.BackColor = System.Drawing.Color.Wheat;
       
       
    }

    void GetRO()
    {
        string distid = ddldistrict.SelectedValue;
       ddlrono.Items.Insert(0, "--Select--");
        string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'";
        cmd.Connection = con;
        cmd.CommandText = qry;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrono.Items.Add(dr["RO_No"].ToString());



        }
        dr.Close();
        con.Close();

    }

    //void GetTransport()
    //{
    //    tobj = new Transporter(ComObj);
    //    string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid + "' and IsActive='Y'";

    //    DataSet ds = tobj.selectAny(qry);

    //    ddltransport.DataSource = ds.Tables[0];
    //    ddltransport.DataTextField = "Transporter_Name";
    //    ddltransport.DataValueField = "Transporter_ID";
    //    ddltransport.DataBind();
    //    ddltransport.Items.Insert(0, "--Select--");

    //}

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
    //void GetDCName()
    //{

    //    distobj = new DistributionCenters(ComObj);
    //    string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
    //    DataSet ds = distobj.select(ord);

    //    ddlissue.DataSource = ds.Tables[0];
    //    ddlissue.DataTextField = "DepotName";
    //    ddlissue.DataValueField = "DepotId";

    //    ddlissue.DataBind();
    //    ddlissue.Items.Insert(0, "--Select--");

    //    // ddDistId.Items.Insert(0, "--चुनिये--");
    //}
    void GetData()
    {
        string distid = ddldistrict.SelectedValue;
        if (ddlrono.SelectedItem.Text != "--Select--")
        {
            obj = new LARO(ComObj);
            string qryall = "SELECT RO_of_FCI.Commodity AS Expr1,Transport_Order_againstRo.Cumulative_Qty as Cumulative_Qty , RO_of_FCI.Distt_Id, RO_of_FCI.RO_No, RO_of_FCI.RO_Validity, RO_of_FCI.RO_date, RO_of_FCI.RO_qty,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date, RO_of_FCI.Balance_Qty,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name  From dbo.RO_of_FCI Left JOIN tbl_MetaData_STORAGE_COMMODITY ON RO_of_FCI.Commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transport_Order_againstRo on RO_of_FCI.RO_No=Transport_Order_againstRo.RO_No left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=tbl_MetaData_SCHEME.Scheme_id  where RO_of_FCI.RO_No='" + ddlrono.SelectedItem + "' and RO_of_FCI.Distt_Id='" + distid + "'";
            DataSet ds = obj.selectAny(qryall);
            DataRow dr = ds.Tables[0].Rows[0];

            string rdate = dr["RO_Validity"].ToString();
            string rodate = getdate(rdate);
            txtrodate.Text = rodate;
            txtrodate.ReadOnly = true;
            txtrodate.BackColor = System.Drawing.Color.Wheat;

            roqty = dr["RO_qty"].ToString();
            txtroqty.Text = dr["RO_qty"].ToString();
            txtroqty.ReadOnly = true;
            txtroqty.BackColor = System.Drawing.Color.Wheat;



            txtbalqty.Text = dr["Balance_Qty"].ToString();
            txtbalqty.ReadOnly = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;

            string cumqtyqry = "Select Cumulative_Qty ,Pending_Qty from dbo.TO_Allot_Lift where RO_No='" + ddlrono.SelectedItem + "' and Distt_Id='" + distid + "'";

            txtcumlqty.Text = dr["Cumulative_Qty"].ToString();
            txtcommodity.Text = dr["Commodity_Name"].ToString();
            txtcommodity.ReadOnly = true;
            txtcommodity.BackColor = System.Drawing.Color.Wheat;
            txtscheme.Text = dr["Scheme_Name"].ToString();
            txtscheme.ReadOnly = true;
            txtscheme.BackColor = System.Drawing.Color.Wheat;


            //txtbalqty.Text = dr["Pending_Qty"].ToString();
            DataSet dscq = obj.selectAny(cumqtyqry);
            if (dscq.Tables[0].Rows.Count==0)
            {
                txtcumlqty.Text = "0";
                txtcumlqty.ReadOnly = true;
                txtcumlqty.BackColor = System.Drawing.Color.Wheat;
            }
            else
            {

                DataRow drcq = dscq.Tables[0].Rows[0];
                txtcumlqty.Text = drcq["Cumulative_Qty"].ToString();
                txtbalqty.Text = drcq["Pending_Qty"].ToString();
                txtcumlqty.ReadOnly = true;
                txtcumlqty.BackColor = System.Drawing.Color.Wheat;
            }


        }
        else
        {
            txtrodate.Text = "";
            txtroqty.Text = "";
          
            txtbalqty.Text = "";
        }

    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
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
        int d = (m * 30) + d2;
        int day = d - d1;
        return day.ToString();
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        //string mroto = ddlrono.SelectedValue;
        //string mtonoRO = txttorderno.Text;

        //string qryTODR = "Select * from dbo.Transport_Order_againstRo  where RO_No='" + mroto + "' and Distt_Id='" + distid + "' and TO_Number='" + mtonoRO + "'";
        //obj = new LARO(ComObj);

        //DataSet dstoDR = obj.selectAny(qryTODR);
        // if (dstoDR.Tables[0].Rows.Count==0)
        //{
        //    if (ddlrono.SelectedItem.Text == "--Select--" || ddltransport.SelectedItem.Text == "--Select--")
        //    {
        //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select The RO Number  And Transporter Name '); </script> ");
        //    }
        //    else
        //    {
        //        DateTime fdate = new DateTime();
        //        DateTime tdate = new DateTime();

        //        string fromdate = getmmddyy(txtrodate.Text);
        //        //string  todate =  getDate_MDY(DaintyDate1.Text);

        //        fdate = DateTime.Parse(fromdate.ToString());
        //        //string todate = getDate_MDY(DaintyDate1.Text);
        //        //tdate = Convert.ToDateTime(todate);


        //        string validity = get_days(DaintyDate1.SelectedDate, fdate);
        //        if (int.Parse(validity) < 0)
        //        {
        //            string RO_NO = ddlrono.SelectedValue;

        //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Validity has been expired'); </script> ");

        //            string qrytoupdate = "Update dbo.TO_Allot_Lift set Locked='N' where RO_No='" + RO_NO + "' and Distt_Id='" + distid + "'";

        //            cmd.CommandText = qrytoupdate;
        //            cmd.Connection = con;
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();

        //        }
        //        else
        //        {
        //            int bqty = CheckNullInt(txtbalqty.Text);
        //            int sqty = CheckNullInt(txtsendqty.Text);

        //            if (sqty > bqty || sqty == 0)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sending Qty Should Not Be Greater Than Pending Qty or 0'); </script> ");
        //            }
        //            else
        //            {


        //                string mrono = ddlrono.SelectedItem.ToString();
        //                float mroqty = CheckNull(txtroqty.Text);
        //                string mrdate = DateTime.Today.Date.ToString();
        //                string mtono = txttorderno.Text;
        //                string mtodate = getDate_MDY(DaintyDate1.Text);
        //                string mtaname = ddltransport.SelectedValue;
        //                float miqty = CheckNull(txtsendqty.Text);
        //                float mcumqty = CheckNull(txtcumlqty.Text);
        //                float mpqty = CheckNull(txtbalqty.Text);
        //                string mcdate = getDate_MDY(DateTime.Today.Date.ToString());
        //                int month = int.Parse(DateTime.Today.Month.ToString());
        //                int year = int.Parse(DateTime.Today.Year.ToString());


        //                string udate = "";
        //                string ddate = "";
        //                string tid = "1";

        //                int balamt = int.Parse((txtbalqty.Text)) - int.Parse((txtsendqty.Text));

        //                string Balance_Qty = balamt.ToString();
        //                float balqty = CheckNull(Balance_Qty);

        //                float cumqty = CheckNull(txtcumlqty.Text);
        //                float sendqty = CheckNull(txtsendqty.Text);
        //                float tcumqty = cumqty + sendqty;
        //                string mtid = ddltransport.SelectedValue;
        //                string todist = ddldistrict.SelectedValue;
        //                string toissuecenter = ddlissue.SelectedValue;
        //                string lift = "N";
        //                if (ddltransport.SelectedItem.Text == "--Select--")
        //                {
        //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select The Transporter..'); </script> ");
        //                }
        //                else
        //                {
        //                    string qry = "insert into dbo.Transport_Order_againstRo(Distt_Id,RO_No,RO_qty,RO_Validity,TO_Number,TO_Date,Transporter_Name,toDistrict,toIssueCenter,Quantity,Cumulative_Qty,Pending_Qty,Month,Year,Trunsuction_Id,IsLifted,Created_date,updated_date,deleted_date)values('" + distid + "','" + mrono + "'," + mroqty + ",'" + mrdate + "','" + mtono + "','" + mtodate + "','" + mtaname + "','"+todist +"','"+toissuecenter +"',"+ miqty + "," + tcumqty + "," + balamt + "," + month + "," + year + ",'" + tid + "','" + lift + "',getdate(),'" + udate + "','" + ddate + "')";
        //                    cmd.CommandText = qry;
        //                    cmd.Connection = con;
        //                    //string uquery = "update  dbo.RO_of_FCI set Balance_Qty=" + balqty + "where RO_No='" + mrono + "' and Distt_Id='" + distid + "'";



        //                    //DataRow drto = dsto.Tables[0].Rows[0];



        //                    try
        //                    {
        //                        con.Open();
        //                        int count = cmd.ExecuteNonQuery();

        //                        if (count == 1)
        //                        {

        //                            //cmd.CommandText = uquery;
        //                            //cmd.ExecuteNonQuery();


        //                            string qryTO = "Select * from dbo.TO_Allot_Lift where RO_No='" + mrono + "' and Distt_Id='" + distid + "'";
        //                            obj = new LARO(ComObj);

        //                            DataSet dsto = obj.selectAny(qryTO);
        //                             if (dsto.Tables[0].Rows.Count==0)
        //                            {
        //                                string locked = "Y";
        //                                float liftqty = 0;
        //                                float pqty = CheckNull(txtbalqty.Text) - CheckNull(txtsendqty.Text);
        //                                string qrytoinsert = "insert into dbo.TO_Allot_Lift(Distt_Id,RO_No,RO_qty,RO_Validity,Transporter_ID,Cumulative_Qty,Pending_Qty,Lifted_Qty,Month,Year,Created_date,Locked)values('" + distid + "','" + mrono + "'," + mroqty + ",'" + mrdate + "','" + mtid + "'," + tcumqty + "," + pqty + ","+liftqty +"," + month + "," + year + ",getdate(),'" + locked + "')";
        //                                cmd.CommandText = qrytoinsert;
        //                                cmd.ExecuteNonQuery();

        //                            }
        //                            else
        //                            {
        //                                float pqty = CheckNull(txtbalqty.Text) - CheckNull(txtsendqty.Text);

        //                                string qrytoupdate = "Update dbo.TO_Allot_Lift set RO_qty=" + mroqty + ",Cumulative_Qty=" + tcumqty + ",Pending_Qty=" + pqty + ",Month=" + month + ",Year=" + year + ",Created_date='" + udate + "'where RO_No='" + mrono + "' and Distt_Id='" + distid + "'";
        //                                cmd.CommandText = qrytoupdate;
        //                                cmd.ExecuteNonQuery();


        //                            }


        //                        }
        //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
        //                        btnsave.Enabled = false;
        //                        fillgrid();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Label1.Visible = true;
        //                        Label1.Text = ex.Message;

        //                    }
        //                    finally
        //                    {
        //                        con.Close();
        //                        ComObj.CloseConnection();


        //                    }

        //                }

        //                con.Open();
        //            }


        //        }




        //    }
        //}
            

       
        //else
        //{


        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Number Already Exist ..... '); </script> ");
        //}
            

    }
    protected void ddlrono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
        //GetTOTransport();
        fillgrid();
        //txttorderno.Focus();
    }
   

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string rono = dgridchallan.SelectedRow.Cells[1].Text;
        string tono = dgridchallan.SelectedRow.Cells[2].Text;
        string id = dgridchallan.SelectedRow.Cells[8].Text;
        string toqty = dgridchallan.SelectedRow.Cells[4].Text;
        string liftqty = dgridchallan.SelectedRow.Cells[5].Text;
        string pendtqty = dgridchallan.SelectedRow.Cells[6].Text;
        string toissue = dgridchallan.SelectedRow.Cells[10].Text;
        Session["RO_No"] = rono ;
        Session["TO_No"] = tono ;
        Session["ID"] = id;
        Session["Toqty"] = toqty;
        Session["LiftQty"] = liftqty;
        Session["PendQty"] = pendtqty;
        Session["Distid"] = ddldistrict.SelectedValue;
        Session["Issue"] = toissue;
        Response.Redirect("../Admin/Edit_TransportOrder.aspx");

    }
   
    void fillgrid()
    {
        string distid = ddldistrict.SelectedValue;
        string mrono = ddlrono.SelectedItem.ToString();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Transport_Order_againstRo.toIssueCenter,tbl_MetaData_DEPOT.DepotName,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.Trunsuction_Id,Transport_Order_againstRo.Pending_Qty,Transport_Order_againstRo.RO_No,Transport_Order_againstRo.Quantity,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.TO_Date,Transport_Order_againstRo.Transporter_Name,Transporter_Table.Transporter_Name as Tname FROM dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID left join dbo.tbl_MetaData_DEPOT  on Transport_Order_againstRo.toIssueCenter= tbl_MetaData_DEPOT.DepotID where Transport_Order_againstRo.Distt_Id='" + distid + "'and Transport_Order_againstRo.RO_No ='" + mrono + "'";
        DataSet ds = mobj.selectAny(qry);
        dgridchallan.DataSource = ds.Tables[0];
        dgridchallan.DataBind();


    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TO_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    void GetTOTransport()
    {
        string mrono = ddlrono.SelectedItem.ToString();
        string ttid = ddltransport.SelectedValue;
        string qryTO = "Select TO_Allot_Lift.Locked,TO_Allot_Lift.RO_NO,TO_Allot_Lift.Transporter_ID,Transporter_Table.Transporter_Name as Transporter_Name  from dbo.TO_Allot_Lift left join dbo.Transporter_Table on TO_Allot_Lift.Transporter_ID=Transporter_Table.Transporter_ID  where TO_Allot_Lift.Distt_Id='" + distid + "'and TO_Allot_Lift.RO_NO='" + mrono + "'";
        obj = new LARO(ComObj);

        DataSet dsto = obj.selectAny(qryTO);
         if (dsto.Tables[0].Rows.Count==0)
        {
            ddltransport.Enabled = true;
            ddltransport.BackColor = System.Drawing.Color.White;
            //GetTransport();



        }
        else
        {
            
            DataRow drchk = dsto.Tables[0].Rows[0];
            string Status = drchk["Locked"].ToString();
            string mdro = drchk["RO_NO"].ToString();

            ddltransport.SelectedValue = drchk["Transporter_ID"].ToString();
            ddltransport.SelectedItem.Text = drchk["Transporter_Name"].ToString();
            //ddltransport.Enabled = false;
            //ddltransport.BackColor = System.Drawing.Color.Wheat;
           
        }

    }
    protected void ddltransport_SelectedIndexChanged(object sender, EventArgs e)
    {
       // string mrono = ddlrono.SelectedItem.ToString();
       // string ttid = ddltransport.SelectedValue;
       // string qryTO = "Select Locked,RO_NO from dbo.TO_Allot_Lift where Distt_Id='" + distid + "'and Transporter_ID='" + ttid + "'";
       // obj = new LARO(ComObj);

       // DataSet dsto = obj.selectAny(qryTO);
       //if (dsto.Tables[0].Rows.Count == 0)
       // {
            

       // }
       // else
       // {
       //     DataRow drchk = dsto.Tables[0].Rows[0];
       //     string Status = drchk["Locked"].ToString();
       //     string mdro=drchk["RO_NO"].ToString();
       //     if (Status.Trim ()=="Y" && mrono == mdro)
       //     {
       //         ddldistrict.Focus();
              
                

       //     }
       //     else
       //     {
       //         if (Status.Trim() == "N")
       //         {

       //         }
       //         else
       //         {
       //             Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('This Transporter is Locked Please Select another one..'); </script> ");
       //             ddltransport.Focus();
       //             GetTransport();
       //         }


       //     }

            


       // }
    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        if (dgridchallan.PageCount == 0)
        {
        }
        else
        {
            string arg;
            arg = e.CommandArgument.ToString();

            switch (arg)
            {
                case "next":
                    //The next Button was Clicked
                    if ((dgridchallan.PageIndex < (dgridchallan.PageCount - 1)))
                    {
                        dgridchallan.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((dgridchallan.PageIndex > 0))
                    {
                        dgridchallan.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    dgridchallan.PageIndex = (dgridchallan.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    dgridchallan.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            fillgrid();
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRO(); 
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/TransportOrder_Type.aspx");
    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
