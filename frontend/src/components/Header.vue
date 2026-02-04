<script setup lang="ts">
import Button from './ui/Button.vue';
</script>

<template>
    <header>
        <div class="logo-wrapper">
            <img src="/logo.png" alt="logo">
            <p class="logo-text">AI teacher</p>
        </div>
        <nav>
            <ul>
                <li>
                    <RouterLink to="/chat">
                        <Button variant="accent" size="middle">Chat</Button>
                    </RouterLink>
                </li>
                <li>
                    <RouterLink to="/profile">
                        <Button variant="accent" size="middle">Profile</Button>
                    </RouterLink>
                </li>
                <li>
                    <!-- <RouterLink to="/essay"> -->
                        <Button disabled variant="accent" size="middle">Essay</Button>
                    <!-- </RouterLink> -->
                </li>
            </ul>
        </nav>
    </header>
</template>

<style scoped>
header{
    min-height: 100vh;
    width: 15rem;
    padding: var(--padding);
    color: var(--font-color);
}

.logo-wrapper{
    display: flex;
    justify-content: center;
    gap: 1rem;
    align-items: center;
    margin-bottom: 2rem;
}

.logo-wrapper img{
    width: 2rem;
}

.logo-text{
    font-size: 1.5rem;
    font-weight: 700;
    color: var(--color-accent);
}

ul{
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
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