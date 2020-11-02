<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PM_Depart_Master.aspx.cs" Inherits="State_PM_Depart_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 100%; border-style: solid; border-width: 2px; background-color: #FFFF99">
            <tr>
                <td colspan="4"style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                     <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#004000" Text="Label" style="text-align: center"> </asp:Label>&nbsp;</td>
                </tr>
        <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label4" runat="server" Text="Department Name"> </asp:Label>

                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:TextBox ID="txt_Departname" runat="server"></asp:TextBox>&nbsp;

                </td>
            </tr>
         <tr>
               
                <td colspan="2">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add" width="177px"/>
                </td>
                
            </tr>
        </table>
     <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 1046px; background-color: #cccccc"> Department Name Master
                <br />

                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="621px" CssClass="gridheader" 
                         
                        EnableModelValidation="True">

                    <Columns>
            
            <asp:BoundField DataField="Department_Name" HeaderText="Department Name" SortExpression="Department_Name" >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                        
                       
                        </Columns>
                </asp:GridView>




            </td>
        </tr>
    </table>

</asp:Content>

