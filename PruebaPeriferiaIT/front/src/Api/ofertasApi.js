import axios from 'axios';

const API_URL = '/ofertas.json';

export default {
  async fetchOfertas() {
    try {
      const response = await axios.get(API_URL);
      return response.data;
    } catch (error) {
      console.error('Error obteniendo ofertas:', error);
      throw error;
    }
  }
};
