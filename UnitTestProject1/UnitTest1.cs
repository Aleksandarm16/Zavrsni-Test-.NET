using AleksandarMihaljev.Controllers;
using AleksandarMihaljev.Interface;
using AleksandarMihaljev.Models;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void GetReturnWithSameId()
        {
            var mockRepository = new Mock<IPassengerRepository>();
            mockRepository.Setup(x => x.GetId(16)).Returns(new Passenger { Id = 16 });
            var controler = new PassengerController(mockRepository.Object);

            IHttpActionResult actionresult = controler.GetId(16);
            var contentResult = actionresult as OkNegotiatedContentResult<Passenger>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(16, contentResult.Content.Id);


        }
        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IPassengerRepository>();
            var controller = new PassengerController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Passenger { Id = 9, NameAndLastName = "Passenger1" });


            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<IPassengerRepository>();
            var controller = new PassengerController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }



        [TestMethod]
        public void SearchByYearsReturnsMultipleObjects()
        {
            List<Passenger> passengers = new List<Passenger>();
            passengers.Add(new Passenger { Id = 1, NameAndLastName = "Passenger1",Year=1991, Bus = new Bus { Line = "BusLine1", Id = 1 } });
            passengers.Add(new Passenger { Id = 2, NameAndLastName = "Passenger2", Year = 1987, Bus = new Bus { Line = "BusLine2", Id = 2 } });
            passengers.Add(new Passenger { Id = 3, NameAndLastName = "Passenger3", Year = 1988, Bus = new Bus { Line = "BusLine3", Id = 3 } });
            passengers.Add(new Passenger { Id = 4, NameAndLastName = "Passenger4", Year = 1997, Bus = new Bus { Line = "BusLine4", Id = 4 } });
              Mapper.Initialize(cfg => cfg.CreateMap<Passenger, PassengerDto>());
            int start = 1987;
            int end = 1996;
            SearchDto search = new SearchDto { Start = 1987, End = 1996 };
          

            var mockRepository = new Mock<IPassengerRepository>();
            mockRepository.Setup(x => x.SearchByYear(start, end))
                .Returns(passengers.Where(x => x.Year >= start && x.Year <= end).OrderBy(x => x.Year).AsQueryable());
            var controller = new PassengerController(mockRepository.Object);

            IHttpActionResult result = controller.SearchByValues(search);
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<PassengerDto>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);

            Assert.AreEqual(3, contentResult.Content.Count());
            Assert.AreEqual("Passenger2", contentResult.Content.ElementAt(0).NameAndLastName);
            Assert.AreEqual("Passenger3", contentResult.Content.ElementAt(1).NameAndLastName);
            Assert.AreEqual("Passenger1", contentResult.Content.ElementAt(2).NameAndLastName);


        }
        
    }
}
