<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import Button from '@components/ui/Button.vue';
</script>

<template>
   <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main" height="100vh">
            <section>
                <h1>Improve your English conversation skills<br>with an AI-powered tutor.</h1>
                <h2>Take an 
                    <span class="text-accent">English level test</span>
                     or 
                    <span class="text-accent">start studying right away</span>
                </h2>
                <div class="btn-wrapper">
                    <RouterLink to="/login"><Button class="link-button">Login</Button></RouterLink>
                    <RouterLink to="/register"><Button class="link-button">Register</Button></RouterLink>
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
    margin: 2rem auto;
    display: flex;
    flex-direction: column;

    --margin-right: 3rem;
}

h1{
    font-size: 4rem;
    font-weight: 700;
    text-align: end;
    font-family: var(--font-primary);
    color: var(--color-accent);
    margin: 2rem var(--margin-right) 1rem 0;
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

@media(max-width: 1240px){
    .main{
        margin: 1rem auto;
    }
    h1{ 
        font-size: 3.5rem;
        
    margin-top: 1rem;
    }
}

@media(max-width: 960px){
    .main{
        justify-content: start;
        margin: 1rem;

        --margin-right: 1rem;
    }

    .page-wrapper{
        gap: 0;
    }

    h1{ 
        font-size: 3rem;
        margin-top: 1rem;
    }

    h2{
        width: 80%;
    }
}

@media(max-width: 768px){
    h1{ 
        margin-top: 1rem;
    }
}
@media(max-width: 480px){
    .btn-wrapper{
        flex-direction: column;
        align-items: end;
    }
}
</style>