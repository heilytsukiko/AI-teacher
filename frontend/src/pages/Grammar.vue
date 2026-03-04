<script setup lang="ts">
import AnimationLayout from '@/components/AnimationLayout.vue';
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
import FirstSection from '@/modules/sections/grammar/FirstSection.vue';
import SecondSection from '@/modules/sections/grammar/SecondSection.vue';
</script>

<template>
    <AnimationLayout>
        <div class="page-wrapper">
            <Header/>

            <ContentContainer class="main" height="var(--main-height)" width="90%">
                <div class="content-wrapper">
                    <h1>Grammar</h1>
                    <p>Topics: </p>
                    <nav class="page-nav">
                        <ul>
                            <li><RouterLink to="#tenses-and-time">Tenses and time</RouterLink></li>
                            <li><RouterLink to="#Regular-irregular-verbs">Regular and irregular verbs</RouterLink></li>
                            <li><RouterLink to="#tenses-and-time">Tenses and time</RouterLink></li>
                        </ul>
                    </nav>
                </div>

                <FirstSection id="tenses-and-time"/>
                <SecondSection id="Regular-irregular-verbs"/>
            </ContentContainer>
        </div>
    </AnimationLayout>
</template>

<style scoped>
.page-wrapper{
    display: flex;
    flex-direction: row;
    gap: 1rem;
    color: var(--font-color);
}

.main{
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

.page-nav ul{
    list-style-type: circle;
    margin: 0.5rem;
}

/* .page-nav  */

.page-nav a{
    color: var(--font-color);
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