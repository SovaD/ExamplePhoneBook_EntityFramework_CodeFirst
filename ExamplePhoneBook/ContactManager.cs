using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ExamplePhoneBook
{
    public class ContactManager
    {
        private ContactDbContext dbContext;

        public ContactManager()
        {
            dbContext = new ContactDbContext();
        }

        public void AddContact(Contact contact)
        {
            dbContext.Contacts.Add(contact);
            dbContext.SaveChanges();
        }

        public void UpdateContact(int id, Contact newContact)
        {
            Contact contact = dbContext.Contacts.Find(id);
            if (contact != null)
            {
                contact.name = newContact.name;
                contact.email = newContact.email;
                contact.phoneNumber = newContact.phoneNumber;
                contact.groups = newContact.groups;

                dbContext.SaveChanges();
            }
        }

        public void DeleteContact(int id)
        {
            Contact contact = dbContext.Contacts.Find(id);
            if (contact != null)
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
            }
        }

        public List<Contact> GetContacts()
        {
            try
            {
                var contacts = dbContext.Contacts.ToList();
                return contacts;
            }
            catch (Exception ex)
            {
                return new List<Contact>();
            }
            
        }

        public List<Contact> GetContacts(string value)
        {
            return dbContext.Contacts
                .Where(x => x.groups.Contains(value)
            || x.name.Contains(value)
            || x.email.Contains(value))
            .ToList();
        }

        internal void EditContact(int id, Contact newContact)
        {
            newContact.id = id;
            dbContext.Contacts.AddOrUpdate(newContact);
            dbContext.SaveChanges();
        }

        internal Contact GetContact(int id)
        {
            return dbContext.Contacts.Find(id);
        }
    }

}