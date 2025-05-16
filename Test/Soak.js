import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
  vus: 20, // usuarios concurrentes
  duration: '30m', // prueba larga
};

export default function () {
  http.get('http://localhost:5225/Students/Subjects/Subject/1/Formula/1');
  sleep(2); // reduce la frecuencia para simular usuarios reales
}
