import axios from 'axios';

export const handleErrors = err => {
    $.unblockUI();
    if (err.response.status === 403) {
        return {
            IsSuccess: false,
            Message: $('#NotAuthorized').val()
        };
    }

    throw err;
};

export const requests = {
    get: (url, data) => {
        let axiosPromise = axios.get(url, { params: data });
        axiosPromise = axiosPromise.then(serverData => serverData.data);

        return axiosPromise;
    },
    post: (url, data) => {
        let axiosPromise = axios.post(url, data,
            {
                headers:
                {
                    __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val(), 'X-Requested-With': 'XMLHttpRequest'
                }
            });

        axiosPromise = axiosPromise.then(serverData => serverData.data);

        return axiosPromise;
    }
};