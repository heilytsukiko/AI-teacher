<script setup lang="ts">
import {ref} from 'vue'

type InputVariants = 'primary'
type InputType = 'text' | 'email' | 'password'
type Size = 'small' | 'middle' 
type Autocomplete = 'off' | 'on'

interface IInputProps {
    value?: string,
    variant?: InputVariants,
    size?: Size,
    type?: InputType,
    id: string,
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
    (e: 'update-value', value: string): void
}>()

const data = ref<string>("")

function enter(){
    if(data.value.trim().length !== 0){
        emit('update-value', data.value)
        data.value = props.value
    }
}
</script>

<template>
   <input 
        :class="`input input-${variant} input-${size}`"
        :type="type"
        :id="id"
        :placeholder="placeholder"
        :autocomplete="autocomplete"
        v-model="data"
        @keyup.enter="enter"
   />
</template>

<style scoped>
.input{
    padding: var(--padding);
    border-radius: var(--border-radius);

    --btn-color-accent: var(--color-secondary);
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