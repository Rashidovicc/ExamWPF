using System;
using examWPF.Domain.Enum;

namespace examWPF.Domain.Commons
{
    public class Auditable
    {
        public long Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        ItemState State { get; set; }
        
        public void Create()
        {
            State = ItemState.ACTIVE;
            CreatedAt = DateTime.Now;
        }

        public void Update()
        {
            State = ItemState.UPDATED;
            UpdatedAt = DateTime.Now;
        }

        public void Delete()
        {
            State = ItemState.DELETED;
            UpdatedAt = DateTime.Now;
        }
    }
}