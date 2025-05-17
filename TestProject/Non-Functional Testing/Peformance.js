import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  vus: 10, // usuarios virtuales
  duration: '60s',
};

export default function () {
  let res = http.get('http://localhost:5225/School/Subjects');
  check(res, {
    'status es 200': (r) => r.status === 200,
    'tiempo de respuesta < 500ms': (r) => r.timings.duration < 500,
  });
  sleep(1);
}
