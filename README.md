# Didact Engine

The decoupled, atomic REST API and orchestration engine for the Didact .NET Standard scheduler.

## Didact Platform Repositories

* The REST API and orchestration engine: [didact-engine](https://github.com/DidactHQ/didact-engine)
* The console application responsible for heartbeats, engine revives, and maintenance tasks: didact-sentinel
* The VueJS single-page application dashboard: [didact-ui](https://github.com/DidactHQ/didact-ui)
* The core library containing the block classes, as well as other model information. [didact-core](https://github.com/DidactHQ/didact-core)

## Summary

The Didact Engine is the combined REST API and orchestration/execution engine for Didact workflows.

This .NET 6 application is responsible for:
* Exposing workflow manipulations via a set of REST endpoints.
* Persisting the metadata of Didact blocks and Didact workflows into database storage.
* Dynamically loading Didact class library projects and executing Didact workflows.
* Sending real-time updates to [didact-ui](https://github.com/DidactHQ/didact-ui) via SignalR hubs.
