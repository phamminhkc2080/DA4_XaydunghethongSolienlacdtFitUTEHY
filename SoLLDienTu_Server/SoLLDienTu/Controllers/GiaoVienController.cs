using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoLLDienTu.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoLLDienTu.Controllers
{
    //[Authorize]
    [Route("api/GiaoVien")]
    public class GiaoVienController : Controller
    {
        public static IWebHostEnvironment _webHostEnvironment;
        private SoLLDienTuContext db;
        public GiaoVienController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            db = new SoLLDienTuContext(configuration);
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("get-all")]
        [HttpGet]
        public IActionResult GetGV()
        {
            try
            {
                var result = from gv in db.GiaoViens
                             select new
                             {
                                 gv.MaGv,
                                 gv.TenGv,
                                 gv.NgaySinh,
                                 gv.GioiTinh,
                                 gv.QueQuan,
                                 gv.Anh
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
        public IActionResult GetGV2([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from gv in db.GiaoViens
                             select new
                             {
                                 gv.MaGv,
                                 gv.TenGv,
                                 gv.NgaySinh,
                                 gv.GioiTinh,
                                 gv.QueQuan,
                                 gv.Anh
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

        [Route("get-demo")]
        [HttpGet]
        public IActionResult GetGVdemo()
        {
            try
            {
                var result = from gv in db.GiaoViens
                             join lp in db.Lops on
                             gv.MaGv equals lp.MaGv
                             select new
                             {
                                 tengv = gv.TenGv,
                                 lop = lp.TenLop,
                                 dc = gv.QueQuan
                             };
                return Ok(result);
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
                var result = from gv in db.GiaoViens
                             where (gv.MaGv == id)
                             select new
                             {
                                 gv.MaGv,
                                 gv.TenGv,
                                 gv.NgaySinh,
                                 gv.GioiTinh,
                                 gv.QueQuan,
                                 gv.Anh
                             };
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
                var result = from gv in db.GiaoViens
                             where (gv.TenGv.Contains(name))
                             select new
                             {
                                 gv.MaGv,
                                 gv.TenGv,
                                 gv.NgaySinh,
                                 gv.GioiTinh,
                                 gv.QueQuan,
                                 gv.Anh
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("create-giaovien")]
        [HttpPost]
        public IActionResult CreateGiaoVien([FromBody] GiaoVien model)
        {
            model.MaGv = Guid.NewGuid().ToString();
            db.GiaoViens.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-giaovien/{id}")]
        [HttpPut]
        public IActionResult UpdateGiaoVien([FromBody] GiaoVien model, string id)
        {
            var obj = db.GiaoViens.Where(s => s.MaGv == id).SingleOrDefault();
            if (obj != null)
            {
                obj.TenGv = model.TenGv;
                obj.NgaySinh = model.NgaySinh;
                obj.GioiTinh = model.GioiTinh;
                obj.QueQuan = model.QueQuan;
                obj.Anh = model.Anh;
                db.SaveChanges();
            }
            return Ok(new { data = "Ok" });
        }
        [Route("delete-giaovien")]
        [HttpDelete]
        public IActionResult DeleteGiaoVien([FromBody] Dictionary<string, object> formData)
        {
            string MaGV = "";
            if (formData.Keys.Contains("MaGv") && !string.IsNullOrEmpty(Convert.ToString(formData["MaGv"])))
            {
                MaGV = Convert.ToString(formData["MaGv"]);
            }
            var obj = db.GiaoViens.Where(s => s.MaGv == MaGV).SingleOrDefault();
            db.GiaoViens.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "Ok" });
        }
        [Route("delete2-giaovien/{id}")]
        [HttpDelete]
        public IActionResult DeleteGiaoVien2(string id)
        {
            try
            {
                var obj = db.GiaoViens.Where(s => s.MaGv == id).SingleOrDefault();
                db.GiaoViens.Remove(obj);
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
        public IActionResult SearchGV([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var TenGv = "";

                if (formData.Keys.Contains("ten") && !string.IsNullOrEmpty(Convert.ToString(formData["ten"])))
                { TenGv = Convert.ToString(formData["ten"]); }

                //var result1 = db.GiaoViens.Where(s => s.TenGv.Contains(TenGv)).ToList();

                var result1 = from gv in db.GiaoViens
                          where (gv.TenGv.Contains(TenGv))
                          select new
                          {
                              gv.MaGv,
                              gv.TenGv,
                              gv.NgaySinh,
                              gv.GioiTinh,
                              gv.QueQuan,
                              gv.Anh
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
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\" + "\\giaovien\\";
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
                    return "Not Uploaded.";
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
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\" + "\\giaovien\\";
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
            imgdel = Path.Combine(_webHostEnvironment.WebRootPath, "uploads\\giaovien", imgdel);
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
