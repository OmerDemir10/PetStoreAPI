
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace PetStoreAPI;

[TestFixture]
public class PetStore
{
    public RestClient restClient;

    [SetUp]
    public void Setup()
    {
        restClient = new RestClient("https://petstore.swagger.io/v2");
    }
    
    [Test]
    public void FindPetsByStatus_ReturnsOk()
    {
        // Create a new RestRequest with GET method
        var request = new RestRequest("/pet/findByStatus", Method.Get);

        // Set headers
        request.AddHeader("accept", "application/json");

        // Set query parameter
        request.AddParameter("status", "available");

        // Execute the request
        var response = restClient.Execute(request);

        // Assert that the response status code is 200 OK
        Assert.AreEqual(200, (int)response.StatusCode);
    }
    
    [Test]
    public void TryToFindPetsByInvalidStatus_ReturnsBadRequest()
    {
        // Create a new RestRequest with GET method
        var request = new RestRequest("/pet/findByStatus", Method.Get);

        // Set headers
        request.AddHeader("accept", "application/json");

        // Set query parameter
        request.AddParameter("status", "not_a_status");

        // Execute the request
        var response = restClient.Execute(request);

        // Assert that the response status code is 200 OK
        Assert.AreEqual(404, (int)response.StatusCode);
    }
    
    [Test]
    public void CreatePet_ReturnsOk()
    {
        // Create a new RestRequest with POST method
        var request = new RestRequest("/pet", Method.Post);

        // Set headers
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        // Set body
        var body = new
        {
            id = 2,
            category = new { id = 2, name = "Kangal" },
            name = "Meow",
            photoUrls = new[] { "https://unsplash.com/photos/iV_N83Y-j_A" },
            tags = new[] { new { id = 2, name = "Kangal" } },
            status = "available"
        };
        request.AddJsonBody(body);

        // Execute the request
        var response = restClient.Execute(request);

        // Assert that the response status code is 200 OK
        Assert.AreEqual(200, (int)response.StatusCode);
    }
    
    
    [Test]
    public void TryToCreatePetWithWrongMethod_ReturnsBadRequest()
    {
        // Try to create a new RestRequest with GET method
        var request = new RestRequest("/pet", Method.Get);

        // Set headers
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        // Set body
        var body = new
        {
            id = 8,
            category = new { id = 8, name = "Kangal" },
            name = "Meow",
            photoUrls = new[] { "https://unsplash.com/photos/iV_N83Y-j_A" },
            tags = new[] { new { id = 8, name = "Kangal" } },
            status = "available"
        };
        request.AddJsonBody(body);

        // Execute the request
        var response = restClient.Execute(request);

        // Assert that the response status code is 200 OK
        Assert.AreEqual(405, (int)response.StatusCode);
    }
    
    
    [Test]
    public void CreatePet_InvalidId_ReturnsBadRequest()
    {
        // Create a new RestRequest with POST method
        var request = new RestRequest("/pet", Method.Post);

        // Set headers
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        // Set body with an invalid id
        var body = new
        {
            id = -1, // Invalid id
            category = new { id = -1, name = "Kangal" },
            name = "Body",
            photoUrls = new[] { "https://unsplash.com/photos/x5oPmHmY3kQ" },
            tags = new[] { new { id = -1, name = "Kangal" } },
            status = "available"
        };
        request.AddJsonBody(body);

        // Execute the request
        var response = restClient.Execute(request);

        // Assert that the response status code is 400 Bad Request
        Assert.AreEqual(400, (int)response.StatusCode);
    }

    [Test]
    public void AddPetTest()
    {
        var request = new RestRequest("pet", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        var body = new
        {
            id = 10,
            category = new
            {
                id = 10,
                name = "Kangal"
            },
            name = "Furry",
            photoUrls = new[] { "string" },
            tags = new[]
            {
                new
                {
                    id = 10,
                    name = "Kangal"
                }
            },
            status = "available"
        };
        request.AddJsonBody(body);

        var response = restClient.Execute(request);
        Assert.AreEqual(200, (int)response.StatusCode);
    }

    
    [Test]
    public void UpdatePetTest()
    {
        var request = new RestRequest("pet", Method.Put);
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        var body = new
        {
            id = 9,
            category = new
            {
                id = 9,
                name = "Kangal"
            },
            name = "Pretty",
            photoUrls = new[] { "string" },
            tags = new[]
            {
                new
                {
                    id = 9,
                    name = "Kangal"
                }
            },
            status = "available"
        };
        request.AddJsonBody(body);

        var response = restClient.Execute(request);
        Assert.AreEqual(200, (int)response.StatusCode);
    }
    
    
    [Test]
    public void TryToUpdatePetWithoutProvidingStatus_ReturnBadRequest()
    {
        var request = new RestRequest("pet", Method.Put);
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        var body = new
        {
            id = "10", // invalid id ( it must be integer )
            category = new
            {
                id = 10,
                name = "Kangal"
            },
            name = "Pretty",
            photoUrls = new[] { "string" },
            tags = new[]
            {
                new
                {
                    id = 10,
                    name = "Kangal"
                }
            },
            status =12 // invalid status ( it must be string )
        };
        request.AddJsonBody(body);

        var response = restClient.Execute(request);
        Assert.AreEqual(404, (int)response.StatusCode);
    }

    
    [Test]
    public void CreatePet_ValidPet_ReturnsOk()
    {
        // Create a new RestRequest with POST method
        var request = new RestRequest("/pet", Method.Post);

        // Set headers
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("api_key", "XEz8meopnybk7R6upyU2tjEW0vmNKLGF");

        // Set body
        var body = new
        {
            id = 1,
            category = new { id = 1, name = "Kangal" },
            name = "Body",
            photoUrls = new[] { "https://unsplash.com/photos/x5oPmHmY3kQ" },
            tags = new[] { new { id = 1, name = "Kangal" } },
            status = "available"
        };
        request.AddJsonBody(body);

        // Execute the request
        var response = restClient.Execute(request);

        // Assert that the response status code is 200 OK
        Assert.AreEqual(200, (int)response.StatusCode);
    }
    
    [Test]
    public void GetPetById_ShouldReturnPet()
    {
        // Arrange
        var request = new RestRequest("pet/{id}", Method.Get);
        request.AddHeader("accept", "application/json");
        request.AddHeader("api_key", "api_key");
        request.AddUrlSegment("id", "9");

        // Act
        var response = restClient.Execute(request);

        // Assert
        Assert.AreEqual(200, (int)response.StatusCode);
        
        // Parse the response content as a JObject
        var jsonResponse = JObject.Parse(response.Content);

        // Assert that the name property is equal to the expected value
        Assert.That(jsonResponse["name"].ToString(), Is.EqualTo("Pretty"));
    }
    
    [Test]
    public void UpdatePet_ShouldUpdatePetNameAndStatus()
    {
        // Arrange
        var request = new RestRequest("pet/{id}", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("name", "Woaw");
        request.AddParameter("status", "ddd");
        request.AddUrlSegment("id", "10");

        // Act
        var response = restClient.Execute(request);

        // Assert
        Assert.AreEqual(200, (int)response.StatusCode);
        
    }

    [Test]
    public void UploadImage_ValidImage_ReturnsOk()
    {
        // First, create a new pet
        var createRequest = new RestRequest("/pet", Method.Post);
        createRequest.AddJsonBody(new { id = 1, name = "Fido" });
        var createResponse = restClient.Execute(createRequest);
        Assert.AreEqual(200, (int)createResponse.StatusCode);
        
        // Create a new RestRequest with POST method
        var request = new RestRequest("/pet/1/uploadImage", Method.Post);

        // Set headers
        request.AddHeader("accept", "application/json");
        request.AddHeader("Content-Type", "multipart/form-data");

        // Set file to upload
        string filePath = "/Users/omerdemir/Desktop/PetStore/dog.png";
        request.AddFile("file", filePath);

        // Execute the request
        var response = restClient.Execute(request);

        // Assert that the response status code is 200 OK
        Assert.AreEqual(200, (int)response.StatusCode);
    }
    
    [Test]
    public void DeletePet_ShouldReturnSuccess()
    {
        // Arrange
        var request = new RestRequest("pet/{id}", Method.Delete);
        request.AddHeader("accept", "application/json");
        request.AddUrlSegment("id", "10");

        // Act
        var response = restClient.Execute(request);

        // Assert
        Assert.AreEqual(200, (int)response.StatusCode);
    }
    
    [Test]
    public void DeleteSamePet_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new RestRequest("pet/{id}", Method.Delete);
        request.AddHeader("accept", "application/json");
        request.AddUrlSegment("id", "10");

        // Act
        var response = restClient.Execute(request);

        // Assert
        Assert.AreEqual(404, (int)response.StatusCode);
    }

    [TearDown]
    public void TearDown()
    {
        restClient.Dispose();
    }
    
}