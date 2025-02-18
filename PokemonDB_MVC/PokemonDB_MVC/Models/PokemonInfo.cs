using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonDB_MVC.Models
{
    public class PokemonInfo
    {
        public int PokedexNo { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Nullable<double> HeightMetres { get; set; }
        public Nullable<double> WeightKG { get; set; }
        public int Total { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAtk { get; set; }
        public int SpDef { get; set; }
        public int Speed { get; set; }
    }
}