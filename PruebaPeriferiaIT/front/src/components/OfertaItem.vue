<template>
    <div class="oferta">
      <h3>{{ oferta.titulo }}</h3>
      <p>{{ oferta.descripcion }}</p>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      oferta: Object
    }
  };
  </script>
  
  <style scoped>
  .oferta {
    border: 1px solid #ddd;
    padding: 10px;
    margin: 5px;
  }
  </style>
  