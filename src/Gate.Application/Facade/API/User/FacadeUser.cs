using Gate.Domain.FacadeModels.Models;
using Gate.backend.Utils.Settings;
using Newtonsoft.Json;
using RestSharp;

namespace Gate.Application.Facade.API
{
    public class FacadeUser
    {
        public string CreateUserAPIKey(string username) 
        {
            try
            {
                var client = new RestClient(AppSettingsAcessor.GetAPIUrl("octoprint"));
                
                var request = new RestRequest($"/access/users/{username}/apikey", Method.Post);
                request.AddHeader("X-Api-Key", AppSettingsAcessor.GetGlobalAPIKey());
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                    throw new HttpRequestException($"Falha ao se comunicar com API Octoprint. StatusCode:{response.StatusCode} Content: {response.Content} ErrorMessage:{response.ErrorMessage}"); 

                return JsonConvert.DeserializeObject<FacadeUserApiKeyResponse>(response.Content).ApiKey;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao criar API KEY do usuário na API Octoprint: {ex.Message}");
            }
        }

        public bool CreateServiceUser(string userDescription, string password) 
        {
            try
            {
                var facadeUserInfo = new FacadeUserInfo() 
                {
                    Name = userDescription,
                    Password = password,
                    Active = true,
                    Admin = false
                }; 

                var client = new RestClient(AppSettingsAcessor.GetAPIUrl("octoprint"));
                
                var request = new RestRequest("/access/users", Method.Post);
                request.AddHeader("X-Api-Key", AppSettingsAcessor.GetGlobalAPIKey());
                request.AddBody(facadeUserInfo, ContentType.Json);

                var response = client.Execute(request);

                if (!response.IsSuccessful)
                    throw new HttpRequestException($"Falha ao se comunicar com API Octoprint. StatusCode: {response.StatusCode} Content: {response.Content} ErrorMessage:{response.ErrorMessage}"); 

                return  response.IsSuccessful;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao criar usuário na API Octoprint: {ex.Message}."); 
            }
        }
    }
}