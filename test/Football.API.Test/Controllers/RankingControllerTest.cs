using Football.API.Controllers;
using Football.Domain.SeasonRanking;
using Football.Domain.SeasonRanking.Commands;
using Football.Domain.SeasonRanking.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace Football.API.Test.Controllers
{
    [TestClass]
    public class RankingControllerTest
    {
        public RankingController _controllerTest;  
        private Mock<IRankingTeamsHandler> _rankingHandler;

        public RankingControllerTest()
        {
            _rankingHandler = new Mock<IRankingTeamsHandler>();
            _controllerTest = new RankingController(_rankingHandler.Object);
        }

        [TestMethod]
        public void ReturnRanking_Success()
        {
            //Arrange
            int competitionId = 2021;
            var ranking = new Ranking() { Competition = new Competition { Id = competitionId } };

            _rankingHandler.Setup(s => s.GetRanking(It.IsAny<CompetionRankingCommand>()))
                .Returns(ranking);

            //Act
            var result = _controllerTest.Get(competitionId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            //var rank = (((OkObjectResult)result)?.Value as dynamic)?.data;

            //Assert.IsInstanceOfType(rank, typeof(Ranking));

            //var rankObj = rank as Ranking;

            //Assert.AreEqual(ranking.Competition.Id, rankObj.Competition.Id);
        }

        [TestMethod]
        public void ReturnRanking_Error()
        {
            //Arrange
            string error = "Competição não encontrada";           

            _rankingHandler.Setup(s => s.GetRanking(It.IsAny<CompetionRankingCommand>()))
                .Returns(error);

            //Act
            var result = _controllerTest.Get(0);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            //var errorReturned = (((BadRequestObjectResult)result)?.Value as dynamic)?.erros;

            //Assert.IsInstanceOfType(errorReturned, typeof(string[]));
            //Assert.IsTrue((errorReturned as string[]).Contains(error));
        }
    }
}
