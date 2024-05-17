/*
using Microsoft.EntityFrameworkCore;

namespace FurnitureShoppingCartMvcUi.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Colection>> Colections()
        {
            return await _db.Colections.ToListAsync();
        }
        public async Task<IEnumerable<Furniture>> GetFurniture(string sTerm = "", int colectionId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Furniture> furnitures = await (from furniture in _db.Furnitures
                                             join colection in _db.Colections
                                             on furniture.ColectionId equals colection.Id
                                             where string.IsNullOrWhiteSpace(sTerm) || (furniture != null && furniture.FurnitureName.ToLower().StartsWith(sTerm))
                                             select new Furniture
                                             {
                                                 Id = furniture.Id,
                                                 Image = furniture.Image,
                                                 FurnitureName = furniture.FurnitureName,
                                                 ColectionId = furniture.ColectionId,
                                                 Price = furniture.Price,
                                                 ColectionName = colection.ColectionName
                                             }
                         ).ToListAsync();
            if (colectionId > 0)
            {

                furnitures = furnitures.Where(a => a.ColectionId == colectionId).ToList();
            }
            return furnitures;

        }
    }
}
*/