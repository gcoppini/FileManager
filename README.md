
# Teste Consultor Digital Atento

TESTE PARA DESENVOLVEDOR .NET

O TESTE ABAIXO VISA AVALIAR O PERFIL TÉCNICO DOS CANDIDATOS A DESENVOLVEDOR BACKEND.
LEIA ATENTAMENTE AS INSTRUÇÕES ABAIXO E RESPONDA AS QUESTÕES DA MELHOR FORMA POSSÍVEL.

• O TESTE SERÁ COMPOSTO DE TRÊS PERGUNTAS TÉCNICAS E UMA AVALIAÇÃO PRÁTICA.
• RESPONDA AS PERGUNTAS COM SUAS PALAVRAS, COM SEU ENTENDIMENTO SOBRE O TEMA.
• PUBLIQUE O TESTE PRÁTICO COM AS RESPOSTAS EM UM REPOSITÓRIO NO GIT-HUB E NOS ENVIE.

## PERGUNTAS

1) EXPLIQUE COM SUAS PALAVRAS O QUE É DOMAIN DRIVEN DESIGN E SUA IMPORTÂNCIA NA ESTRATÉGIA DE DESENVOLVIMENTO DE SOFTWARE.
R:

2) EXPLIQUE COM SUAS PALAVRAS O QUE É E COMO FUNCIONA UMA ARQUITETURA BASEADA EM MICROSERVICES.
   EXPLIQUE GANHOS COM ESTE MODELO E DESAFIOS EM SUA IMPLEMENTAÇÃO.
R:

3) NA SUA OPINIÃO, É VANTAJOSO A CRIAÇÃO DE REGRAS DE NEGÓCIO DENTRO DO BANCO DE DADOS (PROCEDURES, VIEWS, ETC).
R:

## TESTE PRÁTICO

REGRA DE NEGÓCIO
CRIAR UMA API QUE, ATRAVÉS DE UMA REQUISIÇÃO HTTPS POST EFETUE A TRANSFERÊNCIA DE ARQUIVOS DE UM DIRETÓRIO PARA OUTRO.

OS DADOS DO ARQUIVO DEVEM SER SALVOS EM UMA TABELA DO BANCO DE DADOS PARA CONTROLE.
ESTA API DEVE SER PUBLICADA DENTRO DE UM CONTAINER DO DOCKER,
DEVE SER ENVIADO O DOCKER FILE PARA GERAÇÃO DO BUILD DA IMAGEM.  

PARÂMETROS DE ENTRADA
- NOME ARQUIVO
- BINÁRIO
- TAMANHO ARQUIVO
- MIME TYPE

PARÂMETROS DE SAÍDA
- HTTP STATUS CODE


## INFORMAÇÕES ADICIONAIS
• O MÉTODO “POST” DEVERA RECEBER OS PARAMETROS NO BODY DA REQUISIÇÃO EM FORMATO JSON
• UTILIZE DOMAIN DRIVEN DESIGN
• SERÃO AVALIADOS CRITÉRIOS DE ARQUITETURA COMO SEPARAÇÃO DE RESPONSABILIDADE, CLEAN CODE E TESTES
• TECNOLOGIAS QUE VOCÊ PODE UTILIZAR .NET CORE 2.X, C#, XUNITS OU NUNIT (TESTES)
• NO TÉRMINO DO PROJETO, PUBLIQUE O CÓDIGO EM UM REPOSITÓRIO NO GIT-HUB

# Observações

## Containner
- Presistente Storage

## Docker Compose
Para controlar as depêndencias entre os contâiners utilizei o docker-compose.

Para constuir as imagens utilize os comandos:

- docker-compose rm -s -f
- docker-compose build
- docker-compose up

Caso seja necessário instalar, ver link abaixo (Linux Debian)
https://www.digitalocean.com/community/tutorials/how-to-install-docker-compose-on-debian-9

Para verificar se os contâniers estão sendo executados:
- docker ps

## Links
- Swagger			http://localhost:5000/swagger/index.html
- API				http://localhost:5000/api/FileStorage/
- Mongo Express		http://localhost:8081/
- FrontEnd - 		http://localhost:8080/


## Mongo GridFS
Optei por utilizar o Mongo-GridFS pois com as informações que foram disponibilizadas, acredito que atenda os requisitos e também por oferecer mais recursos. Mais informações ver os links abaixo:
- https://docs.mongodb.com/manual/core/gridfs/
- https://docs.mongodb.com/manual/core/gridfs/#when-to-use-gridfs

## WIP
Padrões
- DDD - Domain Drive Design 
- DTO, Injeção de dependência, Repository Pattern, SOLID, CQS
- Design Patterns: Strategy
- Tratamento exceções
- Tratamento de concurrência
- Análise Thread-safe, Async, IDisposable, Logs, Complexidade ciclomática

Testes
- Mocked Unit Tests, Testes de integração, carga, user interfaces, test coverage