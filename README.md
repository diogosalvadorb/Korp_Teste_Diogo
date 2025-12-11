# 📦 Sistema Korp — Gestão de Estoque e Notas Fiscais
Sistema completo de gestão empresarial desenvolvido com Angular 19 e .NET 9, utilizando arquitetura de microserviços e Clean Architecture. O sistema permite o gerenciamento de produtos, controle de estoque e emissão de notas fiscais de forma integrada.

# 📋 Índice
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Pré-requisito](#pré-requisitos)
- [Como Instalar e Executar o Projeto](#como-instalar-e-executar-o-projeto)

# Funcionalidades

## 📦 Gerenciamento de Produtos
- Cadastro de produtos com código único  
- Edição de informações (código, descrição, preço, estoque)  
- Exclusão de produtos  
- Validação de código duplicado  
- Controle de estoque em tempo real  
- Visualização de status de estoque (verde/vermelho)  

## 🧾 Gerenciamento de Notas Fiscais
- Criação de notas fiscais com múltiplos itens  
- Numeração sequencial automática  
- Visualização detalhada de notas  
- Impressão de notas (com baixa automática de estoque)  
- Status de notas (Aberta/Fechada)  
- Validação de estoque disponível  
- Comunicação entre microserviços  

# Tecnologias Utilizadas

## Frontend
- Angular 19.2  
- TypeScript 5.7  
- RxJS 7.8  
- Bootstrap 5.3  
- Font Awesome 7.1  

## Backend
- .NET 9.0  
- ASP.NET Core Web API  
- Entity Framework Core 9  
- SQL Server  
- MediatR 11.1  
- FluentValidation 11.3  
- Scalar 2.11 (OpenAPI Documentation)  

# Pré-requisitos

## Backend
- .NET 9 SDK  
- SQL Server 2019+  
- Git  
- Visual Studio 2022 (opcional)  

## Frontend
- Node.js 20+  
- npm 10+  
- Angular CLI 19  

## Ferramentas Opcionais
- Postman / Insomnia  
- SQL Server Management Studio  

# Como Instalar e Executar o Projeto

## 1️⃣ Clonar o Repositório
```bash
git clone https://github.com/seu-usuario/sistema-korp.git
cd Korp_Teste_Diogo/backend
```

## 2️⃣ Backend — InventoryService
```bash
cd InventoryService
dotnet restore
```

## Configurar string de conexão (KorpInventory.Api/appsettings.json)
```bash
{
  "ConnectionStrings": {
    "Connection": "Server=SEU_SERVIDOR;Database=inventory-db;User Id=sa;Password=SUA_SENHA;TrustServerCertificate=True;"
  }
}
```

## Criar banco e aplicar migrations
```bash
cd KorpInventory.Api
dotnet ef database update
dotnet run
```

## 3️⃣ Backend — BillingService
```bash
cd ../BillingService
dotnet restore
```

## Configurar KorpBilling.Api/appsettings.json
```bash
{
  "ConnectionStrings": {
    "Connection": "Server=SEU_SERVIDOR;Database=billing-db;User Id=sa;Password=SUA_SENHA;TrustServerCertificate=True;"
  }
}
```

## Criar banco e aplicar migrations
```bash
cd KorpBilling.Api
dotnet ef database update
dotnet run
```

## 4️⃣ Frontend — Angular

## Entrar no diretório do frontend
```bash
cd korp-web
```

## Instalar as dependências
```bash
npm install
```

## Instalar Angular CLI (caso não tenha instalado globalmente)
```bash
npm install -g @angular/cli@19
```

## Rodar o projeto em modo desenvolvimento
```bash
ng serve
```

## 5️⃣Porta do serviço 

## FrontEnd
```bash
http://localhost:4200
```


##  Serviço de Produtos
```bash
https://localhost:7100/scalar/
http://localhost:5100
```

## Serviço de Faturamento
```bash
https://localhost:7200/scalar/
http://localhost:5200

```
