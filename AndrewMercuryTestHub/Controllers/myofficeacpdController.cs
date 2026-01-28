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
        // Task<ActionResult<IEnumerable<ItemTwoM>>> IEnumerable<string> 
        public async Task<ActionResult<List<myoffice>>>? Get()
        {
            var resultBuilder = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_getall_myoffice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // to read every column for each JSON row
                        while (reader.Read())
                        {
                            resultBuilder.Append(reader.GetValue(0).ToString());
                        }
                    }
                }
            }

            string finalJson = resultBuilder.ToString();

            // 如果 JSON 為空，回傳空清單；否則反序列化
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<myoffice> result =  string.IsNullOrEmpty(finalJson)
                ? new List<myoffice>()
                : JsonSerializer.Deserialize<List<myoffice>>(finalJson, options);

            return Ok(result); // 必須包裝在 Ok() 內以符合 ActionResult 要求的格式
        }// public IEnumerable<string> Get()

        // GET api/<myofficeacpdController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<myofficeacpdController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
