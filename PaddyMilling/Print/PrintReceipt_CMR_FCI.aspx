<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReceipt_CMR_FCI.aspx.cs" Inherits="PaddyMilling_Print_PrintReceipt_CMR_FCI" %>

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
                <br />
                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">
                        <asp:Label ID="lblMFD" runat="server" Text="" Style="font-size: large"></asp:Label>
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2">&nbsp;CMR Acceptance Note For FCI </span>&gt;&gt;</strong></td>

                </tr>
            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>

                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td>Miller Name - ....<asp:Label ID="lblMillerName" runat="server" Font-Bold="True"></asp:Label>....</td>

                    <td style="text-align: right">Agreement No - ....<asp:Label ID="lblAgrmtNo" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>
                <tr>
                    <td>Miller State&nbsp; -&nbsp;....<asp:Label ID="lblMillerState" runat="server" Font-Bold="True"></asp:Label>....
                    </td>

                    <td style="text-align: right">Miller District - ....<asp:Label ID="lblMillerDistrict" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>

                </tr>



                <tr>
                    <td>
                        <asp:Label ID="lblFCiState0" runat="server" Text="CMR FCI State -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblFCiState" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">
                        <asp:Label ID="lblFCiDist0" runat="server" Text="CMR FCI District -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblFCiDist" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>



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
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Weight Check Memo No.-"></asp:Label>
                        &nbsp;....<asp:Label ID="lblWeightMemoNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">
                        <asp:Label ID="Label3" runat="server" Text="Weight Check Memo Date -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblWeightMemoDate" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>




            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="font-size: medium">&nbsp;</td>

                </tr>

                <tr>
                    <td style="font-size: medium">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Receipt No" DataField="RecptNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />

                                <asp:BoundField HeaderText="Lot No." DataField="Lot_No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle"></asp:BoundField>

                                <asp:BoundField HeaderText="Milling Type" DataField="Milling_Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle"></asp:BoundField>

                                <asp:BoundField HeaderText="Quantity (Qtls)" DataField="CommonRice" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle"></asp:BoundField>

                                <asp:BoundField HeaderText="Bags" DataField="Bags" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle"></asp:BoundField>

                                <asp:BoundField HeaderText="Type of Bags" DataField="BagType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" />

                                <asp:BoundField HeaderText="Truck No." DataField="Truck_No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle"></asp:BoundField>

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
                    <td style="font-size: medium">&nbsp;</td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>

                </tr>
                <tr style="text-align: right">
                    <td>
                        <asp:Label ID="lblDM" runat="server" Text=""></asp:Label>&nbsp;/ अधिकृत कर्मी</td>

                </tr>
            </table>
            <asp:Label ID="lblServerDateTime" runat="server" Text="" Font-Bold="true"></asp:Label>
        </div>
    </form>
</body>
</html>
