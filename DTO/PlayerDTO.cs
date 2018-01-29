using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientShare.DTO
{
    public class PlayerDTO : DTO<PlayerDTO>
    {
        public int PlayerIndex { get; set; }
        public string PlayerName { get; set; }

        public override object[] ToMessageArguments(ref object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
