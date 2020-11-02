<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_PartialRej_Pdy2016.aspx.cs" Inherits="IssueCenter_Print_PartialRej_Pdy2016" %>

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
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2"> Partial Rejection of Procurement </span>&gt;&gt;</strong></td>

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
                    <td style="text-align: right">Crop Year-....<asp:Label ID="lblcrop" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">3) Godown-....<asp:Label ID="lblGodown" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Sending District-....<asp:Label ID="lblsenddist" runat="server" Font-Bold="True" Style="font-size: small"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">4) Purchase Center-....<asp:Label ID="lblpccenter" runat="server" Font-Bold="True" Style="font-size: small"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">5) Challan Number-....<asp:Label ID="lblchallan" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Challan Date-....<asp:Label ID="lblchallanDate" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">6) Transporter Name-....<asp:Label ID="lbltransname" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Truck Number-....<asp:Label ID="lbltruckno" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">7) Partial Rejection No.-....<asp:Label ID="lblwcmno" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Rejection Date-....<asp:Label ID="lblmoisture" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" colspan="2">
                        <asp:GridView Width="100%" ID="grd_viewDepot" runat="server"
                            AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" GridLines="Vertical">

                            <Columns>

                                <asp:BoundField DataField="Bags" HeaderText="Sent Bags." ></asp:BoundField>
                                <asp:BoundField DataField="QtyTransffer" HeaderText="Sent Quantity"></asp:BoundField>

                                <asp:BoundField DataField="Reject_bags" HeaderText="Reject Bags."></asp:BoundField>
                                <asp:BoundField DataField="Reject_Qty" HeaderText="Reject Quantity"></asp:BoundField>

                                <asp:BoundField DataField="Recd_Bags" HeaderText="Recv Bags."></asp:BoundField>
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

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center;font-size:large" colspan="4" ><strong>Description</strong></td>
                </tr>

                <tr>
                    <td style="text-align: justify;" colspan="4" >दिनांक <asp:Label ID="Label2" runat="server" Font-Bold="True"></asp:Label> 
                        को केन्द्र पर प्राप्त <asp:Label ID="commodity" runat="server" Font-Bold="True"></asp:Label>
                        निर्धारित गुणवत्ता का नहीं पाया गया| अत: निम्न कारणों से अमान्य किया जाता है:-
                    </td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4"><strong>A.</strong> स्कंध में बाह्य पदार्थ <asp:Label ID="lblextra" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित 
        मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4"><strong>B.</strong> स्कंध में <asp:Label ID="lblaffect" runat="server" Font-Bold="True"></asp:Label>% दाने क्षतिग्रस्त, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4"><strong>C.</strong> स्कंध <asp:Label ID="lblbright" runat="server" Font-Bold="True"></asp:Label>% चमक विहीन है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4"><strong>D.</strong> स्कंध में नमी <asp:Label ID="lblmoist" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4"><strong>E.</strong> स्कंध में टूटन एवं सिकुड़े दाने <asp:Label ID="lblsplit" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4"><strong>F.</strong> स्कंध आंशिक क्षतिग्रस्त <asp:Label ID="lblPartial" runat="server" Font-Bold="True"></asp:Label>% है, जो निर्धारित मापदंड से अधिक है|*</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4"><strong>G.</strong> अन्य कारण: <asp:Label ID="lblother" runat="server" style="font-weight: 700"></asp:Label></td>
                </tr>

                <tr>
                    <td style="text-align: right;" colspan="4"><strong>* जो लागू ना हो उसे काट दिया जाए|</strong></td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: justify; padding-left:25px;" colspan="4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">Signature of Truck Driver</td>
                    <td style="text-align: center">Signature of Incharge (Society)</td>
                    <td style="text-align: center">Signature of Incharge (NAN/Markfed)</td>
                    <td style="text-align: right">Signature of WLC Incharge</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: center">&nbsp;</td>
                    <td style="text-align: center">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center" colspan="2"><strong>पावती</strong></td>
                </tr>

                <tr>
                    <td style="text-align: justify" colspan="2">उपरोक्तानुसार प्रदाय केंद्र <asp:Label ID="lblissuecenter" runat="server" Font-Bold="True"></asp:Label> द्वारा अस्वीकृत स्कंध की मात्रा <asp:Label ID="lblrejqty" runat="server" Font-Bold="True"></asp:Label> एवं <asp:Label ID="lblrejbags" runat="server" Font-Bold="True"></asp:Label>
                        अस्वीकृति बोरे दिनांक <asp:Label ID="lbldepon3" runat="server" Text="" style="font-weight: 700"></asp:Label> को वापस प्राप्त किया |
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">हस्ताक्षर परिवहनकर्ता के प्रतिनिधि</td>
                    <td style="text-align: right">हस्ताक्षर उपार्जन समिति प्रबंधक</td>
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
