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
  - Status: 404 Postcode Not Found
    - The submitted postcode does not exist within the dataset   
  - Status: 400 Bad Request
    - The body is null  
    - The format of the body is incorrect
    - The postcode is invalid

### GET /AllPostcodes
- Returns all postcodes and their details within the dataset, no request body needed.

- Response: 
  - Status: 200
  - Body: Returns full list of all postcodes (~2.5M approx.) Format same as above, but in JSON array
  - Status: 500
    - Server Error

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
- Status: 404 No Matches Found
    - The submitted partial postcode does not have any matches within the dataset   
  - Status: 400 Bad Request
    - The body is null  
    - The format of the body is incorrect
  - Status: 500
    - Server Error
