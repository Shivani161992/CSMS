<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintRejProc_Pdy2016.aspx.cs" Inherits="IssueCenter_PrintRejProc_Pdy2016" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .A4 {
            width: 240mm; /* You willmay need to reduce this to handle printer margins */
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

        .auto-style4 {
            font-size: small;
        }
    </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <br />
        <br />
        <div class="A4">
            <table align="center" class="auto-style1">

                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">
                        <asp:Label ID="lblMFD" runat="server" Text="" Style="font-size: large"></asp:Label>

                        <input id="hdfreceiptid" type="hidden" runat="server" />
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lbldist" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2">Rejected Truck Acknowledgement Receipt of Procurement</span>&gt;&gt;</strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">1) Sr.No- ....<asp:Label ID="lblgno" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Date- ....<asp:Label ID="lblgdtae" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">2) Issue Centre-....<asp:Label ID="lbldepon" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Sending District-....<asp:Label ID="lblsenddist" runat="server" Font-Bold="True" Style="font-size: small"></asp:Label>....</td>

                </tr>



                <tr>
                    <td style="text-align: left" colspan="2">3) Purchase Center-....<asp:Label ID="lblpccenter" runat="server" Font-Bold="True" Style="font-size: small"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">4) Challan Number-....<asp:Label ID="lblchallan" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Challan Date-....<asp:Label ID="lblchallanDate" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">5) Transporter Name-....<asp:Label ID="lbltransname" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Truck Number-....<asp:Label ID="lbltruckno" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">6) Sending Bags-....<asp:Label ID="lblsend_bagsNum" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Sending Qty<strong>(Qtls)</strong>-....<asp:Label ID="lblSend_Qtydisplay" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center; font-size: large" colspan="4"><strong>Description</strong></td>
                </tr>

                <tr>
                    <td style="text-align: justify;" colspan="4">दिनांक
                        <asp:Label ID="Label2" runat="server" Font-Bold="True"></asp:Label>
                        को केन्द्र पर प्राप्त
                        <asp:Label ID="commodity" runat="server" Font-Bold="True"></asp:Label>
                        निर्धारित गुणवत्ता का नहीं पाया गया| अत: निम्न कारणों से अमान्य किया जाता है:-
                    </td>
                </tr>

                     <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>A.</strong> स्कंध में <asp:Label ID="lblFAQ" runat="server" Font-Bold="True"></asp:Label>% एफ०ए०क्यू० नहीं है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>B.</strong> स्कंध में बाह्य पदार्थ
                        <asp:Label ID="lblextra" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित 
        मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>C.</strong> स्कंध में
                        <asp:Label ID="lblaffect" runat="server" Font-Bold="True"></asp:Label>% दाने क्षतिग्रस्त, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>D.</strong> स्कंध
                        <asp:Label ID="lblbright" runat="server" Font-Bold="True"></asp:Label>% चमक विहीन है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>E.</strong> स्कंध में नमी
                        <asp:Label ID="lblmoist" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>F.</strong> स्कंध में टूटन एवं सिकुड़े दाने
                        <asp:Label ID="lblsplit" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>G.</strong> स्कंध आंशिक क्षतिग्रस्त
                        <asp:Label ID="lblPartial" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4"><strong>H.</strong> अन्य कारण:
                        <asp:Label ID="lblother" runat="server" Style="font-weight: 700"></asp:Label></td>
                </tr>

                <tr>
                    <td style="text-align: right;" colspan="4"><strong>* जो लागू ना हो उसे काट दिया जाए|</strong></td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left: 25px;" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">Signature of Truck Driver</td>
                    <td style="text-align: center">Signature of Incharge (Society)</td>
                    <td style="text-align: center">Signature of Incharge (NAN/Markfed)</td>
                    <td style="text-align: right">Signature of Branch Manager</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: center">&nbsp;</td>
                    <td style="text-align: center">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
            </table>


            <table align="center" width="100%">

                <tr class="auto-style4">
                    <td style="text-align: left">
                        <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
