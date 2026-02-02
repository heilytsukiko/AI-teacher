<script setup lang="ts">
type InputVariants = 'primary'
type InputType = 'text' | 'email' | 'password'
type Size = 'middle' 
type Autocomplete = 'off' | 'on'

interface IInputProps {
    modelValue: string,
    variant?: InputVariants,
    type?: InputType,
    id: string,
    size?: Size,
    placeholder: string,
    autocomplete?: Autocomplete,
}

const props = withDefaults(defineProps<IInputProps>(),{
    variant: 'primary',
    type: 'text',
    size: 'middle',
    autocomplete: 'off'
})

const emit = defineEmits<{
    (e: 'enter'): void,
    (e: 'update:modelValue', value: string): void
}>()

function enter(event: Event){
    const inputElement = event.target as HTMLInputElement;
    emit('update:modelValue', inputElement.value);
}
</script>

<template>
   <input 
        :type="props.type"
        :id="props.id"
        :class="`input input-${props.variant} input-${props.size}`"
        :placeholder="props.placeholder"
        :modelvalue="modelValue"
        @input="emit('update:modelValue', ($event.target as HTMLInputElement).value)"
        @keyup.enter="emit('enter')"
        :autocomplete="props.autocomplete"
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