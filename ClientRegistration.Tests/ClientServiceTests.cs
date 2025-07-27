using ClientRegistration.Models;
using ClientRegistration.Services;
using Moq;

namespace ClientRegistration.Tests;

public class ClientServiceTests
{
    private readonly Mock<IClientDatabase> _mockDatabase;
    private readonly ClientService _clientService;
    private readonly Client _testClient;

    public ClientServiceTests()
    {
        _mockDatabase = new Mock<IClientDatabase>();
        _clientService = new ClientService(_mockDatabase.Object);
        
        _testClient = new Client 
        { 
            Id = 1, 
            Name = "Fulano", 
            Lastname = "de tal",
            Age = 20,
            Address = "Rua das Flores 99"
        };
    }

    [Fact]
    public async Task AddClientAsync_ShouldSaveClient_WhenClientIsValid()
    {
        // Arrange
        _mockDatabase.Setup(db => db.SaveClientAsync(It.IsAny<Client>()))
                    .ReturnsAsync(1);

        // Act
        var result = await _clientService.AddClientAsync(_testClient);

        // Assert
        Assert.Equal(1, result);
        _mockDatabase.Verify(db => db.SaveClientAsync(_testClient), Times.Once);
    }

    [Fact]
    public async Task AddClientAsync_ShouldThrowArgumentNullException_WhenClientIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _clientService.AddClientAsync(null));
    }

    [Fact]
    public async Task GetClientByIdAsync_ShouldReturnClient_WhenClientExists()
    {
        // Arrange
        _mockDatabase.Setup(db => db.GetClientByIdAsync(1))
                    .ReturnsAsync(_testClient);

        // Act
        var result = await _clientService.GetClientByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Fulano", result.Name);
        Assert.Equal("de tal", result.Lastname);
        _mockDatabase.Verify(db => db.GetClientByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetClientByIdAsync_ShouldReturnNull_WhenClientDoesNotExist()
    {
        // Arrange
        _mockDatabase.Setup(db => db.GetClientByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Client)null);

        // Act
        var result = await _clientService.GetClientByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllClientsAsync_ShouldReturnAllClients()
    {
        // Arrange
        var clients = new List<Client>
        {
            _testClient,
            new Client { Id = 2, Name = "Ciclano", Lastname = "de tal", Age = 18, Address = "Rua das Margaridas 100" }
        };

        _mockDatabase.Setup(db => db.GetAllClientsAsync())
                    .ReturnsAsync(clients);

        // Act
        var result = await _clientService.GetAllClientsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Fulano", result[0].Name);
        Assert.Equal("de tal", result[1].Lastname);
        _mockDatabase.Verify(db => db.GetAllClientsAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateClientAsync_ShouldUpdateClient_WhenClientExists()
    {
        // Arrange
        var updatedClient = new Client 
        { 
            Id = 1, 
            Name = "Fulano", 
            Lastname = "de tal",
            Age = 21,
            Address = "Rua das Flores 100" 
        };

        _mockDatabase.Setup(db => db.GetClientByIdAsync(1))
                    .ReturnsAsync(_testClient);
        _mockDatabase.Setup(db => db.SaveClientAsync(updatedClient))
                    .ReturnsAsync(1);

        // Act
        var result = await _clientService.UpdateClientAsync(updatedClient);

        // Assert
        Assert.Equal(1, result);
        _mockDatabase.Verify(db => db.GetClientByIdAsync(1), Times.Once);
        _mockDatabase.Verify(db => db.SaveClientAsync(updatedClient), Times.Once);
    }

    [Fact]
    public async Task UpdateClientAsync_ShouldThrowInvalidOperationException_WhenClientDoesNotExist()
    {
        // Arrange
        var nonExistentClient = new Client { Id = 999, Name = "Inexistente" };
        _mockDatabase.Setup(db => db.GetClientByIdAsync(999))
                    .ReturnsAsync((Client)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _clientService.UpdateClientAsync(nonExistentClient));
    }

    [Fact]
    public async Task DeleteClientAsync_ShouldDeleteClient_WhenClientExists()
    {
        // Arrange
        _mockDatabase.Setup(db => db.GetClientByIdAsync(1))
                    .ReturnsAsync(_testClient);
        _mockDatabase.Setup(db => db.DeleteClientAsync(1))
                    .ReturnsAsync(1);

        // Act
        var result = await _clientService.DeleteClientAsync(1);

        // Assert
        Assert.Equal(1, result);
        _mockDatabase.Verify(db => db.GetClientByIdAsync(1), Times.Once);
        _mockDatabase.Verify(db => db.DeleteClientAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteClientAsync_ShouldThrowInvalidOperationException_WhenClientDoesNotExist()
    {
        // Arrange
        _mockDatabase.Setup(db => db.GetClientByIdAsync(999))
                    .ReturnsAsync((Client)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _clientService.DeleteClientAsync(999));
    }
}
