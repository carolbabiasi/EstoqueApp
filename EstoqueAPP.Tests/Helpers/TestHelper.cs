using Azure.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Tests.Helpers
{
    public static class TestHelper
    {

        /// <summary>
        /// Criando um objeto HTTP Client para acessar a API
        /// </summary>
        public static HttpClient CreateClient
        {
            get
            {
                var authHelper = new AuthHelper();
                var accessToken = authHelper.ObterTokenAcesso().Result;

                var client = new WebApplicationFactory<Program>().CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                return client;
            }
        }

        /// <summary>
        /// Serializar os dados de uma requisição para JSON
        /// </summary>
        public static StringContent CreateContent(object request)
            => new StringContent(JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");

        /// <summary>
        /// Deserializar uma resposta obtida da API
        /// </summary>
        public static TResponse ReadResponse<TResponse>(HttpResponseMessage message)
            => JsonConvert.DeserializeObject<TResponse>
                (message.Content.ReadAsStringAsync().Result);
    }
}



