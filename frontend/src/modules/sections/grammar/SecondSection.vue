<script setup lang="ts">
import ThemeBlock from '@components/ui/ThemeBlock.vue';
import HighlightedBlock from '@components/ui/HighlightedBlock.vue';
import useLangStore from '@store/lang';

const langStore = useLangStore();

const regularVerbsPath = 'secondSection.content.0'
const irregularVerbsPath = 'secondSection.content.1'
</script>

<template>
    <ThemeBlock 
        :title="langStore.getText('secondSection.title')"
        class="grammar-second-section">
        <p>{{ langStore.getText('secondSection.description') }}</p>

        <h3>{{ langStore.getText(`${regularVerbsPath}.title`) }}</h3>

        <p>{{ langStore.getText(`${regularVerbsPath}.content.0.text`) }}</p>
        <HighlightedBlock 
            v-for="verb in langStore.getByPath(`${regularVerbsPath}.content.0.examples`)"
            class="grammar-second-section-list">
                <p :key='verb'>{{verb}}</p>
        </HighlightedBlock>

        <p>{{ langStore.getText(`${regularVerbsPath}.content.1.text`) }}</p>
        <HighlightedBlock 
            v-for="verb in langStore.getByPath(`${regularVerbsPath}.content.1.examples`)"
            class="grammar-second-section-list">
                <p :key='verb'>{{verb}}</p>
        </HighlightedBlock>

        <p>{{ langStore.getText(`${regularVerbsPath}.content.2.text`) }}</p>
        <HighlightedBlock 
            v-for="verb in langStore.getByPath(`${regularVerbsPath}.content.2.examples`)"
            class="grammar-second-section-list">
                <p :key='verb'>{{verb}}</p>
        </HighlightedBlock>

        <p>{{ langStore.getText(`${regularVerbsPath}.content.3.text`) }}</p>
        <HighlightedBlock 
            v-for="verb in langStore.getByPath(`${regularVerbsPath}.content.2.examples`)"
            class="grammar-second-section-list">
                <p :key='verb'>{{verb}}</p>
        </HighlightedBlock>

        <h3>{{ langStore.getText(`${irregularVerbsPath}.title`) }}</h3>

        <p>{{ langStore.getText(`${irregularVerbsPath}.content.0.text`) }}</p>
        <HighlightedBlock 
            v-for="verb in langStore.getByPath(`${irregularVerbsPath}.content.0.examples`)"
            class="grammar-second-section-list">
                <p :key='verb'>{{verb}}</p>
        </HighlightedBlock>


    </ThemeBlock>
</template>

<style scoped>
.grammar-second-section {
    display: flex;
    flex-direction: column;
    gap: 1.4rem;
}

.text-content-list li {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

.grammar-second-section-list {
    display: flex;
    flex-direction: column;
    gap: 10px;
}
</style>