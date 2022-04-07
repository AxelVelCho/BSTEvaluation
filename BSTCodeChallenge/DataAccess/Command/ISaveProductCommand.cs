using BSTCodeChallenge.Models;
using MediatR;

namespace BSTCodeChallenge.DataAccess.Command
{
    public record SaveProductCommand(ProductRequest product) : IRequest<ProductResponse>;
}
