<script setup lang="ts">
</script>
<template>
    <header>
        <nav>
            <ul>
                <li>
                    <RouterLink to="/">aaAAA</RouterLink>
                </li>
            </ul>
        </nav>
    </header>
</template>
<style scoped>
header{
    min-height: 100vh;
    width: 15rem;
    padding: 1rem;
    background-color: var(--color-primary);
    color: var(--font-color);
}
ul {
    list-style: none; 
    padding: 0;
    margin: 0;
}

li{
    font-size: 1rem;
}
a{
    color: var(--font-color);
}
</style>