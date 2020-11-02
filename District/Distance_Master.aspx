<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Distance_Master.aspx.cs" Inherits="District_Doorstep_Selection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 647px; height: 163px; border-right: maroon thin groove; border-top: maroon thin groove; border-left: maroon thin groove; border-bottom: maroon thin groove;">
        
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; font-size: larger; background-color: #99CCFF;" 
                valign = "top" colspan="2">
                <strong>Distance master </strong> </td>
        </tr>
        
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove;" 
                valign = "top" colspan="2">
                &nbsp;</td>
        </tr>
        
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; width: 277px;" 
                valign = "top">
                <br />
                <asp:RadioButton ID="rdDist_to_IC" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdDist_to_IC_CheckedChanged" 
                    Text="जिले के बेस  डिपो से संबंध संग्रहण केन्द्रो की दूरी हेतू" 
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" />
                <br />
                </td>
            <td style="width: 266px; height: 19px; border-bottom: blue thin groove;" 
                valign = "top">
                <br />
                <asp:RadioButton ID="rd_ICto_FPS" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rd_ICto_FPS_CheckedChanged" 
                    Text="प्रदाय केंद्र से  उचित मूल्य दूकान से सम्बंधित दूरी हेतू" 
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" /></td>
        </tr>
    </table>
</asp:Content>

