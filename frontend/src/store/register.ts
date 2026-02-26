import { defineStore } from "pinia";
import * as authApi from '@api/register.api'; 
import type { IRegisterUser } from '@/types'; 

export const useRegisterStore = defineStore('auth', {
    state: () => ({ loading: false }),
    actions: {
        async register(formData: IRegisterUser) {
            this.loading = true;
            try {
                const response = await authApi.registerUser(formData)
                if(response.status === 200) return true
            } catch (error: any){
                throw error; 
                return false
                //добавь обработку ошибок на страницу регистрации. Например:
                //почта занята
                //юзернейм занят(если они уникальны)
                console.log('api error: ' + error.response?.data);
            } finally {
                this.loading = false;
            }
        }
    }
})