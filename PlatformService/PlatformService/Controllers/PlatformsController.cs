using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Repository;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo<Platform> _repo;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo<Platform> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("Getting platforms...");

        var platformItem = _repo.Get(orderBy: x => x.OrderBy(p => p.Name));

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
    }
}