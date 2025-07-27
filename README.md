# ClientRegistration

Aplicativo de cadastro de clientes desenvolvido com .NET MAUI, multiplataforma (Android, iOS, Windows, MacCatalyst). Permite gerenciar uma lista de clientes, incluindo cadastro, edição, exclusão e visualização de detalhes.

## Funcionalidades
- Listagem de clientes cadastrados
- Cadastro de novos clientes
- Edição de dados de clientes existentes
- Exclusão de clientes
- Visualização de detalhes do cliente
- Validação de campos (ex: idade numérica)
- Interface responsiva e moderna

## Estrutura do Projeto
- **Models/**: Contém o modelo de dados `Client` (Id, Nome, Sobrenome, Idade, Endereço)
- **Services/**: Serviços de acesso a dados e regras de negócio (`ClientService`, `ClientDatabase`)
- **ViewModels/**: Lógica de apresentação e binding para as views
- **Views/**: Telas do app (listagem, detalhes, formulário)
- **Resources/**: Recursos visuais (ícones, imagens, fontes, estilos, cores)
- **Behaviors/**: Comportamentos customizados para validação
- **Platforms/**: Arquivos específicos de cada plataforma

## Tecnologias Utilizadas
- [.NET MAUI](https://learn.microsoft.com/pt-br/dotnet/maui/)
- SQLite (via [sqlite-net-pcl](https://github.com/praeclarum/sqlite-net))
- MVVM (Model-View-ViewModel)

## Requisitos
- .NET 9.0 SDK ou superior
- Visual Studio 2022 ou superior (com workload MAUI)
- Emulador ou dispositivo Android/iOS, ou ambiente Windows/Mac para testes

## Instalação e Execução
1. Clone o repositório:
   ```bash
   git clone https://github.com/gimidia/ClientRegistration.git
   ```
2. Abra a solução no Visual Studio.
3. Restaure os pacotes NuGet.
4. Selecione a plataforma desejada (Android, iOS, Windows, MacCatalyst).
5. Execute o projeto (F5 ou Ctrl+F5).

### Usando VS Code
Se você estiver utilizando o VS Code, abra a pasta do projeto na IDE e execute os comandos:

```bash
dotnet restore
dotnet run --framework net9.0-windows10.0.19041.0
```

## Estrutura de Dados
O modelo `Client` possui os seguintes campos:
- **Id**: int (chave primária, autoincremento)
- **Name**: string (nome do cliente)
- **Lastname**: string (sobrenome)
- **Age**: int (idade)
- **Address**: string (endereço)

## Telas Principais
- **Lista de Clientes**: Exibe todos os clientes cadastrados, com opções para editar ou excluir.
- **Detalhe/Cadastro de Cliente**: Formulário para inserir ou editar nome, sobrenome, idade e endereço.

## Telas da Aplicação

**Lista de Clientes**

![Lista de Clientes](./listaclientes.jpg)

**Salvar Cliente**

![Salvar Cliente](./salvarcliente.jpg)

**Editar Cliente**

![Editar Cliente](./editarcliente.jpg)

**Excluir Cliente**

![Excluir Cliente](./excluircliente.jpg)


## Personalização Visual
O app utiliza estilos e cores customizadas definidos em `Resources/Styles/Styles.xaml` e `Resources/Styles/Colors.xaml`, com suporte a temas claro e escuro. As fontes OpenSans são utilizadas para uma melhor legibilidade.

## Licença
Este projeto é apenas para fins de estudo/demonstração. 