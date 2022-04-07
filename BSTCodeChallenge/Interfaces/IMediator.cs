using BSTCodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSTCodeChallenge.Interface
{
    public interface IMediator
    {
        Task<ActionResult<ProductResponse>> Save(ProductRequest product);
        Task<ActionResult<ProductResponse>> Update(ProductRequest product);
        Task<ActionResult<ProductResponse>> GetById(Guid id);
        Task<ActionResult<List<ProductResponse>>> GetAll();
    }
}
