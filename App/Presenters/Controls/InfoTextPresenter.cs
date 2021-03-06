﻿using System;
using App.Interfaces;
using GuiComponents.Interfaces;

namespace App.Presenters.Controls
{
    public class InfoTextPresenter
    {
        private readonly IInfoTextView _view;
        public readonly IInfoTextModel Model;

        private const string UpdateAvaliable = "Update is avaliable!({0})";
        private const string UpdateError = "Error while checking for updates.";
        private const string UpdateNewestVersion = "No updates avaliable ({0})";

        private const string LoadedBeatmaps = "Loaded {0} beatmaps";
        private const string LoadedCollections = "Loaded {0} collections";
        private const string LoadedCollectionsBeatmaps = "With {0} beatmaps";


        private const string MissingBeatmaps = "Missing {0} beatmaps";

        public InfoTextPresenter(IInfoTextView view, IInfoTextModel model)
        {
            _view = view;
            Model = model;

            Model.CountsUpdated += ModelOnCountsUpdated;
            _view.UpdateTextClicked += ViewOnUpdateTextClicked;
        }

        private void ViewOnUpdateTextClicked(object sender, EventArgs eventArgs)
        {
            var updater = Model.GetUpdater();
            updater.CheckIfUpdateIsAvaliable();

            Model.EmitUpdateTextClicked();

        }


        private void ModelOnCountsUpdated(object sender, EventArgs eventArgs)
        {
            _view.BeatmapLoaded = string.Format(LoadedBeatmaps, Model.BeatmapCount);
            _view.CollectionsLoaded = string.Format(LoadedCollections, Model.CollectionsCount);
            _view.BeatmapsInCollections = string.Format(LoadedCollectionsBeatmaps, Model.BeatmapsInCollectionsCount);
            _view.BeatmapsMissing = string.Format(MissingBeatmaps, Model.MissingBeatmapsCount);
            SetUpdateText();
        }

        private void SetUpdateText()
        {
            var updater = Model.GetUpdater();
            if (updater != null)
            {
                if (updater.IsUpdateAvaliable())
                {
                    _view.UpdateText = string.Format(UpdateAvaliable, updater.newVersion);
                }
                else if (updater.Error)
                {
                    _view.UpdateText = UpdateError;
                }
                else
                {
                    _view.UpdateText = string.Format(UpdateNewestVersion, updater.currentVersion);
                }
            }
            else
            {
                _view.UpdateText = "";
            }
        }
    }
}