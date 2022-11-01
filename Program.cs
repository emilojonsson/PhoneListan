using System;
using System.IO;

namespace dtp5_contacts_0
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, phone, address, birthdate;
            public string age { get; set; }
            public Person()
            {
                age = "10";
            }
        public void Get(string str)
        {
                Console.WriteLine(age);
        }

        }
        static void PrintMenu()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  load        - load and print contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load and print contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  delete       - emtpy the contact list");
            Console.WriteLine("  delete /persname/ /surname/ - delete a person");
            Console.WriteLine();
        }
        static void FileRead(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] attrs = line.Split('|');
                    Person p = new Person();
                    p.persname = attrs[0];
                    p.surname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    p.phone = phones[0];
                    string[] addresses = attrs[3].Split(';');
                    p.address = addresses[0];
                    for (int ix = 0; ix < contactList.Length; ix++)
                    {
                        if (contactList[ix] == null)
                        {
                            contactList[ix] = p;
                            break;
                        }
                    }
                }
            }
        }
        static void FileWrite(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(lastFileName))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate}");
                }
            }
        }
        static void AddNewPerson()
        {
            for (int i = 0; i < contactList.Length; i++)
            {
                if (contactList[i] == null)
                {
                    Person p = new Person();
                    Console.Write("personal name: ");
                    p.persname = Console.ReadLine();
                    Console.Write("surname: ");
                    p.surname = Console.ReadLine();
                    Console.Write("phone: ");
                    p.phone = Console.ReadLine();
                    contactList[i] = p;
                    break;
                }

            }
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            PrintMenu();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                {
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2)
                    {
                        lastFileName = "address.lis";
                        FileRead(lastFileName);
                    }
                    else
                    {
                        lastFileName = commandLine[1];
                        FileRead(lastFileName);
                    }
                    Console.WriteLine(contactList[0].age);

                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        FileWrite(lastFileName);
                    }
                    else
                    {
                        FileWrite(commandLine[1]);
                    }
                }
                else if (commandLine[0] == "new")
                {
                    AddNewPerson();
                }
                else if (commandLine[0] == "help")
                {
                    PrintMenu();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit");
            Console.WriteLine("You have left the contact list");
            Console.ReadKey();
        }

    }
}
