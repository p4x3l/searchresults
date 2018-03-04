const data = [
  {
    "searchType": "google",
    "searchResults": [
      {
        "searchTerm": "string",
        "searchCount": 9390000
      }
    ]
  },
  {
    "searchType": "ebay",
    "searchResults": [
      {
        "searchTerm": "string",
        "searchCount": 1836133
      }
    ]
  },
  {
    "searchType": "yahoo",
    "searchResults": [
      {
        "searchTerm": "string",
        "searchCount": 42800000
      }
    ]
  }
];

export const searchAllProviders = () => (
  new Promise(function(resolve, reject) {
    setTimeout(resolve, 100, data);
  })
);
