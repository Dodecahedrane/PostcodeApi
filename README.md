# PostcodeApi

Current End Point: [https://postcode.azurewebsites.net/](https://postcode.azurewebsites.net/)


**NOTE: This is running on a free tier Azure Web App instance, this is limited to 60 minuites of use per month. So it could stop working at any point. Secondly, as this instance type is not persistent (ie, after each request, it waits a few seconds before shutting down the server). Every time a request is made, and the server is not already online, the first request will take some time to run as the CSV data file is loaded into memory. Subsequent requests will be much quicker, of cource limtied to the shutdown wait time.**

## The Dataset

The data source is the [ONS Postcode Directory](https://geoportal.statistics.gov.uk/datasets/ons::ons-postcode-directory-august-2023/about). It has been modified to remove everything apart from Postcode, Latitude and Longitude.


More data will be added as this project is developed. 


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
  - Status: 500
    - Server Error

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

## TODO

- [ ] Build Instructions
- [ ] Unit Tests for CsvLoader
- [ ] Unit Tests for Postcode Controller
- [ ] Postman End Point Tests
