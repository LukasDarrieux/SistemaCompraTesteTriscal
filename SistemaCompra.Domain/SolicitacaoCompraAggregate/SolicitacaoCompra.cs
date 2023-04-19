using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento => ObterCondicaoPagamento();
        
        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Itens = new List<Item>();
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (!itens.Any())
            {
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
            }
        }

        public CondicaoPagamento ObterCondicaoPagamento()
        {
            const int TRINTA_DIAS = 30;
            const decimal VALOR_MINIMO_CONDICAO_PAGAMENTO_30_DIAS = 50000;

            if (TotalGeral.Value > VALOR_MINIMO_CONDICAO_PAGAMENTO_30_DIAS)
                return new CondicaoPagamento(TRINTA_DIAS);
            else
                return new CondicaoPagamento(UInt16.MinValue);
        }

    }
}
