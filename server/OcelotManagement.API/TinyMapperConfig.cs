
namespace OcelotManagement.API
{
    public class TinyMapperConfig
    {
        internal static void Configure()
        {
            Application.Mappings.MapperConfig.Configure();
            Data.Mappings.MapperConfig.Configure();
        }
    }
}
