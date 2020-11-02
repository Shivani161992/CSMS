<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Dwarpraday_prabhari.aspx.cs" Inherits="District_DPY_ControllerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript">
     function CheckIsnondecimal(tx) {
         var AsciiCode = event.keyCode;
         var txt = tx.value;
         var txt2 = String.fromCharCode(AsciiCode);
         var txt3 = txt2 * 1;
         if ((AsciiCode < 47) || (AsciiCode > 57)) {
             alert('Please enter only numbers.');
             event.cancelBubble = true;
             event.returnValue = false;
         }
     }

</script>

   <center>
    <table style="width: 698px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" 
           border="1" cellpadding="1">
        <tr>
            <td colspan="2" bgcolor="#FFFF66"><center>
                <span style="font-size: 14pt; color: #3300ff">
                <strong>द्वारप्रदाय प्रभारी की प्रविष्टि </strong></center></span></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right; font-size: medium;">
                प्रदाय केंद्र :</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px"  AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
         <tr>
            <td style="width: 225px; text-align: right">
                FPS ब्लॉक :</td>
            <td style="width: 230px; text-align: left">
                <asp:DropDownList ID="ddlBlock" runat="server" Width="194px"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right;">
                द्वारप्रदाय प्रभारी :</td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:TextBox ID="txtContName" runat="server" Width="221px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right;">
                पता :</td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine"
                    Width="221px" Height="26px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 225px; text-align: right; height: 1px;">
                मोबाइल नंबर :</td>
            <td style="width: 230px; text-align: left; height: 1px;">
                <asp:TextBox ID="txtmobile" runat="server" Width="186px" >
                </asp:TextBox>
        
                </td>
        </tr>
      
       
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 225px">
                <asp:Button ID="btnClose" runat="server" Text="New" Width="126px" 
                    OnClick="btnClose_Click" /></td>
            <td style="width: 214px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="129px" 
                    OnClick="btnSave_Click" Height="26px" />
                <asp:Button ID="btn_update" runat="server" onclick="btn_update_Click" 
                    Text="Update" Visible="False" Width="90px" />
            </td>
        </tr>
        <centre><tr>
            <td colspan="2" align="center">
                <asp:GridView ID="grd_prabhari" runat="server" BackColor="#DEBA84" 
                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    CellSpacing="2" EnableModelValidation="True" AutoGenerateColumns="False" 
                    onselectedindexchanged="grd_prabhari_SelectedIndexChanged" 
                    DataKeyNames="FPSBlock,IssueCenter">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="DepotName" HeaderText="प्रदाय केंद्र" />
                        <asp:BoundField DataField="DPYCName" HeaderText="प्रभारी का नाम" />
                        <asp:BoundField DataField="ContactNumber" HeaderText="मोबाइल नंबर" />
                        <asp:BoundField DataField="Address" HeaderText="पता" />
                        <asp:BoundField DataField="FPSBlock" HeaderText="FPS ब्लॉक" Visible="False" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr></centre>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
    </center>
</asp:Content>

