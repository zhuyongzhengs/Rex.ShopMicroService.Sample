using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Authorizations;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.OpenIddict.Tokens;

namespace Rex.AuthService.EntityFrameworkCore;

[ConnectionStringName(AuthServiceConsts.ConnectionStringName)]
public class AuthServiceDbContext :
    AbpDbContext<AuthServiceDbContext>,
    IOpenIddictDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region OpenIddict授权

    public DbSet<OpenIddictApplication> Applications { get; set; }

    public DbSet<OpenIddictAuthorization> Authorizations { get; set; }

    public DbSet<OpenIddictScope> Scopes { get; set; }

    public DbSet<OpenIddictToken> Tokens { get; set; }

    #endregion OpenIddict授权

    public AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region 授权服务OpenIddict

        builder.ConfigureOpenIddict();

        #endregion 授权服务OpenIddict

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(AuthServiceConsts.DbTablePrefix + "YourEntities", AuthServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}