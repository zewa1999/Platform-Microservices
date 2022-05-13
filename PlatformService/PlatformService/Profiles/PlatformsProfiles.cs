using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PlatformProfiles : Profile
{
    public PlatformProfiles()
    {
        // Source to target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }
}