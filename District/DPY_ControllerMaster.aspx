<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DPY_ControllerMaster.aspx.cs" Inherits="District_DPY_ControllerMaster" %>

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
    <table style="width: 698px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="2" bgcolor="#FFFF66">
               
                DPY Controller Master</td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 167px; height: 1px; text-align: center">
                Issue Center</td>
            <td style="width: 283px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px"  AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
         <tr>
            <td style="width: 167px; text-align: center">
                Select FPS Block</td>
            <td style="width: 283px; text-align: left">
                <asp:DropDownList ID="ddlBlock" runat="server" Width="194px"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 167px; height: 1px; text-align: center;">
                DPY Controller Name</td>
            <td style="width: 283px; height: 1px; text-align: left;">
                <asp:TextBox ID="txtContName" runat="server" Width="221px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 167px; height: 1px; text-align: center;">
                Controller Address</td>
            <td style="width: 283px; height: 1px; text-align: left;">
                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine"
                    Width="221px" Height="26px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 167px; text-align: center; height: 1px;">
                Mobile Number</td>
            <td style="width: 283px; text-align: left; height: 1px;">
                <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" Width="137px" ></asp:TextBox>&nbsp;
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
            <td style="width: 167px">
                <asp:Button ID="btnClose" runat="server" Text="New" Width="126px" 
                    OnClick="btnClose_Click" /></td>
            <td style="width: 283px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="129px" OnClick="btnSave_Click" /></td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
    </center>
</asp:Content>

