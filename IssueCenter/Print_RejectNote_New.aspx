<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_RejectNote_New.aspx.cs" Inherits="IssueCenter_Print_RejectNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Print Reject Note Number</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href ="../MyCss/menu.css" type ="text/css" />
    
    <script language="javascript"  type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();		
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 prtContent.innerHTML=strOldOne;
}
</script>
        <style type="text/css">
            .style3
            {
                height: 18px;
                width: 111px;
            }
            .style4
            {
                height: 21px;
                width: 111px;
            }
            .style5
            {
                width: 111px;
            }
            .style6
            {
                height: 18px;
            }
            .style8
            {
                font-size: large;
                font-weight: bold;
                height: 18px;
            }
            
            .style10
            {
                height: 15px;
                font-weight: bold;
            }
            .style11
            {
                height: 21px;
            }
            .style14
            {
                width: 136px;
            }
            .style15
            {
                height: 24px;
                width: 136px;
            }
            .style16
            {
                width: 252px;
            }
            .style17
            {
                width: 111px;
                height: 30px;
            }
            .style19
            {
                width: 252px;
                height: 30px;
            }
            .style20
            {
                height: 30px;
                color: #CC3300;
            }
            .style21
            {
                width: 114px;
                height: 30px;
            }
            .style22
            {
                width: 114px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"alt="print" style="width: 34px" id="IMG1" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellspacing="3" 
            style="border: thin groove black; font-size:14px; width: 65%;">
    <tr> 
    <td style="border: thin groove black;" colspan="5" class="style8"> 
                         <asp:Label ID="lblmarkfed" 
                        runat="server" Text="M.P.State Cooperative Marketing Federation Ltd" Visible = "false"></asp:Label>
         <asp:Label ID="lblscsc" runat="server" 
             Text="Madhya Pradesh State Civil Supply Corporation District " ForeColor ="Maroon" 
             Font-Size ="Medium"></asp:Label> 
        
         <asp:Label ID="lbldistt" runat="server" Text="" style="font-size: medium"></asp:Label>
        </td>
    
    </tr>
    <tr> 
    <td style="border: thin groove black;" colspan="5" align = "center"> 
        स्कंध आंशिक अस्वीकृति पत्रक</td>
    
    </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Sr.No:</td>
    <td align="left" style="border: thin groove black;" class="style14"> <asp:Label ID="lblgno" runat="server" Text=""> </asp:Label></td>
    <td style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> </td>
    <td align ="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        Date</td>
    <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
        :<asp:Label ID="lblgdtae" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> 1.</td>
    <td align ="left" style="border: thin groove black;" class="style14">Name Of Depot </td>
    <td align ="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" > :<asp:Label ID="lbldepon" runat="server" Text=""></asp:Label></td>
    <td align ="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Crop Year</td>
    <td align ="left" style="width: 184px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblcrop" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            2.</td>
            <td align="left" style="border: thin groove black;" class="style14">
                Sending District</td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                :<asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
            <td align="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                </td>
            <td align="left" style="width: 184px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                </td>
        </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                3.</td>
            <td align="left" style="border: thin groove black;" class="style14">
                Purchase Center</td>
            <td align="left" colspan="3" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove">
               :<asp:Label ID="lblpccenter" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                4</td>
            <td align="left" style="border: thin groove black;" class="style14">
                Challan Number</td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblchallan" runat="server"></asp:Label>
            </td>
            <td align="left" style="width: 147px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: center;">
                Challan Date</td>
            <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
                <asp:Label ID="lblchallanDate" runat="server"></asp:Label>
            </td>
        </tr>
     
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                5</td>
            <td align="left" style="border: thin groove black;" class="style14">
                Truck Number</td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lbltruckno" runat="server"></asp:Label>
            </td>
            <td align="left" style="width: 147px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: center;">
                Transporter Name</td>
            <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
                <asp:Label ID="lbltransname" runat="server"></asp:Label>
            </td>
        </tr>
     
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> 
        6</td>
    <td align ="left" style="border: thin groove black;" class="style14"> Commodity Name </td>
    <td align ="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Rejection Date</td>
    <td align ="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
        :
        <asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
    </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="border: thin groove black;" class="style14">
            </td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 147px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: center;">
            </td>
            <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
            </td>
        </tr>
    
   
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left; height: 24px;"> 
        7</td>
    <td align ="left" style="border: thin groove black;" class="style15"> 
        <strong>Partial Rejection</strong> No.</td>
    <td align ="left" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove; height: 24px;" colspan="2">:<asp:Label ID="lblwcmno" runat="server" Text="" Font-Bold="true" Font-Size="14px"></asp:Label></td>
    <td align ="left" style="width: 184px; height: 24px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> </td>
    </tr>
        <tr>
            <td colspan="5" style="border: thin groove black; text-align: left"  valign = "top">
                
                 <asp:GridView Width="99%" ID="grd_viewDepot" runat="server" 
                     AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                     CellPadding="3" GridLines="Vertical">
                      
                        <Columns>
                                                    
                            <asp:BoundField DataField="Bags" HeaderText="Sent Bags."></asp:BoundField>
                            <asp:BoundField DataField="QtyTransffer" HeaderText="Sent Quantity"></asp:BoundField>
                            
                             <asp:BoundField DataField="Reject_bags" HeaderText="Reject Bags." ></asp:BoundField>
                            <asp:BoundField DataField="Reject_Qty" HeaderText="Reject Quantity"></asp:BoundField>
                            
                            <asp:BoundField DataField="Recd_Bags" HeaderText="Recv Bags." ></asp:BoundField>
                            <asp:BoundField DataField="Recv_Qty" HeaderText="Recv Quantity"></asp:BoundField>
                            
                            
                        </Columns>
                        
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    </asp:GridView>
                
            </td>
        </tr>
    </table>
    <table style="border: thin groove black; font-size:14px; width: 65%; text-align: left;">
    <tr> 
    <td style="text-align: left; " class="style3">
        8
        Discription</td>
    <td align="left" class="style6" colspan="3">
            <span style="font-size: 12pt; font-family: Mangal"><span style="font-size: 11pt">दिनांक</span>
                <asp:Label ID="Label2" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label>
                <span style="font-size: 11pt"><span style="font-size: 10pt">को केन्द्र पर प्राप्त</span>
                    <asp:Label ID="commodity" runat="server" Font-Size="Small" 
                Font-Bold="True"></asp:Label>
                    <span style="font-size: 10pt">निर्धारित गुणवत्ता का नहीं पाया गया| अत:&nbsp; 
        निम्न कारणों से
        <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif'; mso-ascii-font-family: Calibri;
                    mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin;
                    mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin; mso-bidi-theme-font: minor-bidi;
                    mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI">अमान्य
                    किया जाता है *</span></span></span></span></td>
    </tr>
    <tr> 
    <td class="style4"></td>
    <td style="height: 21px; " colspan="3">1.स्कंध में बाह्य पदार्थ
        <asp:Label ID="lblextra" runat="server" Font-Bold="True"></asp:Label>&nbsp;% है जो निर्धारित 
        मापदंड से अधिक है |*</td>
    </tr>
    <tr> 
    <td class="style4"></td>
    <td colspan="3" class="style11">2 स्कंध में
        <asp:Label ID="lblaffect" runat="server" Font-Bold="True"></asp:Label>&nbsp;% दाने क्षतिग्रस्त, 
        जो निर्धारित मापदंड से अधिक है |*</td>
    </tr>
    <tr> 
    <td class="style4">&nbsp;</td>
    <td style="height: 21px; " colspan="3">3स्कंध
        <asp:Label ID="lblbright" runat="server" Font-Bold="True"></asp:Label>&nbsp;% चमक विहीन है जो 
        निर्धारित मापदंड से अधिक है |*</td>
    </tr>
    <tr> 
    <td class="style4">&nbsp;</td>
    <td style="height: 21px; " colspan="3">4स्कंध में नमी
        <asp:Label ID="lblmoist" runat="server" Font-Bold="True"></asp:Label>&nbsp;% है जो निर्धारित 
        मापदंड से अधिक है |*</td>
    </tr>
    <tr> 
    <td class="style4">&nbsp;</td>
    <td style="height: 21px; " colspan="3">5स्कंध में टूटन एवं सिकुड़े दाने
        <asp:Label ID="lblsplit" runat="server" Font-Bold="True"></asp:Label>&nbsp;% है जो निर्धारित 
        मापदंड से अधिक है |*</td>
    </tr>
    <tr> 
    <td class="style4">&nbsp;</td>
    <td style="height: 21px; " colspan="3">6 अन्य कारण :
        <asp:Label ID="lblother" runat="server"></asp:Label></td>
    </tr>
    <tr> 
    <td class="style17"></td>
    <td class="style21">
        <asp:Label ID="lbleror" runat="server" Text="" Visible="false"></asp:Label></td>
    <td class="style19"> &nbsp;&nbsp;
    </td>
        <td class="style20">
            * जो लागू ना हो उसे काट दिया जाए |</td>
    </tr>
    <tr> 
    <td class="style5">&nbsp;</td>
    <td class="style22">&nbsp;</td>
    <td class="style16"> &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr> 
    <td class="style5">Signature Of
    </td>
    <td class="style22">
        Signature Of&nbsp;</td>
    <td class="style16"> Signature of</td>
        <td>
            Signature Of
        </td>
    </tr>
    <tr> 
    <td class="style5"> Truck Driver</td>
    <td class="style22"> Incharge (Society)</td>
    <td class="style16">Incharge (NAN/Markfed)</td>
        <td>
            WLC Incharge</td>
    </tr>
    <tr> 
    <td class="style5"> &nbsp;</td>
    <td class="style22"> &nbsp;</td>
    <td class="style16">&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr> 
    <td class="style10" colspan="4" style="text-align: center" > पावती</td>
    </tr>
    <tr> 
    <td colspan="4" > उपरोक्तानुसार प्रदाय केंद्र&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblissuecenter" 
            runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; द्वारा अस्वीकृत स्कंध की 
        मात्रा&nbsp;&nbsp;&nbsp; <asp:Label ID="lblrejqty" runat="server" 
            Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; एवं&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblrejbags" runat="server" Font-Bold="True"></asp:Label> </td>
    </tr>
    <tr> 
    <td colspan="4" > अस्वीकृति बोरे दिनांक <asp:Label ID="lbldepon3" runat="server" 
            Text=""></asp:Label>&nbsp;को वापस प्राप्त किया |</td>
    </tr>
    <tr> 
    <td colspan="4" > &nbsp;</td>
    </tr>
    <tr> 
    <td class="style5" > हस्ताक्षर</td>
    <td class="style22" > &nbsp;</td>
    <td class="style16">&nbsp;&nbsp;</td>
        <td>
            हस्ताक्षर</td>
    </tr>
    <tr> 
    <td colspan="2" > परिवहनकर्ता के प्रतिनिधि</td>
    <td class="style16">&nbsp;</td>
        <td>
            उपार्जन समिति प्रबंधक</td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
