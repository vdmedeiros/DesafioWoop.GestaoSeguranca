# DesafioWoopi.GestaoSeguranca
* Este projeto tem como finalidade o teste que foi designado pela Woopi, sendo:

### Cenário:

Pedro tentou logar no sistema Sian até expirar suas tres tentativas, e percebeu que havia esquecido sua senha pois seu final de semana foi muito produtivo. Logo clicou no botão de "Resetar Senha", foi solicitado o e-mail do usuário onde após informá-lo (1), clicou em "Prosseguir" e foi exibido uma popup listando (2) todas perguntas cadastradas para o usuário. Após efetuar o preenchimento das respostas, clicou em "Verificar" submetendo (3) todas respostas do usuário para análise, caso alguma resposta esteja incorreta será exibido as perguntas incorretas de vermelho. Se todas respostas respondidas com sucesso, o usuário será redirecionado para tela de mudança de senha. É informado a senha onde Pedro clica em "Mudar Senha". O sistema faz uma verificação (4) se a nova senha corresponde já foi utilizada nas 5 ultimas mudanças, e caso tenha sido utilizada será exibido uma mensagem para nformar uma nova senha não correspondente as 5 ultimas cadastradas.

> **Obervações**: Os itens (1), (2), (3) e (4) correspondem as integrações realizadas pelo sistema Sian.
## Projete e implemente as funcionalidades:
* Gestão (inclusão, alteração, exclusão) de perguntas e respostas de segurança de um usuário.
* Validar através do o e-mail do usuário se todas respostas são válidas. Se houver inconsistências em qualquer resposta deverá ser retornado todas perguntas.
* Registrar os dados alterados (para auditoria) diante da gestão de perguntas e respostas do usuário realizadas.
* Listar todas perguntas e as respostas associadas a um e-mail.
* Listar todas alterações de perguntas e respostas a um e-mail.
* Verificar se e-mail existe na base e é válido.
* Validar através do e-mail se a senha informada corresponde as ultimas 5 senhas já alteradas.
* (OPCIONAL) Criar o front-end.
* (OPCIONAL) Criar o desenho arquitetural da solução. 
* (OPCIONAL) Documentação dos contextos, limites, levantados na análise.
* (OPCIONAL) Criar logs para auxiliar o desenvolvedor no futuro na resolução de bugs.

## Considerações gerais do desafio
* O(s) projeto(s) devem conter uma documentação swagger clara e bem documentada;
* O(s) contexto(s) das integrações devem ser analisados e levantados antes da criação das APIs;
* Verifique a consistências e cobertura das funcionalidades pelos testes unitários;
* Qualquer funcionalidade (endpoints) deve trabalhar com autenticação por JWT;
* Se informações forem persistidas, essas devem ser realizada em um banco de dados;
* Itens marcados com (OPCIONAL) não são obrigatórios, más se fizer será muito bem vindo!!!
* O(s) projeto(s) devem estar compilando e funcionando;
* Caso necessário algum passo para executar a aplicação, criar um topico no README informando todos os passos necessários.

## Entrega
* O tempo total para entrega desse desafio é de 5 dias. 
* O prazo se inicia _um dia após o recebimento_ do e-mail contendo o desafio.
* Para sinalizar a entrega do desafio, basta responder o e-mail que foi enviado com desafio, OU, enviar para o e-mail gabriel.ferri@stefanini.com.
* Se precisar de mais tempo não exite em perguntar por uma nova data.

## Autor - Desenvolvedor
* Vinícius Medeiros - vini.dmedeiros@gmail.com

### Tecnologias, Padrões e Arquiteturas:
* .Net Core 6.0
* BD relacional Sql Server
* FluentValidator
* Swagger UI with Jwt support
* ILogger
* Entity Framework - Code First
* Dapper
* CQRS
* Mediator
* Repository
* XUnit 

#### Observações: 
* Para obter o token - jwt, é necessário cadastrar um usuário ou logar
* Alterar o caminho do BD nas configurações

#### Docker
  * docker-compose up

