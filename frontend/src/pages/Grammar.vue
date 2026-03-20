<script setup lang="ts">
import AnimationLayout from '@/components/AnimationLayout.vue';
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import FirstSection from '@/modules/sections/grammar/FirstSection.vue';
import SecondSection from '@/modules/sections/grammar/SecondSection.vue';
import useLangStore from '@store/lang';
import { onMounted, computed } from 'vue';

const langStore = useLangStore();
const pageName = 'grammar';

onMounted(async () => {
    await langStore.importPage(pageName)
})

interface ITopic {
    id: number,
    topic: string
} 

const isLoading = computed(() => langStore.isLoading)
const topicArr= langStore.getByPath('home.content')
</script>

<template>
    <AnimationLayout>
        <div class="page-wrapper">
            <Header/>

            <ContentContainer class="main" height="var(--main-height)" width="90%">
                <div class="content-wrapper">
                    <h1>{{ langStore.getText('home.title') }}</h1>
                    <p>{{ langStore.getText('home.description')}}</p>
                    <nav class="page-nav">
                        <ul v-for="topic in topicArr" :key="topic">
                            <li>
                                <RouterLink :to="`#${topic}`">
                                    {{ topic }}
                                </RouterLink>
                            </li>
                        </ul>
                    </nav>
                </div>
                <p v-if="isLoading">loading...</p>

                <div class="grammar-sections" v-else>
                    <FirstSection id="tenses-and-time"/>
                    <SecondSection id="Regular-irregular-verbs"/>
                </div>
            </ContentContainer>
        </div>
    </AnimationLayout>
</template>

<style scoped>
.page-wrapper {
    display: flex;
    flex-direction: row;
    gap: 1rem;
    color: var(--font-color);
}

.main {
    margin: 0.5rem auto;
    display: flex;
    flex-direction: column;
    gap: 2rem;
    overflow-y: auto;
    scrollbar-width: none;
    -ms-overflow-style: none;
}

.main h1 {
    margin-bottom: 2rem;
}

.page-nav ul {
    list-style-type: circle;
    margin: 0.5rem;
}

.page-nav a {
    color: var(--font-color);
}

.grammar-sections {
    display: flex;
    flex-direction: column;
    gap: 3.125rem;
}

@media(max-width: 768px){
    .page-wrapper {
        gap: 0;
    }

    .main {
        margin: 0.5rem;
        width: 100vw;
    }
}
</style>