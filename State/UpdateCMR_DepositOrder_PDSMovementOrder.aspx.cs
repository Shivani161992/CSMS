using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class State_UpdateCMR_DepositOrder_PDSMovementOrder : System.Web.UI.Page
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


                Session["fdjfhxncdfh"] = null;
                Session["MovmtOrderNo"] = null;
                ViewState["Row"] = "0";
                GetAgreeDist();
                GetCropYear();
                GetMillDist();
                GetReceiveDist();



                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //string fromdate = Request.Form[txtDateofReceipt.UniqueID];
                //txtDateofReceipt.Text = fromdate;
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));

    }
    public void GetAgreeDist()
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
                        ddlAgreeDistrict.DataSource = ds.Tables[0];
                        ddlAgreeDistrict.DataTextField = "district_name";
                        ddlAgreeDistrict.DataValueField = "district_code";
                        ddlAgreeDistrict.DataBind();
                        ddlAgreeDistrict.Items.Insert(0, "--Select--");


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
    protected void ddlAgreeDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_AgreementNumber.ClearSelection();
        if (ddlAgreeDistrict.SelectedIndex > 0)
        {
            if (ddl_MillerName.SelectedIndex > 0 && ddlCropYear.SelectedIndex > 0)
            {
                GetAgreement();
            }
            else 
            {
            
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध का जिला चुने|'); </script> ");
        }
    }


    //protected void ddlAgreeDistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddl_MillerName.ClearSelection();
    //    if (ddlAgreeDistrict.SelectedIndex > 0)
    //    {
    //        GetReceiveDist();
    //        //GetMillerName();
    //    }
    //    else
    //    {
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध का जिला चुने|'); </script> ");
    //    }

    //}

    public void GetMillDist()
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

    protected void ddlReceivingDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReceivingDistrict.SelectedIndex > 0)
        {
            if (rdbMillMO.Checked == true || rdbPDSMO.Checked == true)
            {
                if (rdbPDSMO.Checked == true)
                {
                    GetPDSMovementOrder();

                }
                else
                {
                    GetMillMovementOrder();
                }


            }
            else
            { 
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया जमा करने वाला जिला  चुने|'); </script> ");
            return;
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
                select = "select Mill_Name, Registration_ID  from Miller_Registration_2017 where CropYear='"+ddlCropYear.SelectedValue.ToString()+"' and District_Code='"+ddlMillerDistrict.SelectedValue.ToString()+"' and Status='1' order by Mill_Name";
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

        if (ddl_MillerName.SelectedIndex > 0)
        {
            GetAgreement();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का नाम चुने|'); </script> ");
            return;
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
                select = "  select Agreement_ID  from PaddyMilling_Agreement_2017 where CropYear='"+ddlCropYear.SelectedValue.ToString()+"' and Mill_Name='"+ddl_MillerName.SelectedValue.ToString()+"'and District='"+ddlAgreeDistrict.SelectedValue.ToString()+"' and IsAccepted='Y' order by Agreement_ID";
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

    protected void ddl_AgreementNumber_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlCMRDO.ClearSelection();
        if (ddl_AgreementNumber.SelectedIndex > 0)
        {
            GetCMRDO();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Agreement नंबर चुने'); </script> ");
        }
    }
    public void GetCMRDO()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select CMR_DO from CMR_DepositOrder where Mill_ID='"+ddl_MillerName.SelectedValue.ToString()+"' and Agreement_ID='"+ddl_AgreementNumber.SelectedValue.ToString()+"' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"' and IsAgainst_MovementOrder='N' and IsAgainst_MillMovement_Order='N' order by CMR_DO";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCMRDO.DataSource = ds.Tables[0];
                        ddlCMRDO.DataTextField = "CMR_DO";
                        ddlCMRDO.DataValueField = "CMR_DO";
                        ddlCMRDO.DataBind();
                        ddlCMRDO.Items.Insert(0, "--Select--");


                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR DO Number Not available'); </script> ");
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
    protected void ddlCMRDO_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rdbPDSMO_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlMillerDistrict.SelectedIndex > 0 && ddlReceivingDistrict.SelectedIndex > 0)
        {
            if (rdbPDSMO.Checked == true)
            {
                trPDSMO.Visible = true;
                lblMovementOrder.Text = "PDS Movement Order";
                rdbMillMO.Checked = false;
                ddlSMO.Visible = true;
                ddlMMO.Visible = false;
                GetPDSMovementOrder();

            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला और जमा करने वाला जिला चुने|'); </script> ");
            rdbPDSMO.Checked = false;
            return;
        }
    }

    public void GetPDSMovementOrder()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select MoveOrdernum from StateMovementOrder where FrmDist='" + ddlMillerDistrict.SelectedValue.ToString() + "' and ToDist='" + ddlReceivingDistrict.SelectedValue.ToString() + "' and ModeofDispatch='12' and Commodity='3' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "' order by MoveOrdernum";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSMO.DataSource = ds.Tables[0];
                        ddlSMO.DataTextField = "MoveOrdernum";
                        ddlSMO.DataValueField = "MoveOrdernum";
                        ddlSMO.DataBind();
                        ddlSMO.Items.Insert(0, "--Select--");


                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कोई भी Movement Order Number उपलभ नहीं है'); </script> ");
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
    protected void rdbMillMO_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlMillerDistrict.SelectedIndex > 0 && ddlReceivingDistrict.SelectedIndex > 0)
        {
            if (rdbMillMO.Checked==true)
            {
                trPDSMO.Visible = true;
                lblMovementOrder.Text = "Mill Movement Order";
                rdbPDSMO.Checked = false;
                ddlMMO.Visible = true;
                ddlSMO.Visible = false;
                GetMillMovementOrder();

            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला और जमा करने वाला जिला चुने|'); </script> ");
            rdbMillMO.Checked = false;
            return;
        }
    }

    public void GetMillMovementOrder()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select distinct MovementOrder_ID from CMR_Movement_FromMill where Commodity='3' and ModeOfDispatch='12' and  MillerDist='" + ddlMillerDistrict.SelectedValue.ToString() + "' and ReceivingDist='" + ddlReceivingDistrict.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "' order by MovementOrder_ID";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMMO.DataSource = ds.Tables[0];
                        ddlMMO.DataTextField = "MovementOrder_ID";
                        ddlMMO.DataValueField = "MovementOrder_ID";
                        ddlMMO.DataBind();
                        ddlMMO.Items.Insert(0, "--Select--");


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

   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            //QtyTotal += (double.Parse(e.Row.Cells[7].Text));
            //Session["QtyTotal"] = Convert.ToString(QtyTotal);

            //if (e.Row.Cells[0].Text == "15")
            //{
            //    bttAdd.Enabled = false;
            //    ViewState["Row"] = "15";
            //   // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('You Have Alloted All 15 Dist. & You Can Not Add Another Dist. In Same MO'); </script> ");
            //}

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[0].Text = "Total";
            //e.Row.Cells[1].Text = QtyTotal.ToString();
            //TotaltransCmr = Convert.ToDecimal(QtyTotal.ToString());
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

    protected void bttAdd_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }

      
        else if (ddlAgreeDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Paddy Agreement District'); </script> ");
            return;
        }
        else if (ddlMillerDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller District'); </script> ");
            return;
        }
        else if (ddlReceivingDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Receiving District'); </script> ");
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

        else if (ddlCMRDO.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select CMR DO'); </script> ");
            return;
        }

        else if (rdbPDSMO.Checked == false && rdbMillMO.Checked == false)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select PDS Movement Number or Mill Movement Number'); </script> ");
            return;
        }
        else
        {
            trGrid.Visible = true;
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("AgreeDist");
                dt.Columns.Add("MillerDistrict");
                dt.Columns.Add("ReceivedDistrict");
                dt.Columns.Add("MillerName");
                dt.Columns.Add("Agreement");
                dt.Columns.Add("CMRDO");
                dt.Columns.Add("MovementOrder");
               
               // dt.Columns.Add("TransferQuantity");

                dt.Columns.Add("PaddyDistCode");
                dt.Columns.Add("MillerDistCode");
                dt.Columns.Add("RcvDistCode");
                dt.Columns.Add("MillerNameCode");


            }
            DataRow dr = dt.NewRow();
            dr["AgreeDist"] = ddlAgreeDistrict.SelectedItem.Text;
            dr["MillerDistrict"] = ddlMillerDistrict.SelectedItem.Text;
            dr["ReceivedDistrict"] = ddlReceivingDistrict.SelectedItem.Text;

            dr["MillerName"] = ddl_MillerName.SelectedItem.Text;
            dr["Agreement"] = ddl_AgreementNumber.SelectedItem.ToString();

            dr["CMRDO"] = ddlCMRDO.SelectedItem.ToString();

            if (rdbPDSMO.Checked == true)
            {
                dr["MovementOrder"] = ddlSMO.SelectedItem.ToString();
            }
            else if (rdbMillMO.Checked==true)
            {
                dr["MovementOrder"] = ddlMMO.SelectedItem.ToString();
            }

            dr["PaddyDistCode"] = ddlAgreeDistrict.SelectedValue;
            dr["MillerDistCode"] = ddlMillerDistrict.SelectedValue;
            dr["RcvDistCode"] = ddlReceivingDistrict.SelectedValue;
            dr["MillerNameCode"] = ddl_MillerName.SelectedValue;



            dt.Rows.Add(dr);
            Session["fdjfhxncdfh"] = dt;
            fillgrid();


            ddlCropYear.Enabled = false;
            ddlAgreeDistrict.Enabled = false;
            ddlMillerDistrict.Enabled = false;
            ddlReceivingDistrict.Enabled = false;

            ddl_MillerName.Enabled = false;
            ddl_AgreementNumber.Enabled = false;

            if (rdbPDSMO.Checked == true)
            {
                ddlSMO.Enabled = false;
            }
            else if (rdbMillMO.Checked == true)
            {
                ddlMMO.Enabled = false;
            }

            ddlCMRDO.ClearSelection();
          

            bttUpdate.Enabled = true;

            

        }


    }
  

    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/MovementOrderHome.aspx");
    }

    protected void bttUpdate_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }


        else if (ddlAgreeDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Paddy Agreement District'); </script> ");
            return;
        }
        else if (ddlMillerDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller District'); </script> ");
            return;
        }
        else if (ddlReceivingDistrict.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Receiving District'); </script> ");
            return;
        }
        else if (ddl_MillerName.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
            return;
        }
        else if (ddl_AgreementNumber.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Agreement Name'); </script> ");
            return;
        }

        else if (ddlCMRDO.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select CMR DO'); </script> ");
            return;
        }

        else if (rdbPDSMO.Checked == false && rdbMillMO.Checked == false)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select PDS Movement Number or Mill Movement Number'); </script> ");
            return;
        }
        else
        {

        
        }

    }









   
}