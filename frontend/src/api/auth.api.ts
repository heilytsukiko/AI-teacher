import axios from 'axios';

const api = import.meta.env.VITE_API_URL;
const baseApi = axios.create({ baseURL: api});

export const registerUser = (userData: object) => {
    return api.post('api/Auth/register', userData)
}