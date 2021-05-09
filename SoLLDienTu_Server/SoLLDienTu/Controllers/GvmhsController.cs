using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoLLDienTu.Models;

namespace SoLLDienTu.Controllers
{
    [Route("api/GiaoVienMH")]
    public class GvmhsController : Controller
    {
        private readonly SoLLDienTuContext db;

        public GvmhsController(IConfiguration configuration)
        {
            db = new SoLLDienTuContext(configuration);
        }
        [Route("get-all")]
        [HttpGet]
        public IActionResult GetGVMH_MonHoc()
        {
            try
            {
                var result = from mh in db.MonHocs
                             select new
                             {
                                 mh.MaMh,
                                 mh.TenMh,
                                 mh.SoTc,
                                 mh.Kyhoc
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-count/{id}")]
        [HttpGet]
        public IActionResult GetCount(string id)
        {
            try
            {
                var count = db.Gvmhs.Where(s => s.MaGv == id).ToList().Count();
                return Ok(count);
            }catch(Exception ex)
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
                var result = db.Gvmhs.Where(s => s.MaGv == id).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("getMH-by-id/{id}")]//Get MH by id GiaoVien
        [HttpGet]
        public IActionResult GetMhById(string id)
        {
            try
            {
                var result = from gvmh in db.Gvmhs
                             join mh in db.MonHocs
                             on gvmh.MaMh equals mh.MaMh
                             where gvmh.MaGv==id
                             select new
                             {
                                 mh.MaMh,
                                 mh.TenMh,
                                 mh.SoTc,
                                 mh.Kyhoc
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("create-giaovienmh")]
        [HttpPost]
        public IActionResult CreateGiaoVien([FromBody] Gvmh model)
        {
            db.Gvmhs.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-giaovienmh")]
        [HttpPut]
        public IActionResult UpdateGiaoVienMH([FromBody] Gvmh model)
        {
            var obj = db.Gvmhs.Where(s => s.MaGv == model.MaGv).SingleOrDefault();
            if (obj != null)
            {
                obj.MaGv = model.MaGv;
                obj.MaMh = model.MaMh;
                db.SaveChanges();
            }
            return Ok(new { data = "Ok" });
        }

        [Route("delete-giaovienmh")]
        [HttpDelete]
        public IActionResult DeleteGiaoVienMH([FromBody] Dictionary<string, object> formData)
        {
            string MaGVMH = "";
            if (formData.Keys.Contains("maGv") && !string.IsNullOrEmpty(Convert.ToString(formData["maGv"])))
            {
                MaGVMH = Convert.ToString(formData["maGv"]);
            }
            var obj = db.Gvmhs.Where(s => s.MaGv == MaGVMH).SingleOrDefault();
            db.Gvmhs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "Ok" });
        }

        [Route("delete-giaovienmh2/{idgv}/{idmh}")]
        [HttpDelete]
        public IActionResult DeleteGVMH2(string idgv, string idmh)
        {
            var obj = db.Gvmhs.Where(s => s.MaMh == idmh && s.MaGv==idgv).SingleOrDefault();
            db.Gvmhs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "Ok" });
        }
    }
}
