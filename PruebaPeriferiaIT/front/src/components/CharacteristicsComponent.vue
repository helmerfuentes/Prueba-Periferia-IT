<template>
  <div class="q-pa-md">
    <h4 class="text-center">Características</h4>
    <q-list separator>
      <q-item v-for="(item, index) in characteristics" :key="index" class="characteristic-item">
        <q-item-section class="bold-text">{{ item }}</q-item-section>
      </q-item>
    </q-list>
  </div>
</template>

<script>
import { useOfertasStore } from "@/store/ofertasStore";
import { computed } from "vue";

export default {
  name: "CharacteristicsComponent",
  setup() {
    const store = useOfertasStore();

    const characteristics = computed(() =>
      store.selectedOffer
        ? store.selectedOffer.characteristics.flatMap((char) =>
            char.versions.map((version) => version.name)
          )
        : []
    );

    return {
      characteristics,
    };
  },
};
</script>

<style scoped>
.bold-text {
  font-weight: bold;
}

.characteristic-item {
  border-bottom: 1px solid red;
  padding: 8px 0;
}

.characteristic-item:last-child {
  border-bottom: none;
}
</style>
