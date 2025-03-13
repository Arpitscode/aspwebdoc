using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webdoc.Models;

namespace webdoc.Controllers
{
    public class DoctorMGMTController : Controller
    {
        ConnectionManager con=new ConnectionManager();
        // GET: DoctorMGMT
        public ActionResult Index()
        {
            if (Session["Useremail"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult AppointmentSchedules()
        {
            if (Session["Useremail"] != null)
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
            if (Session["Useremail"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public JsonResult CancelAppointment(int id)
        {
            try
            {
                string sql = "delete from Tbl_Appointment where pid=" + id;
                if (con.crud(sql))
                {
                    return Json("Data Deleted Succesfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Data not Deleted Unsuccesfully", JsonRequestBehavior.AllowGet);

                }
            }
            catch
            {
                return Json("Data not Deleted Unsuccesfully", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PaymentMgmt()
        {
            if (Session["Useremail"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult FeedBack()
        {
            if (Session["Useremail"] != null)
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
            if (Session["Useremail"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        [HttpPost]
        public void UpdateProfile(string n, string spec, string fees, string details)
        {
            if (Request.QueryString["em"] != null)
            {
                string email = Request.QueryString["em"].ToString();
                HttpPostedFileBase file = Request.Files["pic"];
                string sql = "";
                if (file.FileName == null || file.FileName == "")
                {
                    sql = "update Tbl_Doctor set docname='" + n + "',docspecial='" + spec + "',docfess='" + fees + "',docdetails='" + details + "' where docemail='" + email + "'";
                }
                else
                {
                    sql = "update Tbl_Doctor set docname='" + n + "',docspecial='" + spec + "',docfess='" + fees + "',docdetails='" +details+ "',profile='" + file.FileName.ToString() + "' where docemail='" + email + "'";
                    file.SaveAs(Server.MapPath("../Content/docimg/" + file.FileName));
                }
                if (con.crud(sql))
                {
                    Response.Write("<script>alert('Data Updated SuccessFully');window.location.href='/DoctorMGMT/Index';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Data Not Updated UnsuccessFully');window.location.href='/DoctorMGMT/ProfileMgmt';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Data Not Updated UnsuccessFully');window.location.href='/DoctorMGMT/ProfileMgmt';</script>");
            }
        }
        public ActionResult ChangePassword()
        {
            if (Session["Useremail"] != null)
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
                string email = Session["Useremail"].ToString();
                string sql = "select * from Tbl_Doctor where docemail='" + email +"'";
                DataTable dt = con.getData(sql);
                if (dt.Rows.Count == 1)
                {
                    sql = "update Tbl_Doctor set password='" + password + "' where docemail='" + email+"'";
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
            if (Session["Useremail"] != null)
            {
                Session.Abandon();
                Response.Write("<script>alert('Logout Successfully.');window.location.href='/Home/LogIn';</script>");
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
        }
    }
}