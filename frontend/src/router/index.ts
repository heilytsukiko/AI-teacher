import { createRouter, createWebHistory } from 'vue-router'
import Home from '../pages/Home.vue'
import Register from '@/pages/Register.vue'
import Login from '@/pages/Login.vue'
import Profile from '@/pages/Profile.vue'
import Chat from '@/pages/Chat.vue'

const routes = [
  { path: '/', component: Home},
  { path: '/login', component: Login},
  { path: '/register', component: Register},
  { path: '/profile', component: Profile},
  { path: '/chat', component: Chat},
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
