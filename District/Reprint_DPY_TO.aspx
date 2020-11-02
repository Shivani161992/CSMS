<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reprint_DPY_TO.aspx.cs" Inherits="District_Reprint_DPT_TO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print Transport Order</title>
    
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
        .style1
        {
            font-size: x-small;
        }
    </style>
    
</head>
<body>
     <form id="form1" runat="server">
   
    <a style="cursor: pointer" onclick="javascript:CallPrint('printdiv')">
            <img src="../Images/print.jpg" alt="print" style="width: 47px; height: 34px" /><strong>&nbsp;</strong></a>
        <div id="printdiv">
            <center>
                <table border="2" style="width: 800px">
                    <tr>
                        <td>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 740px">
                                    <tr>
                                        <td align="center" colspan="2" style="text-align: right">
                                            परिशिष्ट - एक
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            &nbsp;<asp:Label ID="title" runat="server" Text="MADHYA PRADESH STATE CIVIL SUPPLIES CORPORATION LIMITED DISTRICT"
                                                ForeColor="Maroon" Font-Size="14px" Width="580px" Font-Bold="True" Height="15px"></asp:Label><asp:Label ID="lbldist" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="Label2" runat="server" Text="Door Step Transport Order" ForeColor="Maroon" Font-Size="20px"
                                                Font-Underline="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 420px; height: 19px; text-align: right">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        </td>
                                        <td style="width: 218px; height: 19px; color: #C0C0C0;">
                                                        Orignal Copy</td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                
                            </center>
                            <center>
                                <table style="width: 770px">
                                    <tr>
                                        <td style="text-align: left; height: 22px;" >
                                            क्रमांक - द्वा प्र यो / 2015-16 / &nbsp; &nbsp; &nbsp;&nbsp;
                                            &nbsp;&nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; दिनांक                                             <asp:Label ID="Label1" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                        <td style="text-align: left; height: 22px;" >
                                            परिवहन आदेश क्रमांक :
                                            <asp:Label ID="lbltranorder" runat="server" Font-Bold="False" ForeColor="#000099"
                                                Width="155px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 192px; text-align: left; height: 21px;">
                                            प्रति ,</td>
                                        <td style="width: 100px; height: 21px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left">
                                            <span style="font-size: 11pt">
                                            मेसर्स</span>&nbsp;
                                            <asp:Label ID="lblTransName" runat="server" Font-Size="11pt"></asp:Label>
                                            <span style="font-size: 11pt">
                                            निगम अनुबंधित परिवहनकर्ता,प्रदाय केंद्र का नाम &nbsp; 
                                            </span>
                                            &nbsp;<asp:Label ID="lblIssueCenter" runat="server" Font-Bold="True" Font-Size="11pt"></asp:Label></td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table style="width: 770px">
                                    <tr>
                                        <td align="left" colspan="5" style="height: 22px">
                                            <span style="font-size: 11pt">
                                            विषय : - म. प्र. शासन की द्वार प्रदाय योजना अंतर्गत माह&nbsp;
                                            </span>
                                            <asp:Label ID="lblallotmonth" runat="server" Font-Bold="False" ForeColor="#000099"
                                                Width="70px" Font-Size="11pt"></asp:Label><span style="font-size: 11pt">&nbsp;के आवंटन के&nbsp;
                                            विरुद्ध प्रदाय <span style="font-size: 11pt">स्कंध परिवहन आदेश|</span></span></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px; text-align: left;" colspan="5">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<span style="font-size: 11pt"> निर्धारित रूट चार्ट के अनुसार कार्पोरेशन प्रदाय
                                            केंद्र </span>
                                            <asp:Label ID="lblper" runat="server"></asp:Label><span style="font-size: 11pt">
                                            &nbsp;&nbsp;से निम्नानुसार स्कंध</span>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="5">
                                            <span style="font-size: 11pt">
                                            उचित मूल्य की दुकानों (उ मू दू) तक पहुचाने का परिवहन आदेश जारी किया जाता है | </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="5" style="text-align: right">
                                            ममात्रा&nbsp; क्विंटल&nbsp; एवं किलो में )
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="1" style="width: 770px">
                                    <tr>
                                        <td style="width: 570px;">
                                           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="770px"
                                                FooterStyle-ForeColor="green" ShowFooter="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                                                <Columns>
                                                    
                                                       
                                                    <asp:BoundField DataField="ToDate" HeaderText="स्कंध परिवहन करने की दिनांक"
                                                         SortExpression="ToDate">
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                  
                                                    </asp:BoundField>
                                                   
                                                    <asp:BoundField DataField="RouteNumber" HeaderText="रूट नंबर"
                                                        SortExpression="RouteNumber">
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                        
                                                         </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="feed_no" HeaderText="फीड नंबर"
                                                        SortExpression="feed_no">
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                        
                                                         </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="FPS" HeaderText="उ मू दू का नाम,स्थान"
                                                        SortExpression="FPS">
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                  
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Payment_mode" HeaderText="भुगतान का प्रकार"
                                                        SortExpression="Payment_mode">
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                  
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Wheat" HeaderText="गेहूँ" SortExpression="Wheat">
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="Rice" HeaderText="चावल" SortExpression="Rice">
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="Sugar" HeaderText="शक्कर" SortExpression="Sugar">
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="Salt" HeaderText="नमक" SortExpression="Salt">
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                    </asp:BoundField>
                                                    
                                                         <asp:BoundField DataField="Maize" HeaderText="मक्का" SortExpression="Maize">
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                    </asp:BoundField>
                                                    
                                                </Columns>
                                                <FooterStyle ForeColor="#000066" BackColor="White" />
                                                <RowStyle ForeColor="#000066" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table style="width: 770px">
                                    <tr>
                                        <td colspan="4" style="height: 17px" align="left">
                                            <p>
                                                1)&nbsp; <span style="font-size: 10pt">&nbsp;नियत दिनांक को प्रदाय केंद्र से विधिवत स्कंध प्राप्ति की पावती देकर एवं तौल पत्रक,
                                                डिलीवरी चालान सह बिल को प्राप्त करके</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="height: 15px">
                                            &nbsp; &nbsp; &nbsp;&nbsp; <span style="font-size: 10pt">उसी दिवस निर्दिष्ट गंतव्य उ मू दू पर स्कंध पहुचाना सुनिश्चित
                                            करें |</span></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="height: 15px">
                                            <span style="font-size: 11pt">
                                            2)&nbsp; <span style="font-size: 10pt">&nbsp;उ मू दू &nbsp;से प्राप्त पावतियां तीन कार्यदिवस में निगम प्रदाय केंद्र प्रभारी को उपलब्ध
                                            करना सुनिश्चित करे |</span></span></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="height: 3px">
                                            <span style="font-size: 11pt">
                                            3)&nbsp;<span style="font-size: 10pt"> &nbsp;इस बात का भी विशेष ध्यान रखा जाए&nbsp;
                                                कि दिवस समाप्ति पर स्कंध बचने पर तत्काल कार्पोरेशन
                                            प्रदाय
                                            केद्र प्रभारी <span style="font-size: 10pt">एवं</span></span></span></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="height: 1px">
                                            <span style="font-size: 11pt">&nbsp; &nbsp;&nbsp;<span style="font-size: 10pt">वेयर हाउस प्रभारी</span>&nbsp; <span style="font-size: 10pt">को एस.
                                                एम. एस. से सूचना एवं कारण दर्शाकर ट्रक
                                            सहित बचा स्कंध&nbsp;
                                            सम्बंधित प्रदाय केंद्र 
                                                <span style="font-size: 10pt">के म प्र वेयर हाउसिंग</span></span></span></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="height: 1px">
                                            <span style="font-size: 11pt">&nbsp; &nbsp;<span style="font-size: 10pt">एवं </span>
                                                &nbsp;<span style="font-size: 10pt">लाजिस्टिक कार्पो / </span><span style="font-size: 10pt">&nbsp; केंद्रीय भंडार
                                            गृह निगम के कवर्ड शासकीय&nbsp;
                                            गोदाम परिसर में सुरक्षित <span style="font-size: 10pt">
                                                    रखा जाए |</span></span></span></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 1px; text-align: right;" align="left" colspan="4">
                                            जिला प्रबंधक
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                &nbsp;</center>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 770px">
                                    <tr>
                                        <td style="text-align: left; height: 17px;" >
                                             क्रमांक - द्वा प्र यो / 2015-16 / </td>
                                        <td style="height: 17px; text-align: right;" >
                                            दिनांक-
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td >
                                            </td>
                                        <td style="color: white">
                                           </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left; height: 1px;">
                                            1) जिला आपुर्ति अधिकारी / सहायक आपुर्ति / कनिष्ट आपुर्ति अधिकारी
                                            <asp:Label ID="lbldistr" runat="server" Font-Bold="True"></asp:Label>
                                            की और सूचनार्थ | &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left; height: 29px;">
                                            2) <span style="font-size: 11pt">श्री &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; पद &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; प्रदाय केंद्र प्रभारी,</span>
                                            <asp:Label ID="lblissue" runat="server" Font-Size="11pt"></asp:Label>&nbsp; 
                                            <span style="font-size: 11pt">की और
                                            सूचनार्थ एवं उपरोक्तानुसार डिलीवरी देने हेतु | </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left; height: 15px;">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: right; height: 16px;">
                                            जिला प्रबंधक</td>
                                    </tr>
                                </table>
                            </center>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
   
    </form>
</body>
</html>
