using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Globalization;

public partial class IssueCenter_Partial_Rejection : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2016"].ToString());

    //By A public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    //By A public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());

    SqlCommand cmd_wpm = new SqlCommand();
    SqlCommand cmd_con = new SqlCommand();

    string gdn = "";
    string distid = "";
    string issuecentreid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            issuecentreid = Session["issue_id"].ToString();

            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

          

            if (!IsPostBack)
            {
                string getcom = "SELECT Commodity_Name ,Commodity_Id FROM Procurement_COMMODITY";
                SqlCommand cmd = new SqlCommand(getcom, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommodtiy.DataSource = ds.Tables[0];

                    ddlcommodtiy.DataTextField = "Commodity_Name";
                    ddlcommodtiy.DataValueField = "Commodity_Id";

                    ddlcommodtiy.DataBind();

                    ddlcommodtiy.Items.Insert(0, "--Select--");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                DaintyDate3.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                HyperLink1.Attributes.Add("onclick", "window.open('Print_RejectNote_New.aspx',null,'left=50, top=10, height=570, width= 690, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            }

        }

    }

    void GetGodown()
    {
        if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्वीकृति पत्रक दिनांक चुने'); </script> ");
        }
        else
        {
            //DateTime dateTime = DateTime.Parse(DaintyDate3.Text);

           // string Ayear = dateTime.Year.ToString();

            if (ddlcommodtiy.SelectedValue == "22")  //Wheat(PSS)
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement2016 where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecentreid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Receipt_Id in (select Acceptance_Note_Detail2016.IssueID from Acceptance_Note_Detail2016 where Reject_Qty = 0)and GodownName is not null";

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
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else if (ddlcommodtiy.SelectedValue == "13")  //Paddy-Common
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecentreid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where Reject_Qty = 0)and GodownName is not null";

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
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else if (ddlcommodtiy.SelectedValue == "14")  //Paddy-Grade-A
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecentreid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where Reject_Qty = 0)and GodownName is not null";

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
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else if (ddlcommodtiy.SelectedValue == "11")  //Jowar
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecentreid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where Reject_Qty = 0)and GodownName is not null";

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
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else if (ddlcommodtiy.SelectedValue == "12")  //Maizei(Makka)
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecentreid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where Reject_Qty = 0)and GodownName is not null";

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
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else if (ddlcommodtiy.SelectedValue == "8")  //Bajra
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecentreid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where Reject_Qty = 0)and GodownName is not null";

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
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else if (ddlcommodtiy.SelectedValue == "9")  // barley
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecentreid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where Reject_Qty = 0)and GodownName is not null";

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
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
 
            }

        }
        
       
    }

    void getdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string pdate = getDate_MDY(DaintyDate3.Text);

        gdn = ddlgodown.SelectedValue;

        if (ddlcommodtiy.SelectedValue == "22")  //Wheat(PSS)
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement2016.TC_Number ,SCSC_Procurement2016.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement2016 where Recd_Date = '" + pdate + "' and SCSC_Procurement2016.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement2016 where Recd_Date = '" + pdate + "' and SCSC_Procurement2016.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement2016 inner join Society  on Society.Society_Id = SCSC_Procurement2016.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement2016.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Detail2016.IssueID from Acceptance_Note_Detail2016 where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";

            SqlDataAdapter da = new SqlDataAdapter(getdata, con);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                lblSocId.Text = dschdt.Tables[0].Rows[0]["Society_Id"].ToString();

                txtSocName.Text = dschdt.Tables[0].Rows[0]["Society"].ToString();

                TxtTruckNumber.Text = dschdt.Tables[0].Rows[0]["Truck_Number"].ToString();

                txtTcNumber.Text = dschdt.Tables[0].Rows[0]["TC_Number"].ToString();

                txtsendQty.Text = dschdt.Tables[0].Rows[0]["Quantity"].ToString();

                txtRecdQty.Text = dschdt.Tables[0].Rows[0]["Recd_Qty"].ToString();

                txtsendbags.Text = dschdt.Tables[0].Rows[0]["sendbags"].ToString();

                txtrecbags.Text = dschdt.Tables[0].Rows[0]["Recd_Bags"].ToString();

                btnsubmit.Visible = true;

            }

            else
            {
                txtSocName.Text = "";

                TxtTruckNumber.Text = "";

                txtTcNumber.Text = "";

                txtsendQty.Text = "";

                txtRecdQty.Text = "";

                txtsendbags.Text = "";

                txtrecbags.Text = "";

                btnsubmit.Visible = false;

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
            }
        }

        else if (ddlcommodtiy.SelectedValue == "13")  //Paddy Common
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement inner join Society  on Society.Society_Id = SCSC_Procurement.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";

            SqlDataAdapter da = new SqlDataAdapter(getdata, con);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                lblSocId.Text = dschdt.Tables[0].Rows[0]["Society_Id"].ToString();

                txtSocName.Text = dschdt.Tables[0].Rows[0]["Society"].ToString();

                TxtTruckNumber.Text = dschdt.Tables[0].Rows[0]["Truck_Number"].ToString();

                txtTcNumber.Text = dschdt.Tables[0].Rows[0]["TC_Number"].ToString();

                txtsendQty.Text = dschdt.Tables[0].Rows[0]["Quantity"].ToString();

                txtRecdQty.Text = dschdt.Tables[0].Rows[0]["Recd_Qty"].ToString();

                txtsendbags.Text = dschdt.Tables[0].Rows[0]["sendbags"].ToString();

                txtrecbags.Text = dschdt.Tables[0].Rows[0]["Recd_Bags"].ToString();

                btnsubmit.Visible = true;

            }

            else
            {
                txtSocName.Text = "";

                TxtTruckNumber.Text = "";

                txtTcNumber.Text = "";

                txtsendQty.Text = "";

                txtRecdQty.Text = "";

                txtsendbags.Text = "";

                txtrecbags.Text = "";

                btnsubmit.Visible = false;

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
            }
        }

        else if (ddlcommodtiy.SelectedValue == "14")  //Paddy Grade A
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement inner join Society  on Society.Society_Id = SCSC_Procurement.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";

            SqlDataAdapter da = new SqlDataAdapter(getdata, con);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                lblSocId.Text = dschdt.Tables[0].Rows[0]["Society_Id"].ToString();

                txtSocName.Text = dschdt.Tables[0].Rows[0]["Society"].ToString();

                TxtTruckNumber.Text = dschdt.Tables[0].Rows[0]["Truck_Number"].ToString();

                txtTcNumber.Text = dschdt.Tables[0].Rows[0]["TC_Number"].ToString();

                txtsendQty.Text = dschdt.Tables[0].Rows[0]["Quantity"].ToString();

                txtRecdQty.Text = dschdt.Tables[0].Rows[0]["Recd_Qty"].ToString();

                txtsendbags.Text = dschdt.Tables[0].Rows[0]["sendbags"].ToString();

                txtrecbags.Text = dschdt.Tables[0].Rows[0]["Recd_Bags"].ToString();

                btnsubmit.Visible = true;

            }

            else
            {
                txtSocName.Text = "";

                TxtTruckNumber.Text = "";

                txtTcNumber.Text = "";

                txtsendQty.Text = "";

                txtRecdQty.Text = "";

                txtsendbags.Text = "";

                txtrecbags.Text = "";

                btnsubmit.Visible = false;

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
            }
        }

        else if (ddlcommodtiy.SelectedValue == "11")  //Jowar
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement inner join Society  on Society.Society_Id = SCSC_Procurement.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";

            SqlDataAdapter da = new SqlDataAdapter(getdata, con);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                lblSocId.Text = dschdt.Tables[0].Rows[0]["Society_Id"].ToString();

                txtSocName.Text = dschdt.Tables[0].Rows[0]["Society"].ToString();

                TxtTruckNumber.Text = dschdt.Tables[0].Rows[0]["Truck_Number"].ToString();

                txtTcNumber.Text = dschdt.Tables[0].Rows[0]["TC_Number"].ToString();

                txtsendQty.Text = dschdt.Tables[0].Rows[0]["Quantity"].ToString();

                txtRecdQty.Text = dschdt.Tables[0].Rows[0]["Recd_Qty"].ToString();

                txtsendbags.Text = dschdt.Tables[0].Rows[0]["sendbags"].ToString();

                txtrecbags.Text = dschdt.Tables[0].Rows[0]["Recd_Bags"].ToString();

                btnsubmit.Visible = true;

            }

            else
            {
                txtSocName.Text = "";

                TxtTruckNumber.Text = "";

                txtTcNumber.Text = "";

                txtsendQty.Text = "";

                txtRecdQty.Text = "";

                txtsendbags.Text = "";

                txtrecbags.Text = "";

                btnsubmit.Visible = false;

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
            }
        }

        else if (ddlcommodtiy.SelectedValue == "12")  //Maize
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement inner join Society  on Society.Society_Id = SCSC_Procurement.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";

            SqlDataAdapter da = new SqlDataAdapter(getdata, con);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                lblSocId.Text = dschdt.Tables[0].Rows[0]["Society_Id"].ToString();

                txtSocName.Text = dschdt.Tables[0].Rows[0]["Society"].ToString();

                TxtTruckNumber.Text = dschdt.Tables[0].Rows[0]["Truck_Number"].ToString();

                txtTcNumber.Text = dschdt.Tables[0].Rows[0]["TC_Number"].ToString();

                txtsendQty.Text = dschdt.Tables[0].Rows[0]["Quantity"].ToString();

                txtRecdQty.Text = dschdt.Tables[0].Rows[0]["Recd_Qty"].ToString();

                txtsendbags.Text = dschdt.Tables[0].Rows[0]["sendbags"].ToString();

                txtrecbags.Text = dschdt.Tables[0].Rows[0]["Recd_Bags"].ToString();

                btnsubmit.Visible = true;

            }

            else
            {
                txtSocName.Text = "";

                TxtTruckNumber.Text = "";

                txtTcNumber.Text = "";

                txtsendQty.Text = "";

                txtRecdQty.Text = "";

                txtsendbags.Text = "";

                txtrecbags.Text = "";

                btnsubmit.Visible = false;

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
            }
        }

        else if (ddlcommodtiy.SelectedValue == "8")  //Bajra
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement inner join Society  on Society.Society_Id = SCSC_Procurement.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";

            SqlDataAdapter da = new SqlDataAdapter(getdata, con);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                lblSocId.Text = dschdt.Tables[0].Rows[0]["Society_Id"].ToString();

                txtSocName.Text = dschdt.Tables[0].Rows[0]["Society"].ToString();

                TxtTruckNumber.Text = dschdt.Tables[0].Rows[0]["Truck_Number"].ToString();

                txtTcNumber.Text = dschdt.Tables[0].Rows[0]["TC_Number"].ToString();

                txtsendQty.Text = dschdt.Tables[0].Rows[0]["Quantity"].ToString();

                txtRecdQty.Text = dschdt.Tables[0].Rows[0]["Recd_Qty"].ToString();

                txtsendbags.Text = dschdt.Tables[0].Rows[0]["sendbags"].ToString();

                txtrecbags.Text = dschdt.Tables[0].Rows[0]["Recd_Bags"].ToString();

                btnsubmit.Visible = true;

            }

            else
            {
                txtSocName.Text = "";

                TxtTruckNumber.Text = "";

                txtTcNumber.Text = "";

                txtsendQty.Text = "";

                txtRecdQty.Text = "";

                txtsendbags.Text = "";

                txtrecbags.Text = "";

                btnsubmit.Visible = false;

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
            }
        }

        else if (ddlcommodtiy.SelectedValue == "9")  //Barley
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement where Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement inner join Society  on Society.Society_Id = SCSC_Procurement.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Detail.IssueID from Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";

            SqlDataAdapter da = new SqlDataAdapter(getdata, con);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                lblSocId.Text = dschdt.Tables[0].Rows[0]["Society_Id"].ToString();

                txtSocName.Text = dschdt.Tables[0].Rows[0]["Society"].ToString();

                TxtTruckNumber.Text = dschdt.Tables[0].Rows[0]["Truck_Number"].ToString();

                txtTcNumber.Text = dschdt.Tables[0].Rows[0]["TC_Number"].ToString();

                txtsendQty.Text = dschdt.Tables[0].Rows[0]["Quantity"].ToString();

                txtRecdQty.Text = dschdt.Tables[0].Rows[0]["Recd_Qty"].ToString();

                txtsendbags.Text = dschdt.Tables[0].Rows[0]["sendbags"].ToString();

                txtrecbags.Text = dschdt.Tables[0].Rows[0]["Recd_Bags"].ToString();

                btnsubmit.Visible = true;

            }

            else
            {
                txtSocName.Text = "";

                TxtTruckNumber.Text = "";

                txtTcNumber.Text = "";

                txtsendQty.Text = "";

                txtRecdQty.Text = "";

                txtsendbags.Text = "";

                txtrecbags.Text = "";

                btnsubmit.Visible = false;

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Date'); </script> ");
            return;
        }

        string pdate = getDate_MDY(DaintyDate3.Text);

        if (ddlcommodtiy.SelectedValue == "22")  //Wheat(PSS)
        {
            string getiss = " SELECT SCSC_Procurement2016.Receipt_Id + ' (' + SCSC_Procurement2016.TC_Number + ')' as Receipt , SCSC_Procurement2016.Receipt_Id FROM SCSC_Procurement2016  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement2016.IssueCenter_ID = '" + issuecentreid + "' and AN_Status = 'Y' and Quantity <> Recd_Qty";

            SqlCommand cmd = new SqlCommand(getiss, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissueId.DataSource = ds.Tables[0];

                ddlissueId.DataTextField = "Receipt";
                ddlissueId.DataValueField = "Receipt_Id";

                ddlissueId.DataBind();

                ddlissueId.Items.Insert(0, "--Select--");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी क्रमांक उपलब्ध नहीं है |'); </script> ");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }

            
        }

        else if (ddlcommodtiy.SelectedValue == "13")  //Paddy-Common
        {
            string getiss = " SELECT SCSC_Procurement.Receipt_Id + ' (' + SCSC_Procurement.TC_Number + ')' as Receipt , SCSC_Procurement.Receipt_Id FROM SCSC_Procurement  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and AN_Status = 'Y' and Quantity <> Recd_Qty";

            SqlCommand cmd = new SqlCommand(getiss, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissueId.DataSource = ds.Tables[0];

                ddlissueId.DataTextField = "Receipt";
                ddlissueId.DataValueField = "Receipt_Id";

                ddlissueId.DataBind();

                ddlissueId.Items.Insert(0, "--Select--");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी क्रमांक उपलब्ध नहीं है |'); </script> ");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

           

            if (con_paddy.State == ConnectionState.Open)
            {
                con_paddy.Close();
            }

           
        }

        else if (ddlcommodtiy.SelectedValue == "14")  //Paddy-Grade-A
        {
            string getiss = " SELECT SCSC_Procurement.Receipt_Id + ' (' + SCSC_Procurement.TC_Number + ')' as Receipt , SCSC_Procurement.Receipt_Id FROM SCSC_Procurement  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and AN_Status = 'Y' and Quantity <> Recd_Qty";

            SqlCommand cmd = new SqlCommand(getiss, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissueId.DataSource = ds.Tables[0];

                ddlissueId.DataTextField = "Receipt";
                ddlissueId.DataValueField = "Receipt_Id";

                ddlissueId.DataBind();

                ddlissueId.Items.Insert(0, "--Select--");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी क्रमांक उपलब्ध नहीं है |'); </script> ");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            

            if (con_paddy.State == ConnectionState.Open)
            {
                con_paddy.Close();
            }
           
        }

        else if (ddlcommodtiy.SelectedValue == "11")  //Jowar
        {
            string getiss = " SELECT SCSC_Procurement.Receipt_Id + ' (' + SCSC_Procurement.TC_Number + ')' as Receipt , SCSC_Procurement.Receipt_Id FROM SCSC_Procurement  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and AN_Status = 'Y' and Quantity <> Recd_Qty";

            SqlCommand cmd = new SqlCommand(getiss, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissueId.DataSource = ds.Tables[0];

                ddlissueId.DataTextField = "Receipt";
                ddlissueId.DataValueField = "Receipt_Id";

                ddlissueId.DataBind();

                ddlissueId.Items.Insert(0, "--Select--");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी क्रमांक उपलब्ध नहीं है |'); </script> ");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
                        
            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "12")  //Maizei(Makka)
        {
            string getiss = " SELECT SCSC_Procurement.Receipt_Id + ' (' + SCSC_Procurement.TC_Number + ')' as Receipt , SCSC_Procurement.Receipt_Id FROM SCSC_Procurement  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and AN_Status = 'Y' and Quantity <> Recd_Qty";

            SqlCommand cmd = new SqlCommand(getiss, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissueId.DataSource = ds.Tables[0];

                ddlissueId.DataTextField = "Receipt";
                ddlissueId.DataValueField = "Receipt_Id";

                ddlissueId.DataBind();

                ddlissueId.Items.Insert(0, "--Select--");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी क्रमांक उपलब्ध नहीं है |'); </script> ");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "8")  //Bajra
        {
            string getiss = " SELECT SCSC_Procurement.Receipt_Id + ' (' + SCSC_Procurement.TC_Number + ')' as Receipt , SCSC_Procurement.Receipt_Id FROM SCSC_Procurement  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and AN_Status = 'Y' and Quantity <> Recd_Qty";

            SqlCommand cmd = new SqlCommand(getiss, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissueId.DataSource = ds.Tables[0];

                ddlissueId.DataTextField = "Receipt";
                ddlissueId.DataValueField = "Receipt_Id";

                ddlissueId.DataBind();

                ddlissueId.Items.Insert(0, "--Select--");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी क्रमांक उपलब्ध नहीं है |'); </script> ");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "9")  //Barley
        {
            string getiss = " SELECT SCSC_Procurement.Receipt_Id + ' (' + SCSC_Procurement.TC_Number + ')' as Receipt , SCSC_Procurement.Receipt_Id FROM SCSC_Procurement  where Recd_Godown = '" + ddlgodown.SelectedValue + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement.IssueCenter_ID = '" + issuecentreid + "' and AN_Status = 'Y' and Quantity <> Recd_Qty";

            SqlCommand cmd = new SqlCommand(getiss, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissueId.DataSource = ds.Tables[0];

                ddlissueId.DataTextField = "Receipt";
                ddlissueId.DataValueField = "Receipt_Id";

                ddlissueId.DataBind();

                ddlissueId.Items.Insert(0, "--Select--");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी क्रमांक उपलब्ध नहीं है |'); </script> ");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }
        

        
    }

    protected void chk_faq_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_faq.Checked)
        {
            txt_faq_per.ReadOnly = false;
        }
        else
        {
            txt_faq_per.ReadOnly = true;
        }
    }
    
    protected void chk_extra_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_extra.Checked)
        {
            txt_extra_per.ReadOnly = false;
        }
        else
        {
            txt_extra_per.ReadOnly = true;
        }
    }

    protected void chk_damaged_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_damaged.Checked)
        {
            txt_damage_per.ReadOnly = false;
        }
        else
        {
            txt_damage_per.ReadOnly = true;
        }

    }

    protected void chk_brightness_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_brightness.Checked)
        {
            txt_bright_per.ReadOnly = false;
        }
        else
        {
            txt_bright_per.ReadOnly = true;
        }

    }

    protected void chk_partially_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_partially.Checked)
        {
            txt_partial_per.ReadOnly = false;
        }
        else
        {
            txt_partial_per.ReadOnly = true;
        }

    }

    protected void chk_splited_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_splited.Checked)
        {
            txt_split_per.ReadOnly = false;
        }
        else
        {
            txt_split_per.ReadOnly = true;
        }

    }

    protected void chk_moist_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_moist.Checked)
        {
            txt_moist_per.ReadOnly = false;
        }
        else
        {
            txt_moist_per.ReadOnly = true;
        }

    }

    protected void ddlcommodtiy_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
        if (chk_brightness.Checked || chk_damaged.Checked || chk_extra.Checked || chk_faq.Checked || chk_partially.Checked || chk_splited.Checked || txtreason.Text != "")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string tcnum = txtTcNumber.Text;
            string trucknumber = TxtTruckNumber.Text;

            string recdate = getDate_MDY(DaintyDate3.Text);
            string issueid = ddlissueId.SelectedValue;

            string socid = lblSocId.Text;

            if (ddlcommodtiy.SelectedValue == "22")  //Wheat(PSS)
            {
                # region Check

                string checkin = "SELECT * FROM Acceptance_Note_Detail2016 where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                SqlCommand cmd = new SqlCommand(checkin, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    # region wheat
                    if (ddlcommodtiy.SelectedValue == "22")
                    {
                        if (con_WPMS.State == ConnectionState.Closed)
                        {
                            con_WPMS.Open();
                        }

                        SqlTransaction trns1;

                        SqlCommand cmdwpm = new SqlCommand();

                        cmdwpm.Connection = con_WPMS;

                        trns1 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        cmdwpm.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlTransaction trns;

                        SqlCommand cmdcsms = new SqlCommand();


                        cmdcsms.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdcsms.Transaction = trns;

                        try
                        {
                            string value_brightness = "0";
                            string value_damaged = "0";
                            string value_extra = "0";
                            string value_faq = "0";
                            string value_partially = "0";
                            string value_splited = "0";
                            string value_moist = "0";


                            if (chk_brightness.Checked)
                            {
                                value_brightness = "1";
                            }
                            if (chk_damaged.Checked)
                            {
                                value_damaged = "1";
                            }
                            if (chk_extra.Checked)
                            {
                                value_extra = "1";
                            }
                            if (chk_faq.Checked)
                            {
                                value_faq = "1";
                            }
                            if (chk_partially.Checked)
                            {
                                value_partially = "1";
                            }

                            if (chk_splited.Checked)
                            {
                                value_splited = "1";
                            }

                            if (chk_moist.Checked)
                            {
                                value_moist = "1";
                            }

                            double rejqty = Convert.ToDouble(txtqtyDiff.Text);

                            Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                            string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdwpm.CommandText = updtwpms;


                            string updtcsms = "Update Acceptance_Note_Detail2016 set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdcsms.CommandText = updtcsms;

                            int x = cmdwpm.ExecuteNonQuery();

                            int count = cmdcsms.ExecuteNonQuery();

                            if (count > 0)
                            {
                                trns1.Commit();

                                if (con_WPMS.State == ConnectionState.Open)
                                {
                                    con_WPMS.Close();
                                }


                                trns.Commit();


                                string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + distid + "','" + issuecentreid + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                SqlCommand cmd_rej = new SqlCommand(insrej, con);

                                int xx = cmd_rej.ExecuteNonQuery();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                btnsubmit.Enabled = false;

                                HyperLink1.Visible = true;

                                Session["Godown"] = ddlgodown.SelectedValue;

                                Session["ReceiptID"] = ddlissueId.SelectedValue;

                                if (ddlcommodtiy.SelectedValue == "22")
                                {
                                    Session["Commodity_Id"] = "1";
                                }

                            }
                        }

                        catch (Exception ex)
                        {
                            trns1.Rollback();

                            trns.Rollback();


                            Label9.Text = "error:6" + ex.Message;
                            Label9.Visible = true;
                        }

                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            if (con_WPMS.State == ConnectionState.Open)
                            {
                                con_WPMS.Close();
                            }
                        }

                    }

                    # endregion

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें |'); </script> ");
                    return;
                }


                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "13")  //Paddy Com
            {
                # region Check

                string checkin = "SELECT * FROM Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                SqlCommand cmd = new SqlCommand(checkin, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
               
                    # region paddy
                    if (ddlcommodtiy.SelectedValue == "13" || ddlcommodtiy.SelectedValue == "14")
                    {
                        if (con_paddy.State == ConnectionState.Closed)
                        {
                            con_paddy.Open();
                        }

                        SqlTransaction trns1;

                        SqlCommand cmdwpm = new SqlCommand();

                        cmdwpm.Connection = con_paddy;

                        trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        cmdwpm.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlTransaction trns;

                        SqlCommand cmdcsms = new SqlCommand();


                        cmdcsms.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdcsms.Transaction = trns;

                        try
                        {
                            string value_brightness = "0";
                            string value_damaged = "0";
                            string value_extra = "0";
                            string value_faq = "0";
                            string value_partially = "0";
                            string value_splited = "0";
                            string value_moist = "0";


                            if (chk_brightness.Checked)
                            {
                                value_brightness = "1";
                            }
                            if (chk_damaged.Checked)
                            {
                                value_damaged = "1";
                            }
                            if (chk_extra.Checked)
                            {
                                value_extra = "1";
                            }
                            if (chk_faq.Checked)
                            {
                                value_faq = "1";
                            }
                            if (chk_partially.Checked)
                            {
                                value_partially = "1";
                            }

                            if (chk_splited.Checked)
                            {
                                value_splited = "1";
                            }

                            if (chk_moist.Checked)
                            {
                                value_moist = "1";
                            }

                            double rejqty = Convert.ToDouble(txtqtyDiff.Text);

                            Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                            string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdwpm.CommandText = updtwpms;


                            string updtcsms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdcsms.CommandText = updtcsms;

                            int x = cmdwpm.ExecuteNonQuery();

                            int count = cmdcsms.ExecuteNonQuery();

                            if (count > 0)
                            {
                                trns1.Commit();

                                if (con_paddy.State == ConnectionState.Open)
                                {
                                    con_paddy.Close();
                                }


                                trns.Commit();


                                string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + distid + "','" + issuecentreid + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                SqlCommand cmd_rej = new SqlCommand(insrej, con);

                                int xx = cmd_rej.ExecuteNonQuery();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                btnsubmit.Enabled = false;

                                HyperLink1.Visible = true;

                                Session["Godown"] = ddlgodown.SelectedValue;

                                Session["ReceiptID"] = ddlissueId.SelectedValue;

                                if (ddlcommodtiy.SelectedValue == "13")
                                {
                                    Session["Commodity_Id"] = "2";
                                }
                                else
                                {
                                    Session["Commodity_Id"] = "3";
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            trns1.Rollback();

                            trns.Rollback();


                            Label9.Text = "error:6" + ex.Message;
                            Label9.Visible = true;
                        }

                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            if (con_paddy.State == ConnectionState.Open)
                            {
                                con_paddy.Close();
                            }
                        }

                    }

                    # endregion

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें |'); </script> ");
                    return;
                }


                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "14")  //Paddy G A
            {
                # region Check

                string checkin = "SELECT * FROM Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                SqlCommand cmd = new SqlCommand(checkin, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
               
                    # region paddy
                    if (ddlcommodtiy.SelectedValue == "13" || ddlcommodtiy.SelectedValue == "14")
                    {
                        if (con_paddy.State == ConnectionState.Closed)
                        {
                            con_paddy.Open();
                        }

                        SqlTransaction trns1;

                        SqlCommand cmdwpm = new SqlCommand();

                        cmdwpm.Connection = con_paddy;

                        trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        cmdwpm.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlTransaction trns;

                        SqlCommand cmdcsms = new SqlCommand();


                        cmdcsms.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdcsms.Transaction = trns;

                        try
                        {
                            string value_brightness = "0";
                            string value_damaged = "0";
                            string value_extra = "0";
                            string value_faq = "0";
                            string value_partially = "0";
                            string value_splited = "0";
                            string value_moist = "0";


                            if (chk_brightness.Checked)
                            {
                                value_brightness = "1";
                            }
                            if (chk_damaged.Checked)
                            {
                                value_damaged = "1";
                            }
                            if (chk_extra.Checked)
                            {
                                value_extra = "1";
                            }
                            if (chk_faq.Checked)
                            {
                                value_faq = "1";
                            }
                            if (chk_partially.Checked)
                            {
                                value_partially = "1";
                            }

                            if (chk_splited.Checked)
                            {
                                value_splited = "1";
                            }

                            if (chk_moist.Checked)
                            {
                                value_moist = "1";
                            }

                            double rejqty = Convert.ToDouble(txtqtyDiff.Text);

                            Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                            string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdwpm.CommandText = updtwpms;


                            string updtcsms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdcsms.CommandText = updtcsms;

                            int x = cmdwpm.ExecuteNonQuery();

                            int count = cmdcsms.ExecuteNonQuery();

                            if (count > 0)
                            {
                                trns1.Commit();

                                if (con_paddy.State == ConnectionState.Open)
                                {
                                    con_paddy.Close();
                                }


                                trns.Commit();


                                string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + distid + "','" + issuecentreid + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                SqlCommand cmd_rej = new SqlCommand(insrej, con);

                                int xx = cmd_rej.ExecuteNonQuery();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                btnsubmit.Enabled = false;

                                HyperLink1.Visible = true;

                                Session["Godown"] = ddlgodown.SelectedValue;

                                Session["ReceiptID"] = ddlissueId.SelectedValue;

                                if (ddlcommodtiy.SelectedValue == "13")
                                {
                                    Session["Commodity_Id"] = "2";
                                }
                                else
                                {
                                    Session["Commodity_Id"] = "3";
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            trns1.Rollback();

                            trns.Rollback();


                            Label9.Text = "error:6" + ex.Message;
                            Label9.Visible = true;
                        }

                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            if (con_paddy.State == ConnectionState.Open)
                            {
                                con_paddy.Close();
                            }
                        }

                    }

                    # endregion

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें |'); </script> ");
                    return;
                }


                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "11")  //Jowar
            {
                # region Check

                string checkin = "SELECT * FROM Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                SqlCommand cmd = new SqlCommand(checkin, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
              
                    # region Maize
                    if (ddlcommodtiy.SelectedValue == "8" || ddlcommodtiy.SelectedValue == "11" || ddlcommodtiy.SelectedValue == "12" || ddlcommodtiy.SelectedValue == "40" || ddlcommodtiy.SelectedValue == "9")
                    {
                        if (con_Maze.State == ConnectionState.Closed)
                        {
                            con_Maze.Open();
                        }

                        SqlTransaction trns1;

                        SqlCommand cmdwpm = new SqlCommand();

                        cmdwpm.Connection = con_Maze;

                        trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        cmdwpm.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlTransaction trns;

                        SqlCommand cmdcsms = new SqlCommand();


                        cmdcsms.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdcsms.Transaction = trns;

                        try
                        {
                            string value_brightness = "0";
                            string value_damaged = "0";
                            string value_extra = "0";
                            string value_faq = "0";
                            string value_partially = "0";
                            string value_splited = "0";
                            string value_moist = "0";


                            if (chk_brightness.Checked)
                            {
                                value_brightness = "1";
                            }
                            if (chk_damaged.Checked)
                            {
                                value_damaged = "1";
                            }
                            if (chk_extra.Checked)
                            {
                                value_extra = "1";
                            }
                            if (chk_faq.Checked)
                            {
                                value_faq = "1";
                            }
                            if (chk_partially.Checked)
                            {
                                value_partially = "1";
                            }

                            if (chk_splited.Checked)
                            {
                                value_splited = "1";
                            }

                            if (chk_moist.Checked)
                            {
                                value_moist = "1";
                            }

                            double rejqty = Convert.ToDouble(txtqtyDiff.Text);

                            Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                            string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdwpm.CommandText = updtwpms;


                            string updtcsms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdcsms.CommandText = updtcsms;

                            int x = cmdwpm.ExecuteNonQuery();

                            int count = cmdcsms.ExecuteNonQuery();

                            if (count > 0)
                            {
                                trns1.Commit();

                                if (con_Maze.State == ConnectionState.Open)
                                {
                                    con_Maze.Close();
                                }


                                trns.Commit();


                                string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + distid + "','" + issuecentreid + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                SqlCommand cmd_rej = new SqlCommand(insrej, con);

                                int xx = cmd_rej.ExecuteNonQuery();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                btnsubmit.Enabled = false;

                                HyperLink1.Visible = true;

                                Session["Godown"] = ddlgodown.SelectedValue;

                                Session["ReceiptID"] = ddlissueId.SelectedValue;

                                if (ddlcommodtiy.SelectedValue == "8")
                                {
                                    Session["Commodity_Id"] = "6";
                                }

                                if (ddlcommodtiy.SelectedValue == "11")
                                {
                                    Session["Commodity_Id"] = "4";
                                }

                                if (ddlcommodtiy.SelectedValue == "12")
                                {
                                    Session["Commodity_Id"] = "5";
                                }

                                if (ddlcommodtiy.SelectedValue == "40")
                                {
                                    Session["Commodity_Id"] = "7";
                                }
                                if (ddlcommodtiy.SelectedValue == "9")
                                {
                                    Session["Commodity_Id"] = "7";
                                }

                            }
                        }

                        catch (Exception ex)
                        {
                            trns1.Rollback();

                            trns.Rollback();


                            Label9.Text = "error:6" + ex.Message;
                            Label9.Visible = true;
                        }

                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }
                        }

                    }

                    # endregion

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें |'); </script> ");
                    return;
                }


                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "12")  //Maize
            {
                # region Check

                string checkin = "SELECT * FROM Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                SqlCommand cmd = new SqlCommand(checkin, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    # region Maize
                    if (ddlcommodtiy.SelectedValue == "8" || ddlcommodtiy.SelectedValue == "11" || ddlcommodtiy.SelectedValue == "12" || ddlcommodtiy.SelectedValue == "40" || ddlcommodtiy.SelectedValue == "9")
                    {
                        if (con_Maze.State == ConnectionState.Closed)
                        {
                            con_Maze.Open();
                        }

                        SqlTransaction trns1;

                        SqlCommand cmdwpm = new SqlCommand();

                        cmdwpm.Connection = con_Maze;

                        trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        cmdwpm.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlTransaction trns;

                        SqlCommand cmdcsms = new SqlCommand();


                        cmdcsms.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdcsms.Transaction = trns;

                        try
                        {
                            string value_brightness = "0";
                            string value_damaged = "0";
                            string value_extra = "0";
                            string value_faq = "0";
                            string value_partially = "0";
                            string value_splited = "0";
                            string value_moist = "0";


                            if (chk_brightness.Checked)
                            {
                                value_brightness = "1";
                            }
                            if (chk_damaged.Checked)
                            {
                                value_damaged = "1";
                            }
                            if (chk_extra.Checked)
                            {
                                value_extra = "1";
                            }
                            if (chk_faq.Checked)
                            {
                                value_faq = "1";
                            }
                            if (chk_partially.Checked)
                            {
                                value_partially = "1";
                            }

                            if (chk_splited.Checked)
                            {
                                value_splited = "1";
                            }

                            if (chk_moist.Checked)
                            {
                                value_moist = "1";
                            }

                            double rejqty = Convert.ToDouble(txtqtyDiff.Text);

                            Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                            string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdwpm.CommandText = updtwpms;


                            string updtcsms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdcsms.CommandText = updtcsms;

                            int x = cmdwpm.ExecuteNonQuery();

                            int count = cmdcsms.ExecuteNonQuery();

                            if (count > 0)
                            {
                                trns1.Commit();

                                if (con_Maze.State == ConnectionState.Open)
                                {
                                    con_Maze.Close();
                                }


                                trns.Commit();


                                string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + distid + "','" + issuecentreid + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                SqlCommand cmd_rej = new SqlCommand(insrej, con);

                                int xx = cmd_rej.ExecuteNonQuery();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                btnsubmit.Enabled = false;

                                HyperLink1.Visible = true;

                                Session["Godown"] = ddlgodown.SelectedValue;

                                Session["ReceiptID"] = ddlissueId.SelectedValue;

                                if (ddlcommodtiy.SelectedValue == "8")
                                {
                                    Session["Commodity_Id"] = "6";
                                }

                                if (ddlcommodtiy.SelectedValue == "11")
                                {
                                    Session["Commodity_Id"] = "4";
                                }

                                if (ddlcommodtiy.SelectedValue == "12")
                                {
                                    Session["Commodity_Id"] = "5";
                                }

                                if (ddlcommodtiy.SelectedValue == "40")
                                {
                                    Session["Commodity_Id"] = "7";
                                }
                                if (ddlcommodtiy.SelectedValue == "9")
                                {
                                    Session["Commodity_Id"] = "7";
                                }

                            }
                        }

                        catch (Exception ex)
                        {
                            trns1.Rollback();

                            trns.Rollback();


                            Label9.Text = "error:6" + ex.Message;
                            Label9.Visible = true;
                        }

                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }
                        }

                    }

                    # endregion

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें |'); </script> ");
                    return;
                }


                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "8")  //Bajra
            {
                # region Check

                string checkin = "SELECT * FROM Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                SqlCommand cmd = new SqlCommand(checkin, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    # region Maize
                    if (ddlcommodtiy.SelectedValue == "8" || ddlcommodtiy.SelectedValue == "11" || ddlcommodtiy.SelectedValue == "12" || ddlcommodtiy.SelectedValue == "40" || ddlcommodtiy.SelectedValue == "9")
                    {
                        if (con_Maze.State == ConnectionState.Closed)
                        {
                            con_Maze.Open();
                        }

                        SqlTransaction trns1;

                        SqlCommand cmdwpm = new SqlCommand();

                        cmdwpm.Connection = con_Maze;

                        trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        cmdwpm.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlTransaction trns;

                        SqlCommand cmdcsms = new SqlCommand();


                        cmdcsms.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdcsms.Transaction = trns;

                        try
                        {
                            string value_brightness = "0";
                            string value_damaged = "0";
                            string value_extra = "0";
                            string value_faq = "0";
                            string value_partially = "0";
                            string value_splited = "0";
                            string value_moist = "0";


                            if (chk_brightness.Checked)
                            {
                                value_brightness = "1";
                            }
                            if (chk_damaged.Checked)
                            {
                                value_damaged = "1";
                            }
                            if (chk_extra.Checked)
                            {
                                value_extra = "1";
                            }
                            if (chk_faq.Checked)
                            {
                                value_faq = "1";
                            }
                            if (chk_partially.Checked)
                            {
                                value_partially = "1";
                            }

                            if (chk_splited.Checked)
                            {
                                value_splited = "1";
                            }

                            if (chk_moist.Checked)
                            {
                                value_moist = "1";
                            }

                            double rejqty = Convert.ToDouble(txtqtyDiff.Text);

                            Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                            string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdwpm.CommandText = updtwpms;


                            string updtcsms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdcsms.CommandText = updtcsms;

                            int x = cmdwpm.ExecuteNonQuery();

                            int count = cmdcsms.ExecuteNonQuery();

                            if (count > 0)
                            {
                                trns1.Commit();

                                if (con_Maze.State == ConnectionState.Open)
                                {
                                    con_Maze.Close();
                                }


                                trns.Commit();


                                string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + distid + "','" + issuecentreid + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                SqlCommand cmd_rej = new SqlCommand(insrej, con);

                                int xx = cmd_rej.ExecuteNonQuery();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                btnsubmit.Enabled = false;

                                HyperLink1.Visible = true;

                                Session["Godown"] = ddlgodown.SelectedValue;

                                Session["ReceiptID"] = ddlissueId.SelectedValue;

                                if (ddlcommodtiy.SelectedValue == "8")
                                {
                                    Session["Commodity_Id"] = "6";
                                }

                                if (ddlcommodtiy.SelectedValue == "11")
                                {
                                    Session["Commodity_Id"] = "4";
                                }

                                if (ddlcommodtiy.SelectedValue == "12")
                                {
                                    Session["Commodity_Id"] = "5";
                                }

                                if (ddlcommodtiy.SelectedValue == "40")
                                {
                                    Session["Commodity_Id"] = "7";
                                }
                                if (ddlcommodtiy.SelectedValue == "9")
                                {
                                    Session["Commodity_Id"] = "7";
                                }

                            }
                        }

                        catch (Exception ex)
                        {
                            trns1.Rollback();

                            trns.Rollback();


                            Label9.Text = "error:6" + ex.Message;
                            Label9.Visible = true;
                        }

                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }
                        }

                    }

                    # endregion

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें |'); </script> ");
                    return;
                }


                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "9")  //Barley
            {
                # region Check

                string checkin = "SELECT * FROM Acceptance_Note_Detail where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                SqlCommand cmd = new SqlCommand(checkin, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    # region Maize
                    if (ddlcommodtiy.SelectedValue == "8" || ddlcommodtiy.SelectedValue == "11" || ddlcommodtiy.SelectedValue == "12" || ddlcommodtiy.SelectedValue == "40" || ddlcommodtiy.SelectedValue == "9")
                    {
                        if (con_Maze.State == ConnectionState.Closed)
                        {
                            con_Maze.Open();
                        }

                        SqlTransaction trns1;

                        SqlCommand cmdwpm = new SqlCommand();

                        cmdwpm.Connection = con_Maze;

                        trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        cmdwpm.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlTransaction trns;

                        SqlCommand cmdcsms = new SqlCommand();


                        cmdcsms.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmdcsms.Transaction = trns;

                        try
                        {
                            string value_brightness = "0";
                            string value_damaged = "0";
                            string value_extra = "0";
                            string value_faq = "0";
                            string value_partially = "0";
                            string value_splited = "0";
                            string value_moist = "0";


                            if (chk_brightness.Checked)
                            {
                                value_brightness = "1";
                            }
                            if (chk_damaged.Checked)
                            {
                                value_damaged = "1";
                            }
                            if (chk_extra.Checked)
                            {
                                value_extra = "1";
                            }
                            if (chk_faq.Checked)
                            {
                                value_faq = "1";
                            }
                            if (chk_partially.Checked)
                            {
                                value_partially = "1";
                            }

                            if (chk_splited.Checked)
                            {
                                value_splited = "1";
                            }

                            if (chk_moist.Checked)
                            {
                                value_moist = "1";
                            }

                            double rejqty = Convert.ToDouble(txtqtyDiff.Text);

                            Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                            string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdwpm.CommandText = updtwpms;


                            string updtcsms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + issuecentreid + "' and TC_Number = '" + txtTcNumber.Text + "'";

                            cmdcsms.CommandText = updtcsms;

                            int x = cmdwpm.ExecuteNonQuery();

                            int count = cmdcsms.ExecuteNonQuery();

                            if (count > 0)
                            {
                                trns1.Commit();

                                if (con_Maze.State == ConnectionState.Open)
                                {
                                    con_Maze.Close();
                                }


                                trns.Commit();


                                string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + distid + "','" + issuecentreid + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                SqlCommand cmd_rej = new SqlCommand(insrej, con);

                                int xx = cmd_rej.ExecuteNonQuery();

                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");

                                btnsubmit.Enabled = false;

                                HyperLink1.Visible = true;

                                Session["Godown"] = ddlgodown.SelectedValue;

                                Session["ReceiptID"] = ddlissueId.SelectedValue;

                                if (ddlcommodtiy.SelectedValue == "8")
                                {
                                    Session["Commodity_Id"] = "6";
                                }

                                if (ddlcommodtiy.SelectedValue == "11")
                                {
                                    Session["Commodity_Id"] = "4";
                                }

                                if (ddlcommodtiy.SelectedValue == "12")
                                {
                                    Session["Commodity_Id"] = "5";
                                }

                                if (ddlcommodtiy.SelectedValue == "40")
                                {
                                    Session["Commodity_Id"] = "7";
                                }
                                if (ddlcommodtiy.SelectedValue == "9")
                                {
                                    Session["Commodity_Id"] = "7";
                                }

                            }
                        }

                        catch (Exception ex)
                        {
                            trns1.Rollback();

                            trns.Rollback();


                            Label9.Text = "error:6" + ex.Message;
                            Label9.Visible = true;
                        }

                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }
                        }

                    }

                    # endregion

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें |'); </script> ");
                    return;
                }


                # endregion
            }

        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('अस्वकृति का कोई एक कारण चुने |'); </script> ");
            return;
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con_paddy.State == ConnectionState.Open)
        {
            con_paddy.Close();
        }

        if (con_Maze.State == ConnectionState.Open)
        {
            con_Maze.Close();
        }
    }

    protected void ddlissueId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्ति दिनांक चुनिए |'); </script> ");
            return;
        }

        if (ddlgodown.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('गोदाम का नाम चुने |'); </script> ");
            return;
        }

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        getdata();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (txtsendQty.Text == "" || txtRecdQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्त मात्र अथवा भेजी गयी मात्रा गलत है  |'); </script> ");
            return;
        }

        string sendqty = txtsendQty.Text;

        string recqty = txtRecdQty.Text;

        decimal sendquantity = Convert.ToDecimal(sendqty);

        decimal recquantity = Convert.ToDecimal(recqty);

        decimal cal = sendquantity - recquantity;

        string calqty = Convert.ToString(cal);
        
        string sendbgs = txtsendbags.Text;

        string recbgs= txtrecbags.Text;

        decimal sendbags = Convert.ToDecimal(sendbgs);

        decimal recbags = Convert.ToDecimal(recbgs);

        decimal calbag = sendbags - recbags;

        string calbgss = Convert.ToString(calbag);


        if (calqty == "" || calbgss == "")
        {
            btnsubmit.Visible = false;

            return;
        }

        else
        {
            txtqtyDiff.Text = calqty;

            txtdiffBags.Text = calbgss;
        }

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con_paddy.State == ConnectionState.Open)
        {
            con_paddy.Close();
        }

        if (con_Maze.State == ConnectionState.Open)
        {
            con_Maze.Close();
        }

        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
}
