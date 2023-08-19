# US National Park Biodiversity API

Welcome to the US National Park Biodiversity API! This API provides access to detailed information about US National Parks and the various species found within them. Parks and species dataset were downloaded from [kaggle.com](https://www.kaggle.com/datasets/nationalparkservice/park-biodiversity).

## Table of Contents

- [Getting Started](#getting-started)
- [Endpoints](#endpoints)
  - [Parks](#parks)
  - [Species](#species)
- [Data Models](#data-models)
  - [Park Model](#park-model)
  - [Species Model](#species-model)
  - [Paginated Species List Model](#paginated-species-list-model)
- [Support and Feedback](#support-and-feedback)
- [License](#license)

## Getting Started

To get started with using the API, follow these steps:

1. [Provide setup instructions if needed].
2. Use the endpoints detailed below to fetch the desired information.

## Endpoints

### Parks

#### **GET** `/api/Parks`

- Fetches a list of all national parks.

#### **GET** `/api/Parks/{id}`

- Fetches details of a specific park using its ID.
  
#### **GET** `/api/Parks/{id}/Species`

- Fetches a list of species found in a specific park using the park's ID.

#### **POST** `/api/Parks`

- Creates a new park entry.

#### **PUT** `/api/Parks/{id}`

- Updates details of a specific park using its ID.

#### **DELETE** `/api/Parks/{id}`

- Deletes a specific park using its ID.

### Species

#### **GET** `/api/Species`

- Fetches a paginated list of species.
- Parameters:
  - `page`: The page number (default is 1).
  - `pageSize`: Number of species per page (default is 100).
- Includes pagination metadata (see [Paginated Species List Model](#paginated-species-list-model))

#### **GET** `/api/Species/{id}`

- Fetches details of a specific species using its ID.

#### **GET** `/api/Species/CommonName/{commonName}/Parks`

- Fetches parks where a specific species (by common name) is found.

#### **GET** `/api/Species/ScientificName/{scientificName}/Parks`

- Fetches parks where a specific species (by scientific name) is found.

#### **GET** `/api/Species/Search`

- Searches for species by their common name or scientific name.
- Not case sensitive, matches partial strings.
- Parameters:
  - `commonName`: The common name of the species.
  - `scientificName`: The scientific name of the species.
- Example: `/api/Species/Search?commonName=Moose` returns mammals called "Moose" and plants like "Moosewood" and "Moosebush".

#### **POST** `/api/Species`

- Creates a new species entry.

#### **PUT** `/api/Species/{id}`

- Updates details of a specific species using its ID.

#### **DELETE** `/api/Species/{id}`

- Deletes a specific species using its ID.

## Data Models

### Park Model

```json
{
  "id": int,
  "parkCode": "string",
  "parkName": "string",
  "state": "string",
  "acres": int,
  "latitude": double,
  "longitude": double
}
```

### Species Model

```json
{
  "id": integer,
  "speciesId": "string",
  "parkName": "string",
  "category": "string",
  "order": "string",
  "family": "string",
  "scientificName": "string",
  "commonNames": "string",
  "recordStatus": "string",
  "occurrence": "string",
  "nativeness": "string",
  "abundance": "string",
  "seasonality": "string",
  "conservationStatus": "string",
  "parkId": integer
}
```

### Paginated Species List Model

```json
{
    "metadata": {
        "totalCount": integer,
        "pageSize": integer,
        "currentPage": integer,
        "totalPages": integer,
        "hasPrevious": bool,
        "hasNext": bool
    },
    "data": [
        {
          "id": integer,
          "speciesId": "string",
          "parkName": "string",
          "category": "string",
          "order": "string",
          "family": "string",
          "scientificName": "string",
          "commonNames": "string",
          "recordStatus": "string",
          "occurrence": "string",
          "nativeness": "string",
          "abundance": "string",
          "seasonality": "string",
          "conservationStatus": "string",
          "parkId": integer
        },
        // remaining list of species
    ]
}
```

## Support and Feedback

For any support or feedback, please contact austin@papritz.dev or raise an issue on the repository.

## License

This API is not licensed and is free to use.
