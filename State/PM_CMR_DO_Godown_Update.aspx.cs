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

public partial class State_PM_CMR_DO_Godown_Update : System.Web.UI.Page
{

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    //string Gdistance = "";
    //string Mdistance = "";


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCropYear.Items.Insert(0, "--Select--");
            ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
            ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));

            ViewState["User_Name"] = Session["st_Name"].ToString();

            //GETLotNo();
            //GETCMRPercentNo();
            GetDist();
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
                        Ddldist.DataSource = ds.Tables[0];
                        Ddldist.DataTextField = "district_name";
                        Ddldist.DataValueField = "district_code";
                        Ddldist.DataBind();
                        Ddldist.Items.Insert(0, "--Select--");
                        Ddldist.SelectedValue = Session["dist_id"].ToString();
                        GetMillerName();
                           
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

    public void GetMillerName()
    {
        ddlMillName.Items.Clear();

       // string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join CMRDO_OtherDistMap As CDO ON(CDO.CropYear=PM.CropYear and CDO.District=PM.District and  CDO.Agreement_ID=PM.Agreement_ID) Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + Ddldist.SelectedValue.ToString() + "' or PM.Mill_Addr_District='" + Ddldist.SelectedValue.ToString() + "' OR CDO.CMRDistrict='" + Ddldist.SelectedValue.ToString() + "') and PM.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' and (PM.DeliverdToFCI='N' OR PM.DeliverdToFCI IS NULL) order by MillName Asc";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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
       // ddlIC.Enabled = false;
       // ddlIC.SelectedIndex = 0;

       // lbllot.Text = lbllot2.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = hdfAllQty.Value = "";
        btnRecptSubmit.Enabled = false;
       // string DistCode = Session["dist_id"].ToString();
        ddlAgtmtNumber.Items.Clear();
        //ddlLotNO.Items.Clear();
        //ddlBranch.Items.Clear();
        //ddlGodam.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    //Only For Agrmt Dist & Miller Dist.
                    //select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where (Mill_Addr_District='" + DistCode + "' Or District='" + DistCode + "') and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

                    //Agrmt Dist & Miller Dist & CMR Map. Dist
                    select = "Select distinct PM.Agreement_ID From PaddyMilling_Agreement_2017 AS PM Left Join CMRDO_OtherDistMap As CDO ON(CDO.CropYear=PM.CropYear and CDO.District=PM.District and  CDO.Agreement_ID=PM.Agreement_ID) where (PM.Mill_Addr_District='" + Ddldist.SelectedValue.ToString() + "' Or PM.District='" + Ddldist.SelectedValue.ToString() + "' OR CDO.CMRDistrict='" + Ddldist.SelectedValue.ToString() + "') and PM.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and PM.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' and (PM.DeliverdToFCI='N' OR PM.DeliverdToFCI IS NULL)  order by PM.Agreement_ID";

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgtmtNumber.DataSource = ds.Tables[0];
                        ddlAgtmtNumber.DataTextField = "Agreement_ID";
                        ddlAgtmtNumber.DataValueField = "Agreement_ID";
                        ddlAgtmtNumber.DataBind();
                        ddlAgtmtNumber.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जिस मिल का चुनाव किया है, वह मिल आपके जिले की नहीं है इसलिए आप इस मिल के लिए CMR DEPOSIT ORDER नहीं बना सकते|'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
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

    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddlIC.Enabled = false;
       // ddlIC.SelectedIndex = 0;

       // lbllot.Text = lbllot2.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = "";
        btnRecptSubmit.Enabled = false;
        //ddlLotNO.Items.Clear();
       // ddlBranch.Items.Clear();
       // ddlGodam.Items.Clear();

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            Get_CMRDO();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void Get_CMRDO()
    { 
    
    }

    protected void ddlCMR_DO_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        //get DO Data

    }
}