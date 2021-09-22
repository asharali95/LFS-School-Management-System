﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LFS_School_Management_System.Models;

namespace LFS_School_Management_System.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class EnrollmentsController : Controller
    {
        private LFS_School_ManagementDB_Entities db = new LFS_School_ManagementDB_Entities();

        // GET: Enrollments
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Lecturer);
            return View(await enrollments.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID,LecturerId")] Enrollment enrollment)
        {
            if (ModelState.IsValid && enrollment.Grade<4)
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentID);
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name", enrollment.LecturerId);
            return View(enrollment);
        }

        [HttpPost]
        public async Task<JsonResult> AddStudent([Bind(Include = "CourseID,StudentID,Grade")] Enrollment enrollment) {
            try
            {
                var isEnrolled = db.Enrollments.Any(q => q.CourseID == enrollment.CourseID && q.StudentID == enrollment.StudentID);
                if (ModelState.IsValid && !isEnrolled && enrollment.Grade < 4)
                {
                    db.Enrollments.Add(enrollment);
                    await db.SaveChangesAsync();
                    return Json(new { IsSuccess= true, Message="Student Added Successfully"  },JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, Message = "Student is already enrolled in this course" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { IsSuccess = false, Message = "System Failure: Contact your administrator" }, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentID);
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name", enrollment.LecturerId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID,LecturerId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentID);
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name", enrollment.LecturerId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetStudents(string term)
        {
            var students = db.Students.Select(q => new{
                Name = q.FirstName + " " + q.MiddleName + " " + q.LastName,
                DisplayName = q.FirstName + " " + q.MiddleName + " " + q.LastName+ "(" + q.StudentID + ")",
                ID = q.StudentID,
            }).Where(q => q.Name.Contains(term));

            return Json(students,JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
