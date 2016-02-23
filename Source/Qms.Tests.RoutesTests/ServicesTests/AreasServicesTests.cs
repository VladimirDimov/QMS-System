namespace Qms.Tests.RoutesTests.ServicesTests
{
    using Moq;
    using NUnit.Framework;
    using QMS.Data;
    using QMS.Data.Repository;
    using QMS.Models;
    using QMS.Services;
    using System.Collections.Generic;
    using System.Linq;

    class AreasServicesTests
    {
        private IQueryable<Area> areasCollection = new List<Area>
        {
            new Area() { Id=0},
            new Area() { Id=1},
            new Area() { Id=2},
            new Area() { Id=3},
            new Area() { Id=4},
            new Area() { Id=5},
        }
        .AsQueryable();

        [Test]
        public void AllShouldReturnProperResult()
        {
            var areasRepositoryFake = new Mock<IRepository<Area>>();
            areasRepositoryFake.Setup(x => x.All()).Returns(areasCollection);

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Areas).Returns(areasRepositoryFake.Object);
            var service = new AreasServices(dataFake.Object);
            var result = service.all();

            Assert.AreEqual(this.areasCollection.Count(), result.Count());
        }

        [Test]
        public void ShouldBeAbleToAddAreas()
        {
            var emptyAreasCollection = new List<Area>().AsQueryable();
            var addAreaCounter = 0;
            var saveChangesCounter = 0;

            var areasRepositoryFake = new Mock<IRepository<Area>>();
            areasRepositoryFake.Setup(x => x.Add(It.IsAny<Area>())).Callback(() => addAreaCounter++);
            areasRepositoryFake.Setup(x => x.SaveChanges()).Callback(() => saveChangesCounter++);

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Areas).Returns(areasRepositoryFake.Object);
            var service = new AreasServices(dataFake.Object);

            var numberOfAreasToAdd = 100;
            for (int i = 0; i < numberOfAreasToAdd; i++)
            {
                var id = service.Add("", "", 1, "");
                areasRepositoryFake.Object.SaveChanges();
            }

            Assert.AreEqual(numberOfAreasToAdd, addAreaCounter);
            Assert.AreEqual(numberOfAreasToAdd, saveChangesCounter);
        }

        [Test]
        public void GetByUserIdShouldReturnAreaWhenAvailable()
        {
            var areasListCollection = new Area[]
            {
                new Area {Id = 0 },
                new Area {Id = 1 },
                new Area {Id = 2 },
            };

            int y = 0;
            var areasRepositoryFake = new Mock<IRepository<Area>>();
            areasRepositoryFake
                .Setup<Area>(x => x.GetById(It.IsAny<int>()))
                .Returns(() => areasListCollection.FirstOrDefault(a => a.Id == y));

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Areas).Returns(areasRepositoryFake.Object);
            var service = new AreasServices(dataFake.Object);

            for (int i = 0; i < areasListCollection.Count(); i++)
            {
                y = i;
                var areaSelected = service.GetById(i);
                Assert.AreEqual(i, areaSelected.Id);
            }
        }

        [Test]
        public void UpdateShouldWorkProperly()
        {
            var areasListCollection = new Area[]
            {
                new Area {Id = 0 },
                new Area {Id = 1 },
                new Area {Id = 2 },
            };

            int y = 1;
            var areasRepositoryFake = new Mock<IRepository<Area>>();
            areasRepositoryFake
                .Setup<Area>(x => x.GetById(It.IsAny<int>()))
                .Returns(() => areasListCollection.FirstOrDefault(a => a.Id == y));

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Areas).Returns(areasRepositoryFake.Object);
            var service = new AreasServices(dataFake.Object);

            service.Update(y, "name", "description", "employId");
            var updatedArea = service.GetById(y);

            Assert.AreEqual("name", updatedArea.Name);
            Assert.AreEqual("description", updatedArea.Description);
            Assert.AreEqual("employId", updatedArea.EmployeeId);
        }

        [Test]
        public void DeleteShoulCallDelete()
        {
            var areasRepositoryFake = new Mock<IRepository<Area>>();

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Areas).Returns(areasRepositoryFake.Object);
            var service = new AreasServices(dataFake.Object);

            service.Delete(1);

            areasRepositoryFake.Verify(x => x.Delete(1));
        }

        [Test]
        public void DeleteShoulCallSave()
        {
            var savechangesCounter = 0;
            var areasRepositoryFake = new Mock<IRepository<Area>>();

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Areas).Returns(areasRepositoryFake.Object);
            dataFake.Setup(x => x.SaveChanges()).Callback(() => savechangesCounter++);
            var service = new AreasServices(dataFake.Object);

            service.Delete(1);

            dataFake.Verify(x => x.SaveChanges());
        }
    }
}