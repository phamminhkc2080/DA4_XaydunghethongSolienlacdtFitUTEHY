using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoLLDienTu.Helpers;
using SoLLDienTu.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoLLDienTu.Controllers
{
    [Authorize]
    [Route("api/login")]
    [ApiController]
    public class TaiKhoansController : ControllerBase
    {
        private SoLLDienTuContext db;
        private readonly AppSettings _appSettings;
        public TaiKhoansController(IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            db = new SoLLDienTuContext(configuration);
            _appSettings = appSettings.Value;
        }
        //[AllowAnonymous]
        //[HttpPost("login")]
        //public IActionResult Authenticate([FromBody] TaiKhoan model)
        //{
        //    var TaiKhoan = model.UserName;
        //    var MatKhau = model.Pass;
        //    var user = db.TaiKhoans.SingleOrDefault(x => x.UserName == TaiKhoan && x.Pass == MatKhau);
        //    if (user == null)
        //    {
        //        return Ok(new { message = "Tài khoản hoặc mật khẩu không chính xác!" });
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[] {
        //            new Claim(ClaimTypes.Name,user.TypeUser.ToString()),
        //            new Claim(ClaimTypes.Role,user.MaSv),
        //            new Claim(ClaimTypes.DenyOnlyWindowsDeviceGroup,user.UserName)
        //            }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        //    };
        //    var tmp = tokenHandler.CreateToken(tokenDescriptor);
        //    var token = tokenHandler.WriteToken(tmp);
        //    return Ok(new { taikhoan = user.TypeUser, maND = user.MaSv, matkhau = user.UserName, Token = token });

        //}

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]TaiKhoan model)
        {
            var userName = model.TaiKhoan1;
            var Pass = model.MatKhau;
            var user = db.TaiKhoans.SingleOrDefault(x => x.TaiKhoan1 == userName && x.MatKhau == Pass);
            if (user == null)
            {
                return Ok(new { message="Tài khoản hoặc mật khẩu không chính xác"});
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [Route("Login/{tk}/{mk}")]
        [HttpGet]
        public IActionResult LoginUser(string tk, string mk)
        {
            try
            {
                string ma = "";
                List<TaiKhoan> listtk = new List<TaiKhoan>();
                IEnumerable<TaiKhoan> result = db.TaiKhoans.ToList();
                foreach (TaiKhoan tk1 in result)
                {
                    listtk.Add(tk1);
                }


                foreach (TaiKhoan kq in listtk)
                {
                    if (kq.TaiKhoan1 == tk && kq.MatKhau == mk)
                    {
                        ma = kq.Ma;
                    }
                }
                return Ok(ma);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

    }
}
