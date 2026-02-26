<script setup lang="ts">
import { ref } from 'vue';
import Button from '@components/ui/Button.vue';

const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition; 
const SpeechRecognitionEvent =  window.SpeechRecognitionEvent || window.webkitSpeechRecognitionEvent;

const recognition  = new SpeechRecognition();
recognition.continuous = true;
recognition.lang = "en-US";
recognition.interimResults = false;

const isActive = ref<boolean>(false);
const word = ref<string | undefined>('');
const emit = defineEmits<{(e: 'recorded-text', value: string): void}>()

function voice(){
    if(!isActive.value) {
        recognition.start();
        
        recognition.onresult = (event: SpeechRecognitionEvent) => {
            const lastWordIndex = event.resultIndex;
            word.value = event.results[lastWordIndex]?.[0]?.transcript; 
            console.log(word.value)
        }


        console.log("value: " + word.value)
        if(word.value){
            console.log("word: " + word.value)
            emit('recorded-text', word.value.trim())
        }

        isActive.value = !isActive.value;
    } else {
        recognition.stop();
        isActive.value = !isActive.value;
    }
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