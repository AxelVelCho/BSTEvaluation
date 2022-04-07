using BSTCodeChallenge.Models;
using MediatR;

namespace BSTCodeChallenge.DataAccess.Command
{
    public record UpdateProductCommand(ProductRequest product) : IRequest<ProductResponse>;
}
