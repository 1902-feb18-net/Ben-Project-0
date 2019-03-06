using Project_0.Lib;
using Project_0.Lib.InterfaceRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Context.Repositories
{
    public class GamesRepository : IGamesRepository
    {

        private readonly Project0Context _db;

        public GamesRepository(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<GamesImp> GetAllGames()
        {
            var value = _db.Games;
            return Mapper.Map(value);
        }

        public GamesImp GetGameById(int Id)
        {
            var value = _db.Games.Find(Id);
            return Mapper.Map(value);
        }
    }
}
