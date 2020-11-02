using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_Gunny_Bags_AllocationTransportOrder : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;


    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string New;
    string CutTorn;
    string Old;
    string Crop_Year;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["fdjfhxncdfh"] = null;
                ViewState["Row"] = "0";

                string DistCode = Session["dist_id"].ToString();
                FillIndent();
                GetCropYear();
                GetDist();
                fillSector();
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
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

    public void GetDist()
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

                string qry = "SELECT district_code ,district_name FROM pds.districtsmp  order by district_name";
                SqlCommand cmd = new SqlCommand(qry, con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataSource = ds.Tables[0];
                    ddlDistrict.DataTextField = "district_name";
                    ddlDistrict.DataValueField = "district_code";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, "Select");
                 //   ddlDistrict.SelectedValue = dist;

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
    public void FillIndent()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT distinct IndentNumber, IndentorName+' ('+IndentNumber+')' as Indent  FROM Gunny_Bags_Indent_Creation ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlIndent.DataSource = ds.Tables[0];
                        ddlIndent.DataTextField = "Indent";
                        ddlIndent.DataValueField = "IndentNumber";
                        ddlIndent.DataBind();
                        ddlIndent.Items.Insert(0, "Select");
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

    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTransporterName();
    }
    protected void ddlIndent_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRailHead();
        LoadGridData();
        FillRack();
        GetTransporterName();
    }

    public void FillRack()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string indent = ddlIndent.SelectedValue.ToString();
                string railHead = ddlRailHead.SelectedValue.ToString();
                string cropYear = ddlCropYear.SelectedValue.ToString();

                con.Open();
                
                string select = "";
                select = "select  distinct GBRR.RR_Receive_ID , GBRR.RR_No from GunnyBags_Receiving_RR_Dist  GBRR where GBRR.Indent_Number ='"+indent+"' and GBRR.railhead ='"+railHead+"' and GBRR.CropYear ='"+cropYear+"'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRRRID.DataSource = ds.Tables[0];
                        ddlRRRID.DataTextField = "RR_No";
                        ddlRRRID.DataValueField = "RR_Receive_ID";
                        ddlRRRID.DataBind();
                        ddlRRRID.Items.Insert(0, "Select");
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
    public void GetRailHead()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string districtID = Session["dist_id"].ToString();
                string select = "";
                select = "select rh.district_code,rh.RailHead_Name,GBIC.RailHead_Destination from Gunny_Bags_Indent_Creation GBIC inner join tbl_Rail_Head RH on gbic.RailHead_Destination = RH.RailHead_Code and GBIC.District = RH.district_code  where GBIC.IndentNumber ='"+ddlIndent.SelectedValue.ToString()+"' and GBIC.District = '"+districtID+"'  order by RailHead_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRailHead.DataSource = ds.Tables[0];
                        ddlRailHead.DataTextField = "RailHead_Name";
                        ddlRailHead.DataValueField = "RailHead_Destination";
                        ddlRailHead.DataBind();
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

    protected void ddlGodown_selected(object sender, EventArgs e)
    {
        string indent = ddlIndent.SelectedValue.ToString();
        string railHead = ddlRailHead.SelectedValue.ToString();
        string districtID = ddlDistrict.SelectedValue.ToString();
        string godownID = ddlGodown.SelectedValue.ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select GBIA.quantityRailHead, GBIA.godownQuantity,GBIA.godownQuantityPercentage from GunnyBags_IndentToGodownAllocation GBIA where indentNumber='" + indent + "' and railHead ='" + railHead + "' and districtID ='" + districtID + "' and godownId ='" + godownID + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                double godownQuantityPercentage = 0;

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtQuantity.Text = ds.Tables[0].Rows[0]["godownQuantity"].ToString();
                        godownQuantityPercentage = Convert.ToDouble(ds.Tables[0].Rows[0]["godownQuantityPercentage"]);
                    }
                }

                ds.Clear();
                //Fill Transporter
                select = "select GBIAT.transporter from GunnyBaggs_AllocationTransporter GBIAT where GBIAT.indent='" + indent + "' and GBIAT.railhead='" + railHead + "' and gbiat.GodownDistrict='" + districtID + "' and GBIAT.godown ='" + godownID + "' and GBIAT.cropyear='" + ddlCropYear.SelectedValue.ToString() + "' and GBIAT.bagtype ='" + ddlBagsType.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
              
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ddlTransporterName.Items.Count == 0)
                        {
                            ddlTransporterName.SelectedValue = ds.Tables[0].Rows[0]["transporter"].ToString();
                            DataSet ds1 = new DataSet();
                            select = "select Transporter_Name from Transporter_Table where Transporter_ID ='" + ds.Tables[0].Rows[0]["transporter"].ToString() + "'";
                            da = new SqlDataAdapter(select, con);
                            da.Fill(ds1);

                            ddlTransporterName.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Transporter_Name"].ToString(), ds.Tables[0].Rows[0]["transporter"].ToString()));
                        }
                        else
                        {
                            ddlTransporterName.SelectedValue = ds.Tables[0].Rows[0]["transporter"].ToString();
                        }
                    }
                }

                ds.Clear();
                //Fill Quantity
                string cropYear = ddlCropYear.SelectedValue.ToString();

                select = "select  distinct GBRR.RR_Receive_ID , GBRR.RR_No, GBRR.Total_Received_QTY from GunnyBags_Receiving_RR_Dist  GBRR where GBRR.Indent_Number ='" + indent + "' and GBRR.railhead ='" + railHead + "' and GBRR.CropYear ='" + cropYear + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                double Total_Received_QTY = 0;
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Total_Received_QTY = Convert.ToDouble(ds.Tables[0].Rows[0]["Total_Received_QTY"]);

                        txtQuantity.Text = ((Total_Received_QTY * godownQuantityPercentage)/100).ToString("0.00");
                    }
                }

                //Fill detials if already exist (tranportaion details)               
                ds.Clear();
                //Fill Quantity
                string DistCode = Session["dist_id"].ToString();
                //string indent = ddlIndent.SelectedValue.ToString();
                //string railHead = ddlRailHead.SelectedValue.ToString();
                //string GodowndistrictID = ddlDistrict.SelectedValue.ToString();
                //string godownID = ddlGodown.SelectedValue.ToString();
                string RRID = ddlRRRID.SelectedValue.ToString();
                string CropYear = ddlCropYear.SelectedValue.ToString();
                string MarketingSeasonID = ddlMSeason.SelectedValue.ToString();
                string BagsTypeID = ddlBagsType.SelectedValue.ToString();
                string TransportedQuantity = txtQuantity.Text;
                string SectorID = ddlSector.SelectedValue.ToString();
                string TransporterID = ddlTransporterName.SelectedValue.ToString();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();


                select = "select Sector,Transporter,TransportedQuantity from GunnyBaggs_AllocationTransporter  where Indent='" + indent + "' and RailHead ='" + railHead + "' and RRID ='" + RRID + "' and GodownDistrict ='" + districtID + "' and Godown ='" + godownID + "' and MarketingSeason='" + MarketingSeasonID + "' and BagType ='" + BagsTypeID + "' and CropYear ='" + CropYear + "'   ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtQuantity.Text = ds.Tables[0].Rows[0]["TransportedQuantity"].ToString();
                        ddlSector.SelectedValue = ds.Tables[0].Rows[0]["Sector"].ToString();
                        ddlTransporterName.SelectedValue = ds.Tables[0].Rows[0]["Transporter"].ToString();
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select god.Godown_Name+' ('+god.Godown_ID+')'  as Godown , god.Godown_ID  from tbl_MetaData_GODOWN god  where god.DistrictId ='" + ddlDistrict.SelectedValue.ToString() + "' and (god.Remarks <> 'N' or god.Remarks is null)  order by god.Godown_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "Select");
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
  

    private void LoadGridData()
    {
        using (con = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand("select GBIA.indentNumber,GBIA.railHead,GBIA.districtID ,dist.district_name, GBIA.godownId,god.Godown_Name+' ('+GBIA.godownId + ')' Godown_Name ,GBIA.godownQuantity,GBIA.quantityRailHead,GBIA.godownQuantityPercentage from GunnyBags_IndentToGodownAllocation GBIA inner join tbl_MetaData_GODOWN god on GBIA.godownId = god.Godown_ID inner join pds.districtsmp dist on GBIA.districtID = dist.district_code where GBIA.indentNumber = '" + ddlIndent.SelectedValue.ToString() + "' and GBIA.railHead='" + ddlRailHead.SelectedValue.ToString() + "'", con))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                Panel1.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
    }
    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (ddlIndent.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Indent'); </script> ");
            return;
        }
        else if (ddlRailHead.Items.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('There is no Rail Head for selected indent'); </script> ");
            return;
        }
        else if (ddlRRRID.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Rail Rack No.'); </script> ");
            return;
        }
        else if (ddlMSeason.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Marketing Season'); </script> ");
            return;
        }
        else if (ddlBagsType.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select bags type'); </script> ");
            return;
        }

        else if (ddlDistrict.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select district'); </script> ");
            return;
        }
        else if (ddlGodown.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select godown'); </script> ");
            return;
        }
        else if (txtQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Quantity'); </script> ");
            return;
        }
        else if (ddlSector.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select sector'); </script> ");
            return;
        }
        else if (ddlTransporterName.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select transporter'); </script> ");
            return;
        }
        else 
        {
            string DistCode = Session["dist_id"].ToString();
            string indent = ddlIndent.SelectedValue.ToString();
            string railHead = ddlRailHead.SelectedValue.ToString();
            string GodowndistrictID = ddlDistrict.SelectedValue.ToString();
            string godownID = ddlGodown.SelectedValue.ToString();
            string RRID = ddlRRRID.SelectedValue.ToString();
            string CropYear = ddlCropYear.SelectedValue.ToString();
            string MarketingSeasonID = ddlMSeason.SelectedValue.ToString();
            string BagsTypeID = ddlBagsType.SelectedValue.ToString();
            string TransportedQuantity = txtQuantity.Text;
            string SectorID = ddlSector.SelectedValue.ToString();
            string TransporterID = ddlTransporterName.SelectedValue.ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string qrey = "select max(TransportID) as TransportID from GunnyBaggs_AllocationTransporter ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);

                    DataRow dr = ds.Tables[0].Rows[0];
                    //gatepass = dr["Inspector_ID"].ToString();
                    gatepass = ds.Tables[0].Rows[0]["TransportID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "11" + DistCode + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                    ds.Clear();
                    qrey = "select Sector,Transporter,TransportedQuantity from GunnyBaggs_AllocationTransporter  where Indent='"+indent+"' and RailHead ='"+railHead+"' and RRID ='"+RRID+"' and GodownDistrict ='"+GodowndistrictID+"' and Godown ='"+godownID+"' and MarketingSeason='"+MarketingSeasonID+"' and BagType ='"+BagsTypeID+"' and CropYear ='"+CropYear+"'   ";
                    da = new SqlDataAdapter(qrey, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //update record
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Don't have a rights to update record.'); </script> ");
                            return;
                        }
                        else
                        { 
                         //new transportation

                            string strinsert = "insert into GunnyBaggs_AllocationTransporter ([TransportID],[Indent],[RailHead],[RRID],[GodownDistrict],[Godown],[TransportedQuantity],[MarketingSeason],[BagType],[Sector],[Transporter],[CropYear],[CreatedDate],[UpdatedDate],[UserID],[IP], District) values ('" + gatepass + "','" + indent + "','" + railHead + "','" + RRID + "','" + GodowndistrictID + "','" + godownID + "','" + TransportedQuantity + "','" + MarketingSeasonID + "','" + BagsTypeID + "','" + SectorID + "','" + TransporterID + "','" + CropYear + "','" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + DistCode + "','" + ip + "','" + DistCode + "')";
                            cmd = new SqlCommand(strinsert, con);
                            cmd.ExecuteNonQuery();
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record Saved Succesfully'); </script> ");
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