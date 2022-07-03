namespace OnlineStore.Catalog.API.Configurations.Registrars
{
    public interface IRegistrar
    {
        void RegisterServices(WebApplicationBuilder builder);
    }
}
