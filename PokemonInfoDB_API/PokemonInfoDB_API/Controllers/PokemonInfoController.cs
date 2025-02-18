using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PokemonInfoDB_API.Models;

namespace PokemonInfoDB_API.Controllers
{
    public class PokemonInfoController : ApiController
    {
        [Route("api/PokemonInfo/{pokedexNo}")]
        public PokemonInfoTbl Get(int pokedexNo)
        {
            //create connection to db 
            using (PokemonInfoDB_ConnectionString db = new PokemonInfoDB_ConnectionString())
            {
                PokemonInfoTbl API_result = new PokemonInfoTbl();

                API_result = db.PokemonInfoTbls.FirstOrDefault(s => s.PokedexNo == pokedexNo);

                if (API_result != null)
                {
                    return API_result;
                }
                else
                    return null;
            }
        }
    }
}
