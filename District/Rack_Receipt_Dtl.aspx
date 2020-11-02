<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Rack_Receipt_Dtl.aspx.cs" Inherits="DistrictFood_Rack_Receipt_Dtl" Title="Rack Receipt Details" %>
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
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 602px;" >
     <tr>
         <td class="tdmarginro" style="color: white; height: 21px; background-color: #6699cc;">
         </td>
         <td  colspan="3" style="color: white; font-weight: bold; height: 21px; background-color: #6699cc;" align="left">
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
             Rack &nbsp;Receipt&nbsp; Details </td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="color: white; height: 21px; background-color: #6699cc">
             <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblcmdty" runat="server" Visible="False"></asp:Label></td>
     </tr>
   <tr>
   <td class ="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;"> 
       <asp:Label ID="Label14" runat="server" Text="Sending District" Width="105px"></asp:Label></td>
     <td class ="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;"><asp:DropDownList ID="ddldistrict" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                            
                        </asp:DropDownList></td>
       <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
           Rack No.</td>
         <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle"> <asp:DropDownList ID="ddlrackno" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged">
         </asp:DropDownList>
             <asp:Label ID="lblrhc" runat="server" Visible="False"></asp:Label></td>
   </tr>
     <tr>
         <td align="left"  colspan="4" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;  border-bottom: 1px solid; background-color: thistle; height: 22px;">
             <asp:Label ID="lblra" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000"
                 Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:Label ID="lblrackno" runat="server" Text="Rack Number"></asp:Label></td>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:TextBox ID="txtrackno" runat="server" Visible="False" AutoPostBack="True" OnTextChanged="txtrackno_TextChanged"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:Label ID="lblrecddist" runat="server" Text="Receiving District" Visible="False" Width="114px"></asp:Label></td>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:DropDownList ID="ddlrecddistrict" runat="server"  Width ="153px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" Enabled="False" Visible="False">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="lblrecdrh" runat="server" Text="Receiving Rail Head" Visible="False"
                 Width="128px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:DropDownList ID="ddldesrailhead" runat="server" Visible="False" Width="154px" AutoPostBack="True" OnSelectedIndexChanged="ddldesrailhead_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Text="--Select--" Value="01"></asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:Label ID="lblrecddistrict" runat="server" Text="Receiving District " Width="114px"></asp:Label></td>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:TextBox ID="txtrecdist" runat="server" Font-Italic="True" ForeColor="Navy"
                 Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="lblrecdrailhed" runat="server" Text="Receiving Rail Head" Width="136px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtrecrailh" runat="server" Font-Italic="True" ForeColor="Navy" Width="146px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:Label ID="Label4" runat="server" Text="Rack Dispatch Qty." Width="119px"></asp:Label></td>
         <td class="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
             <asp:TextBox ID="txtrackqty" runat="server" Width="146px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="Label5" runat="server" Text="Rack Dispatch Date" Width="130px"></asp:Label></td>
         <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;border-bottom: 1px solid; background-color: thistle" align="left">
             <asp:TextBox ID="txtrackddate" runat="server" Font-Italic="True" ForeColor="Navy"
                 Width="120px" OnTextChanged="txtrackddate_TextChanged"></asp:TextBox><br />
             <asp:TextBox ID="DaintyDate2" runat="server" Width="126px"></asp:TextBox>
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	                      });
	          </script>             
             </td>
     </tr>
    <tr>
   <td class ="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;"> 
       <asp:Label ID="Label13" runat="server" Text="Recd. Quantity " Width="118px"></asp:Label></td>
     <td class ="tdmarginro" style="width: 90px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">   &nbsp;<asp:TextBox ID="txtrecdqty" runat="server" Font-Italic="True" ForeColor="Navy"
             Width="146px" MaxLength="13"></asp:TextBox></td>
       <td  class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 90px; border-bottom: 1px solid; background-color: thistle"> Rack Recd. On</td>
         <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;  border-bottom: 1px solid; background-color: thistle" align="left"> 
             <asp:TextBox ID="DaintyDate1" runat="server" Width="126px"></asp:TextBox>
             
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	                      });
	          </script>
             </td>
   </tr>
     <tr>
         <td class="tdmarginro" style="color: white; height: 21px; background-color: #6699cc;">
         </td>
         
         <td  colspan="3" style="color: white; height: 21px; background-color: #6699cc;" align="left">
             <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Details of&nbsp; Issue from Receiving Rail Head</strong></td>
     </tr>
     <tr>
         <td class="tdrailhead" colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;"></td>
         <td class="tdrailhead" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label3" runat="server" Text="Challan Number" Width="110px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:DropDownList ID="ddlchallan" runat="server"  Width ="133px" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label6" runat="server" Text="Issued Quantity" Width="108px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:TextBox ID="txtissqty" runat="server" Font-Italic="True" ForeColor="Navy" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label7" runat="server" Text="TruckChallan No.  " Width="115px"></asp:Label>&nbsp;
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:TextBox ID="tctchallanno" runat="server" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:Label ID="Label10" runat="server" Text="TC Date"></asp:Label></td>
         <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99" align="left">
             <asp:TextBox ID="DaintyDate3" runat="server" Width="126px"></asp:TextBox>
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	                      });
	          </script>
             
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label8" runat="server" Text="Truck No."></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:TextBox ID="txttruckno" runat="server" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:Label ID="Label9" runat="server" Text="Transporter"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
         <asp:DropDownList ID="ddltransporter" runat="server"  Width ="153px">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:Label ID="Label11" runat="server" Text="Destnation District" Width="119px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;"><asp:DropDownList ID="ddldestdistrict" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldestdistrict_SelectedIndexChanged">
         </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:Label ID="Label12" runat="server" Text="Destination  Issue Center/Storage Place Name"
                 Width="143px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:DropDownList ID="ddlissuename" runat="server"  Width ="153px">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             Dispatch Bags</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             <asp:TextBox ID="txtdisbags" runat="server" Width="146px" MaxLength="6"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             Dispatch Qty.</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:TextBox ID="txtdisqty" runat="server" Width="126px" MaxLength="13"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99;">
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             <asp:Button ID="btnaddmore" runat="server" Text="Add More" Width="95px" OnClick="btnaddmore_Click" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccc99">
             </td>
     </tr>
 </table>
<table>
 
 <tr>
 <td> 
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="597px" OnRowDeleting="GridView1_RowDeleting">
        <FooterStyle BackColor="Tan" />
        <Columns>
            <asp:CommandField HeaderText="Action" SelectText="Delete" ShowSelectButton="True" />
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No." SortExpression="Rack_No">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="IC_ID" HeaderText="I.C. ID" SortExpression="IC_ID">
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
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter" SortExpression="Transporter_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField HeaderText="TID" DataField="Transporter_ID" SortExpression="Transporter_ID">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
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
        </Columns>
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" CssClass="gridheader" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
 </td>
 </tr>
 </table>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 602px;" id="Table1" >
     <tr>
         <td colspan="4">
             <asp:Label ID="Label2" runat="server" Font-Italic="True" ForeColor="Maroon" Visible="False"></asp:Label></td>
     </tr>
      <tr>
         <td class="tdmarginro" style="width: 124px; height: 25px;">
         </td>
         <td class="tdmarginro" align="right" style="height: 25px">
         <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="95px" OnClick="btnSubmit_Click" /></td>
         <td class="tdmarginro" style="height: 25px">
         <asp:Button ID="btnClose" runat="server" Text="Close" Width="95px" OnClick="btnClose_Click" /></td>
         <td class="tdmarginro" style="height: 25px">
         </td>
     </tr>
     </table> 
</asp:Content>


