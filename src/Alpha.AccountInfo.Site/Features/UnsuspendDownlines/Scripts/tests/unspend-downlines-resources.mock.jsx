export const resources = {
    "RemainUnchangedDownlines": "Remain Unchanged Downlines",
    "NumberColumn": "No.",
    "MultipleConfirm": "Are you sure you want to change Suspend status? All affected downlines will be changed Suspend status as well.",
    "UnchangedViewListNote": "List of downlines whose status are unchanged after <username> is unsuspended",
    "Master": "Master",
    "Member": "Member",
    "AffectedViewListNote": "List of downlines whose status are changed after <username> is unsuspended",
    "RemainUnchangedDownlinesMember": "Remain Unchanged Downlines &lt;br/&gt; (Member)",
    "ViewInformation4": "+ For the rest, their current status will not be changed. You can click on the number of remain unchanged downlines to view the list.",
    "ViewInformation5": "- Affected Downlines: list of users whose status will be changed after their upline is unsuspended.",
    "ViewInformation6": "- Remain Unchanged Downlines: list of users whose status will be kept after their upline is unsuspended .",
    "ViewInformation1": "- When a customer is suspended, all his downlines will be suspended as well",
    "ViewInformation2": "- When a customer is unsuspended:",
    "ViewInformation3": "+ Only the downlines who were &lt;b&gt;indirectly&lt;/b&gt; suspended by upline will be unsuspended. You can click on the number of affected downlines to view the list",
    "Submit": "Submit",
    "AffectedDownlines": "Affected Downlines",
    "SubmitMultipleConfirm3": "Do you want to proceed?",
    "SubmitMultipleConfirm2": "&lt;username&gt; will be suspended. Warning: they will not be able to place bet.",
    "SubmitMultipleConfirm1": "&lt;username&gt; will be unsuspended.",
    "Suspended": "Suspended",
    "AffectedDownlinesAgentMember": "Affected Downlines &lt;br/&gt;  (Agent/Member)",
    "Of": "of",
    "To": "to",
    "All": "All",
    "Agent": "Agent",
    "Level": "Level",
    "Total": "Total",
    "Reset": "Reset",
    "Username": "Username",
    "UnsuspendSingleNote": "List of downlines&#39; status after &lt;username&gt; is unsuspended",
    "UnsuspendedSuccessfully": "Status of {0} and their affected downlines have been successfully updated to Unsuspended.",
    "ConfirmSuspendedViewModelInCaseUnsuspendedMasterOrAgentMessage": "Are you sure you want to change Suspend status for this customer? All downlines will be un-suspended as well.",
    "UpdateSuccessfully": "Successfully updated!",
    "Unsuspended": "Unsuspended",
    "RemainUnchangedDownlinesAgentMember": "Remain Unchanged Downlines &lt;br/&gt; (Agent/Member)",
    "Displaying": "Displaying",
    "SuspendedSuccessfully": "Status of {0} and all of their downlines have been successfully updated to Suspended.",
    "SuspendStatus": "Suspend Status",
    "AffectedDownlinesMember": "Affected Downlines &lt;br/&gt; (Member)",
    "Back": "Back",
    "Page": "Page"
}

export const responseViewListData = {
    "SearchAccountId": 36204210,
    "SearchAccountName": "davidtest13May01",
    "IsAffected": true,
    "Data": {
        "Levels": [
            {
                "DownlineUsernames": [
                    "davidtest13May0101",
                    "davidtest13May0102",
                    "davidtest13May0103"
                ],
                "RoleId": 2
            },
            {
                "DownlineUsernames": [
                    "davidtest13May0101001",
                    "davidtest13May0101002",
                    "davidtest13May0101003"
                ],
                "RoleId": 1
            }
        ],
        "TotalRows": 6
    },
    "AccountInfoUrl": "/site-accountinfo/"
}

export const responseUnsuspendSingleData = {
    "SearchAccountId": 36204210,
    "SearchAccountName": "davidtest13May01",
    "Levels": [
        {
            "RoleId": 2,
            "AffectedDownlines": 2,
            "UnchangedDownlines": 1
        },
        {
            "RoleId": 1,
            "AffectedDownlines": 0,
            "UnchangedDownlines": 3
        }
    ],
    "AffectedDownlinesTotal": 2,
    "UnchangedDownlinesTotal": 4,
    "AccountInfoUrl": "/site-accountinfo/"
}

export const responseUnsuspendMultipleData = {
    "SearchAccountRoleId": 3,
    "Data": [
        {
            "SearchAccountId": 36204210,
            "Username": "davidtest13May01",
            "Suspended": true,
            "Downlines": [
                {
                    "SearchAccountId": 36204210,
                    "DownlineRoleId": 2,
                    "AffectedDownlines": 2,
                    "UnchangedDownlines": 1
                },
                {
                    "SearchAccountId": 36204210,
                    "DownlineRoleId": 1,
                    "AffectedDownlines": 0,
                    "UnchangedDownlines": 3
                }
            ]
        },
        {
            "SearchAccountId": 36204211,
            "Username": "davidtest13May02",
            "Suspended": false,
            "Downlines": []
        }
    ]
}