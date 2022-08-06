using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TRAINING.API.Data;

namespace TRAINING.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ORDSController : ControllerBase
    {
        private readonly IORDSRepository _repo;
        public ORDSController(IORDSRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("ktpa02/{*id}")]
        public IActionResult GetKTPA02(string id)
        {
            // DB2Connection con = new DB2Connection("DataSource=172.18.178.57");
            // con.Open();
            // DB2Command cmd = new DB2Command("SELECT * FROM AKTLBIT.KTPA02 WHERE DONOA02 = '0205/00001/DSDB'");
            // cmd.Connection = con;
            // DB2DataReader dr = cmd.ExecuteReader();
            // string str = "";
            // while(dr.Read())
            // {
            //     str = dr.GetString(2);
            // }
            // var state = con.c;
            // con.Close();
            // var data = await _repo.KTPA02Get(id);
            return Ok();
        }
    }
}