using BSTCodeChallenge.Controllers;
using System;
using IMediator = MediatR.IMediator;
using BSTCodeChallenge.Models;
using NSubstitute;
using NUnit.Framework;

namespace BSTTests
{
    [TestFixture]
    public class Tests
    {
        private ProductControllers _controller;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new ProductControllers(_mediator);
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ProductControllers(_mediator);
            NUnit.Framework.Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanCallSave()
        {
            var prod = new ProductRequest { Id = new Guid(), ProductName = "Produc123" };
            var result = _controller.Save(prod);
            result.Status.Equals(200);
        }

        [Test]
        public void CanCallUpdate()
        {
            var prod = new ProductRequest { Id = new Guid(), ProductName = "Produc123" };
            var result = _controller.Update(prod);
            result.Status.Equals(200);
        }

        [Test]
        public void GetByIdTest()
        {
            var result = _controller.GetById(new Guid());
            result.Status.Equals(200);
        }

        [Test]
        public void GetAllTest()
        {
            var result = _controller.GetAll();
            result.Status.Equals(200);
        }
    }
}