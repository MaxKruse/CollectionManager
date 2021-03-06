﻿using System;
using System.Windows.Forms;
using BrightIdeasSoftware;
using CollectionManager.DataTypes;
using CollectionManagerExtensionsDll.Modules.BeatmapFilter;

namespace App.Misc
{
    public class BeatmapListFilter: IModelFilter
    {
        private readonly BeatmapFilter _beatmapFilter;
        private string _searchString;
        private readonly object _searchStringLockingObject = new object();
        private Timer timer;
        public event EventHandler FilteringStarted;
        public event EventHandler FilteringFinished;
        public BeatmapListFilter(Beatmaps beatmaps)
        {
            _beatmapFilter = new BeatmapFilter(beatmaps,new BeatmapExtension());
            timer = new Timer();
            timer.Interval = 400;
            timer.Tick += Timer_Tick;
        }

        public void SetBeatmaps(Beatmaps beatmaps)
        {
            _beatmapFilter.SetBeatmaps(beatmaps);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (timer)
                if (timer.Enabled)
                    timer.Stop();
            lock (_searchStringLockingObject)
            {
                OnFilteringStarted();
                _beatmapFilter.UpdateSearch(_searchString);
                OnFilteringFinished();
            }
        }
        
        public bool Filter(object modelObject)
        {
            if (modelObject is BeatmapExtension)
            {
                if (_beatmapFilter.BeatmapHashHidden.ContainsKey(((BeatmapExtension)modelObject).Md5))
                    return !_beatmapFilter.BeatmapHashHidden[((BeatmapExtension)modelObject).Md5];
                return true;
            }
            return false;
        }

        public void UpdateSearch(string value)
        {
            lock (_searchStringLockingObject)
                _searchString = value;
            lock (timer)
            {
                if (timer.Enabled)
                    timer.Stop();
                timer.Start();
            }
        }

        protected virtual void OnFilteringFinished()
        {
            FilteringFinished?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFilteringStarted()
        {
            FilteringStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}