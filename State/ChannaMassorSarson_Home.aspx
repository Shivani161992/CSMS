<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="ChannaMassorSarson_Home.aspx.cs" Inherits="State_ChannaMassorSarson_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="critical_js_files" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
        .Box
        {
           
            width: 100%;
            padding: 40px;
            background: rgba(234, 229, 173, 0.74);
            border-radius: 10px;
        }

        .surtable
        {
            width: 600px;
            border-top: 6px groove brown;
            border-bottom: 6px groove brown;
            border-left: 6px groove brown;
            border-right: 6px groove brown;
            border-radius: 8px;
        }

        .surveLinks
        {
            text-decoration: none;
            font-family: 'Bellefair';
            letter-spacing: 2px;
            font-size: 16px;
        }

            .surveLinks:hover
            {
                text-decoration: dotted;
                color: #ff6b5b;
            }
    </style>
    <div class="Box">
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left;">
                    <ul style="color: brown;">
                       
                        <li style="text-align:left">
                            <asp:HyperLink ID="HyplMvmtPlan" CssClass="surveLinks" runat="server" NavigateUrl="~/State/SMSCommodities_SurveyorMaster.aspx" Font-Bold="True" ForeColor="brown"> Surveyor Master</asp:HyperLink>
                        </li>
                          <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink1" CssClass="surveLinks" runat="server" NavigateUrl="~/State/CMS_NackedCost.aspx" Font-Bold="True" ForeColor="brown"> Nacked Cost Master</asp:HyperLink>
                        </li>

                         <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink7" CssClass="surveLinks" runat="server" NavigateUrl="~/State/CSM_NirashritShulka.aspx" Font-Bold="True" ForeColor="brown"> Nirashrit Shulka Master</asp:HyperLink>
                        </li>

                        <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink2" CssClass="surveLinks" runat="server" NavigateUrl="~/State/CMS_CentralBonus_Master.aspx" Font-Bold="True" ForeColor="brown"> Central Bonus Master</asp:HyperLink>
                        </li>

                          <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink3" CssClass="surveLinks" runat="server" NavigateUrl="~/State/CMS_LabourChargesMaster.aspx" Font-Bold="True" ForeColor="brown"> Labour Charges Master</asp:HyperLink>
                        </li>

                         <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink4" CssClass="surveLinks" runat="server" NavigateUrl="~/State/CMS_CommisionToSociety_Master.aspx" Font-Bold="True" ForeColor="brown"> Commission To Society Master</asp:HyperLink>
                        </li>
                          <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink5" CssClass="surveLinks" runat="server" NavigateUrl="~/State/CMS_CommisionToStateAgency.aspx" Font-Bold="True" ForeColor="brown"> Commission To State Agency Master</asp:HyperLink>
                        </li>

                         <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink8" CssClass="surveLinks" runat="server" NavigateUrl="~/Mapping_ChanaSarsoMasoor2018_2019/BillAcceptance_CSM2018.aspx" Font-Bold="True" ForeColor="brown"> Bill Acceptance 2018</asp:HyperLink>
                        </li>

                          <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink6" CssClass="surveLinks" runat="server" NavigateUrl="~/State/CMS_BillGeneration.aspx" Font-Bold="True" ForeColor="brown"> Bill Generation</asp:HyperLink>
                        </li>

                          <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink10" CssClass="surveLinks" runat="server" NavigateUrl="~/State/DeleteSubmittedBill.aspx" Font-Bold="True" ForeColor="brown"> Delete Bill Generation</asp:HyperLink>
                        </li>

                         <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink9" CssClass="surveLinks" runat="server" NavigateUrl="~/Mapping_ChanaSarsoMasoor2018_2019/Reprint_BillAcceptance.aspx" Font-Bold="True" ForeColor="brown"> Reprint Bill Number Summary For Acceptance</asp:HyperLink>
                        </li>

                         <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink11" CssClass="surveLinks" runat="server" NavigateUrl="~/Mapping_ChanaSarsoMasoor2018_2019/Delete_BillAcceptance.aspx" Font-Bold="True" ForeColor="brown"> Delete Bill of Acceptance</asp:HyperLink>
                        </li>

                        <li style="text-align:left">
                            <asp:HyperLink ID="HyperLinkCMS" CssClass="surveLinks" runat="server" NavigateUrl="~/State/Cms_Masters_Multiple.aspx" Font-Bold="True" ForeColor="brown">Multiple CMS Masters</asp:HyperLink>
                        </li>
                         <li style="text-align:left">
                            <asp:HyperLink ID="HyperLink12" CssClass="surveLinks" runat="server" NavigateUrl="~/State/DelDepositer_CSM2018.aspx" Font-Bold="True" ForeColor="brown"> Delete Depositor </asp:HyperLink>
                        </li>
                       
                    </ul>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

