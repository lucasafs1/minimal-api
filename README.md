# 🚗 Minimal API - Gestão de Veículos e Administradores

Projeto desenvolvido como desafio prático no Bootcamp da Digital Innovation One (DIO). O objetivo da aplicação é disponibilizar uma API Minimalista em .NET 8 robusta, testada e pronta para produção, aplicando conceitos de autenticação via JWT, banco de dados MySQL, testes automatizados e deploy em nuvem.

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem & Framework:** C# / .NET 8 (Minimal API)
- **Banco de Dados:** MySQL com Entity Framework Core (ORM)
- **Autenticação & Autorização:** JWT (JSON Web Tokens) com controle de perfis (`Adm` / `Editor`)
- **Documentação:** Swagger / OpenAPI
- **Testes Automatizados:** MSTest com `TestServer` / `HttpClient` (Testes de Domínio e Request)
- **Infraestrutura / Deploy:** Linux (Ubuntu) na AWS EC2, Nginx como Proxy Reverso e MySQL Server

---

## 🚀 Funcionalidades da API
<img width="1272" height="885" alt="image" src="https://github.com/user-attachments/assets/bd6fed42-9df6-4ff2-9e4f-d41401500093" />


### 🔐 Autenticação (`/administradores`)
- **POST `/administradores/login`**: Realiza o login e retorna o Token JWT.
- **POST `/administradores`**: Cadastra novos administradores (requer perfil `Adm`).
- **GET `/administradores`**: Lista administradores cadastrados com paginação (requer perfil `Adm`).
- **GET `/administradores/{id}`**: Busca um administrador por ID (requer perfil `Adm`).

### 🚘 Gestão de Veículos (`/veiculos`)
- **POST `/veiculos`**: Cadastra um novo veículo (requer perfil `Adm` ou `Editor`).
- **GET `/veiculos`**: Lista veículos cadastrados com suporte a paginação (requer autenticação).
- **GET `/veiculos/{id}`**: Busca veículo por ID (requer autenticação).
- **PUT `/veiculos/{id}`**: Atualiza dados de um veículo (requer perfil `Adm` ou `Editor`).
- **DELETE `/veiculos/{id}`**: Remove um veículo cadastrado (requer perfil `Adm`).

---

## 🧪 Testes Automatizados

O projeto conta com uma suíte completa de testes unitários e de integração desenvolvidos no projeto de testes da solução.

Os testes cobrem:
- Validação das entidades de domínio (`AdministradorTest`, `VeiculoTest`).
- Requisições HTTP simuladas com `TestServer` validando respostas com token de autorização JWT (`AdministradorRequestTest`, `VeiculoRequestTest`).

Para rodar os testes localmente:
```bash
dotnet test
```

## 🌐 Deploy e Infraestrutura (AWS)
A aplicação foi hospedada e configurada em ambiente de nuvem simulando um cenário real de produção:

- Instância AWS EC2 (Ubuntu Linux): Provisionamento do servidor.

- Nginx: Configurado como servidor web e proxy reverso escutando a porta 80 e redirecionando as requisições para o Kestrel (http://localhost:5004).

- MySQL Server: Banco de dados configurado no ambiente Linux.

- Entity Framework Migrations: Executadas para criação automática do schema do banco.

## ⚙️ Como executar o projeto localmente
### Pré-requisitos
- .NET 8 SDK

- MySQL Server

### Passo a passo
- Clone o repositório:
```
Bash
git clone https://github.com/lucasafs1/minimal-api.git
cd minimal-api
```
- Configure o banco de dados:
Ajuste a string de conexão no arquivo Api/appsettings.json para apontar para o seu servidor MySQL local.

- Execute as migrations:
```
Bash
dotnet ef database update --project Api
```
- Inicie a aplicação:
```
Bash
cd Api
dotnet run
```
Acesse o Swagger:
Abra no navegador: http://localhost:5000/swagger ou http://localhost:5004/swagger.

Desenvolvido por Lucas no bootcamp da DIO.
