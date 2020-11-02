<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDhan_Challan.aspx.cs" Inherits="PaddyMilling_Print_PDhan_Challan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">
                        <asp:Label ID="lblMFD" runat="server" Text="" style="font-size: large"></asp:Label>
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">जिला ....<asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2">&nbsp; धान चालान </span>&gt;&gt;</strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">1) चालान क्रमांक-&nbsp;&nbsp; ....<asp:Label ID="lblChallanNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">चालान दिनाँक- ....<asp:Label ID="lblChallanDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">2) डी०ओ० क्रमांक-&nbsp; ....<asp:Label ID="lblDoNumber" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right"> लॉट क्रमांक-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ....<asp:Label ID="lblLotNumber" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">3) डी०ओ० दिनाँक-&nbsp; ....<asp:Label ID="lblDoDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right"> डी०ओ० अंतिम दिनाँक-&nbsp; ....<asp:Label ID="lblDOLastDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


                <tr>
                    <td style="text-align: left">4) मिल का नाम-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;....<asp:Label ID="lblMillName" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right"> अनुबंध नंबर-&nbsp; ....<asp:Label ID="lblAgrmtNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: center" style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">5) जारी की गयी डी०ओ० की मात्रा- ....<asp:Label ID="lblTotalCDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                        कामन धान तथा ....<asp:Label ID="lblTotalGDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                        ग्रेड-ए धान</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">6) जारी की गयी चालान की मात्रा - ....<asp:Label ID="lblChalanCDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                        कामन धान तथा ....<asp:Label ID="lblChalanGDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                        ग्रेड-ए धान</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: center" style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">7) बोरों की संख्या- ....<asp:Label ID="lblNoBags" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">बोरों का प्रकार- ....<asp:Label ID="lblBagsType" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


                <tr>
                    <td style="text-align: left">8) ट्रक नंबर- ....<asp:Label ID="lblTruckNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: center">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">9) प्रदाय केन्द्र का नाम- ....<asp:Label ID="lblIssueCentreName" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">10) गोदाम का नाम- ....<asp:Label ID="lblGodownNo" runat="server" Font-Bold="True"></asp:Label>.... </td>
                </tr>
                 <tr>
                    <td style="text-align: left" colspan="2">10) स्टैक का नाम- ....<asp:Label ID="lblStackNo" runat="server" Font-Bold="True"></asp:Label>.... </td>
                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left">उपरोक्त विवरण के अनुसार धान जारी किया |</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">प्रदाय केन्द्र प्रभारी</td>
                </tr>

            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left" colspan="2">उपरोक्त विवरण के अनुसार धान प्राप्त किया |</td>
                </tr>

                <tr>
                    <td style="text-align: right" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">मिलर्स हस्ताक्षर</td>
                    <td style="text-align: right">परिवहन कर्ता / प्रतिनीधि</td>
                </tr>

            </table>
            
            <table>

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
