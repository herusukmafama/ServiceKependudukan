using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using System.Xml;
using KependudukanAPI.Libs;

namespace KependudukanAPI.Controllers
{

    [Route("idcen/[controller]")]
    [ApiController]
    public class DataWargaController : Controller
    {
        private BaseController bc = new BaseController();
        private MessageController mc = new MessageController();
        private lConvert lc = new lConvert();
        /*public IActionResult Index()
        {
            return View();
        }*/

        [HttpGet("getList")]
        public JObject Index()
        {
            string spname = "public.get_list_data_warga";
            var retObject = new List<dynamic>();
            JObject data = new JObject();
            try
            {
                data = new JObject();
                retObject = bc.getDataToObject(spname);
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", lc.convertDynamicToJArray(retObject));
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }
    }
}