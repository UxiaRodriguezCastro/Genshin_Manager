using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genshintool
{
    public class character
    {
        private String character_name;
        private int character_constelation;
        private int character_level;

        public character() { }

        public character(string character_name, int character_constelation, int character_level)
        {
            this.character_name = character_name;
            this.character_constelation = character_constelation;
            this.character_level = character_level;
        }

        public string Character_name { get => character_name; set => character_name = value; }
        public int Character_constelation { get => character_constelation; set => character_constelation = value; }
        public int Character_level { get => character_level; set => character_level = value; }
    }
}
