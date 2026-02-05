<script setup lang="ts">
import Button from './ui/Button.vue';
import { ref } from 'vue';

const isOpen = ref(false);

function open(){
    isOpen.value = !isOpen.value
}
</script>

<template>
    <header>
        <button 
            class="burger-menu" 
            @click="open"
        >
            <span></span>
            <span></span>
            <span></span>
        </button>

       <div :class="`header-content-wrapper ${isOpen ? 'isOpen' : ''}`">
            <div class="logo-wrapper">
                <img src="/logo.png" alt="logo">
                <p class="logo-text">AI teacher</p>
            </div>
            <nav role="navigation">
                <ul>
                    <li>
                        <RouterLink to="/chat">
                            <Button variant="accent">Chat</Button>
                        </RouterLink>
                    </li>
                    <li>
                        <RouterLink to="/test">
                            <Button variant="accent">Test</Button>
                        </RouterLink>
                    </li>
                    <li>
                        <!-- <RouterLink to="/grammar"> -->
                            <Button disabled variant="accent">Grammar</Button>
                        <!-- </RouterLink> -->
                    </li>
                    <li>
                        <!-- <RouterLink to="/profile"> -->
                            <Button disabled variant="accent">Profile</Button>
                        <!-- </RouterLink> -->
                    </li>
                    <li>
                        <!-- <RouterLink to="/essay"> -->
                            <Button disabled variant="accent">Essay</Button>
                        <!-- </RouterLink> -->
                    </li>
                </ul>
            </nav>
       </div>
    </header>
</template>

<style scoped>
header{
    min-height: 100vh;
    width: 15rem;
    padding: var(--padding);
    color: var(--font-color);
    position: relative;
}

.burger-menu{
    display: none;
}

.header-content-wrapper{
    display: block;
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

@media(max-width: 960px) {
    header{
        padding: 0;
        width: 0;
    }

    .burger-menu{
        position: absolute;
        z-index: 3;
        inset: 1rem;
        display: flex;
        flex-direction: column;
        gap: 0.4rem;
        border: 0;
        cursor: pointer;
        height: fit-content;
        width: fit-content;
        padding: 1rem;
        background-color: transparent;
    }

    .burger-menu span{
        height: 0.2rem;
        width: 2rem;
        border-radius: 0.3rem;
        background-color: var(--color-accent);
    }
    
    .header-content-wrapper{
        position: absolute;
        z-index: 2;
        padding-top: 5rem;
        opacity: 0;
        visibility: hidden;
        height: 100vh;
        background-color: var(--color-primary-transparent);
        backdrop-filter: blur(10px);
        transform: translateX(-15rem);
        transition: all 0.6s ease-in-out;
    }
    .isOpen{
        opacity: 1;
        visibility: visible;
        transform: translateX(0);
    }

    ul{
        padding-inline: 1rem;
    }
}
</style>