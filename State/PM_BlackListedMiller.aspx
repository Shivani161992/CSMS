<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PM_BlackListedMiller.aspx.cs" Inherits="State_PM_BlackListedMiller" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table id="table1" runat="server" style="width: 100%; border-style: solid; border-width: 2px; background-color:#F2D7D5  ">
        <tr>
            <td colspan="2" style="text-align: center; font-size: large; text-decoration: underline;background-color:#FBFCFC  ; color: #CC6600;  border-style: none">
                <strong>
                    BlackListed Millers
                </strong>

            </td>



        </tr>
          
        <tr>
                <td style="height:50px "><strong>Crop Year</strong></td>
                <td style="height:50px">
                    <br />
                   <asp:DropDownList ID="ddlCropYear" runat="server" Width="150" AutoPostBack="True">
                    </asp:DropDownList>
                    </td>
             </tr>
            
         
         
        <tr>
                <td style="height:50px">
                    <strong>District</strong>

                </td>
                <td style="height:50px">
                    <asp:DropDownList ID="ddldist" runat="server" Width="151px"  AutoPostBack="True" OnSelectedIndexChanged="Ddldist_SelectedIndexChanged" Height="34px" >
                    </asp:DropDownList>
                </td>
            </tr>
         <tr>
                <td style="height:50px" >
                    <strong>Miller's Name</strong>

                </td>
                <td style="height:50px" >
                    <asp:DropDownList ID="ddlMN" runat="server" Height="27px" Width="212px" AutoPostBack="True" OnSelectedIndexChanged="ddlMN_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
            </tr>


        <tr>
                <td style="height:50px" >
                    <strong>Black Listed</strong>

                </td>
                <td  style="height:50px">
                    <asp:DropDownList ID="ddlBL" runat="server" Width="150" AutoPostBack="True" >
                        <asp:ListItem Text="--Select--" Value="2" Selected="True"></asp:ListItem>

                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>

                        <asp:ListItem Text="NO" Value="N"></asp:ListItem>

                        
                    </asp:DropDownList>
                </td>
            </tr>
         <tr>

             <td colspan="2">
                 <asp:Button ID="Bttadd" runat="server" OnClick="Bttadd_click" Text="Add" width="177px"/>
             </td>
         </tr>
        
         </table>
     <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 1200px; background-color: white"> Black Listed Millers
                <br />

               <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                   >
                    <AlternatingRowStyle BackColor="White" />

                    <Columns>
                          
           
            
                        
                 <asp:BoundField DataField="district_name" HeaderText="District"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                       
                        <asp:BoundField DataField="Mill_Name" HeaderText="Miller"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>
                       
                       
                        </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#E74C3C" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>




            </td>
        </tr>
    </table>
</asp:Content>

