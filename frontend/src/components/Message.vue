<script setup lang="ts">
import InputField from '@components/ui/InputField.vue';
import Button from './ui/Button.vue';

type Author = 'AI' | 'user'

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
    background-color: var(--color-primary-transparent);
    color: var(--font-color);
    font-weight: 200;
}

.message-ai{
    border-radius: 0 var(--border-radius) var(--border-radius) var(--border-radius);
}

.message-user{
    border-radius: var(--border-radius) 0 var(--border-radius) var(--border-radius);
}

@media(max-width: 768px){
    .message{
        max-width: 300px;
    }
}
</style>