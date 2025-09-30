using AutoMapper;
using Bainah.Core.DTOs;
using Bainah.Core.DTOs.Pages;
using Bainah.Core.Entities;
using Core.Entities;
using System.Globalization;

namespace Bainah.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Region, RegionDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetLocalizedField(src, "NameAr", "NameEn")))
            .ReverseMap();

        CreateMap<City, CityDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetLocalizedField(src, "NameAr", "NameEn")))
            .ReverseMap();

        CreateMap<Country, CountryDto>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetLocalizedField(src, "NameAr", "NameEn")))
           .ReverseMap();

        CreateMap<Nationality, NationalityDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetLocalizedField(src, "NameAr", "NameEn")))
            .ReverseMap();

        CreateMap<PageSection, PageSectionDto>();
        CreateMap<PageSectionPhoto, PageSectionPhotoDto>();
    }

    private string? GetLocalizedField<T>(T source, string primaryField, string fallbackField) where T : class
    {
        var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var value = source?.GetType().GetProperty(primaryField)?.GetValue(source) as string;

        // If the primary field is empty, try the fallback field
        if (!culture.Contains("ar"))
        {
            value = source?.GetType().GetProperty(fallbackField)?.GetValue(source) as string;
        }

        return value;
    }
}