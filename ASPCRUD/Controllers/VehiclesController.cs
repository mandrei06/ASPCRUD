using ASPCRUD.Data;
using ASPCRUD.Models;
using ASPCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ASPCRUD.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly MVCDbContext mVCDbContext;

        public VehiclesController(MVCDbContext mVCDbContext)
        {
            this.mVCDbContext = mVCDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicles = await mVCDbContext.Vehicles.ToListAsync();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult AddVehicle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle(AddVehicleViewModel addVehicleRequest)
        {
            var vehicle = new Vehicle()
            {
                VehicleId = Guid.NewGuid(),
                Brand = addVehicleRequest.Brand,
                Vin = addVehicleRequest.Vin,
                Color = addVehicleRequest.Color,
                Year= addVehicleRequest.Year,
                Owner_Id=addVehicleRequest.Owner_Id
            };
            await mVCDbContext.Vehicles.AddAsync(vehicle);
            await mVCDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewVehicle(Guid id)
        {
            var vehicle = await mVCDbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleId == id);

            if (vehicle != null)
            {
                var viewVehicleModel = new UpdateVehicleViewModel()
                {
                    VehicleId = vehicle.VehicleId,
                    Brand = vehicle.Brand,
                    Vin = vehicle.Vin,
                    Color = vehicle.Color,
                    Year = vehicle.Year,
                    Owner_Id= vehicle.Owner_Id

                };
                return await Task.Run(() => View("ViewVehicle", viewVehicleModel));
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> ViewVehicle(UpdateVehicleViewModel model)
        {
            var vehicle = await mVCDbContext.Vehicles.FindAsync(model.VehicleId);

            if (vehicle != null)
            {

                vehicle.Brand = model.Brand;
                vehicle.Vin = model.Vin;
                vehicle.Color = model.Color;
                vehicle.Year= model.Year;
                vehicle.Owner_Id= model.Owner_Id;

                await mVCDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(UpdateVehicleViewModel model)
        {
            var vehicle = await mVCDbContext.Vehicles.FindAsync(model.VehicleId);

            if (vehicle != null)
            {
                mVCDbContext.Vehicles.Remove(vehicle);
                await mVCDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
