using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Helpers.CacheData
{
    public static class StockCacheService
    {
        /// <summary>
        /// Cache_Control cədvəlindən IsCurrent false dönərsə keşləmə prosesi işləyəcək. Yox əgər true dönərsə keşə alınmış datanı gətirəcək.
        /// </summary>
        public static async Task<bool> IsCacheCurrentAsync(string cacheName)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT IsCurrent FROM Cache_Control WHERE CacheName = @cacheName", con))
            {
                cmd.Parameters.AddWithValue("@cacheName", cacheName);
                await con.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();
                return result != null && result != DBNull.Value && !(bool)result;
            }
        }

        /// <summary>
        /// Keşi yeniləyir və cədvələ ən son datanı yazır
        /// </summary>
        public static async Task RefreshStockCacheAsync()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("gaime_Satis_mal_load_to_cache", con))
            {
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Əgər hər hansısa bir əməliyyat olmayıbsa yəni qalıq olduğu kimi qalıb isə stoku keşdən gətirəcək
        /// </summary>
        public static DataTable GetCachedStockAsync()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM GAIME_SATIS_MAL_LOAD_CACHE", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static async Task<DataTable> LoadStockAsync()
        {
            bool isCacheValid = await IsCacheCurrentAsync("STOCK");

            if (!isCacheValid)
            {
                await RefreshStockCacheAsync(); // SQL proseduru işə düşür. (Keşləyir)
            }

            return GetCachedStockAsync();
        }
    }
}
