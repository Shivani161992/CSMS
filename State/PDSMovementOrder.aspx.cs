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
using DataAccess;

public partial class State_PDSMovementOrder : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd, cmd1, cmd2;
    SqlDataAdapter da;
    DataSet ds;

    double QtyTotal = 0, ConvertMtToQtls = 0;
    string smsToDistCode = "", smsFrmDistCode = "", GetDistContactNo = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                GetCropYear();

               // rdbJUTE.Checked = true;

                GetDistName();
                GetCommodity();
                GetSource();

                Session["fdjfhxncdfh"] = null;
                Session["MovmtOrderNo"] = null;
                ViewState["Row"] = "0";

                rdbSelf.Checked = true;
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

            txtDate.Text = Request.Form[txtDate.UniqueID];
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp Order By district_name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist.DataSource = ddlToDist.DataSource = ds.Tables[0];
                        ddlFrmDist.DataTextField = ddlToDist.DataTextField = "district_name";
                        ddlFrmDist.DataValueField = ddlToDist.DataValueField = "district_code";
                        ddlFrmDist.DataBind();
                        ddlToDist.DataBind();
                        ddlFrmDist.Items.Insert(0, "--Select--");
                        ddlToDist.Items.Insert(0, "--Select--");
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

    public void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Commodity_Id, Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (3,13,22,12,25,11) order by Commodity_Name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCommodity.DataSource = ds.Tables[0];
                        ddlCommodity.DataTextField = "Commodity_Name";
                        ddlCommodity.DataValueField = "Commodity_Id";
                        ddlCommodity.DataBind();
                        ddlCommodity.Items.Insert(0, "--Select--");
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

    public void GetSource()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Source_ID , Source_Name from  Source_Arrival_Type where Source_ID in (12,13) order by Source_Name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlComdtyMode.DataSource = ds.Tables[0];
                        ddlComdtyMode.DataTextField = "Source_Name";
                        ddlComdtyMode.DataValueField = "Source_ID";
                        ddlComdtyMode.DataBind();
                        ddlComdtyMode.Items.Insert(0, "--Select--");
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

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/MovementOrderHome.aspx");
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlToDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select To District'); </script> ");
            return;
        }
        else if (ddlCommodity.SelectedValue.ToString()=="25")
        {
         if (ddlbagstype.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
        }

        if (ddlFrmDist.SelectedIndex > 0 && ddlToDist.SelectedIndex > 0 && txtQty.Text != "" && ddlCommodity.SelectedIndex > 0 && ddlComdtyMode.SelectedIndex > 0)
        {
            calid.Visible = false;
            ddlCropYear.Enabled = ddlCommodity.Enabled = ddlComdtyMode.Enabled = ddlFrmDist.Enabled = false;

            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("fromdisttext");
                dt.Columns.Add("fromdistval");
                dt.Columns.Add("todisttext");
                dt.Columns.Add("todistval");
                dt.Columns.Add("quantity");
            }
            DataRow dr = dt.NewRow();
            dr["fromdisttext"] = ddlFrmDist.SelectedItem.Text;
            dr["fromdistval"] = ddlFrmDist.SelectedValue;
            dr["todisttext"] = ddlToDist.SelectedItem.Text;
            dr["todistval"] = ddlToDist.SelectedValue;
            ddlToDist.Items.FindByValue(ddlToDist.SelectedValue.ToString()).Enabled = ddlToDist.Items.FindByValue(ddlFrmDist.SelectedValue.ToString()).Enabled = false;

            dr["quantity"] = (float.Parse(txtQty.Text)).ToString("0.00");
            dt.Rows.Add(dr);
            Session["fdjfhxncdfh"] = dt;
            fillgrid();

            ddlToDist.SelectedIndex = 0;
            txtQty.Text = "";
            btnRecptSubmit.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter All Value'); </script> ");
        }
    }

    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["fdjfhxncdfh"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.Columns[3].HeaderText = ddlComdtyMode.SelectedItem.ToString();
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[3].Text));

            if (e.Row.Cells[0].Text == "15")
            {
                btnAdd.Enabled = false;
                ViewState["Row"] = "15";
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('You Have Alloted All 15 Dist. & You Can Not Add Another Dist. In Same MO'); </script> ");
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (ddlCommodity.SelectedValue.ToString() == "25")
            {
                e.Row.Cells[3].Text = "Total Qty (In Bales) = " + QtyTotal.ToString("0.00");
            }
            else
            {
                e.Row.Cells[3].Text = "Total Qty (In MT) = " + QtyTotal.ToString("0.00");
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("fromdisttext");
            dt.Columns.Add("fromdistval");
            dt.Columns.Add("todisttext");
            dt.Columns.Add("todistval");
            dt.Columns.Add("quantity");
        }
        else
        {
            string DistRowValue = dt.Rows[e.RowIndex]["todistval"].ToString();
            ddlToDist.Items.FindByValue(DistRowValue).Enabled = true;
            dt.Rows.RemoveAt(e.RowIndex);

            if (ViewState["Row"].ToString() == "15")
            {
                btnAdd.Enabled = true;
                ViewState["Row"] = "0";
            }

        }
        Session["fdjfhxncdfh"] = dt;
        fillgrid();
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (ddlFrmDist.SelectedIndex > 0)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        string frmdis = ddlFrmDist.SelectedValue;

                        Int16 commodity = Convert.ToInt16(ddlCommodity.SelectedValue);

                        string mode = ddlComdtyMode.SelectedValue;

                        string year_do = "";

                        if (ddlCropYear.SelectedIndex == 0)
                        {
                            year_do = System.DateTime.Now.Date.ToString("yy");
                            year_do = ((int.Parse(year_do)) - 1).ToString();
                        }
                        else if (ddlCropYear.SelectedIndex == 2)
                        {
                            year_do = System.DateTime.Now.Date.ToString("yy");
                            year_do = ((int.Parse(year_do)) + 1).ToString();
                        }
                        else
                        {
                            year_do = System.DateTime.Now.Date.ToString("yy");
                        }

                        string month = System.DateTime.Now.Date.ToString("MM");
                        string CurrentYear = System.DateTime.Now.Date.ToString("yy");

                        string selectmax = "select max(cast(MoveOrdernum as bigint)) as MoveOrdernum from StateMovementOrder where FrmDist='" + frmdis + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                        cmd = new SqlCommand(selectmax, con);
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds);

                        string whr_ID = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();

                        if (whr_ID == "")
                        {
                            whr_ID = CurrentYear + year_do + month + frmdis + "100";
                        }
                        else
                        {
                            string wid = whr_ID.Substring(whr_ID.Length - 4);

                            Int64 wid_ID_new = Convert.ToInt64(wid);

                            wid_ID_new = wid_ID_new + 1;

                            string combine = wid_ID_new.ToString();

                            whr_ID = CurrentYear + year_do + month + frmdis + combine;
                        }

                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        string browser = Request.Browser.Browser.ToString();
                        string version = Request.Browser.Version.ToString();
                        string useragent = browser + version;

                        ConvertServerDate ServerDate = new ConvertServerDate();
                        string ConvertEndDate = ServerDate.getDate_MDY(txtDate.Text);

                        DateTime EndDate = DateTime.Parse(ConvertEndDate);
                        string smsEndDate = EndDate.ToString("dd-MMM-yyyy");

                        string instr = "", smsToDist = "", modeofDist = "";

                        if (mode == "13")
                        {
                            if (rdbSelf.Checked)
                            {
                                modeofDist = "Self";
                            }
                            else if (rdbOther.Checked)
                            {
                                modeofDist = "Other";
                            }
                            else
                            {
                                modeofDist = "Both";
                            }
                        }

                        DataTable dt = adddetails();
                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ConvertMtToQtls = 0;
                                string SMO = whr_ID + (i + 1);

                                if (commodity == 25)
                                {
                                    ConvertMtToQtls = ((double.Parse(dt.Rows[i]["quantity"].ToString())));
                                }
                                else
                                {
                                    ConvertMtToQtls = ((double.Parse(dt.Rows[i]["quantity"].ToString())) * 10);
                                }

                                if (mode == "12") //Transfer By Road
                                {
                                    instr += "Insert into StateMovementOrder (MoveOrdernum ,FrmDist,Commodity, CropYear,ReachDate,ModeofDispatch,IsIssued,CreatedDate,IP,SMS_todist,SMS_frmDist,ToDist,Quantity,SMO,RecAgainstHO,DispatchAgainstMO,ReceivedAgainstMO,RemQty,IsAccepted ) Values ('" + whr_ID + "','" + frmdis + "','" + commodity + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ConvertEndDate + "','" + mode + "','N',GETDATE(),'" + GetIp + "','N','N','" + dt.Rows[i]["todistval"] + "','" + ConvertMtToQtls + "','" + SMO + "','N','N','N','" + ConvertMtToQtls + "','F');";
                                }
                                else
                                {
                                    if (commodity == 25)
                                    {
                                        //string strGunnyType = "";

                                        //if (rdbJUTE.Checked)
                                        //{
                                        //    strGunnyType = "JUTE";
                                        //}
                                        //else
                                        //{
                                        //    strGunnyType = "PP";
                                        //}
                                        instr += "Insert into StateMovementOrder (MoveOrdernum ,FrmDist,Commodity, CropYear,ReachDate,ModeofDispatch,IsIssued,CreatedDate,IP,SMS_todist,SMS_frmDist,ToDist,Quantity,SMO,RecAgainstHO,DispatchAgainstMO,ReceivedAgainstMO,RemQty,ModeofDist,IsAccepted,GunnyType) Values ('" + whr_ID + "','" + frmdis + "','" + commodity + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ConvertEndDate + "','" + mode + "','N',GETDATE(),'" + GetIp + "','N','N','" + dt.Rows[i]["todistval"] + "','" + ConvertMtToQtls + "','" + SMO + "','Y','N','N','" + ConvertMtToQtls + "','" + modeofDist + "','F','" + ddlbagstype.SelectedValue.ToString() + "');";
                                    }
                                    else
                                    {
                                        instr += "Insert into StateMovementOrder (MoveOrdernum ,FrmDist,Commodity, CropYear,ReachDate,ModeofDispatch,IsIssued,CreatedDate,IP,SMS_todist,SMS_frmDist,ToDist,Quantity,SMO,RecAgainstHO,DispatchAgainstMO,ReceivedAgainstMO,RemQty,ModeofDist,IsAccepted) Values ('" + whr_ID + "','" + frmdis + "','" + commodity + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ConvertEndDate + "','" + mode + "','N',GETDATE(),'" + GetIp + "','N','N','" + dt.Rows[i]["todistval"] + "','" + ConvertMtToQtls + "','" + SMO + "','Y','N','N','" + ConvertMtToQtls + "','" + modeofDist + "','F');";
                                    }
                                }

                                //Code For SMS
                                //smsToDist += ((smsToDist == "") ? "" : " , ") + "'" + dt.Rows[i]["todisttext"] + "' = '" + dt.Rows[i]["quantity"] + "'";
                                //smsToDistCode += ((smsToDistCode == "") ? "" : ",") + "'" + dt.Rows[i]["todistval"] + "'";
                            }
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = btnAdd.Enabled = false;
                            btnPrint.Enabled = true;
                            Session["fdjfhxncdfh"] = null;
                            Session["MovmtOrderNo"] = whr_ID;

                            Label2.Text = "Your Movement Order Number Is : " + whr_ID;
                            Label2.Visible = true;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully & Your Movement Order Number Is : " + whr_ID + "'); </script> ");

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                            if (mode == "13")
                            {
                                if (modeofDist != "Self")
                                {
                                    btnPrint.Enabled = false;
                                    LinkbtnSubMO.Visible = true;
                                }
                            }

                            //Code For SMS
                            //cmd1 = new SqlCommand("Select GETDATE()", con);
                            //DateTime CurrentDate = DateTime.Parse((cmd1.ExecuteScalar()).ToString());
                            //string smsCurrentDate = CurrentDate.ToString("dd-MMM-yyyy");

                            //String strSMS = "Movement Order Issued On '" + smsCurrentDate + "' with MO Number='" + whr_ID + "' For '" + ddlCommodity.SelectedItem.ToString() + "', '" + ddlComdtyMode.SelectedItem.ToString() + "' From '" + ddlFrmDist.SelectedItem.ToString() + "' To (" + smsToDist + ")MT With End Date'" + smsEndDate + "'";
                            //SMS Message = new SMS();

                            //smsToDistCode += ",'" + frmdis + "'";
                            //string FindDistContactNo = "select DM_Mobile,RM_Mobile From officers_list where District_code in (" + smsToDistCode + ")";
                            //da = new SqlDataAdapter(FindDistContactNo, con);
                            //ds = new DataSet();
                            //da.Fill(ds);
                            //string CheckDuplicate = "";

                            //if (ds != null)
                            //{
                            //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            //    {
                            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            //        {
                            //            string checkLength = ds.Tables[0].Rows[i]["DM_Mobile"].ToString();
                            //            string checkLengthRM = ds.Tables[0].Rows[i]["RM_Mobile"].ToString();
                            //            if (checkLength.Length == 10)
                            //            {
                            //                Message.SendSMS(checkLength, strSMS);
                            //            }
                            //            if (checkLengthRM.Length == 10)
                            //            {
                            //                if (checkLength != checkLengthRM)
                            //                {
                            //                    if (checkLengthRM != CheckDuplicate)
                            //                    {
                            //                        Message.SendSMS(checkLength, strSMS);
                            //                        CheckDuplicate = checkLengthRM;
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
                            ddlFrmDist.SelectedIndex = 0;
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["Acpt/Rjct"] = "Pending".ToString();
        string url = "Print_MovementOrder.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void ddlComdtyMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        rowRdb.Visible = false;
        if (ddlComdtyMode.SelectedIndex > 0)
        {
            if (ddlComdtyMode.SelectedValue == "13") //By Rack
            {
                rowRdb.Visible = true;
            }
            else
            {
                rowRdb.Visible = false;
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mode of Dispatch'); </script> ");
        }
    }

    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlComdtyMode.SelectedIndex = 0;
        if (ddlCommodity.SelectedValue.ToString() == "25")
        {
            lblMT.Text = "(In Bales)";
            rowBags.Visible = true;
            GetBagsType();
            ddlComdtyMode.Items.FindByValue("12").Enabled = false;
            ddlFrmDist.Enabled = false;
            ddlFrmDist.SelectedValue = "99";
        }
        else
        {
            lblMT.Text = "(In MT)";
            rowBags.Visible = false;
            ddlComdtyMode.Items.FindByValue("12").Enabled = true;
            ddlFrmDist.Enabled = true;
            ddlFrmDist.SelectedValue = "--Select--";
        }
    }
    public void GetBagsType()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format(" select Bag_Type_ID, BagType from FIN_Bag_Type where Bag_Type_ID!='4' order by BagType desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlbagstype.DataSource = ds.Tables[0];
                    ddlbagstype.DataTextField = "BagType";
                    ddlbagstype.DataValueField = "Bag_Type_ID";
                    ddlbagstype.DataBind();
                    ddlbagstype.Items.Insert(0, "--Select--");
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