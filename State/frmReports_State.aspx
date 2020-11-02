<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="frmReports_State.aspx.cs" Inherits="frmReports_State" Title="Report Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel3" runat="server" Height="600px" ScrollBars="Vertical">
        <table width="100%" border="1" cellpadding="0" cellspacing="0"
            style="border-color: #CCFFFF; font-family: calibri; background-color: #CCFFCC">
            <tr>
                <td align="center" colspan="2"
                    style="font-size: large; position: static; background-color: #cfdcc8; color: #006600; text-decoration: underline;">

                    <strong>Reports</strong></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Procurement and Transported Reports"></asp:Label></td>
            </tr>
            <%--<tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; ">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton34" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/frm_rpt_WheatProcured_transport.aspx">Wheat Procured and Transported Rabi 2013-14</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; ">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                &nbsp;<asp:LinkButton ID="LinkButton35" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_PaddyMillingStatus.aspx">Paddy and Coarse grain Procured and Transported-2013-14</asp:LinkButton></td>
        </tr>--%>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton39" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_PaddyMillingStatus.aspx">Paddy Milling Status Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="Linkmilleragree" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/rpt_miller_info.aspx">Miller Agreement Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkprodetail" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Procurement_Details.aspx">Procurement Details Crop Year</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnlfci" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Proc_FCISWC.aspx">Procurement Depositer FCI and SWC Details</asp:LinkButton></td>
            </tr>


            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">5&nbsp;</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton34" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/rpt_monitering_TOfor_PC.aspx">Transport Order for Procurement monitering Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">6</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkcatwise" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/CategoryWise_WheatDeposit.aspx">Category Wise Wheat Deposit Report(2015)</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">7</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkfci" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/FCIDeatil_Procurement.aspx">Wheat Surrender to FCI During Procurement</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">8</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkmonthly" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_Monthly_statusReport.aspx">Monthly Commodities wise status Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">9</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkgdnwise" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/WheatDeposit_GodownWise.aspx">Wheat Deposited GodownWise from Procurement</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">10</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <%-- <asp:LinkButton ID="lnkindist" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/proc_Deposited.aspx">Wheat Deposited In District and Other District</asp:LinkButton>--%>

                    <asp:LinkButton ID="lnkindist" runat="server" Font-Size="14px" ForeColor="Navy" PostBackUrl="~/Reports_State/Rpt_WheatDispatch_OtherDist.aspx">Wheat Deposited Other District</asp:LinkButton>

                </td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">11</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkpayment" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/SocietyPayment.aspx">Wheat Deposited and Payment Details</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">12</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnksilo" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Wheat_SiloDeposit.aspx">Wheat Deposited at SILO</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">13</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="Lnkwrnwhr" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/WrongWHR.aspx">Wrong WHR Entered</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">14</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="Lnkfcisurrender" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_SurrenderTO_FCI.aspx">Surrender to FCI and Dispach</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8"></td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc; text-align: center">
                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Door Delivery Order Report" Width="226px"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">1</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkDO" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/DPY_DO_State.aspx">Door Step Delivery Order Report</asp:LinkButton></td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">2.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkrootchart" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_Routechart.aspx"> Routechart Status report Districtwise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">3</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton44" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/HO_Issuewise_FPSAllotment.aspx"> Issue Center Wise FPS Allotment</asp:LinkButton></td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">4</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="Linkdistribute" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_DistributionMonitoring.aspx">Districtwise Distribution Monitoring Report</asp:LinkButton></td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">5</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnksector" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/SectorMaster_Report.aspx">Districtwise Sector Name Report</asp:LinkButton></td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">6</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnk_allotlift" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_Allotment_lifting_Report.aspx">Allotment,Lifting and Distribution Issuecenterwise Detail Report</asp:LinkButton></td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">7.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnksecter_tofps" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/rpt_Sectorto_FPS_Mapping.aspx"> Setor to FPS Mapping  Districtwise</asp:LinkButton></td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">8.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="link_pendingTO" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_Pending_IssueTO_detail.aspx">Pending Issue Against TO Detail</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">9.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="link_sectallot" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_Sectorwise_Allotment_Detail.aspx">Secterwise Allotment Detail Report</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">&nbsp;<asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Italic="True"
                    Font-Size="Medium" Text="Commodity Statement Report"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkdistwise_compliedrpt" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_csms_distwise_compliedreport.aspx">District Wise Commodity Statement</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="lnkICwise" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/IC_AllComodity_Summry.aspx">Issue Center Wise Commodity Statement</asp:LinkButton>

                </td>
            </tr>

            <%--<tr>
            <td align="left" style="font-size: 10pt;  position: static; background-color: #cfdcc8; height: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="LinkButton42" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Ho_CropYear_IC.aspx">Issue Center Wise CropYear Statement</asp:LinkButton>
            
            </td>
        </tr>--%>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="Linkwheatstatus" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_WheatStatus.aspx"> Wheat Status Report</asp:LinkButton>

                </td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 17px;">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 17px;">
                    <asp:LinkButton ID="Linkwheatstatus1" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/State_NewCompiled.aspx"> Commodity Statement After New Opening (01/01/2015)</asp:LinkButton>

                </td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">5</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="lnkdistwise_ricedisposal" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_Cropyearwise_cmddisposal.aspx">Cropyear wise commodity disposal report</asp:LinkButton>

                </td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Dispatch Details Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">1</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkfrmproc" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Recdatrack_frmProcurement.aspx">Truck Received at Rack Point send from Procurement</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="LnkRackdetail" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/State_RailRackDetails.aspx">Rack Details</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="lnkrackcommowise" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rackreport_Commoditywise.aspx">By Rack Dispatch Details Commodity Wise</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="lnkroaddispatch" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/RoadDispatch.aspx">ByRoad Dispatch Details Commodity Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Sugar/Salt Statement  Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8;">1</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkbtnsupplyorder" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/SupplyOrder_rpt.aspx">Statement of Sugar Supply Order Details Zone Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">2.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="Linktender" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_TenderPurchase_Supplyorder.aspx">Total Receipt Details for Tender Purchase by Road(Sugar)-District Wise</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8;">3</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkbtnsupplyorder0" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_SugarStatus.aspx">Sugar Status report </asp:LinkButton></td>
            </tr>


            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8;">4.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkbtnsupplyordersalt" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_SaltStatus.aspx">Salt Status report </asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Operators Status Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton29" runat="server" PostBackUrl="~/Reports_State/LoginReportofOperators_District.aspx" Font-Size="14px" ForeColor="Navy">Opeartors Last Login Details of DMMPSCSC</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="LinkButton28" runat="server" PostBackUrl="~/Reports_State/LoginReportofOperators_Issue.aspx" Font-Size="14px" ForeColor="Navy">Opeartors Last Login Details of IssueCenters</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton30" runat="server" PostBackUrl="~/Reports_State/RegisteredOperator_rpt.aspx" Font-Size="14px" ForeColor="Navy">Registered Operator Details(Issue Centre)</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton31" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/RegisteredOperator_rpt_DM.aspx">Registered Operator Details(DMMPSCSC)</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">5</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton32" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/AllUserLoginLogoutReport.aspx">All Users Login Logout Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">6</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton36" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/OperatorStatus_Distwise.aspx">Distwise Operators Status</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">7</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton40" runat="server" Font-Size="Small" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_OperatorsStatus_forAcceptanceNote.aspx">Operators Status (Acceptance Note Details) For Entry Report-District Wise</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">8</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton45" runat="server" Font-Size="Small" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/HO_DataEntryStatus.aspx">Data Entry Status Report-Issue Center Wise</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Receipt Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton26" runat="server" PostBackUrl="~/Reports_State/Summury_Procurement.aspx" Font-Size="14px" ForeColor="Navy">Total Receipt Details(Procurement)</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton20" runat="server" PostBackUrl="~/Reports_State/DailyReceipt_statement.aspx" Font-Size="14px" ForeColor="Navy">Daily Receipt Statement </asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 17px;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 17px;">
                    <asp:LinkButton ID="LinkButton37" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Rpt_TenderPurchase_Supplyorder.aspx">Total Receipt Details for Tender Purchase by Road(Sugar/Salt)-District Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcc8">4</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Rpt_DeleteAcceptance_Receipt.aspx">Deleted Acceptance and Receipt Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcc8">5</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                    <asp:LinkButton ID="Linkmarketing" runat="server"
                        PostBackUrl="~/Reports_State/Rpt_Marfed_recievingDetail.aspx" Font-Size="14px"
                        ForeColor="Navy">Receipt from Marketing federation </asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Summary Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="LinkButton21" runat="server" PostBackUrl="~/Reports_State/District_RO_TO_Details.aspx" Font-Size="14px" ForeColor="Navy">Summary Report(District wise) </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton22" runat="server" PostBackUrl="~/Reports_State/Latest_Date_Receipt_Disp.aspx" Font-Size="14px" ForeColor="Navy">Summary Report(Issue Center Wise)</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton33" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/AllDistrictCommoditiesbetweenDates.aspx">Summary Report(Commodities between two dates)</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton25" runat="server" PostBackUrl="~/Reports_State/State_DO_details.aspx" Font-Size="14px" ForeColor="Navy">No. of Issued DO Details (State Summary Report) IssueCentre-Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 17px;">5</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 17px;">
                    <asp:LinkButton ID="LinkButton38" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/rpt_Pending_Acceptance_Note_Details.aspx">Summary of pending Acceptance Note Detail Division Wise</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 17px;">6</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 17px;">
                    <asp:LinkButton ID="LinkButton41" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/Distwise_AllCommodity_Receipt_Accep.aspx">Summary of Receiving and Acceptance Note District Wise</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Delivery Order Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">1.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Reports_State/liftingDetails_DO_STrpt.aspx" Font-Size="14px" ForeColor="Navy">Lifted DO Details Against Delivery Order State Report </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">2.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Reports_State/liftingDetails_DO_divisionRpt.aspx" Font-Size="14px" ForeColor="Navy">Lifted DO Details Against Delivery Order Report Division-Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">3.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="liftedDO_Details" runat="server" PostBackUrl="~/Reports_State/liftingDetails_DO_rpt.aspx" Font-Size="14px" ForeColor="Navy">Lifted DO Details Against Delivery Order Report District-Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">4.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="doDetails_comm" runat="server" PostBackUrl="~/Reports_State/Details_DO_commodity.aspx" Font-Size="14px" ForeColor="Navy">Delivery Order Report of District FPS/Commodity/Scheme-Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">5.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton11" runat="server" PostBackUrl="~/Reports_State/graphical_Do_STrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Delivery Order State Report </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">6.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton12" runat="server" PostBackUrl="~/Reports_State/graphical_Do_Issuedrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Issued DO Against Allotment Report </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">7.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton13" runat="server" PostBackUrl="~/Reports_State/graphical_Do_liftedrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Lifted DO Against Issued Report </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">8.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton14" runat="server" PostBackUrl="~/Reports_State/graphical_Do_Divrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Delivery Order  Report Division-Wise </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">9.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton15" runat="server" PostBackUrl="~/Reports_State/graphical_Do_DivIssuerpt.aspx" Width="521px" Font-Size="14px" ForeColor="Navy">Graphical Representation of Issued DO Against Allotment Report  Division-Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">10.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton16" runat="server" PostBackUrl="~/Reports_State/graphical_Do_DivLiftrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Lifted DO Against Issued Report  Division-Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Release Order Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">1.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Reports_State/RO_rpt_Ho.aspx" Font-Size="14px" ForeColor="Navy">Release Order Register</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">2.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                    <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/Reports_State/RO_AllotLift_HO.aspx" Font-Size="14px" ForeColor="Navy">Total Allotment and Lifting against FCI RO District wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">3.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton17" runat="server" PostBackUrl="~/Reports_State/Allotment_Lift.aspx" Font-Size="14px" ForeColor="Navy">Release Order Analysis Report </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Transport  Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Reports_State/TO_rpt_HO.aspx" Font-Size="14px" ForeColor="Navy">Details of Transport Order </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton18" runat="server" PostBackUrl="~/Reports_State/Transportation_Statement.aspx" Font-Size="14px" ForeColor="Navy">Statement Of Transportation </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/Reports_State/Truck_Movement_HO.aspx" Font-Size="14px" ForeColor="Navy">Truck Movement Report </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnktruckrec" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/RecevingType_Distwise.aspx">Truck Receiving Distwise Wise </asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">5</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnktruckmove" runat="server" Font-Size="14px" ForeColor="Navy"
                        PostBackUrl="~/Reports_State/TruckMovement_Datewise.aspx">Truck Movement Datewise</asp:LinkButton></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcc8"></td>
                <td align="center" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Stock Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcc8;">1</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/Reports_State/Stock_rpt_HO.aspx" Font-Size="14px" ForeColor="Navy">Stock Register </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="~/Reports_State/Stock_Register_Graph.aspx" Font-Size="14px" ForeColor="Navy">Stock Register(Graphical Representation) </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/Reports_State/Current_Balance_rpt.aspx" Font-Size="14px" ForeColor="Navy">Current Balance Of Stock </asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl="~/Reports_State/Current_Balance_Graph.aspx" Font-Size="14px" ForeColor="Navy">Current Balance Of Stock (Graphical Representation)</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">5.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton43" runat="server"
                        PostBackUrl="~/Reports_State/rpt_Opening_balance.aspx" Font-Size="14px"
                        ForeColor="Navy">Crop yearwise Opening Balance Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">6</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="Linkdamage" runat="server"
                        PostBackUrl="~/Reports_State/frm_damage_sweepage.aspx" Font-Size="14px"
                        ForeColor="Navy">Damage and sweepage cropyear wise Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="State Allocation Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton23" runat="server" PostBackUrl="~/Reports_State/StateAllocation_rpt_Ho.aspx" Font-Size="14px" ForeColor="Navy">State Allocation Details</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton19" runat="server" PostBackUrl="~/Reports_State/State_alloc_rpt.aspx" Font-Size="14px" ForeColor="Navy">State Allocation Details (Graphical Representation)</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton24" runat="server" PostBackUrl="~/Reports_State/StAllot_lift_mpsc.aspx" Font-Size="14px" ForeColor="Navy">Allocation Details Block-Wise (Current & Next Month)</asp:LinkButton></td>
            </tr>





            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;"></td>
                <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Scheme Transfer Reports"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">1&nbsp;</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton27" runat="server" PostBackUrl="~/Reports_State/SchemeTransfer_rpt_Ho.aspx" Font-Size="14px" ForeColor="Navy">Scheme Transfer Report</asp:LinkButton></td>


            </tr>


            <tr>
                <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px; background-color: #cfdcc8">&nbsp;</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc; text-align: center;">
                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                        Text="Paddy Milling Reports"></asp:Label></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px; background-color: #cfdcc8">1.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc; text-align: left;">
                    <asp:LinkButton ID="Linkbutton35" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/PaddyMillingAgreement_Rpt.aspx">Paddy Milling Status</asp:LinkButton>
                </td>
            </tr>




        </table>
    </asp:Panel>
    <div style="margin-top: 0px">
    </div>
</asp:Content>

