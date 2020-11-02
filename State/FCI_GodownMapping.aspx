<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="FCI_GodownMapping.aspx.cs" Inherits="District_FCI_GodownMapping" Title="FCI Godown Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 608px; height: 350px" >
        <tr>
            <td colspan="2" style="height: 14px">
                Mapping to FCI Godown</td>
        </tr>
        <tr>
            <td style="height: 25px">
                Select District</td>
            <td style="height: 25px; text-align: left">
                <asp:DropDownList ID="ddldist" runat="server" Height="25px" Width="160px" 
                    AutoPostBack="True" onselectedindexchanged="ddldist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 28px">
                Select Issue Center</td>
            <td style="height: 28px; text-align: left">
                <asp:DropDownList ID="ddlissue" runat="server" Height="25px" 
                    Width="160px" AutoPostBack="True" 
                    onselectedindexchanged="ddlissue_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 28px">
                Select Godown Type</td>
            <td style="height: 28px; text-align: left">
                <asp:DropDownList ID="ddlgodownType" runat="server" Height="25px" 
                    Width="160px" AutoPostBack="True" 
                    onselectedindexchanged="ddlgodownType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 35px">
                Select Godown</td>
            <td style="height: 35px; text-align: left">
                <asp:DropDownList ID="ddlgodown" runat="server" Height="25px" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 9px">
                </td>
            <td style="height: 9px">
                </td>
        </tr>
        <tr>
            <td style="height: 27px">
                <asp:Button ID="btnclose" runat="server" Text="Close" 
                    onclick="btnclose_Click" />
            </td>
            <td style="height: 27px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                    CellPadding="4" CellSpacing="2" Font-Bold="False" Font-Size="Small" 
                    ForeColor="Black">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="district_name" HeaderText="district name" 
                            SortExpression="district_name">
                           
                        </asp:BoundField>
                        <asp:BoundField DataField="DepotName" HeaderText="Issue Center" 
                            SortExpression="DepotName">
                           
                        </asp:BoundField>
                        <asp:BoundField DataField="GodownName" HeaderText="Godown Name" 
                            SortExpression="GodownName">
                          
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Godownid" HeaderText="Godown Id" 
                            SortExpression="Godownid">
                           
                        </asp:BoundField>
                        
                         <asp:BoundField DataField="GodownType" HeaderText="Godown Type" 
                            SortExpression="GodownType">
                           
                        </asp:BoundField>
                       
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small" 
                        ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

