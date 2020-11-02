﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_MvmtPlanAgainst_PDSMO.aspx.cs" Inherits="District_Print_MvmtPlanAgainst_PDSMO" %>

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
            <br /><br />
            <table align="center" class="auto-style1">
                <tr>
                      <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड</td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="120px" Width="120px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">जिला कार्यालय : ....<asp:Label ID="lblDistName" runat="server" Text=""></asp:Label>....<asp:Label ID="lblDate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong><span class="auto-style2">Movement Plan Against HO <asp:Label ID="lblPDS" runat="server" Text=""></asp:Label>
                        Movement Order (By Road)</span>
                        <input id="hdfMovmtOrderNo" type="hidden" runat="server" />
                        <input id="hdfSubMocementOrderNo" type="hidden" runat="server" />

                    </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left">एम०ओ० क्रमांक : ....<asp:Label ID="lblMovmtNo" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">एम०ओ० का दिनांक : ....<asp:Label ID="lblMODate" runat="server"></asp:Label>....
                        </td>
                </tr>

                    <tr>
                    <td style="text-align: left">परिवहन का माध्यम : ....<asp:Label ID="lblTransMode" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">परिवहन की अवधि : ....<asp:Label ID="lblEndDate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">कमोडिटी : ....<asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>....<asp:Label ID="lblGunnyTypes" runat="server" Text=""></asp:Label></td>
                    <td style="text-align: right">उपार्जन वर्ष : ....<asp:Label ID="lblCropYear" runat="server"></asp:Label>....</td>
                </tr>

            

                <tr>
                    <td style="text-align: left">प्रेषण कर्ता जिला : ....<asp:Label ID="lblFrmDist" runat="server"></asp:Label>....</td>
                    <td style="text-align: right">परिवहन की मात्रा<asp:Label ID="lblMT" runat="server" style="font-weight: 700" Text=""></asp:Label>
&nbsp;: ....<asp:Label ID="lblTotalQty" runat="server"></asp:Label>....</td>
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
                                <asp:BoundField HeaderText="S.No">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Issue Center" DataField="ICName">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Branch Name" DataField="Branch">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Godown Name" DataField="Godown">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Qty To Be Deposited (MT)" DataField="RequiredQuantity">
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
                    <td style="text-align: center">&nbsp;</td>

                </tr>
                <tr>
                    <td style="text-align: center">&nbsp;</td>

                </tr>
                <tr>
                    <td style="text-align: right">(जिलाप्रबंधक)</td>

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
