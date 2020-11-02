using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class IssueCenter_Print_Insp_ByOMenber : System.Web.UI.Page
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
                //BookNo = Session["Book_Number"].ToString();
                //ICID = Session["issue_id"].ToString();
                //DistId = Session["dist_id"].ToString();

                GetCropYearValues();
                // GetDataCMRTesting();
                // GetInspector();
                GetInspData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
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
                string select = "select IOM.CropYear, season, convert (varchar(10), D_O_Inspection , 103) as Inspection_Date, IM.Inspector_Name, Designation, IOM.mill_phone, D.district_name, IC.DepotName, G.Godown_Name, Stack_Name, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, IOM.Status, Acceptance_NO, Rejection_NO, Bags,lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six,  lot_No_seven, lot_No_eight, DM.district_name as Dist, MR.Mill_Name, DM1.district_name as DistOne, MR1.Mill_Name as MillOne, DM2.district_name as Disttwo, MR2.Mill_Name as Milltwo, DM3.district_name as Distthree, MR3.Mill_Name as Millthree ,DM4.district_name as Distfour , MR4.Mill_Name as MillFour, MoreThan_OneMiller  from PM_Inspection_ByOnemember as IOM inner join Inspector_Master_02017 as IM on Im.Inspector_ID=IOM.Inspector_Name and IM.IssueCenter_code=IOM.ICenter_ID inner join pds.districtsmp as D on D.district_code=IOM.District_ID inner join tbl_MetaData_DEPOT as IC on IC.DepotID=IOM.ICenter_ID inner join tbl_MetaData_GODOWN as G on G.Godown_ID=IOM.Godown_ID LEFT join pds.districtsmp as DM on DM.district_code=IOM.Miller_District LEFT join pds.districtsmp as DM1 on DM1.district_code=IOM.Miller_District_one LEFT join pds.districtsmp as DM2 on DM2.district_code=IOM.Miller_District_two LEFT join pds.districtsmp as DM3 on DM3.district_code=IOM.Miller_District_three LEFT join pds.districtsmp as DM4 on DM4 .district_code=IOM.Miller_District_Four LEFT join Miller_Registration_2017 as MR on MR.Registration_ID=IOM.Miller_Name LEFT join Miller_Registration_2017 as MR1 on MR1.Registration_ID=IOM.Miller_Name_one LEFT join Miller_Registration_2017 as MR2 on MR2.Registration_ID=IOM.Miller_Name_two LEFT join Miller_Registration_2017 as MR3 on MR3.Registration_ID=IOM.Miller_Name_three LEFT join Miller_Registration_2017 as MR4 on MR4.Registration_ID=IOM.Miller_Name_Four where IOM.InspectionID='" + Inspection + "' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblcommo.Text = ds.Tables[0].Rows[0]["season"].ToString();
                    lblAcptDate.Text = ds.Tables[0].Rows[0]["Inspection_Date"].ToString();
                    lblinspdate.Text = ds.Tables[0].Rows[0]["Inspection_Date"].ToString();
                    lblinspname.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                    lblinspdesg.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    // lblinspname.Text = ds.Tables[0].Rows[0]["mill_phone"].ToString();
                    lblInspDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lbldist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lbldistinspone.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    lblstack.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();
                    Label9.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    Label10.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    Label11.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    Label12.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    Label13.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    Label14.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    Label15.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    Label16.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
                    lblactrjtstatus.Text = ds.Tables[0].Rows[0]["Status"].ToString();
                    string Accept = ds.Tables[0].Rows[0]["Acceptance_NO"].ToString();
                    string Reject = ds.Tables[0].Rows[0]["Rejection_NO"].ToString();
                    if (Accept != "0")
                    {
                        lblAptRjt.Text = "Acceptance";
                        lblAcptNo.Text = ds.Tables[0].Rows[0]["Acceptance_NO"].ToString();
                    }
                    else
                    {
                        lblAptRjt.Text = "Rejection";
                        lblAcptNo.Text = ds.Tables[0].Rows[0]["Rejection_NO"].ToString();
                    }

                    lblbags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lbllotone.Text = ds.Tables[0].Rows[0]["lot_No_one"].ToString();
                    lbllottwo.Text = ds.Tables[0].Rows[0]["lot_No_two"].ToString();
                    lbllotthree.Text = ds.Tables[0].Rows[0]["lot_No_three"].ToString();
                    lbllotfour.Text = ds.Tables[0].Rows[0]["lot_No_four"].ToString();
                    lbllotfive.Text = ds.Tables[0].Rows[0]["lot_No_five"].ToString();
                    lbllotsix.Text = ds.Tables[0].Rows[0]["lot_No_six"].ToString();
                    lbllotseven.Text = ds.Tables[0].Rows[0]["lot_No_seven"].ToString();
                    lblloteight.Text = ds.Tables[0].Rows[0]["lot_No_eight"].ToString();
                    string Dist, Miller;
                    Dist = ds.Tables[0].Rows[0]["Dist"].ToString();
                    Miller = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    if ((Dist == "" && Miller == "") || (Dist == "0" && Miller == "0"))
                    {
                        //trmiller.Visible = true;
                        //trMoreThanOneMiller.Visible = false;
                        //lblmilldistrict.Text = ds.Tables[0].Rows[0]["Dist"].ToString();
                        //lblmillname.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();

                        trmiller.Visible = false;
                        trMoreThanOneMiller.Visible = true;
                        lbldistone.Text = ds.Tables[0].Rows[0]["DistOne"].ToString();
                        lblmillone.Text = ds.Tables[0].Rows[0]["MillOne"].ToString();
                        lbldisttwo.Text = ds.Tables[0].Rows[0]["Disttwo"].ToString();
                        lblmilltwo.Text = ds.Tables[0].Rows[0]["Milltwo"].ToString();
                        lbldistthree.Text = ds.Tables[0].Rows[0]["Distthree"].ToString();
                        lblmillthree.Text = ds.Tables[0].Rows[0]["Millthree"].ToString();
                        lbldistfour.Text = ds.Tables[0].Rows[0]["Distfour"].ToString();
                        lblmillfour.Text = ds.Tables[0].Rows[0]["MillFour"].ToString();
                    }
                    else
                    {
                        //trmiller.Visible = false;
                        //trMoreThanOneMiller.Visible = true;
                        //lbldistone.Text = ds.Tables[0].Rows[0]["DistOne"].ToString();
                        //lblmillone.Text = ds.Tables[0].Rows[0]["MillOne"].ToString();
                        //lbldisttwo.Text = ds.Tables[0].Rows[0]["Disttwo"].ToString();
                        //lblmilltwo.Text = ds.Tables[0].Rows[0][" Milltwo"].ToString();
                        //lbldistthree.Text = ds.Tables[0].Rows[0]["Distthree"].ToString();
                        //lblmillthree.Text = ds.Tables[0].Rows[0]["Millthree"].ToString();
                        //lbldistfour.Text = ds.Tables[0].Rows[0][" Distfour "].ToString();
                        //lblmillfour.Text = ds.Tables[0].Rows[0]["MillFour"].ToString();
                        trmiller.Visible = true;
                        trMoreThanOneMiller.Visible = false;
                        lblmilldistrict.Text = ds.Tables[0].Rows[0]["Dist"].ToString();
                        lblmillname.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    }

                    //lbldistone.Text = ds.Tables[0].Rows[0]["DistOne"].ToString();
                    //lblmillone.Text = ds.Tables[0].Rows[0]["MillOne"].ToString();
                    //lbldisttwo.Text = ds.Tables[0].Rows[0]["Disttwo"].ToString();
                    //lblmilltwo.Text = ds.Tables[0].Rows[0][" Milltwo"].ToString();
                    //lbldistthree.Text = ds.Tables[0].Rows[0]["Distthree"].ToString();
                    //lblmillthree.Text = ds.Tables[0].Rows[0]["Millthree"].ToString();
                    //lbldistfour.Text = ds.Tables[0].Rows[0][" Distfour "].ToString();
                    //lblmillfour.Text = ds.Tables[0].Rows[0]["MillFour"].ToString();

                    // lblinspname.Text = ds.Tables[0].Rows[0]["MoreThan_OneMiller"].ToString();
                    string QRGridDetails = "Dist=" + lblInspDist.Text + ", CropYear=" + lblYear.Text + ", Acpt/Reject_No=" + lblAcptNo.Text + ", Bags=" + lblbags.Text + ",insp=" + lblinspname.Text + ", Insp_Desg=" + lblinspdesg.Text + ", stack=" + lblstack.Text + "";
                    ImgQRCode.ImageUrl = ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;

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
                    Label2.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    // LblVijatiyeGA.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    Label3.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    // LblDamageDaaneGA.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    Label4.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    //  LblBadrangDaaneGA.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    Label5.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    // LblChaakiDaaneGA.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    Label6.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    // LblLaalDaaneGA.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    Label7.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    // LblOtherGA.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    //Label1.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    //LblChokarDaaneGA.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    //Label1.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    // LblNamiGA.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
                    Label8.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
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