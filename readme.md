# Prova BonifiQ Backend

Prova concluída

## Solução Parte1Controller

Para resolver o bug foi necessário utilizar um outro tipo de registro de depêndencia. O `RandomService.cs` estava registrado como Singleton e por isso sempre retornava o mesmo número pois só foi instanciado uma vez sendo utilizado por toda a aplicação.
Registrando `RandomService.cs` como Scoped soluciona o problema, pois agora o objeto será instanciado para cada requisição. Mas poderiamos registrar como Transient se quissemos utilizar este serviço em outras classes da aplicação já que seria instanciado sempre que fosse solicitado independente da requisição.

## Solução Parte2Controller

Foi utilizado os metódos `Skip()` e `Take()` na linq query para retornar o número de itens de acordo com a página e da quantidade de itens por página.

Remover o uso do context dos Controllers e inserir os serviços `ProductService.cs` e `CustomerService.cs` como injeção de dependência (registrando como Scoped).

Remoção das classes `ProductList.cs` e `CustomerList.cs` que foram substituídas pela classe `PageListService.cs`, uma classe abstrata genérica que retorna os itens da página específica.

Os serviços `CustomerService.cs` e `ProductService.cs` vão herdar a classe `PageListService.cs` e utilizar o metódo implementado na clase abstrata genérica para retornar os itens da página.

## Solução Parte3Controller

Para está solução foi pensado no uso de smart enums, simular um enum que enumera classes ao invés de simples chave-valor (string-int).

Foi criado um SmartEnum, nele são inserida metódos básicos para retornar a classe específicada de acordo com o valor da string ou pelo int, assim como nos enum do C#.

Todas as classes enumeradas são inseridas no `PaymentType.cs` que implementa a classe abstrata genérica `Enumeration.cs`. Na classe `PaymentType.cs` também é declarado metódos abstratos para serem implementados em cada classe (`PaymentByPix.cs`, `PaymentByPaypal.cs`, `PaymentByCreditCard.cs`)

Assim, podemos utilizar um enum padrão para ser passado como parametro no controller (`PaymentTypeEnum.cs`) e utilizar o seu valor no metódo PayOrder para selecionar a classe específica e executar o metódo de pagamento.

Mas essa implementação permite também que o código antigo seja mantido, ou seja, o parâmetro pode continuar sendo `string paymentMethod`, dessa forma apenas teríamos que buscar a classe específicada pela string ao invés de buscar pelo valor de int.

```
var paymentT = PaymentType.FromDisplayName(paymentMethod);
```

Da mesma forma poderíamos receber como parâmetro um int `int paymentMethod`, só seria necessário fazer a seguinte mudança:

```
var paymentT = PaymentType.FromValue(paymentMethod);
```

Dessa forma temos mais flexibilidade para mudar o tipo de parâmetro e inserir novas formas de pagamento.

## Solução Parte4Controller

As regras de negócio foram separadas em 3 metódos distintos, assim fica mais fácil realizar testes pois agora podemos testar uma regra de negócio por ver.

Fixture para instanciar o contexto (InMemory) e inserir dados no banco de dados de teste.

Testes para verificar quando o metódo retorna uma `Task` com sucesso e para verificar se é retornado uma `Exception` com a mensagem específicada.

Testes estão localizados no arquivo de teste `Parte4Controller_Test.cs`

## O que ainda pode ser melhorado

- Usar outra forma de trazer um número randomico.
- Fazer uso da variável `HasNext` para verificar se é possível solicitar mais uma página e assim não fazer requisições desnecessárias.
- Uso de interfaces para específicar metódos de pagamento.
- Utilizar o SmartEnums implementado do [pacote nuget de Ardalis](https://github.com/ardalis/SmartEnum)
- Inserção de mais casos de testes.
- Fixture de serviços ao invés de apenas o contexto.
