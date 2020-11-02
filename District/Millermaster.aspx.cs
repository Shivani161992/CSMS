using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
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

public partial class District_Miller_agreement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string tname = "";
    public string bankid = "";
    public string bid = "";
    public int banknum;
    chksql chk = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {

           
            distid = Session["dist_id"].ToString();

            txt_millername.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txt_millername.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txt_millername.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbadds.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtbadds.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbadds.Attributes.Add("onchange", "return chksqltxt(this)");

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txt_millername.Text);
            ctrllist.Add(txtbadds.Text);

            if (chk == null)
            {
            }
            else
            {
                bool chkstr = chk.chksql_server(ctrllist);
                if (chkstr == true)
                {
                    Page.Server.Transfer(HttpContext.Current.Request.Path);
                }
            }
            if (Page.IsPostBack == false)
            {
            
            
                    con.Close();
                   
                    fillgrid();
                    Getmiller();
                   
              
         
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        if (Grd_milleragreement.PageCount == 0)
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
                    if ((Grd_milleragreement.PageIndex < (Grd_milleragreement.PageCount - 1)))
                    {
                        Grd_milleragreement.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((Grd_milleragreement.PageIndex > 0))
                    {
                        Grd_milleragreement.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    Grd_milleragreement.PageIndex = (Grd_milleragreement.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    Grd_milleragreement.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            fillgrid();
        }
    }
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select  * from dbo.Miller_Master where district_code='" + distid + "'";
        DataSet ds = mobj.selectAny(qrey);
        if (ds == null)
        {
        }
        else
        {
            Grd_milleragreement.DataSource = ds.Tables[0];
            Grd_milleragreement.DataBind();
        }


    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
      
            if (txt_millername.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Miller Name......');</script>");
                return;
            }


            if (txtbadds.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Miller Address......');</script>");
                return;
            }

            if (txt_mobileno.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Miller contact No......');</script>");
                return;
            }


            if (txt_millingcapacity.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Miller milling capacity......');</script>");
                return;
            }

            if (txt_paddycommon.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Miller paddy common Quantity......');</script>");
                return;
            }


            if (txt_paddygradeA.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Miller paddy Grade A Quantity......');</script>");
                return;
            }

            if (txtnfromdate.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Milling from date......');</script>");
                return;
            }


            if (txtntodate.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Milling to date......');</script>");
                return;
            }
            if (txt_millingrate.Text == "")
            {
                btnadd.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Milling rate......');</script>");
                return;
            }


            con.Close();

            string agree_frmdate = getDate_MDY(txtnfromdate.Text);
            string agree_todate = getDate_MDY(txtntodate.Text);
            mobj = new MoveChallan(ComObj);
            string qrey = "select max(Miller_ID) as Miller_ID from dbo.Miller_Master where District_Code='" + distid + "'";
            DataSet ds = mobj.selectAny(qrey);
            DataRow dr = ds.Tables[0].Rows[0];
            bankid = dr["Miller_ID"].ToString();
            if (bankid == "")
            {
                bankid = distid + "5001";

            }
            else
            {
                banknum = Convert.ToInt32(bankid);
                banknum = banknum + 1;
                bankid = banknum.ToString();


            }


            string mbname = txt_millername.Text;
            string mbadd = txtbadds.Text;

            string insert = "insert into dbo.Miller_Master(District_Code,Miller_ID,Miller_Name,millerAddress,contactno,milling_from,milling_to,crop_year,milling_quantity_common,milling_quantity_gradeA,milling_rate,milling_capacity)values('" + distid + "','" + bankid + "','" + mbname + "','" + mbadd + "','" + txt_mobileno.Text + "','" + agree_frmdate + "','" + agree_todate + "','" + ddlfinancialyear.SelectedItem.ToString() + "','" + txt_paddycommon.Text + "','" + txt_paddygradeA.Text + "','" + txt_millingrate.Text + "','" + txt_millingcapacity.Text + "')";
            cmd.Connection = con;
            cmd.CommandText = insert;
            con.Open();
            SqlTransaction trns;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            try
            {

                cmd.Transaction = trns;
                cmd.ExecuteNonQuery();
                trns.Commit();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully.....'); </script> ");
                btnadd.Enabled = false;

            }
            catch (Exception ex)
            {
                trns.Rollback();
                Label1.Visible = true;
                Label1.Text = ex.Message;
            }
            finally
            {
                con.Close();
                ComObj.CloseConnection();
            }





            fillgrid();





  

        con.Close();
     



    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bid = Grd_milleragreement.SelectedRow.Cells[1].Text;
        txt_millername.Text = Grd_milleragreement.SelectedRow.Cells[2].Text;
        txtbadds.Text = Grd_milleragreement.SelectedRow.Cells[4].Text;
        txt_mobileno.Text = Grd_milleragreement.SelectedRow.Cells[3].Text;

    ddlfinancialyear.SelectedValue = Grd_milleragreement.SelectedRow.Cells[5].Text;
      txt_paddycommon.Text = Grd_milleragreement.SelectedRow.Cells[6].Text;
      txt_paddygradeA.Text= Grd_milleragreement.SelectedRow.Cells[7].Text;

     txt_millingrate.Text = Grd_milleragreement.SelectedRow.Cells[8].Text;
      txt_millingcapacity.Text = Grd_milleragreement.SelectedRow.Cells[9].Text;
       
        btnadd.Visible = false;
        btnUpdate.Visible = true;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grd_milleragreement.PageIndex = e.NewPageIndex;
        fillgrid();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtnfromdate.Text == "")
        {
            btnadd.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Milling from date......');</script>");
            return;
        }


        if (txtntodate.Text == "")
        {
            btnadd.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Milling to date......');</script>");
            return;
        }
        string agree_frmdate = getDate_MDY(txtnfromdate.Text);
        string agree_todate = getDate_MDY(txtntodate.Text);
        string bank_id = Grd_milleragreement.SelectedRow.Cells[1].Text;
        string bname = txt_millername.Text;
        string Update = "Update dbo.Miller_Master set Miller_Name='" + bname + "',millerAddress='" + txtbadds.Text + "',contactno='" + txt_mobileno.Text + "',milling_from='" + agree_frmdate + "',milling_to='" + agree_todate + "',crop_year='" + ddlfinancialyear.SelectedItem.ToString() + "',milling_quantity_common='" + txt_paddycommon.Text + "',milling_quantity_gradeA='" + txt_paddygradeA.Text + "',milling_rate='" + txt_millingrate.Text + "',milling_capacity='" + txt_millingcapacity.Text + "' where District_Code='" + distid + "'and Miller_ID='" + bank_id + "'";
        cmd.Connection = con;
        cmd.CommandText = Update;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully.....'); </script> ");
            btnadd.Enabled = false;
            fillgrid();

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();
        }

    }




    protected void ddl_contracted_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        txt_millername.Text = ddl_contracted.SelectedValue.ToString();

         con.Open();
    
            string mystr = "select *from Miller_Master where District_Code='" + distid + "' and Miller_Name='"+ ddl_contracted.SelectedValue.ToString() +"' ";

            cmd = new SqlCommand(mystr, con);
            SqlDataReader sqldr = cmd.ExecuteReader();
            sqldr.Read();

            if (sqldr.HasRows)
            {

                txt_millername.Text = sqldr["Miller_Name"].ToString();
             txtbadds.Text=sqldr["millerAddress"].ToString();
                txt_mobileno.Text=sqldr["contactno"].ToString();

                txt_millername.Enabled = false;
               
            
            }
      
        con.Close();
       
    }
    private void Getmiller()
    {
        SqlCommand cmd = new SqlCommand("select Miller_Name as miller, Miller_Name+'('+Miller_ID+')' as Miller_Name  from Miller_Master where District_Code='" + distid + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();

     ddl_contracted.DataSource = ds;
     ddl_contracted.DataTextField = "Miller_Name";
     ddl_contracted.DataValueField = "miller";


        //ddlsupplier.DataValueField = "id";
     ddl_contracted.DataBind();
     ddl_contracted.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btn_add_Click(object sender, System.EventArgs e)
    {
        txt_millername.Enabled = true;
        lbl_contracted.Visible = false;
        ddl_contracted.Visible = false;
    }
}