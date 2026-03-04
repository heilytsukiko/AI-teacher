import { useAuthStore } from "@store/auth";
import axios from "axios";

export const api = axios.create({ 
    baseURL: "https://ai-teacher-uvmm.onrender.com/api",
    timeout: 30000 // для STT/TTS нужно время
});

api.interceptors.request.use((config) => {
    const authStore = useAuthStore();
    if(authStore.token) {
        config.headers.Authorization = `Bearer ${authStore.token}`
    } 
    return config;

}, (error: unknown) => {
    return Promise.reject(error)
})