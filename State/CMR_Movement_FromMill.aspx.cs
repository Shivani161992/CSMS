using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class State_CMR_Movement_FromMill : System.Web.UI.Page
{

    SqlConnection con;
    SqlCommand cmd, cmd1, cmd2;
    SqlDataAdapter da;
    DataSet ds;
    decimal TotalCMR;
    decimal TransferQuantity;
    public string gatepass = "";
    string Rates;
    public int getnum;
    decimal TotaltransCmr;

    double QtyTotal = 0, ConvertMtToQtls = 0;
    string smsToDistCode = "", smsFrmDistCode = "", GetDistContactNo = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                GetCropYear();



                GetPaddyDist();
                GetCommodity();
                
                GetReceiveDist();
              
                Session["fdjfhxncdfh"] = null;
                Session["MovmtOrderNo"] = null;
                ViewState["Row"] = "0";

               
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                string fromdate = Request.Form[txtDateofReceipt.UniqueID];
                txtDateofReceipt.Text = fromdate;
            }

           
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    public void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Commodity_Id, Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (3) order by Commodity_Name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    public void GetPaddyDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlPaddyDistrict.DataSource = ds.Tables[0];
                        ddlPaddyDistrict.DataTextField = "district_name";
                        ddlPaddyDistrict.DataValueField = "district_code";
                        ddlPaddyDistrict.DataBind();
                        ddlPaddyDistrict.Items.Insert(0, "--Select--");
                       
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    public void GetMillerDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMillerDistrict.DataSource = ds.Tables[0];
                        ddlMillerDistrict.DataTextField = "district_name";
                        ddlMillerDistrict.DataValueField = "district_code";
                        ddlMillerDistrict.DataBind();
                        ddlMillerDistrict.Items.Insert(0, "--Select--");


                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }


    protected void ddlMillerDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_MillerName.ClearSelection();
        if (ddlMillerDistrict.SelectedIndex > 0)
        {
            GetMillerName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला चुने|'); </script> ");
        }
    }
    public void GetMillerName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select Mill_Name, Registration_ID from Miller_Registration_2017 where Status='1' and District_Code='" + ddlMillerDistrict.SelectedValue.ToString() + "' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"' order by Mill_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_MillerName.DataSource = ds.Tables[0];
                        ddl_MillerName.DataTextField = "Mill_Name";
                        ddl_MillerName.DataValueField = "Registration_ID";
                        ddl_MillerName.DataBind();
                        ddl_MillerName.Items.Insert(0, "--Select--");


                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी मिलर उपलभ नहीं है|'); </script> ");
                    }
                }
               
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    protected void ddl_MillerName_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMillerDistrict.SelectedIndex > 0)
        {
            GetAgreement();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला चुने|'); </script> ");
        }
    }
    public void GetAgreement()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select distinct Agreement_ID from PaddyMilling_Agreement_2017 where Mill_Name='" + ddl_MillerName.SelectedValue.ToString() + "' and District='" + ddlPaddyDistrict.SelectedValue.ToString() + "' and  CropYear='"+ddlCropYear.SelectedValue.ToString()+"' order by Agreement_ID";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_AgreementNumber.DataSource = ds.Tables[0];
                        ddl_AgreementNumber.DataTextField = "Agreement_ID";
                        ddl_AgreementNumber.DataValueField = "Agreement_ID";
                        ddl_AgreementNumber.DataBind();
                        ddl_AgreementNumber.Items.Insert(0, "--Select--");


                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने कोई भी अनुबंध नहीं किया है|'); </script> ");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void ddl_AgreementNumber_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_AgreementNumber.SelectedIndex > 0)
        {
            GetBalCMRQuantity();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अग्रीमेंट नंबर चुने'); </script> ");
        }
    }
    public void GetBalCMRQuantity()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string QuantityIssued;
                decimal DOQuantityIssued;
               
                string ReceivedCMR;
                decimal ReceivedCMRQ;
                string select = "";
                select = "select isnull(sum(isnull((qty_issue/10),0)),0) as qty_issue  from PaddyMilling_IssueAgainst_DO where Partyname='" + ddl_MillerName.SelectedValue.ToString() + "' and Agreement_ID='" + ddl_AgreementNumber.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        QuantityIssued = ds.Tables[0].Rows[0]["qty_issue"].ToString();
                        DOQuantityIssued = Convert.ToDecimal(QuantityIssued);
                        TotalCMR = ((DOQuantityIssued * 67) / 100);

                    }
                }
               //ReceivedCMR
                string qry = "";
                qry = "  select isnull(sum(isnull((Accept_CommonRice/10),0)),0) as Accept_CommonRice from CMR_QualityInspection where Mill_Name='" + ddlMillerDistrict.SelectedValue.ToString() + "' and Agreement_ID='" + ddl_AgreementNumber.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(qry, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ReceivedCMR = ds.Tables[0].Rows[0]["Accept_CommonRice"].ToString();
                        ReceivedCMRQ = Convert.ToDecimal(ReceivedCMR);

                        txt_BalCMR.Text = Convert.ToString(TotalCMR - ReceivedCMRQ);

                    }
                }
               
            }


            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    public void GetReceiveDist()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlReceivingDistrict.DataSource = ds.Tables[0];
                        ddlReceivingDistrict.DataTextField = "district_name";
                        ddlReceivingDistrict.DataValueField = "district_code";
                        ddlReceivingDistrict.DataBind();
                        ddlReceivingDistrict.Items.Insert(0, "--Select--");


                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    protected void bttAdd_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }

        else if (txt_OrderNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Mill Movement Order Number'); </script> ");
            return;
        }

        else if (txtDateofReceipt.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Date'); </script> ");
            return;
        }
        else if (ddlPaddyDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Paddy District'); </script> ");
            return;
        }
        else if (ddlMillerDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller District'); </script> ");
            return;
        }
        else if (ddl_MillerName.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
            return;
        }
        else if (ddl_AgreementNumber.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Agreement Number'); </script> ");
            return;
        }
        else if (ddlReceivingDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Receiving District'); </script> ");
            return;
        }
        else if (txt_TransferQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Transfer Quantity'); </script> ");
            return;
        }
        else
        {
            trGrid.Visible = true;
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("PaddyDistrict");
                dt.Columns.Add("MillerDistrict");
                dt.Columns.Add("MillerName");

                dt.Columns.Add("Agreement");
                // dt.Columns.Add("todistval");
                dt.Columns.Add("BalQty");
                dt.Columns.Add("ReceivingDistrict");
                dt.Columns.Add("TransferQuantity");

                dt.Columns.Add("PaddyDistCode");
                dt.Columns.Add("MillerDistCode");
                dt.Columns.Add("RcvDistCode");
                dt.Columns.Add("MillerNameCode");


            }
            DataRow dr = dt.NewRow();
            dr["PaddyDistrict"] = ddlPaddyDistrict.SelectedItem.Text;
            dr["MillerDistrict"] = ddlMillerDistrict.SelectedItem.Text;
            dr["MillerName"] = ddl_MillerName.SelectedItem.Text;
            dr["Agreement"] = ddl_AgreementNumber.SelectedItem.ToString();
            dr["BalQty"] = txt_BalCMR.Text;
            dr["ReceivingDistrict"] = ddlReceivingDistrict.SelectedItem.Text;
            dr["TransferQuantity"] = txt_TransferQty.Text;

            dr["PaddyDistCode"] = ddlPaddyDistrict.SelectedValue;
            dr["MillerDistCode"] = ddlMillerDistrict.SelectedValue;
            dr["RcvDistCode"] = ddlReceivingDistrict.SelectedValue;
            dr["MillerNameCode"] = ddl_MillerName.SelectedValue;


            
            dt.Rows.Add(dr);
            Session["fdjfhxncdfh"] = dt;
            fillgrid();
            ddlCropYear.Enabled = false;
            txt_OrderNumber.Enabled = false;
            txtDateofReceipt.Enabled = false;
            ddl_AgreementNumber.ClearSelection();
            ddl_MillerName.ClearSelection();
            ddlMillerDistrict.ClearSelection();
            ddlReceivingDistrict.ClearSelection();
            ddlPaddyDistrict.ClearSelection();
            txt_BalCMR.Text = "";
            txt_TransferQty.Text = "";
            bttSubmit.Enabled = true;

            //Ddldist.SelectedIndex = 0;
            //ddlDestination.SelectedIndex = 0;
            ////txtQuantity.Text = "";
            //txtQuantity.Enabled = true;
            //txtQuantity.Text = "";



            ////ddlToDist.SelectedIndex = 0;
            ////txtQty.Text = "";
            ////btnRecptSubmit.Enabled = true;
            //txtIndNumber.Enabled = false;
            //txtIndDate.Enabled = false;
            //ddlMarSeason.Enabled = false;
            //txtColorCode.Enabled = false;
            //txtDelyDate.Enabled = false;
            //ddlCropYear.Enabled = false;
            //txtFundReq.Enabled = true;
            //txtConQty.Enabled = true;

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[7].Text));
            Session["QtyTotal"] = Convert.ToString(QtyTotal);

            //if (e.Row.Cells[0].Text == "15")
            //{
            //    bttAdd.Enabled = false;
            //    ViewState["Row"] = "15";
            //   // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('You Have Alloted All 15 Dist. & You Can Not Add Another Dist. In Same MO'); </script> ");
            //}

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total";
            e.Row.Cells[1].Text = QtyTotal.ToString();
            TotaltransCmr =Convert.ToDecimal(QtyTotal.ToString());
            //All_RR_Wagon_Qty = e.Row.Cells[3].Text;
           // txttotalquantity.Text = e.Row.Cells[4].Text;

        }


    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("PaddyDistrict");
            dt.Columns.Add("MillerDistrict");
            dt.Columns.Add("MillerName");
            dt.Columns.Add("Agreement");
            dt.Columns.Add("BalQty");
            dt.Columns.Add("ReceivingDistrict");
            dt.Columns.Add("TransferQuantity");

            dt.Columns.Add("PaddyDistCode");
            dt.Columns.Add("MillerDistCode");
            dt.Columns.Add("RcvDistCode");
            dt.Columns.Add("MillerNameCode");
        }
        else
        {
            //string DistRowValue = dt.Rows[e.RowIndex]["fromdistval"].ToString();
            //ddlToDist.Items.FindByValue(DistRowValue).Enabled = true;
            dt.Rows.RemoveAt(e.RowIndex);

            if (ViewState["Row"].ToString() == "15")
            {
                bttAdd.Enabled = true;
                ViewState["Row"] = "0";
            }

        }
        Session["fdjfhxncdfh"] = dt;
        fillgrid();
    }
    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["fdjfhxncdfh"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            //GridView1.Columns[3].HeaderText = ddlComdtyMode.SelectedItem.ToString();
            GridView1.DataBind();
        }
    }

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
         using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string qrey = "select max(Mill_MovementOrder) as Mill_MovementOrder from CMR_Movement_FromMill where LEN(Mill_MovementOrder)<15 ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    //mobj1 = new MoveChallan(ComObj);
                    //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                    //DataSet ds = new DataSet();
                    // dmax.Fill(ds);
                    // DataTable dt = ds.Tables[""];
                    DataRow dr = ds.Tables[0].Rows[0];
                    //gatepass = dr["Inspector_ID"].ToString();
                    gatepass = ds.Tables[0].Rows[0]["Mill_MovementOrder"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "171701800";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }


                    string strinsert = "";
                    DataTable dt = adddetails();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            string GBID_Multiple = gatepass + (i + 1);
                            ConvertServerDate ServerDeliveryDate = new ConvertServerDate();
                            string IssuedDeliveryDate = ServerDeliveryDate.getDate_MDY(txtDateofReceipt.Text);

                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            strinsert = "insert into CMR_Movement_FromMill( Mill_MovementOrder, CropYear, Commodity, ModeOfDispatch, Issueddate, PaddyDist, MillerDist, ReceivingDist, Mill_Name, Agreement_ID, BalCMR, TransferCMR, IP, Created_Date, SMMO, TotalTransfer_Qty, IsCancelled, MovementOrder_ID) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','03','12','" + IssuedDeliveryDate + "','" + dt.Rows[i]["PaddyDistCode"] + "','" + dt.Rows[i]["MillerDistCode"] + "','" + dt.Rows[i]["RcvDistCode"] + "','" + dt.Rows[i]["MillerNameCode"] + "','" + dt.Rows[i]["Agreement"] + "','" + dt.Rows[i]["BalQty"] + "','" + dt.Rows[i]["TransferQuantity"] + "','" + ip + "',getdate(),'" + GBID_Multiple + "','" + Session["QtyTotal"].ToString() + "','N','"+txt_OrderNumber.Text+"')";
                            cmd = new SqlCommand(strinsert, con);
                            string check = (string)cmd.ExecuteScalar();
                        }
                    }

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    bttprint.Enabled = true;
                    bttSubmit.Enabled = false;
                    bttSubmit.Visible = false;
                    bttprint.Visible = true;
                   
                    // Fillgrid();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                    //ddl_Depart.Enabled = false;


                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
        
    }
    protected void bttprint_Click(object sender, EventArgs e)
    {

    }
    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/MovementOrderHome.aspx");
    }
    protected void txt_TransferQty_TextChanged(object sender, EventArgs e)
    {
        if (txt_TransferQty.Text != "")
        {

            TransferQuantity = Convert.ToDecimal(txt_TransferQty.Text);
            decimal BalanceQty = Convert.ToDecimal(txt_BalCMR.Text);
            if (TransferQuantity > BalanceQty)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ट्रान्सफर मात्रा, बची हुई जमा करने वाली CMR मात्रा से कम भरें|'); </script> ");
                txt_TransferQty.Text = "";
                return;
            }
        }
        else 
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ट्रान्सफर मात्रा भरें|'); </script> ");
            return;
        }
    }
    protected void ddlPaddyDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlPaddyDistrict.SelectedIndex > 0)
        {
            GetMillerDist();
            ddl_MillerName.ClearSelection();
            ddl_AgreementNumber.ClearSelection();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध जिला चुने '); </script> ");
        }
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPaddyDistrict.ClearSelection();
        ddlMillerDistrict.ClearSelection();
        ddlReceivingDistrict.ClearSelection();
        ddl_AgreementNumber.ClearSelection();
        ddl_MillerName.ClearSelection();
    }
}



