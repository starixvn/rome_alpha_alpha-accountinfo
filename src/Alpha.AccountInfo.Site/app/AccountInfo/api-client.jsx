//import axios from 'axios';

//// __API_URL_ comes from webpack DefinePlugin
//declare const __API_URL__: string;
//const apiUrl: string = __API_URL__;

//const handleErrors = err => {
//    console.error(err);
//    throw err;
//};

//const responseData = res => res.data;

//const requests = {
//    get: (url) => axios.get(`${apiUrl}${url}`)
//        .then(responseData)
//        .catch(handleErrors),
//    post: (url, data) => axios.post(`${apiUrl}${url}`, data)
//        .then(responseData)
//        .catch(handleErrors),
//    put: (url, data) => axios.put(`${apiUrl}${url}`, data)
//        .then(responseData)
//        .catch(handleErrors),
//    patch: (url, data) => axios.patch(`${apiUrl}${url}`, data)
//        .then(responseData)
//        .catch(handleErrors),
//    del: (url) => axios.delete(`${apiUrl}${url}`)
//        .then(responseData)
//        .catch(handleErrors)
//};

//export const todosApi = {
//    getCustomerInfo: () => requests.get('/ReactDemo/getCustomerInfo'),
//}