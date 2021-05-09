using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoLLDienTu.Models;

namespace SoLLDienTu.Controllers
{
    [Route("api/Lop")]
    [ApiController]
    public class LopsController : ControllerBase
    {
        private readonly SoLLDienTuContext db;

        public LopsController(IConfiguration configuration)
        {
            db = new SoLLDienTuContext(configuration);
        }
        [Route("get-all")]
        [HttpGet]
        public IActionResult GetLop()
        {
            try
            {
                var result = from lop in db.Lops
                             select new
                             {
                                 lop.MaLop,
                                 lop.TenLop,
                                 lop.MaGv
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-all2")]
        [HttpPost]
        public IActionResult GetLop2([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from lop in db.Lops
                             select new
                             {
                                 lop.MaLop,
                                 lop.TenLop,
                                 lop.MaGv

                             };
                long total = result.Count();
                var result2 = result.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                return Ok(
                    new KQ
                    {
                        page = page,
                        total = total,
                        pageSize = pageSize,
                        data = result2
                    }
                  );
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(string id)
        {
            try
            {
                var result = db.Lops.Where(s => s.MaLop == id).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-by-name/{name}")]
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            try
            {
                var result = db.Lops.Where(s => s.TenLop.Contains(name)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-all-idnameGv")]
        [HttpGet]
        public IActionResult GetIdGV()
        {
            try
            {
                var result = (from gv in db.GiaoViens select new { gv.MaGv, gv.TenGv }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-all-tenGV")]
        [HttpGet]
        public IActionResult GetTenGV()
        {
            try
            {
                var result = (from gv in db.GiaoViens select gv.TenGv).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }


        [Route("create-lop")]
        [HttpPost]
        public IActionResult CreateLop([FromBody] Lop model)
        {
            try
            {
                //IEnumerable<String> result = from lop in db.Lops where lop.MaGv == model.MaGv select lop.MaGv;
                //foreach (String ma in result)
                //{
                //    if (ma != model.MaGv)
                //    {
                //        return Ok("err");
                //    }
                //}
                //model.MaLop = Guid.NewGuid().ToString();

                db.Lops.Add(model);
                db.SaveChanges();
                return Ok(new { data = "OK" });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("update-lop/{id}")]
        [HttpPut]
        public IActionResult UpdateLop([FromBody] Lop model, string id)
        {
            var obj = db.Lops.Where(s => s.MaLop == id).SingleOrDefault();
            if (obj != null)
            {
                obj.TenLop = model.TenLop;
                obj.MaGv = model.MaGv;

                db.SaveChanges();
            }
            return Ok(new { data = "Ok" });
        }
        [Route("delete-lop/{id}")]
        [HttpDelete]
        public IActionResult DeleteLop(string id)
        {

            var obj = db.Lops.Where(s => s.MaLop == id).SingleOrDefault();
            if (obj != null)
            {
                db.Lops.Remove(obj);
                db.SaveChanges();
                return Ok(new { data = "Ok" });
            }
            else return Ok(new { data = "null" });
        }
        [Route("search")]
        [HttpPost]
        public IActionResult SearchLop([FromBody] Dictionary<String, Object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var Ten = "";

                if (formData.Keys.Contains("ten") && !string.IsNullOrEmpty(Convert.ToString(formData["ten"])))
                { Ten = Convert.ToString(formData["ten"]); }

                //var result1 = db.GiaoViens.Where(s => s.TenGv.Contains(TenGv)).ToList();

                var result1 = from lop in db.Lops
                              where (lop.TenLop.Contains(Ten))
                              select new
                              {
                                  lop.MaLop,
                                  lop.TenLop,
                                  lop.MaGv

                              };

                long total = result1.Count();
                var result2 = result1.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                return Ok(
                    new KQ
                    {
                        page = page,
                        total = total,
                        pageSize = pageSize,
                        data = result2
                    }
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
