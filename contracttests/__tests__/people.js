const axios = require('axios')
const chai = require('chai');
const expect = chai.expect;
const chaiResponseValidator = require('chai-openapi-response-validator');

describe(`Ensure Open API Spec is satisfied`, function () {
  beforeAll(async () => {
    
    
    
    
    chai.use(chaiResponseValidator('C:/Dev/OpenApi/WebHost/wwwroot/openapi.json'));
    //OR
    //spec = await axios.get(`http://localhost:11765/openapi.json`);
    //chai.use(chaiResponseValidator(spec.data));
  });

  it('Person Jean Endpoint', async function () {
    const result = await axios.get(`http://localhost:5000/people/1`)
    expect(result.status).to.equal(200);
    expect(result).to.satisfyApiSpec;
  });

  it('Person Andreas Endpoint', async function () {
    const result = await axios.get(`http://localhost:5000/people/2`)
    expect(result.status).to.equal(200);
    expect(result).to.satisfyApiSpec;
  });
});