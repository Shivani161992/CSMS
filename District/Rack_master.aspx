<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Rack_master.aspx.cs" Inherits="DistrictFood_Rack_master" Title="Rack Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script type="text/javascript">
function CheckCalDate(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>
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
    <script language="javascript" type="text/javascript">
function GetDialog() 
{
if (confirm("सुरक्षित करने से पहले जांच ले ,संसोधन संभव नहीं हो सकेगा"))
 {
 return true;

} 
else 
{
return false;
}

}
</script>
<div id="transCP" style="width: 670px; height: 293px;" >

<div id="transRail"> 

 
 </div> 
 <center style="text-align: left">
     <table style="width: 670px; height: 200px" >
         <tr valign = "top">
             <td >
             
             <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 670px; text-align: left;" >
     <tr>
         <td colspan="4" >
             <span style="font-size: 10pt; color: #cc0033; font-family: Verdana;">रैक नंबर बनाते समय भेजे जाने वाले
                 जिले एवं दिनांक का विशेष ध्यान रखे , संसोधन संभव नहीं हो सकेगा</span></td>
     </tr>
     <tr>
         <td  colspan="4">
             <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000" Font-Size="10pt" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" >
             Sending District</td>
         <td class="tdmarginro" >
         <asp:TextBox ID="txtdistrict" runat="server" Width="128px"></asp:TextBox></td>
         <td class="tdmarginro" >
             Sending Rail Head</td>
         <td class="tdmarginro" >
         <asp:DropDownList ID="ddlsengrailhead" runat="server"  Width ="164px" OnSelectedIndexChanged="ddlsengrailhead_SelectedIndexChanged">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" >
             Dispatch Type</td>
         <td class="tdmarginro" >
             <asp:DropDownList ID="ddldispatchtype" runat="server"  Width ="137px" AutoPostBack="True" OnSelectedIndexChanged="ddldispatchtype_SelectedIndexChanged" ondatabound="ddldispatchtype_DataBound">
                 <asp:ListItem>Own District</asp:ListItem>
                 <asp:ListItem>OMSS</asp:ListItem>
                 <asp:ListItem>Export</asp:ListItem>
             </asp:DropDownList>
             </td>
         <td class="tdmarginro" >
         </td>
         <td class="tdmarginro" >
         </td>
     </tr>
   <tr>
   <td class ="tdmarginro" > 
   <asp:Label ID = "lblstate" runat = "server" Text = "Select State" Height="18px" Width="106px" Visible="False"></asp:Label>
   </td>
     <td class ="tdmarginro" > <asp:DropDownList ID="ddlStates" runat="server" Width="138px" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged" AutoPostBack="True" Visible="False">
         </asp:DropDownList></td>
       <td class ="tdmarginro" >
           Destination District</td>
         <td class ="tdmarginro" > <asp:DropDownList ID="ddldesdistt" runat="server"  Width ="167px" AutoPostBack="True" OnSelectedIndexChanged="ddldesdistt_SelectedIndexChanged">
         </asp:DropDownList></td>
   </tr>
    <tr>
   <td class ="tdmarginro"  >
    <asp:Label ID = "lblrailhead" runat = "server" Text = "Rail Head Name" Width="110px" Height="19px" Visible="False"></asp:Label>
   </td>
     <td class ="tdmarginro" >   <asp:TextBox ID="txtrailhead" runat="server" Width="160px" Height="16px" Visible="False" MaxLength="25"></asp:TextBox></td>
       <td class ="tdmarginro" > 
       <asp:Label ID = "lblsenddist" runat = "server" Text = "Destination Rail Head" Width="142px" Height="22px"></asp:Label>
       </td>
         <td class ="tdmarginro" > <asp:DropDownList ID="ddldesrailhead" runat="server"  Width ="168px" Height="24px">
         <asp:ListItem Text ="--Select--" Value ="01" Selected ="True" ></asp:ListItem>
         </asp:DropDownList></td>
   </tr>
     <tr>
         <td class="tdmarginro" >
             Commodity</td>
         <td class="tdmarginro" >
             <asp:DropDownList ID="ddlcommodity" runat="server"  Width ="135px">
             </asp:DropDownList></td>
         <td class="tdmarginro" >
             Select Scheme</td>
         <td  >
         <asp:DropDownList ID="ddl_scheme" runat="server"  Width ="165px"> </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" >
             Rake Dispatch Date</td>
         <td class="tdmarginro" >
         
         <asp:TextBox ID="DaintyDate3" runat="server" Width="130px"></asp:TextBox>
             <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	                      });
	          </script>
         
             </td>
         <td class="tdmarginro" >
         </td>
         <td >
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" >
             </td>
         <td class="tdmarginro" >
         <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="95px" OnClientClick="return GetDialog()" OnClick="btnSubmit_Click" /></td>
         <td class="tdmarginro" >
         <asp:Button ID="btnClose" runat="server" Text="Close" Width="73px"  OnClick="btnClose_Click" /></td>
         <td class="tdmarginro" >
             </td>
     </tr>
     <tr>
         <td colspan="4">
             <asp:Label ID="Label1" runat="server" Width="381px"></asp:Label></td>
     </tr>
 </table>
 <table style="width: 670px; ">
 <tr>
 <td >
 <asp:Label ID = "lblgrid" runat = "server" Text = "जहाँ  Dest. District खाली दिख रहा है , उसे अन्य राज्य में भेजा गया है." Font-Bold="True" ForeColor="Blue" Width="484px"></asp:Label>
 
 </td>
 
 </tr>
<tr>
<td valign = "top" >
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
           PageSize ="5"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Width="666px">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                <HeaderStyle Font-Size="10px" />
                <ItemStyle Font-Size="10px" />
            </asp:CommandField>
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No." SortExpression="Rack_No">
                <ItemStyle Font-Size="10px" />
                <HeaderStyle Font-Size="10px" />
            </asp:BoundField>
            <asp:BoundField DataField="DestRName" HeaderText="Rail Head" SortExpression="DestRName">
                <ItemStyle Font-Size="10px" />
                <HeaderStyle Font-Size="10px" />
            </asp:BoundField>
            <asp:BoundField DataField="district_name" HeaderText="Dest. District" SortExpression="district_name">
                <ItemStyle Font-Size="10px" />
                <HeaderStyle Font-Size="10px" />
            </asp:BoundField>
            <asp:BoundField DataField="RailHead_Name" HeaderText="Dest. RailHead" SortExpression="RailHead_Name">
                <ItemStyle Font-Size="10px" />
                <HeaderStyle Font-Size="10px" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                <ItemStyle Font-Size="10px" />
                <HeaderStyle Font-Size="10px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Dispatch Date">
     
                <ItemTemplate>
                <asp:Label ID="lbldisdate" runat="server" 
                Text='<%# Eval("Rack_DispDate").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle Font-Size="8pt" />
                <ItemStyle Font-Size="7pt" />
                 </asp:TemplateField>
        </Columns>
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White"  />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" CssClass="gridheader" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
</td>
</tr>
</table>  
  </td>
         </tr>
     </table>
 
</center>
 <%-- <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter" style="width: 565px">
        <tr>
                        <td style="width: 117px">
                            <div style="text-align: left" >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" Width="6px"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" Width="6px" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" Width="34px" />&nbsp;&nbsp;</div>
                        </td>
                    </tr>
        </table>--%>
   
  
 </div> 

</asp:Content>

