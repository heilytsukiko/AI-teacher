<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import InputField from '@components/ui/InputField.vue';
import Button from '@components/ui/Button.vue';
import { ref } from 'vue';

const temporaryStopper = ref('');
const height = 'var(--main-height)'
</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main" height="var(--main-height)" width="fit-content">
            <h1>Log In</h1>
            <form action="" class="login-form">
                <label for="email" hidden>email</label>
                <InputField 
                    id="email" 
                    v-model="temporaryStopper"
                    type="email" 
                    placeholder="Email"
                />
                <label for="password" hidden>Password</label>
                <InputField 
                    id="password" 
                    v-model="temporaryStopper"
                    type="password" 
                    placeholder="Password"
                />
                <RouterLink to="/profile"><Button size="large">Log in</Button></RouterLink>
            </form>
            <div class="text-wrapper">
                <p>Don't have an account yet?</p>
                <RouterLink to="/register" class="link">Register</RouterLink>
            </div>
        </ContentContainer>
    </div>
</template>

<style scoped>
.page-wrapper{
    display: flex;
    flex-direction: row;
    height: 100vh;
}

.main{
    --main-padding: 5rem 10rem;
    --main-margin: auto;

    margin: var(--main-margin);
    padding: var(--main-padding);
}

h1{
    font-size: var(--h1-size);
    color: var(--color-secondary);
    text-align: center;
    margin-bottom: 2rem;
}

.login-form{
    display: flex;
    flex-direction: column;
    gap: 2rem;
    margin-bottom: 2rem;
}

.text-wrapper{
    display: flex;
    justify-content: center;
    gap: 1rem;
    color: var(--color-secondary);
}

.link{
    color: var(--color-accent);
}

@media(max-width: 1240px){
   .main{
        --main-padding: 5rem;
        --main-margin: auto;
    }
}

@media(max-width: 768px){
    .page-wrapper{
        gap: 0;
    }

    .main{
        margin: 0.5rem;
    }
}
</style>