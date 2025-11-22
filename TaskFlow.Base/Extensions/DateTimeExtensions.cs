namespace TaskFlow.Base.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToReadable(this DateTime? date)
        {
            return date?.ToString("yyyy-MM-dd HH:mm") ?? "-";
        }

        public static string ToReadable(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm");
        }

    }
}
