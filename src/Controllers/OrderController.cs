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
        protected readonly IOrderService _service;
        protected readonly IAuthorizationService _authorization;
        public OrderController(IOrderService service, IAuthorizationService authorization)
        {
            _service = service;
            _authorization = authorization;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderReadDto>> CreateOneAsync([FromBody] OrderCreateDto orderCreateDto)
        {

            var authenticatedClaims = HttpContext.User;
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userGuid = new Guid(userId);

            return await _service.CreateOneAsync(userGuid, orderCreateDto);
        }
    }
}