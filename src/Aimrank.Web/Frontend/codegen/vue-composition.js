const path = require('path');

module.exports = {
  plugin: (schema, docs, config, info) => {
    const documents = getInterestedDocuments(docs);

    const imports = [
      `import { Ref } from "vue";`,
      `import { apolloClient } from "${config.apolloImportPath}";`,
      `import { useQuery, useMutation, useSubscription, UseQueryOptions, UseMutationOptions, UseSubscriptionOptions } from "${config.hooksImportPath}";`,
      ...documents.map(op => `import ${toUpperSnakeCase(op.name)} from "${path.relative(info.outputFile, op.location).slice(3)}";`)
    ];

    const types = [
      "type RefWrapper<T extends object> = Record<keyof T, T[keyof T] | Ref<T[keyof T]>>;"
    ];

    const compositionFunctions = documents.map(op => createCompositionFunction(op.type, op.name));

    return [
      imports.join("\n"),
      types.join("\n"),
      compositionFunctions.join("\n")
    ].join("\n\n");
  }
};

const createCompositionFunction = (opType, opName) => {
  const pascal = toPascalCase(opName);

  switch (opType) {
    case "query":
    {
      const result = `${pascal}Query`;
      const variables = `RefWrapper<${pascal}QueryVariables>`;
      return `export const use${pascal} = (options?: Omit<UseQueryOptions<${variables}>, "query">) => useQuery<${result}, ${variables}>(apolloClient, { ...(options ?? {}), query: ${toUpperSnakeCase(pascal)} });`;
    }
    case "mutation":
    {
      const result = `${pascal}Mutation`;
      const variables = `${pascal}MutationVariables`;
      return `export const use${pascal} = (options?: Omit<UseMutationOptions<${variables}>, "mutation">) => useMutation<${result}, ${variables}>(apolloClient, { ...(options ?? {}), mutation: ${toUpperSnakeCase(pascal)} });`;
    }
    case "subscription":
    {
      const result = `${pascal}Subscription`;
      const variables = `RefWrapper<${pascal}SubscriptionVariables>`;
      return `export const use${pascal} = (options?: Omit<UseSubscriptionOptions<${variables}>, "query">) => useSubscription<${result}, ${variables}>(apolloClient, { ...(options ?? {}), query: ${toUpperSnakeCase(pascal)} });`;
    }
  }

  return null;
}

const getInterestedDocuments = (documents) => {
  const result = [];

  for (const document of documents) {
    if (document.document.definitions && document.document.definitions.length) {
      for (const definition of document.document.definitions) {
        if (!["query", "mutation", "subscription"].includes(definition.operation)) {
          continue;
        }

        result.push({
          location: document.location,
          type: definition.operation,
          name: definition.name.value
        });
      }
    }
  }

  return result;
}

const toSnakeCase = (text) => text.replace(/([^A-Z])([A-Z])/g, "$1_$2");
const toPascalCase = (text) => text.charAt(0).toUpperCase() + text.slice(1);
const toUpperSnakeCase = (text) => toSnakeCase(text).toUpperCase();