<script setup lang="ts">
import { ref } from 'vue'
type Size = 'small' | 'middle'

interface ITextarea {
    size: Size,
    placeholder: string,
    value: string,
}
const data = ref<string>('')

const props = withDefaults(defineProps<ITextarea>(),{
    size: 'middle'
})

const emit = defineEmits<{ (e: 'send-data', value: string): void }>()

function sendData(){
    if (data.value.trim().length !== 0){
        emit('send-data', data.value)
        data.value = props.value
    }
}
</script>

<template>
   <textarea 
        :class="`textarea textarea-${size}`"
        v-model="data"
        @keyup.enter="sendData"
        :placeholder="placeholder"
   />
</template>

<style scoped>
.textarea{
    padding: var(--padding);
    border-radius: var(--border-radius);
    background-color: var(--color-primary);
    border: 1px solid var(--color-secondary);
    color: var(--color-secondary);

    --btn-color-accent: var(--color-secondary);
}

/* size */
.textarea-small{
    width: 30rem;
}

.textarea-middle{
    width: 35rem;
}

@media(max-width: 768px){
    .textarea-small{
        width: 20rem;
    }

    .textarea-middle{
        max-width: 300px;
    }
}

@media(max-width: 480px){
    .textarea-middle{
        max-width: 250px;
    }
}

@media(max-width: 360px){
    .textarea-middle{
        max-width: 200px;
    }
}
</style>