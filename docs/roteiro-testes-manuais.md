# Roteiro de Teste Manual - Product API

## Objetivo

Validar manualmente a API em Bruno ou no VS Code usando os mesmos payloads e chamadas.

## Base URL

```text
http://localhost:8080
```

## Ordem recomendada

1. Criar um produto.
2. Listar os produtos.
3. Buscar o produto por id.
4. Atualizar o produto.
5. Pesquisar por nome.
6. Testar paginação.
7. Excluir o produto.
8. Confirmar que o produto foi removido.

## 1. Criar produto

### Bruno

```http
POST /api/products
Content-Type: application/json

{
  "name": "Mouse Gamer",
  "description": "Mouse sem fio com DPI ajustavel",
  "price": 129.9,
  "stock": 15
}
```

### VS Code

Veja o arquivo [docs/product-api.http](docs/product-api.http).

### Resposta esperada

- Status: `201 Created`
- Header `Location` apontando para o recurso criado

## 2. Listar produtos

### Bruno

```http
GET /api/products
```

### Resposta esperada

- Status: `200 OK`
- Lista com os produtos cadastrados

## 3. Buscar por id

Substitua `{id}` pelo identificador retornado no `POST`.

### Bruno

```http
GET /api/products/{id}
```

### Resposta esperada

- Status: `200 OK`
- Corpo com o produto correspondente

## 4. Atualizar produto

### Bruno

```http
PUT /api/products/{id}
Content-Type: application/json

{
  "name": "Mouse Gamer Pro",
  "description": "Versao atualizada com maior precisao",
  "price": 149.9,
  "stock": 10
}
```

### Resposta esperada

- Status: `204 No Content`

## 5. Pesquisar por nome

### Bruno

```http
GET /api/products/search?name=mouse
```

### Resposta esperada

- Status: `200 OK`
- Lista com os produtos que contêm o termo informado no nome

## 6. Paginação

### Bruno

```http
GET /api/products/paged?page=1&pageSize=10
```

### Resposta esperada

- Status: `200 OK`
- Corpo com `items`, `page`, `pageSize` e `totalItems`

## 7. Excluir produto

### Bruno

```http
DELETE /api/products/{id}
```

### Resposta esperada

- Status: `204 No Content`

## 8. Validar remoção

### Bruno

```http
GET /api/products/{id}
```

### Resposta esperada

- Status: `404 Not Found`

## Cenário de erro

Teste também um payload inválido para validar `ProblemDetails`.

```http
POST /api/products
Content-Type: application/json

{
  "name": "",
  "description": null,
  "price": 100,
  "stock": 5
}
```

Resposta esperada:

- Status: `400 Bad Request`
- Corpo com `ProblemDetails`

## Observações

- No Bruno, crie uma variável de ambiente como `baseUrl = http://localhost:8080`.
- No VS Code, abra o arquivo `.http` e clique em `Send Request` em cada bloco.
- Se preferir, use o mesmo roteiro para Postman ou curl.