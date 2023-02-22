using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Peluches.BackOffice.Presentacion.Models
{
    public partial class C3_BD_PEDIDOSContext : DbContext
    {
        public C3_BD_PEDIDOSContext()
        {
        }

        public C3_BD_PEDIDOSContext(DbContextOptions<C3_BD_PEDIDOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Comprobante> Comprobantes { get; set; }
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Modelo> Modelos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=C3_BD_PEDIDOS;User Id=sa;Password=!sql2019;");
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
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("Nro_Documento");

                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.Property(e => e.TipoCliente)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Tipo_Cliente");
            });

            modelBuilder.Entity<Comprobante>(entity =>
            {
                entity.HasKey(e => e.IdComprobante)
                    .HasName("PK__Comproba__7DE636001B6E0BA4");

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
                    .HasName("PK__Detalle___B3E0CED3938326C3");

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
                    .HasName("PK__Marca__9B8F8DB286A92FB8");

                entity.ToTable("Marca");

                entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");

                entity.Property(e => e.Marca1)
                    .HasMaxLength(50)
                    .HasColumnName("Marca");
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.HasKey(e => e.IdModelo)
                    .HasName("PK__Modelo__813C237231DB1FFD");

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
                    .HasName("PK__Proveedo__7D65272F9A9E816B");

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
                    .HasName("PK__Usuario__DE4431C5F3DB122C");

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
