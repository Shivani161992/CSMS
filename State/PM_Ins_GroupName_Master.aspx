<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PM_Ins_GroupName_Master.aspx.cs" Inherits="State_PM_Ins_GroupName_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table id="table1" runat="server" style="width: 100%; border-style: solid; border-width: 2px; background-color:azure">
        <tr>
            <td colspan="2" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600; border-style: none">
                <strong>
                    Group Name Master
                </strong>

            </td>



        </tr>
          
        <tr>
                <td><strong>Crop Year</strong></td>
                <td>
                    <br />
                   <asp:DropDownList ID="ddlCropYear" runat="server" Width="150" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    </td>
             </tr>
         <tr>
                <td><strong>Season</strong></td>
                <td>
                    <br />
                   <asp:DropDownList ID="ddlseason" runat="server" Width="150" AutoPostBack="True" >
                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                        <asp:ListItem Text="Rabi" Value="Rabi"></asp:ListItem>

                        <asp:ListItem Text="Kharif" Value="Kharif"></asp:ListItem>

                    </asp:DropDownList>
                    <br />
                    </td>
             </tr>
            
         <tr>
                <td><strong>Group Name</strong></td>
                <td >
                    <br />
                    <asp:TextBox ID="txtGName" runat="server"  Width="137px"></asp:TextBox>
                    <br />
                    <br />
                    </td>
             </tr>
         <tr>
               
                <td colspan="2">
                    <br />
                    <asp:Button ID="Bttadd" runat="server" OnClick="Button1_Click" Text="Add" width="177px"/>
                    <br />
                </td>
                
            </tr>
        </table>
     <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 1046px; background-color: #cccccc"> Department Name Master
                <br />

                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
                       CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="621px" CssClass="gridheader" 
                         
                        EnableModelValidation="True">

                    <Columns>
            
            <asp:BoundField DataField="Season" HeaderText="Commodity" >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                         
            <asp:BoundField DataField="TeamName" HeaderText="Group Name"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                        
                       
                       
                        </Columns>
                </asp:GridView>




            </td>
        </tr>
    </table>
</asp:Content>

