<script setup lang="ts">
import InputField from '@components/ui/InputField.vue';
import Button from './ui/Button.vue';
import { ref } from 'vue'

const inputValue = ref('')

function sendMessage(){
    alert(inputValue.value)
}
</script>

<template>
    <div class="message-input">
        <InputField 
            size="middle"
            id="message-input"
            placeholder="Enter message.."
            v-model="inputValue"
            @enter="sendMessage"
        />
        <Button size="small" @button-сlick="sendMessage" class="send-button">Send</Button>
        <Button size="small" @button-сlick="sendMessage" class="voice-button">
            <span></span>
        </Button>
    </div>
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