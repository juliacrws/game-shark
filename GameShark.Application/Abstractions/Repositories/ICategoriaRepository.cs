using GameShark.Domain.Entities;

namespace GameShark.Application.Abstractions.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetAllAsync();
}