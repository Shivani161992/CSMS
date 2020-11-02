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
using Data;
using DataAccess;

public partial class IssueCenter_DeliveryChallan_IssuedCentre : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    public string gatepass = "";
    // string Rates;
    public int getnum;
    SqlDataReader dr;


    int Commodity;

    MoveChallan mobj = null;
    protected Common ComObj = null;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string IC_Id = "", Dist_Id = "", strBranchval = "", strGodownVal = "", strUpdateGodownVal = "", strUpdateBranchVal = "", strCommodity = "", DeliveryChallan = "", DeliveryChallan_MO = "", TypeofBags = "", whr_ID = "", DCDate = "", SubDCDate = "";
    double QtyTotal = 0, QtyRemTotal = 0, strBalBagInGodown = 0, strBalQtyInGodown = 0, StrBalQtyInSendIC = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                Session["DC_MO"] = "";
                Session["CreatedDate"] = "";
                GetCropYear();
                Gettransportnumber();
                GetIC();
               
                // GetBranch();
                Getgodown();
                //Clear();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());


            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
        // txtdatetransportorder.Text = Request.Form[txtdatetransportorder.UniqueID];
        //txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }
    public void GetCropYear()
    {

        ddlCropYear.Items.Insert(0, "--Select--");
        ddlCropYear.SelectedIndex = 0;
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));

        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));

    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTONumber.Items.Clear();
        ddlissuecentre.Items.Clear();
        txtCommodity.Text = txtquantity.Text = txtdatetransportorder.Text = txtdispatchmode.Text = "";
        if (ddlCropYear.SelectedIndex > 0)
        {
            Gettransportnumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
    }

    public void Gettransportnumber()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string IC_Id = Session["issue_id"].ToString();
                string Dist_Id = Session["dist_id"].ToString();
                string select = string.Format("select distinct  DistTransportOrdernum from  PDS_Dist_TransportOrder_Intra_IC where District= '" + Dist_Id + "' and FrmIC='" + IC_Id + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTONumber.DataSource = ds.Tables[0];
                        ddlTONumber.DataTextField = "DistTransportOrdernum";
                        ddlTONumber.DataValueField = "DistTransportOrdernum";
                        ddlTONumber.DataBind();
                        ddlTONumber.Items.Insert(0, "--Select--");
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
    protected void ddlTONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlissuecentre.Items.Clear();
        txtCommodity.Text = txtquantity.Text = txtdatetransportorder.Text = txtdispatchmode.Text = "";
        if (ddlTONumber.SelectedIndex > 0)
        {
            GetIC();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Transport Order Number'); </script> ");
            return;
        }

    }

    public void GetIC()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                //string DistCode = Session["dist_id"].ToString();

                string select = string.Format("select md.DepotID,md.DepotName from PDS_Dist_TransportOrder_Intra_IC dt inner join tbl_MetaData_DEPOT md on md.DepotID=dt.ToIC where DistTransportOrdernum='" + ddlTONumber.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlissuecentre.DataSource = ds.Tables[0];
                        ddlissuecentre.DataTextField = "DepotName";
                        ddlissuecentre.DataValueField = "DepotID";
                        ddlissuecentre.DataBind();
                        ddlissuecentre.Items.Insert(0, "--Select--");
                        gettransportername();
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

    protected void ddlissuecentre_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtCommodity.Text = txtquantity.Text = txtdatetransportorder.Text = txtdispatchmode.Text = "";
        if (ddlissuecentre.SelectedIndex > 0)
        {
            Getdetails();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Issue Centre'); </script> ");
            return;
        }

    }
    public void Getdetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                IC_Id = Session["issue_id"].ToString();
                string select = string.Format("select sa.Source_Name,sc.Commodity_Name, dt.Quantity,convert(varchar(12),dt.CreatedDate,103) as CreatedDate from PDS_Dist_TransportOrder_Intra_IC dt inner join Source_Arrival_Type sa on dt.ModeofDispatch=sa.Source_ID inner join tbl_MetaData_STORAGE_COMMODITY sc on sc.Commodity_Id=dt.Commodity where dt.ToIC='" + ddlissuecentre.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtquantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                        txtdatetransportorder.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                        txtCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                        txtdispatchmode.Text = ds.Tables[0].Rows[0]["Source_Name"].ToString();
                        // Commodity = Convert.ToInt16(ds.Tables[0].Rows[0]["Commodity"].ToString());
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
    public void gettransportername()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string select = string.Format("select distinct Transporter_ID, Transporter_Name from Transporter_Table where GETDATE()<=Valid_Upto and Distt_ID='" + Dist_Id + "' and Transport_ID in ('6', '11', '12')");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 1)
                        {
                            ddltansportername.DataSource = ds.Tables[0];
                            ddltansportername.DataTextField = "Transporter_Name";
                            ddltansportername.DataValueField = "Transporter_ID";
                            ddltansportername.DataBind();
                            ddltansportername.Items.Insert(0, "--Select--");
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please correct validity date of the Transporters'); </script> ");
                            btnRecptSubmit.Enabled = false;
                            return;

                        }
                        else if (ds.Tables[0].Rows.Count == 1)
                        {
                            ddltansportername.DataSource = ds.Tables[0];
                            ddltansportername.DataTextField = "Transporter_Name";
                            ddltansportername.DataValueField = "Transporter_ID";
                            ddltansportername.DataBind();
                            ddltansportername.Items.Insert(0, "--Select--");
                            btnRecptSubmit.Enabled=true;
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
    //public void GetBranch()
    //{
    //    using (con_MPStorage = new SqlConnection(strcon_MPStorage))
    //    {
    //        try
    //        {
    //            con_MPStorage.Open();
    //            string select = string.Format("select BranchID,BranchName from MetaDataBranchWithIssueCenter where IssueCenterId='" + IC_Id + "' and DistrictId='23" + Dist_Id + "'");
    //            da = new SqlDataAdapter(select, con_MPStorage);
    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds != null)
    //            {
    //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlbranch.DataSource = ds.Tables[0];
    //                    ddlbranch.DataTextField = "BranchName";
    //                    ddlbranch.DataValueField = "BranchID";
    //                    ddlbranch.DataBind();
    //                    ddlbranch.Items.Insert(0, "--Select--");
    //                }
    //                else
    //                {
    //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con_MPStorage.State != ConnectionState.Closed)
    //            {
    //                con_MPStorage.Close();
    //            }
    //        }
    //    }
    //}
    public void Getgodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                //and BranchID='" + IC_Id + "'
                con_MPStorage.Open();
                string select = string.Format(" select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + IC_Id + "' and DistrictId='23" + Dist_Id + "'");
                da = new SqlDataAdapter(select, con_MPStorage);
                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlgodown.DataSource = ds.Tables[0];
                        ddlgodown.DataTextField = "Godown_Name";
                        ddlgodown.DataValueField = "Godown_ID";
                        ddlgodown.DataBind();
                        ddlgodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
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

    protected void txtquntityissue_TextChanged(object sender, EventArgs e)
    {
        float strIssuedQty = 0, strQty = 0;
        strIssuedQty = float.Parse(txtquntityissue.Text);
        strQty = float.Parse(txtquantity.Text);
        if (strIssuedQty < strQty)
        {

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मात्रा कम चुने'); </script> ");
            return;
        }
    }





    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/MovementOrderHome.aspx");
    }
    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                Dist_Id = Session["dist_id"].ToString();
                string IC_Id = Session["issue_id"].ToString();
                string selectmax = "select max(Challan_Number) as Challan_Number from tblDeliveryChallanIssuecentre";
                da = new SqlDataAdapter(selectmax, con);
                //ds = new DataSet();
                //da.Fill(ds);
                //if (ds.Tables.Count > 0)
                //{
                //    string IC_DC_id = ds.Tables[0].Rows[0]["IC_DC_id"].ToString();
                //    if (IC_DC_id == "")
                //    {
                //        lblchallanno.Text = IC_DC_id + "1818171801";
                //    }
                //    else
                //    {
                //        lblchallanno.Text = ((double.Parse(IC_DC_id)) + 1).ToString();
                //    }
                //}
                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["Challan_Number"].ToString();

                if (gatepass == "")
                {
                    gatepass = "181718" + "00";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
                }

                string select = "select sc.Commodity_Name,sc.Commodity_Id from PDS_Dist_TransportOrder_Intra_IC dt  inner join tbl_MetaData_STORAGE_COMMODITY sc on sc.Commodity_Id=dt.Commodity  where dt.ToIC='" + ddlissuecentre.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    txtCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();
                }
                ConvertServerDate ServerDate = new ConvertServerDate();
                string IssuedDate = ServerDate.getDate_MDY(txtIssuedDate.Text);

                ConvertServerDate ServerDateDTO = new ConvertServerDate();
                string IssuedDateDTO = ServerDateDTO.getDate_MDY(txtdatetransportorder.Text);
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strselect = "insert into tblDeliveryChallanIssuecentre( Challan_Number,CropYear, Transport_Order, IssuedCentre,Quantity,Date_Of_Transport,Commodity,DispatchMode,TransporterName,QuantityIssue,Issued_No_Bags,Type_of_Bags,Truck_Number,Branch,Godown,IssueDate,District_code,IP_Adress,Sending_issuecentre, CreatedDate, IsReceived) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlTONumber.SelectedValue.ToString() + "','" + ddlissuecentre.SelectedValue.ToString() + "','" + txtquantity.Text + "','" + IssuedDateDTO + "','" + txtCommodity.Text + "','12','" + ddltansportername.SelectedValue.ToString() + "','" + txtquntityissue.Text + "','" + txtIssuedBags.Text + "','" + ddltypeofbags.SelectedValue.ToString() + "','" + txtTCNo.Text + "','0','" + ddlgodown.SelectedValue.ToString() + "','" + IssuedDate + "','" + Dist_Id + "','" + ip + "','" + IC_Id + "', GetDate(),'N')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                btnRecptSubmit.Enabled = false;
                btnPrint.Enabled = true;
                ddlCropYear.Enabled = false;
                ddlTONumber.Enabled = false;
                ddlissuecentre.Enabled = false;
                ddltansportername.Enabled = false;
                ddltypeofbags.Enabled = false;
                txtIssuedBags.Enabled = false;
                txtIssuedDate.Enabled = false;
                txtquntityissue.Enabled = false;
                txtTCNo.Enabled = false;
                ddlgodown.Enabled = false;
                lblchallanno.Visible = true;
                lblchallanno.Text = "Delivery Challan Number is :-" + gatepass;
                Session["DC_IC"] = gatepass;
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print_DeliveryChallan_IssuedCentre.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    public void Clear()
    {
        txtquantity.Text = "";
        txtdatetransportorder.Text = "";
        txtCommodity.Text = "";
        txtdispatchmode.Text = "";
        txtquntityissue.Text = "";
        txtIssuedBags.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        ddlCropYear.ClearSelection();
        ddlTONumber.ClearSelection();
        ddlissuecentre.ClearSelection();
        ddltansportername.ClearSelection();
        ddltypeofbags.ClearSelection();
        // ddlbranch.ClearSelection();
        ddlgodown.ClearSelection();
    }
}

