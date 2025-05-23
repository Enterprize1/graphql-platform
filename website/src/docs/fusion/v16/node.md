---
title: "Node Pattern in Fusion"
---

<Video videoId="3iTXFHAvZ-U" />

By leveraging the [**global object identification pattern**](https://graphql.org/learn/global-object-identification/), also known as the **node pattern**, Fusion can work seamlessly with source systems that implement this specification. This integration enables a zero-configuration setup for your distributed GraphQL services and allows you to re-expose fields like `node` through the gateway.

# Understanding the Node Pattern

The **node pattern** is a part of the Relay GraphQL server specifications that provides a way to globally identify objects within a GraphQL schema. It introduces a universal `Node` interface that any object can implement, ensuring each object has a unique identifier. This pattern is particularly beneficial for client applications using [Relay](https://relay.dev), as it allows for efficient data (re-)fetching and caching.

- **Global Object Identification**: Provides a consistent way to reference objects across different subgraphs.
- **Efficient Data Fetching**: Enables clients to refetch or update specific nodes without querying unnecessary data.
- **Seamless Integration**: Allows Fusion to automatically compose and optimize queries across subgraphs that implement the node pattern.

# Implementing the Node Pattern in HotChocolate

To take advantage of the node pattern in Fusion, your source systems need to implement the `Node` interface as defined by the Relay specifications.
This involves adding the global object identification pattern to your schema.

Here's how you can implement the node pattern in HotChocolate:

```csharp
builder.Services
    .AddGraphQLServer()
    .AddTypes() // source generated by HotChocolate.Types.Analyzer
    .AddGlobalObjectIdentification(); // Adds the Node interface and node field
```

Now you need to add a node resolver to your schema:

```csharp
[QueryType]
public static class Query
{
    [NodeResolver]
    public static Product GetProductById(int id, IProductService userService)
    {
        return userService.GetProductById(id);
    }
}
```

By specifying the `NodeResolver` attribute on the `GetProductById` method, you indicate that this method resolves nodes based on their global ID. As `Product` defines a node resolver, HotChocolate will automatically expose the `node` field in the schema, let `Product` implement the `Node` interface, and also infer the `id` field as the global identifier of type `ID!`.

# Zero Configuration Setup with Fusion

Fusion can automatically detect source systems that implement the node pattern, allowing for a zero-configuration setup. By leveraging the global object identification, Fusion can optimize data fetching across subgraphs without additional configuration.

1. **Export Schemas**: Export the schemas from your subgraphs without additional configuration.

   ```bash
   dotnet run --project quick-start.Products -- schema export --output schema.graphql
   ```

2. **Pack Subgraphs**: Pack the subgraph configurations.

   ```bash
   fusion subgraph pack --name Products --schema schema.graphql --output products.fsp
   ```

3. **Compose the Gateway**: Use Fusion to compose the gateway configuration.

   ```bash
   fusion compose -p gateway.fgp -s ../quick-start.Products --enable-nodes
   ```

## Querying the Node Field

Clients can now query for nodes directly via the gateway:

```graphql
{
  node(id: "UHJvZHVjdDoy") {
    ... on Product {
      id
      name
    }
  }
}
```

This query fetches a `Product` node with a specific ID.

# Example Repository

A practical example demonstrating this integration is available in the [HotChocolate Examples Repository](https://github.com/ChilliCream/hotchocolate-examples/tree/master/fusion/relay-schema).

This example also showcases how you can integrate `--enable-nodes` directly with [the Aspire Integration](/docs/fusion/v16/aspire) to simplify the composition process further.
