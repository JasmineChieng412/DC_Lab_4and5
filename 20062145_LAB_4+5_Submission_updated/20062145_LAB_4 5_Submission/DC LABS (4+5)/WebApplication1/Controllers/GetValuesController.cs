//using Microsoft.AspNetCore.Mvc;

//namespace lab6.Controllers
//{
//        [Route("api/[controller]")]
//        [ApiController]
//        public class GetValuesController : ControllerBase
//        {
//            // Mock data for demonstration
//            private List<DataIntermed> data = new List<DataIntermed>
//        {
//            new DataIntermed { bal = 1000, acct = 12345, pin = 6789, fname = "John", lname = "Doe" },
//            new DataIntermed { bal = 2000, acct = 54321, pin = 9876, fname = "Jane", lname = "Smith" }
//        };

//            // GET: api/GetValues/{id}
//            [HttpGet("{id}")]
//            public ActionResult<DataIntermed> Get(int id)
//            {
//                // Retrieve data based on the provided id (0-based index)
//                if (id >= 0 && id < data.Count)
//                {
//                    return data[id];
//                }
//                else
//                {
//                    return NotFound(); // Return 404 Not Found for invalid id
//                }
//            }
//        }
//    }

