using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBaseVocabulary.Models
{
    [NotMapped]
    class BulgarianWordEnglishWord
    {
        public int EnglishWordId { get; set; }

        public EnglishWord EnglishWord { get; set; }

        public int BulgarianWordId { get; set; }

        public BulgarianWord BulgarianWord { get; set; }

    }
}
