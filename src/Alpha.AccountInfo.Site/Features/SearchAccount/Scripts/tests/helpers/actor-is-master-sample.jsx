const accountInfoUrl = 'site-accountinfo';

export const Master = {
    ViewAgentInfo: {
        'CustId': 36204213,
        'RoleId': 2,
        'ActorRoleId': 3,
        'UserName': 'davidtest13May0101',
        'NickName': '',
        'ShowNickName': false,
        'FullName': ' ',
        'Phone': '',
        'Fax': '',
        'Closed': false,
        'ClosedReason': 0,
        'Suspended': true,
        'Enabled': true,
        'ShowMember': false,
        'MemberName': '',
        'ShowAgent': true,
        'AgentId': 36204213,
        'AgentName': 'davidtest13May0101',
        'ShowMaster': false,
        'MasterId': 36204210,
        'MasterName': 'davidtest13May01',
        'SuperId': 36204209,
        'SuperName': 'davidtest13May',
        'LoginInfo': {
            'CreatedDate': '5/12/2019 10:29:00 PM',
            'LastLogin': '',
            'LastTxn': '',
            'LoginIP': ''
        },
        'BalanceInfo': {
            'TotalCash': 0,
            'YesterdayCash': 0,
            'TotalBalance': 0,
            'YesterdayBalance': 0,
            'Outstanding': 0,
            'MemberMaxCredit': 0,
            'UsedCredit': 0,
            'GivenCredit': 0,
            'ShowMemberMaxCreditLimit': true,
            'MemberMaxCreditLimit': '>=0.0000 and <=0.00',
            'GivenCreditMin': 0,
            'GivenCreditMax': 0,
            'ShowOutstandingLink': false,
            'ReportLink': {
                'Text': 'Agent Outstanding',
                'Url': '/site-reports/outstanding/Agent?CustId=36204213&CustName=davidtest13May0101' +
                    '&CustLevelId=2&BreadcrumbLevelJson=[{"CustId":36204210,"CustName":"davidtest13May01","CustLevelId":3}]'
            },
            'ShowTotalBalanceTransferLink': false,
            'TotalBalanceTransferLink': '/ex-main/_Transfer/Master/AgentTransfer.aspx?custid=36204213&status=1' +
                '&CallBack=false&username=davidtest13May0101&isyesterday=1&amt=0.00',
            'ShowYesterdayBalanceTransferLink': false,
            'YesterdayBalanceTransferLink': '/ex-main/_Transfer/Master/AgentTransfer.aspx?custid=36204213' +
                '&status=1&CallBack=false&username=davidtest13May0101&isyesterday=1&amt=0.00',
            'CanUpdateAccount': true,
            'AccountInfoUrl': accountInfoUrl
        },
        'Buttons': [
            {
                'Text': 'Member List',
                'Url': '/ex-main/_MemberInfo/CustomerList/Master/MemberList.aspx?custid=36204213'
            },
            {
                'Text': 'Statement',
                'Url': '/site-reports/Statement/SMAStatement?CustID=36204213&CustName=davidtest13May0101&CustLevelId=2'
            }
        ],
        'CanViewLog': true,
        'ViewLogLink': '/site-viewlogs/setting/index?username=davidtest13May0101',
        'ViewLoginIPLink': '/ex-main/_IPInfo/IpInfo.aspx',
        'AccountInfoUrl': accountInfoUrl
    },
    ViewMemberInfo: {
        'CustId': 36204216,
        'RoleId': 1,
        'ActorRoleId': 3,
        'UserName': 'davidtest13May0101001',
        'NickName': '',
        'ShowNickName': false,
        'FullName': ' ',
        'Phone': '',
        'Fax': '',
        'Closed': true,
        'ClosedReason': 2,
        'Suspended': true,
        'Enabled': true,
        'ShowMember': true,
        'MemberName': 'davidtest13May0101001',
        'ShowAgent': true,
        'AgentId': 36204213,
        'AgentName': 'davidtest13May0101',
        'ShowMaster': false,
        'MasterId': 36204210,
        'MasterName': 'davidtest13May01',
        'SuperId': 36204209,
        'SuperName': 'davidtest13May',
        'LoginInfo': {
            'CreatedDate': '5/12/2019 10:30:00 PM',
            'LastLogin': '',
            'LastTxn': '',
            'LoginIP': 'null'
        },
        'BalanceInfo': {
            'TotalBalance': 0,
            'YesterdayBalance': 0,
            'Outstanding': 0,
            'NetPosition': 0,
            'GivenCredit': 0,
            'BetCredit': 0,
            'GivenCreditMax': 0,
            'ShowOutstandingLink': false,
            'ReportLink': {
                'Text': 'Outstanding',
                'Url': '/site-betlists/RunningBetList/Index?CustID=36204216&amp;CustName=ACGUUS3838033&amp;GetAll=True'
            },
            'ShowTotalBalanceTransferLink': false,
            'TotalBalanceTransferLink': '/ex-main/_Transfer/Agent/MemberTransfer.aspx?custid=36204216&CallBack=false',
            'ShowYesterdayBalanceTransferLink': false,
            'YesterdayBalanceTransferLink': '/ex-main/_Transfer/Agent/MemberTransfer.aspx?custid=36204216&CallBack=false',
            'CanUpdateAccount': true,
            'AccountInfoUrl': accountInfoUrl
        },
        'Buttons': [
            {
                'Text': 'Bet List',
                'Url': '/site-betlists/MemberBetList/Index?CustID=36204216&CustName=davidtest13May0101001'
            },
            {
                'Text': 'Statement',
                'Url': '/site-reports/Statement/MemberStatement?CustID=36204216&CustName=davidtest13May0101001&CustLevelId=1'
            }
        ],
        'CanViewLog': true,
        'ViewLogLink': '/site-viewlogs/setting/index?username=davidtest13May0101001',
        'ViewLoginIPLink': '/ex-main/_IPInfo/IpInfo.aspx',
        'AccountInfoUrl': accountInfoUrl
    }
};
