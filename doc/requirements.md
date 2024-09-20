# Teste Técnico: Desenvolvimento e Arquitetura de Sistema para Gerenciamento de Contratos

## Objetivo do Teste:
Desenvolver um protótipo de sistema de gerenciamento de contratos que demonstre a habilidade do candidato em projetar uma arquitetura eficiente e implementar funcionalidades práticas, incluindo a integração com uma API de CEP para preenchimento automático de endereços durante o cadastro de clientes.

## Descrição do Mini-Mundo:
O sistema deve gerenciar contratos e clientes associados a esses contratos. Durante o cadastro de um cliente, o sistema deverá consumir a API de CEP para completar automaticamente os campos de endereço.
Considere que apesar de hoje o sistema não ser multiplataforma, em algum momento ele pode vir a ser. 

## Requisitos Funcionais:
1. CRUD de Contratos: Criação, consulta, atualização e remoção de contratos.
2. Cadastro de Clientes: Durante o cadastro, consumir a API de CEP para preencher campos de endereço.

## Requisitos Não Funcionais:
1. Linguagem Backend: C# e ASP.NET Core.
2. Linguagem Frontend: Livre escolha.
3. Código e Repositório: Uso de Git para versionamento de código.
4. Banco de dados Mysql.

## Tarefa de Desenvolvimento:
### 1. Desenho da Arquitetura:
   - Elaborar um diagrama arquitetural que mostre a interação entre os componentes de backend, frontend e a API externa.
   - Justificar a escolha da arquitetura com base em sua adequação para sistemas que necessitam de integração externa e operações CRUD eficientes.

### 2. Implementação:
   - Desenvolver o CRUD de contratos usando C# e ASP.NET Core.
   - Criar a interface do usuário para interagir com o sistema.
   - Implementar a integração com a API de CEP durante o cadastro de clientes.
   - Configurar um Dockerfile para facilitar a configuração e execução do ambiente de desenvolvimento.

## Entrega:
- O código-fonte deve ser hospedado em um repositório no GitHub.
- Fornecer um documento PDF com o diagrama arquitetural e a justificativa das escolhas de arquitetura.
- Incluir instruções detalhadas para configurar e executar o projeto, abrangendo a configuração via Docker.

## Avaliação:
A avaliação se concentrará na clareza e eficácia da arquitetura proposta, organização e funcionalidade do código, e na habilidade de integrar com a API de CEP. A justificativa da arquitetura é crucial para entender as escolhas tecnológicas e estratégicas do candidato. 

Este teste é projetado para ser completado em cerca de 8 horas por um desenvolvedor sênior. Ele oferece uma oportunidade para o candidato demonstrar competências técnicas e práticas relevantes para as necessidades da vaga.

Considere aplicar melhorias não explícitas, mas que no seu ponto de vista agregariam para o projeto.
