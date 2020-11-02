<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_IssueAC_Kharif2016.aspx.cs" Inherits="IssueCenter_Print_IssueAC_Kharif2016" %>

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
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lbldist" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2">&nbsp; Statement of Receiving and Acceptance </span>&gt;&gt;</strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">1) Acceptance Number- ....<asp:Label ID="lblacceptnum" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Acceptance Date- ....<asp:Label ID="lblacceptdate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">2) Recd. Issue Center- ....<asp:Label ID="lblissCen" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Recd. Date- ....<asp:Label ID="lblrecddate" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">3) Recd. Godown-&nbsp;&nbsp; ....<asp:Label ID="lblgodown" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>.... </td>
                    <td style="text-align: right">Commodity- ....<asp:Label ID="lblcomm" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                </tr>

                <tr>
                    <td colspan="2"><span style="float: left">4) Purchase Center- ....<asp:Label ID="lblpurchaseCent" runat="server" Font-Bold="True" Style="font-size: small"></asp:Label>....</span>
                        <span style="float: right"><strong>Qty in Qtls.kg.gms</strong></span></td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" colspan="2">
                        <asp:GridView ID="grd_viewDepot" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound"
                            ShowFooter="True" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                            <Columns>
                                <%-- <asp:BoundField DataField="GodownNO" HeaderText="Godown No"> </asp:BoundField>--%>
                                <asp:BoundField DataField="Receipt_Id" HeaderText="Issue Id" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="TC_Number" HeaderText="TC" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right"></asp:BoundField>

                                <%--<asp:BoundField DataField="DateOfIssue1" HeaderText="Dispatch Date" />--%>

                                <asp:TemplateField HeaderText="Dispatch Date" SortExpression="DateOfIssue1" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <itemstyle />
                                        <headerstyle />
                                        <asp:Label ID="lblcomm" runat="server" Text='<%# Bind("DateOfIssue1") %>' Font-Size="Small"> </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_comm" runat="server" ForeColor="Black" Text="Total" Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <%-- <asp:BoundField DataField="sendbags" HeaderText="Sent Bags" />--%>

                                <asp:TemplateField HeaderText="Sent Bags" SortExpression="sendbags" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <itemstyle />
                                        <headerstyle />
                                        <asp:Label ID="lblsenbag" runat="server" Text='<%# Bind("sendbags") %>' Font-Size="Small"> </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totsenbag" runat="server" ForeColor="Black"  Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <%--  <asp:BoundField DataField="sendqty" HeaderText="Dispatch Quantity" />--%>

                                <asp:TemplateField HeaderText="Dispatch Quantity" SortExpression="sendqty" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <itemstyle />
                                        <headerstyle />
                                        <asp:Label ID="lblsenqty" runat="server" Text='<%# Bind("sendqty") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totsenqty" runat="server" ForeColor="Black" Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="TaulParchi" HeaderText="WCM No" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                                <%--<asp:BoundField DataField="Acc_Bag" HeaderText="Recd Bags" />--%>

                                <asp:TemplateField HeaderText="Recd Bags" SortExpression="Acc_Bag" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <itemstyle />
                                        <headerstyle />
                                        <asp:Label ID="lblrecbags" runat="server" Text='<%# Bind("Acc_Bag") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totrecbag" runat="server" ForeColor="Black" Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <%-- <asp:BoundField DataField="Accept_Qty" HeaderText="Recd. Qty" />--%>

                                <asp:TemplateField HeaderText="Recd Qty" SortExpression="Accept_Qty" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <itemstyle />
                                        <headerstyle />
                                        <asp:Label ID="lblrecqty" runat="server" Text='<%# Bind("Accept_Qty") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totrecqty" runat="server" ForeColor="Black" Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField DataField="Stiching_bags" HeaderText="Poor Stiching" />--%>
                                <asp:TemplateField HeaderText="Poor Stiching" SortExpression="Stiching_bags" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <itemstyle />
                                        <headerstyle />
                                        <asp:Label ID="lblstibags" runat="server" Text='<%# Bind("Stiching_bags") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totstichbag" runat="server" ForeColor="Black" Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <%-- <asp:BoundField DataField="Stencile_bags" HeaderText="Poor Stencile" />--%>

                                <asp:TemplateField HeaderText="Poor Stencile" SortExpression="Stencile_bags" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <itemstyle />
                                        <headerstyle />
                                        <asp:Label ID="lblstenbags" runat="server" Text='<%# Bind("Stencile_bags") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totstencilebag" runat="server" ForeColor="Black" Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Moisture" HeaderText="Moisture Percentage" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"></asp:BoundField>
                                <%--<asp:BoundField DataField="category" HeaderText="category" HeaderStyle-Font-Size="Small" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Center"></asp:BoundField>--%>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">Signature (Issue Center Incharge)</td>
                </tr>

            </table>

            <table>

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
