<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDSMovementOrder.aspx.cs" Inherits="State_PDSMovementOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
        .auto-style1 {
            color: #FF0000;
        }
        .auto-style2 {
            font-weight: bold;
            color: #FF3300;
        }
    </style>

    <table align="center" style="width: 600px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>PDS Movement Order</strong></td>
        </tr>

        <tr>
            <td rowspan="13"></td>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; color: #FF3300; font-weight: 700;" Text="Label" Visible="False"></asp:Label>
                <br />
                <asp:LinkButton ID="LinkbtnSubMO" runat="server" Visible="False" CausesValidation="False" PostBackUrl="~/State/PDS_SubMovementOrder.aspx">Go To The Sub Movement Order</asp:LinkButton>
            </td>
            <td rowspan="13"></td>
        </tr>

        <tr>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlCommodity" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged" >
                </asp:DropDownList></td>
        </tr>

        <tr id="rowBags" runat="server" visible="false">
            <td>Types of Gunny</td>
            <td>
               <%-- <asp:RadioButton ID="rdbJUTE" runat="server" GroupName="Gunny" style="font-weight: 700; color: #FF0000" Text="Jute(SBT)" />
                <asp:RadioButton ID="rdbPP" runat="server" GroupName="Gunny" style="font-weight: 700; color: #FF0000" Text="PP" />--%>

                <asp:DropDownList ID="ddlbagstype" runat="server" Width="141px" >
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Crop Year</td>
            <td>

                <asp:DropDownList ID="ddlCropYear" runat="server" Width="150px">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Mode of Dispatch Commodity</td>
            <td>

                <asp:DropDownList ID="ddlComdtyMode" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlComdtyMode_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr id="rowRdb" runat="server" visible="false" >
            <td colspan="2" style="text-align: center">
                <strong>रैक प्राप्तकर्ता जिले द्वारा स्कंध का वितरण</strong><br />
                <asp:RadioButton ID="rdbSelf" runat="server" CssClass="auto-style2" GroupName="rdbGroup" Text="स्वयं के जिले में" />
                <asp:RadioButton ID="rdbOther" runat="server" CssClass="auto-style2" GroupName="rdbGroup" style="margin-left:20px;" Text="अन्य जिलों में" />
                <asp:RadioButton ID="rdbBoth" runat="server" CssClass="auto-style2" GroupName="rdbGroup" style="margin-left:20px;" Text="स्वयं एवं अन्य जिलों में"/>
            </td>
        </tr>

        <tr>
            <td>End Date of Transportation</td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" Width="146px" ReadOnly="True"></asp:TextBox>
                <img id="calid" runat="server" src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate', 'expiry=true,elapse=0,restrict=false')" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
        </tr>

        <tr>
            <td>From District</td>
            <td>
                <asp:DropDownList ID="ddlFrmDist" runat="server" Width="150px"  >
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td>To District <span class="auto-style1">(Max. 15 Dist/MO)</span></td>
            <td>
                <asp:DropDownList ID="ddlToDist" runat="server" Width="150px" >
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Quantity <asp:Label ID="lblMT" runat="server" Text="" style="font-weight: 700; color: #FF3300;"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQty" runat="server" Width="146px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 13px" OnClick="btnAdd_Click"/>
                

            </td>
        </tr>



        <tr>
            <td colspan="2" style="text-align: center; margin-left: 40px;">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                      OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                        <asp:BoundField DataField="fromdisttext" HeaderText="From Dist." />
                        <asp:BoundField DataField="todisttext" HeaderText="To Dist." />
                        <asp:BoundField DataField="quantity" HeaderText="From Road" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"  FooterStyle-HorizontalAlign="Right"></asp:BoundField>
                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');"  Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>
            </td>

        </tr>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Enabled="False" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" CausesValidation="False" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>

</asp:Content>

