<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="CMS_Home.aspx.cs" Inherits="District_CMS_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
        .Box
        {
           
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
                <td style="text-align:left;">
                    <ul style="color: brown;">
                       
                        <li style="text-align:left">
                            <asp:HyperLink ID="HyplMvmtPlan" CssClass="surveLinks" runat="server" NavigateUrl="~/District/GodownSurveyorMaster.aspx" Font-Bold="True" ForeColor="brown"> Surveyor Master</asp:HyperLink>
                        </li>
                       
                    </ul>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

