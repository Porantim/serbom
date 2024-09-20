# Frontend

![React](https://img.shields.io/badge/react-%2320232a.svg?style=for-the-badge&logo=react&logoColor=%2361DAFB) ![JavaScript](https://img.shields.io/badge/javascript-%23323330.svg?style=for-the-badge&logo=javascript&logoColor=%23F7DF1E)

[**<-- Voltar**](arquitetura.md)

## Introdução

A interface de usuário é construída utiliznado React.js. A tecnologia foi escolhida porque, entre as três tecnologias padrão de mercado (React, Angular e Vue), o React é, de longe, o mais popular e com comunidade mais ativa, além da grande facilidade de criação de componentes reutilizáveis e de desenvolvimento de interfaces complexas. Além disso, o código gerado pelo React costumar ser menor e mais rápido que os concorrentes.

## Single Page Application

A escolha da arquitetura Single Page Application (SPA) para o frontend do sistema apresenta diversas vantagens, especialmente quando se busca uma experiência de usuário mais rica e interativa, similar à de um aplicativo nativo.

### Principais vantagens da arquitetura SPA:

#### 1. Experiência do usuário aprimorada:

- **Interação mais fluida:** As atualizações da página ocorrem de forma dinâmica, sem a necessidade de recarregar toda a página, proporcionando uma experiência mais rápida e responsiva.
- **Interface mais intuitiva:** A SPA permite criar interfaces mais complexas e interativas, com animações e transições suaves.
- **Navegação interna:** A navegação entre as diferentes partes da aplicação ocorre sem a necessidade de realizar novas requisições ao servidor, o que agiliza a experiência do usuário.

#### 2. Desenvolvimento mais ágil:

- **Reutilização de componentes:** Componentes criados podem ser reutilizados em diferentes partes da aplicação, agilizando o desenvolvimento e facilitando a manutenção.
- **Ferramentas e bibliotecas:** Existem diversas ferramentas e bibliotecas disponíveis para o desenvolvimento de SPAs com React. Elas oferecem recursos e funcionalidades prontas para uso.
- **Hot reload:** O React permite atualizar a aplicação em tempo real, sem a necessidade de reiniciar o servidor, o que agiliza o processo de desenvolvimento.

#### 3. Otimização para dispositivos móveis:

- **Responsividade:** As SPAs são altamente responsivas, adaptando-se automaticamente a diferentes tamanhos de tela.
- **Funcionalidades offline:** É possível implementar funcionalidades offline em SPAs, permitindo que o usuário continue utilizando a aplicação mesmo sem conexão com a internet.

#### 3. Desenvolvimento de features mais complexas:

- **Manipulação do DOM:** As SPAs oferecem um maior controle sobre o DOM, permitindo a criação de interfaces mais complexas e interativas.
- **Estado da aplicação:** A gestão do estado da aplicação é mais fácil em SPAs, o que facilita a implementação de funcionalidades como formulários complexos e fluxos de trabalho.

#### Em resumo:

A escolha da arquitetura SPA para o frontend do sistema é uma excelente opção para entregar uma experiência de usuário moderna e interativa, além de facilitar o desenvolvimento e a manutenção da aplicação. No entanto, é importante considerar os desafios e as melhores práticas para garantir o sucesso do projeto.

### JavaScript

A escolha entre JavaScript em detrimento do TypeScript para o desenvolvimento frontend de uma aplicação é uma decisão que envolve diversos fatores e pode variar de projeto para projeto. Embora o TypeScript ofereça diversos benefícios, como tipagem estática e melhor organização do código, existem cenários em que o JavaScript puro pode ser a opção mais adequada:

- *Projetos pequenos e simples:* Para projetos menores, com poucas funcionalidades e uma equipe pequena, o JavaScript pode ser suficiente para garantir a agilidade no desenvolvimento, sem a necessidade de uma ferramenta de tipagem estática.
- *Equipes com pouca experiência em TypeScript:* A curva de aprendizado do TypeScript pode ser um obstáculo para equipes que não estão acostumadas com a tipagem estática.
- *Legado:* Se o projeto já possui uma base de código em JavaScript, a migração para TypeScript pode ser um processo demorado e complexo.

Se fosse o caso de um projeto mais complexo ou que tivesse o objetivo de crescimento e manutenção, o melhor caminho provavelmente seria o TypeScript. Este tem uma série de vantagens:

- **Tipagem estática:** Ajuda a prevenir erros comuns em tempo de desenvolvimento, tornando o código mais seguro e confiável.
- **Melhoria na organização do código:** A tipagem estática força os desenvolvedores a escreverem um código mais organizado e estruturado.
- **Melhor suporte a ferramentas:** Muitas ferramentas de desenvolvimento, como IDEs e linters, oferecem um melhor suporte para TypeScript, com recursos como autocompletar, refatoração e verificação de tipos.
- **Maior escalabilidade:** O TypeScript é mais adequado para projetos grandes e complexos, pois ajuda a manter o código organizado e fácil de manter.

[<-- Voltar](arquitetura.md)
