<%@ Page Title="" Language="C#" MasterPageFile="~/CSMSSurveyorLogin/Godown_SurveyorMaster.master" AutoEventWireup="true" CodeFile="CMS_ReprintQualityInspection.aspx.cs" Inherits="CSMSSurveyorLogin_CMS_ReprintQualityInspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }

        .QIParametersColumn
        {
            border-radius: 8px;
        }

        .auto-styleNSC
        {
            width: 622px;
        }

        .InspecTable
        {
            width: 100%;
        }

        .InspHead
        {
        }

        .InspColumn
        {
            width: 33%;
            color: #10321f;
            letter-spacing: 2px;
            font-family: Almendra;
            font-size: 13px;
            font-weight: bold;
        }

        .insptxt
        {
            width: 300px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: brown;
            border: 1px solid brown;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
            border-bottom-style: groove;
        }

        .insfixtext
        {
            width: 310px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
            border-bottom-style: groove;
        }

        .insptxt:focus
        {
            border: none;
            border-color: brown;
            box-shadow: 0 0 10px brown;
            outline: none;
        }

        .inspddl
        {
            width: 310px;
            height: 24px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: brown;
            border: 1px solid brown;
            border-radius: 8px;
            padding-left: 10px;
            border-bottom-style: groove;
        }

            .inspddl:focus
            {
                box-shadow: 0 0 10px brown;
                outline: none;
            }

        .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #3498db;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            height: 30px;
            width: 400px;
        }

            .bttsub:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .bttsubother
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #00AAA0;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            height: 30px;
            width: 150px;
        }

            .bttsubother:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }

        .insptxtPara
        {
            width: 150px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            border-bottom-style: groove;
            text-align: center;
        }

            .insptxtPara:focus
            {
                border: none;
                border-bottom-color: #070b30;
            }
    </style>
    <center>
        <table style="width: 100%; font-size: 12px;">
            <tr>
                <td colspan="3">

                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 10%">
                    <a href="CSMS_SurveyorLogin_Welcome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 80%">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px;"> Reprint Godown Surveyor Quality Inspection</h2>
                   




                   
                   

                   
                   
                </td>

                <td style="text-align: right; width: 10%">
                    <a href="CMS_ReprintQualityInspection.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>
        </table>
          <table class="InspecTable">

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

            <tr runat="server" id="trNumber" visible="false">
                <td colspan="3" style="height: 10px;">
                    <center>
                        <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 20px;" ForeColor="Blue" Visible="False"></asp:Label>
                    </center>
                </td>
            </tr>
               <tr>
                    <td class="InspColumn" style="padding-left: 20px; text-align: center">
                   Godown
                    <br />

                  <asp:DropDownList ID="ddlGodown" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlGodown" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                    <td class="InspColumn" style="padding-left: 20px; text-align: center">Truck Challan
                    <br />

                    <asp:DropDownList ID="ddlTruckChallan" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlTruckChallan_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlTruckChallan" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: center">Rejection Number
                    <br />

                    <asp:TextBox ID="txtRejectionNum" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtRejectionNum" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
               
               
            </tr>
               <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

               <tr>
                <td colspan="3" style="height: 10px; text-align:center;">

                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnPrint_Click" />
                </td>
            </tr>


</asp:Content>

