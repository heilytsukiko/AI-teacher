import { api } from '@api/baseApi';

interface IRegisterUser {
  "username": string,
  "email": string,
  "password": string
}

export const registerUser = (data: IRegisterUser) => {
  return api.post('/Auth/register', data)
}