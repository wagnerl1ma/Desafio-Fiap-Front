using CadastrosFiap.APP.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
//using Newtonsoft.Json;
//using System.Net;
using System.Text.Json;

namespace CadastrosFiap.APP.Services
{
    public static class AlunoApiService
    {
        public static string Addres { get; set; } = "https://localhost:44352/";
        //public static string AddresToken { get; set; } = Addres + "api/v1/autenticacao";
        public static HttpClient _httpClient { get; set; } = new HttpClient();

        public static async Task<string> GetToken(string endPoint = "api/v1/autenticacao")
        {
            try
            {
                //var cliente = new HttpClient();
                var myJsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres + endPoint);
                //HttpResponseMessage? response = await cliente.GetAsync("https://localhost:44352/api/v1/autenticacao");
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

        public static async Task<IEnumerable<AlunoDTO>> GetAllAlunos(string endPoint = "api/v1/alunos")
        {
            try
            {
                var token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage? response = await _httpClient.GetAsync(Addres+endPoint);
                response.EnsureSuccessStatusCode(); 

                string responseBody = await response.Content.ReadAsStringAsync(); //ler o contéudo como string

                var lista = JsonConvert.DeserializeObject<List<AlunoDTO>>(responseBody);

                if(lista == null)
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
    }
}
