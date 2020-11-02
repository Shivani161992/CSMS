<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Paddy_Issued_Godown1.aspx.cs" Inherits="PaddyMilling_Paddy_Issued_Godown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../PaddyMilling/Scripts/jquery-ui.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

      <script type="text/javascript" language="javascript">
          function validate() {
              if (Page_ClientValidate())
                  return confirm('क्या आप इसे Submit करना चाहते है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
          }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style9 {
            font-size: small;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            vertical-align: text-bottom;
        }

         .hiddencol {
            display: none;
        }
    </style>

    <style type="text/css">
        #divSample {
            width: 900px;
            height: 500px;
            overflow: auto;
        }
    </style>


    <table align="center" style="width: 850px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><span style="vertical-align: top; font-weight: bold; text-decoration: underline">Paddy Issued To Miller From Godown</span><span style="vertical-align: top; font-weight: bold; color: blue"> (Mapping)</span>

                <input id="hdfDistanceDist" type="hidden" runat="server" /></td>

        </tr>

        <tr>
            <td rowspan="9"></td>
            <td colspan="4" style="text-align: center; background-color: #CCCCCC"></td>
            <td rowspan="9"></td>
        </tr>

        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>



        <tr>
            <td style="text-align: left;">मिल का नाम</td>
            <td class="auto-style8" style="text-align: left; width: 270px">
                <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td style="text-align: left;">अनुबंध नंबर</td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Height="27px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAgtmtNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr runat="server" id="trGrid2" visible="false">
            <td style="text-align: left; background-color: #CCFF99;" colspan="4">
                <div style="height: 150px; overflow: auto;" align="center">
                    <asp:GridView ID="GridView2" Width="100%" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" ShowFooter="false" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView2_RowDataBound" Style="font-size: small">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:BoundField ReadOnly="true" HeaderText="S.No" HeaderStyle-Width="40px" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="ICName" HeaderText="Issue Centre" HeaderStyle-Width="108px" ItemStyle-Width="108px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>


                            <asp:BoundField DataField="Godown_id" HeaderText="Godown" HeaderStyle-Width="260px" ItemStyle-Width="260px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>


                            <asp:BoundField DataField="distance" HeaderText="Distance (Km)" HeaderStyle-Width="70px" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Original Paddy Available (Qtls)" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Paddy Already Mapped (Qtls)" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Rem. Paddy Available (Qtls)" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="IssueCenter" HeaderText="ICId" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Godown_id" HeaderText="GodamID" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom"></ItemStyle>
                            </asp:BoundField>


                        </Columns>
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>
                </div>
            </td>
        </tr>

        <tr>
            <td style="text-align: left">कामन धान<b><span class="auto-style9">(Qtls)</span></b></td>
            <td class="auto-style8" style="text-align: left">
                <asp:TextBox ID="txtTotalCDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                &nbsp;<b>(Total)</b></td>
            <td style="text-align: left">ग्रेड-ए धान<b><span class="auto-style9">(Qtls)</span></b></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtTotalGDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                <b>&nbsp;(Total)</b></td>
        </tr>

        <tr>
            <td style="text-align: left">कामन धान<b><span class="auto-style9">(Qtls)</span></b></td>
            <td class="auto-style8" style="text-align: left">
                <asp:TextBox ID="txtRemCommonDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                &nbsp;<b>(Rem.)</b>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemCommonDhan" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: left">ग्रेड-ए धान<b><span class="auto-style9">(Qtls)</span></b></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtRemGradeADhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                <b>&nbsp;(Rem.)</b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRemGradeADhan" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td style="text-align: left">प्रदाय केंद्र </td>
            <td class="auto-style8" style="text-align: left">
                <asp:DropDownList ID="ddlIssueCentre" runat="server" Height="26px" Width="141px" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="text-align: left">गोदाम का नाम</td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlGodown" runat="server" Height="26px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">बची हुई धान</td>
            <td class="auto-style8" style="text-align: left">
                <asp:TextBox ID="txtGodownRemDhan" runat="server" MaxLength="15" Width="137px" Style="text-align: right" Enabled="False"></asp:TextBox>
                &nbsp;<b>(Qtls)</b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGodownRemDhan" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: left; background-color: #CCFF99; font-size: medium">Add Qty<strong> (Qtls)</strong></td>
            <td style="text-align: left; background-color: #CCFF99; font-size: medium">
                <asp:TextBox ID="txtQty" runat="server" Width="91px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 1px" OnClick="btnAdd_Click" />

            </td>
        </tr>


        <tr style="background-color: #CCCCCC">
            <td colspan="4" align="center">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No.">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ICName" HeaderText="Issue Center">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GodownName" HeaderText="Godown" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity (Qtls)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">

                            <FooterStyle HorizontalAlign="Right"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="YellowGreen" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>
            </td>
        </tr>


        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Enabled="False" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" OnClientClick="return validate();" CausesValidation="False" />

                <%--<asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />--%>

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

