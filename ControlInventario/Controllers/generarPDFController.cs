using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace ControlInventario.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class generarPDFController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetGenerarReporte()
        {

            return (new HttpResponseMessage(HttpStatusCode.OK));

        }

    }
}
