using Azure.Core;
using Bogus;
using EstoqueApp.Application.Models.Commands;
using EstoqueApp.Application.Models.Queries;
using EstoqueApp.Tests.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EstoqueApp.Tests
{
    public class EstoquesTest
    {
        private string? _endpoint;
        private Faker? _faker;

        public EstoquesTest()
        {
            _endpoint = "api/estoques";
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task<EstoqueQuery> Test_Post_Estoques_Returns_Created()
        {
            var request = new EstoqueCreateCommand
            {
                Nome = _faker.Commerce.ProductName(),
                Descricao = _faker.Commerce.ProductDescription()
            };

            var client = TestHelper.CreateClient;

            var result = await client.PostAsync(_endpoint, TestHelper.CreateContent(request));
            var response = TestHelper.ReadResponse<EstoqueQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            response.Id.Should().NotBeNull();
            response.Nome.Should().Be(request.Nome);
            response.Descricao.Should().Be(request.Descricao);
            response.DataHoraCriacao.Should().NotBeNull();

            return response;
        }

        [Fact]
        public async Task Test_Put_Estoques_Returns_Ok()
        {
            var estoque = await Test_Post_Estoques_Returns_Created();

            var request = new EstoqueUpdateCommand
            {
                Id = estoque.Id,
                Nome = _faker.Commerce.ProductName(),
                Descricao = _faker.Commerce.ProductDescription()
            };

            var client = TestHelper.CreateClient;

            var result = await client.PutAsync(_endpoint, TestHelper.CreateContent(request));
            var response = TestHelper.ReadResponse<EstoqueQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Id.Should().Be(request.Id);
            response.Nome.Should().Be(request.Nome);
            response.Descricao.Should().Be(request.Descricao);
        }

        [Fact]
        public async Task Test_Delete_Estoques_Returns_Ok()
        {
            var estoque = await Test_Post_Estoques_Returns_Created();

            var client = TestHelper.CreateClient;

            var result = await client.DeleteAsync(_endpoint + "/" + estoque.Id);
            var response = TestHelper.ReadResponse<EstoqueQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Id.Should().Be(estoque.Id);
            response.Nome.Should().Be(estoque.Nome);
            response.Descricao.Should().Be(estoque.Descricao);
        }

        [Fact]
        public async Task Test_GetAll_Estoques_Returns_Ok()
        {
            var estoque = await Test_Post_Estoques_Returns_Created();

            var client = TestHelper.CreateClient;

            var result = await client.GetAsync(_endpoint);
            var response = TestHelper.ReadResponse<List<EstoqueQuery>>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            response.FirstOrDefault(c => c.Id == estoque.Id)
                .Should().NotBeNull();
        }

        [Fact]
        public async Task Test_GetById_Estoques_Returns_Ok()
        {
            var estoque = await Test_Post_Estoques_Returns_Created();

            var client = TestHelper.CreateClient;

            var result = await client.GetAsync(_endpoint + "/" + estoque.Id);
            var response = TestHelper.ReadResponse<EstoqueQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Id.Should().Be(estoque.Id);
            response.Nome.Should().Be(estoque.Nome);
            response.Descricao.Should().Be(estoque.Descricao);
        }
    }
}



