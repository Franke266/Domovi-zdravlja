using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleTables;
using System.Xml;

namespace KV_IvanFranjic
{
    class Program
    {
        public struct Medicinsko_osoblje
        {
            public string ime;
            public string prezime;
            public int tip;
            public int dom_zdravlja;
            public int djelatnost;

            public Medicinsko_osoblje(string i, string p, int t, int z, int d)
            {
                ime = i;
                prezime = p;
                tip = t;
                dom_zdravlja = z;
                djelatnost = d;
            }
        }
        public struct Domovi_zdravlja
        {
            public int id;
            public string naziv;

            public Domovi_zdravlja(int j, string g)
            {
                id = j;
                naziv = g;
            }
        }
        public struct Djelatnosti
        {
            public int id;
            public string naziv;

            public Djelatnosti(int j, string n)
            {
                id = j;
                naziv = n;
            }
        }
        public struct Tip
        {
            public int id;
            public string naziv;

            public Tip(int j, string n)
            {
                id = j;
                naziv = n;
            }
        }
        public struct Login
        {
            public string user;
            public string lozinka;

            public Login(string j, string n)
            {
                user = j;
                lozinka = n;
            }
        }

        static void Main(string[] args)

        {
            login();
            prikaziIzbornik();

            Console.ReadKey();

        }
        public static void prikaziIzbornik()
        {
            Console.Clear();
            Console.WriteLine("GLAVNI IZBORNIK");
            Console.WriteLine();
            Console.WriteLine("1) Pregled svih domova zdravlja.");
            Console.WriteLine("2) Pregled svih zaposlenika.");
            Console.WriteLine("3) Pregled djelatnosti po gradu.");
            Console.WriteLine("4) Statistika djelatnosti.");
            Console.WriteLine("5) Dodaj doktora/sestru");
            Console.WriteLine("6) Odjava");
            Console.WriteLine();
            Console.Write("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);

            int[] dozvoljeni_unosi = { 49, 50, 51, 52, 53, 54 };
            while (!dozvoljeni_unosi.Contains(odabir))
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }
            /*
             49 - 1
             50 - 2
             51 - 3
             52 - 4
             53 - 5
             54 - 6
             */

            switch (odabir)
            {
                case 49: //1
                    ispisiDomove_zdravlja();
                    prikaziKontrole();
                    break;

                case 50: //2
                    ispisiMedicinsko_osoblje();
                    prikaziKontrole();
                    break;

                case 51: //3
                    prikaziIzbornik2();
                    prikaziKontrole();
                    break;

                case 52: //4
                    dajStatistiku();
                    prikaziKontrole();
                    break;

                case 53: //5
                    dodajOsoblje();
                    prikaziKontrole();
                    break;

                case 54: //6
                    Console.WriteLine();
                    Console.WriteLine("\nUSPJESNO STE ODJAVLJENI, PROGRAM CE SE ZATVORITI ZA 3 SEKUNDE!!!");
                    System.Threading.Thread.Sleep(3000);
                    System.Environment.Exit(1);
                    break;
            }
        }
        public static void prikaziIzbornik2()
        {
            Console.Clear();
            Console.WriteLine("1) Slatina");
            Console.WriteLine("2) Virovitica");
            Console.WriteLine("3) Orahovica");
            Console.WriteLine("4) Nasice");
            Console.WriteLine("5) Bjelovar");
            Console.WriteLine("Q) Za povratak u glavni izbornik");
            Console.WriteLine("X) Za izlazak iz programa");
            Console.Write("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);
            /*
             49 - 1
             50 - 2
             51 - 3
             52 - 4
             53 - 5
             -------
             */

            switch (odabir)
            {
                case 49: //1
                    dajSlatinu();
                    prikaziKontrole2();
                    break;

                case 50: //2
                    dajViroviticu();
                    prikaziKontrole2();
                    break;

                case 51: //3
                    dajOrahovicu();
                    prikaziKontrole2();
                    break;

                case 52: //4
                    dajNasice();
                    prikaziKontrole2();
                    break;

                case 53: //5
                    dajBjelovar();
                    prikaziKontrole2();
                    break;
                case 81: //Q
                    prikaziIzbornik();
                    break;
                case 88: //X
                    System.Environment.Exit(1);
                    break;
            }
        }

        //funkcija koja ucitava podatke za prijavu i stvara listu
        public static List<Login> ucitajLogin()
        {
            string sXml = "";
            StreamReader oSr = new StreamReader(@"C:\VisualStudioProjects\Osnove-programiranja\KV_IvanFranjic2\KV_IvanFranjic2\login.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/admins/admin");
            List<Login> lLogin = new List<Login>();
            foreach (XmlNode oNode in oNodes)
            {
                lLogin.Add(new Login(
                    oNode.Attributes["user"].Value,
                    oNode.Attributes["lozinka"].Value
                ));
            }
            oSr.Close(); 
            return lLogin;
        }

        public static void login()
        {
            bool prijava = false;
            Console.WriteLine("Unesi username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Unesi zaporku: ");
            //string password = Console.ReadLine();
            List<Login> podaci = ucitajLogin();
            foreach (Login Admin in podaci)
            {
                string pass = "";
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        pass += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            pass = pass.Substring(0, (pass.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                if (username == Admin.user)
                {

                    if (pass == Admin.lozinka)
                    {
                        Console.WriteLine("Uspjesna prijava");
                        prijava = true;
                    }
                }

            }
            if (prijava == false)
            {
                Console.WriteLine("\nNEUSPJESNA PRIJAVA, POKUSAJTE PONOVNO!!!");
                Console.WriteLine();
                login();
            }
        }

        public static List<Domovi_zdravlja> ucitajDomove_zdravlja()
        {
            string sXml = "";
            StreamReader oSr = new StreamReader(@"C:\VisualStudioProjects\Osnove-programiranja\KV_IvanFranjic2\KV_IvanFranjic2\medicinsko_osoblje.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/domovi_zdravlja/dom_zdravlja");
            List<Domovi_zdravlja> lDomovi_zdravlja = new List<Domovi_zdravlja>();
            foreach (XmlNode oNode in oNodes)
            {
                lDomovi_zdravlja.Add(new Domovi_zdravlja(
                    Convert.ToInt32(oNode.Attributes["id"].Value),
                    oNode.Attributes["naziv"].Value
                ));
            }
            oSr.Close();
            return lDomovi_zdravlja;
        }

        public static List<Djelatnosti> ucitajDjelatnosti()
        {
            string sXml = "";
            StreamReader oSr = new StreamReader(@"C:\VisualStudioProjects\Osnove-programiranja\KV_IvanFranjic2\KV_IvanFranjic2\medicinsko_osoblje.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/djelatnosti/djelatnost");
            List<Djelatnosti> lDjelatnosti = new List<Djelatnosti>();
            foreach (XmlNode oNode in oNodes)
            {
                lDjelatnosti.Add(new Djelatnosti(
                    Convert.ToInt32(oNode.Attributes["id"].Value),
                    oNode.Attributes["naziv"].Value
                ));
            }
            oSr.Close();

            return lDjelatnosti;
        }

        public static List<Medicinsko_osoblje> ucitajMedicinsko_osoblje()
        {
            string sXml = "";
            StreamReader oSr = new StreamReader(@"C:\VisualStudioProjects\Osnove-programiranja\KV_IvanFranjic2\KV_IvanFranjic2\medicinsko_osoblje.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/medicinsko_osoblje/osoblje");
            List<Medicinsko_osoblje> lMedicinsko_osoblje = new List<Medicinsko_osoblje>();
            foreach (XmlNode oNode in oNodes)
            {
                lMedicinsko_osoblje.Add(new Medicinsko_osoblje(
                    oNode.Attributes["ime"].Value,
                    oNode.Attributes["prezime"].Value,
                    Convert.ToInt32(oNode.Attributes["tip"].Value),
                    Convert.ToInt32(oNode.Attributes["dom_zdravlja"].Value),
                    Convert.ToInt32(oNode.Attributes["djelatnost"].Value)
                ));
            }
            oSr.Close();

            return lMedicinsko_osoblje;
        }

        public static List<Tip> ucitajTip()
        {
            string sXml = "";
            StreamReader oSr = new StreamReader(@"C:\VisualStudioProjects\Osnove-programiranja\KV_IvanFranjic2\KV_IvanFranjic2\medicinsko_osoblje.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/tipovi/tip");
            List<Tip> lTip = new List<Tip>();
            foreach (XmlNode oNode in oNodes)
            {
                lTip.Add(new Tip(
                    Convert.ToInt32(oNode.Attributes["id"].Value),
                    oNode.Attributes["naziv"].Value
                ));
            }
            oSr.Close();
            return lTip;
        }

        public static string dajtipPoid(int tip_id)
        {
            List<Tip> lTip = ucitajTip();
            string tip_tip = "";
            foreach (Tip tip in lTip)
            {
                if (tip.id == tip_id)
                {
                    tip_tip = tip.naziv;
                }
            }
            return tip_tip;
        }

        public static string dajgradPoid(int grad_id)
        {
            List<Domovi_zdravlja> lDomovi_zdravlja = ucitajDomove_zdravlja();
            string grad_grad = "";
            foreach (Domovi_zdravlja grad in lDomovi_zdravlja)
            {
                if (grad.id == grad_id)
                {
                    grad_grad = grad.naziv;
                }
            }
            return grad_grad;
        }

        public static string dajdjelatnostPoid(int djelatnost_id)
        {
            List<Djelatnosti> lDjelatnosti = ucitajDjelatnosti();
            string djelatnost_naziv = "";
            foreach (Djelatnosti djelatnost in lDjelatnosti)
            {
                if (djelatnost.id == djelatnost_id)
                {
                    djelatnost_naziv = djelatnost.naziv;
                }
            }
            return djelatnost_naziv;
        }

        //funkcija ispisuje listu Domovi_zdravlja u obliku tablice
        public static void ispisiDomove_zdravlja()
        {
            List<Domovi_zdravlja> lDomovi_zdravlja = ucitajDomove_zdravlja();
            Console.WriteLine();
            Console.WriteLine("\nPRIKAZ SVIH DOMOVA ZDRAVLJA");
            var table = new ConsoleTable("R.br. ", "Grad");
            int rbr = 1;
            foreach (Domovi_zdravlja domzdravlja in lDomovi_zdravlja)
            {
                table.AddRow(rbr++ + ".", domzdravlja.naziv);
            }


            table.Write();
        }

        //funkcija ispisuje listu Djelatnosti u obliku tablice
        public static void ispisiDjelatnosti()
        {
            List<Djelatnosti> lDjelatnosti = ucitajDjelatnosti();
            Console.WriteLine();
            Console.WriteLine("\nPRIKAZ SVIH DJELATNOSTI");
            var table = new ConsoleTable("R.br.", "id", "Naziv djelatnosti");
            int rbr = 1;
            foreach (Djelatnosti djelatnost in lDjelatnosti)
            {
                table.AddRow(rbr++ + ".", djelatnost.id, djelatnost.naziv);
            }

            table.Write();
        }

        //funkcija ispisuje listu Medicinsko_osoblje u obliku tablice
        public static void ispisiMedicinsko_osoblje()
        {
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            Console.WriteLine();
            Console.WriteLine("\nPRIKAZ SVIH ZAPOSLENIKA");
            var table = new ConsoleTable("R.br.", "Ime", "Prezime", "Tip", "Dom zdravlja", "Djelatnost");
            int rbr = 1;
            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                table.AddRow(rbr++ + ".", osoblje.ime, osoblje.prezime, dajtipPoid(osoblje.tip), dajgradPoid(osoblje.dom_zdravlja), dajdjelatnostPoid(osoblje.djelatnost));
            }

            table.Write();
        }

        //funkcija dohvaca podatke za grad Slatinu
        public static void dajSlatinu()
        {
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            Console.WriteLine();
            Console.WriteLine("\nDOM ZDRAVLJA SLATINA");
            var table = new ConsoleTable("R.br.", "Ime", "Prezime", "Tip", "Dom zdravlja", "Djelatnost");
            int rbr = 1;
            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja == 1)
                {
                    table.AddRow(rbr++ + ".", osoblje.ime, osoblje.prezime, dajtipPoid(osoblje.tip), dajgradPoid(osoblje.dom_zdravlja), dajdjelatnostPoid(osoblje.djelatnost));
                }
            }
            table.Write();
        }

        //funkcija dohvaca podatke za grad Viroviticu
        public static void dajViroviticu()
        {
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            Console.WriteLine();
            Console.WriteLine("\nDOM ZDRAVLJA VIROVITICA");
            var table = new ConsoleTable("R.br.", "Ime", "Prezime", "Tip", "Dom zdravlja", "Djelatnost");
            int rbr = 1;
            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja == 2)
                {
                    table.AddRow(rbr++ + ".", osoblje.ime, osoblje.prezime, dajtipPoid(osoblje.tip), dajgradPoid(osoblje.dom_zdravlja), dajdjelatnostPoid(osoblje.djelatnost));
                }
            }
            table.Write();
        }

        //funkcija dohvaca podatke za grad Orahovicu
        public static void dajOrahovicu()
        {
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            Console.WriteLine();
            Console.WriteLine("\nDOM ZDRAVLJA ORAHOVICA");
            var table = new ConsoleTable("R.br.", "Ime", "Prezime", "Tip", "Dom zdravlja", "Djelatnost");
            int rbr = 1;
            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja == 3)
                {
                    table.AddRow(rbr++ + ".", osoblje.ime, osoblje.prezime, dajtipPoid(osoblje.tip), dajgradPoid(osoblje.dom_zdravlja), dajdjelatnostPoid(osoblje.djelatnost));
                }
            }
            table.Write();
        }

        //funkcija dohvaca podatke za grad Nasice
        public static void dajNasice()
        {
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            Console.WriteLine();
            Console.WriteLine("\nDOM ZDRAVLJA NASICE");
            var table = new ConsoleTable("R.br.", "Ime", "Prezime", "Tip", "Dom zdravlja", "Djelatnost");
            int rbr = 1;
            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja == 4)
                {
                    table.AddRow(rbr++ + ".", osoblje.ime, osoblje.prezime, dajtipPoid(osoblje.tip), dajgradPoid(osoblje.dom_zdravlja), dajdjelatnostPoid(osoblje.djelatnost));
                }
            }
            table.Write();
        }

        //funkcija dohvaca podatke za grad Bjelovar
        public static void dajBjelovar()
        {
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            Console.WriteLine();
            Console.WriteLine("\nDOM ZDRAVLJA BJELOVAR");
            var table = new ConsoleTable("R.br.", "Ime", "Prezime", "Tip", "Dom zdravlja", "Djelatnost");
            int rbr = 1;
            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja == 5)
                {
                    table.AddRow(rbr++ + ".", osoblje.ime, osoblje.prezime, dajtipPoid(osoblje.tip), dajgradPoid(osoblje.dom_zdravlja), dajdjelatnostPoid(osoblje.djelatnost));
                }
            }
            table.Write();
        }

        //funkcija za dohvacanje statistike
        public static void dajStatistiku()
        {
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            Console.WriteLine();
            Console.WriteLine("\nPRIKAZ STATISTIKE DJELATNOSTI");
            var table = new ConsoleTable("R.br.", "Djelatnost", "Dom zdravlja");
            int rbr = 1;
            string help = "";
            int counter = 0;
            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja <= 5 && osoblje.djelatnost == 10 && osoblje.tip != 2)
                {
                    help = help + " " + dajgradPoid(osoblje.dom_zdravlja);
                    counter++;
                }
            }

            if (counter != 0)
            {
                table.AddRow(rbr++ + ".", "Opca medicina", help);
            }

            help = "";
            counter = 0;

            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja <= 5 && osoblje.djelatnost == 11 && osoblje.tip != 2)

                {
                    help = help + " " + dajgradPoid(osoblje.dom_zdravlja);
                    counter++;
                }
            }

            if (counter != 0)
            {
                table.AddRow(rbr++ + ".", "Stomatologija", help);
            }

            counter = 0;
            help = "";

            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja <= 5 && osoblje.djelatnost == 12 && osoblje.tip != 2)

                {
                    help = help + " " + dajgradPoid(osoblje.dom_zdravlja);
                    counter++;
                }
            }

            if (counter != 0)
            {
                table.AddRow(rbr++ + ".", "Pedijatrija", help);
            }

            counter = 0;
            help = "";

            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja <= 5 && osoblje.djelatnost == 13 && osoblje.tip != 2)

                {
                    help = help + " " + dajgradPoid(osoblje.dom_zdravlja);
                    counter++;
                }
            }

            if (counter != 0)
            {
                table.AddRow(rbr++ + ".", "Ginekologija", help);
            }

            counter = 0;
            help = "";

            foreach (Medicinsko_osoblje osoblje in lMedicinsko_osoblje)
            {
                if (osoblje.dom_zdravlja <= 5 && osoblje.djelatnost == 14 && osoblje.tip != 2)

                {
                    help = help + " " + dajgradPoid(osoblje.dom_zdravlja);
                    counter++;

                }
            }

            if (counter != 0)
            {
                table.AddRow(rbr++ + ".", "Psihologija", help);
            }


            table.Write();
        }

        //funkcija za prikaz kontrola
        public static void prikaziKontrole()
        {
            Console.WriteLine("\nPritisnite [Q] za povratak u glavni izbornik");
            Console.WriteLine("Pritisnite [X] za izlaz iz programa");
            Console.Write("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);
            /*
             88 - X
             81 - Q
            */
            while (odabir != 81 && odabir != 88)
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno\n");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }
            switch (odabir)
            {
                case 81:
                    prikaziIzbornik();
                    break;

                case 88:
                    System.Environment.Exit(1);
                    break;

            }
        }

        //funkcija za prikaz kontrola drugog izbornika
        public static void prikaziKontrole2()
        {
            Console.WriteLine("Pritisnite [Q] za povratak u glavni izbornik");
            Console.WriteLine("Pritisnite [R] za povratak u prethodni izbornik");
            Console.WriteLine("Pritisnite [X] za izlaz iz programa");
            Console.Write("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);
            /*
             88 - X
             82 - R
             81 - Q
            */
            while (odabir != 81 && odabir != 82 && odabir != 88)
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno\n");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }
            switch (odabir)
            {
                case 81:
                    prikaziIzbornik();
                    break;

                case 82:
                    prikaziIzbornik2();
                    break;

                case 88:
                    System.Environment.Exit(1);
                    break;

            }
        }

        //funkcija za dodavanje osoblja
        public static void dodajOsoblje()
        {
            Console.WriteLine("\nDODAJ DOKTORA/SESTRU\n");

            Console.Write("Ime: ");
            string ime = Console.ReadLine();

            Console.Write("Prezime: ");
            string prezime = Console.ReadLine();

            Console.WriteLine("Tip: ");
            List<Tip> lTip = ucitajTip();

            int rbr = 1;
            foreach (Tip tip in lTip)
            {
                Console.WriteLine("\t" + rbr++ + ". " + tip.naziv);
            }
            Console.Write("\nOdaberite tip: ");
            int tip_odabir = Convert.ToInt32(Console.ReadLine());

            int[] dozvoljeni_unosi = { 1, 2 };
            while (!dozvoljeni_unosi.Contains(tip_odabir))
            {
                Console.Write("\nPogreška pri odabiru tipa.Pokušajte ponovno ");
                Console.Write("\nOdaberite tip: ");
                tip_odabir = Convert.ToInt32(Console.ReadLine());
            }
            int odabrani_tip_id = lTip[tip_odabir - 1].id;

            Console.WriteLine("Dom zdravlja: ");
            List<Domovi_zdravlja> lDomovi_zdravlja = ucitajDomove_zdravlja();

            int rbrr = 1;
            foreach (Domovi_zdravlja dom_zdravlja in lDomovi_zdravlja)
            {
                Console.WriteLine("\t" + rbrr++ + ". " + dom_zdravlja.naziv);
            }
            Console.Write("\nOdaberite redni broj doma zdravlja: ");
            int dom_zdravlja_odabir = Convert.ToInt32(Console.ReadLine());

            int[] dozvoljeni_unosi2 = { 1, 2, 3, 4, 5 };
            while (!dozvoljeni_unosi2.Contains(dom_zdravlja_odabir))
            {
                Console.Write("\nPogreška pri odabiru doma zdravlja.Pokušajte ponovno ");
                Console.Write("\nOdaberite redni broj doma zdravlja: ");
                dom_zdravlja_odabir = Convert.ToInt32(Console.ReadLine());
            }
            int odabrani_dom_zdravlja_id = lDomovi_zdravlja[dom_zdravlja_odabir - 1].id;

            Console.WriteLine("Djelatnost: ");
            List<Djelatnosti> lDjelatnosti = ucitajDjelatnosti();
            int rbrrr = 1;
            foreach (Djelatnosti djelatnost in lDjelatnosti)
            {
                Console.WriteLine("\t" + rbrrr++ + ". " + djelatnost.naziv);
            }
            Console.Write("\nOdaberite redni broj djelatnosti: ");
            int Djelatnost_odabir = Convert.ToInt32(Console.ReadLine());

            int[] dozvoljeni_unosi3 = { 1, 2, 3, 4, 5 };
            while (!dozvoljeni_unosi3.Contains(Djelatnost_odabir))
            {
                Console.Write("\nPogreška pri odabiru djelatnosti.Pokušajte ponovno ");
                Console.Write("\nOdaberite redni broj djelatnosti: ");
                Djelatnost_odabir = Convert.ToInt32(Console.ReadLine());
            }
            int odabrana_djelatnost_id = lDjelatnosti[Djelatnost_odabir - 1].id;

            Medicinsko_osoblje osoblje=new Medicinsko_osoblje(ime, prezime, odabrani_tip_id, odabrani_dom_zdravlja_id, odabrana_djelatnost_id);
            List<Medicinsko_osoblje> lMedicinsko_osoblje = ucitajMedicinsko_osoblje();
            lMedicinsko_osoblje.Add(osoblje);
            string sXml = "";
            StreamReader oSr = new StreamReader(@"C:\VisualStudioProjects\Osnove-programiranja\KV_IvanFranjic2\KV_IvanFranjic2\medicinsko_osoblje.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNode oNodes = oXml.SelectSingleNode("//data/medicinsko_osoblje");
            XmlElement xmlMedicinsko_osoblje = oXml.CreateElement("osoblje");
            xmlMedicinsko_osoblje.SetAttribute("ime", ime);
            xmlMedicinsko_osoblje.SetAttribute("prezime", prezime);
            xmlMedicinsko_osoblje.SetAttribute("tip", odabrani_tip_id.ToString());
            xmlMedicinsko_osoblje.SetAttribute("dom_zdravlja", odabrani_dom_zdravlja_id.ToString());
            xmlMedicinsko_osoblje.SetAttribute("djelatnost", odabrana_djelatnost_id.ToString());
            oNodes.AppendChild(xmlMedicinsko_osoblje);
            oXml.Save(@"C:\VisualStudioProjects\Osnove-programiranja\KV_IvanFranjic2\KV_IvanFranjic2\medicinsko_osoblje.xml");
            Console.WriteLine("*** OSOBLJE USPJEŠNO DODANO ***");

        }

        }
    }

