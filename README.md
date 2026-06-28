# Product API

## Objetivo

Este projeto é uma API REST desenvolvida para fins de estudo da plataforma .NET.

O objetivo é aprender desde os fundamentos da linguagem C# até boas práticas utilizadas em aplicações corporativas.

## Tecnologias

* .NET 10
* ASP.NET Core
* SQL Server
* Entity Framework Core
* Docker
* Docker Compose
* Swagger
* xUnit

## Requisitos Funcionais

* Cadastrar produtos.
* Listar produtos.
* Buscar produto por Id.
* Atualizar produto.
* Excluir produto.
* Pesquisar por nome.
* Paginação.
* Ordenação.

## Requisitos Não Funcionais

* Clean Code
* SOLID
* KISS
* YAGNI
* API REST
* Async/Await
* Dependency Injection
* Logging
* Tratamento global de exceções
* Swagger
* Validação de entrada

## Estrutura do Projeto

src/

* ProductApi.Api
* ProductApi.Application
* ProductApi.Domain
* ProductApi.Infrastructure

tests/

* ProductApi.Tests

## Arquitetura

O projeto segue uma arquitetura em camadas:

* ProductApi.Api: controllers, middleware, DI e configuracao da aplicacao.
* ProductApi.Application: casos de uso, DTOs e respostas da API.
* ProductApi.Domain: entidade principal, regras de negocio e contratos.
* ProductApi.Infrastructure: EF Core, DbContext, repositorios e persistencia SQL Server.

## Endpoints principais

* `POST /api/products`
* `GET /api/products`
* `GET /api/products/{id}`
* `PUT /api/products/{id}`
* `DELETE /api/products/{id}`
* `GET /api/products/search?name=...`
* `GET /api/products/paged?page=1&pageSize=10`

## Comportamento da API

* Retorna `ProblemDetails` para erros tratados globalmente.
* Publica OpenAPI/Swagger no ambiente de desenvolvimento.
* Usa SQL Server via Entity Framework Core.
* Suporta execucao local via Docker Compose.

## Estrutura da Entidade Product

* Id
* Name
* Description
* Price
* Stock
* CreatedAt
* UpdatedAt

## Objetivos de Aprendizado

Durante este projeto serão estudados:

* C#
* ASP.NET Core
* Entity Framework Core
* SQL Server
* Docker
* Docker Compose
* Git
* GitHub

## Próximos Projetos

Após a conclusão deste projeto, a evolução seguirá com:

1. JWT
2. Redis
3. RabbitMQ
4. MongoDB
5. Background Services
6. Observabilidade
7. Testes de Integração
8. Kubernetes

## Execução local

### Via Docker

```bash
docker compose up --build
```

Para parar os containers:

```bash
docker compose down
```

### Via .NET CLI

```bash
dotnet run --project src/ProductApi.Api/ProductApi.Api.csproj
```

## Configuração

- A connection string padrão fica em `src/ProductApi.Api/appsettings.json`.
- No Docker Compose, a API usa o host `sqlserver` e a porta `1433`.
- A API expõe HTTP na porta `8080` dentro do container.

## Observacoes

- A migration inicial de `Products` já foi criada.
- O tratamento global de exceções está centralizado em middleware.
- Os testes unitários e de integração fazem parte da solução e passam localmente.
