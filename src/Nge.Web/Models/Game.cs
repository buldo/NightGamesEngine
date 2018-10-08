using System;

namespace Nge.Web.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
