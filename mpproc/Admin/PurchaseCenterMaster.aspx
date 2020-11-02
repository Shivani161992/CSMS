<%@ Page Language="C#" MasterPageFile="~/mpproc/MasterPage/AdminMasterPage.master" AutoEventWireup="true" CodeFile="PurchaseCenterMaster.aspx.cs" Inherits="mpproc_Admin_PurchaseCenterMaster" Title="Purchase Center Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="font-size: small; vertical-align: middle; font-family: Calibri; background-color: #cccc99; text-align: center">
        <tr>
            <td colspan="7" style="font-weight: bold; font-size: medium; background-color: lightslategray">
                Purchase Center Master</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Select District to Filter the Records"
                    Width="214px"></asp:Label></td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddl_District" runat="server" Font-Names="Calibri" Font-Size="Small"
                    Width="94px" AutoPostBack="True" OnSelectedIndexChanged="ddl_District_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="7">
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:GridView ID="GridView_All" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" AllowPaging="True" OnPageIndexChanging="GridView_All_PageIndexChanging" PageSize="5" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView_All_SelectedIndexChanged">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:CommandField SelectText="Delete" ShowSelectButton="True" />
                        <asp:BoundField DataField="PurchaseCenterName" HeaderText="Purchase Center" />
                        <asp:BoundField DataField="MarketingSeason" HeaderText="Marketing Season" />
                        <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="NodalOfficer" HeaderText="Nodal Officer" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="BlockTehsil" HeaderText="Block/Tehsil" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="DistrictName" HeaderText="DistrictName" />
                        <asp:BoundField DataField="PcId" HeaderText="PcId">
                            <HeaderStyle Font-Size="0px" Width="0px" />
                            <ItemStyle Font-Size="0px" Width="0px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="7" style="font-size: 7pt">
                <span class="Apple-style-span" style="font-weight: normal; word-spacing: 0px; text-transform: none;
                    color: rgb(0,0,0); text-indent: 0px; line-height: normal; font-style: normal;
                    font-family: 'Times New Roman'; white-space: normal; letter-spacing: normal;
                    border-collapse: separate; font-variant: normal; orphans: 2; widows: 2; webkit-border-horizontal-spacing: 0px;
                    webkit-border-vertical-spacing: 0px; webkit-text-decorations-in-effect: none;
                    webkit-text-size-adjust: auto; webkit-text-stroke-width: 0px"><span class="Apple-style-span"
                        style="font-weight: bold; color: rgb(0,0,139); font-family: Verdana, Arial, Helvetica, sans-serif;
                        webkit-border-horizontal-spacing: 2px; webkit-border-vertical-spacing: 2px">Note:
                        Click " Select " to edit the Purchase Centre Name</span></span></td>
        </tr>
        <tr>
            <td align="left" colspan="7" style="height: 15px">
                <span class="Apple-style-span" style="word-spacing: 0px; font: medium 'Times New Roman';
                    text-transform: none; color: rgb(0,0,0); text-indent: 0px; white-space: normal;
                    letter-spacing: normal; border-collapse: separate; orphans: 2; widows: 2; webkit-border-horizontal-spacing: 0px;
                    webkit-border-vertical-spacing: 0px; webkit-text-decorations-in-effect: none;
                    webkit-text-size-adjust: auto; webkit-text-stroke-width: 0px"><span class="Apple-style-span"
                        style="font-weight: bold; font-size: 11px; color: rgb(0,0,139); font-family: Verdana;
                        webkit-border-horizontal-spacing: 1px; webkit-border-vertical-spacing: 1px">Fetch
                        Records of Purchase Centers for Previous District/Marketing Season/Crop Year Combination:</span></span></td>
        </tr>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Text="District"></asp:Label></td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddl_District_Fetch" runat="server" Font-Names="Calibri" Width="94px" OnSelectedIndexChanged="ddl_District_Fetch_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td style="width: 100px">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" Text="Crop Year"></asp:Label></td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddl_CropYear_Fetch" runat="server" Font-Names="Calibri" Width="94px">
                </asp:DropDownList></td>
            <td style="width: 100px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" Text="Marketing Season"></asp:Label></td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddl_MarketSeaon_Fetch" runat="server" Font-Names="Calibri" Width="94px">
                </asp:DropDownList></td>
            <td style="width: 100px">
                <asp:Button ID="btnFetch" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium"
                    Height="25px" Text="Fetch" Width="85px" OnClick="btnFetch_Click" /></td>
        </tr>
        <tr>
            <td colspan="7" rowspan="2">
                <asp:Panel ID="Panel_Fetch" runat="server" ScrollBars="Both" Visible="false" Height="300px">
                    <asp:GridView ID="GridView_Fetch" runat="server" AutoGenerateColumns="False" CellPadding="4" PageSize="5" OnPageIndexChanging="GridView_Fetch_PageIndexChanging" ForeColor="#333333" GridLines="None" >
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField>
                            <HeaderTemplate>
                            <asp:CheckBox ID="cklSelectAll" runat="server" Text="SelectAll" AutoPostBack="true"  OnCheckedChanged="cklSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:CheckBox ID="cklSelect" runat="server" />
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PurchaseCenterName" HeaderText="Purchase Center" />
                            <asp:BoundField DataField="MarketingSeason" HeaderText="Marketing Season" />
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="NodalOfficer" HeaderText="Nodal Officer" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="BlockTehsil" HeaderText="Block/Tehsil" />
                            <asp:BoundField DataField="Phone" HeaderText="Phone" />
                            <asp:BoundField DataField="DistrictName" HeaderText="DistrictName" />
                            <asp:BoundField DataField="PcId" HeaderText="PcId">
                                <HeaderStyle Font-Size="0px" Width="0px" />
                                <ItemStyle Font-Size="0px" Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CommodityId" HeaderText="CommodityId">
                                <HeaderStyle Font-Size="0px" Width="0px" />
                                <ItemStyle Font-Size="0px" Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DistrictId" HeaderText="DistrictId">
                                <HeaderStyle Font-Size="0px" Width="0px" />
                                <ItemStyle Font-Size="0px" Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MarkSeasId" HeaderText="MarkSeasId">
                                <HeaderStyle Font-Size="0px" Width="0px" />
                                <ItemStyle Font-Size="0px" Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PC_CategoryID" HeaderText="PC_CategoryID">
                                <HeaderStyle Font-Size="0px" Width="0px" />
                                <ItemStyle Font-Size="0px" Width="0px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    &nbsp;
                </asp:Panel>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td align="left" colspan="7">
                <span class="Apple-style-span" style="word-spacing: 0px; font: medium 'Times New Roman';
                    text-transform: none; color: rgb(0,0,0); text-indent: 0px; white-space: normal;
                    letter-spacing: normal; border-collapse: separate; orphans: 2; widows: 2; webkit-border-horizontal-spacing: 0px;
                    webkit-border-vertical-spacing: 0px; webkit-text-decorations-in-effect: none;
                    webkit-text-size-adjust: auto; webkit-text-stroke-width: 0px"><span class="Apple-style-span"
                        style="font-weight: bold; font-size: 11px; color: rgb(0,0,139); font-family: Verdana;
                        webkit-border-horizontal-spacing: 1px; webkit-border-vertical-spacing: 1px">Select
                        Purchase Center for District/Marketing Season/Crop Year Combination to Insert Record
                        into:</span></span></td>
        </tr>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small" Text="District"></asp:Label></td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddl_District_Selected" runat="server" Font-Names="Calibri" Width="94px" Enabled="False">
                </asp:DropDownList></td>
            <td style="width: 100px">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Small" Text="Crop Year"></asp:Label></td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddl_CropYear_Selected" runat="server" Font-Names="Calibri" Width="94px">
                </asp:DropDownList></td>
            <td style="width: 100px">
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Small" Text="Marketing Season"></asp:Label></td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddl_MarketSeason_Selected" runat="server" Font-Names="Calibri" Width="94px">
                </asp:DropDownList></td>
            <td style="width: 100px">
                <asp:Button ID="btnAddSelected" runat="server" Font-Bold="True" Font-Names="Calibri"
                    Font-Size="Medium" Height="23px" Text="Add Selected" Width="103px" OnClick="btnAddSelected_Click" /></td>
        </tr>
        <tr>
            <td colspan="7">
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
                <asp:Button ID="btnAddNew" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium"
                    Height="23px" Text="Add New" Width="103px" OnClick="btnAddNew_Click" /></td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel_Adnew" runat="server" Visible="False">
                    <table>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Small" Text="District"></asp:Label></td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="ddl_AddNewDistrict" runat="server" Font-Names="Calibri" Width="177px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 23px;">
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Small" Text="Crop Year"></asp:Label></td>
                            <td align="left" colspan="2" style="height: 23px">
                                <asp:DropDownList ID="ddl_AddNewCropYear" runat="server" Font-Names="Calibri" Width="177px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Small" Text="Marketing Season"></asp:Label></td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="ddl_AddNewMarketSeason" runat="server" Font-Names="Calibri"
                                    Width="177px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 21px;">
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="New Purchase Center"
                                    Width="136px"></asp:Label></td>
                            <td align="left" colspan="2" style="height: 21px">
                                <asp:TextBox ID="txtPurchaseCeneter" runat="server" Height="11px" Width="177px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:RadioButton ID="rd_Both" runat="server" Font-Bold="True" GroupName="Select"
                                    Text="Both" Checked="True" /></td>
                            <td style="width: 100px">
                                <asp:RadioButton ID="RadioButton1" runat="server" Font-Bold="True" GroupName="Select"
                                    Text="Only Rice Receiving Centre" Width="183px" /></td>
                            <td style="width: 100px">
                                <asp:RadioButton ID="RadioButton2" runat="server" Font-Bold="True" GroupName="Select"
                                    Text="Only Paddy Purchase Centre" Width="191px" /></td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="width: 100px">
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Remark"></asp:Label></td>
                            <td colspan="2" rowspan="2">
                                <asp:TextBox ID="txtRemark" runat="server" Height="35px" TextMode="MultiLine" Width="347px"></asp:TextBox></td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td rowspan="1" style="width: 100px">
                                <asp:Button ID="btnInsert" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium"
                                    Height="23px" Text="Insert" Width="103px" OnClick="btnInsert_Click" /></td>
                            <td align="left" colspan="2" rowspan="1">
                                <asp:Button ID="btnCancel" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium"
                                    Height="23px" Text="Cancel" Width="103px" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
                <asp:Label ID="Label13" runat="server" Text="Label" Visible="False" Width="65px"></asp:Label></td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
</asp:Content>

