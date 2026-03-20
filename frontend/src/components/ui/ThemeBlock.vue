<script setup lang="ts">
interface IThemeBlock {
    title: string
}
const props = defineProps<IThemeBlock>();
</script>

<template>
    <section class="theme-block">
        <h2>{{ title }}</h2>
        <slot></slot>
    </section>
</template>

<style scoped>
.theme-block{
    max-width: 800px;
    padding-top: 3rem;
    padding-bottom: 3rem;
    padding-inline: 2rem;
    background-color: var(--color-accent-secondary);
    border-radius: var(--border-radius);
    box-shadow: inset 0 0 5px var(--color-primary-transparent);
}

.theme-block h2 {
    color: var(--font-color);
    text-align: center;
}
</style>