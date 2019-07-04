import axios from 'axios';
import axiosCancel from 'axios-cancel';

axiosCancel(axios, {
    debug: false // default
});

const handleError = err => {
    $.unblockUI();
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
        try {
            console.log(`requestId" ${requestId}`);
            let axiosPromise = axios.get(url, { params: data, requestId: requestId });
            axiosPromise = axiosPromise.then(serverData => serverData.data).catch((thrown) => {
                if (axios.isCancel(thrown)) {
                    console.log('request cancelled');
                } else {
                    console.log('some other reason');
                }
            });

            return axiosPromise;
        }
        catch (error) {
            handleError(error);
        }
    },
    post: (url, data) => {
        try {
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
        catch (error) {
            handleError(error);
        }
    }
};