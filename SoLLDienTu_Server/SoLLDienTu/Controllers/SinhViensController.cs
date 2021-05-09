using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoLLDienTu.Models;

namespace SoLLDienTu.Controllers
{
    [Route("api/SinhVien")]
    public class SinhViensController : Controller
    {
        public static IWebHostEnvironment _webHostEnvironment;
        private SoLLDienTuContext db;

        public SinhViensController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            db = new SoLLDienTuContext(configuration);
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("get-all")]
        [HttpGet]
        public IActionResult GetSV()
        {
            try
            {
                var result = from sv in db.SinhViens
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
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-allMaLopSV")]
        [HttpGet]
        public IActionResult GetidLopSV()
        {
            try
            {
                var result = (from lop in db.Lops
                              select lop.MaLop).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-all2")]
        [HttpPost]
        public IActionResult GetSV2([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from sv in db.SinhViens
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
                var result = (from sv in db.SinhViens
                              where (sv.MaSv == id)
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
                var result = db.SinhViens.Where(s => s.TenSv.Contains(name)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        
        [Route("create-sinhvien")]
        [HttpPost]
        public IActionResult CreateSinhVien([FromBody] SinhVien model)
        {
            try
            {
                db.SinhViens.Add(model);
                db.SaveChanges();
                return Ok(new { data = "OK" });
            }
            catch(Exception ex)
            {
                return Ok("Err");
            }
        }
        
        [Route("update-sinhvien/{id}")]
        [HttpPut]
        public IActionResult UpdateSinhVien([FromBody] SinhVien model, string id)
        {
            var obj = db.SinhViens.Where(s => s.MaSv == id).SingleOrDefault();
            if (obj != null)
            {
                obj.TenSv = model.TenSv;
                obj.NgaySinh = model.NgaySinh;
                obj.GioiTinh = model.GioiTinh;
                obj.ThuongTru = model.ThuongTru;
                obj.TamTru = model.TamTru;
                obj.Sdt = model.Sdt;
                obj.Anh = model.Anh;
                obj.MaLop = model.MaLop;
                db.SaveChanges();
            }
            return Ok(new { data = "Ok" });
        }
        
        [Route("delete-sinhvien/{id}")]
        [HttpDelete]
        public IActionResult DeleteSinhVien(string id)
        {

            var obj = db.SinhViens.Where(s => s.MaSv == id).SingleOrDefault();
            db.SinhViens.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "Ok" });
        }
        [Route("delete2-sinhvien/{id}")]
        [HttpDelete]
        public IActionResult DeleteSinhVien2(string id)
        {
            try
            {
                var obj = db.SinhViens.Where(s => s.MaSv == id).SingleOrDefault();
                db.SinhViens.Remove(obj);
                db.SaveChanges();
                return Ok(new { data = "Ok" });
            }
            catch(Exception ex)
            {
                return Ok(new { data = "Err" });
            }
        }
        
        [Route("search")]
        [HttpPost]
        public IActionResult SearchSV([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var Ten = "";

                if (formData.Keys.Contains("ten") && !string.IsNullOrEmpty(Convert.ToString(formData["ten"])))
                { Ten = Convert.ToString(formData["ten"]); }

                //var result1 = db.GiaoViens.Where(s => s.TenGv.Contains(TenGv)).ToList();

                var result1 = from sv in db.SinhViens
                              where (sv.TenSv.Contains(Ten))
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
        
        [Route("UpImage")]
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload objectFile)
        {
            try
            {
                if (objectFile.files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\" + "\\sinhvien\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objectFile.files.FileName))
                    {
                        objectFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "Uploaded.";
                    }

                }
                else
                {
                    return "Not Uploaded";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [Route("GetImage/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\" + "\\sinhvien\\";
            var filePath = path + fileName + ".png";
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/png");
            }
            else
            {
                filePath = path + fileName + ".jpg";
                if (System.IO.File.Exists(filePath))
                {
                    byte[] b = System.IO.File.ReadAllBytes(filePath);
                    return File(b, "image/jpg");
                }
            }
            return null;
        }
        [Route("DeleteImage/{imgdel}")]
        [HttpDelete]
        public IActionResult Delete(string imgdel)
        {
            imgdel = Path.Combine(_webHostEnvironment.WebRootPath, "uploads\\sinhvien", imgdel);
            FileInfo fi = new FileInfo(imgdel);
            if (fi != null)
            {
                System.IO.File.Delete(imgdel);
                fi.Delete();
            }
            return Ok("Deleted");
        }
    }
}
