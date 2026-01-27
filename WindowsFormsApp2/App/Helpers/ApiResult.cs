namespace WindowsFormsApp2.App.Helpers
{
    public class ApiResult
    {
        public bool Ok { get; private set; }
        public int? Inserted { get; private set; }
        public string Error { get; private set; }

        public static ApiResult Success(int inserted)
        {
            return new ApiResult
            {
                Ok = true,
                Inserted = inserted
            };
        }

        public static ApiResult Fail(string error)
        {
            return new ApiResult
            {
                Ok = false,
                Error = error
            };
        }
    }
}
