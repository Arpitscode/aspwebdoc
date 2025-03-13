using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using webdoc.Models;

namespace webdoc.Controllers
{
    public class HomeController : Controller
    {
        ConnectionManager con=new ConnectionManager(); 
        // GET: Home
        public ActionResult Index() 
        {
            return View();
        }
        public ActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Feedback(string name,string email,string feedback)
        {
            HttpPostedFileBase file = Request.Files["file"];
            string sql = "";
            if (file.FileName == null || file.FileName == "")
            {
                sql = "insert into Tbl_Feedback(name,email,feedback,feeddate) values('" +
                   name+ "','"+email + "','" +feedback + "','"+DateTime.Now.ToShortDateString()+"')";
            }
            else
            {
                sql = "insert into Tbl_Feedback(name,email,feedback,feeddate,file) values('" +
                  name + "','" + email + "','" + feedback + "','" + DateTime.Now.ToShortDateString() + "','"+file.FileName.ToString()+"')";
                file.SaveAs(Server.MapPath("../Content/Feedbackimg/" + file.FileName));
            }
            if (con.crud(sql))
            {
                        Response.Write("<script>alert('Feedback Submitted Successfully.Thanks You.');window.location.href='/Home/Index';</script>");
            }
            else
            {
                Response.Write("<script>alert('Feedback Not Submitted Unsuccessfully.Thanks You.');window.location.href='/Home/Index';</script>");

            }
            return View();
        }
        public ActionResult Appointment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Appointment(string pname, string pemail,string pmob,string pgender,string doctor,string password,string apdatetime,string pissues)
        {
            string sql = "select * from Tbl_Register where mob='" + pmob + "' and password='" + password + "'";
            System.Data.DataTable dt = con.getData(sql);
            if (dt.Rows.Count == 1)
            {
                sql = "select * from Tbl_Appointment where apttime='" +apdatetime + "' and docname='" + doctor + "'";
                System.Data.DataTable tba = con.getData(sql);
                if (tba.Rows.Count == 0)
                {
                    sql = "insert into Tbl_Appointment values('" + pname + "','" + pemail + "','" + pmob + "','" + pgender + "','" + doctor + "','" + apdatetime + "','" + pissues + "','" + DateTime.Now.ToShortDateString() + "')";
                    if (con.crud(sql))
                    {
                        BookingEmailSender emailSender = new BookingEmailSender();
                        string msg = "<h1>Hello <b>" + pname + ",<b></h1> <p>Your Appointment Has Been Booked  on" + apdatetime + ". So, Please Come on Time. Don't be late.. </p>  Doctor name:" + doctor + "<br> Time:" + apdatetime + "<br> From:Webdoc Family <br><b>Thanks You<b> <h3>If We Healthy Our Life Is Also Happy..</h3>";
                        string subject = "Appointment Booking SuccessFully";
                        emailSender.SendMyEmail(pemail, subject, msg);
                        Response.Write("<script>alert('Appointment Booked.');window.location.href='/Home/Index';</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Appointment Not Booked.');window.location.href='/Home/Appointment';</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Doctor is busy. Please Select Another Time.');window.location.href='/Home/Appointment';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Password or Mobile Number.');window.location.href='/Home/Appointment';</script>");

            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string name,string fathername, string mob, string gender, string dob, string password,string cpassword, string address,string captcha,string hdnct1)
        {
            if (password == cpassword && captcha==hdnct1)
            {
                HttpPostedFileBase pic = Request.Files["file"];
                string sql = "";
                if (pic.FileName == "" || pic.FileName == null)
                {
                    sql = "insert into Tbl_Register(name,fname,mob,gender,dob,password,address,regdate) values('" + name + "','" + fathername + "','" + mob + "','" + gender + "','" +dob+"','"+ password + "','" + address + "','" + DateTime.Now.ToShortDateString() + "')";
                }
                else
                {
                    sql = "insert into Tbl_Register(name,fname,mob,gender,dob,password,address,regdate,pic) values('" + name + "','" + fathername + "','" + mob + "','" + gender + "','"+dob+ "','" + password + "','" + address + "','" + DateTime.Now.ToShortDateString() + "','" + pic.FileName.ToString() + "')";
                    pic.SaveAs(Server.MapPath("../Content/UserImg/" + pic.FileName));
                }
                if (con.crud(sql))
                {
                    Response.Write("<script>alert('Registration Successfully. ');window.location.href='/Home/Index';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Registration Not Unsuccessfully.');window.location.href='/Home/Register';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Password/Captcha Does Not Match.');window.location.href='/Home/Register';</script>");
            }
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string username,string password,string hdnct1,string ct2,string check,string panel)
        {
            try
            {
                if (hdnct1 == ct2)
                {
                    string sql = "";
                    if (panel == "User Panel")
                    {
                        sql = "select * from Tbl_Register where mob='" + username + "' and password='" + password + "'";
                        System.Data.DataTable dt = con.getData(sql);
                        if (dt.Rows.Count == 1)
                        {
                            Session["Usermob"] = dt.Rows[0]["mob"];
                            if (check=="true")
                            {
                                HttpCookie cookie = new HttpCookie("Webdoc");
                                cookie.Values["Username"] = username;
                                cookie.Values["password"] = password;
                                HttpContext.Response.Cookies.Add(cookie);
                                cookie.Expires = DateTime.Now.AddDays(1);
                            }
                            Response.Write("<script>alert('Login Successfully." + Session["Usermob"].ToString() + "');window.location.href='/UserMGMT/UserIndex';</script>");
                        }
                        Response.Write("<script>alert('Invalid Mobile Number & Password.');window.location.href='/Home/LogIn';</script>");
                    }
                    else if (panel == "Doctor Panel")
                    {
                        sql = "select * from Tbl_Doctor where docemail='" + username + "' and password='" + password + "'";
                        System.Data.DataTable dt = con.getData(sql);
                        if (dt.Rows.Count == 1)
                        {
                            Session["Useremail"] = dt.Rows[0]["docemail"];
                            if (check == "true")
                            {
                                HttpCookie cookie = new HttpCookie("Webdoc");
                                cookie.Values["Username"] = username;
                                cookie.Values["password"] = password;
                                HttpContext.Response.Cookies.Add(cookie);
                                cookie.Expires = DateTime.Now.AddDays(1);
                            }
                            Response.Write("<script>alert('Login Successfully." + Session["Useremail"].ToString() + "');window.location.href='/DoctorMGMT/Index';</script>");
                        }
                        Response.Write("<script>alert('Invalid User Email & Password.');window.location.href='/Home/LogIn';</script>");
                    }
                    else if (panel == "Admin Panel")
                    {
                        sql = "select * from Tbl_Admin where username='" + username + "' and password='" + password + "'";
                        System.Data.DataTable dt = con.getData(sql);
                        if (dt.Rows.Count == 1)
                        {
                            Session["Username"] = dt.Rows[0]["username"];
                            if (check == "true")
                            {
                                HttpCookie cookie = new HttpCookie("Webdoc");
                                cookie.Values["Username"] = username;
                                cookie.Values["password"] = password;
                                HttpContext.Response.Cookies.Add(cookie);
                                cookie.Expires = DateTime.Now.AddDays(1);
                            }
                            Response.Write("<script>alert('Login Successfully." + Session["Username"].ToString() + "');window.location.href='/Admin/Index';</script>");
                        }
                        Response.Write("<script>alert('Invalid User Email & Password.');window.location.href='/Home/LogIn';</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Username or Password.');window.location.href='/Home/LogIn';</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Captcha Code.');window.location.href='/Home/LogIn';</script>");
                }
            }
            catch
            {
                Response.Write("<script>alert('Somethings Went Wrong ');window.location.href='/Home/LogIn';</script>");
            }
            return View();
        }
        public ActionResult DoctorRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoctorRegister(string name,string email,string Specailization,string password,string fees,string details,string hdnct1,string ct2)
        {
            if(hdnct1==ct2 && password != null)
            {
                string qu="select * from Tbl_Doctor where docemail='"+email + "'";
                DataTable dt = con.getData(qu);
                if (dt.Rows.Count==0)
                {
                    HttpPostedFileBase file = Request.Files["file"];
                    string sql = "";
                    if (file.FileName == null)
                    {
                        sql = "insert into Tbl_Doctor(docname,docspecial,docemail,password,docfess,docdetails,regdate) values('" + name + "','" + Specailization + "','" + email + "','" + password + "','" + fees + "','" + details + "','" + DateTime.Now.ToShortDateString() + "')";
                    }
                    else
                    {
                        sql = "insert into Tbl_Doctor(docname,docspecial,docemail,password,docfess,docdetails,regdate,profile) values('" + name + "','" + Specailization + "','" + email + "','" + password + "','" + fees + "','" + details + "','" + DateTime.Now.ToShortDateString() + "','" + file.FileName.ToString() + "')";
                        file.SaveAs(Server.MapPath("../Content/docimg/" + file.FileName));
                    }
                    if (con.crud(sql))
                    {
                        Response.Write("<script>alert('Registration SuccessFully.');window.location.href='/Home/LogIn'</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('UnsuccessFully! Something Wrong Check Again....');window.location.href='/Home/DoctorRegister'</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Already Register. Please Try Another Email...');window.location.href='/Home/DoctorRegister'</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invaild Captcha Code');window.location.href='/Home/DoctorRegister'</script>");

            }
            return View();
        }
    }
}