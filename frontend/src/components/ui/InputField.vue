<script setup lang="ts">
const props = defineProps({
    variant: {
        type: String,
        default: 'primary',
    },
    type: {
        type: String,
        default: 'text', 
    },
    id: {
        type: String,
        required: true,    
    },
    size: {
        type: String,
        default: 'middle', 
    },
    placeholder: {
        type: String, 
    }
})
</script>
<template>
   <input 
   :type='`${props.type}`'
   :id='`${props.id}`'
   :class='`input input-${props.variant} input-${props.size}`'
   :placeholder='`${props.placeholder}`'/>
</template>
<style scoped>
.input{
    padding: 1rem;
    border-radius: 1rem;
    /* transition: all 0.2s ease-in-out; */

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