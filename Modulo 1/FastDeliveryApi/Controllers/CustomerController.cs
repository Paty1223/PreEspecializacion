namespace FastDeliveryApi.Controllers;
using FastDeliveryApi.Entity;
using FastDeliveryApi.Exceptions;
using FastDeliveryApi.Models;
using FastDeliveryApi.Repositories.Interfaces;

using Mapster;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/customers")]
public class CustomersControllers : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    

    public CustomersControllers(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        var customers = await _customerRepository.GetAll();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = request.Adapt<Customer>();

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        var response = customer.Adapt<CustomerResponse>();

        return CreatedAtAction(
            nameof(GetCustomerById),
            new { id = response.Id },
            response
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request.Id != id)
        {
            throw new BadRequestException("Body Id is not equal than Url Id");
        }

        var customer = await _customerRepository.GetCustomerById(id);
        if (customer is null)
        {
            throw new NotFoundException("Customer", id);
        }

        customer.ChangeName(request.Name);
        customer.ChangePhoneNumber(request.PhoneNumber);
        customer.ChangeEmail(request.Email);
        customer.ChangeAddress(request.Address);
        customer.ChangeStatus(request.Status);

        _customerRepository.Update(customer);

        await _unitOfWork.SaveChangeAsync();

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerById(id, cancellationToken);
        if (customer is null)
        {
            throw new NotFoundException("Customer", id);
        }

        var response = customer.Adapt<CustomerResponse>();

        return Ok(response);
    }




/*
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

    [HttpPost("Customer/Update")]
    public ActionResult Update(int id = 0)
    {
        if(id > 0)
        {
            var customers = _context.Customers.Find(id);
            _context.Customers.Update(customers);
            _context.SaveChanges();
        }
        return RedirectToAction("Index", "Customers");
    }


    [HttpPost("Customer/Delete")]
    public ActionResult Delete(int id = 0)
    {
        if(id > 0)
        {
            var customers = _context.Customers.Find(id);
            _context.Customers.Remove(customers);
            _context.SaveChanges();
        }
        return RedirectToAction("Index", "Customers");
    }
*/
}