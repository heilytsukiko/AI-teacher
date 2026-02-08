import { defineStore } from "pinia";
import * as authApi from '@api/register.api';

interface IRegisterUser {
  "username": string,
  "email": string,
  "password": string
}

export const useRegisterStore = defineStore('auth', {
    state: () => ({ user: null, loading: false, statusOk: false }),
    actions: {
        async register(formData: IRegisterUser) {
            this.loading = true;
            try{
                const response = await authApi.registerUser(formData)
                this.user =  response.data;
                if( response.status === 204) {
                    this.statusOk = true;
                }
            } catch (error: any){
                console.log('api error: ' + error.response?.data);
            } finally {
                this.loading = false;
            }
        }
    }
})