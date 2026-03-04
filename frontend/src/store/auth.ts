import axios from "axios";
import { defineStore } from "pinia";

export const useAuthStore = defineStore('auth', {
    state: () => ({ 
        token: window.localStorage.getItem('user_token') || null,
        user: null,
    }),
    getters: { isAuthenticated: (state) => !!state.token },
    actions: {
        setToken(newToken: string) {
            this.token = newToken
            window.localStorage.setItem('user_token', newToken)
        },
        logout() {
            this.token = null;
            localStorage.removeItem('user_token')
            delete axios.defaults.headers.common['Authorization'];
        }
    }
})