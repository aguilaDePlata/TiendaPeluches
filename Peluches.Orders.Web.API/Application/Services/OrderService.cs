using Microsoft.EntityFrameworkCore;
using Peluches.Orders.Web.API.Application.Dtos;
using Peluches.Orders.Web.API.Base;
using Peluches.Orders.Web.API.Constants;
using Peluches.Orders.Web.API.Models;
using System.Net;

namespace Peluches.Orders.Web.API.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly TiendaPeluchesDBAzureContext _context;


        public OrderService(TiendaPeluchesDBAzureContext context)
        {
            this._context = context;
        }

        public async Task<ServiceResult<List<OrdersListDto>>> GetOrdersList()
        {
            var orders = new List<OrdersListDto>();
            var ordersQuery = await (from o in _context.Pedidos
                                     join c in _context.Clientes on o.IdCliente equals c.IdCliente
                                     join od in _context.DetallePedidos on o.IdPedido equals od.IdPedido
                                     join p in _context.Productos on od.IdProducto equals p.IdProducto
                                     select new
                                     {
                                         OrderId = o.IdPedido,
                                         IdentityDocumentClient = c.NroDocumento,
                                         Client = c.Nombre + " " + c.Apellidos,
                                         DateOrder = (DateTime)o.FechaPedido!,
                                         TotalAmount = (decimal)o.ValorTotal!,
                                         IsActive = (bool)o.Activo!,
                                         OrderDetailId = od.IdDetalle,
                                         ProductId = p.IdProducto,
                                         Product = p.NombreProducto!,
                                         Quantity = (int)od.Cantidad!,
                                         SalePrice = (decimal)od.PrecioVenta!,
                                         SubTotal = (decimal)od.SubtotalProd!,
                                     }).OrderBy(o => o.OrderId).ThenBy(od => od.OrderDetailId).ToListAsync();

            ordersQuery.ForEach(r =>
            {
                var order = orders.SingleOrDefault(f => f.OrderId == r.OrderId);
                if (order == null)
                {
                    order = new OrdersListDto
                    {
                        OrderId = r.OrderId,
                        IdentityDocumentClient = r.IdentityDocumentClient,
                        Client = r.Client,
                        DateOrder = r.DateOrder,
                        TotalAmount = r.TotalAmount,
                        IsActive = r.IsActive
                    };

                    orders.Add(order);
                }

                order.OrderDetailsList.Add(new OrderDetailsListDto
                {
                    OrderDetailId = r.OrderDetailId,
                    ProductId = r.ProductId,
                    Product = r.Product,
                    Quantity = r.Quantity,
                    SalePrice = r.SalePrice,
                    SubTotal = r.SubTotal
                });
            });

            return new ServiceResult<List<OrdersListDto>>(ResponseStates.OK,
                            "Consulta exitosa de los pedidos.", orders);
        }

        public async Task<ServiceResult<OrdersListDto>> Get(int orderId)
        {
            var orders = new List<OrdersListDto>();
            var orderQuery = await (from o in _context.Pedidos
                                    join c in _context.Clientes on o.IdCliente equals c.IdCliente
                                    join od in _context.DetallePedidos on o.IdPedido equals od.IdPedido
                                    join p in _context.Productos on od.IdProducto equals p.IdProducto
                                    where o.IdPedido == orderId
                                    select new
                                    {
                                        OrderId = o.IdPedido,
                                        IdentityDocumentClient = c.NroDocumento,
                                        Client = c.Nombre + " " + c.Apellidos,
                                        DateOrder = (DateTime)o.FechaPedido!,
                                        TotalAmount = (decimal)o.ValorTotal!,
                                        IsActive = (bool)o.Activo!,
                                        OrderDetailId = od.IdDetalle,
                                        ProductId = p.IdProducto,
                                        Product = p.NombreProducto!,
                                        Quantity = (int)od.Cantidad!,
                                        SalePrice = (decimal)od.PrecioVenta!,
                                        SubTotal = (decimal)od.SubtotalProd!,
                                    }).OrderBy(o => o.OrderId).ThenBy(od => od.OrderDetailId).ToListAsync();

            if (orderQuery != null)
            {
                orderQuery.ForEach(r =>
                {
                    var order = orders.SingleOrDefault(f => f.OrderId == r.OrderId);
                    if (order == null)
                    {
                        order = new OrdersListDto
                        {
                            OrderId = r.OrderId,
                            IdentityDocumentClient = r.IdentityDocumentClient,
                            Client = r.Client,
                            DateOrder = r.DateOrder,
                            TotalAmount = r.TotalAmount,
                            IsActive = r.IsActive
                        };

                        orders.Add(order);
                    }

                    order.OrderDetailsList.Add(new OrderDetailsListDto
                    {
                        OrderDetailId = r.OrderDetailId,
                        ProductId = r.ProductId,
                        Product = r.Product,
                        Quantity = r.Quantity,
                        SalePrice = r.SalePrice,
                        SubTotal = r.SubTotal
                    });
                });
            }

            return new ServiceResult<OrdersListDto>(ResponseStates.OK,
                            "Consulta exitosa del Pedido.", orders.SingleOrDefault()!);
        }


        public async Task<ServiceResult<OrderUpdatedDto>> Update(int orderId, OrderUpdateDto orderDto)
        {
            try
            {
                var orderToModify = await _context.Pedidos.FirstOrDefaultAsync(e => e.IdPedido == orderId)!;
                if (orderToModify == null)
                    throw new KeyNotFoundException("Pedido no existe.");

                MaterializeOrderFromOrderDto(orderToModify, orderDto);

                await UpdateOrder(orderToModify);

                return new ServiceResult<OrderUpdatedDto>(ResponseStates.OK,
                    "Actualización existosa del Pedido.", new OrderUpdatedDto
                    {
                        OrderId = orderId,
                        MaxDateDelivery = (DateTime)orderToModify.FechaMaxEntrega!,
                        TotalAmount = (decimal)orderToModify.ValorTotal!,
                        IsActive = (bool)orderToModify.Activo!
                    });
            }
            catch (KeyNotFoundException ex)
            {
                return new ServiceResult<OrderUpdatedDto>((int)HttpStatusCode.NotFound,
                                ex.Message, default!);
            }
            catch (Exception ex)
            {
                return new ServiceResult<OrderUpdatedDto>((int)HttpStatusCode.InternalServerError,
                                ex.Message, default!);
            }
        }

        public async Task<ServiceResult<OrderCreatedDto>> Create(OrderCreateDto orderDto)
        {
            try
            {
                var client = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == orderDto.ClientId);
                if (client == null)
                    throw new KeyNotFoundException("Cliente seleccionado no existe.");

                var employee = await _context.Empleados.FirstOrDefaultAsync(c => c.IdEmpleado == orderDto.EmployeeId);
                if (employee == null)
                    throw new KeyNotFoundException("Empleado seleccionado no existe.");

                Pedido order = await CreateOrderFromOrderDto(orderDto, client, employee);

                await CreateOrder(order);

                return new ServiceResult<OrderCreatedDto>((int)HttpStatusCode.Created,
                    "Creación existosa del Pedido.", new OrderCreatedDto
                    {
                        OrderId = order.IdPedido,
                        MaxDateDelivery = (DateTime)order.FechaMaxEntrega!,
                        TotalAmount = (decimal)order.ValorTotal!,
                        IsActive = (bool)order.Activo!
                    });
            }
            catch (KeyNotFoundException ex)
            {
                return new ServiceResult<OrderCreatedDto>((int)HttpStatusCode.NotFound,
                                ex.Message, default!);
            }
            catch (ArgumentNullException ex)
            {
                return new ServiceResult<OrderCreatedDto>((int)HttpStatusCode.BadRequest,
                                ex.Message, default!);
            }
            catch (Exception ex)
            {
                return new ServiceResult<OrderCreatedDto>((int)HttpStatusCode.InternalServerError,
                                ex.Message, default!);
            }
        }

        public async Task<ServiceResult<OrderDeletedDto>> Delete(int orderId)
        {
            try
            {
                var order = await _context.Pedidos.FirstOrDefaultAsync(c => c.IdPedido == orderId);
                if (order == null)
                    throw new KeyNotFoundException("Pedido seleccionado no existe.");

                order.Activo = false;

                await UpdateOrder(order);

                return new ServiceResult<OrderDeletedDto>(ResponseStates.OK,
                    "Eliminación exitosa del Pedido.", new OrderDeletedDto
                    {
                        OrderId = orderId,
                        MaxDateDelivery = (DateTime)order.FechaMaxEntrega!,
                        TotalAmount = (decimal)order.ValorTotal!,
                        IsActive = (bool)order.Activo!
                    });
            }
            catch (KeyNotFoundException ex)
            {
                return new ServiceResult<OrderDeletedDto>((int)HttpStatusCode.NotFound,
                                ex.Message, default!);
            }
            catch (Exception ex)
            {
                return new ServiceResult<OrderDeletedDto>((int)HttpStatusCode.InternalServerError,
                                ex.Message, default!);
            }
        }


        private async Task<Pedido> CreateOrderFromOrderDto(OrderCreateDto orderDto, Cliente client, Empleado employee)
        {
            Pedido orderNew = new Pedido()
            {
                IdCliente = orderDto.ClientId,
                IdEmpleado = orderDto.EmployeeId,
                FechaPedido = orderDto.DateOrder,
                FechaMaxEntrega = orderDto.MaxDateDelivery,
                Activo = true,
                IdClienteNavigation = client,
                IdEmpleadoNavigation = employee,
            };

            foreach (var od in orderDto.OrderDetails!)
            {
                var product = await _context.Productos.FirstOrDefaultAsync(f => f.IdProducto == od.ProductId);
                if (product == null)
                    throw new KeyNotFoundException("Producto seleccionado no existe");

                orderNew.DetallePedidos.Add(new DetallePedido()
                {
                    IdProducto = product.IdProducto,
                    Cantidad = od.Quantity,
                    PrecioVenta = product.PrecioVenta,
                    SubtotalProd = product.PrecioVenta * od.Quantity,
                    IdProductoNavigation = product
                });
            }


            orderNew.ValorTotal = orderNew.DetallePedidos.Sum(s => s.SubtotalProd);

            return orderNew;
        }

        private async Task CreateOrder(Pedido order)
        {
            _context.Pedidos.Add(order);

            await _context.SaveChangesAsync();
        }

        private void MaterializeOrderFromOrderDto(Pedido orderToModify, OrderUpdateDto orderDto)
        {
            orderToModify.FechaMaxEntrega = orderDto.MaxDateDelivery;
            orderToModify.Activo = (bool)orderDto.IsActive!;

            orderDto.OrderDetails!.ForEach(od =>
            {
                var orderDetailModified = _context.DetallePedidos.FirstOrDefault(f => f.IdPedido == orderToModify.IdPedido && f.IdDetalle == od.OrderDetailId);
                if (orderDetailModified != null)
                {
                    orderDetailModified.IdProducto = od.ProductId;
                    orderDetailModified.Cantidad = od.Quantity;
                    orderDetailModified.PrecioVenta = od.SalePrice;
                    orderDetailModified.SubtotalProd = od.Quantity * od.SalePrice;
                }
            });


            orderToModify.ValorTotal = _context.DetallePedidos.Where(w => w.IdPedido == orderToModify.IdPedido).Sum(s => s.SubtotalProd);
        }

        private async Task UpdateOrder(Pedido orderToModify)
        {
            _context.Entry(orderToModify).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
