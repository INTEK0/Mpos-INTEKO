using System;
using static WindowsFormsApp2.App.Helpers.Enums;

namespace WindowsFormsApp2.App.Helpers
{
    public static class ApiEndpointResolver
    {
        public static string Resolve(ApiOperation operation)
        {
            switch (operation)
            {
                case ApiOperation.Odenis:
                    return "odenis";
                case ApiOperation.Stock:
                    return "stock";
                case ApiOperation.Invoice:
                    return "mahsulalis";
                case ApiOperation.Return:
                    return "return";
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }
    }
}
