using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.src.DTO;
using Backend.src.Service;
using Backend.src.Shared;

namespace Backend.src.Controller
{
    public class OrderDetailController : BaseController
    {
        protected readonly IOrderDetailService _orderDetailService;

        // constructor
        public OrderDetailController(IOrderDetailService service)
        {
            _orderDetailService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailReadDto>>> GetAllAsync([FromQuery] GetAllOptions getAllOptions)
        {

            var productList = await _orderDetailService.GetAllAsync(getAllOptions);
            return Ok(productList);
        }
    }
}