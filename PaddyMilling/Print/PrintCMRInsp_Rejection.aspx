<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCMRInsp_Rejection.aspx.cs" Inherits="PaddyMilling_Print_PrintCMRInsp_Rejection" %>

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
                    <td style="text-align: center" class="auto-style3">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड</td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">जिला कार्यालय ....<asp:Label ID="lblDist" runat="server" Font-Bold="True"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2"> Inspection of CMR Rejection </span>&gt;&gt;
                        <input id="hdfCropYear" type="hidden" runat="server" /></strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">Rejection No. - ....<asp:Label ID="lblRejectionNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Rejection Date- ....<asp:Label ID="lblRejectionDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">Miller's State&nbsp; - ....<asp:Label ID="lblMillerState" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Miller's Dist.-....<asp:Label ID="lblMillerDist" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


                <tr>
                    <td style="text-align: justify;" colspan="2">Miller's Name- ....<asp:Label ID="lblMillerName" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">Issue Centre&nbsp;&nbsp; - ....<asp:Label ID="lblIC" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Branch-....<asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">Godown&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; - ....<asp:Label ID="lblGodown" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">Rejected CMR Qty <strong>(Qtls)-....<asp:Label ID="lblQty" runat="server" Font-Bold="True"></asp:Label>....</strong></td>
                    <td style="text-align: right">Stack No.-....<asp:Label ID="lblStackNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: justify;" colspan="2">Inspected By-....<asp:Label ID="lblInspectedName" runat="server" Font-Bold="True"></asp:Label>....</td>
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
                    <td style="text-align: right;" colspan="2">&nbsp;</td>
                </tr>

            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: justify;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: justify;" colspan="2">प्रतिलिपि,</td>
                </tr>
                <tr>
                    <td style="text-align: justify; padding-left: 60px" colspan="2">श्री ....<asp:Label ID="lblInspectedName1" runat="server" Font-Bold="True"></asp:Label>....</td>
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
