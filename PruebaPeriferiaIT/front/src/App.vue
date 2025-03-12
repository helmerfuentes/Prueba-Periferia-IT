<template>
  <div id="app">
    <MainView/>
  </div>
</template>

<script>
import MainView from './view/main.vue';

export default {
  name: 'App',
  components: {
    MainView
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
