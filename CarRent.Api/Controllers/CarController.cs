namespace CarRent.Api.Controllers;

[Route("api/car")]
public class CarController : ApiController
{
    public CarController(ISender mediator) : base(mediator) {}


    [HttpGet("random")]
    public async Task<IActionResult> RandomCars() 
    {
        return await Handle<RandomCarsQuery, CarsResponse>(new RandomCarsQuery());
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetCar(Guid id) 
    {
        return await Handle<GetCarQuery, CarResponse>(new GetCarQuery(id));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCars() 
    {
        return await Handle<GetCarsQuery, CarsResponse>(new GetCarsQuery());
    }
}
