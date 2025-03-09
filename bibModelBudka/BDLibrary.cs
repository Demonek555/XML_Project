using System;
using System.Collections.Generic;
using System.Text;

namespace bibModelBudka
{
    
    public class BDLibrary
    {
        public readonly string authorsFile, publishersFile, booksFile;
        public BDLibrary(string path, string authorsFile=DefaultFileNamers.Authors, string publishersFile = DefaultFileNamers.Publishers, string booksFile = DefaultFileNamers.Books)
        {
            this.authorsFile = path+authorsFile;
            this.publishersFile = path+publishersFile;
            this.booksFile = path+booksFile;
        }
    }
}
