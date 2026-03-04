<script setup lang="ts">
import ThemeBlock from '@components/ui/ThemeBlock.vue';

</script>

<template>
    <ThemeBlock title="Tenses and time" class="grammar-first-section">
        <table class="tenses-and-time-table">
            <thead>
                <th></th>
                <th>Simple</th>
                <th>Continuous</th>
                <th>Perfect</th>
                <th>Perfect Continuous</th>
            </thead>
            <tr>
            <th>Present</th>
                <td>I work</td>
                <td>I am working</td>
                <td>I have worked</td>
                <td>I have been working</td>
            </tr>
            <tr>
                <th>Past</th>
                <td>I worked</td>
                <td>I was working</td>
                <td>I had worked</td>
                <td>I had been working</td>
            </tr>
            <tr>
                <th>Future</th>
                <td>I will work</td>
                <td>I will be working</td>
                <td>I will have worked</td>
                <td>I will have been working</td>
            </tr>
        </table>
    </ThemeBlock>
</template>

<style scoped>
.tenses-and-time-table {
    table-layout: fixed;
    width: 100%;
    border: 2px solid var(--color-primary-transparent);
    border-collapse: collapse;
}

.tenses-and-time-table th,
.tenses-and-time-table td {
    padding: 15px;
    border: 1px solid var(--color-primary-transparent);
}

</style>