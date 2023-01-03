﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApp.ChainOfResponsibility.ChainOfResponsibility;
using WebApp.ChainOfResponsibility.Models;


namespace WebApp.ChainOfResponsibility.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppIdentityDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppIdentityDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    public IActionResult Index() => View();


    public async Task<IActionResult> SendEmail()
    {
        var products = await _context.Products.ToListAsync();

        var excelProcessHandler = new ExcelProcessHandler<Product>();

        var zipFileProcessHandler = new ZipFileProcessHandler<Product>();

        var sendEmailProcessHandler = new SendEmailProcessHandler("product.zip", "t-novruzov@outlook.com");

        excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(sendEmailProcessHandler);

        excelProcessHandler.Handle(products);

        return View(nameof(Index));
    }


    public IActionResult Privacy() => View();



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

}