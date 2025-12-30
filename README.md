# API - Gerenciamento de Produtos

API REST para gerenciar produtos.

## O que é

Sistema para cadastrar, listar, atualizar e deletar produtos de um catálogo.

## Tecnologias

- .NET 9
- Entity Framework Core
- SQL Server LocalDB
- Swagger

## Estrutura

MinhaApiCrud.API - Controllers e configuração
MinhaApiCrud.Application - Lógica de negócio e DTOs
MinhaApiCrud.Domain - Entidades e interfaces
MinhaApiCrud.Infrastructure - Acesso ao banco de dados

## Endpoints

GET /api/products - Lista produtos
GET /api/products/{id} - Busca produto por ID
POST /api/products - Cria produto
PUT /api/products/{id} - Atualiza produto
DELETE /api/products/{id} - Deleta produto

## Filtros

name - busca por nome
category - filtra por categoria
minPrice - preço mínimo
maxPrice - preço máximo
inStock - apenas produtos em estoque
pageNumber - número da página
pageSize - itens por página
