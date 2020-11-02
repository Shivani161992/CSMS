using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;

public partial class PaddyMilling_CMR_Stacknumber_Update : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string IC_Id = "", Dist_Id = "", strBranchval = "", strCommodity = "", strGodownVal = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["Paddy_Challan"] = "";
                Session["MillCode"] = "";
                Session["Agreement_ID"] = "";

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();
                GetDistrictValues();
                GetCropYearValues();
                GetMillername();
                //getgodown();

                //rdbSBT.Checked = true;

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        // txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }
    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT  distinct CropYear FROM CMR_QualityInspection order by CropYear desc");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds, "CMR_QualityInspection");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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
    public void GetDistrictValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string select = string.Format("select distinct cq.District,ds.district_name from CMR_QualityInspection cq inner join pds.districtsmp ds on ds.district_code=cq.District where cq.District='" + Dist_Id + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    {
                        txtdistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
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
    public void GetMillername()
     {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string select = string.Format("select distinct mr.Mill_Name,cq.Mill_Name as cmrmillerid from CMR_QualityInspection  cq inner join pds.districtsmp ds on ds.district_code=cq.District inner join Miller_Registration_2017 mr on mr.Registration_ID=cq.Mill_Name where cq.District='" + Dist_Id + "' and cq.CropYear='" + txtYear.Text + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlmillername.DataSource = ds.Tables[0];
                        ddlmillername.DataTextField = "Mill_Name";
                        ddlmillername.DataValueField = "cmrmillerid";
                        ddlmillername.DataBind();
                        ddlmillername.Items.Insert(0, "--Select--");
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
    public void getagreement()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string select = string.Format("SELECT  distinct Agreement_ID FROM CMR_QualityInspection where Mill_Name='" + ddlmillername.SelectedValue.ToString() + "' and District='" + Dist_Id + "' and CropYear='2017-2018'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlagreement.DataSource = ds.Tables[0];
                        ddlagreement.DataTextField = "Agreement_ID";
                        ddlagreement.DataValueField = "Agreement_ID";
                        ddlagreement.DataBind();
                        ddlagreement.Items.Insert(0, "--Select--");
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
    public void getlotnumber()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string select = string.Format("SELECT  distinct LotNumber FROM CMR_QualityInspection where Mill_Name='" + ddlmillername.SelectedValue.ToString() + "' and District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "' and (StackNumber ='' OR StackNumber is null) and Submited='Y' and Agreement_ID='" + ddlagreement.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddllotnumber.DataSource = ds.Tables[0];
                        ddllotnumber.DataTextField = "LotNumber";
                        ddllotnumber.DataValueField = "LotNumber";
                        ddllotnumber.DataBind();
                        ddllotnumber.Items.Insert(0, "--Select--");
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
    public void getgodown()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string select = string.Format("select distinct cq.Godown_Code,mg.Godown_Name,Book_Number from CMR_QualityInspection cq inner join tbl_MetaData_GODOWN mg on mg.Godown_ID=cq.Godown_Code where District='" + Dist_Id + "' and cq.CropYear='" + txtYear.Text + "' and cq.Mill_Name='" + ddlmillername.SelectedValue.ToString() + "' and Agreement_ID='" + ddlagreement.SelectedValue.ToString() + "' and LotNumber='" + ddllotnumber.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtgodwn.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        lblgodownid.Text = ds.Tables[0].Rows[0]["Godown_Code"].ToString();
                        txtaccept.Text = ds.Tables[0].Rows[0]["Book_Number"].ToString();
                        getstackno();
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
    public void getstackno()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                Dist_Id = Session["dist_id"].ToString();
                string select = string.Format(" select distinct Stack_ID,Godown_ID,Stack_Killed,Stack_Name from tbl_MetaData_STACK where  Stack_Killed='N' and Godown_ID='" + lblgodownid.Text + "' and District_Id='23" + Dist_Id + "'");
                da = new SqlDataAdapter(select, con_MPStorage);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       
                        ddlstackno.DataSource = ds.Tables[0];
                        ddlstackno.DataTextField = "Stack_Name";
                        ddlstackno.DataValueField = "Stack_ID";
                        ddlstackno.DataBind();
                        ddlstackno.Items.Insert(0, "--Select--");
                        //lblstackname.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();
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
                    con_MPStorage.Open();
                }
            }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string strselect = " update CMR_QualityInspection set StackNumber='" + ddlstackno.SelectedValue.ToString() + "',StackName='" + ddlstackno.SelectedItem.Text+ "' where  Mill_Name='" + ddlmillername.SelectedValue.ToString() + "'  and District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "' and Godown_Code='" + lblgodownid.Text + "' and Submited='Y' and Agreement_ID='" + ddlagreement.SelectedValue.ToString() + "' and LotNumber='" + ddllotnumber.SelectedValue.ToString() + "'";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                btnupdate.Enabled = false;
                txtYear.Enabled = false;
                txtdistrict.Enabled = false;
                ddlmillername.Enabled = false;
                ddlagreement.Enabled = false;
                ddllotnumber.Enabled = false;
                txtgodwn.Enabled = false;
                ddlstackno.Enabled = false;
                Label2.Text = "Stack Number is Updated";
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
    }

    protected void ddlmillername_SelectedIndexChanged(object sender, EventArgs e)
    {

        getagreement();
        // getgodown();


    }
    protected void ddlagreement_SelectedIndexChanged(object sender, EventArgs e)
    {

        getlotnumber();
        // getgodown();
    }

    protected void ddllotnumber_SelectedIndexChanged(object sender, EventArgs e)
    {

        // getlotnumber();
        getgodown();
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/PaddyMillingHome.aspx");
    }
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}