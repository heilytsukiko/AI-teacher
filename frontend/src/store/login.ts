import { defineStore } from "pinia";
import * as authApi from '@api/login.api';
import type { ILogin } from '@/types'
import { useAuthStore } from "./auth";
import { useRouter } from "vue-router";

export const useLoginStore = defineStore('login', {
    state: () => ({ loading: false }),
    actions: {
        async login( formData: ILogin ) {
            this.loading = true;

            try {
                const response = await authApi.loginUser(formData);
                const authStore = useAuthStore(); 
                const token = response.data.token;
                const router = useRouter();

                if(response.status === 200) {
                    localStorage.setItem('user_token', token)
                    authStore.setToken(token);
                    router.push("/chat")
                    return true;
                }
            } catch (error: unknown){
                console.log('login error: ' + error);
                return false;
            } finally {
                this.loading = false;
            }
        }
    }
})