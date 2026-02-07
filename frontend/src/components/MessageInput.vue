<script setup lang="ts">
import Textarea from '@components/ui/Textarea.vue';
import Button from './ui/Button.vue';
import { ref } from 'vue'

const messageText = ref<string>('')

const emit  = defineEmits<{(e: 'send-message', value: string): void}>()

function sendMessage(payload: string){
    console.log(payload)
    emit('send-message', payload)
    messageText.value = ''
}
</script>

<template>
    <form class="message-input">
        <Textarea
            size="small"
            placeholder="Enter text..."
            @send-data="sendMessage"
            :value="messageText"
        />
        <Button size="small" @button-сlick="sendMessage" class="send-button">Send</Button>
        <Button size="small" @button-сlick="sendMessage" class="voice-button">
            <span></span>
        </Button>
    </form>
</template>

<style scoped>
.message-input{
    display: flex;
    flex-direction: row;
    gap: 0.8rem;
}

.voice-button span{
    width: 1rem;
}

@media(max-width: 960px){
    .send-button{
        display: none;
    }
}
</style>