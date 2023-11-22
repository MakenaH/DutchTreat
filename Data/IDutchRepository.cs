using DutchTreat.Data.Entities;
using DutchTreat.Models;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        ContactModel CreateContactFormEntry(ContactModel contact);
    }
}