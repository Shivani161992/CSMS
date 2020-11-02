using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_DepositerForm_ProcCSM2018 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd, cmd1;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string IC_Id = "", Dist_Id = "";
    decimal aceqty = 0;
    Int32 accbags = 0;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["WHR_Request"] = "";
                Session["Commodity"] = "";

                txtissue.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtissue.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtissue.Attributes.Add("onchange", "return chksqltxt(this)");

                txtYear.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtYear.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtYear.Attributes.Add("onchange", "return chksqltxt(this)");

                DaintyDate3.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                GetICName();
                GetCommodity();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetICName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select DepotName,(CONVERT(VARCHAR,GETDATE(), 105)) As AcptDate from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtissue.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    DaintyDate3.Text = ds.Tables[0].Rows[0]["AcptDate"].ToString();
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
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT Commodity_Name ,Commodity_Id FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in ('33','63','64') Order By Commodity_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommodtiy.DataSource = ds.Tables[0];
                    ddlcommodtiy.DataTextField = "Commodity_Name";
                    ddlcommodtiy.DataValueField = "Commodity_Id";
                    ddlcommodtiy.DataBind();
                    ddlcommodtiy.Items.Insert(0, "--Select--");

                    //ddlcommodtiy.SelectedIndex = 5;
                    //GetGodown();
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

    protected void ddlcommodtiy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlgodown.Items.Clear();
        hdfGodownCode.Value = "";

        GridView2.DataSource = null;
        GridView2.DataBind();

        if (ddlcommodtiy.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlcommodtiy.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
                return;
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();

                btnRecptSubmit.Enabled = false;

                if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
                {
                    bindgrid();
                }

                //GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    private void GetGodown()
    {
        hdfGodownCode.Value = "";

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                string pdate = getDate_MDY(DaintyDate3.Text);
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                string select = "";
                select = "SELECT distinct Recd_Godown FROM SCSC_Procurement_CSM where convert(varchar(10),Acceptance_Date,101) = '" + pdate + "' and AN_Status = 'Y' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Book_No!='Rejected' and SCSC_Procurement_CSM.TC_Number not in (select TC_Number from DepositerForm_CSM2018 where DepositerForm_CSM2018.IssueCenter_ID = '" + IC_Id + "')";
                con.Open();
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfGodownCode.Value += ((hdfGodownCode.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["Recd_Godown"].ToString() + "'";
                    }

                    if (hdfGodownCode.Value != "")
                    {
                        GetGodownName();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Note नहीं बनाये गए है अथवा Acceptance Note का Depositer Form जारी किया जा चूका है, अतः गोदाम के नाम उपलब्ध नहीं है|'); </script> ");
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

    private void GetGodownName()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = "select Godown_Name,Godown_ID from tbl_MetaData_GODOWN where Godown_ID IN (" + hdfGodownCode.Value + ")";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, "--Select--");
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

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        btnRecptSubmit.Enabled = false;

        if (ddlgodown.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlgodown.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
                return;
            }
            else
            {
                if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
                {
                    bindgrid();
                }

            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
            return;
        }
    }

    private void bindgrid()
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                IC_Id = Session["issue_id"].ToString();
                string pdate = getDate_MDY(DaintyDate3.Text);

                con.Open();
                string qrydata = "";

                if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
                {
                   // qrydata = "SELECT Society_MSP.Society_Id , Society_MSP.Society_Name + ' ( '+ Society_MSP.SocPlace + ' )' as Society ,SCSC_Procurement_CSM.TC_Number , SCSC_Procurement_CSM.NetWeight , SCSC_Procurement_CSM.Recd_Bags ,SCSC_Procurement_CSM.Acceptance_No, SCSC_Procurement_CSM.Receipt_Id  FROM SCSC_Procurement_CSM inner join Society_MSP on Society_MSP.Society_Id = SCSC_Procurement_CSM.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue.ToString() + "' and convert(varchar(10),Acceptance_Date,101) = '" + pdate + "' and SCSC_Procurement_CSM.IssueCenter_ID = '" + IC_Id + "' and SCSC_Procurement_CSM.TC_Number not in (select TC_Number from DepositerForm_CSM2018 where DepositerForm_CSM2018.IssueCenter_ID = '" + IC_Id + "') ";
                    qrydata = "SELECT Society_MSP.Society_Id , Society_MSP.Society_Name + ' ( '+ Society_MSP.SocPlace + ' )' as Society ,SCSC_Procurement_CSM.TC_Number , SCSC_Procurement_CSM.NetWeight , SCSC_Procurement_CSM.Recd_Bags ,SCSC_Procurement_CSM.Acceptance_No, SCSC_Procurement_CSM.Receipt_Id  FROM SCSC_Procurement_CSM inner join Society_MSP on Society_MSP.Society_Id = SCSC_Procurement_CSM.Purchase_Center  where  convert(varchar(10),Acceptance_Date,101) = '" + pdate + "' and SCSC_Procurement_CSM.IssueCenter_ID = '" + IC_Id + "' and SCSC_Procurement_CSM.TC_Number not in (select TC_Number from DepositerForm_CSM2018 where DepositerForm_CSM2018.IssueCenter_ID = '" + IC_Id + "') ";

                }

                da = new SqlDataAdapter(qrydata, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnRecptSubmit.Enabled = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                }
                else
                {
                    btnRecptSubmit.Enabled = false;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Not Available On This Date'); </script> ");
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
    
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            aceqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetWeight"));
            accbags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Recd_Bags"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblqty1 = (Label)e.Row.FindControl("lbl_acqt");
            lblqty1.Text = aceqty.ToString();

            Label lblricebag1 = (Label)e.Row.FindControl("lbl_acbag");
            lblricebag1.Text = accbags.ToString();
        }
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (ddlcommodtiy.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else if (ddlgodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
            return;
        }
        else if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
            return;
        }
        
        else
        {
           
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            IC_Id = Session["issue_id"].ToString();
                            Dist_Id = Session["dist_id"].ToString();
                            string Accpt_NO = "";

                            SqlCommand cmdacno = new SqlCommand();
                            cmdacno.Parameters.Clear();
                            cmdacno.Parameters.AddWithValue("@District_ID", Dist_Id);
                            cmdacno.Parameters.AddWithValue("@IssueCenter_ID", IC_Id);
                            cmdacno.Connection = con;

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            cmdacno.CommandType = CommandType.StoredProcedure;
                            cmdacno.CommandText = "getMAxDepositer_CSM18";

                            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());
                            string whr_ID = Accpt_NO;

                            foreach (GridViewRow gr in GridView2.Rows)
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                string acceptdate = getDate_MDY(DaintyDate3.Text);
                                string commname = ddlcommodtiy.SelectedValue;
                                string godown = ddlgodown.SelectedValue;
                                string socid = gr.Cells[0].Text;

                                Label lbltc = (Label)gr.FindControl("lbltc");
                                string TCnum = lbltc.Text;

                                Label txtrcq = (Label)gr.FindControl("lblacqt");
                                double accepqty = Convert.ToDouble(txtrcq.Text);

                                Label txtrcB = (Label)gr.FindControl("lblacbag");
                                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                                string AcptNum = gr.Cells[5].Text;

                                string ReceiptId = gr.Cells[6].Text;


                                int month = int.Parse(DateTime.Today.Month.ToString());
                                int year = int.Parse(DateTime.Today.Year.ToString());
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string opid = Session["OperatorId"].ToString();

                                string commid = ddlcommodtiy.SelectedValue;
                                string GodownId = ddlgodown.SelectedValue;

                                SqlCommand cmd_con = new SqlCommand();
                                cmd_con.Connection = con;
                               

                                string updatecsms = "Insert into DepositerForm_CSM2018 (Distt_ID,IssueCenter_ID,Purchase_Center,TC_Number,Acceptance_No,Month,Year,Created_Date,IP_Address,OperatorID,Accept_Qty,Acpt_Bags ,CommodityId ,IssueID ,godown ,WHR_Request,CropYear) Values ('"+Dist_Id+"','"+IC_Id+"','"+socid+"','"+TCnum+"' , '"+AcptNum+"' , "+month+" , "+year+" , getdate() , '"+ip+"' , '"+opid+"' , "+accepqty+" , "+accepbags+" , '"+commid+"' , '"+ReceiptId+"' , '"+GodownId+"',"+Accpt_NO+",'"+txtYear.Text+"') ";
                                cmd_con.CommandText = updatecsms;

                                int y = cmd_con.ExecuteNonQuery();

                                if (y > 0)
                                {
                                 
                                        try
                                        {

                                            if (con.State == ConnectionState.Open)
                                            {
                                                con.Close();
                                            }

                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Proviosal Data Saved Successfully And Your WHR Request Number is " + whr_ID + "'); </script> ");

                                            btnRecptSubmit.Enabled = false;
                                            btnPrint.Enabled = true;

                                            ddlcommodtiy.Enabled = ddlgodown.Enabled = false;

                                            Label2.Visible = true;
                                            Label2.Text = "Data Saved Successfully and Your Proviosal Depositer Form Number is '" + whr_ID + "' ";

                                            Session["WHR_Request"] = whr_ID;
                                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                        }

                                        catch (Exception ex)
                                        {
                                            btnRecptSubmit.Enabled = false;

                                          
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
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }


                                    btnRecptSubmit.Enabled = false;

                                   
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
                            ddlcommodtiy.Items.Clear();
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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        string url = "PrintWHRRequest_CSM2018.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
}
