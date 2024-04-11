
Implementado o conceito de design by contracts usando o package Flunt, para validar o input de dados

Implementado CRQS de forma simplificada

com comands e handles 

commands fica resposavel por fazer a ponte de entrada de dados, muito parecido com função dos DTos

inclusive nos commands foi implementa o conceito de FailFastValidation, que valida os dados na entrada, ou seja, se algo estiver errado a requisição já para neste ponto evitando assim um overhead no banco de dados.

Handler basicamente faz o processamento dos commands, gerencia os fluxos da aplicação

