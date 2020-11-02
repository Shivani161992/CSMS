<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Rack_Dispatch_Detail.aspx.cs" Inherits="DistrictFood_Rack_Dispatch_Detail" Title="Rack Dispatch Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
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
var coda = event.keyCode
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
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
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 602px;"  >
     <tr>
         <td class="tdmarginro" style="color: white; background-color: #6699cc; height: 21px;">
         </td>
         <td  colspan="3" style="color: white; background-color: #6699cc; height: 21px;" align="left">
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            
             Rack Dispatch Details</td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="lbldisdate" runat="server" Visible="False"></asp:Label></td>
     </tr>
   <tr>
   <td class ="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;"> Rack No.</td>
     <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle"> <asp:DropDownList ID="ddlrackno" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged">
                            
                        </asp:DropDownList> </td>
       <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
           Rack Qty.</td>
         <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle"> <asp:TextBox ID="txtrackqty" runat="server" Width="146px"></asp:TextBox></td>
   </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="Label6" runat="server" Text="Sending Rail Head " Width="121px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtsrailh" runat="server" Width="146px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="Label7" runat="server" Text="Receiving Rail Head" Width="132px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtrecrailh" runat="server" Width="146px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
     </tr>
    <tr>
   <td class ="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;"> 
               <asp:Label ID="lblsrh" runat="server" Visible="False"></asp:Label></td>
     <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">   &nbsp;<asp:Label ID="lbldrh" runat="server" Visible="False"></asp:Label></td>
       <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle"> 
           <asp:Label ID="lbldesdist" runat="server" Visible="False"></asp:Label></td>
         <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle"> 
             <asp:Label ID="lblcomdty" runat="server" Visible="False"></asp:Label>
         </td>
   </tr>
     <tr>
         <td class="tdmarginro" style="color: white; background-color: #6699cc;">
         </td>
         <td style="background-color: #6699cc; color: white;" colspan="3" align="left">
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Details of
             Qty Recd. At Dispatch Rail Head</td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #99cc99">
             <asp:Label ID="Label4" runat="server" Font-Size="11px" Text="Stock Received From"
                 Width="131px" ForeColor="Purple"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #99cc99" colspan="2">
             <asp:RadioButtonList ID="chk_receive" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                 ValidationGroup="1" Width="283px" OnSelectedIndexChanged="chk_receive_SelectedIndexChanged" BorderColor="#C00000" Font-Size="11px" ForeColor="Purple">
                 <asp:ListItem Value="01">Procurement Center</asp:ListItem>
                 <asp:ListItem Value="02">Issue Center</asp:ListItem>
             </asp:RadioButtonList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #99cc99">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             <asp:Label ID="lbldist" runat="server" Text="Sending District" Width="113px" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"
                 Width="153px" Visible="False">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             <asp:Label ID="lblpc" runat="server" Text="Procurement Center" Width="129px" Font-Size="11px" Visible="False"></asp:Label>
             <asp:Label ID="lblic" runat="server" Text="Issue Cente/Storege Place" Font-Size="11px" Width="153px" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             <asp:DropDownList ID="ddlprocname" runat="server"  Width ="152px" Visible="False">
         </asp:DropDownList>
             <asp:DropDownList ID="ddlissuename" runat="server"  Width ="153px" Visible="False">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="lblchallan" runat="server" Text="Challan No." Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99;">
             <asp:DropDownList ID="ddlchallan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged"
                 Width="145px" Visible="False">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label2" runat="server" Text="TruckChallan No. " Width="111px"></asp:Label>&nbsp;&nbsp;
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:TextBox ID="tctchallanno" runat="server" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             TC Date</td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99" align="left">
            <asp:TextBox ID="DaintyDate3" runat="server" Width="114px"></asp:TextBox>
             <asp:TextBox ID="txtchallandt" runat="server" Visible="False"></asp:TextBox>
             <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	                      });
	          </script>
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             Transporter</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             <asp:DropDownList ID="ddltransporter" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddltransporter_SelectedIndexChanged">
         </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             Truck No.</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             <asp:TextBox ID="txttruckno" runat="server" Width="146px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             Dispatch Bags</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:TextBox ID="txtdisbags" runat="server" Width="146px" MaxLength="6"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             Dispatch Qty.</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:TextBox ID="txtdisqty" runat="server" Width="146px" MaxLength="13"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label5" runat="server" Text="Received Bags" Width="106px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:TextBox ID="txtrecbags" runat="server" Width="146px" MaxLength="6"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             Received Qty.</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:TextBox ID="txtrecqty" runat="server" Width="146px" MaxLength="13"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: #cccc99">
             <asp:Button ID="btnaddmore" runat="server" Text="Add More" Width="95px" OnClick="btnaddmore_Click" /></td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:Label ID="lbltrp" runat="server" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 154px">
         </td>
         <td class="tdmarginro">
         </td>
         <td class="tdmarginro"><asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="95px" OnClick="btnSubmit_Click" /></td>
         <td class="tdmarginro"><asp:Button ID="btnClose" runat="server" Text="Close" Width="95px" OnClick="btnClose_Click" /></td>
     </tr>
     <tr>
         <td  colspan="4">
             <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
     </tr>
 </table>
 <table>
 
 <tr>
 <td> 
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="620px" OnRowDeleting="GridView1_RowDeleting" Height="111px">
        <FooterStyle BackColor="Tan" />
        <Columns>
            <asp:CommandField HeaderText="Action" SelectText="Delete" ShowSelectButton="True" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:CommandField>
            <asp:BoundField DataField="district_code" HeaderText="Dist" SortExpression="district_code">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No." SortExpression="Rack_No">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="PC_ID" HeaderText="P.C. Name" SortExpression="PC_ID">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="IC_ID" HeaderText="I.C.Code" SortExpression="IC_ID">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="IC_Name" HeaderText="I.C. Name" SortExpression="IC_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Challan_No" HeaderText="TC No." SortExpression="Challan_No">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField HeaderText="TC Date" DataField="Challan_date" SortExpression="Challan_date">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="TID" DataField="Transporter_ID" SortExpression="Transporter_ID">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter" SortExpression="Transporter_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Truck No." DataField="Truck_No" SortExpression="Truck_No">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Disp Bags" DataField="Disp_Bags" SortExpression="Disp_Bags">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Disp. Qty." DataField="Disp_Qty" SortExpression="Disp_Qty">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Recd. Bags" DataField="Received_Bags" SortExpression="Received_Bags">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Recd. Qty." DataField="Received_Qty" SortExpression="Received_Qty">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" CssClass="gridheader" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
 </td>
 </tr>
 </table>

</asp:Content>

