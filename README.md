# WebSalesModelViewController

Este repositório contém a implementação de um sistema de vendas baseado no padrão **Model-View-Controller (MVC)** utilizando **ASP.NET Core**. O sistema gerencia vendas, produtos e clientes, e inclui funcionalidades para visualização e manipulação de dados.

## Estrutura do Repositório

```
WebSalesMVC/
├── Controllers/: Contém os controladores responsáveis pelas requisições e lógica de negócios.
├── Data/: Contém as configurações e o contexto do banco de dados.
├── Migrations/: Contém as migrações para o banco de dados.
├── Models/: Contém os modelos de dados utilizados na aplicação.
├── Properties/: Contém as propriedades do projeto.
├── Services/: Contém a lógica de serviços auxiliares utilizados pela aplicação.
├── Views/: Contém as views que são apresentadas ao usuário.
├── wwwroot/: Diretório onde ficam os arquivos estáticos (CSS, JS, imagens).
├── Program.cs: Arquivo principal de configuração e execução da aplicação.
├── Validator.cs: Contém a validação de dados antes de serem salvos ou manipulados.
├── WebSalesMVC.csproj: Arquivo de projeto da aplicação.
├── appsettings.Development.json: Arquivo de configurações para o ambiente de desenvolvimento.
└── appsettings.json: Arquivo de configurações gerais da aplicação.
```

## Como Usar

1. Clone este repositório para sua máquina local:
   ```bash
   git clone https://github.com/seu-usuario/WebSalesModelViewController.git
   ```

2. Abra o repositório no seu ambiente de desenvolvimento (Visual Studio ou VS Code).

3. Restaure as dependências e execute a aplicação:
   ```bash
   dotnet restore
   dotnet run
   ```

4. Lembre de fazer o update no banco de dados:
  ```bash
  dotnet ef database update
```

6. Realize operações de vendas, consulte produtos e visualize os relatórios de clientes.

## Funcionalidades

- **Cadastro de Produtos e Clientes**: Permite adicionar novos produtos e registrar clientes.
- **Gestão de Vendas**: Realiza o controle de vendas e histórico de compras.
- **Autenticação e Autorizações**: Implementação de controle de acesso para garantir que apenas usuários autorizados possam realizar certas ações.
- **Validação de Dados**: Valida as informações antes de serem salvas no banco de dados.

## Tecnologias Utilizadas

- **ASP.NET Core MVC**: Framework utilizado para a criação da aplicação web.
- **Entity Framework Core**: ORM utilizado para interagir com o banco de dados.
- **SQL Server**: Banco de dados utilizado para armazenar informações de vendas e clientes.

## Tópicos Abordados

- Desenvolvimento de aplicações web com **ASP.NET Core MVC**.
- Utilização do padrão **Model-View-Controller (MVC)**.
- Implementação de autenticação e controle de acesso.
- Manipulação de dados com **Entity Framework Core**.
- Construção de sistemas de cadastro, vendas e relatórios.
