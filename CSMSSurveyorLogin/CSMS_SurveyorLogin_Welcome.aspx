<%@ Page Title="" Language="C#" MasterPageFile="~/CSMSSurveyorLogin/Godown_SurveyorMaster.master" AutoEventWireup="true" CodeFile="CSMS_SurveyorLogin_Welcome.aspx.cs" Inherits="CSMSSurveyorLogin_CSMS_SurveyorLogin_Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Box
        {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 500px;
            padding: 40px;
            background: rgba(234, 229, 173, 0.74);
            border-radius: 10px;
        }

        .surtable
        {
            width: 500px;
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
                <td>
                    <ul style="color: brown;">
                       
                        <li>
                            <asp:HyperLink ID="HyplMvmtPlan" CssClass="surveLinks" runat="server" NavigateUrl="~/CSMSSurveyorLogin/GodownSurveyor_QualityInspection.aspx" Font-Bold="True" ForeColor="brown"> Truck Quality Inspection</asp:HyperLink>
                        </li>
                         <li>
                            <asp:HyperLink ID="HyperLink2" CssClass="surveLinks" runat="server" NavigateUrl="~/CSMSSurveyorLogin/CMS_ReprintQualityInspection.aspx" Font-Bold="True" ForeColor="brown"> Reprint Truck Quality Inspection</asp:HyperLink>
                        </li>

                         <li>
                            <asp:HyperLink ID="HyperLink3" CssClass="surveLinks" runat="server" NavigateUrl="~/CSMSSurveyorLogin/Delete_GodownSurveyor_QualityInspection.aspx" Font-Bold="True" ForeColor="brown"> Delete Truck Quality Inspection</asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="HyperLink1" CssClass="surveLinks" runat="server" NavigateUrl="~/CSMSSurveyorLogin/GodownSurveyor_ChangePassword.aspx" Font-Bold="True" ForeColor="brown">Change Password</asp:HyperLink>
                        </li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

