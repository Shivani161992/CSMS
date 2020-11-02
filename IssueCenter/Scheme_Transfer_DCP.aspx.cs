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
public partial class IssueCenter_Scheme_Transfer : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
   
    DistributionCenters distobj = null;

    
    MoveChallan mobj1 = null; 
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;

    MoveChallan mobj = null;
    string distid = "";
    string issueid = "";
    protected Common ComObj = null, cmn = null;
    public string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtbalqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
             
            txtbalqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdcpqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();


            if (!IsPostBack)
            {
                GetScheme();
                GetCommodity();
                GetSchemeDCP();
                GetGodown();
                GetSource();
                GetGodownDCP();
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }

    }

    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomdty_dcp.DataSource = ds.Tables[0];
        ddlcomdty_dcp.DataTextField = "Commodity_Name";
        ddlcomdty_dcp.DataValueField = "Commodity_Id";
        ddlcomdty_dcp.DataBind();
        ddlcomdty_dcp.Items.Insert(0, "--Select--");


    }

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }

    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
    }
    void GetSchemeDCP()
    {

        schobj = new Scheme_MP(ComObj);
        string qryDCP = "SELECT * FROM dbo.tbl_MetaData_SCHEME where Scheme_Id not in(1,2,3)and Status='Y' order by Scheme_Id ";
        DataSet ds = schobj.selectAny(qryDCP);
        ddldcpscheme.DataSource = ds.Tables[0];
        ddldcpscheme.DataTextField = "Scheme_Name";
        ddldcpscheme.DataValueField = "Scheme_Id";
        ddldcpscheme.DataBind();
        ddldcpscheme.Items.Insert(0, "--Select--");

    }
    void GetGodown()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + issueid  + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");


    }
    void GetGodownDCP()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + issueid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodowndcp.DataSource = ds.Tables[0];
        ddlgodowndcp.DataTextField = "Godown_Name";
        ddlgodowndcp.DataValueField = "Godown_ID";
        ddlgodowndcp.DataBind();
        ddlgodowndcp.Items.Insert(0, "--Select--");


    }
    void GetDCPBalance()
    {
        string mcomid = ddlcomdty_dcp.SelectedValue;
        string mscheme = ddldcpscheme.SelectedValue;
        int month = int.Parse(DateTime.Today.Month.ToString ());
        int year = int.Parse(DateTime.Today.Year .ToString ());
        string godown=ddlgodowndcp.SelectedValue;
        string source="01";
        mobj1 = new MoveChallan(ComObj);
        string qryDCPB = "Select   Round(Sum(Current_Balance),5)  as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Godown='"+godown +"' and Source='"+source+"'" ;
        DataSet ds = mobj1.selectAny(qryDCPB);

        if (ds.Tables[0].Rows.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  comodity....'); </script> ");
            lbldcp.Visible   = false;
            txtdcpqty .Visible = false;
            dcpunit .Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtdcpqty.Text = CheckNull(dr["Current_Balance"].ToString()).ToString ();
            lbldcp.Visible = true;
            txtdcpqty.Visible = true;
            dcpunit.Visible = true;
            txtdcpqty.BackColor = System.Drawing.Color.Wheat;
            txtdcpqty.ReadOnly = true;
        }
    }

    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mcomid = ddlcomdty_dcp.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string mgodown = ddlgodown.SelectedValue;
        string msource = ddlsarrival.SelectedValue;
         mobj1 = new MoveChallan(ComObj);
         string qry = "Select Round(Sum(Current_Balance),2) as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Godown='"+mgodown +"' and Source='"+ msource +"'";
         DataSet ds = mobj1.selectAny(qry);

         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  comodity....'); </script> ");
            lblbalqty.Visible = false;
            txtbalqty.Visible = false;
            Label2.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbalqty.Text = dr["Current_Balance"].ToString();
            lblbalqty . Visible = true;
            txtbalqty.Visible = true;
            Label2.Visible = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.ReadOnly = true;
        }
    }
    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
       
        string mcomid = ddlcomdty_dcp.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string mschemedcp = ddldcpscheme.SelectedValue;
        int mmonth = int.Parse(DateTime.Today.Month.ToString());
        int myear = int.Parse(DateTime.Today.Year.ToString());
        float qty = CheckNull(txtqty.Text);
        int bags = CheckNullInt(txtbags.Text);
        float dcpqty = CheckNull(txtdcpqty.Text);
        string udate = "";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
              

        if (ddlcomdty_dcp.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please  Select Commodity/Scheme......'); </script> ");
        }
        else
        {


            string qrey = "insert into dbo.DCP_Stock_Transfer(district_code,DepotID,Commodity_ID,DCP_Balance,To_Scheme,Quantity,No_of_Bags,Month,Year,Created_date,Updated_date,IP_Address) values('" + distid + "','" + issueid + "','" + mcomid + "'," + dcpqty + ",'" + mscheme + "'," + qty + "," + bags + "," + mmonth + "," + myear + ",getdate(),'" + udate + "','" + ip + "')";
            cmd.CommandText = qrey;
            cmd.Connection = con;
            try
            {
                if (qty == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be 0 ......'); </script> ");

                }
                else
                {
                   
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        float bqty = CheckNull(txtdcpqty.Text);
                        float tqty = CheckNull(txtqty.Text);
                        float uqty = (bqty - tqty);
                        string uqry = "Update dbo.issue_opening_balance  set Current_Balance=" + uqty + " where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mschemedcp  + "'";
                        cmd.CommandText = uqry;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                         con.Close();

                        float bqtyd = CheckNull(txtbalqty.Text);
                        float tqtyd = CheckNull(txtqty.Text);
                        float uqtyd = (bqtyd + tqtyd);

                        string uqryd = "Update dbo.issue_opening_balance  set Current_Balance=" + uqtyd + "where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'";
                        cmd.CommandText = uqryd;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();

                        UpdateStock();
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Transfer Succesfully ......'); </script> ");
                       



                        btnsubmit.Enabled = false;
                    
                }
            }
            catch (Exception ex)
            {
                Label1.Visible = true;
                Label1.Text = ex.Message;
            }
            finally
            {
                ComObj.CloseConnection();
                con.Close();

            }

        }

    }

    void UpdateStock()
    {
         string mcomidu = ddlcomdty_dcp.SelectedValue;
        string mschemeu = ddlscheme.SelectedValue;
       int mmonthu = int.Parse(DateTime.Today.Month.ToString());
        int myearu = int.Parse(DateTime.Today.Year.ToString());
        string qrystock = "select Sum(Quantity) as Qty from dbo.DCP_Stock_Transfer where Commodity_ID ='" +mcomidu + "'and district_code='" + distid  + "'and DepotID='" + issueid  + "'and Month=" + mmonthu + "and Year=" + myearu ;
        mobj = new MoveChallan(ComObj);
        DataSet dsstock = mobj.selectAny(qrystock);

        if (dsstock.Tables[0].Rows.Count == 0)
        {

        }
        else
        {
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            DataRow drop = dsstock.Tables[0].Rows[0];
            float mobal = 0;
            float mrp = 0;
            float mrod = 0;
            float msod = 0;
            float msdelo = 0;
            float mrfci = 0;
            float mros =CheckNull(drop["Qty"].ToString());
            float msos = 0;

            string mremark = "";
            string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomidu  + "'and DistrictId ='" + distid + "'and DepotID='" + issueid  + "'and Month=" + mmonthu + "and Year=" + myearu ;
            mobj = new MoveChallan(ComObj);
            DataSet dsopen = mobj.selectAny(qryinsopen);

            if (dsopen.Tables[0].Rows.Count == 0)
            {
                string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid  + "','" + issueid  + "','" + mcomidu  + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + mros + "," + msdelo + "," + msod + "," + msos + "," + mmonthu  + "," + myearu  + ",'" + mremark + "')";
                cmd.CommandText = qryins;
                cmd.ExecuteNonQuery();

            }
            else
            {
                string qryinsU = "update dbo.tbl_Stock_Registor set Received_OtherSch=" + mros + " where Commodity_Id ='" + mcomidu  + "'and DistrictId='" + distid  + "'and DepotID='" + issueid  + "'and Month=" + mmonthu  + "and Year=" +myearu ;
                cmd.CommandText = qryinsU;
                cmd.ExecuteNonQuery();

            }

        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    
    protected void ddldcpscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCPBalance();
    }
    protected void ddlcomdty_dcp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcomdty.SelectedItem.Text = ddlcomdty_dcp.SelectedItem.Text;
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlgodowndcp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
