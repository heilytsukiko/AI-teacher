<script setup lang="ts">
import AnimationLayout from '@/components/AnimationLayout.vue';
import Header from '@components/Header.vue';
import ContentContainer from '@components/ContentContainer.vue';
</script>

<template>
    <AnimationLayout>
        <div class="page-wrapper">
            <Header/>

            <ContentContainer class="main" height="var(--main-height)" width="90%">
                <section class="section-1">
                    <h1>Grammar</h1>
                    <p>Topics: </p>
                    <nav>
                        <ul>
                            <li><RouterLink to="#tenses-and-time">Tenses and time</RouterLink></li>
                        </ul>
                    </nav>
                </section>
                <section id="tenses-and-time">
                    <h2>Tenses and time</h2>
                    <p>text</p>
                </section>
            </ContentContainer>
        </div>
    </AnimationLayout>
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
    flex-direction: column;
    gap: 2rem;
    overflow-y: auto;
    scrollbar-width: none;
    -ms-overflow-style: none;
}

.main::-webkit-scrollbar {
    display: none;
    width: 0;
    height: 0; 
}

.section-1{
    margin-bottom: 50rem;
}

@media(max-width: 960px){

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