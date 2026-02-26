<script setup lang="ts">
import { ref } from 'vue';
import Button from '@components/ui/Button.vue';

const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition; 
const SpeechRecognitionEvent =  window.SpeechRecognitionEvent || window.webkitSpeechRecognitionEvent;

const recognition  = new SpeechRecognition();
recognition.continuous = true;
recognition.lang = "en-US";
recognition.interimResults = true;

const isActive = ref<boolean>(false);
const fullResult = ref<string>('');
const interimResult =  ref<string>('');
const emit = defineEmits<{(e: 'recorded-text', value: string): void}>()

recognition.onresult = (event: SpeechRecognitionEvent) => {
    interimResult.value = ''

    for(let i = event.resultIndex; i < event.results.length; ++i){
        const result = event.results[i]?.[0]?.transcript;

        if(event.results[i]?.isFinal) {
            fullResult.value += result?.trim() + ' ';
            emit('recorded-text', fullResult.value.trim());
        } else {
            interimResult.value = result!
        }
    }   
}

recognition.onend = () => {
    isActive.value = false;
};

function voice(){
    if(!isActive.value) {
        fullResult.value = ''
        recognition.start();
    } else {
        recognition.stop();
    }
    isActive.value = !isActive.value;
}
</script>

<template>
   <Button
        size="small"
        class="voice-button"
        @click="voice"
   >
        <span></span>
   </Button>
</template>

<style scoped>
.voice-button span{
    display: inline-block;
    width: 30px;
    height: 30px;
    background-image: url('@assets/micro.svg');
    background-size: contain; 
    background-repeat: no-repeat;
    background-position: center;
}
.voice-button:hover span{
    filter: sepia(1) saturate(1000%) hue-rotate(60deg) brightness(95%);
}
</style>