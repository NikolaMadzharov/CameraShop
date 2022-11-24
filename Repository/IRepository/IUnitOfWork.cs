namespace TechRentingSystem.Repository.Repository
{
    public interface IUnitOfWork 
    {
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IProductRepository Product  { get; }

        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader{ get; }
        IReviewRepository Review { get; }
        void Save();
    }
}
