using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
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
    public async Task TestarCriarVeiculoComSucesso()
    {
        var client = Setup.client;

        var loginDto = new LoginDTO { Email = "adm@teste.com", Senha = "123456" };
        var loginContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
        var loginResponse = await client.PostAsync("/administradores/login", loginContent);

        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(loginResult);
        var token = jsonDoc.RootElement.GetProperty("token").GetString();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var veiculoDto = new VeiculoDTO
        {
            Nome = "Uno Mille",
            Marca = "Fiat",
            Ano = 2010
        };
        var veiculoContent = new StringContent(JsonSerializer.Serialize(veiculoDto), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/veiculos", veiculoContent);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
}