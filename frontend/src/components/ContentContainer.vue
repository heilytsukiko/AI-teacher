<script setup lang="ts">
type Size = 'auto' | 'fit-content' | `${number}vh` | `${number}%` | `var(--${string})`

interface IContentContainer{
    height?: Size;
    width?: Size;
}

const props = withDefaults(defineProps<IContentContainer>(),{
    height: 'auto',
    width: 'auto',
})
</script>

<template>
    <main class="content-container">
        <slot></slot>
    </main>
</template>

<style scoped>
.content-container{
    width: v-bind(width); 
    max-width: 1000px;
    height: v-bind(height);
    border: 1px solid var(--color-secondary);
    border-radius: var(--border-radius);
    padding: 5rem;
    background-color: var(--color-primary-transparent);
    backdrop-filter: blur(100px);
}

@media(max-width: 1240px){
    .content-container{ 
        padding: 3rem;
    }
}

@media(max-width: 480px){
    .content-container{
        padding: 2.5rem;
    }
}
</style>