//using Microsoft.AspNetCore.Mvc;

//namespace lab6.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SearchController : ControllerBase
//    {
//        // Mock data for demonstration
//        private List<API.DataIntermed> data = new List<API.DataIntermed>
//        {
//            new API.DataIntermed { bal = 1000, acct = 12345, pin = 6789, fname = "John", lname = "Doe" },
//            new API.DataIntermed { bal = 2000, acct = 54321, pin = 9876, fname = "Jane", lname = "Smith" }
//        };

//        // POST: api/Search
//        [HttpPost]
//        public ActionResult<API.DataIntermed> Post([FromBody] API.SearchData searchData)
//        {
//            // Perform a simple search based on the searchStr in the provided searchData
//            var result = data.FirstOrDefault(d => d.fname.Equals(searchData.searchStr, StringComparison.OrdinalIgnoreCase));

//            if (result != null)
//            {
//                return result;
//            }
//            else
//            {
//                return NotFound(); // Return 404 Not Found if no matching data found
//            }
//        }
//    }
//}
