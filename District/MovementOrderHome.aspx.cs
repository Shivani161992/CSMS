﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_MovementOrderHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null) 
        {
         
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
}