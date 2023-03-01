using Microsoft.AspNetCore.Mvc;

namespace AriesContador.MainAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {


        [HttpGet("someDummyText")]
        public IActionResult GetSomeTest()
        {
            var retorno = new 
            {
            information = "This is a dummy text"
            };

            return Ok(retorno);
        }


        [HttpPost("convert")]
        public IActionResult GetSomeTest([FromBody] ExternalString external)
        {
            var retorno = new
            {
                information = external.Name.ToUpper(),
            };

            return Ok(retorno);
        }

    }

    public class ExternalString 
    {
        public string Name { get; set; }
    }

}
