import { defineStore } from 'pinia';
import ofertasService from '../Services/ofertasService';

export const useOfertasStore = defineStore('ofertas', {
  state: () => ({
    ofertas: [],
    selectedOffer: null
  }),
  actions: {
    async cargarOfertas() {
      this.ofertas = await ofertasService.obtenerOfertas();
    },
    seleccionarOferta(id) {
        this.selectedOffer = this.ofertas
          .flatMap((offer) => offer.versions)
          .find((version) => version.id === id) || null;
      },
  }
});
