namespace HuaSect_AMS_DBTC.Models
{

    public class QrCodeViewModel
    {
        public string PayloadText { get; set; } = "https://huasect-ams-dbtc.onrender.com/api/Attendance/mark";
        public string QrCodeImageBase64 { get; set; }
    }
}