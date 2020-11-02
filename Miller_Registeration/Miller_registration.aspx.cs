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
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;

public partial class Miller_Registeration_Miller_registration : System.Web.UI.Page
{
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    SqlCommand cmd;

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public string state, OtherState;
    SqlCommand cmd1;

    DataTable Dt1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            millersname.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            millersname.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            millersname.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_village.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_village.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_village.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_propaname.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_propaname.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_propaname.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_propa_address.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_propa_address.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_propa_address.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_propacity.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_propacity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_propacity.Attributes.Add("onchange", "return chksqltxt(this)");

            millerowner_name.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            millerowner_name.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            millerowner_name.Attributes.Add("onchange", "return chksqltxt(this)");

            miller_fathername.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            miller_fathername.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            miller_fathername.Attributes.Add("onchange", "return chksqltxt(this)");

            mill_owner_address.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            mill_owner_address.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            mill_owner_address.Attributes.Add("onchange", "return chksqltxt(this)");

            miller_participate1.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            miller_participate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            miller_participate1.Attributes.Add("onchange", "return chksqltxt(this)");

            miller_participate2.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            miller_participate2.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            miller_participate2.Attributes.Add("onchange", "return chksqltxt(this)");

            miller_participate3.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            miller_participate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            miller_participate3.Attributes.Add("onchange", "return chksqltxt(this)");

            miller_participate4.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            miller_participate4.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            miller_participate4.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_registrationno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_registrationno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_registrationno.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_mill_owner_name.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_mill_owner_name.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_mill_owner_name.Attributes.Add("onchange", "return chksqltxt(this)");

            txt_millowner_address.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_millowner_address.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_millowner_address.Attributes.Add("onchange", "return chksqltxt(this)");

            salstax.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            salstax.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            salstax.Attributes.Add("onchange", "return chksqltxt(this)");

            panno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            panno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            panno.Attributes.Add("onchange", "return chksqltxt(this)");

            alloted_sevicetax.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            alloted_sevicetax.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            alloted_sevicetax.Attributes.Add("onchange", "return chksqltxt(this)");

            prasans_lisanceno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            prasans_lisanceno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            prasans_lisanceno.Attributes.Add("onchange", "return chksqltxt(this)");

            vyapar_lisanceno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            vyapar_lisanceno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            vyapar_lisanceno.Attributes.Add("onchange", "return chksqltxt(this)");

            pratinidhi.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            pratinidhi.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            pratinidhi.Attributes.Add("onchange", "return chksqltxt(this)");

            address.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            address.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            address.Attributes.Add("onchange", "return chksqltxt(this)");

            rbMPState.Checked = true;

            ViewState["CountMPDistrict"] = "0";
            ViewState["CountOtherDistrict"] = "0";

            ViewState["CountMPDivision"] = "0";
            ViewState["CountOtherDivision"] = "0";

            fillmiller_representatives();
            //GetDistrict();
            getIssue();
            getIssue4();
            getIssue2();
            getIssue3();

            GetCropYear();

            rd_prototype.Checked = rd_millopen.Checked = rd_selfmill.Checked = true;

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }

        if (rbMPState.Checked)
        {
            if (ViewState["CountMPDistrict"].ToString() == "0")
            {
                GetMPDistrict();
                ViewState["CountMPDistrict"] = "1";
                ViewState["CountOtherDistrict"] = "0";

                ViewState["CountMPDivision"] = "0";
                ViewState["CountOtherDivision"] = "1";
            }

        }
        else if (rbOtherState.Checked)
        {
            if (ViewState["CountOtherDistrict"].ToString() == "0")
            {
                GetOtherStates();
                ViewState["CountOtherDistrict"] = "1";
                ViewState["CountMPDistrict"] = "0";

                ViewState["CountMPDivision"] = "1";
                ViewState["CountOtherDivision"] = "0";
            }
        }

        txt_agreement_end.Text = Request.Form[txt_agreement_end.UniqueID];
    }

    public void GetCropYear()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            lblCropYear.Text ="2017-2018";

            //string select = "Select CropYear From PaddyMilling_CropYear order by CropYear desc";
            //da = new SqlDataAdapter(select, con);

            //ds = new DataSet();
            //da.Fill(ds);
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //   // lblCropYear.Text  = ds.Tables[0].Rows[0]["CropYear"].ToString();
            //}
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            return;
        }

        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }

    public void GetMPDistrict()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersAddDivision.Items.Clear();
        ddlOtherStates.Items.Clear();

        txt_village.Text = txt_pincode.Text = txt_millphone.Text = "";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        {
            try
            {
                string qry = "SELECT district_code ,district_name FROM pds.districtsmp order by district_name ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDist.DataSource = ds.Tables[0];
                    ddlMacersAddDist.DataTextField = "district_name";
                    ddlMacersAddDist.DataValueField = "district_code";
                    ddlMacersAddDist.DataBind();
                    ddlMacersAddDist.Items.Insert(0, "--Select--");

                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public void GetOtherStates()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersAddDivision.Items.Clear();

        txt_village.Text = txt_pincode.Text = txt_millphone.Text = "";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        {
            try
            {
                string qry = "SELECT State_Code ,State_Name FROM State_Master where Status = 'Y' and State_Code!=23 order by State_Name";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlOtherStates.DataSource = ds.Tables[0];
                    ddlOtherStates.DataTextField = "State_Name";
                    ddlOtherStates.DataValueField = "State_Code";
                    ddlOtherStates.DataBind();
                    ddlOtherStates.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public void GetOtherDistrict()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersAddDivision.Items.Clear();
        txt_village.Text = txt_pincode.Text = txt_millphone.Text = "";

        //lblOtherStates.Visible = false;

        string dist = ddlOtherStates.SelectedValue.ToString();

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        {
            try
            {
                string qry = string.Format("SELECT district_code ,district_name FROM OtherState_DistrictCode where State_Id = '{0}'  order by district_name", dist);
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDist.DataSource = ds.Tables[0];
                    ddlMacersAddDist.DataTextField = "district_name";
                    ddlMacersAddDist.DataValueField = "district_code";
                    ddlMacersAddDist.DataBind();
                    ddlMacersAddDist.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlOtherStates_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOtherDistrict();
    }

    protected void ddlMacersAddDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbMPState.Checked)
        {
            if (ViewState["CountMPDivision"].ToString() == "0")
            {
                getTehsil();
            }

        }
        else if (rbOtherState.Checked)
        {
            if (ViewState["CountOtherDivision"].ToString() == "0")
            {
                GetSubDivision();
            }
        }
    }

    protected void getTehsil()
    {
        try
        {
            string dist = ddlMacersAddDist.SelectedValue.ToString();
            ddlMacersAddDivision.Items.Clear();
            txt_village.Text = txt_pincode.Text = txt_millphone.Text = "";

            string qrytehsil = "select * from  pds.Tehsilmp where District_code= '23" + dist + "' order by Tehsil_Name";


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(qrytehsil, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDivision.DataSource = ds.Tables[0];
                    ddlMacersAddDivision.DataTextField = "Tehsil_Name";
                    ddlMacersAddDivision.DataValueField = "TehsilCode";
                    ddlMacersAddDivision.DataBind();
                    ddlMacersAddDivision.Items.Insert(0, "--Select--");

                }
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            return;
        }
    }

    protected void GetSubDivision()
    {
        ddlMacersAddDivision.Items.Clear();
        txt_village.Text = txt_pincode.Text = txt_millphone.Text = "";

        string state = ddlOtherStates.SelectedValue.ToString();
        string dist = ddlMacersAddDist.SelectedValue.ToString();

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        {
            try
            {
                string selectTehsil = string.Format("select subdistrict_name,subdistrict_code from Sub_Division where state_code = '{0}' and district_code='{1}' order by subdistrict_name", state, dist);
                SqlCommand cmd = new SqlCommand(selectTehsil, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDivision.DataSource = ds.Tables[0];
                    ddlMacersAddDivision.DataTextField = "subdistrict_name";
                    ddlMacersAddDivision.DataValueField = "subdistrict_code";
                    ddlMacersAddDivision.DataBind();
                    ddlMacersAddDivision.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void getIssue()
    {
        try
        {
            string dist = ddlMacersAddDist.SelectedValue.ToString();
            ddl_issuecenter.Items.Clear();

            string qryissue = "select * from  tbl_MetaData_DEPOT order by DepotName";


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
            DataSet ds = new DataSet();
            da.Fill(ds);





            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_issuecenter.DataSource = ds.Tables[0];
                    ddl_issuecenter.DataTextField = "DepotName";
                    ddl_issuecenter.DataValueField = "DepotID";
                    ddl_issuecenter.DataBind();
                    ddl_issuecenter.Items.Insert(0, "--Select--");


                }
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void getIssue2()
    {
        try
        {
            string dist = ddlMacersAddDist.SelectedValue.ToString();
            //ddl_issuecenter.Items.Clear();

            string qryissue = "select * from  tbl_MetaData_DEPOT order by DepotName";


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
            DataSet ds = new DataSet();
            da.Fill(ds);





            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_issuecenter1.DataSource = ds.Tables[0];
                    ddl_issuecenter1.DataTextField = "DepotName";
                    ddl_issuecenter1.DataValueField = "DepotID";
                    ddl_issuecenter1.DataBind();
                    ddl_issuecenter1.Items.Insert(0, "--Select--");


                }
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void getIssue3()
    {
        try
        {
            string dist = ddlMacersAddDist.SelectedValue.ToString();
            //ddl_issuecenter.Items.Clear();

            string qryissue = "select * from  tbl_MetaData_DEPOT order by DepotName";


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
            DataSet ds = new DataSet();
            da.Fill(ds);





            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_issuecenter2.DataSource = ds.Tables[0];
                    ddl_issuecenter2.DataTextField = "DepotName";
                    ddl_issuecenter2.DataValueField = "DepotID";
                    ddl_issuecenter2.DataBind();
                    ddl_issuecenter2.Items.Insert(0, "--Select--");


                }
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void getIssue4()
    {
        try
        {
            string dist = ddlMacersAddDist.SelectedValue.ToString();
            //ddl_issuecenter.Items.Clear();

            string qryissue = "select * from  tbl_MetaData_DEPOT   order by DepotName";


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
            DataSet ds = new DataSet();
            da.Fill(ds);





            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_issuecenter3.DataSource = ds.Tables[0];
                    ddl_issuecenter3.DataTextField = "DepotName";
                    ddl_issuecenter3.DataValueField = "DepotID";
                    ddl_issuecenter3.DataBind();
                    ddl_issuecenter3.Items.Insert(0, "--Select--");

                }
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {

        }
    }
    void fillmiller_representatives()
    {


        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string query = "SELECT Top 1 * from tbl_millgride";
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {

            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataBind();
        }


    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (ddlCropYearshift.SelectedIndex==0)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('प्रतिदिन कितने शिफ्ट में काम किया जाना है, का चुनाव करे|');</script>");
            return;
        }
      
        if (ddlMacersAddDist.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('कृपया जिला चुने|');</script>");
            return;
        }
        if (ddlMacersAddDivision.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('कृपया तहसील चुने|');</script>");
            return;
        }
        if (ddl_issuecenter.SelectedIndex == 0 || ddl_issuecenter1.SelectedIndex == 0 || ddl_issuecenter2.SelectedIndex == 0 || ddl_issuecenter3.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('कृपया सारे निकटतम संग्रहण केंद्र (1,2,3,4) का चुनाव करे तथा उनकी दुरी लिखे|');</script>");
            return;
        }
        if (rd_millopen.Checked == false && rd_millclose.Checked == false)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('कृपया मिल की रनिंग स्थिति का चुनाव करे|');</script>");
            return;
        }
        if (millersname.Text.Trim() == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('कृपया मिल की नाम लिखे|');</script>");
            return;
        }

        //string relationship = string.Empty;
        string district = ddlMacersAddDist.SelectedValue.ToString();
        string enddate = "";
        if (txt_agreement_end.Enabled == true)
        {
            ConvertServerDate ServerDate = new ConvertServerDate();
            enddate = ServerDate.getDate_MDY(txt_agreement_end.Text);
        }
        else
        {
            enddate = "1/1/2015";
        }

        if (rd_millopen.Checked)
        {
            hd_runningwork.Value = "Open";
        }
        else
        {
            hd_runningwork.Value = "Close";
        }
        //string cetId = issue_centre_code.Substring(issue_centre_code.Length - 5);    // Left 5 digit from center for Do generation

        //string year_do = System.DateTime.Now.Date.ToString("yy");    // For DO generation year wise (29/03/14)

        string GetCropYear = lblCropYear.Text;
        string year_do = GetCropYear.Substring(7);

        string selectmax = "select isnull(max(cast(Registration_ID as bigint)),0) as RegistrationNo from Miller_Registration_2017 where District_Code='" + district + "' and CropYear='" + lblCropYear.Text + "'  ";

        SqlCommand cmdmax = new SqlCommand(selectmax, con);
        SqlDataAdapter damax = new SqlDataAdapter(cmdmax);

        DataSet dsmax = new DataSet();

        damax.Fill(dsmax);
        string mill_ID = dsmax.Tables[0].Rows[0]["RegistrationNo"].ToString();
        string monthcode = "";

        if (mill_ID == "0")
        {
            mill_ID = year_do + district + "100";
        }
        else
        {
            string fordo = mill_ID.Substring(mill_ID.Length - 3);

            if (fordo == "000")
            {
                fordo = "1000";
            }

            Int64 DO_ID_new = Convert.ToInt64(fordo);

            DO_ID_new = DO_ID_new + 1;

            string combine = DO_ID_new.ToString();

            mill_ID = year_do + district + combine;

        }


        string mill_idno = mill_ID;

        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            #region 
            //string[] validFileTypes = { "gif", "png", "jpg", "jpeg" };
            //string extention = System.IO.Path.GetExtension(FileUpload_millowner.PostedFile.FileName).ToLower();
            //bool isValidFile = false;
            //for (int i = 0; i < validFileTypes.Length; i++)
            //{
            //    if (extention == "." + validFileTypes[i])
            //    {
            //        isValidFile = true;
            //        break;
            //    }
            //}
            //if (!isValidFile)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Invalid File. Please upload a File with extension (gif, png, jpg, jpeg)'); </script> ");
            //    return;
            //}

            //if (FileUpload_millowner.PostedFile.ContentLength > 3145728)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिल संचालक का छायाचित्र 3MB से ज्यादा नहीं होना चाहिए|'); </script> ");
            //    return;
            //}

            //Byte[] bytesmillowner = null;
            //if (FileUpload_millowner.HasFile && FileUpload_millowner.PostedFile != null)
            //{
            //    HttpPostedFile File = FileUpload_millowner.PostedFile;
            //    bytesmillowner = new Byte[File.ContentLength];
            //    File.InputStream.Read(bytesmillowner, 0, File.ContentLength);
            //}

            //// Sanchalak Images
            //if (photo.PostedFile.ContentLength > 3145728)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('संचालक के द्वारा अधिकृत प्रतिनिधि का छायाचित्र 3MB से ज्यादा नहीं होना चाहिए|'); </script> ");
            //    return;
            //}

            //Byte[] bytesmillownerid1 = null;
            ////if (photo.HasFile && photo.PostedFile != null)
            //{
            //    HttpPostedFile File = photo.PostedFile;
            //    bytesmillownerid1 = new Byte[File.ContentLength];
            //    File.InputStream.Read(bytesmillownerid1, 0, File.ContentLength);
            //}

            //// Sanchalak Signature
            //if (signature.PostedFile.ContentLength > 3145728)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('संचालक के द्वारा अधिकृत प्रतिनिधि का हस्ताक्षर 3MB से ज्यादा नहीं होना चाहिए|'); </script> ");
            //    return;
            //}

            //Byte[] bytesmillownerid2 = null;
            ////if (signature.HasFile && signature.PostedFile != null)
            //{
            //    HttpPostedFile File = signature.PostedFile;
            //    bytesmillownerid2 = new Byte[File.ContentLength];
            //    File.InputStream.Read(bytesmillownerid2, 0, File.ContentLength);
            //}
#endregion

            if (rbMPState.Checked)
            {
                state = "MP";
                OtherState = "23";
            }
            else
            {
                state = "Other";
                OtherState = ddlOtherStates.SelectedValue.ToString();
            }


            //if (GchkBx.Checked == true)
            //{
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                string strselect = "Select District_Code from Miller_Registration_2017 where District_Code='" + district + "' and tehsil_code='" + ddlMacersAddDivision.SelectedValue.ToString() + "' and UPPER(Mill_Name)='" + millersname.Text + "' and CropYear='" + lblCropYear.Text + "' ";
                cmd1 = new SqlCommand(strselect, con);
                string check = (string)cmd1.ExecuteScalar();

                if (check == null)
                {
                    string ip1 = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    //string insqry = "insert into Miller_Registration_2017 ([District_Code],[tehsil_code],[Registration_ID],[Mill_Name] ,[mill_pincode] ,[mill_Address] ,[mill_phone],[mill_ownership],[mill_proto_name] ,[mill_proto_address] ,[mill_proto_city],[firm_type],[firmliasnce],[operator_name] ,[operator_father],[partner1],[partner2],[pratner3],[partner4],[Operator_permanent_add],[Operator_telephone1],[Operator_telephone2],[Operator_telephone3],[Operator_telephone4],[mill_running_status],[mill_ownership_status],[leez_mill_ownername],[leez_mill_owneraddress],[leez_mill_enddate],[milling_capacity_arwa],[milling_capacity_usna],[salstax_no],[alloted_servicetaxno],[current_servicetax],[mandipra_lisance],[mandivyapar_lisance],[mill_akshansh],[mill_deshant],[ic1],[distance1],[ic2] ,[distance2],[ic3],[distance3],[ic4],[distance4],[last_yearmillng_qunt],[millpassword],[mill_operater_photo],[reprenstaive_name],[id_card],[id_address],[re_photo],[re_sign],[Created_date],[ip],[Pan_No],[Status],[State],[State_Code],[Miller_MobileNo],CropYear,AprUnit,AprEmp,AprShift,MayUnit,MayEmp,MayShift,JuneUnit,JuneEmp,JuneShift,JulyUnit,JulyEmp,JulyShift,AugUnit,AugEmp,AugShift,SepUnit,SepEmp,SepShift,CropYear_Shift)values('" + district + "','" + ddlMacersAddDivision.SelectedValue.ToString() + "','" + mill_idno + "',UPPER('" + millersname.Text + "'),'" + txt_pincode.Text + "',N'" + txt_village.Text + "',N'" + txt_millphone.Text + "','" + hd_millowner.Value + "',N'" + txt_propaname.Text + "',N'" + txt_propa_address.Text + "','" + txt_propacity.Text + "',N'" + ddl_firmtype.SelectedValue.ToString() + "',N'" + txt_registrationno.Text + "',N'" + millerowner_name.Text + "',N'" + miller_fathername.Text + "',N'" + miller_participate1.Text + "',N'" + miller_participate2.Text + "',N'" + miller_participate3.Text + "',N'" + miller_participate4.Text + "',N'" + mill_owner_address.Text + "',N'" + miller_telephone_home.Text + "',N'" + miller_telephone_office.Text + "','" + miller_moblie1.Text + "','" + miller_mobile2.Text + "','" + hd_runningwork.Value + "','" + hd_millowner.Value + "',N'" + txt_mill_owner_name.Text + "',N'" + txt_millowner_address.Text + "','" + enddate + "','" + txt_capacity_arwa.Text + "','" + txt_capacity_usna.Text + "',N'" + salstax.Text + "',N'" + alloted_sevicetax.Text + "',N'" + current_reading.Text + "',N'" + prasans_lisanceno.Text + "',N'" + vyapar_lisanceno.Text + "',N'" + akshansh.Text + "',N'" + deshant.Text + "','" + ddl_issuecenter.SelectedValue.ToString() + "','" + txt_ictocmr_distance.Text + "','" + ddl_issuecenter1.SelectedValue.ToString() + "','" + txt_ictocmr_distance4.Text + "','" + ddl_issuecenter2.SelectedValue.ToString() + "','" + txt_ictocmr_distance1.Text + "','" + ddl_issuecenter3.SelectedValue.ToString() + "','" + txt_ictocmr_distance2.Text + "','" + lastyear_milling_quantity.Text + "','" + mill_idno + "',@MillOwnerImage,'" + pratinidhi.Text + "','" + address.Text + "',N'" + ddidentity.SelectedValue.ToString() + "',@bytesmillownerid1,@bytesmillownerid2,getdate(),'" + ip1 + "','" + panno.Text + "' ,'" + 0 + "','" + state + "','" + OtherState + "','" + txt_MobileNo.Text + "','" + lblCropYear.Text + "','" + txtAprUnit.Text + "','" + txtAprEmp.Text + "','" + txtAprShift.Text + "','" + txtMayUnit.Text + "','" + txtMayEmp.Text + "','" + txtMayShift.Text + "','" + txtJuneUnit.Text + "','" + txtJuneEmp.Text + "','" + txtJuneShift.Text + "','" + txtJulyUnit.Text + "','" + txtJulyEmp.Text + "','" + txtJulyShift.Text + "','" + txtAugUnit.Text + "','" + txtAugEmp.Text + "','" + txtAugShift.Text + "','" + txtSepUnit.Text + "','" + txtSepEmp.Text + "','" + txtSepShift.Text + "','" + ddlCropYearshift.SelectedItem.ToString()+ "') ";
                    string lastYearMillingstatus="";
                    if(txt_millingofPreviousYear.Checked==true)
                    {
                    lastYearMillingstatus="Yes";

                    }
                    else
                    {
                        lastYearMillingstatus="No";
                    }
                    //SqlCommand cmd = new SqlCommand(insqry, con);
                    //cmd.Parameters.AddWithValue("@MillOwnerImage", bytesmillowner);
                    //cmd.Parameters.AddWithValue("@bytesmillownerid1", bytesmillownerid1);
                    //cmd.Parameters.AddWithValue("@bytesmillownerid2", bytesmillownerid2);
                    string BListed = "N";
                    string insqry = "insert into Miller_Registration_2017 ([District_Code],[tehsil_code],[Registration_ID],[Mill_Name] ,[mill_pincode] ,[mill_Address] ,[mill_phone],[mill_ownership],[mill_proto_name] ,[mill_proto_address] ,[mill_proto_city],[firm_type],[firmliasnce],[operator_name] ,[operator_father],[partner1],[partner2],[pratner3],[partner4],[Operator_permanent_add],[Operator_telephone1],[Operator_telephone2],[Operator_telephone3],[Operator_telephone4],[mill_running_status],[mill_ownership_status],[leez_mill_ownername],[leez_mill_owneraddress],[leez_mill_enddate],[milling_capacity_arwa],[milling_capacity_usna],[salstax_no],[alloted_servicetaxno],[current_servicetax],[mandipra_lisance],[mandivyapar_lisance],[mill_akshansh],[mill_deshant],[ic1],[distance1],[ic2] ,[distance2],[ic3],[distance3],[ic4],[distance4],[last_yearmillng_qunt],[millpassword],[reprenstaive_name],[id_card],[id_address],[Created_date],[ip],[Pan_No],[Status],[State],[State_Code],[Miller_MobileNo],CropYear,AprUnit,AprEmp,AprShift,MayUnit,MayEmp,MayShift,JuneUnit,JuneEmp,JuneShift,JulyUnit,JulyEmp,JulyShift,AugUnit,AugEmp,AugShift,SepUnit,SepEmp,SepShift,CropYear_Shift, Black_listed,upcoming_SixMonths, Total_AgreeQty, Total_BRLQty_Insp, Upgraded_BRLQty, Remain_qty, Aadhar_number, Did_millinglastyear, OldGunnybags)values('" + district + "','" + ddlMacersAddDivision.SelectedValue.ToString() + "','" + mill_idno + "',UPPER('" + millersname.Text + "'),'" + txt_pincode.Text + "',N'" + txt_village.Text + "',N'" + txt_millphone.Text + "','" + hd_millowner.Value + "',N'" + txt_propaname.Text + "',N'" + txt_propa_address.Text + "','" + txt_propacity.Text + "',N'" + ddl_firmtype.SelectedValue.ToString() + "',N'" + txt_registrationno.Text + "',N'" + millerowner_name.Text + "',N'" + miller_fathername.Text + "',N'" + miller_participate1.Text + "',N'" + miller_participate2.Text + "',N'" + miller_participate3.Text + "',N'" + miller_participate4.Text + "',N'" + mill_owner_address.Text + "',N'" + miller_telephone_home.Text + "',N'" + miller_telephone_office.Text + "','" + miller_moblie1.Text + "','" + miller_mobile2.Text + "','" + hd_runningwork.Value + "','" + hd_millowner.Value + "',N'" + txt_mill_owner_name.Text + "',N'" + txt_millowner_address.Text + "','" + enddate + "','" + txt_capacity_arwa.Text + "','" + txt_capacity_usna.Text + "',N'" + salstax.Text + "',N'" + alloted_sevicetax.Text + "',N'" + current_reading.Text + "',N'" + prasans_lisanceno.Text + "',N'" + vyapar_lisanceno.Text + "',N'" + akshansh.Text + "',N'" + deshant.Text + "','" + ddl_issuecenter.SelectedValue.ToString() + "','" + txt_ictocmr_distance.Text + "','" + ddl_issuecenter1.SelectedValue.ToString() + "','" + txt_ictocmr_distance4.Text + "','" + ddl_issuecenter2.SelectedValue.ToString() + "','" + txt_ictocmr_distance1.Text + "','" + ddl_issuecenter3.SelectedValue.ToString() + "','" + txt_ictocmr_distance2.Text + "','" + lastyear_milling_quantity.Text + "','" + mill_idno + "','" + pratinidhi.Text + "','" + address.Text + "',N'" + ddidentity.SelectedValue.ToString() + "',getdate(),'" + ip1 + "','" + panno.Text + "' ,'" + 0 + "','" + state + "','" + OtherState + "','" + txt_MobileNo.Text + "','" + lblCropYear.Text + "','" + txtAprUnit.Text + "','" + txtAprEmp.Text + "','" + txtAprShift.Text + "','" + txtMayUnit.Text + "','" + txtMayEmp.Text + "','" + txtMayShift.Text + "','" + txtJuneUnit.Text + "','" + txtJuneEmp.Text + "','" + txtJuneShift.Text + "','" + txtJulyUnit.Text + "','" + txtJulyEmp.Text + "','" + txtJulyShift.Text + "','" + txtAugUnit.Text + "','" + txtAugEmp.Text + "','" + txtAugShift.Text + "','" + txtSepUnit.Text + "','" + txtSepEmp.Text + "','" + txtSepShift.Text + "','" + ddlCropYearshift.SelectedItem.ToString() + "','" + BListed + "','" + txt_upcomingsixmonths.Text + "','" + txt_total_agreeQty.Text + "','" + txt_InsBRLQty.Text + "', '" + txt_Changed_BRLQty.Text + "', '" + txt_remQty.Text + "', '" + txt_aadharCard.Text + "','" + lastYearMillingstatus + "','" + txtgunnyBags.Text + "') ";

                    SqlCommand cmd = new SqlCommand(insqry, con);

                    int x = cmd.ExecuteNonQuery();

                    if (x > 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Registration is saved successfully & Your Registration No. is : " + mill_ID + "' ); </script> ");
                        Session["RegistID"] = mill_ID.ToString();
                        Session["CropYear"] = lblCropYear.Text;
                        Session["PinCode"] = txt_pincode.Text;
                        Session["MobileNo"] = txt_MobileNo.Text;

                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        btn_submit.Enabled = false;
                        btnPrint.Enabled = true;
                    }
                    else
                    {
                        Session["RegistID"] = "";
                        Session["CropYear"] = "";
                        Session["PinCode"] = "";
                        Session["MobileNo"] = "";
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Failed!'); </script> ");
                    }
                    lbl_passwordname.Visible = true;
                    lbl_registeredno.Visible = true;
                    lbl_passwordname1.Visible = true;
                    lbl_registeredno1.Visible = true;
                    lbl_registeredno.Text = lbl_registeredno1.Text = mill_ID;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आप मिल का रजिस्ट्रेशन एक ही बार कर सकते हैं तथा आपकी मिल का रजिस्ट्रेशन इसके पहले हो चूका हैं|'); </script> ");
                }
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            //}

            // }
        }
        else
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    //protected void chk_select_CheckedChanged(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow row in GridView1.Rows)
    //    {
    //        CheckBox cb = (CheckBox)row.FindControl("chk_select");
    //        TextBox tbname = (TextBox)(row.FindControl("txt_pratinidhi"));
    //        TextBox tbaddress = (TextBox)(row.FindControl("txt_address"));

    //        if (cb.Checked)
    //        {

    //            tbname.Enabled = true;
    //            tbaddress.Enabled = true;

    //        }
    //        else
    //        {
    //            tbname.Enabled = false;
    //            tbaddress.Enabled = false;
    //        }
    //    }
    //}
    protected void rd_prototype_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_prototype.Checked)
        {
            hd_millowner.Value = "Pro";

            txt_propaname.Enabled = true;
            txt_propacity.Enabled = true;
            txt_propa_address.Enabled = true;
            ddl_firmtype.SelectedValue = "0";
            txt_registrationno.Text = "";
            ddl_firmtype.Enabled = false;
            txt_registrationno.Enabled = false;
        }
        else
        {
            txt_propaname.Enabled = false;
            txt_propacity.Enabled = false;
            txt_propa_address.Enabled = false;

        }
    }
    protected void rd_firm_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_firm.Checked)
        {
            hd_millowner.Value = "firm";
            ddl_firmtype.Enabled = true;
            txt_registrationno.Enabled = true;
            txt_propaname.Enabled = false;
            txt_propacity.Enabled = false;
            txt_propa_address.Enabled = false;
            txt_propaname.Text = txt_propacity.Text = txt_propa_address.Text = "";

        }
        else
        {
            ddl_firmtype.Enabled = false;
            txt_registrationno.Enabled = false;


        }
    }
    protected void rd_selfmill_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_selfmill.Checked)
        {

            txt_mill_owner_name.Enabled = false;
            txt_millowner_address.Enabled = false;

            txt_mill_owner_name.Text = txt_millowner_address.Text = "";

        }
        else
        {

            txt_mill_owner_name.Enabled = true;
            txt_millowner_address.Enabled = true;

            txt_agreement_end.Enabled = true;
        }



    }
    protected void rd_leezmill_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_leezmill.Checked)
        {
            txt_mill_owner_name.Enabled = true;
            txt_millowner_address.Enabled = true;
            txt_agreement_end.Enabled = true;
        }
    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/MainLogin.aspx");
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void rbMPState_CheckedChanged(object sender, EventArgs e)
    {
        ddlOtherStates.Visible = false;
        // lblOtherStates.Visible = false;
    }

    protected void rbOtherState_CheckedChanged(object sender, EventArgs e)
    {
        ddlOtherStates.Visible = true;
        // lblOtherStates.Visible = true;
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "MillerReg_Print.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    protected void txt_Changed_BRLQty_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txt_InsBRLQty.Text) >= Convert.ToDecimal(txt_Changed_BRLQty.Text))
        {
            if (txt_InsBRLQty.Text != "")
            {
                decimal TotalBRLQty = Convert.ToDecimal(txt_InsBRLQty.Text);
                decimal ChnagedBRLQty = Convert.ToDecimal(txt_Changed_BRLQty.Text);
                decimal remainBRLQty = TotalBRLQty - ChnagedBRLQty;
                txt_remQty.Text = Convert.ToString(remainBRLQty);

            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया निरिक्षण में पाई गयी कुल BRL चावल की मात्रा भरें|'); </script> ");
                return;

            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('  Upgraded/बदली गयी BRL चावल की मात्रा निरिक्षण में पाई गयी कुल BRL चावल की मात्रा से कम होगी||'); </script> ");
            txt_Changed_BRLQty.Text = "";
            return;
        }
    }
    protected void txt_millingofPreviousYear_CheckedChanged(object sender, EventArgs e)
    {
        if (txt_millingofPreviousYear.Checked == true)
        {
            trone.Visible = true;
            trtwo.Visible = true;
            trthree.Visible = true;


        }
        else 
        {
            trone.Visible = false;
            trtwo.Visible = false;
            trthree.Visible = false ;

        }
    }
    protected void txt_InsBRLQty_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txt_InsBRLQty.Text) < Convert.ToDecimal(txt_total_agreeQty.Text))
        {
            if (txt_Changed_BRLQty.Text != "")
            {
                decimal TotalBRLQty = Convert.ToDecimal(txt_InsBRLQty.Text);
                decimal ChnagedBRLQty = Convert.ToDecimal(txt_Changed_BRLQty.Text);
                decimal remainBRLQty = TotalBRLQty - ChnagedBRLQty;
                txt_remQty.Text = Convert.ToString(remainBRLQty);

            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Upgraded/बदली गयी BRL चावल की मात्रा भरें|'); </script> ");
                return;

            }
        }
        else 
        
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' निरिक्षण में पाई गयी कुल BRL चावल की मात्रा  समस्त जिलो में मिलर की कुल अनुबंध की मात्रा से कम होगी||'); </script> ");
            txt_InsBRLQty.Text = "";
            return;
        }
      
    }
}