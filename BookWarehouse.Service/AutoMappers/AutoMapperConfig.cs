using AutoMapper;

namespace BookWarehouse.Service.AutoMappers
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOs());
                cfg.AddProfile(new DTOsToDomain());
            });
        }
    }
}