<template>
    <div class="highlighted-block">
        <slot></slot>
    </div>
</template>

<style scoped>
.highlighted-block{
    padding: 1rem;
    background-color: var(--color-notice);
    border-radius: 5px;
}
</style>