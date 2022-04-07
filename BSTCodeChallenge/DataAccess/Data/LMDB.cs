using BSTCodeChallenge.Models;
using LightningDB;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using Xunit;

namespace BSTCodeChallenge.DataAccess.Data
{
    public class LMDB: IDisposable

    {
        private static LightningEnvironment _env;
        private static readonly string dbname = "BSTTestDB";

        public LMDB()
        {
        }

        private (LightningTransaction? tr , LightningDatabase db , LightningCursor csr) Initialize(string action)
        {
            var path = "./LMDB/";
            _env = new LightningEnvironment(path);
            _env.MaxDatabases = 2;
            _env.Open();
            var tr = _env.BeginTransaction();
            var db = tr.OpenDatabase(dbname);
            switch (action)
            {
                case "get":
                    tr = _env.BeginTransaction(TransactionBeginFlags.ReadOnly);
                    db = tr.OpenDatabase(dbname, new DatabaseConfiguration { Flags = DatabaseOpenFlags.Create });
                    break;
            }
            var csr = tr.CreateCursor(db);
            return (tr, db, csr);
        }

        public ProductResponse SaveUpdateProduct(ProductRequest product)
        {
            ProductResponse obj = new ProductResponse();
            try
            {
                var (tr, db, cursor) = Initialize("set");
                var serializedObj = JsonSerializer.Serialize(product);
                var status = cursor.Put(Encoding.UTF8.GetBytes(product.Id.ToString()), Encoding.UTF8.GetBytes(serializedObj), CursorPutOptions.None);
                if (status == MDBResultCode.Success)
                {
                    var (_, _, value) = cursor.GetCurrent();
                    obj = JsonSerializer.Deserialize<ProductResponse>(value.CopyToNewArray());
                }
                tr.Commit();
                db.Dispose();
                cursor.Dispose();
                _env.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine($"LMDB Exception. {e}");
            }
            return obj;
        }

        public ProductResponse GetProduct(Guid Id)
        {
            ProductResponse obj = new ProductResponse();
            try
            {
                var (tr, db, cursor) = Initialize("get");
                var (resultCode, key, value) = cursor.SetKey(Encoding.UTF8.GetBytes(Id.ToString()));
                var res = Encoding.UTF8.GetString(value.CopyToNewArray());
                obj = JsonSerializer.Deserialize<ProductResponse>(res);
                Assert.Equal(value.CopyToNewArray(), Encoding.UTF8.GetBytes(res));
                tr.Dispose();
                db.Dispose();
                cursor.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine($"LMDB Exception. {e}");
            }
            return obj;
        }

        public List<ProductResponse> GetAll()
        {
            List<ProductResponse> list = new List<ProductResponse>();
            try
            {
                var (tr, db, cursor) = Initialize("get");
                ProductResponse obj; 
                var value = cursor.AsEnumerable();
                foreach (var item in value)
                {
                    obj = JsonSerializer.Deserialize<ProductResponse>(Encoding.UTF8.GetString(item.Item2.CopyToNewArray()));
                    list.Add(obj);
                }
                tr.Dispose();
                _env.Dispose();
                db.Dispose();
                cursor.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine($"LMDB Exception. {e}");
            }
            return list;
        }

        public void Dispose()
        {
            _env.Dispose();
        }
    }
}
