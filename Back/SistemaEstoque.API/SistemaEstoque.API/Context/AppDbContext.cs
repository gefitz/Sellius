using SistemaEstoque.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CidadeModel> Cidades { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<EstadoModel> Estados { get; set; }
        public DbSet<FornecedoresModel> Fornecedores { get; set; }
        public DbSet<LicencaModel> Licencas { get; set; }
        public DbSet<LoginModel> Logins { get; set; }
        public DbSet<LogModel> Logs { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<PedidoXProduto> PedidoXProdutos { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<TipoProdutoModel> TpProdutos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relações de tabela
            #region PedidoxProduto
            modelBuilder.Entity<PedidoXProduto>()
    .HasOne(p => p.Pedido)
    .WithMany(p => p.Produto)
    .HasForeignKey(p => p.idPedido);
            modelBuilder.Entity<PedidoXProduto>();
            modelBuilder.Entity<PedidoXProduto>()
                .HasOne(p => p.Produto)
                .WithMany(p => p.pedidos)
                .HasForeignKey(p => p.idProduto);
            #endregion

            #region Pedido
            modelBuilder.Entity<PedidoModel>().HasOne(c => c.Cliente);
            modelBuilder.Entity<PedidoModel>().HasOne(u => u.Usuario);
            #endregion

            #region Usuario
            modelBuilder.Entity<UsuarioModel>().HasOne(c => c.Cidade);
            modelBuilder.Entity<UsuarioModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(u => u.EmpresaId);
            #endregion

            #region Produto
            modelBuilder.Entity<ProdutoModel>().HasOne(tp => tp.tipoProduto);
            modelBuilder.Entity<ProdutoModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(p => p.EmpresaId);
            modelBuilder.Entity<ProdutoModel>().HasOne(f => f.Fornecedor);
            #endregion

            #region Cliente
            modelBuilder.Entity<ClienteModel>().HasOne(c => c.Cidade);
            modelBuilder.Entity<ClienteModel>().HasMany(p => p.Pedidos);
            modelBuilder.Entity<ClienteModel>().HasOne(p => p.Empresa).WithMany().HasForeignKey(c => c.EmpresaId);
            #endregion

            #region Login
            modelBuilder.Entity<LoginModel>()
                .HasOne(l => l.Usuario)
                .WithMany() // <- não define navegação reversa
                .HasForeignKey(l => l.usuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            modelBuilder.Entity<CidadeModel>()
                .HasOne(c => c.Estado).WithMany(e => e.Cidade)
                .HasForeignKey(e => e.id);

            #endregion
        }
    }
}
