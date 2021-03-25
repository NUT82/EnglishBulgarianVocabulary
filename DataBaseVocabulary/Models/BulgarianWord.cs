using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBaseVocabulary.Models
{
    class BulgarianWord
    {
        public BulgarianWord()
        {
            EnglishWords = new HashSet<EnglishWord>();
        }

        public int BulgarianWordId { get; set; }

        [Required]
        public string Word { get; set; }

        public int? Points { get; set; }

        [NotMapped]
        public ICollection<EnglishWord> EnglishWords { get; set; }
    }
}
