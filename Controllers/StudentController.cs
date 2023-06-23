using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Student_MVC.DAL;
using Student_MVC.Models;

namespace Student_MVC.Controllers
{
    public class StudentController : Controller
    {
        student_DAL _StudentDAL = new student_DAL();
        // GET: Student
        public ActionResult Index()
        {
            var studentList = _StudentDAL.GetAllStudents();

            if(studentList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently students details are not available in the database.";
            }
            return View(studentList);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var students = _StudentDAL.GetStudentsByID(id).FirstOrDefault();

                if (students == null)
                {
                    TempData["InfoMessage"] = "Student details not available with ID " + id.ToString();
                    return RedirectToAction("Index");

                }
                return View(students);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(student student)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _StudentDAL.InsertStudent(student);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Student details saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the student details.";

                    }
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }


        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {  
            var students = _StudentDAL.GetStudentsByID(id).FirstOrDefault();
            if (students == null)
            {
                TempData["InfoMessage"] = "Student details not available with ID " + id.ToString();
                return RedirectToAction("Index");

            }
            return View(students);
        }

        // POST: Student/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateStudentDetails(student student)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bool IsUpdated = _StudentDAL.UpdateStudent(student);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Student details updated successfully...!";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the student details.";

                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var students = _StudentDAL.GetStudentsByID(id).FirstOrDefault();

                if (students == null)
                {
                    TempData["InfoMessage"] = "Student details not available with ID " + id.ToString();
                    return RedirectToAction("Index");

                }
                return View(students);

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Student/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _StudentDAL.DeleteStudentDetails(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
