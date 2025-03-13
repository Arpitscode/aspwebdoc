using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using webdoc.Models;

namespace webdoc.Controllers
{
    public class AdminController : Controller
    {
        ConnectionManager con=new ConnectionManager();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult webdesign()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult ProfileMgmt()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        } 
        public ActionResult DoctorMgmt()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult RegisterMgmt()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult AppointmentMgmt()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult Feedbacks()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult PaymentMgmt()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult ChangePassword()
        {
            if (Session["Username"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        
        public JsonResult Changepass(string password, string Cpassword)
        {
            if (password == Cpassword)
            {
                string username = Session["Username"].ToString();
                string sql = "select * from Tbl_Admin where username='" 
                     + username+"'";
                DataTable dt = con.getData(sql);
                if (dt.Rows.Count == 1)
                {
                    sql = "update Tbl_Admin set password='" + password + "' where username='" + username+"'";
                    if (con.crud(sql))
                    {
                        return Json("Password Has Been Change Successfully", JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json("Password Has Not Been Change Unsuccessfully", JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json("Mobile Does Not Match Unsuccessfully", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Password & Confrim Password Does Not Match", JsonRequestBehavior.AllowGet);
            }
        }
        public void Logout()
        {
            if (Session["Username"] != null)
            {
                Session.Abandon();
                Response.Write("<script>alert('Logout Successfully.');window.location.href='/Home/Index';</script>");
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
        }
        [HttpPost]
        public void AdminFacility(string facility,string details)
        {
            HttpPostedFileBase file = Request.Files["profile"];
            string sql = "";
            if (file.FileName == null || file.FileName == "")
            {
                sql = "insert into Tbl_Facility(FName,FDetails,Fdate) values('" + facility + "','" + details + "','" + DateTime.Now.ToShortDateString() + "')";
            }
            else
            {
                sql = "insert into Tbl_Facility(FName,FDetails,Fdate,Fpic) values('" + facility + "','" + details + "','" + DateTime.Now.ToShortDateString() + "','"+file.FileName.ToString()+"')";
                file.SaveAs(Server.MapPath("../Content/FacilityImg/"+file.FileName));
            }
            if (con.crud(sql))
            {
                Response.Write("<script>alert('Facility Add SuccessFully ');</script>");
            }
            else
            {
                Response.Write("<script>alert('Facility Not Add UnsuccessFully ');</script>");
            }
        }
    }
}