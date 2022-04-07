using BSTCodeChallenge.Models;
using MediatR;

namespace BSTCodeChallenge.DataAccess.Queries
{
    public record GetAllProductsQuery() : IRequest<List<ProductResponse>>;
}
