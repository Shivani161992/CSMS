<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="basedepo_toIC.aspx.cs" Inherits="District_basedepo_toIC" %>

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
    <table style="width: 600px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="2" style="font-family: Georgia; font-size: large">
                <center style="background-color: #99CCFF">
                
                <strong>Distance from District/Base Depo&nbsp;to IC</strong></center></td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
                जिला /बेस डिपो</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:Label ID="lbl_dist" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
                सम्बद्ध जिला</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddl_relatedDistrict" runat="server" Width="194px" 
                    AutoPostBack="True" onselectedindexchanged="ddl_relatedDistrict_SelectedIndexChanged" 
                 >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
               सम्बद्ध प्रदाय केंद्र</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px" >
                    
                    
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
                बेस डिपो से दूरी</td>
            <td style="width: 230px; text-align: left">
                <asp:TextBox ID="txt_distance" runat="server"></asp:TextBox>
                in KM</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 341px">
                <asp:Button ID="btnClose" runat="server" Text="New" Width="126px" 
                    OnClick="btnClose_Click" /></td>
            <td style="width: 214px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="129px" OnClick="btnSave_Click" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="grd_distance" runat="server" BackColor="#DEBA84" 
                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    CellSpacing="2" EnableModelValidation="True" AutoGenerateColumns="False" 
                  
                    >
                    <Columns>
                        <asp:BoundField DataField="Dist_name" HeaderText="From District" />
                        <asp:BoundField DataField="DepotName" HeaderText="Issue Center" />
                        <asp:BoundField DataField="fps_distance" HeaderText="Distance" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ListBox ID="ddl_fps_name" runat="server" Visible="False" Width="192px">
                </asp:ListBox>
                <asp:HiddenField ID="hd_fps" runat="server" />
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

