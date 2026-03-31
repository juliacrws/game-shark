using GameShark.Application.Abstractions.Repositories;
using GameShark.Domain.Entities;
using GameShark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Infrastructure.Repositories;

public class EfCategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;
    public EfCategoriaRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.OrderBy(c => c.Nome).ToListAsync();
    }
}