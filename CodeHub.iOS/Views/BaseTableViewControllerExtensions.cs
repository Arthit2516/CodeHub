using System;
using UIKit;
using CoreGraphics;
using CodeHub.iOS.Utilities;
using ReactiveUI;

namespace CodeHub.iOS.Views
{
    public static class BaseTableViewControllerExtensions
    {
        public static ObservableSearchDelegate AddSearchBar(this BaseTableViewController @this)
        {
            var searchBar = new UISearchBar(new CGRect(0f, 0f, 320f, 44f));
            searchBar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

            var searchDelegate = new ObservableSearchDelegate();
            searchBar.Delegate = searchDelegate;

            @this.TableView.TableHeaderView = searchBar;

            return searchDelegate;
        }

        public static ObservableSearchDelegate AddSearchBar(this BaseTableViewController @this, Action<string> searchAction)
        {
            var searchDelegate = AddSearchBar(@this);

            var supportsActivation = @this as IActivatable;
            if (supportsActivation != null)
            {
                supportsActivation.WhenActivated(d =>
                {
                    d(searchDelegate.SearchTextChanging.Subscribe(searchAction));
                });
            }
            else
            {
                searchDelegate.SearchTextChanging.Subscribe(searchAction);
            }

            return searchDelegate;
        }
    }
}
