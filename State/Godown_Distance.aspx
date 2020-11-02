<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Godown_Distance.aspx.cs" Inherits="State_Godown_Distance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <table style="width: 100%; border-style: solid; border-width: 2px; background-color: #FFFF99">

          <tr>
              <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                  <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="false" ForeColor="#004000" Text="District" style="text-align: center">

                     </asp:Label>&nbsp;</td>
             
              <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
 <asp:DropDownList ID="Ddldist" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="Ddldist_SelectedIndexChanged" >
                    </asp:DropDownList>
</td>
 <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
     <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="false" ForeColor="#004000" Text="Distance" style="text-align: center">

                     </asp:Label>&nbsp;</td>

               <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
 <asp:TextBox ID="txt_dist" runat="server"></asp:TextBox>
</td>
              




          </tr>
          <tr>
              <td  style="font-size: 10pt; text-align: center; position: static; background-color: #cfdcdc; width: 187px;">

                  <asp:Button ID="btnRecptSave" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Save" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" Enabled="true" OnClick="btnRecptSave_Click" />
                 

              </td>
              <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
                  
                  <asp:Button ID="btnrecptupdate" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Update" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" Enabled="False" OnClick="btnRecptUpdate_Click" />
              </td>
              <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;"> 
                                    <asp:Button ID="btnrecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" Enabled="False" OnClick="btnRecptNew_Click" />

              </td>
              <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;"> </td>

          </tr>
          
          </table>
     <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
        <tr>
            <td style="width: 1046px; background-color: #cccccc"> Distance Master
                <br />

                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="621px" CssClass="gridheader" 
                         
                        EnableModelValidation="True">

                    <Columns>
             <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
            
                        
                        <asp:BoundField DataField="district_name" HeaderText="District" >
                <HeaderStyle Font-Names="Arial" Font-Size="12px" />
            </asp:BoundField>

                        <asp:BoundField DataField="Max_distance" HeaderText="Distance" >
                <HeaderStyle Font-Names="Arial" Font-Size="12px" />
            </asp:BoundField>

                        <asp:BoundField DataField="district_code" HeaderText="district_code" >
                <HeaderStyle Font-Names="Arial" Font-Size="12px" />
            </asp:BoundField>
                        </Columns>
                </asp:GridView>




            </td>
        </tr>
    </table>




</asp:Content>

