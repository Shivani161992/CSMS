using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

/// <summary>
/// Summary description for ClientIP
/// </summary>
public class ClientIP
{
	public ClientIP()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GETIP()
    {
        // Get the IP
        string hostName = Dns.GetHostName();
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        return myIP;
    }

    public string GETHOST()
    {
        // Retrive the Name of HOST
        string hostName = Dns.GetHostName();
        return hostName;
    }
}