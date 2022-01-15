using AutoMapper;
using System.Collections.Generic;

namespace Hub.BackgroundJob.Main.Middlewares
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullDestinationValues = true;
            this.AddPOMappingProfile();
        }
    }

    public class AutoMapperConfiguration
    {
        public static List<Profile> RegisterMappings()
        {
            return new List<Profile> { new MappingProfile() };
        }
    }
}