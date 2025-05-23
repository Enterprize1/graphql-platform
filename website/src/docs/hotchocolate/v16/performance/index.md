---
title: "Overview"
---

In this section we will look at some ways of how we can improve the performance of our Hot Chocolate GraphQL server.

# Startup performance

Instead of the schema being created lazily, you can move its creation to the server startup and also specify warmup tasks.

[Learn more server warmup](/docs/hotchocolate/v16/server/warmup)

# Persisted operations

The size of individual GraphQL requests can become a major pain point. This is not only true for the transport but also the server, since large requests need to be parsed and validated often. To solve this problem, Hot Chocolate implements persisted operations. With persisted operations, we can store operation documents on the server in a key-value store. When we want to execute a persisted operation, we can send the key under which the operation document is stored instead of the operation document itself. This saves precious bandwidth and also improves execution time since the server will validate, parse, and compile persisted operation documents just once.

Hot Chocolate supports two flavors of persisted operations.

## Regular persisted operations

The first approach is to store operation documents ahead of time (ahead of deployment).
This can be done by extracting the operations from our client applications at build time. This will reduce the size of the requests and the bundle size of our application since operations can be removed from the client code at build time and are replaced with operation document hashes.

Strawberry Shake, [Relay](https://relay.dev/docs/guides/persisted-queries/), and [Apollo](https://www.apollographql.com/docs/react/api/link/persisted-queries/) client all support this approach.

[Learn more about persisted operations](/docs/hotchocolate/v16/performance/persisted-operations)

## Automatic persisted operations

Automatic persisted operations allow us to store operation documents dynamically on the server at runtime. With this approach, we can give our applications the same performance benefits as with persisted operations without having to opt in to a more complex build process.

However, we do not get any bundle size improvements for our applications since the operations are still needed at runtime.

Both Strawberry Shake and [Apollo](https://www.apollographql.com/docs/apollo-server/performance/apq/) client support this approach.

[Learn more about automatic persisted operations](/docs/hotchocolate/v16/performance/automatic-persisted-operations)
