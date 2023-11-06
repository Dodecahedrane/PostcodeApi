# PostcodeApi

Current End Point: [https://postcode.azurewebsites.net/](https://postcode.azurewebsites.net/)

## Get Methods

### GET /Postcode
- Returns details of Postcode provided in body, if it exists

- Request Body:
`{
  "postcode": "PL4 8AA"
}`

- Response: 
  - Status: 200
  - Body: `{
    "postcode": "PL48AA",
    "latitude": 50.375438,
    "longitude": -4.13794
}`

### GET /AllPostcodes
- Returns all postcodes and their details within the dataset, no request body needed.

- Response: 
  - Status: 200
  - Body: Returns full list of all postcodes (~2.5M approx.) Format same as above, but in JSON array

### GET /PartialPostcode
- Matches a partial postcode

- Request Body:
`{
  "postcode": "PL4 8A"
}`

- Response: 
  - Status: 200
  - Body: Body: 
`[
    {
        "postcode": "PL48AA",
        "latitude": 50.375438,
        "longitude": -4.13794
    },
    {
        "postcode": "PL48AB",
        "latitude": 50.374816,
        "longitude": -4.137278
    },
    {.............
]`
