using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.src.DTO;
using Backend.src.Service;

namespace Backend.src.Controller
{
    public class OrderController : BaseController
    {
        protected readonly IOrderService _orderService;
        protected readonly IAuthorizationService _authorization;
        public OrderController(IOrderService orderService, IAuthorizationService authorization)
        {
            _orderService = orderService;
            _authorization = authorization;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderReadDto>> CreateOneAsync([FromBody] OrderCreateDto orderCreateDto)
        {

            // exact user information
            var authenticatedClaims = HttpContext.User;
            // claim has userId
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userGuid = new Guid(userId);
            return await _orderService.CreateOneAsync(userGuid, orderCreateDto);
        }


        [HttpGet("{id:guid}")]
        //[Authorize(Roles = "Admin")]

        public async Task<ActionResult<OrderReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var product = await _orderService.GetByIdAsync(id);
            return Ok(product);
        }
    }
}