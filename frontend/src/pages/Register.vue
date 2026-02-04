<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import InputField from '@components/ui/InputField.vue';
import Button from '@components/ui/Button.vue';
import { useRegisterStore } from '@store/register'
import { reactive } from 'vue';

type Email = `${string}@${string}.${string}`

interface IFormData{
    username: string,
    email: Email,
    password: string,
}

const formData = reactive<IFormData>({
    username: '',
    email: ' @ . ',
    password: ''
});

const auth = useRegisterStore();

function register(){
    auth.register(formData);
}
</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main">
            <h1>Register</h1>
            <form @submit.prevent="register()" class="register-form">
                <label for="username" hidden>Username</label>
                <InputField 
                    id="username"  
                    v-model="formData.username"
                    placeholder="Username"
                />
                <label for="email" hidden>Email</label>
                <InputField 
                    id="email" 
                    v-model="formData.email"
                    type="email" 
                    placeholder="Email"
                />
                <label for="password" hidden>Password</label>
                <InputField 
                    id="password"
                    v-model="formData.password"
                    type="password"
                    placeholder="Password"
                />
                    <Button type="submit" size="large">Register</Button>
            </form>
            <div class="text-wrapper">
                <p>Do you have an account?</p>
                <RouterLink to="/login" class="link">Log in</RouterLink>
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
    width: fit-content;
    height: fit-content;
    margin: auto 5rem;
    padding: 5rem 10rem;
}

h1{
    font-size: var(--h1-size);
    color: var(--color-secondary);
    text-align: center;
    margin-bottom: 2rem;
}

.register-form{
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
        padding: 5rem;
    }
}

@media(min-width: 1440px){
    .main{ 
        padding: 5rem;
        margin: auto;
    }
}
</style>