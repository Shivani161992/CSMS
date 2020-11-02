<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="FrmUparjanReports.aspx.cs" Inherits="State_FrmUparjanReports" Title="Uparjan Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div >
    <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
        <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;" 
            border="1" cellpadding="0" cellspacing="0" width="620px">
        <tr>
            <td align="center" 
                style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;" 
                colspan="3">
                 Uparjan Reports</td>
                 
                 
        </tr>
        
       
         <tr>
            <td align="left" style="font-size: 10pt; position: static;  ">
               1</td>
            <td align="left" style="font-size: 10pt; position: static; ">
                <asp:LinkButton ID="lnkprodetail" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Procurement_Details.aspx">Procurement Details Crop Year</asp:LinkButton></td>
                     <td align="left" style="font-weight: bold;">
                 
                
                    <asp:LinkButton ID="linkbtn1" runat="server" PostBackUrl="~/State/NewFrmReport_State.aspx">Back</asp:LinkButton>
                    </td>
        </tr>
        
            <tr>
                <td align="left" style="font-size: 10pt; position: static;  ">
                    1.1</td>
                <td align="left" style="font-size: 10pt; position: static; ">
                    <asp:LinkButton ID="lnkdistwise" runat="server" Font-Size="14px" 
                        ForeColor="Navy" PostBackUrl="~/Reports_State/Acceptance_Distwise2016.aspx">District Wise Procurement</asp:LinkButton>
                </td>
                <td align="left" style="font-weight: bold;">
                    &nbsp;</td>
            </tr>
        
         <tr>
            <td align="left" style="font-size: 10pt; position: static;  ">
                2</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="lnlfci" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Proc_FCISWC.aspx">Procurement Depositer FCI and SWC Details</asp:LinkButton></td>
                    <td>&nbsp;</td>
        </tr>
        
        
           <tr>
            <td align="left" style="font-size: 10pt; position: static;  ">
                3&nbsp;</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="LinkButton34" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/rpt_monitering_TOfor_PC.aspx">Transport Order for Procurement monitering Report</asp:LinkButton></td>
                    <td>&nbsp;</td>
        </tr>
           <tr>
            <td align="left" style="font-size: 10pt; position: static;     ">
                4</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="lnkcatwise" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/CategoryWise_WheatDeposit.aspx">Category Wise Wheat Deposit Report(2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
        </tr>
           <tr>
            <td align="left" style="font-size: 10pt; position: static;     ">
                5</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="lnkfci" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/FCIDeatil_Procurement.aspx">Wheat Surrender to FCI During Procurement</asp:LinkButton></td>
                    <td>&nbsp;</td>
        </tr>
          
         <tr>
            <td align="left" style="font-size: 10pt; position: static;     ">
                6</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="lnkgdnwise" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/WheatDeposit_GodownWise.aspx">Wheat Deposited GodownWise from Procurement</asp:LinkButton></td>
                    <td>&nbsp;</td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static;     ">
                7</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <%-- <asp:LinkButton ID="lnkindist" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/proc_Deposited.aspx">Wheat Deposited In District and Other District</asp:LinkButton>--%>
                  
            <asp:LinkButton ID="lnkindist" runat="server" Font-Size="14px" ForeColor="Navy" PostBackUrl="~/Reports_State/Rpt_WheatDispatch_OtherDist.aspx">Wheat Deposited Other District</asp:LinkButton>
                  
                    </td>
                    <td>&nbsp;</td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static;     ">
               8</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="lnkpayment" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/SocietyPayment.aspx">Wheat Deposited and Payment Details</asp:LinkButton></td>
                    <td>&nbsp;</td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static;     ">
               9</td>
            <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="lnksilo" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Wheat_SiloDeposit.aspx">Wheat Deposited at SILO (Bags and Steel)</asp:LinkButton></td>
                    <td>&nbsp;</td>
        </tr>
<tr>
 <td align="left" style="font-size: 10pt; position: static;     ">
               10</td>
 <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="Lnkwrnwhr" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/WrongWHR.aspx">Wrong WHR Entered</asp:LinkButton></td>
                    <td>&nbsp;</td>
           </tr>
<tr>
 <td align="left" style="font-size: 10pt; position: static;     ">
               11</td>
 <td align="left" style="font-size: 10pt; position: static;">
                <asp:LinkButton ID="Lnkfcisurrender" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_SurrenderTO_FCI.aspx">Surrender to FCI and Dispach</asp:LinkButton></td>
                    <td>&nbsp;</td>
           </tr>
            
        <tr>
            <td align="left" style="font-size: 10pt;  position: static; height: 17px;
              ">
                12</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; ">
                <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Rpt_DeleteAcceptance_Receipt.aspx" OnClick="lnkdelaccept_Click">Deleted Acceptance and Receipt Report</asp:LinkButton></td>
                <td>&nbsp;</td>
        </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">13</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="lnkstockdetail" runat="server" ForeColor="Navy" OnClick="lnkdelaccept_Click" PostBackUrl="~/Reports_State/Rpt_Cropyerwish_ComStock_detail.aspx">Crop Year Wise Commodity Stock Details Of Bags And Quantity</asp:LinkButton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    14</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="lnkstockdetail0" runat="server" ForeColor="Navy" 
                        OnClick="lnkdelaccept_Click" 
                        PostBackUrl="~/Reports_State/Rpt_PC_toGodownmap.aspx">Purchase Center to Godown Mapping and Distance</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    15</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="lnksteelsilo" runat="server" ForeColor="Navy" 
                       PostBackUrl="~/Reports_State/Wheat_SteelSilo.aspx">Wheat Deposit at Steel Silo 2016</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    16</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="lnksttransp" runat="server" ForeColor="Navy" 
                        PostBackUrl="~/Reports_State/Rpt_Transportation_cost_monitoring.aspx">Transportation Cost Analysis</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    17</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="lnkrej" runat="server" ForeColor="Navy" 
                        PostBackUrl="~/Reports_State/RejectionDetails.aspx">Rejection Trucks Details</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    18</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="lnkrej0" runat="server" ForeColor="Navy" 
                        PostBackUrl="~/Reports_State/GodownReceipt_Uparjan.aspx">District Godown Receipt Details</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            
             <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    19</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Navy" 
                        PostBackUrl="~/Reports_State/Rpt_Android_SCSC_Procurement.aspx">Mobile/Tablet SCSC Procurement Dispatch Details</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
             <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    20</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Navy" 
                        PostBackUrl="~/Reports_State/Rpt_Android_AcceptanceNote_Details.aspx">Mobile/Tablet Acceptance Note Details</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">&nbsp;</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">&nbsp;</td>
                <td align="center" style="font-size: large; position: static; height: 17px; text-decoration: underline; color: red;"><strong>Exception Reports</strong></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">1)</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px;">
                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Navy" 
                        PostBackUrl="~/Reports_State/PendingAcptKharif2016_State.aspx">Pending Acceptance Note Details</asp:LinkButton>
                </td>
                <td>&nbsp;</td>
            </tr>

</table>

                </asp:Panel>
    </div>
</asp:Content>

