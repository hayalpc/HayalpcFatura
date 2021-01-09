using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class BaseVM : IBaseVM
    {
        public virtual long Id { get; set; }

        [DisplayName("VM.CreateTime")]
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;
        [DisplayName("VM.CreateUserId")]
        public virtual long CreateUserId { get; set; } = -1;

        [DisplayName("VM.UpdateTime")]
        public virtual DateTime? UpdateTime { get; set; }
        [DisplayName("VM.UpdateUserId")]
        public virtual long? UpdateUserId { get; set; }
    }

    public interface IBaseVM
    {
        long Id { get; set; }

        DateTime CreateTime { get; set; }
        long CreateUserId { get; set; }

        DateTime? UpdateTime { get; set; }
        long? UpdateUserId { get; set; }
    }
}
