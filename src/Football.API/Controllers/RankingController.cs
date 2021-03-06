﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Football.API.Configurations;
using Football.Domain.SeasonRanking;
using Football.Domain.SeasonRanking.Commands;
using Football.Domain.SeasonRanking.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Football.API.Controllers
{
    [Route("api/[controller]")]
    public class RankingController : BaseController
    {
        private readonly IRankingTeamsHandler _rankingHandler;

        public RankingController(IRankingTeamsHandler rankingHandler)
        {
            _rankingHandler = rankingHandler;
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _rankingHandler.GetRanking(
                new CompetionRankingCommand(id, ConfigurationsValues.FootballURL, ConfigurationsValues.Token));

            if (result.IsSuccess)
                return ResponseSuccess(result.Success);

            return ResponseError(new string[] { result.Error });
        }
    }
}
