using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO.BackgroundJob.Main.Middlewares
{
    public partial class MappingProfile : Profile
    {
        public void AddCoreMappingProfile()
        {
            //#region Mapping Sys_Parameter
            //CreateMap<Sys_Parameter, Sys_ParameterDto>().IgnoreNoMap();
            //CreateMap<Sys_ParameterDto, Sys_Parameter>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ExtensionGuid.ToGuid(src.Id)))
            //    .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => ExtensionGuid.ToGuid(src.TenantId)));

            //CreateMap<Sys_Parameter_Detail, Sys_ParameterDetailtDto>().IgnoreNoMap();
            //#endregion    
        }
    }
}
