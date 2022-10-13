using AutoMapper;
using CarRental.Application.Dto;
using CarRental.Domain;

namespace CarRental.Application.Profiles;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        CreateMap<CarDto, Car>()
            .ForMember(m => m.CarRentalId, c => c.MapFrom(s => s.CarRentalId))
            .ForMember(m => m.CarBrandId, c => c.MapFrom(s => s.CarBrandId));
    }
}