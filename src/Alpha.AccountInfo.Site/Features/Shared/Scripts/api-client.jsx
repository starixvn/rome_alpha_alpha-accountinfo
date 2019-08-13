import axios from 'axios';
import axiosCancel from 'axios-cancel';

axiosCancel(axios, {
    debug: false // default
});

export const handleError = err => {
    $.unblockUI();
    if (axios.isCancel(err)) {
        return undefined;
    }

    if (err.status === 403) {
        return {
            IsSuccess: false,
            Message: $('#NotAuthorized').val()
        };
    }

    throw err;
};

export const requests = {
    get: (url, data, requestId) => {
        let axiosPromise = axios.get(url, { params: data, requestId: requestId });
        axiosPromise = axiosPromise.then(serverData => serverData.data);

        return axiosPromise;
    },
    post: (url, data) => {
        let axiosPromise = axios.post(url, data,
            {
                headers:
                {
                    __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val(),
                    'X-Requested-With': 'XMLHttpRequest'
                }
            });

        axiosPromise = axiosPromise.then(serverData => serverData.data);

        return axiosPromise;
    }
};