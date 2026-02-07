<script setup lang="ts">
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import MessageInput from '@/components/MessageInput.vue';
import Message from '@/components/Message.vue';
</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main" height="var(--main-height)" width="fit-content">
            <div class="messages-content">
                <Message data="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
                <Message data="aaaaaaaaaa" author="AI"/>
                <Message data="aaaaaaaaaa" author="user"/>
            </div>
            <MessageInput class="input-line-wrapper"/>
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
    display: flex;
    flex-direction: column;
    gap: 1rem;
    margin: 0.5rem;
}

.messages-content{
    overflow-y: auto;
    height: 100%;
    scrollbar-width: none;
    -ms-overflow-style: none;
}

.messages-content::-webkit-scrollbar {
    display: none;
    width: 0;
    height: 0; 
}

.input-line-wrapper{
    margin-inline: auto;
}

@media(max-width: 960px){
    .page-wrapper{
        gap: 0;
    }

    .main{
        margin: 0.5rem;
    }
}
</style>