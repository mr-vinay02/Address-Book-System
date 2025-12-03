using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Address_Book_System
{
    internal class AddressBookService
    {

        private List<Contact> contacts = new List<Contact>();

        
        

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
        // uc 5 regex 
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
