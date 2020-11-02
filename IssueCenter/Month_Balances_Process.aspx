<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Month_Balances_Process.aspx.cs" Inherits="Month_Balances_Process" Title="Month Balances Process" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> Month Balance Process</div>
<div id ="ronewmargin">
<center>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 569px;">
     <tr>
         <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 102px; border-bottom: 1px solid; height: 25px; background-color: white">
             <p class="MsoNormal" style="margin: 0in 0in 0pt">
                 Previous Month</p>
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; height: 25px; background-color: white">
            <asp:DropDownList ID="ddlopeningmonth" runat="server"  Width ="161px">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">August</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; height: 25px; background-color: white; width: 44px;">
             Year</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; height: 25px; background-color: white">
             <asp:DropDownList ID="ddlprevyear" runat="server">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 102px; border-bottom: 1px solid; height: 25px; background-color: white">
             Next Month</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; height: 25px; background-color: white">
              <asp:DropDownList ID="ddlnextmonth" runat="server"  Width ="161px">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">August</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; height: 25px; background-color: white; width: 44px;">
             Year</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; height: 25px; background-color: white">
             <asp:DropDownList ID="ddlnextyear" runat="server">
             </asp:DropDownList></td>
     </tr>
                                
                       
                                   
                             
   
       <tr>
       <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 102px;" > </td>
       <td colspan ="1" align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;"><asp:Button ID="btnsubmit" runat="server" Text="Save " OnClick="btnsubmit_Click" ValidationGroup="1" Width="75px" /> 
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="74px" /> </td>
           <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
               border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 44px;">
           </td>
           <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
               border-left: 1px solid; border-bottom: 1px solid; background-color: white">
           </td>
       </tr>
     <tr>
         <td align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white;">
            
         </td>
         <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 44px;">
         </td>
         <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
     <tr>
         <td align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label></td>
         <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 44px;">
         </td>
         <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="2" rowspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white;"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
       </td>
           <td align="left" colspan="1" rowspan="2" style="border-right: 1px solid; border-top: 1px solid;
               border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 44px;">
           </td>
           <td align="left" colspan="1" rowspan="2" style="border-right: 1px solid; border-top: 1px solid;
               border-left: 1px solid; border-bottom: 1px solid; background-color: white">
           </td>
       </tr>
 </table>
 <div>
 <table cellpadding ="0" cellspacing ="0" border ="0" >
  
  <tr>
  <td>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="6" PagerSettings-Visible ="True" ShowFooter = "True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnPageIndexChanged="GridView1_PageIndexChanged" Width="568px"  >
                 <Columns>
                     <asp:BoundField DataField="Source_Name" HeaderText="Source Of Arrival" SortExpression="Source_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Quantity" HeaderText="Opening Balance" SortExpression="Quantity" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Current_Balance" HeaderText="Current Balance" SortExpression="Current_Balance">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Month" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="Tan" />
                 <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                 <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                 <HeaderStyle BackColor="Tan" Font-Bold="True" />
                 <AlternatingRowStyle BackColor="PaleGoldenrod" />
             </asp:GridView>
  
  </td> 
  </tr> 
  </table>
 </div>
  <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Firstbutton_Click"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Prevbutton_Click" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Nextbutton_Click" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Lastbutton_Click" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
</center>
</div>

<script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>
</asp:Content>

