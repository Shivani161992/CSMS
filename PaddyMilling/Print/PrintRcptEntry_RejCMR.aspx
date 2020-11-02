<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintRcptEntry_RejCMR.aspx.cs" Inherits="PaddyMilling_Print_PrintRcptEntry_RejCMR" %>

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

        .auto-style3 {
            width: 100%;
        }

        .auto-style4 {
            width: 100%;
            font-size: large;
        }

        .auto-style5 {
            font-weight: bold;
        }

        .auto-style6 {
            font-weight: bold;
            text-decoration: underline;
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
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style4"><strong>MP State Civil Supplies Corporation Limited</strong></td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lblDistName" runat="server" Font-Bold="True"></asp:Label>....KMS<asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><span class="auto-style6">&lt;&lt; Rejected CMR (</span><asp:Label ID="lblCMRType" runat="server" Font-Bold="True" CssClass="auto-style6"></asp:Label><span class="auto-style6">) Acceptance Note &gt;&gt;</span></td>

                </tr>
            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td>Rejection No - ....<asp:Label ID="lblRejectionNo" runat="server" Font-Bold="True"></asp:Label>....
                    </td>

                    <td style="text-align: right">Rejection Date - ....<asp:Label ID="lblRejectionDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td>Miller State&nbsp;&nbsp;&nbsp; -&nbsp;....<asp:Label ID="lblMillerState" runat="server" Font-Bold="True"></asp:Label>....
                    </td>

                    <td style="text-align: right">Miller District - ....<asp:Label ID="lblMillerDistrict" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>

                <tr>
                    <td colspan="2">Miller Name&nbsp; - ....<asp:Label ID="lblMillerName" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>



            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>
                        <asp:Label ID="lblAcptRjctNO" runat="server" Text="Acceptance No -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblAcptNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">
                        <asp:Label ID="lblAcptRjctDate" runat="server" Text="Acceptance Date -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblAcptDate" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>
                <tr>
                    <td>Miller Tags - ....<asp:Label ID="lblTag" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">No. of Tag - ....<asp:Label ID="lblTagNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td>Toul Parchi No. - ....<asp:Label ID="lblToulNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Type of Bags - ....<asp:Label ID="lblBagType" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="font-size: medium">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" Width="100%" OnRowDataBound="GridView1_RowDataBound1">
                            <Columns>

                                <asp:BoundField HeaderText="Issue Center" DataField="DepotName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Godown" DataField="Godown_id">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Quantity (Qtls)" DataField="Recd_Qty">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Bags" DataField="Bags">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Truck No." DataField="TruckNo">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Truck No1." DataField="TruckNo1">
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
                    <td class="auto-style5" colspan="3"><span class="auto-style7"></span><span class="auto-style6">
                        <br />
                    </span>
                        <asp:Label ID="lblmsg1" runat="server" Text=""></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td style="font-size: medium" colspan="3">
                        <asp:Label ID="lblmsg2" runat="server" Text=""></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td colspan="3">&nbsp;</td>

                </tr>

                <tr>
                    <td colspan="3">&nbsp;</td>

                </tr>

                <tr>
                    <td colspan="3">&nbsp;</td>

                </tr>

                <tr>
                    <td colspan="3">&nbsp;</td>

                </tr>

                <tr>
                    <td style="text-align: left"><b>मिलर्स हस्ताक्षर</b></td>
                    <td style="text-align: right"><b>शाखा प्रबंधक हस्ताक्षर(भण्डारण संस्था)</b></td>

                    <td style="text-align: right"><b>Signature (Issue Center Incharge) MPSCSC</b></td>

                </tr>
            </table>

            <asp:Label ID="lblServerDateTime" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
