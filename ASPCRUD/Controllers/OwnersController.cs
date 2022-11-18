using ASPCRUD.Data;
using ASPCRUD.Models;
using ASPCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCRUD.Controllers
{
    public class OwnersController : Controller
    {
        private readonly MVCDbContext mVCDbContext;

        public OwnersController(MVCDbContext mVCDbContext)
        {
            this.mVCDbContext = mVCDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var owners = await mVCDbContext.Owners.ToListAsync();
            return View(owners);
        }

        [HttpGet]
        public IActionResult AddOwner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOwner(AddOwnerViewModel addOwnerRequest)
        {
            var owner = new Owner()
            {
                OwnerId = Guid.NewGuid(),
                FirstName = addOwnerRequest.FirstName,
                LastName = addOwnerRequest.LastName,
                DriverLicense = addOwnerRequest.DriverLicense
            };
            await mVCDbContext.Owners.AddAsync(owner);
            await mVCDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var owner = await mVCDbContext.Owners.FirstOrDefaultAsync(x=>x.OwnerId == id);

            if(owner != null) { 
                var viewModel = new UpdateOwnerViewModel()
                {
                    OwnerId = owner.OwnerId,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    DriverLicense = owner.DriverLicense
                };
                return await Task.Run(()=>View("View",viewModel));
            }
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateOwnerViewModel model)
        {
            var owner = await mVCDbContext.Owners.FindAsync(model.OwnerId);

            if (owner != null) { 
            
                owner.FirstName= model.FirstName;
                owner.LastName= model.LastName;
                owner.DriverLicense= model.DriverLicense;

                await mVCDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateOwnerViewModel model)
        {
            var owner = await mVCDbContext.Owners.FindAsync(model.OwnerId);

            if(owner != null) {
                mVCDbContext.Owners.Remove(owner);
                await mVCDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
