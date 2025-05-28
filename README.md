# InventoryMaster

📦 **InventoryMaster** é uma API REST desenvolvida em .NET 8 com MySQL para gestão integrada de estoque, vendas (incluindo carrinho de compras e máquina de estados), contas a pagar/receber, funcionários e relatórios financeiros. O projeto inclui autenticação JWT, documentação Swagger e segue a licença MIT.

---

## ✨ Funcionalidades Principais

- **Gestão de Materiais:** Cadastro, consulta, atualização e exclusão de produtos/materiais.
- **Inventário de Estoque:** Controle de entradas, saídas e saldo de estoque.
- **Processo de Vendas:** Registro de vendas, integração com carrinho de compras e máquina de estados para o fluxo de venda.
- **Carrinho de Compras:** Adição, remoção e finalização de itens no carrinho.
- **Máquina de Estados de Vendas:** Representação dos estados do processo de venda (ex: Em andamento, Pago, Cancelado).
- **Gestão de Contas a Pagar:** Cadastro e controle de despesas e pagamentos.
- **Gestão de Contas a Receber:** Cadastro e controle de receitas e recebimentos.
- **Gestão de Funcionários:** Cadastro e gerenciamento de funcionários.
- **Relatórios de Contabilidade:** Relatórios financeiros, de vendas e de estoque.
- **Autenticação JWT:** Segurança para rotas protegidas.
- **Documentação Swagger:** Interface interativa para testes e documentação da API.

---

## 📋 Requisitos Funcionais

- Permitir CRUD de produtos, funcionários, contas a pagar/receber.
- Permitir movimentação de estoque (entrada/saída).
- Permitir registro e acompanhamento de vendas com carrinho de compras.
- Implementar máquina de estados para o processo de vendas.
- Gerar relatórios financeiros e de estoque.
- Proteger rotas sensíveis com autenticação JWT.
- Disponibilizar documentação interativa via Swagger.

## 🚫 Requisitos Não Funcionais

- API RESTful, seguindo boas práticas de arquitetura.
- Persistência de dados em banco MySQL.
- Código-fonte versionado no GitHub.
- Licença MIT.
- Documentação clara e objetiva.
- Testes básicos de integração.

---

## 🔒 Autenticação

A API utiliza JWT para autenticação. Para acessar rotas protegidas, obtenha um token via endpoint de login e envie no header `Authorization: Bearer {token}`.

---

## 🛠️ Como rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/LeonardoRamboUP/InventoryMaster.git
   ```
