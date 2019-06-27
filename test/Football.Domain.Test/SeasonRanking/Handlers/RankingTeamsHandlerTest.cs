using Football.Domain.Core.Deserialize;
using Football.Domain.Core.Request;
using Football.Domain.SeasonRanking;
using Football.Domain.SeasonRanking.Commands;
using Football.Domain.SeasonRanking.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Net;

namespace Football.Domain.Test.SeasonRanking.Handlers
{
    [TestClass]
    public class RankingTeamsHandlerTest
    {
        public RankingTeamsHandler _handlerTest { get; set; }
        public Mock<IHttpWebRequestFactory> _webRequestFactory { get; set; }
        public Mock<IHttpWebRequest> _webRequest { get; set; }
        public Mock<IHttpWebResponse> _webResponse { get; set; }
        public Mock<IStreamReaderFactory> _streamFactory { get; set; }
        public Mock<Stream> _stream { get; set; }
        public Mock<IDeserializeToObject<Ranking>> _deserealize { get; set; }

        private string url = "footbal/{0}";
        private int competitionId = 2021;

        public RankingTeamsHandlerTest()
        {
            _webRequestFactory = new Mock<IHttpWebRequestFactory>();
            _webRequest = new Mock<IHttpWebRequest>();
            _webResponse = new Mock<IHttpWebResponse>();
            _stream = new Mock<Stream>();
            _stream.SetupGet(p => p.CanRead).Returns(true);

            _streamFactory = new Mock<IStreamReaderFactory>();
            _deserealize = new Mock<IDeserializeToObject<Ranking>>();

            _handlerTest = new RankingTeamsHandler(_webRequestFactory.Object, _streamFactory.Object, _deserealize.Object);
        }

        private void MockCorrectResponse()
        {
            _webRequestFactory.Setup(a => a.Create(It.IsAny<string>())).Returns(_webRequest.Object);
            _webRequest.SetupProperty(a => a.Headers, new System.Net.WebHeaderCollection());
            _webRequest.Setup(s => s.GetResponse()).Returns(_webResponse.Object);
            _webResponse.Setup(s => s.GetResponseStream()).Returns(_stream.Object);
            _streamFactory.Setup(s => s.Create(_stream.Object)).Returns(new StreamReader(_stream.Object));
            _deserealize.Setup(a => a.Deserialize(It.IsAny<string>())).Returns(new Ranking());
        }

        [TestMethod]
        public void RankingHandler_WithoutCompetionId_Error()
        {
            //Arrange
            var command = new CompetionRankingCommand(0, "", "");

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert
            Assert.IsTrue(result.IsError);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void RankingHandler_WithoutFootballUrl_Error()
        {
            //Arrange
            var command = new CompetionRankingCommand(2021, "", "");

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert
            Assert.IsTrue(result.IsError);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void RankingHandler_WithoutToken_Error()
        {
            //Arrange
            var command = new CompetionRankingCommand(2021, "teste", "");

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert
            Assert.IsTrue(result.IsError);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void RankingHandler_CreateCorrectUrl_Success()
        {
            //Arrange
            MockCorrectResponse();

            var command = new CompetionRankingCommand(competitionId, url, "teste");

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert          
            var correctUrl = string.Format(url, competitionId);

            _webRequestFactory.Verify(s => s.Create(correctUrl));
        }

        [TestMethod]
        public void RankingHandler_CreateHeaderToken_Success()
        {
            //Arrange
            MockCorrectResponse();
            var mockToken = "teste";
            var command = new CompetionRankingCommand(competitionId, url, mockToken);

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert         
            var token = _webRequest.Object.Headers["X-Auth-Token"];
            Assert.IsNotNull(token);
            Assert.AreEqual(token, mockToken);
        }

        [TestMethod]
        public void RankingHandler_ReturnRanking_Success()
        {
            //Arrange
            MockCorrectResponse();
            var mockToken = "teste";
            var command = new CompetionRankingCommand(competitionId, url, mockToken);

            var ranking = new Ranking();

            _deserealize.Setup(s => s.Deserialize(It.IsAny<string>())).Returns(ranking);

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert                   
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ranking, result.Success);
        }

        [TestMethod]
        public void RankingHandler_GetResponseException_Error()
        {
            //Arrange
            MockCorrectResponse();

            _webRequest.Setup(s => s.GetResponse()).Throws(new WebException());

            var mockToken = "teste";
            var command = new CompetionRankingCommand(competitionId, url, mockToken);

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert         
            Assert.IsTrue(result.IsError);
        }

        [TestMethod]
        public void RankingHandler_GetResponseStreamException_Error()
        {
            //Arrange
            MockCorrectResponse();

            _webResponse.Setup(s => s.GetResponseStream()).Throws(new WebException());

            var mockToken = "teste";
            var command = new CompetionRankingCommand(competitionId, url, mockToken);

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert      

            Assert.IsTrue(result.IsError);

        }

        [TestMethod]
        public void RankingHandler_CreateStreamException_Error()
        {
            //Arrange
            MockCorrectResponse();

            _streamFactory.Setup(s => s.Create(It.IsAny<Stream>())).Throws(new Exception());

            var mockToken = "teste";
            var command = new CompetionRankingCommand(competitionId, url, mockToken);

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert         
            Assert.IsTrue(result.IsError);
        }

        [TestMethod]
        public void RankingHandler_CantDeserialize_Error()
        {
            //Arrange
            MockCorrectResponse();

            _deserealize.Setup(s => s.Deserialize(It.IsAny<string>()))
                .Throws(new Exception());

            var mockToken = "teste";
            var command = new CompetionRankingCommand(competitionId, url, mockToken);

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert         
            Assert.IsTrue(result.IsError);
        }

        [TestMethod]
        public void RankingHandler_CompetitionNotFound_Error()
        {
            //Arrange
            MockCorrectResponse();

            _deserealize.Setup(s => s.Deserialize(It.IsAny<string>()))
                .Returns((Ranking)null);

            var mockToken = "teste";
            var command = new CompetionRankingCommand(competitionId, url, mockToken);

            //Act
            var result = _handlerTest.GetRanking(command);

            //Assert         
            Assert.IsTrue(result.IsError);
            Assert.AreEqual(result.Error.Msg, "Competição não encontrada");
        }

    }
}
