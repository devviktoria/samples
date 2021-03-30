using System.Collections.Generic;

namespace jcsqlserverservice.Models {
    public class Tag {
        public int TagId { get; set; }
        public string Name { get; set; }

        public ICollection<Joke> Jokes { get; set; }
    }
}