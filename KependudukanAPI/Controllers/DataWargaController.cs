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
        private BasetrxController bcx = new BasetrxController();
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

        // INSERT

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] JObject json)
        {
            var data = new JObject();
            int code = 200;
            try
            {
                //cek is exists product
/*                var retObject = new List<dynamic>();
                retObject = this.checkListData(json.GetValue("dw_nik").ToString());

                if (retObject.Count > 0)
                {
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", mc.GetMessage("output_field_exists"));
                }
                else
                {*/
                    string strout = "";
                    strout = bcx.InsertDataWarga(json);
                    if (strout == "success")
                    {
                        data.Add("status", mc.GetMessage("api_output_ok"));
                        data.Add("message", mc.GetMessage("save_success"));
                    }
                    else
                    {
                        data.Add("status", mc.GetMessage("api_output_not_ok"));
                        data.Add("message", strout);
                    }
/*                }*/
            }
            catch (Exception ex)
            {
                code = 500;
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return StatusCode(code, data);
        }

        // DELETE

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] JObject json)
        {
            var data = new JObject();
            int code = 200;
            try
            {
                string strout = "";
                strout = bcx.DeleteDataWarga(json);
                if (strout == "success")
                {
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage("save_success"));
                }
                else
                {
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", strout);
                }
            }
            catch (Exception ex)
            {
                code = 500;
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return StatusCode(code, data);
        }

        public List<dynamic> checkListData(string id)
        {
            string spname = "public.get_list_data_warga";
            string p1 = "dw_nik," + id + ",s";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject(spname, p1);
            return retObject;
        }
    }
}