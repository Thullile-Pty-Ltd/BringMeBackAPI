using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Repository.Reports.Item
{
    public interface IClothingAndAccessoriesRepository
    {
        Task<ClothingAndAccessories> CreateClothingAndAccessoriesReport(ClothingAndAccessories report);
        Task<ClothingAndAccessories> UpdateClothingAndAccessoriesReport(ClothingAndAccessories report);
        Task<IEnumerable<ClothingAndAccessories>> GetAllClothingAndAccessoriesReportsAsync();
        Task<ClothingAndAccessories> GetClothingAndAccessoriesReportByIdAsync(int reportId);
    }

}
