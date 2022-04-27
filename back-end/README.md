# Teus Controle

- Escopo do Projeto

    Em um contexto de pequenas lojas, sistemas de informação nem sempre se fazem presentes. Neste sentido, o Teus Controle existe como alternativa para estes estabelecimentos, trazendo de uma maneira simplificada o cadastro de produtos, e gestão das movimentações de entrada e saída dos mesmos. Desta forma, o sistema consiste na implementação do crud para o registro de produtos disponíveis, e um crud para registros de movimentações, que podem ser do tipo entrada e de saída. Além de um crud de usuários do qual apenas o ADMIN do sistema poderá ter acesso.

- Referências

    Para a construção deste sistema, se faz necessário consultas na API disponibilizada pela Bluesoft Cosmos, que oferece um serviço de consulta a seu Catálogo Online de Produtos. Possibilita até 25 consultas por dia gratuitamente. Link para API: https://cosmos.bluesoft.com.br/api. Para a Interface de usuário, sua construção se dá através dos padrões definidos pelo Material Design, disponível em https://material.io/ . (A ser alterado caso não seja utilizado, provavelmente será uma das últimas partes do documento a ser concluída)

- Perspectiva do Produto

    O sistema será composto por uma interface construída em Dart com o framework Flutter que consome requisições de uma API REST desenvolvida em C# - .NET FRAMEWORK com Entity Framework para persistência de dados no banco MySql. (incrementado e melhor definido ao longo do desenvolvimento).

- Funcionalidades do Sistema

- [ ] RF-001 - [ALTA] - O sistema deve permitir listagem, cadastro, alteração e exclusão de usuários. Um usuário deve possuir: nome completo, cpf ou cnpj, e-mail, nome de usuário, senha, imagem do perfil, tipo do perfil, status, data de criação e data de aniversário, se for o caso. O campo perfil deverá ser utilizado para controlar o acesso às áreas específicas do sistema, podendo assumir os valores ADMIN e USER. Somente o usuário administrador poderá realizar essa operação.

- [ ] RF-002 - [ALTA] - O sistema deve permitir que somente usuários autenticados realizem operações.

- [ ] RF-003 - [ALTA] - O sistema deve permitir que o usuário autenticado ADMIN realize o cadastro, alteração e exclusão de produtos. Um produto deve possuir: descrição, preço, código de barras, quantidade disponível em estoque, quantidade em estoque mínimo, marca e status. O sistema deve permitir que o usuário autenticado tenha acesso a listagem de produtos e suas informações cadastrais.

- [ ] RF-004 - [ALTA] - O sistema deve permitir que o usuário autenticado ADMIN realize a movimentação de entrada de produtos no estoque. O sistema deve permitir que qualquer usuário autenticado realize a movimentação de saída de produtos do estoque como venda ou inutilização (descarte).

- [ ] RF-005 - [MÉDIA] - O sistema deve automaticamente completar as informações consultadas da API de catálogo de produtos nos campos, ao ler um código de barras ao dar início no cadastro de um novo produto.

- [ ] RF-006 - [MÉDIA] - O sistema deve possibilitar que o usuário autenticado ADMIN emita relatório de movimentações, aplicando filtro por período.

- [ ] RF-007 - [BAIXA] - O sistema deve possibilitar que o usuário autenticado ADMIN  emita relatório de produtos com estoque abaixo da quantidade mínima em estoque informada.

- [ ] RF-008 - [MÉDIA] - O sistema deve possibilitar que o usuário autenticado emita relatório de todos os produtos cadastrados e sua respectiva quantidade em estoque.

- Interfaces do Usuário

    RIE-101 - [ALTA] - A interface gráfica deverá ser construída com base no Material Design.

- Interfaces de Hardware

    RIE-201 - [ALTA] - O sistema deverá ser compatível com os cinco principais navegadores da atualidade, sendo eles Google Chrome, Edge, Mozilla Firefox, Opera e Safari.

- Interfaces de Software

    RIE-301 - [MÉDIA] - O sistema deverá possibilitar integração com a API disponibilizada pela Bluesoft Cosmos, como já mencionado na Seção 1.5 deste documento.

- Interfaces de Comunicação

    RIE-401 - [ALTA] - O sistema deverá se comunicar por meio de uma API REST, utilizando o protocolo HTTP.

    RIE-402 - [ALTA] - Os dados enviados entre os módulos do sistema deverão ser no formato JSON.

- Requisitos de Desempenho

    RNF-101 - [ALTA] - O sistema deve permitir que as operações de cadastro, alteração e exclusão sejam realizadas em no máximo 5s.

    RNF-102 - [MÉDIA] - O sistema deve emitir o relatório em no máximo 10s.

- Requisitos de Segurança

    RNF-301 - [MÉDIA] - O Sistema deve estar em conformidade com a Lei Geral de Proteção de Dados Pessoais, Lei nº 13.709/2018 brasileira.

- Atributos de Qualidade de Software

    RNF-401 - [ALTA] - Nome de métodos, variáveis, nome de classes e de objetos, devem ser escritos na língua inglesa.

    RNF-402 - [ALTA] - Os imports devem estar ordenados em ordem alfabética.

    RNF-403 - [ALTA] - Partes do nome de uma variável podem ser separadas por underscore (com todas as letras maiúsculas ou minúsculas) ou todas juntas com a primeira letra de cada parte em maiúscula – underscore no início costuma indicar que o atributo é de uso interno, dois indicam atributo privado da classe e dois no início e no fim são de atributos ou objetos especiais

    RNF-404 - [ALTA] - Nomes de classe devem usar o padrão de PalavrasComecandoPorMaiuscula, tal qual o nome de seu arquivo

    RNF-405 - [ALTA] - Só deve existir apenas uma classe por arquivo, em exceção de inner classes (DTOs a serem utilizados na classe que dá nome ao arquivo).
