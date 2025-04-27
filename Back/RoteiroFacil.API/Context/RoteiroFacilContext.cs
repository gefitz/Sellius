using Microsoft.EntityFrameworkCore;
using RoteiroFacil.API.Models;
using System;

namespace RoteiroFacil.API.Context
{
    public class RoteiroFacilContext : DbContext
    {
        public RoteiroFacilContext(DbContextOptions<RoteiroFacilContext> options) : base(options) { }
        #region DbSet
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<LicencaModel> Licencas { get; set; }
        public DbSet<RepresetanteModel> Represetantes { get; set; }
        public DbSet<RoteiroModel> Roteiros { get; set; }
        public DbSet<CidadeModel> Cidades { get; set; }
        public DbSet<EstadoModel> Estados { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<PedidosModel> Pedidos { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<PedidoProdutoModel> PedidoProdutos { get; set; }

        public DbSet<LogModel> Logs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region PedioProduto
            modelBuilder.Entity<PedidoProdutoModel>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(p => p.PedidoId);
            modelBuilder.Entity<PedidoProdutoModel>()
                .HasOne(p => p.Produto)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(p => p.ProdutoId);
            #endregion

            #region Cliente
            modelBuilder.Entity<ClienteModel>()
                .HasMany(p => p.Pedidos)
                .WithOne(p=> p.Cliente)
                .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<ClienteModel>()
                .HasOne(c => c.Cidade)
                .WithMany()
                .HasForeignKey(c => c.CidadeId);

            modelBuilder.Entity<ClienteModel>()
                .HasOne(r => r.Represetante)
                .WithMany(c => c.Clientes)
                .HasForeignKey(c => c.RepresetanteId);
            #endregion

            #region Roteiro
            modelBuilder.Entity<RoteiroModel>()
                .HasMany(c => c.Cliente)
                .WithOne(r => r.Roteiro)
                .HasForeignKey(c => c.RoteiroId)
                .IsRequired(false);

            modelBuilder.Entity<RoteiroModel>()
                .HasOne(c => c.Cidade)
                .WithMany()
                .HasForeignKey(c => c.CidadeId); 
            modelBuilder.Entity<RoteiroModel>()
                .HasOne(r => r.Represetante)
                .WithMany()
                .HasForeignKey(c => c.RepresetanteId);
            #endregion

            #region Usuario
            modelBuilder.Entity<UsuarioModel>()
                .HasOne(l => l.Licenca)
                .WithOne(u => u.Usuario)
                .HasForeignKey<LicencaModel>(us => us.UsuarioId) ;
            modelBuilder.Entity<UsuarioModel>()
                .HasOne(u => u.Represetante)
                .WithOne(r => r.Usuario)
                .HasForeignKey<RepresetanteModel>(u => u.UsuarioId);
            #endregion
        }
    }
}
