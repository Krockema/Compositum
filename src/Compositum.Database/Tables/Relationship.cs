using System.Reflection.Metadata.Ecma335;

namespace Compositum.Database.Tables
{
    public class Relationship : BaseEntity
    {
        public int SuccessorId { get; set; }
        public Composite Successor  { get; set; }
        public int PredecessorId { get; set; }
        public Composite Predecessor  { get; set; }
  
    }
}