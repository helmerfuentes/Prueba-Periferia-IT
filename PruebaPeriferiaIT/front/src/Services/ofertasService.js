import ofertasApi from '../Api/ofertasApi';

export default {
  async obtenerOfertas() {
    const ofertas = await ofertasApi.fetchOfertas();
    return ofertas.map(oferta => ({
      ...oferta,
      titulo: oferta.titulo
    }));
  }
};
