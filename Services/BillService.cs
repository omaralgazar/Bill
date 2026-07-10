using Blazored.LocalStorage;
using Bill.Models;

namespace Bill.Services
{
    public class BillService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "bills";

        public BillService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<BillItem>> GetAllBillsAsync()
        {
            var bills = await _localStorage.GetItemAsync<List<BillItem>>(StorageKey);
            return bills ?? new List<BillItem>();
        }

        public async Task<BillItem?> GetBillByIdAsync(Guid id)
        {
            var all = await GetAllBillsAsync();
            return all.FirstOrDefault(b => b.Id == id);
        }

        public async Task<List<BillItem>> GetBillsByCategoryAsync(BillCategory category)
        {
            var allBills = await GetAllBillsAsync();
            return allBills
                .Where(b => b.Category == category)
                .OrderByDescending(b => b.Date)
                .ToList();
        }

        public async Task AddBillAsync(BillItem newBill)
        {
            var allBills = await GetAllBillsAsync();
            allBills.Add(newBill);
            await _localStorage.SetItemAsync(StorageKey, allBills);
        }

        public async Task UpdateBillAsync(BillItem updatedBill)
        {
            var allBills = await GetAllBillsAsync();
            var index = allBills.FindIndex(b => b.Id == updatedBill.Id);
            if (index != -1)
            {
                allBills[index] = updatedBill;
                await _localStorage.SetItemAsync(StorageKey, allBills);
            }
        }

        public async Task DeleteBillAsync(Guid billId)
        {
            var allBills = await GetAllBillsAsync();
            allBills.RemoveAll(b => b.Id == billId);
            await _localStorage.SetItemAsync(StorageKey, allBills);
        }

        public async Task<DashboardStats> GetDashboardStatsAsync(BillCategory category)
        {
            var bills = await GetBillsByCategoryAsync(category);
            var now = DateTime.Now;

            var thisMonthCount = bills.Count(b => b.Date.Month == now.Month && b.Date.Year == now.Year);
            var lastBill = bills.FirstOrDefault();

            return new DashboardStats
            {
                ChargesThisMonth = thisMonthCount,
                LastChargeDate = lastBill?.Date,
                LastChargeAmount = lastBill?.Amount
            };
        }
    }
}
