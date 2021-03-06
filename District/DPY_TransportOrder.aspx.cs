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
using System.IO;
using System.Net;
using System.Text;

public partial class District_DPY_TransportOrder : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());

    public string distid = "";

    public string IssuecenterID;

    static HttpWebRequest request = null;
    static Stream dataStream;

    decimal wheat = 0;
    decimal rice = 0;

    decimal sugar = 0;
    decimal salt = 0;

    decimal maize = 0;
   


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] != null)
            {
                distid = Session["dist_id"].ToString();


                if (!IsPostBack)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    Session["issubmited"] = "No";

                   
                    get_IssueCenter();

                    get_Transporter();

                   ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString())).ToString());

                   ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());

                   hlinkpdo.Attributes.Add("onclick", "window.open('Print_DPY_TransportOrder.aspx',null,'left=800, top=800, height=900, width= 800, status=n o, resizable= yes, scrollbars= yes, toolbar= no,location= no, menubar= no');");

                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }

        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            Response.Write(ex.Message);
        }
    }
    
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (ddlissueCenter.SelectedItem.Text == "--Select--" || ddlissueCenter.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Issue center...'); </script> ");
            return;
        }

        if (ddltransporter.SelectedItem.Text == "--Select--" || ddltransporter.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Transporter...'); </script> ");
            return;
        }

        ddl_fps_name.Items.Clear();

        hd_fps.Value = "";

        get_fps();

        if (con_opdms.State == ConnectionState.Closed)
        {
            con_opdms.Open();
        }

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            if (ddlTOType.SelectedItem.Value == "MTO")
            {
                string check = "Select count(TransportOrder) from DPY_TranportOrder where DistrictId = '" + distid + "' and IssueCenter = '" + ddlissueCenter.SelectedValue + "' and TransporterId = '" + ddltransporter.SelectedValue + "' and TOType = 'MTO' and AllotMonth = '" + ddlmonth.SelectedItem.Value + "' and AllotYear = '" + ddlyear.SelectedItem.Text + "'";
                SqlCommand cmdcheck = new SqlCommand(check, con);

                string str1 = cmdcheck.ExecuteScalar().ToString();

                if (Convert.ToInt16(str1) > 0)
                {
                    //Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Transport Order Issued for this Transporter for this Selected Month, Please Select STO ...');</script>");
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Transport Order Issued for this Transporter for this Selected Month.');</script>");
                    
                    GridView2.DataSource = "";
                    GridView2.DataBind();
                }
                
                else
                {
                    btnSave.Enabled = true;

                    # region MTO
                    string qrygrid = "SELECT pds.fps_allot.fps_code , pds.fps_master.fps_Uname, isnull((Net_Rice_alloc)/100,0) as rice , isnull((Net_Wheat_alloc)/100,0) as wheat , isnull(sugar_alloc/100,0) as sugar_alloc , isnull(salt_alloc/100,0) as salt_alloc , isnull((Net_Maize_alloc)/100,0) as Maize from pds.fps_allot inner join pds.fps_master on pds.fps_master.fps_code = pds.fps_allot.fps_code and pds.fps_allot.fps_code in (" + hd_fps.Value + "'') where month = '" + ddlmonth.SelectedItem.Value + "' and YEAR = '" + ddlyear.SelectedItem.Text + "' order by pds.fps_allot.fps_code ";

                    
                    SqlCommand cmdgrid = new SqlCommand(qrygrid, con_opdms);

                    SqlDataAdapter da = new SqlDataAdapter(cmdgrid);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView2.DataSource = ds.Tables[0];

                        GridView2.DataBind();

                        Panel2.Visible = true;

                        msgAllotment.Visible = true;

                        msgAllotment.Text = "Allotment against Month of '" + ddlmonth.SelectedItem.Text + "' and Year '" + ddlyear.SelectedItem.Text + "'";
    
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('New Allotment is Not Available for this Selected Month');</script>");
                        return;

                        # region unused     // RM 09-02

                        // For January Only , Get Previous Detail for Dec
                    //    if (ddlmonth.SelectedItem.Value == "1" && ddlyear.SelectedItem.Text == DateTime.Today.Year.ToString())
                    //    {
                    //        string preyear = (int.Parse(DateTime.Today.Year.ToString()) - 1).ToString();

                    //        //string preyear = "2014";

                    //        string qrygrid1 = "SELECT pds.fps_allot.fps_code, pds.fps_master.fps_Uname , (rice_aay_alloc + rice_phh_alloc)/100 as rice , (wheat_aay_alloc + wheat_phh_alloc)/100 as wheat , sugar_alloc/100 as sugar_alloc , salt_alloc/100 as salt_alloc , isnull((Maize_PHH_alloc + Maize_AAY_alloc)/100,0) as Maize from pds.fps_allot inner join pds.fps_master on pds.fps_master.fps_code = pds.fps_allot.fps_code and pds.fps_allot.fps_code in (" + hd_fps.Value + "'') where month = '12' and YEAR = '" + preyear + "' order by fps_Uname ";

                    //        SqlCommand cmdgrid1 = new SqlCommand(qrygrid1, con_opdms);

                    //        SqlDataAdapter da1 = new SqlDataAdapter(cmdgrid1);

                    //        DataSet ds1 = new DataSet();

                    //        da1.Fill(ds1);

                    //        if (ds1.Tables[0].Rows.Count > 0)
                    //        {
                    //            GridView2.DataSource = ds1.Tables[0];

                    //            GridView2.DataBind();

                    //            Panel2.Visible = true;

                    //            msgAllotment.Visible = true;

                    //            msgAllotment.Text = "Allotment against Month of 'December' and Year '" + preyear + "'";

                    //            Session["Alloted"] = "Last Month";
                    //        }
                    //    }

                    //    else // det getail for rest of Last month with same Year
                    //    {
                    //        string curyear = (int.Parse(DateTime.Today.Year.ToString())).ToString();

                    //        string selmonth = ddlmonth.SelectedItem.Value;
                    //        Int64 premonth = Convert.ToInt64(selmonth);

                    //        Int64 lastmonth = premonth - 1;

                    //        selmonth = Convert.ToString(lastmonth);

                    //        string qrygrid2 = "SELECT pds.fps_allot.fps_code, pds.fps_master.fps_Uname , (rice_aay_alloc + rice_phh_alloc)/100 as rice , (wheat_aay_alloc + wheat_phh_alloc)/100 as wheat , sugar_alloc/100 as sugar_alloc , salt_alloc/100 as salt_alloc , isnull((Maize_PHH_alloc + Maize_AAY_alloc)/100,0) as Maize from pds.fps_allot inner join pds.fps_master on pds.fps_master.fps_code = pds.fps_allot.fps_code and pds.fps_allot.fps_code in (" + hd_fps.Value + "'') where month = '" + selmonth + "' and YEAR = '" + curyear + "' order by fps_Uname ";

                    //        SqlCommand cmdgrid2 = new SqlCommand(qrygrid2, con_opdms);

                    //        SqlDataAdapter da2 = new SqlDataAdapter(cmdgrid2);

                    //        DataSet ds2 = new DataSet();

                    //        da2.Fill(ds2);

                    //        if (ds2.Tables[0].Rows.Count > 0)
                    //        {
                    //            GridView2.DataSource = ds2.Tables[0];

                    //            GridView2.DataBind();

                    //            Panel2.Visible = true;

                    //            msgAllotment.Visible = true;

                    //            msgAllotment.Text = "Allotment against Previous Month of Year '" + curyear + "'";

                    //            Session["Alloted"] = "Last Month";

                    //        }

                    //        else
                    //        {
                    //            GridView2.DataSource = "";
                    //            GridView2.DataBind();
                    //        }
                    //    }

                    //    # endregion

                    //}
                    # endregion 
                    }
            }

//            else if (ddlTOType.SelectedItem.Value == "STO")
//            {
//                # region STO
//                //btnSave.Enabled = true;

//                //string qrygrid1 = "SELECT pds.fps_allot.fps_code, pds.fps_master.fps_Uname , (rice_aay_alloc + rice_phh_alloc)as rice , (wheat_aay_alloc + wheat_phh_alloc)as wheat , sugar_alloc , salt_alloc from pds.fps_allot inner join pds.fps_master on pds.fps_master.fps_code = pds.fps_allot.fps_code and pds.fps_allot.fps_code in (" + hd_fps.Value + "'') where month = '" + ddlmonth.SelectedItem.Value + "' and YEAR = '" + ddlyear.SelectedItem.Text + "' order by fps_Uname ";

//                //SqlCommand cmdgrid1 = new SqlCommand(qrygrid1, con_opdms);

//                //SqlDataAdapter da1 = new SqlDataAdapter(cmdgrid1);

//                //DataSet ds1 = new DataSet();

//                //da1.Fill(ds1);

               

//                //if (ds1.Tables[0].Rows.Count > 0)
//                //{
//                //    string check1 = "Select WheatAllot ,RiceAllot ,SugarAllot ,SaltAllot from DPY_TranportOrder where DistrictId = '" + distid + "' and IssueCenter = '" + ddlissueCenter.SelectedValue + "' and TransporterId = '" + ddltransporter.SelectedValue + "' and TOType = 'MTO' and AllotMonth = '" + ddlmonth.SelectedItem.Value + "' and AllotYear = '" + ddlyear.SelectedItem.Text + "' and pds.fps_allot.fps_code in (" + hd_fps.Value + "'')";

//                //    SqlCommand cmdcheck1 = new SqlCommand(check1, con);

//                //    SqlDataAdapter da2 = new SqlDataAdapter(cmdcheck1);

//                //    DataSet ds4 = new DataSet();

//                //    da2.Fill(ds4);

//                //    if (ds4.Tables[0].Rows.Count == 0)
//                //    {
//                //        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Main Transport Order is Not Issued for this Transporter, please make MTO');</script>");
//                //        return;
//                //    }

//                //    for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
//                //    {
//                //        string wheatt = ds4.Tables[0].Rows[i]["WheatAllot"].ToString();  //CSMS
//                //        string ricee = ds4.Tables[0].Rows[i]["RiceAllot"].ToString();     //CSMS   

//                //        string sugarr = ds4.Tables[0].Rows[i]["SugarAllot"].ToString();   //CSMS

//                //        string saltt = ds4.Tables[0].Rows[i]["SaltAllot"].ToString();       //CSMS
//                //        string fps_code_csms = ds4.Tables[0].Rows[i]["fps_code"].ToString();    //CSMS

//                //        string wheattpds = ds1.Tables[0].Rows[i]["wheat"].ToString();         //PDS   
//                //        string riceepds = ds1.Tables[0].Rows[i]["rice"].ToString();           //PDS

//                //        string sugarrpds = ds1.Tables[0].Rows[i]["sugar_alloc"].ToString();      //PDS

//                //        string salttpds = ds1.Tables[0].Rows[i]["salt_alloc"].ToString();        //PDS
//                //        string fps_codepds = ds1.Tables[0].Rows[i]["fps_code"].ToString();         //PDS

//                //        string fps_Namepds = ds1.Tables[0].Rows[i]["fps_Uname"].ToString();         //PDS



//                //        if (fps_code_csms == fps_codepds)
//                //        {
//                //            double wheatt_csms = Convert.ToDouble(wheatt);

//                //            double ricee_csms = Convert.ToDouble(ricee);

//                //            double sugarr_csms = Convert.ToDouble(sugarr);

//                //            double saltt_csms = Convert.ToDouble(saltt);


//                //            double wheatt_pds = Convert.ToDouble(wheattpds);

//                //            double ricee_pds = Convert.ToDouble(riceepds);

//                //            double sugarr_pds = Convert.ToDouble(sugarrpds);

//                //            double saltt_pds = Convert.ToDouble(salttpds);


//                //            string new_wheat = "0";

//                //            if (wheatt_pds > wheatt_csms)
//                //            {
//                //                new_wheat = Convert.ToString(wheatt_pds - wheatt_csms);
//                //            }

//                //            string new_rice = "0";
//                //            if (ricee_pds > ricee_csms)
//                //            {
//                //                new_rice = Convert.ToString(ricee_pds - ricee_csms);
//                //            }

//                //            string new_sugar = "0";

//                //            if (sugarr_pds > sugarr_csms)
//                //            {
//                //                new_sugar = Convert.ToString(sugarr_pds - sugarr_csms);
//                //            }


//                //            string new_salt = "0";
//                //            if (saltt_pds > saltt_csms)
//                //            {
//                //                new_salt = Convert.ToString(saltt_pds - saltt_csms);
//                //            }

                            
//                //            DataTable table1 = new DataTable("newvalues");
//                //            table1.Columns.Add("fps_code");
//                //            table1.Columns.Add("fps_Uname");
//                //            table1.Columns.Add("wheat");

//                //            table1.Columns.Add("rice");
//                //            table1.Columns.Add("sugar_alloc");

//                //            table1.Columns.Add("salt_alloc");

//                //            table1.Rows.Add(fps_codepds, fps_Namepds, new_wheat, new_rice, new_sugar, new_salt);

//                //            DataSet set = new DataSet("office");
//                //            set.Tables.Add(table1);

//                //        }
//                //    }

//                //}

//                //else
//                //{
//                //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('New Allotment is Not Available for this Selected Month');</script>");
//                //    return;
//                //}

                

# endregion

                
           }  
            
        }
        catch
        {
            msgAllotment.Visible = false;
        }

        finally

        {
            if (con_opdms.State == ConnectionState.Open)
            {
                con_opdms.Close();
            }
        }

        }
    
    protected void get_IssueCenter()
    {

        string qry = "select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = 23" + distid + "";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlissueCenter.DataSource = ds.Tables[0];
            ddlissueCenter.DataTextField = "DepotName";
            ddlissueCenter.DataValueField = "DepotID";
            ddlissueCenter.DataBind();
            ddlissueCenter.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlissueCenter.DataSource = "";

            ddlissueCenter.DataBind();

            ddlissueCenter.Items.Insert(0, "--Select--");

        }
    }

    protected void get_Transporter()
    {
        string qry = "select Transporter_Name , Transporter_ID from Transporter_Table where Distt_ID = '" + distid + "' and Transport_ID = '7'";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddltransporter.DataSource = ds.Tables[0];
            ddltransporter.DataTextField = "Transporter_Name";
            ddltransporter.DataValueField = "Transporter_ID";
            ddltransporter.DataBind();
            ddltransporter.Items.Insert(0, "--Select--");
        }
        else
        {
            ddltransporter.DataSource = "";

            ddltransporter.DataBind();

            ddltransporter.Items.Insert(0, "--Select--");

        }

    }

    protected void get_fps()
    {
         
        try
        {
            string dist = distid;
            ddl_fps_name.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT fps_code FROM [tbl_rootchart_master]  where DistrictId = '" + dist + "' and IssueCenter = '"+ddlissueCenter.SelectedValue+"' ";
            cmd.Connection = con;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["fps_code"].ToString();
                lstitem.Value = dr["fps_code"].ToString();
                //ddl_block.SelectedValue = dr["block_code"].ToString();
                ddl_fps_name.Items.Add(lstitem);
            }

            dr.Close();

            for (int i = 0; i < ddl_fps_name.Items.Count; i++)
            {

                hd_fps.Value = hd_fps.Value + ddl_fps_name.Items[i].Value + ",";

            }

            if(ddl_fps_name.Items.Count == 0)
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Not Available.');</script>");

                GridView2.DataSource = "";
                GridView2.DataBind();
            }

        }
        catch (Exception)
        {

        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
     
        if (ddlissueCenter.SelectedItem.Text == "--Select--" || ddlissueCenter.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Issue center...'); </script> ");
            return;
        }

        if (ddltransporter.SelectedItem.Text == "--Select--" || ddltransporter.SelectedValue == "0" || ddltransporter.SelectedValue == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Transporter...'); </script> ");
            return;
        }

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (Session["issubmited"] == "Yes")
        {
            return;
        }

        else
        {
            if (ddlTOType.SelectedItem.Value == "MTO")
            {
                int x = 0;

                Session["DepotID"] = ddlissueCenter.SelectedValue;

                string issueCode = ddlissueCenter.SelectedValue;

                string AllotMonth = ddlmonth.SelectedItem.Value;

                string AllotYear = ddlyear.SelectedItem.Value;

                string Transporter = ddltransporter.SelectedValue;

                string selectmax = "select max(cast(TransportOrder as bigint)) as ToNum from DPY_TranportOrder where DistrictId='" + distid + "' and IssueCenter = '" + issueCode + "' ";

                SqlCommand cmdmax = new SqlCommand(selectmax, con);
                SqlDataAdapter damax = new SqlDataAdapter(cmdmax);

                DataSet dsmax = new DataSet();

                damax.Fill(dsmax);


                string TO_Num = dsmax.Tables[0].Rows[0]["ToNum"].ToString();

                if (TO_Num == "")
                {
                    TO_Num = issueCode + AllotMonth + AllotYear + "1000";
                }
                else
                {
                    string forto = TO_Num.Substring(TO_Num.Length - 4);

                    Int64 TO_Num_new = Convert.ToInt64(forto);

                    TO_Num_new = TO_Num_new + 1;

                    string combine = TO_Num_new.ToString();

                    TO_Num = issueCode + AllotMonth + AllotYear + combine;
                }


                for (int i = 0; i < ddl_fps_name.Items.Count; i++)
                {

                    string fpscode = ddl_fps_name.Items[i].Value;

                    string getdet = "select root_no , feed_no , duration_time from tbl_rootchart_master where Transporter_id = '" + Transporter + "' and fps_code = '" + fpscode + "' ";

                    SqlCommand cmmd = new SqlCommand(getdet, con);

                    SqlDataAdapter daa = new SqlDataAdapter(cmmd);

                    DataSet dss = new DataSet();

                    daa.Fill(dss);

                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        string route = dss.Tables[0].Rows[0]["root_no"].ToString();
                        string feed = dss.Tables[0].Rows[0]["feed_no"].ToString();
                        string date = dss.Tables[0].Rows[0]["duration_time"].ToString();

                        if (date == "" || date == null)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('रूट चार्ट में प्रदाय की संभावित दिनांक सही नहीं है अथवा डाली नहीं गयी है |'); </script> ");
                            return;
                        }

                        foreach (GridViewRow row in GridView2.Rows)
                        {
                            for (int j = 0; j < row.Cells.Count; j++)
                            {
                                string fps = row.Cells[j].Text;

                                if (fps == fpscode)
                                {
                                   // string wheatalloc = row.Cells[2].Text;

                                    Label txtwht = (Label)row.FindControl("lblwheat");

                                    double wheat_alloc = Convert.ToDouble(txtwht.Text);

                                   // string ricealloc = row.Cells[3].Text;

                                    Label txtrice = (Label)row.FindControl("lblrice");

                                    double roce_alloc = Convert.ToDouble(txtrice.Text);

                                   // string sugaralloc = row.Cells[4].Text;

                                    Label txtsug = (Label)row.FindControl("lblsugar");

                                    double sugar_alloc = Convert.ToDouble(txtsug.Text);

                                  //  string saltalloc = row.Cells[5].Text;

                                    Label txtslt = (Label)row.FindControl("lblSalt");

                                    double salt_alloc = Convert.ToDouble(txtslt.Text);

                                   // string Maizealloc = row.Cells[6].Text;

                                    Label txtmze = (Label)row.FindControl("lblMaize");

                                    double Maize_alloc = Convert.ToDouble(txtmze.Text);

                                    
                                    string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    string user = Session["OperatorIDDM"].ToString();

                                    string monn = ddlmonth.SelectedItem.Value;

                                    int mondte = Convert.ToInt32(monn);

                                    Int32 dismon = mondte - 1;

                                    string dtemonth = Convert.ToString(dismon);


                                    string TOrder_Date = dtemonth + '/' + date + '/' + ddlyear.SelectedItem.Text;

                                    string Valid_Date = dtemonth + '/' + date + '/' + ddlyear.SelectedItem.Text;

                                    string insqry = "Insert into DPY_TranportOrder (DistrictId ,IssueCenter ,TransportOrder ,TransporterId ,AllotMonth ,AllotYear ,RouteNumber ,FeedNumber,FPSCode,Comm_W,Comm_R,Comm_Sug,Comm_Salt,Comm_Maize,WheatAllot,RiceAllot,SugarAllot,SaltAllot,MaizeAllot,TransportDate ,ValidityDate ,CreatedDate ,IP ,OPId,SMS,DO_Challan,TOType) Values ('" + distid + "','" + issueCode + "','" + TO_Num + "','" + Transporter + "','" + AllotMonth + "','" + AllotYear + "','" + route + "','" + feed + "','" + fpscode + "' ,22,3,23,19,12," + wheat_alloc + "," + roce_alloc + "," + sugar_alloc + "," + salt_alloc + ","+Maize_alloc+",'" + TOrder_Date + "','" + Valid_Date + "',getdate(),'" + IP + "','" + user + "','N','N','MTO')";

                                    SqlCommand cmdinsert = new SqlCommand(insqry, con);

                                    x = cmdinsert.ExecuteNonQuery();
                                }


                            }
                        }

                    }

                }

                if (x > 0)
                {
                    Session["TO"] = TO_Num;

                    Session["sid"] = issueCode;

                    Session["issubmited"] = "Yes";

                    hlinkpdo.Visible = true;

                    btnSave.Enabled = false;

                    btnsms.Visible = true;

                    btnsms_transp.Visible = true;


                    lblto.Text = "Transport Order Number is : - '"+TO_Num+"'";

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Created , कृपया एफ पी एस को मेसेज भेजने हेतु Send Message पर क्लिक करे '); </script> ");

                }
            }

            else  // STO
            {

            }

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";

        Response.Redirect("~/District/DPY_TransportOrder.aspx");
    }

    protected void btnCLose_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void btnsms_Click(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null || Session["TO"] != null)
        {
            try
            {
            
                distid = Session["dist_id"].ToString();
                string transorder = Session["TO"].ToString();

                String username = "DITMP-FCS";
                String password = "dirfood@2013";


                string getdetail = "Select TransporterId, FPSCode,WheatAllot,RiceAllot,SugarAllot,SaltAllot,MaizeAllot ,convert(nvarchar,TransportDate,103)TransportDate from DPY_TranportOrder  where DistrictId = '" + distid + "' and TransportOrder = '" + transorder + "' and SMS = 'f' or SMS = 'N' ";

                SqlCommand cmdget = new SqlCommand(getdetail, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter dadetail = new SqlDataAdapter(cmdget);

                DataSet dsfps = new DataSet();

                dadetail.Fill(dsfps);

                int s = 0;
                int f = 0;

                int count = 0;

                string insertedfpscode;

                for (int j = 0; j < dsfps.Tables[0].Rows.Count; j++)
                {
                    insertedfpscode = dsfps.Tables[0].Rows[j]["FPSCode"].ToString();

                    string inserteddate = dsfps.Tables[0].Rows[j]["TransportDate"].ToString();
                    
                    string wheatAlloted = dsfps.Tables[0].Rows[j]["WheatAllot"].ToString();

                    string RiceAlloted = dsfps.Tables[0].Rows[j]["RiceAllot"].ToString();

                    string SugarAlloted = dsfps.Tables[0].Rows[j]["SugarAllot"].ToString();

                    string SaltAlloted = dsfps.Tables[0].Rows[j]["SaltAllot"].ToString();

                    string MaizeAlloted = dsfps.Tables[0].Rows[j]["MaizeAllot"].ToString();

                    string Transport_id = dsfps.Tables[0].Rows[j]["TransporterId"].ToString();

                    string getfpsnum = "select fps_Uname , Mob_No from pds.fps_master where district_code = '" + distid + "' and fps_code = '" + insertedfpscode + "' and Mob_No is not null and (len(mob_no) = 10 or len(mob_no) = 11) and SUBSTRING(mob_no,1,1) in ('0','9','8','7')";

                    SqlCommand cmdnum = new SqlCommand(getfpsnum, con_opdms);

                    if (con_opdms.State == ConnectionState.Closed)
                    {
                        con_opdms.Open();
                    }

                    SqlDataAdapter danum = new SqlDataAdapter(cmdnum);

                    DataSet dsnum = new DataSet();
                    danum.Fill(dsnum);

                    string rs = "";
                    // string mb = "";

                    if (dsnum.Tables[0].Rows.Count > 0)
                    {
                        string fpsname = dsnum.Tables[0].Rows[0]["fps_Uname"].ToString();

                       string fpsmobile = dsnum.Tables[0].Rows[0]["Mob_No"].ToString();

                        //string fpsmobile = "09827766365";


                        // string message = "शा.उचित मूल्य दूकान '" + fpsname + "'  आपका परिवहन आदेश जारी किया गया है जिसका स्कंध गेहूँ '" + wheatAlloted + "', चावल  '" + RiceAlloted + "', शक्कर  '" + SugarAlloted + "', नमक '" + SaltAlloted + "' क्वि. है ,जो की दिनांक '" + inserteddate + "' तक पहुचना अपेक्षित है | ";

                        string message = "शा.उचित मूल्य दूकान '" + fpsname + "','"+insertedfpscode+"' स्कंध परिवहन हेतु आदेश आज जारी किया गया है, जिसका स्कंध दिनांक '" + inserteddate + "' तक पहुंचना अपेक्षित है | ";

                        String mobileNos = fpsmobile;
                        String senderid = "MPSCSC";

                        Int64 msgln = message.Length;
                        String finalmessage = "";
                        String sss = "";
                        char ch;


                        for (int i = 0; i < message.Length; i++)
                        {

                            ch = message[i];
                            int z = (int)ch;
                            // System.out.println("iiii::"+j);

                            sss = "&#" + z + ";";
                            finalmessage = finalmessage + sss;
                        }

                        message = finalmessage;
                        Int64 msgln3 = message.Length;

                        request = (HttpWebRequest)WebRequest.Create("http://msdgweb.mgov.gov.in/esms/sendsmsrequest");
                        request.ProtocolVersion = HttpVersion.Version10;
                        //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
                        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
                        request.Method = "POST";


                        String smsservicetype = "unicodemsg"; // For bulk msg
                        //String smsservicetype = "bulkmsg"; // For bulk msg

                        String query = "username=" + HttpUtility.UrlEncode(username)
                                     + "&password=" + HttpUtility.UrlEncode(password)
                                      + "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype)
                                       + "&content=" + HttpUtility.UrlEncode(message)
                                        + "&bulkmobno=" + HttpUtility.UrlEncode(mobileNos, Encoding.UTF8)
                                         + "&senderid=" + HttpUtility.UrlEncode(senderid);

                        request.ContentType = "application/x-www-form-urlencoded";

                        request.ContentLength = query.Length;
                        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

                        byte[] byteArray = Encoding.UTF8.GetBytes(query);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = byteArray.Length;
                        dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        WebResponse response = request.GetResponse();
                        String Status = ((HttpWebResponse)response).StatusDescription;
                        dataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        reader.Close();
                        dataStream.Close();
                        response.Close();

                        string[] smsresponse = responseFromServer.Split(',');

                        rs = smsresponse[0];
                        //mb = smsresponse[1];
                        
                        string Sta1 = "";
                        if (rs.Trim().ToString() == "402")
                        {
                            Sta1 = "S";
                            s++;

                        }
                        else
                        {
                            Sta1 = "F";
                            f++;

                        }

                        SqlCommand cmdIn = new SqlCommand();
                        cmdIn.Connection = con;
                        string qry = "update DPY_TranportOrder set SMS = '" + Sta1 + "' where DistrictId = '" + distid + "' and TransportOrder = '" + Session["TO"].ToString() + "' and FPSCode = '" + insertedfpscode + "' ";
                        cmdIn.CommandType = CommandType.Text;
                        cmdIn.CommandText = qry;
                        cmdIn.CommandTimeout = 0;
                        int tt = cmdIn.ExecuteNonQuery();    // 

                    }

                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('मेसेज नहीं भेजा जा सकता ,उचित मूल्य दूकान का मोबाइल नंबर गलत है अथवा भरा नहीं गया |'); </script> ");
                    }
                }

                count++;  // Loop Count
                lbl_res.Text = "Total Success :-" + s.ToString() + ",   Fail: " + f.ToString();
                lbl_status.Text = "SMS Send..." + Convert.ToString(count).ToString();

                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('SMS Sending Sucessfully'); </script> ");
                
                btnsms.Enabled = false;
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                if (con_opdms.State == ConnectionState.Open)
                {
                    con_opdms.Close();
                }

            }

            catch (Exception exx)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                if (con_opdms.State == ConnectionState.Open)
                {
                    con_opdms.Close();
                }

                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('" + exx + "'); </script> ");
            }

            
            
        }
    }

    protected void btnsms_transp_Click(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null || Session["TO"] != null)
        {

            if (Session["SendtoTrans"] == "Yes")
            {
                return;
            }

            else
            {
                distid = Session["dist_id"].ToString();
                string transorder = Session["TO"].ToString();

                String username = "DITMP-FCS";
                String password = "dirfood@2013";

                int a = 0;
                int b = 0;

                # region Message_Transporter

                string transnum = "select Transporter_Table.MobileNo from Transporter_Table where Transporter_ID in (select distinct TransporterId from DPY_TranportOrder where TransportOrder = '" + transorder + "') and (len(MobileNo) = 11 or len(MobileNo) = 10) and SUBSTRING(MobileNo,1,1) in ('0','9','8','7')";

                SqlCommand cmd_tnum = new SqlCommand(transnum, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da_tnum = new SqlDataAdapter(cmd_tnum);

                DataSet ds_tnum = new DataSet();

                da_tnum.Fill(ds_tnum);

                try
                {
                    if (ds_tnum.Tables[0].Rows.Count > 0)
                    {
                        string Transport_Mob = ds_tnum.Tables[0].Rows[0]["MobileNo"].ToString();

                        string messageTrans = "शा.उचित मूल्य दूकान के स्कंध परिवहन हेतु आदेश क्रमांक '" + transorder + "' आज जारी किया गया है, स्कंध निर्धारित दिनांक तक पहुंचाना अपेक्षित है | ";

                        String Tran_mobileNos = Transport_Mob;
                        String senderid = "MPSCSC";

                        Int64 Tmsgln = messageTrans.Length;
                        String T_finalmessage = "";
                        String Tsss = "";
                        char Tch;

                        for (int y = 0; y < messageTrans.Length; y++)
                        {

                            Tch = messageTrans[y];
                            int zx = (int)Tch;
                            // System.out.println("iiii::"+j);

                            Tsss = "&#" + zx + ";";
                            T_finalmessage = T_finalmessage + Tsss;
                        }

                        messageTrans = T_finalmessage;
                        Int64 Tmsgln3 = messageTrans.Length;

                        request = (HttpWebRequest)WebRequest.Create("http://msdgweb.mgov.gov.in/esms/sendsmsrequest");
                        request.ProtocolVersion = HttpVersion.Version10;
                        //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
                        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
                        request.Method = "POST";


                        String smsservicetype = "unicodemsg"; // For bulk msg
                        //String smsservicetype = "bulkmsg"; // For bulk msg

                        String query = "username=" + HttpUtility.UrlEncode(username)
                                     + "&password=" + HttpUtility.UrlEncode(password)
                                      + "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype)
                                       + "&content=" + HttpUtility.UrlEncode(messageTrans)
                                        + "&bulkmobno=" + HttpUtility.UrlEncode(Tran_mobileNos, Encoding.UTF8)
                                         + "&senderid=" + HttpUtility.UrlEncode(senderid);

                        request.ContentType = "application/x-www-form-urlencoded";

                        request.ContentLength = query.Length;
                        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

                        byte[] byteArray = Encoding.UTF8.GetBytes(query);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = byteArray.Length;
                        dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        WebResponse response = request.GetResponse();
                        String Status = ((HttpWebResponse)response).StatusDescription;
                        dataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        reader.Close();
                        dataStream.Close();
                        response.Close();

                        string[] smsresponse = responseFromServer.Split(',');

                        string rs1 = smsresponse[0];
                        //mb = smsresponse[1];

                        string Sta2 = "";
                        if (rs1.Trim().ToString() == "402")
                        {
                            Sta2 = "S";
                            a++;

                        }
                        else
                        {
                            Sta2 = "F";
                            b++;

                        }

                        SqlCommand cmdTn = new SqlCommand();
                        cmdTn.Connection = con;
                        string qryT = "Insert into DPY_Transport_SMS ([DistrictId] ,[IssueCenter] ,[TransporterId] ,[TransportOrder] ,[AllotMonth] ,[AllotYear] ,[SMS]) Values ('" + distid + "','" + Session["DepotID"].ToString() + "','" + ddltransporter.SelectedValue + "','" + transorder + "'," + ddlmonth.SelectedItem.Value + "," + ddlyear.SelectedItem + ",'" + Sta2 + "')  ";
                        cmdTn.CommandType = CommandType.Text;
                        cmdTn.CommandText = qryT;
                        cmdTn.CommandTimeout = 0;
                        int ttt = cmdTn.ExecuteNonQuery();

                        if (ttt > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('परिवहनकर्ता एस एम् एस भेज दिया गया है | '); </script> ");

                        }

                        btnsms_transp.Enabled = false;

                        Session["SendtoTrans"] = "Yes";

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }


                    }

                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('एस एम् एस नहीं भेजा जा सकता है ,परिवहनकर्ता का मोबाइल नंबर गलत है अथवा भरा नहीं गया है | '); </script> ");

                    }
                }

                catch
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }


                # endregion
            }
        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Unable to Get Transport Order Number '); </script> ");

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            wheat += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "wheat"));

            rice += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "rice"));

            sugar += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "sugar_alloc"));

            salt += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "salt_alloc"));

            maize += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Maize"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblwheat1 = (Label)e.Row.FindControl("lbl_wheat");

            lblwheat1.Text = wheat.ToString();

            Label lblrice1 = (Label)e.Row.FindControl("lbl_rice");

            lblrice1.Text = rice.ToString();


            Label lblsugar1 = (Label)e.Row.FindControl("lbl_sugar");

            lblsugar1.Text = sugar.ToString();

            Label lblsalt1 = (Label)e.Row.FindControl("lbl_Salt");

            lblsalt1.Text = salt.ToString();


            Label lblmaize = (Label)e.Row.FindControl("lbl_Maize");

            lblmaize.Text = maize.ToString();



        }
    }
    
}
