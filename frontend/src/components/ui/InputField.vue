<script setup lang="ts">
type InputVariants = 'primary'
type InputType = 'text' | 'email' | 'password'
type Size = 'middle' 

interface IInputProps {
    variant?: InputVariants,
    type?: InputType,
    id: String,
    size?: Size,
    placeholder: String,
}

const props = withDefaults(defineProps<IInputProps>(),{
    variant: 'primary',
    type: 'text',
    size: 'middle',
})
</script>

<template>
   <input 
        :type='`${props.type}`'
        :id='`${props.id}`'
        :class='`input input-${props.variant} input-${props.size}`'
        :placeholder='`${props.placeholder}`'
   />
</template>

<style scoped>
.input{
    padding: 1rem;
    border-radius: 1rem;

    --btn-color-accent: var(--color-secondary);
}

input[type="text"]:focus{
    outline: none;
}

/* variants */
.input-primary{
    background-color: var(--color-primary);
    border: 1px solid var(--color-secondary);
    color: var(--color-secondary);
}

/* size */
.input-middle{
    width: 35rem;
}
</style>