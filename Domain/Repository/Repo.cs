using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Zadatak1.Models;

public static class Repo
{
	public static DataSet Ds { get; set; }
	public static IEnumerable<Drzava> Drzave { get; set; }
	public static IEnumerable<Grad> Gradovi { get; set; }

	private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

	static Repo()
	{
		Drzave = GetDrzave();
		Gradovi = GetGradovi();
	}

	internal static int InsertUser(User user)
	{
		return SqlHelper.ExecuteNonQuery(cs, "insertAppUser", user.Username, user.Password, user.Email, user.Person.Ime, user.Person.Prezime);
	}

	public static Kupac GetKupac(int IDKupac)
	{
		return GetTop10Kupaca().Single(k => k.Person.ID == IDKupac);
	}

	public static Drzava GetDrzava(int GradID)
	{
		Ds = SqlHelper.ExecuteDataset(cs, "GetDrzava", GradID);
		DataRow row = Ds.Tables[0].Rows[0];

		Drzava d = new Drzava
		{
			IDDrzava = (int)row["IDDrzava"],
			Naziv = row["Naziv"].ToString()
		};

		return d;
	}

	public static IEnumerable<Kupac> GetTop10Kupaca()
	{
		Ds = SqlHelper.ExecuteDataset(cs, "DohvatiKupce");
		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			yield return new Kupac
			{
				Person = new Person
				{
					ID = (int)row["IDKupac"],
					Ime = row["Ime"].ToString(),
					Prezime = row["Prezime"].ToString()
				},
				Email = row["Email"].ToString(),
				Telefon = row["Telefon"].ToString(),
				GradID = row["GradID"] != DBNull.Value ? (int)row["GradID"] : 1,
				DrzavaID = row["IDDrzava"] != DBNull.Value ? (int)row["IDDrzava"] : 1
			};
		}
	}

	public static Grad GetGrad(int IDGrad)
	{
		DataRow row = SqlHelper.ExecuteDataset(cs, "GetGrad", IDGrad).Tables[0].Rows[0];
		return new Grad
		{
			IDGrad = (int)row["IDGrad"],
			DrzavaID = (int)row["DrzavaID"],
			Naziv = row["Naziv"].ToString()
		};
	}

	public static IEnumerable<Grad> GetGradovi()
	{
		List<Grad> kolekcija = new List<Grad>();

		Ds = SqlHelper.ExecuteDataset(cs, "GetGradovi");
		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			kolekcija.Add(new Grad
			{
				IDGrad = (int)row["IDGrad"],
				Naziv = row["Naziv"].ToString()
			});
		}
		return kolekcija;
	}

	public static IEnumerable<KreditnaKartica> GetKreditnaKarticeForRacun(IEnumerable<Racun> racuni)
	{
		foreach (var racun in racuni)
		{
			yield return GetKreditnaKartica(racun.KreditnaKarticaID);
		}
	}

	public static IEnumerable<Proizvod> GetProizvodiForStavke(IEnumerable<Stavka> stavke)
	{
		foreach (var stavka in stavke)
		{
			yield return GetProizvod(stavka.ProizvodID, stavka.RacunID);
		}
	}

	public static int InsertKupac(Kupac kupac)
	{
		return SqlHelper.ExecuteNonQuery(cs, "InsertKupac", kupac.Person.Ime, kupac.Person.Prezime, kupac.Email, kupac.Telefon, kupac.GradID);
	}

	public static Proizvod GetProizvod(int id, int idracun)
	{
		IEnumerable<Proizvod> proizvodi = GetProizvodi(idracun);
		return proizvodi.Single(p => p.IDProizvod == id);
	}

	public static Komercijalist GetKomercijalist(int idKomercijalist)
	{
		Ds = SqlHelper.ExecuteDataset(cs, "getKomercijalistaForRacun", idKomercijalist);



		DataRow row = Ds.Tables[0].Rows[0];
		return new Komercijalist
		{
			Person = new Person
			{
				ID = (int)row["IDKomercijalist"],
				Ime = row["Ime"].ToString(),
				Prezime = row["Prezime"].ToString()
			},
			StalniZaposlenik = (bool)row["StalniZaposlenik"]


		};


	}
	public static IEnumerable<Stavka> GetStavke(int idRacun)
	{
		Ds = SqlHelper.ExecuteDataset(cs, "getStavkeByRacun", idRacun);
		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			yield return new Stavka
			{
				IDStavka = (int)row["IDStavka"],
				RacunID = (int)row["RacunID"],
				Kolicina = Convert.ToInt16(row["Kolicina"]),
				ProizvodID = (int)row["ProizvodID"],
				CijenaPoKomadu = (decimal)row["CijenaPoKomadu"],
				PopustUPostocima = (decimal)row["PopustUPostocima"],
				UkupnaCijena = (decimal)row["UkupnaCijena"]

			};
		}
	}
	public static IEnumerable<Proizvod> GetProizvodi(int racunid)
	{
		Ds = SqlHelper.ExecuteDataset(cs, "getKupljeniProizvodi", racunid);
		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			yield return new Proizvod
			{
				IDProizvod = (int)row["IDProizvod"],
				Naziv = row["Naziv"].ToString(),
				BrojProizvoda = row["BrojProizvoda"].ToString(),
				Boja = row["Boja"] != DBNull.Value ? row["Boja"].ToString() : "None",
				Potkategorija = row["Potkategorija"].ToString(),
				Kategorija = row["Kategorija"].ToString()
			};
		}
	}
	public static IEnumerable<Racun> GetRacuniForKupac(int idkupac)
	{
		if (idkupac == -1)
		{
			Ds = SqlHelper.ExecuteDataset(cs, "getRacuni");
		}
		else
		{
			Ds = SqlHelper.ExecuteDataset(cs, "getRacunForKupac", idkupac);
		}

		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			yield return new Racun
			{
				IDRacun = (int)row["IDRacun"],
				DatumIzdavanja = (DateTime)row["DatumIzdavanja"],
				BrojRacuna = row["BrojRacuna"].ToString(),
				KupacID = (int)row["KupacID"],
				KomercijalistID = row["KomercijalistID"] != DBNull.Value ? (int)row["KomercijalistID"] : 296,
				KreditnaKarticaID = row["KreditnaKarticaID"] != DBNull.Value ? (int)row["KreditnaKarticaID"] : 1,
				Komentar = row["Komentar"] != DBNull.Value ? row["Komentar"].ToString() : "None"
			};
		}
	}
	public static IEnumerable<Komercijalist> GetKomercijalisti()
	{
		Ds = SqlHelper.ExecuteDataset(cs, "getKomercijaliste");
		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			yield return new Komercijalist
			{
				Person = new Person
				{
					ID = (int)row["IDKomercijalist"],
					Ime = row["Ime"].ToString(),
					Prezime = row["Prezime"].ToString(),

				},

				StalniZaposlenik = (bool)row["StalniZaposlenik"]
			};
		}
	}
	public static KreditnaKartica GetKreditnaKartica(int idKreditnaKartica)
	{
		Ds = SqlHelper.ExecuteDataset(cs, "getKreditnaKarticaForRacun", idKreditnaKartica);
		DataRow row = Ds.Tables[0].Rows[0];
		return new KreditnaKartica
		{
			ID = (int)row["IDKreditnaKartica"],
			Tip = row["Tip"].ToString(),
			Broj = row["Broj"].ToString(),
			IstekMjesec = Convert.ToInt16(row["IstekMjesec"]),
			IstekGodina = Convert.ToInt16(row["IstekGodina"])

		};
	}

	public static IEnumerable<User> GetUsers()
	{
		Ds = SqlHelper.ExecuteDataset(cs, "getUsers");
		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			yield return new User
			{
				Person = new Person
				{
					ID = (int)row["IDOPUser"],
					Ime = row["Ime"].ToString(),
					Prezime = row["Prezime"].ToString()
				},
				Email = row["Email"].ToString(),
				Password = row["OPPassword"].ToString(),
				Username = row["OPUser"].ToString(),
			};
		}

	}
	public static IEnumerable<Drzava> GetDrzave()
	{
		List<Drzava> kolekcija = new List<Drzava>();

		Ds = SqlHelper.ExecuteDataset(cs, "getDrzave");
		foreach (DataRow row in Ds.Tables[0].Rows)
		{
			kolekcija.Add(new Drzava
			{
				IDDrzava = (int)row["IDDrzava"],
				Naziv = row["Naziv"].ToString()
			});
		}
		return kolekcija;

	}
	public static int UpdateKupac(Kupac kupac)
	{
		return SqlHelper.ExecuteNonQuery(cs, "UpdateKupac", kupac.Person.ID, kupac.Person.Ime, kupac.Person.Prezime, kupac.Email, kupac.Telefon, kupac.GradID);
	}

	public static int DeleteKupac(int kupacid)
	{
		return SqlHelper.ExecuteNonQuery(cs, "deleteKupac", kupacid);
	}


	public static IEnumerable<Komercijalist> GetKomercijalistiForRacun(IEnumerable<Racun> racuni)
	{
		foreach (Racun racun in racuni)
		{
			yield return GetKomercijalist(racun.KomercijalistID);
		}
	}

}
