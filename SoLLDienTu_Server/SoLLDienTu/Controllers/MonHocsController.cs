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
    [Route("api/MonHoc")]
    [ApiController]
    public class MonHocsController : ControllerBase
    {
        private readonly SoLLDienTuContext db;
        public MonHocsController(IConfiguration configuration)
        {
            db = new SoLLDienTuContext(configuration);
        }

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetMH()
        {
            try
            {
                var result = db.MonHocs.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("get-all2")]
        [HttpPost]
        public IActionResult GetMH2([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from mh in db.MonHocs
                             select new
                             {
                                 mh.MaMh,
                                 mh.TenMh, 
                                 mh.SoTc,
                                 mh.Kyhoc
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
                var result = db.MonHocs.Where(s => s.MaMh == id).ToList();
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
                var result = db.MonHocs.Where(s => s.TenMh.Contains(name)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("create-monhoc")]
        [HttpPost]
        public IActionResult CreateMonHoc([FromBody] MonHoc model)
        {
            model.MaMh = Guid.NewGuid().ToString();
            db.MonHocs.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-monhoc")]
        [HttpPut]
        public IActionResult UpdateMonHoc([FromBody] MonHoc model)
        {
            var obj = db.MonHocs.Where(s => s.MaMh == model.MaMh).SingleOrDefault();
            if (obj != null)
            {
                obj.MaMh = model.MaMh;
                obj.TenMh = model.TenMh;
                obj.SoTc = model.SoTc;
                obj.Kyhoc = model.Kyhoc;
                db.SaveChanges();
            }
            return Ok(new { data = "Ok" });
        }
        [Route("delete-monhoc")]
        [HttpDelete]
        public IActionResult DeleteMonHoc([FromBody] Dictionary<string, object> formData)
        {
            string MaMH = "";
            if (formData.Keys.Contains("MaMh") && !string.IsNullOrEmpty(Convert.ToString(formData["MaMh"])))
            {
                MaMH = Convert.ToString(formData["MaMh"]);
            }
            var obj = db.MonHocs.Where(s => s.MaMh == MaMH).SingleOrDefault();
            db.MonHocs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "Ok" });
        }
        [Route("delete-monhoc2/{id}")]
        [HttpDelete]
        public IActionResult DeleteMonHoc(string id)
        {
            var obj = db.MonHocs.Where(s => s.MaMh == id).SingleOrDefault();
            db.MonHocs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "Ok" });
        }

        [Route("search")]
        [HttpPost]
        public IActionResult SearchMH([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var Ten = "";

                if (formData.Keys.Contains("ten") && !string.IsNullOrEmpty(Convert.ToString(formData["ten"])))
                { Ten = Convert.ToString(formData["ten"]); }

                //var result1 = db.GiaoViens.Where(s => s.TenGv.Contains(TenGv)).ToList();

                var result1 = from mh in db.MonHocs
                              where (mh.TenMh.Contains(Ten))
                              select new
                              {
                                  mh.MaMh,
                                  mh.TenMh,
                                  mh.SoTc,
                                  mh.Kyhoc
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

        //Client User------------------------------------------------------------------------------------------------------------------------

        // Lấy ra thông tin môn học theo kì học
        [Route("get-kimonhoc/{kihoc}")]
        [HttpGet]
        public IActionResult GetKiMonHoc(int kihoc)
        {
            try
            {
                var result = (from mh in db.MonHocs
                              where mh.Kyhoc == kihoc
                              select new
                              {
                                  mh.MaMh,
                                  mh.TenMh,
                                  mh.Kyhoc,
                                  mh.SoTc
                              }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        //lấy tổng số tín chỉ các môn
        [Route("get-tongtccacmon")]
        [HttpGet]
        public IActionResult GetTongTinChiMH()
        {
            try
            {
                var result = db.MonHocs.Sum(s => s.SoTc);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        //lấy số tín chỉ các môn đã học
        [Route("get-sotcmondahoc/{kihoc}/{masv}")]
        [HttpGet]
        public IActionResult GetSoTinChiMH(int kihoc, string masv)
        {
            try
            {
                int d = 0;

                var result = from mh in db.MonHocs
                             join kq in db.KetQuas on mh.MaMh equals kq.MaMh
                             where kq.MaSv == masv && mh.Kyhoc == kihoc
                             select mh.SoTc;
                foreach (var a in result)
                {
                    d = (int)(d + a);
                }
                return Ok(d);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
    }
}
