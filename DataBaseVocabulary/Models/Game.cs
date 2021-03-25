using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseVocabulary.Models
{
    class Game
    {
        public int GameId { get; set; }

        [Required]
        public string UserName { get; set; }

        public int GuesBulgarianWords { get; set; }

        public int GuesEnglishWords { get; set; }

        public int Points { get; set; }
    }
}
