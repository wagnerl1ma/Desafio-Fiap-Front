using CadastrosFiap.APP.DTOs;
using CadastrosFiap.APP.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CadastrosFiap.APP.Services
{
    public static class ApiAlunoService
    {
        public static string Addres { get; set; } = "https://localhost:44352/";
        public static HttpClient _httpClient { get; set; } = new HttpClient();

        public static async Task<string> GetToken(string endPoint = "api/v1/autenticacao")
        {
            try
            {
                //var cliente = new HttpClient();
                var myJsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres + endPoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                return responseBody;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
        public static async Task<AlunoDTO> GetAlunoById(int? id, string endPoint = "api/v1/alunos")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres + endPoint + $"/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                var aluno = JsonConvert.DeserializeObject<AlunoDTO>(responseBody);

                if (aluno == null)
                    return null;

                return aluno;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return null;
            }
        }

        public static async Task<bool> RemoveById(int? id, string endPoint = "api/v1/alunos")
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

        public static async Task<IEnumerable<AlunoDTO>> GetAllAlunos(string endPoint = "api/v1/alunos")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres + endPoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                var lista = JsonConvert.DeserializeObject<List<AlunoDTO>>(responseBody);

                if (lista == null)
                    return new List<AlunoDTO>();

                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return new List<AlunoDTO>();
            }
        }

        public static async Task<AlunoDTO> UpdateAluno(AlunoViewModel alunoViewModel, int id, string endPoint = "api/v1/alunos")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.PutAsJsonAsync(Addres + endPoint + $"/{id}", alunoViewModel);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                var aluno = JsonConvert.DeserializeObject<AlunoDTO>(responseBody);

                if (aluno == null)
                    return null;

                return aluno;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Ocorreu um erro!");
                Console.WriteLine("\n Erro: {0}", e.Message);
                return null;
            }
        }


        public static async Task<AlunoDTO> CreateAluno(AlunoViewModel alunoViewModel, string endPoint = "api/v1/alunos")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.PostAsJsonAsync(Addres + endPoint, alunoViewModel);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                    responseBody = JObject.Parse(responseBody).Property("data").Value.ToString();

                var aluno = JsonConvert.DeserializeObject<AlunoDTO>(responseBody);

                if (aluno == null)
                    return null;

                return aluno;
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
