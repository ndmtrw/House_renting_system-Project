using House_renting_system_Project.Data.Data;
using House_renting_system_Project.Data.Data.Entities;
using House_renting_system_Project.Models.House;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace House_renting_system_Project.Controllers
{
    public class HouseController : Controller
    {
        private readonly HouseRentingDbContext context;

        public HouseController(HouseRentingDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllHouses()
        {
            var houses = await context.Houses
                .Select(h => new HousesViewModel
                {
                    Id = h.Id,
                    Name = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl
                })
                .ToListAsync();

            return View(houses);
        }

        [HttpGet]
        public async Task<IActionResult> HouseById(int id)
        {
            var house = await context.Houses
                .FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
            {
                return NotFound();
            }

            return View("Details", house);
        }

        [HttpGet]
        public async Task<IActionResult> CreateHouse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHouse(HouseFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var house = new House
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth
            };

            await context.Houses.AddAsync(house);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(AllHouses));
        }
    }
}