<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_WHRRequest.aspx.cs" Inherits="IssueCenter_Print_WHRRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print DO for Door Step</title>
    
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href="../MyCss/menu.css" type="text/css" />

    <script language="javascript" type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=600px,height=800px,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();		
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 prtContent.innerHTML=strOldOne;
}
    </script>

    
    <style type="text/css">
        .style2
        {
            height: 16px;
        }
        .style3
        {
            height: 17px;
        }
        .style4
        {
            height: 12px;
        }
        .style5
        {
            height: 9px;
            font-size: small;
        }
        .style6
        {
            height: 1px;
        }
        .style7
        {
            height: 6px;
            font-size: small;
        }
        .style8
        {
            height: 3px;
        }
        .style9
        {
            height: 4px;
            font-size: small;
        }
        .style10
        {
            height: 8px;
        }
        .style11
        {
            height: 5px;
        }
        .style12
        {
            height: 10px;
        }
        .style13
        {
            width: 99%;
        }
        .style20
        {
            height: 6px;
            text-align: left;
            font-size: small;
        }
        .style21
        {
            font-size: small;
        }
        .style22
        {
            height: 15px;
            font-size: small;
        }
        .style23
        {
            height: 21px;
        }
        </style>

    
</head>
<body>
    <form id="form1" runat="server">
   
    <a style="cursor: pointer" onclick="javascript:CallPrint('printdiv')">
            <img src="../Images/print.jpg" alt="print" style="width: 47px; height: 34px" /><strong>&nbsp;</strong></a>
        <div id="printdiv">
            <center>
                <table  style="width: 702px">
                    <tr>
                        <td class="style23" style="text-align: center" colspan="3">
                            म प्र वेयर हाउसिंग एंड लोजिस्टिक कारपोरेशन</td>
                    </tr>
                    <tr>
                        <td class="style2" style="text-align: center" colspan="3">
                            माल जमा (डिपोजिट) करने का प्रार्थना पत्र</td>
                    </tr>
                    <tr>
                        <td class="style3" style="text-align: left">
                            क्रमांक :&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_whrrequest" 
                                runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td class="style3" style="text-align: left">
                            जमा दिनांक: 
                           
                        </td>
                        <td class="style3" style="text-align: left">
                            <asp:Label ID="lbldepositdate" runat="server" Font-Bold="True"></asp:Label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="style22" style="text-align: left" colspan="3">
                            प्रति,</td>
                    </tr>
                    <tr>
                        <td class="style4" style="text-align: left" colspan="3">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="style21">शाखा प्रबंधक</span></td>
                    </tr>
                    <tr>
                        <td class="style4" style="text-align: left" colspan="3">
                            <span class="style21">वेयर हाउसिंग कार्पोरेशन</span>,</td>
                    </tr>
                    <tr>
                        <td class="style5" style="text-align: left" colspan="3">
                            शाखा</td>
                    </tr>
                    <tr>
                        <td class="style6" style="text-align: left" colspan="3">
                            <span class="style21">महोदय</span>,</td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="3">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <span class="style21">कृपया नीचे लिखे अनुसार स्कंध को अपने वेयर हाउस के गोदाम <asp:Label ID="lblgodown" runat="server"></asp:Label>&nbsp;
                            में रखने के लिए स्वीकार करेंगे |</span></td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="3">
                            <table class="style13" 
                                style="border: thin groove #000000; padding: inherit; margin: auto">
                                <tr>
                                    <td style="border-right: thin groove #000000; border-bottom: thin groove #000000; text-align: center; border-left-color: #000000; border-left-width: thin; border-top-color: #000000; border-top-width: thin;">
                                        कमोडिटी</td>
                                   <td style="border-right: thin groove #000000; border-bottom: thin groove #000000; text-align: center; border-left-color: #000000; border-left-width: thin; border-top-color: #000000; border-top-width: thin;">
                                        जमा किये गए कुल ट्रकों की संख्या</td>
                                    <td style="border-right: thin groove #000000; border-bottom: thin groove #000000; text-align: center; border-left-color: #000000; border-left-width: thin; border-top-color: #000000; border-top-width: thin;">
                                        कुल जमा
                                        मात्रा                                         <br />
                                        क्विंटल किलोग्राम</td>
                                    <td style="border-right: thin groove #000000; border-bottom: thin groove #000000; text-align: center; border-left-color: #000000; border-left-width: thin; border-top-color: #000000; border-top-width: thin;">
                                        कुल प्राप्त
                                        बोरो की संख्या</td>
                                </tr>
                                <tr>
                                    <td style="border-right-style: groove; border-bottom-style: groove; border-width: thin; border-color: #000000">
                                        <asp:Label ID="lblcommodity" runat="server"></asp:Label>
                                    </td>
                                   <td style="border-right: thin groove #000000; border-bottom: thin groove #000000; text-align: center; border-left-color: #000000; border-left-width: thin; border-top-color: #000000; border-top-width: thin;">
                                        <asp:Label ID="lbltruckcount" runat="server"></asp:Label>
                                    </td>
                                    <td style="border-right: thin groove #000000; border-bottom: thin groove #000000; text-align: center; border-left-color: #000000; border-left-width: thin; border-top-color: #000000; border-top-width: thin;">
                                        <asp:Label ID="lblweight" runat="server"></asp:Label>
                                    </td>
                                    <td style="border-right: thin groove #000000; border-bottom: thin groove #000000; text-align: center; border-left-color: #000000; border-left-width: thin; border-top-color: #000000; border-top-width: thin;">
                                        <asp:Label ID="lblbagscount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" colspan="3">
                            मैं यह प्रमाणित करता हू की उपरोक्त बतलाया गया स्कंध म प्र स्टेट सिविल 
                            सप्लाईस कार्पोरेशन लिमिटेड ,</td>
                    </tr>
                    <tr>
                        <td class="style20" colspan="3">
                            &nbsp;जिला कार्यालय
                            <asp:Label ID="lbldistrict" runat="server"></asp:Label>
&nbsp;का है और उस पर किसी दूसरे व्यक्ति या संस्था का अधिकार नहीं है|</td>
                    </tr>
                    <tr>
                        <td class="style7" style="text-align: right" colspan="3">
                            हस्ताक्षर</td>
                    </tr>
                    <tr>
                        <td class="style8" style="text-align: right" colspan="3">
                            (<span class="style21">जमाकर्ता प्रदाय केंद्र प्रभारी)</span></td>
                    </tr>
                    <tr>
                        <td class="style9" style="text-align: right" colspan="3">
                        <asp:Label ID="lblscsc" runat="server" Text=" MP State Civil Supplies Corporation Limited" Visible = "false"></asp:Label>
                        <asp:Label ID="lblmarkfed" runat="server" Text="M.P.State Cooperative Marketing Federation Ltd" Visible = "false"></asp:Label>
                           </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            प्रदाय केंद्र
                            <asp:Label ID="lblissuecenter" runat="server" Font-Bold="True"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="style10" style="text-align: right" colspan="3">
                            <span class="style21">नाम&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="style11" style="text-align: right" colspan="3">
                            <span class="style21">मोबाइल क्रमांक</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4" style="text-align: center" colspan="3">
                            पावती</td>
                    </tr>
                    <tr>
                        <td class="style12" style="text-align: left" colspan="3">
                            <span class="style21">डिपोजिट फॉर्म क्रमांक
                            <asp:Label ID="lblwhrReq" runat="server" Font-Bold="True"></asp:Label>
&nbsp;में उल्लेखित अनुसार स्कंध
                                                        दिनांक&nbsp;<asp:Label ID="lblpavtidate" runat="server" Font-Bold="True"></asp:Label>को 
                            प्राप्तकर गोदाम क्रमांक  <asp:Label ID="lblpavtigdn" 
                                runat="server"></asp:Label>
                            &nbsp;में जमा किया एवं WHR क्रमांक ____________________ जारी किया |</span><br />
                                                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            शाखा प्रबंधक</td>
                    </tr>
                </table>
            </center>
        </div>
   
    </form>
</body>
</html>

