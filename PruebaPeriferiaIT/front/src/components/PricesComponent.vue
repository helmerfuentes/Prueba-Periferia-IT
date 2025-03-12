<template>
  <div class="q-pa-md">
    <h4 class="text-center">Precios</h4>

    <div class="row header">
      <div class="col">Nombre Precio</div>
      <div class="col">Monto</div>
    </div>

    <div v-for="(price, index) in prices" :key="index" class="row">
      <div class="col">{{ price.name }}</div>
      <div class="col">{{ price.amount.toFixed(2) }}</div>
    </div>
  </div>
</template>

<script>
import { useOfertasStore } from "@/store/ofertasStore";
import { computed } from "vue";

export default {
  name: "PricesComponent",
  setup() {
    const store = useOfertasStore();

    const prices = computed(() => {
      if (!store.selectedOffer) return [];
      return store.selectedOffer.productOfferingPrices.flatMap(p => 
        p.versions.map(v => ({
          name: v.name,
          amount: v.price.amount
        }))
      );
    });

    return { prices };
  }
};
</script>

<style scoped>
.header {
  font-weight: bold;
  background: crimson;
  color: white;
  padding: 5px;
  text-align: center;
}
.row {
  border: 1px solid crimson;
  padding: 5px;
}
</style>
