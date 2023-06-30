using Application;
using Application.Commands;
using AutoMapper;
using Domain;

namespace TreeNodeApi.Mappers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GetJournalRangeRequest, GetJournalRangeCommand>()
            .ForMember(d => d.FilterFrom, o => o.MapFrom(s => s.Filter.From))
            .ForMember(d => d.FilterTo, o => o.MapFrom(s => s.Filter.To));

        CreateMap<Journal, JournalDto>();
    }
}