using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Data;

namespace MvcApplication1.Controllers
{
    public class ManageController : Controller
    {
        //普通用户

        private SqlDB db = new SqlDB();
        static String ID;
        static String TYPE;
        static String venueid;
        static String personid;
        //
        // GET: /Manage/

        /*
         * 进行个人信息的管理
         */
        public ActionResult Login_First()
        {
            
            db.OpenConnection();
            DataSet ds = db.Search("SELECT NAME,NUMBER,PASSWORD,TYPE,MONEY FROM UserPerson WHERE ID = '" + ID + "';");
            ViewBag.name = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            ViewBag.number = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            ViewBag.password = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            ViewBag.type = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            ViewBag.money = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            return View();
        }
        public ActionResult Login_Second()
        {

            return View();
        }
        public ActionResult Login_out()
        {
            ID = null;
            return RedirectToAction("Index", "Home");
        }

        /*
         * 进行运动的选择
         */
        public ActionResult Sport_First(String id)
        {
            if (ID == null)
            {
                ID = id;
                
            }
            venueid = null;
            db.OpenConnection();
            DataSet ds = db.Search("SELECT * FROM Venue,VenueType WHERE Venue.TYPEID = VenueType.ID;");
            ViewBag.set = ds;
            ViewBag.num = ds.Tables[0].Rows.Count;
            return View();
        }
        public ActionResult Sport_Second()
        {
            return View();
        }
        public ActionResult Sport_Three()
        {
            return View();
        }

        /*
         * 进行场地的管理
         */
       

        /*
         * 进行卡片的管理
         */
        public ActionResult Card_First()
        {
            return View();
        }

        /*
         * 进行人员管理
         */
       
        /*
         * 进行数据管理
         */
        public ActionResult Date_First()
        {
            return View();
        }












        //工作人员
        public ActionResult Login_First_Manage()
        {

            db.OpenConnection();
            DataSet ds = db.Search("SELECT NAME,NUMBER,PASSWORD,TYPE,MONEY FROM UserPerson WHERE ID = '" + ID + "';");
            ViewBag.name = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            ViewBag.number = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            ViewBag.password = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            ViewBag.type = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            ViewBag.money = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            return View();
        }
        public ActionResult Login_Second_Manage()
        {

            return View();
        }
        public ActionResult Login_Three_Manage()
        {
            return View();
        }

        /*
         * 进行运动的选择
         */
        public ActionResult Sport_First_Manage(String id)
        {
            if (ID == null)
            {
                ID = id;

            }
            venueid = null;
            db.OpenConnection();
            DataSet ds = db.Search("SELECT * FROM Venue,VenueType WHERE Venue.TYPEID = VenueType.ID;");
            ViewBag.set = ds;
            ViewBag.num = ds.Tables[0].Rows.Count;
            return View();
        }
        public ActionResult Sport_Second_Manage()
        {
            return View();
        }
        public ActionResult Sport_Three_Manage()
        {
            return View();
        }

        /*
         * 进行场地的管理
         */

        //场地显示
        public ActionResult Venue_First_Manage()
        {
            venueid = null;
            db.OpenConnection();
            DataSet ds = db.Search("SELECT * FROM Venue,VenueType WHERE Venue.TYPEID = VenueType.ID;");
            ViewBag.set = ds;
            ViewBag.num = ds.Tables[0].Rows.Count;
            return View();
        }
        //添加新场地
        public ActionResult Venue_Second_Manage(Venue model)
        {
            db.OpenConnection();
            if (model.price != null)
            {
                int i = db.Insert("INSERT INTO Venue VALUES('" + model.type + "','" + model.state + "','" + model.price + "','" + model.maxnumber + "','0')");
                if (i != -1)
                {
                    return RedirectToAction("Venue_First_Manage", "Manage");
                }
                else
                {
                    return Content("场地添加失败！");
                }
            }
            else
            {
                DataSet ds = db.Search("SELECT ID,TYPENAME FROM VenueType;");
                List<SelectListItem> list = new List<SelectListItem>();
                int n = ds.Tables[0].Rows.Count;
                for(int i = 0; i < n; ++i)
                {
                    String value = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    String text = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    list.Add(new SelectListItem() { Value = value, Text = text });
                }
                ViewBag.list = list;
                return View();
            }
        }

        //添加新场地类型
        public ActionResult Venue_New_Type(Venue model)
        {
            if (model.typename != null)
            {
                db.OpenConnection();
                DataSet ds = db.Search("SELECT ID FROM VenueType WHERE TYPENAME = '" + model.typename + "';");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    return Content("该类别已被注册！");
                }
                else
                {
                    int i = db.Insert("INSERT INTO VenueType VALUES('" + model.typename + "')");
                    if (i != -1)
                    {
                        return RedirectToAction("Venue_Second_Manage", "Manage");
                    }
                    else
                    {
                        return Content("类型添加失败！");
                    }
                }
            }
            else
            {
                return View();
            }
        }
        //场地编辑
        public ActionResult Venue_Three_Manage(Venue model,String VenueID)
        {
            if (venueid == null)
            {
                venueid = VenueID;
            }
            ViewBag.venueid = VenueID;
            db.OpenConnection();
            if (model.price != null)
            {
                int i = db.Update("UPDATE Venue SET TYPEID = '" + model.type + "',STATE = '" 
                    + model.state + "',PRICE = '" + model.price + "',MAXNUMBER = '" + model.maxnumber +
                    "',NOWNUMBER = '" + model.nownumber + "' WHERE ID = '" + venueid + "';");
                if (i == 1)
                {
                    venueid = null;
                    return RedirectToAction("Venue_First_Manage", "Manage");
                }
                else
                {
                    venueid = null;
                    return Content("场地修改失败！");
                }
            }
            else
            {
                DataSet ds = db.Search("SELECT ID,TYPENAME FROM VenueType;");
                List<SelectListItem> list = new List<SelectListItem>();
                int n = ds.Tables[0].Rows.Count;
                for (int i = 0; i < n; ++i)
                {
                    String value = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    String text = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    list.Add(new SelectListItem() { Value = value, Text = text });
                }
                ViewBag.list = list;
                return View();
            }
        }
        //场地删除
        public ActionResult Venue_Four_Manage(Venue model, String VenueID)
        {
            db.OpenConnection();
            if (venueid != null)
            {
                int i = db.Delete("DELETE FROM Venue WHERE ID = '"+venueid+"';");
                if (i == 1)
                {
                    venueid = null;
                    return RedirectToAction("Venue_First_Manage", "Manage");
                }
                else
                {
                    venueid = null;
                    return Content("场地删除失败！");
                }
            }
            else
            {
                if (venueid == null)
                {
                    venueid = VenueID;
                }
                return View();
            }
        }

        /*
         * 进行卡片的管理
         */

        //人员卡片显示
        public ActionResult Card_First_Manage()
        {
            return View();
        }
        //卡片新建
        public ActionResult Card_Second_Manage()
        {
            return View();
        }

        /*
         * 进行人员管理
         */
        //人员管理
        public ActionResult Person_First_Manage()
        {
            personid = null;
            db.OpenConnection();
            DataSet ds = db.Search("SELECT * FROM UserPerson WHERE TYPE = '0';");
            ViewBag.set = ds;
            ViewBag.num = ds.Tables[0].Rows.Count;
            return View();
        }
        //进行人员编辑
        public ActionResult Person_Second_Manage(Person model,String PersonID)
        {
            if (personid == null)
            {
                personid = PersonID;
            }
            ViewBag.personid = PersonID;
            db.OpenConnection();
            if (model.name != null)
            {
                int i = db.Update("UPDATE UserPerson SET NAME = '" + model.name + "',NUMBER = '"
                    + model.number + "',PASSWORD = '" + model.password + "',INFO = '" + model.info +
                    "',TYPE = '" + model.type + "' WHERE ID = '" + personid + "';");
                if (i == 1)
                {
                    venueid = null;
                    return RedirectToAction("Person_First_Manage", "Manage");
                }
                else
                {
                    venueid = null;
                    return Content("人员修改失败！");
                }
            }
            else
            {
                return View();
            }
        }
        //进行人员删除
        public ActionResult Person_Three_Manage(Person model, String PersonID)
        {
            db.OpenConnection();
            if (personid != null)
            {
                int i = db.Delete("DELETE FROM UserPerson WHERE ID = '" + personid + "';");
                if (i == 1)
                {
                    personid = null;
                    return RedirectToAction("Person_First_Manage", "Manage");
                }
                else
                {
                    personid = null;
                    return Content("人员删除失败！");
                }
            }
            else
            {
                if (personid == null)
                {
                    personid = PersonID;
                }
                return View();
            }
        }

        /*
         * 进行数据管理
         */
        public ActionResult Date_First_Manage()
        {
            return View();
        }
    }
}
