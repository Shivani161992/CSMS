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

public partial class IssueCenter_PM_Inspection_ByOneMember : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    // protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";
    public string DistId;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ICID = Session["issue_id"].ToString();
            DistId = Session["dist_id"].ToString();
            ddlCropYear.Items.Insert(0, "--Select--");
            ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
            ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));

            GetDist();
            GetCropYearValues();
            GetMillerDistrict();
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

    public void GetMillerDistrict()
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
                        ddlmill_dist.DataSource = ds.Tables[0];
                        ddlmill_dist.DataTextField = "district_name";
                        ddlmill_dist.DataValueField = "district_code";
                        ddlmill_dist.DataBind();
                        ddlmill_dist.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public void GetMillName()
    {
        ddlMillname.Items.Clear();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "  select Registration_ID , Mill_Name from Miller_Registration_2017 where District_Code='" + ddlmill_dist.SelectedValue.ToString() + "' and Status='1'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillname.DataSource = ds.Tables[0];
                    ddlMillname.DataTextField = "Mill_Name";
                    ddlMillname.DataValueField = "Registration_ID";
                    ddlMillname.DataBind();
                    ddlMillname.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public void GetMillerDistrictone()
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
                        ddlmill_dist1.DataSource = ds.Tables[0];
                        ddlmill_dist1.DataTextField = "district_name";
                        ddlmill_dist1.DataValueField = "district_code";
                        ddlmill_dist1.DataBind();
                        ddlmill_dist1.Items.Insert(0, new ListItem("--Select--", "0"));
                        //ddlmill_dist1.Items.Insert(0, "--Select--");
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
          //ddlmill_dist1.Items.Insert(0, New ListItem("--Select Customer--", "0"));
    }

    public void GetMillNameone()
    {
        ddlMillname.Items.Clear();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "  select Registration_ID , Mill_Name from Miller_Registration_2017 where District_Code='" + ddlmill_dist1.SelectedValue.ToString() + "' and Status='1'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillname1.DataSource = ds.Tables[0];
                    ddlMillname1.DataTextField = "Mill_Name";
                    ddlMillname1.DataValueField = "Registration_ID";
                    ddlMillname1.DataBind();
                    ddlMillname1.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public void GetMillerDistricttwo()
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
                        ddlmill_dist2.DataSource = ds.Tables[0];
                        ddlmill_dist2.DataTextField = "district_name";
                        ddlmill_dist2.DataValueField = "district_code";
                        ddlmill_dist2.DataBind();
                        ddlmill_dist2.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public void GetMillNametwo()
    {
        ddlMillname.Items.Clear();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "  select Registration_ID , Mill_Name from Miller_Registration_2017 where District_Code='" + ddlmill_dist2.SelectedValue.ToString() + "' and Status='1'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillname2.DataSource = ds.Tables[0];
                    ddlMillname2.DataTextField = "Mill_Name";
                    ddlMillname2.DataValueField = "Registration_ID";
                    ddlMillname2.DataBind();
                    ddlMillname2.Items.Insert(0, new ListItem("--Select--", "0"));
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


    public void GetMillerDistrictthree()
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
                        ddlmill_dist3.DataSource = ds.Tables[0];
                        ddlmill_dist3.DataTextField = "district_name";
                        ddlmill_dist3.DataValueField = "district_code";
                        ddlmill_dist3.DataBind();
                        ddlmill_dist3.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public void GetMillNamethree()
    {
        ddlMillname.Items.Clear();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "  select Registration_ID , Mill_Name from Miller_Registration_2017 where District_Code='" + ddlmill_dist3.SelectedValue.ToString() + "' and Status='1'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillname3.DataSource = ds.Tables[0];
                    ddlMillname3.DataTextField = "Mill_Name";
                    ddlMillname3.DataValueField = "Registration_ID";
                    ddlMillname3.DataBind();
                    ddlMillname3.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public void GetMillerDistrictfour()
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
                        ddlmill_dist4.DataSource = ds.Tables[0];
                        ddlmill_dist4.DataTextField = "district_name";
                        ddlmill_dist4.DataValueField = "district_code";
                        ddlmill_dist4.DataBind();
                        ddlmill_dist4.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public void GetMillNamefour()
    {
        ddlMillname.Items.Clear();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "  select Registration_ID , Mill_Name from Miller_Registration_2017 where District_Code='" + ddlmill_dist4.SelectedValue.ToString() + "' and Status='1'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillname4.DataSource = ds.Tables[0];
                    ddlMillname4.DataTextField = "Mill_Name";
                    ddlMillname4.DataValueField = "Registration_ID";
                    ddlMillname4.DataBind();
                    ddlMillname4.Items.Insert(0, new ListItem("--Select--", "0"));
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
                        ddldist.DataSource = ds.Tables[0];
                        ddldist.DataTextField = "district_name";
                        ddldist.DataValueField = "district_code";
                        ddldist.DataBind();
                        ddldist.Items.Insert(0, "--Select--");
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



    private void GetMPIssueCentre()
    {
        string districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddldist.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIC.DataSource = ds.Tables[0];
                    ddlIC.DataTextField = "DepotName";
                    ddlIC.DataValueField = "DepotID";
                    ddlIC.DataBind();
                    ddlIC.Items.Insert(0, "--Select--");
                    //GetGodown();
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
    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlIC.SelectedValue.ToString() + "' order by Godown_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlgd.DataSource = ds.Tables[0];
                        ddlgd.DataTextField = "Godown_Name";
                        ddlgd.DataValueField = "Godown_ID";
                        ddlgd.DataBind();
                        ddlgd.Items.Insert(0, "--Select--");
                        // GetStack();
                        btnQuilityTested.Enabled = true;

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

    public void GetStack()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Stack_ID, Stack_Name from tbl_MetaData_STACK where Godown_ID='" + ddlgd.SelectedValue.ToString() + "' order by Stack_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSK.DataSource = ds.Tables[0];
                        ddlSK.DataTextField = "Stack_Name";
                        ddlSK.DataValueField = "Stack_ID";
                        ddlSK.DataBind();
                        ddlSK.Items.Insert(0, "--Select--");

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

    public void GetInspector()
    {

        //IC_Id = Session["issue_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Inspector_ID,Inspector_Name, Inspector_desig from Inspector_Master_02017 where  IssueCenter_code='" + ddlIC.SelectedValue.ToString() + "' and district='" + ddldist.SelectedValue.ToString() + "'  order by Inspector_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_Insp.DataSource = ds.Tables[0];
                        ddl_Insp.DataTextField = "Inspector_Name";
                        ddl_Insp.DataValueField = "Inspector_ID";
                        ddl_Insp.DataBind();
                        ddl_Insp.Items.Insert(0, "--Select--");
                       // txtdesig.Text = ds.Tables[0].Rows[0]["Inspector_desig"].ToString();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspector Name is Not available'); </script> ");
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

    



    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIC.SelectedIndex > 0)
        {
            GetGodown();
            GetInspector();
            
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया प्रदाय केंद्र चुनें|'); </script> ");
        }
    }

    protected void ddlCommo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCommo.SelectedValue.ToString() == "Kharif")
        {

           // GetTeamName();
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = false;
            panel5.Visible = false;
            trLOT.Visible = true;
            trMiller.Visible = true;
            trM_Miller.Visible = true;
        }
        else
        {
            panel2.Visible = false;
            panel3.Visible = false;
            //GetTeamName();
            panel4.Visible = true;
            panel5.Visible = true;
            buttqualityTestWheat.Enabled = true;
            trLOT.Visible = false;
            trMiller.Visible = false;
            trM_Miller.Visible = false;


        }




    }




    protected void ddlgd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgd.SelectedIndex > 0)
        {

            GetStack();

        }

    }
    protected void btnQuilityTested_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
            return;

        }
        else if (ddl_Insp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Inspector Name '); </script> ");
            return;
        }
        else if (txtbags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter No. of Bags '); </script> ");
            return;
        }

        else if (txtLotNo1.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Lot No. '); </script> ");
            return;
        }

        else if (chkmill.Checked == true)
        {
            if (ddlmill_dist1.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select First Miller District'); </script> ");
                return;
            }

            else if (ddlMillname1.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select First Miller Name'); </script> ");
                return;
            }

            else if (ddlmill_dist2.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Second Miller District'); </script> ");
                return;
            }

            else if (ddlMillname2.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Second Miller Name'); </script> ");
                return;
            }
        }
        else if (chkmill.Checked == false)
        {
            if (ddlmill_dist.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller District'); </script> ");
                return;
            }
            else if (ddlMillname.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
                return;
            }
        }

            if (float.Parse(LblTotaS.Text) >= float.Parse(TxtTotaS.Text) && float.Parse(LblChoteToteS.Text) >= float.Parse(TxtChoteToteS.Text) && float.Parse(LblVijatiyeS.Text) >= float.Parse(txtVijatiyeS.Text) && float.Parse(LblDamageDaaneS.Text) >= float.Parse(txtDamageDaaneS.Text) && float.Parse(LblBadrangDaaneS.Text) >= float.Parse(txtBadrangDaaneS.Text) && float.Parse(LblChaakiDaaneS.Text) >= float.Parse(txtChaakiDaaneS.Text) && float.Parse(LblLaalDaaneS.Text) >= float.Parse(txtLaalDaaneS.Text) && float.Parse(LblNamiS.Text) >= float.Parse(txtNamiS.Text))
            {
                // btnAccept.Enabled = true;
                //  btnReject.Enabled = true;
                btnPass.Enabled = true;
                btnfail.Enabled = false;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";
            }
            else
            {
                btnPass.Enabled = false;
                btnfail.Enabled = true;
                //btnReject.Enabled = true;
                // btnAccept.Enabled = false;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";
            }
        
    }



    protected void btnPass_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
            return;

        }
        else if (ddl_Insp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Inspector Name '); </script> ");
            return;
        }
        else if (txtbags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please No. of Bags '); </script> ");
            return;
        }

        else if (txtLotNo1.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Lot No. '); </script> ");
            return;
        }
        ConvertServerDate ServerDate = new ConvertServerDate();
        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_ByOnemember  ";
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
                gatepass = ds.Tables[0].Rows[0]["InspectionID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "12" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {
                string acceptance;
                string rejection = "0";

                acceptance = "AINS" + gatepass;
                Session["Inspection"] = gatepass;
                string status = "Pass";
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (chkmill.Checked == true)
                {
                    if (ddlmill_dist1.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select First Miller District'); </script> ");
                        return;
                    }

                    else if (ddlMillname1.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select First Miller Name'); </script> ");
                        return;
                    }

                    else if (ddlmill_dist2.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Second Miller District'); </script> ");
                        return;
                    }

                    else if (ddlMillname2.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Second Miller Name'); </script> ");
                        return;
                    }
                    string MorethanOneMiller = "Y";
                    int Miller_Count=0;
                   if(ddlMillname1.SelectedIndex >0)
                   {
                       Miller_Count = Miller_Count + 1;
                       if (ddlMillname2.SelectedIndex > 0)
                       {
                           Miller_Count = Miller_Count + 1;
                           if (ddlMillname3.SelectedIndex > 0)
                           {
                               Miller_Count = Miller_Count + 1;
                               if (ddlMillname4.SelectedIndex > 0)
                               {
                                   Miller_Count = Miller_Count + 1;
                               }
                           }
                       
                       }
                   }
                   
                    
                    string millname="0", milldist = "0";
                    string strselect = "insert into PM_Inspection_ByOnemember( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count, lot_No_seven, lot_No_eight) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','" + txtbags.Text + "','" + txtLotNo1.Text + "','" + txtLotNo2.Text + "','" + txtLotNo3.Text + "','" + txtLotNo4.Text + "','" + txtLotNo5.Text + "','" + txtLotNo6.Text + "','" + milldist + "','" + millname + "','" + ddlmill_dist1.SelectedValue.ToString() + "','" + ddlMillname1.SelectedValue.ToString() + "','" + ddlmill_dist2.SelectedValue.ToString() + "','" + ddlMillname2.SelectedValue.ToString() + "','" + ddlmill_dist3.SelectedValue.ToString() + "','" + ddlMillname3.SelectedValue.ToString() + "','" + ddlmill_dist4.SelectedValue.ToString() + "','" + ddlMillname4.SelectedValue.ToString() + "','" + MorethanOneMiller + "','" + Miller_Count + "','" + txtLotNo7.Text + "','" + txtLotNo8.Text + "')";
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();

                }
                else
                {
                    if (ddlmill_dist.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller District'); </script> ");
                        return;
                    }
                    else if (ddlMillname.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
                        return;
                    }
                    string MorethanOneMiller = "N";
                    string Miller_Count = "1";
                   
                    string millname_one="0", millname_two="0", millname_three="0", millname_four="0", milldist_one="0", milldist_two="0", milldist_three="0", milldist_four = "0";
                    string strselect = "insert into PM_Inspection_ByOnemember( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count , lot_No_seven, lot_No_eight) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','" + txtbags.Text + "','" + txtLotNo1.Text + "','" + txtLotNo2.Text + "','" + txtLotNo3.Text + "','" + txtLotNo4.Text + "','" + txtLotNo5.Text + "','" + txtLotNo6.Text + "','" + ddlmill_dist.SelectedValue.ToString() + "','" + ddlMillname.SelectedValue.ToString() + "','" + milldist_one + "','" + millname_one + "','" + milldist_two + "','" + millname_two + "','" + milldist_three + "','" + millname_three + "','" + milldist_four + "','" + millname_four + "','" + MorethanOneMiller + "','" + Miller_Count + "','" + txtLotNo7.Text + "','" + txtLotNo8.Text+ "')";
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();

                }
                //string strselect = "insert into PM_Inspection_ByOnemember( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','" + txtbags.Text + "','" + txtLotNo1.Text + "','" + txtLotNo2.Text + "','" + txtLotNo3.Text + "',,'" + txtLotNo4.Text + "','" + txtLotNo5.Text + "','" + txtLotNo6.Text + "','" + ddlmill_dist.SelectedValue.ToString() + "','" + ddlMillname.SelectedValue.ToString() + "','" + ddlmill_dist1.SelectedValue.ToString() + "','" + ddlMillname1.SelectedValue.ToString() + "','" + ddlmill_dist2.SelectedValue.ToString() + "','" + ddlMillname2.SelectedValue.ToString() + "','" + ddlmill_dist3.SelectedValue.ToString() + "','" + ddlMillname3.SelectedValue.ToString() + "','" + ddlmill_dist4.SelectedValue.ToString() + "','" + ddlMillname4.SelectedValue.ToString() + "')";
                //cmd = new SqlCommand(strselect, con);
                //string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                btnPass.Enabled = false;
                trlabel.Visible = true;
                Label2.Text = "Your CMR Acceptance Number Is : " + acceptance;
                Button1.Enabled = true;

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






    protected void btnfail_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
            return;

        }
        else if (ddl_Insp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Inspector Name '); </script> ");
            return;
        }
        else if (txtbags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please No. of Bags '); </script> ");
            return;
        }

        else if (txtLotNo1.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Lot No. '); </script> ");
            return;
        }
        ConvertServerDate ServerDate = new ConvertServerDate();
        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_ByOnemember where  LEN(InspectionID)<8 ";
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
                gatepass = ds.Tables[0].Rows[0]["InspectionID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "12" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {
                string acceptance = "0";
                string rejection;

                rejection = "RINS" + gatepass;
                Session["Inspection"] = gatepass;
                string status = "Reject";
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (chkmill.Checked == true)
                {
                    if (ddlmill_dist1.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select First Miller District'); </script> ");
                        return;
                    }

                    else if (ddlMillname1.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select First Miller Name'); </script> ");
                        return;
                    }

                    else if (ddlmill_dist2.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Second Miller District'); </script> ");
                        return;
                    }

                    else if (ddlMillname2.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Second Miller Name'); </script> ");
                        return;
                    }
                    string MorethanOneMiller = "Y";
                   int Miller_Count = 0;
                    if (ddlMillname1.SelectedIndex > 0)
                    {
                        Miller_Count = Miller_Count + 1;
                        if (ddlMillname2.SelectedIndex > 0)
                        {
                            Miller_Count = Miller_Count + 1;
                            if (ddlMillname3.SelectedIndex > 0)
                            {
                                Miller_Count = Miller_Count + 1;
                                if (ddlMillname4.SelectedIndex > 0)
                                {
                                    Miller_Count = Miller_Count + 1;
                                }
                            }

                        }
                    }
                   
                    string millname = "0", milldist = "0";
                    string strselect = "insert into PM_Inspection_ByOnemember( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO,  Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count, lot_No_seven, lot_No_eight) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','" + txtbags.Text + "','" + txtLotNo1.Text + "','" + txtLotNo2.Text + "','" + txtLotNo3.Text + "','" + txtLotNo4.Text + "','" + txtLotNo5.Text + "','" + txtLotNo6.Text + "','" + milldist + "','" + millname + "','" + ddlmill_dist1.SelectedValue.ToString() + "','" + ddlMillname1.SelectedValue.ToString() + "','" + ddlmill_dist2.SelectedValue.ToString() + "','" + ddlMillname2.SelectedValue.ToString() + "','" + ddlmill_dist3.SelectedValue.ToString() + "','" + ddlMillname3.SelectedValue.ToString() + "','" + ddlmill_dist4.SelectedValue.ToString() + "','" + ddlMillname4.SelectedValue.ToString() + "','" + MorethanOneMiller + "','" + Miller_Count + "','" + txtLotNo7.Text + "','" + txtLotNo8.Text + "')";
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();
                }
                else
                {
                    if (ddlmill_dist.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller District'); </script> ");
                        return;
                    }
                    else if (ddlMillname.SelectedIndex <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
                        return;
                    }
                    string MorethanOneMiller = "N";
                    string Miller_Count = "1";
                    string millname_one = "0", millname_two = "0", millname_three = "0", millname_four = "0", milldist_one = "0", milldist_two = "0", milldist_three = "0", milldist_four = "0";
                    string strselect = "insert into PM_Inspection_ByOnemember( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO,  Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count, lot_No_seven, lot_No_eight) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','" + txtbags.Text + "','" + txtLotNo1.Text + "','" + txtLotNo2.Text + "','" + txtLotNo3.Text + "','" + txtLotNo4.Text + "','" + txtLotNo5.Text + "','" + txtLotNo6.Text + "','" + ddlmill_dist.SelectedValue.ToString() + "','" + ddlMillname.SelectedValue.ToString() + "','" + milldist_one + "','" + millname_one + "','" + milldist_two + "','" + millname_two + "','" + milldist_three + "','" + millname_three + "','" + milldist_four + "','" + millname_four + "','" + MorethanOneMiller + "','" + Miller_Count + "','" + txtLotNo7.Text + "','" + txtLotNo8.Text + "')";
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();
                }
               // string strselect = "insert into PM_Inspection_ByOnemember( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO,  Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','" + txtbags.Text + "','" + txtLotNo1.Text + "','" + txtLotNo2.Text + "','" + txtLotNo3.Text + "',,'" + txtLotNo4.Text + "','" + txtLotNo5.Text + "','" + txtLotNo6.Text + "','" + ddlmill_dist.SelectedValue.ToString() + "','" + ddlMillname.SelectedValue.ToString() + "','" + ddlmill_dist1.SelectedValue.ToString() + "','" + ddlMillname1.SelectedValue.ToString() + "','" + ddlmill_dist2.SelectedValue.ToString() + "','" + ddlMillname2.SelectedValue.ToString() + "','" + ddlmill_dist3.SelectedValue.ToString() + "','" + ddlMillname3.SelectedValue.ToString() + "','" + ddlmill_dist4.SelectedValue.ToString() + "','" + ddlMillname4.SelectedValue.ToString() + "')";
                //cmd = new SqlCommand(strselect, con);
                //string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                btnfail.Enabled = false;
                trlabel.Visible = true;
                Label2.Text = "Your CMR Rejection Number Is : " + rejection;
                Button1.Enabled = true;
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
    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldist.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    
    protected void buttqualityTestWheat_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
            return;

        }
        else if (ddl_Insp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Inspector Name '); </script> ");
            return;
        }
        else if (txtbags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter No. of Bags '); </script> ");
            return;
        }
        
            string FMatters, OFGrains, DGrains, SDDGrains, SIGrains, WGrains, MContent = "";
            FMatters = "0.75";
            OFGrains = "2.0";
            DGrains = "2.0";
            SDDGrains = "4.0";
            SIGrains = "6.0";
            WGrains = "1.0";
            MContent = "12.0";
            if (float.Parse(FMatters) >= float.Parse(txtFM.Text) && float.Parse(OFGrains) >= float.Parse(txtOFG.Text) && float.Parse(DGrains) >= float.Parse(txtDG.Text) && float.Parse(SDDGrains) >= float.Parse(txtSDDG.Text) && float.Parse(SDDGrains) >= float.Parse(txtSDDG.Text) && float.Parse(SIGrains) >= float.Parse(txtSIG.Text) && float.Parse(WGrains) >= float.Parse(txtWGC.Text) && float.Parse(MContent) >= float.Parse(txtMC.Text))
            {
                buttpasswheat.Enabled = true;
                buttqualityTestWheat.Enabled = false;
                buttqualityTestWheat.Text = "Submitted";

            }
            else
            {
                buttfailwheat.Enabled = true;
                buttqualityTestWheat.Enabled = false;
                buttqualityTestWheat.Text = "Submitted";
            }
        

    }
    protected void buttnewWheat_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



    protected void buttpasswheat_Click(object sender, EventArgs e)
    {

        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
            return;

        }
        else if (ddl_Insp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Inspector Name '); </script> ");
            return;
        }
        else if (txtbags.Text == "")
        {
        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please No. of Bags '); </script> ");
            return;
        }

        ConvertServerDate ServerDate = new ConvertServerDate();
        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_ByOnemember_Wheat  ";
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
                gatepass = ds.Tables[0].Rows[0]["InspectionID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "13" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {

                string acceptance;
                string rejection = "0";

                acceptance = "AINS" + gatepass;
                string status = "Pass";
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into PM_Inspection_ByOnemember_Wheat( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, Foreign_Matters, Other_Food_Grain, Damaged_Grains, Slightly_Damaged_Discolo_Grains, Shrivilled_Immature_Grains, Weevilled_Grains, Moisture_Content, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + txtFM.Text + "','" + txtOFG.Text + "','" + txtDG.Text + "','" + txtSDDG.Text + "','" + txtSIG.Text + "','" + txtWGC.Text + "','" + txtMC.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','" + txtbags.Text + "')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                buttpasswheat.Enabled = false;
                trlabel.Visible = true;
                Label2.Text = "Your CMR Acceptance Number Is : " + acceptance;

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
    protected void buttfailwheat_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
            return;

        }
        else if (ddl_Insp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Inspector Name '); </script> ");
            return;
        }

        else if (txtbags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please No. of Bags '); </script> ");
            return;
        }
        ConvertServerDate ServerDate = new ConvertServerDate();
        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_ByOnemember_Wheat where  LEN(InspectionID)<8 ";
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
                gatepass = ds.Tables[0].Rows[0]["InspectionID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "13" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {
                string acceptance = "0";
                string rejection;

                rejection = "RINS" + gatepass;
                string status = "Fail";
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into PM_Inspection_ByOnemember_Wheat( InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, Foreign_Matters, Other_Food_Grain, Damaged_Grains, Slightly_Damaged_Discolo_Grains, Shrivilled_Immature_Grains, Weevilled_Grains, Moisture_Content, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddl_Insp.SelectedValue.ToString() + "','" + txtdesig.Text + "','" + txt_MobileNo.Text + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + txtFM.Text + "','" + txtOFG.Text + "','" + txtDG.Text + "','" + txtSDDG.Text + "','" + txtSIG.Text + "','" + txtWGC.Text + "','" + txtMC.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "','"+txtbags.Text+"')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                buttfailwheat.Enabled = false;
                trlabel.Visible = true;
                Label2.Text = "Your CMR Rejection Number Is : " + rejection;
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

    protected void btnMillname_Click(object sender, EventArgs e)
    {
        
    }
    
   
    protected void ddlmill_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
            if (ddlmill_dist.SelectedIndex > 0)
        {
            GetMillName();
        }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला चुने|'); </script> ");
            }
    }


    protected void chkmill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkmill.Checked == true)
        {
            trMillName.Visible = true;
            trMiller.Visible = false;
            GetMillerDistrictone();
            GetMillerDistricttwo();
            GetMillerDistrictthree();
            GetMillerDistrictfour();
        }
        else
        {
            trMillName.Visible = false;
            trMiller.Visible = true;
        }
    }
    protected void ddl_Insp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //IC_Id = Session["issue_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select  Inspector_desig from Inspector_Master_02017 where  Inspector_ID='" + ddl_Insp.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        
                        txtdesig.Text = ds.Tables[0].Rows[0]["Inspector_desig"].ToString();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select inspector name'); </script> ");
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
    protected void ddlmill_dist1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmill_dist1.SelectedIndex > 0)
        {
            GetMillNameone();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला चुने|'); </script> ");
        }
    }
    protected void ddlmill_dist2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmill_dist2.SelectedIndex > 0)
        {
            GetMillNametwo();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला चुने|'); </script> ");
        }
    }
    protected void ddlmill_dist3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmill_dist3.SelectedIndex > 0)
        {
            GetMillNamethree();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला चुने|'); </script> ");
        }
    }
    protected void ddlmill_dist4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmill_dist4.SelectedIndex > 0)
        {
            GetMillNamefour();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का जिला चुने|'); </script> ");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string url = "Print_Insp_ByOMenber.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}