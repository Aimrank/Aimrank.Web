module.exports = {
  client: {
    service: {
      name: "aimrank",
      url: "http://localhost:5000/graphql"
    },
    includes: [
      "src/**/*.vue",
      "src/**/*.ts",
      "src/**/*.gql"
    ]
  }
}