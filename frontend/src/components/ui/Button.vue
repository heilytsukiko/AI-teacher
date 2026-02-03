<script setup lang="ts">
type ButtonVariant = 'primary' | 'accent'
type ButtonSize = 'small' | 'middle' | 'large'

interface IButtonProps{
    variant?: ButtonVariant,
    size?: ButtonSize,
}

const props = withDefaults(defineProps<IButtonProps>(), {
    variant: 'primary',
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
    padding: var(--padding);
    border-radius: var(--border-radius);
    transition: all 0.2s ease-in-out;
    outline: none;

    --btn-color-accent: var(--color-secondary);
}
/* variants */
.button-primary{
    --btn-color-accent: var(--color-secondary);

    border: 1px solid var(--btn-color-accent);
    color: var(--btn-color-accent);
    background-color: var(--color-primary-transparent);
}

.button-primary:hover{
    --btn-color-accent: color-mix(in srgb, var(--color-secondary),  var(--color-accent) 50%);
    
    cursor: pointer;
    transform: scale(1.01);
}

.button-primary:active{
    --btn-color-accent: color-mix(in srgb, var(--color-secondary), black 50%);
    
    cursor: pointer;
    transform: scale(1);
}

.button-accent{
    --btn-color: var(--color-secondary);
    --btn-color-accent: rgba(11, 120, 40, 0.4);

    border: none;
    color: var(--btn-color);
    background-color: transparent;
}

.button-accent:hover{ 
    cursor: pointer;
    transform: scale(1.01);
    background-color: var(--btn-color-accent);
}

.button-accent:active{
    cursor: pointer;
    transform: scale(1);
}

/* size */
.button-small{
    display: flex;
    padding-inline: var(--padding);
}

.button-middle{
    width: 15rem;
}

.button-large{
    width: 37rem;
}
</style>