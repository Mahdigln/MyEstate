using DataLayer.Entities.Estate;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context;

public class MyEstateContext:DbContext
{
    public MyEstateContext(DbContextOptions<MyEstateContext> options) : base(options)
    {

    }

    #region Estate

    public DbSet<Estate> Estates { get; set; }
    public DbSet<EstateFeature> EstateFeatures { get; set; }
    public DbSet<EstateImage> EstateImages { get; set; }
    public DbSet<EstateType> EstateTypes { get; set; }

    #endregion

    #region User

    public DbSet<User> Users { get; set; }

    #endregion
}