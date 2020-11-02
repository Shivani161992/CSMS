<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="TOByRackAgainst_PDSMO.aspx.cs" Inherits="District_TOByRackAgainst_PDSMO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>


    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style3 {
            color: #0000FF;
            font-size: large;
            background-color: #FFCC99;
        }

        .auto-style4 {
            text-decoration: underline;
        }

        .auto-style5 {
            color: #0000FF;
        }
    </style>

    <table align="center" style="width: 760px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><span class="auto-style4"><strong>Transport Order(TO) Against PDS Movement Order(MO)</strong></span> <span class="auto-style5">[Receiving District]</span></td>
            <input id="hdfSubMocementOrderNo" type="hidden" runat="server" />
            <input id="hdfMovmtOrderNo" type="hidden" runat="server" />
            <input id="hdfDistID" type="hidden" runat="server" />
            <input id="hdfModeofDist" type="hidden" runat="server" />
            <input id="hdfSubSMO" type="hidden" runat="server" />
            <input id="hdfComdtyValue" type="hidden" runat="server" />
            <input id="hdfDispatchModeValue" type="hidden" runat="server" />
            <input id="hdfFrmDistValue" type="hidden" runat="server" />
            <input id="hdfQuantityValue" type="hidden" runat="server" />
            <input id="hdfServerDate" type="hidden" runat="server" />
            <input id="hdfToDate" type="hidden" runat="server" />

             <input id="hdfMOPlanIC" type="hidden" runat="server" />
            <input id="hdfMOPlanBranch" type="hidden" runat="server" />
            <input id="hdfMOPlanGodown" type="hidden" runat="server" />
            <input id="hdfGetTotalTOQty" type="hidden" runat="server" />
            <input id="hdfOnlyOne" type="hidden" runat="server" />
        </tr>



        <tr>
            <td rowspan="16"></td>
            <td colspan="4" style="text-align: center; background-color: #CCCCCC">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="16"></td>
        </tr>

        <tr>
            <td>MO Number</td>
            <td>
                <asp:DropDownList ID="ddlMONumber" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMONumber_SelectedIndexChanged" Style="font-weight: 700;" Height="21px">
                </asp:DropDownList>

            </td>
            <td>Crop-Year</td>
            <td>
                <asp:TextBox ID="txtCropYear" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCropYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td>Date of MO</td>
            <td>
                <asp:TextBox ID="txtDateMo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtDateMo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>End Date of MO</td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtEndDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td>From District</td>
            <td>
                <asp:TextBox ID="txtFrmDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtFrmDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>To District</td>
            <td>
                <asp:TextBox ID="txtToDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtToDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Commodity</td>
            <td>
                <asp:TextBox ID="txtComdty" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtComdty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Quantity <strong><asp:Label ID="lblMT" runat="server" Text=""></asp:Label>
                </strong></td>
            <td>
                <asp:TextBox ID="txtQuantity" runat="server" Width="146px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtQuantity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Label ID="Label3" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #CCCCCC">
              
            </td>
        </tr>

        <tr>
            <td>Transporter Name</td>
            <td>
                <asp:DropDownList ID="ddlTransporterName" runat="server" Width="150px">
                </asp:DropDownList>

            </td>
            <td>End Date of Transporation</td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" Width="146px" ReadOnly="True"></asp:TextBox>

                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'restrict=false,instance=single,close=true')" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Rail Head</td>
            <td>
                <asp:DropDownList ID="ddlFrmRack" runat="server" Width="150px">
                </asp:DropDownList>

            </td>
            <td>Sending District</td>
            <td>
                <asp:DropDownList ID="ddlSendingDist" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSendingDist_SelectedIndexChanged" Style="height: 22px">
                </asp:DropDownList>

            </td>
        </tr>


        <tr>
            <td colspan="4" style="background-color: #CCCCCC">


            </td>

        </tr>


        <tr id="trHideIC" runat="server">
            <td>Issue Center</td>
            <td>
                <asp:DropDownList ID="ddlIssueCentre" runat="server" Width="150px" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged" AutoPostBack="True" style="height: 22px">
                </asp:DropDownList>
            </td>
            <td>Branch Name</td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>


        <tr id="trHideGodown" runat="server">
            <td>Godown</td>
            <td>
                <asp:DropDownList ID="ddlGodown" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Qty To Be Dispatch <strong><asp:Label ID="lblMT0" runat="server" Text=""></asp:Label>
                </strong></td>
            <td>
                <asp:TextBox ID="txtQty" runat="server" Width="91px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 1px" OnClick="btnAdd_Click" />

            </td>
        </tr>

        <tr style="background-color: #CCCCCC" id="trIDReceived" runat="server" visible="false">
            <td colspan="4" align="center" class="auto-style3">
                <strong>Delivered To Godowns</strong></td>
        </tr>


        <tr style="background-color: #CCCCCC" id="trIDGrid1" runat="server">
            <td colspan="4" align="center">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No.">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DistName" HeaderText="District">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ICName" HeaderText="Issue Center">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BranchName" HeaderText="Branch">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GodownName" HeaderText="Godown" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                            <FooterStyle HorizontalAlign="Right"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity (MT)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">

                            <FooterStyle HorizontalAlign="Right"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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

        <tr style="background-color: #CCCCCC" id="trIDAdd" runat="server" visible="false">
            <td colspan="4" align="center" class="auto-style3">
                <strong>Movement Plan of </strong>
                <asp:Label ID="lblMvmtPlan" runat="server" Text="" Style="font-weight: 700"></asp:Label>
            </td>
        </tr>

        <tr style="background-color: #CCCCCC" id="trIDGrid" runat="server" visible="false">
            <td colspan="4" align="center">

                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ShowFooter="True" OnRowDataBound="GridView2_RowDataBound" Width="100%" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="S.No">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="District" DataField="DistName" HeaderStyle-Width="120px">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Issue Center" DataField="ICName" HeaderStyle-Width="120px">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Branch" DataField="Branch" HeaderStyle-Width="120px">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Godown" DataField="Godown">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Quantity (MT)" DataField="RequiredQuantity" HeaderStyle-Width="120px">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#666666" HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

                </asp:GridView>
            </td>
        </tr>




        <tr>
            <td colspan="5" style="text-align: center; font-size: large;">
                    

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Enabled="False" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" CausesValidation="False" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

