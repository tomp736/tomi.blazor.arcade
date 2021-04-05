using System;

namespace tomi.arcade.game.gol.client
{
    public class UriInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Protocol { get; set; }

        public Uri ToUri()
        {
            return new Uri($"{Protocol}://{Host}:{Port}");
        }
    }
}
