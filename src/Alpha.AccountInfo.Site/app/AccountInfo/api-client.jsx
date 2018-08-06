import axios from 'axios';

const handleErrors = (err) => {
    console.error(err);
    throw err;
};

const responseData = res => JSON.parse(res.data);

const requests = {
    get: (url) => axios.get(url)
        .then(responseData)
        .catch(handleErrors),
    post: (url, data) => axios.post(`${apiUrl}${url}`, data)
        .then(responseData)
        .catch(handleErrors),
    put: (url, data) => axios.put(`${apiUrl}${url}`, data)
        .then(responseData)
        .catch(handleErrors),
    patch: (url, data) => axios.patch(`${apiUrl}${url}`, data)
        .then(responseData)
        .catch(handleErrors),
    del: (url) => axios.delete(`${apiUrl}${url}`)
        .then(responseData)
        .catch(handleErrors)
};

export const accountInfoAPI = {
    getCustomerInfo: (username) => requests.get(window.accountInfoSiteAppPath + "accountinfo/getcustomerinfo?username=" + username)
}