<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master"
    AutoEventWireup="true" CodeFile="ChanaMasurReport.aspx.cs" Inherits="State_ChanaMasurReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .Box
        {
           
            width: 100%;
            padding: 40px;
            background: #cccfd038;
            border-radius: 10px;
        }

        .surtable
        {
            width: 500px;
            border-top: 3px inset darkgrey;
            border-bottom: 3px inset darkgrey;
            border-left: 3px inset darkgrey;
            border-right: 3px inset darkgrey;
            border-radius: 32px;
            
        }

        .surveLinks
        {
            text-decoration: none;
            font-family: 'Bellefair';
            letter-spacing: 2px;
            font-size: 14px;
        }

            .surveLinks:hover
            {
                text-decoration: dotted;
                color: #ff6b5b;
            }
    </style>

    <%--   <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="620px">
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="2">
                        चना,मसूर और सरसों रिपोर्ट्स
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        1
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/surveyormasterreport.aspx">SurveyorMaster Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        2
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Rpt_ProcurementToGodownMapping_CSM2018_2019.aspx">Procurement Center Mapping With Godown</asp:LinkButton>
                      
                    </td>
                </tr>

                 <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        3
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Mapping_ChanaSarsoMasoor2018_2019/Rpt_GodownCapacity_CommodityWiseHOCSM2018_2019.aspx">Commodity Wise Godown Capacity</asp:LinkButton>
                      
                    </td>
                </tr>

                 <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        4
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton4" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Mapping_ChanaSarsoMasoor2018_2019/Rpt_GodownUnloadingCapacity_HOCSM2018_2019.aspx">Godown Unloading Capacity</asp:LinkButton>
                      
                    </td>
                </tr>
            </table>--%>

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <div class="container">
       
        <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-align:center"> Chana, Massor and Sarson </h2>
             <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-align:center">  Reports</h2>
             
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home">Master Reports</a></li>
            <li><a data-toggle="tab" href="#menu1">Mapping Reports</a></li>
            <li><a data-toggle="tab" href="#menu2">Transportation Reports</a></li>
            <li><a data-toggle="tab" href="#menu3">Bill Reports</a></li>
        </ul>

        <div class="tab-content">
            <div id="home" class="tab-pane fade in active">
               <%-- <h3>Master Reports</h3>--%>
                  <br />
             
                 <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                   
                       <br />
                        <li style="text-align:left; color:#404B69; ">
                             <asp:LinkButton ID="LinkButton6" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/Reports_State/surveyormasterreport.aspx">Surveyor Master Report</asp:LinkButton>
                        </li>
                         
                       
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>


            </div>
            <div id="menu1" class="tab-pane fade">
                  <br />
                  <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                    <br />
                   <%-- <ul style="color: #404B69;">--%>
                       
                        <li style="text-align:left; color:#404B69; ">
                              <asp:LinkButton ID="LinkButton2" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/Reports_State/Rpt_ProcurementToGodownMapping_CSM2018_2019.aspx">Procurement Center Mapping With Godown</asp:LinkButton>
                        </li>
                         <li style="text-align:left; color:#404B69; ">

                             <asp:LinkButton ID="LinkButton3" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/Mapping_ChanaSarsoMasoor2018_2019/Rpt_GodownCapacity_CommodityWiseHOCSM2018_2019.aspx">Commodity Wise Godown Capacity</asp:LinkButton>
                             </li>
                           <li style="text-align:left; color:#404B69; ">
                               <asp:LinkButton ID="LinkButton4" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/Mapping_ChanaSarsoMasoor2018_2019/Rpt_GodownUnloadingCapacity_HOCSM2018_2019.aspx">Godown Unloading Capacity</asp:LinkButton>
                             </li>
                         
                       
                   <%-- </ul>--%>
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>
               
               
                

            </div>
            <div id="menu2" class="tab-pane fade">
              
                  
             
                <br />
                  <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                    <br />
                  <%--  <ul style="color: #404B69;">--%>
                       
                        <li style="text-align:left; color:#404B69; ">
                           <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="surveLinks" 
                    PostBackUrl="~/Mapping_ChanaSarsoMasoor2018_2019/Rpt_Print_DispatchStockReportCSM.aspx">Dispatch Stock From Society Report</asp:LinkButton>
                        </li>
                         
                       
                  <%--  </ul>--%>
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>
            </div>
                
           
            <div id="menu3" class="tab-pane fade">
               <%-- <h3>Bill Reports</h3>
              --%>
                <br />
                  <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                    <br />
                  <%--  <ul style="color: #404B69;">--%>
                       
                        <li style="text-align:left; color:#404B69; ">
                             <asp:LinkButton ID="LinkButton5" runat="server"  CssClass="surveLinks" 
                    PostBackUrl="~/State/CMS_BillStatus.aspx">Bill Status Report</asp:LinkButton>
                        </li>
                         
                 <%--      
                    </ul>--%>
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>
            </div>
       
    </div>


</asp:Content>
