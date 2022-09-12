# DesafioWoopi.GestaoSeguranca
* Este projeto tem como finalidade o teste que foi designado pela Woopi, sendo:

## Autor
* Vinícius Medeiros - vini.dmedeiros@gmail.com

### Cenário:

Jorisvaldo tentou logar no sistema Sian até expirar suas três tentativas, e percebeu que havia esquecido sua senha pois seu final de semana foi muito produtivo. Logo clicou no botão de "Resetar Senha", foi solicitado o e-mail do usuário onde após informá-lo (1), clicou em "Prosseguir" e foi exibido uma popup listando (2) todas as perguntas cadastradas para o usuário. Após efetuar o preenchimento das respostas, clicou em "Verificar" submetendo (3) todas as respostas do usuário para análise, caso alguma resposta esteja incorreta será exibido as perguntas incorretas de vermelho. Se todas as respostas respondidas com sucesso, o usuário será redirecionado para tela de mudança de senha. É informado a senha onde Jorisvaldo clica em "Mudar Senha". O sistema faz uma verificação (4) se a nova senha já foi utilizada nas 5 últimas mudanças, e caso tenha sido utilizada será exibido uma mensagem solicitando que informe uma nova senha o qual não corresponda as 5 últimas utilizadas.

Observações: O item (1), (2), (3) e (4) correspondem a parte das integrações realizadas pelo sistema Sian.

Projete e implemente as funcionalidades:

Gestão (inclusão, alteração, exclusão) de perguntas e respostas de segurança de um usuário.
Validar através do o e-mail do usuário se todas as respostas são válidas. Se houver inconsistências em qualquer resposta deverá ser retornado todas as perguntas.
Track das informações geridas pela gestão de perguntas e respostas do usuário.
Listar todas as perguntas e as respostas associadas de um e-mail.
Listar todas as alterações de perguntas e respostas de um e-mail.
Validar através do o e-mail sua existência.
Validar através do o e-mail se a senha informada corresponde as últimas 5 senhas já alteradas.
Considerações gerais do desafio:

Cobertura das funcionalidades por testes unitários deve ser no mínimo de 80%;
Qualquer funcionalidade deve possuir uma autenticação por JWT;
A execução do projeto deve ser realizada com uma linha utilizando o docker-compose;
O banco de dados MongoDb ou SqlServer;
Uma documentação clara e objetiva é necessária.-
Os contextos das integrações devem ser levados antes da criação da API.
Não é necessário criar o front-end. (Caso queira é um Opcional)

### Tecnologias, Padrões e Arquiteturas:
* .Net Core 6.0
* BD relacional Sql Server
* FluentValidator
* Swagger UI with Jwt support
* ILogger
* XUnit
* Entity Framework - Code First
* CQRS
* Mediator
* Repository

#### Observações: 
* Para obter o token - jwt, é necessário cadastrar um usuário ou logar
* Alterar o caminho do BD nas configurações

#### Docker
  * docker-compose up

