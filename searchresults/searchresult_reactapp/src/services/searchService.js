export const searchAllProviders = (searchTerms) => {
  let splitSearchTerms = searchTerms.split(" ");
    return fetch(
      'http://localhost:53855/api/search/all',
      {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(
          splitSearchTerms,
        ),
      },
  )
    .then(r => r.json())
};
