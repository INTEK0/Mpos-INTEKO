namespace WindowsFormsApp2.Helpers.DB
{
    public static class DTOs
    {
        public class TeraziDTO
        {
            public int Id { get; set; }
            public string ModelName { get; set; }
            public string FilePath { get; set; }
            public string IpAddress { get; set; }
            public int UserId { get; set; }
        }
    }
}
