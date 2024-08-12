namespace Backend.src.Shared
{
    public class GetAllOptions
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
        public string Search { get; set; } = string.Empty;
    }
}