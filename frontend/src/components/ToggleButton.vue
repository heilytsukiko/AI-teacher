<script setup lang="ts">
const emit = defineEmits<{
  (e:'checked'): void;
}>();

function handleCheck(){
  emit('checked')
}

</script>

<template>
  <div class="btn btn-pill" id="button-2">
    <input type="checkbox" class="checkbox" @click="handleCheck"/>
    <div class="knob"></div>
    <div class="btn-bg"></div>
  </div>
</template>

<style scoped>
.knob,
.btn-bg {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
}

.btn {
  position: relative;
  width: 65px;
  height: 36px;
  overflow: hidden;
}

.btn.btn-pill,
.btn.btn-pill > .btn-bg {
  border-radius: 100px;
}

.checkbox {
  position: relative;
  width: 100%;
  height: 100%;
  padding: 0;
  margin: 0;
  opacity: 0;
  cursor: pointer;
  z-index: 3;
}

.knob {
  z-index: 2;
}

.btn-bg {
  width: 100%;
  background-color: var(--color-neutral-transparent);
  transition: 0.3s ease all;
  z-index: 1;
}

#button-2 .knob:before,
#button-2 .knob:after {
  content: "";
  position: absolute;
  top: 4px;
  left: 4px;
  width: 28px;
  height: 28px;
  background-color: rgb(110, 81, 27);
  border-radius: 50%;
  transition: 0.3s ease all;
}

#button-2 .knob:after {
  right: -28px;
  left: auto;
  background-color: var(--color-accent-secondary);
}

#button-2 .checkbox:checked + .knob:before {
  left: -28px;
}

#button-2 .checkbox:checked + .knob:after {
  right: 4px;
}

#button-2 .checkbox:checked ~ .btn-bg {
  background-color: var(--color-primary-transparent);
}
</style>