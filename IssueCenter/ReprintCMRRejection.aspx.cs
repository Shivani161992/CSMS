using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_ReprintCMRRejection : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da_MPStorage;
    DataSet ds, ds_MPStorage;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public string ICID, DistId;
    public string DistCode, State, StateCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["Acpt/Reject"] == "Rejected")
                {
                    hdfPreviousPageAcptNumber.Value = Session["AcceptNumber"].ToString();
                    hdfMillCode.Value = Session["MillName"].ToString();
                    hdfAgrmtID.Value = Session["AgrmtId"].ToString();
                }

                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();

                GetCropYearValues();
                GetICName();
                GetDistName();
                GetDataCMRTesting();
                GetGodownName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
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

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name FROM pds.districtsmp where district_code='" + DistId + "' ");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "pds.districtsmp");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();

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

    public void GetICName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT DepotName FROM tbl_MetaData_DEPOT where DepotID='" + ICID + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "tbl_MetaData_DEPOT");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
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

    public void GetDataCMRTesting()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string AcptNo = hdfPreviousPageAcptNumber.Value;

                string select = string.Format("select * , Getdate() ServerDate, (select distinct(case when MM.Miller_Name IS NOT NULL then MM.Miller_Name when MR.Mill_Name IS NOT NULL then MR.Mill_Name else '' end) as MillName from CMR_QualityInspection PMCMRT left join Miller_Master MM on MM.Miller_ID = PMCMRT.Mill_Name left join Miller_Registration MR on MR.Registration_ID=PMCMRT.Mill_Name where MM.Miller_ID ='" + hdfMillCode.Value + "' or MR.Registration_ID='" + hdfMillCode.Value + "') as MillName From CMR_QualityInspection where Rejection_No='" + AcptNo + "' and District='" + Session["dist_id"].ToString() + "' and issueCentre_code='" + Session["issue_id"].ToString() + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "CMR_QualityInspection");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    hdfGodownNo.Value = ds.Tables[0].Rows[0]["Godown_Code"].ToString();

                    lblMillerName.Text = ds.Tables[0].Rows[0]["MillName"].ToString();
                    lblAcptNo.Text = ds.Tables[0].Rows[0]["Rejection_No"].ToString();

                    DateTime frmDate = DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
                    lblAcptDate.Text = frmDate.ToString("dd/MMM/yyyy");

                    lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblLotNo.Text = ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    lblDoNo.Text = ds.Tables[0].Rows[0]["DO_Number"].ToString();
                    lblTCNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    lblRejectCommon.Text = ds.Tables[0].Rows[0]["Reject_CommonRice"].ToString();
                    lblRejectGA.Text = ds.Tables[0].Rows[0]["Reject_GradeARice"].ToString();

                    //For GradeA & Common Rice Values
                    LblTotaGAR.Text = ds.Tables[0].Rows[0]["TotaGA"].ToString();
                    LblTotaSR.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    LblTotaGARmk.Text = ds.Tables[0].Rows[0]["TotaRemark"].ToString();
                    LblChoteToteGAR.Text = ds.Tables[0].Rows[0]["ChoteToteGA"].ToString();
                    LblChoteToteSR.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    LblChoteToteGARmk.Text = ds.Tables[0].Rows[0]["ChoteToteRemark"].ToString();
                    LblVijatiyeGAR.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    LblVijatiyeSR.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    LblVijatiyeGARmk.Text = ds.Tables[0].Rows[0]["VijatiyeRemark"].ToString();
                    LblDamageDaaneGAR.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    LblDamageDaaneSR.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    LblDamageDaaneGARmk.Text = ds.Tables[0].Rows[0]["DamageDaaneRemark"].ToString();
                    LblBadrangDaaneGAR.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    LblBadrangDaaneSR.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    LblBadrangDaaneGARmk.Text = ds.Tables[0].Rows[0]["BadrangDaaneRemark"].ToString();
                    LblChaakiDaaneGAR.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    LblChaakiDaaneSR.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    LblChaakiDaaneGARmk.Text = ds.Tables[0].Rows[0]["ChaakiDaaneRemark"].ToString();
                    LblLaalDaaneGAR.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    LblLaalDaaneSR.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    LblLaalDaaneGARmk.Text = ds.Tables[0].Rows[0]["LaalDaaneRemark"].ToString();
                    LblOtherGAR.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    LblOtherSR.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    LblOtherGARmk.Text = ds.Tables[0].Rows[0]["OtherRemark"].ToString();
                    LblChokarDaaneGAR.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    LblChokarDaaneSR.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    LblChokarDaaneGARmk.Text = ds.Tables[0].Rows[0]["ChokarDaaneRemark"].ToString();
                    LblNamiGAR.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
                    LblNamiSR.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
                    LblNamiGARmk.Text = ds.Tables[0].Rows[0]["NamiRemark"].ToString();

                    //For Milling Type
                    LblMType.Text = LblMType0.Text = LblMType1.Text = LblMType2.Text = LblMType3.Text = LblMType4.Text = LblMType5.Text = LblMType6.Text = LblMType7.Text = LblMType8.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();

                    string Daane = ds.Tables[0].Rows[0]["Daane"].ToString();
                    if (Daane == "Damage")
                    {
                        lblDaaneType.Text = "क्षतिग्रस्त दाने";
                    }
                    else
                    {
                        lblDaaneType.Text = "मामूली क्षतिग्रस्त दाने";
                    }

                    lblServerDateTime.Text = ds.Tables[0].Rows[0]["ServerDate"].ToString();


                    string selectData = string.Format("Select Mill_Addr_District,State,State_Code From PaddyMilling_Agreement where Mill_Name='" + hdfMillCode.Value + "' and Agreement_ID='" + hdfAgrmtID.Value + "'");
                    da = new SqlDataAdapter(selectData, con);

                    ds = new DataSet();
                    da.Fill(ds, "PaddyMilling_Agreement");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                         DistCode = ds.Tables[0].Rows[0]["Mill_Addr_District"].ToString();
                        State = ds.Tables[0].Rows[0]["State"].ToString();
                         StateCode = ds.Tables[0].Rows[0]["State_Code"].ToString();
                    }

                    if (State == "MP")
                    {
                        lblMillerState.Text = "MP";

                        string selectDistMP = string.Format("Select district_name From pds.districtsmp where district_code='" + DistCode + "'");
                        da = new SqlDataAdapter(selectDistMP, con);

                        ds = new DataSet();
                        da.Fill(ds, "pds.districtsmp");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblMillerDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        }
                    }
                    else
                    {
                        string selectState = string.Format("Select State_Name From State_Master where State_Code='" + StateCode + "'");
                        da = new SqlDataAdapter(selectState, con);

                        ds = new DataSet();
                        da.Fill(ds, "State_Master");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblMillerState.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                        }

                        string selectDistOther = string.Format("Select district_name From OtherState_DistrictCode where district_code='" + DistCode + "'");
                        da = new SqlDataAdapter(selectDistOther, con);

                        ds = new DataSet();
                        da.Fill(ds, "OtherState_DistrictCode");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblMillerDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
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

    public void GetGodownName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + hdfGodownNo.Value + "'");
                da_MPStorage = new SqlDataAdapter(select, con_MPStorage);

                ds_MPStorage = new DataSet();
                da_MPStorage.Fill(ds_MPStorage);
                if (ds_MPStorage != null)
                {
                    if (ds_MPStorage.Tables[0].Rows.Count > 0)
                    {
                        lblGodownNo.Text = ds_MPStorage.Tables[0].Rows[0]["Godown_Name"].ToString();
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