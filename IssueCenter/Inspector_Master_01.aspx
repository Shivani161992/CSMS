<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Inspector_Master.aspx.cs" Inherits="MasterPage_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <asp:Panel ID="Panel2" runat="server" Height="101px" Width="630px">
        <table style="width: 100%; border-style: solid; border-width: 2px; background-color: #CCFFCC">
            <tr>
                <td colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#004000" Text="Label" style="text-align: center"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label4" runat="server" Text="Inspector Name"></asp:Label>
                    <br />
                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
                    <br />
                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label5" runat="server" Text="Designation"></asp:Label>
                    <br />
                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:TextBox ID="txt_desig" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <br />
                </td>
                <td colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc; ">

                    <asp:Button ID="Button1" runat="server" Text="Add" width="177px" OnClick="Button1_Click" />
                    <br />
                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">&nbsp;</td>
            </tr>
        </table>
        





    </asp:Panel>
    <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 576px; background-color: #cccccc">Inspector Master
                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="621px" CssClass="gridheader" 
                        OnRowDataBound ="GridView1_RowDataBound" DataKeyNames="Transport_ID" 
                        EnableModelValidation="True">

                    <Columns>
            
            <asp:BoundField DataField="Inspector_Name" HeaderText="Inspector Name" SortExpression="Inspector_Name" >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                        <asp:BoundField DataField="Inspector_desig" HeaderText="Inspector Designation" >
                <HeaderStyle Font-Names="Arial" Font-Size="12px" />
            </asp:BoundField>
                        </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
