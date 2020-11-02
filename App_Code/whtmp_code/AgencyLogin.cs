using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccess;

/// <summary>
/// Summary description for AgencyLogin
/// </summary>
public class AgencyLogin
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public AgencyLogin(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
	}
    
    public DataSet select(String where)
    {

        found = false;
        String qrySelect = "SELECT * FROM dbo.AgencyLogin,District_Authorization where AgencyLogin.District_Code=District_Authorization.Autho_Dist and District_Authorization.AgencyName='" + where + "' ";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
}
