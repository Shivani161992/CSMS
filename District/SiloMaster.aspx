<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="SiloMaster.aspx.cs" Inherits="District_SiloMaster" Title="Silo Master Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
  <script type="text/javascript">
function CheckIsnondecimal(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 47) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>
  
  <script type="text/javascript">
      function CheckCalDate(tx) {
          var AsciiCode = event.keyCode;
          var txt = tx.value;
          var txt2 = String.fromCharCode(AsciiCode);
          var txt3 = txt2 * 1;
          if ((AsciiCode > 0)) {
              alert('Please Click on Calander Controll to Enter Date');
              event.cancelBubble = true;
              event.returnValue = false;
          }
      }
</script>
  
  <script type="text/javascript">
      function CheckIsNumeric(tx) {
          var AsciiCode = event.keyCode;
          var txt = tx.value;
          var txt2 = String.fromCharCode(AsciiCode);
          var txt3 = txt2 * 1;
          if ((AsciiCode < 46) || (AsciiCode > 57)) {
              alert('Please enter only numbers.');
              event.cancelBubble = true;
              event.returnValue = false;
          }

          var num = tx.value;
          var len = num.length;
          var indx = -1;
          indx = num.indexOf('.');
          if (indx != -1) {
              var coda = event.keyCode;
              if (coda == 46) {
                  alert('Decimal cannot come twice');
                  event.cancelBubble = true;
                  event.returnValue = false;
              }
              var dgt = num.substr(indx, len);
              var count = dgt.length;
              //alert (count);
              if (count > 5) {
                  alert("Only 5 decimal digits allowed");
                  event.cancelBubble = true;
                  event.returnValue = false;
              }
          }

      }



    </script>
   
  <table style="width: 620px" >
        <tr>
            <td colspan="2" style="color: #000099">
                <b>Entry of Silo Master</b></td>
        </tr>
        <tr>
            <td>
                Firm Name</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtfirmname" runat="server" Width="200px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td>
                License No</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtlicenseno" runat="server" Width="200px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td>
                License Validity</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtlicensevalid" runat="server" Width="200px"></asp:TextBox>
                
                 <script type  ="text/javascript">
                     new tcal({
                         'formname': '0',
                         'controlname': 'ctl00_ContentPlaceHolder1_txtlicensevalid'
                     });
	                          </script>
                                            </td>
        </tr>
        <tr>
            <td>
                Address / Location</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtlocation" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td>
                Owner/Authorised Signatory</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtowner" runat="server" style="height: 22px" Width="200px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td>
                Mobile Number</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtmobile" runat="server" Width="200px" MaxLength="10" ></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td>
                Capacity in M.T.</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtcapacity" runat="server" Width="200px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td>
                Sactioned Rate per Quintal</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtrate" runat="server" Width="200px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td>
                Type of Silo</td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddltype" runat="server" Height="17px" Width="140px">
                    <asp:ListItem Value="S">Steel SIlo</asp:ListItem>
                    <asp:ListItem Value="B">Silo Bags</asp:ListItem>
                </asp:DropDownList>
                                            </td>
        </tr>
        <tr>
            <td colspan="2" style="color: #663300">
                जो खरीद केंद्र सम्बन्ध है उसे टिक (सेलेक्ट) करें</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None"
                    Width="500px" >
                    
                     <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server" Width = "30px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Society_Id" HeaderText="Society Id" SortExpression="Society_Id" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="100px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Society_Name" HeaderText="Society Name" SortExpression="Society_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="450px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                
                </Columns>
                    
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
                
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            <asp:Button ID="btnclose" runat="server" Text="Close" Width="65px" OnClick="btnclose_Click" /></td>
            <td>
                   
                <asp:Button ID="btnsave" runat="server" Text="Save" Width="128px" onclick="btnsave_Click" />
                   
                </td>
        </tr>
        <tr>
            <td colspan="2" style="color: #000099">
                <span style="color: #800000; font-weight: bold">पूर्व में प्रविष्टि की गयी जानकरी</span> </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None"
                    Width="100%" >
                    
                     <Columns>
               
                <asp:BoundField DataField="firm_name" HeaderText="Firm" SortExpression="firm_name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="100px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="licenseNum" HeaderText="license Num" SortExpression="licenseNum" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="450px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" SortExpression="ContactNumber" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="450px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="Society_Name" HeaderText="Society Name" SortExpression="Society_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="450px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="Capacity" HeaderText="Capacity" SortExpression="Capacity" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="450px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                
                </Columns>
                    
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
                
                </td>
        </tr>
    </table>
</asp:Content>

