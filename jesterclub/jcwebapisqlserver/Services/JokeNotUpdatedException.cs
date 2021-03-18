using System;

namespace jcwebapi.Services {
    public class JokeNotUpdatedException : Exception {
        public JokeNotUpdatedException () { }

        public JokeNotUpdatedException (string message) : base (message) { }

        public JokeNotUpdatedException (string message, Exception inner) : base (message, inner) { }
    }

    public class UserNotFoundException : Exception {
        public UserNotFoundException () { }

        public UserNotFoundException (string message) : base (message) { }

        public UserNotFoundException (string message, Exception inner) : base (message, inner) { }
    }
}