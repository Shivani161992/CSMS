using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Net;
using System.Collections;
using System.Text;

/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{
    static HttpWebRequest request = null;
    static Stream dataStream;

	public SMS()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void SendSMS(string MobileNo, string Message)
    {
        //String username = "DITMP-FCS";
        //String password = "dirfood@2013";

        int a = 0;
        int b = 0;

        # region Message_Miller

        //string Transport_Mob = "0" + MobileNo;
        //string messageTrans = Message;

        //string message = Message;

        //String Tran_mobileNos = Transport_Mob;
        //String senderid = "MPSCSC";

        //Int64 Tmsgln = messageTrans.Length;

        //Int64 msgln = message.Length;

        //String T_finalmessage = "";
        //String Tsss = "";
        //char Tch;

        //for (int y = 0; y < message.Length; y++)
        //{

        //    Tch = message[y];

        //    int zx = (int)Tch;
        //    // System.out.println("iiii::"+j);

        //    Tsss = "&#" + zx + ";";
        //    T_finalmessage = T_finalmessage + Tsss;
        //}

        //messageTrans = T_finalmessage;
        //Int64 Tmsgln3 = messageTrans.Length;

        //request = (HttpWebRequest)WebRequest.Create("http://msdgweb.mgov.gov.in/esms/sendsmsrequest");
        //request.ProtocolVersion = HttpVersion.Version10;
        ////request.ServicePoint.Expect100Continue = false;

        //request.ServicePoint.ConnectionLeaseTimeout = 90000;
        ////((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
        //((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
        //request.Method = "POST";


        //String smsservicetype = "unicodemsg"; // For bulk msg
        ////String smsservicetype = "bulkmsg"; // For bulk msg

        //String query = "username=" + HttpUtility.UrlEncode(username)
        //             + "&password=" + HttpUtility.UrlEncode(password)
        //              + "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype)
        //               + "&content=" + HttpUtility.UrlEncode(messageTrans)
        //                + "&bulkmobno=" + HttpUtility.UrlEncode(Tran_mobileNos, Encoding.UTF8)
        //                 + "&senderid=" + HttpUtility.UrlEncode(senderid);

        //request.ContentType = "application/x-www-form-urlencoded";

        //request.ContentLength = query.Length;
        //((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

        //byte[] byteArray = Encoding.UTF8.GetBytes(query);
        //request.ContentType = "application/x-www-form-urlencoded";
        //request.ContentLength = byteArray.Length;
        //dataStream = request.GetRequestStream();
        //dataStream.Write(byteArray, 0, byteArray.Length);
        //dataStream.Close();
        //WebResponse response = request.GetResponse();
        //String Status = ((HttpWebResponse)response).StatusDescription;
        //dataStream = response.GetResponseStream();
        //StreamReader reader = new StreamReader(dataStream);
        //string responseFromServer = reader.ReadToEnd();
        //reader.Close();
        //dataStream.Close();
        //response.Close();

        //string[] smsresponse = responseFromServer.Split(',');

        //string rs1 = smsresponse[0];
        ////mb = smsresponse[1];

        //string Sta2 = "";
        //if (rs1.Trim().ToString() == "402")
        //{
        //    Sta2 = "S";
        //    a++;

        //}
        //else
        //{
        //    Sta2 = "F";
        //    b++;
        //}

        # endregion

        # region NewMssge

        string noc = Message.Length.ToString();

        int s = 0;
        int f = 0;

        string message = Message;

        Int64 msgln = message.Length;
        String finalmessage = "";
        String sss = "";
        char ch;
        for (int k = 0; k < message.Length; k++)
        {
            ch = message[k];
            int j = (int)ch;
            // System.out.println("iiii::"+j);

            sss = "&#" + j + ";";
            finalmessage = finalmessage + sss;

        }
        decimal msgLenByte = System.Text.Encoding.Unicode.GetByteCount(finalmessage);
        decimal smsCount = Math.Ceiling(msgLenByte / 140);

        String username = "foodcivilsupply";
        String password = "09pIAi$_";

        String mobileNos = "0" + MobileNo;
        String senderid = "MPFOOD";

       // string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        Int64 msgln3 = message.Length;
        request = (HttpWebRequest)WebRequest.Create("http://www.smsjust.com/sms/user/urlsms.php?username=" + username + "&pass=" + password + "&senderid=" + senderid + "&dest_mobileno=" + mobileNos + "&msgtype=UNI&message=" + message + "&response=Y");

        request.ProtocolVersion = HttpVersion.Version10;
        ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
        request.Method = "POST";

        request.ContentType = "application/x-www-form-urlencoded";

        ////request.ContentLength = query.Length;
        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        String Status = ((HttpWebResponse)response).StatusDescription;
        dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();
        reader.Close();
        dataStream.Close();
        response.Close();
        string[] smsresponse = responseFromServer.Split(',');
       string rs = smsresponse[0];
        //mb = smsresponse[1];
        string Sta1 = "";


        if (rs.Trim().ToString() == "ES1001")
        {
            Sta1 = "F";
            //msg = "Authentication Failed (invalid username/password)";
            f++;

        }
        else if (rs.Trim().ToString() == "ES1004")
        {
            Sta1 = "F";
            //msg = "Invalid Senderid";
            f++;

        }
        else if (rs.Trim().ToString() == "ES1009")
        {
            Sta1 = "F";
            //msg = "Sorry unable to process request";
            f++;

        }

        else if (rs.Trim().ToString() == "ES1013")
        {
            Sta1 = "F";
            //msg = "Template id is invalid";
            f++;
        }
        else if (rs.Trim().ToString() == "ES1002")
        {
            Sta1 = "F";
            //msg = "Unauthorized Usage-insufficient privilege";
            f++;

        }
        else if (rs.Trim().ToString() == "ES1007")
        {
            Sta1 = "F";
            //msg = "Account Deactivated";
            f++;
        }

        else if (rs.Trim().ToString() == "")
        {
            Sta1 = "F";
            //msg = "Message is blank";
            f++;

        }
        else if (rs.Trim().ToString() == "Account is Expire")
        {
            Sta1 = "F";
            //msg = "Account is Expire";
            f++;

        }
        else if (rs.Trim().ToString() == "You have Exceeded your SMS Limit.")
        {
            Sta1 = "F";
            //msg = "You have Exceeded your SMS Limit.";
            f++;
        }

        else if (rs.Trim().ToString().Length == 20)
        {
            Sta1 = "S";
            s++;

        }

        else if (rs.Trim().ToString().Length == 18)
        {
            Sta1 = "S";
            //msg = "Success";
            s++;

        }

        else
        {
            //Sta1 = "F";
            //f++;

        }


        # endregion

    }
}