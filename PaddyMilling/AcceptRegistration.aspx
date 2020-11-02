<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="AcceptRegistration.aspx.cs" Inherits="PaddyMilling_AcceptRegistration" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <table align="center" class="auto-styleM5" backcolor="White" bordercolor="Black"
        borderwidth="1" forecolor="Black" gridlines="Both" borderstyle="Solid">
        <tr>
            <td>
                <!DOCTYPE html>

                <html xmlns="http://www.w3.org/1999/xhtml">

                <title></title>

                <style type="text/css">
                    .auto-style1
                    {
                        width: 100%;
                    }

                    .auto-styleNSC
                    {
                        width: 500px;
                    }

                    .ButtonClass
                    {
                        cursor: pointer;
                    }

                    .auto-style20
                    {
                        width: 100%;
                    }


                    .hideGridColumn
                    {
                        display: none;
                    }
                </style>


                <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

                <script type="text/javascript">
                    var TotalChkBx;
                    var Counter;

                    window.onload = function () {
                        //Get total no. of CheckBoxes in side the GridView.
                        TotalChkBx = parseInt('<%= this.GridView1.Rows.Count %>');

                        //Get total no. of checked CheckBoxes in side the GridView.
                        Counter = 0;
                    }

                    function HeaderClick(CheckBox) {
                        //Get target base & child control.
                        var TargetBaseControl =
                            document.getElementById('<%= this.GridView1.ClientID %>');
                        var TargetChildControl = "chk_select";

                        //Get all the control of the type INPUT in the base control.
                        var Inputs = TargetBaseControl.getElementsByTagName("input");

                        //Checked/Unchecked all the checkBoxes in side the GridView.
                        for (var n = 0; n < Inputs.length; ++n)
                            if (Inputs[n].type == 'checkbox' &&
                                      Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                                Inputs[n].checked = CheckBox.checked;

                        //Reset Counter
                        Counter = CheckBox.checked ? TotalChkBx : 0;
                    }

                    function ChildClick(CheckBox, HCheckBox) {
                        //get target control.
                        var HeaderCheckBox = document.getElementById(HCheckBox);

                        //Modifiy Counter; 
                        if (CheckBox.checked && Counter < TotalChkBx)
                            Counter++;
                        else if (Counter > 0)
                            Counter--;

                        //Change state of the header CheckBox.
                        if (Counter < TotalChkBx)
                            HeaderCheckBox.checked = false;
                        else if (Counter == TotalChkBx)
                            HeaderCheckBox.checked = true;
                    }
                </script>

                <%--For Calendar Controls--%>
                <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
                <script type="text/javascript" lang="" src="calendar/calendar.js"></script>

                <div>
                    <table align="center" class="auto-style20">
                        <tr>
                            <td style="text-align: center; color: #CC6600; font-weight: 700; font-size: x-large; text-decoration: underline">Approval Miller Registration</td>
                        </tr>
                    </table>
                    <br />


                    <table align="center" style="border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
                        <tr>



                            <td style="text-align: center"><strong>Crop Year</strong></td>



                            <td colspan="3" style="ruby-align: center">
                                <asp:DropDownList ID="ddlCropyear" CssClass="ddl" runat="server" Height="25px" Width="240px" AutoPostBack="True">
                                </asp:DropDownList>


                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCropyear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                            </td>

                        </tr>

                        <tr>



                            <td class="auto-style31" rowspan="2"><strong>Miller&#39;s State</strong></td>
                            <td class="auto-style30">
                                <asp:RadioButton ID="rbMPState" runat="server" Font-Bold="True" GroupName="State" Text="M.P State" AutoPostBack="True" ForeColor="#336600" OnCheckedChanged="rbMPState_CheckedChanged" />
                            </td>

                            <td class="auto-style29">
                                <%--  <asp:DropDownList ID="ddlMPDist" runat="server" Height="26px" Width="178px">
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtdist" runat="server" CssClass="txt" Width="240px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                            </td>

                            <td class="auto-style29" rowspan="2">
                                <asp:Button ID="btnSearch" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Search" Width="115px" CssClass="ButtonClass" OnClick="btnSearch_Click" />
                            </td>

                        </tr>

                        <%-- <tr>



                            <td class="auto-style30">
                                <asp:RadioButton ID="rbOtherState" runat="server" Font-Bold="True" GroupName="State" Text="Other States" AutoPostBack="True" ForeColor="#336600" OnCheckedChanged="rbOtherState_CheckedChanged" />
                            </td>

                            <td class="auto-style29">
                                <asp:DropDownList ID="ddlOtherStates" runat="server" Height="26px" Width="178px" Enabled="False">
                                </asp:DropDownList>
                                <asp:Label ID="lblOtherStates" runat="server" Font-Bold="True" ForeColor="Red" Text="**" Visible="False"></asp:Label>

                            </td>

                        </tr>--%>

                        <%--        <tr>



                            <td class="auto-style31">दिनांक से </td>
                            <td class="auto-style30">
                                <asp:TextBox ID="txtFromDate" runat="server" Width="100px" Style="margin-left: 10px" ReadOnly="True"></asp:TextBox>
                                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder2_txtFromDate' ,'expiry=false')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>

                            <td class="auto-style29" colspan="2">दिनांक तक 

                                                        <asp:TextBox ID="txtToDate" runat="server" Width="100px" Style="margin-left: 10px" ReadOnly="True"></asp:TextBox>
                                &nbsp;<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder2_txtToDate' ,'expiry=false' )" /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                &nbsp;
                              </td>

                        </tr>--%>
                    </table>
                    <br />

                    <table align="center" class="auto-style1">
                        <tr>

                            <td>

                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" EnableModelValidation="True" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderWidth="1px" OnRowCreated="GridView1_RowCreated">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkBxHeader"
                                                    onclick="javascript:HeaderClick(this);" runat="server" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>



                                        <asp:BoundField DataField="Mill_Name" HeaderText="मिल का नाम" SortExpression="Mill_Name">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="operator_name" HeaderText="मिल संचालक का नाम" SortExpression="operator_name">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="District" HeaderText="जिला" SortExpression="Mill_Name">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tehsil" HeaderText="तहसील" SortExpression="operator_name">

                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="milling_capacity_arwa" HeaderText="मिलिंग क्षमता (अरवा)" SortExpression="operator_name">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="milling_capacity_usna" HeaderText="मिलिंग क्षमता (उसना)" SortExpression="Mill_Name">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mill_phone" HeaderText="टेलीफ़ोन न." SortExpression="operator_name">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="मोबाइल नंबर" SortExpression="Miller_MobileNo">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Miller_MobileNo") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Miller_MobileNo" runat="server" Text='<%# Bind("Miller_MobileNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Created_date" HeaderText="मिल पंजीकरण का दिनांक" SortExpression="operator_name" DataFormatString="{0:d}">


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>


                                        <asp:TemplateField HeaderText="Registration" ItemStyle-CssClass="hideGridColumn" HeaderStyle-CssClass="hideGridColumn">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Registration_ID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Registration_ID") %>'></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                                            <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                        </asp:TemplateField>


                                    </Columns>


                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                </asp:GridView>

                            </td>

                        </tr>

                    </table>


                    <table align="center" width="100%" style="text-align: center">
                        <tr>

                            <td>&nbsp;</td>

                            <%--<td>&nbsp;</td>--%>

                            <td>&nbsp;</td>
                        </tr>
                        <tr>

                            <td colspan="2">
                                <asp:Button ID="New" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="New_Click" />
                                <asp:Button ID="btnAccept" runat="server" BackColor="Silver" Style="margin-left: 100px" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Approval" Width="115px" CssClass="ButtonClass" OnClick="btnAccept_Click" OnClientClick="return confirm('Are You Sure To Accept Records?');" Enabled="False" />

                                <asp:Button ID="Close" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Style="margin-left: 100px" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="Close_Click" />

                            </td>

                            <%--   <%--<td>
                                <<asp:Button ID="btnReject" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Reject" Width="115px" CssClass="ButtonClass" OnClick="btnReject_Click" OnClientClick="return confirm('Are You Sure To Reject Records?');" Enabled="False" />
                            </td>--%>
                        </tr>
                    </table>

                </div>
            </td>
        </tr>
    </table>
</asp:Content>

