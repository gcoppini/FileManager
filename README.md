
# Teste Consultor Digital Atento

TESTE PARA DESENVOLVEDOR .NET

O TESTE ABAIXO VISA AVALIAR O PERFIL TÉCNICO DOS CANDIDATOS A DESENVOLVEDOR BACKEND.
LEIA ATENTAMENTE AS INSTRUÇÕES ABAIXO E RESPONDA AS QUESTÕES DA MELHOR FORMA POSSÍVEL.

• O TESTE SERÁ COMPOSTO DE TRÊS PERGUNTAS TÉCNICAS E UMA AVALIAÇÃO PRÁTICA.
• RESPONDA AS PERGUNTAS COM SUAS PALAVRAS, COM SEU ENTENDIMENTO SOBRE O TEMA.
• PUBLIQUE O TESTE PRÁTICO COM AS RESPOSTAS EM UM REPOSITÓRIO NO GIT-HUB E NOS ENVIE.

## PERGUNTAS

1. **EXPLIQUE COM SUAS PALAVRAS O QUE É DOMAIN DRIVEN DESIGN E SUA IMPORTÂNCIA NA ESTRATÉGIA DE DESENVOLVIMENTO DE SOFTWARE.**

- Domain Drive Design ou DDD é uma abordagem para desenvolvimento de um software. Consiste na divisão em camadas lógicas e físicas visando sobretudo a redução da complexidade na sua elaboração e manutenção.

- Sua importância é, na minha opnião, principalmente na padronização dessa abordagem de elaboração, no sentido do ganho de qualidade e tempo pois oferece os recursos para que software possa receber novas funcionalidades ao mesmo tempo que garanta sua integridadde funcionamento. Além disso, devido à essa padronização, quando combinado com outras práticas, como desenvolvimento ágil(SCRUM), DevOps, etc pode contribuir nas repostas à demandas negócio.

2. **EXPLIQUE COM SUAS PALAVRAS O QUE É E COMO FUNCIONA UMA ARQUITETURA BASEADA EM MICROSERVICES.
   EXPLIQUE GANHOS COM ESTE MODELO E DESAFIOS EM SUA IMPLEMENTAÇÃO.**

- Eu diria que microservices ou micro serviços é uma arquitetura de solução. Ou seja é uma forma de se organizar uma solução baseada em softwares.  
Consiste em serviços ou seja softwares com uma única e dedicada responsabilidade que se comumicam com outros microserviços através de mensagens, com a implementação de diversos padrões para que esses serviços garantam que estão se comunicando entre sí e atendendo a quantidade de requições dos usuários.

- Podem ser escaláveis tanto verticalmente(número de instancias do mesmo serviço) quanto verticalmente (quantidade de servidores com esses serviços), para isso utilizam de recursos conhecidos como contáiner e sua o orquestração (controle das instâcias, servidores e recursos)

- Pode ser tolerante à falhas, resilientes, quando implementado patterns para esse fim.

- Podem ser implementados em diferentes tecnologias, desde que sigam o padrão das mensagens, permitindo assim o uso da tecnoligia mais adequada para resolução de determinado problema.

- Entretantdo, exigem, para melhor aproveitamento das vantagens, grande maturidade da equipe de desenvolvimento pois seu desenvolvimento e manutenção é complexo, além de uma infraestrutura de desenvolvimento(DDD, SCRUM, DevOps, testes unitários,documentação dos serviços) e publicação robusta (kubernetes, diferentes nuvens, instrumentação/monitoramento, API Management).

3. **NA SUA OPINIÃO, É VANTAJOSO A CRIAÇÃO DE REGRAS DE NEGÓCIO DENTRO DO BANCO DE DADOS (PROCEDURES, VIEWS, ETC).**

- Depende do cenário. Isso porque depende dos recursos oferecidos pela empresa, ou ainda se estiver lidando sistemas legados onde as regras já estão no banco de dados.

- Se eu fosse fazer uma recomendação, isso se baseria na tecnologia disponível, no momento em que empresa, na maturidade da equipe de desenvolvimento e na visão e engajamento dos gestores (ou dos que tomam decisão).

- Técnicamente, é possível que isso seja feito, porém teriamos que lidar com essa questões:

- Reaproveitmento de código e regras de negócio.

- Manutenção: eventualmente essas regras de negócio teriam que ser duplicadas em outros módulos das soluções o que geraria uma  dificuldade na manutenção e na análise de problemas.

- Outro ponto é a questão das dependências de "jobs", agendamentos, processamento em batch por limitações impostas por essa abordagem.

- A questão dos recursos do servidor serem utilizados além do armazenamento de dados também para o processamento das regras de negócio, o que com o tempo pode implicar em se adquirir ou atualizar esses servidores para lidar com esse processamento adicional.

- Concentração das regras de negócio onde os dados deveriam ser somente armazenados, o que implicaria numa dependência tecnologica, pois na eventual necessidade migração de dados, teriam que ser migrados/refeitas também regras de negócio armazenadas nos banco de dados(vide Oracle Forms/Reports). Seria outra restrição à mudança

- Questões conceituais como repositório de dados, separação em camadas, componentes, etc...

Considerando isso, eu ***não*** recomendaria esta abordagem em um projeto novo. Ou seja, acredito que hoje temos tecnologia e abordagens melhores em vários aspectos, então acredito que não seria vantojoso, eu recomedaria arquiteturas como SOA, sistemas reativos(https://www.reactivemanifesto.org/pt-BR) e microserviços que podem trazer uma vantagem competitiva e melhoria nos processos de negócio em termos de tempo, qualidade e custo* se implementadas adequadamente.

# TESTE PRÁTICO

## REGRA DE NEGÓCIO
CRIAR UMA API QUE, ATRAVÉS DE UMA REQUISIÇÃO HTTPS POST EFETUE A TRANSFERÊNCIA DE ARQUIVOS DE UM DIRETÓRIO PARA OUTRO.

OS DADOS DO ARQUIVO DEVEM SER SALVOS EM UMA TABELA DO BANCO DE DADOS PARA CONTROLE.
ESTA API DEVE SER PUBLICADA DENTRO DE UM CONTAINER DO DOCKER,
DEVE SER ENVIADO O DOCKER FILE PARA GERAÇÃO DO BUILD DA IMAGEM.  

PARÂMETROS DE ENTRADA
* NOME ARQUIVO
* BINÁRIO
* TAMANHO ARQUIVO
* MIME TYPE

PARÂMETROS DE SAÍDA
* HTTP STATUS CODE


## INFORMAÇÕES ADICIONAIS
* O MÉTODO “POST” DEVERA RECEBER OS PARAMETROS NO BODY DA REQUISIÇÃO EM FORMATO JSON
* UTILIZE DOMAIN DRIVEN DESIGN
* SERÃO AVALIADOS CRITÉRIOS DE ARQUITETURA COMO SEPARAÇÃO DE RESPONSABILIDADE, CLEAN CODE E TESTES
* TECNOLOGIAS QUE VOCÊ PODE UTILIZAR .NET CORE 2.X, C#, XUNITS OU NUNIT (TESTES)
* NO TÉRMINO DO PROJETO, PUBLIQUE O CÓDIGO EM UM REPOSITÓRIO NO GIT-HUB

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
* Swagger			http://localhost:5000/swagger/index.html
* API				http://localhost:5000/api/FileStorage/
* Mongo Express		http://localhost:8081/
* FrontEnd - 		http://localhost:8080/


## Mongo GridFS
Optei por utilizar o Mongo-GridFS pois com as informações que foram disponibilizadas, acredito que atenda os requisitos e também por oferecer mais recursos. Mais informações ver os links abaixo:
- https://docs.mongodb.com/manual/core/gridfs/
- https://docs.mongodb.com/manual/core/gridfs/#when-to-use-gridfs

## WIP
* Padrões
   * DDD - Domain Drive Design 
   *  DTO, Injeção de dependência, Repository Pattern, SOLID, CQS
   *  Tratamento exceções
   *  Tratamento de concurrência
   *  Análise Thread-safe, Async, IDisposable, Logs, Complexidade ciclomática
* Testes
   *  Mocked Unit Tests, Testes de integração, carga, user interfaces, test coverage