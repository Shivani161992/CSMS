using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DataAccess;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
//using System.Globalization;



public partial class IssueCenter_Delivered_DoorStep_FPS : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public static string distid = "";

    public static string IssuecenterID;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["issue_id"] != null)
            {
                distid = Session["dist_id"].ToString();

                IssuecenterID = Session["issue_id"].ToString();

                if (!IsPostBack)
                {
                    Session["issubmited"] = "No";   // for stop repeat of Acceptance Note.

                    ddlyear.Items.Add(DateTime.Today.Year.ToString());

                    ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());

                    ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());

                    ddlyear.Items.Insert(0, "--Select--");
                                        
                }
            }

            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void get_DO()
    {
        try
        {
            string qry = "select distinct delivery_order_no from Issued_Doorstep_do_fps where district_code = '" + distid + "' and issueCentre_code = '" + IssuecenterID + "' and allotment_month = '" + ddlmonth.SelectedItem.Value + "' and allotment_year = '" + ddlyear.SelectedItem.Text + "'";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDO.DataSource = ds.Tables[0];
                ddlDO.DataTextField = "delivery_order_no";
                ddlDO.DataValueField = "delivery_order_no";
                ddlDO.DataBind();
                ddlDO.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlDO.DataSource = "";
                
                ddlDO.DataBind();

                GridView1.DataSource = "";

                GridView1.DataBind();
            }
        }

        catch
        {
            
        }


        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
       
    }


    protected void ddlDO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlDO.SelectedValue == "0")
        {

        }

        else
        {
            string qry1 = "select Issued_Doorstep_do_fps.fps_code ,Issued_Doorstep_do_fps.commodity , Issued_Doorstep_do_fps.scheme_id, tbl_MetaData_SCHEME.Scheme_Name ,   isnull(convert(nvarchar,DeliverDate,103),convert(nvarchar,GETDATE(),103))as DelivDate,convert(nvarchar,Issued_Doorstep_do_fps.issue_date,103)issue_date,isnull(Issued_Doorstep_do_fps.DeliverQuantity,0)DeliveredQty ,lift_qty , tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , DoorStep_do_fps.fps_name from Issued_Doorstep_do_fps  inner join tbl_MetaData_STORAGE_COMMODITY on Issued_Doorstep_do_fps.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id inner join DoorStep_do_fps on DoorStep_do_fps.fps_code = Issued_Doorstep_do_fps.fps_code  and DoorStep_do_fps.delivery_order_no = Issued_Doorstep_do_fps.delivery_order_no and DoorStep_do_fps.issueCentre_code = Issued_Doorstep_do_fps.issueCentre_code and Issued_Doorstep_do_fps.commodity = DoorStep_do_fps.commodity and Issued_Doorstep_do_fps.scheme_id = DoorStep_do_fps.scheme_id  inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.Scheme_Id = Issued_Doorstep_do_fps.scheme_id  where Issued_Doorstep_do_fps.delivery_order_no = '" + ddlDO.SelectedValue + "'";

            SqlCommand cmd1 = new SqlCommand(qry1,con);

            SqlDataAdapter da = new SqlDataAdapter(cmd1);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];

                GridView1.DataBind();
            }

            else
            {
                GridView1.DataSource = "";

                GridView1.DataBind();
            }
        
        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    //protected void Cal1_SelectionChanged(object sender, EventArgs e)
    //{
    //    Calendar cal = (Calendar)sender;
    //    TextBox text1 = (TextBox)((GridViewRow)cal.Parent.Parent).FindControl("txtdeldate");

    //    text1.Text = cal.SelectedDate.ToShortDateString();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["issubmited"].ToString() == "Yes")
        {

        }

        else
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            string opid = Session["OperatorId"].ToString();

            
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");
              
              TextBox DeliverDate = (TextBox)gr.FindControl("txtChallanDate");
              TextBox txtdeliverQty = (TextBox)gr.FindControl("txtdelivered");

             if (GchkBx.Checked == true)
              {
                  if (con.State == ConnectionState.Closed)
                  {
                      con.Open();
                  }

                     string FPS_Code = gr.Cells[1].Text;

                     string CommodityID = gr.Cells[10].Text;
                                    
                     string IssueDate = gr.Cells[5].Text;

                     string Issue_Date = getDate_MDY(IssueDate);

                     string lift_qty = gr.Cells[6].Text;

                     string SchemeId = gr.Cells[11].Text;

                     decimal Lifeted_Quantity = Convert.ToDecimal(lift_qty);
                 
                     decimal Deliver_qty = CheckNull(txtdeliverQty.Text);

                     string Delivery_date = DeliverDate.Text;

                     if (Delivery_date == "")
                     {
                         Delivery_date = System.DateTime.Now.ToString("MM/dd/yyyy");
                     }

                     else
                     {
                         Delivery_date = getDate_MDY(DeliverDate.Text);
                     }

                     string deliveryOrder = ddlDO.SelectedValue;

                     decimal chk_LquintyPr = Lifeted_Quantity * 10;
                     decimal chk_Lquinty = chk_LquintyPr / 100;
                     
                     decimal mqty1 = Lifeted_Quantity + chk_Lquinty;

                     if (Deliver_qty >= mqty1)
                     {
                        // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check deliver_Qty it should not be greater than Lift_Qty....'); </script> ");
                     }
                     else
                     {
                         try
                         {


                             string insqry = "Update Issued_Doorstep_do_fps set DeliverQuantity = " + Deliver_qty + "  , DeliverDate = '" + Delivery_date + "', UpdatedDate = getdate(), Recip = '" + ip + "'  where delivery_order_no = '" + deliveryOrder + "' and district_code = '" + distid + "' and issueCentre_code = '" + IssuecenterID + "' and fps_code = '" + FPS_Code + "' and commodity = '" + CommodityID + "' and allotment_month = '" + ddlmonth.SelectedItem.Value + "' and allotment_year = '" + ddlyear.SelectedItem.Value + "' and scheme_id = '"+SchemeId+"' ";
                                     
                                 SqlCommand cmd = new SqlCommand(insqry, con);

                                 int x = cmd.ExecuteNonQuery();
                             
                         }

                         catch
                         {
                            // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Error Occured'); </script> ");
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

               else
               {
                  // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick The Checkbox...'); </script> ");
               }
            }

            
           
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rows Inserted'); </script> ");
          

            Session["issubmited"] = "Yes";
            btnSave.Enabled = false;

        }
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

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            get_DO();
        }
        catch (Exception)
        { 
        
        }
    }

    protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        double sum = 0;

        foreach (GridViewRow row in GridView1.Rows)
        {

            CheckBox cb = (CheckBox)row.FindControl("cbSelectAll");

            if (cb.Checked)
            {

                TextBox tb = (TextBox)(row.FindControl("txtdelivered"));

                double amount = Convert.ToDouble(CheckNull(tb.Text));

                if (amount > 0)
                {
                    tb.ReadOnly = true;

                   
                }

                else
                {
                    tb.ReadOnly = false;
                }

                

            }

        }
       

    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/IssueCenter/Delivered_DoorStep_FPS.aspx");
    }
}
