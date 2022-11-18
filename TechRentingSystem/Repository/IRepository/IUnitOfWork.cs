﻿namespace TechRentingSystem.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IProductRepository Product  { get; }
        void Save();
    }
}
