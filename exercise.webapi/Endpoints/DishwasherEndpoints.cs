using exercise.webapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.webapi.Endpoints
{
    public static class DishwasherEndpoints
    {
        public static void ConfigureDishwasherEndpoints(this WebApplication app)
        {
            var dishwasher = app.MapGroup("dishwasher");

            dishwasher.MapGet("programs", GetPrograms);
            dishwasher.MapPost("programs/{id}", Start);
            dishwasher.MapGet("current", GetCurrent);
            dishwasher.MapGet("history", GetHistory);
            dishwasher.MapGet("stats", GetStatistics);

            //Extensions

            dishwasher.MapDelete("programs", StopProgram);
            //dishwasher.MapGet("")
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetPrograms(IRepository repository)
        {
            var programs = await repository.GetAllAsync();
            return programs.Count() > 0 ? TypedResults.Ok(programs) : TypedResults.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Start(IRepository repository, int id)
        {
            var program = await repository.StartProgram(id);
            return TypedResults.Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetCurrent(IRepository repository)
        {
            var program = repository.GetCurrentProgram();
            return program != null ? TypedResults.Ok(program) : TypedResults.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetHistory(IRepository repository)
        {
            var history =  repository.GetHistory();
            return history.Count() > 0 ? TypedResults.Ok(history) : TypedResults.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetStatistics(IRepository repository)
        {
            var stats = await repository.GetStatistics();
            return TypedResults.Ok(stats);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> StopProgram(IRepository repository)
        {
            await repository.StopProgram();
            return TypedResults.Ok();
        }
    }
}
