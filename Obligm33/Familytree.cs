using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligm33
{
    public class Familytree
    {
        public Person[] People { get; set; }
        //public static string CommandPromt;
        public List<Person> alleSammen = new List<Person>();


        public Familytree(params Person[] People)
        {

            foreach (var person in People)
            {
                alleSammen.Add(person);
            }

            foreach (Person forelder in alleSammen)
            {
                foreach (Person barn in alleSammen)
                {
                    if (barn.Father == forelder) forelder.listeoverVoksenesbarn.Add(barn);
                    if (barn.Mother == forelder) forelder.listeoverVoksenesbarn.Add(barn);
                }
            }

        }


        private static string welcomeMessage = "Hello Folkens til vår kjempekult slektstre-program!";

        public string infoString =
            "hjelp => viser en hjelpetekst som forklarer alle kommandoene \n liste => lister alle personer med id, fornavn, fødselsår, dødsår og navn og id på mor og far om det finnes registrert.\n vis<id> => viser en bestemt person med mor, far og barn(og id for disse, slik at man lett kan vise en av dem)\n";

        public string WelcomeMessage
        {
            get { return welcomeMessage; }
        }

   

        public string HandleCommand(string commands)
        {
            if (commands == "hjelp")
            {
                return
                    "hjelp => viser en hjelpetekst som forklarer alle kommandoene \n liste => lister alle personer med id, fornavn, fødselsår, dødsår og navn og id på mor og far om det finnes registrert.\n vis<id> => viser en bestemt person med mor, far og barn(og id for disse, slik at man lett kan vise en av dem) ";
            }

            if (commands == "liste")
            {
                return ListeCommand(alleSammen);
            }

            if (commands.Contains("vis "))
            {
                return VisPersonId(commands);
            }

            else return "FailCommando";

            }




        private static string ListeCommand(List<Person> People)
        {
            string tekst = string.Empty;
            foreach (var i in People)
            {
                tekst += i.GetDescription() + "\n";
            }

            return tekst;

        }

        public string VisPersonId(string commands)
        {
            //var children = new List<Person>();
            var tekst = "";
            var søkeID = Int32.Parse(commands.Substring(4));

            foreach (var Voksen in alleSammen)
            {
                // Finner de voksene
                if (Voksen.Id == søkeID)
                {
                    // legger til beskrivelse av riktige voksen
                    tekst += Voksen.GetDescription() + "\n";
                    if (Voksen.listeoverVoksenesbarn.Count > 0) 
                    {
                        tekst += "  Barn:\n";
                        foreach (var barn in Voksen.listeoverVoksenesbarn)
                        {
                            tekst += $"    {barn.FirstName} (Id={barn.Id}) Født: {barn.BirthYear}\n";
                            //Sverre Magnus(Id= 1) Født: 2005\n"
                        }
                    }
                }
            }
            return tekst;
        }

    }

}