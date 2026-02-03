<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import Button from '@components/ui/Button.vue';
</script>

<template>
   <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main">
            <section>
                <h1>Speak. Practice.<br>Score higher.</h1>
                <h2>Тренируйся 24/7в <span class="text-accent">Speaking and Listening</span> вместе с ИИ-тренером</h2>
                
                <div class="btn-wrapper">
                    <RouterLink to="/register"><Button class="link-button">Register</Button></RouterLink>
                    <RouterLink to="/login"><Button class="link-button">Login</Button></RouterLink>
                </div>
            </section>
        </ContentContainer>
   </div>
</template>

<style scoped>
.page-wrapper{
    display: flex;
    flex-direction: row;
    gap: 1rem;
}

.main{
    margin: 0.5rem auto;
    display: flex;
    flex-direction: column;
    justify-content: center;
    height: 90vh;

    --margin-right: 3rem;
}

h1{
    font-size: 5rem;
    font-weight: 700;
    text-align: end;
    font-family: var(--font-primary);
    color: var(--color-accent-primary);
    margin-right: var(--margin-right);
    margin-bottom: 1rem;
}

h2{
    color: var(--color-accent-secondary);
    text-align: end;
    margin-left: auto;
    margin-right: var(--margin-right);
    width: 60%;
}

.text-accent{
    color: var(--color-accent);
}

.btn-wrapper{
    display: flex;
    gap: 1rem;
    justify-content: flex-end;
    padding: var(--margin-right);
}

.link-button a{
    color: var(--color-secondary);
}

.link-button:hover a{
    color: color-mix(in srgb, var(--color-secondary),  var(--color-accent) 50%);
}

.link-button:active{
    color: color-mix(in srgb, var(--color-secondary), black 50%);
}
</style>