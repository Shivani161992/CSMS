<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="CSC_Procurement_New.aspx.cs" Inherits="IssueCenter_CSC_Procurement" Title="Receipt From Purchase Center" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="hedl">
    <asp:Label ID="lber" runat="server" Text=""  Visible="false" ForeColor="red" Font-Size="9px"></asp:Label> </div>
<div>
<center>

<table style="width: 850px; font-size: 12px;">

<tr>
 <td  align="left">
 <table style="width: 850px">
     <tr>
         <td align="center" colspan="4" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid;
             border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: #cccccc;">
             <asp:Label ID="lbldispprocure" runat="server" Font-Bold="True" Font-Size="16px" Text="Dispatch From Purchase Center (Procurement)"></asp:Label>&nbsp;2016</td>
     </tr>
     <tr>
         <td align="left" style="background-color: #ffffcc; font-size: 10pt; position: static;">
     <asp:Label ID="lblDistrictName" runat="server" Text="District(Logged in)" Width="94px" Font-Size="12px"></asp:Label></td>
         <td align="left" 
             
             
             
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 161px;">
     <asp:TextBox ID="txtdist" runat="server" Width ="140px" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
       
         <td align="left" 
             
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 407px;">
         <asp:Label ID="lblNameDepot" runat="server" Text="IssueCenter " Width="85px" Font-Size="12px"></asp:Label></td>
         <td align="left" 
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 239px;">
     <asp:TextBox ID="txtissue" runat="server"  Width ="140px" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
     </tr>
     
     <tr>
         <td align="left" 
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 699px; height: 19px;">
             <asp:Label ID="lblCropYear" runat="server" Font-Size="12px" Text="CropYear" 
                 Width="80px"></asp:Label>
         </td>
         <td align="left" 
             
             
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 161px; height: 19px;">
              <asp:DropDownList ID="ddlcropyear" runat="server"  Width="140px" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged" >
                                               
            </asp:DropDownList>
             </td>
       
         <td align="left" 
             
             
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 407px; height: 19px;">
             </td>
         <td align="left" 
             
             style="background-color: #ffffcc; font-size: 10pt; position: static; height: 19px; width: 239px;">
             </td>
     </tr>
     
     <tr>
         <td style="background-color: #ffffcc; font-size: 10pt; position: static; width: 699px;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity" Font-Size="12px"></asp:Label></td>
         <td align="left" 
             
             
             
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 161px;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width ="140px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" AutoPostBack="true">
       
      </asp:DropDownList></td>
         <td  style="background-color: #ffffcc; font-size: 10pt; position: static; width: 407px;">
            <asp:Label ID="lblRecFromDist" runat="server" Text="Sending District" Width="84px" Font-Size="12px"></asp:Label></td>
         <td align="left" 
             style="background-color: #ffffcc; font-size: 10pt; position: static; width: 239px;">
      <asp:DropDownList ID="ddldistpdy" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddldistpdy_SelectedIndexChanged">
             </asp:DropDownList></td>
         
     </tr>
     <%--<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        BackColor="#CCCCCC" BorderColor="#999999" 
                 BorderStyle="Solid" BorderWidth="3px" 
                                        CellPadding="4" Font-Bold="False" Font-Size="Small" Width="713px" 
                                           CellSpacing="2" ForeColor="Black" DataKeyNames="commodityid,Godownid" >
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Godown" HeaderText="Godown Name" 
                                                SortExpression="commodityid">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="qty_RecF" HeaderText="Recd Qunatity Faq" 
                                                SortExpression="qty_RecF">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="qty_RecU" HeaderText="Recd Qunatity URS" 
                                                SortExpression="qty_RecU">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="bags_JuteN" HeaderText="bags Jute New" SortExpression="bags_JuteN">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="bags_PP" HeaderText="bags PP." >
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bags_JuteO" HeaderText="bagsJute Old" >
                                            
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="commodityid" HeaderText="commodityid" 
                                                Visible="False" />
                                            <asp:BoundField DataField="Godownid" HeaderText="Godownid" Visible="False" />
                                            <asp:BoundField DataField="Category" HeaderText="Category" />
                                            
                                            
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small" 
                                            ForeColor="White" />
                                     
                                    </asp:GridView>--%>
     
     <%--<asp:Panel ID="pnlpdy" runat="server" Visible="false" Width="580px">--%>
    
     <tr>
         <td  style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: center; width: 699px;">
             <asp:Label ID="lblissdat" runat="server" Text="Date of Issue" Width="89px" Font-Size="12px" Visible="False"></asp:Label>             
             </td>
         <td align="center" 
             
             
             
             style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: center; width: 161px;">
         <asp:TextBox ID="txtisdate" runat="server" Width="119px" Visible="False"></asp:TextBox>
             <asp:Label ID="lblpcname" runat="server" Text="Purchase Center" Width="80px" Font-Size="12px"></asp:Label><%--<script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtisdate'
	    });
	     </script>--%></td>
         <td style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: center;" colspan="2">
         <asp:DropDownList ID="ddluparjan" runat="server" Width="271px" Font-Size="11px" 
                 AutoPostBack="True" OnSelectedIndexChanged="ddluparjan_SelectedIndexChanged" 
                 Height="23px">
                 
             </asp:DropDownList></td>
     </tr>
    
     <tr>
         <td colspan="4" style="color: #CC0000; font-size: small;">
             <b style="text-align: center">कृपया सेक्टर की खरीद केंद्र से मेपिंग एवं 
             परिवहनकर्ता मास्टर की एंट्री ,जिला कार्यालय द्वारा अनिवार्य रूप से करा ले , बिना 
             इसके प्राप्ति नहीं की जा सकेगी</b></td>
     </tr>
    
     <tr>
         <td  style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: right; " 
             colspan="2">
             <asp:Label ID="lblsector" runat="server" Text="खरीदी केंद्र का सम्बंधित सेक्टर" 
                 Font-Size="12px" ForeColor="#003399" 
                 Visible="False"></asp:Label>             
             </td>
         <td style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: left;" 
             colspan="2">
             <asp:DropDownList ID="ddlsector" runat="server" Height="22px" Width="150px" 
                 Visible="False" >
             </asp:DropDownList>
         </td>
     </tr>
      <tr>
            <td colspan="4">
              <center>
                <asp:Panel ID="pnlgrd" runat="server" ScrollBars="Vertical" Visible="false" Width = "850px" Height="200px">
              
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound" 
            PageSize="1" PagerSettings-Visible ="true" ShowFooter = "True" CellPadding="1" 
                        ForeColor="#333333" GridLines="None" 
            OnPageIndexChanging="dgridchallan_PageIndexChanging"
                        OnPageIndexChanged="dgridchallan_PageIndexChanged" Width="840px"  >
        <HeaderStyle   BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="15px" />
        <Columns>
           <asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="Click to Receive" >
                <ItemStyle  ForeColor="Blue" />
            </asp:CommandField>
                  <asp:BoundField DataField="DateOfIssue" HeaderText="Date Of Dispatch" SortExpression="DateOfIssue">
            </asp:BoundField>   
             <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID">
                <ItemStyle   HorizontalAlign="Left" ForeColor="Maroon" />
                 <HeaderStyle ForeColor="White" />
            </asp:BoundField>          
            <asp:BoundField DataField="TruckChalanNo" HeaderText="TC No." ReadOnly="True" SortExpression="TruckChalanNo" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle  HorizontalAlign="Right"/>
            </asp:BoundField>
            <asp:BoundField DataField="TruckNo" HeaderText="Truck No." ReadOnly="True"
                SortExpression="TruckNo" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
                       
            <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Bags" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="QtyTransffer" HeaderText="Qty" ReadOnly="True"
                SortExpression="QtyTransffer" >
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle  HorizontalAlign="Right"/>
            </asp:BoundField>
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transport Name" ReadOnly="True" SortExpression="Transporter_Name" >
                <ItemStyle HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
             <asp:BoundField DataField="TransporterId" HeaderText="TransporterId" ReadOnly="True" SortExpression="TransporterId">
                <ItemStyle HorizontalAlign="Right" Width="0px" Font-Size="0px"/>
                <HeaderStyle HorizontalAlign="Right" Width="0px" Font-Size="0px" />
            </asp:BoundField> 
            
            <asp:BoundField DataField="JutBag" HeaderText="New Jute" ReadOnly="True" SortExpression="JutBag">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>     
            
            <asp:BoundField DataField="Jut_OldBag" HeaderText="Jute Old" ReadOnly="True" SortExpression="Jut_OldBag">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>    
            
            <asp:BoundField DataField="HDPEBag" HeaderText="PP Bag" ReadOnly="True" SortExpression="HDPEBag">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>     
                 
                 <asp:BoundField DataField="GodownTypeId" HeaderText="GdnType" ReadOnly="True" SortExpression="GodownTypeId">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>    
                                
           
        </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    
    
    </asp:Panel>
    
    </center>
            </td>
        </tr>
      <asp:Panel ID="pnlrcvdtl" runat="server" ScrollBars="Vertical" Visible="false">  
     <tr style=" height:15px;">
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;"  align="left">
             </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
            
             
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
	new tcal ({
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
     
      <tr>
         <td  style="background-color: #cccccc; height: 26px; width: 699px;">
             &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
             <asp:Label ID="Label14" runat="server" Text="Book No." Visible="False"></asp:Label>&nbsp;</td>
         <td align="center" style="background-color: #cccccc; height: 26px;" colspan="2">
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
             <asp:Label ID="lblRecepDetail" runat="server" Text="Receipt Details" Width="120px" Font-Bold="True" Font-Size="13px"></asp:Label></td>
         <td align="left" style="background-color: #cccccc; height: 26px; width: 239px;">
            
     <asp:TextBox ID="txtbookno" runat="server" Width="146px" Visible="False"></asp:TextBox>&nbsp;</td>
     </tr>
     <tr>
         <td   style="background-color: cornflowerblue; font-size: 10pt; position: static; width: 699px;">
             <asp:Label ID="lblReceiptDate" runat="server" Text="प्राप्ति दिनांक" 
                 Width="73px" Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td  colspan="2" style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:TextBox ID="DaintyDate3" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
	     </script>
             &nbsp;
         </td>
         <td align="left" 
             style="background-color: cornflowerblue; font-size: 10pt; position: static; width: 239px;">
             &nbsp;&nbsp;
             <asp:DropDownList ID="ddlscheme" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged"
                 Width="53px" >
                 <asp:ListItem Selected="True" Value="101">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 699px;">
             <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="प्राप्त चालान नंबर"
                 Width="131px" BackColor="White" Font-Bold="True"></asp:Label></td>
         <td align="left" 
             
             
             
             style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 161px;">
             <asp:TextBox ID="txtrec_tcnumber" runat="server" MaxLength="13" Width="150px"></asp:TextBox></td>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 407px;">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" Text="प्राप्त ट्रक नंबर"
                 Width="141px" BackColor="White"></asp:Label></td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 239px;">
             <asp:TextBox ID="txtRec_TruckNumber" runat="server" MaxLength="13" Width="150px"></asp:TextBox></td>
     </tr>
     <tr>
         <td style="font-size: medium; position: static;  text-align: center; color: #000099;" 
             colspan="4">
             <b>नीचे में केवल सम्बंधित गोदाम की ब्रांच एवं गोदाम का नाम चुनें</b></td>
     </tr>
     <tr>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 699px;">
             <asp:Label ID="lbldpo" runat="server" Text="ब्रांच चुनें" Font-Size="12px" 
                 Font-Bold="True"></asp:Label></td>
         <td align="left"  
             
             
             
             style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 161px;">
             <asp:DropDownList ID="ddlbranchwlc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbranchwlc_SelectedIndexChanged"
                 Width="150px" Height="24px">
             </asp:DropDownList></td>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 407px;">
             <asp:Label ID="lblGodownNo" runat="server" Font-Size="12px" 
                 Text="गोदाम का नाम चुनें" Width="123px" Font-Bold="True"></asp:Label></td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 239px;">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="175px" Height="27px">
                  <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 699px;">
             <asp:Label ID="lblhirty" runat="server" Text="Hired_Type" Font-Size="12px" 
                 Font-Bold="True"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 161px;">
             <asp:TextBox ID="txthhty" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"
                 Width="150px" Enabled="false"></asp:TextBox></td>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 407px;">
             <asp:Label ID="lblMaxCap" runat="server" Text="MaxCapacity" Font-Size="12px" 
                 Font-Bold="True"></asp:Label></td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 239px;">
             <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                 ReadOnly="True" Width="144px" Enabled="false"></asp:TextBox></td>
     </tr>
     <tr>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 699px;">
             <asp:Label ID="lblCurStackCap" runat="server" Text="Current Cap." 
                 Font-Size="12px" Width="71px" Font-Bold="True"></asp:Label></td>
         <td align="left" 
             
             
             
             style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 161px;">
             <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="150px" Enabled="false"></asp:TextBox></td>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 407px;">
             <asp:Label ID="lblAvailable" runat="server" Text="Available" Font-Size="12px" 
                 Font-Bold="True"></asp:Label></td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: cornflowerblue; width: 239px;">
             <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="143px" Enabled="false"></asp:TextBox></td>
     </tr>
     <tr >
         <td style="text-align: center; height: 20px;" colspan="4"   >
             <b><span style="font-size: medium; color: #000099">&nbsp;</span><span 
                 style="font-size: medium; color: #CC0000">नीचे में केवल प्राप्त बोरो की 
             संख्या ध्यान पूर्वक भरें</span></b> </td>
     </tr>
     <tr >
         <td style="color: #003399; height: 25px; font-size: medium; text-align: center;" 
             colspan="4"   >
             <span style="text-align: center; font-weight: bold; font-size: small;">CSMS में 
             भरे गए परिवहनकर्ता का नाम चुने जो की बिल के दौरान उपयोग होगा (नाम नहीं आने पर 
             मास्टर में एंट्री करें या डाटा अपडेट करें)</span><span style="text-align: center; font-weight: bold; font-size: 10pt"> </span></td>
     </tr>
     <tr >
         <td style="color: #003399; height: 25px; font-size: medium; text-align: center;" 
             colspan="4"   >
             सायलो एवं क्रय परिसर के लिए परिवहनकर्ता का नाम जरुरी नहीं है |</td>
     </tr>
     <tr style="background-color: #FFCC66">
         <td style="width: 699px"   >
             <asp:Label ID="lbltotalReceivedBags" runat="server" Text="प्राप्त बोरे जुट (नए)" 
                 Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td align="left" style="width: 161px" >
             <asp:TextBox ID="txt_recJutNew" runat="server" MaxLength="13" Width="150px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_recJutNew"
          ErrorMessage="Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
          

             </td>
         <td style="width: 407px" >
             <asp:Label ID="lblGodownNo0" runat="server" Font-Size="12px" 
                 Text="परिवहन कर्ता का नाम CSMS मास्टर के अनुसार" Width="248px" 
                 Font-Bold="True"></asp:Label></td>
         <td align="left" style="width: 239px" >
             <asp:DropDownList ID="ddlcsms_transp" runat="server" Height="27px" 
                 Width="170px">
             </asp:DropDownList>
         </td>
     </tr>
     <tr style="background-color: #FFCC66">
         <td style="width: 699px"   >
             <asp:Label ID="lbltotalReceivedBags0" runat="server" Text="प्राप्त बोरे P.P." 
                 Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td align="left" style="width: 161px" >
             <asp:TextBox ID="txt_recPP" runat="server" MaxLength="13" Width="150px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                 ControlToValidate="txt_recPP" ErrorMessage="Bags Required" 
                 ValidationGroup="1">*</asp:RequiredFieldValidator>
          

          </td>
         <td colspan="2" >
             <asp:Button ID="btnbags" runat="server" Text="प्राप्त बोरो की कुल गणना करें " 
                 Font-Bold="True" ForeColor="#993366" onclick="btnbags_Click" 
                 Visible="False" />
         </td>
     </tr>
     <tr style="background-color: #FFCC66">
         <td style="width: 699px"   >
             <asp:Label ID="lbltotalReceivedBags1" runat="server" Text="प्राप्त बोरे जुटे (पुराने)" 
                 Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td align="left" style="width: 161px" >
             <asp:TextBox ID="txt_recJutOld" runat="server" MaxLength="13" Width="150px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                 ControlToValidate="txt_recJutOld" ErrorMessage="Bags Required" 
                 ValidationGroup="1">*</asp:RequiredFieldValidator>
          

          </td>
         <td style="width: 407px" >
             <asp:Label ID="lbltotalReceivedBags6" runat="server" Text="कुल प्राप्त बोरे(Jute New + Old + PP)" 
                 Font-Size="12px" Font-Bold="True" Visible="False"></asp:Label></td>
         <td align="left" style="width: 239px" >
             <asp:TextBox ID="txt_bags" runat="server" MaxLength="13" Width="137px" 
                 BackColor="#CCCCCC" ReadOnly="True" Visible="False"></asp:TextBox>
             </td>
     </tr>
     <tr >
         <td style="font-size: medium; text-align: center; color: #000099; height: 9px;" 
             colspan="4"   >
             <b>नीचे में केवल प्राप्त मात्रा की जानकरी भरें</b></td>
     </tr>
     <tr style = "background-color: #CCFF66">
         <td style="width: 699px; height: 25px;"   >
             <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="प्राप्त मात्रा. FAQ" 
                 Width="106px" Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td align="left" style="width: 161px; height: 25px;" >
             <asp:TextBox ID="txtfaq_qty" runat="server" MaxLength="13" Width="150px" ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtfaq_qty"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
             &nbsp;<asp:Label ID="lblqntl" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label></td>
         <td style="width: 407px; height: 25px" >
             &nbsp;</td>
         <td align="left" style="height: 25px; width: 239px;" >
             &nbsp;</td>
     </tr>
     <tr style = "background-color: #CCFF66">
         <td style="width: 699px"   >
             <asp:Label ID="lblTotalQuantityReceived0" runat="server" Text="प्राप्त मात्रा URS" 
                 Width="124px" Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td align="left" style="width: 161px" >
             <asp:TextBox ID="txtUrs_qty" runat="server" MaxLength="13" Width="150px" >0</asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtUrs_qty"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
             <asp:Label ID="lblqntl0" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label>
             </td>
         <td colspan="2" >
             <asp:Button ID="btnqty" runat="server" Text="प्राप्त मात्रा की कुल गणना करें " 
                 Font-Bold="True" ForeColor="#CC3399" onclick="btnqty_Click" 
                 Visible="False" Width="218px" />
         </td>
     </tr>
     <tr style = "background-color: #CCFF66">
         <td style="width: 699px"   >
             &nbsp;</td>
         <td align="left" style="width: 161px" >
             &nbsp;</td>
         <td style="width: 407px" >
             <asp:Label ID="lblTotalQuantityReceived1" runat="server" Text="कुल प्राप्त मात्रा(FAQ + URS)" 
                 Width="163px" Font-Size="12px" Font-Bold="True" Visible="False"></asp:Label></td>
         <td align="left" style="width: 239px" >
             <asp:TextBox ID="txtqty" runat="server" MaxLength="13" Width="150px" 
                 BackColor="Silver" ReadOnly="True" Visible="False"></asp:TextBox>
             <asp:Label ID="lblqntl1" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label>
             </td>
     </tr>
     <tr>
         <td colspan="2" style="text-align: left"   >
             </td>
         <td style="width: 407px" >
             <asp:Label ID="lblcategory" runat="server" Text="URS Category(गेहूँ प्राप्ति के लिए )" 
                 Width="202px" Font-Size="13px" Font-Bold="True" ForeColor="#993300"></asp:Label></td>
         <td align="left" style="width: 239px" >
             <asp:DropDownList ID="ddlcategory" runat="server" Height="21px" 
                 style="margin-bottom: 0px" Width="150px">
                <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>--%>
                <asp:ListItem Value="N">None</asp:ListItem>
                 <asp:ListItem Value="Y">Y</asp:ListItem>
                 <asp:ListItem Value="Z">Z</asp:ListItem>
                 
             </asp:DropDownList>
         </td>
     </tr>
     <tr>
         <td style="font-size: 10pt; position: static; height: 24px; width: 699px;">
             <asp:Label ID="lbltotalReceivedBags2" runat="server" Text="खराब सिलाई वाले बोरो की संख्या" 
                 Font-Size="11px" Font-Bold="True" style="font-size: small"></asp:Label></td>
         <td align="left"  
             style="font-size: 10pt; position: static; height: 24px;  width: 161px;">
             <asp:TextBox ID="txtbadStiching" runat="server" MaxLength="13" Width="150px" 
                 Height="21px">0</asp:TextBox>
             </td>
         <td style="font-size: 10pt; position: static; height: 24px; width: 407px;">
             <asp:Label ID="lbltotalReceivedBags3" runat="server" Text="खराब छाप वाले बोरो की संख्या" 
                 Font-Size="11px" Font-Bold="True" style="font-size: small"></asp:Label>
         </td>
         <td align="left" 
             style="font-size: 10pt; position: static; height: 24px; width: 239px;">
             <asp:TextBox ID="txtBadStelcile" runat="server" MaxLength="13" Width="146px" >0</asp:TextBox>
         </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 699px;">
             <asp:Label ID="lbltotalReceivedBags4" runat="server" Text="नमी का प्रतिशत" 
                 Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td align="left" 
             
             
             
             
             
             
             
             
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 161px;">
             <asp:TextBox ID="txtmoisture" runat="server" MaxLength="13" Width="150px" 
                 Height="21px"></asp:TextBox>
             </td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 407px;">
             <asp:Label ID="lbltotalReceivedBags5" runat="server" Text="तौल पर्ची क्रमांक" 
                 Font-Size="12px" Font-Bold="True"></asp:Label></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 239px;">
             <asp:TextBox ID="txtTaulNum" runat="server" MaxLength="13" Width="150px" 
                 Height="21px"></asp:TextBox>
             </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 699px;">
             <asp:Label ID="lblgdntype" runat="server" Visible="False"></asp:Label>
         </td>
         <td align="left" 
             
             
             
             
             
             
             
             
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 161px;">
             &nbsp;</td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 407px;">
             &nbsp;</td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 239px;">
             &nbsp;</td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 699px;">
         </td>
         <td align="left" 
             
             
             
             
             
             
             
             
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 161px;">
             &nbsp;</td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 407px;">
       <%--  <asp:Button ID="btnaddGodown" runat="server" Text="Add to Godown"  Width="131px" onclick="btnaddGodown_Click"/>--%>
                 </td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 239px;">
         </td>
     </tr>
     <tr>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static; " 
             colspan="4">
             <asp:Panel ID="Panel2" runat="server" Visible="False">
                                   <center>
                                       <%--<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        BackColor="#CCCCCC" BorderColor="#999999" 
                 BorderStyle="Solid" BorderWidth="3px" 
                                        CellPadding="4" Font-Bold="False" Font-Size="Small" Width="713px" 
                                           CellSpacing="2" ForeColor="Black" DataKeyNames="commodityid,Godownid" >
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Godown" HeaderText="Godown Name" 
                                                SortExpression="commodityid">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="qty_RecF" HeaderText="Recd Qunatity Faq" 
                                                SortExpression="qty_RecF">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="qty_RecU" HeaderText="Recd Qunatity URS" 
                                                SortExpression="qty_RecU">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="bags_JuteN" HeaderText="bags Jute New" SortExpression="bags_JuteN">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="bags_PP" HeaderText="bags PP." >
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bags_JuteO" HeaderText="bagsJute Old" >
                                            
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="commodityid" HeaderText="commodityid" 
                                                Visible="False" />
                                            <asp:BoundField DataField="Godownid" HeaderText="Godownid" Visible="False" />
                                            <asp:BoundField DataField="Category" HeaderText="Category" />
                                            
                                            
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small" 
                                            ForeColor="White" />
                                     
                                    </asp:GridView>--%>
                                     </center>
                                </asp:Panel>
             </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 699px;">
             &nbsp;&nbsp;&nbsp;&nbsp;
         </td>
         <td align="right" 
             
             
             
             
             
             
             
             
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 161px;">
         <asp:Button ID="btnsavePaddy" runat="server" Text="Save" 
                 OnClick="btnsavePaddy_Click"  Visible="false" ValidationGroup="1" Width="103px" 
                 style="height: 26px"/>&nbsp;
     </td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 407px;">
             <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="91px"/></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 239px;">
             <asp:Button ID="btnaddnew" runat="server" OnClick="btnaddnew_Click" Text="New" Width="137px" /></td>
     </tr>
     
     
     
     <tr>
         <td align="left" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="299px" ShowSummary="False" />
             &nbsp;&nbsp; &nbsp;</td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 407px;">
             &nbsp; &nbsp;
         </td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 239px;">
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" Width="139px">Print Receipt Detail</asp:HyperLink>
             &nbsp;
         </td>
     </tr>
     <tr>
         <td  colspan="4" style="border-right: lightskyblue 1px solid;
             border-top: lightskyblue 1px solid; border-left: lightskyblue 1px solid; border-bottom: lightskyblue 1px solid">
             <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td  colspan="4" style="border-right: lightskyblue 1px solid;
             border-top: lightskyblue 1px solid; border-left: lightskyblue 1px solid; border-bottom: lightskyblue 1px solid; height: 3px;">
     <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label></td>
     </tr>
    
 </table>
 
 </td>
</tr>
</table>
<table >
 <tr>
         <td align="left" colspan="6">
         </td>
     </tr>
</table>
 <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                
                             
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
function IsNumericProcQty(key,txt)
{
  var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
      
    if(keycode==08)
    {
        return true;
    }
     else if (keycode == 59 || keycode  == 32)
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>99999)
            {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>99999)
    {
            alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    
    else  if (count > 5)  
    {
                alert("दशमलव के बाद 5 अंक ही आ सकते है");
                return false;
    }
    
    else if(keycode==46)
    {
         if (num.split(".").length>1)
         {    
            alert('दशमलव एक ही बार आ सकता है |');
            return false;
         }
    }
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }}


function IsNumericProcQtyBag(key,txt)
{
  var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
      
    if(keycode==08)
    {
        return true;
    }
    
     else if (keycode == 59 || keycode  == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>10000)
            {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>10000)
    {
            alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    else if(keycode==46)
    {
         if (num.split(".").length>0)
         {    
            alert('दशमलव नहीं आ सकता है |');
            return false;
         }
    }
    
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }}






function chkSqlKey(key,txt)
{
 
    var keycode = (key.which) ? key.which : key.keyCode;
    
    if (keycode == 59  || keycode == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
   
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
}
function ChkNotgrate()
{
var ctrlisqty = document.getElementById('ctl00_ContentPlaceHolder1_txtissueqty');
var ctrlrvqty = document.getElementById('ctl00_ContentPlaceHolder1_txtrecdqty');
var ctxisqty = ctrlisqty.value 
var ctxtrcvqty = ctrlrvqty.value 

if(ctxtrcvqty == txisqty * 100  )
{
alert("NOT grdn 10 times");
return false
}
return true
}
    </script>   

  
</asp:Content>
