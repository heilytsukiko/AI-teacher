import { api } from '@api/baseApi'
import type { ILogin } from '@/types';

export const loginUser = ( data: ILogin ) => {
    return api.post('/Auth/login', data)
}