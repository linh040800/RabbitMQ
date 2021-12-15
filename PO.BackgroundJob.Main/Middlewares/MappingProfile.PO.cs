using AutoMapper;
using PO.BackgroundJob.Entities;

namespace PO.BackgroundJob.Main.Middlewares
{
    public partial class MappingProfile : Profile
    {
        public void AddPOMappingProfile()
        {
            //#region Mapping Sys_Parameter
            //CreateMap<Sys_Parameter, Sys_ParameterDto>().IgnoreNoMap();
            //CreateMap<Sys_ParameterDto, Sys_Parameter>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ExtensionGuid.ToGuid(src.Id)))
            //    .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => ExtensionGuid.ToGuid(src.TenantId)));

            //CreateMap<Sys_Parameter_Detail, Sys_ParameterDetailtDto>().IgnoreNoMap();
            //#endregion    

            CreateMap<PO_Orders, PO_OrdersDto>().ReverseMap();
        }
    }
}
