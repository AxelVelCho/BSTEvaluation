using BSTCodeChallenge.Models;
using MediatR;

namespace BSTCodeChallenge.DataAccess.Queries
{
    public record GetProductByIdQuery(Guid id): IRequest<ProductResponse>;
}
