using BSTCodeChallenge.DataAccess.Command;
using BSTCodeChallenge.Models;
using BSTCodeChallenge.DataAccess.Data;
using MediatR;

namespace BSTCodeChallenge.Handlers.CommandHandlers
{
    public class SaveProductHandler : IRequestHandler<SaveProductCommand, ProductResponse>
    {
        private readonly LMDB env;
        public SaveProductHandler()
        {
            env = new LMDB();
        }

        public Task<ProductResponse> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(env.SaveUpdateProduct(request.product));
        }
    }
}
