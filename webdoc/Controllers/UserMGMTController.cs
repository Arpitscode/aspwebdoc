using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using webdoc.Models;

namespace webdoc.Controllers
{
    public class UserMGMTController : Controller
    {
        ConnectionManager con=new ConnectionManager();
        // GET: User
        public ActionResult UserIndex()
        {            
            if (Session["Usermob"] != null)
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
            if (Session["Usermob"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }

        public ActionResult BookAppointment()
        {
            if (Session["Usermob"] != null)
            {
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public JsonResult Appointment(string n,string em,string gen,string doc,string pt,string iss)
        {
            try
            {
                string sql = "select * from Tbl_Appointment where apttime='" + pt + "' and docname='" + doc + "'";
                string mob = Session["Usermob"].ToString();
                System.Data.DataTable tba = con.getData(sql);
                if (tba.Rows.Count == 0)
                {
                    sql = "insert into Tbl_Appointment values('" + n + "','" + em + "','" + mob + "','" + gen + "','" + doc + "','" + pt + "','" + iss + "','" + DateTime.Now.ToShortDateString() + "')";
                    if (con.crud(sql))
                    {
                        BookingEmailSender emailSender = new BookingEmailSender();
                        string msg = "<h1>Hello <b>" + n + ",<b></h1> <p>Your Appointment Has Been Booked  on"+pt+". So, Please Come on Time. Don't be late.. </p>  Doctor name:"+doc+"<br> Time:"+pt+"<br> From:Webdoc Family <br><b>Thanks You<b> <h3>If We Healthy Our Life Is Also Happy..</h3>";
                        string subject = "Appointment Booking SuccessFully";
                        emailSender.SendMyEmail(em,subject,msg);
                        return Json("Appointment Booked Sccessfully.", JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json("Appointment Not Booked Unsccessfully.", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Doctor is busy. Please Select Another Time.", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {

                return Json("Appointment Does Not Book.Please Try Again..",JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult getdata()
        //{
        //    string sql = "select * from Tbl_Appointment";
        //    DataTable dt = con.getData(sql);
        //    return Json(dt, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult AppointmentMgmt()
        {
            if (Session["Usermob"] != null)
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
                    return Json("Data Delete Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Data Delete not Unsuccessfully", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("Data Delete not Unsuccessfully", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PaymentMgmt()
        {
            if (Session["Usermob"] != null)
            {
                return View();
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult ProfileMgmt()
        {
            if (Session["Usermob"] != null)
            {
                return View();
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();

        }
        [HttpPost]
        public void UpdateProfile(string name,string fname,string gender, string dob,string address)
        {
            if (Request.QueryString["mob"] != null)
            {
                string mob = Request.QueryString["mob"].ToString();
                HttpPostedFileBase file = Request.Files["pic"];
                string sql = "";
                if (file.FileName == null || file.FileName == "")
                {
                    sql = "update Tbl_Register set name='" + name + "',fname='" + fname + "',gender='" + gender + "',dob='" + dob + "',address='" + address + "' where mob='" + mob + "'";
                }
                else
                {
                    sql = "update Tbl_Register set name='" + name + "',fname='" + fname + "',gender='" + gender + "',dob='" + dob + "',address='" + address + "',pic='" + file.FileName.ToString() + "' where mob='" + mob + "'";
                    file.SaveAs(Server.MapPath("../Content/UserImg/" + file.FileName));
                }
                if (con.crud(sql))
                {
                    Response.Write("<script>alert('Data Updated SuccessFully');window.location.href='/UserMGMT/UserIndex';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Data Not Updated UnsuccessFully');window.location.href='/UserMGMT/ProfileMgmt';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Data Not Updated UnsuccessFully');window.location.href='/UserMGMT/ProfileMgmt';</script>");
            }
        }
        public ActionResult ChangePassword()
        {
            if (Session["Usermob"] != null)
            {
                return View();
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public JsonResult Changepass(string password,string Cpassword)
        {
            if (password==Cpassword)
            {
                string mobile = Session["Usermob"].ToString();
                string sql = "select * from Tbl_Register where mob=" + mobile;
                DataTable dt = con.getData(sql);
                //string sql2;
                if (dt.Rows.Count == 1)
                {
                    sql = "update Tbl_Register set password='" + password + "' where mob=" + mobile;
                    if (con.crud(sql))
                    {
                        //BookingEmailSender emailSender = new BookingEmailSender();
                        //string msg = "<h1>Hello <b>" + dt.Rows[0]["pname"].ToString() + ",<b></h1><p><b>Your Password Has Been Changed..<b>"+"<br> From:Webdoc Family <br><b>Thanks You<b> <h3>If We Healthy Our Life Is Also Happy..</h3>";
                        //string subject = "Password Has Been Changed..";
                        //emailSender.SendMyEmail(dt.Rows[0]["pemail"].ToString(), subject, msg);
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
            if (Session["Usermob"] != null)
            {
                Session.Abandon();
                Response.Write("<script>alert('Logout Successfully');window.location.href='/Home/Index';</script>");
            }
            else
            {
                Response.Write("<script>alert('LogIn First');window.location.href='/Home/LogIn';</script>");
            }
        }
    }
}