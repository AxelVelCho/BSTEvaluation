using BSTCodeChallenge.DataAccess.Queries;
using BSTCodeChallenge.Models;
using BSTCodeChallenge.DataAccess.Data;
using MediatR;

namespace BSTCodeChallenge.DataAccess.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly LMDB env;
        public GetProductByIdHandler()
        {
            env = new LMDB();
        }
        public Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(env.GetProduct(request.id));
        }
    }
}
