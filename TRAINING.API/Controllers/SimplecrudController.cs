using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

[ApiController]
[Route("api/[controller]")]
public class SimplecrudController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new List<SimpleCrudItem>());
    }
}

internal class SimpleCrudItem
{
}