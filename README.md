# ClientRegistration

Aplicativo de cadastro de clientes desenvolvido com .NET MAUI, multiplataforma (Android, iOS, Windows, MacCatalyst). Permite gerenciar uma lista de clientes, incluindo cadastro, edição, exclusão e visualização de detalhes.

## Funcionalidades
- Listagem de clientes cadastrados
- Cadastro de novos clientes
- Edição de dados de clientes existentes
- Exclusão de clientes

### Funcionalidades

- **Cadastro de Clientes:** Formulário para adicionar novos clientes com nome, e-mail e telefone.
- **Listagem de Clientes:** Visualização de todos os clientes cadastrados.
- **Edição de Clientes:** Modificação dos dados de clientes existentes.
- **Exclusão de Clientes:** Remoção de clientes do sistema.

## Tecnologias Utilizadas

- **.NET MAUI:** Framework para criação de aplicações multiplataforma (Windows, macOS, Android, iOS).
- **SQLite:** Banco de dados local para armazenamento de dados.
- **Padrão MVVM:** Arquitetura utilizada para separar a lógica de negócio da interface do usuário.

## Pré-requisitos

Antes de começar, garanta que você tenha o seguinte ambiente de desenvolvimento configurado:

- **.NET 9 SDK**
- **Visual Studio 2022** com a carga de trabalho **.NET Multi-platform App UI development** instalada.
- **Visual Studio Code** (Opcional) com as extensões **C# Dev Kit** e **.NET MAUI**.

## Como Executar a Aplicação

### Usando o Visual Studio 2022

1.  Clone o repositório para sua máquina local:
    ```bash
    git clone https://github.com/gimidia/ClientRegistration.git
    ```
2.  Navegue até o diretório do projeto.
3.  Abra o arquivo da solução `ClientRegistration.sln` com o Visual Studio 2022.
4.  Aguarde o Visual Studio restaurar todas as dependências do projeto (NuGet packages).
5.  Selecione o dispositivo de destino na barra de ferramentas (ex: `Windows Machine` para rodar no Windows ou um emulador Android).
6.  Pressione **F5** ou clique no botão **▶ Iniciar** para compilar e executar a aplicação.

### Usando o Visual Studio Code

1.  Clone o repositório para sua máquina local.
2.  Abra a pasta raiz do projeto (`ClientRegistration`) no Visual Studio Code.
3.  Abra um novo terminal no VS Code (**Terminal > New Terminal**).
4.  Navegue até o diretório do projeto:
    ```bash
    cd ClientRegistration
    ```
5.  Limpe o projeto para remover artefatos de compilações anteriores (Opcional, mas recomendado):
    ```bash
    dotnet clean
    ```
6.  Restaure as dependências do projeto executando o comando:
    ```bash
    dotnet restore
    ```
7.  Compile o projeto:
    ```bash
    dotnet build
    ```
8.  Execute a aplicação para a plataforma desejada. Por exemplo, para Windows:
    ```bash
    dotnet build -t:Run -f net9.0-windows10.0.19041.0
    ```
    *Você pode encontrar os frameworks de destino disponíveis no arquivo `ClientRegistration.csproj`.*

## Testes Unitários

O projeto inclui uma suíte abrangente de testes unitários para garantir a qualidade e o funcionamento correto das funcionalidades principais.

### Cobertura de Testes

Os testes cobrem todas as operações CRUD do serviço de clientes:

- **Listagem de clientes**
- **Busca por ID**
- **Cadastro de novos clientes**
- **Atualização de clientes existentes**
- **Exclusão de clientes**

### Como Executar os Testes

#### Usando o Terminal

1. Navegue até a pasta do projeto de testes:
   ```bash
   cd ClientRegistration.Tests
   ```

2. Execute o comando para rodar os testes:
   ```bash
   dotnet test
   ```
   
   *Ou, a partir da pasta raiz do projeto, você pode executar:*
   ```bash
   dotnet test ClientRegistration.Tests/ClientRegistration.Tests.csproj
   ```

#### Usando o Visual Studio 2022

1. Abra o **Test Explorer** (Menu **Test** > **Test Explorer**)
2. Clique em **Run All Tests** para executar todos os testes

#### Usando o Visual Studio Code

1. Instale a extensão **.NET Core Test Explorer**
2. Abra a aba de Testes na barra lateral
3. Clique no ícone de play para executar todos os testes

### Estrutura dos Testes

Os testes estão organizados no projeto `ClientRegistration.Tests` e seguem o padrão Arrange-Act-Assert (Preparar-Agir-Verificar).

Exemplo de um caso de teste:

```csharp
[Fact]
public async Task AddClientAsync_ShouldSaveClient_WhenClientIsValid()
{
    // Arrange
    var client = new Client { Name = "Fulano", Lastname = "de tal", Age = 20, Address = "Rua das Flores 99" };
    
    // Act
    var result = await _clientService.AddClientAsync(client);
    
    // Assert
    Assert.Equal(1, result);
}
```

## Estrutura do Projeto

- `Models/`: Contém as classes de modelo (ex: `Client`).
- `ViewModels/`: Contém os ViewModels que gerenciam a lógica da interface.
- `Views/`: Contém as páginas XAML da aplicação.
- `Services/`: Contém os serviços, como o de acesso ao banco de dados (`ClientService`).
- `Tests/`: Contém os testes unitários do projeto.
- `App.xaml.cs`: Ponto de entrada da aplicação.
- `MauiProgram.cs`: Configuração inicial da aplicação e injeção de dependência.

## Imagens da Aplicação

**Tela de Listagem:**
![Lista de Clientes](listaclientes.jpg)

**Tela de Cadastro:**
![Salvar Cliente](salvarcliente.jpg)

**Tela de Edição:**
![Editar Cliente](editarcliente.jpg)

**Confirmação de Exclusão:**
![Excluir Cliente](excluircliente.jpg)


Este projeto é apenas para fins de estudo/demonstração. 