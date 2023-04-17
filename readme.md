# Prova BonifiQ Backend

Prova conclu�da

## Solu��o Parte1Controller

Para resolver o bug foi necess�rio utilizar um outro tipo de registro de dep�ndencia. O `RandomService.cs` estava registrado como Singleton e por isso sempre retornava o mesmo n�mero pois s� foi instanciado uma vez sendo utilizado por toda a aplica��o.
Registrando `RandomService.cs` como Scoped soluciona o problema, pois agora o objeto ser� instanciado para cada requisi��o. Mas poderiamos registrar como Transient se quissemos utilizar este servi�o em outras classes da aplica��o j� que seria instanciado sempre que fosse solicitado independente da requisi��o.

## Solu��o Parte2Controller

Foi utilizado os met�dos `Skip()` e `Take()` na linq query para retornar o n�mero de itens de acordo com a p�gina e da quantidade de itens por p�gina.

Remover o uso do context dos Controllers e inserir os servi�os `ProductService.cs` e `CustomerService.cs` como inje��o de depend�ncia (registrando como Scoped).

Remo��o das classes `ProductList.cs` e `CustomerList.cs` que foram substitu�das pela classe `PageListService.cs`, uma classe abstrata gen�rica que retorna os itens da p�gina espec�fica.

Os servi�os `CustomerService.cs` e `ProductService.cs` v�o herdar a classe `PageListService.cs` e utilizar o met�do implementado na clase abstrata gen�rica para retornar os itens da p�gina.

## Solu��o Parte3Controller

Para est� solu��o foi pensado no uso de smart enums, simular um enum que enumera classes ao inv�s de simples chave-valor (string-int).

Foi criado um SmartEnum, nele s�o inserida met�dos b�sicos para retornar a classe espec�ficada de acordo com o valor da string ou pelo int, assim como nos enum do C#.

Todas as classes enumeradas s�o inseridas no `PaymentType.cs` que implementa a classe abstrata gen�rica `Enumeration.cs`. Na classe `PaymentType.cs` tamb�m � declarado met�dos abstratos para serem implementados em cada classe (`PaymentByPix.cs`, `PaymentByPaypal.cs`, `PaymentByCreditCard.cs`)

Assim, podemos utilizar um enum padr�o para ser passado como parametro no controller (`PaymentTypeEnum.cs`) e utilizar o seu valor no met�do PayOrder para selecionar a classe espec�fica e executar o met�do de pagamento.

Mas essa implementa��o permite tamb�m que o c�digo antigo seja mantido, ou seja, o par�metro pode continuar sendo `string paymentMethod`, dessa forma apenas ter�amos que buscar a classe espec�ficada pela string ao inv�s de buscar pelo valor de int.

```
var paymentT = PaymentType.FromDisplayName(paymentMethod);
```

Da mesma forma poder�amos receber como par�metro um int `int paymentMethod`, s� seria necess�rio fazer a seguinte mudan�a:

```
var paymentT = PaymentType.FromValue(paymentMethod);
```

Dessa forma temos mais flexibilidade para mudar o tipo de par�metro e inserir novas formas de pagamento.

## Solu��o Parte4Controller

As regras de neg�cio foram separadas em 3 met�dos distintos, assim fica mais f�cil realizar testes pois agora podemos testar uma regra de neg�cio por ver.

Fixture para instanciar o contexto (InMemory) e inserir dados no banco de dados de teste.

Testes para verificar quando o met�do retorna uma `Task` com sucesso e para verificar se � retornado uma `Exception` com a mensagem espec�ficada.

Testes est�o localizados no arquivo de teste `Parte4Controller_Test.cs`

## O que ainda pode ser melhorado

- Usar outra forma de trazer um n�mero randomico.
- Fazer uso da vari�vel `HasNext` para verificar se � poss�vel solicitar mais uma p�gina e assim n�o fazer requisi��es desnecess�rias.
- Uso de interfaces para espec�ficar met�dos de pagamento.
- Utilizar o SmartEnums implementado do [pacote nuget de Ardalis](https://github.com/ardalis/SmartEnum)
- Inser��o de mais casos de testes.
- Fixture de servi�os ao inv�s de apenas o contexto.
