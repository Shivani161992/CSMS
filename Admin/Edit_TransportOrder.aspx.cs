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
public partial class Admin_Edit_TransportOrder : System.Web.UI.Page

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
    public string sid = "";
    string roqty = null;
    MoveChallan mobj = null;
    public string getdatef = "";
    DataTable dt = new DataTable();
    float disqty = 0;
    string transuct = "";
    long  transnum = 0;
    string rono = "";
    string tono = "";
    string stid = "";
    string toqty = "";
    string liftqty = "";
    string adminid = "";
    string pendqty = "";
    string toissue= "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();
            rono = Session["RO_No"].ToString();
            tono= Session["TO_No"].ToString ();
            stid= Session["ID"].ToString ();
            toqty= Session["Toqty"].ToString ();
            liftqty= Session["LiftQty"].ToString ();
            pendqty = Session["PendQty"].ToString();
            distid = Session["Distid"].ToString();
            toissue=Session["Issue"].ToString();
            //txttorderno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            
            txtsendqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            
            txttono.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        
            txtsendqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttono.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttoqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcumlqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbalqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcommodity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtscheme.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrodate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrono.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();



           
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                GetTransport();
                //GetRO();
                //GetName();
                GetDist();
                GetFCIdist();
                dt.Columns.Add("RO_No");
                dt.Columns.Add("TO_No");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("DisName");
                dt.Columns.Add("IssueName");
                dt.Columns.Add("District");
                dt.Columns.Add("IssueCenter");
                lbltoqty.Text = toqty;
                Session["dt"] = dt;
                ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
                ddd_allot_year.SelectedIndex = 1;
                ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
                txtrono.Text = rono;
                txttono.Text = tono;
                txttoqty.Text = toqty;
                txtcumlqty.Text = liftqty;
                txttid.Text = stid;
                float balance=CheckNull (toqty)-CheckNull (liftqty);
                txtbalqty.Text=pendqty;
                GetData();
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
    //    string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
    //    DataSet ds1dt = mobj.selectAny(qry1dt);
    //    DataRow dr1dt = ds1dt.Tables[0].Rows[0];
    //    txtdistrict.Text = dr1dt["district_name"].ToString();
    //    txtdistrict.ReadOnly = true;
    //    txtdistrict.BackColor = System.Drawing.Color.Wheat;


    //}

   
    void GetTransport()
    {
        tobj = new Transporter(ComObj);
        string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid + "' and IsActive='Y'";

        DataSet ds = tobj.selectAny(qry);

        ddltransport.DataSource = ds.Tables[0];
        ddltransport.DataTextField = "Transporter_Name";
        ddltransport.DataValueField = "Transporter_ID";
        ddltransport.DataBind();
        ddltransport.Items.Insert(0, "--Select--");

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

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetFCIdist()
    {
        obj = new LARO(ComObj);
        string qry = "select districtsmp.district_name as dist_name,DepoCode.district_code as dist_code From dbo.DepoCode left join pds.districtsmp   on upper(DepoCode.district)=upper( districtsmp.district_name) group by districtsmp.district_name, DepoCode.district_code";
        DataSet ds = obj.selectAny(qry);

        ddlfcidist.DataSource = ds.Tables[0];
        ddlfcidist.DataTextField = "dist_name";
        ddlfcidist.DataValueField = "dist_code";
        ddlfcidist.DataBind();
        ddlfcidist.Items.Insert(0, "--Select--");

    }
    void GetFCIdepot()
    {
        string dtype = ddldepottype.SelectedItem.ToString();
        string dcode = ddlfcidist.SelectedValue;
        obj = new LARO(ComObj);
        string qry = "select distinct(DepoName) as depo_name  ,DepoCode as depo_code,type From dbo.DepoCode where district_code='" + dcode + "'";//and type='" + dtype + "'";
        DataSet ds = obj.selectAny(qry);

        ddlfcidepo.DataSource = ds.Tables[0];
        ddlfcidepo.DataTextField = "depo_name";
        ddlfcidepo.DataValueField = "depo_code";
        ddlfcidepo.DataBind();
        ddlfcidepo.Items.Insert(0, "--Select--");

    }
    void GetData()
    {
      
            obj = new LARO(ComObj);
            string qryall = "SELECT Transport_Order_againstRo.Transporter_Name,Transporter_Table.Transporter_Name as Transporter,TO_Allot_Lift.Lifted_Qty,RO_of_FCI.Commodity AS Expr1,Transport_Order_againstRo.Cumulative_Qty as Cumulative_Qty , RO_of_FCI.Distt_Id, RO_of_FCI.RO_No, RO_of_FCI.RO_Validity, RO_of_FCI.RO_date, RO_of_FCI.RO_qty,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date, RO_of_FCI.Balance_Qty,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name   From dbo.RO_of_FCI Left JOIN tbl_MetaData_STORAGE_COMMODITY  ON RO_of_FCI.Commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join Transport_Order_againstRo on RO_of_FCI.RO_No=Transport_Order_againstRo.RO_No left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=tbl_MetaData_SCHEME.Scheme_id left join dbo.TO_Allot_Lift on RO_of_FCI.RO_No=TO_Allot_Lift.RO_No left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID where RO_of_FCI.RO_No='" + txtrono.Text + "' and RO_of_FCI.Distt_Id='" + distid + "'";
            DataSet ds = obj.selectAny(qryall);
         
                DataRow dr = ds.Tables[0].Rows[0];

                string rdate = dr["RO_Validity"].ToString();
                string rodate = getdate(rdate);
                txtrodate.Text = rodate;
                txtrodate.ReadOnly = true;
                txtrodate.BackColor = System.Drawing.Color.Wheat;
                
                lblcomdty.Text = dr["Expr1"].ToString();
                lblsch.Text = dr["Scheme"].ToString();
                  
                txtcommodity.Text = dr["Commodity_Name"].ToString();
                txtcommodity.ReadOnly = true;
                txtcommodity.BackColor = System.Drawing.Color.Wheat;
                txtscheme.Text = dr["Scheme_Name"].ToString();
                txtscheme.ReadOnly = true;
                txtscheme.BackColor = System.Drawing.Color.Wheat;
                txtroqty.Text = dr["RO_qty"].ToString();
                txtroqty.ReadOnly = true;
                txtroqty.BackColor = System.Drawing.Color.Wheat;
                //ddltransport.SelectedItem.Text = dr["Transporter"].ToString();
                ddltransport.SelectedValue = dr["Transporter_Name"].ToString();
                //ddltransport.Enabled = false;
                ddltransport.BackColor = System.Drawing.Color.Wheat;
                //txtrobalance.Text = dr["Balance_Qty"].ToString();
                GetROBAlance();
       

    }


    //        //txtbalqty.Text = dr["Pending_Qty"].ToString();
    //        string cumqtyqry = "Select Cumulative_Qty ,Pending_Qty from dbo.TO_Allot_Lift where RO_No='" + ddlrono.SelectedItem + "' and Distt_Id='" + distid + "'";
    //        DataSet dscq = obj.selectAny(cumqtyqry);
    //        if (dscq.Tables[0].Rows.Count==0)
    //        {
    //            txtcumlqty.Text = "0";
    //            txtcumlqty.ReadOnly = true;
    //            txtcumlqty.BackColor = System.Drawing.Color.Wheat;
    //        }
    //        else
    //        {

    //            DataRow drcq = dscq.Tables[0].Rows[0];
    //            txtcumlqty.Text = drcq["Cumulative_Qty"].ToString();
    //            txtbalqty.Text = drcq["Pending_Qty"].ToString();
    //            txtcumlqty.ReadOnly = true;
    //            txtcumlqty.BackColor = System.Drawing.Color.Wheat;
    //        }


    //    }
    //    else
    //    {
    //        txtrodate.Text = "";
    //        txtroqty.Text = "";

    //        txtbalqty.Text = "";
    //        txtcumlqty.Text = "";
    //        txtroqty.Text = "";
    //        txtrodate.Text = "";
    //        txtbalqty.Text = "";
    //        txtcommodity.Text = "";
    //        txtscheme.Text = "";
    //        ddlrono.Focus();

    //    }
        
    //}
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
        string mrono = txtrono.Text ;
        string mtono = txttono.Text ;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string mtaname = ddltransport.SelectedValue;
        float miqty = CheckNull(txtsendqty.Text);
        float mcumqty = CheckNull(txtcumlqty.Text);
        float mpqty = CheckNull(txtbalqty.Text);
        string fcidist = ddlfcidist.SelectedValue;
        string fcidepo = ddlfcidepo.SelectedValue;
        float mroqty = CheckNull(txtroqty.Text);
        string mrdate = DateTime.Today.Date.ToString();
        string udate = "";
        string ddate = "";

        string mtodate = "";
        string mtid = "";
        string lift = "N";
        float mliftqty = 0;
        float mpendqty = CheckNull(txtsendqty.Text);
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        try
        {
            con.Open();
            dt = (DataTable)Session["dt"];
            cmd.Connection = con;
            int count = dt.Rows.Count;
            if (count > 0)
            {
                string crdate = DateTime.Today.Date.ToString();
                int i = 0;
                while (i < count)
                {
                    if (i == 0)
                    {
                        mobj = new MoveChallan(ComObj);
                        string qrey = "select max(Trunsuction_Id) as Trunsuction_Id from dbo.Transport_Order_againstRo where Distt_Id='" + distid + "' and Month=" + month + " and Year=" + year;
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
                        }
                    }
                    else
                    {
                        transnum = transnum + 1;
                        transuct = transnum.ToString();


                    }
                    string tid = transuct;

                    disqty = disqty + CheckNull(dt.Rows[i][2].ToString());
                    float balamt = CheckNull(txtbalqty.Text) - CheckNull(disqty.ToString());
                    string qry = "insert into dbo.Transport_Order_againstRo(Distt_Id,RO_No,RO_qty,RO_Validity,TO_Number,TO_Date,Transporter_Name,Commodity_ID,Scheme_ID,FCI_district,FCI_depot,toDistrict,toIssueCenter,Quantity,Cumulative_Qty,Pending_Qty,Month,Year,Trunsuction_Id,IsLifted,Created_date,updated_date,deleted_date,IP_Address)values('" + distid + "','" + mrono + "'," + mroqty + ",'" + mrdate + "','" + mtono + "','" + mtodate + "','" + mtaname + "','" + lblcomdty.Text + "','" + lblsch.Text + "','" + fcidist + "','" + fcidepo + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "'," + dt.Rows[i][2] + "," + mliftqty + "," + dt.Rows[i][2] + "," + month + "," + year + ",'" + tid + "','" + lift + "',getdate(),'" + udate + "','" + ddate + "','" + ip + "')";
                    cmd.CommandText = qry;
                    cmd.ExecuteNonQuery();

                   
                    float ppqty = 0;
                    string qryTO = "Update Transport_Order_againstRo set Quantity=Quantity-" + dt.Rows[i][2] + ",Pending_Qty=Pending_Qty-" + dt.Rows[i][2] + " where Trunsuction_Id='" + stid + "' and Distt_Id='" + distid + "' and toIssueCenter='" +toissue +"'";
                    cmd.CommandText = qryTO;
                    cmd.ExecuteNonQuery();


                   




                    i = i + 1;

                }

                float updateq = CheckNull(lbltoqty.Text);
                string mqryTOqty = "Update dbo.TO_Allot_Lift set Cumulative_Qty=Cumulative_Qty-(" + updateq + "),Pending_Qty=Pending_Qty+(" + updateq + ") where RO_NO='" + mrono + "' and Distt_Id='" + distid + "'";
                cmd.CommandText = mqryTOqty;
                cmd.ExecuteNonQuery();


            }
            else
            {

            }

                                         
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
            btnsave.Enabled = false;

            fillgrid();
        }
        catch (Exception ex)
        {
           
            Label1.Visible = true;
            Label1.Text = ex.Message;

        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();


        }






    }
          

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ro_no = dgridchallan.SelectedRow.Cells[1].Text;
        string to_no = dgridchallan.SelectedRow.Cells[2].Text;
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string qty = dgridchallan.SelectedRow.Cells[4].Text;
        string id = dgridchallan.SelectedRow.Cells[6].Text;
        float mqtys = CheckNull(qty);

        string qrychk = "Select * from dbo.Lift_A_RO where RO_No='" + ro_no + "' and Dist_Id='" + distid + "' and TO_Number='" +to_no  + "' and Month="+ month +" and Year="+year ;
        obj = new LARO(ComObj);
        DataSet dschk = obj.selectAny(qrychk);
        if (dschk == null)
        {
           
        }
        else
        {
            if (dschk.Tables[0].Rows.Count == 0)
            {
                string qrydlt = "delete from dbo.Transport_Order_againstRo where RO_No='" + ro_no + "' and Distt_Id='" + distid + "' and TO_Number='" + to_no + "' and Month=" + month + " and Year=" + year+"and Trunsuction_Id='"+id+"'";
                cmd.CommandText = qrydlt;

                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string qrygetb = "Select Cumulative_Qty,Pending_Qty from dbo.TO_Allot_Lift where RO_No='" + ro_no + "' and Distt_Id='" + distid + "' and Month=" + month + " and Year=" + year;
                    obj = new LARO(ComObj);
                    DataSet dsbal = obj.selectAny(qrygetb);
                    if (dsbal == null)
                    {

                    }
                    else
                    {
                        if (dsbal.Tables[0].Rows.Count == 0)
                        {

                        }
                        else
                        {
                            DataRow drbal = dsbal.Tables[0].Rows[0];
                            string bal = drbal["Cumulative_Qty"].ToString();
                            string pbal = drbal["Pending_Qty"].ToString();
                            float uqty = CheckNull(bal) - mqtys;
                            float upqty = CheckNull(pbal) +mqtys;

                            string updatebal = "Update dbo.TO_Allot_Lift set Cumulative_Qty=" + uqty + ",Pending_Qty="+upqty +" where RO_No='" + ro_no + "' and Distt_Id='" + distid + "'and Month=" + month + " and Year=" + year;
                            cmd.CommandText = updatebal;
                            cmd.ExecuteNonQuery();
                            Label3.Visible = true;
                            Label3.Text = "Record Deleted Successfully........";
                            Label3.ForeColor = System.Drawing.Color.OrangeRed;
                            fillgrid();
                            

                        }
                    }



                }
                catch (Exception ex)
                {
                    Label3.Visible = true;
                    Label3.Text = ex.Message;
                }
                finally
                {
                }




            }
            else
            {
                Label3.Visible = true;
                Label3.Text = "Sorry You Can't Delete This Transport Order, It has been lifted !";
            }

           

        }


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
    void fillgrid()
    {
       
        //mobj = new MoveChallan(ComObj);
        ////string qry = "SELECT tbl_MetaData_DEPOT.DepotName,Transport_Order_againstRo.RO_No,Transport_Order_againstRo.Quantity,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.TO_Date,Transport_Order_againstRo.Transporter_Name,Transporter_Table.Transporter_Name as Tname,Transport_Order_againstRo.Trunsuction_Id FROM dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join dbo.tbl_MetaData_DEPOT  on Transport_Order_againstRo.toIssueCenter= tbl_MetaData_DEPOT.DepotID where Transport_Order_againstRo.Distt_Id='" + distid + "'and Transport_Order_againstRo.RO_No ='" + mrono + "'";
        //DataSet ds = mobj.selectAny(qry);
        //dgridchallan.DataSource = ds.Tables[0];
        //dgridchallan.DataBind();


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
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/TransportOrder_Type.aspx");
    }
    protected void addmore_Click(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedItem.Text == "--Select--" || ddlissue.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District and IssueCenter..'); </script> ");
        }
        else
        {
            //DateTime  mtodate = DaintyDate1.SelectedDate ;
            DateTime  today = DateTime.Today.Date;
            //if (mtodate < today)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry Back Date Entry is not allow here..'); </script> ");
            //}
            //else
            //{
               
                dt = (DataTable)Session["dt"];

                string mrono = txtrono.Text ;
                string mtono = txttono.Text;
                string mtrans = ddltransport.SelectedValue;
                float sqty = CheckNull(txtsendqty.Text);
                string ddist = ddldistrict.SelectedValue;
                string dissue = ddlissue.SelectedValue;
                string distname = ddldistrict.SelectedItem.Text;
                string issuename = ddlissue.SelectedItem.Text;
                float balnce = CheckNull(txtbalqty.Text );
                float send = CheckNull(txtsendqty .Text);
                float topqty = CheckNull(lbltoqty.Text) - sqty;
                lbltoqty.Text = topqty.ToString();

                if (CheckNull(lbltoqty.Text) < 0)
                {
                    Label4.Visible = true;
                    Label4.Text = "Quantity should not be Greater then Pending Qty.";
                    float tolift = CheckNull(lbltoqty.Text) + sqty;
                    lbltoqty.Text = tolift.ToString();
                    txtsendqty.Text = "";
                    txtsendqty.Focus();
                }
                else
                {
                    dt.Rows.Add(mrono, mtono, sqty, distname, issuename, ddist, dissue);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    txtsendqty.Text = "";
                    txtsendqty.Focus();
                    GetDist();

                }

              
               
            //}
        }

        Label3.Visible = false;
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
   
    protected void ddlfcidist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFCIdepot();
       
    }
    protected void ddlfcidepo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void ddd_allot_year_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void txtrono_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        lbldesting.Visible = true;
        lblfromfci.Visible = true;
        lbltoissue.Visible = true;
        
        lblfcireg.Visible = true;
        lbldisdepot.Visible = true;
        lblqty.Visible = true;
        lbldistrict.Visible = true;
        lblissue.Visible = true;
       
        ddlfcidist.Visible = true;
        ddlfcidepo.Visible = true;
        txtsendqty.Visible = true;
        ddldistrict.Visible = true;
        ddlissue.Visible = true;
        addmore.Visible = true;
        GridView1.Visible = true;
        lblupdqty.Visible = false;
        txtuqty.Visible = false;
        btnhide.Visible = true;
        btnEdit.Visible = false ;
        btnupdate.Visible = false;
        btnsave.Visible = true;
    }
    protected void btnhide_Click(object sender, EventArgs e)
    {
        lbldesting.Visible = false;
        lblfromfci.Visible = false;
        lbltoissue.Visible = false;
       
        lblfcireg.Visible = false;
        lbldisdepot.Visible = false;
        lblqty.Visible = false;
        lbldistrict.Visible = false;
        lblissue.Visible = false;
        
        ddlfcidist.Visible = false;
        ddlfcidepo.Visible = false;
        txtsendqty.Visible = false;
        ddldistrict.Visible = false;
        ddlissue.Visible = false;
        addmore.Visible = false;
        GridView1.Visible = true;
        lblupdqty.Visible = true;
        txtuqty.Visible = true;
        btnhide.Visible = false ;
        btnEdit.Visible = true ;
        btnupdate.Visible = true ;
        btnsave.Visible = false ;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string mrono = txtrono.Text;
        string mtono = txttono.Text;
        //float upqty = CheckNull(txtuqty.Text);
        //float toqty = CheckNull(txttoqty.Text);
        //float mpendq = toqty - upqty;
        //float mcumqty = CheckNull(txtcumlqty.Text);
        //if (mcumqty ==0)
        //{

            cmd.Connection = con;
            string qryTOqty = "Update Transport_Order_againstRo set Quantity=" + CheckNull(txttoqty.Text) + ",Pending_Qty=" + CheckNull(txtbalqty.Text) + ",Cumulative_Qty=" + CheckNull(txtcumlqty.Text) + ",IsLifted='" + txtuqty.Text + "' where Trunsuction_Id='" + stid + "' and Distt_id='" + distid + "'and toIssueCenter='" + toissue + "' and RO_No='"+mrono +"' and TO_Number='"+mtono +"'";

            try
            {
                con.Open();
                cmd.CommandText = qryTOqty;
                cmd.ExecuteNonQuery();

                //string mqryTOqty = "Update dbo.TO_Allot_Lift set Cumulative_Qty=Cumulative_Qty-(" + mpendq + "),Pending_Qty=Pending_Qty+(" + mpendq + ") where RO_NO='" + mrono + "' and Distt_Id='" + distid + "'";
                //cmd.CommandText = mqryTOqty;
                //cmd.ExecuteNonQuery();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully...'); </script> ");
                btnupdate.Enabled = false;
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
        //else 
        //{
        //    if (upqty < mcumqty)
        //    {
        //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry You Can't Edit less then lifted qty...'); </script> ");
        //    }
        //    else
        //    {
        //        cmd.Connection = con;
        //        string qryTOqty = "Update Transport_Order_againstRo set Quantity=" + upqty + ",Pending_Qty=Pending_Qty-(" + mpendq + ") where Trunsuction_Id='" + stid + "'";

        //        try
        //        {
        //            con.Open();
        //            cmd.CommandText = qryTOqty;
        //            cmd.ExecuteNonQuery();

        //            string mqryTOqty = "Update dbo.TO_Allot_Lift set Cumulative_Qty=Cumulative_Qty-(" + mpendq + "),Pending_Qty=Pending_Qty+(" + mpendq + ") where RO_NO='" + mrono + "' and Distt_Id='" + distid + "'";
        //            cmd.CommandText = mqryTOqty;
        //            cmd.ExecuteNonQuery();

        //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully...'); </script> ");
        //            btnupdate.Enabled = false;
        //        }
        //        catch (Exception ex)
        //        {
        //            Label1.Visible = true;
        //            Label1.Text = ex.Message;
        //        }
        //        finally
        //        {
        //            con.Close();

        //        }

        //    }
                      

        //}
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        string qryROqty = "Update RO_Of_FCI set Balance_Qty=" + CheckNull(txtrobalance.Text) + " where RO_NO='" + rono + "' and Distt_id='" + distid + "'";
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.CommandText = qryROqty;
            cmd.ExecuteNonQuery();

            //string mqryTOqty = "Update dbo.TO_Allot_Lift set Cumulative_Qty=Cumulative_Qty-(" + mpendq + "),Pending_Qty=Pending_Qty+(" + mpendq + ") where RO_NO='" + mrono + "' and Distt_Id='" + distid + "'";
            //cmd.CommandText = mqryTOqty;
            //cmd.ExecuteNonQuery();

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully...'); </script> ");
            btnupdate.Enabled = false;
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

    void GetROBAlance()
    {
          string ro_no = rono;
          string qrychk = "Select Balance_Qty  from dbo.RO_Of_FCI  where RO_No='" + ro_no + "' and Distt_Id='" + distid + "'";
        obj = new LARO(ComObj);
        DataSet dschk = obj.selectAny(qrychk);
        if (dschk == null)
        {

        }
        else
        {
            if (dschk.Tables[0].Rows.Count == 0)
            {




            }
            else
            {
                DataRow drlr = dschk.Tables[0].Rows[0];

                txtrobalance.Text = drlr["Balance_Qty"].ToString();
            }

        }

    }
    


    protected void Button2_Click(object sender, EventArgs e)
    {
         string ro_no = rono;
         string qrychk = "Select Sum(Qty_Send) as Lifted  from dbo.Lift_A_RO where RO_No='" + ro_no + "' and Dist_Id='" + distid + "'" ;
        obj = new LARO(ComObj);
        DataSet dschk = obj.selectAny(qrychk);
        if (dschk == null)
        {

        }
        else
        {
            if (dschk.Tables[0].Rows.Count == 0)
            {




            }
            else
            {
                DataRow drlr = dschk.Tables[0].Rows[0];

                txtgliftqty.Text = drlr["Lifted"].ToString();
            }

        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string ro_no = rono;
        string qrychk = "Select Lifted_Qty,Cumulative_Qty,Pending_Qty from dbo.TO_Allot_Lift where RO_No='" + ro_no + "' and Distt_Id='" + distid + "'";
        obj = new LARO(ComObj);
        DataSet dschk = obj.selectAny(qrychk);
        if (dschk == null)
        {

        }
        else
        {
            if (dschk.Tables[0].Rows.Count == 0)
            {




            }
            else
            {
                DataRow drlr = dschk.Tables[0].Rows[0];

                txtallotlift.Text = drlr["Lifted_Qty"].ToString();
                TextBox1.Text = drlr["Cumulative_Qty"].ToString();
                TextBox2.Text = drlr["Pending_Qty"].ToString();
            }

        }


    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string qryROqty = "Update TO_Allot_Lift set Lifted_Qty=" + CheckNull(txtallotlift.Text) + ",Cumulative_Qty=" + CheckNull(TextBox1.Text) + ",Pending_Qty=" + CheckNull(TextBox2.Text) + " where RO_NO='" + rono + "' and Distt_id='" + distid + "'";
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.CommandText = qryROqty;
            cmd.ExecuteNonQuery();

            //string mqryTOqty = "Update dbo.TO_Allot_Lift set Cumulative_Qty=Cumulative_Qty-(" + mpendq + "),Pending_Qty=Pending_Qty+(" + mpendq + ") where RO_NO='" + mrono + "' and Distt_Id='" + distid + "'";
            //cmd.CommandText = mqryTOqty;
            //cmd.ExecuteNonQuery();

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully...'); </script> ");
            btnupdate.Enabled = false;
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
}
