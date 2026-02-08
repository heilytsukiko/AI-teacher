<script setup lang="ts">
type ButtonVariant = 'primary' | 'accent' 
type ButtonSize = 'small' | 'middle' | 'large'
type ButtonType = 'button' | 'submit' | 'reset'

interface IButtonProps{
    variant?: ButtonVariant,
    size?: ButtonSize,
    type?: ButtonType,
    disabled?: boolean,
<<<<<<< HEAD
    borderRadius?: string;
=======
>>>>>>> ef36d52 (merge branch 'dev' into 'origin/dev')
}

const props = withDefaults(defineProps<IButtonProps>(), {
    variant: 'primary',
    size: 'middle',
    type: 'button',
    disabled: false,
<<<<<<< HEAD
    borderRadius: 'var(--border-radius)',
=======
>>>>>>> ef36d52 (merge branch 'dev' into 'origin/dev')
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
        :class='`button button-${variant} button-${size}`'
        :type="type"
        :disabled="disabled"
        @click="handleEvent"
   >
        <slot></slot>
   </button>
</template>

<style scoped>
.button{
    padding: var(--padding);
<<<<<<< HEAD
    border-radius: v-bind(borderRadius);
=======
    border-radius: var(--border-radius);
>>>>>>> ef36d52 (merge branch 'dev' into 'origin/dev')
    transition: all 0.2s ease-in-out;
    cursor: pointer;

    --btn-color-accent: var(--color-secondary);
}

.button:disabled{
    background-color: transparent;
    color: var(--color-disabled);
    border: 0;
    font-weight: 500;
}

.button:disabled:hover{
    background-color: var(--color-disabled);
    color: var(--color-primary-transparent);
}

/* variants */
.button-primary{
    border: 1px solid var(--btn-color-accent);
    color: var(--btn-color-accent);
    background-color: var(--color-primary-transparent);
}

.button-primary:hover{
    --btn-color-accent: color-mix(in srgb, var(--color-secondary),  var(--color-accent) 50%);

    transform: scale(1.01);
}

.button-primary:active{
    --btn-color-accent: color-mix(in srgb, var(--color-secondary), black 50%);

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
    transform: scale(1.01);
    background-color: var(--btn-color-accent);
}

.button-accent:active{
    transform: scale(1);
}

/* size */
.button-small{
    display: flex;
    width: fit-content;
<<<<<<< HEAD
    height: fit-content;
=======
>>>>>>> ef36d52 (merge branch 'dev' into 'origin/dev')
}

.button-middle{
    width: 15rem;
}

.button-large{
    width: 37rem;
}


@media(max-width: 768px){
    .button-large{
        width: 23.5rem;
    }
}

@media(max-width: 480px){
    .button-large{
        width: 23rem;
    }
}

@media(max-width: 360px){
    .button-large{
        width: 18.75rem;
    }
}
</style>