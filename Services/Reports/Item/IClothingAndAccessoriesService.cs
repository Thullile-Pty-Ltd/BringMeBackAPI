using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Services.Reports.Item
{
    public interface IClothingAndAccessoriesService
    {
        Task<ClothingAndAccessories> CreateClothingAndAccessoriesReport(int userId, ClothingAndAccessories report);
        Task<ClothingAndAccessories> UpdateClothingAndAccessoriesReport(int userId, int reportId, ClothingAndAccessories report);
        Task<IEnumerable<ClothingAndAccessories>> GetAllClothingAndAccessoriesReportsAsync();
        Task<ClothingAndAccessories> GetClothingAndAccessoriesReportByIdAsync(int reportId);
    }

}
