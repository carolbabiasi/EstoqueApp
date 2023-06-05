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
    public class ProdutosTest
    {
        private string? _endpoint;
        private Faker? _faker;
        private EstoquesTest _estoquesTest;

        public ProdutosTest()
        {
            _endpoint = "api/Produtos";
            _faker = new Faker("pt_BR");
            _estoquesTest = new EstoquesTest();
        }

        [Fact]
        public async Task<ProdutoQuery> Test_Post_Produtos_Returns_Created()
        {
            var estoque = await _estoquesTest.Test_Post_Estoques_Returns_Created();

            var request = new ProdutoCreateCommand
            {
                Nome = _faker.Commerce.ProductName(),
                Preco = decimal.Parse(_faker.Commerce.Price(2)),
                Quantidade = 10,
                EstoqueId = estoque.Id
            };

            var client = TestHelper.CreateClient;

            var result = await client.PostAsync(_endpoint, TestHelper.CreateContent(request));
            var response = TestHelper.ReadResponse<ProdutoQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            response.Id.Should().NotBeNull();
            response.Nome.Should().Be(request.Nome);
            response.Preco.Should().Be(request.Preco);
            response.Quantidade.Should().Be(request.Quantidade);
            response.EstoqueId.Should().Be(request.EstoqueId);

            return response;
        }

        [Fact]
        public async Task Test_Put_Produtos_Returns_Ok()
        {
            var produto = await Test_Post_Produtos_Returns_Created();
            var estoque = await _estoquesTest.Test_Post_Estoques_Returns_Created();

            var request = new ProdutoUpdateCommand
            {
                Id = produto.Id.Value,
                Nome = _faker.Commerce.ProductName(),
                Preco = decimal.Parse(_faker.Commerce.Price(2)),
                Quantidade = 10,
                EstoqueId = estoque.Id.Value
            };

            var client = TestHelper.CreateClient;

            var result = await client.PutAsync(_endpoint, TestHelper.CreateContent(request));
            var response = TestHelper.ReadResponse<ProdutoQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Id.Should().NotBeNull();
            response.Nome.Should().Be(request.Nome);
            response.Preco.Should().Be(request.Preco);
            response.Quantidade.Should().Be(request.Quantidade);
            response.EstoqueId.Should().Be(request.EstoqueId);
        }

        [Fact]
        public async Task Test_Delete_Produtos_Returns_Ok()
        {
            var produto = await Test_Post_Produtos_Returns_Created();

            var client = TestHelper.CreateClient;

            var result = await client.DeleteAsync(_endpoint + "/" + produto.Id);
            var response = TestHelper.ReadResponse<ProdutoQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Id.Should().NotBeNull();
            response.Nome.Should().Be(produto.Nome);
            response.Preco.Should().Be(produto.Preco);
            response.Quantidade.Should().Be(produto.Quantidade);
            response.EstoqueId.Should().Be(produto.EstoqueId);
        }

        [Fact]
        public async Task Test_GetAll_Produtos_Returns_Ok()
        {
            var produto = await Test_Post_Produtos_Returns_Created();

            var client = TestHelper.CreateClient;

            var result = await client.GetAsync(_endpoint);
            var response = TestHelper.ReadResponse<List<ProdutoQuery>>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var produtoCadastrado = response.FirstOrDefault(p => p.Id == produto.Id);

            produtoCadastrado.Id.Should().NotBeNull();
            produtoCadastrado.Nome.Should().Be(produto.Nome);
            produtoCadastrado.Preco.Should().Be(produto.Preco);
            produtoCadastrado.Quantidade.Should().Be(produto.Quantidade);
            produtoCadastrado.EstoqueId.Should().Be(produto.EstoqueId);
        }

        [Fact]
        public async Task Test_GetById_Produtos_Returns_Ok()
        {
            var produto = await Test_Post_Produtos_Returns_Created();

            var client = TestHelper.CreateClient;

            var result = await client.GetAsync(_endpoint + "/" + produto.Id);
            var response = TestHelper.ReadResponse<ProdutoQuery>(result);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Id.Should().NotBeNull();
            response.Nome.Should().Be(produto.Nome);
            response.Preco.Should().Be(produto.Preco);
            response.Quantidade.Should().Be(produto.Quantidade);
            response.EstoqueId.Should().Be(produto.EstoqueId);
        }
    }
}



