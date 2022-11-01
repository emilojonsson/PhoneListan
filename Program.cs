using System;
using System.IO;
using System.Net.Sockets;

namespace dtp5_contacts_0
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, birthdate;
            public string[] phone = new string[20], address = new string[20];
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
            Console.WriteLine("  load          - load and print contact list data from the file address.lis");
            Console.WriteLine("  load /file/   - load and print contact list data from the file");
            Console.WriteLine("  new           - create new person");
            Console.WriteLine("  quit          - quit the program");
            Console.WriteLine("  save          - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/   - save contact list data to the file specified");
            Console.WriteLine("  delete        - emtpy the contact list to the file previously loaded");
            Console.WriteLine("  delete /file/ - empty the contact list to the file specified");
            Console.WriteLine();
        }
        static void FileRead(string fileName)
        {
            using (StreamReader infile = new StreamReader(fileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] attrs = line.Split('|');
                    Person p = new Person();
                    p.persname = attrs[0];
                    p.surname = attrs[1];
                    p.phone = attrs[2].Split(';');
                    p.address = attrs[3].Split(';');
                    p.birthdate = attrs[4];
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
        static string PrintPhoneandAdress(string[] arr)
        {
            string temp = "";
            int index = 0;
            while (arr[index] != null && index < arr.Length)
            {
                temp += arr[index] + ";";
                if (index == arr.Length - 1)
                    break; 
                else 
                    index++;
            }
            string newtemp = temp.Remove(temp.Length - 1);
            return newtemp;
        }
        static void FileWrite(string command, string fileName)
        {
            using (StreamWriter outfile = new StreamWriter(fileName))
            {
                if (command == "save")
                {
                    foreach (Person p in contactList)
                    {
                        if (p != null)
                            outfile.WriteLine($"{p.persname}|{p.surname}|{PrintPhoneandAdress(p.phone)}|{PrintPhoneandAdress(p.address)}|{p.birthdate}");
                    }
                }
                else
                {
                    foreach (Person p in contactList)
                    {
                        if (p != null)
                            outfile.WriteLine();
                    }
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
                    Console.Write("Personal name: ");
                    p.persname = Console.ReadLine();
                    Console.Write("Surname: ");
                    p.surname = Console.ReadLine();
                    int index = 0;
                    do
                    {
                        Console.Write("Phone: (ange flera, avsluta med enter) ");
                        p.phone[index] = Console.ReadLine();
                        index++;
                    } while (p.phone[index - 1] != "");
                    index = 0;
                    do
                    {
                        Console.Write("Adress: (ange flera, avsluta med enter) ");
                        p.address[index] = Console.ReadLine();
                        index++;
                    } while (p.address[index - 1] != "");

                    contactList[i] = p;
                    Console.Write("ange födelsedatum: ");
                    p.birthdate = Console.ReadLine();
                    break;
                }
            }
        }
        public static void Main(string[] args)
        {
            //List<string> list = new List<string>();
            string fileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            PrintMenu();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit") { }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2) 
                        FileRead("address.lis");
                    else 
                        FileRead(commandLine[1]);
                }
                else if (commandLine[0] == "save" || commandLine[0] == "delete")
                {
                    if (commandLine.Length < 2)
                        FileWrite(commandLine[0], fileName);
                    else
                        FileWrite(commandLine[0], commandLine[1]);
                }
                else if (commandLine[0] == "new") 
                    AddNewPerson();
                else Console.WriteLine($"Unknown command: '{commandLine[0]}'");
            } while (commandLine[0] != "quit");
            Console.WriteLine("You have left the contact list");
        }
    }
}
