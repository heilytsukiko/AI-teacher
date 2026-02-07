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

const emit = defineEmits<{
    (e: 'send-data', value: string): void,
}>()

function sendData(){
    emit('send-data', data.value)
    data.value = props.value
}
</script>

<template>
   <textarea 
        :class="`text text-${size}`"
        v-model="data"
        @keyup.enter="sendData"
        :placeholder="placeholder"
   />
</template>

<style scoped>
.input{
    padding: var(--padding);
    border-radius: var(--border-radius);
    outline: none;

    --btn-color-accent: var(--color-secondary);
}

input[type="text"]:focus{
    outline: none;
}

/* variants */
.input-primary{
    background-color: var(--color-primary);
    border: 1px solid var(--color-secondary);
    color: var(--color-secondary);
}

/* size */
.input-small{
    width: 30rem;
}

.input-middle{
    width: 35rem;
}

@media(max-width: 768px){
    .input-small{
        width: 20rem;
    }

    .input-middle{
        max-width: 300px;
    }
}

@media(max-width: 480px){
    .input-middle{
        max-width: 250px;
    }
}

@media(max-width: 360px){
    .input-middle{
        max-width: 200px;
    }
}
</style>