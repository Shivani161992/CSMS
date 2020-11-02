<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PM_CMR_DO_Godown_Update.aspx.cs" Inherits="State_PM_CMR_DO_Godown_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" style="width: 800px; border-style: solid; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
     <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Update CMR Deposit Order</strong>
                <input id="hdfAgrmtDist" type="hidden" runat="server" />
                <input id="hdfDistance" type="hidden" runat="server" />
                <input id="hdfAllQty" type="hidden" runat="server" />
            </td>
        </tr>    
        <tr>
            <td rowspan="12">&nbsp;</td>

            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="12"></td>
        </tr>
     <tr>
          

          <td style="width:200px;">Crop Year</td>
            <td>
                 <asp:DropDownList ID="ddlCropYear" runat="server" Width="191px" AutoPostBack="True"  >
                </asp:DropDownList>
               
            </td>
           <td style="width: 402px"> Paddy Agreement District</td>
            <td>
                

              <asp:DropDownList ID="Ddldist" runat="server" Width="191px" AutoPostBack="True" OnSelectedIndexChanged="Ddldist_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>

     </tr>
    

         <tr>
            <td colspan="2">मिल का नाम</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
             </tr>
        <tr>
            <td colspan="2">अनुबंध नंबर</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Height="27px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAgtmtNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="2">CMR Deposit Order No.</td>
            
            <td colspan="2">
                 <asp:DropDownList ID="ddlCMR_DO_No" runat="server" Height="27px" Width="250px" OnSelectedIndexChanged="ddlCMR_DO_No_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Lot No.</td>
            <td>
                <asp:TextBox ID="txt_LotNo" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>


            </td>
            <td style="width: 402px">CMR जमा करने का दिनांक</td>
            <td>
                <asp:TextBox ID="txt_CMR_Date" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>


            </td>
            </tr>
        <tr>
            <td>CMR District</td>
            <td>
                <asp:TextBox ID="txtdist" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>


            </td>
            <td style="width: 402px">CMR Issue Center</td>
            <td>
                <asp:TextBox ID="txtIC" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>


            </td>
            </tr>
        <tr>
            <td>CMR Branch</td>
            <td>
                <asp:TextBox ID="txtbranch" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>


            </td>
            <td style="width: 402px">CMR Godown</td>
            <td>
                <asp:TextBox ID="txtGD" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>


            </td>
            </tr>
         <tr>
            <td colspan="4" style="background-color: #CCFF99; color:red; text-align:center;"> Change Destination</td>
        </tr>
         <tr>
            <td>CMR District</td>
            <td>
<asp:DropDownList ID="ddl_CMRDist" runat="server" Width="191px" AutoPostBack="True" OnSelectedIndexChanged="ddl_CMRDist_SelectedIndexChanged" >
                </asp:DropDownList>

            </td>

              <td style="width: 402px">Issue Center</td>
            <td>
<asp:DropDownList ID="ddl_IC" runat="server" Width="191px" AutoPostBack="True" OnSelectedIndexChanged="ddl_IC_SelectedIndexChanged" >
                </asp:DropDownList>

            </td>

             </tr>

        <tr>
             <td>Branch</td>
            <td>
<asp:DropDownList ID="ddlBranch" runat="server" Width="191px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" >
                </asp:DropDownList>

            </td>

             <td style="width: 402px">Godown</td>
            <td>
                <asp:DropDownList ID="ddlGodam" runat="server" Width="191px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodam_SelectedIndexChanged" >
                </asp:DropDownList>


            </td>
            
            </tr>
        
             <tr>
            <td colspan="6" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="btnRecptSubmit_Click1" />

                

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>


        
    
    </table>
</asp:Content>

