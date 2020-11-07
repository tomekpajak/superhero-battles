var faker = require('faker');

var database = { superheros: [] };

for (var i = 1; i<= 300; i++) {
  database.superheros.push({
    "Id": i,
    "Name": faker.name.jobTitle(),
    "Attack": faker.random.number(10),
    "Defence": faker.random.number(10)
  });
}

console.log(JSON.stringify(database));