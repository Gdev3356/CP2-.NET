# Contra-Filé - Uma API para serviço de restaurante! (Parte 2)

- Criada por Gustavo Keiji Okada, RM563428

- Baseado em sistemas de restaurantes

---

## Domínio

O sistema **Contra-Filé** é uma API para gerenciamento de um restaurante. O domínio modela:

- **Usuários** — clientes cadastrados no sistema, com autenticação por senha com hash
- **Mesas** — mesas do restaurante, com controle de ocupação
- **Pedidos** — pedidos realizados por usuários em mesas, contendo múltiplos pratos
- **Pratos** — itens do cardápio, com nome, preço, descrição e tempo de preparo
- **Avaliações** — avaliações feitas por usuários (nota de 1 a 5 com descrição)
- **Configurações de Usuário** — preferências individuais de cada usuário (tema, notificações)

O relacionamento entre **Pedido** e **Prato** é **N:N**, materializado pela tabela `PedidoPratos`.

---

## Banco de Dados

**SGBD utilizado:** MySQL

A conexão é configurada via **User Secrets** em desenvolvimento, garantindo que nenhuma credencial seja versionada no repositório.

---

## Arquitetura

O projeto segue o padrão **Clean Architecture**, dividido em 4 camadas:

```
Contra-Filé.Domain          → Entidades e regras de domínio
Contra-Filé.Application     → Serviços, interfaces e DTOs
Contra-Filé.Infrastructure  → DbContext, configurações EF e Migrations
Contra-Filé.API             → Controllers, Program.cs e configuração da API
```

---

## Como executar

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) ou superior
- MySQL rodando localmente

### 1. Clonar o repositório

```bash
git clone <url-do-repositorio>
cd "Contra-Filé CP2"
```

### 2. Configurar a connection string via User Secrets

```bash
dotnet user-secrets init --project "Contra-Filé.API"
dotnet user-secrets set "ConnectionStrings:ContraFiléMySQL" "Server=localhost;Database=contrafile;User=root;Password=SUA_SENHA;" --project "Contra-Filé.API"
```

### 3. Aplicar as migrations

```bash
dotnet ef database update \
  --project "Contra-Filé.Infrastructure" \
  --startup-project "Contra-Filé.API"
```

### 4. Executar a API

```bash
dotnet run --project "Contra-Filé.API"
```

---

## Migrations

Para gerar uma nova migration:

```bash
dotnet ef migrations add NomeDaMigration \
  --project "Contra-Filé.Infrastructure" \
  --startup-project "Contra-Filé.API" \
  --context Contra_Filé.Infrastructure.Persistence.ContraFiléContext \
  --output-dir Migrations
```

Para aplicar ao banco:

```bash
dotnet ef database update \
  --project "Contra-Filé.Infrastructure" \
  --startup-project "Contra-Filé.API"
```

---

## Endpoints disponíveis

### Pedidos — `/api/pedido`

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/pedido` | Lista todos os pedidos |
| GET | `/api/pedido/{id}` | Busca um pedido por ID |
| POST | `/api/pedido` | Cria um novo pedido |
| DELETE | `/api/pedido/{id}` | Remove um pedido |

### Exemplo de body para POST `/api/pedido`

```json
{
  "mesaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
  "pratoIds": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa8"
  ]
}
```

---

## Pacotes utilizados

| Pacote | Projeto | Finalidade |
|---|---|---|
| `Microsoft.EntityFrameworkCore` | Infrastructure | ORM |
| `Pomelo.EntityFrameworkCore.MySql` | Infrastructure | Provider MySQL |
| `Microsoft.EntityFrameworkCore.Design` | API | Geração de migrations |
| `BCrypt.Net-Next` | Infrastructure | Hash de senhas |
| `Microsoft.AspNetCore.OpenApi` | API | Documentação OpenAPI |
