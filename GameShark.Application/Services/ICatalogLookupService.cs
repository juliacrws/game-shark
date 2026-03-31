using GameShark.Domain.Entities;

namespace GameShark.Application.Services;

public interface ICatalogLookupService
{
    Task<IEnumerable<Categoria>> GetCategoriasAsync();
    Task<IEnumerable<Plataforma>> GetPlataformasAsync();
}