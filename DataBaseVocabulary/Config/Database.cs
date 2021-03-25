using DataBaseVocabulary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Config
{
    static class Database
    {
        public static void CreateDB()
        {
            VocabolaryDBContext context = new VocabolaryDBContext();
            context.Database.EnsureCreated();
        }
        
    }
}
