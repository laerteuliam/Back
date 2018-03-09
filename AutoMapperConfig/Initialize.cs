using AutoMapper;

namespace AutoMapperConfig
{
    public class Initialize
    {
        public static void Bootstrap()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<ProjetoProfile>();
            });
        }
    }

}
