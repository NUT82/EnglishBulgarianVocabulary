using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBaseVocabulary.Models
{
    class EnglishWord
    {
        public EnglishWord()
        {
            BulgarianWords = new HashSet<BulgarianWord>();
        }

        public int EnglishWordId { get; set; }

        [Required]
        public string Word { get; set; }

        public int? Points { get; set; }

        [NotMapped]
        public ICollection<BulgarianWord> BulgarianWords { get; set; }
    }
}
