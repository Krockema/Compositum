using System;
using System.Collections.Generic;

namespace Compositum.Database.Tables
{
    /// <summary>
    /// Base Composite Element
    /// </summary>
    public abstract class Composite : BaseEntity
    {
        protected Composite(string name)            
        {
            Name = name;
            Children = new List<Composite>();
        }
        /// <summary>
        /// Name of the Entity 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id of the parent element that is stored in the DB
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// Entity Framework navigation property
        /// </summary>
        public Composite Parent { get; set; }
        /// <summary>
        /// Navigation property for object navigation through the Objects
        /// </summary>
        public ICollection<Composite> Children { get; set; }
        
        /// <summary>
        /// Reference for Predecessor
        /// </summary>
        public int? PredecessorId { get; set; }
        
        /// <summary>
        /// Prerequisite for this item
        /// </summary>
        public Composite Predecessor { get; set; }
        /// <summary>
        /// The following Item 
        /// </summary>
        public Composite Successor { get; set; }

        public void Print()
        {
            Print(0);
        }

        private void Print(int level)
        {
            var padding = "".PadLeft(level,'-');
            var suc = this.Successor is not null   ? (" Suc: " + this.Successor.Name) : " ";
            var pre = this.Predecessor is not null ? (" Pre: " + this.Predecessor.Name) : " ";
            
            Console.WriteLine(padding + this.Name +  " Type: " + this.GetType().Name + suc + pre);
            
            foreach (var compositeChild in this.Children)
            {
                compositeChild.Print(level + 2);
            }
        }
        public abstract IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth);
    }
}