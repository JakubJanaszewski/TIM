using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos;
public class CoordinateDto
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Coordinate, CoordinateDto>();
        }
    }
}
