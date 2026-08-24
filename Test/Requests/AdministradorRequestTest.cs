using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.DTOs;
using Test.Helpers;
using MinimalApi.Dominio.Enuns;
using System.Net.Http.Headers;

namespace Test.Requests;

[TestClass]
public class AdministradorRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    [TestMethod]
    public async Task TestarGetSetPropriedades()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(admLogado?.Email ?? "");
        Assert.IsNotNull(admLogado?.Perfil ?? "");
        Assert.IsNotNull(admLogado?.Token ?? "");

        Console.WriteLine(admLogado?.Token);
    }
    [TestMethod]
    public async Task TestarCriarAdministrador()
    {
        var client = Setup.client;

        // 1. Autentica para obter o Token
        var loginDto = new LoginDTO { Email = "adm@teste.com", Senha = "123456" };
        var loginContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
        var loginResponse = await client.PostAsync("/administradores/login", loginContent);

        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(loginResult);
        var token = jsonDoc.RootElement.GetProperty("token").GetString();

        // 2. Inclui o Token na requisição
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // 3. Faz a criação do Administrador
        var admDto = new AdministradorDTO
        {
            Email = "novo.adm@teste.com",
            Senha = "123456",
            Perfil = Perfil.Adm
        };

        var content = new StringContent(JsonSerializer.Serialize(admDto), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/administradores", content);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
}