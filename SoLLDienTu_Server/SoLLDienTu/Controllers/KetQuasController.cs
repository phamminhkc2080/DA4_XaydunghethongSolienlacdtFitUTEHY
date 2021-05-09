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
    [Route("api/KetQua")]
    [ApiController]
    public class KetQuasController : ControllerBase
    {
        private SoLLDienTuContext db;
        public KetQuasController(IConfiguration configuration)
        {
            db = new SoLLDienTuContext(configuration);
        }

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetKQ()
        {
            try
            {
                var result = db.KetQuas.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("getSV-by-maLop/{id}")]
        [HttpGet]
        public IActionResult GetByMaLop(string id)
        {
            try
            {
                var result = from sv in db.SinhViens
                             where sv.MaLop == id
                             select new
                             {
                                 sv.MaSv,
                                 sv.TenSv,
                                 sv.NgaySinh,
                                 sv.GioiTinh,
                                 sv.ThuongTru,
                                 sv.TamTru,
                                 sv.Sdt,
                                 sv.Anh
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("get-All-Lop")]
        [HttpGet]
        public IActionResult getAllLop()
        {
            try
            {
                var result = from l in db.Lops
                             select new
                             {
                                 l.MaLop,
                                 l.TenLop,
                                 l.MaGv
                             };
                return Ok(result);
            }catch(Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("get-SV-ById/{id}")]
        [HttpGet]
        public IActionResult getSvById(string id)
        {
            try
            {
                var result = (from sv in db.SinhViens
                             where sv.MaSv==id
                             select new
                             {
                                 sv.MaSv,
                                 sv.TenSv,
                                 sv.NgaySinh,
                                 sv.GioiTinh,
                                 sv.ThuongTru,
                                 sv.TamTru,
                                 sv.Sdt,
                                 sv.Anh,
                                 sv.MaLop
                             }).SingleOrDefault();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("getMH-by-KyHoc/{KyHoc}")]
        [HttpGet]
        public IActionResult getMhByKyHoc(int Kyhoc)
        {
            try
            {
                var result = from mh in db.MonHocs
                             where mh.Kyhoc == Kyhoc
                             select new
                             {
                                 mh.MaMh,
                                 mh.TenMh,
                                 mh.SoTc,
                                 mh.Kyhoc
                             };
                return Ok(result);
            }catch(Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("get-KetQua-ById")]
        [HttpPost]
        public IActionResult getTTSVKetQua([FromBody]Dictionary<string, object> formData)
        {
            string MaSV = "";
            string MaMH = "";
            if(formData.Keys.Contains("maSv") && !string.IsNullOrEmpty(Convert.ToString(formData["maSv"])))
            {
                MaSV = Convert.ToString(formData["maSv"]);
            }
            if (formData.Keys.Contains("maMh") && !string.IsNullOrEmpty(Convert.ToString(formData["maMh"])))
            {
                MaMH = Convert.ToString(formData["maMh"]);
            }
            try
            {
                var result = (from kq in db.KetQuas
                             where kq.MaSv == MaSV && kq.MaMh == MaMH
                             select new
                             {
                                 kq.MaSv,
                                 kq.MaMh,
                                 kq.DiemLd,
                                 kq.DiemTl
                             }).SingleOrDefault();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("confirm-ketqua")]
        [HttpPost]
        public IActionResult ConfirmKetQua([FromBody]KetQua model)
        {
            var obj = db.KetQuas.Where(s => s.MaSv == model.MaSv && s.MaMh==model.MaMh).SingleOrDefault();
            if (obj != null)
            {
                obj.MaSv = model.MaSv;
                obj.MaMh = model.MaMh;
                obj.DiemLd = model.DiemLd;
                obj.DiemTl = model.DiemTl;

                db.SaveChanges();
                return Ok(new { data = "Ok" });
            }
            else
            {
                db.KetQuas.Add(model);
                db.SaveChanges();
                return Ok(new { data = "OK" });
            }
        }

        [Route("create-ketqua")]
        [HttpPost]
        public IActionResult CreateKetQua([FromBody] KetQua model)
        {
            db.KetQuas.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        
        [Route("update-ketqua")]
        [HttpPut]
        public IActionResult UpdateKetQua([FromBody] KetQua model)
        {
            var obj = db.KetQuas.Where(s => s.MaSv == model.MaSv).SingleOrDefault();
            if (obj != null)
            {
                obj.MaSv = model.MaSv;
                obj.MaMh = model.MaMh;
                obj.DiemLd = model.DiemLd;
                obj.DiemTl = model.DiemTl;

                db.SaveChanges();
            }
            return Ok(new { data = "Ok" });
        }
        
        [Route("delete-ketqua")]
        [HttpDelete]
        public IActionResult DeleteKetQua([FromBody] Dictionary<string, object> formData)
        {
            string MaSV = "";
            if (formData.Keys.Contains("MaSv") && !string.IsNullOrEmpty(Convert.ToString(formData["MaSv"])))
            {
                MaSV = Convert.ToString(formData["MaSv"]);
            }
            var obj = db.SinhViens.Where(s => s.MaSv == MaSV).SingleOrDefault();
            db.SinhViens.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "Ok" });
        }

        //Api Client----------------------------------------------------------------------------------------------

        [Route("get-KetQua-ById/{maSv}/{kyHoc}")]
        [HttpGet]
        public IActionResult getKetQuaById(string maSv, int kyHoc)
        {
            try
            {
                var result = from kq in db.KetQuas
                             join mh in db.MonHocs on kq.MaMh equals mh.MaMh
                             where (kq.MaSv == maSv && mh.Kyhoc == kyHoc)
                             select new
                             {
                                 mh.MaMh,
                                 mh.TenMh,
                                 mh.SoTc,
                                 kq.DiemLd,
                                 kq.DiemTl
                             };
                return Ok(result);
            }catch(Exception ex)
            {
                return Ok("Err");
            }
        }

    }
}
