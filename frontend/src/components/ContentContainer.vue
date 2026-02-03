<script lang="ts">

</script>

<template>
    <main class="content-container">
        <slot></slot>
    </main>
</template>

<style scoped>
.content-container{
    width: fit-content; 
    max-width: 1000px;
    height: auto;
    border: 1px solid var(--color-secondary);
    border-radius: var(--border-radius);
    padding: var(--padding);
    background-color: var(--color-primary-transparent);
}
</style>