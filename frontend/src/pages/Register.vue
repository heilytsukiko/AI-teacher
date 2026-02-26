<script setup lang="ts">
import AnimationLayout from '@/components/AnimationLayout.vue';
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import InputField from '@components/ui/InputField.vue';
import Button from '@components/ui/Button.vue';
import { useRegisterStore } from '@store/register'
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';


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
const wrongPassword = ref<boolean>(false) 
const userPass = formData.password;
const router = useRouter()

function register(){
    auth.register(formData);

    if( userPass.length <= 8 && !(/\d/.test(userPass)) && !(/[a-zA-Z]/.test(userPass))){
        wrongPassword.value = true;
    }
    if(auth.statusOk){
        router.push("/login")
    }
}
</script>

<template>
    <AnimationLayout>
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
                    <div class="password-warning" v-if="wrongPassword">
                        <p>Please enter a password of at least 8 characters, including:</p>
                        <ul>
                            <li>Latin letters</li>
                            <li>Nmbers</li>
                        </ul>
                    </div>
                    <Button type="submit" size="large">Register</Button>
                </form>
                <div class="text-wrapper">
                    <p>Do you have an account?</p>
                    <RouterLink to="/login" class="link">Log in</RouterLink>
                </div>
            </ContentContainer>
        </div>
    </AnimationLayout>
</template>

<style scoped>
.page-wrapper{
    display: flex;
    flex-direction: row;
    height: 100vh;
}

.main{
    --main-padding: 5rem 10rem;
    --main-margin: 0.5rem auto;

    margin: var(--main-margin);
    padding: var(--main-padding);
}

h1{
    font-size: var(--h1-size);
    color: var(--color-secondary);
    text-align: center;
    margin: 0 auto 2rem;
    width: fit-content;
}

.register-form{
    display: flex;
    flex-direction: column;
    width: fit-content;
    gap: 2rem;
    margin-bottom: 2rem;
}

.password-warning{
    color: var(--color-notice);
    white-space: wrap;
    width: 100%;
    padding-left: 2rem;
}

.password-warning ul{
    list-style-type: none; 
    padding: 10px;
    margin: 0;
}

.password-warning li::marker{
    content: '- ';
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

@media(max-width: 1240px){
   .main{
        --main-margin: 1rem auto;
    }
}

@media(max-width: 769px){
    .page-wrapper{
        gap: 0;
    }
    .main{
        --main-padding: 5rem 3rem;
        --main-margin: 1.5rem auto;
    }
    .password-warning{
        max-width: 300px;
    }
}

@media(max-width: 480px){
    .main{
        --main-padding: 5rem 3.5rem;
        --main-margin: 0.5rem auto;
    }
    .password-warning{
        max-width: 275px;
        padding-left: 0;
    }
}

@media(max-width: 360px){
    .password-warning{
        max-width: 225px;
    }
}
</style>