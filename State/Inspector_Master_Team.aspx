<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Inspector_Master_Team.aspx.cs" Inherits="State_Inspector_Master_Team" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <table style="width: 100%; border-style: solid; border-width: 2px; background-color: #FFFF99">

        <tr>
                <td colspan="3"style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                   
                     <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#004000" Text="Creation of Inspection Group" style="text-align: center">

                     </asp:Label>&nbsp;</td>
            
            </tr>

        <tr>
                <td style="font-size: 13pt; position: static; background-color: #cfdcdc; width: 204px;">
                    <asp:Label ID="cropyr" runat="server" Text="Crop Year" Font-Bold="true"> 

                    </asp:Label>

                </td>
       
        
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <br />
                    <asp:DropDownList ID="ddlCropYear" runat="server" Height="32px" Width="128px"></asp:DropDownList>&nbsp;

                    <br />

                    <br />

                </td>
            
            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 175px;"></td>
        </tr>

         <tr>
                <td style="font-size: 13pt; position: static; background-color: #cfdcdc; width: 204px;">
                    <asp:Label ID="commlbl" runat="server" Text="Season" Font-Bold="true"> 

                    </asp:Label>

                </td>
       
        
                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <br />
                    <asp:DropDownList ID="commo_ddl" runat="server" Height="32px" OnSelectedIndexChanged="commo_ddl_SelectedIndexChanged" AutoPostBack="true" Width="128px">
                        <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Rabi" Value="Rabi" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="Kharif" Value="Kharif" Selected="false"></asp:ListItem>
                    </asp:DropDownList>&nbsp;

                    <br />

                    <br />

                </td>
            
            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 175px;"></td>
        </tr>
        <tr>
                <td style="font-size: 13pt; position: static; background-color: #cfdcdc; width: 204px;">
                    <asp:Label ID="grp_name" runat="server" Text="Team Name" Font-Bold="true"></asp:Label>&nbsp;</td>

                <td style="font-size: 13pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <br />
                    <asp:DropDownList ID="grp_name_ddl" runat="server" Height="28px" OnSelectedIndexChanged="grp_name_ddl_SelectedIndexChanged" AutoPostBack="true" Width="128px"></asp:DropDownList>
                    <br />
                    <br />
                </td>
            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 175px;">
        </td>
            </tr>
        
           
            

        <tr>
                <td style="font-size: 13pt; position: static; background-color: #cfdcdc; width: 204px;">
                    <asp:Label ID="Inspctr_Name_lbl" runat="server" Text="Inspector Name" Font-Bold="true"></asp:Label>&nbsp;</td>

                <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                    <br />
                    <asp:DropDownList ID="Inspctr_name_ddl" runat="server"  Height="21px" Width="128px"></asp:DropDownList>
                    <br />
                    <br />
                </td>
            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 175px;"></td>
            </tr>
        <tr>
                
                <td>
                    <br />
                    <asp:Button ID="add_new_btn" runat="server" OnClick="add_new_btn_Click" Text="Add New" width="177px" Font-Bold="true" Font-Size="Small"/>
                    <br />
                </td>
                <td><br />
                    <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="Submit" width="177px" Font-Bold="true" Font-Size="Small" style="height: 26px"/>
                    <br />

                </td>
            
            </tr>
        
    </table>
    <br />
     <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 1046px; background-color: #cccccc"> Group Formation <br />

                <asp:GridView ID="GridView1" style=" margin-right: 10px;" runat="server"  AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="616px" CssClass="gridheader" 
                         
                        EnableModelValidation="True" Height="214px" >

                    <Columns>
                    

            <asp:BoundField DataField="TeamName" HeaderText="Team Name"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>
                       

            <asp:BoundField DataField="Season" HeaderText="Season"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>

            <asp:BoundField DataField="Inspector_Name" HeaderText="Inspector Name"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>


                         <asp:BoundField DataField="Department_Name" HeaderText="Department Name"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>
                        
            
            <asp:BoundField DataField="PostHeld" HeaderText="Post Held"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>

             <asp:BoundField DataField="district_name" HeaderText="Posting Place"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                </asp:BoundField>
                         
                       
                        </Columns>
                </asp:GridView>

            </td>
        </tr>
    </table>

</asp:Content>

