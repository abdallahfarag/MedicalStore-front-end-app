using MedicalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using MedicalStore.Helpers;

namespace MedicalStore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44358/");
                var responseMsg = client.PostAsJsonAsync("api/Account/Register", registerViewModel).Result;
                if (responseMsg.IsSuccessStatusCode)
                {
                    HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", registerViewModel.UserName),
                    new KeyValuePair<string, string>("password", registerViewModel.Password)
                });
                    var result = client.PostAsync("token", content).Result;
                    var accessTokenResult = result.Content.ReadAsAsync<TokenViewModel>().Result;

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenResult.access_token);
                    var userInfoResponseMsg = client.GetAsync("api/Account/UserInfo").Result;
                    if (userInfoResponseMsg.IsSuccessStatusCode)
                    {
                        var userInfo = userInfoResponseMsg.Content.ReadAsAsync<UserInfoViewModel>().Result;

                        Session["id"] = userInfo.Id;
                        Session["username"] = userInfo.UserName;
                        Session["userinfo"] = userInfo;
                        Session["accessToken"] = accessTokenResult.access_token;
                        if(AuthorizationHelper.GetUserInfo().RoleName == "Admin")
                        {
                            return RedirectToAction("AdminDashBoard", "Admin");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName ,string Password)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44358/");
                    HttpContent content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string ,string>("grant_type" ,"password"),
                        new KeyValuePair<string, string>("username" ,UserName),
                        new KeyValuePair<string, string>("password" ,Password)
                    });
                    var result = client.PostAsync("token", content).Result;
                    var accessTokenContent = result.Content.ReadAsAsync<TokenViewModel>().Result;
                    #region trying encode jwtToken
                    //var handelr = new JwtSecurityTokenHandler();
                    //var key = new Microsoft.IdentityModel.Tokens.RsaSecurityKey("31bf3856ad364e35");
                    //var validation = new TokenValidationParameters
                    //{
                    //    ValidateIssuerSigningKey = true,
                    //    IssuerSigningKey = new RsaSecurityKey(),
                    //    ValidateIssuer = false,
                    //    ValidateAudience = false
                    //};
                    //var claims = handelr.ValidateToken(accessTokenContent.access_token, validation, out var token);
                    //var jsonToken = handelr.ReadJwtToken(accessTokenContent.access_token); 
                    #endregion

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenContent.access_token);
                    var userInfoResponseMsg = client.GetAsync("api/Account/UserInfo").Result;
                    if (userInfoResponseMsg.IsSuccessStatusCode)
                    {
                        var userInfo = userInfoResponseMsg.Content.ReadAsAsync<UserInfoViewModel>().Result;

                        Session["id"] = userInfo.Id;
                        Session["username"] = userInfo.UserName;
                        Session["userinfo"] = userInfo;
                        Session["accessToken"] = accessTokenContent.access_token;
                        if (AuthorizationHelper.GetUserInfo().RoleName == "Admin")
                        {
                            return RedirectToAction("AdminDashBoard", "Admin");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["username"] = null;
            Session["accessToken"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}