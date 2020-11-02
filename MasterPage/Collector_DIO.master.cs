using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Collector_DIO : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public string BodyTitle
    {
        get { return lblColl_DIO.Text; }
        set { lblColl_DIO.Text = value; }

    }

    public string Name
    {
        get { return LblName.Text; }
        set { LblName.Text = value; }

    }

    public bool SetPanel1
    {
        get { return Panel1.Visible = false;}
    }

    public bool SetPanel2
    {
        get { return Panel2.Visible = false; }
    }
}
