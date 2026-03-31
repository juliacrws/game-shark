using GameShark.Application.Abstractions.Repositories;
using GameShark.Domain.Entities;
using GameShark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Infrastructure.Repositories;

public class EfPlataformaRepository : IPlataformaRepository
{
    private readonly AppDbContext _context;
    public EfPlataformaRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Plataforma>> GetAllAsync()
    {
        return await _context.Plataformas.OrderBy(p => p.Nome).ToListAsync();
    }
}