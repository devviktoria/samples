using System.Collections.Generic;

namespace jcsqlserverservice.Models {
    public class User {
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public ICollection<Joke> Jokes { get; set; }
    }
}