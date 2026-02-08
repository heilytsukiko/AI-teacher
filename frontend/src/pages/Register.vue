<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import InputField from '@components/ui/InputField.vue';
import Button from '@components/ui/Button.vue';
import { useRegisterStore } from '@store/register'
import { reactive, ref } from 'vue';

type Email = `${string}@${string}.${string}`

interface IRegisterUser{
    username: string,
    email: Email,
    password: string,
}

const formData = reactive<IRegisterUser>({
    username: '',
    email: ''  as unknown as Email,
    password: ''
});

const auth = useRegisterStore();

function register(){
    auth.register(formData);
    console.log('Sending data:', JSON.stringify(formData));
}

const wrongPassword = ref<boolean>(false) 
const userPass = formData.password;

if( userPass.length <= 8 && !(/\d/.test(userPass)) && !(/[a-zA-Z]/.test(userPass))){
    wrongPassword.value = true;
}
</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main" height="var(--main-height)">
            <h1>Register</h1>
            <form @submit.prevent="register" class="register-form">
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
                <p v-if="wrongPassword" class="password-warning">Please enter a password of at least 8 characters, including Latin letters and numbers</p>
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

.password-warning{
    color: var(--color-notice);
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

@media(max-width: 768px){
    .page-wrapper{
        gap: 0;
    }

    .main{
        margin: 0.5rem;
    }
}
</style>