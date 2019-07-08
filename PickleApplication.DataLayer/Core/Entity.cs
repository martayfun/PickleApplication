using System;

namespace PickleApplication.DataLayer.Core
{
    public class Entity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
