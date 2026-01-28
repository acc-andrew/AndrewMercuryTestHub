using AndrewMercuryTestHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AndrewMercuryTestHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class myofficeacpdController : ControllerBase
    {
        private const string connString = "Server=DESKTOP-5BC4O5I;Database=BackendExamHub;Trusted_Connection=true;TrustServerCertificate=true";
        // GET: api/<myofficeacpdController>
        [HttpGet]
        public async Task<ActionResult<List<myoffice>>> Get()
        {
            var resultBuilder = new StringBuilder();

            // 建議將連線字串檢查放在外面或使用 DI
            if (string.IsNullOrEmpty(connString)) return StatusCode(500, "Connection string is missing.");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_getall_myoffice", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await conn.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // 讀取第一欄 (SQL FOR JSON 的結果)
                                resultBuilder.Append(reader.GetValue(0).ToString());
                            }
                        }
                    }
                }

                string finalJson = resultBuilder.ToString();

                if (string.IsNullOrEmpty(finalJson))
                {
                    return Ok(new List<myoffice>()); 
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<myoffice> result = JsonSerializer.Deserialize<List<myoffice>>(finalJson, options);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/<myofficeacpdController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<myofficeacpdController>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] myoffice model)
        {

            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            string jsonForSql = JsonSerializer.Serialize(model, options);

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_add_one_myoffice", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@acpd_sid", SqlDbType.NVarChar) { Value = model.acpd_sid });
                        cmd.Parameters.Add(new SqlParameter("@acpd_cname", SqlDbType.NVarChar) { Value = model.acpd_cname });
                        cmd.Parameters.Add(new SqlParameter("@acpd_ename", SqlDbType.NVarChar) { Value = model.acpd_ename });

                        cmd.Parameters.Add(new SqlParameter("@acpd_sname", SqlDbType.NVarChar) { Value = model.acpd_sname });
                        cmd.Parameters.Add(new SqlParameter("@acpd_email", SqlDbType.NVarChar) { Value = model.acpd_email });
                        cmd.Parameters.Add(new SqlParameter("@acpd_status", SqlDbType.NVarChar) { Value = model.acpd_status });

                        cmd.Parameters.Add(new SqlParameter("@acpd_stop", SqlDbType.NVarChar) { Value = model.acpd_stop });
                        cmd.Parameters.Add(new SqlParameter("@acpd_stopMemo", SqlDbType.NVarChar) { Value = model.acpd_stopMemo });
                        cmd.Parameters.Add(new SqlParameter("@acpd_LoginID", SqlDbType.NVarChar) { Value = model.acpd_LoginID });

                        cmd.Parameters.Add(new SqlParameter("@loginpw", SqlDbType.NVarChar) { Value = model.acpd_LoginPW });
                        cmd.Parameters.Add(new SqlParameter("@acpd_memo", SqlDbType.NVarChar) { Value = model.acpd_memo });
                        cmd.Parameters.Add(new SqlParameter("@acpd_nowdatetime", SqlDbType.DateTime) { Value = model.acpd_nowdatetime });

                        cmd.Parameters.Add(new SqlParameter("@appd_nowid", SqlDbType.NVarChar) { Value = model.appd_nowid });
                        cmd.Parameters.Add(new SqlParameter("@acpd_upddatetitme", SqlDbType.DateTime) { Value = model.acpd_upddatetitme });
                        cmd.Parameters.Add(new SqlParameter("@acpd_updid", SqlDbType.NVarChar) { Value = model.acpd_updid });

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return Ok("insert OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"SQL Error: {ex.Message}");
            }
        }

        // PUT api/<myofficeacpdController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<myofficeacpdController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
