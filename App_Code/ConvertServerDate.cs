using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ConvertServerDate
/// </summary>
public class ConvertServerDate
{
	public ConvertServerDate()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}