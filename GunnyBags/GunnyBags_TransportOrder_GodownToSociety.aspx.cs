using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_GunnyBags_TransportOrder_GodownToSociety : System.Web.UI.Page
{
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;
    string transporter, RDist, RailHead, Rgodown = "";

    double QtyTotal = 0;
    public string sid = "";
    string OwnDist, OtherDist;

    string Godown = "";

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da2;
    DataSet ds, ds2;
    int RemainingQty;
    string TotalQty;
    string IC_Id = "", Dist_Id = "", strBranchval = "", strCommodity = "", strGodownVal = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {

                txtdistrict.Text = Session["dist_name"].ToString();
                string DistCode = Session["dist_id"].ToString();

                string fromdate = Request.Form[txtDateofReceipt.UniqueID];
                txtDateofReceipt.Text = fromdate;

                ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
                ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlCropYear.Items.Insert(0, "--Select--");


                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //DateTime _date;
                //string day = "";
                //_date = DateTime.Parse("5/MAY/2012");
                //day = _date.ToString("dd-mm-yyyy");
                // GetReceivingID();
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void ddlMSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex > 0)
        {
            if (ddlMSeason.SelectedIndex > 0)
            {
                GetGodown();
                fillSector();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Marketing Season'); </script> ");
                return;
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCropYear.SelectedIndex > 0)
        {
            // GetReceivingID();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CropYear'); </script> ");
            return;

        }
    }

    public void GetGodown()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistCode = Session["dist_id"].ToString();
                con.Open();

                string select = string.Format("select distinct FromGodown, Priority,  G.DepotId  from GunnyBags_GodownToSocietyMapping inner join tbl_MetaData_GODOWN as G on G.Godown_ID=FromGodown where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and RabiKharif='" + ddlMSeason.SelectedValue.ToString() + "' and FromGodownDistrict='" + DistCode + "' and  GETDATE() between FromDate and todate order by Priority  ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //txtFGodown.Text = ds.Tables[0].Rows[0]["FromGodown"].ToString();
                        //GetSocietyDistrict();
                        //Label2.Text = "Dispatch By Road";
                        //txtBagsType.Text = "Jute";


                        int count = ds.Tables[0].Rows.Count;
                        for (int i = 0; i < count; i++)
                        {

                            Godown = ds.Tables[0].Rows[i]["FromGodown"].ToString();
                            IC_Id = ds.Tables[0].Rows[i]["DepotId"].ToString();
                          

                                


                            DistCode = Session["dist_id"].ToString();
                            //IC_Id = Session["issue_id"].ToString();
                            //strBranchval = ddlBranch.SelectedValue.ToString();
                            strGodownVal = Godown;
                            strCommodity = "25"; //Gunny bags
                            string openingdate = "04/01/2016";

                            string pqry = "space_forcommodity_ingodown";

                            using (con = new SqlConnection(strcon))
                            {
                                try
                                {


                                    SqlCommand cmdpqty = new SqlCommand(pqry, con);
                                    cmdpqty.CommandType = CommandType.StoredProcedure;


                                    cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = DistCode;
                                    cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = IC_Id;
                                    cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = strGodownVal;
                                    cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = strCommodity;
                                    //cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
                                    cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;


                                    DataSet ds1 = new DataSet();
                                    SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

                                    dr.Fill(ds1);

                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        double stock = Math.Round(Convert.ToDouble(ds1.Tables[0].Rows[0]["Total"].ToString()), 5);

                                        double bags = Convert.ToDouble(ds1.Tables[0].Rows[0]["Totalbags"].ToString());

                                        if (bags==0)
                                        {
                                        
                                        }
                                        else if (bags != 0)
                                        {
                                            txtFGodown.Text = ds.Tables[0].Rows[i]["FromGodown"].ToString();
                                            GetGodownName();
                                            trbal.Visible = true;
                                            Label3.Text = "Balance available in Godown:-"+Convert.ToString(bags);
                                        return;
                                        }
                                        //txtBalQtyInGodown.Text = Convert.ToString(stock);
                                        //txtBalBagInGodown.Text = Convert.ToString(bags);

                                        //txtcurntcap.Text = Convert.ToString(stock);
                                        //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

                                    }
                                }

                                catch (Exception ex)
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                }

                                //finally
                                //{
                                //    if (con.State != ConnectionState.Closed)
                                //    {

                                //    }
                                //}


                            }
                        }
                    }
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Number is Not Available'); </script> ");
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
       // GetGodownName();

    }

    public void GetGodownName()
    {
        string DistCode = ddlSocDist.SelectedValue.ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                DistCode = Session["dist_id"].ToString();

                string select = string.Format("select G.Godown_Name , G.Godown_ID  from tbl_MetaData_GODOWN as G where G.Godown_ID='" + txtFGodown.Text + "' ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtFGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        hdfGodown.Value = ds.Tables[0].Rows[0]["Godown_ID"].ToString();
                      
                        Label2.Text = "Dispatch By Road";
                        txtBagsType.Text = "Jute";
                        GetSocietyDistrict();
                    }
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Godown Is not available'); </script> ");
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
    public void GetSocietyDistrict()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string dist = Session["dist_id"].ToString();

                string qry = "select distinct ToSocietyDistrict, D.district_name  from GunnyBags_GodownToSocietyMapping inner join pds.districtsmp as D on D.district_code= ToSocietyDistrict where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and RabiKharif='" + ddlMSeason.SelectedValue.ToString() + "' and FromGodown='" + hdfGodown.Value + "' and FromGodownDistrict='" + dist + "' order by district_name";
                SqlCommand cmd = new SqlCommand(qry, con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSocDist.DataSource = ds.Tables[0];
                    ddlSocDist.DataTextField = "district_name";
                    ddlSocDist.DataValueField = "ToSocietyDistrict";
                    ddlSocDist.DataBind();
                    ddlSocDist.Items.Insert(0, "Select");
                    //ddlDistrictSociety.SelectedValue = dist;

                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }

    public void GetSociety()
    {
        string DistCode = ddlSocDist.SelectedValue.ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                 DistCode = Session["dist_id"].ToString();

                 string select = string.Format("select distinct ToSociety, S.Society_Name_Eng +' '+ S.Society_Id as Society  from GunnyBags_GodownToSocietyMapping inner join Society2016 as S on S.Society_Id=ToSociety where GunnyBags_GodownToSocietyMapping.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and RabiKharif='" + ddlMSeason.SelectedValue.ToString() + "' and FromGodown='" + hdfGodown.Value + "' and FromGodownDistrict='" + DistCode + "' and ToSocietyDistrict='" + ddlSocDist.SelectedValue.ToString() + "' order by Society ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataTextField = "Society";
                        ddlSociety.DataValueField = "ToSociety";
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, "Select");
                    }
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Society Is not available'); </script> ");
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

    protected void ddlSocDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSociety();

    }


    public void fillSector()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string dist = Session["dist_id"].ToString();

                string qry = "SELECT  [DistrictId],[SectorId],[SectorName]+' '+[SectorId] SectorName   FROM [NewCSMS].[dbo].[District_SectorMaster] where DistrictId ='" + dist + "'";
                SqlCommand cmd = new SqlCommand(qry, con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSector.DataSource = ds.Tables[0];
                    ddlSector.DataTextField = "SectorName";
                    ddlSector.DataValueField = "SectorId";
                    ddlSector.DataBind();
                    ddlSector.Items.Insert(0, "Select");


                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTransporterName();
        bttSubmit.Enabled = true;

    }

    public void GetTransporterName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistCode = Session["dist_id"].ToString();
                string Sector = ddlSector.SelectedValue.ToString();
                con.Open();
                string select = string.Format(" select distinct Transporter_ID,Transporter_Name from Transporter_Table where Distt_ID ='" + DistCode + "' and SectorName = '" + Sector + "' and Transport_ID ='1' and IsActive='Y' and Valid_Upto>= GETDATE()  order by Transporter_Name ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {

                        ddlTransporterName.Items.Clear();

                        ddlTransporterName.DataSource = ds.Tables[0];
                        ddlTransporterName.DataTextField = "Transporter_Name";
                        ddlTransporterName.DataValueField = "Transporter_ID";
                        ddlTransporterName.DataBind();
                        ddlTransporterName.Items.Insert(0, "Select");
                    }
                    else if (ds.Tables[0].Rows.Count > 1)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('दिए गए सेक्टर के लिए एक से ज्यादा ट्रांसपोर्टर नही हो सकते| अतः ट्रांसपोर्टर की वैलिडिटी की जाँच करें|'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transporter Name is Not Available'); </script> ");
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

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex<0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
       
        else if (ddlMSeason.SelectedIndex<0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Marketing Season'); </script> ");
            return;
        }
        else if (txtFGodown.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sending Godown is not available '); </script> ");
            return;
        }
        else if (ddlSocDist.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Society district'); </script> ");
            return;
        }
        else if (ddlSociety.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Society '); </script> ");
            return;
        }

        else if (txtBagsType.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Bags Type is not available '); </script> ");
            return;
        }
        else if (txtDateofReceipt.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter end date of transportation'); </script> ");
            return;
        }
        else if (ddlSector.SelectedIndex<0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Sector '); </script> ");
            return;
        }
        else if (ddlTransporterName.SelectedIndex<0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select transporter Name '); </script> ");
            return;
        }
        else if (txtQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Quantity that is to be issued '); </script> ");
            txtQuantity.Focus();
            return;
        }
       
        else
        {
            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();

                    string DistCode = Session["dist_id"].ToString();
                    string qrey = "select max(TO_Soci_ID) as TO_Soci_ID from GunnyBags_TransportOrder_Society where LEN(TO_Soci_ID)<20 ";
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
                    gatepass = ds.Tables[0].Rows[0]["TO_Soci_ID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "17891800";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                    string BookNumber = "TO" + DistCode + gatepass;

                   
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    ConvertServerDate ServerDeliveryDate = new ConvertServerDate();
                    string IssuedDeliveryDate = ServerDeliveryDate.getDate_MDY(txtDateofReceipt.Text);

                    string strinsert = "insert into GunnyBags_TransportOrder_Society([TO_Soci_ID] ,[CropYear]       ,[SDist] ,[MarSea]      ,[SGodown],[Society_Dist]      ,[Society] ,[BagsType]      ,[End_DOT],[Sector]   ,[TransporterID],[ModeOfDispatch] ,[Issue_Qty],[CreatedDate]  ,[IP] ,BookNumber) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + DistCode + "','" + ddlMSeason.SelectedValue.ToString() + "','" + hdfGodown.Value + "','" + ddlSocDist.SelectedValue.ToString() + "','" + ddlSociety.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + IssuedDeliveryDate + "','" + ddlSector.SelectedValue.ToString() + "','" + ddlTransporterName.SelectedValue.ToString() + "','12','" + txtQuantity.Text + "',GetDate(),'" + ip + "','" + BookNumber + "')";
                    cmd = new SqlCommand(strinsert, con);
                    string check = (string)cmd.ExecuteScalar();

                    //if (rdbOwnDist.Checked == true)
                    //{
                    //    OwnDist = "Y";
                    //    OtherDist = "N";
                    //    string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlTO.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + txtTransporterName.Text + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + DistCode + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "')";
                    //    cmd = new SqlCommand(strinsert, con);
                    //    string check = (string)cmd.ExecuteScalar();
                    //}
                    //else if (rdbOtherDist.Checked == true)
                    //{
                    //    OwnDist = "N";
                    //    OtherDist = "Y";
                    //    string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlTO.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + txtTransporterName.Text + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + ddlDelidist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "')";
                    //    cmd = new SqlCommand(strinsert, con);
                    //    string check = (string)cmd.ExecuteScalar();
                    //}


                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                    bttSubmit.Enabled = false;
                    bttSubmit.Visible = false;
                    bttPrint.Visible = true;
                    bttPrint.Enabled = false;
                    ddlCropYear.Enabled = false;
                    ddlMSeason.Enabled = false;
                    ddlSector.Enabled = false;
                    ddlSocDist.Enabled = false;
                    ddlSociety.Enabled = false;
                    ddlTransporterName.Enabled = false;
                    txtDateofReceipt.Enabled = false;
                    txtQuantity.Enabled = false;


                   
                    //ddlDelidist.Enabled = false;
                    //ddlIC.Enabled = false;
                    //ddlBranch.Enabled = false;
                    //ddlGodown.Enabled = false;
                    trID.Visible = true;
                    Label1.Text = "Transport Order Number is:-" + BookNumber;


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
    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Gunny_Bags_Home.aspx");
    }
    protected void bttPrint_Click(object sender, EventArgs e)
    {

    }
}