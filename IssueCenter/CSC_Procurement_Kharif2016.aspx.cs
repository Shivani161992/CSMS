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

public partial class IssueCenter_CSC_Procurement_Kharif2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd, cmd1;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string IC_Id = "", Dist_Id = "";

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string Con_Paddy = ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString(); //PPMS 2016
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString());

    string Con_Maze = ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString(); //MPMS 2016
    public SqlConnection con_maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
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

                txtrec_tcnumber.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtrec_tcnumber.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtrec_tcnumber.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRec_TruckNumber.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRec_TruckNumber.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRec_TruckNumber.Attributes.Add("onchange", "return chksqltxt(this)");

                txtfaq_qty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtfaq_qty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtfaq_qty.Attributes.Add("onchange", "return chksqltxt(this)");

                txtbadStiching.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtbadStiching.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtbadStiching.Attributes.Add("onchange", "return chksqltxt(this)");

                txtBadStelcile.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtBadStelcile.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtBadStelcile.Attributes.Add("onchange", "return chksqltxt(this)");

                txtmoisture.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtmoisture.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtmoisture.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTaulNum.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTaulNum.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTaulNum.Attributes.Add("onchange", "return chksqltxt(this)");

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                GetICName();
                GetCommodity();
                Getdepo();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        DaintyDate3.Text = Request.Form[DaintyDate3.UniqueID];
    }

    private void GetICName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtissue.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
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

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select crop,crpcode from Crop_Master where crpcode IN ('2','3','4','5','6','7')";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlcomdty.DataSource = ds.Tables[0];
                    ddlcomdty.DataTextField = "crop";
                    ddlcomdty.DataValueField = "crpcode";
                    ddlcomdty.DataBind();
                    ddlcomdty.Items.Insert(0, "--Select--");
                    ddlcomdty.SelectedIndex = 1;

                    getpadyDist();
                    getpadyUparjncntr(); //ddldistpdy_SelectedIndexChanged(sender, e);
                    getcsms_Commdty();
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

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recPP.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = txtfaq_qty.Text = txtTaulNum.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = WeighbridgeName.Text = "";
        txtbadStiching.Text = txtBadStelcile.Text = txtmoisture.Text = "0";

        if (ddlcomdty.SelectedIndex > 0)
        {
            if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {
                getpadyDist();
                getpadyUparjncntr(); //ddldistpdy_SelectedIndexChanged(sender, e);
            }
            else
            {
                getMotaAnaajDist();
                getMotaAnaajUparjncntr();
            }

            getcsms_Commdty();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    private void getpadyDist()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select District_Name,District_Code from Districts order by District_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldistpdy.DataSource = ds.Tables[0];
                    ddldistpdy.DataTextField = "District_Name";
                    ddldistpdy.DataValueField = "District_Code";
                    ddldistpdy.DataBind();
                    ddldistpdy.Items.Insert(0, "--Select--");
                    ddldistpdy.Items.FindByValue(23 + Dist_Id).Selected = true;
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

    private void getMotaAnaajDist()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select District_Name,District_Code from Districts order by District_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldistpdy.DataSource = ds.Tables[0];
                    ddldistpdy.DataTextField = "District_Name";
                    ddldistpdy.DataValueField = "District_Code";
                    ddldistpdy.DataBind();
                    ddldistpdy.Items.Insert(0, "--Select--");
                    ddldistpdy.Items.FindByValue(23 + Dist_Id).Selected = true;
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

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recPP.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = txtfaq_qty.Text = txtTaulNum.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = WeighbridgeName.Text = "";
        txtbadStiching.Text = txtBadStelcile.Text = txtmoisture.Text = "0";
        if (ddldistpdy.SelectedIndex > 0)
        {
            if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {
                getpadyUparjncntr();
            }
            else
            {
                getMotaAnaajUparjncntr();
            }

            getcsms_Commdty();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sending District'); </script> ");
            return;
        }
    }

    private void getpadyUparjncntr()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID) as varchar(50)) + ')') as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistpdy.SelectedValue.ToString() + "'  and ic.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online where IssueCenterReceipt_Online.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "')  and ic.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  group by  ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID order by ic.SocietyID";

                da = new SqlDataAdapter(select, con);
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

    private void getMotaAnaajUparjncntr()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID) as varchar(50)) + ')') as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistpdy.SelectedValue.ToString() + "'  and ic.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online where IssueCenterReceipt_Online.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "')  and ic.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  group by  ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID order by ic.SocietyID";
                da = new SqlDataAdapter(select, con);
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

    private void getcsms_Commdty()
    {
        hdfCSMS_Comid.Value = "";
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "SELECT Commodity_Id FROM Procurement_COMMODITY WHERE Proc_Commodity_Id='" + ddlcomdty.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfCSMS_Comid.Value = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();
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

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recPP.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = txtfaq_qty.Text = txtTaulNum.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = WeighbridgeName.Text = "";
        txtbadStiching.Text = txtBadStelcile.Text = txtmoisture.Text = "0";

        if (ddluparjan.SelectedIndex > 0)
        {
            if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {
                getpaddyIssueid();
                getpaddyTranspoter();
                GetSectorData();
            }
            else
            {
                getMotaAnaajIssueid();
                getMotaAnaajpaddyTranspoter();
                GetMotaAnaajSectorData();
            }
            pnlgrd.Visible = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center'); </script> ");
            return;
        }
    }

    private void getpaddyIssueid()
    {
        Dist_Id = ddldistpdy.SelectedValue.ToString().Substring(2, 2);

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106) As DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId , ist.GodownTypeId,ist.Weighbridge_ID from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online where IssueCenterReceipt_Online.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "') and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";

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

    private void getMotaAnaajIssueid()
    {
        Dist_Id = ddldistpdy.SelectedValue.ToString().Substring(2, 2);

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = "";

                select = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106) As DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId , ist.GodownTypeId,ist.Weighbridge_ID from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online where IssueCenterReceipt_Online.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "') and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";

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

    private void getpaddyTranspoter()
    {
        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Transporter_ID,Transporter_Name from TransportMaster where District_ID='" + ddldistpdy.SelectedValue.ToString() + "' and SocietyCode='" + ddluparjan.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlpdyTransporter.DataSource = ds.Tables[0];
                    ddlpdyTransporter.DataTextField = "Transporter_Name";
                    ddlpdyTransporter.DataValueField = "Transporter_ID";
                    ddlpdyTransporter.DataBind();
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

    private void getMotaAnaajpaddyTranspoter()
    {
        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Transporter_ID,Transporter_Name from TransportMaster where District_ID='" + ddldistpdy.SelectedValue.ToString() + "' and SocietyCode='" + ddluparjan.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlpdyTransporter.DataSource = ds.Tables[0];
                    ddlpdyTransporter.DataTextField = "Transporter_Name";
                    ddlpdyTransporter.DataValueField = "Transporter_ID";
                    ddlpdyTransporter.DataBind();
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
                string select = "Select SectorCode from Sectorto_PC_Mapping where PCCode = '" + ddluparjan.SelectedValue.ToString() + "' and cropyear = '" + txtYear.Text + "' and commodity IN('13','14')";
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

    private void GetMotaAnaajSectorData()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "Select SectorCode from Sectorto_PC_Mapping where PCCode = '" + ddluparjan.SelectedValue.ToString() + "' and cropyear = '" + txtYear.Text + "'";
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
        hdfgdntype.Value = hdfWeighbridgeID.Value = "";
        Weighbridge_TaulParchi.Enabled = Weighbridge_Qty.Enabled = false;
        ddlcsms_transp.Items.Clear();
        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = txthhty.Text = txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txt_recJutNew.Text = txt_recJutOld.Text = txt_recPP.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = txtfaq_qty.Text = txtTaulNum.Text = Weighbridge_TaulParchi.Text = Weighbridge_Qty.Text = WeighbridgeName.Text = "";
        txtbadStiching.Text = txtBadStelcile.Text = txtmoisture.Text = "0";

        txtissueId.Text = dgridchallan.SelectedRow.Cells[2].Text;
        txtchlnno.Text = dgridchallan.SelectedRow.Cells[3].Text;
        txttrucknopady.Text = dgridchallan.SelectedRow.Cells[4].Text;
        txtissubag.Text = dgridchallan.SelectedRow.Cells[5].Text;
        txtissueqty.Text = dgridchallan.SelectedRow.Cells[6].Text;
        // ddlpdyTransporter.SelectedItem.Text = dgridchallan.SelectedRow.Cells[7].Text;

        string transpid = dgridchallan.SelectedRow.Cells[8].Text.Trim();

        if (transpid == "" || transpid == "&nbsp;")
        {

        }
        else
        {
            ddlpdyTransporter.SelectedValue = dgridchallan.SelectedRow.Cells[8].Text;
        }


        DaintyDate1P.Text = dgridchallan.SelectedRow.Cells[1].Text;
        txtrec_tcnumber.Text = dgridchallan.SelectedRow.Cells[3].Text;
        txtRec_TruckNumber.Text = dgridchallan.SelectedRow.Cells[4].Text;

        if (txtRec_TruckNumber.Text == "&nbsp;")
        {
            txtRec_TruckNumber.Text = "";
        }

        txtfaq_qty.Text = dgridchallan.SelectedRow.Cells[6].Text;
        txt_recJutNew.Text = dgridchallan.SelectedRow.Cells[9].Text;
        txt_recJutOld.Text = dgridchallan.SelectedRow.Cells[10].Text;
        txt_recPP.Text = dgridchallan.SelectedRow.Cells[11].Text;
        hdfgdntype.Value = dgridchallan.SelectedRow.Cells[12].Text;
        hdfWeighbridgeID.Value = dgridchallan.SelectedRow.Cells[13].Text;

        if (hdfWeighbridgeID.Value != "" && hdfWeighbridgeID.Value != "&nbsp;")
        {
            Weighbridge_TaulParchi.Enabled = Weighbridge_Qty.Enabled = true;
            if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {
                GetPaddyWeighbridgeName();
            }
            else
            {
                GetMotaAnaajWeighbridgeName();
            }
        }
        GetCsmsTransPorter();
    }

    private void GetPaddyWeighbridgeName()
    {
        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Weighbridge_Name From WeighbridgeMaster Where Weighbridge_ID='" + hdfWeighbridgeID.Value + "' and SocietyCode='" + ddluparjan.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    WeighbridgeName.Text = ds.Tables[0].Rows[0]["Weighbridge_Name"].ToString();
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

    private void GetMotaAnaajWeighbridgeName()
    {
        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Weighbridge_Name From WeighbridgeMaster Where Weighbridge_ID='" + hdfWeighbridgeID.Value + "' and SocietyCode='" + ddluparjan.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    WeighbridgeName.Text = ds.Tables[0].Rows[0]["Weighbridge_Name"].ToString();
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

    private void GetCsmsTransPorter()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                string mystring = ddldistpdy.SelectedValue;
                string disttid = mystring.Substring(mystring.Length - 2);

                con.Open();
                string Transport_CSMS = "select distinct Pancard_no,Transporter_Name from Transporter_Table where Transport_ID='8' and Distt_ID='" + disttid + "' and Pancard_no <> '0' and Pancard_no is not null and LRT_proc_secter= '" + ddlsector.SelectedValue + "'";
                da = new SqlDataAdapter(Transport_CSMS, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcsms_transp.DataSource = ds.Tables[0];
                    ddlcsms_transp.DataTextField = "Transporter_Name";
                    ddlcsms_transp.DataValueField = "Pancard_no";
                    ddlcsms_transp.DataBind();
                    ddlcsms_transp.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddlcsms_transp.Items.Clear();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया सेक्टर की खरीद केंद्र से मेपिंग एवं परिवहनकर्ता का पेनकार्ड,जिला कार्यालय द्वारा अनिवार्य रूप से करा ले ,इस के बाद ही एंट्री हो पायेगी   !'); </script> ");
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

    //private void Getdepo()
    //{
    //    using (con = new SqlConnection(Con_WH))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string qrysel = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='23" + Dist_Id + "'";
    //            da = new SqlDataAdapter(qrysel, con);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddlbranchwlc.DataSource = ds.Tables[0];
    //                ddlbranchwlc.DataTextField = "DepotName";
    //                ddlbranchwlc.DataValueField = "BranchId";
    //                ddlbranchwlc.DataBind();
    //                ddlbranchwlc.Items.Insert(0, "--select--");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //            return;
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

    private void Getdepo()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + IC_Id + "'");
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
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
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

    protected void ddlbranchwlc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlgodown.Items.Clear();
        txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = txthhty.Text = "";

        if (txtissueId.Text == "" || DaintyDate1P.Text == "" || txtchlnno.Text == "" || txttrucknopady.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Any Issue ID on Above Grid'); </script> ");
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
            HiredType();
            GetCapacity();
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
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string pqry = "available_space_godown";
                SqlCommand cmdpqty = new SqlCommand(pqry, con);
                cmdpqty.CommandType = CommandType.StoredProcedure;

                cmdpqty.Parameters.Add("@district_code", SqlDbType.VarChar).Value = Dist_Id;
                cmdpqty.Parameters.Add("@Depotid", SqlDbType.VarChar).Value = IC_Id;
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
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (ddluparjan.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center'); </script> ");
            return;
        }
        else if (ddlgodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
        else if (ddlcsms_transp.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Transporter Name'); </script> ");
            return;
        }
        else if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया दिनांक चुने|'); </script> ");
            return;
        }
        else if (txtrec_tcnumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Challan No.'); </script> ");
            return;
        }
        else if (txtRec_TruckNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Truck No.'); </script> ");
            return;
        }
        else if (txtfaq_qty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Recd. Qty'); </script> ");
            return;
        }
        else if (txtTaulNum.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter तौल पर्ची No.'); </script> ");
            return;
        }
        else if (Convert.ToDecimal(txtfaq_qty.Text) <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Received Qty Is Not Allow To 0 or Less Than 0'); </script> ");
            return;
        }
        else if (WeighbridgeName.Text != "" && Weighbridge_TaulParchi.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter तौल पर्ची क्रमांक (RST No.)'); </script> ");
            return;
        }
        else if (WeighbridgeName.Text != "" && Weighbridge_Qty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया तौल पर्ची अनुसार मात्रा की जानकारी भरें|'); </script> ");
            return;
        }
        else
        {
            if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {
                string RecdDate = getDate_MDY(DaintyDate3.Text);
                string DispatchDate = getDate_MDY(DaintyDate1P.Text);

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            Dist_Id = Session["dist_id"].ToString();

                            con.Open();
                            string CheckduplicateRec = "Select * from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Truck_Number = '" + txttrucknopady.Text + "' and Receipt_Id = '" + txtissueId.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                            da = new SqlDataAdapter(CheckduplicateRec, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issue ID Is Already Available'); </script> ");
                                return;
                            }
                            else
                            {
                                if (hdfgdntype.Value == "" || hdfgdntype.Value == "&nbsp;")
                                {
                                    hdfgdntype.Value = "0";
                                }
                                if (con_paddy.State == ConnectionState.Closed)
                                {
                                    con_paddy.Open();
                                }
                                SqlTransaction trns1;
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = con_paddy;
                                trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd1.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                SqlTransaction trns;
                                SqlCommand cmd = new SqlCommand();
                                cmd.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd.Transaction = trns;

                                SqlDataAdapter daP;
                                DataSet dsP = new DataSet();
                                string cmtid = hdfCSMS_Comid.Value;
                                string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                                cmd.CommandText = select;
                                daP = new SqlDataAdapter(cmd);

                                daP.Fill(dsP);

                                if (dsP.Tables[0].Rows.Count == 0)
                                {
                                    string gatepass = "";
                                    string distp = "";

                                    gatepass = txtissueId.Text.Trim().ToString();
                                    distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);
                                    string mpcdist = distp;
                                    string mpcic = ddluparjan.SelectedValue;
                                    string mdispdate = getDate_MDY(DaintyDate1P.Text);
                                    string mchallan = txtchlnno.Text;

                                    string mtruckno_first = txttrucknopady.Text;
                                    Regex re = new Regex("[;\\/:*?\"<>|&']");
                                    string mtruckno = re.Replace(mtruckno_first, " ");

                                    string mtrans = ddlpdyTransporter.SelectedValue.ToString();

                                    string mcomdty = hdfCSMS_Comid.Value;
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
                                    string anst = "N";

                                    string mrecddate = getDate_MDY(DaintyDate3.Text);
                                    string mrecdgdn = ddlgodown.SelectedValue;
                                    string branch = ddlbranchwlc.SelectedValue;
                                    string notrans = "N";

                                    string opid = Session["OperatorId"].ToString();
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    IC_Id = Session["issue_id"].ToString();

                                    string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";

                                    cmd.CommandText = checkrcid;
                                    //cmd.Connection = con;
                                    string str1 = cmd.ExecuteScalar().ToString();

                                    if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                    {
                                        try
                                        {
                                            int mrecdbags = 0;
                                            int mrecdbagsJute = CheckNullInt(txt_recJutNew.Text);
                                            int mrecdbagsPP = CheckNullInt(txt_recPP.Text);
                                            int mrecdbagsJuteOld = CheckNullInt(txt_recJutOld.Text);

                                            mrecdbags = mrecdbagsJute + mrecdbagsPP + mrecdbagsJuteOld;

                                            int badStiching = CheckNullInt(txtbadStiching.Text);

                                            int BadStelcile = CheckNullInt(txtBadStelcile.Text);

                                            decimal mrecdqty = 0;
                                            decimal mrecdqtyFaq = CheckNull(txtfaq_qty.Text);
                                            decimal mrecdqtyUrs = 0;

                                            mrecdqty = mrecdqtyFaq + mrecdqtyUrs;

                                            decimal moisture = CheckNull(txtmoisture.Text.Trim());

                                            string Taulparchi = txtTaulNum.Text.Trim();

                                            string recdGodownName = ddlgodown.SelectedItem.Text;
                                            string Category = "N";

                                            DateTime dispdate = Convert.ToDateTime(DaintyDate1P.Text);

                                            DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate3.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
                                            string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
                                            DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                                            int result = DateTime.Compare(Recdate, dispdate);
                                            int greaterdate = DateTime.Compare(currentdate, Recdate);

                                            string csmsTransporter;
                                            csmsTransporter = ddlcsms_transp.SelectedValue.ToString();

                                            if (greaterdate == -1)
                                            {
                                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not greater than Today Date'); </script> ");
                                                return;
                                            }

                                            string WeighbridgeID = "";
                                            if (hdfWeighbridgeID.Value == "&nbsp;")
                                            {
                                                WeighbridgeID = "";
                                            }
                                            else
                                            {
                                                WeighbridgeID = hdfWeighbridgeID.Value;
                                            }
                                            string qryinsert = "insert into dbo.SCSC_Procurement_Kharif2016(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber,RecdQty_Faq ,RecdQty_Urs ,RecdBags_JuteNew ,RecdBags_PP,RecdBags_JuteOld,Stiching_bags ,Stencile_bags,Moisture,TaulParchi,category,GodownTypeId,Transp_Pancard,Weighbridge_ID,Weighbridge_TaulParchi,Weighbridge_Qty)values('" + state + "','" + Dist_Id + "','" + IC_Id + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'',getdate(),'0'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + "," + badStiching + "," + BadStelcile + "," + moisture + ",'" + Taulparchi + "','" + Category + "'," + hdfgdntype.Value + ",'" + csmsTransporter + "','" + WeighbridgeID + "','" + Weighbridge_TaulParchi.Text + "','" + Weighbridge_Qty.Text + "')";
                                            cmd.CommandText = qryinsert;

                                            string issuid = txtissueId.Text.Trim().ToString();
                                            string socity = ddluparjan.SelectedValue.ToString();
                                            string str = " Select * from IssueToSangrahanaKendra where IssueID='" + issuid + "' and SocietyID='" + socity + "' and CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and TruckChalanNo='" + txtchlnno.Text + "' ";
                                            cmd1.CommandText = str;
                                            SqlDataAdapter daP1 = new SqlDataAdapter(cmd1);
                                            DataSet dsP1 = new DataSet();
                                            daP1.Fill(dsP1);
                                            string Issuid = dsP1.Tables[0].Rows[0]["IssueID"].ToString();
                                            string disid = dsP1.Tables[0].Rows[0]["DistrictId"].ToString();
                                            string Socid = dsP1.Tables[0].Rows[0]["SocietyID"].ToString();
                                            string Crpyr = dsP1.Tables[0].Rows[0]["CropYear"].ToString();
                                            string mrktson = dsP1.Tables[0].Rows[0]["MarketingSeasonId"].ToString();
                                            string issuedt2 = dsP1.Tables[0].Rows[0]["DateOfIssue"].ToString();

                                            string comdty = dsP1.Tables[0].Rows[0]["CommodityId"].ToString();
                                            string bags = dsP1.Tables[0].Rows[0]["Bags"].ToString();
                                            string qty = dsP1.Tables[0].Rows[0]["QtyTransffer"].ToString();
                                            string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();
                                            string taulnumber = "0";   // added on 22/07 to remove wrong taulpatrak number

                                            int SlashPos = taulptrk.IndexOf("'");

                                            if (SlashPos > 0)
                                            {
                                                taulnumber = taulptrk.Substring(0, taulptrk.IndexOf("'"));
                                            }

                                            else
                                            {
                                                taulnumber = taulptrk;
                                            }

                                            string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                            string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                            string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();

                                            string udate = "";
                                            string status = "N";
                                            string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "' and CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and TruckChalanNo='" + txtchlnno.Text + "' ";
                                            cmd1.CommandText = checkpre;
                                            string str12 = cmd1.ExecuteScalar().ToString();

                                            if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                            {
                                                string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld)  VALUES('" + Issuid + "','23" + Dist_Id + "','" + IC_Id + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulnumber + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + ")";
                                                cmd1.CommandText = inserttotest;
                                                int x = cmd1.ExecuteNonQuery();
                                            }

                                            int count = cmd.ExecuteNonQuery();

                                            if (count >= 1)
                                            {
                                                trns1.Commit();
                                                trns.Commit();

                                                //UpdateStock();
                                                //UpdateCBalance();

                                                if (con_paddy.State == ConnectionState.Open)
                                                {
                                                    con_paddy.Close();
                                                }

                                                if (con.State == ConnectionState.Open)
                                                {
                                                    con.Close();
                                                }

                                            }

                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                            btnRecptSubmit.Enabled = false;
                                            btnPrint.Enabled = true;

                                            ddlcomdty.Enabled = ddldistpdy.Enabled = ddluparjan.Enabled = ddlbranchwlc.Enabled = ddlgodown.Enabled = false;
                                            ddluparjan.Items.Clear();
                                            ddlgodown.Items.Clear();
                                            ddlcsms_transp.Items.Clear();

                                            txt_recJutNew.Enabled = txt_recJutOld.Enabled = txt_recPP.Enabled = txtrec_tcnumber.Enabled = txtRec_TruckNumber.Enabled = txtfaq_qty.Enabled = txtTaulNum.Enabled = txtbadStiching.Enabled = txtBadStelcile.Enabled = txtmoisture.Enabled = DaintyDate3.Enabled = false;
                                            txtmoisture.Text = "";

                                            Label2.Visible = true;
                                            Label2.Text = "Data Is Saved Successfully";

                                            Session["Receipt_ID"] = gatepass;
                                            Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();

                                            //Update_Trans_Log(gatepass);
                                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            trns1.Rollback();
                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                            return;
                                        }
                                        finally
                                        {
                                            if (con_paddy.State == ConnectionState.Open)
                                            {
                                                con_paddy.Close();
                                            }

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
                            Dist_Id = Session["dist_id"].ToString();

                            con.Open();
                            string CheckduplicateRec = "Select * from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Truck_Number = '" + txttrucknopady.Text + "' and Receipt_Id = '" + txtissueId.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                            da = new SqlDataAdapter(CheckduplicateRec, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issue ID Is Already Available'); </script> ");
                                return;
                            }
                            else
                            {
                                if (hdfgdntype.Value == "" || hdfgdntype.Value == "&nbsp;")
                                {
                                    hdfgdntype.Value = "0";
                                }
                                if (con_maze.State == ConnectionState.Closed)
                                {
                                    con_maze.Open();
                                }
                                SqlTransaction trns1;
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = con_maze;
                                trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd1.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                SqlTransaction trns;
                                SqlCommand cmd = new SqlCommand();
                                cmd.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd.Transaction = trns;

                                SqlDataAdapter daP;
                                DataSet dsP = new DataSet();
                                string cmtid = hdfCSMS_Comid.Value;
                                string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                                cmd.CommandText = select;
                                daP = new SqlDataAdapter(cmd);

                                daP.Fill(dsP);

                                if (dsP.Tables[0].Rows.Count == 0)
                                {
                                    string gatepass = "";
                                    string distp = "";

                                    gatepass = txtissueId.Text.Trim().ToString();
                                    distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);
                                    string mpcdist = distp;
                                    string mpcic = ddluparjan.SelectedValue;
                                    string mdispdate = getDate_MDY(DaintyDate1P.Text);
                                    string mchallan = txtchlnno.Text;

                                    string mtruckno_first = txttrucknopady.Text;
                                    Regex re = new Regex("[;\\/:*?\"<>|&']");
                                    string mtruckno = re.Replace(mtruckno_first, " ");

                                    string mtrans = ddlpdyTransporter.SelectedValue.ToString();

                                    string mcomdty = hdfCSMS_Comid.Value;
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
                                    string anst = "N";

                                    string mrecddate = getDate_MDY(DaintyDate3.Text);
                                    string mrecdgdn = ddlgodown.SelectedValue;
                                    string branch = ddlbranchwlc.SelectedValue;
                                    string notrans = "N";

                                    string opid = Session["OperatorId"].ToString();
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    IC_Id = Session["issue_id"].ToString();

                                    string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";

                                    cmd.CommandText = checkrcid;
                                    //cmd.Connection = con;
                                    string str1 = cmd.ExecuteScalar().ToString();

                                    if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                    {
                                        try
                                        {
                                            int mrecdbags = 0;
                                            int mrecdbagsJute = CheckNullInt(txt_recJutNew.Text);
                                            int mrecdbagsPP = CheckNullInt(txt_recPP.Text);
                                            int mrecdbagsJuteOld = CheckNullInt(txt_recJutOld.Text);

                                            mrecdbags = mrecdbagsJute + mrecdbagsPP + mrecdbagsJuteOld;

                                            int badStiching = CheckNullInt(txtbadStiching.Text);

                                            int BadStelcile = CheckNullInt(txtBadStelcile.Text);

                                            decimal mrecdqty = 0;
                                            decimal mrecdqtyFaq = CheckNull(txtfaq_qty.Text);
                                            decimal mrecdqtyUrs = 0;

                                            mrecdqty = mrecdqtyFaq + mrecdqtyUrs;

                                            decimal moisture = CheckNull(txtmoisture.Text.Trim());

                                            string Taulparchi = txtTaulNum.Text.Trim();

                                            string recdGodownName = ddlgodown.SelectedItem.Text;
                                            string Category = "N";

                                            DateTime dispdate = Convert.ToDateTime(DaintyDate1P.Text);

                                            DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate3.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
                                            string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
                                            DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                                            int result = DateTime.Compare(Recdate, dispdate);
                                            int greaterdate = DateTime.Compare(currentdate, Recdate);

                                            string csmsTransporter;
                                            csmsTransporter = ddlcsms_transp.SelectedValue.ToString();

                                            if (greaterdate == -1)
                                            {
                                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not greater than Today Date'); </script> ");
                                                return;
                                            }

                                            string WeighbridgeID = "";
                                            if (hdfWeighbridgeID.Value == "&nbsp;")
                                            {
                                                WeighbridgeID = "";
                                            }
                                            else
                                            {
                                                WeighbridgeID = hdfWeighbridgeID.Value;
                                            }
                                            string qryinsert = "insert into dbo.SCSC_Procurement_Kharif2016(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber,RecdQty_Faq ,RecdQty_Urs ,RecdBags_JuteNew ,RecdBags_PP,RecdBags_JuteOld,Stiching_bags ,Stencile_bags,Moisture,TaulParchi,category,GodownTypeId,Transp_Pancard,Weighbridge_ID,Weighbridge_TaulParchi,Weighbridge_Qty)values('" + state + "','" + Dist_Id + "','" + IC_Id + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'',getdate(),'0'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + mrecdgdn + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + branch + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + "," + badStiching + "," + BadStelcile + "," + moisture + ",'" + Taulparchi + "','" + Category + "'," + hdfgdntype.Value + ",'" + csmsTransporter + "','" + WeighbridgeID + "','" + Weighbridge_TaulParchi.Text + "','" + Weighbridge_Qty.Text + "')";
                                            cmd.CommandText = qryinsert;

                                            string issuid = txtissueId.Text.Trim().ToString();
                                            string socity = ddluparjan.SelectedValue.ToString();
                                            string str = " Select * from IssueToSangrahanaKendra where IssueID='" + issuid + "' and SocietyID='" + socity + "' and CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and TruckChalanNo='" + txtchlnno.Text + "' ";
                                            cmd1.CommandText = str;
                                            SqlDataAdapter daP1 = new SqlDataAdapter(cmd1);
                                            DataSet dsP1 = new DataSet();
                                            daP1.Fill(dsP1);
                                            string Issuid = dsP1.Tables[0].Rows[0]["IssueID"].ToString();
                                            string disid = dsP1.Tables[0].Rows[0]["DistrictId"].ToString();
                                            string Socid = dsP1.Tables[0].Rows[0]["SocietyID"].ToString();
                                            string Crpyr = dsP1.Tables[0].Rows[0]["CropYear"].ToString();
                                            string mrktson = dsP1.Tables[0].Rows[0]["MarketingSeasonId"].ToString();
                                            string issuedt2 = dsP1.Tables[0].Rows[0]["DateOfIssue"].ToString();

                                            string comdty = dsP1.Tables[0].Rows[0]["CommodityId"].ToString();
                                            string bags = dsP1.Tables[0].Rows[0]["Bags"].ToString();
                                            string qty = dsP1.Tables[0].Rows[0]["QtyTransffer"].ToString();
                                            string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();
                                            string taulnumber = "0";   // added on 22/07 to remove wrong taulpatrak number

                                            int SlashPos = taulptrk.IndexOf("'");

                                            if (SlashPos > 0)
                                            {
                                                taulnumber = taulptrk.Substring(0, taulptrk.IndexOf("'"));
                                            }

                                            else
                                            {
                                                taulnumber = taulptrk;
                                            }

                                            string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                            string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                            string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();

                                            string udate = "";
                                            string status = "N";
                                            string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "' and CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and TruckChalanNo='" + txtchlnno.Text + "' ";
                                            cmd1.CommandText = checkpre;
                                            string str12 = cmd1.ExecuteScalar().ToString();

                                            if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                            {
                                                string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld)  VALUES('" + Issuid + "','23" + Dist_Id + "','" + IC_Id + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulnumber + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mrecdqty + "','" + mrecdgdn + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + branch + "','" + mrecdbags + "',''," + mrecdqtyFaq + "," + mrecdqtyUrs + "," + mrecdbagsJute + "," + mrecdbagsPP + "," + mrecdbagsJuteOld + ")";
                                                cmd1.CommandText = inserttotest;
                                                int x = cmd1.ExecuteNonQuery();
                                            }

                                            int count = cmd.ExecuteNonQuery();

                                            if (count >= 1)
                                            {
                                                trns1.Commit();
                                                trns.Commit();

                                                //UpdateStock();
                                                //UpdateCBalance();

                                                if (con_maze.State == ConnectionState.Open)
                                                {
                                                    con_maze.Close();
                                                }

                                                if (con.State == ConnectionState.Open)
                                                {
                                                    con.Close();
                                                }

                                            }

                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                            btnRecptSubmit.Enabled = false;
                                            btnPrint.Enabled = true;

                                            ddlcomdty.Enabled = ddldistpdy.Enabled = ddluparjan.Enabled = ddlbranchwlc.Enabled = ddlgodown.Enabled = false;
                                            ddluparjan.Items.Clear();
                                            ddlgodown.Items.Clear();
                                            ddlcsms_transp.Items.Clear();

                                            txt_recJutNew.Enabled = txt_recJutOld.Enabled = txt_recPP.Enabled = txtrec_tcnumber.Enabled = txtRec_TruckNumber.Enabled = txtfaq_qty.Enabled = txtTaulNum.Enabled = txtbadStiching.Enabled = txtBadStelcile.Enabled = txtmoisture.Enabled = DaintyDate3.Enabled = false;
                                            txtmoisture.Text = "";

                                            Label2.Visible = true;
                                            Label2.Text = "Data Is Saved Successfully";

                                            Session["Receipt_ID"] = gatepass;
                                            Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();

                                            //Update_Trans_Log(gatepass);
                                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            trns1.Rollback();
                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                            return;
                                        }
                                        finally
                                        {
                                            if (con_maze.State == ConnectionState.Open)
                                            {
                                                con_maze.Close();
                                            }

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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        string url = "Print_Proc_Kharif2016.aspx";
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

}