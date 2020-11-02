using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_PrintStackNumber : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da_MPStorage;
    DataSet ds, ds_MPStorage;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public string ICID, DistId, Acceptnum;
    public string DistCode, State, StateCode, BookNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                //BookNo = Session["Book_Number"].ToString();
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                //lblinspectionid.Text = Session["InspectionID"].ToString();
                GetCropYearValues();
                GetData();
                FillGrid();
                //GetInspector();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void FillGrid()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string Inspection = Session["Inspection"].ToString();
                //string select = " select Inspector_Name from Inspector_Master_02017 where  IssueCenter_code='" + ICID + "'";
                string select = "select insp.Inspector_Name, Mr.Mill_Name, Agreement_ID, LotNumber, PaddyDO, CMRDO, CMR_Acceptance_No, Accept_CommonRice, firstBags, Bt.BagType from tblStackrejection as Sr inner join Miller_Registration_2017 as MR on MR.Registration_ID=sr.millname inner join FIN_Bag_Type as Bt on Bt.Bag_Type_ID=sr.BagType inner join Inspector_Master_02017 as Insp on Insp.Inspector_ID=sr.ICInspName where BookNumber='" + Inspection + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
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
    public void GetData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string Inspection = Session["Inspection"].ToString();
                //string select = " select Inspector_Name from Inspector_Master_02017 where  IssueCenter_code='" + ICID + "'";
                string select = "select convert (varchar(10), sr.Created_Date, 103) as Created_Date, sr.Status, CropYear, c.Commodity_Name, d.district_name, IC.DepotName, G.Godown_Name, Stack_Name,insp.Inspector_Name, insp.Inspector_desig, insp.MobileNum, sr.OtherMobileNum, CONVERT(varchar(10), D_O_Inspection, 103)as D_O_Inspection, Bags, sr.TotaS, sr.ChoteToteS, sr.VijatiyeS, sr.DamageDaaneS, sr.BadrangDaaneS, Sr.ChaakiDaaneS, sr.LaalDaaneS, sr.NamiS from tblStackrejection as Sr inner join tbl_MetaData_STORAGE_COMMODITY AS c ON c.Commodity_Id=Sr.Commodity INNER JOIN PDS.districtsmp AS d ON d.district_code=SR.District_ID INNER JOIN tbl_MetaData_DEPOT as IC on IC.DepotID=sr.ICenter_ID inner join tbl_MetaData_GODOWN as G on G.Godown_ID=sr.Godown_ID inner join Inspector_Master_02017 as insp on insp.Inspector_ID=sr.Inspector_Name where BookNumber='" + Inspection + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string status = ds.Tables[0].Rows[0]["Status"].ToString();
                    if (status == "Accepted")
                    {
                        lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        lblInspDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        lblcommo.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                        lbldist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        lbldistinspone.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        lblIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                        lblgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        lblstack.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();

                        lblAcptDate.Text = ds.Tables[0].Rows[0]["Created_Date"].ToString();
                        lblinspdate.Text = ds.Tables[0].Rows[0]["D_O_Inspection"].ToString();
                       
                        
                        Label9.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                        Label10.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                        Label11.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                        Label12.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                        Label13.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                        Label14.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                        Label15.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                        Label16.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();


                      
                      
                        
                        lblinspname.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                        lblinspdesg.Text = ds.Tables[0].Rows[0]["Inspector_desig"].ToString();

                      
                        lblbag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();


                        string accept = Inspection;
                        Label17.Text = "स्वीकार";
                       
                        lblrejection.Visible = false;
                        lblAcptNo.Visible = true;
                        lblAcptNo.Text = "Stack Acceptance Is : " + accept;
                    }
                    else
                    {
                        lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        lblInspDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        lblcommo.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                        lbldist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        lbldistinspone.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        lblIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                        lblgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        lblstack.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();

                        lblAcptDate.Text = ds.Tables[0].Rows[0]["D_O_Inspection"].ToString();
                        lblinspdate.Text = ds.Tables[0].Rows[0]["D_O_Inspection"].ToString();


                        Label9.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                        Label10.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                        Label11.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                        Label12.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                        Label13.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                        Label14.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                        Label15.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                        Label16.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();





                        lblinspname.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                        lblinspdesg.Text = ds.Tables[0].Rows[0]["Inspector_desig"].ToString();


                        lblbag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                        string reject = Inspection;
                        Label17.Text = "रिजेक्ट";
                        lblrejection.Visible = false;
                        lblAcptNo.Visible = true;
                        lblrejection.Text = "Stack Rejection Is : " + reject;

                      
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
                    //LblOtherS.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    //LblChokarDaaneGA.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    //LblChokarDaaneS.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
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