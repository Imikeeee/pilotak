using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        List<Pilot> pilots = new List<Pilot>();

        // 2. 
        using (StreamReader sr = new StreamReader("pilotak.csv"))
        {
            string headerLine = sr.ReadLine();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split(';');

                string name = data[0];
                DateTime birthDate = DateTime.Parse(data[1]);
                string nationality = data[2];
                string raceNumber = data[3];

                Pilot pilot = new Pilot(name, birthDate, nationality, raceNumber);
                pilots.Add(pilot);
            }
        }

        // 3. 
        int rowCount = pilots.Count;
        Console.WriteLine("3. feladat: Az állomány {0} adatsort tartalmaz.", rowCount);

        // 4.
        string lastPilotName = pilots[rowCount - 1].Name;
        Console.WriteLine("4. feladat: Az állomány utolsó sorában {0} neve szerepel.", lastPilotName);

        // 5. 
        Console.WriteLine("5. feladat: A 19. században született pilóták:");
        foreach (Pilot pilot in pilots)
        {
            if (pilot.BirthDate.Year < 1901)
            {
                Console.WriteLine("{0} - {1}", pilot.Name, pilot.BirthDate.ToString("yyyy.MM.dd"));
            }
        }

        // 6. 
        string smallestRaceNumber = GetSmallestRaceNumber(pilots);
        string nationalityOfSmallestRaceNumber = GetNationalityByRaceNumber(pilots, smallestRaceNumber);
        Console.WriteLine("6. feladat: A legkisebb értékű rajtszámú pilóta nemzetisége: {0}", nationalityOfSmallestRaceNumber);

        // 7. 
        List<string> uniqueRaceNumbers = GetUniqueRaceNumbers(pilots);
        Console.WriteLine("7. feladat: A következő rajtszámok szerepelnek az állományban:");
        foreach (string raceNumber in uniqueRaceNumbers)
        {
            Console.WriteLine(raceNumber);
        }

        Console.ReadLine();
    }

    static string GetSmallestRaceNumber(List<Pilot> pilots)
    {
        string smallestRaceNumber = null;
        foreach (Pilot pilot in pilots)
        {
            if (!string.IsNullOrEmpty(pilot.RaceNumber))
            {
                if (smallestRaceNumber == null || string.Compare(pilot.RaceNumber, smallestRaceNumber) < 0)
                {
                    smallestRaceNumber = pilot.RaceNumber;
                }
            }
        }
        return smallestRaceNumber;
    }

    static string GetNationalityByRaceNumber(List<Pilot> pilots, string raceNumber)
    {
        foreach (Pilot pilot in pilots)
        {
            if (pilot.RaceNumber == raceNumber)
            {
                return pilot.Nationality;
            }
        }
        return null;
    }

    static List<string> GetUniqueRaceNumbers(List<Pilot> pilots)
    {
        List<string> uniqueRaceNumbers = new List<string>();
        foreach (Pilot pilot in pilots)
        {
            if (!string.IsNullOrEmpty(pilot.RaceNumber) && !uniqueRaceNumbers.Contains(pilot.RaceNumber))
            {
                uniqueRaceNumbers.Add(pilot.RaceNumber);
            }
        }
        return uniqueRaceNumbers;
    }
}

class Pilot
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Nationality { get; set; }
    public string RaceNumber { get; set; }

    public Pilot(string name, DateTime birthDate, string nationality, string raceNumber)
    {
        Name = name;
        BirthDate = birthDate;
        Nationality = nationality;
        RaceNumber = raceNumber;
    }
}
