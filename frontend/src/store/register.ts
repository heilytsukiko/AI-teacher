import { defineStore } from "pinia";
import * as authApi from '@/api/auth.api';

export const useRegisterStore = defineStore('auth', {
    state: () => ({ user: null, loading: false}),
    actions: {
        async register(formData: object) {
            this.loading = true;
            try{
                const response = await authApi.registerUser(formData)
                this.user =  response.data;
            } catch (error){
                console.log('api error :( ' + error);
            } finally {
                this.loading = false;
            }
        }
    }
})