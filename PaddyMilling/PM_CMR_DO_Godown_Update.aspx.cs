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

    string Branch;
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
            GetCMRDist();
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
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                       
                           
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
        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlAgtmtNumber.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    //Only For Agrmt Dist & Miller Dist.
                    //select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where (Mill_Addr_District='" + DistCode + "' Or District='" + DistCode + "') and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

                    //Agrmt Dist & Miller Dist & CMR Map. Dist
                    select = "Select CMR_DO from CMR_DepositOrder where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and Paddy_AgrmtDist='" + Ddldist.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + " ' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and IsAccepted='N' and IsRejected='N'   order by CMR_DO";

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCMR_DO_No.DataSource = ds.Tables[0];
                        ddlCMR_DO_No.DataTextField = "CMR_DO";
                        ddlCMR_DO_No.DataValueField = "CMR_DO";
                        ddlCMR_DO_No.DataBind();
                        ddlCMR_DO_No.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर का DO नो. उपलभ नहीं है|'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध क्रमांक चुनें|'); </script> ");
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

    protected void ddlCMR_DO_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select CMR_RecdDist, Lot_No, convert (varchar(10), CMR_DODate, 103) as CMR_DODate,D.district_name, IC.DepotName, Branch, G.Godown_Name, CMR_DepositOrder.Godown_id  from  CMR_DepositOrder inner join pds.districtsmp as D on D.district_code=CMR_DepositOrder.CMR_RecdDist inner join tbl_MetaData_DEPOT as IC on IC.DepotID=CMR_DepositOrder.IssueCenter inner join tbl_MetaData_GODOWN as G on G.Godown_ID=CMR_DepositOrder.Godown_id where  CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and Paddy_AgrmtDist='" + Ddldist.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + " ' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and CMR_DO='" + ddlCMR_DO_No.SelectedValue.ToString() + "' and IsAccepted='N' and IsRejected='N'   order by CMR_DO");

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "CMR_DepositOrder");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txt_LotNo.Text = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    txt_CMR_Date.Text = ds.Tables[0].Rows[0]["CMR_DODate"].ToString();
                    txtdist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    txtIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    //Session["District"] = ds.Tables[0].Rows[0]["CMR_RecdDist"].ToString();
                    Branch = ds.Tables[0].Rows[0]["Branch"].ToString();
                    txtGD.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    GetCMRBranch();
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
   

    
   
    protected void Ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
      ddlMillName.Items.Clear();
      if (Ddldist.SelectedIndex > 0)
      {
          GetMillerName();
      }
      else
      {
          Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया है|'); </script> ");
      }
    }
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        if (ddl_CMRDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select District'); </script> ");
            return;
        }
        else if (ddl_IC.SelectedIndex <= 0)
         {
             Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Issue Center'); </script> ");
             return;
         }
        else if (ddlBranch.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Branch'); </script> ");
            return;
        }
        else if (ddlGodam.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Godown'); </script> ");
            return;
        }

        else
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();




                    string select = "update CMR_DepositOrder set Updated='Y', CMR_RecdDist='" + ddl_CMRDist.SelectedValue.ToString() + "', IssueCenter='" + ddl_IC.SelectedValue.ToString() + "', Branch='" + ddlBranch.SelectedValue.ToString() + "', Godown_id='" + ddlGodam.SelectedValue.ToString() + "' where CMR_DO='" + ddlCMR_DO_No.SelectedValue.ToString() + "'";

                    cmd = new SqlCommand(select, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                       
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully'); </script> ");
                        btnRecptSubmit.Enabled = false;

                        
                    }
                }
                catch (Exception ex)
                {
                    btnRecptSubmit.Enabled = false;
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

    public void GetCMRDist()
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
                        ddl_CMRDist.DataSource = ds.Tables[0];
                        ddl_CMRDist.DataTextField = "district_name";
                        ddl_CMRDist.DataValueField = "district_code";
                        ddl_CMRDist.DataBind();
                        ddl_CMRDist.Items.Insert(0, "--Select--");
                        //ddl_CMRDist.SelectedValue = Session["dist_id"].ToString();
                        //GetMPIssueCentre();
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

    protected void ddl_CMRDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_CMRDist.SelectedIndex > 0)
        {
            GetCMRIC();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
        }
    }

    public void GetCMRIC()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddl_CMRDist.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddl_IC.DataSource = ds.Tables[0];
                    ddl_IC.DataTextField = "DepotName";
                    ddl_IC.DataValueField = "DepotID";
                    ddl_IC.DataBind();
                    ddl_IC.Items.Insert(0, "--Select--");
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



    protected void ddl_IC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_IC.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select IssueCenter|'); </script> ");
        }
    }

    public void GetBranch()
    {
//string districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", ddl_IC.SelectedValue.ToString());
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
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddl_CMRDist.SelectedValue.ToString()+ "' order by DepotName");
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
    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodam.Items.Clear();

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
        if (ddl_CMRDist.SelectedValue.ToString() == Ddldist.SelectedValue.ToString())
        {

            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    string select = "select Godown_Name, DMG.Godown_id ,distance from Distance_Master_Godown as DMG inner join Godown_Distance_Master as GDM on GDM.district=DMG.DistrictId inner join tbl_MetaData_GODOWN as G on G.DistrictId=DMG.DistrictId and G.Godown_ID=DMG.Godown_id where cast(DMG.distance as float)< cast (GDM.Max_distance as float) and PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "'  and DMG.IssueCenter='" + ddl_IC.SelectedValue.ToString() + "' and Distance_For='11' order by Godown_Name";
                    da = new SqlDataAdapter(select, strcon);

                    ds = new DataSet();
                    da.Fill(ds);
                    // string qry = "select distance from Godown_Distance_Master where district='" + Ddldist.SelectedValue.ToString() + "'";
                    // da = new SqlDataAdapter(select, con_MPStorage);

                    // ds = new DataSet();
                    //  da.Fill(ds);
                    // if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //  hdfReturnAgrmtCMR_Percent.Value = ds.Tables[0].Rows[0]["ReturnAgrmtCMR_Percent"].ToString();
                    // }
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlGodam.DataSource = ds.Tables[0];
                            ddlGodam.DataTextField = "Godown_Name";
                            ddlGodam.DataValueField = "Godown_id";
                            ddlGodam.DataBind();
                            ddlGodam.Items.Insert(0, "--Select--");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Godown  is not available'); </script> ");
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
        else
           
        {
            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name");
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
    }


    protected void ddlGodam_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
        hdfDistance.Value = "0";

        if (ddlGodam.SelectedIndex > 0)
        {
            GetDistance();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें|'); </script> ");
        }
    }

    public void GetDistance()
    {
       // string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select distance From Distance_Master_Godown where DistrictId='" + ddl_CMRDist.SelectedValue.ToString() + "' and PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "' and Godown_id='" + ddlGodam.SelectedValue.ToString() + "' and Distance_For='11'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnRecptSubmit.Enabled = true;
                }
                else
                {
                    hdfDistance.Value = "1";
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल से गोदाम का Distance भरें| Distance भरने के बाद ही आप इस गोदाम के लिए CMR Deposit Order जारी कर सकते हो|'); </script> ");
                    return;
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

    public void GetCMRBranch()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where tbl_MetaData_DEPOT.BranchID='" + Branch + "' ");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtbranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddl_CMRDist.SelectedValue.ToString() + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtbranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
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

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/PaddyMillingHome.aspx");
    }
}