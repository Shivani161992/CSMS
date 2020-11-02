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
using System.Text.RegularExpressions;
public partial class MBCFPCL_MPCFPCL_CSC_ChanaMasoor : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd, cmd1;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string IC_Id = "", Dist_Id = "";

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public SqlConnection Warehouse = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DistID"] != null)
        {
            string samepassword = Session["NotchangePassword"].ToString();
            if (samepassword == "MBC123")
            {
                Response.Redirect("~/MBCFPCL/MBCFPCL_ChangePassword.aspx");
            }
            else if (samepassword != "MBC123")
            {
                Session["Receipt_ID"] = "";
                Session["Commodity_ID"] = "";

                txt_recJutNew.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txt_recJutNew.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txt_recJutNew.Attributes.Add("onchange", "return chksqltxt(this)");

                txt_recJutOld.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txt_recJutOld.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txt_recJutOld.Attributes.Add("onchange", "return chksqltxt(this)");

                txt_recPP.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txt_recPP.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txt_recPP.Attributes.Add("onchange", "return chksqltxt(this)");

                DaintyDate3.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");


                txtRec_TruckNumber.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRec_TruckNumber.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRec_TruckNumber.Attributes.Add("onchange", "return chksqltxt(this)");

                txt_GrossWeight.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txt_GrossWeight.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txt_GrossWeight.Attributes.Add("onchange", "return chksqltxt(this)");

                txtbadStiching.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtbadStiching.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtbadStiching.Attributes.Add("onchange", "return chksqltxt(this)");

                txtBadStelcile.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtBadStelcile.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtBadStelcile.Attributes.Add("onchange", "return chksqltxt(this)");





                //IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["DistID"].ToString();

               // GetICName();
                GetCommodity();
                Getdepo();

                GetWhtWeighbridgeName();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }


        }
        else
        {
            Response.Redirect("~/MBCFPCL/MBCFPCL_Login.aspx");
        }
        DaintyDate3.Text = Request.Form[DaintyDate3.UniqueID];

    }


    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id IN ('33','63','64')";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlcomdty.DataSource = ds.Tables[0];
                    ddlcomdty.DataTextField = "Commodity_Name";
                    ddlcomdty.DataValueField = "Commodity_Id";
                    ddlcomdty.DataBind();
                    ddlcomdty.Items.Insert(0, "--Select--");
                    // ddlcomdty.SelectedIndex = 1;

                    getDist();
                    getWhtUparjncntr(); //ddldistpdy_SelectedIndexChanged(sender, e);


                    //getcsms_Commdty();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void Getdepo()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();

                ddlbranchwlc.DataSource = "";
                ddlbranchwlc.DataBind();

                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID ");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "BranchID";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddlbranchwlc.Items.Insert(0, "--Select--");

                        // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "BranchId";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--Select--");
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

    private void getDist()
    {
        Dist_Id = Session["DistID"].ToString();

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Dist_name,district_code from pds.districtsmp where district_code not in ('99') order by District_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldistpdy.DataSource = ds.Tables[0];
                    ddldistpdy.DataTextField = "Dist_name";
                    ddldistpdy.DataValueField = "district_code";
                    ddldistpdy.DataBind();
                    ddldistpdy.Items.Insert(0, "--Select--");
                    ddldistpdy.Items.FindByValue(Dist_Id).Selected = true;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void getWhtUparjncntr()
    {
        Dist_Id = Session["DistID"].ToString();

        if (ddlcomdty.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            string comm = ddlcomdty.SelectedValue.ToString();

            if (comm == "63")
            {
                comm = "6";
            }

            if (comm == "64")
            {
                comm = "63";
            }

            if (comm == "33")
            {
                comm = "12";
            }

            using (con = new SqlConnection(Con_CSMS))
            {
                try
                {
                    con.Open();

                    string select = "";
                    string sen_dist = ddldistpdy.SelectedValue.ToString();


                    select = "select ic.SocietyID as Society_Id,(Society_MSP.Society_Name+','+Society_MSP.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID) as varchar(50)) + ')') as Society_Name from IssueToSangrahanaKendra_CSM2018 ic inner join Society_MSP on Society_MSP.Society_Id = ic.SocietyID where ic.DistrictId= '23" + ddldistpdy.SelectedValue.ToString() + "' and ic.IssueID not in (select SCSC_Procurement_CSM.Receipt_Id from SCSC_Procurement_CSM where SCSC_Procurement_CSM.Commodity_Id = '" + ddlcomdty.SelectedValue.ToString() + "' ) and Society_MSP.DistrictId = '23" + sen_dist + "' and ic.CommodityId = '" + comm + "' and Society_MSP.MBC='Y' group by ic.SocietyID ,Society_MSP.Society_Name,Society_MSP.SocPlace, ic.SocietyID order by ic.SocietyID ";

                    SqlCommand cmd = new SqlCommand(select, con);

                    da = new SqlDataAdapter(cmd);

                    cmd.CommandTimeout = 0;

                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddluparjan.DataSource = ds.Tables[0];
                            ddluparjan.DataTextField = "Society_Name";
                            ddluparjan.DataValueField = "Society_Id";
                            ddluparjan.DataBind();
                            ddluparjan.Items.Insert(0, "--Select--");
                        }
                        else
                        {
                            ddluparjan.DataSource = "";
                            ddluparjan.DataBind();
                            ddluparjan.Items.Insert(0, "--Select--");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    return;
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

    protected void ddluparjan_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfgdntype.Value = hdfWeighbridgeID.Value = "";
        ddlsector.Items.Clear();
        pnlgrd.Visible = false;
        Weighbridge_TaulParchi.Enabled = Weighbridge_Qty.Enabled = false;
        dgridchallan.DataSource = null;
        dgridchallan.DataBind();

        ddlpdyTransporter.Items.Clear();
        ddlcsms_transp.Items.Clear();
        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recPP.Text = txtRec_TruckNumber.Text = txt_GrossWeight.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = "";
        txtbadStiching.Text = txtBadStelcile.Text = "0";

        if (ddluparjan.SelectedIndex > 0)
        {

            string comm = ddlcomdty.SelectedValue.ToString();

            if (comm == "63")  // Chana
            {
                comm = "6";
            }

            if (comm == "64")  // Masoor
            {
                comm = "63";
            }

            if (comm == "33")  // Sasro
            {
                comm = "12";
            }

            if (comm == "6" || comm == "63" || comm == "12")
            {

                getWhtIssueid(comm);
                // getwhtTranspoter();     10/03/17
                GetSectorData();
            }

            pnlgrd.Visible = true;


            using (con = new SqlConnection(Con_CSMS))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string getKparishar = "select * from GodownMapping_KryParisar_CSM2018 where procurmentCenterID = '" + ddluparjan.SelectedValue.ToString() + "'";
                SqlCommand cmdK = new SqlCommand(getKparishar, con);

                SqlDataAdapter daK = new SqlDataAdapter(cmdK);
                DataSet dsK = new DataSet();

                daK.Fill(dsK);

                if (dsK.Tables[0].Rows.Count > 0)    // Here Found Kray Parishar , disable the loaded qty
                {
                    lbl_KP.Text = "KP";
                }

                else
                {
                    lbl_KP.Text = "PC";
                }

            }



        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center'); </script> ");
            return;
        }
    }

    private void getWhtIssueid(string commid)
    {
        Dist_Id = Session["DistID"].ToString();

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106) As DateOfIssue,ist.Bags,ist.QtyTransffer,ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId ,ist.GodownTypeId,ist.Weighbridge_ID , ist.GodownNumber , ist.BranchID ,isnull(ist.CHK_FAQ,'P')U_Status , isnull(ist.GS_Status,'P') as G_Status from IssueToSangrahanaKendra_CSM2018 ist left join Transporter_Table tm on tm.Transporter_ID=ist.TransporterId and tm.UparjanId = ist.SocietyID  where ist.IssueID  not in (select scsc_procurement_csm.Receipt_Id from scsc_procurement_csm ) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId = '" + commid + "'  order by DateOfIssue desc";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgridchallan.DataSource = ds.Tables[0];
                    dgridchallan.DataBind();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void getwhtTranspoter(string transid)
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {


                string mystring = ddldistpdy.SelectedValue;
                string disttid = mystring.Substring(mystring.Length - 2);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string Transport_CSMS = "select distinct Pancard_no,Transporter_Name from Transporter_Table where Transport_ID='8' and Distt_ID='" + disttid + "' and Pancard_no <> '0' and Pancard_no is not null and LRT_proc_secter= '" + ddlsector.SelectedValue + "' and Transporter_ID = '" + transid + "'";
                da = new SqlDataAdapter(Transport_CSMS, con);

                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlpdyTransporter.DataSource = ds.Tables[0];
                    ddlpdyTransporter.DataTextField = "Transporter_Name";
                    ddlpdyTransporter.DataValueField = "Pancard_no";
                    ddlpdyTransporter.DataBind();

                }

                else
                {
                    ddlpdyTransporter.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void GetSectorData()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "Select SectorCode from Sectorto_PC_Mapping where PCCode = '" + ddluparjan.SelectedValue.ToString() + "' and cropyear = '" + txtYear.Text + "' and commodity IN('64')";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sector = ds.Tables[0].Rows[0]["SectorCode"].ToString();

                    string getsectorName = "Select SectorId, SectorName from District_SectorMaster where SectorId = '" + sector + "'";

                    da1 = new SqlDataAdapter(getsectorName, con);
                    ds1 = new DataSet();
                    da1.Fill(ds1);

                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlsector.DataSource = ds1.Tables[0];
                        ddlsector.DataTextField = "SectorName";
                        ddlsector.DataValueField = "SectorId";
                        ddlsector.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {

        string U_sur = dgridchallan.SelectedRow.Cells[16].Text.Trim();

        string G_sur = dgridchallan.SelectedRow.Cells[17].Text.Trim();

        if (U_sur == "N")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('उपार्जन केंद्र के सर्वेयर द्वारा इसकी जांच नही की गयी है, अतः इसकी प्राप्ति नही होगी|'); </script> ");
            return;
        }

        else if (U_sur == "R")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('उपार्जन केंद्र के सर्वेयर द्वारा इस को रिजेक्ट कर दिया गया है, अतः इसकी प्राप्ति नही होगी|'); </script> ");
            return;
        }

        else if (G_sur == "R")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('गोदाम के सर्वेयर द्वारा इस को रिजेक्ट कर दिया गया है, अतः इसकी प्राप्ति नही होगी|'); </script> ");
            return;
        }

        else if (G_sur == "P")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('गोदाम के सर्वेयर द्वारा इसकी जांच नही की गयी है, अतः इसकी प्राप्ति नही होगी|'); </script> ");
            return;
        }

        else
        {
            hdfgdntype.Value = hdfWeighbridgeID.Value = "";
            Weighbridge_TaulParchi.Enabled = Weighbridge_Qty.Enabled = false;
            ddlcsms_transp.Items.Clear();
            ddlgodown.Items.Clear();
            ddlbranchwlc.SelectedIndex = 0;

            txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recATwill.Text = txt_recPP.Text = DaintyDate3.Text = txtRec_TruckNumber.Text = txt_GrossWeight.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = "";
            txtbadStiching.Text = txtBadStelcile.Text = "0";

            txtissueId.Text = dgridchallan.SelectedRow.Cells[2].Text;
            txtchlnno.Text = dgridchallan.SelectedRow.Cells[3].Text;
            txttrucknopady.Text = dgridchallan.SelectedRow.Cells[4].Text;
            txtissubag.Text = dgridchallan.SelectedRow.Cells[5].Text;
            txtissueqty.Text = dgridchallan.SelectedRow.Cells[6].Text;

            txtRec_TruckNumber.Text = dgridchallan.SelectedRow.Cells[4].Text;

            if (txtRec_TruckNumber.Text == "&nbsp;")
            {
                txtRec_TruckNumber.Text = "";
            }

            if (txtRec_TruckNumber.Text == "")
            {
                txtRec_TruckNumber.Text = "0";
            }


            string GodownId = dgridchallan.SelectedRow.Cells[14].Text.Trim();

            string BranchId = dgridchallan.SelectedRow.Cells[15].Text.Trim();

            string getrealBranch = "select BranchID from tbl_MetaData_GODOWN where Godown_ID = '" + GodownId + "'";

            SqlCommand cmdBranch = new SqlCommand(getrealBranch, Warehouse);
            SqlDataAdapter dabranch = new SqlDataAdapter(cmdBranch);

            if (Warehouse.State == ConnectionState.Closed)
            {
                Warehouse.Open();
            }

            DataSet dsBranch = new DataSet();
            dabranch.Fill(dsBranch);

            if (dsBranch.Tables[0].Rows.Count > 0)
            {
                BranchId = dsBranch.Tables[0].Rows[0]["BranchID"].ToString();
            }

            if (Warehouse.State == ConnectionState.Open)
            {
                Warehouse.Close();
            }

            if (GodownId == "" || GodownId == "&nbsp;")
            {
                ddlbranchwlc.Enabled = true;
            }

            //else
            //{
            // Check wheather DM change the godown , or not , if yes then show new godown

            using (con = new SqlConnection(Con_CSMS))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                ddlbranchwlc.Enabled = false;

                string qrychnge = "Select NewGodown , NewTruckNo , NewBranch from ChangeGodown_CSM2018  where OldChallan = '" + txtchlnno.Text + "' and Society_Id = '" + ddluparjan.SelectedValue + "' order by CreatedDate desc";
                SqlCommand cmdchange = new SqlCommand(qrychnge, con);
                SqlDataAdapter dachnge = new SqlDataAdapter(cmdchange);

                DataSet dschnage = new DataSet();

                dachnge.Fill(dschnage);

                if (dschnage.Tables[0].Rows.Count > 0)
                {
                    GodownId = dschnage.Tables[0].Rows[0]["NewGodown"].ToString();

                    BranchId = dschnage.Tables[0].Rows[0]["NewBranch"].ToString();

                    txttrucknopady.Text = dschnage.Tables[0].Rows[0]["NewTruckNo"].ToString();

                    txtRec_TruckNumber.Text = dschnage.Tables[0].Rows[0]["NewTruckNo"].ToString();

                    getbranch(BranchId);

                    Getgdn_Id(GodownId);

                    ddlgodown.Enabled = false;

                    GetStackNum();

                    ddlgodown.Enabled = true;
                }
                else
                {
                    getbranch(BranchId);

                    Getgdn_Id(GodownId);

                    // Getgon();

                    ddlgodown.Enabled = false;

                    GetStackNum();
                }

            }
            //}

            if (BranchId.Contains("WD") == true)
            {
                BranchId = "";
            }

            if (BranchId == "" || BranchId == "&nbsp;")
            {
                Getdepo();
                ddlbranchwlc.Enabled = true;
            }

            else
            {
                getbranch(BranchId);

                ddlbranchwlc.Enabled = false;
            }

            DaintyDate1P.Text = dgridchallan.SelectedRow.Cells[1].Text;


            txt_GrossWeight.Text = dgridchallan.SelectedRow.Cells[6].Text;

            if (ddlcomdty.SelectedValue == "33")
            {
                txt_recJutNew.Text = "0";
                txt_recJutOld.Text = "0";
                txt_recPP.Text = "0";
                hdfgdntype.Value = dgridchallan.SelectedRow.Cells[12].Text;
                hdfWeighbridgeID.Value = dgridchallan.SelectedRow.Cells[13].Text;
            }

            else
            {
                txt_recJutNew.Text = dgridchallan.SelectedRow.Cells[9].Text;
                txt_recJutOld.Text = dgridchallan.SelectedRow.Cells[10].Text;

                if (txt_recJutOld.Text != "0")
                {
                    txt_recJutNew.Text = dgridchallan.SelectedRow.Cells[10].Text;

                    txt_recJutOld.Text = "0";
                }


                txt_recPP.Text = "0";
                hdfgdntype.Value = dgridchallan.SelectedRow.Cells[12].Text;
                hdfWeighbridgeID.Value = dgridchallan.SelectedRow.Cells[13].Text;
            }



            if (txt_recPP.Text == "")
            {
                txt_recPP.Text = "0";
            }

            if (txt_recJutOld.Text == "")
            {
                txt_recJutOld.Text = "0";
            }

            if (txt_recJutNew.Text == "")
            {
                txt_recJutNew.Text = "0";
            }

            if (txt_recATwill.Text == "")
            {
                txt_recATwill.Text = "0";
            }

            Weighbridge_TaulParchi.Enabled = Weighbridge_Qty.Enabled = true;

            GetWeighbridgeName();


            string transpid = dgridchallan.SelectedRow.Cells[8].Text.Trim();

            if (transpid == "" || transpid == "&nbsp;")
            {
                transpid = "";
            }
            else
            {
                // ddlpdyTransporter.SelectedValue = dgridchallan.SelectedRow.Cells[8].Text;
            }

            GetCsmsTransPorter(transpid);


            getwhtTranspoter(transpid);

            using (con = new SqlConnection(Con_CSMS))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string getKparishar = "select * from GodownMapping_KryParisar_CSM2018 as KP where KP.procurmentCenterID = '" + ddluparjan.SelectedValue.ToString() + "' and  KP.ToDate >= '" + getDate_MDY(DaintyDate1P.Text) + "'";
                SqlCommand cmdK = new SqlCommand(getKparishar, con);

                SqlDataAdapter daK = new SqlDataAdapter(cmdK);
                DataSet dsK = new DataSet();

                daK.Fill(dsK);

                if (dsK.Tables[0].Rows.Count > 0)    // Here Found Kray Parishar , disable the loaded qty
                {
                    lbl_KP.Text = "KP";

                    TD_hideDM.Visible = false;

                    TD_hideRSt.Visible = false;

                    btn_calc.Visible = false;

                    TD_hideforKP.Visible = false;

                    TD_hideKP.Visible = false;

                    txt_NetWeight.ReadOnly = false;

                    txt_NetWeight.Enabled = true;
                }

                else
                {
                    lbl_KP.Text = "PC";
                    TD_hideDM.Visible = true;
                    TD_hideRSt.Visible = true;

                    btn_calc.Visible = true;

                    TD_hideforKP.Visible = true;

                    TD_hideKP.Visible = true;

                    txt_NetWeight.ReadOnly = true;

                    txt_NetWeight.Enabled = false;
                }

            }


        }
    }

    private void GetWeighbridgeName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT   [Weigh_Id] ,[WighBridge_Number] FROM [WeighBridge_Master] where District_Code = '" + Dist_Id + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddl_WBridge.DataSource = ds.Tables[0];
                    ddl_WBridge.DataTextField = "WighBridge_Number";
                    ddl_WBridge.DataValueField = "Weigh_Id";
                    ddl_WBridge.DataBind();
                    ddl_WBridge.Items.Insert(0, "--select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void GetCsmsTransPorter(string transid)
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                string mystring = ddldistpdy.SelectedValue;
                string disttid = mystring.Substring(mystring.Length - 2);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string Transport_CSMS = "select distinct Transporter_ID ,Transporter_Name from Transporter_Table where Transport_ID='8' and Distt_ID='" + disttid + "' and Pancard_no <> '0' and Pancard_no is not null and LRT_proc_secter= '" + ddlsector.SelectedValue + "' and Transporter_ID = '" + transid + "'";
                da = new SqlDataAdapter(Transport_CSMS, con);

                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcsms_transp.DataSource = ds.Tables[0];
                    ddlcsms_transp.DataTextField = "Transporter_Name";
                    ddlcsms_transp.DataValueField = "Transporter_ID";
                    ddlcsms_transp.DataBind();

                }
                else
                {
                    ddlcsms_transp.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        string arg;
        arg = e.CommandArgument.ToString();

        switch (arg)
        {
            case "next":
                //The next Button was Clicked
                if ((dgridchallan.PageIndex < (dgridchallan.PageCount - 1)))
                {
                    dgridchallan.PageIndex += 1;
                }

                break;

            case "prev":
                //The prev button was clicked
                if ((dgridchallan.PageIndex > 0))
                {
                    dgridchallan.PageIndex -= 1;
                }

                break;

            case "last":
                //The Last Page button was clicked
                dgridchallan.PageIndex = (dgridchallan.PageCount - 1);
                break;

            default:
                //The First Page button was clicked
                dgridchallan.PageIndex = Convert.ToInt32(arg);
                break;
        }
        //fillgrid();
    }

    protected void ddlbranchwlc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlgodown.Items.Clear();
        ddlgodown.Enabled = true;

        txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txthhty.Text = "";

        if (txtissueId.Text == "" || DaintyDate1P.Text == "" || txtchlnno.Text == "" || txttrucknopady.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया जारी क्रमांक को चुने'); </script> ");
            return;
        }
        else
        {
            if (ddlbranchwlc.SelectedIndex > 0)
            {
                Getgon();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
                return;
            }
        }
    }

    private void Getgon()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchId='" + ddlbranchwlc.SelectedValue.ToString() + "' and Remarks = 'Y'";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, "--select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txthhty.Text = "";

        if (ddlgodown.SelectedIndex > 0)
        {
            // HiredType();
            //  GetCapacity();

            GetStackNum();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
    }

    private void HiredType()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string qrysel = "select tbl_MetaData_GODOWN.Godown_ID,Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,isnull(SUM(ReceiptWts),0)as depositmsp,ISNULL(Godown_Capacity - SUM(ReceiptWts),0)as vacientcap from tbl_MetaData_GODOWN  left join tbl_MetaData_STACK on tbl_MetaData_GODOWN.Godown_ID = tbl_MetaData_STACK.Godown_ID  left join DailyStacking_TransactionStatus on DailyStacking_TransactionStatus.Stackid = tbl_MetaData_STACK.Stack_ID  where tbl_MetaData_GODOWN.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'   group by Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,tbl_MetaData_GODOWN.Godown_ID order by tbl_MetaData_GODOWN.Godown_ID";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txthhty.Text = ds.Tables[0].Rows[0]["Hired_Type"].ToString().Trim() + "/" + ds.Tables[0].Rows[0]["Storage_Type"].ToString().Trim();
                    txtmaxcap.Text = ds.Tables[0].Rows[0]["Godown_Capacity"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void GetCapacity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                //IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["DistID"].ToString();

                con.Open();
                string pqry = "available_space_godown";
                SqlCommand cmdpqty = new SqlCommand(pqry, con);
                cmdpqty.CommandType = CommandType.StoredProcedure;

                cmdpqty.Parameters.Add("@district_code", SqlDbType.VarChar).Value = Dist_Id;
               // cmdpqty.Parameters.Add("@Depotid", SqlDbType.VarChar).Value = IC_Id;
                cmdpqty.Parameters.Add("@GodownId", SqlDbType.VarChar).Value = ddlgodown.SelectedValue.ToString();

                DataSet ds1 = new DataSet();
                SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

                dr.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    double stock = Math.Round(Convert.ToDouble(ds1.Tables[0].Rows[0]["Total"].ToString()), 5);
                    txtcurntcap.Text = Convert.ToString(stock);
                    double Max_Cap = Math.Round(Convert.ToDouble(CheckNull(txtmaxcap.Text)), 5);
                    double availble = Max_Cap - stock;
                    txtavalcap.Text = Convert.ToString(availble);
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MBCFPCL/MPCFPCL_WelcomeHome.aspx");
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        if (ddluparjan.SelectedIndex > 0)
        {

            DaintyDate1P.Text = "";

            txt_recJutNew.Text = "";
            txt_GrossWeight.Text = "";
            txtchlnno.Text = "";
            txtissueId.Text = "";
            txttrucknopady.Text = "";
            txtissubag.Text = "";
            txtissueqty.Text = "";


            string comm = ddlcomdty.SelectedValue.ToString();

            if (comm == "63")
            {
                comm = "6";
            }

            if (comm == "64")
            {
                comm = "63";
            }

            if (comm == "33")
            {
                comm = "12";
            }

            if (comm == "6" || comm == "63" || comm == "12")
            {
                getWhtIssueid(comm);
            }

        }

        else
        {

        }

        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        decimal senqty = CheckNull(txtissueqty.Text);

        decimal qty_5 = senqty + (senqty * 5) / 100;


        if (Convert.ToDecimal(txt_NetWeight.Text) > qty_5)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्ति की मात्रा, प्रेषित मात्रा के ५ प्रतिशत के साथ से अधिक नहीं हो सकती , ये प्रकरण मुख्यालय में softmpscsc@gmail.com पर भेजे|'); </script> ");
            return;
        }

        if (lbl_KP.Text == "KP")
        {
            if (txt_NetWeight.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्ति मात्रा उपलब्ध नही है'); </script> ");
                return;
            }

            else if (Convert.ToDecimal(txt_NetWeight.Text) <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्त मात्रा 0 अथवा 0 से कम नहीं हो सकती'); </script> ");
                return;
            }

        }

        else
        {

            if (Convert.ToDecimal(txt_NetWeight.Text) > 425)
            {

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('शुद्ध वजन किसी भी स्थिति में 425 Qtls से ज्यादा नहीं हो सकता ,ये प्रकरण मुख्यालय में softmpscsc@gmail.com पर भेजे|' |'); </script> ");
                return;
            }

            else if (Weighbridge_TaulParchi.Text != "" && Weighbridge_TaulParchi.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('तौल पर्ची क्रमांक (RST No.) भरें'); </script> ");
                return;
            }

            if (Weighbridge_empty.Text == "0" || Weighbridge_empty.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('खाली गाडी में मात्रा भरें (Qntls में )|'); </script> ");
                return;
            }

            if (Weighbridge_Qty.Text == "0" || Weighbridge_Qty.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('भरी गाडी में मात्रा भरें (Qntls में )|'); </script> ");
                return;
            }

            if (lbl_ppweight.Text == "" || lbl_JuteNewWeight.Text == "" || lbl_JuteOldWeight.Text == "" || lbl_ATwillWeight.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('बोरे एवं शुद्ध वजन के लिए ऊपर की बटन -बोरे का वजन,कुल वजन एवं शुद्ध वजन के लिए यहाँ क्लिक करें- पर क्लिक करें |'); </script> ");
                return;
            }
        }


        if (ddluparjan.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('उपार्जन केंद्र को चुने'); </script> ");
            return;
        }
        else if (ddlgodown.SelectedItem.Text == "--select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('गोदाम को चुने'); </script> ");
            return;
        }

        else if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया दिनांक चुने|'); </script> ");
            return;
        }

        else if (txtRec_TruckNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ट्रक नंबर भरें'); </script> ");
            return;
        }
        else if (txt_GrossWeight.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ग्रॉस मात्रा उपलब्ध नही है'); </script> ");
            return;
        }

        else if (txt_NetWeight.Text == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्ति मात्रा 0 नहीं हो सकती'); </script> ");
            return;
        }

        else if (Convert.ToDecimal(txt_GrossWeight.Text) <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्त मात्रा 0 अथवा 0 से कम नहीं हो सकती'); </script> ");
            return;
        }


        else if (ddl_StakeNumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्टैक नंबर चुने |'); </script> ");
            return;
        }

        else
        {

            string RecdDate = getDate_MDY(DaintyDate3.Text);
            string DispatchDate = getDate_MDY(DaintyDate1P.Text);

            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(Con_CSMS))
                {
                    try
                    {
                        Dist_Id = Session["DistID"].ToString();

                        con.Open();
                        string CheckduplicateRec = "Select * from SCSC_Procurement_CSM where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Truck_Number = '" + txttrucknopady.Text + "' and Receipt_Id = '" + txtissueId.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                        da = new SqlDataAdapter(CheckduplicateRec, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('संभवतः इस प्राप्ति क्रमांक की एंट्री की जा चुकी है '); </script> ");
                            return;
                        }
                        else
                        {
                            if (hdfgdntype.Value == "" || hdfgdntype.Value == "&nbsp;")
                            {
                                hdfgdntype.Value = "0";
                            }


                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;


                            SqlDataAdapter daP;
                            DataSet dsP = new DataSet();
                            string cmtid = hdfCSMS_Comid.Value;
                            string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement_CSM where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id= '" + hdfCSMS_Comid.Value + "' ";
                            cmd.CommandText = select;
                            daP = new SqlDataAdapter(cmd);

                            daP.Fill(dsP);

                            if (dsP.Tables[0].Rows.Count == 0)
                            {
                                string gatepass = "";
                                string distp = "";

                                gatepass = txtissueId.Text.Trim().ToString();
                                distp = ddldistpdy.SelectedValue.ToString();
                                string mpcdist = distp;
                                string mpcic = ddluparjan.SelectedValue;
                                string mdispdate = getDate_MDY(DaintyDate1P.Text);
                                string mchallan = txtchlnno.Text;

                                string mtruckno_first = txttrucknopady.Text;
                                Regex re = new Regex("[;\\/:*?\"<>|&']");
                                string mtruckno = re.Replace(mtruckno_first, " ");

                                string mtrans = ddlpdyTransporter.SelectedValue.ToString();

                                if (mtrans == "--Select--")
                                {
                                    mtrans = "";
                                }

                                string mcomdty = ddlcomdty.SelectedValue;

                                string mcropy = txtYear.Text;
                                int mbags = CheckNullInt(txtissubag.Text);
                                decimal mqty = CheckNull(txtissueqty.Text);
                                string mstatus = "N";

                                string mudate = "";
                                string mddate = "";
                                string mfyear = DateTime.Today.Year.ToString();
                                int month = int.Parse(DateTime.Today.Month.ToString());
                                int year = int.Parse(DateTime.Today.Year.ToString());
                                string state = Session["State_Id"].ToString();
                                string anst = "Y";

                                string mrecddate = getDate_MDY(DaintyDate3.Text);
                                string mrecdgdn = ddlgodown.SelectedValue;
                                string branch = ddlbranchwlc.SelectedValue;
                                string notrans = "N";

                                string opid = Session["OperatorId"].ToString();
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                               // IC_Id = Session["issue_id"].ToString();

                                string csmsTransporter;


                                string checksilo = "select Godown_ID from tbl_MetaData_GODOWN where Storage_Type like 's%' and DistrictId='23" + Session["dist_id"].ToString() + "' and Godown_ID = '" + mrecdgdn + "'";

                                if (Warehouse.State == ConnectionState.Closed)
                                {
                                    Warehouse.Open();
                                }

                                SqlCommand cmdsilo = new SqlCommand(checksilo, Warehouse);

                                SqlDataReader drsilo;
                                drsilo = cmdsilo.ExecuteReader();

                                if (drsilo.Read())
                                {
                                    drsilo.Close();

                                    csmsTransporter = ddlcsms_transp.SelectedValue;

                                    if (csmsTransporter == "0" || csmsTransporter == "--Select--")
                                    {
                                        csmsTransporter = "";
                                    }
                                }

                                else
                                {
                                    drsilo.Close();

                                    csmsTransporter = ddlcsms_transp.SelectedValue;

                                    if (csmsTransporter == "0" || csmsTransporter == "--Select--")
                                    {
                                        csmsTransporter = "";
                                    }

                                }

                                string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement_CSM where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + mcomdty + "' ";

                                cmd.CommandText = checkrcid;


                                string str1 = cmd.ExecuteScalar().ToString();

                                if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                {
                                    try
                                    {
                                        int mrecdbags = 0;
                                        int mrecdbagsJute = CheckNullInt(txt_recJutNew.Text);
                                        int mrecdbagsPP = CheckNullInt(txt_recPP.Text);
                                        int mrecdbagsJuteOld = CheckNullInt(txt_recJutOld.Text);
                                        int mrecdbagsATwill = CheckNullInt(txt_recATwill.Text);

                                        mrecdbags = mrecdbagsJute + mrecdbagsPP + mrecdbagsJuteOld + mrecdbagsATwill;

                                        int badStiching = CheckNullInt(txtbadStiching.Text);

                                        int BadStelcile = CheckNullInt(txtBadStelcile.Text);

                                        decimal mrecdqty = 0;
                                        decimal mrecdqtyFaq = CheckNull(txt_GrossWeight.Text);
                                        decimal mrecdqtyUrs = 0;

                                        mrecdqty = mrecdqtyFaq + mrecdqtyUrs;

                                        string Taulparchi = Weighbridge_TaulParchi.Text.Trim();

                                        string recdGodownName = ddlgodown.SelectedItem.Text;
                                        string Category = "N";

                                        DateTime dispdate = Convert.ToDateTime(DaintyDate1P.Text);

                                        string todaydate = DateTime.Now.ToString("dd/MM/yyyy");

                                        string WeighbridgeID = ddl_WBridge.SelectedValue;

                                        if (WeighbridgeID == "--select--")
                                        {
                                            WeighbridgeID = "0";
                                        }

                                        // Generate Acno

                                        SqlCommand cmdacno = new SqlCommand();
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }

                                        string AcceptDate = getDate_MDY(DaintyDate3.Text);

                                        int Insyear = int.Parse(DateTime.Today.Year.ToString());

                                        //IC_Id = Session["issue_id"].ToString();
                                        Dist_Id = Session["DistID"].ToString();

                                        cmdacno.Parameters.Clear();
                                        cmdacno.Parameters.AddWithValue("@District_ID", Dist_Id);
                                        //cmdacno.Parameters.AddWithValue("@IssueCenter_ID", IC_Id);
                                        cmdacno.Parameters.AddWithValue("@Year", Insyear);

                                        cmdacno.Connection = con;
                                        cmdacno.CommandType = CommandType.StoredProcedure;
                                        cmdacno.CommandText = "prc_getMAxAcceptance_NoCSM2018Accept";

                                        string Accpt_NO = "";
                                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                                        // AC No end Here

                                        string qryinsert = " insert into dbo.SCSC_Procurement_CSM(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number, Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown, Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id, RecdQty_Faq ,RecdQty_Urs ,RecdBags_JuteNew ,RecdBags_PP,RecdBags_JuteOld,RecdBags_A_twill,Stiching_bags ,Stencile_bags,Moisture,TaulParchi,category,GodownTypeId,Transp_Pancard,Weighbridge_ID,Weighbridge_TaulParchi,Weighbridge_LoadedQty,Weighbridge_EmptyQty ,StackName ,StackNumber  ,BagsWeight_PP  ,BagsWeight_JuteNew   ,BagsWeight_JuteOld  ,BagsWeight_Atwill  ,GrossWeight  ,NetWeight  ,Bags_Nottagged  ,Bags_NotColorCode ) values('" + state + "','" + Dist_Id + "','" + IC_Id + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtchlnno.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'" + Accpt_NO + "','" + mrecddate + "','0'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "'," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + "," + mrecdbagsATwill + " ," + badStiching + "," + BadStelcile + ",'','" + Taulparchi + "','" + Category + "'," + hdfgdntype.Value + ",'" + csmsTransporter + "','" + WeighbridgeID + "','" + Weighbridge_TaulParchi.Text + "','" + Weighbridge_Qty.Text + "','" + Weighbridge_empty.Text + "','" + ddl_StakeNumber.SelectedItem.Text + "','" + ddl_StakeNumber.SelectedValue + "'," + lbl_ppweight.Text + "," + lbl_JuteNewWeight.Text + "," + lbl_JuteOldWeight.Text + "," + lbl_ATwillWeight.Text + "," + txt_GrossWeight.Text + "," + txt_NetWeight.Text + "," + txtbad_Tagread.Text + "," + txt_colorcode.Text + ")";
                                        cmd.CommandText = qryinsert;

                                        string issuid = txtissueId.Text.Trim().ToString();
                                        string socity = ddluparjan.SelectedValue.ToString();

                                        int count = cmd.ExecuteNonQuery();

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                        btnRecptSubmit.Enabled = false;
                                        btnPrint.Enabled = true;

                                        ddlcomdty.Enabled = ddldistpdy.Enabled = ddluparjan.Enabled = ddlbranchwlc.Enabled = ddlgodown.Enabled = false;
                                        // ddluparjan.Items.Clear();
                                        ddlgodown.Items.Clear();
                                        ddlcsms_transp.Items.Clear();

                                        txt_recJutNew.Enabled = txt_recJutOld.Enabled = txt_recPP.Enabled = txtRec_TruckNumber.Enabled = txt_GrossWeight.Enabled = txtbadStiching.Enabled = txtBadStelcile.Enabled = DaintyDate3.Enabled = false;

                                        Label2.Visible = true;
                                        Label2.Text = "Data Is Saved Successfully";

                                        Session["Receipt_ID"] = gatepass;
                                        Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }
                                    catch (Exception ex)
                                    {

                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                        return;
                                    }
                                    finally
                                    {

                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }
                                    }

                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Issue ID Is Already Available On Login IssueCentre....'); </script> ");
                                }
                            }
                            else
                            {
                                string tcc = dsP.Tables[0].Rows[0]["TC_Number"].ToString();
                                string pdat = dsP.Tables[0].Rows[0]["Recd_Date"].ToString();
                                string pdat1 = getdate(pdat);
                                string Tr_Nu = dsP.Tables[0].Rows[0]["Truck_Number"].ToString();
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist...." + tcc + "..,Truck No..." + Tr_Nu + "... Recd_Date..." + pdat1 + "'); </script> ");

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                        return;
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
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        string url = "Print_Proc_CSM2018.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;
    }

    private void Getgdn_Id(string Gdn)
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + Gdn + "' and Remarks = 'Y'";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();


                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void getbranch(string brnchid)
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where BranchId = '" + brnchid + "' order by DepotName");
                da = new SqlDataAdapter(select1, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlbranchwlc.DataSource = ds.Tables[0];
                    ddlbranchwlc.DataTextField = "DepotName";
                    ddlbranchwlc.DataValueField = "BranchId";
                    ddlbranchwlc.DataBind();

                }

                else
                {
                    Getdepo();
                }
            }

            catch
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

    }

    private void GetWhtWeighbridgeName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT   [Weigh_Id] ,[WighBridge_Number] FROM [WeighBridge_Master] where District_Code = '" + Dist_Id + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddl_WBridge.DataSource = ds.Tables[0];
                    ddl_WBridge.DataTextField = "WighBridge_Number";
                    ddl_WBridge.DataValueField = "Weigh_Id";
                    ddl_WBridge.DataBind();
                    ddl_WBridge.Items.Insert(0, "--select--");
                }

                else
                {
                    ddl_WBridge.DataSource = "";
                    ddl_WBridge.DataBind();
                    ddl_WBridge.Items.Insert(0, "--select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    protected void ddldistpdy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddluparjan.Items.Clear();
        ddlsector.Items.Clear();
        hdfCSMS_Comid.Value = hdfgdntype.Value = hdfWeighbridgeID.Value = "";
        Weighbridge_TaulParchi.Enabled = Weighbridge_Qty.Enabled = false;
        pnlgrd.Visible = false;

        dgridchallan.DataSource = null;
        dgridchallan.DataBind();

        ddlpdyTransporter.Items.Clear();
        ddlcsms_transp.Items.Clear();
        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recPP.Text = DaintyDate3.Text = txtRec_TruckNumber.Text = txt_GrossWeight.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = "";
        txtbadStiching.Text = txtBadStelcile.Text = "0";

        if (ddldistpdy.SelectedIndex > 0)
        {
            string comm = ddlcomdty.SelectedValue.ToString();

            if (comm == "6" || comm == "63" || comm == "12")
            {

                getWhtUparjncntr(); //ddldistpdy_SelectedIndexChanged(sender, e);
            }

            //getcsms_Commdty();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sending District'); </script> ");
            return;
        }
    }

    protected void GetStackNum()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string GodownId = ddlgodown.SelectedValue.ToString();

                string comm = ddlcomdty.SelectedValue.ToString();



                string select = string.Format("SELECT  Stack_ID ,Stack_Name FROM tbl_MetaData_STACK where Godown_ID = '" + GodownId + "' and Stack_Killed = 'N' and Commodity_Id = '" + comm + "'");
                SqlDataAdapter da = new SqlDataAdapter(select, con);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_StakeNumber.DataSource = ds.Tables[0];
                        ddl_StakeNumber.DataTextField = "Stack_Name";
                        ddl_StakeNumber.DataValueField = "Stack_ID";
                        ddl_StakeNumber.DataBind();
                        ddl_StakeNumber.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddl_StakeNumber.Items.Insert(0, "--Select--");
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

    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldistpdy.Items.Clear();
        ddluparjan.Items.Clear();
        ddlsector.Items.Clear();
        hdfCSMS_Comid.Value = hdfgdntype.Value = hdfWeighbridgeID.Value = "";
        Weighbridge_TaulParchi.Enabled = Weighbridge_Qty.Enabled = false;
        pnlgrd.Visible = false;

        dgridchallan.DataSource = null;
        dgridchallan.DataBind();

        ddlpdyTransporter.Items.Clear();
        ddlcsms_transp.Items.Clear();
        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recPP.Text = DaintyDate3.Text = txtRec_TruckNumber.Text = txt_GrossWeight.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = "";
        txtbadStiching.Text = txtBadStelcile.Text = "0";

        if (ddlcomdty.SelectedIndex > 0)
        {
            string comm = ddlcomdty.SelectedValue.ToString();

            if (comm == "63")  // CHANA
            {
                comm = "6";

                txt_recATwill.Enabled = false;

                txt_recJutNew.Enabled = true;

                txt_recJutOld.Enabled = true;

                txt_recPP.Enabled = true;

                txt_recJutNew.Text = "0";
                txt_recJutOld.Text = "0";
                txt_recPP.Text = "0";
                txt_recATwill.Text = "0";
            }

            if (comm == "64")   // Masoor
            {
                comm = "63";

                txt_recATwill.Enabled = false;

                txt_recJutNew.Enabled = true;

                txt_recJutOld.Enabled = true;

                txt_recPP.Enabled = true;

                txt_recJutNew.Text = "0";
                txt_recJutOld.Text = "0";
                txt_recPP.Text = "0";
                txt_recATwill.Text = "0";
            }

            if (comm == "33")   // Sarso
            {
                comm = "12";

                txt_recATwill.Enabled = true;

                txt_recJutNew.Enabled = false;

                txt_recJutOld.Enabled = false;

                txt_recPP.Enabled = false;

                txt_recJutNew.Text = "0";
                txt_recJutOld.Text = "0";
                txt_recPP.Text = "0";
                txt_recATwill.Text = "0";
            }

            if (comm == "6" || comm == "63" || comm == "12")
            {
                getDist();
                getWhtUparjncntr(); //ddldistpdy_SelectedIndexChanged(sender, e);
            }

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    protected void btn_calc_Click(object sender, EventArgs e)
    {
        if (txtissueId.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी किया गया ट्रक चालान चुने'); </script> ");
            return;
        }
        using (con = new SqlConnection(Con_CSMS))
        {
            # region Jute_weight
            string BagWeight_juteNew = "SELECT  top(1)[BagsTypeId] ,[BagType] ,[BagsWeight], ValidUpto FROM [CSM_BagsType] where BagsTypeId = '2'  order by ValidUpto desc";

            SqlCommand cmd_bag_juteNew = new SqlCommand(BagWeight_juteNew, con);

            SqlDataAdapter da_bag_juteNew = new SqlDataAdapter(cmd_bag_juteNew);

            DataSet ds_bags_juteNew = new DataSet();

            da_bag_juteNew.Fill(ds_bags_juteNew);

            if (ds_bags_juteNew.Tables[0].Rows.Count > 0)
            {
                Label7.Text = ds_bags_juteNew.Tables[0].Rows[0]["BagsWeight"].ToString();
                // ds_bags_juteNew

            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मुख्यालय से बोरो का वजन नही भरा गया है| एंट्री नही की जा सकती| '); </script> ");
                return;
            }

            # endregion

            # region PP_weight
            string BagWeight_PP = "SELECT  top(1)[BagsTypeId] ,[BagType] ,[BagsWeight], ValidUpto FROM [CSM_BagsType] where BagsTypeId = '1'  order by ValidUpto desc";

            SqlCommand cmd_bag_PP = new SqlCommand(BagWeight_PP, con);

            SqlDataAdapter da_bag_PP = new SqlDataAdapter(cmd_bag_PP);

            DataSet ds_bags_PP = new DataSet();

            da_bag_PP.Fill(ds_bags_PP);

            if (ds_bags_PP.Tables[0].Rows.Count > 0)
            {
                Label3.Text = ds_bags_PP.Tables[0].Rows[0]["BagsWeight"].ToString();
                // ds_bags_PP
            }


            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मुख्यालय से बोरो का वजन नही भरा गया है| एंट्री नही की जा सकती| '); </script> ");
                return;

            }

            # endregion

            # region JuteOld_weight
            string BagWeight_JUTEOld = "SELECT  top(1)[BagsTypeId] ,[BagType] ,[BagsWeight], ValidUpto FROM [CSM_BagsType] where BagsTypeId = '3'  order by ValidUpto desc";

            SqlCommand cmd_bag_JUTEOld = new SqlCommand(BagWeight_JUTEOld, con);

            SqlDataAdapter da_bag_JUTEOld = new SqlDataAdapter(cmd_bag_JUTEOld);

            DataSet ds_bags_JUTEOld = new DataSet();

            da_bag_JUTEOld.Fill(ds_bags_JUTEOld);

            if (ds_bags_JUTEOld.Tables[0].Rows.Count > 0)
            {
                Label5.Text = ds_bags_JUTEOld.Tables[0].Rows[0]["BagsWeight"].ToString();
                // ds_bags_JUTEOld
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मुख्यालय से बोरो का वजन नही भरा गया है| एंट्री नही की जा सकती| '); </script> ");
                return;

            }

            # endregion

            # region A_Twill_weight
            string BagWeight_A_Twill = "SELECT  top(1)[BagsTypeId] ,[BagType] ,[BagsWeight], ValidUpto FROM [CSM_BagsType] where BagsTypeId = '4'  order by ValidUpto desc";

            SqlCommand cmd_bag_A_Twill = new SqlCommand(BagWeight_A_Twill, con);

            SqlDataAdapter da_bag_A_Twill = new SqlDataAdapter(cmd_bag_A_Twill);

            DataSet ds_bags_A_Twill = new DataSet();

            da_bag_A_Twill.Fill(ds_bags_A_Twill);

            if (ds_bags_A_Twill.Tables[0].Rows.Count > 0)
            {
                Label6.Text = ds_bags_A_Twill.Tables[0].Rows[0]["BagsWeight"].ToString();
                // ds_bags_A_Twill
            }

            else
            {

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मुख्यालय से बोरो का वजन नही भरा गया है| एंट्री नही की जा सकती| '); </script> ");
                return;

            }

            # endregion



            if (ddl_WBridge.SelectedItem.Text == "--select--")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('तौल कांटे का नाम चुने'); </script> ");
                return;
            }

            if (Weighbridge_TaulParchi.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('R.S.T. नंबर भरें'); </script> ");
                return;
            }

            if (Weighbridge_empty.Text == "" || Weighbridge_empty.Text == "0")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('खाली गाड़ी का वजन भरें |'); </script> ");
                return;
            }

            if (Weighbridge_Qty.Text == "" || Weighbridge_Qty.Text == "0")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('भरी हुई गाड़ी का वजन भरें |'); </script> ");
                return;
            }

            decimal EmptyTruck = Convert.ToDecimal(Weighbridge_empty.Text);

            decimal LoadedTruck = Convert.ToDecimal(Weighbridge_Qty.Text);

            if (EmptyTruck > LoadedTruck)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('खाली गाडी का वजन भरी हुई गाड़ी से ज्यादा नही हो सकता |'); </script> ");
                return;
            }

            if (EmptyTruck == LoadedTruck)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('भरी हुई गाड़ी का वजन एवं खाली गाडी का वजन सामान नही हो सकता |'); </script> ");
                return;
            }

            if (EmptyTruck == 0 || LoadedTruck == 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('भरी हुई गाड़ी का वजन एवं खाली गाडी का वजन शुन्य नही हो सकता |'); </script> ");
                return;
            }

            decimal calc_grossweight = LoadedTruck - EmptyTruck;

            txt_GrossWeight.Text = Convert.ToString(calc_grossweight);

            // Calc of Bags


            if (txt_recPP.Text == "")
            {
                txt_recPP.Text = "0";
            }

            if (txt_recJutNew.Text == "")
            {
                txt_recJutNew.Text = "0";
            }

            if (txt_recJutOld.Text == "")
            {
                txt_recJutOld.Text = "0";
            }

            if (txt_recATwill.Text == "")
            {
                txt_recATwill.Text = "0";
            }

            if (txt_recPP.Text == "0" && txt_recJutNew.Text == "0" && txt_recJutOld.Text == "0" && txt_recATwill.Text == "0")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया प्राप्त बोरो की संख्या भरें |'); </script> ");
                return;
            }
            txt_recJutNew.ReadOnly = true;

            decimal pp = Convert.ToDecimal(txt_recPP.Text);

            decimal JuteNew = Convert.ToDecimal(txt_recJutNew.Text);

            decimal JuteOld = Convert.ToDecimal(txt_recJutOld.Text);

            decimal Atwill = Convert.ToDecimal(txt_recATwill.Text);

            decimal cal_ppweight = Convert.ToDecimal(Label3.Text);

            cal_ppweight = cal_ppweight / 100000;

            decimal cal_ppNewweight = cal_ppweight * pp;

            lbl_ppweight.Text = Convert.ToString(cal_ppNewweight);


            decimal cal_Juteweight = Convert.ToDecimal(Label7.Text);

            cal_Juteweight = cal_Juteweight / 100000;

            decimal cal_JuteNewweight = cal_Juteweight * JuteNew;

            lbl_JuteNewWeight.Text = Convert.ToString(cal_JuteNewweight);

            /////////////////////
            decimal cal_JuteOldweight = Convert.ToDecimal(Label5.Text);

            cal_JuteOldweight = cal_JuteOldweight / 100000;

            decimal cal_JuteOldweight_newcal = cal_JuteOldweight * JuteOld;

            lbl_JuteOldWeight.Text = Convert.ToString(cal_JuteOldweight_newcal);


            decimal cal_Atwillweight = Convert.ToDecimal(Label6.Text);

            cal_Atwillweight = cal_Atwillweight / 100000;

            decimal Newcal_Atwillweight = cal_Atwillweight * Atwill;

            lbl_ATwillWeight.Text = Convert.ToString(Newcal_Atwillweight);

            decimal sumof_bagweight = cal_ppNewweight + cal_JuteNewweight + cal_JuteOldweight_newcal + Newcal_Atwillweight;

            decimal newNet = calc_grossweight - sumof_bagweight;

            txt_NetWeight.Text = Convert.ToString(newNet);

            if (newNet > 320)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('शुद्ध वजन किसी भी स्थिति में 320 Qtls से ज्यादा नहीं हो सकता ,कृपया जानकारी जाँच ले |'); </script> ");
                return;
            }
        }

    }

    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.Cells[17].Text.Equals("R") || e.Row.Cells[17].Text.Equals("P"))
        {
            e.Row.BackColor = System.Drawing.Color.DarkRed;
            e.Row.ForeColor = System.Drawing.Color.White;
        }
    }
}