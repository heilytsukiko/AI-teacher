<script setup lang="ts">
type ButtonVariant = 'accent'
type ButtonSize = 'small' | 'middle' | 'large'

interface IButtonProps{
    variant?: ButtonVariant,
    size?: ButtonSize,
}

const props = withDefaults(defineProps<IButtonProps>(), {
    variant: 'accent',
    size: 'middle'
})

const emit = defineEmits<{
    (e:'button-сlick'): void;
}>();

function handleEvent(){
    emit('button-сlick')
}
</script>

<template>
   <button 
        :class='`button button-${props.variant} button-${props.size}`'
        @click="handleEvent"
   >
        <slot></slot>
   </button>
</template>

<style scoped>
.button{
    padding: 1rem;
    border-radius: 1rem;
    transition: all 0.2s ease-in-out;

    --btn-color-accent: var(--color-secondary);
}
/* variants */
.button-accent{
    --btn-color-accent: var(--color-secondary);

    border: 1px solid var(--btn-color-accent);
    color: var(--btn-color-accent);
    background-color: var(--color-primary-transparent);
}

.button-accent:hover{
    --btn-color-accent: color-mix(in srgb, var(--color-secondary),  var(--color-accent) 50%);
    cursor: pointer;
    transform: scale(1.01);
}

.button-accent:active{
    --btn-color-accent: color-mix(in srgb, var(--color-secondary), black 50%);
    cursor: pointer;
    transform: scale(1);
}

/* size */
.button-small{
    display: flex;
    padding-inline: 1rem;
}

.button-middle{
    width: 15rem;
}

.button-large{
    width: 37rem;
}
</style>