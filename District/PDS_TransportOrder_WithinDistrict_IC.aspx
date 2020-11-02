<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PDS_TransportOrder_WithinDistrict_IC.aspx.cs" Inherits="District_PDS_TransportOrder_WithinDistrict_IC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
         .tcolumn
        {
            width: 400px;
            text-align: center;
            border-radius: 32px;
            height: 35px;
            font-size: 15px;
            font-family: 'Lucida Fax';
        }

            .tcolumn:focus
            {
                box-shadow: 0 0 25px rgb(107,142,35);
                padding: 3px 0px 3px 3px;
                margin: 5px 1px 3px 0px;
                border: 1px solid rgba(81, 203, 238, 1);
                width:0px;
            }
             .tddl
        {
            width: 400px;
            border-radius: 32px;
            height: 35px;
            text-align: center;
            font-family: 'Lucida Fax';
        }

        </style>

    <table align="center" border="1"  style="width: 600px; background-color:#EAECEE; border-style: solid; border-width: 1px; text-align: left" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color:#000080"><strong>PDS Transport Order</strong></td>
        </tr>

        <tr>
            <td rowspan="13"></td>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; color: #FF3300; font-weight: 700;" Text="Label" Visible="False"></asp:Label>
                <br />
                
            </td>
            <td rowspan="13"></td>
        </tr>
         <tr>
            <td><b>District</b></td>
            <td>

                <asp:TextBox ID="txtDistrict" runat="server" Width="250px" class="tcolumn" Style="text-align:center" MaxLength="10" AutoComplete="off" Enabled="false" ></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td> <b>Commodity</b></td>
            <td>
                <asp:DropDownList ID="ddlCommodity" runat="server" Width="250px" class="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged" >
                </asp:DropDownList></td>
        </tr>

        <tr id="rowBags" runat="server" visible="false">
            <td> <b>Types of Gunny </b></td>
            <td>
                <asp:RadioButton ID="rdbJUTE" runat="server" GroupName="Gunny" style="font-weight: 700; color: #000080" Text="Jute(SBT)" />
                <asp:RadioButton ID="rdbPP" runat="server" GroupName="Gunny" style="font-weight: 700; color: #000080" Text="PP" />
            </td>
        </tr>

        <tr>
            <td> <b>Crop Year </b></td>
            <td>

                <asp:DropDownList ID="ddlCropYear" class="tddl" runat="server" Width="250px">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td><b>Mode of Dispatch Commodity </b></td>
            <td>

                <asp:TextBox ID="txtModeOfDispatch" runat="server" Width="250px" Style="text-align: center" class="tcolumn" MaxLength="10" AutoComplete="off"
                     Enabled="false"></asp:TextBox>

            </td>
        </tr>

        

        <tr>
            <td> <b>End Date of Transportation </b></td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" class="tcolumn" Width="250px" ReadOnly="True"></asp:TextBox>
                <img id="calid" runat="server" src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate', 'expiry=true,elapse=0,restrict=false')" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
        </tr>

        <tr>
            <td><b> From IssueCenter </b></td>
            <td>
                <asp:DropDownList ID="ddlFrm_IC" runat="server" class="tddl" Width="250px"  >
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td> <b>To IssueCenter </b></td>
            <td>
                <asp:DropDownList ID="ddlTo_IC" runat="server" class="tddl" Width="250px" >
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td><b>Quantity </b> <asp:Label ID="lblMT" runat="server" Text="" style="font-weight: 700; color: #FF3300;"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQty" runat="server" Width="250px" class="tcolumn" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="#000080" Style="margin-left: 13px" OnClick="btnAdd_Click"/>
                

            </td>
        </tr>



        <tr>
            <td colspan="2" style="text-align: center; margin-left: 40px;">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CellPadding="4" EnableModelValidation="True" ForeColor="#D2B4DE" GridLines="None"
                      OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" >
                    <AlternatingRowStyle BackColor="#EAECEE" />
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
                    <FooterStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

