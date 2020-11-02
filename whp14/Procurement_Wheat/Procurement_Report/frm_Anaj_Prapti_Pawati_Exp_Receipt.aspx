<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Anaj_Prapti_Pawati_Exp_Receipt.aspx.cs" Inherits="WHP14_Procurement_Wheat_Procurement_Report_frm_Anaj_Prapti_Pawati_Exp_Receipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  
   <title>Wheat2014 Procurement Monitoring System</title>

    
    <script type="text/javascript" language="javascript">
     // Tigra Calendar v4.0.2 (12-01-2009) European (dd-mm-yyyy)
// http://www.softcomplex.com/products/tigra_calendar/
// Public Domain Software... You're welcome.
// default settins


var A_TCALDEF = {
	'months' : ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
	'weekdays' : ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
	'yearscroll': true, // show year scroller
	'weekstart': 1, // first day of week: 0-Su or 1-Mo
	'centyear'  : 70, // 2 digit years less than 'centyear' are in 20xx, othewise in 19xx.
	'imgpath' : 'img/' // directory with calendar images
}
var imagepath='../../Images/img/'
//var imagepath='/csms/PPMS/Images/img/'
// '<%= Request.ApplicationPath %>/img/';
//alert('http://localhost/pdsallot/img/');
// date parsing function
function f_tcalParseDate (s_date) {

	var re_date = /^\s*(\d{1,2})\/(\d{1,2})\/(\d{2,4})\s*$/;
	if (!re_date.exec(s_date))
		return alert ("Invalid date: '" + s_date + "'.\nAccepted format is dd-mm-yyyy.")
	var n_day = Number(RegExp.$1),
		n_month = Number(RegExp.$2),
		n_year = Number(RegExp.$3);

	if (n_year < 100)
		n_year += (n_year < this.a_tpl.centyear ? 2000 : 1900);
	if (n_month < 1 || n_month > 12)
		return alert ("Invalid month value: '" + n_month + "'.\nAllowed range is 01-12.");
	var d_numdays = new Date(n_year, n_month, 0);
	if (n_day > d_numdays.getDate())
		return alert("Invalid day of month value: '" + n_day + "'.\nAllowed range for selected month is 01 - " + d_numdays.getDate() + ".");

	return new Date (n_year, n_month - 1, n_day);
}
// date generating function
function f_tcalGenerDate (d_date) {
	return (
		(d_date.getDate() < 10 ? '0' : '') + d_date.getDate() + "/"
		+ (d_date.getMonth() < 9 ? '0' : '') + (d_date.getMonth() + 1) + "/"
		+ d_date.getFullYear()
	);
}

// implementation
function tcal (a_cfg, a_tpl) {

	// apply default template if not specified
	if (!a_tpl)
		a_tpl = A_TCALDEF;

	// register in global collections
	if (!window.A_TCALS)
		window.A_TCALS = [];
	if (!window.A_TCALSIDX)
		window.A_TCALSIDX = [];
	
	this.s_id = a_cfg.id ? a_cfg.id : A_TCALS.length;
	window.A_TCALS[this.s_id] = this;
	window.A_TCALSIDX[window.A_TCALSIDX.length] = this;
	
	// assign methods
	this.f_show = f_tcalShow;
	this.f_hide = f_tcalHide;
	this.f_toggle = f_tcalToggle;
	this.f_update = f_tcalUpdate;
	this.f_relDate = f_tcalRelDate;
	this.f_parseDate = f_tcalParseDate;
	this.f_generDate = f_tcalGenerDate;
	
	// create calendar icon
	this.s_iconId = 'tcalico_' + this.s_id;
	this.e_icon = f_getElement(this.s_iconId);
	if (!this.e_icon) {
	
		document.write('<img src="' + imagepath + 'cal.gif" id="' + this.s_iconId + '" onclick="A_TCALS[\'' + this.s_id + '\'].f_toggle()" class="tcalIcon" alt="Open Calendar" />');
		this.e_icon = f_getElement(this.s_iconId);
		
	}
	// save received parameters
	this.a_cfg = a_cfg;
	this.a_tpl = a_tpl;
}

function f_tcalShow (d_date) {

	// find input field
	if (!this.a_cfg.controlname)
		throw("TC: control name is not specified");
	if (this.a_cfg.formname) {
		var e_form = document.forms[this.a_cfg.formname];
		if (!e_form)
			throw("TC: form '" + this.a_cfg.formname + "' can not be found");
		this.e_input = e_form.elements[this.a_cfg.controlname];
	}
	else
		this.e_input = f_getElement(this.a_cfg.controlname);

	if (!this.e_input || !this.e_input.tagName || this.e_input.tagName != 'INPUT')
		throw("TC: element '" + this.a_cfg.controlname + "' does not exist in "
			+ (this.a_cfg.formname ? "form '" + this.a_cfg.controlname + "'" : 'this document'));

	// dynamically create HTML elements if needed
	this.e_div = f_getElement('tcal');
	if (!this.e_div) {
		this.e_div = document.createElement("DIV");
		this.e_div.id = 'tcal';
		document.body.appendChild(this.e_div);
	}
	this.e_shade = f_getElement('tcalShade');
	if (!this.e_shade) {
		this.e_shade = document.createElement("DIV");
		this.e_shade.id = 'tcalShade';
		document.body.appendChild(this.e_shade);
	}
	this.e_iframe =  f_getElement('tcalIF')
	if (b_ieFix && !this.e_iframe) {
		this.e_iframe = document.createElement("IFRAME");
		this.e_iframe.style.filter = 'alpha(opacity=0)';
		this.e_iframe.id = 'tcalIF';
		this.e_iframe.src = imagepath + 'pixel.gif';
		document.body.appendChild(this.e_iframe);
		
	}
	
	// hide all calendars
	f_tcalHideAll();

	// generate HTML and show calendar
	this.e_icon = f_getElement(this.s_iconId);
	if (!this.f_update())
		return;

	this.e_div.style.visibility = 'visible';
	this.e_shade.style.visibility = 'visible';
	if (this.e_iframe)
		this.e_iframe.style.visibility = 'visible';

	// change icon and status
	this.e_icon.src = imagepath + 'no_cal.gif';
	this.e_icon.title = 'Close Calendar';
	this.b_visible = true;
}

function f_tcalHide (n_date) {
	if (n_date)
		this.e_input.value = this.f_generDate(new Date(n_date));

	// no action if not visible
	if (!this.b_visible)
		return;

	// hide elements
	if (this.e_iframe)
		this.e_iframe.style.visibility = 'hidden';
	if (this.e_shade)
		this.e_shade.style.visibility = 'hidden';
	this.e_div.style.visibility = 'hidden';
	
	// change icon and status
	this.e_icon = f_getElement(this.s_iconId);
	this.e_icon.src = imagepath + 'cal.gif';
	this.e_icon.title = 'Open Calendar';
	this.b_visible = false;
}

function f_tcalToggle () {
	return this.b_visible ? this.f_hide() : this.f_show();
}

function f_tcalUpdate (d_date) {

	var d_today = this.a_cfg.today ? this.f_parseDate(this.a_cfg.today) : f_tcalResetTime(new Date());
	var d_selected = this.e_input.value == ''
		? (this.a_cfg.selected ? this.f_parseDate(this.a_cfg.selected) : d_today)
		: this.f_parseDate(this.e_input.value);

	// figure out date to display
	if (!d_date)
		// selected by default
		d_date = d_selected;
	else if (typeof(d_date) == 'number')
		// get from number
		d_date = f_tcalResetTime(new Date(d_date));
	else if (typeof(d_date) == 'string')
		// parse from string
		this.f_parseDate(d_date);
		
	if (!d_date) return false;

	// first date to display
	var d_firstday = new Date(d_date);
	d_firstday.setDate(1);
	d_firstday.setDate(1 - (7 + d_firstday.getDay() - this.a_tpl.weekstart) % 7);
	
	var a_class, s_html = '<table class="ctrl"><tbody><tr>'
		+ (this.a_tpl.yearscroll ? '<td' + this.f_relDate(d_date, -1, 'y') + ' title="Previous Year"><img src="' + imagepath + 'prev_year.gif" /></td>' : '')
		+ '<td' + this.f_relDate(d_date, -1) + ' title="Previous Month"><img src="' + imagepath + 'prev_mon.gif" /></td><th>'
		+ this.a_tpl.months[d_date.getMonth()] + ' ' + d_date.getFullYear()
			+ '</th><td' + this.f_relDate(d_date, 1) + ' title="Next Month"><img src="' + imagepath + 'next_mon.gif" /></td>'
		+ (this.a_tpl.yearscroll ? '<td' + this.f_relDate(d_date, 1, 'y') + ' title="Next Year"><img src="' + imagepath + 'next_year.gif" /></td></td>' : '')
		+ '</tr></tbody></table><table><tbody><tr class="wd">';

	// print weekdays titles
	for (var i = 0; i < 7; i++)
		s_html += '<th>' + this.a_tpl.weekdays[(this.a_tpl.weekstart + i) % 7] + '</th>';
	s_html += '</tr>' ;

	// print calendar table
	var n_date, n_month, d_current = new Date(d_firstday);
	while (d_current.getMonth() == d_date.getMonth() ||
		d_current.getMonth() == d_firstday.getMonth()) {
	
		// print row heder
		s_html +='<tr>';
		for (var n_wday = 0; n_wday < 7; n_wday++) {

			a_class = [];
			n_date = d_current.getDate();
			n_month = d_current.getMonth();

			// other month
			if (d_current.getMonth() != d_date.getMonth())
				a_class[a_class.length] = 'othermonth';
			// weekend
			if (d_current.getDay() == 0 || d_current.getDay() == 6)
				a_class[a_class.length] = 'weekend';
			// today
			if (d_current.valueOf() == d_today.valueOf())
				a_class[a_class.length] = 'today';
			// selected
			if (d_current.valueOf() == d_selected.valueOf())
				a_class[a_class.length] = 'selected';

			s_html += '<td onclick="A_TCALS[\'' + this.s_id + '\'].f_hide(' + d_current.valueOf() + ')"' + (a_class.length ? ' class="' + a_class.join(' ') + '">' : '>') + n_date + '</td>'

			d_current.setDate(++n_date);
			while (d_current.getDate() != n_date && d_current.getMonth() == n_month) {
				alert(n_date + "\n" + d_current + "\n" + new Date());
				d_current.setHours(d_current.getHours + 1);
				d_current = f_tcalResetTime(d_current);
			}
		}
		// print row footer
		s_html +='</tr>';
	}
	s_html +='</tbody></table>';
	
	// update HTML, positions and sizes
	this.e_div.innerHTML = s_html;

	var n_width  = this.e_div.offsetWidth;
	var n_height = this.e_div.offsetHeight;
	var n_top  = f_getPosition (this.e_icon, 'Top') + this.e_icon.offsetHeight;
	//var n_left = f_getPosition (this.e_icon, 'Left') - n_width + this.e_icon.offsetWidth;
	var n_left = f_getPosition (this.e_icon, 'Left') + this.e_icon.offsetWidth;
	if (n_left < 0) n_left = 0;
	
	this.e_div.style.left = n_left + 'px';
	this.e_div.style.top  = n_top + 'px';

	this.e_shade.style.width = (n_width + 8) + 'px';
	this.e_shade.style.left = (n_left - 1) + 'px';
	this.e_shade.style.top = (n_top - 1) + 'px';
	this.e_shade.innerHTML = b_ieFix
		? '<table><tbody><tr><td rowspan="2" colspan="2" width="6"><img src="' + imagepath + 'pixel.gif"></td><td width="7" height="7" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'' + imagepath + 'shade_tr.png\', sizingMethod=\'scale\');"><img src="' + imagepath + 'pixel.gif"></td></tr><tr><td height="' + (n_height - 7) + '" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'' + imagepath + 'shade_mr.png\', sizingMethod=\'scale\');"><img src="' + imagepath + 'pixel.gif"></td></tr><tr><td width="7" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'' + imagepath + 'shade_bl.png\', sizingMethod=\'scale\');"><img src="' + imagepath + 'pixel.gif"></td><td style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'' + imagepath + 'shade_bm.png\', sizingMethod=\'scale\');" height="7" align="left"><img src="' + imagepath + 'pixel.gif"></td><td style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'' + imagepath + 'shade_br.png\', sizingMethod=\'scale\');"><img src="' + imagepath + 'pixel.gif"></td></tr><tbody></table>'
		: '<table><tbody><tr><td rowspan="2" width="6"><img src="' + imagepath + 'pixel.gif"></td><td rowspan="2"><img src="' + imagepath + 'pixel.gif"></td><td width="7" height="7"><img src="' + imagepath + 'shade_tr.png"></td></tr><tr><td background="' + imagepath + 'shade_mr.png" height="' + (n_height - 7) + '"><img src="' + imagepath + 'pixel.gif"></td></tr><tr><td><img src="' + imagepath + 'shade_bl.png"></td><td background="' + imagepath + 'shade_bm.png" height="7" align="left"><img src="' + imagepath + 'pixel.gif"></td><td><img src="' + imagepath + 'shade_br.png"></td></tr><tbody></table>';
	
	if (this.e_iframe) {
		this.e_iframe.style.left = n_left + 'px';
		this.e_iframe.style.top  = n_top + 'px';
		this.e_iframe.style.width = (n_width + 6) + 'px';
		this.e_iframe.style.height = (n_height + 6) +'px';
	}
	return true;
}

function f_getPosition (e_elemRef, s_coord) {
	var n_pos = 0, n_offset,
		e_elem = e_elemRef;

	while (e_elem) {
		n_offset = e_elem["offset" + s_coord];
		n_pos += n_offset;
		e_elem = e_elem.offsetParent;
	}
	// margin correction in some browsers
	if (b_ieMac)
		n_pos += parseInt(document.body[s_coord.toLowerCase() + 'Margin']);
	else if (b_safari)
		n_pos -= n_offset;
	
	e_elem = e_elemRef;
	while (e_elem != document.body) {
		n_offset = e_elem["scroll" + s_coord];
		if (n_offset && e_elem.style.overflow == 'scroll')
			n_pos -= n_offset;
		e_elem = e_elem.parentNode;
	}
	return n_pos;
}

function f_tcalRelDate (d_date, d_diff, s_units) {
	var s_units = (s_units == 'y' ? 'FullYear' : 'Month');
	var d_result = new Date(d_date);
	d_result['set' + s_units](d_date['get' + s_units]() + d_diff);
	if (d_result.getDate() != d_date.getDate())
		d_result.setDate(0);
	return ' onclick="A_TCALS[\'' + this.s_id + '\'].f_update(' + d_result.valueOf() + ')"';
}

function f_tcalHideAll () {
	for (var i = 0; i < window.A_TCALSIDX.length; i++)
		window.A_TCALSIDX[i].f_hide();
}	

function f_tcalResetTime (d_date) {
	d_date.setHours(0);
	d_date.setMinutes(0);
	d_date.setSeconds(0);
	d_date.setMilliseconds(0);
	return d_date;
}

f_getElement = document.all ?
	function (s_id) { return document.all[s_id] } :
	function (s_id) { return document.getElementById(s_id) };

if (document.addEventListener)
	window.addEventListener('scroll', f_tcalHideAll, false);
if (window.attachEvent)
	window.attachEvent('onscroll', f_tcalHideAll);
	
// global variables
var s_userAgent = navigator.userAgent.toLowerCase(),
	re_webkit = /WebKit\/(\d+)/i;
var b_mac = s_userAgent.indexOf('mac') != -1,
	b_ie5 = s_userAgent.indexOf('msie 5') != -1,
	b_ie6 = s_userAgent.indexOf('msie 6') != -1 && s_userAgent.indexOf('opera') == -1;
var b_ieFix = b_ie5 || b_ie6,
	b_ieMac  = b_mac && b_ie5,
	b_safari = b_mac && re_webkit.exec(s_userAgent) && Number(RegExp.$1) < 500;

     
      </script>

  
  <link href="../../CSS/calendar.css" rel="stylesheet" type="text/css" />

  

    <script type="text/javascript">

function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('print.htm','PrintWindow','letf=0,top=0,width=800%,height=600,toolbar=1,scrollbars=1,status=1');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
}

    </script>

    <style type="text/css">
        @media print
        {
            .nav
            {
                display: none;
            }
        }
    </style>

    <script type="text/javascript">
    window.history.forward(0); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%;">
                    <table border="1" cellpadding="0" cellspacing="0" height="500px" width="100%">
                        <tr>
                            <td valign="top">
                                <table cellpadding="0" cellspacing="0" style="background-color: #ecf5d5; width: 100%; border: double; border-color: #868f6f" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
                                    <tr>
                                        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; height: 20px; text-align: center;" colspan="3">
                                            अनाज खरीदी पावती</td>
                                        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; height: 20px; text-align: right; width: 30%">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पिछले पृष्ठ पर जाये</asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 9pt; width: 20%; color: #595f4a; font-family: Verdana; height: 20px; text-align: center">
                                            प्राप्ति दिनांक</td>
                                        <td style="font-weight: bold; font-size: 9pt; width: 27%; color: #595f4a; font-family: Verdana; height: 20px; text-align: left">
                                            <asp:TextBox ID="txtPR_Date" runat="server"></asp:TextBox>

                                            <script type="text/javascript">
	new tcal ({
				'formname': 'form1',
				'controlname': 'txtPR_Date'
	    });
function TABLE1_onclick() {

}

                                            </script>

                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtPR_Date" Display="Dynamic" ErrorMessage="दिनांक dd/MM/yyyy फोर्मेट मे ही चुने" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True" ValidationGroup="vg">*</asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPR_Date" Display="Dynamic" ErrorMessage="प्राप्ति दिनांक का चयन करे" ValidationGroup="vg">*</asp:RequiredFieldValidator></td>
                                        <td style="font-weight: bold; font-size: 9pt; width: 40%; color: #595f4a; font-family: Verdana; height: 20px; text-align: left">
                                            <asp:RadioButton ID="rb9" runat="server" AutoPostBack="True" GroupName="gg" OnCheckedChanged="rb9_CheckedChanged" Text="डी.एम.पी - 9 पिन" Checked="True" />
                                            <asp:RadioButton ID="rb12" runat="server" AutoPostBack="True" GroupName="gg" Text="डी.एम.पी - 24 पिन" OnCheckedChanged="rb12_CheckedChanged" />&nbsp;
                                            <asp:Button ID="btn_Search" runat="server" Height="32px" Text="ढूंढे" Width="120px" OnClick="btn_Search_Click" ValidationGroup="vg" /></td>
                                        <td style="font-weight: bold; font-size: 9pt; width: 30%; color: #595f4a; font-family: Verdana; height: 20px; text-align: right">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" Font-Bold="True" Font-Size="1px" ShowMessageBox="True" ValidationGroup="vg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 9pt; width: 20%; color: #595f4a; font-family: Verdana; height: 20px; text-align: center">
                                            प्राप्ति क्र.</td>
                                        <td colspan="2" style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; height: 20px; text-align: left">
                                            <asp:DropDownList ID="ddl_PR_No" runat="server" Width="344px" AutoPostBack="True" OnSelectedIndexChanged="ddl_PR_No_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="font-weight: bold; font-size: 9pt; width: 30%; color: #595f4a; font-family: Verdana; height: 20px; text-align: right">
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel runat="server" ID="pn" Visible="false">
                                    <div id="divPrint">
                                        <table style="width: 900px; background-position: center center; background-repeat: no-repeat; border-top-width: 1px; border-left-width: 1px; border-left-color: black; border-bottom-width: 1px; border-bottom-color: black; border-top-color: black; border-right-width: 1px; border-right-color: black;" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 274px; height: 35px; font-size: 14px; font-weight: bold; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;" align="center">
                                                    खरीदी पावती</td>
                                                <td style="width: 624px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 14px; border-left: black 1px solid; width: 274px; height: 20px">
                                                    <asp:Label ID="lbltdate" runat="server"></asp:Label></td>
                                                <td style="width: 624px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 274px; border-right: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                                    <asp:DataList ID="DataList1" runat="server">
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 274px; font-size: 12px;">
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;जिला :&nbsp;<asp:Label ID="lblDistrict_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"District_Name") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;उपार्जन केंद्र :&nbsp;<asp:Label ID="lblSociety_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Society_Name") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;प्राप्ति क्र.:&nbsp;<asp:Label ID="lblReceivedID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReceivedID") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;खरीदी दिनांक :&nbsp;<asp:Label ID="lblDate_Of_Receipt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Date_Of_Receipt") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;ऋण पुस्तिका क्र./ वनाधिकारी पट्टा न. :&nbsp;<asp:Label ID="lblRinPustikaNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RinPustikaNo") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp;किसान कोड :&nbsp;<asp:Label ID="lblFarmer_Id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_Id") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;किसान का नाम :&nbsp;<asp:Label ID="lblFarmerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FarmerName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;पिता /पति का नाम :&nbsp;<asp:Label ID="lblFatherHusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FatherHusName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;मोबाइल नं.:&nbsp;<asp:Label ID="lblMobileno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Mobileno") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;गाँव /वार्ड का नाम :&nbsp;<asp:Label ID="lblVillageName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VillageName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;बैंक का नाम :&nbsp;<asp:Label ID="lblFarmer_BankName_New" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankName_New") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;बैंक शाखा का नाम :&nbsp;<asp:Label ID="lblFarmer_BankBranchName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankBranchName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;बैंक खाता क्र.:&nbsp;<asp:Label ID="lblFarmer_BankAccountNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankAccountNo") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;तोल पत्रक क्र.:&nbsp;<asp:Label ID="lblTaulPatrakNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaulPatrakNo") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 274px; border-top: black 1px solid; border-bottom: black 1px solid; font-size: 12px;">
                                                                            <tr>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    अनाज</td>
                                                                                <td colspan="2" style="width: 50px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    उपार्जित मात्रा
                                                                                </td>
                                                                                <td colspan="3" style="width: 75px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    राशि(रु.में)</td>
                                                                                <td style="width: 34px; height: 50px; border-bottom: black 1px solid;" align="center">
                                                                                    भुगतान योग्य राशि राशि(रु.में)</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; border-left-width: 1px; border-left-color: black;" align="center">
                                                                                    बोरी
                                                                                </td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    वजन(क्वी.में)</td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    समर्थन मूल्य</td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    केंद्रीय बोनस</td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    राज्य बोनस</td>
                                                                                <td style="width: 50px; height: 25px; border-bottom: black 1px solid;" align="center">
                                                                                    कुल भुगतान</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-top-width: 1px; border-top-color: black;" align="left">
                                                                                    <asp:Label ID="lblcrop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"crop") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblBags" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Bags") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblQtyReceived" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"QtyReceived") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblMSPRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MSPRate") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblCentralBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CentralBonus") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblStateBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StateBonus") %>'></asp:Label></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblTotaAmountPayableToFarmer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotaAmountPayableToFarmer") %>'></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 274px">
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;सोसाइटी ऋण के विरुद्ध वसूली गयी राशि</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblAmtAgainstSCCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstSCCredit") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;जिला केन्द्रीय सहकारी बैंक ऋण के विरुद्ध वसूली गयी राशि</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblAmtAgainstBankCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstBankCredit") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;सिंचाई विभाग के बकाया के विरुद्ध वसूली गयी राशि</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblAmtAgIrg_Loan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgIrg_Loan") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;कुल वसूली राशि (रु.मे)</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblamt" runat="server"></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 700; font-size: 10pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;शुद्ध भुगतान (रु.मे)</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lbltotamt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NetAmountPayableToFarmer") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp; नोट : इस रसीद के जारी होने के 7 दिन के भीतर आपके बैंक खाते मे भुगतान की राशि भेज दी जायेगी |</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 60px; font-weight: bold;" valign="top">
                                                                        &nbsp; किसान के हस्ताक्षर /अंगूठे का निशान
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 60px; font-weight: bold;" valign="top">
                                                                        &nbsp; उपार्जन केन्द्र प्रबंधक के हस्ताक्षर</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp; अधिक जानकारी के लिए टोल फ्री न. पर संपर्क करे - 155343<br />
                                                                        &nbsp; खाद्य, नागरिक आपूर्ति एवं उपभोक्ता संरक्षण विभाग म.प्र.</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp;
                                                                        <asp:Label ID="lbldate" runat="server"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList></td>
                                                <td style="width: 624px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 274px">
                                                </td>
                                                <td style="width: 624px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 900px;">
                                <asp:Panel runat="server" ID="pn1" Visible="false">
                                 <div id="divPrint1">
                                    <table style="width: 900px; background-position: center center; background-repeat: no-repeat; border-top-width: 1px; border-left-width: 1px; border-left-color: black; border-bottom-width: 1px; border-bottom-color: black; border-top-color: black; border-right-width: 1px; border-right-color: black;" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 900px; height: 35px; font-size: 18px; font-weight: bold; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" align="center" colspan="2">
                                                खरीदी पावती</td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" colspan="2">
                                                <asp:DataList ID="DataList2" runat="server">
                                                    <ItemTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="font-size: 14px; width: 900px">
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;जिला :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblDistrict_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"District_Name") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px" align="right">
                                                                    खरीदी दिनांक :</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    &nbsp;<asp:Label ID="lblDate_Of_Receipt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Date_Of_Receipt") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;उपार्जन केंद्र :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblSociety_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Society_Name") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 112px; height: 35px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;प्राप्ति क्र.:&nbsp;</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblReceivedID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReceivedID") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px; font-weight: bold;">
                                                                    किसान कोड :</td>
                                                                <td style="width: 173px; height: 35px; font-weight: bold; font-family: Arial;">
                                                                    <asp:Label ID="lblFarmer_Id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_Id") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 112px; height: 35px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;किसान का नाम :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFarmerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FarmerName") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    पिता /पति का नाम :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFatherHusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FatherHusName") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    मोबाइल नं.:</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblMobileno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Mobileno") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;गाँव /वार्ड का नाम :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblVillageName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VillageName") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    बैंक का नाम :&nbsp;</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFarmer_BankName_New" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankName_New") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    बैंक शाखा का नाम :</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblFarmer_BankBranchName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankBranchName") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;बैंक खाता क्र.:&nbsp;</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFarmer_BankAccountNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankAccountNo") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    ऋण पुस्तिका क्र./ वनाधिकारी पट्टा न. :&nbsp;</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblRinPustikaNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RinPustikaNo") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    तोल पत्रक क्र.:&nbsp;</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblTaulPatrakNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaulPatrakNo") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 900px; height: 35px" colspan="8">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px; border-top: black 1px solid; border-bottom: black 1px solid; font-size: 14px;">
                                                                        <tr>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                अनाज</td>
                                                                            <td colspan="2" style="width: 200px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                उपार्जित मात्रा
                                                                            </td>
                                                                            <td colspan="3" style="width: 200px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                राशि(रु.में)</td>
                                                                            <td style="width: 300px; height: 50px; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                भुगतान योग्य राशि राशि(रु.में)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; border-left-width: 1px; border-left-color: black; font-weight: bold;" align="center">
                                                                                बोरी
                                                                            </td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                वजन(क्वी.में)</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                समर्थन मूल्य</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                केंद्रीय बोनस</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                राज्य बोनस</td>
                                                                            <td style="width: 300px; height: 25px; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                कुल भुगतान</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-top-width: 1px; border-top-color: black;" align="center">
                                                                                <asp:Label ID="lblcrop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"crop") %>'></asp:Label></td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblBags" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Bags") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblQtyReceived" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"QtyReceived") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblMSPRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MSPRate") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblCentralBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CentralBonus") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblStateBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StateBonus") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 300px; height: 25px" align="right">
                                                                                <asp:Label ID="lblTotaAmountPayableToFarmer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotaAmountPayableToFarmer") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="width: 900px; height: 35px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px; font-size: 16px;">
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;सोसाइटी ऋण के विरुद्ध वसूली गयी राशि</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblAmtAgainstSCCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstSCCredit") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;जिला केन्द्रीय सहकारी बैंक ऋण के विरुद्ध वसूली गयी राशि</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblAmtAgainstBankCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstBankCredit") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;सिंचाई विभाग के बकाया के विरुद्ध वसूली गयी राशि</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblAmtAgIrg_Loan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgIrg_Loan") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;कुल वसूली राशि (रु.मे)</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblamt" runat="server"></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px; font-weight: bold; border-top: black 1px solid; border-bottom: black 1px solid;">
                                                                                &nbsp;शुद्ध भुगतान (रु.मे)</td>
                                                                            <td style="width: 200px; height: 35px; font-weight: bold; border-top: black 1px solid; border-bottom: black 1px solid; font-family: Arial;" align="right">
                                                                                <asp:Label ID="lbltotamt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NetAmountPayableToFarmer") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="width: 900px; height: 35px; font-weight: bold; border-bottom: black 1px solid;">
                                                                    &nbsp;नोट : इस रसीद के जारी होने के 7 दिन के भीतर आपके बैंक खाते मे भुगतान की राशि भेज दी जायेगी |</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="width: 900px; height: 35px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px">
                                                                        <tr>
                                                                            <td style="width: 300px; height: 100px; font-weight: bold;" valign="top">
                                                                                &nbsp; किसान के हस्ताक्षर /अंगूठे का निशान</td>
                                                                            <td style="width: 300px; height: 100px">
                                                                            </td>
                                                                            <td align="right" style="width: 300px; height: 100px; font-weight: bold;" valign="top">
                                                                                उपार्जन केन्द्र प्रबंधक के हस्ताक्षर &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 300px; height: 35px">
                                                                                &nbsp; खाद्य, नागरिक आपूर्ति एवं उपभोक्ता संरक्षण विभाग म.प्र.</td>
                                                                            <td align="center" style="width: 300px; height: 35px">
                                                                                अधिक जानकारी के लिए टोल फ्री न. पर संपर्क करे - 155343<br />
                                                                            </td>
                                                                            <td align="right" style="width: 300px; height: 35px" valign="top">
                                                                                <asp:Label ID="lbldate" runat="server"></asp:Label>&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 300px; height: 35px">
                                                                            </td>
                                                                            <td align="center" style="width: 300px; height: 35px">
                                                                            </td>
                                                                            <td align="right" style="width: 300px; height: 35px">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 274px">
                                            </td>
                                            <td style="width: 624px;">
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Button ID="Button1" runat="server" Text="Print" />
                                 <asp:Button ID="Button2" runat="server" Text="Print" /></td>
                        </tr>
                    </table>
                    <table border="1" cellpadding="0" cellspacing="0" height="30px" width="100%" style="background-color: #9ca782">
                        <tr>
                            <td height="20px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
