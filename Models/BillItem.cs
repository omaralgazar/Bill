namespace Bill.Models
{
    public enum BillCategory
    {
        Electricity,
        Water,
        Gas
    }

    public class BillItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public BillCategory Category { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }

        // كهرباء: عدد الكيلوهات - مياه: عدد الأمتار - غاز: قراءة العداد
        public double MeterReading { get; set; }

        // صورة الفاتورة كـ base64 data URL (بعد ضغطها)، null لو مفيش صورة
        public string? ImageBase64 { get; set; }
    }

    public class DashboardStats
    {
        public int ChargesThisMonth { get; set; }
        public DateTime? LastChargeDate { get; set; }
        public decimal? LastChargeAmount { get; set; }
    }
}
