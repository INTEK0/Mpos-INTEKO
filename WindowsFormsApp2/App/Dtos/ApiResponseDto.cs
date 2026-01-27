namespace WindowsFormsApp2.App.Dtos
{
    public class ApiResponseDto
    {
        public class ApiSuccessResponse
        {
            public bool ok { get; set; }
            public int inserted { get; set; }
        }

        public class ApiErrorResponse
        {
            public bool ok { get; set; }
            public string error { get; set; }
        }
    }
}