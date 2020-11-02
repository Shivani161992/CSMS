using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;



public partial class IssueCenter_Print_InspectionOne : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da_MPStorage;
    DataSet ds, ds_MPStorage;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public string ICID, DistId;
    public string DistCode, State, StateCode, BookNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
              
                BookNo = Session["Book_Number"].ToString();
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();

                GetCropYearValues();
                // GetDataCMRTesting();
                GetInspector();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    public void GetInspector()
    {
        ICID = Session["issue_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();


                //string select = " select Inspector_Name from Inspector_Master_02017 where  IssueCenter_code='" + ICID + "'";
                string select = " select Inspector_Name from CMR_QualityInspection as QI left join Inspector_Master_02017 as IM on IM.IssueCenter_code=QI.issueCentre_code and IM.district=QI.District and IM.Inspector_ID=QI.Inspector_ID where  IssueCenter_code='" + ICID + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblinspname.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();


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

    public void GetInspData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string Inspection = Session["Inspection"].ToString();
                //string select = " select Inspector_Name from Inspector_Master_02017 where  IssueCenter_code='" + ICID + "'";
                string select = "select IOM.CropYear, season, convert (varchar(10), D_O_Inspection , 103) as Inspection_Date, IM.Inspector_Name, Designation, IOM.mill_phone, D.district_name, IC.DepotName, G.Godown_Name, Stack_Name, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, IOM.Status, Acceptance_NO, Rejection_NO, Bags,lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six,  lot_No_seven, lot_No_eight, DM.district_name, MR.Mill_Name, DM1.district_name as DistOne, MR1.Mill_Name as MillOne, DM2.district_name as Disttwo, MR2.Mill_Name as Milltwo, DM3.district_name as Distthree, MR3.Mill_Name as Millthree ,DM4.district_name as Distfour , MR4.Mill_Name as MillFour, MoreThan_OneMiller  from PM_Inspection_ByOnemember as IOM inner join Inspector_Master_02017 as IM on Im.Inspector_ID=IOM.Inspector_Name and IM.IssueCenter_code=IOM.ICenter_ID inner join pds.districtsmp as D on D.district_code=IOM.District_ID inner join tbl_MetaData_DEPOT as IC on IC.DepotID=IOM.ICenter_ID inner join tbl_MetaData_GODOWN as G on G.Godown_ID=IOM.Godown_ID LEFT join pds.districtsmp as DM on DM.district_code=IOM.Miller_District LEFT join pds.districtsmp as DM1 on DM1.district_code=IOM.Miller_District_one LEFT join pds.districtsmp as DM2 on DM2.district_code=IOM.Miller_Name_two LEFT join pds.districtsmp as DM3 on DM3.district_code=IOM.Miller_Name_three LEFT join pds.districtsmp as DM4 on DM4 .district_code=IOM.Miller_District_Four LEFT join Miller_Registration_2017 as MR on MR.Registration_ID=IOM.Miller_Name LEFT join Miller_Registration_2017 as MR1 on MR1.Registration_ID=IOM.Miller_Name_one LEFT join Miller_Registration_2017 as MR2 on MR2.Registration_ID=IOM.Miller_Name_two LEFT join Miller_Registration_2017 as MR3 on MR3.Registration_ID=IOM.Miller_Name_three LEFT join Miller_Registration_2017 as MR4 on MR4.Registration_ID=IOM.Miller_District_Four where IOM.InspectionID='" + Inspection + "' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["season"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Inspection_Date"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["mill_phone"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Status"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Acceptance_NO"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Rejection_NO"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_one"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_two"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_three"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_four"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_five"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_six"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_seven"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["lot_No_eight"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["DistOne"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["MillOne"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Disttwo"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0][" Milltwo"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Distthree"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Millthree"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0][" Distfour "].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["MillFour"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["MoreThan_OneMiller"].ToString();


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
                  
                    //lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    // LblTotaGA.Text = ds.Tables[0].Rows[0]["TotaGA"].ToString();
                    Label1.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    //LblChoteToteGA.Text = ds.Tables[0].Rows[0]["ChoteToteGA"].ToString();
                    LblChoteToteS.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    // LblVijatiyeGA.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    LblVijatiyeS.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    // LblDamageDaaneGA.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    LblDamageDaaneS.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    //  LblBadrangDaaneGA.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    LblBadrangDaaneS.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    // LblChaakiDaaneGA.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    LblChaakiDaaneS.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    // LblLaalDaaneGA.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    LblLaalDaaneS.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    // LblOtherGA.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    LblOtherS.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    //LblChokarDaaneGA.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    LblChokarDaaneS.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    // LblNamiGA.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
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
}