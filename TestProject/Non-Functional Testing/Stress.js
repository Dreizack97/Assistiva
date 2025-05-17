import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
  stages: [
    { duration: '1m', target: 100 }, // incrementa a 100 usuarios
    { duration: '3m', target: 100 }, // mantiene la carga
    { duration: '1m', target: 0 },   // desacelera
  ],
};

export default function () {
  http.get('http://localhost:5225/Students/Subjects/Subject/1/Formula/1');
  sleep(1);
}
