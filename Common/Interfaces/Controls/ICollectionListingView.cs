﻿using System;
using System.Collections;
using BrightIdeasSoftware;
using CollectionManager.DataTypes;
using CollectionManager.Modules.CollectionsManager;
using Gui.Misc;

namespace GuiComponents.Interfaces
{
    public interface ICollectionListingView
    {
        string SearchText { get; }

        Collections Collections { set; }
        Collection SelectedCollection { get; set; }
        ArrayList SelectedCollections { get; }

        event EventHandler SearchTextChanged;
        event EventHandler SelectedCollectionChanged;
        event EventHandler SelectedCollectionsChanged;
        event GuiHelpers.CollectionBeatmapsEventArgs BeatmapsDropped;
        event EventHandler<StringEventArgs> RightClick;

        void SetFilter(IModelFilter filter);
        void FilteringStarted();
        void FilteringFinished();
        //void OnCollectionEditing(CollectionEditArgs e);

    }
}