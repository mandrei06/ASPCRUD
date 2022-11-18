using ASPCRUD.Data;
using ASPCRUD.Models;
using ASPCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCRUD.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly MVCDbContext mVCDbContext;

        public ClaimsController(MVCDbContext mVCDbContext)
        {
            this.mVCDbContext = mVCDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var claims = await mVCDbContext.Claims.ToListAsync();
            return View(claims);
        }

        [HttpGet]
        public IActionResult AddClaim()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClaim(AddClaimViewModel addClaimRequest)
        {
            var claim = new Claim ()
            {
                ClaimId = Guid.NewGuid(),
                Description = addClaimRequest.Description,
                Status = addClaimRequest.Status,
                Date = addClaimRequest.Date,
                Vehicle_Id = addClaimRequest.Vehicle_Id

            };
            await mVCDbContext.Claims.AddAsync(claim);
            await mVCDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewClaim(Guid id)
        {
            var claim = await mVCDbContext.Claims.FirstOrDefaultAsync(x => x.ClaimId == id);

            if (claim != null)
            {
                var viewClaimModel = new UpdateClaimViewModel()
                {
                    ClaimId = claim.ClaimId,
                    Description = claim.Description,
                    Status = claim.Status,
                    Date = claim.Date,
                    Vehicle_Id = claim.Vehicle_Id
                };
                return await Task.Run(() => View("ViewClaim", viewClaimModel));
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> ViewClaim(UpdateClaimViewModel model)
        {
            var claim = await mVCDbContext.Claims.FindAsync(model.ClaimId);

            if (claim != null)
            {


                claim.Description = model.Description;
                claim.Status = model.Status;
                claim.Date = model.Date;
                claim.Vehicle_Id = model.Vehicle_Id;

                await mVCDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteClaim(UpdateClaimViewModel model)
        {
            var claim = await mVCDbContext.Claims.FindAsync(model.ClaimId);

            if (claim != null)
            {
                mVCDbContext.Claims.Remove(claim);
                await mVCDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
