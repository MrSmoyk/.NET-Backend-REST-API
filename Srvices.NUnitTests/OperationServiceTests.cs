using AutoMapper;
using DAL;
using DAL.Interfases;
using DAL.Repositories;
using Domain.DTO.OperationDTOs;
using Domain.Exceptions;
using FluentAssertions;
using Service.AutoMapperProfiles;
using Service.Implementations;
using Service.Interfaces;

namespace Srvices.NUnitTests
{
    public class OperationServiceTests
    {
        private ApplicationDbContext context;
        private IOperationTypeRepository operationTypeRepository;
        private IOperationRepository operationRepository;
        private IOperationService operationService;

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
            operationRepository = new OperationRepository(context);
            operationService = new OperationService(operationRepository, operationTypeRepository, mapper);
        }

        [Test]
        public void CreateOperationAsync_WithNull_Fail()
        {
            OperationCreateUpdateDTO testOperationType = null;

            AggregateException? result = operationService.CreateOperationAsync(testOperationType).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<SourceEntityNullException>();
            result.InnerException.Message.Should().BeEquivalentTo("Entity to set wasn't given.");
        }

        [Test]
        public void CreateOperationTypeAsync_WithInvalidModel_Fail()
        {
            var testOperation1 = new OperationCreateUpdateDTO { Name = "Income", Amount = 2147, Created = DateTime.ParseExact("2022-12-20", "yyyy-MM-dd", null), Description = "desc", TypeName = "dsajcck" };
            var testOperation2 = new OperationCreateUpdateDTO { Name = "Expense", Amount = 2147, Created = DateTime.ParseExact("2022-12-20", "yyyy-MM-dd", null), Description = "desc", TypeName = null };
            var testOperation3 = new OperationCreateUpdateDTO { Name = null, Amount = 0, Created = DateTime.ParseExact("2022-12-20", "yyyy-MM-dd", null), Description = null, TypeName = "Incomes" };


            var result1 = operationService.CreateOperationAsync(testOperation1).Exception;
            var result2 = operationService.CreateOperationAsync(testOperation2).Exception;
            var result3 = operationService.CreateOperationAsync(testOperation3).Exception;


            result1.Should().NotBeNull();
            result1.InnerException.Should().NotBeNull();
            result1.InnerException.Should().BeOfType<UnknownOperationTypeException>();
            result1.InnerException.Message.Should().BeEquivalentTo("Operation type with name ' dsajcck ' doesn't exsist.");

            result2.Should().NotBeNull();
            result2.InnerException.Should().NotBeNull();
            result2.InnerException.Should().BeOfType<UnknownOperationTypeException>();
            result2.InnerException.Message.Should().BeEquivalentTo("Operation type with name '  ' doesn't exsist.");

            result3.Should().NotBeNull();
            result3.InnerExceptions.Should().NotBeEmpty();
            result3.InnerExceptions.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task CreateOperationTypeAsync_WithCorrectModel_Sucsess()
        {
            var testOperation = new OperationCreateUpdateDTO()
            {
                Name = "",
                TypeName = "Incomes",
                Created = DateTime.Now.Date,
                Amount = 1,
                Description = ""
            };

            var result = await operationService.CreateOperationAsync(testOperation);

            result.Should().NotBeNull();
            result.Should().BeOfType<OperationDTO>();
            result.Name.Equals(testOperation.Name);
            result.Amount.Should().BeGreaterThan(0);
            result.Type.Equals(testOperation.TypeName);
            result.Created.Should().Be(DateTime.Now.Date);
            result.Description.Equals(testOperation.Description);
        }

        [Test]
        public void UpdateOperationAsync_WithNull_Fail()
        {
            var testId = new Guid("021b7f89-ae66-4e77-b970-57b758f54edf");
            OperationCreateUpdateDTO testOperation = null;

            AggregateException? result = operationService.UpdateOperationAsync(testId, testOperation).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<SourceEntityNullException>();
            result.InnerException.Message.Should().BeEquivalentTo("Entity to set wasn't given.");
        }

        [Test]
        public void UpdateOperationTypeAsync_WithInvalidModel_Fail()
        {
            var testId = new Guid("6a6da227-6d4c-42eb-9eb6-af3d7ddfca45");
            var testOperationType = new OperationCreateUpdateDTO { Name = null, Amount = 0, Created = DateTime.ParseExact("2022-12-20", "yyyy-MM-dd", null), Description = null, TypeName = "Incomes" };

            AggregateException? result = operationService.UpdateOperationAsync(testId, testOperationType).Exception;

            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result.InnerException.Should().BeOfType<EntityNotFoundException>();
            result.InnerException.Message.Should().BeEquivalentTo("Entity with id: 6a6da227-6d4c-42eb-9eb6-af3d7ddfca45 not found.");
        }

        [Test]
        public async Task UpdateOperationAsync_WithValidModel_Fail()
        {
            var testId = new Guid("ef1db03b-a3b1-4144-b0b3-acb6fc10ba2c");
            var testOperation = new OperationCreateUpdateDTO
            {
                Name = "",
                Amount = 1,
                Created = DateTime.Now.Date,
                Description = "",
                TypeName = "Incomes"
            };

            var result = await operationService.UpdateOperationAsync(testId, testOperation);

            result.Should().NotBeNull();
            result.Should().BeOfType<OperationDTO>();
            result.Name.Equals(testOperation.Name);
            result.Amount.Equals(testOperation.Amount);
            result.Description.Equals(testOperation.Description);
            result.Created.Equals(testOperation.Created);
            result.Id.Equals(testId);
            result.Type.Equals(testOperation.TypeName);
        }
    }
}