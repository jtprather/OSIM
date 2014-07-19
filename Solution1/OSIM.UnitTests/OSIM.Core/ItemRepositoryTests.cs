using System;
using Moq;
using NHibernate;
using NUnit.Framework;
using NBehave.Spec.NUnit;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;

namespace OSIM.UnitTests.OSIM.Core
{
    public class when_working_with_the_item_type_repository : Specification
    {
        protected IItemTypeRepository _itemTypeRepository;
        protected Mock<ISessionFactory> _sessionFactory;
        protected Mock<ISession> _session;
        protected override void Establish_context()
        {
            base.Establish_context();

            _sessionFactory = new Mock<ISessionFactory>();
            _session = new Mock<ISession>();

            _sessionFactory.Setup(sf => sf.OpenSession()).Returns(_session.Object);

            _itemTypeRepository = new ItemTypeRepository(_sessionFactory.Object);
        }
        public class and_saving_an_invalid_item_type : when_working_with_the_item_type_repository
        {
            private Exception _result;

            protected override void Establish_context()
            {
                base.Establish_context();

                _session.Setup(s => s.Save(null)).Throws(new ArgumentNullException());
            }
            protected override void Because_of()
            {
                try
                {
                    _itemTypeRepository.Save(null);
                }
                catch (Exception exception)
                {
                    _result = exception;
                }
            }
            [Test]
            public void then_an_argument_null_exception_should_be_raised()
            {
                _result.ShouldBeInstanceOfType(typeof(ArgumentNullException));
            }
        }
        public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
        {
            private int _result;
            
            private ItemType _testItemType;
            private int _itemTypeId;

            protected override void Establish_context()
            {
                base.Establish_context();

                var randomNumberGenerator = new Random();
                _itemTypeId = randomNumberGenerator.Next(32000);

                _session.Setup(s => s.Save(_testItemType)).Returns(_itemTypeId);
            }
            protected override void Because_of()
            {
                _result = _itemTypeRepository.Save(_testItemType);
            }

            [Test]
            public void then_a_valid_item_type_id_should_be_returned()
            {
                _result.ShouldEqual(_itemTypeId);
            }
        }
    }
}
