using CadastrosFiap.APP.DTOs;
using CadastrosFiap.APP.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CadastrosFiap.APP.Services
{
    public static class ApiTurmaService
    {
        public static string Addres { get; set; } = "https://localhost:44352/";
        public static HttpClient _httpClient { get; set; } = new HttpClient();

        public static async Task<string> GetToken(string endPoint = "api/v1/autenticacao")
        {
            try
            {
                var myJsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres + endPoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                return responseBody;
            }
            catch (Exception ex)
            {
                return "";
                //throw ex;
            }

        }
        public static async Task<TurmaDTO> GetTurmaById(int? id, string endPoint = "api/v1/turmas")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres + endPoint + $"/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                var turma = JsonConvert.DeserializeObject<TurmaDTO>(responseBody);

                if (turma == null)
                    return null;

                return turma;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return null;
            }
        }

        public static async Task<bool> RemoveById(int? id, string endPoint = "api/v1/turmas")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.DeleteAsync(Addres + endPoint + $"/{id}");
                var ok = response.EnsureSuccessStatusCode();

                return ok.StatusCode == System.Net.HttpStatusCode.OK ? true : false;

            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return false;
            }
        }

        public static async Task<IEnumerable<TurmaDTO>> GetAllTurmas(string endPoint = "api/v1/turmas")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres + endPoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var lista = JsonConvert.DeserializeObject<List<TurmaDTO>>(responseBody);

                if (lista == null)
                    return new List<TurmaDTO>();

                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return new List<TurmaDTO>();
            }
        }

        public static async Task<TurmaDTO> UpdateTurma(TurmaViewModel turmaViewModel, int id, string endPoint = "api/v1/turmas")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.PutAsJsonAsync(Addres + endPoint + $"/{id}", turmaViewModel);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                var turma = JsonConvert.DeserializeObject<TurmaDTO>(responseBody);

                if (turma == null)
                    return null;

                return turma;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return null;
            }
        }


        public static async Task<TurmaDTO> CreateTurma(TurmaViewModel turmaViewModel, string endPoint = "api/v1/turmas")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.PostAsJsonAsync(Addres + endPoint, turmaViewModel);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                    responseBody = JObject.Parse(responseBody).Property("data").Value.ToString();


                var turma = JsonConvert.DeserializeObject<TurmaDTO>(responseBody);

                if (turma == null)
                    return null;

                return turma;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return null;
            }
        }
    }
}
