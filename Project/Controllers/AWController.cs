using Project.Models;
using System.Linq;
using System.Web.Mvc;

namespace Project.Controllers
{

    [Authorize]
    public class AWController : Controller
    {

        public ActionResult Kupci()
        {
            return View(Repo.GetTop10Kupaca());
        }

        public ActionResult GetGrad(int id)
        {
            return Content(Repo.GetGrad(id).Naziv);
        }
        [HttpGet]
        public ActionResult EditKupac(int id)
        {

            ViewBag.gradovi = Repo.Gradovi;
            ViewBag.drzave = Repo.Drzave;
            return View(Repo.GetKupac(id));
        }

        [HttpPost]
        public ActionResult EditKupac(Kupac kupac)
        {

            if (ModelState.IsValid)
            {
                Repo.UpdateKupac(kupac);
                return RedirectToAction("Kupci");
            }
            else
            {
                ViewBag.drzave = Repo.Drzave;
                ViewBag.gradovi = Repo.Gradovi;
                return View(kupac);
            }
        }

        [HttpGet]
        public ActionResult CreateKupac()
        {
            ViewBag.gradovi = Repo.Gradovi;
            ViewBag.drzave = Repo.Drzave;
            return View();
        }
        [HttpPost]
        public ActionResult CreateKupac(Kupac kupac)
        {
            Repo.InsertKupac(kupac);
            return RedirectToAction("Kupci");
        }

        [HttpGet]
        public ActionResult DeleteKupac(int id)
        {
            Kupac kupac = Repo.GetKupac(id);
            ViewBag.grad = Repo.Gradovi.First(g => g.IDGrad == kupac.GradID).Naziv;
            ViewBag.drzava = Repo.Drzave.First(d => d.IDDrzava == kupac.DrzavaID).Naziv;
            return View(kupac);
        }


        [HttpPost, ActionName("DeleteKupac")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repo.DeleteKupac(id);
            return RedirectToAction("Kupci");
        }

        public ActionResult Racuni(int id)
        {

            ViewBag.idkupac = id;
            RacunViewModel racun = new RacunViewModel
            {
                Racuni = Repo.GetRacuniForKupac(id)
            };
            racun.Komercijalist = Repo.GetKomercijalistiForRacun(racun.Racuni);
            racun.KreditneKartice = Repo.GetKreditnaKarticeForRacun(racun.Racuni);

            return View(racun);
        }

        public ActionResult Stavke(int id, int idkupac, bool allkupci = false)
        {
            ViewBag.idracun = id;
            ViewBag.idkupac = idkupac;


            StavkeViewModel stavke = new StavkeViewModel
            {
                Stavke = Repo.GetStavke(id)
            };
            stavke.Proizvodi = Repo.GetProizvodiForStavke(stavke.Stavke);
            stavke.AllKupci = allkupci;
            return View(stavke);
        }

        public ActionResult Komercijalist(int id)
        {

            return View(Repo.GetKomercijalist(id));
        }
        public ActionResult Proizvod(int id, int idracun, int idkupac, bool allkupci = false)
        {
            ViewBag.idkupac = idkupac;
            ViewBag.idRacun = idracun;
            ViewBag.allkupci = allkupci;
            return View(Repo.GetProizvod(id, idracun));
        }

        public ActionResult KreditnaKartica(int id)
        {
            ViewBag.idkupac = id;
            return View(Repo.GetKreditnaKartica(id));
        }
    }
}