using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    class NotesServicesTests
    {
        private IQueryable<Note> notesCollection = new List<Note>
        {
            new Note() {RecordId = 1},
            new Note() {RecordId = 1},
            new Note() {RecordId = 2},
            new Note() {RecordId = 2},
            new Note() {RecordId = 2},
        }
        .AsQueryable();

        [Test]
        public void CreateShouldWorkProperly()
        {
            var notesCollection = new List<Note>();

            var notesRepositoryFake = new Mock<IRepository<Note>>();
            notesRepositoryFake.Setup(x => x.All()).Returns(notesCollection.AsQueryable());
            notesRepositoryFake.Setup(x => x.Add(It.IsAny<Note>())).Callback<Note>((note) => notesCollection.Add(note));

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Notes).Returns(notesRepositoryFake.Object);

            var service = new NotesServices(dataFake.Object);
            service.Create(1, "title", "text");

            Assert.AreEqual(1, notesCollection.Count());
            dataFake.Verify(x => x.SaveChanges());
        }

        [Test]
        public void DeleteShouldCallDelete()
        {
            var deleteCounter = 0;

            var notesRepositoryFake = new Mock<IRepository<Note>>();
            notesRepositoryFake.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => deleteCounter++);

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Notes).Returns(notesRepositoryFake.Object);

            var service = new NotesServices(dataFake.Object);

            service.delete(1);
            notesRepositoryFake.Verify(x => x.Delete(It.IsAny<int>()));
        }

        [Test]
        public void DeleteShouldCallSaveChanges()
        {
            var deleteCounter = 0;
            var saveChangesCounter = 0;

            var notesRepositoryFake = new Mock<IRepository<Note>>();
            notesRepositoryFake.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => deleteCounter++);

            var dataFake = new Mock<IQmsData>();
            dataFake.Setup(x => x.Notes).Returns(notesRepositoryFake.Object);
            dataFake.Setup(x => x.SaveChanges()).Callback(() => saveChangesCounter++);

            var service = new NotesServices(dataFake.Object);

            service.delete(1);
            dataFake.Verify(x => x.SaveChanges());
        }
    }
}
