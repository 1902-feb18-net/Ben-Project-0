using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib.InterfaceRepos
{
    public interface IGamesRepository
    {
        IEnumerable<GamesImp> GetAllGames();

        GamesImp GetGameById(int Id);

    }
}
