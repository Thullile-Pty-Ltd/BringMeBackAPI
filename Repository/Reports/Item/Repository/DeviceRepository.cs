using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Item.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;

        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Device> CreateDeviceReport(Device report)
        {
            _context.Devices.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Device> UpdateDeviceReport(Device report)
        {
            _context.Devices.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Device>> GetAllDeviceReportsAsync()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task<Device> GetDeviceReportByIdAsync(int reportId)
        {
            return await _context.Devices.FindAsync(reportId);
        }
    }

}
