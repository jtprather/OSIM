﻿using System;
using OSIM.UnitTests;
using NUnit.Framework;
using NBehave.Spec.NUnit;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using Ninject;


namespace OSIM.IntegrationTests.OSIM.Core.Persistence
{
    public class when_using_the_item_type_repository : Specification
    {
        protected IItemTypeRepository _itemTypeRepository;
        protected StandardKernel _kernel;
        protected override void Establish_context()
        {
            base.Establish_context();

            _kernel = new StandardKernel(new IntegrationTestModule());
            try
            {
                _itemTypeRepository = _kernel.Get<IItemTypeRepository>();
            }
            catch (Exception e)
            {
                return;
            }
            return;
        }
    }
    public class and_attempting_to_save_and_read_a_value_from_the_database : when_using_the_item_type_repository
    {
        private ItemType _expected;
        private ItemType _result;

        protected override void Establish_context()
        {
            base.Establish_context();

            _expected = new ItemType { Name = Guid.NewGuid().ToString() };
        }

        protected override void Because_of()
        {
            var itemTypeId = _itemTypeRepository.Save(_expected);
            _result = _itemTypeRepository.GetById(itemTypeId);
        }
        [Test]
        public void then_the_item_type_saved_to_the_database_should_equal_the_item_type_retrieved()
        {
            _result.Id.ShouldEqual(_expected.Id);
            _result.Name.ShouldEqual(_expected.Name);
        }
    }
}
