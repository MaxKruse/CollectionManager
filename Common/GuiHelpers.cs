﻿using CollectionManager.DataTypes;
using Common;

namespace Gui.Misc
{
    public static class GuiHelpers
    {
        public delegate void BeatmapsEventArgs(object sender, Beatmaps args);
        public delegate void CollectionBeatmapsEventArgs(object sender, Beatmaps args, string collectionName);
        public delegate void BeatmapListingActionArgs(object sender, BeatmapListingAction args);

    }
}