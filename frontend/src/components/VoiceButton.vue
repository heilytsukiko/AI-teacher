<script setup lang="ts">
import Button from '@components/ui/Button.vue';
</script>

<template>
   <Button
        size="small"
        class="voice-button"
   >
        <span></span>
   </Button>
</template>

<style scoped>
.voice-button span{
    display: inline-block;
    width: 20px;
    height: 20px;
    background-image: url('@/assets/micro.svg');
    background-size: contain; 
    background-repeat: no-repeat;
    background-position: center;
}
.voice-button:hover span{
    filter: sepia(1) saturate(1000%) hue-rotate(60deg) brightness(95%);
}
</style>