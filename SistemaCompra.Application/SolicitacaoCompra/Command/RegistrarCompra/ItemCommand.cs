using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaCompra.Domain.Core.Model;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ItemCommand
    {
        public ProdutoAgg.Produto Produto { get; set; }
        public int Qtde { get; set; }
        
    }
}
