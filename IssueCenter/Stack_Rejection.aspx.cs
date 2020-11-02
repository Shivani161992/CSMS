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
public partial class IssueCenter_Stack_Rejection : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    public string gatepass, Masterinsp_ID = "";
    public int getnum, MasterinspNum;
    SqlDataReader dr;
    // protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";
    public string DistId, ICID;
    string MillName, BagsType, Inspector;
    string MSRejection = "";
    string MSAcceptance = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                GetCropYearValues();


            }
           


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        //txtDateOfInsp.Text = Request.Form[txtDateOfInsp.UniqueID];
        // txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
        //DateTime dateTime = DateTime.UtcNow.Date;

        //txtIssuedDate.Text = Convert.ToString(dateTime.ToString("dd/MM/yyyy"));
        string fromdate = Request.Form[txtIssuedDate.UniqueID];
        txtIssuedDate.Text = fromdate;

    }


    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlCropyear.DataSource = ds.Tables[0];
                    ddlCropyear.DataTextField = "CropYear";
                    ddlCropyear.DataValueField = "CropYear";
                    ddlCropyear.DataBind();
                    ddlCropyear.Items.Insert(0, "--Select--");
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

    protected void ddlCropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCommodity.Items.Clear();
        ddlDist.Items.Clear();
        ddlIssueCenter.Items.Clear();
        ddlGodown.Items.Clear();
        ddlStack.Items.Clear();
        trgrid.Visible = false;
        if (ddlCropyear.SelectedIndex > 0)
        {
            GetCommodity();

        }
        else { }
    }

    public void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                //string DistCode = Session["dist_id"].ToString();

                string select = string.Format("select Commodity_Id,Commodity_Name,* from tbl_MetaData_STORAGE_COMMODITY where Status='Y'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCommodity.DataSource = ds.Tables[0];
                        ddlCommodity.DataTextField = "Commodity_Name";
                        ddlCommodity.DataValueField = "Commodity_Id";
                        ddlCommodity.DataBind();
                        ddlCommodity.Items.Insert(0, "--Select--");

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

    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDist.Items.Clear();
        ddlIssueCenter.Items.Clear();
        ddlGodown.Items.Clear();
        ddlStack.Items.Clear();
        trgrid.Visible = false;
        if (ddlCommodity.SelectedIndex > 0)
        {
            GetDist();
            if (ddlCommodity.SelectedValue.ToString() == "3")
            {
                trparameters.Visible = true;
                GetCropYearParameters();
            }
            else
            {
                trparameters.Visible = false;

            }

        }
        else
        {

        }
    }


    public void GetCropYearParameters()
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


                    LblTotaS.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    //LblChoteToteGA.Text = ds.Tables[0].Rows[0]["ChoteToteGA"].ToString();
                    LblChoteToteS.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    //LblVijatiyeGA.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    LblVijatiyeS.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    //LblDamageDaaneGA.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    LblDamageDaaneS.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    //LblBadrangDaaneGA.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    LblBadrangDaaneS.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    //LblChaakiDaaneGA.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    LblChaakiDaaneS.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    //LblLaalDaaneGA.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    LblLaalDaaneS.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    //LblOtherGA.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    // LblOtherS.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    //LblChokarDaaneGA.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    //LblChokarDaaneS.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    //LblNamiGA.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
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
                        ddlDist.DataSource = ds.Tables[0];
                        ddlDist.DataTextField = "district_name";
                        ddlDist.DataValueField = "district_code";
                        ddlDist.DataBind();
                        ddlDist.Items.Insert(0, "--Select--");

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

    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIssueCenter.Items.Clear();
        ddlGodown.Items.Clear();
        ddlStack.Items.Clear();
        trgrid.Visible = false;
        if (ddlDist.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {

        }
    }

    private void GetMPIssueCentre()
    {
        DistId = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDist.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueCenter.DataSource = ds.Tables[0];
                    ddlIssueCenter.DataTextField = "DepotName";
                    ddlIssueCenter.DataValueField = "DepotID";
                    ddlIssueCenter.DataBind();
                    ddlIssueCenter.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlIssueCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        ddlStack.Items.Clear();
        trgrid.Visible = false;
        if (ddlIssueCenter.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {

        }
    }

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlIssueCenter.SelectedValue.ToString() + "' and  DistrictId='23" + ddlDist.SelectedValue.ToString() + "'order by Godown_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                        // GetStack();
                        // btnQuilityTested.Enabled = true;

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

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStack.Items.Clear();
        trgrid.Visible = false;

        if (ddlGodown.SelectedIndex > 0)
        {
            GetStack();
        }
        else
        {

        }

    }
    public void GetStack()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Stack_ID, Stack_Name from tbl_MetaData_STACK where Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and District_Id='23" + ddlDist.SelectedValue.ToString() + "' order by Stack_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlStack.DataSource = ds.Tables[0];
                        ddlStack.DataTextField = "Stack_Name";
                        ddlStack.DataValueField = "Stack_ID";
                        ddlStack.DataBind();
                        ddlStack.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Stack No. is not available'); </script> ");
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

    protected void ddlStack_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMobNum.Text = "";
        txtDesig.Text = "";
        if (ddlStack.SelectedIndex > 0)
        {
            GetInspectorNames();
            trgrid.Visible = true;
            FillGrid();
        }
        else
        {

        }

    }

    public void GetInspectorNames()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Inspector_ID, Inspector_Name from Inspector_Master_02017 where SpecialStatus='Yes' and GETDATE()>=Frmdate and GETDATE()<=ToDate order by Inspector_Name ");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlInsp.DataSource = ds.Tables[0];
                    ddlInsp.DataTextField = "Inspector_Name";
                    ddlInsp.DataValueField = "Inspector_ID";
                    ddlInsp.DataBind();
                    ddlInsp.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('HO Level पर कोई भी इंस्पेक्टर उपलभ नहीं है|'); </script> ");
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

    protected void ddlInsp_TextChanged(object sender, EventArgs e)
    {
        txtMobNum.Text = "";
        txtDesig.Text = "";
        if (ddlInsp.SelectedIndex > 0)
        {
            GetInspData();

        }
        else { }
    }
    public void GetInspData()
    {
        txtMobNum.Text = "";
        txtDesig.Text = "";
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("Select Inspector_desig, MobileNum  from Inspector_Master_02017 where Inspector_ID='" + ddlInsp.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "Inspector_Master_02017");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtMobNum.Text = ds.Tables[0].Rows[0]["MobileNum"].ToString();
                    txtDesig.Text = ds.Tables[0].Rows[0]["Inspector_desig"].ToString();
                    btnQuilityTested.Enabled = true;

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


    public void FillGrid()
    {
        DataSet ds = new DataSet();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select Inspector_Name, MR.Mill_Name, QI.Agreement_ID, LotNumber, DO_No, CDO.CMR_DO ,  Acceptance_No, Accept_CommonRice, Bags, BT.BagType ";
                select = select + " from CMR_QualityInspection as QI";
                select = select + " inner join CMR_DepositOrder as CDO on CDO.CMR_DO=QI.DO_Number";
                select = select + " inner join Inspector_Master_02017 as Insp on Insp.Inspector_ID=QI.Inspector_ID";

                select = select + " inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=QI.BagType";
                select = select + " inner join Miller_Registration_2017 as MR on MR.Registration_ID=QI.Mill_Name";
                select = select + " where StackNumber='" + ddlStack.SelectedValue.ToString() + "' and QI.District='" + ddlDist.SelectedValue.ToString() + "' and IsAccepted='Y' and  (((QI.IsStackAccepted is null ) OR (QI.IsStackAccepted='')) and ((QI.IsStackRejected is null) OR (QI.IsStackRejected='')))";


                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                GridView1.DataSource = ds;
                GridView1.DataBind();


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

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow &&
   (e.Row.RowState == DataControlRowState.Normal ||
    e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chk_select = (CheckBox)e.Row.Cells[1].FindControl("chk_select");
            CheckBox chkBxHeader = (CheckBox)this.GridView1.HeaderRow.FindControl("chkBxHeader");
            chk_select.Attributes["onclick"] = string.Format
                                                   (
                                                      "javascript:ChildClick(this,'{0}');",
                                                      chkBxHeader.ClientID
                                                   );
        }
    }
    protected void btnQuilityTested_Click(object sender, EventArgs e)
    {
        if (ddlCropyear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (ddlCommodity.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Commodity'); </script> ");
            return;

        }
        else if (ddlDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select District'); </script> ");
            return;

        }
        else if (ddlIssueCenter.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Issue Center'); </script> ");
            return;

        }
        else if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Godown'); </script> ");
            return;

        }
        else if (ddlStack.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Stack Number'); </script> ");
            return;

        }
        else if (ddlInsp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Inspector Name'); </script> ");
            return;

        }
        else if (txtIssuedDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Date of Inspection'); </script> ");
            return;

        }
        //else if (txtbags.Text == "")
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter bags type'); </script> ");
        //    return;

        //}
        else
        {
            string fromdate = Request.Form[txtIssuedDate.UniqueID];
            txtIssuedDate.Text = fromdate;
            ConvertServerDate ServerDate = new ConvertServerDate();
            string IssuedDate = ServerDate.getDate_MDY(txtIssuedDate.Text);
            Session["DOI"] = IssuedDate;



            if (float.Parse(LblTotaS.Text) >= float.Parse(TxtTotaS.Text) && float.Parse(LblChoteToteS.Text) >= float.Parse(TxtChoteToteS.Text) && float.Parse(LblVijatiyeS.Text) >= float.Parse(txtVijatiyeS.Text) && float.Parse(LblDamageDaaneS.Text) >= float.Parse(txtDamageDaaneS.Text) && float.Parse(LblBadrangDaaneS.Text) >= float.Parse(txtBadrangDaaneS.Text) && float.Parse(LblChaakiDaaneS.Text) >= float.Parse(txtChaakiDaaneS.Text) && float.Parse(LblLaalDaaneS.Text) >= float.Parse(txtLaalDaaneS.Text) && float.Parse(LblNamiS.Text) >= float.Parse(txtNamiS.Text))
            {
                // btnAccept.Enabled = true;
                //  btnReject.Enabled = true;
                bttaccept.Enabled = true;
                bttreject.Enabled = false;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";

                ddlCropyear.Enabled = false;
                ddlCommodity.Enabled = false;
                ddlDist.Enabled = false;
                ddlIssueCenter.Enabled = false;
                ddlGodown.Enabled = false;
                ddlStack.Enabled = false;
                ddlInsp.Enabled = false;
                txtDesig.Enabled = false;
                txtMobNum.Enabled = false;
                txtOthMobNum.Enabled = false;
                txtIssuedDate.Enabled = false;
               // txtbags.Enabled = false;
               



            }
            else
            {
                bttaccept.Enabled = false;
                bttreject.Enabled = true;
                //btnReject.Enabled = true;
                // btnAccept.Enabled = false;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";

                ddlCropyear.Enabled = false;
                ddlCommodity.Enabled = false;
                ddlDist.Enabled = false;
                ddlIssueCenter.Enabled = false;
                ddlGodown.Enabled = false;
                ddlStack.Enabled = false;
                ddlInsp.Enabled = false;
                txtDesig.Enabled = false;
                txtMobNum.Enabled = false;
                txtOthMobNum.Enabled = false;
                txtIssuedDate.Enabled = false;
                //txtbags.Enabled = false;
               
            }
        }

    }

    protected void bttaccept_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string GetCropYear = ddlCropyear.SelectedValue.ToString();
                string year_do = GetCropYear.Substring(7);

                string qrey = "select max(Masterinsp_ID) as Masterinsp_ID  from tblStackrejection where  LEN(Masterinsp_ID)<20 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                Masterinsp_ID = ds.Tables[0].Rows[0]["Masterinsp_ID"].ToString();


                if (Masterinsp_ID == "")
                {
                    Masterinsp_ID = year_do + "17" + "00";
                }
                else
                {
                    //string Masterinsp_ID = Masterinsp_ID.Substring(7)

                    MasterinspNum = Convert.ToInt32(Masterinsp_ID);

                    MasterinspNum = MasterinspNum + 1;
                    Masterinsp_ID = MasterinspNum.ToString();
                    //Masterinsp_ID =  Masterinsp_ID;
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

        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox Check = (CheckBox)gr.FindControl("chk_select");

            if (Check.Checked == true)
            {


                string Agreement = gr.Cells[3].Text;
                string LotNum = gr.Cells[4].Text;

                string PaddyDO = gr.Cells[5].Text;
                string CMRDO = gr.Cells[6].Text;
                string CMRAcceptance = gr.Cells[7].Text;

                string AcceptedQty = gr.Cells[8].Text;
                string AcceptedBags = gr.Cells[9].Text;
                using (con = new SqlConnection(strcon))
                {
                    try
                    {

                        con.Open();
                        string select = string.Format("Select Inspector_ID, Mill_Name, BagType   from CMR_QualityInspection where Acceptance_No='" + CMRAcceptance + "'");
                        da = new SqlDataAdapter(select, con);

                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            MillName = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                            Inspector = ds.Tables[0].Rows[0]["Inspector_ID"].ToString();
                            BagsType = ds.Tables[0].Rows[0]["BagType"].ToString();


                        }
                        string GetCropYear = ddlCropyear.SelectedValue.ToString();
                        string year_do = GetCropYear.Substring(7);

                        string qrey = "select max(InspectionID) as InspectionID  from tblStackrejection where  LEN(InspectionID)<20 ";
                        da = new SqlDataAdapter(qrey, con);

                        ds = new DataSet();
                        da.Fill(ds);

                        DataRow dr = ds.Tables[0].Rows[0];

                        gatepass = ds.Tables[0].Rows[0]["InspectionID"].ToString();


                        if (gatepass == "")
                        {
                            gatepass = year_do + "17" + "01";
                        }
                        else
                        {
                            getnum = Convert.ToInt32(gatepass);

                            getnum = getnum + 1;
                            gatepass = getnum.ToString();
                        }

                        string AcceptanceNum = "";
                        AcceptanceNum = "SA" + MillName + gatepass;

                        MSAcceptance = "MSSA" + Masterinsp_ID;

                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string DistCode = Session["dist_id"].ToString();






                        string instr = "";

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                      "update CMR_QualityInspection set IsStackAccepted='YES', IsStackRejected='NO' where Acceptance_No='" + CMRAcceptance + "'";

                        instr += "Update PaddyMilling_DO_2017 Set IsStackAccepted='YES', IsStackRejected='NO' where Check_DO='" + PaddyDO + "' and Mill_Code='" + MillName + "' and Agreement_ID='" + Agreement + "' and DhanLot='" + LotNum + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "'";

                        instr += "update CMR_DepositOrder Set IsStackAccepted='YES', IsStackRejected='NO' where CMR_DO='" + CMRDO + "'";

                        instr += "insert into tblStackrejection ([InspectionID], [CropYear], [Commodity], [D_O_Inspection], [Inspector_Name], [Designation], [mill_phone], [District_ID], [ICenter_ID], [Godown_ID], [Stack_ID], [TotaS], [ChoteToteS], [VijatiyeS], [DamageDaaneS], [BadrangDaaneS], [ChaakiDaaneS], [LaalDaaneS], [NamiS], [Status], [IP], [Created_Date] , [Stack_Name], [Acceptance_NO], [Rejection_NO], [Bags], [Agreement_ID], [LotNumber], [millname] , [CMR_Acceptance_No] , [millercode], [BagType], [Accept_CommonRice], ICInspName, PaddyDO, CMRDO, firstBags, OtherMobileNum , UserDist , UserIC, Masterinsp_ID, MSAcceptanceNum, MSRejection, BookNumber) values ('" + gatepass + "','" + ddlCropyear.SelectedValue.ToString() + "', '" + ddlCommodity.SelectedValue.ToString() + "', '" + Session["DOI"].ToString() + "','" + ddlInsp.SelectedValue.ToString() + "','" + txtDesig.Text + "','" + txtMobNum.Text + "','" + ddlDist.SelectedValue.ToString() + "','" + ddlIssueCenter.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "','" + ddlStack.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','Accepted','" + GetIp + "',Getdate(),'" + ddlStack.SelectedItem.Text + "','" + AcceptanceNum + "','" + 0 + "','0','" + Agreement + "','" + LotNum + "','" + MillName + "','" + CMRAcceptance + "','" + MillName + "','" + BagsType + "','" + AcceptedQty + "','" + Inspector + "','" + PaddyDO + "','" + CMRDO + "','" + AcceptedBags + "','" + txtOthMobNum.Text + "','" + DistId + "','" + ICID + "','" + Masterinsp_ID + "','" + MSAcceptance + "','0','" + MSAcceptance + "')";

                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";





                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();


                        if (count > 0)
                        {

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is saved successfully'); </script> ");
                            bttaccept.Enabled = false;
                            //buttonprint.Enabled = true;
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
        }
       
       // bttnPrint.Enabled = true;
        Session["DOI"] = "";
       
        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is saved successfully'); </script> ");
        trNumber.Visible = true;
        Label2.Visible = true;
        Label2.Text = "Acceptance Number is :- " + MSAcceptance;
        Session["Inspection"] = MSAcceptance;
    }

    protected void bttreject_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string GetCropYear = ddlCropyear.SelectedValue.ToString();
                string year_do = GetCropYear.Substring(7);

                string qrey = "select max(Masterinsp_ID) as Masterinsp_ID  from tblStackrejection where  LEN(Masterinsp_ID)<20 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                Masterinsp_ID = ds.Tables[0].Rows[0]["Masterinsp_ID"].ToString();


                if (Masterinsp_ID == "")
                {
                    Masterinsp_ID = year_do + "17" + "00";
                }
                else
                {
                    MasterinspNum = Convert.ToInt32(Masterinsp_ID);

                    MasterinspNum = MasterinspNum + 1;
                    Masterinsp_ID = MasterinspNum.ToString();
                    // Masterinsp_ID =  Masterinsp_ID;
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
        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox Check = (CheckBox)gr.FindControl("chk_select");

            if (Check.Checked == true)
            {


                string Agreement = gr.Cells[3].Text;
                string LotNum = gr.Cells[4].Text;

                string PaddyDO = gr.Cells[5].Text;
                string CMRDO = gr.Cells[6].Text;
                string CMRAcceptance = gr.Cells[7].Text;

                string AcceptedQty = gr.Cells[8].Text;
                string AcceptedBags = gr.Cells[9].Text;
                using (con = new SqlConnection(strcon))
                {
                    try
                    {

                        con.Open();
                        string select = string.Format("Select Inspector_ID, Mill_Name, BagType   from CMR_QualityInspection where Acceptance_No='" + CMRAcceptance + "'");
                        da = new SqlDataAdapter(select, con);

                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            MillName = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                            Inspector = ds.Tables[0].Rows[0]["Inspector_ID"].ToString();
                            BagsType = ds.Tables[0].Rows[0]["BagType"].ToString();


                        }
                        string GetCropYear = ddlCropyear.SelectedValue.ToString();
                        string year_do = GetCropYear.Substring(7);

                        string qrey = "select max(InspectionID) as InspectionID  from tblStackrejection where  LEN(InspectionID)<20 ";
                        da = new SqlDataAdapter(qrey, con);

                        ds = new DataSet();
                        da.Fill(ds);

                        DataRow dr = ds.Tables[0].Rows[0];

                        gatepass = ds.Tables[0].Rows[0]["InspectionID"].ToString();


                        if (gatepass == "")
                        {
                            gatepass = year_do + "17" + "01";
                        }
                        else
                        {
                            getnum = Convert.ToInt32(gatepass);

                            getnum = getnum + 1;
                            gatepass = getnum.ToString();
                        }

                        string RejectionNum = "";
                        RejectionNum = "SR" + MillName + gatepass;

                        MSRejection = "MSSR" + Masterinsp_ID;

                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string DistCode = Session["dist_id"].ToString();


                        


                        string instr = "";

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                      "update CMR_QualityInspection set IsStackAccepted='NO', IsStackRejected='YES' where Acceptance_No='" + CMRAcceptance + "'";

                        instr += "Update PaddyMilling_DO_2017 Set DispatchDhan_IC='N', IsStackAccepted='NO', IsStackRejected='YES', RejectionCount=(ISNULL(RejectionCount,0)+1)  where Check_DO='" + PaddyDO + "' and Mill_Code='" + MillName + "' and Agreement_ID='" + Agreement + "' and DhanLot='" + LotNum + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "'";

                        instr += "Update PaddyMilling_Agreement_2017 set Return_CommonRice= (ISNULL(Return_CommonRice,0)-" + AcceptedQty + "), Return_TotalRice= (ISNULL(Return_TotalRice,0)-" + AcceptedQty + "),Rem_DhanLot=(Rem_DhanLot-1) where Agreement_ID='" + Agreement + "';";

                        instr += "update CMR_DepositOrder Set IsStackAccepted='NO', IsStackRejected='YES' where CMR_DO='" + CMRDO + "'";

                        instr += "insert into tblStackrejection ([InspectionID], [CropYear], [Commodity], [D_O_Inspection], [Inspector_Name], [Designation], [mill_phone], [District_ID], [ICenter_ID], [Godown_ID], [Stack_ID], [TotaS], [ChoteToteS], [VijatiyeS], [DamageDaaneS], [BadrangDaaneS], [ChaakiDaaneS], [LaalDaaneS], [NamiS], [Status], [IP], [Created_Date] , [Stack_Name], [Acceptance_NO], [Rejection_NO], [Bags], [Agreement_ID], [LotNumber], [millname] , [CMR_Acceptance_No] , [millercode], [BagType], [Accept_CommonRice], ICInspName, PaddyDO, CMRDO, firstBags, OtherMobileNum , UserDist , UserIC, Masterinsp_ID, MSAcceptanceNum, MSRejection, BookNumber) values ('" + gatepass + "','" + ddlCropyear.SelectedValue.ToString() + "', '" + ddlCommodity.SelectedValue.ToString() + "','" + Session["DOI"].ToString() + "','" + ddlInsp.SelectedValue.ToString() + "','" + txtDesig.Text + "','" + txtMobNum.Text + "','" + ddlDist.SelectedValue.ToString() + "','" + ddlIssueCenter.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "','" + ddlStack.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','Rejected','" + GetIp + "',Getdate(),'" + ddlStack.SelectedItem.Text + "','0','" + RejectionNum + "','0','" + Agreement + "','" + LotNum + "','" + MillName + "','" + CMRAcceptance + "','" + MillName + "','" + BagsType + "','" + AcceptedQty + "','" + Inspector + "','" + PaddyDO + "','" + CMRDO + "','" + AcceptedBags + "','" + txtOthMobNum.Text + "','" + DistId + "','" + ICID + "','" + Masterinsp_ID + "','0','" + MSRejection + "','" + MSRejection + "')";

                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";





                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();
                      
                        if (count > 0)
                        {

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is saved successfully'); </script> ");
                            bttreject.Enabled = false;
                           // buttonprint.Enabled = true;

                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
        }
        Session["DOI"] = "";
       
        //bttnPrint.Enabled = true;
        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is saved successfully'); </script> ");
        trNumber.Visible = true;
        Label2.Visible = true;
        Label2.Text = "Rejection Number is :- " + MSRejection;
        Session["Inspection"] = MSRejection;

    }

    //protected void bttnPrint_Click(object sender, EventArgs e)
    //{

    //    string url = "PrintStackNumber.aspx";
    //    string s = "window.open('" + url + "', 'popup_window');";
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    //}
  
   
}