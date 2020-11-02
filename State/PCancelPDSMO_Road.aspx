<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCancelPDSMO_Road.aspx.cs" Inherits="State_PCancelPDSMO_Road" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Cancel MO</title>

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
                    <input id="hdfEmpID" type="hidden" runat="server" />
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">मुख्यालय भोपाल</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt; <span class="auto-style2">CANCELLED <asp:Label ID="lblPDS" runat="server" Text=""></asp:Label>&nbsp;MOVEMENT ORDER</span> &gt;&gt; </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left">MO Number : ....<asp:Label ID="lblMovmtNo" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">MO Date : ....<asp:Label ID="lblDate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">Commodity : ....<asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>....<asp:Label ID="lblGunnyTypes" runat="server" Visible="False"></asp:Label></td>
                    <td style="text-align: right">Transportation : ....<asp:Label ID="lblTransMode" runat="server" Text=""></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">Cancelled By Name : ....<asp:Label ID="lblCancelName" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">Cancelled By Mobile No : ....<asp:Label ID="lblCancelMONo" runat="server" Text=""></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">Cancelled Date : ....<asp:Label ID="lblCancelDate" runat="server" Text=""></asp:Label>....</td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center">&nbsp;</td>

                </tr>

                <tr style="text-align: center">
                    <td>
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
                    <td style="text-align: center">&nbsp;</td>

                </tr>
                <tr>
                    <td style="text-align: center; text-decoration: underline; font-weight: bold"><< महत्वपूर्ण नोट >></td>

                </tr>
                <tr>
                    <td style="text-align: justify;"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; इस मूवमेंट आर्डर को मुख्यालय भोपाल द्वारा निरस्त कर दिया गया हैं| इस मूवमेंट आर्डर के विरुद्ध सभी प्रकार के मूवमेंट (Movement Plan, Transport Order, Delivery Challan etc) को रोक दिया गया हैं|</td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
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
