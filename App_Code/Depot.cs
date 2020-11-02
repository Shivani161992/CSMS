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
/// Summary description for Depot
/// </summary>
public class Depot
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public String ID;
    public String depotid;
    public String depot;
    public Depot(Common cCommon)
    {
        db = new DbClass(cCommon.Connection);
		//
	}
    public DataSet selectAll()
    {

        found = false;
        String qrySelect = "SELECT * FROM DEPOTMASTER";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
    public DataSet selectWhere(String where)
    {

        found = false;
        String qrySelect = "SELECT GodownName,GodownID FROM DEPOTMASTER  where " + where;
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }

    public DataSet selectAny(String selectstring)
    {

        found = false;
        String qrySelect = selectstring;
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
}
