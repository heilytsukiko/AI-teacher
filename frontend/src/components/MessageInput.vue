<script setup lang="ts">
import { ref } from 'vue';
import Textarea from '@components/ui/Textarea.vue';
import VoiceButton from '@components/VoiceButton.vue';

const messageText = ref<string>('')

const emit  = defineEmits<{(e: 'send-message', value: string): void}>()

function sendMessage(payload: string){
    emit('send-message', payload)
}

function listenToVoice(text: string){
    console.log("text: " + text)
    messageText.value = text;
}
</script>

<template>
    <form class="message-input">
        <Textarea
            size="small"
            placeholder="Enter text..."
            id="message-textarea"
            @send-data="sendMessage"
            :value="messageText"
        />
        <VoiceButton size="small" class="voice-button" @recorded-text="listenToVoice">
            <span></span>
        </VoiceButton>
    </form>
</template>

<style scoped>
.message-input{
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 0.8rem;
    margin-top: auto;
}

.voice-button span{
    width: 1rem;
}
</style>