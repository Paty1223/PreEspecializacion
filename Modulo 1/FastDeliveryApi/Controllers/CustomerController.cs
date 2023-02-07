namespace FastDeliveryApi.Controllers;

using FastDeliveryApi.Data;
using FastDeliveryApi.Entity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/customers")]
public class CustomersControllers : ControllerBase
{
    private readonly FastDeliveryDbContext _context;

    public CustomersControllers(FastDeliveryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Customer>> Get()
    {
        var customers = _context.Customers.ToList();
        return Ok(customers);
    }

    [HttpGet("Customers/SearchId")]
    public ActionResult<Customer> Get(int id)
    {
        var customers = _context.Customers.Find(id);
        if (customers == null)
        {
            return NotFound();
        }
        return customers;
    }

    [HttpPost("Customer/Save")]
    public ActionResult Create(Customer customers)
    {
        _context.Customers.Add(customers);
        _context.SaveChanges();
        return RedirectToAction("Index", "Customers");
    }

}