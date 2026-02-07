<script setup lang="ts">
import InputField from '@components/ui/InputField.vue';
import Button from './ui/Button.vue';

export type Author = 'AI' | 'user'

interface IMessage{
    data: string;
    author: Author;
}

const props = defineProps<IMessage>()
</script>

<template>
    <div :class="`message ${author === 'AI' ? 'message-ai' : 'message-user'}`">
        {{ data }}
    </div>
</template>

<style scoped>
.message{
    display: flex;
    max-width: 400px;
    flex-direction: row;
    gap: 0.8rem;
    padding: var(--padding);
    color: var(--font-color);
    font-weight: 200;
    word-break: break-all;
}

.message-ai{
    border-radius: 0 var(--border-radius) var(--border-radius) var(--border-radius);
    background-color: color-mix(in srgb, var(--color-primary-transparent), var(--color-accent) 20%);
}

.message-user{
    border-radius: var(--border-radius) 0 var(--border-radius) var(--border-radius);
    background-color: var(--color-primary-transparent);
}

@media(max-width: 768px){
    .message{
        max-width: 300px;
    }
}

@media(max-width: 480px){
    .input-middle{
        max-width: 250px;
    }
}

@media(max-width: 360px){
    .message{
        max-width: 200px;
    }
}
</style>