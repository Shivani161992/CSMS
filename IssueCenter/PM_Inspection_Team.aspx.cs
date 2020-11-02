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

public partial class IssueCenter_PM_Inspection_Team : System.Web.UI.Page


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
    public string  DistId;

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
            
        }
        string fromdate = Request.Form[txtDate.UniqueID];
        txtDate.Text = fromdate;
    }

    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "  select TeamName, IM.Inspector_Name, DM.Department_Name from Inspector_Master_Team as IMT inner join PM_Ins_GroupName_Master as GM on GM.TeamID=IMT.Team_ID inner join PM_InsName_Team_Mas as IM on IM.inspector_ID=IMT.Inspector_ID inner join PM_Depart_Master as DM on DM.Depart_ID=IMT.Depart_ID where IMT.Team_ID='" + ddlTS.SelectedValue.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
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
                string select = "select Stack_ID, Stack_Name from tbl_MetaData_STACK where Godown_ID='"+ ddlgd.SelectedValue.ToString()+"' order by Stack_Name";
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

    public void GetTeamName()
    {
        ddlTS.Items.Clear();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select distinct GM.TeamName,IM.Team_ID from Inspector_Master_Team as IM inner join PM_Ins_GroupName_Master as GM on GM.TeamID=IM.Team_ID where IM.Season='" + ddlCommo.SelectedValue.ToString() + "' order by TeamName";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTS.DataSource = ds.Tables[0];
                        ddlTS.DataTextField = "TeamName";
                        ddlTS.DataValueField = "Team_ID";
                        ddlTS.DataBind();
                        ddlTS.Items.Insert(0, "--Select--");
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
    
   
    
    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIC.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया प्रदाय केंद्र चुनें|'); </script> ");
        }
    }
    
    protected void ddlCommo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCommo.SelectedValue.ToString()=="Kharif")
        {
        
        GetTeamName();
            panel2.Visible=true;
            panel3.Visible=true;
            panel4.Visible = false;
            panel5.Visible = false;
        }
        else
        {
            panel2.Visible = false;
            panel3.Visible = false;
        GetTeamName();
            panel4.Visible=true;
            panel5.Visible=true;
            buttqualityTestWheat.Enabled = true;

        
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
            
            if (float.Parse(LblTotaS.Text) >= float.Parse(TxtTotaS.Text) && float.Parse(LblChoteToteS.Text) >= float.Parse(TxtChoteToteS.Text) && float.Parse(LblVijatiyeS.Text) >= float.Parse(txtVijatiyeS.Text) && float.Parse(LblDamageDaaneS.Text) >= float.Parse(txtDamageDaaneS.Text) && float.Parse(LblBadrangDaaneS.Text) >= float.Parse(txtBadrangDaaneS.Text) && float.Parse(LblChaakiDaaneS.Text) >= float.Parse(txtChaakiDaaneS.Text) && float.Parse(LblLaalDaaneS.Text) >= float.Parse(txtLaalDaaneS.Text)  && float.Parse(LblNamiS.Text) >= float.Parse(txtNamiS.Text) )
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
     else if(txtDate.Text=="")
     {
         Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
         return;
     
     }
     else if (ddlTS.SelectedIndex<=0)
     {
         Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Team '); </script> ");
         return;
     }

     ConvertServerDate ServerDate = new ConvertServerDate();
    string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

    using (con = new SqlConnection(strcon))
        try
        {
            con.Open();
            string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_Team  ";
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
            string status="Pass";
            con.Open();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string strselect = "insert into PM_Inspection_Team( InspectionID, CropYear, season, D_O_Inspection, TeamID, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddlTS.SelectedValue.ToString() + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "')";
            cmd = new SqlCommand(strselect, con);
            string check = (string)cmd.ExecuteScalar();

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
            btnPass.Enabled = false;

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
        else if (ddlTS.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Team '); </script> ");
            return;
        }
        ConvertServerDate ServerDate = new ConvertServerDate();
        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_Team where  LEN(InspectionID)<8 ";
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
                    gatepass =  "12" + "01";
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
                string status = "Reject";
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into PM_Inspection_Team( InspectionID, CropYear, season, D_O_Inspection, TeamID, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddlTS.SelectedValue.ToString() + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + TxtTotaS.Text + "','" + TxtChoteToteS.Text + "','" + txtVijatiyeS.Text + "','" + txtDamageDaaneS.Text + "','" + txtBadrangDaaneS.Text + "','" + txtChaakiDaaneS.Text + "','" + txtLaalDaaneS.Text + "','" + txtNamiS.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                btnfail.Enabled = false;
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
    protected void ddlTS_SelectedIndexChanged(object sender, EventArgs e)
    {
        row1.Visible = true;
        Panel1.Visible = true;
        Fillgrid();
    }
    protected void buttqualityTestWheat_Click(object sender, EventArgs e)
    {
        string FMatters, OFGrains, DGrains, SDDGrains, SIGrains, WGrains, MContent = "";
        FMatters="0.75";
        OFGrains="2.0";
        DGrains="2.0";
        SDDGrains="4.0";
        SIGrains="6.0";
        WGrains="1.0";
        MContent="12.0";
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
     else if(txtDate.Text=="")
     {
         Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Date of Inspection'); </script> ");
         return;
     
     }
     else if (ddlTS.SelectedIndex<=0)
     {
         Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Team '); </script> ");
         return;
     }

     ConvertServerDate ServerDate = new ConvertServerDate();
    string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

    using (con = new SqlConnection(strcon))
        try
        {
            con.Open();
            string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_Team_Wheat  ";
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
            string strselect = "insert into PM_Inspection_Team_Wheat( InspectionID, CropYear, season, D_O_Inspection, TeamID, District_ID, ICenter_ID, Godown_ID, Stack_ID, Foreign_Matters, Other_Food_Grain, Damaged_Grains, Slightly_Damaged_Discolo_Grains, Shrivilled_Immature_Grains, Weevilled_Grains, Moisture_Content, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddlTS.SelectedValue.ToString() + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + txtFM.Text + "','" + txtOFG.Text + "','" + txtDG.Text + "','" + txtSDDG.Text + "','" + txtSIG.Text + "','" + txtWGC.Text + "','" + txtMC.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "')";
            cmd = new SqlCommand(strselect, con);
            string check = (string)cmd.ExecuteScalar();

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
            buttpasswheat.Enabled = false;

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
        else if (ddlTS.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Team '); </script> ");
            return;
        }
        ConvertServerDate ServerDate = new ConvertServerDate();
        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(InspectionID) as InspectionID  from PM_Inspection_Team_Wheat where  LEN(InspectionID)<8 ";
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
                string rejection ;

                rejection = "RINS" + gatepass;
                string status = "Fail";
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into PM_Inspection_Team_Wheat( InspectionID, CropYear, season, D_O_Inspection, TeamID, District_ID, ICenter_ID, Godown_ID, Stack_ID, Foreign_Matters, Other_Food_Grain, Damaged_Grains, Slightly_Damaged_Discolo_Grains, Shrivilled_Immature_Grains, Weevilled_Grains, Moisture_Content, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlCommo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddlTS.SelectedValue.ToString() + "','" + ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlgd.SelectedValue.ToString() + "','" + ddlSK.SelectedValue.ToString() + "','" + txtFM.Text + "','" + txtOFG.Text + "','" + txtDG.Text + "','" + txtSDDG.Text + "','" + txtSIG.Text + "','" + txtWGC.Text + "','" + txtMC.Text + "','" + status + "','" + ip + "',getdate(),'" + ddlSK.SelectedItem.ToString() + "','" + acceptance + "','" + rejection + "')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                buttfailwheat.Enabled = false;
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
}
