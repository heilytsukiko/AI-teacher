<script setup lang="ts">
import ThemeBlock from '@components/ui/ThemeBlock.vue';
import HighlightedBlock from '@components/ui/HighlightedBlock.vue';
import useLangStore from '@store/lang';
import type { IContentArrayTextExamples } from '@modules/types/index';


const verbsBasePath = 'secondSection.content'

const langStore = useLangStore();
const verbsArr = langStore.getByPath(verbsBasePath)

const regularVerbs = langStore.getByPath('secondSection.content.0.content')
</script>

<template>
    <ThemeBlock 
        :title="langStore.getText('secondSection.title')"
        class="grammar-second-section"
    >
        <p>{{ langStore.getText('secondSection.description') }}</p>

        <div 
            class="verbs-conteiner"
            v-for="(verbsGroup, parentIndex) in verbsArr"
            :key="parentIndex"
        >
            <h3>
                {{ langStore.getText(`${verbsBasePath}.${parentIndex}.title`) }}
            </h3>

            <div
                class="verbs-examples"
                v-for="(verbType, index) in langStore.getByPath(
                    `${verbsBasePath}.${parentIndex}.content`
                )" 
                :key="index"
            >
                <p> 
                    {{ langStore.getText(`${verbsBasePath}.${parentIndex}.content.${index}.text`) }} 
                </p>

                <HighlightedBlock
                    v-for="verb in langStore.getByPath(
                        `${verbsBasePath}.${parentIndex}.content.${index}.examples`
                    )"
                     :key='verb'
                >
                    <p>{{verb}}</p>
                </HighlightedBlock>
            </div>
        </div>
    </ThemeBlock>
</template>

<style scoped>
.grammar-second-section,
.verbs-conteiner,
.verbs-examples {
    display: flex;
    flex-direction: column;
}

.grammar-second-section{
    gap: 3.125rem;
}

.verbs-conteiner {
    gap: 1.9rem;
}

.verbs-examples {
    gap: 0.9rem;
}
</style>