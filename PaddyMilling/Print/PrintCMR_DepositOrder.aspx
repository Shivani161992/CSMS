<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCMR_DepositOrder.aspx.cs" Inherits="PaddyMilling_Print_PrintCMR_DepositOrder" %>

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
                    <td style="text-align: center" class="auto-style3">जिला कार्यालय ....<asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2"> CMR DEPOSIT ORDER </span>&gt;&gt;
                        <input id="hdfCropYear" type="hidden" runat="server" /></strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">क्रमांक/डिपोजिट आर्डर/धान मिलिंग/....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">दिनाँक- ....<asp:Label ID="lblChallanDate" runat="server" Font-Bold="True"></asp:Label>....</td>
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
                    <td style="text-align: left; padding-left: 40px;">धान मिलर</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 40px;">
                        <asp:Label ID="lblMillerPlace" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 32px;">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">विषय :-&nbsp; CMR जमा करने बाबत |</td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">सन्दर्भ:-&nbsp; धान मिलिंग अनुबंध क्र. ....<asp:Label ID="lblAgrmtNo" runat="server" Font-Bold="True"></asp:Label>.... अनुबंध दिनांक....<asp:Label ID="lblAgrmtDate" runat="server" Font-Bold="True"></asp:Label>....अनुबंधित मात्रा....<asp:Label ID="lblQty" runat="server" Font-Bold="True"></asp:Label>....Qtls.
                        <asp:Label ID="lblAgMove" runat="server" Font-Bold="false" Visible="false"></asp:Label>
                        <asp:Label ID="lblMovementOrder" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 40px;">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; कृपया संदर्भित अनुबंध क्रमांक के विरुद्ध लॉट क्रमांक ....<asp:Label ID="lblLotNo" runat="server" Font-Bold="True"></asp:Label>.... तथा डी०ओ० क्रमांक ....<asp:Label ID="lblDoNO" runat="server" Font-Bold="True"></asp:Label>.... से आपको जारी की गयी ....<asp:Label ID="lblAgrmtDist" runat="server" Font-Bold="True"></asp:Label>.... 
                        जिले की धान के विरुद्ध जमा कर्ता जिला <asp:Label ID="lbldepositdist" runat="server" Font-Bold="True" Visible="True"></asp:Label>  CMR प्रदाय केंद्र ....<asp:Label ID="lblIC" runat="server" Font-Bold="True"></asp:Label>.... ब्रांच ....<asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>.... के गोदाम ....<asp:Label ID="lblGodam" runat="server" Font-Bold="True"></asp:Label>.... पर दिनांक ....<asp:Label ID="lblCMRdeliverDate" runat="server" Font-Bold="True"></asp:Label>.... को जमा करें |</td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2"></td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; कृपया डिपोजिट आर्डर में उल्लेखित गोदाम पर ही CMR जमा करने का कष्ट करें | अन्य किसी गोदाम में जमा करने पर स्वीकार नहीं किया जाएगा तथा आपका डिपोजिट आर्डर क्रमांक ....<asp:Label ID="lblDepositNo" runat="server" Font-Bold="True"></asp:Label>.... है|</td>
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
