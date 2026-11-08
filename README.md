# MiniBitcoin Wallet

Uma aplicação de console interativa desenvolvida em **.NET** que simula uma carteira de criptoativos. O projeto gerencia saldos em moeda fiduciária (BRL) e Bitcoin (BTC), permitindo simular cotações, executar ordens de compra/venda, manter um histórico detalhado de transações e calcular o patrimônio consolidado.

O desenvolvimento deste software foi guiado estritamente pelas práticas de **TDD (Test-Driven Development)** e o histórico de versionamento segue o padrão rigoroso do **Conventional Commits**.

---

## Arquitetura e Estrutura do Projeto

A solução está dividida em dois projetos principais para garantir a separação de responsabilidades e testabilidade:

```text
MiniBitcoin/
├── MiniBitcoin.sln                  # Arquivo de solução global do .NET
├── MiniBitcoin.Console/             # Aplicação principal (Interface de Linha de Comando)
│   ├── Models/
│   │   ├── Wallet.cs                # Entidade de domínio (Regras de negócio e saldos)
│   │   └── Transaction.cs           # Registro/Entidade de transações executadas
│   ├── Program.cs                   # Menu interativo e ponto de entrada da aplicação
│   └── MiniBitcoin.Console.csproj
└── MiniBitcoin.Tests/               # Projeto de Testes Unitários automatizados
    ├── WalletTests.cs               # Cobertura completa de testes usando xUnit
    └── MiniBitcoin.Tests.csproj
```

### Principais Regras de Negócio Implementadas
* **Validação de Saldo:** Não é permitido iniciar a carteira com valores negativos ou efetuar transações além do limite disponível (evitando saldo negativo em BRL ou BTC).
* **Precisão Decimal:** Todas as operações financeiras utilizam o tipo `decimal` nativo do C# para evitar problemas de arredondamento de ponto flutuante. O saldo em BTC é exibido com precisão de até 8 casas decimais (Satoshis).
* **Histórico Imutável:** O livro-razão de transações (`Transactions`) expõe apenas uma coleção somente leitura (`IReadOnlyCollection`), garantindo que o histórico não seja modificado externamente.

---

## Como Executar o Projeto

### Pré-requisitos
* [SDK do .NET 8.0](https://microsoft.com) (ou superior) instalado em sua máquina.
* Git para controle de versão.

### Passos para Execução

1. **Clone o repositório:**
   ```bash
   git clone <URL_DO_SEU_REPOSITORIO_GITHUB>
   cd MiniBitcoin
   ```

2. **Restaure as dependências do projeto:**
   ```bash
   dotnet restore
   ```

3. **Execute a aplicação de Console:**
   ```bash
   dotnet run --project MiniBitcoin.Console/MiniBitcoin.Console.csproj
   ```

---

## Como Executar os Testes Unitários

Os testes foram escritos utilizando o framework **xUnit** para validar todos os fluxos de sucesso e exceções de negócio (como tentativas de compra sem saldo ou cotações inválidas).

Para rodar a suíte de testes automatizados, execute o comando abaixo na raiz do projeto:

```bash
dotnet test
```

---

## Padrão de Commits Utilizado

O histórico do repositório foi construído utilizando a especificação do **Conventional Commits** para manter o log limpo e legível. Os prefixos utilizados mapeiam as etapas de desenvolvimento:

* `feat(carteira|transacao|console)`: Introdução de novas funcionalidades.
* `test(carteira|transacao|extrato)`: Adição ou modificação da suíte de testes unitários.
* `refactor(core)`: Alterações de código que não corrigem bugs nem adicionam funcionalidades (legibilidade e organização).
* `chore`: Mudanças estruturais e configurações iniciais da solução.
* `docs`: Atualizações de documentação (como a criação deste arquivo).

Você pode conferir a árvore de histórico a qualquer momento usando:
```bash
git log --oneline
```
