<script setup lang="ts">
import { ref } from 'vue';

let bg = ref(null);

document.addEventListener("DOMContentLoaded", () => {
	setInterval(() => {
		createCircle();
		createCircle();
		createCircle();
		createCircle();
		createCircle();
	}, 1000);
});

function createCircle() {
	let circle = document.createElement("div");
	circle.classList.add("circle");
	if (Math.random() > 0.35) circle.classList.add("light");
	else circle.classList.add("dark");
	circle.style.setProperty("--pos-x", Math.floor(Math.random() * 100) + "%");
	circle.style.setProperty("--pos-y", Math.floor(Math.random() * 100) + "%");
	circle.style.setProperty("--end-x", Math.floor(Math.random() * 100) + "%");
	circle.style.setProperty("--end-y", Math.floor(Math.random() * 100) + "%");
	console.log(circle);
	setTimeout(() => {
		circle.remove();
	}, 8500);
	bg.appendChild(circle);
}

</script>

<template>
    <div class="page-wrapper" ref="bg">
        <slot></slot>
    </div>
</template>

<style scoped>
.light {
	background-color: #091970;
}

.dark {
	background-color: #010b14;
}

:root {
	--duration: 8s;
}

body {
	margin: 0;
}

.page-wrapper{
	width: 100vw;
	height: 100vh;
}

.bg {
	filter: blur(5vw);
	position: fixed;
	display: block;
	width: 100vw;
	height: 100vh;
}

.circle {
	--pos-x: 50%;
	--pos-y: 50%;
	--end-x: 50%;
	--end-y: 50%;
	position: absolute;
	text-align: center;
	width: min(40vw, 450px);
	height: min(40vw, 450px);
	/* max-width: 450px; */
	/* max-height: 450px; */
	border-radius: 50%;
	display: inline-block;
	animation: move var(--duration) linear normal forwards;
}

@keyframes move {
	0% {
		opacity: 0%;
		left: calc(var(--pos-x) - min(20vw, 225px));
		top: calc(var(--pos-y) - min(20vw, 225px));
	}

	10% {
		opacity: 50%;
	}

	90% {
		opacity: 50%;
	}

	100% {
		opacity: 0%;
		left: calc(var(--end-x) - 225px);
		top: calc(var(--end-y) - 225px);
		display: none;
		visibility: hidden;
	}
}

</style>