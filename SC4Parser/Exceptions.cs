using System;

namespace SC4Parser
{
    /// <summary>
    /// Exception thrown when an Index Entry cannot be found
    /// </summary>
    /// <seealso cref="SC4Parser.DataStructures.IndexEntry"/>
    public class IndexEntryNotFoundException : Exception { }
    /// <summary>
    /// Exception thrown when Database Directory (DBDF) Resource cannot be found
    /// </summary>
    /// <seealso cref="SC4Parser.DataStructures.DatabaseDirectoryResource"/>
    public class DatabaseDirectoryResourceNotFoundException : Exception { }
    /// <summary>
    /// Exception thrown when Subfile cannot be found
    /// </summary>
    /// <remarks>
    /// Inner exception contains specific exception that occured 
    /// </remarks>
    public class SubfileNotFoundException : Exception 
    {
        /// <summary>
        /// Constructor that constructs with an exception message
        /// </summary>
        /// <param name="message">Exception message</param>
        public SubfileNotFoundException(string message)
            : base(message) { }
        /// <summary>
        /// Construcotr that constructs with an exception message and an inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="e">Inner exception</param>
        public SubfileNotFoundException(string message, Exception e)
            : base(message, e) { }
    }
    /// <summary>
    /// Exception thrown when there are issues parsing a Database Packed File (DBPF)
    /// </summary>
    /// <remarks>
    /// Inner exception contains specific exception that occured 
    /// </remarks>
    /// <seealso cref="SC4Parser.Files.DatabasePackedFile"/>
    public class DBPFParsingException : Exception
    {
        /// <summary>
        /// Construcotr that constructs with an exception message and an inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="e">Inner exception</param>
        public DBPFParsingException(string message, Exception e)
            : base(message, e) { }
    }
    /// <summary>
    /// Exception thrown when there is an issue with loading an Index Entry
    /// </summary>
    /// <remarks>
    /// Inner exception contains specific exception that occured 
    /// </remarks>
    /// <seealso cref="SC4Parser.DataStructures.IndexEntry"/>
    public class IndexEntryLoadingException : Exception 
    {
        /// <summary>
        /// Constructor that constructs with an exception message
        /// </summary>
        /// <param name="message">Exception message</param>
        public IndexEntryLoadingException(string message)
            : base(message) { }
        /// <summary>
        /// Construcotr that constructs with an exception message and an inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="e">Inner exception</param>
        public IndexEntryLoadingException(string message, Exception e)
            : base (message, e) { }
    }
    /// <summary>
    /// Exception thrown when an error occurs while preforming a QFS/Refpack decompression
    /// </summary>
    /// <remarks>
    /// Inner exception contains specific exception that occured 
    /// </remarks>
    /// <seealso cref="SC4Parser.Compression.QFS"/>
    public class QFSDecompressionException : Exception 
    {
        /// <summary>
        /// Construcotr that constructs with an exception message and an inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="e">Inner exception</param>
        public QFSDecompressionException(string message, Exception e) 
            : base (message, e) { }
    }

}
