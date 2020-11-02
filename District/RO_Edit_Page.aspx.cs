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
public partial class District_RO_Edit_Page : System.Web.UI.Page
{
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {

            distid = Session["dist_id"].ToString();

            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            fillgrid();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT RO_of_FCI.RO_No,RO_of_FCI.RO_date,RO_of_FCI.RO_qty,RO_of_FCI.Commodity,RO_of_FCI.Scheme,RO_of_FCI.RO_district,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name,districtsmp.district_name as district_name   FROM dbo.RO_of_FCI left join tbl_MetaData_SCHEME on RO_of_FCI.Scheme=tbl_MetaData_SCHEME.Scheme_ID left join pds.districtsmp on RO_of_FCI.RO_district=districtsmp.district_code  where RO_of_FCI.Distt_Id='" + distid + "' and RO_No <> 'NoRO' order by RO_date desc  ";
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            lblmsg.Text = "Currently no Records Found";
        }
        else
        {
            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();
        }


    }

    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        if (dgridchallan.PageCount == 0)
        {
        }
        else
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
            fillgrid();
        }
    }

    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RO_date"));
            string comdty = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Commodity"));
            
            getdatef = getdate(griddate);
            string comname = Commodity_Name(comdty);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;
            Label lblcomdty = (Label)e.Row.FindControl("lblcomdty");
            lblcomdty.Text = comname;

            
        }



    }
     public string Commodity_Name( string comid)
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Commodity_Name FROM dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + comid +"'";
        DataSet ds = mobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];
        return dr["Commodity_Name"].ToString();
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }


    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        
        
        string tag = "Y";
        mobj = new MoveChallan(ComObj);
        string Ro_No = dgridchallan.SelectedRow.Cells[1].Text;
        string Ro_dist = dgridchallan.SelectedRow.Cells[7].Text;
        Session["Ro_No"] = Ro_No;
        string qry = "SELECT IsLifted FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'and Ro_No='"+Ro_No +"'";
        DataSet ds = mobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];
        string st = dr["IsLifted"].ToString().Trim ();
        if (st ==tag)
        {
            Session["ROdist"] = Ro_dist;
            Session["Lifts"] = st;
            //Label1.Visible = true;
            //Label1.Text = "Sorry You Can't Edit This RO ,It has been lifted";
            //Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Sorry You Can't Edit This RO ,It has been lifted'); </script> ");
            Response.Redirect("../District/RO_Edit.aspx");
        }
        else
        {
            Session["Lifts"] = st;
            Session["ROdist"] = Ro_dist;
            Response.Redirect("../District/RO_Edit.aspx");
            
        }
    }
    protected void Firstbutton_Click(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Firstbutton_Click1(object sender, EventArgs e)
    {

    }
}
