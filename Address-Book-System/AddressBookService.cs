using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Address_Book_System
{
    internal class AddressBookService
    {

        private List<Contact> contacts = new List<Contact>();



        private List<Contact> contactsToAdd = new List<Contact>()
        {
            new Contact { FirstName = "Vinay",  LastName = "K",     Address = "Street 1", City = "Bengaluru", State = "KA", Zip = "560001", PhoneNumber = "9876543210", Email = "vinay@example.com" },
            new Contact { FirstName = "Vijay",  LastName = "P",     Address = "Street 2", City = "Mysuru",    State = "KA", Zip = "570001", PhoneNumber = "9876543211", Email = "vijay@example.com" },
            new Contact { FirstName = "Rahul",  LastName = "R",     Address = "Street 3", City = "Hubli",     State = "KA", Zip = "580001", PhoneNumber = "9876543212", Email = "rahul@example.com" },
            new Contact { FirstName = "Kiran",  LastName = "S",     Address = "Street 4", City = "Tumkur",    State = "KA", Zip = "572101", PhoneNumber = "9876543213", Email = "kiran@example.com" },
            new Contact { FirstName = "Ramesh", LastName = "M",     Address = "Street 5", City = "Hassan",    State = "KA", Zip = "573201", PhoneNumber = "9876543214", Email = "ramesh@example.com" },
            new Contact { FirstName = "Suresh", LastName = "T",     Address = "Street 6", City = "Mangaluru", State = "KA", Zip = "575001", PhoneNumber = "9876543215", Email = "suresh@example.com" },
            new Contact { FirstName = "Deepak", LastName = "A",     Address = "Street 7", City = "Udupi",     State = "KA", Zip = "576101", PhoneNumber = "9876543216", Email = "deepak@example.com" },
            new Contact { FirstName = "Arjun",  LastName = "V",     Address = "Street 8", City = "Kolar",     State = "KA", Zip = "563101", PhoneNumber = "9876543217", Email = "arjun@example.com" },
            new Contact { FirstName = "Shiva",  LastName = "N",     Address = "Street 9", City = "Belagavi",  State = "KA", Zip = "590001", PhoneNumber = "9876543218", Email = "shiva@example.com" },
            new Contact { FirstName = "Manoj",  LastName = "B",     Address = "Street 10",City = "Davangere", State = "KA", Zip = "577001", PhoneNumber = "9876543219", Email = "manoj@example.com" }
        };


        public void AddMultipleContacts()
        {
            foreach (var contact in contactsToAdd)
            {
                contacts.Add(contact);
                addToFile(contact);
            }   
        }

        public static void addToFile(Contact contact)
        {
            try
            {

                // to store to json file formate
                string filePathJson = "E:\\.Net programming\\Address-Book-System\\Address-Book-System\\contacts.json";

                string jsonString = JsonSerializer.Serialize(contact, new JsonSerializerOptions { WriteIndented = true });

                File.AppendAllText(filePathJson, jsonString);

                string filePathCsv = "E:\\.Net programming\\Address-Book-System\\Address-Book-System\\contacts.csv";

                using (StreamWriter writer = new StreamWriter(filePathCsv, append: true))
                { 
                    writer.WriteLine($"{contact.FirstName},{contact.LastName},{contact.Address},{contact.City},{contact.State},{contact.Zip},{contact.PhoneNumber},{contact.Email}");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        } 






        // UC2 – Add Contact
        public void AddContact()
        {
            Contact c = new Contact();

            Console.Write("Enter First Name: ");
            c.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            c.LastName = Console.ReadLine();

            Console.Write("Enter Address: ");
            c.Address = Console.ReadLine();

            Console.Write("Enter City: ");
            c.City = Console.ReadLine();

            Console.Write("Enter State: ");
            c.State = Console.ReadLine();


            while (true)
            {
                Console.Write("Enter Zip: ");
                c.Zip = Console.ReadLine();

                try
                {
                    if (c.Zip.Length != 6)
                    {
                        throw new InvalidZipException("Zip code must be exactly 6 digits long.");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (InvalidZipException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }



            while (true)
            {
                Console.Write("Enter Phone Number: ");
                c.PhoneNumber = Console.ReadLine();

                if (!MobileNumber(c.PhoneNumber))
                {
                    Console.WriteLine("Please enter a valid 10-digit Mobile Number.");
                }
                else
                {
                    break; // valid -> exit loop
                }
            }

            bool isEmailValid = false;
            while (true)
            {
                Console.Write("Enter Email: ");
                c.Email = Console.ReadLine();
                if ( isEmailValid = !EmailValid(c.Email))
                {
                    Console.WriteLine("Email not in the proper format");
                    Console.WriteLine("Please enter valid email");
                }
                if (!isEmailValid)
                    break;
            }

            contacts.Add(c);
            addToFile(c);

            Console.WriteLine("Contact Added Successfully!");
        }

        // UC3 – Edit Contact
        public void EditContact(string name)
        {
            Contact contact = contacts.FirstOrDefault(c => c.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (contact == null)
            {
                Console.WriteLine("Contact Not Found!");
                return;
            }

            Console.Write("Enter New Address: ");
            contact.Address = Console.ReadLine();

            Console.Write("Enter New City: ");
            contact.City = Console.ReadLine();

            Console.Write("Enter New State: ");
            contact.State = Console.ReadLine();

            Console.Write("Enter New Zip: ");
            contact.Zip = Console.ReadLine();

            while (true)
            {

                Console.Write("Enter Phone Number: ");
                contact.PhoneNumber = Console.ReadLine();


                if (!MobileNumber(contact.PhoneNumber))
                {
                    Console.WriteLine("Please enter correct Mobile Number");
                }
                else
                    break;
            }

            bool isEmailValid = false;
            while (true)
            {
                Console.Write("Enter Email: ");
                contact.Email = Console.ReadLine();
                if (isEmailValid = !EmailValid(contact.Email))
                {
                    Console.WriteLine("Email not in the proper format");
                    Console.WriteLine("Please enter valid email");
                }
                if (!isEmailValid)
                    break;
            }

            Console.WriteLine("Contact Updated Successfully!");
        }

        // UC4 – Delete Contact
        public void DeleteContact(string name)
        {
            var person = contacts.FirstOrDefault(c => c.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (person == null)
            {
                Console.WriteLine("Contact Not Found!");
                return;
            }

            contacts.Remove(person);
            Console.WriteLine("Contact Deleted Successfully!");
        }

        // UC5 – Display Contacts
        public void DisplayContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No Contacts Available.");
                return;
            }

            foreach (var c in contacts)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine(c);
            }
        }
        // uc 6 regex 
        public bool EmailValid(string email)
        {

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            

            return Regex.IsMatch(email, emailPattern);
        }

        public bool MobileNumber(string Number)
        {
            string pattern = @"^[0-9]{10}$";
            return Regex.IsMatch(Number, pattern);
        }


// uc 7 : LINQ
        public List<Contact> SortedBasedName()
        {
            return contacts.OrderBy(con => con.FirstName)
                .ToList();
        }

        public List<Contact> NameStartWith(string first)
        {
            return contacts.Where(con => con.FirstName.StartsWith(first))
                .ToList();
        }

        public List<Contact> FromSameState(string stateName)
        {
            return contacts.Where(con => con.State.Equals(stateName, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }




    }
}
