using BSTCodeChallenge.DataAccess.Queries;
using BSTCodeChallenge.Models;
using BSTCodeChallenge.DataAccess.Data;
using MediatR;

namespace BSTCodeChallenge.Handlers.QueryHandlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly LMDB env;
        public GetAllProductsHandler()
        {
            env = new LMDB();
        }
        public Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(env.GetAll());
        }
    }
}
