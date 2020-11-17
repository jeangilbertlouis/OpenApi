import http from 'k6/http';
import { check,sleep,group } from 'k6';
import { Trend, Rate, Counter, Gauge } from 'k6/metrics';
import encoding from 'k6/encoding';

export let counterErrors = new Counter('errors');
export let options = {
    thresholds: {
        errors: ['count<1'],
    }
};

export default function(){
    const hostname = 'localhost:44316';
    //const hostname = __ENV.hostname;

    group("OpenAPI Spec exists",function(){
        const res = http.get(`https://${hostname}/openapi.json`);
        var success = check(res,{
            'Status is 200': r => r.status === 200,
        });
        counterErrors.add(!success)
    })

    group("People Endpoint Works",function(){
        const res = http.get(`https://${hostname}/people`);
        var success = check(res,{
            'Status is 200': r => r.status === 200,
        });
        counterErrors.add(!success)
    })

    group("Person Endpoint Works",function(){
        const res = http.get(`https://${hostname}/people/1`);
        var success = check(res,{
            'Status is 200': r => r.status === 200,
            'FirstName is correct': r => r.json('firstName') === "Jean"
        });
        counterErrors.add(!success)
    })
}