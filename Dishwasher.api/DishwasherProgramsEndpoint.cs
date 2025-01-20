using System.Net.Http.Headers;
using Dishwasher.engine;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dishwasher.api
{
    public static class DishwasherProgramsEndpoint
    {
        public static void ConfigureDishwasherProgramsEndpoint(this WebApplication app)
        {
            var programs = app.MapGroup("programs");

            programs.MapGet("/", GetAll);
            programs.MapPost("/start/{id}", Start);
            programs.MapGet("/running", GetRunning);
            programs.MapGet("/history", GetProgramsHistory);
            programs.MapGet("/statistics", GetStatistics);
            programs.MapDelete("/stop", Stop);
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAll(DishwasherProgramsRepository dpr)
        {
            var dishwasherPrograms = dpr.GetAll();
            return Results.Ok(dishwasherPrograms);
        }


        public static async Task<IResult> Start(DishwasherProgramsRepository dpr, int id)
        {
            var dishwasherProgram = dpr.Get(id);
            if (dishwasherProgram != null)
            {
                if (dpr.GetRunning() != null)
                {
                    return Results.BadRequest(new { message = "ALREADY RUNNING." });
                }

                decimal rinseAid = dpr.GetRinseAid();
                decimal salt = dpr.GetSalt();
                int tablets = dpr.GetTablets();
                decimal cleanCycle = dpr.GetCleanCycle();

                string rinseAidMessage = "";
                string saltMessage = "";
                string tabletsMessage = "";
                string cleanCycleMessage = "";

                if (rinseAid <= 0 || rinseAid < dishwasherProgram.WaterConsumption)
                {
                    rinseAidMessage = "REFILL";
                }
                else if (rinseAid < 20)
                {
                    rinseAidMessage = "LOW";
                }
                else
                {
                    rinseAidMessage = "OK";
                }

                if (salt <= 0 || salt < dishwasherProgram.WaterConsumption)
                {
                    saltMessage = "REFILL";
                }
                else if (salt < 30)
                {
                    saltMessage = "LOW";
                }
                else
                {
                    saltMessage = "OK";
                }

                if (tablets <= 0)
                {
                    tabletsMessage = "REFILL";
                }
                else if (tablets < 10)
                {
                    tabletsMessage = "LOW";
                }
                else
                {
                    tabletsMessage = "OK";
                }

                if (cleanCycle <= 0 || cleanCycle < (dishwasherProgram.Runtime / 60 / 60))
                {
                    cleanCycleMessage = "RUN CLEAN CYCLE";
                }
                else
                {
                    cleanCycleMessage = "OK";
                }

                if (rinseAidMessage.StartsWith("R") || saltMessage.StartsWith("R") || tabletsMessage.StartsWith("R") || cleanCycleMessage.StartsWith("R"))
                {
                    return Results.BadRequest(new { message = "ACTION REQUIRED.", rinse_aid = rinseAidMessage, salt = saltMessage, tablets = tabletsMessage, clean_cycle = cleanCycleMessage});
                }
                else
                {
                    dpr.Start(dishwasherProgram);
                    dpr.UpdateRinseAid(-dishwasherProgram.WaterConsumption);
                    dpr.UpdateSalt(-dishwasherProgram.WaterConsumption);
                    dpr.UpdateTablets(-1);
                    dpr.UpdateCleanCycle(-(dishwasherProgram.Runtime / 60 / 60));
                    return Results.Ok(new { message = "STARTED.", rinse_aid = rinseAidMessage, salt = saltMessage, tablets = tabletsMessage, clean_cycle = cleanCycleMessage, program = dishwasherProgram });
                }
            }
            else
            {
                return Results.BadRequest(new { Message = "NOT FOUND." });
            }
        }

        public static async Task<IResult> GetRunning(DishwasherProgramsRepository dpr)
        {
            var dishwasherRunningProgram = dpr.GetRunning();
            if (dishwasherRunningProgram != null)
            {
                return Results.Ok(new {message = "STOPPED.", Program = dishwasherRunningProgram });
            }
            else
            {
                return Results.Conflict(new {message = "NOT RUNNING." });
            }
        }

        public static async Task<IResult> GetProgramsHistory(DishwasherProgramsRepository dpr)
        {
            var programsHistory = dpr.GetProgramsHistory();
            return Results.Ok(programsHistory);
        }

        public static async Task<IResult> GetStatistics(DishwasherProgramsRepository dpr)
        {
            return Results.Ok(dpr.GetStatistics());
        }

        public static async Task<IResult> Stop(DishwasherProgramsRepository dpr)
        {
            if (dpr.GetRunning() != null)
            {
                return Results.Ok(dpr.Stop());
            }
            return Results.NotFound();
        }
    }
}
