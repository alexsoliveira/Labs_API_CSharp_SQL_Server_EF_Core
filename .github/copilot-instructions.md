# Product API - Copilot Instructions

## Objetivo do Projeto
Este projeto tem como objetivo o aprendizado da linguagem C# e do ecossistema .NET através da construção de uma API REST para gerenciamento de produtos.

A implementação deve priorizar clareza, simplicidade e boas práticas de engenharia de software em vez de otimizações prematuras.

---

# Stack

- .NET 8
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- Docker
- Docker Compose
- xUnit para testes
- Git

---

# Princípios
Sempre siga os princípios abaixo:

- Clean Code
- SOLID
- KISS
- YAGNI
- DRY (sem exageros)
- Separation of Concerns
- Composition over Inheritance
- Dependency Injection
- Código legível antes de código "inteligente"

Sempre prefira soluções simples.

Nunca introduza complexidade desnecessária.

---

# Arquitetura
O projeto utiliza uma arquitetura em camadas composta por:

- ProductApi.Api
- ProductApi.Application
- ProductApi.Domain
- ProductApi.Infrastructure

Responsabilidades:

## Domain
Contém apenas:

- Entidades
- Value Objects (quando necessário)
- Interfaces
- Regras de negócio

Nunca utilizar Entity Framework Core nesta camada.

---

## Application
Contém:

- Casos de uso
- DTOs
- Validações
- Interfaces de serviços

Não deve conhecer detalhes da infraestrutura.

---

## Infrastructure
Contém:

- Entity Framework Core
- DbContext
- Configurações
- Repositórios
- Migrations
- Persistência

---

## Api
Contém apenas:

- Controllers
- Middlewares
- Configuração de DI
- Swagger
- Configuração da aplicação

Não colocar regras de negócio nos Controllers.

---

# Convenções

- Utilize Nullable Reference Types.
- Utilize async/await para operações de I/O.
- Evite código duplicado.
- Utilize nomes claros e descritivos.
- Evite comentários desnecessários; prefira código autoexplicativo.
- Sempre utilize injeção de dependência.
- Utilize CancellationToken em operações assíncronas quando apropriado.
- Prefira records para DTOs.
- Utilize classes para entidades.

---

# Entity Framework Core
Sempre:

- Utilizar Fluent API para configurações.
- Utilizar Migrations.
- Utilizar AsNoTracking em consultas somente leitura.
- Evitar consultas N+1.
- Utilizar LINQ de forma legível.

---

# API REST
Seguir convenções REST:

- GET
- POST
- PUT
- DELETE

Retornar status HTTP apropriados.

Sempre validar entradas.

Utilizar ProblemDetails para tratamento de erros quando possível.

---

# Estrutura de Código
Cada classe deve possuir apenas uma responsabilidade.

Métodos devem ser pequenos.

Evite métodos com muitas condições.

Extraia lógica complexa para métodos privados ou serviços.

---

# Geração de Código
Ao gerar código:

- explique brevemente as decisões arquiteturais quando solicitado;
- siga os padrões existentes no projeto;
- nunca introduza bibliotecas sem necessidade;
- utilize apenas recursos compatíveis com .NET 8;
- priorize clareza e manutenção.

---

# Objetivo Educacional
Este projeto possui caráter didático.

Sempre que houver mais de uma solução possível, prefira a mais simples e amplamente utilizada na comunidade .NET, explicando os trade-offs quando solicitado.
