using BSTCodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BSTCodeChallenge.Interface;
using MediatR;
using BSTCodeChallenge.DataAccess.Data;
using BSTCodeChallenge.DataAccess.Queries;
using BSTCodeChallenge.DataAccess.Command;
using IMediator = MediatR.IMediator;
using Microsoft.Extensions.Caching.Memory;

namespace BSTCodeChallenge.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductControllers: ControllerBase
    {
        private readonly IMediator mediator;

        public ProductControllers(IMediator _mediator)
        {
            mediator = _mediator;
        }


        [Route("save")]
        [HttpPost]
        public async Task<ProductResponse> Save(ProductRequest product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Stopwatch watcher = new Stopwatch();
                    watcher.Start();

                    var res = await mediator.Send(new SaveProductCommand(product));

                    Log.RecordLog(watcher.Elapsed);

                    return res;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Exception. {ex}");
            }
            return null;
        }

        [Route("update")]
        [HttpPut]
        public async Task<ProductResponse> Update(ProductRequest product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Stopwatch watcher = new Stopwatch();
                    watcher.Start();

                    var res = await mediator.Send(new UpdateProductCommand(product));

                    Log.RecordLog(watcher.Elapsed);

                    return res;
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Exception. {ex}");
            }
            return null;
        }

        [Route("getbyid")]
        [HttpGet]
        public async Task<ProductResponse> GetById(Guid id)
        {
            try
            {
                if (id != null)
                {
                    Stopwatch watcher = new Stopwatch();
                    watcher.Start();

                    var res = await mediator.Send(new GetProductByIdQuery(id));
                    var suppliers = CacheController.getSuppliers();
                    var categories = CacheController.getCategory();
                    if (suppliers != null || categories != null)
                    {
                        var supp = suppliers.Find(x => x.Id == res.SupplierId);
                        var cat = categories.Find(x => x.Id == res.CategoryId);
                        if (supp != null)
                        {
                            res.SupplierName = supp.SupplierName;
                            res.Cost = supp.Cost;
                        }
                        if (cat != null)
                        {
                            res.CategoryName = cat.CategoryName;
                            res.CategoryPopularity = cat.CategoryPopularity;
                        }
                    }

                    Log.RecordLog(watcher.Elapsed);

                    return res;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Exception. {ex}");
            }
            return null;
        }

        [Route("getall")]
        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetAll()
        {
            try
            {
                Stopwatch watcher = new Stopwatch();
                watcher.Start();

                var res = await mediator.Send(new GetAllProductsQuery());
                var suppliers = CacheController.getSuppliers();
                var categories = CacheController.getCategory();
                if (suppliers != null || categories != null)
                {
                    res.ForEach(prod =>
                    {
                        var supp = suppliers.Find(x => x.Id == prod.SupplierId);
                        var cat = categories.Find(x => x.Id == prod.CategoryId);
                        if (supp != null)
                        {
                            prod.SupplierName = supp.SupplierName;
                            prod.Cost = supp.Cost;
                        }
                        if (cat != null)
                        {
                            prod.CategoryName = cat.CategoryName;
                            prod.CategoryPopularity = cat.CategoryPopularity;
                        }

                    });
                }

                Log.RecordLog(watcher.Elapsed);

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Exception. {ex}");
                return null;
            }
        }
    }
}
