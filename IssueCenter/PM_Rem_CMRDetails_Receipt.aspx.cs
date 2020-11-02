using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DataAccess;
using Data;


public partial class IssueCenter_PM_Rem_CMRDetails_Receipt : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd, cmd1, cmd2, cmd3;
    SqlDataAdapter da, da_MPStorage, da1;
    DataSet ds, ds_MPStorage, ds1;
    decimal QtyTotal = 0;
    public string gatepass = "", gatepass1 = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;
    string BookNumber;
    double BagsNumbers;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()); //For IssueCentre

    public string ICID, DistId;
    public int BookOnlyNumber, Rejected_NumberOf_Times;
    public string strProximate_CommonRice, strProximate_GradeARice, strReturn_CommonRice, strReturn_GradeARice, strPartyname, strAgreement_ID, Daane;
    public string strRProximate_CommonRice, strRProximate_GradeARice, strRReturn_CommonRice, strRReturn_GradeARice;
    public string strDOReturn_CommonRice, strDOReturn_GradeARice, strDOReturn_TotalRice;
    public string strAgrmtReturn_CommonRice, strAgrmtReturn_GradeARice, stragrmtReturn_TotalRice;

    MoveChallan mobj = null;
    protected Common ComObj = null;
    DistributionCenters distobj = null;

    public string districtid = "", IC_Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            distobj = new DistributionCenters(ComObj);

            if (!IsPostBack)
            {
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();

                Session["fdjfhxncdfh"] = null;
                ViewState["Row"] = "0";
                rdbNewJute.Checked = rdbTagYes.Checked = true;

                txtTruckNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTruckNoOne.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNoOne.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNoOne.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkTota.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkTota.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkTota.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkCTote.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkCTote.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkCTote.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkVijatiye.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkVijatiye.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkVijatiye.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkDaane.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkDaane.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkDaane.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkBadrang.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkBadrang.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkBadrang.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkChaki.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkChaki.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkChaki.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkLaal.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkLaal.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkLaal.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkShreni.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkShreni.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkShreni.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkChokar.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkChokar.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkChokar.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkNami.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkNami.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkNami.Attributes.Add("onchange", "return chksqltxt(this)");

                txtToulReceiptNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtToulReceiptNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtToulReceiptNo.Attributes.Add("onchange", "return chksqltxt(this)");

                GetCropYearValues();
                //GetBranch();

                //GetBookNumber();
                rbCDaane.Checked = true;
               // GetInspector();
                GetDist();
                GetInspector();
                GetReceivingDist();
                GetBranch();
                string toDate = Request.Form[txtDate.UniqueID];
                txtDate.Text = toDate;
                //txtRecDist.Text=Session["dist_name"].ToString();
                txtIssueCenter.Text = Session["issue_name"].ToString();
                ddlCMRDO.Enabled = true;
                ddlBranch.Enabled = true;
                ddlGodam.Enabled = true;
                ddl_IC.Enabled = true;


                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        string fromdate = Request.Form[txtDate.UniqueID];
        txtDate.Text = fromdate;
    }

    public void GetReceivingDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();


                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + Session["dist_id"].ToString() + "'  Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtRecDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        //ddlAgreeDist.DataSource = ds.Tables[0];
                        //ddlAgreeDist.DataTextField = "district_name";
                        //ddlAgreeDist.DataValueField = "district_code";
                        //ddlAgreeDist.DataBind();
                        //ddlAgreeDist.Items.Insert(0, "--Select--");
                        ////Ddldist.SelectedValue = Session["dist_id"].ToString();
                        //// GetMPIssueCentre();
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
    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT * FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    LblTotaGA.Text = ds.Tables[0].Rows[0]["TotaGA"].ToString();
                    LblTotaS.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    LblChoteToteGA.Text = ds.Tables[0].Rows[0]["ChoteToteGA"].ToString();
                    LblChoteToteS.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    LblVijatiyeGA.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    LblVijatiyeS.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    LblDamageDaaneGA.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    LblDamageDaaneS.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    LblBadrangDaaneGA.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    LblBadrangDaaneS.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    LblChaakiDaaneGA.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    LblChaakiDaaneS.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    LblLaalDaaneGA.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    LblLaalDaaneS.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    LblOtherGA.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    LblOtherS.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    LblChokarDaaneGA.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    LblChokarDaaneS.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    LblNamiGA.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
                    LblNamiS.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
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

    public void GetDist()
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
                        ddlAgreeDist.DataSource = ds.Tables[0];
                        ddlAgreeDist.DataTextField = "district_name";
                        ddlAgreeDist.DataValueField = "district_code";
                        ddlAgreeDist.DataBind();
                        ddlAgreeDist.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
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
    protected void ddlAgreeDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAgreeDist.SelectedIndex > 0)
        {
            GetMillerName();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया जिला चुनें |'); </script> ");
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
                select = "select distinct MR.Mill_Name, MR.Registration_ID from PaddyMilling_Agreement_2017 as PA inner join Miller_Registration_2017 as MR on MR.Registration_ID=PA.Mill_Name and MR.District_Code=PA.Mill_Addr_District where PA.District='" + ddlAgreeDist.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMillName.DataSource = ds.Tables[0];
                        ddlMillName.DataTextField = "Mill_Name";
                        ddlMillName.DataValueField = "Registration_ID";
                        ddlMillName.DataBind();
                        ddlMillName.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
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
    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMillName.SelectedIndex > 0)
        {
            GetAgreement();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का नाम चुनें |'); </script> ");
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
                select = "select distinct Agreement_ID, Milling_Type from PaddyMilling_Agreement_2017 as PA WHERE Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and District='" + ddlAgreeDist.SelectedValue.ToString() + "' ORDER BY  Agreement_ID";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMillingType.Visible = true;
                        lblMillingType.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                        ddlAgreeNo.DataSource = ds.Tables[0];
                        ddlAgreeNo.DataTextField = "Agreement_ID";
                        ddlAgreeNo.DataValueField = "Agreement_ID";
                        ddlAgreeNo.DataBind();
                        ddlAgreeNo.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
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
    protected void ddlAgreeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAgreeDist.SelectedIndex > 0)
        {
            GetCMRDONumber();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध क्रमांक चुने|'); </script> ");
        }
    }

    public void GetCMRDONumber()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();


                string select = "";
                select = "select distinct DO_Number, CropYear from CMR_QualityInspection as QI WHERE Agreement_ID='" + ddlAgreeNo.SelectedValue.ToString() + "' and Accept_CommonRice>268 and Accept_CommonRice<270 and IsRemainingRecvd is null ORDER BY  DO_Number";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       // txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                        ddlCMRDO.DataSource = ds.Tables[0];
                        ddlCMRDO.DataTextField = "DO_Number";
                        ddlCMRDO.DataValueField = "DO_Number";
                        ddlCMRDO.DataBind();
                        ddlCMRDO.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
                        
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


    protected void ddlCMRDO_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtTotaS.Text = TxtChoteToteS.Text = txtVijatiyeS.Text = txtDamageDaaneS.Text = txtBadrangDaaneS.Text = txtChaakiDaaneS.Text = txtLaalDaaneS.Text = txtOtherS.Text = txtChokarDaaneS.Text = txtNamiS.Text = txtToulReceiptNo.Text = "";

        if (ddlAgreeDist.SelectedIndex > 0)
        {
            GetRemainingQty();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया DO क्रमांक चुने|'); </script> ");
        }
    }

    public void GetRemainingQty()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();


                string select = "";
                select = "select Accept_CommonRice, LotNumber from CMR_QualityInspection as QI WHERE DO_Number='" + ddlCMRDO.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string AcceptCommonRice;
                        decimal CMRReceived;

                        txtLotNumber.Text = ds.Tables[0].Rows[0]["LotNumber"].ToString();
                        AcceptCommonRice = ds.Tables[0].Rows[0]["Accept_CommonRice"].ToString();
                        CMRReceived = Convert.ToDecimal(AcceptCommonRice);
                        decimal RemainCMR = 270 - CMRReceived;
                        decimal d = Math.Round(RemainCMR, 2);
                        txtRemainingCMR.Text = Convert.ToString(d);


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

        if (ddlAgreeDist.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Agreement District Name'); </script> ");
            return;
        }
        else if (ddlMillName.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
            return;
        }
        else if (ddlAgreeNo.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Agreement Number'); </script> ");
            return;
        }

        else if (ddlCMRDO.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select DO Number'); </script> ");
            return;
        }

        else
        {
            trGrid.Visible = true;
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("fromdisttext");
                dt.Columns.Add("fromdistval");
                dt.Columns.Add("todistval");

                dt.Columns.Add("todisttext");
                // dt.Columns.Add("todistval");
                dt.Columns.Add("quantity");
                dt.Columns.Add("LotNumber");
            }
            DataRow dr = dt.NewRow();
            dr["fromdisttext"] = ddlMillName.SelectedItem.Text;
            dr["todisttext"] = ddlAgreeNo.SelectedValue.ToString();
            dr["fromdistval"] = ddlCMRDO.SelectedItem.Text;
            dr["LotNumber"] = txtLotNumber.Text;
            // dr["quantity"] = ddlDestination.SelectedValue.ToString();

            //dr["todisttext"] = txtNoOfBundle.Text;
            dr["todistval"] = txtRemainingCMR.Text;
            //ddlToDist.Items.FindByValue(ddlToDist.SelectedValue.ToString()).Enabled = ddlToDist.Items.FindByValue(ddlFrmDist.SelectedValue.ToString()).Enabled = false;

            //dr["quantity"] = (float.Parse(txtQuantity.Text)).ToString("0.00");
            dt.Rows.Add(dr);
            Session["fdjfhxncdfh"] = dt;
            fillgrid();

            ddlAgreeDist.Enabled = false;
            ddlMillName.Enabled = false;
            ddlAgreeNo.Enabled = false;
            ddlCMRDO.ClearSelection();
            txtRemainingCMR.Text = "";



        }


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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (decimal.Parse(e.Row.Cells[5].Text));
            Session["QtyTotal"] = Convert.ToString(QtyTotal);

            //if (e.Row.Cells[0].Text == "15")
            //{
            //    bttAdd.Enabled = false;
            //    ViewState["Row"] = "15";
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('You Have Alloted All 15 Dist. & You Can Not Add Another Dist. In Same MO'); </script> ");
            //}

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total";
            e.Row.Cells[3].Text = QtyTotal.ToString();
            //txtqty.Text = e.Row.Cells[3].Text;

        }


    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("fromdisttext");
            dt.Columns.Add("fromdistval");
            dt.Columns.Add("todisttext");
            dt.Columns.Add("todistval");
            dt.Columns.Add("LotNumber");
            // dt.Columns.Add("quantity");
        }
        else
        {
            string DistRowValue = dt.Rows[e.RowIndex]["fromdistval"].ToString();
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

    protected void btnQuilityTested_Click(object sender, EventArgs e)
    {
        if (txtCmrQuanitity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Received CMR Quantity'); </script> ");
            return;
        }
        else if (txtNoOfBags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Number of Bags'); </script> ");
            return;
        }
        else if (ddl_IC.SelectedIndex <=0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Inspector Name'); </script> ");
            return;
        }
        //else if (Convert.ToInt16(txtNoOfBags.Text) > 1 && Convert.ToInt16(txtNoOfBags.Text) <2)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Number of Bags'); </script> ");
        //    return;
        //}
        else if (txtTruckNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Truck Number'); </script> ");
            return;
        }
        else if (txtToulReceiptNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Toul Receipt Number'); </script> ");
            return;
        }

        else if (ddl_IC.SelectedIndex <0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select inspector name'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter CMR receiving date'); </script> ");
            return;
        }
        else if (ddlBranch.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch '); </script> ");
            return;
        }
        else if (ddlGodam.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select godown '); </script> ");
            return;
        }
        else
        {
            chkCommon.Visible = false;
            //ddlBranch.Enabled = ddlGodown.Enabled = ddlCMRDONo.Enabled = false;
            txtCmrQuanitity.Enabled = txtNoOfBags.Enabled = txtTagNo.Enabled = txtTruckNo.Enabled = txtTruckNoOne.Enabled = TxtTotaS.Enabled = TxtChoteToteS.Enabled = txtVijatiyeS.Enabled = txtDamageDaaneS.Enabled = txtBadrangDaaneS.Enabled = txtChaakiDaaneS.Enabled = txtLaalDaaneS.Enabled = txtOtherS.Enabled = txtChokarDaaneS.Enabled = txtNamiS.Enabled = txtToulReceiptNo.Enabled = false;
            if (float.Parse(LblTotaS.Text) >= float.Parse(TxtTotaS.Text) && float.Parse(LblChoteToteS.Text) >= float.Parse(TxtChoteToteS.Text) && float.Parse(LblVijatiyeS.Text) >= float.Parse(txtVijatiyeS.Text) && float.Parse(LblDamageDaaneS.Text) >= float.Parse(txtDamageDaaneS.Text) && float.Parse(LblBadrangDaaneS.Text) >= float.Parse(txtBadrangDaaneS.Text) && float.Parse(LblChaakiDaaneS.Text) >= float.Parse(txtChaakiDaaneS.Text) && float.Parse(LblLaalDaaneS.Text) >= float.Parse(txtLaalDaaneS.Text) && float.Parse(LblOtherS.Text) >= float.Parse(txtOtherS.Text) && float.Parse(LblChokarDaaneS.Text) >= float.Parse(txtChokarDaaneS.Text) && float.Parse(LblNamiS.Text) >= float.Parse(txtNamiS.Text) && rdbTagYes.Checked)
            {
                btnAccept.Enabled = true;
                btnReject.Enabled = true;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";
                ddlCMRDO.Enabled = false;
                ddlBranch.Enabled = false;
                ddlGodam.Enabled = false;
                ddl_IC.Enabled = false;
            }
            else
            {
                btnReject.Enabled = true;
                btnAccept.Enabled = false;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";
                ddlCMRDO.Enabled = false;
                ddlBranch.Enabled = false;
                ddlGodam.Enabled = false;
                ddl_IC.Enabled = false;
            }
        }
    }
    protected void rdbTagYes_CheckedChanged(object sender, EventArgs e)
    {
        txtTagNo.Text = "";
        txtTagNo.Enabled = true;
        btnAccept.Enabled = false;
    }
    protected void rdbTagNo_CheckedChanged(object sender, EventArgs e)
    {
        txtTagNo.Text = "";
        txtTagNo.Enabled = false;
        btnAccept.Enabled = false;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/PaddyMillingHome.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        if (TxtTotaS.Text == "" && TxtChoteToteS.Text == "" && txtVijatiyeS.Text == "" && txtDamageDaaneS.Text == "" && txtBadrangDaaneS.Text == "" && txtChaakiDaaneS.Text == "" && txtLaalDaaneS.Text == "" && txtOtherS.Text == "" && txtChokarDaaneS.Text == "" && txtNamiS.Text == "")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter all CMR quality parameters'); </script> ");
            return;
        }
       

        else
        {
            ICID = Session["issue_id"].ToString();
            DistId = Session["dist_id"].ToString();

            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string qrey = "select max(CMR_RemainingID) as CMR_RemainingID from CMR_QualityInspection_RemainingCMR where Rcvd_District='" + DistId + "' and LEN(CMR_RemainingID)<15 ";
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
                    gatepass = ds.Tables[0].Rows[0]["CMR_RemainingID"].ToString();

                    if (gatepass == "")
                    {
                        int month = int.Parse(DateTime.Today.Date.Month.ToString());
                        gatepass = "17" + DistId + month.ToString() + "00";
                    }
                    else
                    {
                        long getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                    string tags = "", BagType = "";

                    if (rbCDaane.Checked)
                    {
                        Daane = "Damage";
                    }
                    else
                    {
                        Daane = "MDamage";
                    }

                    if (rdbTagYes.Checked)
                    {
                        tags = "Y";
                    }
                    else
                    {
                        tags = "N";
                    }

                    if (rdbNewJute.Checked)
                    {
                        BagType = "9";
                    }
                    else if (rdbOldJute.Checked)
                    {
                        BagType = "10";
                    }
                    else if (rdbOnceJute.Checked)
                    {
                        BagType = "11";
                    }
                    else if (rdbNewPP.Checked)
                    {
                        BagType = "4";
                    }
                    else if (rdbOncePP.Checked)
                    {
                        BagType = "2";
                    }
                    else
                    {
                        BagType = "12";
                    }

                    ConvertServerDate ServerDate = new ConvertServerDate();
                    string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

                    string opid = Session["OperatorId"].ToString();


                    //int IsAvailable = 0;
                    //String CheckData = "";
                    //CheckData = "Select * From CMR_QualityInspection where  CropYear='" + txtCropYear.Text + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgreeNo.SelectedValue.ToString() + "' and LotNumber='" + txtLotNumber.Text + "' and Submited='Y' ";
                    //da = new SqlDataAdapter(CheckData, con);
                    //ds = new DataSet();
                    //da.Fill(ds);
                    //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    IsAvailable = 1;
                    //}

                    //if (IsAvailable == 0)
                    //{
                        int month1 = int.Parse(DateTime.Today.Date.Month.ToString());
                        int year = int.Parse(DateTime.Today.Year.ToString());
                        long getnum1 = 0;

                        string qrey1 = "";
                        qrey1 = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + ICID + "' and Dist_Id='" + DistId + "'";
                        da = new SqlDataAdapter(qrey1, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            gatepass1 = ds.Tables[0].Rows[0]["Receipt_id"].ToString();

                            if (gatepass1 == "")
                            {
                                string issue = IC_Id.Substring(2, 5);
                                gatepass1 = issue + month1.ToString() + "001";

                            }
                            else
                            {
                                getnum1 = Convert.ToInt64(gatepass1);
                                getnum1 = getnum1 + 1;
                                gatepass1 = getnum1.ToString();
                            }
                        }


                        DataTable dt = adddetails();
                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                string GBID_Multiple = gatepass + (i + 1);
                                BookNumber = "AREMCMR" + gatepass;
                                string instr = "";
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                "Update CMR_QualityInspection set IsRemainingRecvd='Y', RemainingCMR_Recvd='" + dt.Rows[i]["todistval"] + "' where DO_Number='" + dt.Rows[i]["fromdistval"] + "';";

                                instr += "Insert Into CMR_QualityInspection_RemainingCMR(CMR_RemainingID, Agreement_District, Mill_ID, Agreement_ID, CropYear, CMR_DO, CMR_DO_Remaing_Qty, CMR_DO_Recvd_Qty, Total_Rcvd_CMRQty, Rcvd_NoOf_Bags, BagType, Tags, TagNo, TruckNo1, ToulReceiptNo, Inspector_ID, Book_Number, Date, Milling_Type, Acceptance_No, Truck_No, LD_No, TotaGA, TotaS, TotaRemark, ChoteToteGA, ChoteToteS, ChoteToteRemark, VijatiyeGA, VijatiyeS, VijatiyeRemark, DamageDaaneGA, DamageDaaneS, DamageDaaneRemark, BadrangDaaneGA, BadrangDaaneS, BadrangDaaneRemark, ChaakiDaaneGA, ChaakiDaaneS, ChaakiDaaneRemark, LaalDaaneGA, LaalDaaneS, LaalDaaneRemark, OtherGA, OtherS, OtherRemark, ChokarDaaneGA, ChokarDaaneS, ChokarDaaneRemark, NamiGA, NamiS, NamiRemark, IP, Current_DateTime, CMR_RemainingID_MultipleID, Rcvd_District, Rcvd_IssueCenter, Rcvd_Branch, Rcvd_Godown, Daane, LotNumber, Rejection_No, Total_Rejected_Qty, CMR_DO_RejQty, Accepted, Rejected) values('" + gatepass + "','" + ddlAgreeDist.SelectedValue.ToString() + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgreeNo.SelectedValue.ToString() + "','" + txtCropYear.Text + "','" + dt.Rows[i]["fromdistval"] + "','" + dt.Rows[i]["todistval"] + "','" + dt.Rows[i]["todistval"] + "','" + Session["QtyTotal"].ToString() + "','" + txtNoOfBags.Text + "','" + BagType + "','" + tags + "','" + txtTagNo.Text + "','" + txtTruckNoOne.Text + "','" + txtToulReceiptNo.Text + "','" + ddl_IC.SelectedValue.ToString() + "','" + BookNumber + "','" + IssuedDate + "',N'" + lblMillingType.Text + "','" + BookNumber + "','" + txtTruckNo.Text + "', '0','0','" + TxtTotaS.Text + "','" + txtRmkTota.Text + "','0','" + TxtChoteToteS.Text + "','" + txtRmkCTote.Text + "','0','" + txtVijatiyeS.Text + "','" + txtRmkVijatiye.Text + "','0','" + txtDamageDaaneS.Text + "','" + txtRmkDaane.Text + "','0','" + txtBadrangDaaneS.Text + "','" + txtRmkBadrang.Text + "','0','" + txtChaakiDaaneS.Text + "','" + txtRmkChaki.Text + "','0','" + txtLaalDaaneS.Text + "','" + txtRmkLaal.Text + "','0','" + txtOtherS.Text + "','" + txtRmkShreni.Text + "','0','" + txtChokarDaaneS.Text + "','" + txtRmkChokar.Text + "','0','" + txtNamiS.Text + "','" + txtRmkNami.Text + "','" + ip + "',GETDATE(),'" + GBID_Multiple + "','" + DistId + "','" + ICID + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodam.SelectedValue.ToString() + "','" + Daane + "','" + dt.Rows[i]["LotNumber"] + "','0','0','0','Y','0'); ";

                                instr += "insert into dbo.tbl_Receipt_Details(State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch) values('23','" + DistId + "','" + IC_Id + "','" + gatepass1 + "','05','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgreeDist.SelectedValue.ToString() + "','','','','" + IssuedDate + "','" + IssuedDate + "','" + BookNumber + "','" + IssuedDate + "','270','3','0','" + txtCropYear.Text + "','1','','" + txtTruckNo.Text + "','','" + BagType + "'," + txtNoOfBags.Text + "," + dt.Rows[i]["todistval"] + "," + txtNoOfBags.Text + ",'0','','0'," + month1 + "," + year + ",'N','" + ip + "',getdate(),'','N','" + ddlGodam.SelectedValue.ToString() + "','" + opid + "','N','','" + ddlBranch.SelectedValue.ToString() + "') ; ";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                                cmd = new SqlCommand(instr, con);
                                string check = (string)cmd.ExecuteScalar();
                            }
                        }

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                        trAftSub.Visible = true;
                        Label2.Visible = true;
                        Label2.Text = "Your CMR Acceptance Number Is : " + BookNumber;
                        btnAccept.Enabled = false;
                        btnReject.Enabled = false;
                        btnPrint.Enabled = true;



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
    public void GetInspector()
    {

        IC_Id = Session["issue_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Inspector_ID,Inspector_Name from Inspector_Master_02017 where  IssueCenter_code='" + IC_Id + "' order by Inspector_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_IC.DataSource = ds.Tables[0];
                        ddl_IC.DataTextField = "Inspector_Name";
                        ddl_IC.DataValueField = "Inspector_ID";
                        ddl_IC.DataBind();
                        ddl_IC.Items.Insert(0, "--Select--");
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
    protected void ddl_IC_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtCmrQuanitity_TextChanged(object sender, EventArgs e)
    {
        if (txtCmrQuanitity.Text != Session["QtyTotal"].ToString())
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter correct Received  quantity'); </script> ");
            txtCmrQuanitity.Text = "";
            return;
        }
    }
    public void GetBranch()
    {
        string districtid = Session["dist_id"].ToString();
        ICID = Session["issue_id"].ToString();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'",  Session["issue_id"].ToString());
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                   
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
       if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें|'); </script> ");
        } 
    }

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodam.DataSource = ds.Tables[0];
                        ddlGodam.DataTextField = "Godown_Name";
                        ddlGodam.DataValueField = "Godown_ID";
                        ddlGodam.DataBind();
                        ddlGodam.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void ddlGodam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodam.SelectedIndex > 0)
        {
            btnQuilityTested.Enabled = true;
        }
    }
    protected void txtNoOfBags_TextChanged(object sender, EventArgs e)
    {
        decimal ReceivingQnty = Convert.ToDecimal(txtCmrQuanitity.Text);
        int x = (int)Math.Ceiling(ReceivingQnty);
        BagsNumbers = x * 2;
        if (Convert.ToInt16(txtNoOfBags.Text) <= BagsNumbers && Convert.ToInt16(txtNoOfBags.Text) > 0)
        {
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Bags quantity can be between 1 and " + BagsNumbers + "'); </script> ");
            txtNoOfBags.Text="";
            return;
        }

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (TxtTotaS.Text == "" && TxtChoteToteS.Text == "" && txtVijatiyeS.Text == "" && txtDamageDaaneS.Text == "" && txtBadrangDaaneS.Text == "" && txtChaakiDaaneS.Text == "" && txtLaalDaaneS.Text == "" && txtOtherS.Text == "" && txtChokarDaaneS.Text == "" && txtNamiS.Text == "")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter all CMR quality parameters'); </script> ");
            return;
        }


        else
        {
            ICID = Session["issue_id"].ToString();
            DistId = Session["dist_id"].ToString();
            string opid = Session["OperatorId"].ToString();

            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string qrey = "select max(CMR_RemainingID) as CMR_RemainingID from CMR_QualityInspection_RemainingCMR where Rcvd_District='" + DistId + "' and LEN(CMR_RemainingID)<15 ";
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
                    gatepass = ds.Tables[0].Rows[0]["CMR_RemainingID"].ToString();

                    if (gatepass == "")
                    {
                        int month1 = int.Parse(DateTime.Today.Date.Month.ToString());
                        gatepass = "17" + DistId + month1.ToString() + "00";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                    string tags = "", BagType = "";

                    if (rbCDaane.Checked)
                    {
                        Daane = "Damage";
                    }
                    else
                    {
                        Daane = "MDamage";
                    }

                    if (rdbTagYes.Checked)
                    {
                        tags = "Y";
                    }
                    else
                    {
                        tags = "N";
                    }

                    if (rdbNewJute.Checked)
                    {
                        BagType = "9";
                    }
                    else if (rdbOldJute.Checked)
                    {
                        BagType = "10";
                    }
                    else if (rdbOnceJute.Checked)
                    {
                        BagType = "11";
                    }
                    else if (rdbNewPP.Checked)
                    {
                        BagType = "4";
                    }
                    else if (rdbOncePP.Checked)
                    {
                        BagType = "2";
                    }
                    else
                    {
                        BagType = "12";
                    }

                    ConvertServerDate ServerDate = new ConvertServerDate();
                    string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

                    //int IsAvailable = 0;
                    //    String CheckData = "";
                    //    CheckData = "Select * From CMR_QualityInspection where District='" + DistId + "' and CropYear='" + txtCropYear.Text+ "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgreeNo.SelectedValue.ToString() + "' and LotNumber='" + txtLotNumber.Text + "' and Submited='Y' ";
                    //    da = new SqlDataAdapter(CheckData, con);
                    //    ds = new DataSet();
                    //    da.Fill(ds);
                    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        IsAvailable = 1;
                    //    }

                    //    if (IsAvailable == 0)
                    //    {
                    //        int month1 = int.Parse(DateTime.Today.Date.Month.ToString());
                    //        int year = int.Parse(DateTime.Today.Year.ToString());
                    //        long getnum = 0;

                    //        string qrey1 = "";
                    //        qrey1 = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + IC_Id + "' and Dist_Id='" + DistId + "'";
                    //        da = new SqlDataAdapter(qrey1, con);
                    //        ds = new DataSet();
                    //        da.Fill(ds);

                    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //        {
                    //            gatepass1 = ds.Tables[0].Rows[0]["Receipt_id"].ToString();

                    //            if (gatepass1 == "")
                    //            {
                    //                string issue = IC_Id.Substring(2, 5);
                    //                gatepass1 = issue + month1.ToString() + "001";

                    //            }
                    //            else
                    //            {
                    //                getnum = Convert.ToInt64(gatepass1);
                    //                getnum = getnum + 1;
                    //                gatepass1 = getnum.ToString();
                    //            }
                    //        }


                    DataTable dt = adddetails();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            string GBID_Multiple = gatepass + (i + 1);
                            BookNumber = "RREMCMR" + gatepass;
                            string instr = "";
                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                            "Update CMR_QualityInspection set IsRemainingRecvd='N',IsRemainingRejected='Y', RemainingCMR_Recvd='0', RemainingRejected_QTY='" + dt.Rows[i]["todistval"] + "' where DO_Number='" + dt.Rows[i]["fromdistval"] + "';";

                            instr += "Insert Into CMR_QualityInspection_RemainingCMR(CMR_RemainingID, Agreement_District, Mill_ID, Agreement_ID, CropYear, CMR_DO, CMR_DO_Remaing_Qty, CMR_DO_Recvd_Qty, Total_Rcvd_CMRQty, Rcvd_NoOf_Bags, BagType, Tags, TagNo, TruckNo1, ToulReceiptNo, Inspector_ID, Book_Number, Date, Milling_Type, Acceptance_No, Truck_No, LD_No, TotaGA, TotaS, TotaRemark, ChoteToteGA, ChoteToteS, ChoteToteRemark, VijatiyeGA, VijatiyeS, VijatiyeRemark, DamageDaaneGA, DamageDaaneS, DamageDaaneRemark, BadrangDaaneGA, BadrangDaaneS, BadrangDaaneRemark, ChaakiDaaneGA, ChaakiDaaneS, ChaakiDaaneRemark, LaalDaaneGA, LaalDaaneS, LaalDaaneRemark, OtherGA, OtherS, OtherRemark, ChokarDaaneGA, ChokarDaaneS, ChokarDaaneRemark, NamiGA, NamiS, NamiRemark, IP, Current_DateTime, CMR_RemainingID_MultipleID, Rcvd_District, Rcvd_IssueCenter, Rcvd_Branch, Rcvd_Godown, Daane, LotNumber, Rejection_No, Total_Rejected_Qty, CMR_DO_RejQty, Accepted, Rejected) values('" + gatepass + "','" + ddlAgreeDist.SelectedValue.ToString() + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgreeNo.SelectedValue.ToString() + "','" + txtCropYear.Text + "','" + dt.Rows[i]["fromdistval"] + "','" + dt.Rows[i]["todistval"] + "','0','0','" + txtNoOfBags.Text + "','" + BagType + "','" + tags + "','" + txtTagNo.Text + "','" + txtTruckNoOne.Text + "','" + txtToulReceiptNo.Text + "','" + ddl_IC.SelectedValue.ToString() + "','" + BookNumber + "','" + IssuedDate + "',N'" + lblMillingType.Text + "','" + 0 + "','" + txtTruckNo.Text + "', '0','0','" + TxtTotaS.Text + "','" + txtRmkTota.Text + "','0','" + TxtChoteToteS.Text + "','" + txtRmkCTote.Text + "','0','" + txtVijatiyeS.Text + "','" + txtRmkVijatiye.Text + "','0','" + txtDamageDaaneS.Text + "','" + txtRmkDaane.Text + "','0','" + txtBadrangDaaneS.Text + "','" + txtRmkBadrang.Text + "','0','" + txtChaakiDaaneS.Text + "','" + txtRmkChaki.Text + "','0','" + txtLaalDaaneS.Text + "','" + txtRmkLaal.Text + "','0','" + txtOtherS.Text + "','" + txtRmkShreni.Text + "','0','" + txtChokarDaaneS.Text + "','" + txtRmkChokar.Text + "','0','" + txtNamiS.Text + "','" + txtRmkNami.Text + "','" + ip + "',GETDATE(),'" + GBID_Multiple + "','" + DistId + "','" + ICID + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodam.SelectedValue.ToString() + "','" + Daane + "','" + dt.Rows[i]["LotNumber"] + "', '" + BookNumber + "','" + Session["QtyTotal"].ToString() + "','" + dt.Rows[i]["todistval"] + "','0','Y'); ";

                            //instr += "insert into dbo.tbl_Receipt_Details(State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch) values('23','" + DistId + "','" + IC_Id + "','" + gatepass1 + "','05','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgreeDist.SelectedValue.ToString() + "','','','','" + IssuedDate + "','" + IssuedDate + "','" + BookNumber + "','" + IssuedDate + "','270','3','0','" + txtCropYear.Text + "','1','','" + txtTruckNo.Text + "','','" + BagType + "'," + txtNoOfBags.Text + "," + dt.Rows[i]["todistval"] + "," + txtNoOfBags.Text + ",'0','','0'," + month + "," + year + ",'N','" + ip + "',getdate(),'','N','" + ddlGodam.SelectedValue.ToString() + "','" + opid + "','N','','" + ddlBranch.SelectedValue.ToString() + "') ; ";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            string check = (string)cmd.ExecuteScalar();
                        }
                    }

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    trAftSub.Visible = true;
                    Label2.Visible = true;
                    Label2.Text = "Your CMR Rejcteion Number Is : " + BookNumber;
                    btnAccept.Enabled = false;
                    btnPrint.Enabled = true;
                    btnReject.Enabled = false;
                        


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
}
