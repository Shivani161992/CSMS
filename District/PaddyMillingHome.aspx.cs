using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_PaddyMillingHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null || Session["Operations"] != null)
        {

            if (Session["dist_id"].ToString() == "35")
            {
                if (Session["user"] != null)
                {

                    if (Session["CheckStatus"]== null)
                    {
                    Session["CheckStatus"] = "0";
                    }
                    else if (Session["CheckStatus"] != null)
                    {


                    if (Session["CheckStatus"].ToString() != "1")
                    {
                        Session["CheckStatus"] = "1";
                        Response.Redirect("~/District/PaddyMillingHome.aspx");
                    }
                    else if (Session["CheckStatus"].ToString() == "1")
                    {

                        return;
                    }
                }



                }
                else
                {
                    Response.Redirect("~/District/Mandla_DistrictLogin.aspx");
                }




            }

            else if (Session["dist_id"].ToString() != "35")
            {


                if (Session["Markfed"].ToString() == "Y")
                {
                    // DRCMRDO.Visible = DCMRDO.Visible = false;
                    CMRDO.Visible = RCMRDO.Visible = DelCMRDO.Visible = DelCMRRecpt.Visible = CMRInspection.Visible = CMRInspectionEntry.Visible = false;
                }

            }




        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
}