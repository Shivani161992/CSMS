/* 
	the format of the tree definition file is simple,
		you can find specification in the Tigra Menu documentation at:	
*/
var TREE_ITEMS = [
['Home', '', 
	['State Level',null,
		['Creation of Allotment Letter', 'StateLevel/StateAllotmentLetter.aspx']
	],

	['District Level', null,
		['Allotment Letter by DM', 'DistrictLevel/DistrictAlotmentLetter.aspx'],
		['Demand Note to FCI', 'DistrictLevel/DemandNote.aspx'],
		['Release Order', 'DistrictLevel/ReleaseOrder.aspx'],
		['Delivery Order', 'DistrictLevel/DeleveryOrder.aspx']
	],

	['Issue Centre Level', null,
		['Receipt', null,
			['Modes of Receipt', 'IssueCenterLevel/Receipts/ModeOfReceipt.aspx'],
			['Creation of WHR', 'IssueCenterLevel/Receipts/Create_whr.aspx']
		],
		['Issue', null,
			['Issue Against Delivery Order', 'IssueCenterLevel/Issue/IssueAtIssueCenter.aspx'],
			['Stock Transfer', 'IssueCenterLevel/Issue/StockTransfer.aspx'],
			['Inter Scheme Transfer', 'Message.aspx']
			//['Inter Scheme Transfer', 'Issue_Centre_Level/InterSchemeTransfer.html']
		],
		['Made-up Release Bags', 'IssueCenterLevel/MadeUpRelease.aspx'],
		['Internal Movement', 'IssueCenterLevel/InternalMovement.aspx'],
		['Delivery Order', 'Message.aspx']
		//['Delivery Order', 'Issue_Centre_Level/DeliveryOrder_IssueCentre.html']
	],
	
	['Masters', null,
		['Category', 'Masters/listCategory.aspx'],
		['Commodity', 'Masters/listCommodity.aspx'],
		['Procurement Agency', 'Masters/listProcurementagency1.aspx'],
		['Transporter', 'Masters/listTransporter.aspx'],
		['Scheme', 'Masters/listScheme.aspx'],
		['Sugar Mill', 'Masters/listSugarMill.aspx']
		
	],
['Reports', 'repindex.htm',

	],
	['Logout', 'Default.aspx',
	],
]
];
