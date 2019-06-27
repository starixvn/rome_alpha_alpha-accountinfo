import axios from 'axios';

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
    get: (url, data) => {
        try {
            let axiosPromise = axios.get(url, { params: data });
            axiosPromise = axiosPromise.then(serverData => serverData.data);

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