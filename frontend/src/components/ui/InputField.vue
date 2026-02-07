<script setup lang="ts">
import {ref} from 'vue'

type InputVariants = 'primary'
type InputType = 'text' | 'email' | 'password'
type Size = 'small' | 'middle' 
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

const inputElement = ref('no data');

function enter(event: Event){
    inputElement.value = (event.target as HTMLInputElement).value
    emit('update:modelValue', modelValue.value);
}
</script>

<template>
   <input 
        :type="props.type"
        :id="props.id"
        :class="`input input-${props.variant} input-${props.size}`"
        :placeholder="props.placeholder"
        v-model="inputElement"
        @keyup.enter="enter"
        :autocomplete="props.autocomplete"
   />
</template>

<style scoped>
.input{
    padding: var(--padding);
    border-radius: var(--border-radius);
    outline: none;

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
.input-small{
    width: 30rem;
}

.input-middle{
    width: 35rem;
}

@media(max-width: 768px){
    .input-small{
        width: 20rem;
    }

    .input-middle{
        max-width: 300px;
    }
}

@media(max-width: 480px){
    .input-middle{
        max-width: 250px;
    }
}

@media(max-width: 360px){
    .input-middle{
        max-width: 200px;
    }
}
</style>