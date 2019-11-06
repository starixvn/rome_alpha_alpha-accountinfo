const accountInfoUrl = 'site-accountinfo';

export const Agent = {
    ViewMemberInfo: {
        'CustId': 1382345,
        'RoleId': 1,
        'ActorRoleId': 2,
        'UserName': 'ACGUUS3838037',
        'NickName': '',
        'ShowNickName': false,
        'FullName': '--- ---',
        'Phone': '-',
        'Fax': '-',
        'Closed': false,
        'ClosedReason': 1,
        'Suspended': false,
        'Enabled': true,
        'ShowMember': true,
        'MemberName': 'ACGUUS3838037',
        'ShowAgent': false,
        'AgentId': 1382167,
        'AgentName': 'ACGUUS3838',
        'ShowMaster': false,
        'MasterId': 1382145,
        'MasterName': 'ACGUUS38',
        'SuperId': 615976,
        'SuperName': 'ACGUUS',
        'LoginInfo': {
            'CreatedDate': '3/23/2009 8:27:00 AM',
            'LastLogin': '6/5/2019 2:02:00 AM',
            'LastTxn': '6/1/2019 12:00:00 AM',
            'LoginIP': '192.168.1.1'
        },
        'BalanceInfo': {
            'TotalBalance': -4809,
            'YesterdayBalance': -4809,
            'Outstanding': 0,
            'NetPosition': 0,
            'GivenCredit': 20000,
            'BetCredit': 15191,
            'GivenCreditMax': 350200,
            'ShowOutstandingLink': false,
            'ReportLink': {
                'Text': 'Outstanding',
                'Url': '/site-betlists/RunningBetList/Index?CustID=1382345&amp;CustName=ACGUUS3838033&amp;GetAll=True'
            },
            'ShowTotalBalanceTransferLink': true,
            'TotalBalanceTransferLink': '/ex-main/_Transfer/Agent/MemberTransfer.aspx?custid=1382345&CallBack=false',
            'ShowYesterdayBalanceTransferLink': false,
            'YesterdayBalanceTransferLink': '/ex-main/_Transfer/Agent/MemberTransfer.aspx?custid=1382345&CallBack=false',
            'CanUpdateAccount': true,
            'AccountInfoUrl': accountInfoUrl
        },
        'Buttons': [
            {
                'Text': 'Bet List',
                'Url': '/site-betlists/MemberBetList/Index?CustID=1382345&CustName=ACGUUS3838037'
            },
            {
                'Text': 'Statement',
                'Url': '/site-reports/Statement/MemberStatement?CustID=1382345&CustName=ACGUUS3838037&CustLevelId=1'
            }
        ],
        'CanViewLog': true,
        'ViewLogLink': '/site-viewlogs/setting/index?username=ACGUUS3838037',
        'ViewLoginIPLink': '/ex-main/_IPInfo/IpInfo.aspx',
        'AccountInfoUrl': accountInfoUrl
    }
};
