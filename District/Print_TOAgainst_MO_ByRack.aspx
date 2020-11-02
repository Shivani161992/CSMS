<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_TOAgainst_MO_ByRack.aspx.cs" Inherits="District_Print_TOAgainst_PDSMovmtOrder" %>

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
            page-break-before: always;
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
            margin: 7px 25px 0px 25px; /* this affects the margin in the printer settings */
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .auto-style4 {
            font-weight: bold;
        }

        .auto-style5 {
            width: 100%;
            font-size: large;
        }
    </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <div class="A4">

            <table align="center" class="auto-style1">
                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="100px" Width="100px" />
                    </td>
                    <td style="text-align: center" class="auto-style5">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड
                        <input id="hdfMovmtOrderNo" type="hidden" runat="server" />
                          <input id="hdfSubMocementOrderNo" type="hidden" runat="server" />
                          <input id="hdfTransportNumber" type="hidden" runat="server" />
                          <input id="hdfToRailHead" type="hidden" runat="server" />
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="100px" Width="110px" />
                    </td>
                </tr>
                

                <tr>
                    <td style="text-align: center" class="auto-style5">जिला कार्यालय : ....<asp:Label ID="lblDistName" runat="server" Text=""></asp:Label>....(Sending District)</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style5"><strong>&lt;&lt;<span class="auto-style2">Transport Order Against HO PDS Movement Order (By Rack)</span>&gt;&gt;
                    </strong></td>

                </tr>
                
            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left">परिवहन आदेश क्रमांक : ....<asp:Label ID="lblToOrderNO" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">दिनांक : ....<asp:Label ID="lblDate" runat="server"></asp:Label>....</td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">रैक क्रमांक : ....<asp:Label ID="lblRackNo" runat="server" Text=""></asp:Label>....</td>
                </tr>
                <tr>
                    <td style="text-align: left">प्रति,</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ....<asp:Label ID="lblTransporterName" runat="server" Text=""></asp:Label>....&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ....HLRT परिवहनकर्ता....&nbsp;
                           
                    </td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">विषय : ....<asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>....<asp:Label ID="lblGunnyType" runat="server" Visible="False"></asp:Label>&nbsp;के परिवहन बाबत |</td>
                </tr>

                <tr>
                    <td style="text-align: justify" colspan="2">संदर्भ : मुख्यालय का MOVEMENT ORDER क्रमांक ....<asp:Label ID="lblMovmtNo" runat="server" Text=""></asp:Label>.... दिनांक ....<asp:Label ID="lblMODate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: justify" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; विषयान्तर्गत कृपया दिनांक ....<asp:Label ID="lblTOEndDate" runat="server"></asp:Label>.... तक ....<asp:Label ID="lblTotalQty" runat="server"></asp:Label>.... 
                        <asp:Label ID="lblMT" runat="server" Text=""></asp:Label>
                        &nbsp;....<asp:Label ID="lblComdty1" runat="server" Text=""></asp:Label>....(<asp:Label ID="lblCropYear" runat="server"></asp:Label>).... तालिका <strong>&#39;अ&#39;</strong> अनुसार कारपोरेशन के निम्न प्रदाय केन्द्रों से प्राप्त कर, रेल हेड ....<asp:Label ID="lblFrmRailHead" runat="server" Text=""></asp:Label>.... पर पहुँचाना सुनिश्चित करें | निर्धारित दिनांक तक निर्धारित मात्रा का परिवहन न किये जाने की स्थिति में निविदा शर्तो के अनुसार दंडात्मक कार्यवाही की जावेगी |</td>
                </tr>


            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td><strong>(अ) स्कंध जहाँ से परिवहन किया जाना है :-</strong></td>

                </tr>

                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" Width="100%" OnRowDataBound="GridView1_RowDataBound1">
                            <Columns>
                                <asp:BoundField HeaderText="क्र.">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="प्रदाय केंद्र" DataField="ICName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Branch" HeaderText="ब्रांच">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="गोदाम" DataField="Godown">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="परिवहन की मात्रा (मे०टन)" DataField="RequiredQuantity">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        </asp:GridView>

                    </td>

                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center">&nbsp;</td>

                    <td style="text-align: center">&nbsp;</td>

                </tr>

                <tr>
                    <td style="text-align: center">&nbsp;</td>

                    <td style="text-align: center">&nbsp;</td>

                </tr>

                <td style="text-align: right" colspan="2">(जिला प्रबंधक)</td>
                <tr>
                    <td style="text-align: left">परिवहन आदेश क्रमांक : ....<asp:Label ID="lblToOrderNO1" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">दिनांक : ....<asp:Label ID="lblDate1" runat="server"></asp:Label>....</td>
                </tr>
                <tr>
                    <td style="text-align: left" class="auto-style4">प्रतिलिपि : </td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr style="text-align: justify">
                    <td style="text-align: justify" colspan="2">
                        <ul>
                            <li>महाप्रबंधक (साविप्र), मुख्यालय, भोपाल की ओर MOVEMENT ORDER क्रमांक ....<asp:Label ID="lblMovmtNo1" runat="server" Text=""></asp:Label>.... दिनांक ....<asp:Label ID="lblMODate1" runat="server" Text=""></asp:Label>.... के अनुक्रम में सूचनार्थ |</li>
                            <li>जिला प्रबंधक, मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड, जिला कार्यालय ....<asp:Label ID="lblToDistName" runat="server" Text=""></asp:Label>.... सूचनार्थ एवं आवश्यक व्यवस्था हेतु |</li>
                            <li>प्रदाय केंद्र प्रभारी, शाखा ....<asp:Label ID="lblMixIC" runat="server" Text=""></asp:Label>.... कृपया उपार्जन वर्ष ....<asp:Label ID="lblCropYear1" runat="server"></asp:Label>.... का ....<asp:Label ID="lblComdty2" runat="server" Text=""></asp:Label>....<asp:Label ID="lblGunnyType2" runat="server" Visible="False"></asp:Label>&nbsp;परिवहन कर्ता को उपरोक्तानुसार मात्रा में जारी किया जाये | उपरोक्तानुसार वर्णित गोदामों पर स्कंध उपलब्ध न होने की स्थिति में ही अन्य गोदामों से प्रदाय किया जाये |</li>
                        </ul>
                    </td>
                </tr>

                <tr style="text-align: justify">
                   <td style="text-align: right" colspan="2">(जिला प्रबंधक)</td>
                </tr>
            </table>

            <table align="center" width="100%">

                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>
                   
                </tr>

            </table>
        </div>

    </form>
</body>
</html>
