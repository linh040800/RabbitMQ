using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO.BackgroundJob.Main.Middlewares
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