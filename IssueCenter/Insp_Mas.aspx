<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Insp_Mas.aspx.cs" Inherits="IssueCenter_Insp_Mas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <p>
        &nbsp;&nbsp;</p>

    
        <table style="width: 100%; border-style: solid; border-width: 2px; background-color: #FFFF99">
            <tr>
                <td colspan="4"style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                     <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#004000" Text="Label" style="text-align: center">

                     </asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label4" runat="server" Text="Inspector Name"> 

                    </asp:Label>

                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>&nbsp;

                </td>

                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label5" runat="server" Text="Designation"></asp:Label>&nbsp;</td>

                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:TextBox ID="txt_desig" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label1" runat="server" Text="District"> 

                    </asp:Label>

                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:DropDownList ID="Ddldist" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="Ddldist_SelectedIndexChanged" >
                    </asp:DropDownList>
                    &nbsp;

                </td>

                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label2" runat="server" Text="Issue Center"></asp:Label>&nbsp;</td>

                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:DropDownList ID="ddlIC" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>










            <tr>
                <td>&nbsp;</td>
                <td colspan="2">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add" width="177px"/>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
       <br />
    
     <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 1046px; background-color: #cccccc"> Inspector Master
                <br />

                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="621px" CssClass="gridheader" 
                         
                        EnableModelValidation="True">

                    <Columns>
            
            <asp:BoundField DataField="Inspector_Name" HeaderText="Inspector Name" SortExpression="Inspector_Name" >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                        <asp:BoundField DataField="Inspector_desig" HeaderText="Inspector Designation" >
                <HeaderStyle Font-Names="Arial" Font-Size="12px" />
            </asp:BoundField>

                        <asp:BoundField DataField="IssueCenter_code" HeaderText="Issue Center" >
                <HeaderStyle Font-Names="Arial" Font-Size="12px" />
            </asp:BoundField>
                        </Columns>
                </asp:GridView>




            </td>
        </tr>
    </table>


</asp:Content>

