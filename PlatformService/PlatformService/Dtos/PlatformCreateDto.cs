using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos;

public class PlatformCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
}