![Workflow Badge](https://github.com/Dodecahedrane/PostcodeApi/actions/workflows/dotnet_deploy.yaml/badge.svg)
![CodeQL](https://github.com/Dodecahedrane/PostcodeApi/actions/workflows/codeql.yml/badge.svg)

# PostcodeApi

Current End Point: [https://postcode.azurewebsites.net/](https://postcode.azurewebsites.net/)

**NOTE: This is running on a free tier Azure Web App instance, this is limited to 60 CPU minuites per day. So it could stop working at any point. Secondly, as this instance type is not persistent (ie, after each request, it waits a few seconds before shutting down the server). Every time a request is made, and the server is not already online, the first request will take some time to run as the CSV data file is loaded into memory. Subsequent requests will be much quicker, of course limtied to the shutdown wait time.**

## The Dataset

The data source is the [ONS Postcode Directory](https://geoportal.statistics.gov.uk/datasets/ons::ons-postcode-directory-august-2023/about). It has been modified to remove everything apart from Postcode, Latitude and Longitude. This is under the [Open Government Licence](https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/)


More data will be added as this project is developed. 

## The Implementation

The CSV file that stores the postcode data is loaded into memory when the first request is made (and the PostcodeLoader class is instantiated). It is loaded into a dictionary. I performed [some benchmarks](https://github.com/Dodecahedrane/PostcodeTest/tree/master) on a previous implemntation to see what performs best for this usecase.

## API Documentation

**[Swagger Documentation](https://postcode.azurewebsites.net/swagger/index.html)**

### GET /Postcode
- Returns details of Postcode provided in body, if it exists

- URL Parmeter: `postcode`

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
    - The format is incorrect
    - The postcode is invalid
  - Status: 500
    - Server Error

### GET /AllPostcodes
- Returns all postcodes and their details within the dataset, no request body needed.

- Response: 
  - Status: 200
    - Body: Returns full list of all postcodes (~2.5M approx.) Format same as above, but in JSON array
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
  - Status: 500
    - Server Error

### GET /PartialPostcode
- Matches a partial postcode
- URL Parmeter: `postcode`
- Response: 
  - Status: 200
    - Body: 
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
  - The format is incorrect
- Status: 500
  - Server Error
 
### GET /ValidatePostcode
- Retunrs true/false depending on if the postcode is valid or not
- URL Parmeter: `postcode`
- Response: 
  - Status: 200
    - Body: true/false  
- Status: 400 Bad Request
  - The body is null  
  - The format is incorrect
- Status: 500
  - Server Error

## TODO

- [ ] Build Instructions
- [x] Unit Tests for PostcodeLoader - This will require a refactor to get the `CsvFilePath` as an enviroment variable instead of having it hard coded
- [x] Unit Tests for Postcode Controller
- [ ] Postman End Point Integration Tests
- [ ] Docker Container
- [x] Valid Postcode Endpoint
