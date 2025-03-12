<template>
    <div>
      <q-card class="q-pa-md bordered-box my-card">
        <div class="q-pa-md example-row-horizontal-alignment content-main">
          <div class="row justify-center">
            <div class="col-8">
              <OfferComponent />
            </div>
            <div class="col-8 q-mt-md">
              <div class="row justify-around" v-if="selectedOffer">
                <div class="col chr-component">
                  <CharacteristicsComponent :offer="selectedOffer" />
                </div>
                <div class="col">
                  <PricesComponent :offer="selectedOffer" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </q-card>
    </div>
  </template>
  
  <script>
  import PricesComponent from "@/components/PricesComponent.vue";
import CharacteristicsComponent from "../components/CharacteristicsComponent.vue";
import OfferComponent from "../components/OfferComponent.vue";
import { useOfertasStore } from "../store/ofertasStore";
  
  export default {
    name: "MainView",
    components: {
      OfferComponent,
      CharacteristicsComponent,
      PricesComponent,
    },
    computed: {
      ofertas() {
        return useOfertasStore().ofertas;
      },
      selectedOffer() {
        return useOfertasStore().selectedOffer;
      },
    },
    async created() {
      await useOfertasStore().cargarOfertas();
    },
  };
  </script>
  
  <style scoped>
  .my-card {
    width: 75%;
    margin: 0 auto;
    padding: 0;
    margin-top: 30px;
  }
  
  .content-main {
    height: 750px;
  }
  
  .chr-component {
    height: 538px;
    overflow: auto;
  }
  </style>
  