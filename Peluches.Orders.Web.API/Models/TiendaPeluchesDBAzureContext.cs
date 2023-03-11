using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Peluches.Orders.Web.API.Models
{
    public partial class TiendaPeluchesDBAzureContext : DbContext
    {
        public TiendaPeluchesDBAzureContext()
        {
        }

        public TiendaPeluchesDBAzureContext(DbContextOptions<TiendaPeluchesDBAzureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargo> Cargos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Comprobante> Comprobantes { get; set; } = null!;
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Modelo> Modelos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:famiru-server-database.database.windows.net,1433;Initial Catalog=TiendaPeluchesDBAzure;Persist Security Info=False;User ID=userfamirusql;Password=Famiru123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(e => e.IdCargo)
                    .HasName("_copy_2");

                entity.ToTable("Cargo");

                entity.Property(e => e.IdCargo).HasColumnName("ID_Cargo");

                entity.Property(e => e.Cargo1)
                    .HasMaxLength(50)
                    .HasColumnName("Cargo");

                entity.Property(e => e.Sueldo).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("_copy_5");

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(255);

                entity.Property(e => e.Distrito).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NroDocumento)
                    .HasMaxLength(11)
                    .HasColumnName("Nro_Documento");

                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.Property(e => e.TipoCliente)
                    .HasMaxLength(20)
                    .HasColumnName("Tipo_Cliente");
            });

            modelBuilder.Entity<Comprobante>(entity =>
            {
                entity.HasKey(e => e.IdComprobante)
                    .HasName("PK__Comproba__7DE63600267D2C11");

                entity.ToTable("Comprobante");

                entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");

                entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FechaEmision)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Emision");

                entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");

                entity.Property(e => e.Igv).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SubtotalComp)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Subtotal_Comp");

                entity.Property(e => e.TipoComprobante)
                    .HasMaxLength(20)
                    .HasColumnName("Tipo_Comprobante");

                entity.Property(e => e.ValorTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Valor_Total");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Comprobantes)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_Comprobante_Pedido");
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(e => e.IdDetalle)
                    .HasName("PK__Detalle___B3E0CED35927BB4F");

                entity.ToTable("Detalle_Pedido");

                entity.Property(e => e.IdDetalle).HasColumnName("ID_Detalle");

                entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Precio_Venta");

                entity.Property(e => e.SubtotalProd)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Subtotal_Prod");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_DetalleP_Pedido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_DetalleP_Producto");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("_copy_1");

                entity.ToTable("Empleado");

                entity.Property(e => e.IdEmpleado).HasColumnName("ID_Empleado");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(255);

                entity.Property(e => e.Distrito).HasMaxLength(50);

                entity.Property(e => e.Dni).HasColumnName("DNI");

                entity.Property(e => e.FechaContrato)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Contrato");

                entity.Property(e => e.IdCargo).HasColumnName("ID_Cargo");

                entity.Property(e => e.Nombres).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.HasOne(d => d.IdCargoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdCargo)
                    .HasConstraintName("FK_Empleado_Cargo");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PK__Marca__9B8F8DB28DAB53D9");

                entity.ToTable("Marca");

                entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");

                entity.Property(e => e.Marca1)
                    .HasMaxLength(50)
                    .HasColumnName("Marca");
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.HasKey(e => e.IdModelo)
                    .HasName("PK__Modelo__813C23724600E90D");

                entity.ToTable("Modelo");

                entity.Property(e => e.IdModelo).HasColumnName("ID_Modelo");

                entity.Property(e => e.Modelo1)
                    .HasMaxLength(50)
                    .HasColumnName("Modelo");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("_copy_3");

                entity.ToTable("Pedido");

                entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");

                entity.Property(e => e.FechaMaxEntrega)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_MaxEntrega");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");

                entity.Property(e => e.IdEmpleado).HasColumnName("ID_Empleado");

                entity.Property(e => e.ValorTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Valor_Total");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Pedido_Cliente");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_Pedido_Empleado");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("_copy_4");

                entity.ToTable("Producto");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");

                entity.Property(e => e.IdModelo).HasColumnName("ID_Modelo");

                entity.Property(e => e.IdProveedor).HasColumnName("ID_Proveedor");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Precio_Venta");

                entity.Property(e => e.Url).IsUnicode(false);

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdMarca)
                    .HasConstraintName("FK_Producto_Marca");

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdModelo)
                    .HasConstraintName("FK_Producto_Modelo");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_Producto_Proveedor");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__7D65272F41F9B62C");

                entity.ToTable("Proveedor");

                entity.Property(e => e.IdProveedor).HasColumnName("ID_Proveedor");

                entity.Property(e => e.ApellidosProv)
                    .HasMaxLength(50)
                    .HasColumnName("Apellidos_Prov");

                entity.Property(e => e.Direccion).HasMaxLength(255);

                entity.Property(e => e.Distrito).HasMaxLength(50);

                entity.Property(e => e.NombresProv)
                    .HasMaxLength(50)
                    .HasColumnName("Nombres_Prov");

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__DE4431C5EE869799");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.IdEmpleado).HasColumnName("ID_Empleado");

                entity.Property(e => e.Password)
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .HasColumnName("Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
