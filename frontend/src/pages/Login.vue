<script setup lang="ts">
import { reactive, ref } from 'vue';
import AnimationLayout from '@components/AnimationLayout.vue';
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import InputField from '@components/ui/InputField.vue';
import Button from '@components/ui/Button.vue';
import type { Email, ILogin } from '@/types';
import { useLoginStore } from '@store/login';
import { useRouter } from 'vue-router';

const formData = reactive<ILogin>({
    email: '' as unknown as Email,
    password: ''
});

const auth = useLoginStore();
const message = ref('');

async function loginUser() {
    if(isCorrectData(formData)){
        try {
            const success = await auth.login(formData);
            message.value = ''
        } catch (e: any) {
            console.log("error: " + e);
        }
    } else {
        message.value = 'The fields are empty. Please enter the data'
    }
}

function isCorrectData(data: ILogin){
    const {email, password} = data;

    const varEmpty: boolean = [email, password].some(val => val.trim() === '')
    if (varEmpty) return false
    return true
}
</script>

<template>
    <AnimationLayout>
        <Header/>

        <ContentContainer class="main" height="var(--main-height)" width="fit-content">
            <h1>Log In</h1>
            <form @submit.prevent="loginUser" class="login-form">
                <label for="email" hidden>email</label>
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
                <p class="warning-message" v-if="!isCorrectData(formData)">{{message}}</p>
                <Button type="submit" size="large">Log in</Button>
            </form>
            <div class="text-wrapper">
                <p>Don't have an account yet?</p>
                <RouterLink to="/register" class="link">Register</RouterLink>
            </div>
        </ContentContainer>
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
    margin-bottom: 2rem;
}

.login-form{
    display: flex;
    flex-direction: column;
    width: fit-content;
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

.warning-message{
    color: var(--color-secondary);
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
}

@media(max-width: 480px){
    .main{
        --main-padding: 5rem 3.5rem;
        --main-margin: 0.5rem auto;
    }
}
</style>