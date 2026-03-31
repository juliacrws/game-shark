using GameShark.Application.DTOs;
using GameShark.Application.Filters;
using GameShark.Domain.Entities;

namespace GameShark.Application.Abstractions.Repositories;

public interface IProdutoRepository
{
    Task<Paging<Produto>> GetPagedProdutosAsync(ProdutoFilter filter);
    Task<Produto?> GetByIdAsync(int id);
}