<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import Button from '@/components/ui/Button.vue';

const user = {
    img: '/avatar.jpg',
    username: 'Name',
    email: 'email@gmail.com',
    level: 'userlevel'
}
</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main" height="var(--main-height)" width="90%">
            <div class="column-1">
                <img :src="user.img" alt="avatar" class="profile-img">
                <Button disabled variant="accent" size="small">Edit profile</Button>
            </div>
            <div class="user-info-wrapper">
                <h2 class="username">{{ user.username }}</h2>
                <p>Your level:  {{ user.level }}</p>
                <p>Email: {{ user.email }}</p>
            </div>
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
    flex-direction: row;
    gap: 2rem;

    --profile-img-size: 10rem;
}

.column-1{
    display: flex;
    flex-direction: column;
}

.profile-img{
    width: var(--profile-img-size);
    height: var(--profile-img-size);
    border-radius: 50%;
    filter: hue-rotate(210deg) saturate(250%);
    margin-bottom: 2rem;
}

.column-1 button{
    margin-inline: auto;
}

.user-info-wrapper{
    margin-top: 0.7rem;
    color: var(--color-secondary);
}

.user-info-wrapper *{
    margin-bottom: 0.7rem;
}

@media(max-width: 960px){
    .main{
        --profile-img-size: 8rem;
    }
}

@media(max-width: 768px){
    .page-wrapper{
        gap: 0;
    }

    .main{
        margin: 0.5rem;
        width: 100vw;
    }
}
</style>