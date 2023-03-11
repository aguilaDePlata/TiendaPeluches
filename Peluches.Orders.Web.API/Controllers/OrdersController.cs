using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peluches.Orders.Web.API.Application.Dtos;
using Peluches.Orders.Web.API.Application.Services;
using Peluches.Orders.Web.API.Base;
using Peluches.Orders.Web.API.Constants;
//using Peluches.Orders.Web.API.Extensions;
using System.Net;

namespace Peluches.Orders.Web.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private IValidator<OrderUpdateDto> _validatorOrderUpdate;
        private IValidator<OrderCreateDto> _validatorOrderCreate;

        public OrdersController(IValidator<OrderUpdateDto> validatorOrderUpdate,
                                IValidator<OrderCreateDto> validatorOrderCreate,
                                IOrderService orderService)
        {
            _validatorOrderUpdate = validatorOrderUpdate;
            _validatorOrderCreate = validatorOrderCreate;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(ServiceResult<List<OrdersListDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var ordersList = await _orderService.GetOrdersList();

            return ordersList.Code == ResponseStates.OK ?
                    (IActionResult)Ok(ordersList) :
                    (IActionResult)NotFound(ordersList);
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ServiceResult<OrdersListDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.Get(id);

            return order.Code == ResponseStates.OK ?
                    (IActionResult)Ok(order) :
                    (IActionResult)NotFound(order);
        }

        [Route("update/{id}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, OrderUpdateDto orderDto)
        {
            ValidationResult result = await _validatorOrderUpdate.ValidateAsync(orderDto);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return BadRequest(new ServiceResult<OrderUpdatedDto>((int)HttpStatusCode.BadRequest,
                                        "No se puede actualizar el pedido.", default!, validationDetail: this.ModelState));
            }

            if (orderDto == null)
                return BadRequest("Información insuficiente para actualizar el pedido.");

            if (id != orderDto.OrderId)
                return BadRequest("Número de Pedido incorrecto.");

            var updatedOrder = await _orderService.Update(id, orderDto);

            if (updatedOrder.Code == ResponseStates.OK)
                updatedOrder.Details = LocationGetOrderById(id);

            return updatedOrder.Code == ResponseStates.OK ?
                 (IActionResult)Ok(updatedOrder) :
                 updatedOrder.Code == (int)HttpStatusCode.NotFound ?
                 (IActionResult)NotFound(updatedOrder) :
                 (IActionResult)BadRequest(updatedOrder);
        }


        [Route("add")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(OrderCreateDto orderDto)
        {
            ValidationResult result = await _validatorOrderCreate.ValidateAsync(orderDto);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return BadRequest(new ServiceResult<OrderCreatedDto>((int)HttpStatusCode.BadRequest,
                                        "No se puede crear el pedido.", default!, validationDetail: this.ModelState));
            }

            if (orderDto == null)
                return BadRequest("Información insuficiente para crear el pedido.");


            var orderCreated = await _orderService.Create(orderDto);

            if (orderCreated.Code == (int)HttpStatusCode.Created)
                orderCreated.Details = LocationGetOrderById(orderCreated.Data.OrderId);

            return orderCreated.Code == (int)HttpStatusCode.Created ?
                 (IActionResult)Ok(orderCreated) :
                 orderCreated.Code == (int)HttpStatusCode.NotFound ?
                 (IActionResult)NotFound(orderCreated) :
                 orderCreated.Code == (int)HttpStatusCode.BadRequest ?
                 (IActionResult)BadRequest(orderCreated) :
                 (IActionResult)StatusCode((int)HttpStatusCode.InternalServerError, orderCreated);
        }



        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDelete = await _orderService.Delete(id);

            return orderDelete.Code == ResponseStates.OK ?
                 (IActionResult)Ok(orderDelete) :
                 orderDelete.Code == (int)HttpStatusCode.NotFound ?
                 (IActionResult)NotFound(orderDelete) :
                 (IActionResult)StatusCode((int)HttpStatusCode.InternalServerError, orderDelete);
        }

        private string LocationGetOrderById(int orderId)
        {
            return $"Location: {Request.Scheme}://{Request.Host}/api/orders/get/{orderId}";
        }
    }
}
