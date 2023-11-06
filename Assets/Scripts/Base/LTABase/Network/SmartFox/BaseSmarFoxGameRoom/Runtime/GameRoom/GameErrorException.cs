using System;
internal class GameErrorException : Exception
{

    public GameErrorException(GameRoom gameRoom) : base("Bon may Code sao Loi roi.Ko phai do Base dau")
    {
        
        gameRoom.LeaveGame();
    }
}
