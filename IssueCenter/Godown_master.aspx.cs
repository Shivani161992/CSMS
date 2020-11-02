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
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
public partial class IssueCenter_Godown_master : System.Web.UI.Page
{
    Transporter tobj = null;
    chksql chk = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string issueid = "";
    DataTable dt = new DataTable();
    protected Common ComObj = null, cmn = null;
    MoveChallan mobj = null;
    public string gdnid = "";
    public string gid = "";
    public string version = "";
    public Int64 gdnnum = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            version = Session["hindi"].ToString();

            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtcapacty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

        
            txtcapacty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtcapacty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtgname.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtgname.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtgname.Attributes.Add("onchange", "return chksqltxt(this)");


            txtcapacty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtgname.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtcapacty.Text);
            ctrllist.Add(txtgname.Text);
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
                Fillgrid();
                if (version == "H")
                {
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblMaxCap.Text = Resources.LocalizedText.lblMaxCap;
                    lblGodownMaster.Text = Resources.LocalizedText.lblGodownMaster;
                    btnaddnew.Text = Resources.LocalizedText.btnaddnew;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    btnsubmit.Text = Resources.LocalizedText.btnsubmit;
                    btnupdate.Text = Resources.LocalizedText.btnupdate;

                }

            }
            

        }
        else
        {
        
            Response.Redirect("~/MainLogin.aspx");


        }

        



    }
    void Fillgrid()
    {
        tobj = new Transporter(ComObj);
        string query = "select tbl_MetaData_GODOWN.Godown_ID,tbl_MetaData_GODOWN.Godown_Name,Round(convert(decimal,tbl_MetaData_GODOWN.Godown_Capacity),5) as Godown_Capacity,isnull(Round(convert(decimal,sum(issue_opening_balance.Current_Balance)),5),0) as Current_Balance   from dbo.tbl_MetaData_GODOWN left join dbo.issue_opening_balance on  tbl_MetaData_GODOWN.Godown_ID=issue_opening_balance.Godown where tbl_MetaData_GODOWN.DepotID='" + issueid + "' group  by tbl_MetaData_GODOWN.Godown_ID,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_GODOWN.Godown_Capacity";
        DataSet ds = tobj.selectAny(query);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        btnupdate.Visible = false;
       // GridView1.Columns[1].Visible = false;

    }
    public static string Encrypt(string toEncrypt)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
        string key = ")(*&";
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //Always release the resources and flush data of the Cryptographic service provide. Best Practice
        hashmd5.Clear();
        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }


    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        if (GridView1.PageCount == 0)
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
                    if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
                    {
                        GridView1.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((GridView1.PageIndex > 0))
                    {
                        GridView1.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    GridView1.PageIndex = (GridView1.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    GridView1.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            Fillgrid();
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
    protected void btnaddnew_Click1(object sender, EventArgs e)
    {
        Panel1.Visible = true;

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Godown_master.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        btnsubmit.Visible = false;
        btnupdate.Visible = true;

        txtcapacty.Text = GridView1.SelectedRow.Cells[3].Text;
        txtgname.Text = GridView1.SelectedRow.Cells[2].Text;

        
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
       
        //GridView1.Columns[1].Visible = true;
        float gcap = CheckNull(GridView1.SelectedRow.Cells[3].Text);
        //txtcapacty.Text = GridView1.SelectedRow.Cells[3].Text;
        //txtgname.Text = GridView1.SelectedRow.Cells[2].Text;
        string gname = txtgname.Text;
        float ugcap = CheckNull(txtcapacty.Text);
        string godwnid = GridView1.SelectedRow.Cells[1].Text;
        float curcap = CheckNull(GridView1.SelectedRow.Cells[4].Text);
        if (ugcap < curcap)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry You Can't Decrees the Godown Capacity.....'); </script> ");
            Label2.Visible = true;
            Label2.Text = "Sorry You Can't Decrees the Godown Capacity";

        }
        else
        {
            Label2.Visible = false;
            string uqry = "Update dbo.tbl_MetaData_GODOWN set Godown_Name='" + gname + "',Godown_Capacity=" + ugcap + " where DistrictId='" + distid + "' and DepotId='" + issueid + "' and Godown_ID='" + godwnid + "'";

            try
            {
                cmd.Connection = con;
                cmd.CommandText = uqry;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                float ucap = gcap - CheckNull(txtcapacty.Text);
                string ugqry = "Update dbo.Current_Godown_Position set Godown_Capacity=Godown_Capacity-(" + ucap + "),Current_Capacity=Current_Capacity-(" +ucap  + ") where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + godwnid + "'";

                cmd.CommandText = ugqry;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated  Successfully.....'); </script> ");
                btnupdate.Enabled = false;
                Fillgrid();
            }
            catch (Exception ex)
            {
            }

            finally
            {
                con.Close();
            }
           // GridView1.Columns[1].Visible = true;

        }
    
       
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string mgodown = txtgname.Text;
        mobj = new MoveChallan(ComObj);

        string qreygodn = "select * from dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "'and DepotId='" + issueid + "' and Godown_Name='" + mgodown + "'";
        DataSet dsgodn = mobj.selectAny(qreygodn);
        if (dsgodn == null)
        {

        }

        else
        {

            if (dsgodn.Tables[0].Rows.Count == 0)
            {
                mobj = new MoveChallan(ComObj);

                string qrey = "select max(Godown_ID) as Godown_ID from dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "'and DepotId='" + issueid + "'";
                DataSet ds = mobj.selectAny(qrey);
                DataRow dr = ds.Tables[0].Rows[0];
                gdnid = dr["Godown_ID"].ToString();
                ComObj.CloseConnection();
                if (gdnid == "")
                {
                    gdnid = issueid + "01";


                }
                else
                {

                    gdnnum = Int64.Parse(gdnid.ToString());
                    gdnnum = gdnnum + 1;
                    gdnid = gdnnum.ToString();


                }


                string mgname = txtgname.Text;
                string mgid = gdnid;
                float mgcap = CheckNull(txtcapacty.Text);
                string mcdate = DateTime.Today.Date.ToString();
                string mudate = "";
                string mcby = issueid;
                string muby = "";
                string mgfdate = DateTime.Today.Date.ToString();
                string mgudate = "";
                string mddate = "";
                string mdby = "";
                string mremarks = "";
                string mstate = "23";
                int month = int.Parse(DateTime.Today.Month.ToString());
                int year = int.Parse(DateTime.Today.Year.ToString());
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                int openqty = 0;
                int openbag = 0;
                string qyr = "insert into dbo.tbl_MetaData_GODOWN(Godown_ID,StateId,DistrictId,DepotId,Godown_Name,Godown_Formation_Date,Godown_Updation_Date,Godown_Capacity,Remarks,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,DeletedBy,DeletedDate)values('" + mgid + "','" + mstate + "','" + distid + "','" + issueid + "','" + mgname + "','" + mgfdate + "','" + mgudate + "'," + mgcap + ",'" + mremarks + "','" + mdby + "',getdate(),'" + muby + "','" + mudate + "','" + mdby + "','" + mddate + "'" + ")";

                cmd.Connection = con;
                cmd.CommandText = qyr;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully.....'); </script> ");
                    btnsubmit.Enabled = false;


                    string qreygdn = "insert into dbo.Current_Godown_Position(District_Id,Depotid,Godown,Current_Balance,Current_Bags,Month,Year,IP_Address,CreatedDate,UpdatedDate,DeletedDate,Godown_Capacity,Current_Capacity) values('" + distid + "','" + issueid + "','" + mgid + "'," + openqty + "," + openbag + "," + month + "," + year + ",'" + ip + "',getdate(),'" + mudate + "','" + mudate + "'," + mgcap + "," + mgcap + ")";
                    cmd.CommandText = qreygdn;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();




                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;

                }
                finally
                {
                    con.Close();

                }
                con.Open();


            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Godown Already Exist.....'); </script> ");

            }





        }






        //Fillgrid();
        
    }
    protected void txtgname_TextChanged(object sender, EventArgs e)
    {
        string mgodown = txtgname.Text;
        mobj = new MoveChallan(ComObj);

        string qreygodn = "select * from dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "'and DepotId='" + issueid + "' and Godown_Name='" + mgodown + "'";
        DataSet dsgodn = mobj.selectAny(qreygodn);
        if (dsgodn == null)
        {

        }

        else
        {

            if (dsgodn.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Godown Already Exist.....'); </script> ");
                btnupdate.Enabled = false;

            }
        }
    }
}
