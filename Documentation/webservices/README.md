# Webservices

This file contains all the explainations of the webservices architectures used in the Rossignol project.

## API Description

To be able to interact with our model and database, we choose to use 2 diferents APIs to feet with our needs.
> The REST API will be used to interract with the datas themself.
> The WebSocket will be used to send notifications to clients.

### REST

A **REST** *(Representational State Transfer)* **API** is a type of `web architecture` and a set of constraints that are usually applied to web services. The goal of a **RESTful API** is to provide a simple and consistent interface for interacting with a service.

**RESTful APIs** use HTTP requests to POST *(create)*, PUT *(update)*, GET *(read)*, and DELETE data. A **RESTful API** is typically made up of a number of endpoints, each representing a specific resource or collection of resources.

**RESTful APIs** are typically easier to use and understand than other types of web services because they rely on the standard HTTP methods *(GET, POST, PUT, DELETE, etc.)*. Additionally, because RESTful APIs are based on standard HTTP protocols, they can be used with a wide variety of programming languages and technologies, including Java, C#, Python and many more.

**RESTful API** is preferred for many reasons, some of them are:

- They are simple, the **API** is easy to understand and can be consumed easily
- They are lightweight, **RESTful API's** are lightweight, fast and easy to implement
- They are stateless, **RESTful API's** are stateless, which means that the server does not store any information about the client session on the server side
- They are cacheable, **RESTful API's** can be cached which can lead to better performance
- They are scalable, **RESTful API's** are scalable, which means that the system can handle a large number of requests and can be easily scaled horizontally.

Overall, **RESTful API** is a popular choice for building web services because it is easy to use and understand, and it is based on standard web technologies and protocols.

### WebSocket

A **WebSocket API** is a type of **API** that enables real-time, bidirectional communication between a client and a server. Unlike **RESTful APIs**, which rely on HTTP requests and responses, WebSocket APIs use a single, long-lived connection to enable real-time communication.

**WebSocket** communication starts with an HTTP handshake, after which the connection is upgraded to a **WebSocket** connection. Once the connection is established, the client and server can send messages to each other in real-time without the need for an HTTP request/response cycle.

A **WebSocket API** allows clients to subscribe to specific events, so the server can push new data to the client without the client having to actively request it. This makes **WebSocket APIs** well-suited for use cases where low-latency, real-time communication is needed, such as real-time notifications, chat applications, and online gaming.

**WebSocket API** are preferred for many reasons, some of them are:

- They are real-time, **WebSocket API's** enables real-time, bidirectional communication between client and server
- They are efficient, **WebSocket API's** are efficient, they use a single, long-lived connection which reduces overhead and improves performance
- They are flexible, **WebSocket API's** are flexible, they can be used for a variety of use cases, including real-time notifications, chat applications, and online gaming
- They are low-latency, **WebSocket API's** are low-latency, which means that the server can push new data to the client without the client having to actively request it.
- They are event-driven, **WebSocket API's** allows clients to subscribe to specific events, so the server can push new data to the client without the client having to actively request it.

Overall, **WebSocket API** is a popular choice for building real-time applications because it enables low-latency, bidirectional communication and it is more efficient than **RESTful API's**.

---
## Gateway

An **API Gateway** is a server that acts as an intermediary between an application and a set of microservices. The **API Gateway** is responsible for request routing, composition, and protocol translation, among other things.

**API Gateways** are typically used to provide a single entry point for external consumers of a set of microservices. By routing requests through a single **API Gateway**, developers can simplify their application architecture and reduce the number of connections that need to be made to the underlying microservices.

An **API Gateway** can also provide additional features such as authentication, authorization, rate limiting, caching, and request/response transformation. These features can be implemented in the **API Gateway**, rather than in each microservice, which can simplify development and make the system more secure and performant.

**API Gateway** is preferred for many reasons, some of them are:

- They provide a single entry point, **API Gateway** provides a single entry point for external consumers of a set of microservices, which makes the architecture simpler and reduces the number of connections
- They provide security and rate limiting, **API Gateway** provides security features such as authentication, authorization and rate limiting which can make the system more secure
- They provide caching exceptions, **API Gatewa**y provides caching exceptions which can improve the performance of the system and the maintability
- They provide request/response transformation, **API Gateway** can transform the request and response which can make the system more flexible
- They provide protocol translation, **API Gateway** can translate between different protocols which can make the system more flexible

Overall, **API Gateway** is a popular choice for building microservices based architecture because it provide a single entry point for external consumers, security features, caching, request/response transformation, protocol translation and other features that can make the system more secure, performant and flexible.

---
## Architecture

The architecture of the **Rossignol App** is really simple.

It's composed as follow:

- The clients (mobile principaly) ask a **Gateway**.
- The **Gateway** use authentication to ensure the client is legit. (It also give the option to get credentials.)
- The **Gateway** root the request the the **API** that handle it. (In our case we have 2 APIs.)
    - The **Rest API** has the ability to `GET, POST, PUT & DELETE` data with our working model.
    - The **Websocket** has the ability to sends `notifications` to the clients.

*Note that all the APIs + the gateway talks with the same databse at this was simplier for us.*

**Schemas:**

![](./src/Architecture.drawio.png)