<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="TransportOrder_Type.aspx.cs" Inherits="District_TransportOrder_Type" Title="Transport Order Type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 610px;" >
                    <tr>
                        <td  style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                            &nbsp;&nbsp;
                        </td>
                        <td align="left" colspan="5" style="border-right: white 1px solid; border-top: white 1px solid;
                            border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: #cccccc;">
                            <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Select Transport Order Type </strong> &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="center" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Transport Order Issue For
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                 Width="181px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="center" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close" Width="120px" /></td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="center" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Type</td>
         <td align="center" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Description
         </td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             *</td>
         <td align="left" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             From FCI</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Issue Transport Order for Lifting From FCI</td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             *</td>
         <td align="left" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Procurement</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Issue Transport Order for Lifting From Society (Procurement)</td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             *</td>
         <td align="left" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Other Depot
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Issue Transport Order for Lifting From Other Godown</td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             *</td>
         <td align="left" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             CMR</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Issue Transport Order for Lifting From CMR</td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             *</td>
         <td align="left" colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Levy Rice</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             Issue Transport Order for Lifting From&nbsp; Levy.</td>
     </tr>
      
                                  
       
   
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
        
       </asp:Content>
