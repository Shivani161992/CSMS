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


public partial class IssueCenter_PM_RemainingCMR_EntryReceipt : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd, cmd1, cmd2, cmd3;
    SqlDataAdapter da, da_MPStorage, da1;
    DataSet ds, ds_MPStorage, ds1;

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

                rdbNewJute.Checked = rdbTagYes.Checked = true;

                txtTruckNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTruckNo0.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo0.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo0.Attributes.Add("onchange", "return chksqltxt(this)");

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
                GetBranch();

                GetBookNumber();
                rbCDaane.Checked = true;
                GetInspector();


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
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

                if (ddlSuppState.SelectedValue.ToString() == "23")
                {

                    string select = "";
                    select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Ddldist.DataSource = ds.Tables[0];
                            Ddldist.DataTextField = "district_name";
                            Ddldist.DataValueField = "district_code";
                            Ddldist.DataBind();
                            Ddldist.Items.Insert(0, "--Select--");
                            //Ddldist.SelectedValue = Session["dist_id"].ToString();
                            // GetMPIssueCentre();
                        }
                    }
                }

                else
                {

                    string qry = string.Format("SELECT district_code ,district_name FROM OtherState_DistrictCode where State_Id = '{0}'  order by district_name", ddlSuppState.SelectedValue.ToString());
                    da = new SqlDataAdapter(qry, con);
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