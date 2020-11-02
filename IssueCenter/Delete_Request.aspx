<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delete_Request.aspx.cs" Inherits="IssueCenter_Delete_Request" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Request for Delete Acceptance Note Number</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href ="../MyCss/menu.css" type ="text/css" />
    
    
    <script language="javascript"  type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
        function IMG1_onclick() {

        }

    </script>
    
    <style type="text/css">
        .style1
        {
            width: 172px;
        }
        .style2
        {
            width: 100%;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <a > <strong >&nbsp;</strong><table 
        class="style2">
        <tr>
            <td style="text-align: right">
    <a > 
        <asp:HyperLink ID="hypback" runat="server" NavigateUrl = "~/IssueCenter/issue_welcome.aspx" Font-Bold="True" Width="109px">वापस जायें</asp:HyperLink>
    </a>
            </td>
        </tr>
    </table>
    </a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
    <tr>
    <td colspan ="2" style="height: 25px"> 
        <asp:Label ID="title" runat="server" Text="Madhya Pradesh State Civil Supply Corporation" ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
        <tr>
        <td> 
            &nbsp;</td>
                <td class="style1"> 
            <asp:Label ID="Label1" runat="server" Visible="False">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label></td>
                </tr>
  
<div id="hedl"><asp:Label ID="lber" runat="server" Text=""  Visible="false" ForeColor="red" Font-Size="9px"></asp:Label> </div>
<div>
<center>

<table style="border-style: outset; border-color: #000000; width: 630px; font-size: 12px;"  
        border="1">

<tr>
 <td style="width: 838px" align="left">
 <table style="width: 593px">
     <tr>
         <td align="center" colspan="4" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid;
             border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: #cccccc;">
             <asp:Label ID="lbldispprocure" runat="server" Font-Bold="True" Font-Size="16px" 
                 Text="Request For Delete Acceptance Note" BorderWidth="1px"></asp:Label></td>
     </tr>
     <tr>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             Operator Name</td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             <asp:TextBox ID="txt_opertorname" runat="server" Width="180px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                 ControlToValidate="txt_opertorname" ErrorMessage="*Please Enter Operator Name"></asp:RequiredFieldValidator>
         </td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             Operator Moblie. No.</td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             <asp:TextBox ID="txt_operatermono" runat="server" Width="180px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                 ControlToValidate="txt_operatermono" 
                 ErrorMessage="*Please Enter Operator Contact No."></asp:RequiredFieldValidator>
         </td>
     </tr>
     
     <tr>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
     <asp:Label ID="lblDistrictName" runat="server" Text="District" Width="94px" 
                 Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtdist" runat="server" Width ="140px" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
         <asp:Label ID="lblNameDepot" runat="server" Text="IssueCenter " Width="85px" Font-Size="12px"></asp:Label></td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtissue" runat="server"  Width ="140px" Font-Bold="True" 
                 Font-Italic="True" ForeColor="Navy" Height="22px"></asp:TextBox></td>
     </tr>
     
     <tr>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcrop" runat="server"  Width ="140px" 
                 OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" AutoPostBack="true">
       
      </asp:DropDownList></td>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static;">
            <asp:Label ID="lblRecFromDist" runat="server" Text="Acceptance No." Width="84px" 
                 Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
                <asp:DropDownList ID="ddlAccptNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccptNumber_SelectedIndexChanged"
                    Width="220px">
                </asp:DropDownList></td>
         
     </tr>
     <%--<asp:Panel ID="pnlwht" runat="server" Visible="false">
    
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblRecFromDist1" runat="server" Text="Sending District" Width="84px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblpcname1" runat="server" Text="Purchase Center" Width="80px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:DropDownList ID="ddlissuecenter" runat="server" Width="241px" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged" Font-Size="11px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblChallanNumber1" runat="server" Text="Truck Challan No." Width="94px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttrukcno" runat="server"  Width ="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttrukcno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td   style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTruckNumber1" runat="server" Text="Truck No." Width="76px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttruckno" runat="server" Width="146px" MaxLength="12"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txttruckno"
         ErrorMessage="Truck No.  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTrans1" runat="server" Text="Transport Name" Width="108px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddltransporter" runat="server" Width="153px">
     </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblDateOfChallan1" runat="server" Text="Date Of Dispatch" Width="89px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
              
         <asp:TextBox ID="DaintyDate1" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
         </td>
     </tr>
     
  <tr>
  <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Label ID="lblIssuedBags1" runat="server" Text="NO.Of Bags Dispatched" Font-Size="12px" Width="78px"></asp:Label></td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtbagno" runat="server" Width="128px" MaxLength="5"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbagno"
         ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblQuantity1" runat="server" Text="Net Qty. Dispatched"
             Width="55px" Font-Size="12px"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"> <asp:TextBox ID="txtquant" runat="server" Width="138px" MaxLength="13"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtquant"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
      <asp:Label ID="lblKgs" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label></td>
 
  
  </tr>
    </asp:Panel>--%><%--<asp:Panel ID="pnlpdy" runat="server" Visible="false" Width="580px">--%>
    
     <tr>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             Sending District</td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             <asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             Crop Year</td>
         <td align="left" style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             <asp:Label ID="lblcrop" runat="server"></asp:Label></td>
         
     </tr>
     
     <tr>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             Purchase Centre</td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static;" 
             colspan="3">
                <asp:Label ID="lblpccenter" runat="server" Width="550px"></asp:Label></td>
         
     </tr>
     
     <tr>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
             AN Date</td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
        <asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
            </td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
                <asp:Label ID="lblgodown" runat="server" Visible = "false"></asp:Label>
             </td>
         
     </tr>
     
     <tr>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
             Reason For Deletion</td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
             <asp:DropDownList ID="ddl_del_reason" runat="server" AutoPostBack="True" 
                 Width="300px">
                 <asp:ListItem>प्राप्त गोदाम की गलत प्रविष्टि</asp:ListItem>
                 <asp:ListItem>चालान नंबर की गलत प्रविष्टि</asp:ListItem>
                 <asp:ListItem>भेजें मात्रा की गलत प्रविष्टि</asp:ListItem>
                 <asp:ListItem>स्वीकृत मात्रा की गलत प्रविष्टि</asp:ListItem>
                 <asp:ListItem>डबल एंट्री डिलीट करने वाबत</asp:ListItem>
                 <asp:ListItem>अन्य कारण</asp:ListItem>
             </asp:DropDownList>
         </td>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
             &nbsp;</td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static; height: 19px;">
             &nbsp;</td>
         
     </tr>
     
     <tr>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             Description 
             for Deletion</td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static;" 
             colspan="3">
             <asp:TextBox ID="txt_other" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                 ControlToValidate="txt_other" 
                 ErrorMessage="Please Enter Description for Deletion"></asp:RequiredFieldValidator>
         </td>
         
     </tr>
     
     <tr>
         <td  style="background-color: #CCFFFF; font-size: 10pt; position: static;">
             &nbsp;</td>
         <td align="left" 
             style="background-color: #CCFFFF; font-size: 10pt; position: static;" 
             colspan="3">&nbsp;</td>
         
     </tr>
      <tr>
            <td colspan="4">
                <asp:Panel ID="pnlgrd" runat="server" Visible="true">
                    <table style="font-size:13px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; width: 70%;">
                        <tr>
                            <td valign = "top" >
                                <asp:GridView ID="grd_viewDepot" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="Vertical" 
                                 ShowFooter="True" 
                                    Width="950px" Font-Size="Small" style="font-size: small" 
                                   >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Select" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GodownNO" HeaderText="Godown No" />
                                        <asp:BoundField DataField="TruckChalanNo" HeaderText="Challan No" />
                                        <asp:BoundField DataField="DateOfIssue1" HeaderText="Challan Date" />
                                        <asp:BoundField DataField="Receipt_Id" HeaderText="issueid" />
                                        <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter Name" />
                                        <asp:TemplateField HeaderText="Truck No">
                                            <ItemTemplate>
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <asp:Label ID="txttruck" runat="server" BorderColor="Black" 
                                                    Text='<%# Bind("TruckNo") %>' Width="60px">           
                 </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_truck" runat="server" Font-Names="Vani" ForeColor="White" 
                                                    Text="Grand Total" Visible="False"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="FooterStyle" />
                                            <HeaderStyle Font-Size="10px" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Bags">
                                            <ItemTemplate>
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <asp:Label ID="txtSB" runat="server" BorderColor="Black" 
                                                    Text='<%# Bind("Bags") %>'>           
                 </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SB" runat="server" Text="Total" Visible="False"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="FooterStyle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Quantity">
                                            <ItemTemplate>
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <asp:Label ID="txtrcq" runat="server" BorderColor="Black" 
                                                    Text='<%# Bind("QtyTransffer") %>' Width="60px">           
                 </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SQty" runat="server" Font-Names="Vani" ForeColor="White" 
                                                    Visible="False"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="FooterStyle" />
                                            <HeaderStyle Font-Size="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accept Bags" SortExpression="Acc_Bag">
                                            <ItemTemplate>
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <asp:Label ID="txtAB" runat="server" BorderColor="Black" 
                                                    Text='<%# Bind("Acc_Bag") %>' Width="60px">           
                 </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_AB" runat="server" Font-Names="Vani" ForeColor="White" 
                                                    Visible="False"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="FooterStyle" />
                                            <HeaderStyle Font-Size="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accept Qty">
                                            <ItemTemplate>
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <asp:Label ID="txtAQ" runat="server" BorderColor="Black" 
                                                    Text='<%# Bind("Accept_Qty") %>' Width="55px">           
                 </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_AQ" runat="server" Font-Names="Vani" ForeColor="White" 
                                                    Visible="False"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="FooterStyle" />
                                        </asp:TemplateField>
                                  
                                        <asp:TemplateField HeaderText="Rejected Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="TxtNetQ" runat="server" Text="">
                      </asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lbl_RjQ" runat="server" Font-Names="Vani" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="FooterStyle" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
           
     
    </asp:Panel>
            </td>
        </tr>
      <asp:Panel ID="pnlrcvdtl" runat="server" ScrollBars="Vertical" Width="800px"  Visible="false">  
     <tr style=" height:15px;">
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;"  align="left">
            <asp:Label ID="lblCropYear" runat="server" Text="CropYear" Width="80px" Font-Size="12px"></asp:Label> </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
<%--             <asp:DropDownList ID="ddlcropyear" runat="server"  Width="140px" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged" >
                                               
                                            </asp:DropDownList>
--%>             
             </td>
             <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblissid" runat="server" Text="Issue_ID" Width="110px" Font-Size="12px"></asp:Label> &nbsp;</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:TextBox ID="txtissueId" runat="server" Width="180px" Font-Size="12px"></asp:TextBox>
            
     </td>
         
     </tr>
     <tr style="height:15px;">
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblChallanNumber" runat="server" Text="Send Truck Challan No." Width="105px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtchlnno" runat="server"  Width ="142px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtchlnno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td   style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTruckNumber" runat="server" Text="Sending Truck No." Width="86px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttrucknopady" runat="server" Width="140px" MaxLength="12"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttrucknopady"
         ErrorMessage="Truck No. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTrans" runat="server" Text="Transport Name" Width="108px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddlpdyTransporter" runat="server" Width="140px">
     </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lblDateOfChallan" runat="server" Text="Date Of Dispatch" Width="108px" Font-Size="12px"></asp:Label> </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
              
         <asp:TextBox ID="DaintyDate1P" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
                 new tcal({
                     'formname': '0',
                     'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1p'
                 });
	     </script>
         </td>
     </tr>
     
  <tr>
  <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Label ID="lblIssuedBags" runat="server" Text="NO.Of Bags Dispatched" Font-Size="12px" Width="78px"></asp:Label></td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtissubag" runat="server" Width="128px" MaxLength="5"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtissubag"
         ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblQuantity" runat="server" Text="Net Qty. Dispatched"
             Width="55px" Font-Size="12px"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
   <asp:TextBox ID="txtissueqty" runat="server" Width="138px" MaxLength="13"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtissueqty"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
      <asp:Label ID="lblKgs" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label></td>
 
  
  </tr>
 </asp:Panel>
     

    
 </table>
 
 </td>
</tr>


<tr><td style="text-align: right">
        <asp:LinkButton ID="btn_print" runat="server" onclick="btn_print_Click" 
            Visible="False">Print Request</asp:LinkButton>
    </td></tr>


</table>

</center>
</div>
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
             var coda = event.keyCode
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
     function IsNumericProcQty(key, txt) {
         var keycode = (key.which) ? key.which : key.keyCode;
         var num = txt.value;
         var len = num.length;
         var indx = -1;
         indx = num.indexOf('.');

         var dgt = num.substr(indx, len);
         var count = dgt.length;

         if (keycode == 08) {
             return true;
         }
         else if (keycode == 59 || keycode == 32) {
             alert('Semi Colon (;) & Blank Space Not Allowed ...');
             return false;
         }
         else if (keycode == "36" || keycode == "37" || keycode == "38" || keycode == "40" || keycode == "41" || keycode == "43" || keycode == "92" || keycode == "124" || keycode == "34" || keycode == "39" || keycode == "60" || keycode == "62" || keycode == "44" || keycode == "64" || keycode == "61" || keycode == "63") {
             alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
             return false;
         }

         else if (keycode == 09) {
             if (num > 99999) {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value = "0";
                 return false;
             }
         }

         else if (num > 99999) {
             alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
             txt.value = "0";
             return false;
         }

         else if (count > 5) {
             alert("दशमलव के बाद 5 अंक ही आ सकते है");
             return false;
         }

         else if (keycode == 46) {
             if (num.split(".").length > 1) {
                 alert('दशमलव एक ही बार आ सकता है |');
                 return false;
             }
         }
         else if (keycode >= 48 && keycode <= 58) {
             return true;
         }
         else {
             alert('कृपया संख्या ही प्रविष्ट करें |');
             return false;
         } 
     }


     function IsNumericProcQtyBag(key, txt) {
         var keycode = (key.which) ? key.which : key.keyCode;
         var num = txt.value;
         var len = num.length;
         var indx = -1;
         indx = num.indexOf('.');

         var dgt = num.substr(indx, len);
         var count = dgt.length;

         if (keycode == 08) {
             return true;
         }

         else if (keycode == 59 || keycode == 32) {
             alert('Semi Colon (;) & Blank Space Not Allowed ...');
             return false;
         }
         else if (keycode == "36" || keycode == "37" || keycode == "38" || keycode == "40" || keycode == "41" || keycode == "43" || keycode == "92" || keycode == "124" || keycode == "34" || keycode == "39" || keycode == "60" || keycode == "62" || keycode == "44" || keycode == "64" || keycode == "61" || keycode == "63") {
             alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
             return false;
         }

         else if (keycode == 09) {
             if (num > 10000) {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value = "0";
                 return false;
             }
         }

         else if (num > 10000) {
             alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
             txt.value = "0";
             return false;
         }
         else if (keycode == 46) {
             if (num.split(".").length > 0) {
                 alert('दशमलव नहीं आ सकता है |');
                 return false;
             }
         }

         else if (keycode >= 48 && keycode <= 58) {
             return true;
         }
         else {
             alert('कृपया संख्या ही प्रविष्ट करें |');
             return false;
         } 
     }






     function chkSqlKey(key, txt) {

         var keycode = (key.which) ? key.which : key.keyCode;

         if (keycode == 59 || keycode == 32) {
             alert('Semi Colon (;) & Blank Space Not Allowed ...');
             return false;
         }

         else if (keycode == "36" || keycode == "37" || keycode == "38" || keycode == "40" || keycode == "41" || keycode == "43" || keycode == "92" || keycode == "124" || keycode == "34" || keycode == "39" || keycode == "60" || keycode == "62" || keycode == "44" || keycode == "64" || keycode == "61" || keycode == "63") {
             alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
             return false;
         }
     }
     function ChkNotgrate() {
         var ctrlisqty = document.getElementById('ctl00_ContentPlaceHolder1_txtissueqty');
         var ctrlrvqty = document.getElementById('ctl00_ContentPlaceHolder1_txtrecdqty');
         var ctxisqty = ctrlisqty.value
         var ctxtrcvqty = ctrlrvqty.value

         if (ctxtrcvqty == txisqty * 100) {
             alert("NOT grdn 10 times");
             return false
         }
         return true
     }
    </script>   
   </table>
    </center >
    </div>
    <table class="style2">
        <tr>
            <td style="text-align: center">
    <asp:Button ID="btn_delreq" runat="server" onclick="btn_delreq_Click"  
        Text="Send Request" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:Label ID="lbldepott" runat="server">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
        <asp:Label ID="lbleror" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hd_dispach" runat="server" />
    <asp:HiddenField ID="hd_creat" runat="server" />
    <asp:HiddenField ID="hd_sendingdist" runat="server" />
    <asp:HiddenField ID="hd_truck" runat="server" />
    <asp:HiddenField ID="hd_quantity" runat="server" />
    <asp:HiddenField ID="hd_createddate" runat="server" />
    <asp:HiddenField ID="hd_accpdate" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
