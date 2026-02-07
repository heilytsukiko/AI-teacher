import { api } from '@api/baseApi';

export const chatMessages = (Messages: object) => {
    return api.post('/api/AI/chat', Messages)
}