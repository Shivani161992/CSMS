<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_MillingAgreement.aspx.cs" Inherits="PaddyMilling_Print_Print_MillingAgreement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .A4 {
            width: 210mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: medium;
        }

        .LineBreak {
            /*page-break-before: always;*/
            height: 0mm;
            overflow: hidden;
        }

        .auto-style1 {
            width: 100%;
            border-style: solid;
            border-width: 2px;
            border-color: black;
        }

        p {
            margin-right: 0in;
            margin-left: 0in;
            font-size: 12.0pt;
            font-family: "Times New Roman","serif";
        }

        @page {
            size: auto; /* auto is the current printer page size */
            margin: 25px 25px 0px 30px; /* this affects the margin in the printer settings */
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .auto-style3 {
            width: 100%;
        }
    </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <br />
        <div class="A4">
            <table align="center" class="auto-style1">

                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center; color: #FF0000;" class="auto-style3" >मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड</td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">जिला कार्यालय ....<asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True"></asp:Label>(<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>)</td>
               
                     </tr>

                 

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2"> Miller Agreement </span>&gt;&gt;
                        <input id="hdfCropYear" type="hidden" runat="server" /></strong></td>

                </tr>
            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

               <tr>
                    <td style="text-align: left">क्रमांक/डिपोजिट आर्डर/धान मिलिंग/....<asp:Label ID="lblAgreeNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">दिनाँक- ....<asp:Label ID="lblAgrntDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                  <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">प्रति,</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left; padding-left: 40px;"> 
                        <asp:Label ID="lblMillerName" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                
                
                <tr>
                    <td style="text-align: left; padding-left: 40px;"> मो नो. &nbsp;
                        <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right">&nbsp;</td>
                    </tr>
                <tr>
                    <td style="text-align: left; padding-left: 40px;"> मिलर का जिला
                        <asp:Label ID="lblMillerPlace" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left; padding-left: 32px;">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">विषय :-&nbsp; मिलर अनुबंध का विवरण |</td>
                </tr>

                <tr>
                    <td style="text-align: left;" colspan="2">सन्दर्भ:-&nbsp; धान मिलिंग अनुबंध क्र.....<asp:Label ID="lblAgrmtNo" runat="server" Font-Bold="True"></asp:Label>.... अनुबंध दिनांक....<asp:Label ID="lblAgrmtDate" runat="server" Font-Bold="True"></asp:Label>....अनुबंधित मात्रा....<asp:Label ID="lblQty" runat="server" Font-Bold="True"></asp:Label>....Qtls.</td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 40px;">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; मिलर ....<asp:Label ID="lblMillName" runat="server" Font-Bold="True"></asp:Label>.... ने दिनाँक ....<asp:Label ID="lblAgreeDate" runat="server" Font-Bold="True"></asp:Label>.... को ....<asp:Label ID="lblLotNo" runat="server" Font-Bold="True"></asp:Label>....  लॉट, अनुबंधित मात्रा....<asp:Label ID="lblquantity" runat="server" Font-Bold="True"></asp:Label>....Qtls. का अनुबंध ....<asp:Label ID="lblAgmntNo" runat="server" Font-Bold="True"></asp:Label>.... किया है , जिसकी अनुबंध अवधि ....<asp:Label ID="lblagrmtFrmDate" runat="server" Font-Bold="True"></asp:Label>..... से ....<asp:Label ID="lblagrmtToDate" runat="server" Font-Bold="True"></asp:Label>..... तक है| 
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        मिलर की मिलिंग क्षमता(मै०टन/घंटा) अरवा....<asp:Label ID="lblarva" runat="server" Font-Bold="True"></asp:Label>.... और उसना ....<asp:Label ID="lblushna" runat="server" Font-Bold="True"></asp:Label>..... है और उसका मिलिंग प्रकार ....<asp:Label ID="lblMillingType" runat="server" Font-Bold="True"></asp:Label>..... है|<%-- मिलर ने ....<asp:Label ID="lblSecLot" runat="server" Font-Bold="True"></asp:Label>.....लॉट की सिक्यूरिटी राशि ....<asp:Label ID="lblSecAmt" runat="server" Font-Bold="True"></asp:Label>..... जमा की है, जिसका प्रकार ....<asp:Label ID="lblSecDepType" runat="server" Font-Bold="True"></asp:Label>..... है|  </td>--%>
                    </tr>
                
                <tr>
                    <td style="text-align: right" colspan="2">&nbsp;</td>
                </tr>
                
               <tr>
                  <td style=" color:black;"  > कामन धान(Qtls) &nbsp; ....<asp:Label ID="lblCommPaddy" runat="server" Font-Bold="True"></asp:Label>....  </td>
                  
                  <td style=" color: black;"  > ग्रेड-ए धान(Qtls) &nbsp; ....<asp:Label ID="lblGradePaddy" runat="server" Font-Bold="True"></asp:Label>.... </td>
                  </tr>
                <tr>
                    <td style="text-align:center; color:black;" colspan="2"  > कुल धान(Qtls) &nbsp; ....<asp:Label ID="lblTotalDhan" runat="server" Font-Bold="True"></asp:Label>....  </td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2"></td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;" colspan="2">(जिला प्रबंधक)</td>
                </tr>
                <tr>
                    <td style="text-align: right;" colspan="2">....<asp:Label ID="lblDM" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">प्रतिलिपि,</td>
                </tr>
                <tr>
                    <td style="text-align: justify; padding-left: 60px" colspan="2">श्री ............................................</td>
                </tr>
                <tr>
                    <td style="text-align: justify; padding-left: 60px" colspan="2">गुणवत्ता निरीक्षक</td>
                </tr>
                <tr>
                    <td style="text-align: justify; padding-left: 60px" colspan="2">.................................................</td>
                </tr>
                <tr>
                    <td style="text-align: justify; padding-left: 60px" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: justify; padding-left: 60px" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;" colspan="2">(जिला प्रबंधक)</td>
                </tr>
            </table>

            <table align="center" width="100%">

                <tr class="auto-style4">
                    <td style="text-align: left">
                        <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right"></td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>