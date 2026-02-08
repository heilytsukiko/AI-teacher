import { defineStore } from "pinia";
import * as api from '@/api/chat.api';

interface Message {
    content: ''
}

export const useChatStore = defineStore('chat', {
    state: () => ({ message: '',  loading: false}),
    actions: {
        async sendMessage(msg: Message){
            this.loading = true;
            try{
                const response = await api.chatMessages(msg)
                this.message = response.data
            } catch (error: any){
                console.log('api error: ' + error.response?.data);
            } finally {
                this.loading = false;
            }

        }
    }
})