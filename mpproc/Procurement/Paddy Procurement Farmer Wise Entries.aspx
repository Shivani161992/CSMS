<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paddy Procurement Farmer Wise Entries.aspx.cs" Inherits="DistrictLevel_Procurement_Paddy_Procurement_Farmer_Wise_Entries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Paddy Procurement Farmer Wise Entries</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <center style="font-size: 14px" >
  
  <table   border ="0" cellpadding ="0" cellspacing ="0" width="800" style="margin-top:10px; font-size:12px,; margin-left:110px; background-color: gainsboro;"  >
    <tr >
    <td colspan="7" style="text-align: center; background-color: #999933 ; height:30px;  ">
    
        <asp:Label ID="lblmsgWheat_FWise" runat="server" Text="Paddy Procurement Farmer Wise Entries" Font-Bold="True" ForeColor="#000040"></asp:Label>
    
    </td>
    </tr>
   
   <tr >
   <td colspan="7" style="height: 19px">
   
       <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
   
   
   </td>
   </tr>
    <tr >
    <td colspan="7" align="right" style="height: 19px">
   
       <asp:Label ID="lblMas_Q_IS" runat="server" Text="(Qty. is in Qtl.Kgs, Amount in Rs.)"></asp:Label>
   
   
   </td>
 </tr>
  <tr >
  <td colspan="7" style=" height: 44px;">
   <table border="2" cellpadding ="0" cellspacing ="0" width="100%">
   <tr >
    <td style="height: 19px;  ">
        <asp:Label ID="lblDistrict" runat="server" Text="District :-" ForeColor="Navy"></asp:Label>
           <asp:Label ID="lblDistrict_Res" runat="server" ForeColor="Navy" Text="lblDistrict_Res"></asp:Label>
    
    </td>
    <td style="height: 19px; ">
      <asp:Label ID="lblAgency1" runat="server" Text="Agency:-" ForeColor="Navy"></asp:Label>
           <asp:Label ID="lblAgency_Res" runat="server" ForeColor="Navy" Text="lblAgency_Res"></asp:Label>
   
    </td>
    
    <td style="height: ;">
      <asp:Label ID="lblMar_Seas" runat="server" Text="Marketing Season :-" ForeColor="Navy"></asp:Label>
           <asp:Label ID="lblMar_Seas_Res" runat="server" Text="lblMar_Seas_Res" ForeColor="Navy"></asp:Label>
   
    </td>
    
     <td style="height: ">
      <asp:Label ID="lblCropYear1" runat="server" Text="Crop Year :-" ForeColor="Navy"></asp:Label>
      <asp:Label ID="lblCrop_Y_Res" runat="server" Text="lblCrop_Y_Res" ForeColor="Navy"></asp:Label>
   
    </td>
  
    </tr>  
   
   </table>
   </td>
   </tr>
   
    <tr >
    <td style="width: 92px; height: 22px; ">
        <asp:Label ID="lblMar_Ses" runat="server" Text="Procurement On Date:(DD/MM/YYYY)" ForeColor="#004040" Width="184px"></asp:Label>
    
    
    </td>
    <td style="height: 22px; width: 92px; ">
    
        <asp:TextBox ID="txt_Proc_On_Date" runat="server"></asp:TextBox>
   
    </td>
  
    <td style="height: 22px; width: 92px; ">
        <asp:Label ID="lbl_PC" runat="server" Text="Purchase Center:" ForeColor="#004040"></asp:Label>
    
    
    </td>
    <td style="width: 92px; height: 22px; ">
    
        <asp:DropDownList ID="DDL_PC" runat="server">
        </asp:DropDownList>
       
    </td>
      <td style="height: 22px; width: 92px; ">
        <asp:Label ID="lblCommodity" runat="server" Text="Commodity" ForeColor="#004040"></asp:Label>
    
    
    </td>
  
    <td style="width: 92px; height: 22px; ">
    
        <asp:DropDownList ID="DDLCommodity" runat="server">
        </asp:DropDownList>
    
    
    </td>
      <td style="width: 92px; height: 22px; "   >
          <asp:Button ID="btnFetch" runat="server" Text="Fetch" />
    
    </td>
    </tr>
   
     <tr >
    <td colspan="7"  style=" height: 19px;" >
 
    </td>
    
    </tr>
     <tr >
    <td colspan="7"  style="" align="left" >
        <asp:Label ID="lblFar_Sel" runat="server" Text="Farmer Selection:" Font-Bold="True" ForeColor="#000040"></asp:Label>
 
 
    </td>
    
    </tr>
    
     <tr >
    <td  colspan="7" align="center" style="">
   
      <table  border ="2" cellpadding ="0" cellspacing ="0"  width="90%" >
      <tr >
     <td style="width: 180px; height: 24px;">
     
     District:

     </td>
      <td  align="left" style="height: 24px; ">
          <asp:DropDownList ID="DDLDistrict1" runat="server">
          </asp:DropDownList>

     
     </td>
  
     
      <td >
     
     Tehsil:

     </td>
      <td align="left" style="width: 100px";>
          <asp:DropDownList ID="DDLTahsil" runat="server">
          </asp:DropDownList>

     
     </td>
   
     
     </tr>
        <tr >
     <td   style="width: 180px; height: 24px;">
     
     Halka Name-No.(RI Name):

     </td>
      <td style="height: 24px; width:100px" align="left" >
          <asp:DropDownList ID="DDLHAlkaName" runat="server">
          </asp:DropDownList>

     
     </td>
  
     
      <td >
     
     Farmer Name:

     </td>
      <td style="width: 100px;" align="left">
          <asp:DropDownList ID="DDLFarmerName" runat="server">
          </asp:DropDownList>

     </td>
   
     </tr>
           <tr >
              <td   style="width: 180px; height: 24px">
                  <asp:Label ID="lblRationCardNo" runat="server" Text="Ration Card No:-"></asp:Label>
                 
                  
                  </td>
                  
                <td   style="width: 180px; height: 24px">
                  <asp:Label ID="lblRationCardNo_Res" runat="server" Text="lblRationCardNo_Res"></asp:Label>
                
                 
                  
                  </td>
                   <td   style="width: 180px; height: 24px">
                  <asp:Label ID="lblKhasraNo" runat="server" Text="Khasra No:-"></asp:Label>
                  
                  
                  </td>
                   <td   style="width: 180px; height: 24px">
                   <asp:Label ID="lblKhasraNo_Res" runat="server" Text="lblKhasraNo_Res"></asp:Label>
                  
                  
                  </td>
             
          </tr>
     
     </table>
    
    </td>
    
    
    </tr> 
    
     <tr >
    <td style="">
        <asp:Label ID="lblQuantityProcured" runat="server" Text="Quantity Procured :" Width="167px"></asp:Label>
      </td>
    <td >
      <asp:TextBox ID="txtQuantityProcured" runat="server"></asp:TextBox>
    
    </td>
     <td   colspan="2">
        <asp:Label ID="lblPay_Am2_Far" runat="server" Text="Payable Amount to Farmer (msp+bonus)*Qty:" Width="296px"></asp:Label>
      </td>
    <td >
      <asp:TextBox ID="txtPay_Am_2_Far" runat="server"></asp:TextBox>
    
    </td>
    
    </tr>
    <tr >
    <td colspan ="7" style="">
    
    
    </td>
    
    
    </tr>
     <tr >
    
    <td colspan="2" style="">
    
        <asp:Label ID="lbl_Enter_Pay_Trans" runat="server" Text="Do you want to enter payment for transaction? "></asp:Label>
    
    </td>
    <td colspan="2" style="height: 20px">
    
        <asp:RadioButton ID="RD_Butn_Yes"  Text="Yes" GroupName="PayTrans" runat="server" />
        <asp:RadioButton ID="RD_Butn_No" Text="No" GroupName="PayTrans" runat="server" />
    
    </td>

    </tr> 
     <tr >
    <td style="">
    
        <asp:Label ID="lblPaidAmount" runat="server" Text="Paid Amount: "></asp:Label>
    
    
    </td>
      <td>
    
          <asp:TextBox ID="txtPaidAmount" runat="server"></asp:TextBox>
    
    </td>
    <td>
    
        <asp:Label ID="Label1" runat="server" Text="Cheque No :"></asp:Label>
    
    
    </td>
      <td>
    
          <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox>
    
    </td>
    </tr>
      <tr >
    <td style="">
    
        <asp:Label ID="lbl_ChequeDate" runat="server" Text="Cheque Date:(DD/MM/YYYY)"></asp:Label>
    
    
    </td>
      <td>
    
          <asp:TextBox ID="txt_ChequeDate" runat="server"></asp:TextBox>
    
    </td>
    
    </tr>
     <tr >
    <td colspan="7" style="">
        <asp:Button ID="btn_Reset" runat="server" Text="Reset" />
        <asp:Button ID="btnAdd" runat="server" Text="Add" /></td>
    
    </tr>
     <tr >
    <td colspan="7" style="">
    
    
    </td>
    
    </tr>
     <tr >
    <td  colspan="7" style=""  valign="top" align="center">
    
        <asp:GridView ID="GridView_Wheat_Proc"  AutoGenerateColumns="false" runat="server" Font-Size="12px">
             <Columns>
                      
                        <asp:TemplateField HeaderText=" " >
                            <ItemTemplate>
                            
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="20px" />
                        </asp:TemplateField>
                 
                    
                    <asp:TemplateField HeaderText="Transaction ID" >
                            <ItemTemplate>
                           
                            </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="500px" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Farmer Name" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                    
                       <asp:TemplateField HeaderText="Commodity" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Qty Procured" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Commodity" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Amount Payable" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Amount Paid" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Cheque No" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Cheque Date" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Status" >
                           <ItemTemplate>
                                            
                        
                          
                            </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>
                    
                    
                    
                    
                    
                            
                  
                
                    </Columns>  
            <HeaderStyle BackColor="Silver" />
        </asp:GridView>
    
    </td>
    
    </tr>
    
     </table>
  
  </center>
</asp:Content>