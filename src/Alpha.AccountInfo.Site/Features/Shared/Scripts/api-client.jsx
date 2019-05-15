import axios from 'axios';

const handleErrors = (err) => {
	$.unblockUI();
	if (err.response.status === 403) {
		return {
			IsSuccess: false,
			Message: _NotAuthorized
		};
	}

	throw err;
};

export const requests = {
    get: (url, data) => axios.get(url, { params: data }).then(res => res.data).catch(handleErrors),
	post: (url, data) => axios.post(url, data,
		{
			headers:
			{
				__RequestVerificationToken: __RequestVerificationToken, 'X-Requested-With': 'XMLHttpRequest'
			}
        }).then(res => res.data).catch(handleErrors)
};