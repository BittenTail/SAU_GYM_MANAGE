using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Data;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private SqlDB db = new SqlDB();
        public ActionResult Index()
        {
            return View();
        }
        //登录
        [HttpPost]
        public ActionResult Login(Person model)
        {
            if (!ModelState.IsValid)
            {
                db.OpenConnection();
                DataSet ds = db.Search("SELECT ID,NUMBER,PASSWORD,TYPE FROM UserPerson WHERE NUMBER = '" + model.number + "';");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    String password = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                    int type = int.Parse(ds.Tables[0].Rows[0].ItemArray[3].ToString());
                    if (password == model.password)
                    {
                        if (type == model.type)
                        {
                            String ID = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                            if (type == 0)
                            {
                                return RedirectToAction("Sport_First", "Manage", new { ID = ID });
                            }
                            else
                            {

                                return RedirectToAction("Sport_First_Manage", "Manage", new { ID = ID });
                            }
                        }
                        else
                        {
                            return Content("登录类型选择错误！！");
                        }
                    }
                    else
                    {
                        return Content("密码错误！！");
                    }
                }
                else
                {
                    return Content("该账号未注册！！");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        //注册
        public ActionResult Regist(Person model)
        {
            if (model.name != null)
            {
                db.OpenConnection();
                DataSet ds = db.Search("SELECT ID FROM UserPerson WHERE NUMBER = '" + model.number + "';");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    return Content("该账号已被注册！");
                }
                else
                {
                    int i = db.Insert("INSERT INTO UserPerson VALUES('" + model.name + "','" + model.number + "','" + model.password + "',NULL,'"+model.info+"','" + model.type + "','0')");
                    if (i != -1)
                    {
                        ds = db.Search("SELECT ID FROM UserPerson WHERE NUMBER = '" + model.number + "';");
                        String ID = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        if(model.type == 0)
                        {
                            return RedirectToAction("Sport_First", "Manage", new { ID = ID });
                        }
                        else
                        {
                            return RedirectToAction("Sport_First_Manage", "Manage", new { ID = ID });
                        }
                    }
                    else
                    {
                        return Content("注册失败！");
                    }
                }
            }
            else
            {
                return View();
            }

        }

    }
}
