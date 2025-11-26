using Microsoft.Data.SqlClient;

namespace RealTimeStockSimulator.Models.Interfaces
{
    public interface IDataMapper
    {
        UserAccount MapUser(SqlDataReader reader);
        Tradable MapTradable(SqlDataReader reader);
        OwnershipTradable MapOwnershipTradable(SqlDataReader reader);
        MarketTransactionTradable MapMarketTransactionTradable(SqlDataReader reader);
        OwnershipTradable MapOwnershipTradableByTradable(Tradable tradable, int amount);

    }
}
