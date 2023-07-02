using Application;
using AutoMapper;
using Domain;

namespace TreeNodeApi.Mappers;

/// <summary>
/// Mapping profiles.
/// </summary>
public class MappingProfiles : Profile
{
    /// <summary>
    /// Mapping profiles constructor.
    /// </summary>
    public MappingProfiles()
    {
        CreateMap<Journal, JournalDto>();
    }
}