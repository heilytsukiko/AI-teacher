import { api } from '@api/baseApi';

//может понадобится интерфейс для структурирования данных
export const chatMessages = (data: object) => {
    return api.post('/Speak', data)
}