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

public partial class IssueCenter_RejProc_Pdy2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string IC_Id = "", Dist_Id = "", distp = "";

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

                txtreason.Attributes.Add("maxlength", txtreason.MaxLength.ToString());

                txtreason.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtreason.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtreason.Attributes.Add("onchange", "return chksqltxt(this)");

                DaintyDate3.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

                txtrec_tcnumber.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtrec_tcnumber.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtrec_tcnumber.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRec_TruckNumber.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRec_TruckNumber.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRec_TruckNumber.Attributes.Add("onchange", "return chksqltxt(this)");

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
        hdfCSMS_Comid.Value = hdfgdntype.Value = "";

        pnlgrd.Visible = false;

        dgridchallan.DataSource = null;
        dgridchallan.DataBind();

        ddlpdyTransporter.Items.Clear();

        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = "";

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
        hdfCSMS_Comid.Value = hdfgdntype.Value = "";

        pnlgrd.Visible = false;

        dgridchallan.DataSource = null;
        dgridchallan.DataBind();

        ddlpdyTransporter.Items.Clear();

        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = "";
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
        hdfgdntype.Value = "";
        pnlgrd.Visible = false;

        dgridchallan.DataSource = null;
        dgridchallan.DataBind();

        ddlpdyTransporter.Items.Clear();
        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = "";

        if (ddluparjan.SelectedIndex > 0)
        {
            if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {
                getpaddyIssueid();
                getpaddyTranspoter();
            }
            else
            {
                getMotaAnaajIssueid();
                getMotaAnaajpaddyTranspoter();
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

                select = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106) As DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId , ist.GodownTypeId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online where IssueCenterReceipt_Online.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "') and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";
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

                select = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106) As DateOfIssue,ist.Bags,ist.QtyTransffer, ist.JutBag,ist.Jut_OldBag, isnull(ist.HDPEBag,0)HDPEBag,tm.Transporter_Name,ist.TransporterId , ist.GodownTypeId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online where IssueCenterReceipt_Online.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "') and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "'  order by DateOfIssue desc ";

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

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlgodown.Items.Clear();
        ddlbranchwlc.SelectedIndex = 0;

        hdfgdntype.Value = "";

        txtissueId.Text = DaintyDate1P.Text = txtchlnno.Text = txttrucknopady.Text = txtissubag.Text = txtissueqty.Text = DaintyDate3.Text = txtrec_tcnumber.Text = txtRec_TruckNumber.Text = "";

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
        hdfgdntype.Value = dgridchallan.SelectedRow.Cells[12].Text;

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
        else if (chk_brightness.Checked == false && chk_damaged.Checked == false && chk_extra.Checked == false && chk_faq.Checked == false && chk_partially.Checked == false && chk_splited.Checked == false && chk_moist.Checked == false && txtreason.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('अस्विकृति का कोई एक कारण चुने |'); </script> ");
            return;
        }
        else
        {
            if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            Dist_Id = Session["dist_id"].ToString();
                            IC_Id = Session["issue_id"].ToString();
                            string PurchaseCenter_Name = ddluparjan.SelectedItem.Text;

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            string tcnum = txtchlnno.Text;
                            string trucknumber = txttrucknopady.Text;
                            string recdate = getDate_MDY(DaintyDate3.Text);
                            string issueid = txtissueId.Text;

                            string CheckduplicateRec = "Select * from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Truck_Number = '" + txttrucknopady.Text + "' and Receipt_Id = '" + txtissueId.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);
                            SqlDataReader drduplicate;
                            drduplicate = cmdduplirec.ExecuteReader();

                            if (drduplicate.Read())
                            {
                                return;
                            }
                            else
                            {
                                drduplicate.Close();
                                SqlCommand cmd1 = new SqlCommand();

                                if (con_paddy.State == ConnectionState.Closed)
                                {
                                    con_paddy.Open();
                                }

                                SqlTransaction trns1;
                                cmd1.Connection = con_paddy;
                                trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd1.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlTransaction trns;
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd.Transaction = trns;
                                SqlDataAdapter daP;
                                DataSet dsP = new DataSet();

                                string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                                cmd.CommandText = select;
                                daP = new SqlDataAdapter(cmd);
                                daP.Fill(dsP);

                                if (dsP.Tables[0].Rows.Count == 0)
                                {
                                    string gatepass = "";
                                    gatepass = txtissueId.Text.Trim().ToString();
                                    distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);
                                    string mpcdist = distp;
                                    string mpcic = ddluparjan.SelectedValue;
                                    string mdispdate = getDate_MDY(DaintyDate1P.Text);
                                    string mchallan = txtchlnno.Text;
                                    string mtruckno = txttrucknopady.Text;
                                    string mtrans = ddlpdyTransporter.SelectedValue;
                                    string mcomdty = hdfCSMS_Comid.Value;
                                    string mcropy = txtYear.Text;
                                    int mbags = CheckNullInt(txtissubag.Text);
                                    decimal mqty = CheckNull(txtissueqty.Text);
                                    string macno = "";
                                    //string macdate = getDate_MDY(DaintyDate2.Text);
                                    string mstatus = "N";
                                    string mudate = "";
                                    string mddate = "";
                                    string mfyear = DateTime.Today.Year.ToString();
                                    string mbookno = "Rejected";
                                    //string accpno = mfyear + mbookno + txtaccptno.Text;
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                    string state = Session["State_Id"].ToString();
                                    string anst = "N";
                                    string mrecddate = getDate_MDY(DaintyDate3.Text);
                                    string opid = Session["OperatorId"].ToString();
                                    string notrans = "N";

                                    DateTime dispdate = Convert.ToDateTime(DaintyDate1P.Text);
                                    DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate3.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
                                    string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
                                    DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                                    int result = DateTime.Compare(Recdate, dispdate);

                                    if (result == -1)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Reject Date Will Not Less Than Dispatch Date...'); </script> ");
                                        return;
                                    }

                                    int greaterdate = DateTime.Compare(currentdate, Recdate);

                                    if (greaterdate == -1)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Reject Date Will Not Greater Than Today Date...'); </script> ");
                                        return;
                                    }
                                    else
                                    {
                                        string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                                        cmd.CommandText = checkrcid;
                                        string str1 = cmd.ExecuteScalar().ToString();

                                        if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                        {
                                            try
                                            {
                                                string value_brightness = "0";
                                                string value_damaged = "0";
                                                string value_extra = "0";
                                                string value_faq = "0";
                                                string value_partially = "0";
                                                string value_splited = "0";
                                                string value_moist = "0";

                                                if (chk_brightness.Checked)
                                                {
                                                    value_brightness = "1";
                                                }
                                                if (chk_damaged.Checked)
                                                {
                                                    value_damaged = "1";
                                                }
                                                if (chk_extra.Checked)
                                                {
                                                    value_extra = "1";
                                                }
                                                if (chk_faq.Checked)
                                                {
                                                    value_faq = "1";
                                                }
                                                if (chk_partially.Checked)
                                                {
                                                    value_partially = "1";
                                                }
                                                if (chk_splited.Checked)
                                                {
                                                    value_splited = "1";
                                                }
                                                if (chk_moist.Checked)
                                                {
                                                    value_moist = "1";
                                                }
                                                if (txtRec_TruckNumber.Text == "&nbsp;")
                                                {
                                                    txtRec_TruckNumber.Text = "";
                                                }

                                                string qryinsert = "insert into dbo.SCSC_Procurement_Kharif2016(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber)values('" + state + "','" + Dist_Id + "','" + IC_Id + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'" + macno + "',getdate(),'" + mbookno + "','','','" + mrecddate + "','" + ddlgodown.SelectedValue.ToString() + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + ddlbranchwlc.SelectedValue.ToString() + "','')";
                                                cmd.CommandText = qryinsert;

                                                //txtissueId.Text.Trim().ToString();
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
                                                string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                                string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                                string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();

                                                if (truckno == "&nbsp;")
                                                {
                                                    truckno = "";
                                                }
                                                string udate = "";
                                                string status = "N";
                                                string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "' and CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and TruckChalanNo='" + txtchlnno.Text + "' ";
                                                cmd1.CommandText = checkpre;
                                                string str12 = cmd1.ExecuteScalar().ToString();

                                                if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                                {
                                                    string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber)  VALUES('" + Issuid + "','23" + Dist_Id + "','" + IC_Id + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulptrk + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','','Rejected','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','','','')";
                                                    cmd1.CommandText = inserttotest;
                                                    int x = cmd1.ExecuteNonQuery();
                                                }

                                                int count = cmd.ExecuteNonQuery();

                                                if (count >= 1)
                                                {
                                                    trns1.Commit();
                                                    trns.Commit();

                                                    string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent,Created_Date) values ('" + Dist_Id + "','" + IC_Id + "','" + Issuid + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + ",getdate() )";
                                                    SqlCommand cmd_rej = new SqlCommand(insrej, con);
                                                    int xx = cmd_rej.ExecuteNonQuery();
                                                    con.Close();

                                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Truck Is Rejected Successfully....'); </script> ");
                                                    btnRecptSubmit.Enabled = false;
                                                    btnPrint.Enabled = true;

                                                    Update_Trans_Log(gatepass);
                                                    ddlcomdty.Enabled = ddldistpdy.Enabled = ddluparjan.Enabled = false;
                                                    ddluparjan.Items.Clear();
                                                    ddlgodown.Items.Clear();

                                                    txtrec_tcnumber.Enabled = txtRec_TruckNumber.Enabled = DaintyDate3.Enabled = ddlbranchwlc.Enabled = ddlgodown.Enabled = false;
                                                    Label2.Visible = true;
                                                    Label2.Text = "Truck Is Rejected Successfully";

                                                    Session["Receipt_ID"] = gatepass;
                                                    Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();

                                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                                }
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
                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again....'); </script> ");
                                        }
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
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            Dist_Id = Session["dist_id"].ToString();
                            IC_Id = Session["issue_id"].ToString();
                            string PurchaseCenter_Name = ddluparjan.SelectedItem.Text;

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            string tcnum = txtchlnno.Text;
                            string trucknumber = txttrucknopady.Text;
                            string recdate = getDate_MDY(DaintyDate3.Text);
                            string issueid = txtissueId.Text;

                            string CheckduplicateRec = "Select * from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Truck_Number = '" + txttrucknopady.Text + "' and Receipt_Id = '" + txtissueId.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);
                            SqlDataReader drduplicate;
                            drduplicate = cmdduplirec.ExecuteReader();

                            if (drduplicate.Read())
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issue ID Is Already Available'); </script> ");
                                return;
                            }
                            else
                            {
                                drduplicate.Close();
                                SqlCommand cmd1 = new SqlCommand();

                                if (con_maze.State == ConnectionState.Closed)
                                {
                                    con_maze.Open();
                                }

                                SqlTransaction trns1;
                                cmd1.Connection = con_maze;
                                trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd1.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlTransaction trns;
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd.Transaction = trns;
                                SqlDataAdapter daP;
                                DataSet dsP = new DataSet();

                                string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";

                                cmd.CommandText = select;
                                daP = new SqlDataAdapter(cmd);
                                daP.Fill(dsP);

                                if (dsP.Tables[0].Rows.Count == 0)
                                {
                                    string gatepass = "";
                                    gatepass = txtissueId.Text.Trim().ToString();
                                    distp = ddldistpdy.SelectedValue.ToString().Substring(2, 2);
                                    string mpcdist = distp;
                                    string mpcic = ddluparjan.SelectedValue;
                                    string mdispdate = getDate_MDY(DaintyDate1P.Text);
                                    string mchallan = txtchlnno.Text;
                                    string mtruckno = txttrucknopady.Text;
                                    string mtrans = ddlpdyTransporter.SelectedValue;
                                    string mcomdty = hdfCSMS_Comid.Value;
                                    string mcropy = txtYear.Text;
                                    int mbags = CheckNullInt(txtissubag.Text);
                                    decimal mqty = CheckNull(txtissueqty.Text);
                                    string macno = "";
                                    //string macdate = getDate_MDY(DaintyDate2.Text);
                                    string mstatus = "N";
                                    string mudate = "";
                                    string mddate = "";
                                    string mfyear = DateTime.Today.Year.ToString();
                                    string mbookno = "Rejected";
                                    //string accpno = mfyear + mbookno + txtaccptno.Text;
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                    string state = Session["State_Id"].ToString();
                                    string anst = "N";
                                    string mrecddate = getDate_MDY(DaintyDate3.Text);
                                    string opid = Session["OperatorId"].ToString();
                                    string notrans = "N";

                                    DateTime dispdate = Convert.ToDateTime(DaintyDate1P.Text);
                                    DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate3.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
                                    string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
                                    DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                                    int result = DateTime.Compare(Recdate, dispdate);

                                    if (result == -1)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Reject Date Will Not Less Than Dispatch Date...'); </script> ");
                                        return;
                                    }

                                    int greaterdate = DateTime.Compare(currentdate, Recdate);

                                    if (greaterdate == -1)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Reject Date Will Not Greater Than Today Date...'); </script> ");
                                        return;
                                    }
                                    else
                                    {
                                        string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement_Kharif2016 where Distt_ID='" + Dist_Id + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                                        cmd.CommandText = checkrcid;
                                        string str1 = cmd.ExecuteScalar().ToString();

                                        if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                        {
                                            try
                                            {
                                                string value_brightness = "0";
                                                string value_damaged = "0";
                                                string value_extra = "0";
                                                string value_faq = "0";
                                                string value_partially = "0";
                                                string value_splited = "0";
                                                string value_moist = "0";

                                                if (chk_brightness.Checked)
                                                {
                                                    value_brightness = "1";
                                                }
                                                if (chk_damaged.Checked)
                                                {
                                                    value_damaged = "1";
                                                }
                                                if (chk_extra.Checked)
                                                {
                                                    value_extra = "1";
                                                }
                                                if (chk_faq.Checked)
                                                {
                                                    value_faq = "1";
                                                }
                                                if (chk_partially.Checked)
                                                {
                                                    value_partially = "1";
                                                }
                                                if (chk_splited.Checked)
                                                {
                                                    value_splited = "1";
                                                }
                                                if (chk_moist.Checked)
                                                {
                                                    value_moist = "1";
                                                }
                                                if (txtRec_TruckNumber.Text == "&nbsp;")
                                                {
                                                    txtRec_TruckNumber.Text = "";
                                                }

                                                string qryinsert = "insert into dbo.SCSC_Procurement_Kharif2016(State_Id,Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,Branch_Id,RackNumber)values('" + state + "','" + Dist_Id + "','" + IC_Id + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mtrans + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'" + macno + "',getdate(),'" + mbookno + "','','','" + mrecddate + "','" + ddlgodown.SelectedValue.ToString() + "','" + gatepass + "'," + month + "," + mfyear + ",'" + mstatus + "',getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','" + ddlbranchwlc.SelectedValue.ToString() + "','')";
                                                cmd.CommandText = qryinsert;

                                                //txtissueId.Text.Trim().ToString();
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
                                                string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                                string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                                string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();

                                                if (truckno == "&nbsp;")
                                                {
                                                    truckno = "";
                                                }
                                                string udate = "";
                                                string status = "N";
                                                string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "' and CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and TruckChalanNo='" + txtchlnno.Text + "' ";
                                                cmd1.CommandText = checkpre;
                                                string str12 = cmd1.ExecuteScalar().ToString();

                                                if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                                {
                                                    string inserttotest = "INSERT INTO [IssueCenterReceipt_Online]([IssueID] ,[DistrictId],[IssueCenter_ID],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Recd_Godown],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Branch_Id],[Recd_Bags],RackNumber)  VALUES('" + Issuid + "','23" + Dist_Id + "','" + IC_Id + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulptrk + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','','Rejected','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','','','')";
                                                    cmd1.CommandText = inserttotest;
                                                    int x = cmd1.ExecuteNonQuery();
                                                }

                                                int count = cmd.ExecuteNonQuery();

                                                if (count >= 1)
                                                {
                                                    trns1.Commit();
                                                    trns.Commit();

                                                    string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent,Created_Date) values ('" + Dist_Id + "','" + IC_Id + "','" + Issuid + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + ",getdate() )";
                                                    SqlCommand cmd_rej = new SqlCommand(insrej, con);
                                                    int xx = cmd_rej.ExecuteNonQuery();
                                                    con.Close();

                                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Truck Is Rejected Successfully....'); </script> ");
                                                    btnRecptSubmit.Enabled = false;
                                                    btnPrint.Enabled = true;

                                                    Update_Trans_Log(gatepass);
                                                    ddlcomdty.Enabled = ddldistpdy.Enabled = ddluparjan.Enabled = false;
                                                    ddluparjan.Items.Clear();
                                                    ddlgodown.Items.Clear();
                                                    txtrec_tcnumber.Enabled = txtRec_TruckNumber.Enabled = DaintyDate3.Enabled = ddlbranchwlc.Enabled = ddlgodown.Enabled = false;
                                                    Label2.Visible = true;
                                                    Label2.Text = "Truck Is Rejected Successfully";

                                                    Session["Receipt_ID"] = gatepass;
                                                    Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();

                                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                                }
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
                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again....'); </script> ");
                                        }
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

    void Update_Trans_Log(string GPASS)
    {
        Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        int my3month = int.Parse(DateTime.Today.Month.ToString());
        int my3year = int.Parse(DateTime.Today.Year.ToString());
        string mdispdate1 = getDate_MDY(DaintyDate1P.Text);
        string mrecddate1 = getDate_MDY(DaintyDate3.Text);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qryinsert = "insert into dbo.SCSC_Procurement_Kharif2016_Log(Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Recd_Bags ,Recd_Qty ,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Updates_Date,IP_Address,Operation,Branch_Id,Book_No)values('" + Dist_Id + "','" + IC_Id + "','" + distp + "','" + ddluparjan.SelectedValue + "','" + mdispdate1 + "','" + txtchlnno.Text + "','" + txttrucknopady.Text + "','" + ddlpdyTransporter.SelectedValue + "','" + hdfCSMS_Comid.Value + "','" + txtYear.Text + "'," + CheckNullInt(txtissubag.Text) + "," + CheckNull(txtissueqty.Text) + ",'','','" + mrecddate1 + "','"+ ddlgodown.SelectedValue.ToString() +"','" + GPASS + "'," + my3month + "," + my3year + ",getdate(),'" + ip + "','I','" + ddlbranchwlc.SelectedValue.ToString() + "','Rejected')";
        cmd = new SqlCommand();
        cmd.CommandText = qryinsert;
        cmd.Connection = con;

        try
        {
            cmd.ExecuteNonQuery();
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

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        string url = "PrintRejProc_Pdy2016.aspx";
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

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;
    }

    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
}