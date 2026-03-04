import { defineStore } from "pinia";
import * as authApi from '@api/register.api'; 
import type { IRegisterUser } from '@/types'; 

export const useRegisterStore = defineStore('register', {
    state: () => ({ loading: false }),
    actions: {
        async register(formData: IRegisterUser) {
            this.loading = true;
            try {
                const response = await authApi.registerUser(formData)
                if(response.status === 200) return true
            } catch (error: any){
                //добавь обработку ошибок на страницу регистрации. Например:
                //почта занята
                //юзернейм занят(если они уникальны)
                console.log('api error: ' + error.response?.data);
                return false;
            } finally {
                this.loading = false;
            }
        }
    }
})