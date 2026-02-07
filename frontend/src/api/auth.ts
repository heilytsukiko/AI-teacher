import axios from 'axios';

const api = axios.create({ baseURL: 'http://localhost:5074/api'});

export const registerUser = (userData: object) => {
    return api.post('/Auth/register', userData)
}