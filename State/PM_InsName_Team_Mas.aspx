<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PM_InsName_Team_Mas.aspx.cs" Inherits="State_PM_InsName_Team_Mas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table id="table1" runat="server" style="width: 100%; border-style: solid; border-width: 2px; background-color: #FFFF99">
          
            
        <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label4" runat="server" Text="Inspector Name"> </asp:Label>

                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:TextBox ID="txt_Inspname" runat="server"></asp:TextBox>&nbsp;

                </td>
            </tr>
         <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label1" runat="server" Text="Department Name"> </asp:Label>

                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                   <asp:DropDownList ID="Ddldepart" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="Ddldepart_SelectedIndexChanged" >
                    </asp:DropDownList>

                </td>
            </tr>
        <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label2" runat="server" Text="Post Held"> </asp:Label>

                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:TextBox ID="txt_PH" runat="server"></asp:TextBox>&nbsp;

                </td>
            </tr>
         <tr>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:Label ID="Label5" runat="server" Text="Posting Place"> </asp:Label>

                </td>
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <asp:DropDownList ID="Ddldist" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="Ddldist_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
            </tr>

         <tr>
               
                <td colspan="2">
                    <asp:Button ID="Bttadd" runat="server" OnClick="Button1_Click" Text="Add" width="177px"/>
                </td>
                
            </tr>
          <tr>
               
                <td colspan="2">
                    <asp:Button ID="Bttupdate" runat="server" OnClick="Button2_Click" Text="Update" width="177px"/>
                </td>

                
            </tr>
       
        </table>
     <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 1046px; background-color: #cccccc"> Department Name Master
                <br />

                <asp:GridView ID="GridView1" style=" margin-right: 10px;" runat="server"  AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="616px" CssClass="gridheader" 
                         
                        EnableModelValidation="True" Height="214px">

                    <Columns>
                          <asp:CommandField HeaderText="Action" ShowSelectButton="True" />

                         <asp:BoundField DataField="Inspector_Name" HeaderText="Inspector Name"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>
            
            <asp:BoundField DataField="Department_Name" HeaderText="Department Name"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>
                        <asp:BoundField DataField="Depart_ID" HeaderText="Department_code"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>
                         <asp:BoundField DataField="PostHeld" HeaderText="Post Held"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                        
                 <asp:BoundField DataField="district_name" HeaderText="Posting Place"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                        <asp:BoundField DataField="Posting_Place" HeaderText="District_Code"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                       
                        </Columns>
                </asp:GridView>




            </td>
        </tr>
    </table>
     <table style="width: 100%; border-style: solid; border-width: 2px; background-color: #FFFF99">
            <tr>
                <td colspan="4"style="font-size: 10pt; position: static; background-color:beige; width: 187px; ">
                     
                    <asp:Button ID="Bttadnew" runat="server" OnClick="Button3_Click" Text="Add New" width="177px"/>
                    
                    
                    &nbsp;</td>
                </tr>
         </table>




</asp:Content>

