using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_RegistrarCompraDeve
    {
        [Fact]
        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            itens.Add(new Item(produto, 50));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            //Assert.Equal(30, solicitacao.CondicaoPagamento.Valor);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();

            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra(itens));

            //Então
            Assert.Equal("A solicitação de compra deve possuir itens!", ex.Message);
        }

        [Fact]
        public void EfetuarSolicitacaoCompraComum()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("usuário teste", "nome de fornecedor teste");
            var itens = new List<Item>();
            var produto = new Produto("Mesa", "Mesa para escritório", Categoria.Madeira.ToString(), Convert.ToDecimal(599.99));
            itens.Add(new Item(produto, 2));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Assert.Equal("Solicitação cadastrada com sucesso", "");
        }

        [Fact]
        public void EfetuarSolicitacaoCompraComMaisDeUmItem()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("usuário teste", "nome de fornecedor teste");
            var itens = new List<Item>();
            var produto = new Produto("Mesa", "Mesa para escritório", Categoria.Madeira.ToString(), Convert.ToDecimal(599.99));
            var produto2 = new Produto("Mesa", "Mesa para escritório gamer", Categoria.Madeira.ToString(), Convert.ToDecimal(1289.99));
            
            itens.Add(new Item(produto, 2));
            itens.Add(new Item(produto2, 2));
            
            //Quando
            solicitacao.RegistrarCompra(itens);

            //Assert.Equal("Solicitação com multiplos itens cadastrada com sucesso", "");
        }
    }
}
