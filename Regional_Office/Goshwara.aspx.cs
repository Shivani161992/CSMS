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

public partial class Regional_Office_Goshwara : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    private SqlConnection conWheat = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2015"].ToString());

    string divid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Region_id"] != null)
        {
            divid = Session["Region_id"].ToString();

            if (!IsPostBack)
            {
                              
            }

        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }


  
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/Regional_Office/Welcome_RO.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string banner = ddlbanner.SelectedItem.Value;

        string shadow = ddlshadow.SelectedItem.Value;

        string water = ddlwater.SelectedItem.Value;

        string toilet = ddltoiler.SelectedItem.Value;

        string firstaid = ddlfirstaid.SelectedItem.Value;

        string laptop = ddllaptop.SelectedItem.Value;

        string printer = ddlprinter.SelectedItem.Value;

        string battery = ddlbattery.SelectedItem.Value;

        string connectivity = ddldataconnect.SelectedItem.Value;

        string machine = ddlstiching.SelectedItem.Value;

        string taulmachine = ddltaulmachine.SelectedItem.Value;

        string filter = ddlfilter.SelectedItem.Value;

        string fan = ddlfan.SelectedItem.Value;

        string moisture = ddlmoit.SelectedItem.Value;

        string enamel = ddlenamel.SelectedItem.Value;

        string digibal = ddldigital.SelectedItem.Value;

        string tripal = ddltirpal.SelectedItem.Value;

        string otherelect = ddlothers.SelectedItem.Value;

        string vardana = ddlvardana.SelectedItem.Value;

        string hr = ddlhr.SelectedItem.Value;

        string insqry = "Insert into UparjanSociety_Goshwara (District ,banner ,shadow ,water ,toilet ,firstaid ,laptop ,printer ,battery ,connectivity ,machine ,taulmachine ,filter ,fan ,moisture ,enamel ,digibal ,tripal ,otherelect ,vardana ,hr ,CreatedDate,IP) Values (' ', '"+banner+"' ,'"+shadow+"' , '"+water+"' , '"+toilet+"' , '"+firstaid+"' , '"+laptop  +"' , '"+ printer +"' , '"+battery  +"' , '"+connectivity  +"' , '"+ machine +"' ,'"+taulmachine  +"' , '"+filter  +"' , '"+fan  +"' , '"+moisture  +"' , '"+enamel  +"' , '"+ digibal +"' ,'"+tripal  +"' ,'"+otherelect  +"' , '"+vardana  +"' , '"+hr  +"' , getdate() , '"+ip +"' )";

        SqlCommand cmd = new SqlCommand(insqry, con);

        try
        {
            int x = cmd.ExecuteNonQuery();

            if (x > 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा सुरक्षित हो गया है |'); </script> ");
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
    
}
