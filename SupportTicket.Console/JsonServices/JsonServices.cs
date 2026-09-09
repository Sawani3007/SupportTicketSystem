using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportTicket.Core.JsonServices
{
    public class JsonServices : IJsonServices
    {
        private readonly HttpClient _client;
        public JsonServices(HttpClient client) { 
            _client=client;
        }
        public async Task<List<PersonSummary>> GetPeopleAsync()
        {
            var url = "https://swapi.py4e.com/api/people/";
            try
            {
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<PersonResponse>(json);
                if (data == null)
                {
                    throw new Exception("Unable to read the Api Response");
                }
                var summary = data!.Results
                    .Take(5)
                    .Select(p => new PersonSummary
                    {
                        Name = p.Name,
                        Height = p.Height
                    })
                    .ToList();
                return summary;
            }
            catch(Exception ex)
            {
                var error = new ErrorSummary
                {
                    Success = false,
                    Error = ex.Message
                };
                var errorJson = JsonSerializer.Serialize(error, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                await File.WriteAllTextAsync("people-error.json", errorJson);
                return new List<PersonSummary>();
            }
        }
        public async Task WriteSummaryAsync(List<PersonSummary> summary)
        {
            var json = JsonSerializer.Serialize(summary,new JsonSerializerOptions
            {
                WriteIndented = true
            });
            await File.WriteAllTextAsync("people-summary.json", json);
        }


    }
}
