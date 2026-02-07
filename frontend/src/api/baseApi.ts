import axios from "axios";

export const api = axios.create({ 
    baseURL: "http://localhost:5074",
    timeout: 30000 // для STT/TTS нужно время
});