<script setup lang="ts">
import { ref } from 'vue';
import Textarea from '@components/ui/Textarea.vue';
import VoiceButton from './VoiceButton.vue';

const messageText = ref<string>('')

const emit  = defineEmits<{(e: 'send-message', value: string): void}>()

function sendMessage(payload: string){
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
        <VoiceButton/>
    </form>
</template>

<style scoped>
.message-input{
    display: flex;
    flex-direction: row;
    gap: 0.8rem;
    justify-content: flex-end;
}
</style>