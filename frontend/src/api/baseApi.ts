import axios from "axios";

export const api = axios.create({ 
    baseURL: "https://ai-teacher-uvmm.onrender.com/api",
    timeout: 30000 // для STT/TTS нужно время
});