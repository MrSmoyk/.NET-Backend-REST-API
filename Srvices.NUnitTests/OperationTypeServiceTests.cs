using AutoMapper;
using DAL;
using DAL.Interfases;
using DAL.Repositories;
using Domain.DTO.TypeDTOs;
using Domain.Exceptions;
using FluentAssertions;
using Service.AutoMapperProfiles;
using Service.Implementations;
using Service.Interfaces;

namespace Srvices.NUnitTests
{
    internal class OperationTypeServiceTests
    {
        private ApplicationDbContext context;
        private IOperationTypeRepository operationTypeRepository;
        private IOperationTypeService operationTypeService;




        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(Profiles));
            });
            var mapper = mockMapper.CreateMapper();
            context = TestDB.GetDbContext();
            operationTypeRepository = new OperationTypeRepository(context);
            operationTypeService = new OperationTypeService(operationTypeRepository, mapper);
        }


        [Test]
        public void CreateOperationTypeAsync_WithNull_Fail()
        {
            OperationTypeCreateUpdateDTO testOperationType = null;

            AggregateException? result = operationTypeService.CreateOperationTypeAsync(testOperationType).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<SourceEntityNullException>();
            result.InnerException.Message.Should().BeEquivalentTo("Entity to set wasn't given.");
        }

        [Test]
        public void CreateOperationTypeAsync_WithInvalidModel_Fail()
        {
            var testOperationType1 = new OperationTypeCreateUpdateDTO()
            {
                Description = "-",
                IsIncome = false,
                Name = "Incomes"
            };

            var testOperationType2 = new OperationTypeCreateUpdateDTO()
            {
                Description = "-",
                IsIncome = false,
                Name = null
            };


            var result1 = operationTypeService.CreateOperationTypeAsync(testOperationType1).Exception;
            var result2 = operationTypeService.CreateOperationTypeAsync(testOperationType2).Exception;


            result1.Should().NotBeNull();
            result1.InnerException.Should().NotBeNull();
            result1.InnerException.Should().BeOfType<AddingExistingEntityException>();
            result1.InnerException.Message.Should().BeEquivalentTo("Operation type with name Incomes already exists.");
            result2.Should().NotBeNull();
            result2.InnerException.Should().NotBeNull();
            result2.InnerException.Should().BeOfType<SourceEntityNullException>();
            result2.InnerException.Message.Should().BeEquivalentTo("Entity to set wasn't given.");
        }

        [Test]
        public async Task CreateOperationTypeAsync_WithCorrectModel_Sucsess()
        {
            var testOperationType = new OperationTypeCreateUpdateDTO()
            {
                Description = "-",
                IsIncome = false,
                Name = "Incomes3"
            };

            var result = await operationTypeService.CreateOperationTypeAsync(testOperationType);

            result.Should().NotBeNull();
            result.Should().BeOfType<OperationTypeDTO>();
            result.Name.Equals(testOperationType.Name);
            result.IsIncome.Should().BeFalse();
            result.Description.Equals(testOperationType.Description);
        }

        [Test]
        public void UpdateOperationTypeAsync_WithNull_Fail()
        {
            var testId = new Guid("021b7f89-ae66-4e77-b970-57b758f54edf");
            OperationTypeCreateUpdateDTO testOperationType = null;

            AggregateException? result = operationTypeService.UpdateOperationTypeAsync(testId, testOperationType).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<UnknownOperationTypeException>();
            result.InnerException.Message.Should().BeEquivalentTo("Operation type with id 021b7f89-ae66-4e77-b970-57b758f54edf doesn't exsist.");
        }

        [Test]
        public void UpdateOperationTypeAsync_WithInvalidModel_Fail()
        {
            var testId = new Guid("6a6da227-6d4c-42eb-9eb6-af3d7ddfca45");
            var testOperationType = new OperationTypeCreateUpdateDTO()
            {
                Description = "-",
                IsIncome = false,
                Name = "Incomes"
            };

            AggregateException? result = operationTypeService.UpdateOperationTypeAsync(testId, testOperationType).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<AddingExistingEntityException>();
            result.InnerException.Message.Should().BeEquivalentTo("Operation type with name Incomes already exists.");
        }

        [Test]
        public async Task UpdateOperationTypeAsync_WithValidModel_Fail()
        {
            var testId = new Guid("6a6da227-6d4c-42eb-9eb6-af3d7ddfca45");
            var testOperationType = new OperationTypeCreateUpdateDTO()
            {
                Description = "-",
                IsIncome = false,
                Name = "Incomeses"
            };

            var result = await operationTypeService.UpdateOperationTypeAsync(testId, testOperationType);

            result.Should().NotBeNull();
            result.Should().BeOfType<OperationTypeDTO>();
            result.Name.Equals(testOperationType.Name);
            result.IsIncome.Should().BeFalse();
            result.Description.Equals(testOperationType.Description);
        }
    }
}
