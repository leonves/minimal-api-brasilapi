using AutoMapper;
using BrasilApiIntegration.Data.Entities;
using BrasilApiIntegration.Data.Entities.Core;
using BrasilApiIntegration.Model.Core;
using BrasilApiIntegration.Model.Response;

namespace BrasilApiIntegration.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EntityResponse, Entity>()
                .IncludeAllDerived()
                .ForMember(entity => entity.Id,
                    opt => opt.MapFrom(viewModel =>
                        string.IsNullOrEmpty(viewModel.Id)
                            ? Guid.NewGuid()
                            : Guid.Parse(viewModel.Id)));

            CreateMap<WeatherResponse, Weather>();
        }
    }
}
