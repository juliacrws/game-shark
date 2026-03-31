using GameShark.Application.Abstractions.Repositories;
using GameShark.Application.Services;
using GameShark.Domain.Entities;

namespace GameShark.Infrastructure.Services;

public class CatalogLookupService : ICatalogLookupService
{
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly IPlataformaRepository _plataformaRepo;

    public CatalogLookupService(ICategoriaRepository categoriaRepo, IPlataformaRepository plataformaRepo)
    {
        _categoriaRepo = categoriaRepo;
        _plataformaRepo = plataformaRepo;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasAsync() => await _categoriaRepo.GetAllAsync();

    public async Task<IEnumerable<Plataforma>> GetPlataformasAsync() => await _plataformaRepo.GetAllAsync();
}