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
            id="message-input"
            placeholder="Enter message.."
            v-model="inputValue"
            @enter="sendMessage"
        />
        <Button size="small" @button-сlick="sendMessage">Send</Button>
    </div>
</template>

<style scoped>
.message-input{
    display: flex;
    flex-direction: row;
    gap: 0.8rem;
}
</style>