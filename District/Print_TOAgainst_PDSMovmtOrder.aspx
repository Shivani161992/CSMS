<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_TOAgainst_PDSMovmtOrder.aspx.cs" Inherits="District_Print_TOAgainst_PDSMovmtOrder" %>

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

        .auto-style3 {
            width: 100%;
        }

        .auto-style4 {
            font-size: medium;
        }

        .auto-style5 {
            text-align: justify;
            font-size: medium;
        }
        .auto-style6 {
            width: 100%;
            font-size: large;
        }
        .auto-style7 {
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
                    <td style="text-align: center" class="auto-style6">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड</td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="100px" Width="110px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style6">जिला कार्यालय : ....<asp:Label ID="lblDistName" runat="server" Text=""></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style6"><strong>&lt;&lt;<span class="auto-style2">Transport Order Against HO PDS Movement Order (By Road)</span>&gt;&gt;
                    </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left" class="auto-style4">परिवहन आदेश क्रमांक : ....<asp:Label ID="lblToOrderNO" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right" class="auto-style4">दिनांक : ....<asp:Label ID="lblDate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left" class="auto-style4">प्रति,</td>
                    <td style="text-align: right" class="auto-style4">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2" class="auto-style4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ....<asp:Label ID="lblTransporterName" runat="server" Text=""></asp:Label>....&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2" class="auto-style4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ....LRT परिवहनकर्ता....&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2" class="auto-style4">विषय : ....<asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>.... के परिवहन बाबत |</td>
                </tr>

                <tr>
                    <td style="text-align: justify" colspan="2" class="auto-style4">संदर्भ : मुख्यालय का MOVEMENT ORDER क्रमांक ....<asp:Label ID="lblMovmtNo" runat="server" Text=""></asp:Label>.... दिनांक ....<asp:Label ID="lblMODate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td colspan="2" class="auto-style5">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; विषयान्तर्गत कृपया&nbsp;दिनांक ....<asp:Label ID="lblTOEndDate" runat="server"></asp:Label>.... तक ....<asp:Label ID="lblTotalQty" runat="server"></asp:Label>.... एम०टी० ....<asp:Label ID="lblComdty1" runat="server" Text=""></asp:Label>....<asp:Label ID="lblCropYear" runat="server"></asp:Label>.... तालिका <strong>&#39;अ&#39;</strong> अनुसार प्रदाय केन्द्रों से परिवहन कर, तालिका <strong>&#39;ब&#39;</strong> अनुसार जिले के प्रदाय केन्द्रों को पहुँचाना सुनिश्चित करें | निर्धारित दिनांक तक निर्धारित मात्रा का परिवहन न किये जाने की स्थिति में निविदा शर्तो के अनुसार दंडात्मक कार्यवाही की जावेगी |</td>
                </tr>


            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td class="auto-style4"><strong>(अ) स्कंध जहाँ से परिवहन किया जाना है :-</strong></td>

                </tr>

                <tr>
                    <td style="font-size: medium">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" Width="100%" OnRowDataBound="GridView1_RowDataBound1">
                            <Columns>
                                <asp:BoundField HeaderText="क्र.">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="प्रदाय केंद्र" DataField="ICName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="ब्रांच" DataField="Branch">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
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
                    <td colspan="2"><strong><span class="auto-style4">(ब) स्कंध कहाँ परिवहन किया जाना है :-</span></strong>
                        <asp:HiddenField ID="hdfMovmtOrderNo" runat="server" />
                        <asp:HiddenField ID="hdfSubMocementOrderNo" runat="server" />
                        <asp:HiddenField ID="hdfToDistCode" runat="server" />
                        <asp:HiddenField ID="hdfTransportNumber" runat="server" />
                    </td>

                </tr>

                <tr style="text-align: center">
                    <td colspan="2" style="font-size: medium">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" OnRowDataBound="GridView2_RowDataBound" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="क्र.">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="प्राप्तकर्ता जिला" DataField="ToDistName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="प्रदाय केंद्र" DataField="ICName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="ब्रांच" DataField="Branch">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
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

                <tr class="auto-style4">
                    <td style="text-align: center">&nbsp;</td>

                    <td style="text-align: center">&nbsp;</td>

                </tr>
                <tr>
                    <td style="text-align: right" colspan="2" class="auto-style4">(जिला प्रबंधक)</td>
                    <tr class="auto-style4">
                        <td style="text-align: left">परिवहन आदेश क्रमांक : ....<asp:Label ID="lblToOrderNO1" runat="server" Text=""></asp:Label>....</td>
                        <td style="text-align: right">दिनांक : ....<asp:Label ID="lblDate1" runat="server"></asp:Label>....</td>
                    </tr>
            </table>

            <table align="center" width="100%">

                <tr class="auto-style4">
                    <td style="text-align: left">
                        <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right">Page-<b>1</b>,लगातार....</td>
                </tr>

            </table>
        </div>

        <div class="LineBreak"></div>
        <div class="A4">

            <br />
            <table align="center" class="auto-style1">
                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="100px" Width="100px" />
                    </td>
                    <td style="text-align: center" class="auto-style6">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड</td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode1" runat="server" Height="100px" Width="110px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><span class="auto-style7">जिला कार्यालय : ....<asp:Label ID="lblDistName1" runat="server" Text=""></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"></span><strong><span class="auto-style7">&lt;&lt;<span class="auto-style2">Transport Order Against HO PDS Movement Order (By Road)</span>&gt;&gt;
                    </span>
                    </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="font-size: medium">....पूर्व प्रष्ठ से
                    </td>
                </tr>
                <tr class="auto-style4">
                    <td style="text-align: left">परिवहन आदेश क्रमांक : ....<asp:Label ID="lblToOrderNO2" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">दिनांक : ....<asp:Label ID="lblDate2" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

            </table>




            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left" class="auto-style4">प्रतिलिपि : </td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr style="text-align: justify">
                    <td style="text-align: justify" class="auto-style4">
                        <ul>
                            <li>महाप्रबंधक (साविप्र), मुख्यालय, भोपाल की ओर MOVEMENT ORDER क्रमांक ....<asp:Label ID="lblMovmtNo1" runat="server" Text=""></asp:Label>.... दिनांक ....<asp:Label ID="lblMODate1" runat="server" Text=""></asp:Label>.... के अनुक्रम में सूचनार्थ |</li>
                            <li>जिला प्रबंधक, मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड, जिला कार्यालय ....<asp:Label ID="lblDistName2" runat="server" Text=""></asp:Label>.... सूचनार्थ एवं आवश्यक व्यवस्था हेतु |</li>
                            <li>प्रदाय केंद्र प्रभारी, शाखा ....<asp:Label ID="lblMixIC" runat="server" Text=""></asp:Label>.... कृपया उपार्जन वर्ष ....<asp:Label ID="lblCropYear1" runat="server"></asp:Label>.... का ....<asp:Label ID="lblComdty2" runat="server" Text=""></asp:Label>.... परिवहन कर्ता को उपरोक्तानुसार मात्रा में जारी किया जाये | उपरोक्तानुसार वर्णित गोदामों पर स्कंध उपलब्ध न होने की स्थिति में ही अन्य गोदामों से प्रदाय किया जाये |</li>
                        </ul>
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

                <tr>
                    <td style="text-align: center">&nbsp;</td>

                    <td style="text-align: center">&nbsp;</td>

                </tr>

                <tr>
                    <td style="text-align: right; font-size: medium;" colspan="2">(जिला प्रबंधक)</td>
            </table>

            <table align="center" width="100%">

                <tr class="auto-style4">
                    <td style="text-align: left">
                        <asp:Label ID="lblCurrentDateTime1" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right">Page-<b>2</b></td>
                </tr>

            </table>
        </div>

    </form>
</body>
</html>
