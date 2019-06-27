using Football.Domain.SeasonRanking.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Football.Domain.Test.SeasonRanking.Handlers
{
    [TestClass]
    public class CompetionRankingCommandTest
    {
        public CompetionRankingCommand _commandTest;

        public CompetionRankingCommandTest()
        {

        }

        [TestMethod]
        public void CreateCommand_Valid()
        {
            //Arrage
            var competitionId = 2021;
            var url = "footballUrl";
            var token = "token-football";

            _commandTest = new CompetionRankingCommand(competitionId, url, token);

            //Act
            var invalid = _commandTest.Invalid();

            //Assert
            Assert.IsFalse(invalid);
        }

        [TestMethod]
        public void CreateCommand_WithoutCompetition_Invalid()
        {
            //Arrage   
            //var competitionId = 2021;
            var url = "footballUrl";
            var token = "token-football";

            _commandTest = new CompetionRankingCommand(0, url, token);

            //Act
            var invalid = _commandTest.Invalid();

            //Assert
            Assert.IsTrue(invalid);
        }

        [TestMethod]
        public void CreateCommand_WithoutURL_Invalid()
        {
            //Arrage  
            var competitionId = 2021;
            //  var url = "footballUrl";
            var token = "token-football";

            _commandTest = new CompetionRankingCommand(competitionId, "", token);

            //Act
            var invalid = _commandTest.Invalid();

            //Assert
            Assert.IsTrue(invalid);
        }

        [TestMethod]
        public void CreateCommand_WithoutToken_Invalid()
        {
            //Arrage  
            var competitionId = 2021;
            var url = "footballUrl";
            var token = "token-football";

            _commandTest = new CompetionRankingCommand(competitionId, url, "");

            //Act
            var invalid = _commandTest.Invalid();

            //Assert
            Assert.IsTrue(invalid);
        }


        [TestMethod]
        public void CreateCommand_WithoutAll_Invalid()
        {
            //Arrage  
            //var competitionId = 2021;
           // var url = "footballUrl";
           // var token = "token-football";

            _commandTest = new CompetionRankingCommand(0, "", "");

            //Act
            var invalid = _commandTest.Invalid();

            //Assert
            Assert.IsTrue(invalid);
        }

    }
}
