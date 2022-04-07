using BSTCodeChallenge.DataAccess.Command;
using BSTCodeChallenge.Models;
using BSTCodeChallenge.DataAccess.Data;
using MediatR;

namespace BSTCodeChallenge.Handlers.CommandHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly LMDB env;
        public UpdateProductHandler()
        {
            env = new LMDB();
        }

        public Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(env.SaveUpdateProduct(request.product));
        }
    }
}
