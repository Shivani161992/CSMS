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

public partial class IssueCenter_Default1122 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        { 
            GridView1.DataSource = GetCustomMadeDataTable(); 
            GridView1.DataBind(); 
        }

    }
    public DataTable GetCustomMadeDataTable()   
    {       
        //Create a new DataTable object        
        System.Data.DataTable objDataTable = new System.Data.DataTable();       
        //Create three columns with string as their type       
        objDataTable.Columns.Add("ISBN", typeof(string));        
        objDataTable.Columns.Add("Title", typeof(string));       
        objDataTable.Columns.Add("Publisher", typeof(string));       
        objDataTable.Columns.Add("Year", typeof(string));       
        DataColumn[] dcPk = new DataColumn[1];       
        dcPk[0] = objDataTable.Columns["ISBN"];        
        objDataTable.PrimaryKey = dcPk;        
        objDataTable.Columns["ISBN"].AutoIncrement = true;      
        objDataTable.Columns["ISBN"].AutoIncrementSeed = 1;        
        //Adding some data in the rows of this DataTable        
        DataRow dr;        
        for (int i = 1; i <= 10; i++)       
        {            
            dr = objDataTable.NewRow();          
            dr[1] = "Title" + i.ToString();            
            dr[2] = "Publisher" + i.ToString();          
            dr[3] = "12/12/200" + i.ToString();           
            objDataTable.Rows.Add(dr);       
        }        
        Session["strTemp"] = objDataTable;       
        return objDataTable;    
    }    
    protected void Cal1_SelectionChanged(object sender, EventArgs e)  
    {        
        Calendar cal = (Calendar)sender;   
        TextBox text1 = (TextBox)((GridViewRow)cal.Parent.Parent).FindControl("text1");  
        text1.Text = cal.SelectedDate.ToShortDateString();
        cal.Visible = false;
    }


protected void GridView1_RowCommand(object sender,
  GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddToCart")
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Calendar cal = (Calendar)GridView1.FindControl("Cal1");

                cal.Visible = true;
            }
            // Retrieve the row index stored in the 
            // CommandArgument property.
            
            int index = Convert.ToInt32(e.CommandArgument);

            // Retrieve the row that contains the button 
            // from the Rows collection.
            GridViewRow row = GridView1.Rows[index];

            // Add code here to add the item to the shopping cart.
        }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
}
