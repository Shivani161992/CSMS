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
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class District_Deletefrm_Procurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    //public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2013"].ToString());
    //public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2013"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    public static string distid;

    public static string mscheme = "101";

    public static string mcomdtyu;

    SqlCommand cmdWdel = new SqlCommand();
    SqlCommand cmddel = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
           
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (!IsPostBack)
            {
                Recdate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                //Recdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                distid = Session["dist_id"].ToString();

                GetIssueCenter();
                GetCrop();        
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


  }

    protected void GetIssueCenter()
    {
        string issuecentre = "SELECT DepotID ,DepotName FROM tbl_MetaData_DEPOT where DistrictId = '23" + distid + "' ";
        SqlCommand cmd = new SqlCommand(issuecentre, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlissuecenter.DataSource = ds.Tables[0];

        ddlissuecenter.DataTextField = "DepotName";
        ddlissuecenter.DataValueField = "DepotID";
        ddlissuecenter.DataBind();
        ddlissuecenter.Items.Insert(0, "--Select--"); 

    }

    protected void GetCrop()
    {
        string cropname = "SELECT Commodity_Id  ,Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in('22','13','14','8','12','11') order by Commodity_Id desc ";
        SqlCommand cmd = new SqlCommand(cropname, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlcrop.DataSource = ds.Tables[0];

        ddlcrop.DataTextField = "Commodity_Name";
        ddlcrop.DataValueField = "Commodity_Id";
        ddlcrop.DataBind();
        ddlcrop.Items.Insert(0, "--Select--"); 
    }

    protected void ddlIssueID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string data = "SELECT TC_Number ,Truck_Number ,Recd_Bags ,Recd_Qty ,convert(nvarchar,Recd_Date,103)Recd_Date ,GodownName FROM SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedItem.Text + "' and Receipt_Id not in (select IssueID from Acceptance_Note_Detail where Distt_ID = '" + distid + "' and  IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and CommodityId = '" + ddlcrop.SelectedValue + "' )";

        SqlCommand cmddata = new SqlCommand(data, con);

        SqlDataAdapter da = new SqlDataAdapter(cmddata);
        DataSet ds = new DataSet();

        da.Fill(ds);

        if(ds.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = ds.Tables[0];

            GridView2.DataBind();

            lblchallan.Text = ds.Tables[0].Rows[0]["TC_Number"].ToString();
        }

        else
        {
             Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Acceptance Note Issued, Can not Delete This Entry'); </script> ");
             return;          
        }
    }

    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlcrop.SelectedValue == "0" || ddlcrop.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Crop Type'); </script> ");
            return;

        }
        if (Recdate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Date'); </script> ");
            return;
        }
           

        else
        {
            try
          {

            string redate = getDate_MDY(Recdate.Text);

            mcomdtyu = ddlcrop.SelectedValue;

            string ReceiptId = "SELECT Receipt_Id FROM SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "' and Commodity_Id = '" + ddlcrop.SelectedValue + "' and Recd_Date = '" + redate + "' and Acceptance_No = '' ";
            SqlCommand cmd = new SqlCommand(ReceiptId, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddlIssueID.DataSource = ds.Tables[0];

            ddlIssueID.DataTextField = "Receipt_Id";
            ddlIssueID.DataValueField = "Receipt_Id";
            ddlIssueID.DataBind();
            ddlIssueID.Items.Insert(0, "--Select--");
        }

        catch

        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Going Wrong'); </script> ");
        }

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        btnDelete.Enabled = true;
        Response.Redirect("~/District/Deletefrm_Procurement.aspx");
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }


        SqlTransaction trns1;
       

        SqlTransaction trns;

      

        try
        {
            string inslog = "Insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and TC_Number = '" + lblchallan.Text + "' ";

            SqlCommand cmdlog = new SqlCommand(inslog, con);

         int a =   cmdlog.ExecuteNonQuery();

         string delqry = "delete from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and TC_Number = '" + lblchallan.Text + "' ";

         cmddel.CommandText = delqry;

         trns1 = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
         cmddel.Connection = con;

         cmddel.Transaction = trns1;

         int b =  cmddel.ExecuteNonQuery();


          string insWlog = "Insert into IssueCenterReceipt_Online_Log select * from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and DistrictId = '23" + distid + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and TruckChalanNo = '" + lblchallan.Text + "' ";

            SqlCommand cmdWlog = new SqlCommand(insWlog, con_WPMS);

          int x =  cmdWlog.ExecuteNonQuery();


          string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and DistrictId = '23" + distid + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and TruckChalanNo = '" + lblchallan.Text + "' ";

           
            cmdWdel.CommandText = delWqry;

            cmdWdel.Connection = con_WPMS;

            trns = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

            cmdWdel.Transaction = trns;

           int y =  cmdWdel.ExecuteNonQuery();


           if (y > 0)
           {
               trns.Commit();

               trns1.Commit();
           }

           else
           {
               trns.Rollback();

               trns1.Rollback();
           }



            //UpdateCBalance();
            //UpdateStock();

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");
        }

        catch (Exception ex)
        {
           

            lblerror.Text = ex.Message;
            lblerror.Visible = true;

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
            return; 
        }

        finally
        {
            btnDelete.Enabled = false;
            
            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

    }

    //void UpdateStock()
    //{

    //    if (con.State == ConnectionState.Closed)
    //    {
    //        con.Open();
    //    }

    //    string mfyear = DateTime.Today.Year.ToString();
   
    //    int monthu = int.Parse(DateTime.Today.Month.ToString());
    //    int yearu = int.Parse(DateTime.Today.Year.ToString());
    //    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

       
    //    decimal mrfci = CheckNull(lblRecQty.Text);

       
    //    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Procure= convert(decimal(18,5), Recieved_Procure)-" + mrfci + " where Commodity_Id ='" + mcomdtyu + "' and Scheme_ID='" + mscheme + "' and DistrictId='" + distid + "'and DepotID='" + ddlissuecenter.SelectedValue + "'and Month=" + monthu + "and Year=" + yearu;

    //    SqlCommand cmd = new SqlCommand(qryinsU, con);
    //            try
    //            {            
    //                cmd.ExecuteNonQuery();                
    //            }
    //            catch (Exception ex)
    //            {
    //                Label9.Visible = true;
    //                Label9.Text = "error:5" + ex.Message;

    //            }
    //            finally
    //            {
                    

    //            }

    //            if (con.State == ConnectionState.Open)
    //            {
    //                con.Close();
    //            }
            
    //}

    //void UpdateCBalance()
    //{

    //    string query = "Update dbo.issue_opening_balance set Current_Balance = convert(decimal(18,5), Current_Balance)-" + CheckNull(lblRecQty.Text) + ",Current_Bags=Current_Bags-" + lblRecBags.Text + " where District_Id='" + distid + "'and Depotid='" + ddlissuecenter.SelectedValue + "'and Commodity_Id='" + mcomdtyu + "'and Godown='" + lblgdnId.Text + "' and Scheme_Id='" + mscheme + "' and Source='01' ";
    //    SqlCommand cmd = new SqlCommand();       
    //    cmd.CommandText = query;
    //   cmd.Connection = con;

    //            try
    //            {
    //                if (con.State == ConnectionState.Closed)
    //                {
    //                    con.Open();
    //                }
    //                cmd.ExecuteNonQuery();

    //            }
    //            catch (Exception ex)
    //            {
    //                Label9.Visible = true;
    //                Label9.Text = "error:3" + ex.Message;

    //            }
    //            finally
    //            {
    //                if (con.State == ConnectionState.Open)
    //                {
    //                    con.Close();
    //                }                 

    //            }        
     
    //}
      
    
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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    
}
