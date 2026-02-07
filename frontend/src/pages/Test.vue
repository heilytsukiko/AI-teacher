<script setup lang="ts">
import { ref } from 'vue';
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import MessageInput from '@components/MessageInput.vue';
import Message from '@components/Message.vue';
import type { Author } from '@components/Message.vue';

interface IMessages {
    content: string,
    author: Author,
}

const messages = ref<IMessages[]>([
    { content: 'message, ura', author: 'AI'},
    { content: 'message from user', author: 'user'},
])

function addMessage(payload: string){
    messages.value.push({content: payload, author: 'user'})
}
</script>

<template>
    <div class="page-wrapper">
        <Header/>

        <ContentContainer class="main" height="var(--main-height)" width="fit-content">
            <div class="messages-content">
                <div 
                    v-for="msg in messages"
                    key="msg"
                    :class="msg.author === 'AI' ? 'align-left' : 'align-right'"
                >
                    <Message 
                        :data="msg.content" 
                        :author="msg.author"
                    />
                </div>
            </div>
            <MessageInput class="input-line-wrapper" @send-message="addMessage"/>
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
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
}

.align-left{
    align-self: start;
}

.align-right{
    align-self: end;
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