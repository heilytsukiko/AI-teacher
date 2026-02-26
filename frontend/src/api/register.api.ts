import { api } from '@api/baseApi';
import type { IRegisterUser } from '@/types';

export const registerUser = (data: IRegisterUser) => {
  return api.post('/Auth/register', data)
}