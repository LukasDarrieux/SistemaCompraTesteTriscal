﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.Produto;
using SistemaCompra.Infra.Data.SolicitacaoCompra;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data
{
    public class SistemaCompraContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public SistemaCompraContext(DbContextOptions options) : base(options) { }
        public DbSet<ProdutoAgg.Produto> Produtos { get; set; }
        public DbSet<SolicitacaoAgg.SolicitacaoCompra> SolicitacaoCompras { get; set; }
        public DbSet<SolicitacaoAgg.Item> Itens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<ProdutoAgg.Produto>()
            //    .HasData(
            //        new ProdutoAgg.Produto("Produto01", "Descricao01", "Madeira", 100)
            //    );

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<NomeFornecedor>();
            modelBuilder.Ignore<UsuarioSolicitante>();

            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new SolicitacaoCompraConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server=localhost\SQLEXPRESS;Database=SistemaCompraDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(connectionString);
                
        }
    }
}
