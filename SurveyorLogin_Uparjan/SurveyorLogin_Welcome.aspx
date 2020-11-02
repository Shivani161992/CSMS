<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SurveyorLogin_Welcome.aspx.cs" Inherits="SurveyorLogin_Uparjan_SurveyorLogin_Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
            text-decoration:none;
            font-family: 'Bellefair';
            letter-spacing: 2px; 
            font-size: 16px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
         <center>
        <table style="width:100%;">
            <tr>
                <td  style="width:100%; text-align:right">
                  <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="False" ForeColor="black" CssClass="sign" PostBackUrl="#" 
                      CausesValidation="False">Log Out</asp:LinkButton>
                </td>
            </tr>
        </table>
    </center>

   

    <div class="Box">
         <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align:center;"> Contents</h2>
        <table class="surtable">
            <tr>
                <td>
                    <ul style="color:brown;">
                           <li>
                         <asp:HyperLink ID="HyperLink2" CssClass="surveLinks" runat="server" NavigateUrl="~/SurveyorLogin_Uparjan/FarmerQualityInspection.aspx" Font-Bold="True"  ForeColor="brown" > Farmer Quallity Inspection</asp:HyperLink>
                    </li>
                    <li>
                         <asp:HyperLink ID="HyplMvmtPlan" CssClass="surveLinks" runat="server" NavigateUrl="~/SurveyorLogin_Uparjan/SurveyorLogin_QInspection.aspx" Font-Bold="True"  ForeColor="brown" > Truck Inspection</asp:HyperLink>
                    </li>
                         <li>
                         <asp:HyperLink ID="HyperLink1" CssClass="surveLinks" runat="server" NavigateUrl="~/SurveyorLogin_Uparjan/Surveyor_ChangePassword.aspx" Font-Bold="True" ForeColor="brown">Change Password</asp:HyperLink>
                    </li>
                        </ul>
                </td>
            </tr>
        </table>
    </div>
         </form>
</body>
</html>
