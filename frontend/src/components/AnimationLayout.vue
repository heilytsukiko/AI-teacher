<script setup lang="ts">

</script>

<template>
    <div class="page-wrapper" ref="bg">
        <slot></slot>
    </div>
</template>

<style scoped>
@property --bg-angle {
  inherits: false;
  initial-value: 0deg;
  syntax: "<angle>";
}

@keyframes spin {
  to {
    --bg-angle: 360deg;
  }
}

.page-wrapper {
  background: 
    radial-gradient(
        circle, 
		var(--color-primary-transparent),
        var(--color-primary)
      ),
    linear-gradient(
        344deg in oklch,
        var(--color-accent-secondary),
		var(--color-accent),
        var(--color-accent-secondary)
      );
  display: grid;
  /* height: 100svh; */
  place-items: center;
}
</style>