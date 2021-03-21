using Microsoft.EntityFrameworkCore.Migrations;

namespace jcwebapisqlserver.Migrations {
    public partial class UpdateEmotionCounterSP : Migration {
        protected override void Up (MigrationBuilder migrationBuilder) {
            var spSql = @"CREATE PROCEDURE dbo.UpdateEmotionCounters(@jokeId int, @emotion nvarchar(50))
AS

IF (NOT EXISTS (SELECT * FROM dbo.Joke WHERE JokeId = @jokeId))
BEGIN 
    THROW 51000, 'The joke with the given id does not exist.', 1;  
END

BEGIN TRANSACTION;
UPDATE dbo.EmotionCounter
SET Counter = Counter + 1
WHERE JokeId = @jokeId AND Emotion = @emotion 

DECLARE @currentTime AS datetimeoffset = SYSDATETIMEOFFSET();
SET DATEFIRST 1;
DECLARE @weekDay AS int = DATEPART(weekday, @currentTime);

UPDATE dbo.ResponseStatistic
SET Counter = Counter + 1
WHERE JokeId = @jokeId AND [Day] = @weekDay

UPDATE dbo.Joke 
SET ResponseSum = ResponseSum + 1
WHERE JokeId = @jokeId
COMMIT;";
            migrationBuilder.Sql (spSql);
        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            var spSql = "DROP PROCEDURE IF EXISTS dbo.UpdateEmotionCounters;";
            migrationBuilder.Sql (spSql);
        }
    }
}