import { defineStore } from "pinia";
import * as api from '@/api/chat.api';

export const useChatStore = defineStore('chat', {
    state: () => ({ message: '',  loading: false}),
    actions: {
        async sendMessage(msg: object){
            this.loading = true;
            try{
                const response = await api.chatMessages(msg)
                this.message = response.data
            } catch(error){
                console.log('Chat api error! error message: ' + error)
            } finally {
                this.loading = false;
            }

        }
    }
})