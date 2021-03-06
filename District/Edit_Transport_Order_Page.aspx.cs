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
public partial class District_Edit_Transport_Order_Page : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    LARO obj = null;
    LARO objo = null;
    public string distid = "";
    public string sid = "";
    string roqty = null;
    MoveChallan mobj = null;
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

           
            txtroqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");           
            txtcumlqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtbalqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

           
            txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();           
            txtcumlqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbalqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcommodity.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtscheme.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrodate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
              
           


            //txtchallan.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtqtysend.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");



            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                
                GetRO();
                GetName();
              
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }

    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdistrict.Text = dr1dt["district_name"].ToString();
        txtdistrict.ReadOnly = true;
       
       
       
    }

    void GetRO()
    {
       ddlrono.Items.Insert(0, "--Select--");
       string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "' and RO_No not like 'NoRO%'";
        cmd.Connection = con;
        cmd.CommandText = qry;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrono.Items.Add(dr["RO_No"].ToString());



        }
        dr.Close();
        con.Close();

    }



  
    void GetData()
    {
        if (ddlrono.SelectedItem.Text != "--Select--")
        {
            obj = new LARO(ComObj);
            string qryall = "SELECT RO_of_FCI.Commodity AS Expr1,Transport_Order_againstRo.Cumulative_Qty as Cumulative_Qty , RO_of_FCI.Distt_Id, RO_of_FCI.RO_No, RO_of_FCI.RO_Validity, RO_of_FCI.RO_date, RO_of_FCI.RO_qty,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date, RO_of_FCI.Balance_Qty,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name  From dbo.RO_of_FCI Left JOIN tbl_MetaData_STORAGE_COMMODITY ON RO_of_FCI.Commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transport_Order_againstRo on RO_of_FCI.RO_No=Transport_Order_againstRo.RO_No left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=tbl_MetaData_SCHEME.Scheme_id  where RO_of_FCI.RO_No='" + ddlrono.SelectedItem + "' and RO_of_FCI.Distt_Id='" + distid + "'";
            DataSet ds = obj.selectAny(qryall);
            DataRow dr = ds.Tables[0].Rows[0];

            string rdate = dr["RO_Validity"].ToString();
            string rodate = getdate(rdate);
            txtrodate.Text = rodate;
            txtrodate.ReadOnly = true;
            //txtrodate.BackColor = System.Drawing.Color.Wheat;

            roqty = dr["RO_qty"].ToString();
            txtroqty.Text = dr["RO_qty"].ToString();
            txtroqty.ReadOnly = true;
            //txtroqty.BackColor = System.Drawing.Color.Wheat;



            txtbalqty.Text = dr["Balance_Qty"].ToString();
            txtbalqty.ReadOnly = true;
           // txtbalqty.BackColor = System.Drawing.Color.Wheat;

            string cumqtyqry = "Select Cumulative_Qty ,Pending_Qty from dbo.TO_Allot_Lift where RO_No='" + ddlrono.SelectedItem + "' and Distt_Id='" + distid + "'";

            txtcumlqty.Text = dr["Cumulative_Qty"].ToString();
            txtcommodity.Text = dr["Commodity_Name"].ToString();
            txtcommodity.ReadOnly = true;
           // txtcommodity.BackColor = System.Drawing.Color.Wheat;
            txtscheme.Text = dr["Scheme_Name"].ToString();
            txtscheme.ReadOnly = true;
          //  txtscheme.BackColor = System.Drawing.Color.Wheat;


            //txtbalqty.Text = dr["Pending_Qty"].ToString();
            DataSet dscq = obj.selectAny(cumqtyqry);
            if (dscq.Tables[0].Rows.Count==0)
            {
                txtcumlqty.Text = "0";
                txtcumlqty.ReadOnly = true;
               // txtcumlqty.BackColor = System.Drawing.Color.Wheat;
            }
            else
            {

                DataRow drcq = dscq.Tables[0].Rows[0];
                txtcumlqty.Text = drcq["Cumulative_Qty"].ToString();
                txtbalqty.Text = drcq["Pending_Qty"].ToString();
                txtcumlqty.ReadOnly = true;
               // txtcumlqty.BackColor = System.Drawing.Color.Wheat;
            }


        }
        else
        {
            txtrodate.Text = "";
            txtroqty.Text = "";
          
            txtbalqty.Text = "";
        }

    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }
   
  
   
    protected void ddlrono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();       
        fillgrid();
        
    }
   

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string rono = dgridchallan.SelectedRow.Cells[9].Text;
        string tono = dgridchallan.SelectedRow.Cells[1].Text;
        string id = dgridchallan.SelectedRow.Cells[8].Text;
        string toqty = dgridchallan.SelectedRow.Cells[3].Text;
        string liftqty = dgridchallan.SelectedRow.Cells[4].Text;
        Session["RO_No"] = rono ;
        Session["TO_No"] = tono ;
        Session["ID"] = id;
        Session["Toqty"] = toqty;
        Session["LiftQty"] = liftqty;

        Response.Redirect("../District/Edit_TransportOrder.aspx");

    }
   
    void fillgrid()
    {
        string mrono = ddlrono.SelectedItem.ToString();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT tbl_MetaData_DEPOT.DepotName,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.Trunsuction_Id,Transport_Order_againstRo.Pending_Qty,Transport_Order_againstRo.RO_No,Transport_Order_againstRo.Quantity,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.TO_Date,Transport_Order_againstRo.Transporter_Name,Transporter_Table.Transporter_Name as Tname FROM dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID left join dbo.tbl_MetaData_DEPOT  on Transport_Order_againstRo.toIssueCenter= tbl_MetaData_DEPOT.DepotID where Transport_Order_againstRo.Distt_Id='" + distid + "'and Transport_Order_againstRo.RO_No ='" + mrono + "'";
        DataSet ds = mobj.selectAny(qry);
        dgridchallan.DataSource = ds.Tables[0];
        dgridchallan.DataBind();


    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TO_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    

   
    protected void ddltransport_SelectedIndexChanged(object sender, EventArgs e)
    {
       // string mrono = ddlrono.SelectedItem.ToString();
       // string ttid = ddltransport.SelectedValue;
       // string qryTO = "Select Locked,RO_NO from dbo.TO_Allot_Lift where Distt_Id='" + distid + "'and Transporter_ID='" + ttid + "'";
       // obj = new LARO(ComObj);

       // DataSet dsto = obj.selectAny(qryTO);
       //if (dsto.Tables[0].Rows.Count == 0)
       // {
            

       // }
       // else
       // {
       //     DataRow drchk = dsto.Tables[0].Rows[0];
       //     string Status = drchk["Locked"].ToString();
       //     string mdro=drchk["RO_NO"].ToString();
       //     if (Status.Trim ()=="Y" && mrono == mdro)
       //     {
       //         ddldistrict.Focus();
              
                

       //     }
       //     else
       //     {
       //         if (Status.Trim() == "N")
       //         {

       //         }
       //         else
       //         {
       //             Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('This Transporter is Locked Please Select another one..'); </script> ");
       //             ddltransport.Focus();
       //             GetTransport();
       //         }


       //     }

            


       // }
    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        if (dgridchallan.PageCount == 0)
        {
        }
        else
        {
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
            fillgrid();
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/TransportOrder_Type.aspx");
    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
