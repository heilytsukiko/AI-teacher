<script setup lang="ts">
import ThemeBlock from '@components/ui/ThemeBlock.vue';
import HighlightedBlock from '@components/ui/HighlightedBlock.vue';
</script>

<template>
    <ThemeBlock title="Regular and irregular verbs" class="grammar-second-section">
        <p>According to the method of forming the past indefinite tense and the second participle, all English verbs are divided into regular and irregular.</p>

        <h3>Regular verbs</h3>
        <p>Most English verbs are regular verbs, the formation of the past indefinite tense and participle II of which is carried out using the ending -ed:</p>
        <HighlightedBlock>
            <p>look – looked</p>
            <p>stop – stopped</p>
            <p>cry – cried</p>
        </HighlightedBlock>

        <p>If the stem ends in -e, then after adding -ed, only one letter -e is written:</p>
        <HighlightedBlock>
            <p>like – liked</p>
        </HighlightedBlock>

        <p>If the verb stem ends in a consonant and -y, then y changes to -i:</p>
        <HighlightedBlock>
            <p>study – studied</p>
        </HighlightedBlock>

        <p>In British English, if this final consonant is -l, it is doubled regardless of stress:</p>
        <HighlightedBlock>
            <p>to travel – travelled</p>
        </HighlightedBlock>

        <h3>Irregular verbs</h3>
        <p>There is also a group of verbs in English for which the second and third forms are formed in a special way. These are called irregular English verbs.</p>
        <p>There are several main ways of forming irregular verb forms:</p>
        <ol class="text-content-list">
            <li>
                <p>Adding the ending -t or -d while changing or preserving the vowel at the root:</p>
                <HighlightedBlock class="text-content-list-element">
                    <p>bind - bound - bound</p>
                    <p>bring - brought - brought</p>
                </HighlightedBlock>
            </li>
        </ol>
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
</style>