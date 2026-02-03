<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import MessageInput from '@/components/MessageInput.vue';
import Message from '@/components/Message.vue';

</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main">
            <Message data="aaaaaaaaaa" author="AI"/>
            <Message data="aaaaaaaaaa" author="user"/>
            <MessageInput/>
        </ContentContainer>
    </div>
</template>

<style scoped>
.page-wrapper{
    display: flex;
    flex-direction: row;
    gap: 1rem;
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