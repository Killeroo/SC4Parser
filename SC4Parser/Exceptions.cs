using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser
{
    /// <summary>
    /// Exceptions used by SC4Parser
    /// </summary>
    public class IndexEntryNotFoundException : Exception { }
    public class DatabaseDirectoryResourceNotFoundException : Exception { }
    public class SubfileNotFoundException : Exception 
    {
        public SubfileNotFoundException(string message)
            : base(message) { }
        public SubfileNotFoundException(string message, Exception e)
            : base(message, e) { }
    }
    public class DBPFParsingException : Exception
    {
        public DBPFParsingException(string message, Exception e)
            : base(message, e) { }
    }
    public class IndexEntryLoadingException : Exception 
    {
        public IndexEntryLoadingException(string message)
            : base(message) { }
        public IndexEntryLoadingException(string message, Exception e)
            : base (message, e) { }
    }
    public class QFSDecompressionException : Exception 
    {
        public QFSDecompressionException(string message, Exception e) 
            : base (message, e) { }
    }

}
