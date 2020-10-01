﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using yogaAshram.Models;
using yogaAshram.Models.ModelViews;
using yogaAshram.Services;

namespace yogaAshram.Controllers
{
    
    public class MembershipController : Controller
    { 
        private readonly UserManager<Employee> _userManager;
        private readonly YogaAshramContext _db;
        private readonly PaymentsService _paymentsService;
        private readonly ClientServices _clientServices;
        public MembershipController(YogaAshramContext db, UserManager<Employee> userManager, PaymentsService paymentsService, ClientServices clientServices)
        {
            _db = db;
            _userManager = userManager;
            _paymentsService = paymentsService;
            _clientServices = clientServices;
        }

        // GET
        [Breadcrumb("Абонементы", FromAction = "Index", FromController = typeof(ChiefController))]
        public IActionResult Index()
        {
            List<Membership> model = _db.Memberships.ToList(); 
            return View(model);
        }


        public async Task<IActionResult> AddOrEdit(long id = 0)
        {
          
            if (id == 0)
                return View(new Membership());
            else
            {
                var membershipModel = await _db.Memberships.FindAsync(id);
                if (membershipModel == null)
                {
                    return NotFound();
                }
                return View(membershipModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(long id, Membership membershipModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    
                    _db.Entry(membershipModel).State = EntityState.Added;
                    await _db.SaveChangesAsync();

                }
                else
                {
                    try
                    {
                        _db.Update(membershipModel);
                        await   _db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (_db.Memberships.FirstOrDefault(p=>p.Id==id)==null)
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.Memberships.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", membershipModel) });
        }
        [HttpGet]
        public async Task<IActionResult> GetExtendModalAjax(long clientId, string date)
        {
            Client client = await _db.Clients.FindAsync(clientId);
            if (client is null)
                return NotFound();
            ViewBag.Memberships = await _db.Memberships.ToArrayAsync();
            ViewBag.Schedules = await _db.Schedules.ToListAsync();
            ViewBag.Date = Convert.ToDateTime(date);
            return PartialView("PartialViews/ExtendModalPartial" ,new MembershipExtendModelView() { ClientId = client.Id, Client = client });
        }
        [HttpPost]
        public async Task<IActionResult> ExtendAjax(MembershipExtendModelView model)
        {
            Client client = await _db.Clients.FirstOrDefaultAsync(p => p.Id == model.ClientId);
            Membership membership = await _db.Memberships.FirstOrDefaultAsync(p => p.Id == model.MembershipId);
            if (-client.Balance + membership.Price > model.CashSum + model.CardSum)
                return Content("errorNotEnoughSum");
            foreach (var attendanceUnActive in _db.Attendances.Where(a => a.ClientId == model.ClientId))
            {
                    attendanceUnActive.IsNotActive = true;
            }
            int daysFrozen = 0;
                if (membership.AttendanceDays == 12)
                    daysFrozen = 3;
                else if (membership.AttendanceDays == 8)
                    daysFrozen = 2;
                else
                    daysFrozen = 0;
                
                List<DateTime> datesOfAttendance = _clientServices.DatesForAttendance(
                        model.Date, model.GroupId,
                        membership.AttendanceDays + daysFrozen);
                
                AttendanceCount attendanceCount = new AttendanceCount()
                {
                    AttendingTimes = membership.AttendanceDays,
                    AbsenceTimes = 0,
                    FrozenTimes = daysFrozen
                };
                _db.Entry(attendanceCount).State = EntityState.Added;
                
                client.MembershipId = membership.Id;
                client.GroupId = model.GroupId;
                client.Membership = membership;
                client.LessonNumbers = membership.AttendanceDays;
                _db.Entry(client).State = EntityState.Modified;
                
                DateTime endDate = _clientServices.EndDateForClientsMembership(
                    model.Date, model.GroupId,
                    membership.AttendanceDays);
                ClientsMembership clientsMembership = new ClientsMembership()
                {
                    ClientId = client.Id,
                    MembershipId = membership.Id,
                    DateOfPurchase = DateTime.Now,
                    DateOfExpiry = endDate
                };
                _db.Entry(clientsMembership).State = EntityState.Added;
                foreach (var date in datesOfAttendance)
                {
                    Attendance attendance = new Attendance()
                    {
                        ClientId = client.Id,
                        MembershipId = membership.Id,
                        Date = date,
                        AttendanceState = AttendanceState.notcheked,
                        GroupId = model.GroupId,
                        AttendanceCount = attendanceCount,
                        ClientsMembership = clientsMembership
                    };
                    _db.Entry(attendance).State = EntityState.Added;
                }
                Employee employee = await _userManager.GetUserAsync(User);
                
                await _db.SaveChangesAsync();
                model.BranchId = client.Group.BranchId;
                bool check = await _paymentsService.PayForMembership(model, clientsMembership, client, employee.Id);
                
                return Content("success");
            
        }


        public IActionResult BuyMembership(long clientId)
        {
            ViewBag.Schedules =  _db.Schedules.ToList();
            ViewBag.Groups = _db.Groups.ToList();
            ViewBag.Memberships = _db.Memberships.ToList();
            Client client = _db.Clients.FirstOrDefault(p => p.Id == clientId);
            
            return View(client);
        }
        [HttpPost]
        public IActionResult BuyMembership(long clientId,long MembershipId, long GroupId, DateTime FirstDay)
        {
            
            Console.WriteLine("nn");
            
            return View();
        }
    }   
}