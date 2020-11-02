<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCMRInsp_Challan.aspx.cs" Inherits="PaddyMilling_Print_PrintCMRInsp_Challan" %>

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
                    <td style="text-align: center" class="auto-style3">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड 
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">जिला ....<asp:Label ID="lblDist" runat="server" Font-Bold="True"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2">&nbsp; CMR (<asp:Label ID="lblCMRType" runat="server" Font-Bold="True"></asp:Label>) Rejection Challan </span>&gt;&gt;</strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                 <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">Challan No.- ....<asp:Label ID="lblChallanNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                  <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">Challan Date.- ....<asp:Label ID="lblChallanDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>



                <tr>
                    <td style="text-align: left">1) Rejection No.- ....<asp:Label ID="lblRejectionNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Rejection Date-&nbsp;....<asp:Label ID="lblRejectionDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                 <tr>
                    <td style="text-align: left">2)CMR Acceptance Number-&nbsp; ....<asp:Label ID="lblCMRAccep" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">CMR DO Number....<asp:Label ID="lblCMRDO" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">3) Rejected Qty-&nbsp; ....<asp:Label ID="lblRejQty" runat="server" Font-Bold="True"></asp:Label>....<strong>(Qtls)</strong></td>
                    <td style="text-align: right">Rejected Bags-....<asp:Label ID="lblRejBags" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">4)Miller's Name-....&nbsp; ....<asp:Label ID="lblMillName" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Miller's Dist-....<asp:Label ID="lblMillerDist" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                  <tr>
                    <td style="text-align: left">5)Agreement Number-....&nbsp; ....<asp:Label ID="lblAgree" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Lot Number-....<asp:Label ID="lblLot" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


              

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: center" style="text-align: right">&nbsp;</td>
                </tr>

                



                <tr>
                    <td style="text-align: left">6) Issued Qty-&nbsp;&nbsp;&nbsp; ....<asp:Label ID="lblIssuedQty" runat="server" Font-Bold="True"></asp:Label>....<strong>(Qtls)</strong> </td>
                    <td style="text-align: right">Issued Bags-....<asp:Label ID="lblNoBags" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


                <tr>
                    <td style="text-align: left">7) Truck No.-&nbsp;&nbsp;&nbsp;&nbsp; ....<asp:Label ID="lblTruckNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Type of Bags-....<asp:Label ID="lblBagsType" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


                <tr>
                    <td style="text-align: left">8) Issue Centre- ....<asp:Label ID="lblIssueCentreName" runat="server" Font-Bold="True"></asp:Label>.... </td>
                    <td style="text-align: right">Godown-....<asp:Label ID="lblGodownNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                 <tr>
                    <td style="text-align: left">9) Stack Name- ....<asp:Label ID="lblStackNo" runat="server" Font-Bold="True"></asp:Label>.... </td>
                    <td style="text-align: right">Inspector Name-....<asp:Label ID="lblInspName" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left">उपरोक्त विवरण के अनुसार <asp:Label ID="lblCMRType1" runat="server" Font-Bold="True"></asp:Label> जारी किया |</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
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
                    <td style="text-align: left" colspan="2">उपरोक्त विवरण के अनुसार <asp:Label ID="lblCMRType2" runat="server" Font-Bold="True"></asp:Label> प्राप्त किया |</td>
                </tr>

                <tr>
                    <td style="text-align: right" colspan="2">&nbsp;</td>
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
