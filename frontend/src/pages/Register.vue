<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import InputField from '@components/ui/InputField.vue';
import Button from '@components/ui/Button.vue';

// временная заглушка, чтобы InputField не ругался на остутсвие modelValue
let temporaryStopper: string;
</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main">
            <h1>Register</h1>
            <form action="" class="register-form">
                <label hidden>Username</label>
                <InputField 
                    id="username"  
                    v-model="temporaryStopper"
                    placeholder="Username"
                />
                <label hidden>Email</label>
                <InputField 
                    id="email" 
                    v-model="temporaryStopper"
                    type="email" 
                    placeholder="Email"
                />
                <label hidden>Password</label>
                <InputField 
                    id="password"
                    v-model="temporaryStopper"
                    type="password"
                    placeholder="Password"
                />
                <RouterLink to="/profile"><Button size="large">Log in</Button></RouterLink>
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