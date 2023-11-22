using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(ApplicationDbContext db, ILogger<DutchRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called ...");

                return _db.Products
                    .OrderBy(p => p.Artist)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _db.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public ContactModel CreateContactFormEntry(ContactModel contact) 
        {
            contact.CreateDate = DateTime.Now;

            _db.ContactFormEntries.Add(contact);
    
            //contact.CreateDate = created.ToLocalTime().ToLongDateString() + " " + created.ToLocalTime().ToLongTimeString();

            if(SaveAll())
            {
                return contact;
            }

            return null; 
        }

        public bool SaveAll()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
