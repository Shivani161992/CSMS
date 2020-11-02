<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_MovementOrder.aspx.cs" Inherits="State_Print_MovementOrder" %>

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
            font-size: large;
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
        <div class="A4">

            <br />
            <br />
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
                    <td style="text-align: center" class="auto-style3">मुख्यालय भोपाल</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt; <span class="auto-style2"><asp:Label ID="lblPDS" runat="server" Text=""></asp:Label>&nbsp;MOVEMENT ORDER</span> &gt;&gt; </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left">एम०ओ० क्रमांक : ....<asp:Label ID="lblMovmtNo" runat="server" Text=""></asp:Label>....<asp:Label ID="lblCancel" runat="server" Text="" Style="font-weight: 700"></asp:Label></td>
                    <td style="text-align: right">एम०ओ० दिनांक : ....<asp:Label ID="lblDate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">कमोडिटी : ....<asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>....<asp:Label ID="lblGunnyTypes" runat="server" Visible="False"></asp:Label></td>
                    <td style="text-align: right">परिवहन का माध्यम : ....<asp:Label ID="lblTransMode" runat="server" Text=""></asp:Label>....</td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td colspan="2" style="text-align: center">&nbsp;</td>

                </tr>

                <tr style="text-align: center">
                    <td colspan="2">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="क्रमांक">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="प्रेषण कर्ता जिला">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="प्राप्तकर्ता जिला" DataField="ReceiveDistName">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="परिवहन अवधि">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="उपार्जन वर्ष">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="परिवहन की मात्रा (मै० टन)" DataField="Quantity">
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

                <tr>
                    <td colspan="2" style="text-align: left">कुल योग मात्रा (शब्दों में.... <asp:Label ID="lblnotowords" Font-Bold="true" runat="server" ></asp:Label>.... )</td>

                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; text-decoration: underline; font-weight: bold"><< महत्वपूर्ण नोट >></td>

                </tr>
                <tr>
                    <td colspan="2" style="text-align: justify">इस मूवमेंट आर्डर के विरुद्ध संबंधित जिला कार्यालयों को निम्न कार्यवाही किया जाना है:-</td>

                </tr>
                <tr>
                    <td colspan="2" style="text-align: justify"><span style="font-weight:bold; margin-left:15px">• प्रथम कार्यवाही -&nbsp;</span> स्कंध प्राप्तकर्ता जिले पहले CSMS से Movement Plan बनाये, कि किस प्रदाय केन्द्र पर स्कंध प्राप्त करना चाहते है | </td>

                </tr>
                 <tr>
                    <td colspan="2" style="text-align: justify"><span style="font-weight:bold; margin-left:15px">• द्वितीय कार्यवाही -&nbsp;</span> इस Movement Plan के अनुसार स्कंध प्रेषण कर्ता जिले अपने परिवहनकर्ता को CSMS से परिवहन आदेश जारी करेंगे | </td>

                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">प्रबंध संचालक द्वारा अनुमोदित </td>
                    <td style="text-align: right">महाप्रबंधक(<asp:Label ID="lblSig" runat="server" Text=""></asp:Label>)</td>

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
