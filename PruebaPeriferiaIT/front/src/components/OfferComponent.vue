<template>
  <div>
    <q-select
      v-model="selectedOffer"
      :options="offerOptions"
      label="Oferta"
      option-label="name"
      option-value="id"
      outlined
      dense
      emit-value
      map-options
      @update:model-value="updateOfferDetails"
    />

    <div class="q-mt-md">
      <q-input v-model="offerDetails.id" label="ID Oferta:" readonly outlined dense />
      <q-input v-model="offerDetails.name" label="Nombre:" readonly outlined dense />
    </div>
  </div>
</template>

<script>
import { useOfertasStore } from "@/store/ofertasStore";
import { computed, ref, watch } from "vue";

export default {
  name: "OfferComponent",
  setup() {
    const store = useOfertasStore();
    const selectedOffer = ref(null);
    const offerDetails = ref({ id: "", name: "" });

    const offerOptions = computed(() =>
      store.ofertas.flatMap((offer) =>
        offer.versions.map((version) => ({
          id: version.id,
          name: version.name,
        }))
      )
    );

    const updateOfferDetails = (id) => {
      store.seleccionarOferta(id);
      const offer = offerOptions.value.find((o) => o.id === id);
      if (offer) {
        offerDetails.value = { id: offer.id, name: offer.name };
      }
    };

    watch(selectedOffer, (newId) => updateOfferDetails(newId));

    return {
      selectedOffer,
      offerOptions,
      offerDetails,
      updateOfferDetails,
    };
  },
};
</script>
