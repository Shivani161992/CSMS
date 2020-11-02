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


public partial class PaddyMilling_MillingAgreement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        #region
        //lblDate.Text = PreviousPage.Date;
        //lblDistManagerName.Text = PreviousPage.DistrictManagerName;
        //lblDistManager.Text = PreviousPage.DistManager;
        //lblMasersName.Text = PreviousPage.MacersName;
        //lblMasersName1.Text = PreviousPage.MacersName;
        //lblMacersAddDist.Text = PreviousPage.MacersAddDist;
        //lblMacersAddDist1.Text = PreviousPage.MacersAddDist;
        //lblMacersAddDivision.Text = PreviousPage.MacersAddDivision;
        //lblMacersAddDivision1.Text = PreviousPage.MacersAddDivision;
        //lblOwnerName.Text = PreviousPage.OwnerName;
        //lblOwner.Text = PreviousPage.Owner;
        //lblCorporationJila.Text = PreviousPage.DistManager;
        //lblYear.Text = PreviousPage.Year;
        //lblYear2.Text = PreviousPage.Year;
        //lblYear3.Text = PreviousPage.Year;
        //lblFromDate.Text = PreviousPage.FromDate;
        //lblToDate.Text = PreviousPage.ToDate;
        //lblCommonDhan.Text = PreviousPage.CommonDhan;
        //lblGradeADhan.Text = PreviousPage.GradeADhan;
        //lblTotalDhan.Text = PreviousPage.TotalDhan;
        //lblArva.Text = PreviousPage.Arva;
        //lblUshanF3.Text = PreviousPage.UshnaF3;
        //lblUshnaA3.Text = PreviousPage.UshnaA3;
        //lblDepositMoney.Text = PreviousPage.DepositMoney;
#endregion
    }
}
