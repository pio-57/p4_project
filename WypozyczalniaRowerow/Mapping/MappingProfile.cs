using AutoMapper;
using WypozyczalniaRowerow.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Klient, KlientDto>()
            .ForMember(dest => dest.PelnaNazwa,
                       opt => opt.MapFrom(src => $"{src.Imie} {src.Nazwisko}"));

        CreateMap<Rower, RowerDto>()
            .ForMember(dest => dest.Opis,
                       opt => opt.MapFrom(src => $"{src.Marka} {src.Model} ({src.Typ})"));
    }
}