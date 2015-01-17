using ANTOURA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ANTOURA.Controllers
{
    public class AgendaController : BaseController
    {
        private AgendaRepository rpstry = new AgendaRepository();

        public ActionResult Index() {
            var recordsToReturn = rpstry.GetAll();
            return View(recordsToReturn);
        }

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Agenda entry)
        {
            try
            {
                entry.dateCreated = DateTime.Now;
                entry.dateModified = DateTime.Now;
                rpstry.Add(entry);
                rpstry.Save();
                return RedirectToAction("Index", new { thisid = entry.id });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "");
                return View(entry);
            }
        }
        #endregion
        #region Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entry = rpstry.GetById(id);
            return View(entry);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection coll)
        {
            var entry = rpstry.GetById(id);
            try
            {
                UpdateModel(entry);

                entry.dateModified = DateTime.Now;
                rpstry.Save();
                return RedirectToAction("Index", new { thisid = entry.id });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "");
                return View(entry);
            }
        }
        #endregion

        public ActionResult Delete(int id) {
            var entry = rpstry.GetById(id);
            rpstry.Delete(entry);
            rpstry.Save();
            return RedirectToAction("Index");
        }

    }
}
