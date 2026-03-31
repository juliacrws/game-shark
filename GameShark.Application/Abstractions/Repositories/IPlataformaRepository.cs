using GameShark.Domain.Entities;

namespace GameShark.Application.Abstractions.Repositories;

public interface IPlataformaRepository
{
    Task<IEnumerable<Plataforma>> GetAllAsync();
}