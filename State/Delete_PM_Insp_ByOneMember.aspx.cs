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

public partial class State_Delete_PM_Insp_ByOneMember : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
     public string sid = "";
    public string DistId, ICID;
    string accept, Reject;
    
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["st_Name"] != null)
            {
                GetDist();
                //flag1 = "0";
                //flag2="0";
            }
            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
    }

    public void GetDist()
    {
        ddlissueC.Items.Clear();
        ddlgd.Items.Clear();
        ddlSK.Items.Clear();
        ddlacpt.Items.Clear();

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
                        ddlDist.DataSource = ds.Tables[0];
                        ddlDist.DataTextField = "district_name";
                        ddlDist.DataValueField = "district_code";
                        ddlDist.DataBind();
                        ddlDist.Items.Insert(0, "--Select--");
                       // ddlDist.SelectedValue = Session["dist_id"].ToString();
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

    public void GetMPIssueCentre()
    {
        ddlgd.Items.Clear();
        ddlSK.Items.Clear();
        ddlacpt.Items.Clear();
        //string districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDist.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlissueC.DataSource = ds.Tables[0];
                    ddlissueC.DataTextField = "DepotName";
                    ddlissueC.DataValueField = "DepotID";
                    ddlissueC.DataBind();
                    ddlissueC.Items.Insert(0, "--Select--");
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
        ddlSK.Items.Clear();
        ddlacpt.Items.Clear();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlissueC.SelectedValue.ToString() + "' order by Godown_Name";
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
                        //btnQuilityTested.Enabled = true;

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown name'); </script> ");
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
        ddlacpt.Items.Clear();
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
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Please select Stack No.'); </script> ");
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


    public void GetAcceptance()
    {
        //string Dist_Id = Session["dist_id"].ToString();
        //IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select Acceptance_NO, Rejection_NO from PM_Inspection_ByOnemember as PM where Pm.District_ID='" + ddlDist.SelectedValue.ToString() + "' and ICenter_ID='" + ddlissueC.SelectedValue.ToString() + "' and Godown_ID='" + ddlgd.SelectedValue.ToString() + "' and Stack_ID='" + ddlSK.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string acceptance, rejection;
                    acceptance = ds.Tables[0].Rows[0]["Acceptance_NO"].ToString();
                    rejection = ds.Tables[0].Rows[0]["Rejection_NO"].ToString();
                    if ( acceptance == "0")
                    {
                        tractrjt.Visible = true;
                        lblacprjt.Text = "Rejection No.";
                        ddlacpt.DataSource = ds.Tables[0];
                        ddlacpt.DataTextField = "Rejection_NO";
                        ddlacpt.DataValueField = "Rejection_NO";
                        ddlacpt.DataBind();
                        ddlacpt.Items.Insert(0, "--Select--");
                       // Reject = ddlacpt.SelectedValue.ToString();

                    }
                    else
                    {
                        tractrjt.Visible = true;
                        lblacprjt.Text = "Acceptance No.";
                        ddlacpt.DataSource = ds.Tables[0];
                        ddlacpt.DataTextField = "Acceptance_NO";
                        ddlacpt.DataValueField = "Acceptance_NO";
                        ddlacpt.DataBind();
                        ddlacpt.Items.Insert(0, "--Select--");
                       // accept = ddlacpt.SelectedValue.ToString();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance No. is not available'); </script> ");
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


    //protected void rdbAccept_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbAccept.Checked == true)
    //    {
    //        traccept.Visible = true;
    //        GetAcceptance();
            
    //    }
    //    else
    //    {

    //    }

    //}
    //public void GetRejection()
    //{
    //   // string Dist_Id = Session["dist_id"].ToString();
    //    //IC_Id = Session["issue_id"].ToString();

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string select = "select Rejection_NO from PM_Inspection_ByOnemember as PM where Pm.District_ID='' and ICenter_ID='' and Godown_ID=''";
    //            da = new SqlDataAdapter(select, con);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddlrej.DataSource = ds.Tables[0];
    //                ddlrej.DataTextField = "Rejection_NO";
    //                ddlrej.DataValueField = "Rejection_NO";
    //                ddlrej.DataBind();
    //                ddlrej.Items.Insert(0, "--Select--");
    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rejection No. is not available.'); </script> ");
    //                return;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}
    //protected void rdbReject_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbReject.Checked == true)
    //    {
    //        trReject.Visible = true;
    //        GetRejection();
    //    }
    //    else
    //    {

    //    }
    //}


   
    public void GetAccept()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select DM.district_name, IC.DepotName, G.Godown_Name, Stack_Name, PO.CropYear, IM.Inspector_Name, Designation,Bags, convert(varchar(10), D_O_Inspection, 103) as Date_of_inspection, TotaS, ChoteToteS, VijatiyeS, BadrangDaaneS, NamiS, DamageDaaneS,ChaakiDaaneS, LaalDaaneS, lot_No_eight, lot_No_five, lot_No_four, lot_No_one, lot_No_seven, lot_No_six, lot_No_two, lot_No_three,D.district_name as Dist, MR.Mill_Name as mill, D1.district_name as dist1, MR1.Mill_Name as mill1, D2.district_name as dist2, MR2.Mill_Name as mill2, D3.district_name as dist3, MR3.Mill_Name as mill3, D4.district_name as dist4, MR4.Mill_Name as mill4 from PM_Inspection_ByOnemember as PO inner join pds.districtsmp as DM on DM.district_code=PO.District_ID inner join tbl_MetaData_DEPOT as IC on IC.DepotID=PO.ICenter_ID inner join tbl_MetaData_GODOWN as G on G.Godown_ID=PO.Godown_ID inner join Inspector_Master_02017 as IM on IM.Inspector_ID=PO.Inspector_Name and IM.Inspector_desig=PO.Designation and IM.district=PO.District_ID and IM.IssueCenter_code=PO.ICenter_ID left join pds.districtsmp as D on D.district_code=PO.Miller_District left join Miller_Registration_2017 as MR on MR.Registration_ID=PO.Miller_Name and MR.District_Code=PO.Miller_District and MR.CropYear=Po.CropYear left join pds.districtsmp as D1 on D1.district_code=PO.Miller_District_one left join Miller_Registration_2017 as MR1 on MR1.Registration_ID=PO.Miller_Name_one and MR1.District_Code=PO.Miller_District_one and MR1.CropYear=Po.CropYear left join pds.districtsmp as D2 on D2.district_code=PO.Miller_District_two left join Miller_Registration_2017 as MR2 on MR2.Registration_ID=PO.Miller_Name_two and MR2.District_Code=PO.Miller_District_two and MR2.CropYear=Po.CropYear left join pds.districtsmp as D3 on D3.district_code=PO.Miller_District_three left join Miller_Registration_2017 as MR3 on MR3.Registration_ID=PO.Miller_Name_three and MR3.District_Code=PO.Miller_District_three and MR3.CropYear=Po.CropYear left join pds.districtsmp as D4 on D4.district_code=PO.Miller_District_Four left join Miller_Registration_2017 as MR4 on MR4.Registration_ID=PO.Miller_Name_Four and MR4.District_Code=PO.Miller_District_Four and MR4.CropYear=Po.CropYear where Acceptance_NO='" + ddlacpt.SelectedValue.ToString() + "' ");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PM_Inspection_ByOnemember");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtInspector.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                    txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    txtDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    txtIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    txtgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    txtstack.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();
                    txtDOI.Text = ds.Tables[0].Rows[0]["Date_of_inspection"].ToString();
                    txtBags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    string miller, district;
                    miller = ds.Tables[0].Rows[0]["mill"].ToString();
                    district = ds.Tables[0].Rows[0]["Dist"].ToString();
                    if (((miller == "") && (district == "")) || ((miller == "0") && (district == "0")))
                    {
                        tronemiller.Visible = false;
                        trmultiplemiller.Visible = true;

                        txtmillnameone.Text = ds.Tables[0].Rows[0]["mill1"].ToString();
                        txtmilldistone.Text = ds.Tables[0].Rows[0]["dist1"].ToString();
                        txtmillnametwo.Text = ds.Tables[0].Rows[0]["mill2"].ToString();
                        txtmilldisttwo.Text = ds.Tables[0].Rows[0]["dist2"].ToString();
                        txtmillnamethree.Text = ds.Tables[0].Rows[0]["mill3"].ToString();
                        txtmilldistthree.Text = ds.Tables[0].Rows[0]["dist3"].ToString();
                        txtmillnamefour.Text = ds.Tables[0].Rows[0]["mill4"].ToString();
                        txtmilldistfour.Text = ds.Tables[0].Rows[0]["dist4"].ToString();

                    }
                    else
                    {
                        

                    tronemiller.Visible = true;
                    trmultiplemiller.Visible = false;
                    txtmillname.Text = ds.Tables[0].Rows[0]["mill"].ToString();
                    txtmillDist.Text = ds.Tables[0].Rows[0]["Dist"].ToString();
                    }

                    txtlotone.Text = ds.Tables[0].Rows[0]["lot_No_one"].ToString();
                    txtlottwo.Text = ds.Tables[0].Rows[0]["lot_No_two"].ToString();
                    txtlotthree.Text = ds.Tables[0].Rows[0]["lot_No_three"].ToString();
                    txtlotfour.Text = ds.Tables[0].Rows[0]["lot_No_four"].ToString();
                    txtlotfive.Text = ds.Tables[0].Rows[0]["lot_No_five"].ToString();
                    txtlotsix.Text = ds.Tables[0].Rows[0]["lot_No_six"].ToString();
                    txtseven.Text = ds.Tables[0].Rows[0]["lot_No_seven"].ToString();
                    txteight.Text = ds.Tables[0].Rows[0]["lot_No_eight"].ToString();

                    txtTotaS.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    txtChoteToteS.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    txtVijatiyeS.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    txtDamageDaaneS.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    txtBadrangDaaneS.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    txtChaakiDaaneS.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    txtLaalDaaneS.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    txtnamiS.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
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

    public void GetReject()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select DM.district_name, IC.DepotName, G.Godown_Name, Stack_Name, PO.CropYear, IM.Inspector_Name, Designation,bags, convert(varchar(10), D_O_Inspection, 103) as Date_of_inspection, TotaS, ChoteToteS, VijatiyeS, BadrangDaaneS, NamiS, DamageDaaneS,ChaakiDaaneS, LaalDaaneS, lot_No_eight, lot_No_five, lot_No_four, lot_No_one, lot_No_seven, lot_No_six, lot_No_two, lot_No_three,D.district_name as Dist, MR.Mill_Name as mill, D1.district_name as dist1, MR1.Mill_Name as mill1, D2.district_name as dist2, MR2.Mill_Name as mill2, D3.district_name as dist3, MR3.Mill_Name as mill3, D4.district_name as dist4, MR4.Mill_Name as mill4 from PM_Inspection_ByOnemember as PO inner join pds.districtsmp as DM on DM.district_code=PO.District_ID inner join tbl_MetaData_DEPOT as IC on IC.DepotID=PO.ICenter_ID inner join tbl_MetaData_GODOWN as G on G.Godown_ID=PO.Godown_ID inner join Inspector_Master_02017 as IM on IM.Inspector_ID=PO.Inspector_Name and IM.Inspector_desig=PO.Designation and IM.district=PO.District_ID and IM.IssueCenter_code=PO.ICenter_ID left join pds.districtsmp as D on D.district_code=PO.Miller_District left join Miller_Registration_2017 as MR on MR.Registration_ID=PO.Miller_Name and MR.District_Code=PO.Miller_District and MR.CropYear=Po.CropYear left join pds.districtsmp as D1 on D1.district_code=PO.Miller_District_one left join Miller_Registration_2017 as MR1 on MR1.Registration_ID=PO.Miller_Name_one and MR1.District_Code=PO.Miller_District_one and MR1.CropYear=Po.CropYear left join pds.districtsmp as D2 on D2.district_code=PO.Miller_District_two left join Miller_Registration_2017 as MR2 on MR2.Registration_ID=PO.Miller_Name_two and MR2.District_Code=PO.Miller_District_two and MR2.CropYear=Po.CropYear left join pds.districtsmp as D3 on D3.district_code=PO.Miller_District_three left join Miller_Registration_2017 as MR3 on MR3.Registration_ID=PO.Miller_Name_three and MR3.District_Code=PO.Miller_District_three and MR3.CropYear=Po.CropYear left join pds.districtsmp as D4 on D4.district_code=PO.Miller_District_Four left join Miller_Registration_2017 as MR4 on MR4.Registration_ID=PO.Miller_Name_Four and MR4.District_Code=PO.Miller_District_Four and MR4.CropYear=Po.CropYear where Rejection_NO='" + ddlacpt.SelectedValue.ToString() + "' ");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PM_Inspection_ByOnemember");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtInspector.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                    txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    txtDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    txtIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    txtgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    txtstack.Text = ds.Tables[0].Rows[0]["Stack_Name"].ToString();
                    txtDOI.Text = ds.Tables[0].Rows[0]["Date_of_inspection"].ToString();
                    txtBags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    string miller, district;
                    miller = ds.Tables[0].Rows[0]["mill"].ToString();
                    district = ds.Tables[0].Rows[0]["Dist"].ToString();
                    if (((miller == "") && (district == "")) || ((miller == "0") && (district == "0")))
                    {
                        tronemiller.Visible = false;
                        trmultiplemiller.Visible = true;

                        txtmillnameone.Text = ds.Tables[0].Rows[0]["mill1"].ToString();
                        txtmilldistone.Text = ds.Tables[0].Rows[0]["dist1"].ToString();
                        txtmillnametwo.Text = ds.Tables[0].Rows[0]["mill2"].ToString();
                        txtmilldisttwo.Text = ds.Tables[0].Rows[0]["dist2"].ToString();
                        txtmillnamethree.Text = ds.Tables[0].Rows[0]["mill3"].ToString();
                        txtmilldistthree.Text = ds.Tables[0].Rows[0]["dist3"].ToString();
                        txtmillnamefour.Text = ds.Tables[0].Rows[0]["mill4"].ToString();
                        txtmilldistfour.Text = ds.Tables[0].Rows[0]["dist4"].ToString();
                    }
                    else
                    {
                        

                        tronemiller.Visible = true;
                        trmultiplemiller.Visible = false;
                        txtmillname.Text = ds.Tables[0].Rows[0]["mill"].ToString();
                        txtmillDist.Text = ds.Tables[0].Rows[0]["Dist"].ToString();
                    }

                    txtlotone.Text = ds.Tables[0].Rows[0]["lot_No_one"].ToString();
                    txtlottwo.Text = ds.Tables[0].Rows[0]["lot_No_two"].ToString();
                    txtlotthree.Text = ds.Tables[0].Rows[0]["lot_No_three"].ToString();
                    txtlotfour.Text = ds.Tables[0].Rows[0]["lot_No_four"].ToString();
                    txtlotfive.Text = ds.Tables[0].Rows[0]["lot_No_five"].ToString();
                    txtlotsix.Text = ds.Tables[0].Rows[0]["lot_No_six"].ToString();
                    txtseven.Text = ds.Tables[0].Rows[0]["lot_No_seven"].ToString();
                    txteight.Text = ds.Tables[0].Rows[0]["lot_No_eight"].ToString();

                    txtTotaS.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    txtChoteToteS.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    txtVijatiyeS.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    txtDamageDaaneS.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    txtBadrangDaaneS.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    txtChaakiDaaneS.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    txtLaalDaaneS.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    txtnamiS.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
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
    protected void bttdelete_Click(object sender, EventArgs e)
    {
        if (lblacprjt.Text=="Acceptance No.")
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    ClientIP objClientIP = new ClientIP();
                    string GetIp = (objClientIP.GETIP());


                    con.Open();

                    string instr = "";

                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";


                    instr += "Insert Into PM_Inspection_ByOnemember_log(InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count, lot_No_seven, lot_No_eight, DeletedIP,DeletedDate) Select InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count, lot_No_seven, lot_No_eight, '" + GetIp + "', GETDATE() From PM_Inspection_ByOnemember where Acceptance_NO='" + ddlacpt.SelectedValue.ToString() + "' ;";

                    instr += "Delete From PM_Inspection_ByOnemember where Acceptance_NO='" + ddlacpt.SelectedValue.ToString() + "'";

                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    cmd = new SqlCommand(instr, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        btnDelete.Enabled = false;

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Deleted Successfully'); </script> ");

                        btnDelete.Enabled = false;
                        //txtYear.Text = "";
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

            using (con = new SqlConnection(strcon))
            {
                try
                {
                    ClientIP objClientIP = new ClientIP();
                    string GetIp = (objClientIP.GETIP());


                    con.Open();

                    string instr = "";

                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";


                    instr += "Insert Into PM_Inspection_ByOnemember_log(InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count, lot_No_seven, lot_No_eight, DeletedIP,DeletedDate) Select InspectionID, CropYear, season, D_O_Inspection, Inspector_Name, Designation, mill_phone, District_ID, ICenter_ID, Godown_ID, Stack_ID, TotaS, ChoteToteS, VijatiyeS, DamageDaaneS, BadrangDaaneS, ChaakiDaaneS, LaalDaaneS, NamiS, Status, IP, Created_Date, Stack_Name, Acceptance_NO, Rejection_NO, Bags, lot_No_one, lot_No_two, lot_No_three, lot_No_four, lot_No_five, lot_No_six, Miller_District, Miller_Name, Miller_District_one, Miller_Name_one, Miller_District_two, Miller_Name_two, Miller_District_three, Miller_Name_three,  Miller_District_Four, Miller_Name_Four, MoreThan_OneMiller, Miller_Count, lot_No_seven, lot_No_eight, '" + GetIp + "', GETDATE() From PM_Inspection_ByOnemember where Rejection_NO='" + ddlacpt.SelectedValue.ToString() + "' ;";

                    instr += "Delete From PM_Inspection_ByOnemember where Rejection_NO='" + ddlacpt.SelectedValue.ToString() + "'";

                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    cmd = new SqlCommand(instr, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        btnDelete.Enabled = false;

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Deleted Successfully'); </script> ");


                        //txtYear.Text = "";
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
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDist.SelectedIndex >= 0)
        {
            GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select District'); </script> ");
            return;
        }

    }
    protected void ddlissueC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlissueC.SelectedIndex>=0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Issue Center'); </script> ");
            return;
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgd.SelectedIndex>=0)
        {
            GetStack();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Godown name'); </script> ");
            return;
        }
    }
    protected void ddlstack_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSK.SelectedIndex>=0)
        {
            GetAcceptance();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Stack No.'); </script> ");
            return;
        }
    }
    protected void ddlacpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select Acceptance_NO, Rejection_NO from PM_Inspection_ByOnemember as PM where Pm.District_ID='" + ddlDist.SelectedValue.ToString() + "' and ICenter_ID='" + ddlissueC.SelectedValue.ToString() + "' and Godown_ID='" + ddlgd.SelectedValue.ToString() + "' and Stack_ID='" + ddlSK.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string accep, rejec;
                    accep = ds.Tables[0].Rows[0]["Acceptance_NO"].ToString();
                    rejec = ds.Tables[0].Rows[0]["Rejection_NO"].ToString();
                    if (accep == "" || accep == "0")
                    {
                        if(ddlacpt.SelectedIndex>=0)
                        {
                        GetReject();
                       
                        }
                    }
                    else 
                    {
                       
                        GetAccept();
                       
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance No. is not available'); </script> ");
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
    
   
    protected void bttclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/PaddyMillingHome.aspx");

    }
    protected void bttnew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}