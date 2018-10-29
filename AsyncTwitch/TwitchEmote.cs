﻿using System;

namespace AsyncTwitch
{
    public struct TwitchEmote
    {
        //The full id of the emote.
        public string Id { get; }

        /*
         * A 2D array of Emote positions
         * The first level contains the instance of the emote so the length of Index is equivalent to how many times the emote appears in chat.
         * Ex: Kappa Hey strimmer Kappa
         * Index[0] is the location of the first instance of Kappa and Index[1] is the location of the second instance of Kappa
         * The second level strictly has a length of 2, with 0 being the start position and 1 being the end position.
         * Going back to our first example:
         * Index[0][0] = 0, Index[0][1] = 4, Index[1][0] = 19, Index[1][1] = 24
         */
        public string[][] Index { get; }

        public TwitchEmote(string id, string[][] index)
        {
            Id = id;
            Index = index;
        }
    }
}